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
        If Len(Kode_Produk.Text) < 1 Then
            Kode_Produk.Text = ""
            Produk.Text = ""
            Kode_Perajin.Text = ""
            Perajin.Text = ""
            NoSP.Text = ""
            NoPO.Text = ""
            Kode_Importir.Text = ""
            Importir.Text = ""
            Kargo.Text = ""
            tglTerima.Value = "1/1/1900"
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
            " And NoSP = '" & Trim(JumlahRetur.Text) & "' " &
            " And KodeProduk = '" & Kode_Produk.Text & "' "
        JSP = Proses.ExecuteSingleDblQuery(MsgSQL)


        MsgSQL = "Select Sum(JumlahBaik) JBaik " &
            " From T_LHP " &
            "Where AktifYN = 'Y' " &
            " And NoSP = '" & Trim(JumlahRetur.Text) & "' " &
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
            MsgSQL = "INSERT INTO t_LHP(IDRec, NoLHP, NoPraLHP, tglLHP, " &
                    "Kode_Produk, Produk, HargaBeli, JumlahPack, Kirim, " &
                    "JumlahHitung, JumlahBaik, JumlahTolak, Pemeriksa, " &
                    "TglMulaiPeriksa, TglSelesaiPeriksa, Koordinator, AlasanDiTolak, " &
                    "Keterangan, KodePerajin, NamaPerajin, NoSP, NoSPB, " &
                    "NoPO, Kode_Importir, Importir, Kargo, TglTerima, " &
                    "TglMasukGudang, JumlahKoli, AktifYN, UserID, LastUpd) VALUES (" &
                    "'" & IDRecord.Text & "', '" & NoLHP.Text & "', " &
                    "'" & NoPraLHP.Text & "', '" & Format(TglLHP.Value, "YYYY-MM-DD") & "', " &
                    "'" & Kode_Produk.Text & "', '" & Produk.Text & "', " &
                    "" & HargaBeli.Text * 1 & ", " & JumlahPack.Text * 1 & ", " &
                    "" & Kirim.Text * 1 & ", " & JumlahHitung.Text * 1 & ", " &
                    "" & JumlahBaik.Text * 1 & ", " & JumlahRetur.Text * 1 & " ," &
                    "'" & Trim(Pemeriksa.Text) & "', " &
                    "'" & Format(tglMulaiPeriksa.Value, "YYYY-MM-DD") & "', " &
                    "'" & Format(TglSelesaiPeriksa.Value, "YYYY-MM-DD") & "', " &
                    "'" & Trim(Koordinator.Text) & "', '" & Trim(AlasanDiTolak.Text) & "', " &
                    "'" & Trim(AlasanDiTolak.Text) & "', '" & Trim(Kode_Perajin.Text) & "', '" & Trim(Perajin.Text) & "', " &
                    "'" & Trim(JumlahRetur.Text) & "', '" & Trim(NoSPB.Text) & "', " &
                    "'" & Trim(NoPO.Text) & "', '" & Trim(Kode_Importir.Text) & "', " &
                    "'" & Trim(Importir.Text) & "', '" & Trim(JumlahHitung.Text) & "', " &
                    "'" & Format(tglMulaiPeriksa.Value, "YYYY-MM-DD") & "', " &
                    "'" & Format(TglMasukGudang.Value, "YYYY-MM-DD") & "', " &
                    "" & JumlahKoli.Text * 1 & ", 'Y', '" & UserID & "', GetDate())"
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
            MsgSQL = "Update t_LHP Set NoLHP = '" & NoLHP.Text & "', " &
                " NoPraLHP = '" & NoPraLHP.Text & "', tglLHP = '" & Format(TglLHP.Value, "YYYY-MM-DD") & "', " &
                " Kode_Produk = '" & Kode_Produk.Text & "', Produk = '" & Produk.Text & "', " &
                " HargaBeli = " & HargaBeli.Text * 1 & ", JumlahPack = " & JumlahPack.Text * 1 & " , " &
                " Kirim = " & Kirim.Text * 1 & ", " &
                " JumlahHitung = " & JumlahHitung.Text * 1 & ", " &
                " JumlahBaik = " & JumlahBaik.Text * 1 & ", JumlahTolak = " & JumlahRetur.Text * 1 & " ," &
                " Pemeriksa = '" & Trim(Pemeriksa.Text) & "', " &
                " TglMulaiPeriksa = '" & Format(tglMulaiPeriksa.Value, "YYYY-MM-DD") & "', " &
                " TglSelesaiPeriksa = '" & Format(TglSelesaiPeriksa.Value, "YYYY-MM-DD") & "', " &
                " Koordinator = '" & Trim(Koordinator.Text) & "', AlasanDiTolak = '" & Trim(AlasanDiTolak.Text) & "', " &
                " Keterangan = '" & Trim(AlasanDiTolak.Text) & "', " &
                " KodePerajin = '" & Trim(Kode_Perajin.Text) & "', NamaPerajin = '" & Trim(Perajin.Text) & "', " &
                " NoSP = '" & Trim(JumlahRetur.Text) & "',NoSPB = '" & Trim(NoSPB.Text) & "', " &
                " NoPO = '" & Trim(NoPO.Text) & "',Kode_importir = '" & Trim(Kode_Importir.Text) & "', " &
                " Importir = '" & Trim(Importir.Text) & "', Kargo = '" & Trim(JumlahHitung.Text) & "', " &
                " TglTerima = '" & Format(tglMulaiPeriksa.Value, "YYYY-MM-DD") & "', " &
                " TglMasukGudang = '" & Format(TglMasukGudang.Value, "YYYY-MM-DD") & "', " &
                " JumlahKoli = " & JumlahKoli.Text * 1 & ", TransferYN = 'N', UserID = '" & UserID & "', LastUpd = GetDate() " &
                " Where IDRec = '" & IDRecord.Text & "' "
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
        ShowFoto("")
        oJumBaik = 0
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        If Trim(NoLHP.Text) = "" Then
            MsgBox("No LHP masih kosong!", vbCritical, ".:ERROR!")
            NoLHP.Focus()
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
        Keterangan.Text = ""
        '    Pemeriksa.Text = ""
        '    tglMulaiPeriksa.Value = Date
        '    TglSelesaiPeriksa.Value = Date
        '    Koordinator.Text = ""
        AlasanDiTolak.Text = ""
        Kode_Produk.Focus()
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
            lKoordinator = rs05.Rows(0)!KoordinatorLHP
            lPemeriksa = rs05.Rows(0)!Pemeriksa
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
            HargaBeli.Text = RSP.Rows(0)!HargaBeli
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

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_LHP " &
        "Where AktifYN = 'Y' " &
        "  And IDRec < '" & IDRecord.Text & "' " &
        "  And NOLHP = '" & NoLHP.Text & "' " &
        "ORDER BY tgllhp desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call ISILHP(RSNav.Rows(0)!IdRec)
        End If
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
            Call ISILHP(RSNav.Rows(0)!IdRec)
        End If
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
            DGView2.Rows.Add(RSL.Rows(a)!Kode_Produk,
                   Format(RSL.Rows(a)!JumlahPack, "###,##0"),
                   Format(RSL.Rows(a)!Kirim, "###,##0"),
                   Format(RSL.Rows(a)!JumlahHitung, "###,##0"),
                   Format(RSL.Rows(a)!JumlahBaik, "###,##0"),
                   Format(RSL.Rows(a)!JumlahTolak, "###,##0"),
                   RSL.Rows(a)!AlasanDiTolak,
                   RSL.Rows(a)!NoSP)
        Next a

        DGView2.Visible = True
        If DGView2.Rows.Count <> 0 Then
            DGView2_CellClick(sender, e)
        End If
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

    Private Sub NoLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Or LTambahKode Then NoPraLHP.Focus()
        End If
    End Sub

    Private Sub NoPraLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPraLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim rsn1 As New DataTable, rsn2 As New DataTable
            MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin " &
            " From T_PraLHP " &
            "Where NoPraLHP = '" & NoPraLHP.Text & "' "
            rsn1 = Proses.ExecuteQuery(MsgSQL)
            If rsn1.Rows.Count <> 0 Then
                Kode_Perajin.Text = rsn1.Rows(0)!Kode_Perajin
                Perajin.Text = rsn1.Rows(0)!NamaPerajin
            Else
                NoPraLHP.Text = FindPraLHP(NoPraLHP.Text)
                MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin " &
                    " From T_PraLHP " &
                    "Where NoPraLHP = '" & NoPraLHP.Text & "' "
                rsn2 = Proses.ExecuteQuery(MsgSQL)
                If rsn2.Rows.Count <> 0 Then
                    Kode_Perajin.Text = rsn2.Rows(0)!Kode_Perajin
                    Perajin.Text = rsn2.Rows(0)!NamaPerajin
                Else
                    MsgBox("NO Pra LHP tidak boleh kosong!", vbCritical, ".:ERROR!")
                    NoPraLHP.Focus()
                    Exit Sub
                End If
            End If
            Proses.CloseConn()
            If LAdd Or LEdit Or LTambahKode Then Kode_Produk.Focus()
        End If
    End Sub

    Public Function FindPraLHP(Cari As String) As String
        Dim RSD As New DataTable, mKondisi As String
        Dim MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoPraLHP like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin, TGLPRALHP   " &
        " From T_PraLHP " &
        "Where AktifYN = 'Y' " &
        "  " & mKondisi & " " &
        "Group By NoPraLHP, Kode_Perajin, NamaPerajin, TGLPRALHP  " &
        "Order By TGLPRALHP Desc, NoPraLHP Desc "

        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar LHP"
        Form_Daftar.ShowDialog()


        'RSD.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'Frm_Browse.lstView.ListItems.Clear
        'Frm_Browse.lstView.Visible = False
        'Frm_Browse.lstView.ColumnHeaders(1).Text = "No Pra LHP"
        'Frm_Browse.lstView.ColumnHeaders(1).Width = 1750
        'Frm_Browse.lstView.ColumnHeaders(2).Text = "Perajin"


        FindPraLHP = Trim(FrmMenuUtama.TSKeterangan.Text)
    End Function

End Class


