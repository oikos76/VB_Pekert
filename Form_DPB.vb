Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class Form_DPB
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        lKoordinator As String, lPemeriksa As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Private DA As SqlDataAdapter
    Private CN As SqlConnection
    Private Cmd As SqlCommand

    Private Sub tambahDPB()
        If Trim(nodpb.Text) = "" Then
            MsgBox("No DPB masih kosong!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        LAdd = False
        LEdit = False
        AturTombol(False)
        NoLHP.Focus()
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPB " &
            "Where AktifYN = 'Y' " &
            "  And nodpb = '" & nodpb.Text & "' " &
            "ORDER BY tgldpb, IDRec "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call ISIdPb(RSNav.Rows(0) !IdRec)
        End If
        Proses.CloseConn()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSQL As String
        Dim rs04 As New DataTable, rs05 As New DataTable
        If LAdd Or LEdit Or LTambahKode Then
            If Trim(nodpb.Text) = "" Then
                MsgBox("No DPB masih kosong!", vbOKOnly + vbCritical, ".:ERROR!")
                nodpb.Focus()
                Exit Sub
            End If
            If Trim(OngKir.Text) = "" Then OngKir.Text = 0
            If Trim(Jumlah.Text) = "" Then Jumlah.Text = 0
            If LAdd Or LTambahKode Then
                MsgSQL = "Select NoLHP From t_DPB " &
                    "Where NoLHP = '" & NoLHP.Text & "' " &
                    "  and aktifYN = 'Y' "
                rs04 = Proses.ExecuteQuery(MsgSQL)
                If rs04.Rows.Count <> 0 Then
                    MsgBox("Hi " & Trim(UserID) & "... No LHP " & NoLHP.Text & " sudah pernah di buat", vbCritical, ".:Error!")
                    Exit Sub
                End If
                MsgSQL = "Select * From T_LHP " &
                    "Where NoLHP = '" & NoLHP.Text & "' " &
                    "  And AktifYN = 'Y' " &
                    "Order By LastUPD "
                rs05 = Proses.ExecuteQuery(MsgSQL)
                For a = 0 To rs05.Rows.Count - 1
                    Application.DoEvents()
                    IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_DPB", "PB")
                    Produk.Text = rs05.Rows(0) !Produk
                    KodePerajin.Text = rs05.Rows(0) !KodePerajin
                    Jumlah.Text = rs05.Rows(0) !JumlahBaik
                    MsgSQL = "select KodePerajin, Jumlah, CatatanProduk, CatatanSP, TglMasukGudang " &
                        "From t_SP " &
                        "Where NoSP = '" & rs05.Rows(0) !NoSP & "' " &
                        " And KodeProduk = '" & rs05.Rows(0) !Kode_Produk & "' "
                    rs04 = Proses.ExecuteQuery(MsgSQL)
                    If rs04.Rows.Count <> 0 Then
                        SpecProduk.Text = rs04.Rows(0) !CatatanProduk
                        Keterangan.Text = rs04.Rows(0) !CatatanSP
                        DeadlineSP.Value = rs04.Rows(0) !TglMasukGudang
                    Else
                        SpecProduk.Text = ""
                        Keterangan.Text = ""
                        DeadlineSP.Value = rs05.Rows(0) !TglSelesaiPeriksa
                    End If
                    NoSP.Text = rs05.Rows(0) !NoSP
                    KodeProduk.Text = rs05.Rows(0) !Kode_Produk
                    '                DeadlineSP.Value = RS05!TglSelesaiPeriksa
                    HargaBeli.Text = rs05.Rows(0) !HargaBeli
                    MsgSQL = "INSERT INTO t_DPB(IdRec, NoDPB, TglDPB, NoSP, NoLHP, " &
                        " Kode_Produk, NamaProduk, KodePerajin, Jumlah, HargaBeli, " &
                        " DeadlineSP, Ongkir, SpecProduk,Keterangan, Pengirim, SPB, " &
                        " TglTerima, Kargo, AktifYN, LastUPD, UserID, tglcetak,ID," &
                        " StatusDPB, TransferYN) VALUES('" & IDRecord.Text & "', '" & nodpb.Text & "'," &
                        " '" & Format(TglDPB.Value, "yyyy-MM-dd") & "', " &
                        "'" & rs05.Rows(0) !NoSP & "', '" & NoLHP.Text & "', '" & rs05.Rows(0) !Kode_Produk & "', " &
                        "'" & rs05.Rows(0) !Produk & "','" & KodePerajin.Text & "'," & Jumlah.Text * 1 & ", " &
                        "" & rs05.Rows(0) !HargaBeli & ", '" & Format(DeadlineSP.Value, "yyyy-MM-dd") & "', " &
                        "" & OngKir.Text * 1 & ", '" & Trim(SpecProduk.Text) & "', " &
                        "'" & Trim(Keterangan.Text) & "','" & Trim(rs05.Rows(0) !NamaPerajin) & "', " &
                        "'" & Trim(rs05.Rows(0) !NoSPB) & "', '" & rs05.Rows(0) !TglTerima & "', " &
                        "'" & Trim(rs05.Rows(0) !Kargo) & "', 'Y', GetDate(), '" & UserID & "','', '','', 'N') "
                    Proses.ExecuteNonQuery(MsgSQL)
                Next a
                NoLHP.Text = ""
                Pengirim.Text = ""
                Keterangan.Text = ""
                OngKir.Text = 0
                tambahDPB()
            ElseIf LEdit Then
                MsgSQL = "Delete t_DPB " &
                    "Where NoDPB = '" & nodpb.Text & "' " &
                    "  And NoLHP = '" & NoLHP.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "Select * From T_LHP " &
                    "Where NoLHP = '" & NoLHP.Text & "' " &
                    "  And AktifYN = 'Y' " &
                    "Order By LastUPD "
                rs05 = Proses.ExecuteQuery(MsgSQL)
                For a = 0 To rs05.Rows.Count - 1
                    Application.DoEvents()
                    IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_DPB", "PB")
                    Produk.Text = rs05.Rows(0) !Produk
                    KodePerajin.Text = rs05.Rows(0) !KodePerajin
                    Jumlah.Text = rs05.Rows(0) !JumlahBaik
                    MsgSQL = "select KodePerajin, Jumlah, CatatanProduk, CatatanSP, TglMasukGudang " &
                        "From t_SP " &
                        "Where NoSP = '" & rs05.Rows(0) !NoSP & "' " &
                        " And KodeProduk = '" & rs05.Rows(0) !Kode_Produk & "' "
                    rs04 = Proses.ExecuteQuery(MsgSQL)
                    If rs04.Rows.Count <> 0 Then
                        SpecProduk.Text = rs04.Rows(0) !CatatanProduk
                        Keterangan.Text = rs04.Rows(0) !CatatanSP
                        DeadlineSP.Value = rs04.Rows(0) !TglMasukGudang
                    Else
                        SpecProduk.Text = ""
                        Keterangan.Text = ""
                        DeadlineSP.Value = rs05.Rows(0) !TglSelesaiPeriksa
                    End If
                    NoSP.Text = rs05.Rows(0) !NoSP
                    KodeProduk.Text = rs05.Rows(0) !Kode_Produk
                    'DeadlineSP.Value = RS05!TglSelesaiPeriksa
                    HargaBeli.Text = rs05.Rows(0) !HargaBeli
                    MsgSQL = "INSERT INTO t_DPB(IdRec, NoDPB, TglDPB, StatusDPB, " &
                        "NoSP, NoLHP, Kode_Produk, NamaProduk, KodePerajin, " &
                        "Jumlah, HargaBeli, DeadlineSP, Ongkir, SpecProduk, " &
                        "Keterangan, Pengirim, SPB, TglTerima, Kargo, AktifYN, " &
                        "LastUPD, UserID, tglcetak,ID, TransferYN) VALUES('" & IDRecord.Text & "', " &
                        "'" & nodpb.Text & "', '" & Format(TglDPB.Value, "yyyy-MM-dd") & "', '', " &
                        "'" & rs05.Rows(0) !NoSP & "', '" & NoLHP.Text & "', '" & rs05.Rows(0) !Kode_Produk & "', " &
                        "'" & rs05.Rows(0) !Produk & "','" & KodePerajin.Text & "'," & Jumlah.Text * 1 & ", " &
                        "" & rs05.Rows(0) !HargaBeli & ", '" & Format(DeadlineSP.Value, "yyyy-MM-dd") & "', " &
                        "" & OngKir.Text * 1 & ", '" & Trim(SpecProduk.Text) & "', " &
                        "'" & Trim(Keterangan.Text) & "','" & Trim(rs05.Rows(0) !NamaPerajin) & "', " &
                        "'" & Trim(rs05.Rows(0) !NoSPB) & "', '" & rs05.Rows(0) !TglTerima & "', " &
                        "'" & Trim(rs05.Rows(0) !Kargo) & "', 'Y', GetDate(), '" & UserID & "','', '', 'N') "
                    Proses.ExecuteNonQuery(MsgSQL)
                Next (a)
                AturTombol(True)
                LAdd = False
                LEdit = False
                LTambahKode = False
            End If
        Else
            'CetakDPB
        End If
        DaftarDPB()
    End Sub
    Private Sub Form_DPB_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String
        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Dim rs05 As New DataTable
        MsgSQL = "Select KoordinatorLHP, pemeriksa From M_Company "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            lKoordinator = rs05.Rows(0) !KoordinatorLHP
            lPemeriksa = rs05.Rows(0) !Pemeriksa
        End If
        Dim Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_DPB " &
            "where AktifYN = 'Y' " &
            "Order By TglDPB Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiDPB(tIdRec)
        tTambah = Proses.UserAksesTombol(UserID, "32_DPB", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "32_DPB", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "32_DPB", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "32_DPB", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarDPB()
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

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
        ShowFoto("")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub ShowFoto(NamaFileJPG As String)
        If NamaFileJPG = "" Then
            LocGmb1.Text = ""
            PictureBox1.Image = Nothing
        End If
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.ImageLocation = filename
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_DPB " &
            "Where AktifYN = 'Y' " &
            "  And NoDPB = '" & nodpb.Text & "' " &
            "ORDER BY tgldpb desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiDPB(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPB " &
            "Where AktifYN = 'Y' " &
            "  And IDRec < '" & IDRecord.Text & "' " &
            "  And NoDPB = '" & nodpb.Text & "' " &
            "ORDER BY t_DPB.IDREC DESC "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiDPB(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPB " &
        "Where AktifYN = 'Y' " &
        "  And IDRec > '" & IDRecord.Text & "' " &
        "  And NoDPB = '" & nodpb.Text & "' " &
        "ORDER BY t_DPB.IDREC "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiDPB(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnPenambahanKodeDPB_Click(sender As Object, e As EventArgs) Handles btnPenambahanKodeDPB.Click
        If Trim(nodpb.Text) = "" Then
            MsgBox("No DPB masih kosong!", vbCritical + vbOKOnly, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        LAdd = False
        LEdit = False
        AturTombol(False)
        NoLHP.ReadOnly = False
        NoLHP.Focus()
    End Sub

    Private Sub btnPenambahanLHP_Click(sender As Object, e As EventArgs) Handles btnPenambahanLHP.Click
        If Trim(nodpb.Text) = "" Then
            MsgBox("No DPB masih kosong!", vbCritical + vbOKOnly, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        LAdd = False
        LEdit = False
        AturTombol(False)
        NoLHP.ReadOnly = False
        NoLHP.Text = ""
        NoLHP.Focus()
    End Sub

    Private Sub NoLHP_TextChanged(sender As Object, e As EventArgs) Handles NoLHP.TextChanged
        If Len(Trim(NoLHP.Text)) < 1 Then
            NoSP.Text = ""
            Pengirim.Text = ""
            SPB.Text = ""
            Kargo.Text = ""
            OngKir.Text = 0
        End If
    End Sub

    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 33
            .MinimumHeight = 33
        End With
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
        With Me.DGView2.RowTemplate
            .Height = 33
            .MinimumHeight = 33
        End With
        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView2.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub OngKir_TextChanged(sender As Object, e As EventArgs) Handles OngKir.TextChanged
        If Trim(OngKir.Text) = "" Then OngKir.Text = 0
        If IsNumeric(OngKir.Text) Then
            Dim temp As Double = OngKir.Text
            OngKir.Text = Format(temp, "###,##0")
            OngKir.SelectionStart = OngKir.TextLength
        Else
            OngKir.Text = 0
        End If
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Enabled = False
        Else
            cmdTambah.Visible = tAktif
        End If
        If tEdit = False Then
            cmdEdit.Enabled = False
            cmdSimpan.Visible = False
        Else
            cmdEdit.Visible = tAktif
            cmdSimpan.Visible = Not tAktif
        End If
        If tHapus = False Then
            cmdHapus.Enabled = False
        Else
            cmdHapus.Visible = tAktif
        End If

        btnPenambahanLHP.Visible = tAktif
        btnPenambahanKodeDPB.Visible = tAktif

        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True

        'Atur Readonly
        nodpb.ReadOnly = tAktif
        NoLHP.ReadOnly = tAktif
        Pengirim.ReadOnly = tAktif
        OngKir.ReadOnly = tAktif
        If LAdd Or LEdit Or LTambahKode Then
            TglDPB.Enabled = Not tAktif
            NoSP.Enabled = tAktif
            KodeProduk.Enabled = tAktif
            Produk.Enabled = tAktif
            KodePerajin.Enabled = tAktif
            Jumlah.Enabled = tAktif
            HargaBeli.Enabled = tAktif
            DeadlineSP.Enabled = tAktif
            SpecProduk.Enabled = tAktif
            Keterangan.Enabled = tAktif
            SPB.Enabled = tAktif
            Kargo.Enabled = tAktif
        Else
            TglDPB.Enabled = Not tAktif
            NoSP.ReadOnly = tAktif
            KodeProduk.ReadOnly = tAktif
            Produk.ReadOnly = tAktif
            KodePerajin.ReadOnly = True
            Jumlah.ReadOnly = True
            HargaBeli.ReadOnly = tAktif
            DeadlineSP.Enabled = Not tAktif
            SpecProduk.ReadOnly = tAktif
            Keterangan.ReadOnly = tAktif
            SPB.ReadOnly = False
            Kargo.ReadOnly = True
        End If
        TglTerima.Enabled = False
        Me.Text = "Daftar Penerimaan Barang"
    End Sub

    Private Sub Pengirim_TextChanged(sender As Object, e As EventArgs) Handles Pengirim.TextChanged

    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        AturTombol(False)
        ClearTextBoxes()
        nodpb.Text = Proses.MaxYNoUrut("NoDPB", "t_DPB", "DPB")
        NoLHP.Focus()
    End Sub
    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        LTambahKode = False
        AturTombol(False)
        NoLHP.Focus()
    End Sub
    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical + vbOKOnly, ".:Empty Data!")
            Exit Sub
        End If
        Form_Hapus.Left = Me.Left
        Form_Hapus.Top = Me.Top
        Form_Hapus.tIDSebagian.Text = IDRecord.Text
        Form_Hapus.tIDSemua.Text = nodpb.Text
        Form_Hapus.Text = "Hapus DPB"
        Form_Hapus.ShowDialog()
        DaftarDPB()
    End Sub
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click

        PanelNavigate.Enabled = False
        Form_Export2Excel.JenisTR.Text = "DPB"
        Form_Export2Excel.idRec.Text = nodpb.Text
        Form_Export2Excel.ShowDialog()
        PanelNavigate.Enabled = True

    End Sub

    Private Sub HargaBeli_TextChanged(sender As Object, e As EventArgs) Handles HargaBeli.TextChanged

    End Sub

    Private Sub Jumlah_TextChanged(sender As Object, e As EventArgs) Handles Jumlah.TextChanged

    End Sub

    Private Sub CariDPB_TextChanged(sender As Object, e As EventArgs) Handles CariDPB.TextChanged

    End Sub

    Private Sub IsiDPB(IdRec As String)
        Dim MsgSQL As String, RSP As New DataTable
        MsgSQL = "SELECT * " &
            " FROM t_DPB " &
            "WHERE IDRec = '" & IdRec & "' " &
            "  AND AktifYN = 'Y'"
        RSP = Proses.ExecuteQuery(MsgSQL)
        If RSP.Rows.Count <> 0 Then
            IDRecord.Text = RSP.Rows(0) !IdRec
            nodpb.Text = RSP.Rows(0) !NoDPB
            NoSP.Text = RSP.Rows(0) !NoSP
            TglDPB.Value = RSP.Rows(0) !TglDPB
            NoLHP.Text = RSP.Rows(0) !NoLHP
            KodeProduk.Text = RSP.Rows(0) !Kode_Produk
            StatusDPB.Text = RSP.Rows(0) !StatusDPB
            Produk.Text = RSP.Rows(0) !NamaProduk
            KodePerajin.Text = RSP.Rows(0) !KodePerajin
            Jumlah.Text = Format(RSP.Rows(0) !Jumlah, "###,##0")
            HargaBeli.Text = Format(RSP.Rows(0) !HargaBeli, "###,##0")
            DeadlineSP.Value = RSP.Rows(0) !DeadlineSP
            OngKir.Text = Format(RSP.Rows(0) !OngKir, "###,##0")
            SpecProduk.Text = RSP.Rows(0) !SpecProduk
            Keterangan.Text = RSP.Rows(0) !Keterangan
            Pengirim.Text = RSP.Rows(0) !Pengirim
            SPB.Text = RSP.Rows(0) !SPB
            TglTerima.Value = RSP.Rows(0) !TglTerima
            Kargo.Text = RSP.Rows(0) !Kargo
            If Format(RSP.Rows(0) !tglCetak, "dd-MM-yyyy") = "01-01-1900" Then
                tglCetak.Text = "-"
            Else
                tglCetak.Text = Format(RSP.Rows(0) !tglCetak, "dd-MM-yyyy")
            End If

            LocGmb1.Text = Trim(KodeProduk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
        End If
        Proses.CloseConn()
    End Sub

    Private Sub cariLHP_TextChanged(sender As Object, e As EventArgs) Handles cariLHP.TextChanged

    End Sub

    Private Sub DaftarDPB()
        Dim MsgSQL As String, RSDaf As New DataTable,
            mKondisi As String = ""
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        Me.Cursor = Cursors.WaitCursor
        If CariDPB.Text <> "" Then
            mKondisi = " And NoDPB like '%" & CariDPB.Text & "%' "
        End If
        If cariLHP.Text <> "" Then
            mKondisi = " and t_DPB.NoLHP like '%" & cariLHP.Text & "%' "
        End If
        If cariSP.Text <> "" Then
            mKondisi = " and t_DPB.NoSP like '%" & cariSP.Text & "%' "
        End If
        If cariKodeBrg.Text <> "" Then
            mKondisi = " and t_DPB.Kode_Produk like '%" & cariKodeBrg.Text & "%' "
        End If
        MsgSQL = "Select distinct NoDPB, TglDPB, pengirim as NamaPerajin, right(nodpb,2) + left(nodpb,3) " &
            " From t_DPB  " &
            "Where t_DPB.AktifYN = 'Y' " & mKondisi & "  " &
            "  and right(nodpb,2) not in ('s/', '99') " &
            "  and year(TglDPB) > 1999 " &
            "Order By right(nodpb,2) + left(nodpb,3) desc, TglDPB Desc, nodpb Desc "
        RSDaf = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSDaf.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(RSDaf.Rows(a) !NoDPB,
                   Format(RSDaf.Rows(a) !TglDPB, "dd-MM-yyyy"),
                   RSDaf.Rows(a) !NamaPerajin)
        Next a
        DGView.Visible = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cariSP_TextChanged(sender As Object, e As EventArgs) Handles cariSP.TextChanged

    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        If DGView2.Rows.Count = 0 Then Exit Sub
        IDRecord.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        IsiDPB(IDRecord.Text)
    End Sub

    Private Sub cariKodeBrg_TextChanged(sender As Object, e As EventArgs) Handles cariKodeBrg.TextChanged

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String
        Dim RSL As New DataTable, Produk As String = ""
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Rows.Clear()
        DGView2.Visible = False
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "SELECT IDRec, Kode_Produk, Kargo, KodePerajin, " &
            "      Jumlah, NoLHP, HargaBeli, TglTerima, DeadlineSP, NoSP " &
            " FROM t_DPB " &
            "WHERE t_DPB.AktifYN = 'Y' " &
            "  And NoDPB = '" & tCari & "' and jumlah <> 0 " &
            "ORDER BY t_DPB.NoSP, t_DPB.NoLHP, " &
            "         t_DPB.IDREC, t_DPB.KODE_PRODUK "
        RSL = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSL.Rows.Count - 1
            Application.DoEvents()

            'MsgSQL = "SELECT Deskripsi FROM  m_KodeProduk " &
            '    "WHERE KodeProduk = '" & RSL.Rows(a) !Kode_Produk & "' " &
            '    "  AND AktifYN = 'Y' "
            'Produk = Proses.ExecuteSingleStrQuery(MsgSQL)
            DGView2.Rows.Add(RSL.Rows(a) !idrec,
                   RSL.Rows(a) !Kode_Produk,
                   Format(RSL.Rows(a) !Jumlah, "###,##0"),
                   Format(RSL.Rows(a) !HargaBeli, "###,##0"),
                   Format(RSL.Rows(a) !TglTerima, "dd-mm-yyyy"),
                   Format(RSL.Rows(a) !DeadlineSP, "dd-mm-yyyy"),
                   RSL.Rows(a) !Kargo,
                   RSL.Rows(a) !NoLHP,
                   RSL.Rows(a) !NoSP)
        Next a
        DGView2.Visible = True
        If DGView2.Rows.Count <> 0 Then
            DGView2_CellClick(sender, e)
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim tExpedisi As String = "", mRevisi As String = "", tanggal As String = ""

        Me.Cursor = Cursors.WaitCursor

        MsgSQL = "select distinct nospb, TglDPB " &
            "From t_dpb inner join t_LHP on " &
            "     t_dpb.NoLHP = t_LHP.NoLHP " &
            "Where t_DPB.NoDPB =  '" & nodpb.Text & "' "
        dttable = Proses.ExecuteQuery(MsgSQL)
        If dttable.Rows.Count <> 0 Then
            tanggal = "Jakarta, " & Proses.TglIndo(Format(dttable.Rows(0) !TglDPB, "dd-MM-yyyy"))
        End If
        For b = 0 To dttable.Rows.Count - 1
            tExpedisi = tExpedisi + dttable.Rows(b) !NoSPB + "; "
        Next
        If tExpedisi <> "" Then
            tExpedisi = "Expedisi : " & Microsoft.VisualBasic.Left(tExpedisi, Len(tExpedisi) - 2)
        End If

        If Trim(mRevisi) = "" Then
            If MsgBox("Apakah DPB ini Revisi?", vbYesNo + vbInformation, ".:Confirm!") = vbYes Then
                MsgSQL = "Update t_DPB Set StatusDPB = 'REVISI' " &
                    "Where NoDPB = '" & nodpb.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                mRevisi = "REVISI"
                StatusDPB.Text = "REVISI"
            Else
                mRevisi = ""
                StatusDPB.Text = ""
            End If
        End If
        mRevisi = StatusDPB.Text
        Proses.OpenConn(CN)
        dttable = New DataTable
        SQL = "SELECT t_DPB.NoDPB, t_DPB.TglDPB, t_DPB.NoSP, t_DPB.NoLHP, " &
            " t_DPB.Kode_Produk, t_DPB.NamaProduk, t_DPB.KodePerajin, " &
            " t_DPB.Jumlah, t_DPB.HargaBeli, t_DPB.DeadlineSP, t_DPB.Pengirim, t_DPB.ongkir, " &
            " t_DPB.TglTerima , t_DPB.tglCetak, m_KodeProduk.Satuan, m_KodeProduk.KodePerajin2 " &
            " FROM Pekerti.dbo.t_DPB t_DPB INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON " &
            "    t_DPB.Kode_Produk = m_KodeProduk.KodeProduk " &
            "Where t_DPB.NoDPB = '" & nodpb.Text & "' and t_DPB.Jumlah <> 0 " &
            "  And t_DPB.AktifYN = 'Y' order by t_DPB.NoSP, t_DPB.NoLHP, " &
            " t_DPB.IDREC, t_DPB.KODE_PRODUK "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_DPB
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("tExpedisi", tExpedisi)
            objRep.SetParameterValue("tgl", tanggal)
            objRep.SetParameterValue("MRevisi", mRevisi)

            Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_Report.CrystalReportViewer1.Refresh()
            Form_Report.CrystalReportViewer1.ReportSource = objRep
            Form_Report.CrystalReportViewer1.ShowRefreshButton = False
            Form_Report.CrystalReportViewer1.ShowPrintButton = False
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
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        AturTombol(True)
        LAdd = False
        LEdit = False
        LTambahKode = False
    End Sub

    Private Sub NoLHP_GotFocus(sender As Object, e As EventArgs) Handles NoLHP.GotFocus
        With NoLHP
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub OngKir_GotFocus(sender As Object, e As EventArgs) Handles OngKir.GotFocus
        With OngKir
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Pengirim_GotFocus(sender As Object, e As EventArgs) Handles Pengirim.GotFocus
        With Pengirim
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub OngKir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles OngKir.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If OngKir.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(OngKir.Text) Then
                Dim temp As Double = OngKir.Text
                OngKir.Text = Format(temp, "###,##0")
                OngKir.SelectionStart = OngKir.TextLength
            Else
                OngKir.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Pengirim.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub NoLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim rsN1 As New DataTable
            NoLHP.Text = FindLHP(NoLHP.Text)
            MsgSQL = "Select NamaPerajin, a.NoSPB, a.TglTerima, a.Kargo " &
                " From T_LHP a " &
                "Where NoLHP = '" & NoLHP.Text & "' "
            RSN1 = Proses.ExecuteQuery(MsgSQL)
            If rsN1.Rows.Count <> 0 Then
                Pengirim.Text = rsN1.Rows(0) !NamaPerajin
                SPB.Text = rsN1.Rows(0) !NoSPB
                TglTerima.Value = rsN1.Rows(0) !TglTerima
                Kargo.Text = rsN1.Rows(0) !Kargo
                OngKir.Focus()
            Else
                NoSP.Text = ""
                Pengirim.Text = ""
                SPB.Text = ""
                Kargo.Text = ""
                OngKir.Text = 0
                NoLHP.Focus()
            End If
        End If
    End Sub

    Private Function findLHP(Cari As String) As String
        Dim RSD As New DataTable, mKondisi As String
        Dim MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoLHP Like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select NoLHP, a.NamaPerajin, a.TglLHP " &
            " From T_LHP a  " &
            "Where a.AktifYN = 'Y' " & mKondisi & " " &
            "Group By NoLHP, a.NamaPerajin, a.TglLHP " &
            "Order By a.tglLHP desc, NoLHP Desc "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar LHP"
        Form_Daftar.ShowDialog()
        findLHP = Trim(FrmMenuUtama.TSKeterangan.Text)
        FrmMenuUtama.TSKeterangan.Text = ""
    End Function

    Private Sub CariDPB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CariDPB.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarDPB()
        End If
    End Sub

    Private Sub cariLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cariLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarDPB()
        End If
    End Sub

    Private Sub cariSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cariSP.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarDPB()
        End If
    End Sub

    Private Sub cariKodeBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cariKodeBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarDPB()
        End If
    End Sub

    Private Sub Pengirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pengirim.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub
End Class