Public Class Form_Cetak
    Private Sub btnScreen_Click(sender As Object, e As EventArgs) Handles btnScreen.Click
        FrmMenuUtama.TSKeterangan.Text = "LAYAR"
        FrmMenuUtama.TipeCetakan.Text = cbCetakan.Text
        Me.Close()
    End Sub

    Private Sub btnPrinter_Click(sender As Object, e As EventArgs) Handles btnPrinter.Click
        FrmMenuUtama.TSKeterangan.Text = "PRINTER"
        FrmMenuUtama.TipeCetakan.Text = cbCetakan.Text
        Me.Close()
    End Sub

    Private Sub Form_Cetak_Load(sender As Object, e As EventArgs) Handles Me.Load
        cbCetakan.Items.Clear()
        Dim Proses As New ClsKoneksi
        Dim UserID As String = FrmMenuUtama.TsPengguna.Text
        Dim tSJ As Boolean = Proses.UserAksesTombol(UserID, "SURAT_JALAN", "akses")
        If tSJ Then
            cbCetakan.Items.Add("Surat Jalan")
            cbCetakan.Items.Add("Faktur")
            cbCetakan.SelectedIndex = 1
        Else
            cbCetakan.Items.Add("Faktur")
            cbCetakan.SelectedIndex = 0
        End If
        NamaKertas.Text = Trim(My.Settings.NamaKertas)
        NamaPrinter.Text = Trim(My.Settings.NamaPrinter)
        FrmMenuUtama.TSKeterangan.Text = ""
        FrmMenuUtama.TipeCetakan.Text = ""
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        'Form_Penjualan_CompId.ShowDialog()
        NamaKertas.Text = Trim(My.Settings.NamaKertas)
        NamaPrinter.Text = Trim(My.Settings.NamaPrinter)
    End Sub
End Class