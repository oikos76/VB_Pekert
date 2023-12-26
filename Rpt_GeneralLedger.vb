Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Rpt_GeneralLedger

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

    Private Sub Rpt_GeneralLedger_Load(sender As Object, e As EventArgs) Handles Me.Load
        Tgl1.Value = DateAdd(DateInterval.Month, -3, Now)
        Tgl2.Value = Now
        cmbJenisLaporan.Items.Clear()
        cmbJenisLaporan.Items.Add("1. BUKU BESAR")
        cmbJenisLaporan.Items.Add("2. Per Kode GL")
        AccCode1.Text = ""
        KetAccCode1.Text = ""
        AccCode1.Visible = False
    End Sub

    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
    End Sub


    Private Sub Ledger()
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

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        Ledger()
    End Sub

    Private Sub AccCode1_TextChanged(sender As Object, e As EventArgs) Handles AccCode1.TextChanged
        If Len(AccCode1.Text) <= 1 Then
            KetAccCode1.Text = ""
        End If
    End Sub

    Private Sub AccCode1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AccCode1.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim MsgSQL As String, RS01 As New DataTable
            MsgSQL = "Select * From M_PERKIRAAN " &
                    "where aktifYN = 'Y' " &
                    " And NO_PERKIRAAN = '" & AccCode1.Text & "'"
            RS01 = Proses.ExecuteQuery(MsgSQL)
            If RS01.Rows.Count <> 0 Then
                AccCode1.Text = Trim(RS01.Rows(0) !no_PERKIRAAN)
                KetAccCode1.Text = Trim(RS01.Rows(0) !NM_PERKIRAAN)
                cmdCetak.Focus()
            Else
                MsgSQL = "Select * From M_PERKIRAAN " &
                    "Where AktifYN = 'Y' " &
                    "  And (NM_PERKIRAAN like '%" & AccCode1.Text & "%' or " &
                    "      NO_PERKIRAAN like '%" & AccCode1.Text & "%') " &
                    " Order By NO_PERKIRAAN, NM_PERKIRAAN "
                Form_Daftar.txtQuery.Text = MsgSQL
                Form_Daftar.Text = "Daftar Kode GL"
                Form_Daftar.ShowDialog()
                AccCode1.Text = FrmMenuUtama.TSKeterangan.Text
                MsgSQL = "Select * From M_PERKIRAAN " &
                    "where aktifYN = 'Y' " &
                    " And NO_PERKIRAAN = '" & AccCode1.Text & "'"
                RS01 = Proses.ExecuteQuery(MsgSQL)
                If RS01.Rows.Count <> 0 Then
                    AccCode1.Text = Trim(RS01.Rows(0) !no_PERKIRAAN)
                    KetAccCode1.Text = Trim(RS01.Rows(0) !NM_PERKIRAAN)
                    cmdCetak.Focus()
                Else
                    MsgBox("Account Code tidak ditemukan!", vbQuestion + vbCritical, ".:Warning !")
                    AccCode1.Text = ""
                    AccCode1.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmbJenisLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisLaporan.SelectedIndexChanged
        If Mid(cmbJenisLaporan.Text, 1, 1) = "2" Then
            AccCode1.Visible = True
        Else
            AccCode1.Visible = False
        End If
    End Sub
End Class