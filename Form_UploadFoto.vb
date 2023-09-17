Imports System.Data.SqlClient
Imports System.IO
Public Class Form_UploadFoto
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim SQL As String, UserID As String = FrmMenuUtama.TsPengguna.Text

    Private Sub btnBrowseImg_Click(sender As Object, e As EventArgs) Handles btnBrowseImg.Click
        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.Filter = "JPG (*.jpg)|*.jpg|Bitmap File (*.bmp)|*.bmp|PNG (*.png)|*.png|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 4
        OpenFileDialog1.RestoreDirectory = True
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
            namaFile.Text = OpenFileDialog1.FileName
            If Len(namaFile.Text) > 100 Then
                MsgBox("Nama Lokasi File Gambar Terlalu Panjang !" & vbCrLf &
                        "Hanya bisa 100 character.", vbCritical, ".:Warning !")
            End If
        End If
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        If Trim(namaFile.Text) <> "" Then
            store_pic_Sql(PictureBox1.Image, "pekerti")
            Retrieve("PKT", "pekerti")
        End If
    End Sub


    Private Sub store_pic_Sql(ByVal img As Image, dbase As String)
        Dim ipserver As String = My.Settings.IPServer
        Dim pwd As String = My.Settings.Password
        Dim dbUser As String = My.Settings.UserID
        Dim database As String = My.Settings.Database
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlC As String = ""
        '   "Initial Catalog=" & database & "; " &
        '   "user id=" & dbUser & ";password=" & pwd & "; " &
        '   "Data Source=" & ipserver & ";Integrated Security=SSPI "
        sqlC = "Initial Catalog=" & dbase & "; " &
                "user id=" & dbUser & ";password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";"
        conn.ConnectionString = sqlC
        conn.Open()
        'SQL = "insert into " & dbase & ".dbo.m_barang_foto values('" & IdRec.Text & "',@name, @photo, '" & UserID & "',  GetDate() ) "
        SQL = "UPDATE m_Company  set TTDireksi = @photo"
        Dim cmd As SqlCommand = New SqlCommand(Sql, conn)
        cmd.Parameters.AddWithValue("@name", namaFile.Text)
        Dim ms As New MemoryStream()
        PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
        Dim data As Byte() = ms.GetBuffer()
        Dim p As New SqlParameter("@photo", SqlDbType.Image)
        p.Value = data
        cmd.Parameters.Add(p)
        cmd.ExecuteNonQuery()
        If Not IsNothing(conn) Then
            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub Retrieve(idRecord As String, DBase As String)
        Dim ipserver As String = My.Settings.IPServer
        Dim pwd As String = My.Settings.Password
        Dim dbUser As String = My.Settings.UserID
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlC As String = ""
        sqlC = "Initial Catalog=" & DBase & "; " &
            "user id=" & dbUser & ";password=" & pwd & "; " &
            "Persist Security Info=True;" &
            "Data Source=" & ipserver & ";"
        conn.ConnectionString = sqlC
        conn.Open()
        'Sql = "select photo from " & DBase & ".dbo.m_barang_foto where IDRec='" & idRecord & "'"
        SQL = "SELECT TTDireksi FROM m_Company "
        Dim cmd = New SqlCommand(Sql, conn)
        Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())
        If Not imageData Is Nothing Then
            Try
                Using ms As New MemoryStream(imageData, 0, imageData.Length)
                    ms.Write(imageData, 0, imageData.Length)
                    PictureBox1.BackgroundImage = Image.FromStream(ms, True)
                End Using
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
        End If
        If Not IsNothing(conn) Then
            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub Form_UploadFoto_Load(sender As Object, e As EventArgs) Handles Me.Load
        Retrieve("PKT", "pekerti")
    End Sub
End Class