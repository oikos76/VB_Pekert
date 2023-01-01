Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Form_KodifNegara
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
        If cmbBenua.Text = "" Then
            MsgBox("Benua belum di pilih !", vbCritical + vbOKOnly, ".:Warning !")
            Exit Sub
        End If
        Dim cBenua As String = ""
        cBenua = Mid(cmbBenua.Text, 1, 1)
        If LAdd Then
            SQL = "Select NamaIndonesia 
                 From m_KodeNegara 
                Where NamaIndonesia = '" & NamaIndonesia.Text & "'
                  And aktifYN='Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaIndonesia.Focus()
                MsgBox("Nama " & NamaIndonesia.Text & " Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            End If

            SQL = "Select KodeNegara, NamaIndonesia 
                 From M_KodeNegara
                Where KodeNegara = '" & KodeNegara.Text & "'
                  AND Benua = '" & cBenua & "'
                  And aktifYN='Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                KodeNegara.Focus()
                MsgBox("Kode " & KodeNegara.Text & " benua " & cmbBenua.Text & vbCrLf & "SUDAH digunakan " & dbTable.Rows(0) !namaindonesia, vbCritical, "Warning!")
                Exit Sub
            End If

            SQL = "INSERT INTO m_KodeNegara (Benua, KodeNegara, NamaIndonesia, " &
                "NamaInternasional, AktifYN, UserID, LastUPD, TransferYN) VALUES " &
                "('" & cBenua & "', '" & KodeNegara.Text & "', '" & NamaIndonesia.Text & "', " &
                "'" & NamaInternasional.Text & "', 'Y', '" & UserID & "',  GetDate(), 'N') "
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update m_KodeNegara SET " &
                    "namaIndonesia = '" & NamaIndonesia.Text & "', " &
                    "namaInternasional = '" & NamaInternasional.Text & "', " &
                    "TransferYN = 'N', " &
                    " UserID = '" & UserID & "',  LastUPD = GetDate() " &
                    "WHERE KodeNegara = '" & KodeNegara.Text & "' " &
                    "  AND Benua = '" & cBenua & "' "
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

        SQL = "SELECT * FROM m_KodeNegara WHERE aktifyn = 'Y' " &
            "ORDER BY kodenegara, NamaIndonesia "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !Benua,
                                .Table.Rows(a) !kodenegara,
                                .Table.Rows(a) !NamaIndonesia,
                                .Table.Rows(a) !NamaInternasional,
                                "Edit", "Hapus")
            Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub
    Sub Isi_Data()
        SQL = "SELECT * FROM m_KodeNegara " &
            "WHERE KodeNegara = '" & KodeNegara.Text & "' " &
            " AND aktifYN='Y' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            KodeNegara.Text = dbTable.Rows(0) !KodeBahan
            NamaIndonesia.Text = dbTable.Rows(0) !NamaIndonesia
            NamaInternasional.Text = dbTable.Rows(0) !Namainternasional
            For i = 0 To cmbBenua.Items.Count - 1
                cmbBenua.SelectedIndex = i
                If Mid(cmbBenua.Text, 1, 1) = dbTable.Rows(0) !benua Then
                    Exit Sub
                End If
            Next i
        End If
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        KodeNegara.ReadOnly = False
        KodeNegara.Focus()
    End Sub

    Private Sub KodeNegara_TextChanged(sender As Object, e As EventArgs) Handles KodeNegara.TextChanged

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

    Private Sub NamaInternasional_TextChanged(sender As Object, e As EventArgs) Handles NamaInternasional.TextChanged

    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        cmdCetak.Enabled = False
        Cetak()
        cmdCetak.Enabled = True
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Cetak()

        Me.Cursor = Cursors.WaitCursor
        Proses.OpenConn(CN)
        dttable = New DataTable
        SQL = "select *, " &
            " CASE WHEN benua='A' THEN 'Asia'  " &
            "      WHEN benua='B' THEN 'Afrika'  " &
            "      WHEN benua='C' THEN 'Australia & Selandia Baru'  " &
            "      WHEN benua='D' THEN 'Amerika'  " &
            "      WHEN benua='E' THEN 'Eropa'  " &
            "  END namaBenua  " &
            " FROM m_KodeNegara  " &
            "WHERE AktifYN ='Y'  " &
            "ORDER BY benua, NamaIndonesia "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_KodifNegara
            objRep.SetDataSource(dttable)
            Form_CetakLaporan.Text = "Daftar Negara"
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
        Dim cBenua As String = ""
        If e.RowIndex >= 0 Then
            cbenua = DGView.Rows(e.RowIndex).Cells(0).Value
            For i = 0 To cmbBenua.Items.Count - 1
                cmbBenua.SelectedIndex = i
                If Mid(cmbBenua.Text, 1, 1) = cBenua Then
                    Exit For
                End If
            Next i
            KodeNegara.Text = DGView.Rows(e.RowIndex).Cells(1).Value
            NamaIndonesia.Text = DGView.Rows(e.RowIndex).Cells(2).Value
            NamaInternasional.Text = DGView.Rows(e.RowIndex).Cells(3).Value
        End If
        If e.ColumnIndex = 4 Then 'Edit
            tEdit = Proses.UserAksesTombol(UserID, "41_KODIF_NEGARA", "edit")
            If tEdit = False Then
                MsgBox("Anda tidak punya akses untuk edit data ini !", vbCritical + vbOKOnly, ".:Warning")
                Exit Sub
            End If
            LAdd = False
            LEdit = True
            AturTombol(False)
            KodeNegara.ReadOnly = True
            NamaIndonesia.Focus()
        ElseIf e.ColumnIndex = 5 Then 'Hapus
            tHapus = Proses.UserAksesTombol(UserID, "41_KODIF_NEGARA", "hapus")
            If tHapus = False Then
                MsgBox("Anda tidak punya akses untuk hapus data ini !", vbCritical + vbOKOnly, ".:Warning")
                Exit Sub
            End If
            If Trim(KodeNegara.Text) = "" Then
                MsgBox("Kode Bahan Belum di pilih!", vbCritical, ".: ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                SQL = "Update m_KodeNegara Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate(), TransferYN='N' " &
                       "Where KodeNegara = '" & KodeNegara.Text & "' " &
                       "  And benua = '" & cBenua & "' "
                Proses.ExecuteNonQuery(SQL)
                ClearTextBoxes()
                Data_Record()
            End If
        End If
    End Sub
    Private Sub Form_KodifNegara_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmbBenua.Items.Clear()
        cmbBenua.Items.Add("A. Asia")
        cmbBenua.Items.Add("B. Afrika")
        cmbBenua.Items.Add("C. Australia & Selandia Baru")
        cmbBenua.Items.Add("D. Amerika")
        cmbBenua.Items.Add("E. Eropa")

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

        tTambah = Proses.UserAksesTombol(UserID, "41_KODIF_NEGARA", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "41_KODIF_NEGARA", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "41_KODIF_NEGARA", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "41_KODIF_NEGARA", "laporan")

        AturTombol(True)
    End Sub

    Private Sub KodeNegara_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeNegara.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then NamaIndonesia.Focus()
    End Sub

    Private Sub NamaIndonesia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaIndonesia.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then NamaInternasional.Focus()
    End Sub

    Private Sub NamaInternasional_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaInternasional.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then cmdSimpan.Focus()
    End Sub
End Class