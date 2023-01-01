Imports System.Data.SqlClient
Imports System.IO

Public Class Form_BackupRestore

    Protected tblPengguna = New DataTable
    Protected SQL As String
    Protected Ds As DataSet
    Protected Dt As DataTable


    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter

    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected userid As String = My.Settings.UserID
    Protected database As String = My.Settings.Database

    Private Sub cariFolder_Click(sender As Object, e As EventArgs) Handles cariFolder.Click
        If (OpenFileDialog1.ShowDialog() = DialogResult.OK) Then
            locFile.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub cmdRestore_Click(sender As Object, e As EventArgs) Handles cmdRestore.Click
        Dim dbase As String = My.Settings.Database
        Dim Proses As New ClsKoneksi
        Dim sql As String, dbtable As New DataTable, version As String = ""
        sql = "Select * from m_Company "
        dbtable = Proses.ExecuteQuery(sql)
        If dbtable.Rows.Count <> 0 Then
            version = dbtable.Rows(0) !ipcloud
        End If
        Dim lokasiSimpan As String = ""
        If Trim(locFile.Text) = "" Then
            MsgBox("Nama File Backup belum di pilih.", vbCritical, ".:Warning!")
            Exit Sub
        End If
        cmdRestore.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Proses.CloseConn()

        sql = "use master "
        Proses.ExecuteNonQuery(sql)

        Dim rescon As New SqlConnection("Initial Catalog=master; " &
                "user id=" & userid & ";password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";")

        Dim resCmd As SqlCommand

        Dim SQLsentence As String = "RESTORE DATABASE " & dbase & "	FROM DISK='" & Trim(locFile.Text) & "'"

        rescon.Open()
        resCmd = New SqlCommand(SQLsentence, rescon)
        resCmd.ExecuteNonQuery()

        sql = "update m_company set ipcloud = '" & version & "' "
        Proses.ExecuteNonQuery(sql)

        cmdRestore.Enabled = True
        Me.Cursor = Cursors.Default
        MsgBox("Data berhasil di Restore...." & lokasiSimpan, vbInformation + vbOKOnly, ".:Finish !")

    End Sub
End Class