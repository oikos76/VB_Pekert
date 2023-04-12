Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_Gd_LHP
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        lKoordinator As String, lPemeriksa As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Dim oJumBaik As Double
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        LTambahKode = False
        oJumBaik = JumlahBaik.Text
        AturTombol(False)
        cmdSimpan.Visible = tEdit
    End Sub

    Protected Ds As DataSet

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_LHP " &
            "Where AktifYN = 'Y' " &
            "  And NOLHP = '" & NoLHP.Text & "' " &
            "ORDER BY tgllhp, IDRec "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call ISILHP(RSNav.Rows(0)!IdRec)
        End If
        Proses.CloseConn()
    End Sub

    Private Sub cmdPenambahanKode_Click(sender As Object, e As EventArgs) Handles cmdPenambahanKode.Click
        TambahKode()
    End Sub
    Private Sub TambahKode()
        If Trim(NoLHP.Text) = "" Then
            MsgBox("No LHP masih kosong!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        LAdd = False
        LEdit = False
        AturTombol(False)
        Produk.Text = ""
        Kode_Produk.Text = ""
        Produk.Text = ""
        HargaBeli.Text = ""
        JumlahPack.Text = ""
        Kirim.Text = ""
        JumlahHitung.Text = ""
        JumlahBaik.Text = ""
        JumlahRetur.Text = ""
        AlasanDiTolak.Text = ""
        Keterangan.Text = ""
        Kode_Produk.Focus()
    End Sub

    Private Sub Kode_Produk_TextChanged(sender As Object, e As EventArgs) Handles Kode_Produk.TextChanged
        If Len(Trim(Kode_Produk.Text)) < 1 Then
            Kode_Produk.Text = ""
            Produk.Text = ""
            Kode_Perajin.Text = ""
            Perajin.Text = ""
            NoSP.Text = ""
            NoPO.Text = ""
            Kode_Importir.Text = ""
            Importir.Text = ""
            Kargo.Text = ""
            'tglTerima.Value = Now
            'TglMasukGudang.Value = Now
            'tglMulaiPeriksa.Value = Now
            'TglSelesaiPeriksa.Value = Now
            JumlahKoli.Text = 0
            JumlahPack.Text = 0
            Kirim.Text = 0
            HargaBeli.Text = 0
        ElseIf Len(Kode_Produk.Text) = 4 Then
            Kode_Produk.Text = Kode_Produk.Text + "-"
            Kode_Produk.SelectionStart = Len(Trim(Kode_Produk.Text)) + 1
        ElseIf Len(Kode_Produk.Text) = 7 Then
            Kode_Produk.Text = Kode_Produk.Text + "-"
            Kode_Produk.SelectionStart = Len(Trim(Kode_Produk.Text)) + 1
        End If
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSQL As String, Rs As New DataTable, JSP As Double, JLHP As Double

        If Trim(JumlahBaik.Text) = "" Then JumlahBaik.Text = 0
        If Trim(JumlahRetur.Text) = "" Then JumlahRetur.Text = 0
        If Trim(JumlahHitung.Text) = "" Then JumlahHitung.Text = 0
        If (JumlahBaik.Text * 1) + (JumlahRetur.Text * 1) <> (JumlahHitung.Text * 1) Then
            MsgBox("Jumlah Hitung tidak sama dengan jumlah baik & jumlah retur ", vbCritical, "Salah hitung!")
        End If

        MsgSQL = "Select Sum(Jumlah) Jumlah " &
            " From t_SP " &
            "Where AktifYN = 'Y' " &
            " And NoSP = '" & Trim(NoSP.Text) & "' " &
            " And KodeProduk = '" & Kode_Produk.Text & "' "
        JSP = Proses.ExecuteSingleDblQuery(MsgSQL)


        MsgSQL = "Select Sum(JumlahBaik) JBaik " &
            " From T_LHP " &
            "Where AktifYN = 'Y' " &
            " And NoSP = '" & Trim(NoSP.Text) & "' " &
            " And Kode_Produk = '" & Kode_Produk.Text & "' "
        JLHP = Proses.ExecuteSingleDblQuery(MsgSQL)


        If (JLHP - (oJumBaik * 1)) + (JumlahBaik.Text * 1) > JSP Then
            MsgBox("QTY " & Kode_Produk.Text & " di  LHP ini lebih besar dari Jumlah SP !" & vbCrLf & "QTY " & Kode_Produk.Text & " di SP = " & Format(JSP, "###,##0"), vbCritical + vbOKOnly, ".:Warning!")
            JumlahBaik.Focus()
            Exit Sub
        End If

        If LAdd Then
            MsgSQL = "Select * From t_LHP " &
                "Where NoLHP = '" & NoLHP.Text & "' " &
                "  And AktifYN = 'Y'"
            Rs = Proses.ExecuteQuery(MsgSQL)
            If Rs.Rows.Count <> 0 Then
                MsgBox("No LHP " & NoLHP.Text & " Sudah ADA !", vbCritical + vbOKOnly, ".:Warning!")
                Exit Sub
            End If
        End If

        If LAdd Or LTambahKode Then
            IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_LHP", "LH")
            MsgSQL = "INSERT INTO t_LHP(IDRec, NoLHP, NoPraLHP, tglLHP, Kode_Produk, " &
                    "Produk, HargaBeli, JumlahPack, Kirim, JumlahHitung,  JumlahBaik, " &
                    "JumlahTolak, Pemeriksa, TglMulaiPeriksa, TglSelesaiPeriksa, Koordinator, " &
                    "AlasanDiTolak,Keterangan, KodePerajin, NamaPerajin, NoSP, NoSPB, " &
                    "NoPO, Kode_Importir, Importir, Kargo, TglTerima, TglMasukGudang, " &
                    "JumlahKoli, TransferYN, AktifYN, UserID, LastUpd) VALUES (" &
                    "'" & IDRecord.Text & "', '" & NoLHP.Text & "', " &
                    "'" & NoPraLHP.Text & "', '" & Format(TglLHP.Value, "yyyy-MM-dd") & "', " &
                    "'" & Kode_Produk.Text & "', '" & Produk.Text & "', " &
                    "" & HargaBeli.Text * 1 & ", " & JumlahPack.Text * 1 & ", " &
                    "" & Kirim.Text * 1 & ", " & JumlahHitung.Text * 1 & ", " &
                    "" & JumlahBaik.Text * 1 & ", " & JumlahRetur.Text * 1 & " ," &
                    "'" & Trim(Pemeriksa.Text) & "', " &
                    "'" & Format(tglMulaiPeriksa.Value, "yyyy-MM-dd") & "', " &
                    "'" & Format(TglSelesaiPeriksa.Value, "yyyy-MM-dd") & "', " &
                    "'" & Trim(Koordinator.Text) & "', '" & Trim(AlasanDiTolak.Text) & "', " &
                    "'" & Trim(Keterangan.Text) & "', '" & Trim(Kode_Perajin.Text) & "', '" & Trim(Perajin.Text) & "', " &
                    "'" & Trim(NoSP.Text) & "', '" & Trim(NoSPB.Text) & "', " &
                    "'" & Trim(NoPO.Text) & "', '" & Trim(Kode_Importir.Text) & "', " &
                    "'" & Trim(Importir.Text) & "', '" & Trim(Kargo.Text) & "', " &
                    "'" & Format(tglTerima.Value, "yyyy-MM-dd") & "', " &
                    "'" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                    "" & JumlahKoli.Text * 1 & ", 'N', 'Y', '" & UserID & "', GetDate())"
            Proses.ExecuteNonQuery(MsgSQL)
            TambahKode()
        ElseIf LEdit Then
            MsgSQL = "Update t_LHP Set " &
                " TransferYN = 'N', " &
                " KodePerajin = '" & Trim(Kode_Perajin.Text) & "', NamaPerajin = '" & Trim(Perajin.Text) & "', " &
                " UserID = '" & UserID & "', " &
                " LastUpd = GetDate() " &
                "WHere NoLHP = '" & NoLHP.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            MsgSQL = "UPDATE t_LHP SET NoLHP = '" & NoLHP.Text & "', " &
                " NoPraLHP = '" & NoPraLHP.Text & "', tglLHP = '" & Format(TglLHP.Value, "yyyy-MM-dd") & "', " &
                " Kode_Produk = '" & Kode_Produk.Text & "', Produk = '" & Produk.Text & "', " &
                " HargaBeli = " & HargaBeli.Text * 1 & ", JumlahPack = " & JumlahPack.Text * 1 & " , " &
                " Kirim = " & Kirim.Text * 1 & ", " &
                " JumlahHitung = " & JumlahHitung.Text * 1 & ", " &
                " JumlahBaik = " & JumlahBaik.Text * 1 & ", JumlahTolak = " & JumlahRetur.Text * 1 & " ," &
                " Pemeriksa = '" & Trim(Pemeriksa.Text) & "', " &
                " TglMulaiPeriksa = '" & Format(tglMulaiPeriksa.Value, "yyyy-MM-dd") & "', " &
                " TglSelesaiPeriksa = '" & Format(TglSelesaiPeriksa.Value, "yyyy-MM-dd") & "', " &
                " Koordinator = '" & Trim(Koordinator.Text) & "', AlasanDiTolak = '" & Trim(AlasanDiTolak.Text) & "', " &
                " Keterangan = '" & Trim(Keterangan.Text) & "', " &
                " KodePerajin = '" & Trim(Kode_Perajin.Text) & "', NamaPerajin = '" & Trim(Perajin.Text) & "', " &
                " NoSP = '" & Trim(NoSP.Text) & "',NoSPB = '" & Trim(NoSPB.Text) & "', " &
                " NoPO = '" & Trim(NoPO.Text) & "',Kode_importir = '" & Trim(Kode_Importir.Text) & "', " &
                " Importir = '" & Trim(Importir.Text) & "', Kargo = '" & Trim(Kargo.Text) & "', " &
                " TglTerima = '" & Format(tglTerima.Value, "yyyy-MM-dd") & "', " &
                " TglMasukGudang = '" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                " JumlahKoli = " & JumlahKoli.Text * 1 & ",  " &
                " TransferYN = 'N', UserID = '" & UserID & "', LastUpd = GetDate() " &
                "WHERE IdRec = '" & IDRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            LTambahKode = False
            LAdd = False
            LEdit = False
            AturTombol(True)
        End If
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
        tglMulaiPeriksa.Value = Now
        TglSelesaiPeriksa.Value = Now
        Koordinator.Text = lKoordinator
        Pemeriksa.Text = lPemeriksa
        ShowFoto("")
        oJumBaik = 0
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click

        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        oJumBaik = 0
        NoLHP.Text = Proses.MaxYNoUrut("NoLHP", "t_LHP", "LHP")
        NoPraLHP.Focus()
        NoLHP.ReadOnly = True

    End Sub

    Private Sub NoLHP_TextChanged(sender As Object, e As EventArgs) Handles NoLHP.TextChanged

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

    Private Sub NoPraLHP_TextChanged(sender As Object, e As EventArgs) Handles NoPraLHP.TextChanged
        If Len(Trim(NoPraLHP.Text)) < 1 Then
            ClearTextBoxes()
            'Kode_Produk.Text = ""
            'Produk.Text = ""
            'Kode_Perajin.Text = ""
            'Perajin.Text = ""
            'NoSP.Text = ""
            'NoPO.Text = ""
            'Kode_Importir.Text = ""
            'Importir.Text = ""
            'Kargo.Text = ""
            'tglTerima.Value = "1/1/1900"
            'JumlahKoli.Text = 0
            'JumlahPack.Text = 0
            'Kirim.Text = 0
            'HargaBeli.Text = 0
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
        cmdPenambahanKode.Visible = tAktif

        cmdRiwayatHarga.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True

        'Atur Readonly
        NoLHP.ReadOnly = tAktif
        NoPraLHP.ReadOnly = tAktif
        TglLHP.Enabled = Not tAktif
        Kode_Produk.ReadOnly = tAktif
        Produk.ReadOnly = tAktif
        HargaBeli.ReadOnly = tAktif
        JumlahPack.ReadOnly = tAktif
        Kirim.ReadOnly = tAktif
        JumlahHitung.ReadOnly = tAktif
        JumlahBaik.ReadOnly = tAktif
        JumlahRetur.ReadOnly = tAktif
        Pemeriksa.ReadOnly = tAktif
        tglMulaiPeriksa.Enabled = Not tAktif
        TglSelesaiPeriksa.Enabled = Not tAktif
        Koordinator.ReadOnly = tAktif
        AlasanDiTolak.ReadOnly = tAktif
        Keterangan.ReadOnly = tAktif

        Kode_Perajin.ReadOnly = True
        Perajin.ReadOnly = True
        NoSP.ReadOnly = True
        NoSPB.ReadOnly = True
        NoPO.ReadOnly = True
        Kode_Importir.ReadOnly = True
        Importir.ReadOnly = True
        Kargo.ReadOnly = True
        tglTerima.Enabled = False
        TglMasukGudang.Enabled = False
        JumlahKoli.ReadOnly = False

        Me.Text = "Laporan Hasil Pemeriksaan (LHP)"
    End Sub
    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
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
        With Me.DGView2.RowTemplate
            .Height = 35
            .MinimumHeight = 30
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
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Form_Gd_LHP_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        tTambah = Proses.UserAksesTombol(UserID, "52_LHP", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "52_LHP", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "52_LHP", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "52_LHP", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarLHP()
    End Sub
    Private Sub DaftarLHP()
        Dim rsdaftar As New DataTable
        Dim MsgSQL As String, mKondisi As String
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        mKondisi = ""
        If tNoPO.Text <> "" Then
            mKondisi = " and nopo like '%" & tNoPO.Text & "%' "
        End If
        If tNOLHP.Text <> "" Then
            mKondisi = " and nolhp like '%" & tNOLHP.Text & "%' "
        End If
        If tNoSP.Text <> "" Then
            mKondisi = " and nosp like '%" & tNoSP.Text & "%' "
        End If
        If tNoPraLHP.Text <> "" Then
            mKondisi = " and noPraLHP like '%" & tNoPraLHP.Text & "%' "
        End If
        If tKodeBrg.Text <> "" Then
            mKondisi = " and Kode_Produk like '%" & tKodeBrg.Text & "%' "
        End If
        MsgSQL = "Select NoLHP, TglLHP, NamaPerajin, max(NoPraLHP) NoPraLHP, " &
            "max(NoSPB) NoSPB, max(NoPO) NoPO, max(Importir) Importir " &
            "From T_LHP " &
            "Where Year(tglLHP) >= (year(getdate())-2) " &
            "  and aktifYN = 'Y' " & mKondisi & " " &
            "Group By NoLHP, TglLHP, NamaPerajin " &
            "Order By TglLHP Desc, Right(NoLHP,2) + left(nolhp,3) Desc "
        rsdaftar = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsdaftar.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsdaftar.Rows(a)!NoLHP,
                   Format(rsdaftar.Rows(a)!TglLHP, "dd-MM-yyyy"),
                   rsdaftar.Rows(a)!NamaPerajin,
                   rsdaftar.Rows(a)!NoPraLHP,
                   rsdaftar.Rows(a)!NoSPB,
                   rsdaftar.Rows(a)!NoPO,
                   rsdaftar.Rows(a)!Importir)
        Next a
        Me.Cursor = Cursors.Default
        DGView.Visible = True
    End Sub

    Private Sub ISILHP(IdRec As String)
        Dim MsgSQL As String, RSP As New DataTable
        MsgSQL = "select * " &
        " From T_LHP " &
        "Where  IDRec = '" & IdRec & "' " &
        " AND AktifYN = 'Y'"
        RSP = Proses.ExecuteQuery(MsgSQL)
        If RSP.Rows.Count <> 0 Then
            IDRecord.Text = RSP.Rows(0)!IdRec
            NoLHP.Text = RSP.Rows(0)!NoLHP
            NoPraLHP.Text = RSP.Rows(0)!NoPraLHP
            TglLHP.Value = RSP.Rows(0)!TglLHP
            Kode_Produk.Text = RSP.Rows(0)!Kode_Produk
            Produk.Text = RSP.Rows(0)!Produk
            HargaBeli.Text = Format(RSP.Rows(0) !HargaBeli, "###,##0")
            JumlahPack.Text = RSP.Rows(0)!JumlahPack
            Kirim.Text = RSP.Rows(0)!Kirim
            JumlahHitung.Text = RSP.Rows(0)!JumlahHitung
            JumlahBaik.Text = RSP.Rows(0)!JumlahBaik
            JumlahRetur.Text = RSP.Rows(0)!JumlahTolak
            Pemeriksa.Text = RSP.Rows(0)!Pemeriksa
            tglMulaiPeriksa.Value = RSP.Rows(0)!tglMulaiPeriksa
            TglSelesaiPeriksa.Value = RSP.Rows(0)!TglSelesaiPeriksa
            Koordinator.Text = RSP.Rows(0)!Koordinator
            AlasanDiTolak.Text = RSP.Rows(0)!AlasanDiTolak
            Keterangan.Text = RSP.Rows(0)!Keterangan
            Perajin.Text = RSP.Rows(0)!NamaPerajin
            Kode_Perajin.Text = RSP.Rows(0)!KodePerajin
            NoSP.Text = RSP.Rows(0)!NoSP
            NoSPB.Text = RSP.Rows(0)!NoSPB
            NoPO.Text = RSP.Rows(0)!NoPO
            Kode_Importir.Text = RSP.Rows(0)!Kode_Importir
            Importir.Text = RSP.Rows(0)!Importir
            Kargo.Text = RSP.Rows(0)!Kargo
            tglTerima.Value = RSP.Rows(0)!tglTerima
            TglMasukGudang.Value = RSP.Rows(0)!TglMasukGudang
            JumlahKoli.Text = RSP.Rows(0)!JumlahKoli
        End If
        Proses.CloseConn()
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_LHP " &
            "Where AktifYN = 'Y' " &
            "  And NOLHP = '" & NoLHP.Text & "' " &
            "ORDER BY tgllhp desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call ISILHP(RSNav.Rows(0)!IdRec)
        End If
    End Sub

    Private Sub Kode_Importir_TextChanged(sender As Object, e As EventArgs) Handles Kode_Importir.TextChanged
        If Len(Kode_Importir.Text) < 1 Then
            Importir.Text = ""
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_LHP " &
        "Where AktifYN = 'Y' " &
        "  And IDRec < '" & IDRecord.Text & "' " &
        "  And NOLHP = '" & NoLHP.Text & "' " &
        "ORDER BY tgllhp desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call ISILHP(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub AlasanDiTolak_TextChanged(sender As Object, e As EventArgs) Handles AlasanDiTolak.TextChanged

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_LHP " &
        "Where AktifYN = 'Y' " &
        "  And IDRec > '" & IDRecord.Text & "' " &
        "  And NOLHP = '" & NoLHP.Text & "' " &
        "ORDER BY tgllhp , IDRec  "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call ISILHP(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub Keterangan_TextChanged(sender As Object, e As EventArgs) Handles Keterangan.TextChanged


    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String
        Dim RSL As New DataTable
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Rows.Clear()
        DGView2.Visible = False

        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "Select * " &
            "From T_LHP " &
            "Where T_LHP.AktifYN = 'Y' " &
            "  And NoLHP = '" & tCari & "' " &
            "ORDER BY t_LHP.NoSP, t_LHP.IDRec "
        RSL = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSL.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSL.Rows(a) !Kode_Produk,
                   Format(RSL.Rows(a) !JumlahPack, "###,##0"),
                   Format(RSL.Rows(a) !Kirim, "###,##0"),
                   Format(RSL.Rows(a) !JumlahHitung, "###,##0"),
                   Format(RSL.Rows(a) !JumlahBaik, "###,##0"),
                   Format(RSL.Rows(a) !JumlahTolak, "###,##0"),
                   RSL.Rows(a) !AlasanDiTolak,
                   RSL.Rows(a) !NoSP)
        Next a

        DGView2.Visible = True
        If DGView2.Rows.Count <> 0 Then
            DGView2_CellClick(sender, e)
        End If
    End Sub

    Private Sub Pemeriksa_TextChanged(sender As Object, e As EventArgs) Handles Pemeriksa.TextChanged

    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        If DGView2.Rows.Count = 0 Then Exit Sub
        NoLHP.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "select idrec " &
            " From T_LHP " &
            "Where  nolhp = '" & NoLHP.Text & "' " &
            " AND AktifYN = 'Y' " &
            "ORDER by idrec "
        IDRecord.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
        ISILHP(IDRecord.Text)
    End Sub

    Private Sub Koordinator_TextChanged(sender As Object, e As EventArgs) Handles Koordinator.TextChanged

    End Sub

    Private Sub NoLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Or LTambahKode Then NoPraLHP.Focus()
        End If
    End Sub

    Private Sub JumlahHitung_TextChanged(sender As Object, e As EventArgs) Handles JumlahHitung.TextChanged
        If Trim(JumlahHitung.Text) = "" Then JumlahHitung.Text = 0
        If IsNumeric(JumlahHitung.Text) Then
            Dim temp As Double = JumlahHitung.Text
            JumlahHitung.SelectionStart = JumlahHitung.TextLength
        Else
            JumlahHitung.Text = 0
        End If
    End Sub

    Private Sub NoPraLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPraLHP.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Dim rsn1 As New DataTable, rsn2 As New DataTable
            NoPraLHP.ReadOnly = True
            Me.Cursor = Cursors.WaitCursor
            MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin " &
                " From T_PraLHP " &
                "Where NoPraLHP = '" & NoPraLHP.Text & "' "
            rsn1 = Proses.ExecuteQuery(MsgSQL)
            If rsn1.Rows.Count <> 0 Then
                Kode_Perajin.Text = rsn1.Rows(0) !Kode_Perajin
                Perajin.Text = rsn1.Rows(0) !NamaPerajin
                Kode_Produk.Focus()
            Else
                NoPraLHP.Text = FindPraLHP(NoPraLHP.Text)
                MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin " &
                    " From T_PraLHP " &
                    "Where NoPraLHP = '" & NoPraLHP.Text & "' "
                rsn2 = Proses.ExecuteQuery(MsgSQL)
                If rsn2.Rows.Count <> 0 Then
                    Kode_Perajin.Text = rsn2.Rows(0) !Kode_Perajin
                    Perajin.Text = rsn2.Rows(0) !NamaPerajin
                    Kode_Produk.Focus()
                Else
                    MsgBox("NO Pra LHP tidak boleh kosong!", vbCritical, ".:ERROR!")
                    NoPraLHP.Focus()
                    Exit Sub
                End If
            End If
            Proses.CloseConn()
            Me.Cursor = Cursors.Default
            NoPraLHP.ReadOnly = False
            If LAdd Or LEdit Or LTambahKode Then Kode_Produk.Focus()
        End If
    End Sub
    Public Function FindKodeProdukPraLHP(Cari As String, tNoPraLHP As String) As String
        Dim   mKondisi As String
        Dim MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And Produk like '%" & Trim(Cari) & "%' "
        End If

        MsgSQL = "Select NoPraLHP, Importir, NoSP, TglTerima, " &
            "      HargaBeli, Produk, Kode_Produk " &
            " From T_PraLHP " &
            "Where AktifYN = 'Y' " &
            " " & mKondisi & " " &
            " And NoPraLHP = '" & tNoPraLHP & "' " &
            "order by idrec "
        'Frm_Browse.lstView.ListItems.Clear
        'Frm_Browse.lstView.Visible = False
        'Frm_Browse.lstView.ColumnHeaders(1).Text = "KodeProduk + SP"
        'Frm_Browse.lstView.ColumnHeaders(1).Width = 1
        'Frm_Browse.lstView.ColumnHeaders(2).Text = "Kode Produk"
        'Frm_Browse.lstView.ColumnHeaders(2).Width = 1750
        'Frm_Browse.lstView.ColumnHeaders(3).Text = "Produk"
        'Frm_Browse.lstView.ColumnHeaders(4).Text = ""
        'Frm_Browse.lstView.ColumnHeaders(4).Width = 1750
        'Frm_Browse.lstView.ColumnHeaders(5).Text = ""
        'Frm_Browse.lstView.ColumnHeaders(6).Text = "NoPraLHP"


        'Do While Not RSD.EOF
        '    DoEvents
        'Set Lst = Frm_Browse.lstView.ListItems.Add(, , left(RSD!Kode_Produk & Space(25), 25) & Right(Space(25) & RSD!NoSP, 25))
        'Lst.SubItems(1) = RSD!Kode_Produk
        '    Lst.SubItems(2) = RSD!Produk
        '    Lst.SubItems(3) = Format(RSD!tglTerima, "dd-MM-YYYY")
        '    Lst.SubItems(4) = RSD!NoSP
        '    Lst.SubItems(5) = RSD!NoPraLHP
        '    RSD.MoveNext
        'Loop


        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar Produk Pra LHP"
        Form_Daftar.ShowDialog()
        FindKodeProdukPraLHP = Trim(FrmMenuUtama.TSKeterangan.Text)
    End Function


    Public Function FindPraLHP(Cari As String) As String
        Dim RSD As New DataTable, mKondisi As String
        Dim MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoPraLHP Like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin, TGLPRALHP   " &
        " From T_PraLHP " &
        "Where AktifYN = 'Y' " &
        "  " & mKondisi & " " &
        "Group By NoPraLHP, Kode_Perajin, NamaPerajin, TGLPRALHP  " &
        "Order By TGLPRALHP Desc, NoPraLHP Desc "

        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar Pra LHP"
        Form_Daftar.ShowDialog()

        FindPraLHP = Trim(FrmMenuUtama.TSKeterangan.Text)
    End Function

    Private Sub HargaBeli_TextChanged(sender As Object, e As EventArgs) Handles HargaBeli.TextChanged
        If Trim(HargaBeli.Text) = "" Then HargaBeli.Text = 0
        If IsNumeric(HargaBeli.Text) Then
            Dim temp As Double = HargaBeli.Text
            HargaBeli.SelectionStart = HargaBeli.TextLength
        Else
            HargaBeli.Text = 0
        End If
    End Sub

    Private Sub Kode_Produk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Produk.KeyPress
        Dim RSI As New DataTable, RSK As New DataTable
        Dim tKode As String
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            MsgSQL = "Select * From T_PraLHP " &
                "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
                " And AktifYN = 'Y' " &
                " And NoPraLHP = '" & NoPraLHP.Text & "' " &
                " And noSP = '" & NoSP.Text & "' "
            RSI = Proses.ExecuteQuery(MsgSQL)
            If RSI.Rows.Count <> 0 Then
                If CekLHP(NoSP.Text, Kode_Produk.Text) Then
                    MsgBox(Kode_Produk.Text + " Sudah pernah di input di no LHP ini!", vbCritical, ".:Double Input!")
                    Kode_Produk.Focus()
                    Exit Sub
                End If
                Kode_Perajin.Text = RSI.Rows(0) !Kode_Perajin
                Perajin.Text = RSI.Rows(0) !NamaPerajin
                NoSP.Text = RSI.Rows(0) !NoSP
                NoPO.Text = RSI.Rows(0) !NoPO
                Kode_Importir.Text = RSI.Rows(0) !Kode_Importir
                Importir.Text = RSI.Rows(0) !Importir
                Kargo.Text = RSI.Rows(0) !Kargo
                tglTerima.Value = RSI.Rows(0) !tglTerima
                JumlahKoli.Text = RSI.Rows(0) !JumlahKoli
                JumlahPack.Text = RSI.Rows(0) !JumlahPack
                Kirim.Text = RSI.Rows(0) !Kirim
                HargaBeli.Text = Format(RSI.Rows(0) !HargaBeli, "###,##0")
                Kode_Produk.Text = RSI.Rows(0) !Kode_Produk
                Produk.Text = Replace(RSI.Rows(0) !Produk, "'", "`")
                NoSPB.Text = RSI.Rows(0) !SuratPengantar
                'IsiJumlahProduk
            Else
                tKode = FindKodeProdukPraLHP(Kode_Produk.Text, NoPraLHP.Text)
                Kode_Produk.Text = Trim(Microsoft.VisualBasic.Left(tKode, 25))
                NoSP.Text = Trim(Microsoft.VisualBasic.Right(tKode, 25))
                If CekLHP(NoSP.Text, Kode_Produk.Text) Then
                    MsgBox(Kode_Produk.Text + " Sudah pernah di input di no LHP ini!", vbCritical, ".:Double Input!")
                    Kode_Produk.Focus()
                    Exit Sub
                End If

                If Trim(Kode_Produk.Text) <> "" Then
                    MsgSQL = "Select * From T_PraLHP " &
                        "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
                        " And AktifYN = 'Y' " &
                        " And NoPraLHP = '" & NoPraLHP.Text & "' " &
                        " And noSP = '" & NoSP.Text & "' "
                    RSK = Proses.ExecuteQuery(MsgSQL)
                    If RSK.Rows.Count <> 0 Then
                        Kode_Perajin.Text = RSK.Rows(0) !Kode_Perajin
                        Perajin.Text = RSK.Rows(0) !NamaPerajin
                        NoSP.Text = RSK.Rows(0) !NoSP
                        NoPO.Text = RSK.Rows(0) !NoPO
                        Kode_Importir.Text = RSK.Rows(0) !Kode_Importir
                        Importir.Text = RSK.Rows(0) !Importir
                        Kargo.Text = RSK.Rows(0) !Kargo
                        tglTerima.Value = RSK.Rows(0) !tglTerima
                        JumlahKoli.Text = RSK.Rows(0) !JumlahKoli
                        JumlahPack.Text = RSK.Rows(0) !JumlahPack
                        Kirim.Text = RSK.Rows(0) !Kirim
                        HargaBeli.Text = Format(RSK.Rows(0) !HargaBeli, "###,##0")
                        Kode_Produk.Text = RSK.Rows(0) !Kode_Produk
                        Produk.Text = Replace(RSK.Rows(0) !Produk, "'", "`")
                        NoSPB.Text = RSK.Rows(0) !SuratPengantar
                        'IsiJumlahProduk
                    End If
                End If
            End If
            Proses.CloseConn()
            LocGmb1.Text = Trim(Kode_Produk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If

            LocGmb1.Text = Trim(Kode_Produk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
            If LAdd Or LEdit Or LTambahKode Then
                If Trim(Kode_Produk.Text) = "" Or Trim(Produk.Text) = "" Then
                    Kode_Produk.Focus()
                ElseIf Trim(Kode_Produk.Text) <> "" Or Trim(Produk.Text) <> "" Then
                    JumlahHitung.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub IsiJumlahProduk()
        Dim RSK As New DataTable, MsgSQL As String
        'FrmJumlah.Visible = True
        Dim jSP As Double = 0, jKirim As Double = 0, jKurang As Double = 0, JKrgSP As Double = 0
        MsgSQL = "Select isnull(Jumlah,0) Jumlah From t_PraLHP " &
            "Where NoPraLHP = '" & NoPraLHP.Text & "' " &
            "  and AktifYN = 'Y' And NoSP = '" & NoSP.Text & "' " &
            "  and Kode_produk = '" & Kode_Produk.Text & "' "
        RSK = Proses.ExecuteQuery(MsgSQL)
        If RSK.Rows.Count <> 0 Then
            jSP = RSK.Rows(0) !Jumlah
        End If
        MsgSQL = "Select isnull(Sum(JumlahBaik),0) JB From T_LHP " &
            "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
            " And NoSP = '" & NoSP.Text & "' and nolhp = '" & NoLHP.Text & "' " &
            " And AktifYN = 'Y' "
        RSK = Proses.ExecuteQuery(MsgSQL)
        If RSK.Rows.Count <> 0 Then
            jKirim = RSK.Rows(0) !jb
            jKurang = jSP - jKirim
        Else
            JKirim = 0
            jKurang = 0
        End If

        MsgSQL = "Select isnull(Sum(JumlahBaik),0) JB From T_LHP " &
            "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
            " And NoSP = '" & NoSP.Text & "' " &
            " And AktifYN = 'Y' "
        RSK = Proses.ExecuteQuery(MsgSQL)
        If RSK.Rows.Count <> 0 Then
            JumlahBaik.Text = RSK.Rows(0) !jb
            JKrgSP = jSP - RSK.Rows(0) !jb
        Else
            JumlahBaik.Text = 0
            JKrgSP = 0
        End If
        'JumlahRetur.Text = Format(jkrgsp, "###,##0")
        Proses.CloseConn()
    End Sub

    Private Sub Kirim_TextChanged(sender As Object, e As EventArgs) Handles Kirim.TextChanged
        If Trim(Kirim.Text) = "" Then Kirim.Text = 0
        If IsNumeric(Kirim.Text) Then
            Dim temp As Double = Kirim.Text
            Kirim.SelectionStart = Kirim.TextLength
        Else
            Kirim.Text = 0
        End If
    End Sub

    Private Function CekLHP(tNoSP As String, tKodeProduk As String) As Boolean
        Dim MsgSQL As String, RSCek As New DataTable
        MsgSQL = "Select * " &
            "From t_LHP " &
            "Where Kode_Produk = '" & tKodeProduk & "' " &
            "  And NoSP = '" & tNoSP & "' " &
            "  And NoLHP = '" & NoLHP.Text & "' " &
            "  And AktifYN = 'Y' "
        RSCek = Proses.ExecuteQuery(MsgSQL)
        If RSCek.Rows.Count <> 0 Then
            CekLHP = True
        Else
            CekLHP = False
        End If
        Proses.CloseConn()
    End Function

    Private Sub JumlahPack_TextChanged(sender As Object, e As EventArgs) Handles JumlahPack.TextChanged
        If Trim(JumlahPack.Text) = "" Then JumlahPack.Text = 0
        If IsNumeric(JumlahPack.Text) Then
            Dim temp As Double = JumlahPack.Text
            JumlahPack.SelectionStart = JumlahPack.TextLength
        Else
            JumlahPack.Text = 0
        End If
    End Sub

    Private Sub tglMulaiPeriksa_ValueChanged(sender As Object, e As EventArgs) Handles tglMulaiPeriksa.ValueChanged

    End Sub

    Private Sub NoPraLHP_KeyDown(sender As Object, e As KeyEventArgs) Handles NoPraLHP.KeyDown

    End Sub

    Private Sub TglSelesaiPeriksa_ValueChanged(sender As Object, e As EventArgs) Handles TglSelesaiPeriksa.ValueChanged

    End Sub

    Private Sub AlasanDiTolak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AlasanDiTolak.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Keterangan.Focus()
        End If
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click

    End Sub

    Private Sub JumlahBaik_TextChanged(sender As Object, e As EventArgs) Handles JumlahBaik.TextChanged
        If Trim(JumlahBaik.Text) = "" Then JumlahBaik.Text = 0
        If IsNumeric(JumlahBaik.Text) Then
            Dim temp As Double = JumlahBaik.Text
            JumlahBaik.SelectionStart = JumlahBaik.TextLength
        Else
            JumlahBaik.Text = 0
        End If
    End Sub

    Private Sub JumlahRetur_TextChanged(sender As Object, e As EventArgs) Handles JumlahRetur.TextChanged
        If Trim(JumlahRetur.Text) = "" Then JumlahRetur.Text = 0
        If IsNumeric(JumlahRetur.Text) Then
            Dim temp As Double = JumlahRetur.Text
            JumlahRetur.SelectionStart = JumlahRetur.TextLength
        Else
            JumlahRetur.Text = 0
        End If
    End Sub

    Private Sub Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Keterangan.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub

    Private Sub Pemeriksa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pemeriksa.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            tglMulaiPeriksa.Focus()
        End If
    End Sub

    Private Sub Koordinator_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Koordinator.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            AlasanDiTolak.Focus()
        End If
    End Sub

    Private Sub JumlahHitung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JumlahHitung.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If JumlahHitung.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(JumlahHitung.Text) Then
                Dim temp As Double = JumlahHitung.Text
                JumlahHitung.Text = Format(temp, "###,##0")
                JumlahHitung.SelectionStart = JumlahHitung.TextLength
            Else
                JumlahHitung.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then JumlahBaik.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub JumlahBaik_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JumlahBaik.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If JumlahBaik.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(JumlahBaik.Text) Then
                Dim temp As Double = JumlahBaik.Text
                JumlahBaik.Text = Format(temp, "###,##0")
                JumlahBaik.SelectionStart = JumlahBaik.TextLength
            Else
                JumlahBaik.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then JumlahRetur.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub JumlahRetur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JumlahRetur.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If JumlahRetur.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(JumlahRetur.Text) Then
                Dim temp As Double = JumlahRetur.Text
                JumlahRetur.Text = Format(temp, "###,##0")
                JumlahRetur.SelectionStart = JumlahRetur.TextLength
            Else
                JumlahRetur.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Pemeriksa.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub HargaBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaBeli.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HargaBeli.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(HargaBeli.Text) Then
                Dim temp As Double = HargaBeli.Text
                HargaBeli.Text = Format(temp, "###,##0.00")
                HargaBeli.SelectionStart = HargaBeli.TextLength
            Else
                HargaBeli.Text = 0
            End If
            If LAdd Or LEdit Then JumlahPack.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub JumlahPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JumlahPack.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If JumlahPack.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(JumlahPack.Text) Then
                Dim temp As Double = JumlahPack.Text
                JumlahPack.Text = Format(temp, "###,##0.00")
                JumlahPack.SelectionStart = JumlahPack.TextLength
            Else
                JumlahPack.Text = 0
            End If
            If LAdd Or LEdit Then JumlahPack.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Kirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kirim.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Kirim.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Kirim.Text) Then
                Dim temp As Double = Kirim.Text
                Kirim.Text = Format(temp, "###,##0.00")
                Kirim.SelectionStart = Kirim.TextLength
            Else
                Kirim.Text = 0
            End If
            If LAdd Or LEdit Then JumlahHitung.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub tglMulaiPeriksa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglMulaiPeriksa.KeyPress
        If e.KeyChar = Chr(13) Then
            TglSelesaiPeriksa.Focus()
        End If
    End Sub

    Private Sub TglSelesaiPeriksa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglSelesaiPeriksa.KeyPress
        If e.KeyChar = Chr(13) Then
            Koordinator.Focus()
        End If
    End Sub

    Private Sub NoPraLHP_GotFocus(sender As Object, e As EventArgs) Handles NoPraLHP.GotFocus
        With NoPraLHP
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Kode_Produk_GotFocus(sender As Object, e As EventArgs) Handles Kode_Produk.GotFocus
        With Kode_Produk
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub JumlahHitung_GotFocus(sender As Object, e As EventArgs) Handles JumlahHitung.GotFocus
        With JumlahHitung
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub JumlahBaik_GotFocus(sender As Object, e As EventArgs) Handles JumlahBaik.GotFocus
        With JumlahBaik
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub JumlahRetur_GotFocus(sender As Object, e As EventArgs) Handles JumlahRetur.GotFocus
        With JumlahRetur
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Pemeriksa_GotFocus(sender As Object, e As EventArgs) Handles Pemeriksa.GotFocus
        With Pemeriksa
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Koordinator_GotFocus(sender As Object, e As EventArgs) Handles Koordinator.GotFocus
        With Koordinator
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub AlasanDiTolak_GotFocus(sender As Object, e As EventArgs) Handles AlasanDiTolak.GotFocus
        With AlasanDiTolak
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Keterangan_GotFocus(sender As Object, e As EventArgs) Handles Keterangan.GotFocus
        With Keterangan
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub
End Class


