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

            If rs05.Rows(i) !NoUrut > 15500 Then
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
        MsgSQL = "SELECT NoUrut, KodeGL, Description, Awal, Debet, Kredit, Akhir, Level, BoldYN " &
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

End Class