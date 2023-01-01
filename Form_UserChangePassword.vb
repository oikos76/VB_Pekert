Public Class Form_UserChangePassword
    Dim sql As String
    Dim proses As New ClsKoneksi
    Dim tblLogin As DataTable

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If UserID.Text = "" Then UserID.Focus() : Exit Sub
        If PasswordLama.Text = "" Then PasswordLama.Focus() : Exit Sub
        If PasswordBaru.Text = "" Then PasswordBaru.Focus() : Exit Sub


        Dim Acak As New Crypto
        Dim encryptpassword As String = ""
        encryptpassword = Acak.Encrypt(PasswordLama.Text)
        tblLogin = proses.ExecuteQuery("Select * From m_User " &
                                        "Where UserID = '" & UserID.Text & "' " &
                                        "  and password ='" & encryptpassword & "'")

        If tblLogin.Rows.Count = 0 Then
            MessageBox.Show("Password lama salah..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            PasswordLama.Focus()
        Else
            encryptpassword = Acak.Encrypt(PasswordBaru.Text)
            sql = "Update m_User Set " &
                    "Password = '" & Trim(encryptpassword) & "' " &
                    "where userid = '" & UserID.Text & "' "
            proses.ExecuteNonQuery(sql)
            MsgBox("Password sudah berubah!", vbInformation, "Congratulation!")
            UserID.Text = ""
            PasswordLama.Text = ""
            PasswordBaru.Text = ""
        End If
    End Sub

    Private Sub cmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub UserID_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UserID.KeyPress
        If e.KeyChar = Chr(13) Then
            sql = "Select * From m_User " & _
               "Where UserID = '" & UserID.Text & "' "
            tblLogin = proses.ExecuteQuery(sql)
            If tblLogin.Rows.Count = 0 Then
                MessageBox.Show("User ID tidak ditemukan..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                UserID.Focus()
            Else
                PasswordLama.Focus()
            End If
        End If
    End Sub

    Private Sub PasswordLama_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PasswordLama.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim Acak As New Crypto, encryptpassword As String = ""
            encryptpassword = Acak.Encrypt(PasswordLama.Text)


            sql = "Select * From m_User " &
                "Where UserID = '" & UserID.Text & "' " &
                "  and password ='" & encryptpassword & "'"

            tblLogin = proses.ExecuteQuery(sql)
            If tblLogin.Rows.Count = 0 Then
                MessageBox.Show("Password Lama Salah..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                PasswordLama.Focus()
            Else
                PasswordBaru.Focus()
            End If
        End If
    End Sub

    Private Sub PasswordBaru_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PasswordBaru.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSave.Focus()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Crypto.EncryptionAlgorithm = Crypto.Algorithm.DES
        Crypto.Encoding = Crypto.EncodingType.HEX
        Crypto.Key = "Mat28:19"
        Crypto.EncryptString(PasswordBaru.Text)
        UserID.Text = Crypto.Content
        Crypto.Clear()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Crypto.EncryptionAlgorithm = Crypto.Algorithm.DES
        Crypto.Key = "Mat28:19"
        Crypto.Encoding = Crypto.EncodingType.HEX
        Crypto.Content = PasswordBaru.Text
        Crypto.DecryptString()
        UserID.Text = Crypto.Content
        Crypto.Clear()
    End Sub

    Private Sub Form_UserChangePassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If FrmMenuUtama.TsPengguna.Text = "EKO_K" Then
        '    Button1.Visible = True
        '    Button2.Visible = True
        'Else
        Button1.Visible = False
            Button2.Visible = False
        'End If
    End Sub

    Private Sub PasswordLama_TextChanged(sender As Object, e As EventArgs) Handles PasswordLama.TextChanged

    End Sub
End Class