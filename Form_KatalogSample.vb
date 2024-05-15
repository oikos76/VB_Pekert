Imports System.IO
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Form_KatalogSample
    Dim kodeGLHutangPerajin As String = "",
      KodeGLPiutangPerajin As String = ""
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean, lTambah As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Dim FotoLoc As String = My.Settings.path_foto

    Private Sub cmdPenambahanKode_Click(sender As Object, e As EventArgs) Handles cmdPenambahanKode.Click
        If IDRecord.Text = "" Then
            MsgBox("Katalog yang akan di tambah BELUM di pilih!", vbCritical, ".:WARNING!")
            Exit Sub
        End If
        LAdd = False
        LEdit = False
        lTambah = True
        AturTombol(False)
        ClearProduk()
        KodeProduk.Focus()
    End Sub

    Private Sub Form_KatalogSample_Load(sender As Object, e As EventArgs) Handles Me.Load

        DGView.Rows.Clear()
        DGView2.Rows.Clear()

        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        Me.Cursor = Cursors.Default

        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With

        With Me.DGView2.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With

        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        'DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        'DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Dim mSupplier As String
        SQL = "Select Nama, KodeImportir From m_KodeImportir " &
            "Where AktifYN = 'Y' " &
            "Order By Nama "
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            mSupplier = Microsoft.VisualBasic.Left(dbTable.Rows(a) !Nama + Space(100), 100) + " " +
                Microsoft.VisualBasic.Right(Space(10) + dbTable.Rows(a) !KodeImportir, 10)
            cmbSupplier.Items.Add(mSupplier)
        Next

        SQL = "Select Kode from m_Currency Where AktifYN = 'Y'"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            cmbMataUang.Items.Add(dbTable.Rows(a) !kode)
        Next
        DaftarKatalog()
        SQL = "Select Top 1 * From t_KatalogProduk " &
            " where aktifYN = 'Y' " &
            "Order By IDRec desc "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            IDRecord.Text = dbTable.Rows(0) !IDRec
        Else
            IDRecord.Text = ""
        End If
        Call IsiKatalog(IDRecord.Text)
        tTambah = Proses.UserAksesTombol(UserID, "28_KATALOG_CONTOH", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "28_KATALOG_CONTOH", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "28_KATALOG_CONTOH", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "28_KATALOG_CONTOH", "laporan")
        AturTombol(True)
        tKonversi.Visible = False
        LKonv.Visible = False
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select Top 1 * From t_KatalogProduk " &
            "Where NamaFile = '" & NamaFile.Text & "' " &
            "Order By IdRec "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            IDRecord.Text = Rs.Rows(0) !IdRec
        Else
            IDRecord.Text = ""
        End If
        Proses.CloseConn()
        IsiKatalog(IDRecord.Text)
    End Sub

    Private Sub DaftarKatalog()
        Dim mKondisi As String = ""
        Me.Cursor = Cursors.WaitCursor

        SQL = "SELECT Distinct NamaFile, Importir, Kode_Importir " &
            " FROM t_KatalogProduk " &
            "WHERE aktifYN = 'Y'  " &
            "ORDER BY namafile "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !NamaFile,
                    .Table.Rows(a) !Importir,
                    .Table.Rows(a) !Kode_Importir)
            Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub ClearTextBoxes(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        If ctlcol Is Nothing Then ctlcol = Me.Controls
        For Each ctl As Control In ctlcol
            If TypeOf (ctl) Is TextBox Then
                DirectCast(ctl, TextBox).Clear()
            Else
                If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                    ClearTextBoxes(ctl.Controls)
                End If
            End If
        Next
    End Sub
    Private Sub LoadPic(NamaFileJPG As String)
        PictureBox1.BackgroundImage = Nothing
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.BackgroundImage = Image.FromFile(filename)
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub
    Private Sub ShowFoto(NamaFileJPG As String)
        If NamaFileJPG = "" Then
            LocGmb1.Text = ""
        End If
        PictureBox1.Image = Nothing
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.ImageLocation = filename
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub
    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Visible = False
            cmdSimpan.Visible = False
        Else
            cmdTambah.Visible = tAktif
            cmdSimpan.Visible = Not tAktif
        End If
        If tEdit = False Then
            cmdEdit.Visible = False
            cmdSimpan.Visible = False
        Else
            cmdEdit.Visible = tAktif
            cmdSimpan.Visible = Not tAktif
        End If
        If tHapus = False Then
            cmdHapus.Visible = False
        Else
            cmdHapus.Visible = tAktif
        End If
        cmdPenambahanKode.Visible = tAktif
        PanelNavigate.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Visible = tAktif
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim oId As String = IDRecord.Text
        If Trim(IDRecord.Text) = "" Then Exit Sub
        MsgSQL = "Select Top 1 * From t_KatalogProduk " &
            "Where NamaFile = '" & NamaFile.Text & "' " &
            " And IdRec < " & IDRecord.Text & " " &
            "Order By IdRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            IDRecord.Text = Rs.Rows(0) !IdRec
        Else
            IDRecord.Text = oId
        End If
        Proses.CloseConn()
        IsiKatalog(IDRecord.Text)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim oId As String = IDRecord.Text
        If Trim(IDRecord.Text) = "" Then Exit Sub
        MsgSQL = "Select Top 1 * From t_KatalogProduk " &
            "Where NamaFile = '" & NamaFile.Text & "' " &
            " And IdRec > " & IDRecord.Text & " " &
            "Order By IdRec "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            IDRecord.Text = Rs.Rows(0) !IdRec
        Else
            IDRecord.Text = oId
        End If
        Proses.CloseConn()
        IsiKatalog(IDRecord.Text)
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select Top 1 * From t_KatalogProduk " &
            "Where NamaFile = '" & NamaFile.Text & "' " &
            "Order By IdRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            IDRecord.Text = Rs.Rows(0) !IdRec
        Else
            IDRecord.Text = ""
        End If
        Proses.CloseConn()
        IsiKatalog(IDRecord.Text)
    End Sub

    Public Sub SetupToolTip()
        With Me.ToolTip1
            .AutomaticDelay = 0
            .AutoPopDelay = 30000
            .BackColor = System.Drawing.Color.AntiqueWhite
            .InitialDelay = 50
            .IsBalloon = False
            .ReshowDelay = 50
            .ShowAlways = True
            .Active = False
            .Active = True
            .SetToolTip(Me.HargaFOB, "ambil harga dolar")
            .SetToolTip(Me.HargaUSD, "Harga jual dollar setelah di konversi")

        End With
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        lTambah = False
        AturTombol(False)
        ClearTextBoxes()
        ClearProduk()
        cmbSupplier.Enabled = True
        NamaFile.Enabled = True
        cmbSupplier.Enabled = True
        cmbMataUang.Enabled = True
        OptInternal.Focus()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim OpsiInternal As Byte, BahasaInd As Byte
        If OptBahasaIndonesia.Checked = False And OptBahasaInggris.Checked = False Then
            MsgBox("Opsi pencetakan bahasa belum di pilih!", vbOKOnly + vbCritical, ".:Warning!")
            Exit Sub
        End If
        If OptExternal.Checked = False And OptInternal.Checked = False Then
            MsgBox("Opsi pencetakan belum di pilih!", vbOKOnly + vbCritical, ".:Warning!")
            Exit Sub
        End If
        If NamaFile.Text = "" Then
            MsgBox("Nama file belum di isi!", vbOKOnly + vbCritical, ".:Warning!")
            If LAdd Or LEdit Then NamaFile.Focus()
            Exit Sub
        End If

        If KodeProduk.Text = "" Then
            MsgBox("Produk belum di isi!", vbOKOnly + vbCritical, ".:Warning!")
            If LAdd Or LEdit Then KodeProduk.Focus()
            Exit Sub
        End If

        If Trim(Dir(FotoLoc + "\" + Trim(KodeProduk.Text))) + ".jpg" = "" Or Trim(LocGmb1.Text) = "" Then
            LocGmb1.Text = ""
        Else
            LocGmb1.Text = Trim(FotoLoc) + "\" + Trim(KodeProduk.Text) + ".jpg"
        End If


        If Trim(Konversi.Text) = "" Then Konversi.Text = 0
        If Trim(HargaJual.Text) = "" Then HargaJual.Text = 0
        If Trim(HargaUSD.Text) = "" Then HargaUSD.Text = 0
        If Trim(Kapasitas.Text) = "" Then Kapasitas.Text = 0
        Dim rsCek As New DataTable
        If LAdd Then
            SQL = "Select * From t_KatalogProduk " &
                    "Where NamaFile = '" & Trim(NamaFile.Text) & "' " &
                    "  And AktifYN = 'Y' "
            rsCek = Proses.ExecuteQuery(SQL)
            If rsCek.Rows.Count <> 0 Then
                MsgBox("Nama file katalog : " & NamaFile.Text & " sudah pernah di buat!", vbCritical + vbOKOnly, ".:Nama File Kembar !")
                NamaFile.Focus()
                Exit Sub
            End If
            Proses.CloseConn()
        ElseIf lTambah Then
            SQL = "Select * From t_KatalogProduk " &
                    "Where NamaFile = '" & Trim(NamaFile.Text) & "' " &
                    "  And KodeProduk = '" & Trim(KodeProduk.Text) & "' " &
                    "  And AktifYN = 'Y' "
            rsCek = Proses.ExecuteQuery(SQL)
            If rsCek.Rows.Count <> 0 Then
                MsgBox("Produk " & KodeProduk.Text & " sudah pernah di buat untuk katalog : " & NamaFile.Text & " !", vbOKOnly + vbCritical + vbOKOnly, ".:Double produk di katalog " & NamaFile.Text & "!")
                KodeProduk.Focus()
                Exit Sub
            End If
            Proses.CloseConn()
        End If
        Me.Cursor = Cursors.WaitCursor
        cmdSimpan.Enabled = False
        If LAdd Or lTambah Then
            SQL = "Select isnull(Max(IdRec),0) + 1 as idrec From t_KatalogProduk "
            rsCek = Proses.ExecuteQuery(SQL)
            If rsCek.Rows.Count <> 0 Then
                IDRecord.Text = rsCek.Rows(0) !IdRec
            End If

            If OptInternal.Checked = True And OptExternal.Checked = False Then
                OpsiInternal = 1
            Else
                OpsiInternal = 0
            End If
            If OptBahasaIndonesia.Checked = True And OptBahasaInggris.Checked = False Then
                BahasaInd = 1
            Else
                BahasaInd = 0
            End If
            SQL = "INSERT INTO t_KatalogProduk(IdRec, OpsiInternal, BahasaInd, NamaFile, " &
                     "Importir, Kode_Importir, KodeProduk, NamaProduk, HargaAsli, Konversi, " &
                     "HargaJual, Kapasitas, Panjang, Lebar, Tinggi, Tebal, Diameter, Berat, " &
                     "HargaBeli, HargaFOB, MataUang, AktifYN, UserID, LastUPD, FileGambar, Gambar) " &
                     "VALUES (" & IDRecord.Text & ", " & OpsiInternal & " , " & BahasaInd & ",  " &
                     "'" & Trim(NamaFile.Text) & "', '" & Trim(Microsoft.VisualBasic.Left(cmbSupplier.Text, 100)) & "', " &
                     "'" & Trim(Microsoft.VisualBasic.Right(cmbSupplier.Text, 10)) & "', '" & KodeProduk.Text & "', " &
                     "'" & Produk.Text & "', " & HargaUSD.Text * 1 & ", " & Konversi.Text * 1 & ", " &
                     "" & HargaJual.Text * 1 & ", " & Kapasitas.Text * 1 & ", " & Panjang.Text * 1 & ", " &
                     "" & Lebar.Text * 1 & ", " & Tinggi.Text * 1 & ", " & Tebal.Text * 1 & ", " &
                     "" & Diameter.Text * 1 & ", " & Berat.Text * 1 & ", " & HargaBeli.Text * 1 & ", " &
                     "" & HargaFOB.Text * 1 & ", '" & cmbMataUang.Text & "', 'Y', '" & UserID & "', " &
                     "GetDate(), '" & LocGmb1.Text & "', '')"
            Proses.ExecuteNonQuery(SQL)
            If LocGmb1.Text <> "" Then
                store_pic_Sql(PictureBox1.Image)
            End If
            cmdSimpan.Enabled = True
            LAdd = False
            LEdit = False
            lTambah = True
            ClearProduk()
            cmbMataUang.Enabled = False
            NamaFile.Enabled = False
            cmbSupplier.Enabled = False
            KodeProduk.Focus()
        ElseIf LEdit Then
            SQL = "Update t_KatalogProduk Set " &
                    "KodeProduk = '" & KodeProduk.Text & "', NamaProduk = '" & Produk.Text & "', " &
                    "HargaAsli = " & HargaUSD.Text * 1 & ", Konversi = " & Konversi.Text * 1 & ", " &
                    "HargaJual = " & HargaJual.Text * 1 & ", Kapasitas = " & Kapasitas.Text * 1 & ", " &
                    "Panjang = " & Panjang.Text * 1 & ", Lebar = " & Lebar.Text * 1 & ", " &
                    "Tinggi = " & Tinggi.Text * 1 & ", Tebal = " & Tebal.Text * 1 & ", " &
                    "Diameter = " & Diameter.Text * 1 & ", Berat = " & Berat.Text * 1 & ", " &
                    "HargaBeli = " & HargaBeli.Text * 1 & ", HargaFOB = " & HargaFOB.Text * 1 & ", " &
                    "MataUang = '" & cmbMataUang.Text & "', " &
                    "UserID = '" & UserID & "', LastUPD = GetDate(), FileGambar = '" & LocGmb1.Text & "' " &
                    "Where IdRec = " & IDRecord.Text & " "
            Proses.ExecuteNonQuery(SQL)

            If LocGmb1.Text <> "" Then
                store_pic_Sql(PictureBox1.Image)
            End If
            LAdd = False
            LEdit = False
            lTambah = False
            AturTombol(True)
        End If
        cmdSimpan.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        lTambah = False
        AturTombol(True)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub ClearProduk()
        KodeProduk.Text = ""
        Produk.Text = ""
        HargaJual.Text = 0
        HargaUSD.Text = 0
        Konversi.Text = 0
        Kapasitas.Text = 0
        Panjang.Text = 0
        Lebar.Text = 0
        Tinggi.Text = 0
        Tebal.Text = 0
        Diameter.Text = 0
        Berat.Text = 0
        HargaBeli.Text = 0
        HargaFOB.Text = 0
        LocGmb1.Text = ""
        PictureBox1.BackgroundImage = Nothing
        PictureBox1.Image = Nothing
    End Sub

    Private Sub cmbMataUang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMataUang.SelectedIndexChanged
        If Trim(HargaUSD.Text) = "" Then HargaUSD.Text = 0
        If Trim(Konversi.Text) = "" Then Konversi.Text = 1
        SQL = "SELECT ERCurrent FROM M_Currency " &
            " WHERE aktifYN='Y'  " &
            "   And KODE = '" & cmbMataUang.Text & "' "
        Dim mKonversi As Double = Proses.ExecuteSingleDblQuery(SQL)
        If LAdd Or LEdit Then
            If cmbMataUang.Text = "USD" Then
                Konversi.Text = 1
                tKonversi.Text = 1
                HargaJual.Text = Format((HargaUSD.Text * 1) * (Konversi.Text * 1), "###,##0.00")
            ElseIf cmbMataUang.Text = "RP" Then
                Konversi.Text = Format(mKonversi, "###,##0")
                tKonversi.Text = Format(mKonversi, "###,##0")
                HargaJual.Text = Format((HargaUSD.Text * 1) * (Konversi.Text * 1), "###,##0.00")
            ElseIf cmbMataUang.Text = "EURO" Then
                Konversi.Text = Format(mKonversi, "###,##0.0000")
                tKonversi.Text = Format(mKonversi, "###,##0.0000")
                HargaJual.Text = Format((HargaUSD.Text * 1) / (Konversi.Text * 1), "###,##0.00")
            End If
            tKonversi.Visible = False
            LKonv.Visible = False
        Else
            tKonversi.Visible = True
            LKonv.Visible = True

            If cmbMataUang.Text = "USD" Then
                tKonversi.Text = 1
            ElseIf cmbMataUang.Text = "EURO" Then
                tKonversi.Text = Format(mKonversi, "###,##0.0000")
            ElseIf cmbMataUang.Text = "RP" Then
                tKonversi.Text = Format(mKonversi, "###,##0")
            End If
        End If
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        lTambah = False
        AturTombol(False)
        cmdSimpan.Visible = tEdit
        cmbSupplier.Enabled = False
        KodeProduk.Focus()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged
        If Len(Trim(KodeProduk.Text)) < 1 Then
            ClearProduk()
        ElseIf Len(Trim(KodeProduk.Text)) = 5 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        ElseIf Len(Trim(KodeProduk.Text)) = 8 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        End If
    End Sub

    Private Sub KodeProduk_GotFocus(sender As Object, e As EventArgs) Handles KodeProduk.GotFocus
        With KodeProduk
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim RS05 As New DataTable, MsgSQL As String = ""

            Me.Cursor = Cursors.WaitCursor
            MsgSQL = "Select deskripsi, cur_rp, tamb_sp, perajin, kode_perajin, " &
                    "Panjang, Lebar, Tinggi, Diameter, Tebal, Berat " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' and kodeproduk <> '' "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            If RS05.Rows.Count <> 0 Then
                IsiProduk()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodeProduk " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodeProduk Like '%" & KodeProduk.Text & "%' or Deskripsi Like '%" & KodeProduk.Text & "%') " &
                    "Order By Deskripsi "
                Form_Daftar.Text = "Daftar Produk"
                Form_Daftar.ShowDialog()
                KodeProduk.Text = FrmMenuUtama.TSKeterangan.Text
                MsgSQL = "Select deskripsi, cur_rp, tamb_sp, perajin, kode_perajin " &
                    "Panjang, Lebar, Tinggi, Diameter, Tebal, Berat " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                If RS05.Rows.Count <> 0 Then
                    IsiProduk()
                Else
                    ClearProduk()
                    KodeProduk.Focus()
                End If
            End If
            If LAdd Or LEdit Or lTambah Then
                If Produk.Text = "" Then
                    KodeProduk.Focus()
                Else
                    Konversi.Focus()
                End If
            End If
        End If
        Me.Cursor = Cursors.Default

    End Sub
    Private Sub IsiProduk()
        Dim dbProduk As New DataTable
        SQL = "SELECT ERCurrent FROM M_Currency " &
            " WHERE aktifYN='Y'  " &
            "   And KODE = '" & cmbMataUang.Text & "' "
        Dim mKonversi As Double = Proses.ExecuteSingleDblQuery(SQL)
        Konversi.Text = Replace(Format(mKonversi, "###,##0.0000"), ".0000", "")
        SQL = "Select * " &
            " From m_KodeProduk " &
            "Where KodeProduk = '" & KodeProduk.Text & "' "
        dbProduk = Proses.ExecuteQuery(SQL)

        If dbProduk.Rows.Count <> 0 Then
            If OptBahasaIndonesia.Checked = True And OptBahasaInggris.Checked = False Then
                Produk.Text = Replace(dbProduk.Rows(0) !Deskripsi, "'", "`")
            ElseIf OptBahasaIndonesia.Checked = False And OptBahasaInggris.Checked = True Then
                Produk.Text = Replace(dbProduk.Rows(0) !Descript, "'", "`")
            Else
                MsgBox("Bahasa Belum dipilih!", vbCritical + vbOKOnly, ".:Warning")
                Exit Sub
            End If
            HargaJual.Text = Format(dbProduk.Rows(0) !Cur_USD, "###,##0.00")
            Kapasitas.Text = Format(dbProduk.Rows(0) !Kapasitas, "###,##0")
            Panjang.Text = Format(dbProduk.Rows(0) !Panjang, "###,##0.00")
            Lebar.Text = Format(dbProduk.Rows(0) !Lebar, "###,##0.00")
            Tinggi.Text = Format(dbProduk.Rows(0) !Tinggi, "###,##0.00")
            Tebal.Text = Format(dbProduk.Rows(0) !Tebal, "###,##0.00")
            Diameter.Text = Format(dbProduk.Rows(0) !Diameter, "###,##0.00")
            Berat.Text = Format(dbProduk.Rows(0) !Berat, "###,##0.00")
            HargaBeli.Text = Replace(Format(dbProduk.Rows(0) !cur_rp, "###,##0.00"), ".00", "")
            HargaFOB.Text = Replace(Format(dbProduk.Rows(0) !Cur_USD, "###,##0.00"), ".00", "")
            HargaUSD.Text = Replace(Format(dbProduk.Rows(0) !Cur_USD, "###,##0.00"), ".00", "")
            If cmbMataUang.Text = "USD" Then
                Konversi.Text = 1
                HargaJual.Text = Replace(Format((HargaUSD.Text * 1) * (Konversi.Text * 1), "###,##0.00"), ".00", "")
            ElseIf cmbMataUang.Text = "RP" Then
                HargaJual.Text = Replace(Format((HargaUSD.Text * 1) * (Konversi.Text * 1), "###,##0.00"), ".00", "")
            ElseIf cmbMataUang.Text = "EURO" Then
                If Trim(Konversi.Text) = "" Then Konversi.Text = 1.35
                HargaJual.Text = Replace(Format((HargaUSD.Text * 1) / (Konversi.Text * 1), "###,##0.00"), ".00", "")
            End If
            LocGmb1.Text = dbProduk.Rows(0) !file_foto
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                LocGmb1.Text = ""
                PictureBox1.BackgroundImage = Nothing
                PictureBox1.Image = Nothing
            Else
                LoadPic(LocGmb1.Text)
            End If
        End If
    End Sub

    Private Sub IsiKatalog(tCode As String)
        Dim MsgSQL As String, RSK As New DataTable, i As Integer, Ketemu As Boolean
        If Trim(tCode) = "" Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select *, DataLength(Gambar) LengthGambar " &
            " From T_KatalogProduk " &
            "Where IdRec = " & tCode & " " &
            " And AktifYN = 'Y'"
        RSK = Proses.ExecuteQuery(MsgSQL)
        If RSK.Rows.Count <> 0 Then
            NamaFile.Text = RSK.Rows(0) !NamaFile
            cmbMataUang.Text = RSK.Rows(0) !MataUang
            KodeProduk.Text = RSK.Rows(0) !KodeProduk
            Produk.Text = RSK.Rows(0) !NamaProduk
            Konversi.Text = RSK.Rows(0) !Konversi
            Ketemu = False
            For i = 0 To cmbSupplier.Items.Count - 1
                cmbSupplier.SelectedIndex = i
                If Trim(Microsoft.VisualBasic.Right(cmbSupplier.Text, 10)) = Trim(RSK.Rows(0) !Kode_Importir) Then
                    Ketemu = True
                    Exit For
                End If
            Next i

            If Not Ketemu Then cmbSupplier.SelectedIndex = -1
            If RSK.Rows(0) !BahasaInd = True Then
                OptBahasaIndonesia.Checked = True
                OptBahasaInggris.Checked = False
            Else
                OptBahasaIndonesia.Checked = False
                OptBahasaInggris.Checked = True
            End If

            If RSK.Rows(0) !OpsiInternal = True Then
                OptInternal.Checked = True
                OptExternal.Checked = False
            Else
                OptInternal.Checked = False
                OptExternal.Checked = True
            End If


            HargaJual.Text = Replace(Format(RSK.Rows(0) !HargaJual, "###,##0.00"), ".00", "")
            HargaUSD.Text = Replace(Format(RSK.Rows(0) !hargaasli, "###,##0.00"), ".00", "")
            Konversi.Text = Format(RSK.Rows(0) !Konversi, "###,##0.00")
            Kapasitas.Text = Format(RSK.Rows(0) !Kapasitas, "###,##0")
            Panjang.Text = Format(RSK.Rows(0) !Panjang, "###,##0.00")
            Lebar.Text = Format(RSK.Rows(0) !Lebar, "###,##0.00")
            Tinggi.Text = Format(RSK.Rows(0) !Tinggi, "###,##0.00")
            Tebal.Text = Format(RSK.Rows(0) !Tebal, "###,##0.00")
            Diameter.Text = Format(RSK.Rows(0) !Diameter, "###,##0.00")
            Berat.Text = Format(RSK.Rows(0) !Berat, "###,##0.00")
            HargaBeli.Text = Replace(Format(RSK.Rows(0) !HargaBeli, "###,##0.00"), ".00", "")
            HargaFOB.Text = Replace(Format(RSK.Rows(0) !HargaFOB, "###,##0.00"), ".00", "")

            If cmbMataUang.Text = "USD" Then
                Konversi.Text = 1
                HargaJual.Text = Replace(Format((HargaUSD.Text * 1) * (Konversi.Text * 1), "###,##0.00"), ".00", "")
            ElseIf cmbMataUang.Text = "RP" Then
                HargaJual.Text = Replace(Format((HargaUSD.Text * 1) * (Konversi.Text * 1), "###,##0.00"), ".00", "")
            ElseIf cmbMataUang.Text = "EURO" Then
                If Trim(Konversi.Text) = "" Then Konversi.Text = 1.35
                HargaJual.Text = Replace(Format((HargaUSD.Text * 1) / (Konversi.Text * 1), "###,##0.00"), ".00", "")
            End If

            If RSK.Rows(0) !LengthGambar = 0 Then
                PictureBox1.BackgroundImage = Nothing
                PictureBox1.Image = Nothing
            Else
                PictureBox1.BackgroundImage = Nothing
                PictureBox1.Image = Nothing
                LocGmb1.Text = KodeProduk.Text + ".jpg"
                If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                    LocGmb1.Text = ""
                Else
                    ShowFoto(FotoLoc + "\" + LocGmb1.Text)
                End If

            End If

        End If
        Proses.CloseConn()
        Me.Cursor = Cursors.Default
        cmbMataUang.Enabled = True
    End Sub

    Private Sub Retrieve(idRecord As String)
        Dim ipserver As String = My.Settings.IPServer
        Dim pwd As String = My.Settings.Password
        Dim dbUser As String = My.Settings.UserID
        Dim database As String = My.Settings.Database
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlC As String = "Initial Catalog=" & database & "; " &
                "user id=" & dbUser & ";password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";"
        conn.ConnectionString = sqlC
        conn.Open()
        SQL = "SELECT gambar FROM t_katalogProduk WHERE IDRec='" & idRecord & "' "
        Dim cmd = New SqlCommand(SQL, conn)
        Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())
        If IsDBNull(cmd.ExecuteScalar()) = False Then
            If Not IsNothing(conn) Then
                conn.Close()
                conn = Nothing
            End If
            Exit Sub
        End If
        If Not imageData Is Nothing Then
            Using ms As New MemoryStream(imageData, 0, imageData.Length)
                ms.Write(imageData, 0, imageData.Length)
                PictureBox1.BackgroundImage = Image.FromStream(ms, True)
            End Using
        End If
        If Not IsNothing(conn) Then
            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim footer1 As String = "", footer2 As String = "", footer3 As String = ""
        Dim nPrinter As String = "", nKertas As String = "", nPrintYN As String = ""

        Me.Cursor = Cursors.WaitCursor

        Form_Report.Text = "Daftar Produk"
        ' terbilang = "#" + tb.Terbilang(CDbl(Form_InvoiceCustomer.Total.Text)) + " Rupiah #"  
        nPrinter = My.Settings.NamaPrinter
        nKertas = My.Settings.NamaKertas
        nPrintYN = FrmMenuUtama.TSKeterangan.Text
        Proses.OpenConn(CN)

        dttable = New DataTable
        SQL = "SELECT t_KatalogProduk.IdRec, t_KatalogProduk.NamaFile, t_KatalogProduk.Importir, " &
            "  t_KatalogProduk.KodeProduk, t_KatalogProduk.NamaProduk, t_KatalogProduk.HargaJual, " &
            "  t_KatalogProduk.Kapasitas, t_KatalogProduk.Panjang, t_KatalogProduk.Lebar, " &
            "  t_KatalogProduk.Tinggi, t_KatalogProduk.Tebal, t_KatalogProduk.Diameter, " &
            "  t_KatalogProduk.Berat, t_KatalogProduk.MataUang, t_KatalogProduk.Gambar, " &
            "  m_KodeImportir.Alamat , m_KodeBahan.NamaIndonesia " &
            "  FROM Pekerti.dbo.t_KatalogProduk t_KatalogProduk INNER JOIN " &
            "     Pekerti.dbo.m_KodeBahan m_KodeBahan ON substring(t_KatalogProduk.KodeProduk,7,2) = m_KodeBahan.KodeBahan " &
            "     INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON t_KatalogProduk.Kode_Importir = m_KodeImportir.KodeImportir " &
            " Where t_KatalogProduk.AktifYN = 'Y' " &
            "  And t_KatalogProduk.NamaFile = '" & NamaFile.Text & "' " &
            "Order By KodeProduk "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_KatalogProduk_InInd
            objRep.SetDataSource(dttable)
            Form_Report.Text = "Cetak Katalog Sample"
            Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_Report.CrystalReportViewer1.Refresh()
            Form_Report.CrystalReportViewer1.ReportSource = objRep
            Form_Report.CrystalReportViewer1.ShowRefreshButton = False
            Form_Report.CrystalReportViewer1.ShowPrintButton = True
            Form_Report.CrystalReportViewer1.ShowExportButton = True
            Form_Report.CrystalReportViewer1.ShowParameterPanelButton = False
            Form_Report.ShowDialog()
            dttable.Dispose()
            DTadapter.Dispose()
            Proses.CloseConn(CN)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Sub store_pic_Sql(ByVal img As Image)
        Dim ipserver As String = My.Settings.IPServer
        Dim pwd As String = My.Settings.Password
        Dim dbUser As String = My.Settings.UserID
        Dim database As String = My.Settings.Database
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlC As String = ""
        sqlC = "Initial Catalog=" & database & "; " &
                "user id=" & dbUser & ";password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";"
        conn.ConnectionString = sqlC
        conn.Open()
        'SQL = "SELECT gambar FROM t_katalogProduk WHERE IDRec='" & IDRecord & "' "
        'SQL = "insert into agtemp.dbo.m_barang_foto values('" & IdRec.Text & "',@name, @photo, '" & UserID & "',  GetDate() ) "
        SQL = "UPDATE t_katalogProduk SET gambar = @gambar " &
            " WHERE IDRec='" & IDRecord.Text & "' "
        Dim cmd As SqlCommand = New SqlCommand(SQL, conn)
        Dim ms As New MemoryStream()
        PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
        Dim data As Byte() = ms.GetBuffer()
        Dim p As New SqlParameter("@gambar", SqlDbType.Image)
        p.Value = data
        cmd.Parameters.Add(p)
        cmd.ExecuteNonQuery()
        If Not IsNothing(conn) Then
            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, RSP As New DataTable
        Dim tImportir As String = "", tNamaFile As String = ""
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Visible = False
        DGView2.Rows.Clear()
        tNamaFile = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        tImportir = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value
        MsgSQL = "Select * From t_KatalogProduk " &
            "Where aktifYN = 'Y' " &
            "  And NamaFile = '" & tNamaFile & "'  " &
            "  And Kode_Importir = '" & tImportir & "' " &
            "order by IdRec "
        RSP = Proses.ExecuteQuery(MsgSQL)

        RSP = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSP.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSP.Rows(a) !IdRec,
                RSP.Rows(a) !KodeProduk,
                RSP.Rows(a) !NamaProduk,
                Format(RSP.Rows(a) !Kapasitas, "###,##0"),
                RSP.Rows(a) !MataUang,
                Format(RSP.Rows(a) !HargaJual, "###,##0.00"),
                Format(RSP.Rows(a) !HargaBeli, "###,##0.00"),
                Format(RSP.Rows(a) !HargaFOB, "###,##0.00"),
                Replace(Format(RSP.Rows(a) !Konversi, "###,##0.00"), ".00", ""))
        Next (a)
        DGView2.Visible = True
    End Sub

    Private Sub DGView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        Dim MsgSQL As String, RSP As New DataTable
        Dim tImportir As String = "", tNamaFile As String = ""
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Visible = False
        DGView2.Rows.Clear()
        tNamaFile = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        tImportir = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value
        MsgSQL = "Select * From t_KatalogProduk " &
            "Where aktifYN = 'Y' " &
            "  And NamaFile = '" & tNamaFile & "'  " &
            "  And Kode_Importir = '" & tImportir & "' " &
            "order by IdRec "
        RSP = Proses.ExecuteQuery(MsgSQL)
        If RSP.Rows.Count <> 0 Then
            IDRecord.Text = RSP.Rows(0) !idrec
            Call IsiKatalog(IDRecord.Text)
        End If
        TabControl1.SelectedTab = TabPageFormEntry_
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Me.Cursor = Cursors.WaitCursor
        DGView2.Enabled = False
        If DGView2.Rows.Count = 0 Then Exit Sub
        Dim tCode As String = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        If tCode = "" Then Exit Sub
        IDRecord.Text = tCode
        Call IsiKatalog(tCode)
        DGView2.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub DGView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellDoubleClick
        Me.Cursor = Cursors.WaitCursor
        DGView2.Enabled = False
        If DGView2.Rows.Count = 0 Then Exit Sub
        Dim tCode As String = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        If tCode = "" Then Exit Sub
        IDRecord.Text = tCode
        Call IsiKatalog(tCode)
        DGView2.Enabled = True
        Me.Cursor = Cursors.Default

        TabControl1.SelectedTab = TabPageFormEntry_
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        If MsgBox("Apakah anda yakin hapus katalog '" & NamaFile.Text & "' ?", vbYesNo + vbInformation) = vbYes Then
            MsgSQL = "Update T_KatalogProduk Set AktifYN = 'N', Lastupd = getdate(), userid = '" & UserID & "' " &
                "Where NamaFile = '" & NamaFile.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            Dim rs As New DataTable
            MsgSQL = "Select Top 1 * From T_KatalogProduk " &
                "Where aktifYN = 'Y' " &
                "Order By IdRec desc "
            rs = Proses.ExecuteQuery(MsgSQL)
            If rs.Rows.Count <> 0 Then
                IsiKatalog(rs.Rows(0) !idrec)
            Else
                ClearTextBoxes()
            End If
        End If
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = 0 Then
        ElseIf e.TabPageIndex = 1 Then
            DaftarKatalog()
        End If
    End Sub
End Class


'Private Sub btnBrowseImg_Click(sender As Object, e As EventArgs) Handles btnBrowseImg.Click
'    OpenFileDialog1.InitialDirectory = "c:\"
'    OpenFileDialog1.Filter = "JPG (*.jpg)|*.jpg|Bitmap File (*.bmp)|*.bmp|PNG (*.png)|*.png|All files (*.*)|*.*"
'    OpenFileDialog1.FilterIndex = 4
'    OpenFileDialog1.RestoreDirectory = True
'    If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
'        PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
'        namaFile.Text = OpenFileDialog1.FileName
'        If Len(namaFile.Text) > 100 Then
'            MsgBox("Nama Lokasi File Gambar Terlalu Panjang !" & vbCrLf &
'                        "Hanya bisa 100 character.", vbCritical, ".:Warning !")
'        End If
'    End If
'End Sub