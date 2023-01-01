Public Class Form_BackupDb
    Private Sub cariFolder_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmdBackup_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cariFolder_Click_1(sender As Object, e As EventArgs) Handles cariFolder.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            locFile.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub cmdBackup_Click_1(sender As Object, e As EventArgs) Handles cmdBackup.Click
        Dim dbase As String = My.Settings.Database
        Dim Proses As New ClsKoneksi

        Dim lokasiSimpan As String = ""
        If Trim(locFile.Text) = "" Then
            MsgBox("Folder simpan hasil backup belum di pilih", vbCritical, ".:Warning!")
            Exit Sub
        Else
            If UCase(Mid(dbase, 1, 5)) = "PRIMA" Then
                lokasiSimpan = locFile.Text + "\DBPrimaJaya_" & Format(Now, "yyMMdd_HHmmss") & ".bak"
            ElseIf UCase(Mid(dbase, 1, 5)) = "DOMIN" Then
                lokasiSimpan = locFile.Text + "\DBDomino_" & Format(Now, "yyMMdd_HHmmss") & ".bak"
            ElseIf UCase(Mid(dbase, 1, 5)) = "KUNCI" Then
                lokasiSimpan = locFile.Text + "\DBKunciMas_" & Format(Now, "yyMMdd_HHmmss") & ".bak"
            Else
                lokasiSimpan = locFile.Text + "\DBBackup_" & Format(Now, "yyMMdd_HHmmss") & ".bak"
            End If

        End If
        cmdBackup.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Proses.CloseConn()
        Dim sql As String = "BACKUP DATABASE " & dbase & " TO DISK='" & lokasiSimpan & "' "
        Proses.ExecuteNonQuery(sql)
        cmdBackup.Enabled = True
        Me.Cursor = Cursors.Default
        MsgBox("Data berhasil di backup." & vbCrLf & "File hasil backup di simpan di : " & lokasiSimpan, vbInformation + vbOKOnly, ".:Finish !")

    End Sub

    Private Sub Form_BackupDb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Backup database " + My.Settings.Database
    End Sub
End Class