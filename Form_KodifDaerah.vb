Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Form_KodifDaerah
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        If Trim(NamaWilayah.Text) = "" Then
            MsgBox("Nama Wilayah tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning!")
            NamaWilayah.Focus()
            Exit Sub
        End If
        If LAdd Then
            SQL = "Select Wilayah 
                 From M_KodeWilayah 
                Where Wilayah = '" & NamaWilayah.Text & "'
                  And aktifYN='Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaWilayah.Focus()
                MsgBox("Nama " & NamaWilayah.Text & " Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            End If
            SQL = "Select KodeWilayah, Wilayah 
                 From M_KodeWilayah 
                Where KodeWilayah = '" & KodeWilayah.Text & "'
                  And aktifYN='Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                KodeWilayah.Focus()
                MsgBox("Kode " & KodeWilayah.Text & " SUDAH digunakan " & dbTable.Rows(0) !NamaWilayah, vbCritical, "Warning!")
                Exit Sub
            End If
            SQL = "INSERT INTO m_KodeWilayah (KodeWilayah, Wilayah, " &
                " AktifYN, UserID, LastUPD, TransferYN) VALUES " &
                "('" & KodeWilayah.Text & "', '" & NamaWilayah.Text & "', " &
                " 'Y', '" & UserID & "',  GetDate(), 'N') "
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update m_KodeWilayah SET " &
                "Wilayah = '" & NamaWilayah.Text & "', " &
                "TransferYN = 'N', " &
                "UserID = '" & UserID & "',  LastUPD = GetDate() " &
                "where KodeWilayah = '" & KodeWilayah.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Sub Data_Record()
        Dim mKondisi As String = ""
        Me.Cursor = Cursors.WaitCursor
        SQL = "SELECT * FROM m_KodeWilayah WHERE aktifyn = 'Y' " &
            "ORDER BY Wilayah "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !KodeWilayah,
                    .Table.Rows(a) !Wilayah, "Edit", "Hapus")
            Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub KodeWilayah_TextChanged(sender As Object, e As EventArgs) Handles KodeWilayah.TextChanged

    End Sub

    Sub Isi_Data()
        SQL = "SELECT * FROM m_KodeWilayah " &
            "WHERE KodeWilayah = '" & KodeWilayah.Text & "' " &
            " AND aktifYN='Y' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            KodeWilayah.Text = dbTable.Rows(0) !KodeBahan
            NamaWilayah.Text = dbTable.Rows(0) !Wilayah
        End If
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        KodeWilayah.ReadOnly = False
        KodeWilayah.Focus()
    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        cmdCetak.Enabled = False
        Cetak()
        cmdCetak.Enabled = True
    End Sub

    Private Sub Cetak()
        Me.Cursor = Cursors.WaitCursor
        Proses.OpenConn(CN)
        dttable = New DataTable
        SQL = "SELECT * FROM m_KodeWilayah " &
            "WHERE AktifYN='Y'  " &
            "ORDER BY Wilayah "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_KodifDaerah
            objRep.SetDataSource(dttable)
            Form_CetakLaporan.Text = "Daftar Wilayah"
            Form_CetakLaporan.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_CetakLaporan.CrystalReportViewer1.Refresh()
            Form_CetakLaporan.CrystalReportViewer1.ReportSource = objRep
            Form_CetakLaporan.CrystalReportViewer1.ShowRefreshButton = False
            Form_CetakLaporan.CrystalReportViewer1.ShowPrintButton = True
            Form_CetakLaporan.CrystalReportViewer1.ShowExportButton = True
            Form_CetakLaporan.CrystalReportViewer1.ShowParameterPanelButton = False
            Form_CetakLaporan.ShowDialog()

            dttable.Dispose()
            DTadapter.Dispose()
            Proses.CloseConn(CN)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
    Private Sub ClearTextBoxes(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        If ctlcol Is Nothing Then ctlcol = Me.Controls
        For Each ctl As Control In ctlcol
            If TypeOf (ctl) Is TextBox Then
                DirectCast(ctl, TextBox).Clear()
            Else
                If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                    ClearTextBoxes(ctl.Controls)
                End If
            End If
        Next
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Enabled = False
        Else
            cmdTambah.Visible = tAktif
        End If
        If tLaporan = False Then
            cmdCetak.Enabled = False
        Else
            cmdCetak.Visible = tAktif
        End If
        cmdSimpan.Visible = Not tAktif
        cmdBatal.Visible = Not tAktif
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If e.RowIndex >= 0 Then
            KodeWilayah.Text = DGView.Rows(e.RowIndex).Cells(0).Value
            NamaWilayah.Text = DGView.Rows(e.RowIndex).Cells(1).Value
        End If
        If e.ColumnIndex = 2 Then 'Edit
            tEdit = Proses.UserAksesTombol(UserID, "23_KODIF_DAERAH", "edit")
            If tEdit = False Then
                MsgBox("Anda tidak punya akses untuk edit data ini !", vbCritical + vbOKOnly, ".:Warning!")
                Exit Sub
            End If
            LAdd = False
            LEdit = True
            AturTombol(False)
            KodeWilayah.ReadOnly = True
            NamaWilayah.Focus()
        ElseIf e.ColumnIndex = 3 Then 'Hapus
            tHapus = Proses.UserAksesTombol(UserID, "23_KODIF_DAERAH", "hapus")
            If tHapus = False Then
                MsgBox("Anda tidak punya akses untuk hapus data ini !", vbCritical + vbOKOnly, ".:Warning!")
                Exit Sub
            End If
            If Trim(KodeWilayah.Text) = "" Then
                MsgBox("Kode Wilayah Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                SQL = "Update M_KodeWilayah Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate(), TransferYN='N' " &
                        "Where KodeWilayah = '" & KodeWilayah.Text & "' "
                Proses.ExecuteNonQuery(SQL)
                ClearTextBoxes()
                Data_Record()
            End If
        End If
    End Sub

    Private Sub Form_KodifDaerah_Load(sender As Object, e As EventArgs) Handles Me.Load
        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Data_Record()

        tTambah = Proses.UserAksesTombol(UserID, "23_KODIF_DAERAH", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "23_KODIF_DAERAH", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "23_KODIF_DAERAH", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "23_KODIF_DAERAH", "laporan")

        AturTombol(True)
    End Sub

    Private Sub KodeWilayah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeWilayah.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then NamaWilayah.Focus()
    End Sub

    Private Sub NamaWilayah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaWilayah.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then cmdSimpan.Focus()
    End Sub
End Class