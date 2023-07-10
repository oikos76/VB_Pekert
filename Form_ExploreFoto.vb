Imports System.IO
Public Class Form_ExploreFoto
    Private Sub Form_ExploreFoto_Load(sender As Object, e As EventArgs) Handles Me.Load
        FrmMenuUtama.TSKeterangan.Visible = True
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen     'LightSkyBlue
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGView.AllowUserToResizeColumns = True

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        With Me.DGView.RowTemplate
            .Height = 32
            .MinimumHeight = 32
        End With
        btnRename.Visible = False
    End Sub
    Sub Data_Refresh()
        DGView.ColumnCount = 5
        DGView.Columns(0).Visible = True

        DGView.Rows.Clear()
        DGView.Columns(0).HeaderText = "No."
        DGView.Columns(0).Width = 100
        DGView.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.Columns(1).HeaderText = "Nama File"
        DGView.Columns(1).Width = 300
        DGView.Columns(2).HeaderText = "Nama File Baru"
        DGView.Columns(2).Width = 200
        DGView.Columns(3).HeaderText = ""
        DGView.Columns(4).HeaderText = ""
        Try
            Dim a As Integer = 1
            Dim fileNames1 = My.Computer.FileSystem.GetFiles(locFile.Text, FileIO.SearchOption.SearchAllSubDirectories, (ext.Text))
            For Each fileName1 As String In fileNames1
                Application.DoEvents()
                DGView.Rows.Add(Format(a, "##0"), fileName1)
                ext.Text = "*.*"
                a += 1
            Next
        Catch e As Exception
            Dim b As Integer = 1
            If MessageBox.Show("An error occured for searching all subdirectories in this folder. Switching to searching top level only.") Then
                Dim fileNames2 = My.Computer.FileSystem.GetFiles(locFile.Text, FileIO.SearchOption.SearchTopLevelOnly, (ext.Text))
                For Each fileName2 As String In fileNames2
                    Application.DoEvents()
                    DGView.Rows.Add(Format(b, "##0"), fileName2)
                    ext.Text = "*.jpg"
                    b += 1
                Next
            End If
        End Try
    End Sub

    Private Sub cariFolder_Click(sender As Object, e As EventArgs) Handles cariFolder.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            btnRename.Visible = False
            Me.Cursor = Cursors.WaitCursor
            locFile.Text = FolderBrowserDialog1.SelectedPath
            DGView.Visible = False
            Data_Refresh()
            DGView.Visible = True
            If DGView.Rows.Count <> 0 Then
                btnRename.Visible = True
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        Dim NamaFile As String = "", tNamaFile As String = "",
            NamaFileBaru As String = ""
        Me.Cursor = Cursors.WaitCursor
        For i As Integer = 0 To DGView.Rows.Count - 1
            If i = DGView.Rows.Count Then Exit For
            Application.DoEvents()
            If Trim(DGView.Rows(i).Cells(0).Value) = "" Then Exit For
            NamaFile = DGView.Rows(i).Cells(1).Value
            tNamaFile = GetFileName_OtherMethod(NamaFile)
            NamaFileBaru = Mid(tNamaFile, 1, 2) + "0" + Mid(tNamaFile, 3, Len(tNamaFile) - 2)
            My.Computer.FileSystem.RenameFile(NamaFile, NamaFileBaru)
            DGView.Rows(i).Cells(2).Value = NamaFileBaru
        Next i
        btnRename.Enabled = False
        Me.Cursor = Cursors.Default
        MsgBox("4 Digit nama file Foto di depan, berhasil di ganti menjadi 2 digit kode area, 3 digit running number perajin", vbInformation + vbOKOnly, "Success !")
    End Sub

    Private Function GetFileName_OtherMethod(ByVal path As String) As String
        Dim _filename As String = ""
        Dim sep() As Char = {"/", "\", "//"}
        _filename = path.Split(sep).Last()
        Return _filename
    End Function
End Class