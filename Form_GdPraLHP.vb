Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_GdPraLHP
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet

    Private Sub DaftarPraLHP()
        Dim MsgSQL As String, rsdaftar As New DataTable
        Dim mKondisi As String
        DGView.Rows.Clear()
        DGView.Visible = False
        DGView2.Rows.Clear()
        If cPraLHP.Text = "" Then
            mKondisi = ""
        Else
            mKondisi = " And NoPraLHP like '%" & cPraLHP.Text & "%' "
        End If
        MsgSQL = "Select NoPraLHP, TglPraLHP, NamaPerajin, MAX(NoSP) NOSP, MAX(NoPO) NoPO  " &
            " From T_PraLHP " &
            "Where AktifYN = 'Y' " & mKondisi & " " &
            "Group By NoPraLHP, TglPraLHP, NamaPerajin  " &
            "ORDER BY TglPraLHP DESC, " &
            "         Right(NoPraLHP,2) + LEFT(nOpRALHP,3) Desc "
        rsdaftar = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsdaftar.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsdaftar.Rows(a) !NoPraLHP,
                    Format(rsdaftar.Rows(a) !TglPraLHP, "dd-MM-yyyy"),
                    rsdaftar.Rows(a) !NamaPerajin,
                    rsdaftar.Rows(a) !NoSP,
                    rsdaftar.Rows(a) !NoPO)
        Next (a)
        Me.Cursor = Cursors.Default
        DGView.Visible = True
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From T_PraLHP " &
            "Where IDRec > '" & IDRecord.Text & "' " &
            " And NoSP = '" & NoSP.Text & "' and aktifYN = 'Y' " &
            " And NoPraLHP = '" & NoPraLHP.Text & "' " &
            "Order By IdRec "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
            IDRecord.Text = tIdRec
        Else
            tIdRec = ""
        End If
        Call IsiPraLHP()
    End Sub


    Private Sub IsiPraLHP()
        Dim MsgSQL As String, RSP As New DataTable
        On Error Resume Next
        MsgSQL = "Select * From T_PraLHP " &
            "Where IDRec = '" & IDRecord.Text & "' "
        RSP = Proses.ExecuteQuery(MsgSQL)
        If RSP.Rows.Count <> 0 Then
            NoPraLHP.Text = RSP.Rows(0) !NoPraLHP
            TglPraLHP = RSP.Rows(0) !TglPraLHP
            Kargo.Text = RSP.Rows(0) !Kargo
            NoSP.Text = RSP.Rows(0) !NoSP
            KodeProduk.Text = RSP.Rows(0) !Kode_Produk
            Produk.Text = RSP.Rows(0) !Produk
            LocGmb1.Text = Trim(KodeProduk.Text) + ".jpg"
            '   LocGmb1.Text = RSP.Rows(0) !file_foto
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If

            QTYPack.Text = Format(RSP.Rows(0) !JumlahPack, "###,##0")
            Jumlah.Text = Format(RSP.Rows(0) !Jumlah, "###,##0")
            Kirim.Text = Format(RSP.Rows(0) !Kirim, "###,##0")
            tglTerima.Value = RSP.Rows(0) !tglTerima
            SuratPengantar.Text = RSP.Rows(0) !SuratPengantar
            QtyKoli.Text = Format(RSP.Rows(0) !JumlahKoli, "###,##0")
            Koordinator.Text = RSP.Rows(0) !Koordinator
            Keterangan.Text = RSP.Rows(0) !Keterangan
            InstruksiPacking.Text = RSP.Rows(0) !InstruksiPacking
            Kode_Perajin.Text = RSP.Rows(0) !Kode_Perajin
            NamaPerajin.Text = RSP.Rows(0) !NamaPerajin
            NoPO.Text = RSP.Rows(0) !NoPO
            Kode_Importir.Text = RSP.Rows(0) !Kode_Importir
            Importir.Text = RSP.Rows(0) !Importir
            HargaBeli.Text = Format(RSP.Rows(0) !HargaBeli, "###,##0")
            SpeckSP.Text = RSP.Rows(0) !SpecSP
        Else
            ClearTextBoxes()
        End If
        Proses.CloseConn()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSQL As String
        If Trim(KodeProduk.Text) = "" Then
            MsgBox("Kode Produk salah/tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning !")
            KodeProduk.Focus()
            Exit Sub
        Else

        End If
        If Trim(NoSP.Text) = "" Then
            MsgBox("No SP salah/tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning !")
            NoSP.Focus()
            Exit Sub
        End If
        If LAdd Or LEdit Or LTambahKode Then
            If Trim(NoPraLHP.Text) = "" Then
                MsgBox("No Pra LHP masih kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                Exit Sub
            End If
            If LAdd Then
                Dim dbCek As New DataTable
                MsgSQL = "Select * From t_PraLHP " &
                "Where NoPraLHP = '" & NoPraLHP.Text & "' " &
                "  And AktifYN = 'Y' "
                dbCek = Proses.ExecuteQuery(MsgSQL)
                If dbCek.Rows.Count <> 0 Then
                    MsgBox("No Pra LHP " & NoPraLHP.Text & " sudah ada!", vbCritical + vbOKOnly, ".:Warning!")
                    Exit Sub
                End If
            End If
            If Trim(Kirim.Text) = "" Then
                MsgBox("QTY Kirim Masih Kosong!", vbCritical + vbOKOnly, ".:Salah input kirim!")
                Kirim.Focus()
                Exit Sub
            End If
            If QtyKoli.Text = "" Then QtyKoli.Text = 0
            If LAdd Or LTambahKode Then
                IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_PraLHP", "PL")
                MsgSQL = "INSERT INTO t_PraLHP(IDRec, NoPraLHP, TglPraLHP, Kargo, " &
                "NoSP, Kode_Produk, Produk, JumlahPack, Jumlah, Kirim, " &
                "TglTerima, SuratPengantar, JumlahKoli, Koordinator, " &
                "Keterangan, InstruksiPacking, Kode_Perajin, NamaPerajin, " &
                "NoPO, Kode_Importir, Importir, HargaBeli, SpecSP, " &
                "AktifYN, UserID, LastUPD, TransferYN) VALUES ('" & IDRecord.Text & "', " &
                "'" & NoPraLHP.Text & "', '" & Format(TglPraLHP.Value, "yyyy-MM-dd") & "', " &
                "'" & Kargo.Text & "', '" & Trim(NoSP.Text) & "', " &
                "'" & KodeProduk.Text & "', '" & Trim(Produk.Text) & "', " &
                "" & QTYPack.Text * 1 & ", " & Jumlah.Text * 1 & ", " &
                "" & Kirim.Text * 1 & ", '" & Format(tglTerima.Value, "yyyy-MM-d") & "', " &
                "'" & SuratPengantar.Text & "', " & QtyKoli.Text * 1 & ", " &
                "'" & Trim(Koordinator.Text) & "','" & Trim(Keterangan.Text) & "', " &
                "'" & Trim(InstruksiPacking.Text) & "', '" & Trim(Kode_Perajin.Text) & "', " &
                "'" & Trim(NamaPerajin.Text) & "', '" & Trim(NoPO.Text) & "', " &
                "'" & Kode_Importir.Text & "',  '" & Trim(Importir.Text) & "', " &
                "" & HargaBeli.Text * 1 & ",'" & Trim(SpeckSP.Text) & "', " &
                "'Y', '" & UserID & "', GetDate(), 'N')"
                Proses.ExecuteNonQuery(MsgSQL)
                TambahKode()
            ElseIf LEdit Then
                MsgSQL = "Update t_PraLHP Set " &
                "  NoPraLHP = '" & NoPraLHP.Text & "', " &
                " TglPraLHP = '" & Format(TglPraLHP.Value, "yyyy-MM-dd") & "', " &
                "     Kargo = '" & Kargo.Text & "', " &
                "      NoSP = '" & Trim(NoSP.Text) & "', " &
                " Kode_Produk = '" & KodeProduk.Text & "', Produk = '" & Trim(Produk.Text) & "',  " &
                "  JumlahPack = " & QTYPack.Text * 1 & ",  Jumlah = " & Jumlah.Text * 1 & ", " &
                "       Kirim = " & Kirim.Text * 1 & ", " &
                "   TglTerima = '" & Format(tglTerima.Value, "yyyy-MM-dd") & "', " &
                " SuratPengantar = '" & SuratPengantar.Text & "', " &
                "  JumlahKoli = " & QtyKoli.Text * 1 & ", " &
                " Koordinator = '" & Trim(Koordinator.Text) & "', " &
                "  Keterangan = '" & Trim(Replace(Keterangan.Text, "'", "`")) & "', " &
                " InstruksiPacking = '" & Trim(InstruksiPacking.Text) & "', " &
                " Kode_Perajin = '" & Trim(Kode_Perajin.Text) & "', NamaPerajin = '" & Trim(NamaPerajin.Text) & "', " &
                "  NoPO = '" & Trim(NoPO.Text) & "', " &
                " Kode_Importir = '" & Kode_Importir.Text & "', Importir = '" & Trim(Importir.Text) & "', " &
                " HargaBeli = " & HargaBeli.Text * 1 & ", SpecSP = '" & Trim(SpeckSP.Text) & "' " &
                " Where IDRec = '" & IDRecord.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)

                MsgSQL = "Update t_PraLHP Set " &
                "  TglPraLHP = '" & Format(TglPraLHP.Value, "yyyy-MM-dd") & "', " &
                "      Kargo = '" & Kargo.Text & "', " &
                "Koordinator = '" & Trim(Koordinator.Text) & "', " &
                " TransferYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate() " &
                " Where NoPraLHP = '" & NoPraLHP.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)

                LTambahKode = False
                LAdd = False
                LEdit = False
                AturTombol(True)
            End If
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

        'MsgSQL = "SELECT t_PraLHP.IDRec, t_PraLHP.NoPraLHP, t_PraLHP.Kargo, " &
        '"t_PraLHP.Kode_Produk, t_PraLHP.Produk, t_PraLHP.JumlahPack, " &
        '"t_PraLHP.Kirim, t_PraLHP.TglTerima, t_PraLHP.SuratPengantar, " &
        '"t_PraLHP.JumlahKoli, t_PraLHP.Keterangan, t_SP.NoSP , " &
        '"t_SP.KodeImportir, t_SP.Importir, t_SP.Perajin " &
        '" FROM Pekerti.dbo.t_PraLHP t_PraLHP INNER JOIN Pekerti.dbo.t_SP t_SP ON " &
        '"      t_PraLHP.NoSP = t_SP.NoSP  AND t_PraLHP.Kode_Produk = t_SP.KodeProduk " &
        '"Where NoPraLHP = '" & NoPraLHP.Text & "' " &
        '"  And T_PraLHP.AktifYN = 'Y' and t_SP.AktifYN = 'Y'  order by t_PraLHP.IDRec "
        'With CrIns
        '    .Reset
        '    .LogOnServer "PDSODBC.DLL", "DBPEKERTI", "PEKERTI", Usr, PWD
        '    .ReportFileName = Left(RptLoc, Len(Trim(RptLoc)) - 1) & "\Rpt_PraLHP.rpt"
        '    .SQLQuery = MsgSQL
        '    .WindowState = crptMaximized
        '    .WindowShowGroupTree = False
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

    Private Sub cmdPenambahanKode_Click(sender As Object, e As EventArgs) Handles cmdPenambahanKode.Click
        TambahKode()
    End Sub
    Private Sub TambahKode()
        If Trim(NoSP.Text) = "" Then
            MsgBox("No SP masih kosong!", vbCritical, ".:ERROR!")
            Exit Sub
        Else
            NoSP.ReadOnly = True
        End If
        LTambahKode = True
        LAdd = False
        LEdit = False

        AturTombol(False)
        ClearProduk()
        KodeProduk.Focus()
    End Sub
    Private Sub ClearProduk()
        KodeProduk.Text = ""
        Produk.Text = ""
        Jumlah.Text = "0"
        QTYPack.Text = ""
        Kirim.Text = ""
        SuratPengantar.Text = ""
        QtyKoli.Text = ""
        Keterangan.Text = ""
        InstruksiPacking.Text = ""
        NoPO.Text = ""
        Kode_Importir.Text = ""
        Importir.Text = ""
        HargaBeli.Text = "0"
        SpeckSP.Text = ""
        LocGmb1.Text = ""
        ShowFoto("")
    End Sub
    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        Else
            ' IsiKodeProduk(KodeProduk.Text)
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
        NoSP.ReadOnly = False
        NoPraLHP.ReadOnly = True
    End Sub


    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub NoSP_TextChanged(sender As Object, e As EventArgs) Handles NoSP.TextChanged
        If Len(Trim(NoSP.Text)) < 1 Then
            NoSP.Text = ""
            Kode_Perajin.Text = ""
            NamaPerajin.Text = ""
            KodeProduk.Text = ""
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        ClearTextBoxes()
        AturTombol(False)
        NoPraLHP.Text = Proses.MaxYNoUrut("NoPraLHP", "t_PraLHP", "PLHP")
        Kargo.Focus()
        NoPraLHP.ReadOnly = False
        NoSP.ReadOnly = False
    End Sub

    Private Sub Kargo_TextChanged(sender As Object, e As EventArgs) Handles Kargo.TextChanged

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
        TglPraLHP.Value = Now
        tglTerima.Value = Now
    End Sub

    Private Sub Keterangan_TextChanged(sender As Object, e As EventArgs) Handles Keterangan.TextChanged

    End Sub



    Private Sub InstruksiPacking_TextChanged(sender As Object, e As EventArgs) Handles InstruksiPacking.TextChanged

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
        'Kirim.ReadOnly = True
        'NoSP.ReadOnly = True
        Me.Text = "Pra LHP"
    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged
        If Len(KodeProduk.Text) < 1 Then
            KodeProduk.Text = ""
            ClearProduk()
        ElseIf Len(KodeProduk.Text) = 4 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        ElseIf Len(KodeProduk.Text) = 7 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        End If
    End Sub

    Private Sub Koordinator_TextChanged(sender As Object, e As EventArgs) Handles Koordinator.TextChanged

    End Sub

    Private Sub Form_GdPraLHP_Load(sender As Object, e As EventArgs) Handles Me.Load
        LAdd = False
        LEdit = False
        LTambahKode = False

        DGView.Rows.Clear()
        DGView2.Rows.Clear()

        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_PraLHP " &
            "where AktifYN = 'Y' " &
            "Order By TglPraLHP Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        IDRecord.Text = tIdRec
        Call IsiPraLHP()
        Me.Cursor = Cursors.WaitCursor
        If Format(Now, "YYMMDD") < "131231" Then
            MsgSQL = "SELECT t_SPContoh.IDRec AS IDSP, t_SPContoh.TglSP, " &
            "t_DPBSample.TglDPB " &
            "FROM t_SPContoh INNER JOIN t_DPBSample ON " &
            "     t_SPContoh.NoSP = t_DPBSample.NoSP " &
            "     AND t_SPContoh.Kode_Produk = t_DPBSample.KodeProduk " &
            "Where t_SPContoh.TglSP <> t_DPBSample.TglDPB " &
            "  And t_SPContoh.AktifYN = 'Y' " &
            "  And Year(TglSP) > = 2011 " &
            "ORDER BY t_SPContoh.IDRec "
            Rs = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To Rs.Rows.Count - 1
                Application.DoEvents()
                MsgSQL = "Update t_SPContoh Set " &
                    "TglSP = '" & Rs.Rows(a) !TglDPB & "' " &
                    "Where IdRec = '" & Rs.Rows(a) !idSP & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next (a)
        End If
        tTambah = Proses.UserAksesTombol(UserID, "51_PRA_LHP", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "51_PRA_LHP", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "51_PRA_LHP", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "51_PRA_LHP", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarPraLHP()
    End Sub

    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 30
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

    Private Sub NoSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoSP.KeyPress
        Dim DBSP As New DataTable
        If e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Then
                SQL = "Select NoSP, Kode_Perajin, Perajin " &
                    " From T_SP " &
                    "Where AktifYN='Y' and NoSP = '" & NoSP.Text & "' "
                DBSP = Proses.ExecuteQuery(SQL)
                If DBSP.Rows.Count <> 0 Then
                    Kode_Perajin.Text = DBSP.Rows(0) !Kode_Perajin
                    NamaPerajin.Text = DBSP.Rows(0) !Perajin
                    KodeProduk.Focus()
                Else
                    NoSP.Text = Proses.FindSP(NoSP.Text, Kode_Perajin.Text)
                    'NoSP.Text = FindSP(NoSP.Text, Kode_Perajin.Text)
                    SQL = "Select NoSP, Kode_Perajin, Perajin " &
                    " From T_SP " &
                    "Where AktifYN='Y' and NOSP = '" & NoSP.Text & "' "
                    DBSP = Proses.ExecuteQuery(SQL)
                    If DBSP.Rows.Count <> 0 Then
                        Kode_Perajin.Text = DBSP.Rows(0) !Kode_Perajin
                        NamaPerajin.Text = DBSP.Rows(0) !Perajin
                        KodeProduk.Focus()
                    Else
                        MsgBox("NO SP tidak boleh kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                        Kode_Perajin.Text = ""
                        NamaPerajin.Text = ""
                        NoSP.Text = ""
                        NoSP.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Kargo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kargo.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            NoSP.Focus()
        End If
    End Sub

    Private Sub QTYPack_TextChanged(sender As Object, e As EventArgs) Handles QTYPack.TextChanged
        If Trim(QTYPack.Text) = "" Then QTYPack.Text = 0
        If IsNumeric(QTYPack.Text) Then
            Dim temp As Double = QTYPack.Text
            QTYPack.SelectionStart = QTYPack.TextLength
        Else
            QTYPack.Text = 0
        End If
    End Sub

    Private Sub Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Keterangan.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then InstruksiPacking.Focus()
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

    Private Sub InstruksiPacking_KeyPress(sender As Object, e As KeyPressEventArgs) Handles InstruksiPacking.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then cmdSimpan.Focus()
    End Sub

    Private Sub tglTerima_ValueChanged(sender As Object, e As EventArgs) Handles tglTerima.ValueChanged

    End Sub

    Private Sub Koordinator_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Koordinator.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Keterangan.Focus()
    End Sub

    Private Sub SuratPengantar_TextChanged(sender As Object, e As EventArgs) Handles SuratPengantar.TextChanged

    End Sub

    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim MsgSQL As String = "", RSI As New DataTable, CekSP As String = ""
            SQL = "Select NoSP " &
                    " From T_SP " &
                    "Where NoSP = '" & NoSP.Text & "' and aktifYN = 'Y' "
            CekSP = Proses.ExecuteSingleStrQuery(SQL)
            If CekSP = "" Then
                MsgBox("No SP : " & NoSP.Text & " tidak ada/ tidak terdaftar !", vbCritical + vbOKOnly, ".:Warning! ")
                NoSP.Focus()
                Exit Sub
            End If
            MsgSQL = "Select NoPO, KodeImportir, Importir, Jumlah, CatatanProduk, HargaBeliRP, Produk, CatatanSP, KodeProduk " &
                " From T_SP " &
                "Where KodeProduk = '" & KodeProduk.Text & "' " &
                " And AktifYN = 'Y' " &
                " And NoSP = '" & NoSP.Text & "' "
            RSI = Proses.ExecuteQuery(MsgSQL)
            If RSI.Rows.Count <> 0 Then
                If CekPraLHP(NoSP.Text, KodeProduk.Text) Then
                    MsgBox(KodeProduk.Text + " Sudah pernah di input di no Pra LHP ini!", vbCritical, ".:Double Input!")
                    KodeProduk.Focus()
                    Exit Sub
                End If
                NoPO.Text = RSI.Rows(0) !NoPO
                Kode_Importir.Text = RSI.Rows(0) !KodeImportir
                Importir.Text = RSI.Rows(0) !Importir
                Jumlah.Text = Format(RSI.Rows(0) !Jumlah, "###,##0")
                QTYPack.Text = Format(RSI.Rows(0) !Jumlah - JumPack(KodeProduk.Text, NoSP.Text), "###,##0")
                HargaBeli.Text = Format(RSI.Rows(0) !HargaBeliRp, "###,##0")
                Produk.Text = Replace(RSI.Rows(0) !Produk, "'", "`")
                SpeckSP.Text = RSI.Rows(0) !CatatanProduk
                LocGmb1.Text = Trim(RSI.Rows(0) !KodeProduk) + ".jpg"
                '   LocGmb1.Text = RSI.Rows(0) !file_foto
            Else
                Dim RS05 As New DataTable
                KodeProduk.Text = Proses.FindKodeProdukSP(KodeProduk.Text, NoSP.Text)
                If Trim(KodeProduk.Text) <> "" Then
                    If CekPraLHP(NoSP.Text, KodeProduk.Text) Then
                        MsgBox(KodeProduk.Text + " Sudah pernah di input di no Pra LHP ini!", vbCritical, ".:Double Input!")
                        KodeProduk.Focus()
                        Exit Sub
                    End If
                    MsgSQL = "Select NoPO, KodeProduk, KodeImportir, CatatanProduk, Importir, Jumlah, HargaBeliRP, Produk, CatatanSP " &
                        " From T_SP " &
                        "Where KodeProduk = '" & KodeProduk.Text & "' " &
                        " And AktifYN = 'Y' " &
                        " And NoSP = '" & NoSP.Text & "' "
                    RS05 = Proses.ExecuteQuery(MsgSQL)

                    If RS05.Rows.Count() <> 0 Then
                        NoPO.Text = RS05.Rows(0) !NoPO
                        Kode_Importir.Text = RS05.Rows(0) !KodeImportir
                        Importir.Text = RS05.Rows(0) !Importir
                        Jumlah.Text = Format(RS05.Rows(0) !Jumlah, "###,##0")
                        QTYPack.Text = Format(RS05.Rows(0) !Jumlah - JumPack(KodeProduk.Text, NoSP.Text), "###,##0")
                        HargaBeli.Text = Format(RS05.Rows(0) !HargaBeliRp, "###,##0")
                        Produk.Text = Replace(RS05.Rows(0) !Produk, "'", "`")
                        SpeckSP.Text = RS05.Rows(0) !CatatanProduk
                        LocGmb1.Text = Trim(RS05.Rows(0) !KodeProduk) + ".jpg"
                    End If
                    Proses.CloseConn()
                End If
            End If

            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If

            If LAdd Or LEdit Or LTambahKode Then
                If Trim(KodeProduk.Text) = "" Or Trim(Produk.Text) = "" Then
                    KodeProduk.Focus()
                ElseIf Trim(KodeProduk.Text) <> "" Or Trim(Produk.Text) <> "" Then
                    QTYPack.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub QtyKoli_TextChanged(sender As Object, e As EventArgs) Handles QtyKoli.TextChanged
        If Trim(QtyKoli.Text) = "" Then QtyKoli.Text = 0
        If IsNumeric(QtyKoli.Text) Then
            Dim temp As Double = QtyKoli.Text
            QtyKoli.SelectionStart = QtyKoli.TextLength
        Else
            QtyKoli.Text = 0
        End If
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

    Private Sub Jumlah_TextChanged(sender As Object, e As EventArgs) Handles Jumlah.TextChanged

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Function CekPraLHP(tNoSP As String, tKodeProduk As String) As Boolean
        Dim MsgSQL As String, RSCek As New DataTable
        MsgSQL = "Select * " &
            "From t_PraLHP " &
            "Where Kode_Produk = '" & tKodeProduk & "' " &
            "  And NoSP = '" & tNoSP & "' " &
            "  And NoPraLHP = '" & NoPraLHP.Text & "' " &
            "  And AktifYN = 'Y' "
        RSCek = Proses.ExecuteQuery(MsgSQL)

        If RSCek.Rows.Count <> 0 Then
            CekPraLHP = True
        Else
            CekPraLHP = False
        End If
    End Function

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Function JumPack(tKode, tNoSP) As Double
        Dim RJ As New DataTable, msgSQL As String = ""
        msgSQL = "Select isnull(Sum(JumlahBaik),0) JumlahPack " &
            "From t_LHP " &
            "Where NoSP = '" & tNoSP & "' " &
            "  And Kode_Produk = '" & tKode & "' " &
            "  And AktifYN = 'Y' "
        RJ = Proses.ExecuteQuery(msgSQL)
        If Not RJ.Rows.Count() <> 0 Then
            JumPack = RJ.Rows(0) !JumlahPack
        Else
            JumPack = 0
        End If
    End Function

    Private Sub cPraLHP_TextChanged(sender As Object, e As EventArgs) Handles cPraLHP.TextChanged

    End Sub

    Private Sub QTYPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QTYPack.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Jumlah.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(QTYPack.Text) Then
                Dim temp As Double = QTYPack.Text
                QTYPack.Text = Format(temp, "###,##0.00")
                QTYPack.SelectionStart = QTYPack.TextLength
            Else
                QTYPack.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Kirim.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        If NoSP.Text = "" Then Exit Sub
        MsgSQL = "Select Top 1 * From t_PraLHP " &
            "Where aktifYN = 'Y' " &
            " And NoPraLHP = '" & NoPraLHP.Text & "' " &
            "Order By TglPraLHP, IdRec  "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
            IDRecord.Text = tIdRec
        Else
            tIdRec = ""
        End If
        Call IsiPraLHP()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_PraLHP " &
            "Where NoSP = '" & NoSP.Text & "' " &
            " And IDRec < '" & IDRecord.Text & "' " &
            " And NoPraLHP = '" & NoPraLHP.Text & "' " &
            " And aktifYN = 'Y' " &
            "Order By IdRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
            IDRecord.Text = tIdRec
        Else
            tIdRec = ""
        End If
        Call IsiPraLHP()
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        If NoSP.Text = "" Then Exit Sub
        MsgSQL = "Select Top 1 * From t_PraLHP " &
            "Where aktifYN = 'Y' " &
            " And NoPraLHP = '" & NoPraLHP.Text & "' " &
            "Order By TglPraLHP Desc, IdRec Desc  "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
            IDRecord.Text = tIdRec
        Else
            tIdRec = ""
        End If
        Call IsiPraLHP()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        Form_Hapus.Left = Me.Left
        Form_Hapus.Top = Me.Top
        Form_Hapus.tIDSebagian.Text = IDRecord.Text
        Form_Hapus.tIDSemua.Text = NoPraLHP.Text
        Form_Hapus.Text = "Hapus Pra LHP"
        Form_Hapus.ShowDialog()
        ClearTextBoxes()
        DaftarPraLHP()
        'Dim MsgSQL As String
        'If Trim(IDRecord.Text) = "" Then
        '    MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
        '    Exit Sub
        'End If
        'If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation) = vbYes Then
        '    MsgSQL = "Update t_PraLHP SET  " &
        '        " AktifYN = 'N', " &
        '        "  UserID = '" & UserID & "', " &
        '        " LastUPD = GetDate(), " &
        '        " TransferYN = 'N'  " &
        '        "Where IDRec = '" & IDRecord.Text & "' "
        '    Proses.ExecuteNonQuery(MsgSQL)
        '    Dim rs As New DataTable
        '    MsgSQL = "Select Top 1 * From t_PraLHP " &
        '        "Where IDRec > '" & IDRecord.Text & "' " &
        '        " And NoSP = '" & NoSP.Text & "' and aktifYN = 'Y' " &
        '        "Order By IdRec "
        '    rs = Proses.ExecuteQuery(MsgSQL)
        '    If rs.Rows.Count <> 0 Then
        '        btnNext_Click(sender, e)
        '    Else
        '        btnTop_Click(sender, e)
        '    End If
        'End If
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

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
            If Jumlah.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
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
            If LAdd Or LEdit Or LTambahKode Then tglTerima.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub tglTerima_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglTerima.KeyPress
        If e.KeyChar = Chr(13) Then
            SuratPengantar.Focus()
        End If
    End Sub

    Private Sub SuratPengantar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SuratPengantar.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            QtyKoli.Focus()
        End If
    End Sub

    Private Sub QtyKoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyKoli.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Jumlah.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(QtyKoli.Text) Then
                Dim temp As Double = QtyKoli.Text
                QtyKoli.Text = Format(temp, "###,##0.00")
                QtyKoli.SelectionStart = QtyKoli.TextLength
            Else
                QtyKoli.Text = 0
            End If
            SQL = "Select KoordinatorPraLHP From M_Company "
            Koordinator.Text = Proses.ExecuteSingleStrQuery(SQL)
            If LAdd Or LEdit Or LTambahKode Then Koordinator.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub TabControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseClick
        If TabControl1.SelectedIndex.ToString = "1" Then
            'DaftarPraLHP()
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Visible = False
        DGView2.Rows.Clear()
        Me.Cursor = Cursors.WaitCursor
        Dim tCari As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Dim MsgSQL As String, rsc As New DataTable, Terkirim As Double = 0, KurangKirim As Double = 0
        MsgSQL = "Select IDRec, Produk, Kode_Produk, JumlahPack, Jumlah, HargaBeli, NoSP,NoPO " &
            " From T_PraLHP " &
            "Where AktifYN = 'Y' " &
            "  And NoPraLHP = '" & tCari & "' " &
            "Order By IDRec "
        rsc = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsc.Rows.Count - 1
            Application.DoEvents()
            SQL = "Select Sum(JumlahBaik) JB From T_LHP " &
                "Where Kode_Produk = '" & rsc.Rows(a) !Kode_Produk & "' " &
                " And NoPraLHP = '" & tCari & "' "
            Terkirim = Proses.ExecuteSingleDblQuery(SQL)
            KurangKirim = rsc.Rows(a) !JumlahPack - Terkirim
            DGView2.Rows.Add(rsc.Rows(a) !IdRec,
                    rsc.Rows(a) !Kode_Produk,
                    Format(rsc.Rows(a) !JumlahPack, "###,##0"),
                    Format(rsc.Rows(a) !Jumlah, "###,##0"),
                    Format(rsc.Rows(a) !HargaBeli, "###,##0"),
                    Format(Terkirim, "###,##0"),
                    Format(KurangKirim, "###,##0"),
                    rsc.Rows(a) !NoSP,
                    rsc.Rows(a) !NoPO)
        Next (a)
        Me.Cursor = Cursors.Default
        DGView2.Visible = True

        If DGView2.Rows.Count <> 0 Then
            IDRecord.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
            IsiPraLHP()
        End If
    End Sub

    Private Sub cPraLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cPraLHP.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarPraLHP()
        End If
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        If DGView2.Rows.Count <> 0 Then
            IDRecord.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
            IsiPraLHP()
        End If
    End Sub
End Class