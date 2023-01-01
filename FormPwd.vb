Public Class FormPwd
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Acak As New Crypto
        Label1.Text = Acak.Decrypt(TextBox1.Text)

    End Sub
End Class