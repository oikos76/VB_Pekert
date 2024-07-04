Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Rpt_LabaRugi
    Dim SQL As String
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Dim UserID As String = FrmMenuUtama.TsPengguna.Text
    Protected CN As SqlConnection
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database

    Private Function OpenConn() As Boolean
        CN = New SqlConnection

        SQL = "Initial Catalog=" & db & "; " &
            "user id=" & dbUserId & ";password=" & pwd & "; " &
            "Persist Security Info=True;" &
            "Data Source=" & ipserver & ";"

        CN.ConnectionString = SQL

        Try
            If CN.State = ConnectionState.Closed Then
                CN.Open()
                Return True
            Else
                CN.Close()
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try
    End Function
    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
        Panel1.Enabled = False
        Dim Periode1 As String, Periode2 As String, xPeriode As String
        Dim MsgSQL As String, NoUrut As Integer, IDRpt As String
        Dim SaldoAwal As Double, Debet As Double, Kredit As Double, SaldoAkhir As Double, Spasi As Integer

        Dim BiayaPenjualanAwal As Double, BiayaPenjualanAkhir As Double
        Dim BarangTersediaAwal As Double, BarangTersediaAkhir As Double
        Dim HPPAwal As Double, HPPAkhir As Double
        Dim PenjualanAW As Double, PenjualanAK As Double
        Dim BebanAwal As Double, BebanAkhir As Double
        Dim BiayaAwal As Double, BiayaAkhir As Double
        Dim PendapatanAwal As Double, PendapatanAkhir As Double
        Dim Laba As Double, Pajak As Double, X1 As Double, X2 As Double
        Dim LabaAK As Double, PajakAK As Double, Y1 As Double, Y2 As Double
        Dim RS05 As New DataTable, RS04 As New DataTable

        Periode1 = Format(DateAdd("m", -1, Tgl1.Value), "MM-yyyy")
        Periode2 = Format(Tgl1.Value, "MM-yyyy")
        NoUrut = 1
        Randomize()
        Me.Cursor = Cursors.WaitCursor
        cmdCetak.Enabled = False
        IDRpt = Microsoft.VisualBasic.Left(Replace(Trim(Rnd(10000) * 100000), ".", UserID), 30)
        MsgSQL = "Delete tmp_RPT_LabaRugi " &
            "Where IdRPT = '" & IDRpt & "'"
        Proses.ExecuteNonQuery(MsgSQL)
        MsgSQL = "SELECT Urut, Description, KodeGL, ParentYN, Spasi, TUrut, GarisYN " &
        "From TMP_RPT_Laba_Rugi order by urut "
        RS05 = Proses.ExecuteQuery(MsgSQL)

        For a = 0 To RS05.Rows.Count - 1
            Application.DoEvents()
            Spasi = RS05.Rows(a) !Spasi
            'If RS05.Rows(a) !Urut = "301" Then
            '    Debug.Print(RS05.Rows(a) !Description)
            'End If
            status.Text = RS05.Rows(a) !KodeGL & " | " & RS05.Rows(a) !Description
            If RS05.Rows(a) !ParentYN = "Y" Then
                MsgSQL = "INSERT INTO TMP_RPT_LabaRugi(IdRPT, NoUrut, Description, Awal, " &
                    "Debet, Kredit, Akhir, Spasi, TUrut, GarisYN) VALUES ( '" & IDRpt & "', " &
                    "" & RS05.Rows(a) !Urut & ", '" & Space(Spasi) + RS05.Rows(a) !Description & "', " &
                    "0, 0, 0, 0, " & Spasi & ", '" & RS05.Rows(a) !tUrut & "', '" & RS05.Rows(a) !GarisYN & "')"
                Proses.ExecuteNonQuery(MsgSQL)
            Else
                If Trim(RS05.Rows(a) !KodeGL) = "" Then
                    SaldoAwal = 0
                    Debet = 0
                    Kredit = 0
                Else
                    If Trim(RS05.Rows(a) !kodegl) = "60.10.01.01" Then
                        Debug.Print(RS05.Rows(a) !kodegl)
                    End If
                    MsgSQL = "Select isnull(Sum(Saldo),0) Saldo From M_SaldoAwalCompany " &
                        "Where AktifYN = 'Y' " &
                        "  And Periode = '" & Periode1 & "' " &
                        "  And COA like '" & Trim(RS05.Rows(a) !KodeGL) & "%' "
                    SaldoAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

                    MsgSQL = "Select isnull(Sum(Debet),0) as Debet From t_Jurnal " &
                        "Where AktifYN = 'Y' " &
                        "  And Convert(char(8), tanggal,112) Between " &
                        "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' and '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
                        "  And AccountCode like '" & Trim(RS05.Rows(a) !KodeGL) & "%' "
                    Debet = Proses.ExecuteSingleDblQuery(MsgSQL)

                    MsgSQL = "Select isnull(Sum(Kredit),0) as Kredit From t_Jurnal " &
                        "Where AktifYN = 'Y' " &
                        "  And Convert(char(8), tanggal,112) Between " &
                        "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' and '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
                        "  And AccountCode like '" & Trim(RS05.Rows(a) !KodeGL) & "%' "
                    Kredit = Proses.ExecuteSingleDblQuery(MsgSQL)
                End If
                'If RS05.Rows(a) !Urut = "311" Then
                '    Debug.Print(RS05.Rows(a) !Description)
                'End If
                If RS05.Rows(a) !tUrut = "0700" Or RS05.Rows(a) !Urut = "200" Then
                    SaldoAkhir = SaldoAwal + Kredit - Debet
                ElseIf RS05.Rows(a) !Urut = "302" Then
                    SaldoAkhir = SaldoAwal + Debet - Kredit
                    'ElseIf RS05.Rows(a) !Urut = "318" Then
                    '    MsgSQL = "Select isnull(Sum(Saldo),0) Saldo From M_SaldoAwalCompany " &
                    '        "Where AktifYN = 'Y' " &
                    '        "  And Periode = '" & Format(Tgl2.Value, "MM-yyyy") & "' " &
                    '        "  And COA like '" & Trim(RS05.Rows(a) !KodeGL) & "%' "
                    '    SaldoAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)

                    '    MsgSQL = "Select isnull(Sum(Saldo),0) Saldo From M_SaldoAwalCompany " &
                    '        "Where AktifYN = 'Y' " &
                    '        "  And Periode = '" & Format(Tgl1.Value, "MM-yyyy") & "' " &
                    '        "  And COA like '" & Trim(RS05.Rows(a) !KodeGL) & "%' "
                    '    SaldoAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

                    '    Debet = 0
                    '    Kredit = 0
                Else
                    SaldoAkhir = SaldoAwal + Debet - Kredit
                    'SaldoAkhir = Debet - Kredit
                End If
                MsgSQL = "INSERT INTO TMP_RPT_LabaRugi(IdRPT, NoUrut, " &
                    "Description, Awal, Debet, Kredit, Akhir, Spasi, TUrut, GarisYN) VALUES ( " &
                    "'" & IDRpt & "', " & RS05.Rows(a) !Urut & ", '" & Space(Spasi) + RS05.Rows(a) !Description & "', " &
                    " " & SaldoAwal & ", " & Debet & ", " & Kredit & ", " & SaldoAkhir & ", " &
                    " " & Spasi & ", '" & RS05.Rows(a) !tUrut & "', '" & RS05.Rows(a) !GarisYN & "')"
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            NoUrut = NoUrut + 1
        Next a

        MsgSQL = "Select isnull(Awal,0) awal, isnull(Akhir,0) akhir " &
            " From TMP_RPT_LabaRugi " &
            "Where NoUrut = 200 And idrpt = '" & IDRpt & "' "
        RS04 = Proses.ExecuteQuery(MsgSQL)
        If RS04.Rows.Count <> 0 Then
            PenjualanAW = RS04.Rows(0) !awal
            PenjualanAK = RS04.Rows(0) !akhir
        Else
            PenjualanAW = 0
            PenjualanAK = 0
        End If

        'MsgSQL = "UPDATE TMP_RPT_LabaRugi SET akhir = awal + Debet - Kredit"
        'Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Select isnull(sum(awal),0) Awal From TMP_RPT_LabaRugi " &
            "Where NoUrut > 300 and NoUrut < 317 " &
            "  and idrpt = '" & IDRpt & "' "
        BarangTersediaAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Select isnull(sum(Akhir),0) Akhir From TMP_RPT_LabaRugi " &
            "Where NoUrut > 300 and NoUrut < 317 " &
            "  and idrpt = '" & IDRpt & "' "
        BarangTersediaAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)
        If BarangTersediaAkhir = 0 Then BarangTersediaAkhir = BarangTersediaAwal
        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & BarangTersediaAwal & ", " &
            "Akhir = " & BarangTersediaAkhir & " " &
            "Where NoUrut = 317 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        'MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = Awal * -1, " &
        '    "Akhir = Akhir * -1 " &
        '    "Where NoUrut = 318  " &
        '    "  And idrpt = '" & IDRpt & "' "
        'Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Select isnuLl(sum(awal),0) Awal From TMP_RPT_LabaRugi " &
        "Where NoUrut between 317 and 318 " &
        "  and idrpt = '" & IDRpt & "' "
        HPPAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Select isnull(sum(Akhir),0) Akhir From TMP_RPT_LabaRugi " &
            "Where NoUrut between 317 and 318 " &
            "  and idrpt = '" & IDRpt & "' "
        HPPAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & HPPAwal & ", " &
            "Akhir = " & HPPAkhir & " " &
            "Where NoUrut = 319 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)


        MsgSQL = "Select isnull(sum(awal), 0) Awal From TMP_RPT_LabaRugi " &
        "Where NoUrut between 420 and 449 " &
        "  and idrpt = '" & IDRpt & "' "
        BiayaPenjualanAwal = Proses.ExecuteSingleDblQuery(MsgSQL)


        MsgSQL = "Select isnull(sum(Akhir), 0) Akhir From TMP_RPT_LabaRugi " &
            "Where NoUrut between 420 and 449 " &
            "  and idrpt = '" & IDRpt & "' "
        BiayaPenjualanAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & HPPAwal - BiayaPenjualanAwal & ", " &
            "Akhir = " & HPPAkhir - BiayaPenjualanAkhir & " " &
            "Where NoUrut = 450 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        "Awal = " & PenjualanAW - (HPPAwal) & ", " &
        "Akhir = " & PenjualanAK - (HPPAkhir) & " " &
        "Where NoUrut = 350 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Select isnull(sum(Awal),0) Awal From TMP_RPT_LabaRugi " &
        "Where NoUrut >= 700 and NoUrut <= 1099 and idrpt = '" & IDRpt & "' "
        BebanAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Select isnull(sum(Akhir),0) Akhir From TMP_RPT_LabaRugi " &
            "Where NoUrut >= 700 and NoUrut <= 1099 and idrpt = '" & IDRpt & "' "
        BebanAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & BebanAwal & ", " &
            "Akhir = " & BebanAkhir & " " &
            "Where NoUrut = 1100 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)


        MsgSQL = "Select isnull(sum(Awal),0) awal From TMP_RPT_LabaRugi " &
            "Where NoUrut between 1400 and 1499 and idrpt = '" & IDRpt & "' "
        PendapatanAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Select isnull(sum(Akhir),0) Akhir From TMP_RPT_LabaRugi " &
            "Where NoUrut between 1400 and 1499 and idrpt = '" & IDRpt & "' "
        PendapatanAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & PendapatanAwal & ", " &
            "Akhir = " & PendapatanAkhir & " " &
            "Where NoUrut = 1500 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Select isnull(Sum(Awal),0) Awal From TMP_RPT_LabaRugi " &
        "Where NoUrut between 1700 and 1799  and idrpt = '" & IDRpt & "' "
        BiayaAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

        MsgSQL = "Select isnull(Sum(Akhir),0) Akhir From TMP_RPT_LabaRugi " &
            "Where NoUrut between 1700 and 1799 and idrpt = '" & IDRpt & "' "
        BiayaAkhir = Proses.ExecuteSingleDblQuery(MsgSQL)



        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & BiayaAwal & ", " &
            "Akhir = " & BiayaAkhir & " " &
            "Where NoUrut = 1800 " &
            "  And idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)
        'tambahan request dari bu indri per tgl 28/10/14
        'MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '"Akhir = kredit " &
        '"Where NoUrut = 200 and idrpt = '" & IDRpt & "' "
        'Proses.ExecuteNonQuery(MsgSQL)
        ' end of tambahan request dari bu indri per tgl 28/10/14

        Laba = PenjualanAW - (HPPAwal + BiayaPenjualanAwal) - BebanAwal + PendapatanAwal - BiayaAwal
        LabaAK = PenjualanAK - (HPPAkhir + BiayaPenjualanAkhir) - BebanAkhir + PendapatanAkhir - BiayaAkhir



        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & Laba & ", " &
            "Akhir = " & LabaAK & " " &
            "Where NoUrut = 1900 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        If PenjualanAW <= 4800000000.0# Then
            Pajak = 0.5 * 0.25 * Laba
        Else
            X1 = 4800000000.0# / PenjualanAW * Laba
            X2 = Laba - X1
            Pajak = (0.5 * 0.25 * X1) + (0.25 * X2)
        End If

        If PenjualanAK <= 4800000000.0# Then
            PajakAK = 0.5 * 0.25 * LabaAK
        Else
            Y1 = 4800000000.0# / PenjualanAK * LabaAK
            Y2 = LabaAK - Y1
            PajakAK = (0.5 * 0.25 * Y1) + (0.25 * Y2)
        End If


        If Format(Tgl1.Value, "yyyyMM") <= "202301" Then
            Pajak = 0
            PajakAK = 0
        End If
        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
            "Awal = " & Pajak & ", " &
            "Akhir = " & PajakAK & " " &
            "Where NoUrut = 1910 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        "Awal = " & Laba - Pajak & ", " &
        "Akhir = " & LabaAK - PajakAK & " " &
        "Where NoUrut = 1920 and idrpt = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)


        Call OpenConn()
        dttable = New DataTable
        xPeriode = Format(Tgl1.Value, "dd-MMM-yy") & " s.d. " & Format(Tgl2.Value, "dd-MMM-yy")
        MsgSQL = "SELECT TMP_RPT_LabaRugi.* " &
            "FROM Pekerti.dbo.TMP_RPT_LabaRugi TMP_RPT_LabaRugi " &
            "Where IDRpt = '" & IDRpt & "' " &
            "Order By NoUrut "

        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New RPT_LabaRugiD
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Periode", xPeriode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            ' CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
            CrystalReportViewer1.Refresh()
            CrystalReportViewer1.ReportSource = objRep
            dttable.Dispose()
            DTadapter.Dispose()
            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
        Panel1.Enabled = True
        status.Text = ""
        cmdCetak.Enabled = True
        Me.Cursor = Cursors.Default
        MsgSQL = "Delete tmp_RPT_LabaRugi " &
            "Where IdRPT = '" & IDRpt & "'"
        Proses.ExecuteNonQuery(MsgSQL)
    End Sub

    Private Sub Rpt_LabaRugi_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mPeriode As String = ""
        SQL = " SELECT periode FROM M_SaldoAwalCompany "
        mPeriode = Proses.ExecuteSingleStrQuery(SQL)
        If mPeriode <> "" Then
            mPeriode = Mid(mPeriode, 4, 4) + "-" + Mid(mPeriode, 1, 3) + "01"
            Dim oDate As DateTime = Convert.ToDateTime(mPeriode)
            Tgl1.Value = DateAdd(DateInterval.Month, 1, oDate)
        Else
            Tgl1.Value = Now
        End If
        status.Text = ""
        CrystalReportViewer1.Zoom(1)
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
    End Sub
End Class