Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop

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
            ShowFoto("")
        ElseIf Len(Kode_Produk.Text) = 5 Then
            Kode_Produk.Text = Kode_Produk.Text + "-"
            Kode_Produk.SelectionStart = Len(Trim(Kode_Produk.Text)) + 1
        ElseIf Len(Kode_Produk.Text) = 8 Then
            Kode_Produk.Text = Kode_Produk.Text + "-"
            Kode_Produk.SelectionStart = Len(Trim(Kode_Produk.Text)) + 1
        End If
    End Sub

    Private Sub getMaxIdLHP()
        Dim MsgSQL As String, RsMax As New DataTable, MaxNoUrut As String = ""
        Dim Proses As New ClsKoneksi, Kode As String = ""

        MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin " &
                    " From T_PraLHP " &
                    "Where NoPraLHP = '" & NoPraLHP.Text & "' "
        RsMax = Proses.ExecuteQuery(MsgSQL)
        If RsMax.Rows.Count = 0 Then
            MsgBox("No Pra LHP salah/tidak terdaftar !", vbCritical + vbOKOnly, ".:Warning !")
            NoPraLHP.Focus()
            Exit Sub
        End If

        Kode = Mid(NoSP.Text, 5, 1)

        MsgSQL = "Select convert(Char(2), GetDate(), 12) TGL, " &
            "      isnull(Max(left(NoLHP, 3)),0) + 1000001 RecId " &
            " From t_LHP " &
            "Where Right(NoLHP, 8) = '" & Kode & "' + '/LHP/' + convert(Char(2), GetDate(), 12)  " &
            "  And aktifYN = 'Y' "
        RsMax = Proses.ExecuteQuery(MsgSQL)
        If LAdd Then
            NoLHP.Text = Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 3) + "/" + Kode + "/LHP/" +
                         Trim(Str(RsMax.Rows(0) !tGL))
        End If

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
        'If Trim(JumlahBaik.Text) = "0" Then
        '    MsgBox("Jumlah baik nya kenapa nol ya ?", vbCritical + vbOKOnly, ".:Warning !")
        '    JumlahBaik.Focus()
        '    'Exit Sub
        'End If
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
        If CekJumlahPack() Then
            MsgBox("Jummlah Pack lebih besar dari yang di SP", vbCritical + vbOKOnly, ".:Warning!")
            Exit Sub
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
        JumlahPackPraLHP.Text = 0
        JumlahSP.Text = 0
        tglMulaiPeriksa.Value = Now
        TglSelesaiPeriksa.Value = Now
        TglLHP.Value = Now
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
        'NoLHP.Text = Proses.MaxYNoUrut("NoLHP", "t_LHP", "LHP")

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
            Dim oNoLHP As String = NoLHP.Text
            ClearTextBoxes()
            NoLHP.Text = oNoLHP
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
        If LAdd Or LEdit Then
            JumlahPackPraLHP.Visible = True
            JumlahSP.Visible = True
        Else
            JumlahPackPraLHP.Visible = False
            JumlahSP.Visible = False
        End If

        cmdPenambahanKode.Visible = tAktif

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
        Dim Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_LHP " &
            "where AktifYN = 'Y' " &
            "Order By TglLHP Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call ISILHP(tIdRec)
        tTambah = Proses.UserAksesTombol(UserID, "52_LHP", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "52_LHP", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "52_LHP", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "52_LHP", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarLHP()
        SetupToolTip()
        JumlahPackPraLHP.Visible = False
        JumlahSP.Visible = False
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
            "max(NoSPB) NoSPB, max(NoPO) NoPO, max(Importir) Importir, max(nosp) nosp " &
            "From T_LHP " &
            "Where convert(char(8), tglLHP, 112)   >= convert(char(8),  DATEADD(m, -12, getdate()) , 112) " &
            "  and aktifYN = 'Y' " & mKondisi & " " &
            "Group By NoLHP, TglLHP, NamaPerajin " &
            "Order By TglLHP Desc, Right(NoLHP,2) + left(nolhp,3) Desc "
        rsdaftar = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsdaftar.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsdaftar.Rows(a) !NoLHP,
                   Format(rsdaftar.Rows(a) !TglLHP, "dd-MM-yyyy"),
                   rsdaftar.Rows(a) !NamaPerajin,
                   rsdaftar.Rows(a) !NoPraLHP,
                   rsdaftar.Rows(a) !NoSPB,
                   rsdaftar.Rows(a) !NoPO,
                   rsdaftar.Rows(a) !Importir,
                   rsdaftar.Rows(a) !NoSP)
        Next a
        Me.Cursor = Cursors.Default
        DGView.Visible = True
    End Sub

    Private Sub IsiLHP(IdRec As String)
        Dim MsgSQL As String, RSP As New DataTable
        MsgSQL = "select * " &
            " From T_LHP " &
            "Where  IDRec = '" & IdRec & "' " &
            " AND AktifYN = 'Y'"
        RSP = Proses.ExecuteQuery(MsgSQL)
        If RSP.Rows.Count <> 0 Then
            IDRecord.Text = RSP.Rows(0) !IdRec
            NoLHP.Text = RSP.Rows(0) !NoLHP
            NoPraLHP.Text = RSP.Rows(0) !NoPraLHP
            TglLHP.Value = RSP.Rows(0) !TglLHP
            Kode_Produk.Text = RSP.Rows(0) !Kode_Produk
            Produk.Text = RSP.Rows(0) !Produk
            HargaBeli.Text = Format(RSP.Rows(0) !HargaBeli, "###,##0")
            JumlahPack.Text = RSP.Rows(0) !JumlahPack
            Kirim.Text = RSP.Rows(0) !Kirim
            JumlahHitung.Text = RSP.Rows(0) !JumlahHitung
            JumlahBaik.Text = RSP.Rows(0) !JumlahBaik
            JumlahRetur.Text = RSP.Rows(0) !JumlahTolak
            Pemeriksa.Text = RSP.Rows(0) !Pemeriksa
            tglMulaiPeriksa.Value = RSP.Rows(0) !tglMulaiPeriksa
            TglSelesaiPeriksa.Value = RSP.Rows(0) !TglSelesaiPeriksa
            Koordinator.Text = RSP.Rows(0) !Koordinator
            AlasanDiTolak.Text = RSP.Rows(0) !AlasanDiTolak
            Keterangan.Text = RSP.Rows(0) !Keterangan
            Perajin.Text = RSP.Rows(0) !NamaPerajin
            Kode_Perajin.Text = RSP.Rows(0) !KodePerajin
            NoSP.Text = RSP.Rows(0) !NoSP
            NoSPB.Text = RSP.Rows(0) !NoSPB
            NoPO.Text = RSP.Rows(0) !NoPO
            Kode_Importir.Text = RSP.Rows(0) !Kode_Importir
            Importir.Text = RSP.Rows(0) !Importir
            Kargo.Text = RSP.Rows(0) !Kargo
            tglTerima.Value = RSP.Rows(0) !tglTerima
            TglMasukGudang.Value = RSP.Rows(0) !TglMasukGudang
            JumlahKoli.Text = RSP.Rows(0) !JumlahKoli
            LocGmb1.Text = Trim(Kode_Produk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
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
            DGView2.Rows.Add(RSL.Rows(a) !idrec,
                   RSL.Rows(a) !Kode_Produk,
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
        If LAdd Or LEdit Then Exit Sub
        IDRecord.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        ISILHP(IDRecord.Text)
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
            PanelEntry.Enabled = False
            NoPraLHP.ReadOnly = True
            Me.Cursor = Cursors.WaitCursor
            If LAdd Or LEdit Or LTambahKode Then
                MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin, NoSP " &
                " From T_PraLHP " &
                "Where NoPraLHP = '" & NoPraLHP.Text & "' "
                rsn1 = Proses.ExecuteQuery(MsgSQL)
                If rsn1.Rows.Count <> 0 Then
                    Kode_Perajin.Text = rsn1.Rows(0) !Kode_Perajin
                    Perajin.Text = rsn1.Rows(0) !NamaPerajin
                    NoSP.Text = rsn1.Rows(0) !nosp
                    Kode_Produk.Focus()
                Else
                    NoPraLHP.Text = FindPraLHP(NoPraLHP.Text)
                    MsgSQL = "Select NoPraLHP, Kode_Perajin, NamaPerajin, nosp " &
                    " From T_PraLHP " &
                    "Where NoPraLHP = '" & NoPraLHP.Text & "' "
                    rsn2 = Proses.ExecuteQuery(MsgSQL)
                    If rsn2.Rows.Count <> 0 Then
                        Kode_Perajin.Text = rsn2.Rows(0) !Kode_Perajin
                        Perajin.Text = rsn2.Rows(0) !NamaPerajin
                        NoSP.Text = rsn2.Rows(0) !nosp
                        Kode_Produk.Focus()
                    Else
                        MsgBox("NO Pra LHP tidak boleh kosong!", vbCritical, ".:ERROR!")
                        NoPraLHP.Focus()
                        Exit Sub
                    End If
                End If
                Proses.CloseConn()
                If Trim(NoPraLHP.Text) <> "" Then
                    If Trim(NoSP.Text) = "" Then
                        MsgBox("No SP salah/kosong", vbCritical + vbOKOnly, ".:Warning !")
                        Exit Sub
                    Else
                        getMaxIdLHP()
                    End If
                End If
                Me.Cursor = Cursors.Default
                NoPraLHP.ReadOnly = False
                PanelEntry.Enabled = True
                Kode_Produk.Focus()
            End If
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
        FrmMenuUtama.TSKeterangan.Text = ""
        MsgSQL = "Select NoPraLHP, Importir, NoSP, TglTerima, " &
            "      HargaBeli, Produk, Kode_Produk " &
            " From T_PraLHP " &
            "Where AktifYN = 'Y' " &
            " " & mKondisi & " " &
            " And NoPraLHP = '" & tNoPraLHP & "' " &
            "order by idrec "

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
        Me.Cursor = Cursors.WaitCursor
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar Pra LHP"
        Form_Daftar.ShowDialog()
        Me.Cursor = Cursors.Default
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
            If LAdd Or LEdit Or LTambahKode Then
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
                    JumlahPack.Text = 0
                    JumlahPackPraLHP.Text = RSI.Rows(0) !JumlahPack
                    Kirim.Text = RSI.Rows(0) !Kirim
                    HargaBeli.Text = Format(RSI.Rows(0) !HargaBeli, "###,##0")
                    Kode_Produk.Text = RSI.Rows(0) !Kode_Produk
                    Produk.Text = Replace(RSI.Rows(0) !Produk, "'", "`")
                    NoSPB.Text = RSI.Rows(0) !SuratPengantar
                    IsiJumlahProduk()
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
                            JumlahPack.Text = 0
                            JumlahPackPraLHP.Text = RSK.Rows(0) !JumlahPack
                            Kirim.Text = RSK.Rows(0) !Kirim
                            HargaBeli.Text = Format(RSK.Rows(0) !HargaBeli, "###,##0")
                            Kode_Produk.Text = RSK.Rows(0) !Kode_Produk
                            Produk.Text = Replace(RSK.Rows(0) !Produk, "'", "`")
                            NoSPB.Text = RSK.Rows(0) !SuratPengantar
                            IsiJumlahProduk()
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
                'If LAdd Or LEdit Or LTambahKode Then
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
            JumlahSP.Text = jSP
        End If
        'MsgSQL = "Select isnull(Sum(JumlahBaik),0) JB From T_LHP " &
        '    "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
        '    " And NoSP = '" & NoSP.Text & "' and nolhp = '" & NoLHP.Text & "' " &
        '    " And AktifYN = 'Y' "
        'RSK = Proses.ExecuteQuery(MsgSQL)
        'If RSK.Rows.Count <> 0 Then
        '    jKirim = RSK.Rows(0) !jb
        '    jKurang = jSP - jKirim
        'Else
        '    JKirim = 0
        '    jKurang = 0
        'End If

        MsgSQL = "Select isnull(Sum(JumlahBaik),0) JB From T_LHP " &
            "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
            " And NoSP = '" & NoSP.Text & "' " &
            " And AktifYN = 'Y' "
        RSK = Proses.ExecuteQuery(MsgSQL)
        If RSK.Rows.Count <> 0 Then
            'JumlahBaik.Text = RSK.Rows(0) !jb
            JKrgSP = jSP - RSK.Rows(0) !jb
        Else
            'JumlahBaik.Text = 0
            JKrgSP = 0
        End If
        JumlahPack.Text = Format(JKrgSP, "###,##0")
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
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        Form_Hapus.Left = Me.Left
        Form_Hapus.Top = Me.Top
        Form_Hapus.tIDSebagian.Text = IDRecord.Text
        Form_Hapus.tIDSemua.Text = NoLHP.Text
        Form_Hapus.Text = "Hapus LHP"
        Form_Hapus.ShowDialog()
        DaftarLHP()
        ClearTextBoxes()
    End Sub

    Private Sub JumlahBaik_TextChanged(sender As Object, e As EventArgs) Handles JumlahBaik.TextChanged
        If Trim(JumlahBaik.Text) = "" Then JumlahBaik.Text = 0
        If Trim(JumlahHitung.Text) = "" Then JumlahHitung.Text = 0
        If IsNumeric(JumlahBaik.Text) Then
            Dim temp As Double = JumlahBaik.Text
            JumlahBaik.SelectionStart = JumlahBaik.TextLength
            JumlahRetur.Text = Format(Trim(JumlahHitung.Text) * 1 - Trim(JumlahBaik.Text) * 1, "###,##0")
        Else
            If Trim(JumlahHitung.Text) = "" Then JumlahHitung.Text = 0
            JumlahBaik.Text = 0
        End If
        If Trim(JumlahBaik.Text) = 0 Then
            With JumlahBaik
                .SelectionStart = 0
                .SelectionLength = .TextLength
            End With
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
            If LAdd Or LEdit Then
                If IsNumeric(JumlahPack.Text) Then
                    If CekJumlahPack() Then
                        MsgBox("Jummlah Pack lebih besar dari yang di SP", vbCritical + vbOKOnly, ".:Warning!")
                        Exit Sub
                    Else
                        Dim temp As Double = JumlahPack.Text
                        JumlahPack.Text = Format(temp, "###,##0")
                        JumlahPack.SelectionStart = JumlahPack.TextLength
                    End If

                Else
                    JumlahPack.Text = 0
                End If
                If LAdd Or LEdit Then JumlahPack.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Function CekJumlahPack() As Boolean
        Dim Hasil As Boolean, rsk As New DataTable, JKrgSP As Double
        MsgSQL = "Select isnull(Sum(JumlahBaik),0) JB From T_LHP " &
            "Where Kode_Produk = '" & Kode_Produk.Text & "' " &
            " And NoSP = '" & NoSP.Text & "' " &
            " And AktifYN = 'Y' "
        RSK = Proses.ExecuteQuery(MsgSQL)
        If rsk.Rows.Count <> 0 Then
            JKrgSP = (JumlahSP.Text * 1) - rsk.Rows(0) !jb
        Else
            JKrgSP = 0
        End If
        If (JumlahPack.Text * 1) > JKrgSP Then
            Hasil = True
            JumlahPack.Focus()
        Else
            Hasil = False
        End If
        CekJumlahPack = Hasil
    End Function
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

    Private Sub tNoPO_TextChanged(sender As Object, e As EventArgs) Handles tNoPO.TextChanged

    End Sub

    Private Sub tglMulaiPeriksa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglMulaiPeriksa.KeyPress
        If e.KeyChar = Chr(13) Then
            TglSelesaiPeriksa.Focus()
        End If
    End Sub

    Private Sub tNOLHP_TextChanged(sender As Object, e As EventArgs) Handles tNOLHP.TextChanged

    End Sub

    Private Sub TglSelesaiPeriksa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglSelesaiPeriksa.KeyPress
        If e.KeyChar = Chr(13) Then
            Koordinator.Focus()
        End If
    End Sub

    Private Sub tNoPraLHP_TextChanged(sender As Object, e As EventArgs) Handles tNoPraLHP.TextChanged

    End Sub

    Private Sub NoPraLHP_GotFocus(sender As Object, e As EventArgs) Handles NoPraLHP.GotFocus
        With NoPraLHP
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub tNoSP_TextChanged(sender As Object, e As EventArgs) Handles tNoSP.TextChanged

    End Sub

    Private Sub Kode_Produk_GotFocus(sender As Object, e As EventArgs) Handles Kode_Produk.GotFocus
        With Kode_Produk
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub tKodeBrg_TextChanged(sender As Object, e As EventArgs) Handles tKodeBrg.TextChanged

    End Sub

    Private Sub JumlahHitung_GotFocus(sender As Object, e As EventArgs) Handles JumlahHitung.GotFocus
        With JumlahHitung
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        PanelNavigate.Enabled = False
        Form_Export2Excel.JenisTR.Text = "LHP"
        Form_Export2Excel.idRec.Text = NoLHP.Text
        Form_Export2Excel.ShowDialog()
        PanelNavigate.Enabled = True
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

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        cetakLHP
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

    Private Sub cmdRiwayatHarga_Click(sender As Object, e As EventArgs)

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

    Private Sub tNoPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tNoPO.KeyPress
        If e.KeyChar = Chr(13) Then
            tNOLHP.Text = ""
            tNoPraLHP.Text = ""
            tNoSP.Text = ""
            tKodeBrg.Text = ""
            DaftarLHP()
        End If
    End Sub

    Private Sub tNOLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tNOLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            tNoPO.Text = ""
            tNoPraLHP.Text = ""
            tNoSP.Text = ""
            tKodeBrg.Text = ""
            DaftarLHP()
        End If
    End Sub

    Private Sub tNoPraLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tNoPraLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            tNoPO.Text = ""
            tNOLHP.Text = ""
            tNoSP.Text = ""
            tKodeBrg.Text = ""
            DaftarLHP()
        End If
    End Sub

    Private Sub tNoSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tNoSP.KeyPress
        If e.KeyChar = Chr(13) Then
            tNoPO.Text = ""
            tNOLHP.Text = ""
            tNoPraLHP.Text = ""
            tKodeBrg.Text = ""
            DaftarLHP()
        End If
    End Sub

    Private Sub tKodeBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tKodeBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            tNoPO.Text = ""
            tNOLHP.Text = ""
            tNoPraLHP.Text = ""
            tKodeBrg.Text = ""
            DaftarLHP()
        End If
    End Sub

    Private Sub JumlahBaik_LostFocus(sender As Object, e As EventArgs) Handles JumlahBaik.LostFocus
        If JumlahBaik.Text = "" Then JumlahBaik.Text = 0
        If JumlahHitung.Text = "" Then JumlahHitung.Text = 0
        If Trim(JumlahBaik.Text) * 1 > Trim(JumlahHitung.Text) * 1 Then
            MsgBox("Jumlah Lulus Periksa Lebih Besar dari Jumlah Hitung", vbCritical, ".:Imposible!")
            JumlahBaik.Focus()
            Exit Sub
        Else
            JumlahRetur.Text = Format(Trim(JumlahHitung.Text) * 1 - Trim(JumlahBaik.Text) * 1, "###,##0")
        End If
    End Sub

    Private Sub JumlahRetur_LostFocus(sender As Object, e As EventArgs) Handles JumlahRetur.LostFocus
        If Trim(JumlahBaik.Text) = "" Then JumlahBaik.Text = 0
        If Trim(JumlahHitung.Text) = "" Then JumlahHitung.Text = 0
        If Trim(JumlahRetur.Text) = "" Then JumlahRetur.Text = 0

        If IsNumeric(JumlahRetur.Text) Then
            Dim temp As Double = JumlahRetur.Text
            JumlahBaik.SelectionStart = JumlahBaik.TextLength
            JumlahBaik.Text = Format(Trim(JumlahHitung.Text) * 1 - Trim(JumlahRetur.Text) * 1, "###,##0")
        Else
            JumlahRetur.Text = 0
        End If
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = 0 Then
        ElseIf e.TabPageIndex = 1 Then
            DaftarLHP()
        End If
    End Sub
    Private Sub CetakLHP()
        Dim dbCek As New DataTable
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim MsgSQL As String, rsc As New DataTable
        Me.Cursor = Cursors.WaitCursor

        Proses.OpenConn(CN)
        dttable = New DataTable

        MsgSQL = "SELECT Distinct t_LHP.IDRec, t_LHP.NoLHP, t_LHP.NoPraLHP, t_LHP.Kode_Produk, " &
            "    t_LHP.Produk, t_LHP.JumlahPack, t_LHP.Kirim, t_LHP.JumlahHitung,  " &
            "    t_LHP.JumlahBaik, t_LHP.JumlahTolak, t_LHP.Pemeriksa, t_LHP.TglMulaiPeriksa,  " &
            "    t_LHP.TglSelesaiPeriksa, t_LHP.Koordinator, t_LHP.Keterangan, t_LHP.NoSP,  " &
            "    t_PraLHP.Kargo, t_PraLHP.SuratPengantar, t_PraLHP.JumlahKoli,  " &
            "    AlasanDiTolak, T_LHP.TglTerima, t_LHP.NamaPerajin " &
            "FROM Pekerti.dbo.t_LHP t_LHP INNER JOIN Pekerti.dbo.t_PraLHP t_PraLHP ON  " &
            "        t_LHP.NoPraLHP = t_PraLHP.NoPraLHP AND t_LHP.NoSP = t_PraLHP.NoSP AND  " &
            "        t_LHP.Kode_Produk = t_PraLHP.Kode_Produk " &
            "Where t_LHP.NoLHP = '" & NoLHP.Text & "' " &
            "  And t_LHP.AktifYN = 'Y' And t_PraLHP.AktifYN = 'Y' " &
            "ORDER BY t_LHP.NoSP, t_LHP.IDRec ASC "
        dbCek = Proses.ExecuteQuery(MsgSQL)
        If dbCek.Rows.Count = 0 Then
            MsgBox("No Pra LHP " & NoPraLHP.Text & " tidak ada! (mungkin Pra LHP tsb terhapus)" & vbCrLf &
                "Silakan cek di Pra LHP!", vbCritical + vbOKOnly, ".:Warning!")
            NoPraLHP.Focus()
            Exit Sub
        End If
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_LHP
            objRep.SetDataSource(dttable)
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
        'RS05.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly '
        'If RS05.EOF Then
        '    MsgBox "No Pra LHP " & NoPraLHP.Text & " tidak ada! (mungkin Pra LHP tsb terhapus)" & vbCrLf &
        '        "Silakan cek di Pra LHP!", vbCritical + vbOKOnly, ".:Warning!"
        'End If
        'RS05.Close
        'With CrINS
        '    .Reset
        '    .LogOnServer "PDSODBC.DLL", "DBPEKERTI", "PEKERTI", Usr, PWD
        '    .ReportFileName = Left(RptLoc, Len(Trim(RptLoc)) - 1) & "\Rpt_LHP.rpt"
        '    .SQLQuery = MsgSQL
        '    .WindowState = crptMaximized
        '    '            .WindowShowGroupTree = True
        '    .WindowShowSearchBtn = True
        '    .WindowShowCloseBtn = True
        '    .WindowShowNavigationCtls = True
        '    .WindowShowProgressCtls = True
        '    .WindowShowPrintSetupBtn = True
        '    .WindowShowPrintBtn = True
        '    .WindowAllowDrillDown = True
        '    .Action = 1
        'End With
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
            .SetToolTip(Me.JumlahPackPraLHP, "Jumlah Pack Pra-LHP")
            .SetToolTip(Me.JumlahSP, "Jumlah Pack yang di SP")
        End With
    End Sub
End Class


