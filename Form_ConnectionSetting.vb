Imports System.Data.SqlClient
Imports System.IO

Public Class Form_ConnectionSetting
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

    Private Sub Form_ConnectionSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtIP.Text = My.Settings.IPServer
        Dim dbtable As New DataTable, proses As New ClsKoneksi
        Dim Sql As String = ""
        Sql = "Select name From sys.databases           
            Order by name "
        dbtable = proses.ExecuteQueryAllDB(Sql)
        With dbtable.Columns(0)
            For a = 0 To dbtable.Rows.Count - 1
                cmbDataBase.Items.Add(.Table.Rows(a)!name)
            Next (a)
        End With
        proses.CloseConn()
        cmbDataBase.Text = My.Settings.Database
    End Sub

    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click
        My.Settings.IPServer = Trim(txtIP.Text)
        My.Settings.Database = Trim(cmbDataBase.Text)
        My.Settings.Save()
        End
    End Sub

    Private Sub cmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub cariFile_Click(sender As Object, e As EventArgs) Handles cariFile.Click

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "backup files (*.bak)|*.bak|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    FileName.Text = openFileDialog1.FileName.ToString
                    '  FileName.Text = System.IO.Path.GetFileName(openFileDialog1.FileName)
                    ' Insert code to read the stream here.
                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub cmdRestore_Click(sender As Object, e As EventArgs) Handles cmdRestore.Click
        Dim lokasiSimpan As String = ""
        If Trim(FileName.Text) = "" Then
            MsgBox("Nama file backup belum di pilih", vbCritical, ".:Warning!")
            Exit Sub
        End If
        cmdRestore.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Dim dbase As String = My.Settings.Database


        'Dim sql As String = "RESTORE DATABASE " & dbase & " FROM DISK='" & FileName.Text & "' "
        'ExecuteNonQuery(sql)



        Try
            Dim rescon As New SqlConnection("Initial Catalog=master; " &
                "user id=" & userid & ";password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";")

            ' Dim resAdp As SqlDataAdapter
            Dim resCmd As SqlCommand

            Dim SQLsentence As String = "RESTORE DATABASE " & dbase & "	FROM DISK='" & FileName.Text & "'"

            rescon.Open()
            resCmd = New SqlCommand(SQLsentence, rescon)
            resCmd.ExecuteNonQuery()

            MsgBox("Db Has been Restored.")
            rescon.Close()
        Catch ex As Exception
            MsgBox("Could not be restored.", vbOKOnly)
        End Try

        cmdRestore.Enabled = True
        Me.Cursor = Cursors.Default
        MsgBox("Data berhasil di restore !", vbInformation + vbOKOnly, ".:Finish !")
    End Sub
End Class