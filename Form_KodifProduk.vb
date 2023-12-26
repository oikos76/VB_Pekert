Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_KodifProduk
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean, lVariasi As Boolean, LMultiKode As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim tdPrev_USD As Date, tPrev_USD As Double,
        tdPrev_RP As Date, tPrev_RP As Double

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSql As String = ""
        Descript.Text = Replace(Descript.Text, "'", "`")
        Deskripsi.Text = Replace(Deskripsi.Text, "'", "`")
        If Trim(PathFoto.Text) = "" Then
            PathFoto.Text = FotoLoc
        End If
        If cmbInisiatif.Text = "" Then
            MsgBox("Inisiatif Produk ini belum di pilih!", vbCritical + vbOKOnly, ".:Warning!")
            cmbInisiatif.Focus()
            Exit Sub
        End If
        If Trim(Lebar.Text) = "" Then Lebar.Text = 0
        If Trim(Diameter.Text) = "" Then Diameter.Text = 0
        If Trim(Panjang.Text) = "" Then Panjang.Text = 0

        If Trim(Tinggi.Text) = "" Then Tinggi.Text = 0
        If Trim(Berat.Text) = "" Then Berat.Text = 0
        If Trim(Tebal.Text) = "" Then Tebal.Text = 0

        If Trim(Kapasitas.Text) = "" Then Kapasitas.Text = 0

        If Trim(cur_Euro.Text) = "" Then cur_Euro.Text = 0
        If Trim(Prev_Euro.Text) = "" Then Prev_Euro.Text = 0

        If Trim(Cur_USD.Text) = "" Then Cur_USD.Text = 0
        If Trim(Prev_USD.Text) = "" Then Prev_USD.Text = 0

        If Trim(cur_rp.Text) = "" Then cur_rp.Text = 0
        If Trim(Prev_RP.Text) = "" Then Prev_RP.Text = 0
        If Trim(HPP_USD.Text) = "" Then HPP_USD.Text = 0

        LocGmb1.Text = Trim(KodeProduk.Text) + ".jpg"
        If LAdd Or LMultiKode Then
            Dim CekProduk As String = ""
            MsgSql = "SELECT Deskripsi FROM M_KodeProduk WHERE kodeProduk = '" & KodeProduk.Text & "'  AND AktifYN='Y' "
            CekProduk = Proses.ExecuteSingleStrQuery(MsgSql)
            If CekProduk <> "" Then
                MsgBox("Kode : " & KodeProduk.Text & " SUDAH Pernah di buat !", vbOKOnly + vbCritical, ".:Warning !")
                Exit Sub
            End If
            MsgSql = "INSERT INTO m_KodeProduk (KodeProduk, descript, " &
                "deskripsi, satuan, FotoLoc, tanggal, panjang, lebar, " &
                "tinggi, diameter, berat, tebal, tamb_ing, notes, tamb_sp, " &
                "contoh, kode_bahan, bahan_ind, fungsi, fungsi_ind, cur_usd, " &
                "dcur_usd, prev_usd, dprev_usd, cur_rp, dcur_rp, prev_rp, " &
                "dprev_rp, Kode_Perajin, Perajin, kapasitas, KodePerajin2, " &
                "usd_edited, rp_edited, file_foto, hpp_usd, AktifYN, UserID, " &
                "LastUPD, TransferYN, inisiatif, cur_euro, dcur_euro, " &
                "prev_euro, dprev_euro) VALUES('" & Trim(KodeProduk.Text) & "', " &
                "'" & Trim(Descript.Text) & "', '" & Replace(Trim(Deskripsi.Text), "'", "`") & "', " &
                "'" & Satuan.Text & "', '" & PathFoto.Text & "', " &
                "'" & Format(tglMasuk.Value, "yyyy-MM-dd") & "', " & Panjang.Text * 1 & ", " &
                " " & Lebar.Text * 1 & ", " & Tinggi.Text * 1 & ", " & Diameter.Text * 1 & ", " &
                " " & Berat.Text * 1 & ", " & Tebal.Text * 1 & ", 'tamb_ing', " &
                " '" & Trim(Catatan.Text) & "', '" & Trim(TambSP.Text) & "',  " &
                " '" & chkContoh.Checked & "', '" & Trim(Kode_Bahan.Text) & "', " &
                " '" & Trim(Bahan.Text) & "', '" & Trim(Kode_Fungsi.Text) & "', " &
                " '" & Trim(Fungsi.Text) & "', " & Cur_USD.Text * 1 & ", " &
                " '" & Format(DCur_USD.Value, "yyyy-MM-dd") & "' , " & Prev_USD.Text * 1 & ", " &
                " '" & Format(dprev_usd.Value, "yyyy-MM-dd") & "', " & cur_rp.Text * 1 & ", " &
                " '" & Format(DCur_RP.Value, "yyyy-MM-dd") & "', " & Prev_RP.Text * 1 & ", " &
                " '" & Format(DPrev_RP.Value, "yyyy-MM-dd") & "', '" & Trim(Kode_Perajin.Text) & "', " &
                " '" & Trim(Perajin.Text) & "', '" & Kapasitas.Text & "', '" & Trim(KodePerajin2.Text) & "', " &
                " '" & UserID & "', '', '" & Trim(LocGmb1.Text) & "', " & HPP_USD.Text * 1 & ", " &
                " 'Y', '" & UserID & "', GetDate(), 'N', '" & Trim(cmbInisiatif.Text) & "', " &
                " " & cur_Euro.Text * 1 & ", '" & Format(DCur_Euro.Value, "yyyy-MM-dd") & "' , " &
                " " & Prev_Euro.Text * 1 & ", '" & Format(dprev_Euro.Value, "yyyy-MM-dd") & "')"
            Proses.ExecuteNonQuery(MsgSql)
        ElseIf LEdit Then

            MsgSql = "Update m_KodeProduk Set " &
                "FotoLoc = '" & Trim(PathFoto.Text) & "', file_foto = '" & Trim(LocGmb1.Text) & "', " &
                "Kode_Perajin = '" & Kode_Perajin.Text & "', inisiatif = '" & Trim(cmbInisiatif.Text) & "', " &
                "Perajin = '" & Perajin.Text & "', " &
                "Descript = '" & Trim(Descript.Text) & "', Deskripsi = '" & Deskripsi.Text & "', " &
                "Satuan = '" & Trim(Satuan.Text) & "', " &
                "tanggal = '" & Format(tglMasuk.Value, "yyyy-MM-dd") & "', " &
                "Panjang = " & Panjang.Text & ", Lebar  = " & Lebar.Text & "," &
                "Tinggi = " & Tinggi.Text & ", Diameter = " & Diameter.Text & ", " &
                "Berat = " & Berat.Text & ", Tebal = " & Tebal.Text & ", " &
                "Notes = '" & Trim(Catatan.Text) & "', " &
                "tamb_sp = '" & Trim(TambSP.Text) & "', " &
                "Contoh = '" & chkContoh.Checked & "',usd_edited = '" & Trim(Edit.Text) & "', " &
                "Kode_Bahan = '" & Kode_Bahan.Text & "', bahan_ind = '" & Bahan.Text & "', " &
                "Fungsi = '" & Kode_Fungsi.Text & "', fungsi_ind = '" & Trim(Fungsi.Text) & "', " &
                "Cur_USD = " & Cur_USD.Text * 1 & ", DCur_USD = '" & Format(DCur_USD.Value, "yyyy-MM-dd") & "', " &
                "Prev_USD = " & Prev_USD.Text * 1 & ", dprev_usd = '" & Format(dprev_usd.Value, "yyyy-MM-dd") & "', " &
                "Cur_euro = " & cur_Euro.Text * 1 & ", DCur_euro = '" & Format(DCur_Euro.Value, "yyyy-MM-dd") & "', " &
                "Prev_euro= " & Prev_Euro.Text * 1 & ", dprev_euro = '" & Format(dprev_Euro.Value, "yyyy-MM-dd") & "', " &
                "cur_rp= " & cur_rp.Text * 1 & ", DCur_RP = '" & Format(DCur_RP.Value, "yyyy-MM-dd") & "', " &
                "Prev_RP = " & Prev_RP.Text * 1 & ", DPrev_RP = '" & Format(DPrev_RP.Value, "yyyy-MM-dd") & "', " &
                "Kapasitas = " & Trim(Kapasitas.Text) * 1 & ", KodePerajin2 = '" & Trim(KodePerajin2.Text) & "', " &
                "HPP_USD = " & HPP_USD.Text * 1 & ", TransferYN = 'N' " &
                "Where KodeProduk = '" & KodeProduk.Text & "'"
            Proses.ExecuteNonQuery(MsgSql)

            MsgSql = "update t_KatalogProduk Set " &
                " HargaAsli = " & Cur_USD.Text * 1 & ", " &
                " HargaBeli = " & cur_rp.Text * 1 & ", " &
                "  HargaFOB = " & Cur_USD.Text * 1 & " " &
                "Where KodeProduk = '" & KodeProduk.Text & "' "
            Proses.ExecuteNonQuery(MsgSql)

            MsgSql = "update t_KatalogProduk Set " &
                " HargaJual = " & Cur_USD.Text * 1 & " * Konversi " &
                "Where KodeProduk = '" & KodeProduk.Text & "' "
            Proses.ExecuteNonQuery(MsgSql)

            MsgSql = "update t_KatalogProduk Set " &
                " HargaJual = " & Cur_USD.Text * 1 & " * Konversi " &
                "Where KodeProduk = '" & KodeProduk.Text & "' and matauang = 'USD' "
            Proses.ExecuteNonQuery(MsgSql)

            MsgSql = "update t_KatalogProduk Set " &
                " HargaJual = " & Cur_USD.Text * 1 & " / Konversi " &
                "Where KodeProduk = '" & KodeProduk.Text & "' and matauang = 'EURO' "
            Proses.ExecuteNonQuery(MsgSql)
        End If
        If lstVwMultiKode.Rows.Count <> 0 Then
            MsgSql = "Delete m_KodeProdukVariasi " &
                "Where KodeInduk = '" & Trim(KodeProduk.Text) & "' "
            Proses.ExecuteNonQuery(MsgSql)
            For i As Integer = 0 To lstVwMultiKode.Rows.Count - 1
                MsgSql = "INSERT INTO m_KodeProdukVariasi(KodeInduk, " &
                    "KodeAnak, NamaProduk, transferYN) Values ('" & Trim(KodeProduk.Text) & "', " &
                    "'" & Trim(lstVwMultiKode.Rows(i).Cells(0).Value) & "', " &
                    "'" & Replace(Trim(lstVwMultiKode.Rows(i).Cells(1).Value), "'", "`") & "', 'N') "
                Proses.ExecuteNonQuery(MsgSql)
            Next i
        End If

        LAdd = False
        LEdit = False
        lVariasi = False
        LMultiKode = False
        AturTombol(True)
        Daftar()
        Me.Cursor = Cursors.Default
        If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
            ShowFoto("")
        Else
            ShowFoto(LocGmb1.Text)
        End If
    End Sub


    'Private Sub Daftar()
    '    Dim mKondisi As String, RSD As New ADODB.Recordset
    '    Dim MsgSQL As String, Lst As ListItem, i As Double
    '    If Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) = "" And Trim(TDeskripsi.Text) = "" Then
    '        mKondisi = ""
    '    ElseIf Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) <> "" Then
    '        mKondisi = "And kode_bahan = '" & Trim(Right(cmbBahan.Text, 10)) & "' "
    '    ElseIf Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) = "" Then
    '        mKondisi = "And fungsi = '" & Trim(Right(CmbFungsi.Text, 10)) & "' "
    '    ElseIf Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) <> "" Then
    '        mKondisi = "And fungsi = '" & Trim(Right(CmbFungsi.Text, 10)) & "' " &
    '        "And kode_bahan = '" & Trim(Right(cmbBahan.Text, 10)) & "' "
    '    ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) = "" Then
    '        mKondisi = " AND Kode_Perajin ='" & Trim(Right(cmbNamaPerajin.Text, 10)) & "' "
    '    ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) <> "" Then
    '        mKondisi = " AND Kode_Perajin ='" & Trim(Right(cmbNamaPerajin.Text, 10)) & "' " &
    '        "And kode_bahan = '" & Trim(Right(cmbBahan.Text, 10)) & "' "
    '    ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) = "" Then
    '        mKondisi = " AND Kode_Perajin ='" & Trim(Right(cmbNamaPerajin.Text, 10)) & "' " &
    '        "And fungsi = '" & Trim(Right(CmbFungsi.Text, 10)) & "' "
    '    ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) <> "" Then
    '        mKondisi = " AND Kode_Perajin ='" & Trim(Right(cmbNamaPerajin.Text, 10)) & "' " &
    '        "And fungsi = '" & Trim(Right(CmbFungsi.Text, 10)) & "' " &
    '        "And kode_bahan = '" & Trim(Right(cmbBahan.Text, 10)) & "' "
    '    ElseIf Trim(TDeskripsi.Text) <> "" Then
    '        mKondisi = " AND Deskripsi Like '%" & TDeskripsi & "%' "
    '    End If
    '    MsgSQL = "Select Count(KodeProduk) JRec " &
    '    " From m_KodeProduk A " &
    '    " Where A.AktifYN = 'Y' " &
    '    "   And KodeProduk is Not NULL " &
    '    "   And Deskripsi is NOT NULL " &
    '    " " & mKondisi & " "
    '    RSD.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
    'If Not RSD.EOF Then
    '        If RSD!JRec = Null Then
    '            JRec.Text = 0
    '        Else
    '            JRec.Text = Format(RSD!JRec, "###,##0")
    '        End If
    '    End If
    '    RSD.Close
    '    PBar1.Visible = True
    '    PBar1.Max = (JRec * 1) + 1
    '    MsgSQL = "Select A.* " &
    '    " From m_KodeProduk A " &
    '    " Where A.AktifYN = 'Y' " &
    '    "   And KodeProduk is Not NULL " &
    '    "   And Deskripsi is NOT NULL " &
    '    " " & mKondisi & " " &
    '    " Order By KodeProduk "
    '    RSD.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
    'LstBahan.ListItems.Clear
    '    LstBahan.Visible = False
    '    i = 0
    '    Do While Not RSD.EOF
    '        DoEvents
    '        PBar1.Value = i
    '    Set Lst = LstBahan.ListItems.Add(, , RSD!KodeProduk)
    '    Lst.SubItems(1) = RSD!Deskripsi
    '        Lst.SubItems(2) = IIf(IsNull(RSD!Perajin), "", RSD!Perajin)
    '        Lst.SubItems(3) = RSD!fungsi_ind
    '        Lst.SubItems(4) = RSD!bahan_ind
    '        Lst.SubItems(5) = RSD!File_Foto
    '        Lst.SubItems(6) = Format(RSD!Tanggal, "dd-MM-yyyy")
    '        Lst.SubItems(7) = IIf(IsNull(RSD!Descript), "", RSD!Descript)
    '        i = i + 1
    '        RSD.MoveNext
    '    Loop
    '    RSD.Close
    '    SeekKodeBrg
    'Set RSD = Nothing
    'LstBahan.Visible = True
    '    PBar1.Visible = False
    'End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        ClearTextBoxes()
        Edit.Text = UserID
        CmbFungsi.SelectedIndex = -1
        AturTombol(False)
        Kode_Fungsi.ReadOnly = False
        CmbFungsi.Select()
        CmbFungsi.Focus()
        TabPageDaftar_.Enabled = False
        TabPageVariasi_.Enabled = False
    End Sub

    Dim objRep As New ReportDocument

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
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
        With Me.lstVwMultiKode.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        lstVwMultiKode.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        lstVwMultiKode.BackgroundColor = Color.LightGray
        lstVwMultiKode.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        lstVwMultiKode.DefaultCellStyle.SelectionForeColor = Color.White
        lstVwMultiKode.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        lstVwMultiKode.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        lstVwMultiKode.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        lstVwMultiKode.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        lstVwMultiKode.AlternatingRowsDefaultCellStyle.BackColor = Color.White
    End Sub
    Private Sub Form_KodifProduk_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim tWilayah As String = ""
        DGView.Rows.Clear()
        tglMasuk.Value = Now()
        Lebar.Text = ""
        LAdd = False
        LEdit = False
        lVariasi = False
        LMultiKode = False
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        cmbInisiatif.Items.Clear()
        cmbInisiatif.Items.Add("")
        cmbInisiatif.Items.Add("PERAJIN")
        cmbInisiatif.Items.Add("PEKERTI")
        cmbInisiatif.Items.Add("IMPORTIR")
        cmbInisiatif.SelectedIndex = 2
        IsiComboDaftar()
        tTambah = Proses.UserAksesTombol(UserID, "26_SP_DENGAN_KODE", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "26_SP_DENGAN_KODE", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "26_SP_DENGAN_KODE", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "26_SP_DENGAN_KODE", "laporan")
        AturTombol(True)
        PanelSimpanMultiKode.Visible = False
        cmdSimpan.Visible = False
        cmdBatal.Visible = False
        ShowFoto("")
        SQL = "Select FotoLoc FROM M_COMPANY "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            FotoLoc = dbTable.Rows(0) !FotoLoc
            If FotoLoc <> My.Settings.path_foto Then
                FotoLoc = My.Settings.path_foto
            End If
        End If
        Dim dbCek As String
        SQL = "SELECT * FROM m_KodeProduk WHERE aktifYN = 'N' "
        dbCek = Proses.ExecuteSingleStrQuery(SQL)
        If dbCek <> "" Then
            SQL = " DELETE M_KODEPRODUK WHERE aktifYN='N' "
            Proses.ExecuteNonQuery(SQL)
        End If

        'SQL = "Select kodeproduk, deskripsi, bahan_ind, namaindonesia
        '  From m_KodeProduk inner join m_KodeBahan on m_KodeProduk.kode_bahan = m_KodeBahan.KodeBahan
        ' Where NamaIndonesia <> bahan_ind "
        'dbCek = Proses.ExecuteSingleStrQuery(SQL)
        'If dbCek <> "" Then
        '    SQL = " UPDATE M_KODEPRODUK
        '        SET BAHAN_IND = NAMAINDONESIA
        '        FROM m_KodeProduk INNER JOIN m_KodeBahan on m_KodeProduk.kode_bahan = m_KodeBahan.KodeBahan "
        '    ' Proses.ExecuteNonQuery(SQL)
        'End If
    End Sub

    Private Sub KodeCari_TextChanged(sender As Object, e As EventArgs) Handles KodeCari.TextChanged
        If Len(KodeCari.Text) < 1 Then
            KodeCari.Text = ""
        ElseIf Len(KodeCari.Text) = 5 Then
            KodeCari.Text = KodeCari.Text + "-"
            KodeCari.SelectionStart = Len(Trim(KodeCari.Text)) + 1
        ElseIf Len(KodeCari.Text) = 8 Then
            KodeCari.Text = KodeCari.Text + "-"
            KodeCari.SelectionStart = Len(Trim(KodeCari.Text)) + 1
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub IsiComboDaftar()
        Dim MsgSQL As String, rsc As New DataTable, tString As String = ""

        Me.Cursor = Cursors.WaitCursor
        cmbNamaPerajin.Items.Clear()
        cmbNamaPerajin.Items.Add("")
        MsgSQL = "Select Nama, KodePerajin From M_KodePerajin " &
            "Where AktifYN = 'Y' " &
            "Order By Nama "
        rsc = Proses.ExecuteQuery(MsgSQL)
        With rsc.Columns(0)
            For a = 0 To rsc.Rows.Count - 1
                Application.DoEvents()
                tString = Microsoft.VisualBasic.Left(.Table.Rows(a) !Nama & Space(100), 100) +
                           Microsoft.VisualBasic.Right(Space(10) + .Table.Rows(a) !KodePerajin, 10)
                cmbNamaPerajin.Items.Add(tString)
            Next (a)
        End With

        CmbFungsi.Items.Clear()
        CmbFungsi.Items.Add(" ")
        MsgSQL = "Select NamaIndonesia, KodeFungsi From M_KodeFungsi " &
            "Where AktifYN = 'Y' " &
            "Order By NamaIndonesia "
        rsc = Proses.ExecuteQuery(MsgSQL)
        With rsc.Columns(0)
            For a = 0 To rsc.Rows.Count - 1
                Application.DoEvents()
                tString = Microsoft.VisualBasic.Left(.Table.Rows(a) !NamaIndonesia & Space(100), 100) +
                           Microsoft.VisualBasic.Right(Space(10) + .Table.Rows(a) !KodeFungsi, 10)
                CmbFungsi.Items.Add(tString)
            Next (a)
        End With

        cmbBahan.Items.Clear()
        cmbBahan.Items.Add(" ")
        MsgSQL = "Select NamaIndonesia, KodeBahan From M_KodeBahan " &
        "Where AktifYN = 'Y' " &
        "Order By NamaIndonesia "

        rsc = Proses.ExecuteQuery(MsgSQL)
        With rsc.Columns(0)
            For a = 0 To rsc.Rows.Count - 1
                Application.DoEvents()
                tString = Microsoft.VisualBasic.Left(.Table.Rows(a) !NamaIndonesia & Space(100), 100) +
                           Microsoft.VisualBasic.Right(Space(10) + .Table.Rows(a) !KodeBahan, 10)
                cmbBahan.Items.Add(tString)
            Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdCari_Click(sender As Object, e As EventArgs) Handles cmdCari.Click
        PanelCariKode.Visible = True
        GBoxInfoUmum.Visible = False
        KodeCari.Text = ""
        KodeCari.Focus()
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
            .SetToolTip(Me.LocGmb1, "Click DI SINI untuk ganti nama Folder simpan Foto.")
            .SetToolTip(Me.cmdPrint, "Cetak Daftar Produk")
        End With
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select Top 1 isnull(KodeProduk,'') KodeProduk From M_KodeProduk " &
            "WHERE KodeProduk > '" & KodeProduk.Text & "' and AKTIFYN = 'Y'  " &
            "Order By KodeProduk "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            KodeProduk.Text = Rs.Rows(0) !KodeProduk
        Else
            KodeProduk.Text = ""
        End If
        IsiKodeProduk(KodeProduk.Text)
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select Top 1 isnull(KodeProduk,'') KodeProduk From M_KodeProduk " &
            "WHERE KodeProduk < '" & KodeProduk.Text & "' and AKTIFYN = 'Y' " &
            "Order By KodeProduk desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count > 0 Then
            KodeProduk.Text = Rs.Rows(0) !KodeProduk
        Else
            KodeProduk.Text = ""
        End If
        IsiKodeProduk(KodeProduk.Text)
    End Sub

    Private Sub LocGmb1_Click(sender As Object, e As EventArgs) Handles LocGmb1.Click
        Form_SettingFolderFoto.ShowDialog()
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select Top 1 isnull(KodeProduk,'') KodeProduk " &
            " From M_KodeProduk " &
            "WHERE AKTIFYN = 'Y' and KodeProduk <> '' " &
            "Order By KodeProduk "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            KodeProduk.Text = Rs.Rows(0) !KodeProduk
        Else
            KodeProduk.Text = ""
        End If
        IsiKodeProduk(KodeProduk.Text)
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select Top 1 isnull(KodeProduk,'') KodeProduk From M_KodeProduk WHERE AKTIFYN = 'Y' " &
            "Order By KodeProduk DESC "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            KodeProduk.Text = Rs.Rows(0) !KodeProduk
        Else
            KodeProduk.Text = ""
        End If
        IsiKodeProduk(KodeProduk.Text)
    End Sub

    Private Sub RiwayatHarga()
        Dim MsgSQL As String, rsfind As New DataTable
        '    Dim LstFind As ListItem, TView As ListItem, i As Integer
        '    '    ClearText
        dgRiwayatHarga.Visible = False
        dgRiwayatHarga.Rows.Clear()
        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select t_DPB.HargaBeli, t_DPB.NoDPB, t_DPB.TglDPB, isnull(m_KodePerajin.Nama, '')  Nama " &
            " FROM t_DPB left JOIN m_KodePerajin ON t_DPB.KodePerajin = m_KodePerajin.KodePerajin " &
            "Where t_DPB.AktifYN = 'Y' and t_DPB.Kode_Produk = '" & KodeProduk.Text & "'  " &
            "Order by TglDPB Desc, NoDPB Desc "
        rsfind = Proses.ExecuteQuery(MsgSQL)

        For a = 0 To rsfind.Rows.Count - 1
            Application.DoEvents()
            dgRiwayatHarga.Rows.Add(Format(rsfind.Rows(a) !HargaBeli, "###,##0"),
                    rsfind.Rows(a) !nodpb,
                    Format(rsfind.Rows(a) !TglDPB, "dd-MM-yyyy"),
                    rsfind.Rows(a) !Nama)
        Next (a)
        dgRiwayatHarga.Visible = True
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Kode_Perajin_TextChanged(sender As Object, e As EventArgs) Handles Kode_Perajin.TextChanged
        If Len(Kode_Perajin.Text) < 1 Then
            Perajin.Text = ""
        End If
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdCari.Visible = tAktif
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
        PanelSimpanMultiKode.Visible = LMultiKode
        cmdVariasiMultiKode.Visible = tAktif
        cmdVariasi.Visible = tAktif

        PanelCariKode.Visible = False
        GBoxInfoUmum.Visible = True
        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageVariasi_.Enabled = True
        Me.Text = "Kodifikasi Produk"
    End Sub

    Private Sub Kode_Fungsi_TextChanged(sender As Object, e As EventArgs) Handles Kode_Fungsi.TextChanged
        If Len(Kode_Fungsi.Text) < 1 Then
            Kode_Fungsi.Text = ""
            Fungsi.Text = ""
            KodeProduk.Text = ""
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
        LocGmb1.Text = ""
        PictureBox1.Image = Nothing
    End Sub

    Private Sub Kode_Bahan_TextChanged(sender As Object, e As EventArgs) Handles Kode_Bahan.TextChanged
        If Len(Kode_Bahan.Text) < 1 Then
            Kode_Bahan.Text = ""
            Bahan.Text = ""
            KodeProduk.Text = ""
        End If
    End Sub

    Private Sub ShowFoto(NamaFileJPG As String)
        If NamaFileJPG = "" Then PictureBox1.Image = Nothing
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.ImageLocation = filename
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    Private Sub KodeCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeCari.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            IsiKodeProduk(KodeCari.Text)
            GBoxInfoUmum.Visible = True
            PanelCariKode.Visible = False
        End If
    End Sub

    Private Sub IsiVariasi(tKode As String)
        Dim MsgSQl As String = "", RSL As New DataTable
        KodeVariasi.Text = tKode
        NamaVariasi.Text = Deskripsi.Text
        lstVwMultiKode.Visible = False
        lstVwMultiKode.Rows.Clear()
        MsgSQl = "SELECT KodeAnak, deskripsi, cur_rp, Prev_RP, HPP_USD, Cur_USD, Prev_USD " &
            " FROM m_kodeprodukvariasi " &
            "      INNER JOIN m_KodeProduk on KodeProduk = KodeAnak " &
            "WHERE kodeinduk = '" & tKode & "' "
        RSL = Proses.ExecuteQuery(MsgSQl)
        Me.Cursor = Cursors.WaitCursor
        With RSL.Columns(0)
            For a = 0 To RSL.Rows.Count - 1
                Application.DoEvents()
                lstVwMultiKode.Rows.Add(.Table.Rows(a) !KodeAnak,
                    .Table.Rows(a) !Deskripsi,
                    Format(.Table.Rows(a) !cur_rp, "###,##0"),
                    Format(.Table.Rows(a) !Prev_RP, "###,##0"),
                    Format(.Table.Rows(a) !HPP_USD, "###,##0.00"),
                    Format(.Table.Rows(a) !Cur_USD, "###,##0.00"),
                    Format(.Table.Rows(a) !Prev_USD, "###,##0.00"))
            Next (a)
        End With
        Me.Cursor = Cursors.Default
        lstVwMultiKode.Visible = True
    End Sub

    Private Sub IsiKodeProduk(tKode As String)
        Dim MsgSQl As String = "", RSL As New DataTable, ada As Boolean = False
        MsgSQl = "SELECT * FROM m_KodeProduk " &
            "WHERE KodeProduk = '" & tKode & "' " &
            "  AND AktifYN = 'Y' "
        RSL = Proses.ExecuteQuery(MsgSQl)
        If PanelCariKode.Visible = True Then
            PanelCariKode.Visible = False
            GBoxInfoUmum.Visible = True
        End If
        If RSL.Rows.Count > 0 Then
            KodeProduk.Text = RSL.Rows(0) !KodeProduk
            KodePerajin2.Text = IIf(IsDBNull(RSL.Rows(0) !KodePerajin2), "", RSL.Rows(0) !KodePerajin2)
            Kode_Perajin.Text = RSL.Rows(0) !Kode_Perajin
            Perajin.Text = RSL.Rows(0) !Perajin
            Descript.Text = Trim(RSL.Rows(0) !Descript)
            Deskripsi.Text = Trim(RSL.Rows(0) !Deskripsi)
            Satuan.Text = IIf(IsDBNull(RSL.Rows(0) !Satuan), "", RSL.Rows(0) !Satuan)

            tglMasuk.Value = RSL.Rows(0) !Tanggal
            Panjang.Text = RSL.Rows(0) !Panjang
            Lebar.Text = RSL.Rows(0) !Lebar
            Tinggi.Text = RSL.Rows(0) !Tinggi
            Diameter.Text = RSL.Rows(0) !Diameter
            Berat.Text = RSL.Rows(0) !Berat
            Tebal.Text = RSL.Rows(0) !Tebal
            Catatan.Text = IIf(IsDBNull(RSL.Rows(0) !notes), "", RSL.Rows(0) !notes)
            TambSP.Text = IIf(IsDBNull(RSL.Rows(0) !tamb_SP), "", RSL.Rows(0) !tamb_SP)
            If RSL.Rows(0) !inisiatif = "" Then
                cmbInisiatif.SelectedIndex = -1
            Else
                For i = 0 To cmbInisiatif.Items.Count - 1
                    cmbInisiatif.SelectedIndex = i
                    If Trim(cmbInisiatif.Text) = Trim(RSL.Rows(0) !inisiatif) Then
                        ada = True
                        Exit For
                    End If
                Next i
                If Not ada Then cmbInisiatif.SelectedIndex = -1
            End If
            chkContoh.Checked = RSL.Rows(0) !contoh
            Kode_Bahan.Text = RSL.Rows(0) !Kode_Bahan
            Bahan.Text = RSL.Rows(0) !bahan_ind
            Kode_Fungsi.Text = RSL.Rows(0) !Fungsi
            Fungsi.Text = RSL.Rows(0) !fungsi_ind
            Cur_USD.Text = Format(RSL.Rows(0) !Cur_USD, "###,##0.00")
            DCur_USD.Value = RSL.Rows(0) !DCur_USD
            Prev_USD.Text = Format(RSL.Rows(0) !Prev_USD, "###,##0.00")
            dprev_usd.Value = RSL.Rows(0) !dprev_usd

            cur_Euro.Text = IIf(IsDBNull(RSL.Rows(0) !Cur_euro), 0, Format(RSL.Rows(0) !Cur_euro, "###,##0.00"))
            DCur_Euro.Value = IIf(IsDBNull(RSL.Rows(0) !DCur_euro), "01-01-1900", RSL.Rows(0) !DCur_euro)
            Prev_Euro.Text = IIf(IsDBNull(RSL.Rows(0) !Prev_euro), 0, Format(RSL.Rows(0) !Prev_euro, "###,##0.00"))
            dprev_Euro.Value = IIf(IsDBNull(RSL.Rows(0) !dprev_euro), "01-01-1900", RSL.Rows(0) !dprev_euro)

            cur_rp.Text = Format(RSL.Rows(0) !cur_rp, "###,##0")
            DCur_RP.Value = RSL.Rows(0) !DCur_RP
            Prev_RP.Text = Format(RSL.Rows(0) !Prev_RP, "###,##0")
            DPrev_RP.Value = RSL.Rows(0) !DPrev_RP
            Kapasitas.Text = RSL.Rows(0) !Kapasitas
            HPP_USD.Text = Format(RSL.Rows(0) !HPP_USD, "###,##0.00")
            Edit.Text = IIf(IsDBNull(RSL.Rows(0) !usd_edited), "", RSL.Rows(0) !usd_edited)
            PathFoto.Text = Trim(RSL.Rows(0) !FotoLoc)
            LocGmb1.Text = RSL.Rows(0) !File_Foto
            ShowFoto(RSL.Rows(0) !File_Foto)
            IsiVariasi(KodeProduk.Text)
            RiwayatHarga()
        Else
            ClearTextBoxes()
        End If
        Proses.CloseConn()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LocGmb1_MouseLeave(sender As Object, e As EventArgs) Handles LocGmb1.MouseLeave
        CType(sender, Label).ForeColor = Color.FromKnownColor(KnownColor.ControlText)
        LocGmb1.Font = New Font(LocGmb1.Font, FontStyle.Regular)
    End Sub

    Private Sub LocGmb1_MouseEnter(sender As Object, e As EventArgs) Handles LocGmb1.MouseEnter
        CType(sender, Label).ForeColor = Color.DarkSeaGreen
        LocGmb1.Font = New Font(LocGmb1.Font, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline)
    End Sub

    Private Sub Kode_Perajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Perajin.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_KodePerajin " &
              " Where KodePerajin = '" & Kode_Perajin.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Perajin.Text = dbTable.Rows(0) !nama
                Kode_Fungsi.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodePerajin " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodePerajin Like '%" & Kode_Perajin.Text & "%' or nama Like '%" & Kode_Perajin.Text & "%') " &
                    "Order By nama "
                Form_Daftar.Text = "Daftar Perajin"
                Form_Daftar.ShowDialog()

                Kode_Perajin.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nama From m_KodePerajin " &
                   " Where KodePerajin = '" & Kode_Perajin.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Perajin.Text = dbTable.Rows(0) !nama
                    Kode_Fungsi.Focus()
                Else
                    Kode_Perajin.Text = ""
                    Perajin.Text = ""
                    Kode_Perajin.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Satuan_TextChanged(sender As Object, e As EventArgs) Handles Satuan.TextChanged

    End Sub

    Private Sub Kode_Fungsi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Fungsi.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select namaIndonesia From m_KodeFungsi " &
              " Where KodeFungsi = '" & Kode_Fungsi.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Fungsi.Text = dbTable.Rows(0) !namaIndonesia
                Kode_Bahan.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodeFungsi " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodeFungsi Like '%" & Kode_Perajin.Text & "%' or namaIndonesia Like '%" & Kode_Fungsi.Text & "%') " &
                    "Order By namaIndonesia "
                Form_Daftar.Text = "Daftar Kelompok (Fungsi)"
                Form_Daftar.ShowDialog()

                Kode_Fungsi.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select namaIndonesia From m_KodeFungsi " &
                   " Where KodeFungsi = '" & Kode_Fungsi.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Fungsi.Text = dbTable.Rows(0) !namaIndonesia
                    Kode_Bahan.Focus()
                Else
                    Kode_Fungsi.Text = ""
                    Fungsi.Text = ""
                    Kode_Fungsi.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Kode_Bahan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Bahan.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select namaIndonesia From m_KodeBahan " &
              " Where KodeBahan = '" & Kode_Bahan.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Bahan.Text = dbTable.Rows(0) !namaIndonesia
                If LAdd Or LEdit Then
                    BuatKodeProdukBaru()
                End If
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_Kodebahan " &
                    "Where AktifYN = 'Y' " &
                    "  And ( Kodebahan Like '%" & Kode_Bahan.Text & "%' or namaIndonesia Like '%" & Kode_Bahan.Text & "%') " &
                    "Order By namaIndonesia "
                Form_Daftar.Text = "Daftar Bahan"
                Form_Daftar.ShowDialog()

                Kode_Bahan.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select namaIndonesia From m_Kodebahan " &
                   " Where Kodebahan = '" & Kode_Bahan.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Bahan.Text = dbTable.Rows(0) !namaIndonesia
                    If LAdd Or LEdit Then
                        BuatKodeProdukBaru()
                    End If
                Else
                    Kode_Bahan.Text = ""
                    Bahan.Text = ""
                    Kode_Fungsi.Text = ""
                    KodeProduk.Text = ""
                    Kode_Bahan.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub BuatKodeProdukBaru()
        If LAdd Or LEdit Then
            If Trim(Kode_Bahan.Text) = "" Or Trim(Bahan.Text) = "" Then
                MsgBox("Kode Bahan Belum DI ISI", vbCritical + vbOKOnly, "..:Warning !")
                Kode_Bahan.Focus()
                Exit Sub
            ElseIf Trim(Kode_Bahan.Text) <> "" Or Trim(Bahan.Text) <> "" Then
                If Trim(Kode_Perajin.Text) = "" Then
                    MsgBox("Kode Perajin tidak boleh kosong!", vbCritical, ".:ERROR!")
                    Kode_Perajin.Focus()
                    Exit Sub
                End If
                If Trim(Kode_Fungsi.Text) = "" Then
                    MsgBox("Kode Fungsi (Kelompok) tidak boleh kosong!", vbCritical, ".:ERROR!")
                    Kode_Fungsi.Focus()
                    Exit Sub
                End If
                If Trim(Kode_Bahan.Text) = "" Then
                    MsgBox("Kode Bahan tidak boleh kosong!", vbCritical, ".:ERROR!")
                    Kode_Bahan.Focus()
                    Exit Sub
                End If
                KodeProduk.Text = Trim(Kode_Perajin.Text) + "-" +
                    Trim(Kode_Bahan.Text) + "-" +
                    Trim(Kode_Fungsi.Text) +
                    MaxKodeProduk(Kode_Perajin.Text + "-" + Kode_Bahan.Text + "-" + Kode_Fungsi.Text)
                KodePerajin2.Focus()
            End If
        End If
    End Sub

    Private Function MaxKodeProduk(tKode) As String
        Dim MsgSQL As String, rsc As New DataTable,
            RMax As String = ""
        MsgSQL = "Select KodeProduk " &
            " From M_KodeProduk " &
            "Where left(kodeproduk, 10) = '" & tKode & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)

        If rsc.Rows.Count = 0 Then
            MaxKodeProduk = "000"
        Else
            MsgSQL = "Select isnull(max(subString(kodeproduk,11,3)),0) + 1001 IDRec " &
                " From M_KodeProduk " &
                "Where left(kodeproduk, 10) = '" & tKode & "' "
            RMax = Proses.ExecuteSingleStrQuery(MsgSQL)
            MaxKodeProduk = Microsoft.VisualBasic.Right(RMax, 3)
        End If
    End Function

    Private Sub KodePerajin2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodePerajin2.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Descript.Focus()
        End If
    End Sub

    Private Sub Descript_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Descript.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Deskripsi.Focus()
        End If
    End Sub

    Private Sub Panjang_TextChanged(sender As Object, e As EventArgs) Handles Panjang.TextChanged
        If Trim(Panjang.Text) = "" Then Panjang.Text = 0
        If IsNumeric(Panjang.Text) Then
            Dim temp As Double = Panjang.Text
            Panjang.SelectionStart = Panjang.TextLength
        Else
            Panjang.Text = 0
        End If
    End Sub

    Private Sub Deskripsi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Deskripsi.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Satuan.Focus()
        End If
    End Sub

    Private Sub Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Satuan.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Lebar.Focus()
        End If
    End Sub

    Private Sub Tinggi_TextChanged(sender As Object, e As EventArgs) Handles Tinggi.TextChanged
        If Trim(Tinggi.Text) = "" Then Tinggi.Text = 0
        If IsNumeric(Tinggi.Text) Then
            Dim temp As Double = Tinggi.Text
            Tinggi.SelectionStart = Tinggi.TextLength
        Else
            Tinggi.Text = 0
        End If
    End Sub

    Private Sub Lebar_TextChanged(sender As Object, e As EventArgs) Handles Lebar.TextChanged
        If Trim(Lebar.Text) = "" Then Lebar.Text = 0
        If IsNumeric(Lebar.Text) Then
            Dim temp As Double = Lebar.Text
            Lebar.SelectionStart = Lebar.TextLength
        Else
            Lebar.Text = 0
        End If
    End Sub
    Private Sub Lebar_GotFocus(sender As Object, e As EventArgs) Handles Lebar.GotFocus
        With Lebar
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Berat_TextChanged(sender As Object, e As EventArgs) Handles Berat.TextChanged
        If Trim(Berat.Text) = "" Then Berat.Text = 0
        If IsNumeric(Berat.Text) Then
            Dim temp As Double = Berat.Text
            Berat.SelectionStart = Berat.TextLength
        Else
            Berat.Text = 0
        End If
    End Sub

    Private Sub Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lebar.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Lebar.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Lebar.Text) Then
                Dim temp As Double = Lebar.Text
                Lebar.Text = Format(temp, "###,##0.00")
                Lebar.SelectionStart = Lebar.TextLength
            Else
                Lebar.Text = 0
            End If
            If LAdd Or LEdit Then Diameter.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub Diameter_GotFocus(sender As Object, e As EventArgs) Handles Lebar.GotFocus
        With Diameter
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Tebal_TextChanged(sender As Object, e As EventArgs) Handles Tebal.TextChanged
        If Trim(Tebal.Text) = "" Then Tebal.Text = 0
        If IsNumeric(Tebal.Text) Then
            Dim temp As Double = Tebal.Text
            Tebal.SelectionStart = Tebal.TextLength
        Else
            Tebal.Text = 0
        End If
    End Sub

    Private Sub Diameter_TextChanged(sender As Object, e As EventArgs) Handles Diameter.TextChanged
        If Trim(Diameter.Text) = "" Then Diameter.Text = 0
        If IsNumeric(Diameter.Text) Then
            Dim temp As Double = Diameter.Text
            Diameter.SelectionStart = Diameter.TextLength
        Else
            Diameter.Text = 0
        End If
    End Sub
        Private Sub Diameter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Diameter.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Diameter.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Diameter.Text) Then
                Dim temp As Double = Diameter.Text
                Diameter.Text = Format(temp, "###,##0.00")
                Diameter.SelectionStart = Diameter.TextLength
            Else
                Diameter.Text = 0
            End If
            If LAdd Or LEdit Then Panjang.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Cur_USD_TextChanged(sender As Object, e As EventArgs) Handles Cur_USD.TextChanged
        If Trim(Cur_USD.Text) = "" Then Cur_USD.Text = 0
        If IsNumeric(Cur_USD.Text) Then
            Dim temp As Double = Cur_USD.Text
            Cur_USD.SelectionStart = Cur_USD.TextLength
        Else
            Cur_USD.Text = 0
        End If
    End Sub

    Private Sub Panjang_GotFocus(sender As Object, e As EventArgs) Handles Panjang.GotFocus
        With Panjang
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Panjang.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Panjang.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Panjang.Text) Then
                Dim temp As Double = Panjang.Text
                Panjang.Text = Format(temp, "###,##0.00")
                Panjang.SelectionStart = Panjang.TextLength
            Else
                Panjang.Text = 0
            End If
            If LAdd Or LEdit Then Tinggi.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub



    Private Sub cur_rp_TextChanged(sender As Object, e As EventArgs) Handles cur_rp.TextChanged
        If Trim(cur_rp.Text) = "" Then cur_rp.Text = 0
        If IsNumeric(cur_rp.Text) Then
            Dim temp As Double = cur_rp.Text
            cur_rp.Text = Format(temp, "###,##0")
            cur_rp.SelectionStart = cur_rp.TextLength
        Else
            cur_rp.Text = 0
        End If
    End Sub

    Private Sub Prev_RP_TextChanged(sender As Object, e As EventArgs) Handles Prev_RP.TextChanged
        If Trim(Prev_RP.Text) = "" Then Prev_RP.Text = 0
        If IsNumeric(Prev_RP.Text) Then
            Dim temp As Double = Prev_RP.Text
            Prev_RP.Text = Format(temp, "###,##0")
            Prev_RP.SelectionStart = Prev_RP.TextLength
        Else
            Prev_RP.Text = 0
        End If
    End Sub


    Private Sub Tinggi_GotFocus(sender As Object, e As EventArgs) Handles Tinggi.GotFocus
        With Tinggi
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tinggi.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Tinggi.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Tinggi.Text) Then
                Dim temp As Double = Tinggi.Text
                Tinggi.Text = Format(temp, "###,##0.00")
                Tinggi.SelectionStart = Tinggi.TextLength
            Else
                Tinggi.Text = 0
            End If
            If LAdd Or LEdit Then Berat.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Berat_GotFocus(sender As Object, e As EventArgs) Handles Berat.GotFocus
        With Berat
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Berat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Berat.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Berat.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Berat.Text) Then
                Dim temp As Double = Berat.Text
                Berat.Text = Format(temp, "###,##0.00")
                Berat.SelectionStart = Berat.TextLength
            Else
                Berat.Text = 0
            End If
            If LAdd Or LEdit Then Tebal.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Catatan_TextChanged(sender As Object, e As EventArgs) Handles Catatan.TextChanged

    End Sub

    Private Sub Tebal_GotFocus(sender As Object, e As EventArgs) Handles Tebal.GotFocus
        With Tebal
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Kapasitas_TextChanged(sender As Object, e As EventArgs) Handles Kapasitas.TextChanged
        If Trim(Kapasitas.Text) = "" Then Kapasitas.Text = 0
        If IsNumeric(Kapasitas.Text) Then
            Dim temp As Double = Kapasitas.Text
            Kapasitas.SelectionStart = Kapasitas.TextLength
        Else
            Kapasitas.Text = 0
        End If
    End Sub

    Private Sub Tebal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tebal.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Tebal.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Tebal.Text) Then
                Dim temp As Double = Tebal.Text
                Tebal.Text = Format(temp, "###,##0.00")
                Tebal.SelectionStart = Tebal.TextLength
            Else
                Tebal.Text = 0
            End If
            If LAdd Or LEdit Then Cur_USD.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub PathFoto_TextChanged(sender As Object, e As EventArgs) Handles PathFoto.TextChanged

    End Sub

    Private Sub Cur_USD_GotFocus(sender As Object, e As EventArgs) Handles Cur_USD.GotFocus
        With Cur_USD
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Prev_USD_TextChanged(sender As Object, e As EventArgs) Handles Prev_USD.TextChanged
        If Trim(Prev_USD.Text) = "" Then Prev_USD.Text = 0
        If IsNumeric(Prev_USD.Text) Then
            Dim temp As Double = Prev_USD.Text
            Prev_USD.SelectionStart = Prev_USD.TextLength
        Else
            Prev_USD.Text = 0
        End If
    End Sub

    Private Sub HPP_USD_TextChanged(sender As Object, e As EventArgs) Handles HPP_USD.TextChanged
        If Trim(HPP_USD.Text) = "" Then HPP_USD.Text = 0
        If IsNumeric(HPP_USD.Text) Then
            Dim temp As Double = HPP_USD.Text
            HPP_USD.SelectionStart = HPP_USD.TextLength
        Else
            HPP_USD.Text = 0
        End If
    End Sub

    Private Sub Cur_USD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cur_USD.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Cur_USD.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Cur_USD.Text) Then
                Dim temp As Double = Cur_USD.Text
                Cur_USD.Text = Format(temp, "###,##0.00")
                Cur_USD.SelectionStart = Cur_USD.TextLength
            Else
                Cur_USD.Text = 0
            End If
            If LAdd Or LEdit Then Prev_USD.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub cur_rp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cur_rp.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If cur_rp.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(cur_rp.Text) Then
                Dim temp As Double = cur_rp.Text
                cur_rp.Text = Format(temp, "###,##0")
                cur_rp.SelectionStart = cur_rp.TextLength
            Else
                cur_rp.Text = 0
            End If
            If LAdd Or LEdit Then Prev_RP.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub


    Private Sub Catatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Catatan.KeyPress
        If e.KeyChar = Chr(13) Then
            Kapasitas.Focus()
        End If
    End Sub

    Private Sub Kapasitas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kapasitas.KeyPress

        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Kapasitas.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Kapasitas.Text) Then
                Dim temp As Double = Kapasitas.Text
                Kapasitas.Text = Format(temp, "###,##0.00")
                Kapasitas.SelectionStart = Kapasitas.TextLength
            Else
                Kapasitas.Text = 0
            End If
            If LAdd Or LEdit Then PathFoto.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub PathFoto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PathFoto.KeyPress
        If e.KeyChar = Chr(13) Then
            TambSP.Focus()
        End If
    End Sub

    Private Sub TambSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TambSP.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub

    Private Sub Edit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Edit.KeyPress
        If e.KeyChar = Chr(13) Then
            cur_rp.Focus()
        End If
    End Sub

    Private Sub cmbNamaPerajin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNamaPerajin.SelectedIndexChanged
        Daftar()
    End Sub

    Private Sub CmbFungsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFungsi.SelectedIndexChanged
        Daftar()
    End Sub

    Private Sub cmbBahan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBahan.SelectedIndexChanged
        Daftar()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If Trim(KodeProduk.Text) = "" Then
            MsgBox("Data Produk Belum di pilih!", vbCritical, ".:ERROR!")
            DGView.Focus()
        End If
        If MsgBox("Yakin hapus data " & Trim(KodeProduk.Text) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            'SQL = "Update m_KodeProduk Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate() " &
            '    "Where KodeProduk  = '" & KodeProduk.Text & "' "

            SQL = "DELETE m_KodeProduk WHERE KodeProduk  = '" & KodeProduk.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            ClearTextBoxes()
            Daftar()
        End If

    End Sub

    Private Sub TDeskripsi_TextChanged(sender As Object, e As EventArgs) Handles TDeskripsi.TextChanged

    End Sub

    Private Sub Prev_RP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Prev_RP.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Prev_RP.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Prev_RP.Text) Then
                Dim temp As Double = Prev_RP.Text
                Prev_RP.Text = Format(temp, "###,##0")
                Prev_RP.SelectionStart = Prev_RP.TextLength
            Else
                Prev_RP.Text = 0
            End If
            If LAdd Or LEdit Then HPP_USD.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub HPP_USD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HPP_USD.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HPP_USD.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(HPP_USD.Text) Then
                Dim temp As Double = HPP_USD.Text
                HPP_USD.Text = Format(temp, "###,##0.00")
                HPP_USD.SelectionStart = HPP_USD.TextLength
            Else
                HPP_USD.Text = 0
            End If
            If LAdd Or LEdit Then Catatan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub HPP_USD_GotFocus(sender As Object, e As EventArgs) Handles HPP_USD.GotFocus
        With HPP_USD
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
    End Sub

    Private Sub KodeVariasi_TextChanged(sender As Object, e As EventArgs) Handles KodeVariasi.TextChanged
        If Len(KodeVariasi.Text) < 1 Then
            KodeVariasi.Text = ""
        ElseIf Len(KodeVariasi.Text) = 5 Then
            KodeVariasi.Text = KodeVariasi.Text + "-"
            KodeVariasi.SelectionStart = Len(Trim(KodeVariasi.Text)) + 1
        ElseIf Len(KodeVariasi.Text) = 8 Then
            KodeVariasi.Text = KodeVariasi.Text + "-"
            KodeVariasi.SelectionStart = Len(Trim(KodeVariasi.Text)) + 1
        End If

    End Sub

    Private Sub Prev_USD_GotFocus(sender As Object, e As EventArgs) Handles Prev_USD.GotFocus
        With Prev_USD
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
    End Sub

    Private Sub cmdBatalVariasi_Click(sender As Object, e As EventArgs) Handles cmdBatalVariasi.Click
        LMultiKode = False
        LAdd = False
        LEdit = False
        lVariasi = False
        AturTombol(True)
        TabPageFormEntry_.Enabled = True
        TabPageDaftar_.Enabled = True
        TabControl1.SelectedTab = TabPageFormEntry_
    End Sub

    Private Sub cmdSimpanVariasi_Click(sender As Object, e As EventArgs) Handles cmdSimpanVariasi.Click
        Dim MsgSQL As String, RSF As New DataTable
        Dim KdVar As String = "", RSL As New DataTable
        Dim KProduk As String = "", i As Integer

        Dim tBeliRP As Double, HBeliRP As Double
        Dim tBeliSebelumRP As Double, HBeliSebelumRP As Double
        Dim tBeliUS As Double, HBeliUS As Double
        Dim tJualUS As Double, HJualUS As Double
        Dim tJualSebelumUS As Double, HJualSebelumUS As Double

        TabPageFormEntry_.Enabled = True
        TabControl1.SelectedTab = TabPageFormEntry_

        If lstVwMultiKode.Rows.Count = 0 Then
            MsgBox("Kode Variasi Belum Lengkap!", vbOKOnly + vbCritical, ".:ERROR!")
            KodeVariasi.Focus()
            Exit Sub
        Else
            KProduk = lstVwMultiKode.Rows(0).Cells(0).Value
        End If

        MsgSQL = "Select KodeProduk From M_KodeProduk " &
            "Where KodeProduk like '" & Microsoft.VisualBasic.Left(Trim(KProduk), 12) & "%' " &
            "order by kodeproduk desc "
        RSF = Proses.ExecuteQuery(MsgSQL) '.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        If RSF.Rows.Count <> 0 Then
            If Len(RSF.Rows(0) !KodeProduk) = 12 Then
                KdVar = "A"
            Else
                KdVar = Chr(Asc(Microsoft.VisualBasic.Right(RSF.Rows(0) !KodeProduk, 1)) + 1)
            End If
        End If

        KodeProduk.Text = Microsoft.VisualBasic.Left(Trim(KProduk), 12) + Trim(KdVar)

        tBeliRP = 0
        tBeliSebelumRP = 0
        tBeliUS = 0
        tJualUS = 0
        tJualSebelumUS = 0
        'lstVwMultiKode.Rows(i).Cells(0).Value
        For i = 0 To lstVwMultiKode.Rows.Count - 1

            KProduk = Trim(lstVwMultiKode.Rows(i).Cells(0).Value)
            HBeliRP = Trim(lstVwMultiKode.Rows(i).Cells(2).Value)
            HBeliSebelumRP = Trim(lstVwMultiKode.Rows(i).Cells(3).Value)
            HBeliUS = Trim(lstVwMultiKode.Rows(i).Cells(4).Value)
            HJualUS = Trim(lstVwMultiKode.Rows(i).Cells(5).Value)
            HJualSebelumUS = Trim(lstVwMultiKode.Rows(i).Cells(6).Value)

            tBeliRP = tBeliRP + HBeliRP
            tBeliSebelumRP = tBeliSebelumRP + HBeliSebelumRP
            tBeliUS = tBeliUS + HBeliUS
            tJualUS = tJualUS + HJualUS
            tJualSebelumUS = tJualSebelumUS + HJualSebelumUS

            MsgSQL = "Select * from m_KodeProduk " &
                "Where KodeProduk = '" & KProduk & "' "
            RSL = Proses.ExecuteQuery(MsgSQL)
            If RSL.Rows.Count <> 0 Then
                If i = 0 Then
                    Kode_Perajin.Text = RSL.Rows(i) !Kode_Perajin
                    Perajin.Text = RSL.Rows(i) !Perajin
                    Descript.Text = Trim(RSL.Rows(i) !Descript)
                    Deskripsi.Text = Trim(RSL.Rows(i) !Deskripsi)
                    Satuan.Text = RSL.Rows(i) !Satuan
                    LocGmb1.Text = RSL.Rows(i) !File_Foto
                    ShowFoto(RSL.Rows(i) !File_Foto)
                    tglMasuk.Value = RSL.Rows(i) !Tanggal
                    Panjang.Text = 0 'RSL!Panjang
                    Lebar.Text = 0 'RSL!Lebar
                    Tinggi.Text = 0 'RSL!Tinggi
                    Diameter.Text = 0 'RSL!Diameter
                    Berat.Text = 0 'RSL!Berat
                    Tebal.Text = 0 'RSL!Tebal
                    Catatan.Text = IIf(IsDBNull(RSL.Rows(i) !notes), "", RSL.Rows(i) !notes)
                    If RSL.Rows(i) !contoh Then
                        chkContoh.Checked = 1
                    Else
                        chkContoh.Checked = 0
                    End If
                    Kode_Bahan.Text = RSL.Rows(i) !Kode_Bahan
                    Bahan.Text = RSL.Rows(i) !bahan_ind
                    Kode_Fungsi.Text = RSL.Rows(i) !Fungsi
                    Fungsi.Text = RSL.Rows(i) !fungsi_ind
                    'Cur_USD.Text = (Cur_USD.Text * 1) + RSL!Cur_USD
                    DCur_USD.Value = RSL.Rows(i) !DCur_USD
                    'Prev_USD.Text = RSL!Prev_USD
                    dprev_usd.Value = RSL.Rows(i) !dprev_usd
                    'cur_rp.Text = (cur_rp.Text * 1) + RSL!cur_rp
                    DCur_RP.Value = RSL.Rows(i) !DCur_RP
                    'Prev_RP.Text = RSL!Prev_RP
                    DPrev_RP.Value = RSL.Rows(i) !DPrev_RP
                    Kapasitas.Text = RSL.Rows(i) !Kapasitas
                    KodePerajin2.Text = IIf(IsDBNull(RSL.Rows(i) !KodePerajin2), "", RSL.Rows(i) !KodePerajin2)
                    'HPP_USD.Text = RSL!HPP_USD
                    TambSP.Text = KProduk + " " + RSL.Rows(i) !tamb_SP +
                        "# Sat : " + RSL.Rows(i) !Satuan +
                        "; Lbr " + Format(RSL.Rows(i) !Lebar, "###,##0") +
                        "; Dia " + Format(RSL.Rows(i) !Diameter, "###,##0") +
                        "; Pj " + Format(RSL.Rows(i) !Panjang, "###,##0") +
                        "; Tg " + Format(RSL.Rows(i) !Tinggi, "###,##0") +
                        "; Brt " + Format(RSL.Rows(i) !Berat, "###,##0") +
                        "; Tbl " + Format(RSL.Rows(i) !Tebal, "###,##0")
                Else
                    TambSP.Text = TambSP.Text + vbNewLine +
                        KProduk + " " + Trim(IIf(IsDBNull(RSL.Rows(0) !tamb_SP), "", RSL.Rows(0) !tamb_SP)) +
                        "# Sat : " + IIf(IsDBNull(RSL.Rows(0) !Satuan), "", RSL.Rows(0) !Satuan) +
                        "; Lbr " + Format(RSL.Rows(0) !Lebar, "###,##0") +
                        "; Dia " + Format(RSL.Rows(0) !Diameter, "###,##0") +
                        "; Pj " + Format(RSL.Rows(0) !Panjang, "###,##0") +
                        "; Tg " + Format(RSL.Rows(0) !Tinggi, "###,##0") +
                        "; Brt " + Format(RSL.Rows(0) !Berat, "###,##0") +
                        "; Tbl " + Format(RSL.Rows(0) !Tebal, "###,##0")
                    Descript.Text = Trim(Descript.Text) + vbNewLine +
                                    KProduk + " " + Trim(RSL.Rows(0) !Descript)
                    Deskripsi.Text = Trim(Deskripsi.Text) + vbNewLine +
                                     KProduk + " " + Trim(RSL.Rows(0) !Deskripsi)
                End If
            End If
        Next i
        cur_rp.Text = tBeliRP
        Prev_RP.Text = tBeliSebelumRP
        HPP_USD.Text = tBeliUS
        Cur_USD.Text = tJualUS
        Prev_USD.Text = tJualSebelumUS
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
        SQL = "SELECT m_KodeProduk.KodeProduk, m_KodeProduk.deskripsi, " &
            "m_KodeProduk.bahan_ind, m_KodeProduk.fungsi_ind, m_KodeProduk.cur_usd, " &
            "m_KodeProduk.cur_rp, m_KodeProduk.kapasitas " &
            "FROM Pekerti.dbo.m_KodeProduk m_KodeProduk " &
            "Where AktifYN = 'Y' " &
            "  And Fungsi <> '' " &
            "ORDER BY m_KodeProduk.fungsi_ind ASC, m_KodeProduk.bahan_ind ASC "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_KodifProduk
            objRep.SetDataSource(dttable)
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

    Private Sub lstVwMultiKode_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lstVwMultiKode.CellContentClick

    End Sub

    Private Sub Prev_USD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Prev_USD.KeyPress

        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Prev_USD.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Prev_USD.Text) Then
                Dim temp As Double = Prev_USD.Text
                Prev_USD.Text = Format(temp, "###,##0.00")
                Prev_USD.SelectionStart = Prev_USD.TextLength
            Else
                Prev_USD.Text = 0
            End If
            If LAdd Or LEdit Then cur_Euro.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If

    End Sub

    Private Sub cmdVariasiMultiKode_Click(sender As Object, e As EventArgs) Handles cmdVariasiMultiKode.Click
        If Trim(KodeProduk.Text) = "" Then
            MsgBox("Kode Produk yang mau di buat Variasi MULTI Kode belum di pilih!", vbCritical, ".:Error!")
            cmdCari.Focus()
            Exit Sub
        End If
        LMultiKode = True
        LAdd = False
        LEdit = False
        lVariasi = True
        AturTombol(False)
        TabPageFormEntry_.Enabled = False
        TabPageDaftar_.Enabled = False
        TabControl1.SelectedTab = TabPageVariasi_
        lstVwMultiKode.Rows.Clear()
        lstVwMultiKode.Rows.Add(
            KodeProduk.Text,
            Deskripsi.Text,
            cur_rp.Text,
            Prev_RP.Text,
            HPP_USD.Text,
            Cur_USD.Text,
            Prev_USD.Text)
        KodeVariasi.Enabled = True
        NamaVariasi.Enabled = True
        KodeVariasi.Focus()
        tdPrev_USD = DCur_USD.Value
        tPrev_USD = Cur_USD.Text * 1
        tdPrev_RP = DCur_RP.Value
        TPrev_RP = cur_rp.Text * 1
    End Sub

    Private Sub cur_Euro_TextChanged(sender As Object, e As EventArgs) Handles cur_Euro.TextChanged
        If Trim(cur_Euro.Text) = "" Then cur_Euro.Text = 0
        If IsNumeric(cur_Euro.Text) Then
            Dim temp As Double = cur_Euro.Text
            cur_Euro.SelectionStart = cur_Euro.TextLength
        Else
            cur_Euro.Text = 0
        End If
    End Sub

    Private Sub cmdVariasi_Click(sender As Object, e As EventArgs) Handles cmdVariasi.Click
        Dim MsgSQL As String, RSF As New DataTable, KdVar As String = ""
        Dim Judul As String
        If Trim(KodeProduk.Text) = "" Then
            MsgBox("Kode Produk yang mau di buat variasi belum di pilih !", vbCritical + vbOKOnly, ".:Error!")
            cmdCari.Focus()
            Exit Sub
        End If
        Judul = Me.Text
        LEdit = False
        LAdd = True
        lVariasi = True
        LMultiKode = False
        AturTombol(False)
        Me.Text = Judul + " ~ BUAT VARIASI KODE " & Trim(KodeProduk.Text)

        MsgSQL = "Select KodeProduk From M_KodeProduk " &
            "Where KodeProduk like '" & Trim(Microsoft.VisualBasic.Left(KodeProduk.Text, 13)) & "%' " &
            "order by kodeproduk desc "
        RSF = Proses.ExecuteQuery(MsgSQL)
        If RSF.Rows.Count <> 0 Then
            If Len(RSF.Rows(0) !KodeProduk) = 13 Then
                KdVar = "A"
            Else
                KdVar = Chr(Asc(Microsoft.VisualBasic.Right(RSF.Rows(0) !KodeProduk, 1)) + 1)
            End If
        End If
        KodeProduk.Text = Microsoft.VisualBasic.Left(KodeProduk.Text, 13) + Trim(KdVar)
        tdPrev_USD = DCur_USD.Value
        tPrev_USD = Cur_USD.Text * 1
        tdPrev_RP = DCur_RP.Value
        tPrev_RP = cur_rp.Text * 1
        Proses.CloseConn()
    End Sub

    Private Sub Prev_Euro_TextChanged(sender As Object, e As EventArgs) Handles Prev_Euro.TextChanged
        If Trim(Prev_Euro.Text) = "" Then Prev_USD.Text = 0
        If IsNumeric(Prev_Euro.Text) Then
            Dim temp As Double = Prev_Euro.Text
            Prev_Euro.SelectionStart = Prev_Euro.TextLength
        Else
            Prev_Euro.Text = 0
        End If
    End Sub

    Private Sub Kapasitas_GotFocus(sender As Object, e As EventArgs) Handles Kapasitas.GotFocus
        With Kapasitas
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With

    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(KodeProduk.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        Else
            IsiKodeProduk(KodeProduk.Text)
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
        TabPageDaftar_.Enabled = False
        TabPageVariasi_.Enabled = False

        tdPrev_USD = DCur_USD.Value
        tPrev_USD = Cur_USD.Text * 1
        tdPrev_RP = DCur_RP.Value
        tPrev_RP = cur_rp.Text * 1
        Kode_Perajin.Focus()

    End Sub

    Private Sub Descript_TextChanged(sender As Object, e As EventArgs) Handles Descript.TextChanged

    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged

    End Sub

    Private Sub Deskripsi_TextChanged(sender As Object, e As EventArgs) Handles Deskripsi.TextChanged

    End Sub

    Private Sub TKodeProduk_TextChanged(sender As Object, e As EventArgs) Handles TKodeProduk.TextChanged

    End Sub

    Private Sub Daftar()
        Dim mKondisi As String = "", RSD As New DataTable
        Dim MsgSQL As String, a As Integer
        If Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) = "" And Trim(TDeskripsi.Text) = "" Then
            mKondisi = ""

        ElseIf Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) <> "" Then
            mKondisi = "And kode_bahan = '" & Trim(Microsoft.VisualBasic.Right(cmbBahan.Text, 10)) & "' "
        ElseIf Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) = "" Then
            mKondisi = "And fungsi = '" & Trim(Microsoft.VisualBasic.Right(CmbFungsi.Text, 10)) & "' "
        ElseIf Trim(cmbNamaPerajin.Text) = "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) <> "" Then
            mKondisi = "And fungsi = '" & Trim(Microsoft.VisualBasic.Right(CmbFungsi.Text, 10)) & "' " &
            "And kode_bahan = '" & Trim(Microsoft.VisualBasic.Right(cmbBahan.Text, 10)) & "' "
        ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) = "" Then
            mKondisi = " AND Kode_Perajin ='" & Trim(Microsoft.VisualBasic.Right(cmbNamaPerajin.Text, 10)) & "' "
        ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) = "" And Trim(cmbBahan.Text) <> "" Then
            mKondisi = " AND Kode_Perajin ='" & Trim(Microsoft.VisualBasic.Right(cmbNamaPerajin.Text, 10)) & "' " &
            "And kode_bahan = '" & Trim(Microsoft.VisualBasic.Right(cmbBahan.Text, 10)) & "' "
        ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) = "" Then
            mKondisi = " AND Kode_Perajin ='" & Trim(Microsoft.VisualBasic.Right(cmbNamaPerajin.Text, 10)) & "' " &
            "And fungsi = '" & Trim(Microsoft.VisualBasic.Right(CmbFungsi.Text, 10)) & "' "
        ElseIf Trim(cmbNamaPerajin.Text) <> "" And Trim(CmbFungsi.Text) <> "" And Trim(cmbBahan.Text) <> "" Then
            mKondisi = " AND Kode_Perajin ='" & Trim(Microsoft.VisualBasic.Right(cmbNamaPerajin.Text, 10)) & "' " &
            "And fungsi = '" & Trim(Microsoft.VisualBasic.Right(CmbFungsi.Text, 10)) & "' " &
            "And kode_bahan = '" & Trim(Microsoft.VisualBasic.Right(cmbBahan.Text, 10)) & "' "
        ElseIf Trim(TDeskripsi.Text) <> "" Then
            mKondisi = " AND Deskripsi Like '%" & TDeskripsi.Text & "%' "
        End If
        If Trim(TKodeProduk.Text) <> "" Then
            mKondisi += " AND KodePerajin2 Like '%" & TKodeProduk.Text & "%' "
        End If
        PBar1.Visible = True
        DGView.Rows.Clear()
        DGView.Visible = False
        MsgSQL = "Select A.* " &
            " From m_KodeProduk A " &
            " Where A.AktifYN = 'Y' " &
            "   And KodeProduk is Not NULL " &
            "   And Deskripsi is NOT NULL " &
            " " & mKondisi & " " &
            " Order By KodeProduk "
        RSD = Proses.ExecuteQuery(MsgSQL)
        PBar1.Maximum = RSD.Rows.Count + 1
        Me.Cursor = Cursors.WaitCursor
        With RSD.Columns(0)
            For a = 0 To RSD.Rows.Count - 1
                Application.DoEvents()
                PBar1.Value = a
                DGView.Rows.Add(.Table.Rows(a) !KodeProduk,
                    IIf(IsDBNull(.Table.Rows(a) !Kode_Perajin), "", .Table.Rows(a) !Kode_Perajin),
                    .Table.Rows(a) !Deskripsi,
                    IIf(IsDBNull(.Table.Rows(a) !Perajin), "", .Table.Rows(a) !Perajin),
                    .Table.Rows(a) !fungsi_ind,
                    .Table.Rows(a) !bahan_ind,
                     .Table.Rows(a) !KodePerajin2,
                    Format(.Table.Rows(a) !tanggal, "dd-MM-yyyy"),
                    IIf(IsDBNull(.Table.Rows(a) !Descript), "", .Table.Rows(a) !Descript))
            Next (a)
        End With
        PBar1.Value = PBar1.Maximum
        JRec.Text = Format(a, "###,##0")
        Me.Cursor = Cursors.Default
        DGView.Visible = True
        PBar1.Visible = False
        DGView.Focus()
    End Sub

    Private Sub TDeskripsi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TDeskripsi.KeyPress
        If e.KeyChar = Chr(13) Then
            Daftar()
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim tID As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        KodeProduk.Text = tID
        If LEdit Or LAdd Then

        Else
            IsiKodeProduk(KodeProduk.Text)
        End If
    End Sub

    Private Sub DGView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        TabControl1.SelectedTab = TabPageFormEntry_
    End Sub

    Private Sub KodeVariasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeVariasi.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select Deskripsi, Cur_RP, prev_RP, hpp_usd, Cur_USD, Prev_USD " &
                " From m_KodeProduk " &
                "Where KodeProduk = '" & Trim(KodeVariasi.Text) & "' " &
                "  And AktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaVariasi.Text = dbTable.Rows(0) !Deskripsi
                AddItem()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodeProduk " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodeProduk Like '%" & KodeVariasi.Text & "%' or Deskripsi Like '%" & KodeVariasi.Text & "%') " &
                    "Order By Deskripsi "
                Form_Daftar.Text = "Daftar Produk"
                Form_Daftar.ShowDialog()

                KodeVariasi.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select Deskripsi, Cur_RP, prev_RP, hpp_usd, Cur_USD, Prev_USD " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & Trim(KodeVariasi.Text) & "' " &
                    "  And AktifYN = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    NamaVariasi.Text = dbTable.Rows(0) !Deskripsi
                    AddItem()
                Else
                    NamaVariasi.Text = ""
                    KodeVariasi.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub AddItem()
        If LMultiKode Then
            Dim ada As Boolean = False, i As Integer = 0
            Dim dt As New DataTable
            For i = 0 To lstVwMultiKode.Rows.Count - 1
                If Trim(KodeVariasi.Text) = Trim(lstVwMultiKode.Rows(i).Cells(0).Value) Then
                    ada = True
                    Exit For
                End If
            Next
            If ada Then
                MsgBox("Kode Barang " & KodeVariasi.Text & " ~ " & NamaVariasi.Text & " SUDAH ADA di List... ", vbInformation + vbOKOnly, ".:Warning !")
            Else
                SQL = "Select KodeProduk, Deskripsi, Cur_RP, prev_RP, " &
                    "         hpp_usd, Cur_USD, Prev_USD " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & Trim(KodeVariasi.Text) & "' " &
                    "  And AktifYN = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count = 0 Then Exit Sub
                KodeVariasi.Text = dbTable.Rows(0) !KodeProduk
                NamaVariasi.Text = dbTable.Rows(0) !Deskripsi
                lstVwMultiKode.Rows.Add(KodeVariasi.Text,
                    NamaVariasi.Text,
                    Format(dbTable.Rows(0) !cur_rp, "###,##0"),
                    Format(dbTable.Rows(0) !Prev_RP, "###,##0"),
                    Format(dbTable.Rows(0) !HPP_USD, "###,##0.00"),
                    Format(dbTable.Rows(0) !Cur_USD, "###,##0.00"),
                    Format(dbTable.Rows(0) !Prev_USD, "###,##0.00"))
                Proses.CloseConn()
            End If
            KodeVariasi.SelectionStart = Len(Trim(KodeVariasi.Text)) + 1
        End If
    End Sub

    Private Sub lstVwMultiKode_KeyDown(sender As Object, e As KeyEventArgs) Handles lstVwMultiKode.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not lstVwMultiKode.CurrentRow.IsNewRow Then
                lstVwMultiKode.Rows.Remove(lstVwMultiKode.CurrentRow)
                SendKeys.Send("{home}")
            Else
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(0).Value = ""
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(1).Value = ""
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(2).Value = ""
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(3).Value = ""
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(4).Value = ""
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(5).Value = ""
                lstVwMultiKode.Rows(lstVwMultiKode.CurrentCell.RowIndex).Cells(6).Value = ""
            End If
        End If
    End Sub

    Private Sub KodeVariasi_GotFocus(sender As Object, e As EventArgs) Handles KodeVariasi.GotFocus
        With KodeVariasi
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub cur_Euro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cur_Euro.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If cur_Euro.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(cur_Euro.Text) Then
                Dim temp As Double = cur_Euro.Text
                cur_Euro.Text = Format(temp, "###,##0.00")
                cur_Euro.SelectionStart = cur_Euro.TextLength
            Else
                cur_Euro.Text = 0
            End If
            If LAdd Or LEdit Then Prev_Euro.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Prev_Euro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Prev_Euro.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Prev_Euro.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Prev_Euro.Text) Then
                Dim temp As Double = Prev_Euro.Text
                Prev_Euro.Text = Format(temp, "###,##0.00")
                Prev_Euro.SelectionStart = Prev_Euro.TextLength
            Else
                Prev_Euro.Text = 0
            End If
            If LAdd Or LEdit Then Edit.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        If TabControl2.SelectedTab Is TabRiwayatHarga_ Then
            RiwayatHarga()
        End If
    End Sub

    Private Sub TKodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TKodeProduk.KeyPress
        If e.KeyChar = Chr(13) Then
            Daftar()
        End If
    End Sub
End Class