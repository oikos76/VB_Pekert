Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Rpt_Jurnal
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

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
        DaftarJurnal()
    End Sub

    Private Sub DaftarJurnal()
        Dim Proses As New ClsKoneksi
        Dim Periode As String = "", mKondisi As String = ""
        cmdCetak.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        If Format(Tgl1.Value, "yyMMdd") = Format(Tgl2.Value, "yyMMdd") Then
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy")
        Else
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy") + " s.d " +
                      Format(Tgl2.Value, "dd MMM yyyy")
        End If
        If cmbJenisLaporan.Text <> "" Then
            mKondisi = " AND JenisJurnal = '" & cmbJenisLaporan.Text & "' "
        End If
        Call OpenConn()
        dttable = New DataTable
        SQL = "SELECT * FROM T_JURNAL " &
            "WHERE AKTIFYN = 'Y' " &
            " AND Convert(varchar(8), tanggal, 112) Between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
             "      And '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
             " " & mKondisi & " " &
            "ORDER BY tanggal, idrec, NoUrut "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_Jurnal_All
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Periode", Periode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            'CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
            CrystalReportViewer1.Refresh()
            CrystalReportViewer1.ReportSource = objRep
            dttable.Dispose()
            DTadapter.Dispose()
            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
        Me.Cursor = Cursors.Default
        cmdCetak.Enabled = True
    End Sub
    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
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
    Private Sub Rpt_Jurnal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Tgl1.Value = DateAdd(DateInterval.Month, -3, Now)
        Tgl2.Value = Now
        cmbJenisLaporan.Items.Clear()
        cmbJenisLaporan.Items.Add("JURNAL KELUAR")
        cmbJenisLaporan.Items.Add("JURNAL MASUK")
        cmbJenisLaporan.Items.Add("JURNAL UMUM")
        cmbJenisLaporan.Items.Add("")
    End Sub
End Class