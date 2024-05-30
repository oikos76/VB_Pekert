Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Rpt_Neraca
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

    Private Sub Rpt_Neraca_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Tgl2.Value = Now
        CrystalReportViewer1.Zoom(1)
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
        COA.Text = ""
    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
        CetakNeraca()
    End Sub
    Private Sub CetakNeraca()
        Dim Periode1 As String, Periode2 As String, xPeriode As String
        Dim MsgSQL As String, NoUrut As Integer, IDRpt As String
        Dim SaldoAwal As Double, Debet As Double, Kredit As Double, SaldoAkhir As Double
        Dim ProfitLost As Double, tLaba1 As Double, tPajak1 As Double, tLaba2 As Double, tPajak2 As Double
        Dim rs04 As New DataTable, rs05 As New DataTable

        cmdCetak.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Periode1 = Format(DateAdd("m", -1, Tgl1.Value), "MM-yyyy")
        Periode2 = Format(Tgl1.Value, "MM-yyyy")
        NoUrut = 1
        Randomize()
        IDRpt = Microsoft.VisualBasic.Left(Replace(Trim(Rnd(10000) * 100000), ".", UserID), 30)
        MsgSQL = "Delete tmp_RPT_Neraca Where IdRPT = '" & IDRpt & "' "
        Proses.ExecuteNonQuery(MsgSQL)
        '/****** Script for SelectTopNRows command from SSMS  ******/
        'Select Top 1000 [NO_PERKIRAAN]      ,[NO_SUB]      ,[NM_PERKIRAAN]      ,[AKTIFYN]      ,[LASTUPD]      ,[SAkhir]
        '      From [Pekerti].[dbo].[m_Perkiraan]
        'Where no_sub = '10.10.15.03'
        'order by NO_PERKIRAAN
        '
        MsgSQL = "SELECT NoUrut, coalesce(Description, '') Description, KodeGL, Level, BoldYN, TotalYN " &
            "From TMP_RPTNeraca order by NoUrut "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To rs05.Rows.Count - 1
            Application.DoEvents()
            If rs05.Rows(i) !NoUrut = 133 Then
                Debug.Print(rs05.Rows(i) !Description)
            End If
            COA.Text = IIf(rs05.Rows(i) !Description = "", rs05.Rows(i) !Description + " " + rs05.Rows(i) !KodeGL, rs05.Rows(i) !Description)

            If Trim(rs05.Rows(i) !KodeGL) = "" Then
                SaldoAwal = 0
                Debet = 0
                Kredit = 0
            Else
                MsgSQL = "Select coalesce(Sum(Saldo),0) Saldo From M_SaldoAwalCompany " &
                    "Where AktifYN = 'Y' " &
                    "  And Periode = '" & Periode1 & "' " &
                    "  And COA like '" & Trim(rs05.Rows(i) !KodeGL) & "%' "
                SaldoAwal = Proses.ExecuteSingleDblQuery(MsgSQL)

                MsgSQL = "Select coalesce(Sum(Debet),0) as Debet From t_Jurnal " &
                    "Where AktifYN = 'Y' " &
                    "  And Convert(char(6), tanggal,112) Between " &
                    "      '" & Format(Tgl1.Value, "yyyyMM") & "' and '" & Format(Tgl2.Value, "yyyyMM") & "' " &
                    "  And AccountCode like '" & rs05.Rows(i) !KodeGL & "%' "
                Debet = Proses.ExecuteSingleDblQuery(MsgSQL)

                MsgSQL = "Select coalesce(Sum(Kredit),0) as Kredit From t_Jurnal " &
                    "Where AktifYN = 'Y' " &
                    "  And Convert(char(6), tanggal,112) Between " &
                    "      '" & Format(Tgl1.Value, "yyyyMM") & "' and '" & Format(Tgl2.Value, "yyyyMM") & "' " &
                    "  And AccountCode like '" & rs05.Rows(i) !KodeGL & "%' "
                Kredit = Proses.ExecuteSingleDblQuery(MsgSQL)
            End If

            If rs05.Rows(i) !NoUrut > 151 Then
                SaldoAkhir = SaldoAwal + Kredit - Debet
            Else
                SaldoAkhir = SaldoAwal + Debet - Kredit
            End If
            NoUrut = rs05.Rows(i) !NoUrut

            MsgSQL = "INSERT INTO TMP_RPT_Neraca (IdRPT, NoUrut, Description, " &
                "Awal, Debet, Kredit, Akhir, Level, BoldYN, TotalYN, KodeGL) VALUES ( " &
                "'" & IDRpt & "', " & NoUrut & ", '" & rs05.Rows(i) !Description & "', " &
                " " & SaldoAwal & ", " & Debet & ", " & Kredit & ", " & SaldoAkhir & ", " &
                " " & rs05.Rows(i) !Level & ", '" & rs05.Rows(i) !BoldYN & "', " &
                " '" & rs05.Rows(i) !totalYN & "', '" & rs05.Rows(i) !KodeGL & "') "
            Proses.ExecuteNonQuery(MsgSQL)

        Next i

        tLaba1 = 0
        tPajak1 = 0
        tLaba2 = 0
        tPajak2 = 0
        ProfitLost = LabaRugi(tLaba1, tPajak1, tLaba2, tPajak2)
        'If Format(Tgl1.Value, "MM-YYYY") = "01-2015" Then
        '    MsgSQL = "Select * From m_SaldoAwalCompany " &
        '    "Where Periode = '" & Periode1 & "' " &
        '    "  and AktifYN = 'Y' " &
        '    "  and COA = '30.10.02.01.002' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        '    If Not RS04.EOF Then
        '        tLaba1 = RS04!Saldo
        '    End If
        '    RS04.Close
        'End If

        MsgSQL = "Update TMP_RPT_Neraca set Awal = " & tLaba1 & ", Akhir = " & tLaba2 & " " &
            "Where idrpt = '" & IDRpt & "' and KodeGL = '30.10.02.01.002' "
        Proses.ExecuteNonQuery(MsgSQL)

        'MsgSQL = "Update tmp_RPT_Neraca Set " &
        '    " Awal = Awal * -1, " &
        '    "Akhir = Akhir * -1 " &
        '    "Where NoUrut between 133 and 135 " &
        '    "  and idrpt = '" & IDRpt & "' "  
        'Proses.ExecuteNonQuery(MsgSQL) PENYUSUTAN Wajib Minus

        MsgSQL = "SELECT * FROM TMP_RPTNeraca Where TotalYN = 'Y' order by NoUrut "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To rs05.Rows.Count - 1
            '        If RS05!NoUrut = 109 Or RS05!NoUrut = 110 Then
            '            Debug.Print RS05!Description
            '        End If
            Debug.Print(rs05.Rows(i) !NoUrut)
            MsgSQL = "Select coalesce(Sum(Awal),0) Awal, coalesce(Sum(Akhir),0) Akhir " &
                " From TMP_RPT_Neraca " &
                "Where NoUrut between " & rs05.Rows(i) !TAwal & " and " & rs05.Rows(i) !TAkhir & " " &
                "  and idrpt = '" & IDRpt & "' and TotalYN = 'N' "
            rs04 = Proses.ExecuteQuery(MsgSQL)
            For j = 0 To rs04.Rows.Count - 1
                MsgSQL = "Update tmp_RPT_Neraca Set " &
                    " Awal = " & rs04.Rows(j) !awal & ", " &
                    "Akhir = " & rs04.Rows(j) !akhir & " " &
                    "Where NoUrut = " & rs05.Rows(i) !NoUrut & " " &
                    "  and idrpt = '" & IDRpt & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next j
        Next i
        '159=160-131
        COA.Text = ""
        Call OpenConn()
        dttable = New DataTable
        xPeriode = Format(Tgl1.Value, "dd-MMM-yy") & " s.d. " & Format(Tgl2.Value, "dd-MMM-yy")
        MsgSQL = "SELECT NoUrut, KodeGL, Description, Awal, Debet, Kredit, Akhir " &
            " FROM Pekerti.dbo.TMP_RPT_Neraca TMP_RPT_Neraca " &
            "Where IDRpt = '" & IDRpt & "' " &
            "Order By NoUrut "
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New RPt_NeracaPL
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Periode", xPeriode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            ' CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
            CrystalReportViewer1.Refresh()
            CrystalReportViewer1.ReportSource = objRep
            CrystalReportViewer1.Zoom(1)
            dttable.Dispose()
            DTadapter.Dispose()
            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
        Me.Cursor = Cursors.Default
        cmdCetak.Enabled = True
        MsgSQL = "DELETE tmp_RPT_Neraca WHERE IdRPT = '" & IDRpt & "'"
        Proses.ExecuteNonQuery(MsgSQL)
    End Sub

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

    Private Function LabaRugi(ByRef tLaba1 As Double, ByRef tPajak1 As Double,
    ByRef tLaba2 As Double, ByRef tPajak2 As Double)
        Dim Periode1 As String, Periode2 As String
        Dim MsgSQL As String, NoUrut As Integer, IDRpt As String
        Dim SaldoAwal As Double, Debet As Double, Kredit As Double, SaldoAkhir As Double
        Dim Penjualan As Double, BiayaPenjualan As Double, Spasi As Integer
        Dim BiayaPenjualanAwal As Double, BiayaPenjualanAkhir As Double
        Dim BarangTersediaAwal As Double, BarangTersediaAkhir As Double
        Dim HPPAwal As Double, HPPAkhir As Double
        Dim BebanAwal As Double, BebanAkhir As Double
        Dim BiayaAwal As Double, BiayaAkhir As Double
        Dim PendapatanAwal As Double, PendapatanAkhir As Double

        '    Periode1 = Format(DateAdd("m", -1, Tgl1.Value), "MM-YYYY")
        '    Periode2 = Format(Tgl1.Value, "MM-YYYY")
        '    NoUrut = 1
        '    Randomize()
        '    IDRpt = Left(Replace(Trim(Rnd(10000) * 100000), ".", UserID), 30)
        '    MsgSQL = "Delete tmp_RPT_LabaRugi " &
        '    "Where IdRPT = '" & IDRpt & "'"
        '    ConnSQL.Execute MsgSQL
        'MsgSQL = "SELECT Urut, Description, KodeGL, ParentYN, Spasi, tUrut, GarisYN " &
        '    "From TMP_RPT_Laba_Rugi order by urut "
        '    RS05.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'Do While Not RS05.EOF
        '        DoEvents
        '        Spasi = RS05!Spasi
        '        If RS05!ParentYN = "Y" Then
        '            MsgSQL = "INSERT INTO TMP_RPT_LabaRugi(IdRPT, NoUrut, " &
        '            "Description, Awal, Debet, Kredit, Akhir, Spasi, TUrut, GarisYN) VALUES ( " &
        '            "'" & IDRpt & "', " & RS05!Urut & ", '" & Space(Spasi) + RS05!Description & "', " &
        '            "0, 0, 0, 0, " & Spasi & ", '" & RS05!tUrut & "', '" & RS05!GarisYN & "')"
        '            ConnSQL.Execute MsgSQL
        '    Else
        '            If Trim(RS05!KodeGL) = "" Then
        '                SaldoAwal = 0
        '                Debet = 0
        '                Kredit = 0
        '            Else
        '                MsgSQL = "Select Sum(Saldo) Saldo From M_SaldoAwalCompany " &
        '                "Where AktifYN = 'Y' " &
        '                "  And Periode = '" & Periode1 & "' " &
        '                "  And COA like '" & Trim(RS05!KodeGL) & "%' "
        '                RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        '            If Not RS04.EOF Then
        '                    SaldoAwal = IIf(IsNull(RS04!Saldo), 0, RS04!Saldo)
        '                Else
        '                    SaldoAwal = 0
        '                End If
        '                RS04.Close


        '                MsgSQL = "Select Sum(Debet) as Debet From t_Jurnal " &
        '                "Where AktifYN = 'Y' " &
        '                "  And Convert(char(6), tanggal,112) Between " &
        '                "      '" & Format(Tgl1.Value, "YYYYMM") & "' and '" & Format(Tgl2.Value, "YYYYMM") & "' " &
        '                "  And AccountCode like '" & Trim(RS05!KodeGL) & "%' "
        '                RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        '            If Not RS04.EOF Then
        '                    Debet = IIf(IsNull(RS04!Debet), 0, RS04!Debet)
        '                Else
        '                    Debet = 0
        '                End If
        '                RS04.Close

        '                MsgSQL = "Select Sum(Kredit) as Kredit From t_Jurnal " &
        '                "Where AktifYN = 'Y' " &
        '                "  And Convert(char(6), tanggal,112) Between " &
        '                "      '" & Format(Tgl1.Value, "YYYYMM") & "' and '" & Format(Tgl2.Value, "YYYYMM") & "' " &
        '                "  And AccountCode like '" & Trim(RS05!KodeGL) & "%' "
        '                RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        '            If Not RS04.EOF Then
        '                    Kredit = IIf(IsNull(RS04!Kredit), 0, RS04!Kredit)
        '                Else
        '                    Kredit = 0
        '                End If
        '                RS04.Close
        '            End If
        '            SaldoAkhir = SaldoAwal + Debet - Kredit
        '            MsgSQL = "INSERT INTO TMP_RPT_LabaRugi(IdRPT, NoUrut, " &
        '            "Description, Awal, Debet, Kredit, Akhir, Spasi, TUrut, GarisYN) VALUES ( " &
        '            "'" & IDRpt & "', " & RS05!Urut & ", '" & Space(Spasi) + RS05!Description & "', " &
        '            " " & SaldoAwal & ", " & Debet & ", " & Kredit & ", " & SaldoAkhir & ", " &
        '            " " & Spasi & ", '" & RS05!tUrut & "', '" & RS05!GarisYN & "')"
        '            ConnSQL.Execute MsgSQL
        '    End If

        '        RS05.MoveNext
        '        NoUrut = NoUrut + 1
        '    Loop
        '    RS05.Close
        '    MsgSQL = "Select awal, Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut = 200 and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        PenjualanAK = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '        PenjualanAW = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        PenjualanAW = 0
        '        PenjualanAK = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Select sum(awal) Awal From TMP_RPT_LabaRugi " &
        '    "Where NoUrut > 300 and NoUrut < 317 " &
        '    "  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BarangTersediaAwal = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        BarangTersediaAwal = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Select sum(Akhir) Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut > 300 and NoUrut < 317 " &
        '    "  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BarangTersediaAkhir = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '    Else
        '        BarangTersediaAkhir = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & BarangTersediaAwal & ", " &
        '    "Akhir = " & BarangTersediaAkhir & " " &
        '    "Where NoUrut = 317 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL

        'MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = Awal * -1, " &
        '    "Akhir = Akhir * -1 " &
        '    "Where NoUrut = 318  " &
        '    "  And idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL


        'MsgSQL = "Select sum(awal) Awal From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 317 and 318 " &
        '    "  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        HPPAwal = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        HPPAwal = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Select sum(Akhir) Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 317 and 318 " &
        '    "  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        HPPAkhir = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '    Else
        '        HPPAkhir = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & HPPAwal & ", " &
        '    "Akhir = " & HPPAkhir & " " &
        '    "Where NoUrut = 319 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL


        'MsgSQL = "Select sum(awal) Awal From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 320 and 329 " &
        '    "  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BiayaPenjualanAwal = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        BiayaPenjualanAwal = 0
        '    End If
        '    RS04.Close


        '    MsgSQL = "Select sum(Akhir) Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 320 and 329 " &
        '    "  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BiayaPenjualanAkhir = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '    Else
        '        BiayaPenjualanAkhir = 0
        '    End If
        '    RS04.Close


        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & HPPAwal - BiayaPenjualanAwal & ", " &
        '    "Akhir = " & HPPAkhir - BiayaPenjualanAkhir & " " &
        '    "Where NoUrut = 400 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL


        'MsgSQL = "Select sum(Awal) Awal From TMP_RPT_LabaRugi " &
        '    "Where NoUrut >= 700 and NoUrut <= 1099 and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BebanAwal = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        BebanAwal = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Select sum(Akhir) Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut >= 700 and NoUrut <= 1099 and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BebanAkhir = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '    Else
        '        BebanAkhir = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & BebanAwal & ", " &
        '    "Akhir = " & BebanAkhir & " " &
        '    "Where NoUrut = 1100 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL


        'MsgSQL = "Select sum(Awal) Awal From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 1400 and 1499 and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        PendapatanAwal = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        PendapatanAwal = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Select sum(Akhir) Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 1400 and 1499 and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        PendapatanAkhir = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '    Else
        '        PendapatanAkhir = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & PendapatanAwal & ", " &
        '    "Akhir = " & PendapatanAkhir & " " &
        '    "Where NoUrut = 1500 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL

        'MsgSQL = "Select Sum(Awal) Awal From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 1700 and 1799 and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BiayaAwal = IIf(IsNull(RS04!awal), 0, RS04!awal)
        '    Else
        '        BiayaAwal = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Select Sum(Akhir) Akhir From TMP_RPT_LabaRugi " &
        '    "Where NoUrut between 1700 and 1799  and idrpt = '" & IDRpt & "' "
        '    RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'If Not RS04.EOF Then
        '        BiayaAkhir = IIf(IsNull(RS04!akhir), 0, RS04!akhir)
        '    Else
        '        BiayaAkhir = 0
        '    End If
        '    RS04.Close

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & BiayaAwal & ", " &
        '    "Akhir = " & BiayaAkhir & " " &
        '    "Where NoUrut = 1800 " &
        '    "  And idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL


        'MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & Penjualan - (HPPAwal + BiayaPenjualanAwal) - BebanAwal + PendapatanAwal - BiayaAwal & ", " &
        '    "Akhir = " & Penjualan - (HPPAkhir + BiayaPenjualanAkhir) - BebanAkhir + PendapatanAkhir - BiayaAkhir & " " &
        '    "Where NoUrut = 1900 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL


        'Laba = PenjualanAW - (HPPAwal + BiayaPenjualanAwal) - BebanAwal + PendapatanAwal - BiayaAwal
        '    LabaAK = PenjualanAK - (HPPAkhir + BiayaPenjualanAkhir) - BebanAkhir + PendapatanAkhir - BiayaAkhir

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & Laba & ", " &
        '    "Akhir = " & LabaAK & " " &
        '    "Where NoUrut = 1900 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL

        'If PenjualanAW <= 4800000000.0# Then
        '        Pajak = 0.5 * 0.25 * Laba
        '    Else
        '        X1 = 4800000000.0# / PenjualanAW * Laba
        '        X2 = Laba - X1
        '        Pajak = (0.5 * 0.25 * X1) + (0.25 * X2)
        '    End If

        '    If PenjualanAK <= 4800000000.0# Then
        '        PajakAK = 0.5 * 0.25 * LabaAK
        '    Else
        '        Y1 = 4800000000.0# / PenjualanAK * LabaAK
        '        Y2 = LabaAK - Y1
        '        PajakAK = (0.5 * 0.25 * Y1) + (0.25 * Y2)
        '    End If

        '    MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & Pajak & ", " &
        '    "Akhir = " & PajakAK & " " &
        '    "Where NoUrut = 1910 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL

        'MsgSQL = "Update TMP_RPT_LabaRugi Set " &
        '    "Awal = " & Laba - Pajak & ", " &
        '    "Akhir = " & LabaAK - PajakAK & " " &
        '    "Where NoUrut = 1920 and idrpt = '" & IDRpt & "' "
        '    ConnSQL.Execute MsgSQL

        'Laba = Laba - Pajak
        '    LabaAK = LabaAK - PajakAK

        '    tLaba1 = Laba
        '    tPajak1 = Pajak

        '    tLaba2 = LabaAK
        '    tPajak2 = PajakAK

        '    MsgSQL = "Delete tmp_RPT_LabaRugi " &
        '    "Where IdRPT = '" & IDRpt & "'"
        '    ConnSQL.Execute MsgSQL
    End Function

End Class