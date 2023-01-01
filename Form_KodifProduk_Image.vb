Public Class Form_KodifProduk_Image
    Public Sub ShowFoto(NamaFileJPG As String)
        Dim FotoLoc As String = My.Settings.path_foto
        If NamaFileJPG = "" Then PictureBox1.Image = Nothing
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.ImageLocation = filename
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub
End Class