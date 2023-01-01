Public Class Form_LoginMenu
    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        FrmMenuUtama.TSKeterangan.Text = ""
        FrmMenuUtama.UserLoginMenu.Text = ""
        FrmMenuUtama.PasswordLoginMenu.Text = ""
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        FrmMenuUtama.TSKeterangan.Text = "OK"
        FrmMenuUtama.UserLoginMenu.Text = Trim(KdPenggunaTxt.Text)
        FrmMenuUtama.PasswordLoginMenu.Text = Trim(PswTxt.Text)
        If Trim(PswTxt.Text) = "" Then
            MsgBox("Password tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning!")
            PswTxt.Focus()
            Exit Sub
        End If
        If Trim(KdPenggunaTxt.Text) = "" Then
            MsgBox("User id tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning!")
            KdPenggunaTxt.Focus()
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub PswTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PswTxt.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdOK.Focus()
        End If
    End Sub

    Private Sub Form_LoginMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub KdPenggunaTxt_TextChanged(sender As Object, e As EventArgs) Handles KdPenggunaTxt.TextChanged

    End Sub

    Private Sub KdPenggunaTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KdPenggunaTxt.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim proses As New ClsKoneksi
            Dim tblLogin As DataTable
            tblLogin = proses.ExecuteQuery("Select * From m_user Where UserID = '" & KdPenggunaTxt.Text & "'")
            If tblLogin.Rows.Count = 0 Then
                MessageBox.Show("User ID tidak ditemukan..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                KdPenggunaTxt.Focus()
            Else
                PswTxt.Focus()
            End If
        End If
    End Sub

    Private Sub PswTxt_TextChanged(sender As Object, e As EventArgs) Handles PswTxt.TextChanged

    End Sub
End Class