Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO

Public Class Form_SettingFolderFoto
    Dim SQL As String, dbTable As New DataTable
    Dim Proses As New ClsKoneksi
    Private Sub cariFile_Click(sender As Object, e As EventArgs) Handles cariFile.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            NamaFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        My.Settings.path_foto = NamaFolder.Text
        MsgBox("Folder Foto berhasil di simpan ke : " & NamaFolder.Text, vbOKOnly + vbInformation, ".:Success")
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub Form_SettingFolderFoto_Load(sender As Object, e As EventArgs) Handles Me.Load
        SQL = "SELECT FotoLoc FROM M_COMPANY "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaFolder.Text = dbTable.Rows(0) !FotoLoc
            If NamaFolder.Text <> My.Settings.path_foto Then
                NamaFolder.Text = My.Settings.path_foto
            End If
        End If
    End Sub

End Class