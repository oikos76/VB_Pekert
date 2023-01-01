Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Form_KodifFungsi
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
        If Trim(NamaIndonesia.Text) = "" Then
            MsgBox("Nama Indonesia tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning!")
            NamaIndonesia.Focus()
            Exit Sub
        End If
        If LAdd Then
            SQL = "Select NamaIndonesia 
                 From M_KodeFungsi 
                Where NamaIndonesia = '" & NamaIndonesia.Text & "'
                  And aktifYN='Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaIndonesia.Focus()
                MsgBox("Nama " & NamaIndonesia.Text & " Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            End If

            SQL = "Select KodeFungsi, NamaIndonesia 
                 From M_KodeFungsi 
                Where KodeFungsi = '" & KodeFungsi.Text & "'
                  And aktifYN='Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                KodeFungsi.Focus()
                MsgBox("Kode " & KodeFungsi.Text & " SUDAH digunakan " & dbTable.Rows(0) !namaindonesia, vbCritical, "Warning!")
                Exit Sub
            End If

            SQL = "INSERT INTO m_KodeFungsi (KodeFungsi, NamaIndonesia, " &
                "NamaInggris, AktifYN, UserID, LastUPD, TransferYN) VALUES " &
                "('" & KodeFungsi.Text & "', '" & NamaIndonesia.Text & "', " &
                "'" & NamaInggris.Text & "', 'Y', '" & UserID & "',  GetDate(), 'N') "
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update m_KodeFungsi SET " &
                    "namaIndonesia = '" & NamaIndonesia.Text & "', " &
                    "namaInggris = '" & NamaInggris.Text & "', " &
                    "TransferYN = 'N', " &
                    " UserID = '" & UserID & "',  LastUPD = GetDate() " &
                    "where KodeFungsi = '" & KodeFungsi.Text & "' "
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

        SQL = "SELECT * FROM m_KodeFungsi WHERE aktifyn = 'Y' " &
            "ORDER BY NamaIndonesia "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !KodeFungsi,
                    .Table.Rows(a) !NamaIndonesia,
                    .Table.Rows(a) !NamaInggris, "Edit", "Hapus")
            Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub

    Sub Isi_Data()
        SQL = "SELECT * FROM m_KodeFungsi " &
            "WHERE KodeFungsi = '" & KodeFungsi.Text & "' " &
            " AND aktifYN='Y' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            KodeFungsi.Text = dbTable.Rows(0) !KodeFungsi
            NamaIndonesia.Text = dbTable.Rows(0) !NamaIndonesia
            NamaInggris.Text = dbTable.Rows(0) !NamaInggris
        End If
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        KodeFungsi.ReadOnly = False
        KodeFungsi.Focus()
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

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        cmdCetak.Enabled = False
        Cetak()
        cmdCetak.Enabled = True
    End Sub

    Private Sub Cetak()
        Me.Cursor = Cursors.WaitCursor
        Proses.OpenConn(CN)
        dttable = New DataTable
        SQL = "SELECT * FROM m_KodeFungsi " &
            "WHERE AktifYN='Y'  " &
            "ORDER BY NamaIndonesia "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_KodifFungsi
            objRep.SetDataSource(dttable)
            Form_CetakLaporan.Text = "Daftar Fungsi Produk"
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
    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If e.RowIndex >= 0 Then
            KodeFungsi.Text = DGView.Rows(e.RowIndex).Cells(0).Value
            NamaIndonesia.Text = DGView.Rows(e.RowIndex).Cells(1).Value
            NamaInggris.Text = DGView.Rows(e.RowIndex).Cells(2).Value
        End If
        If e.ColumnIndex = 3 Then 'Edit
            tEdit = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "edit")
            If tEdit = False Then
                MsgBox("Anda tidak punya akses untuk edit data ini !", vbCritical + vbOKOnly, ".:Warning !")
                Exit Sub
            End If
            If Trim(KodeFungsi.Text) = "" Then
                MsgBox("Kode Fungsi Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            LAdd = False
            LEdit = True
            AturTombol(False)
            KodeFungsi.ReadOnly = True
            NamaIndonesia.Focus()
        ElseIf e.ColumnIndex = 4 Then 'Hapus
            tHapus = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "hapus")
            If tHapus = False Then
                MsgBox("Anda tidak punya akses untuk menghapus data ini !", vbCritical + vbOKOnly, ".:Warning !")
                Exit Sub
            End If
            If Trim(KodeFungsi.Text) = "" Then
                MsgBox("Kode Fungsi Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                SQL = "Update M_KodeFungsi Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate(), TransferYN='N' " &
                        "Where KodeFungsi = '" & KodeFungsi.Text & "' "
                Proses.ExecuteNonQuery(SQL)
                ClearTextBoxes()
                Data_Record()
            End If
        End If
    End Sub

    Private Sub Form_KodifFungsi_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        tTambah = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "laporan")

        AturTombol(True)
    End Sub

    Private Sub KodeFungsi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeFungsi.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then NamaIndonesia.Focus()
    End Sub

    Private Sub NamaIndonesia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaIndonesia.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then NamaInggris.Focus()
    End Sub

    Private Sub NamaInggris_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaInggris.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then cmdSimpan.Focus()
    End Sub
End Class