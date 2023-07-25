
Imports System.Threading
Imports System.Globalization

Public Class FrmMenuUtama
    Dim Proses As New ClsKoneksi

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        If MessageBox.Show("Apakah benar-benar mau logout ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
            Status.Text = "EXIT"
            Application.Exit()
        End If
    End Sub


    Private Sub FrmMenuUtama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dbTable As New DataTable, SQL As String, tglServer As String
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
        'Me.BackgroundImage = ImageList1.Images(2)
        Status.Text = ""
        ServerName.Text = My.Settings.IPServer
        TsCompany.Text = "IOTA Blessindo"
        TsPengguna.Text = My.Settings.IPServer

        CompCode.Text = "PK"
        TsTanggal.Text = Format(Now, "dddd, dd/MM/yyyy")
        TSKeterangan.Text = ""
        Form_Login.ShowDialog()
        CekTable()
        Dim UserID As String = TsPengguna.Text, dVersion As String = ""
        'mnuBackup.Visible = Proses.UserAksesMenu(UserID, "BACKUP_DB")
        'mnuRestore.Visible = Proses.UserAksesMenu(UserID, "RESTORE_DB")
        _18Utility.Visible = Proses.UserAksesMenu(UserID, "18_UTILITY")
        _81_GANTI_PASSWORD.Visible = Proses.UserAksesMenu(UserID, "81_GANTI_PASSWORD")
        _82_USER_BARU.Visible = Proses.UserAksesMenu(UserID, "82_USER_BARU")
        _83_PENGATURAN_USER.Visible = Proses.UserAksesMenu(UserID, "83_PENGATURAN_USER")
        _84_COMPANY_SETUP.Visible = Proses.UserAksesMenu(UserID, "84_COMPANY_SETUP")

        _8A_BACKUP_DATA.Visible = Proses.UserAksesMenu(UserID, "8A_BACKUP_DB")
        _8B_RESTORE_DATA.Visible = Proses.UserAksesMenu(UserID, "8B_RESTORE_DB")

        _21KodifikasiBahanBaku.Visible = Proses.UserAksesMenu(UserID, "21_KODIF_BAHAN_BAKU")
        _22KodifikasiFungsiProduk.Visible = Proses.UserAksesMenu(UserID, "22_KODIF_FUNGSI_PRODUK")
        _23KodifikasiDaerah.Visible = Proses.UserAksesMenu(UserID, "23_KODIF_DAERAH")
        _24KodifikasiPerajin.Visible = Proses.UserAksesMenu(UserID, "24_KODIF_PERAJIN")
        _25KodifikasiProduk.Visible = Proses.UserAksesMenu(UserID, "25_KODIF_PRODUK")
        _26SPDenganKode.Visible = Proses.UserAksesMenu(UserID, "26_SP_DENGAN_KODE")
        _27DaftarPenerimaanBarang.Visible = Proses.UserAksesMenu(UserID, "27_DPB_CONTOH")
        _28Katalog.Visible = Proses.UserAksesMenu(UserID, "28_KATALOG_CONTOH")

        _31SuratPesanan.Visible = Proses.UserAksesMenu(UserID, "31_SURAT_PESANAN")
        _32DPB.Visible = Proses.UserAksesMenu(UserID, "32_DPB")
        _33PengajuanUangMukaPerajin.Visible = Proses.UserAksesMenu(UserID, "33_UANG_MUKA_PERAJIN")

        _41KodifikasiNegara.Visible = Proses.UserAksesMenu(UserID, "41_KODIF_NEGARA")
        _42KodifikasiImportir.Visible = Proses.UserAksesMenu(UserID, "42_KODIF_IMPORTIR")
        _43KodeProdukBuyer.Visible = Proses.UserAksesMenu(UserID, "43_KODIF_PRODUK_BUYER")
        _44PurchasingOrder.Visible = Proses.UserAksesMenu(UserID, "44_PURCHASE_ORDER")
        _45ProformaInvoice.Visible = Proses.UserAksesMenu(UserID, "45_PROFORMA_INVOICE")

        _51PraLHP.Visible = Proses.UserAksesMenu(UserID, "51_PRA_LHP")
        _52LHP.Visible = Proses.UserAksesMenu(UserID, "52_LHP")
        _53DraftPackingListDPL.Visible = Proses.UserAksesMenu(UserID, "53_DPL")
        _54JenisBox.Visible = Proses.UserAksesMenu(UserID, "54_JENIS_BOX")
        _55PackingListInvoice.Visible = Proses.UserAksesMenu(UserID, "55_PACKING_LIST_INV")
        _56MetodePengiriman.Visible = Proses.UserAksesMenu(UserID, "56_METODE_PENGIRIMAN")
        _57ReturBarang.Visible = Proses.UserAksesMenu(UserID, "57_RETUR")


        If (UCase(UserID) = "EKO_K" Or UCase(UserID) = "ADMIN") Then
            _18Utility.Visible = True
            _81_GANTI_PASSWORD.Visible = True
            _82_USER_BARU.Visible = True
            _83_PENGATURAN_USER.Visible = True
            _84_COMPANY_SETUP.Visible = True
            _8A_BACKUP_DATA.Visible = True
            _8B_RESTORE_DATA.Visible = True
        End If
        SQL = "Select company, kode_toko, convert(varchar(8), " &
            "         getdate(), 112) as tglServer, compcode, ipcloud, version " &
            "From M_Company "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            TsCompany.Text = dbTable.Rows(0) !company
            Kode_Toko.Text = dbTable.Rows(0) !kode_toko
            tglServer = dbTable.Rows(0) !tglserver
            CompCode.Text = dbTable.Rows(0) !compcode
            dVersion = dbTable.Rows(0) !version
            SQL = "Select * " &
                " From M_Toko " &
                "Where IdRec = '" & Kode_Toko.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Nama_Toko.Text = dbTable.Rows(0) !nama
            End If
            If tglServer <> Format(Now, "yyyyMMdd") Then
                MsgBox("Tgl Server beda dengan tgl komputer ini !", vbCritical + vbOKOnly, ".:Warning!")
                Status.Text = "EXIT"
                Application.Exit()
            End If
        Else
            MsgBox("Company belom di setting !", vbCritical + vbOKOnly, ".:Warning !")
            Status.Text = "EXIT"
            Application.Exit()
        End If

        Dim version As String = My.Application.Info.Version.Major.ToString +
           My.Application.Info.Version.Minor.ToString +
           My.Application.Info.Version.Build.ToString + "-" +
           My.Application.Info.Version.Revision.ToString
        Me.Text = CompCode.Text + "-" + version
        If dVersion <> version Then
            MsgBox("Program ini masih menggunakan versi lama (" & dVersion & ")" & vbCrLf & "Versi yang terbaru : " & version, vbCritical + vbOKOnly, "Perbaharui Program Anda... !")
            If UCase(UserID) = "EKO_K" Or UCase(UserID) = "ADMIN" Then
                SQL = "Update m_company set Version = '" & version & "' "
                Proses.ExecuteNonQuery(SQL)
                MsgBox("Program ini berhasil di update ke versi " & version, vbInformation + vbOKOnly, "Congratulation !")
            Else
                _11Keuangan.Visible = False
                _12BagianContoh.Visible = False
                _13BagianPembelian.Visible = False
                _14BagianPemasaran.Visible = False
                _15BagianGudang.Visible = False
                _16Monitoring.Visible = False
                _17AnalisaData.Visible = False
                _18Utility.Visible = False
            End If
        End If

    End Sub


    Private Sub CekTable()
        Dim SQL As String
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        'SQL = "SELECT *  FROM information_schema.COLUMNS " &
        '     "WHERE TABLE_NAME = 't_TransferH'  "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "CREATE TABLE  t_TransferH (
        '     IdRec   varchar (15) NOT NULL,
        '     TglTransfer   datetime  NOT NULL,
        '     KodeTokoAsal   varchar (5) NOT NULL,
        '     KodeTokoTujuan   varchar (5) NOT NULL,
        '     SubTotal   money  NOT NULL,
        '     PsDisc   real  NOT NULL,
        '     Disc   money  NOT NULL,
        '     PPN   money  NOT NULL,
        '     PsPPN   real  NOT NULL,
        '     TotalSales   money  NOT NULL,  
        '     Keterangan   varchar (255) NOT NULL,
        '     AktifYN   char (1) NOT NULL,
        '     LastUPD   datetime  NOT NULL,
        '     UserID   varchar (20) NOT NULL, 
        '     Kode_Toko   char (5) NOT NULL,  
        '     PostingYN   char (1) NULL DEFAULT ('N') ) "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "CREATE TABLE t_TransferD (
        '      Id_Rec   varchar (15) NOT NULL,
        '      NoUrut   varchar (3) NOT NULL,
        '      KodeBrg   varchar (30) NOT NULL,
        '      NamaBrg   varchar (100) NOT NULL,
        '      QTYB   float  NOT NULL,
        '      SatB   varchar (10) NOT NULL,
        '      QTY   float  NOT NULL,
        '      satuan   varchar (10) NOT NULL,
        '      PsDisc1   float  NOT NULL,
        '      Disc1   money  NOT NULL,
        '      Harga   money  NOT NULL,
        '      Sub_Total   money  NOT NULL,
        '      HargaSatuan_Asli   money  NULL,
        '      HargaModal   money  NOT NULL,
        '      Flag   char (1) NOT NULL,
        '      AktifYN   char (1) NOT NULL,
        '      LastUpd   datetime  NOT NULL,
        '      UserID   varchar (20) NOT NULL, 
        '      Kode_Toko   char (5) NOT NULL ) "
        '    Proses.ExecuteNonQuery(SQL)
        'End If

        'SQL = "SELECT *  FROM information_schema.COLUMNS " &
        '     "WHERE TABLE_NAME = 'm_Kota'  "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "CREATE TABLE m_Kota(
        '     IdRec int IDENTITY(1,1) PRIMARY KEY, 
        '     Nama varchar(100) NULL,
        '     AktifYN char(1) NULL,
        '     UserID varchar(20) NULL,
        '     LastUPD datetime NULL )  "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
        'SQL = "SELECT *  FROM information_schema.COLUMNS " &
        '     "WHERE TABLE_NAME = 'm_barang'  " &
        '     "  And column_name = 'psDisc' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "ALTER TABLE m_barang ADD psDisc float default 0 "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Update m_barang set psDisc = 0 "
        '    Proses.ExecuteNonQuery(SQL)
        'End If


        SQL = "Select company, kode_toko, convert(varchar(8), getdate(), 112) as tglServer, compcode " &
            "From M_Company "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Kode_Toko.Text = dbTable.Rows(0) !kode_toko
        End If
        'SQL = "Select Menu From m_UserMenu " &
        '    "Where Menu = 'DAFTAR_HARGA_UPDATE'"
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Insert into m_UserMenu (Menu) values  ('DAFTAR_HARGA_UPDATE')  "
        '    Proses.ExecuteNonQuery(SQL)
        'End If

        'SQL = "Select Menu From m_UserMenu " &
        '    "Where Menu = 'M_BARANG_UPD_HARGA'"
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Insert into m_UserMenu (Menu) values  ('M_BARANG_UPD_HARGA') "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
        'SQL = "select * from m_userakses
        '  where User_ID = 'HER'
        '    and menu = 'DAFTAR_HARGA_UPDATE' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '        "Edit, Hapus, Laporan, AktifYN, area) Values ('HER', " &
        '        "'DAFTAR_HARGA_UPDATE', 1, 1, 1, 1, 1, 'Y', " &
        '        "'" & Kode_Toko.Text & "') "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '        "Edit, Hapus, Laporan, AktifYN, area) Values ('EKO_K', " &
        '        "'DAFTAR_HARGA_UPDATE', 1, 1, 1, 1, 1, 'Y', " &
        '        "'" & Kode_Toko.Text & "') "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
        'SQL = "select * from m_userakses
        '  where User_ID = 'HER'
        '    and menu = 'M_BARANG_UPD_HARGA' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '        "Edit, Hapus, Laporan, AktifYN, area) Values ('HER', " &
        '        "'M_BARANG_UPD_HARGA', 1, 1, 1, 1, 1, 'Y', " &
        '        "'" & Kode_Toko.Text & "') "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '        "Edit, Hapus, Laporan, AktifYN, area) Values ('EKO_K', " &
        '        "'M_BARANG_UPD_HARGA', 1, 1, 1, 1, 1, 'Y', " &
        '        "'" & Kode_Toko.Text & "') "
        '    Proses.ExecuteNonQuery(SQL)
        '    'SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '    '    "Edit, Hapus, Laporan, AktifYN, area) Values ('HER', " &
        '    '    "'RPT_HISTORY_PELUNASAN_SUPPLIER', 1, 1, 1, 1, 1, 'Y', " &
        '    '    "'" & Kode_Toko.Text & "') "
        '    'Proses.ExecuteNonQuery(SQL)
        '    'SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '    '    "Edit, Hapus, Laporan, AktifYN, area) Values ('HER', " &
        '    '    "'RPT_HISTORY_PELUNASAN', 1, 1, 1, 1, 1, 'Y', " &
        '    '    "'" & Kode_Toko.Text & "') "
        '    'Proses.ExecuteNonQuery(SQL)

        '    'SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '    '    "Edit, Hapus, Laporan, AktifYN, area) Values ('EKO_K', " &
        '    '    "'RPT_HISTORY_PELUNASAN_CUSTOMER', 1, 1, 1, 1, 1, 'Y', " &
        '    '    "'" & Kode_Toko.Text & "') "
        '    'Proses.ExecuteNonQuery(SQL)
        '    'SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '    '    "Edit, Hapus, Laporan, AktifYN, area) Values ('EKO_K', " &
        '    '    "'RPT_HISTORY_PELUNASAN_SUPPLIER', 1, 1, 1, 1, 1, 'Y', " &
        '    '    "'" & Kode_Toko.Text & "') "
        '    'Proses.ExecuteNonQuery(SQL)
        '    'SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
        '    '    "Edit, Hapus, Laporan, AktifYN, area) Values ('EKO_K', " &
        '    '    "'RPT_HISTORY_PELUNASAN', 1, 1, 1, 1, 1, 'Y', " &
        '    '    "'" & Kode_Toko.Text & "') "
        '    'Proses.ExecuteNonQuery(SQL)
        'End If
    End Sub


    Private Function UserAkses(ByVal tUser As String, ByVal JenisTR As String) As Boolean
        Dim Result As Boolean = False
        Dim SQL As String
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        SQL = "Select * From M_UserAkses " &
            "Where User_ID = '" & tUser & "' " &
            "  And Menu = '" & JenisTR & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Result = True
        Else
            Result = False
        End If
        UserAkses = Result
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TsJam.Text = Format(Now, "hh:mm:ss")
    End Sub


    Public Sub GantiToko(kodetoko As String, namatoko As String, namaperusahaan As String)
        Kode_Toko.Text = kodetoko
        Nama_Toko.Text = namatoko
        TsCompany.Text = namaperusahaan
        StatusStrip1.Refresh()
    End Sub

    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Form_Pembelian.MdiParent = Me
        Form_Pembelian.Show()
    End Sub

    Private Sub mnuGantiPassword_Click(sender As Object, e As EventArgs) Handles _81_GANTI_PASSWORD.Click
        Form_UserChangePassword.MdiParent = Me
        Form_UserChangePassword.Show()
    End Sub

    Private Sub mnuUserBaru_Click(sender As Object, e As EventArgs) Handles _82_USER_BARU.Click
        Form_User.MdiParent = Me
        Form_User.Show()
    End Sub

    Private Sub mnuPengaturanUser_Click(sender As Object, e As EventArgs) Handles _83_PENGATURAN_USER.Click
        Form_UserAkses.MdiParent = Me
        Form_UserAkses.Show()
    End Sub

    Private Sub mnuCompanySetup_Click(sender As Object, e As EventArgs) Handles _84_COMPANY_SETUP.Click
        Form_CompanySetup.ShowDialog()
    End Sub



    Private Sub KoreksiStock_Click(sender As Object, e As EventArgs)
        Form_KoreksiStock.MdiParent = Me
        Form_KoreksiStock.Show()
    End Sub

    Private Sub Kode_Toko_Click(sender As Object, e As EventArgs) Handles Kode_Toko.Click
        Dim ceklogin As Boolean = Proses.UserAksesMenu(TsPengguna.Text, "M_COMPANY_SETUP")
        If ceklogin Then
            Form_CompanySetup.ShowDialog()
        End If
    End Sub

    Private Sub mnuSettingDB_Click(sender As Object, e As EventArgs)
        Form_Backup.MdiParent = Me
        Form_Backup.Show()
    End Sub

    Private Sub mnuToko_Click(sender As Object, e As EventArgs)
        Form_MToko.MdiParent = Me
        Form_MToko.Show()
    End Sub

    Private Sub FrmMenuUtama_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Status.Text <> "EXIT" Then
            If MessageBox.Show("Apakah benar-benar mau logout ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub mnuPenjualan_Click(sender As Object, e As EventArgs)
        Form_Penjualan.MdiParent = Me
        Form_Penjualan.Show()
    End Sub


    Private Sub mnuKosongData_Click(sender As Object, e As EventArgs) Handles mnuKosongData.Click
        Dim sql As String = ""
        'If MsgBox("Yakin kosongkan data transaksi & stock menjadi NOL ?", vbInformation + vbYesNo, ".: Confirmation ") = vbYes Then
        If MessageBox.Show("Yakin kosongkan data transaksi & stock menjadi NOL ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            sql = " Update m_barang set stock01 = 0, stock02 = 0, stock03 = 0, stock04 = 0;
	            truncate table m_Pricelist;
	            truncate table t_BayarCustD;
	            truncate table t_BayarCustH;
                truncate table t_BayarSuppD;
                truncate table t_BayarSuppH;
	            truncate table t_KoreksiHargaBarang;
	            truncate table t_KoreksiStock;
	            truncate table t_pod;
	            truncate table t_poh;
	            truncate table t_ReturJualD;
	            truncate table t_ReturJualH;
	            truncate table t_SOD;
	            truncate table t_SOH;
	            truncate table t_transaksi;
	            truncate table t_stockawal;
                truncate table t_ReturBeliD;
                truncate table t_ReturBeliH;
                truncate table t_ReturJualD;
                truncate table t_ReturJualH;
                truncate table t_SalesCommD;
                truncate table t_SalesCommH;
                truncate table t_TagihCustD;
                truncate table t_TagihCustH;
                truncate table t_TagihSuppD;
                truncate table t_TagihSuppH; 
                truncate table tmp_RptStock;
                truncate table tmp_transaksi; "
            Proses.ExecuteNonQuery(sql)
            MsgBox("Data berhasil di kosongkan", vbOKOnly + vbInformation, ".: Congratulation !")

        End If
    End Sub


    Private Sub mnuBackup_Click(sender As Object, e As EventArgs) Handles _8A_BACKUP_DATA.Click
        Form_BackupDb.MdiParent = Me
        Form_BackupDb.Show()
    End Sub


    Private Sub mnuHitungStock_Click(sender As Object, e As EventArgs)
        Form_HitungStock.MdiParent = Me
        Form_HitungStock.Show()
    End Sub


    Private Sub mnuRestore_Click(sender As Object, e As EventArgs) Handles _8B_RESTORE_DATA.Click
        Form_BackupRestore.MdiParent = Me
        Form_BackupRestore.Show()
    End Sub


    Private Sub _23KodifikasiDaerah_Click(sender As Object, e As EventArgs) Handles _23KodifikasiDaerah.Click
        Form_KodifDaerah.MdiParent = Me
        Form_KodifDaerah.Show()
    End Sub

    Private Sub _21KodifikasiBahanBaku_Click(sender As Object, e As EventArgs) Handles _21KodifikasiBahanBaku.Click
        Form_KodifBahanBaku.MdiParent = Me
        Form_KodifBahanBaku.Show()
    End Sub

    Private Sub _41KodifikasiNegara_Click(sender As Object, e As EventArgs) Handles _41KodifikasiNegara.Click
        Form_KodifNegara.MdiParent = Me
        Form_KodifNegara.Show()
    End Sub

    Private Sub _22KodifikasiFungsiProduk_Click(sender As Object, e As EventArgs) Handles _22KodifikasiFungsiProduk.Click
        Form_KodifFungsi.MdiParent = Me
        Form_KodifFungsi.Show()
    End Sub

    Private Sub _42KodifikasiImportir_Click(sender As Object, e As EventArgs) Handles _42KodifikasiImportir.Click
        Form_KodifImportir.MdiParent = Me
        Form_KodifImportir.Show()
    End Sub

    Private Sub _24KodifikasiPerajin_Click(sender As Object, e As EventArgs) Handles _24KodifikasiPerajin.Click
        Form_KodifPerajin.MdiParent = Me
        Form_KodifPerajin.Show()
    End Sub

    Private Sub _25KodifikasiProduk_Click(sender As Object, e As EventArgs) Handles _25KodifikasiProduk.Click
        form_kodifProduk.MdiParent = Me
        form_kodifProduk.show()
    End Sub

    Private Sub _27DaftarPenerimaanBarang_Click(sender As Object, e As EventArgs) Handles _27DaftarPenerimaanBarang.Click
        Form_DPBContoh.MdiParent = Me
        Form_DPBContoh.Show()
    End Sub

    Private Sub _26SPDenganKode_Click(sender As Object, e As EventArgs) Handles _26SPDenganKode.Click
        Form_SP_Contoh.MdiParent = Me
        Form_SP_Contoh.Show()
    End Sub

    Private Sub _28Katalog_Click(sender As Object, e As EventArgs) Handles _28Katalog.Click
        Form_KatalogSample.MdiParent = Me
        Form_KatalogSample.Show()
    End Sub

    Private Sub _51PraLHP_Click(sender As Object, e As EventArgs) Handles _51PraLHP.Click
        Form_GdPraLHP.MdiParent = Me
        Form_GdPraLHP.Show()
    End Sub

    Private Sub _31SuratPesanan_Click(sender As Object, e As EventArgs) Handles _31SuratPesanan.Click
        Form_SP.MdiParent = Me
        Form_SP.Show()
    End Sub

    Private Sub _54JenisBox_Click(sender As Object, e As EventArgs) Handles _54JenisBox.Click
        Form_JenisBox.MdiParent = Me
        Form_JenisBox.Show()
    End Sub



    Private Sub _44PurchasingOrder_Click(sender As Object, e As EventArgs) Handles _44PurchasingOrder.Click
        Form_PO.MdiParent = Me
        Form_PO.Show()
    End Sub

    Private Sub _45ProformaInvoice_Click(sender As Object, e As EventArgs) Handles _45ProformaInvoice.Click
        Form_PI.MdiParent = Me
        Form_PI.Show()
    End Sub

    Private Sub _43KodeProdukBuyer_Click(sender As Object, e As EventArgs) Handles _43KodeProdukBuyer.Click
        Form_KodeProdukBuyer.MdiParent = Me
        Form_KodeProdukBuyer.Show()
    End Sub

    Private Sub _32DPB_Click(sender As Object, e As EventArgs) Handles _32DPB.Click
        Form_DPB.MdiParent = Me
        Form_DPB.Show()
    End Sub

    Private Sub _52LHP_Click(sender As Object, e As EventArgs) Handles _52LHP.Click
        Form_Gd_LHP.MdiParent = Me
        Form_Gd_LHP.Show()
    End Sub

    Private Sub _33PengajuanUangMukaPerajin_Click(sender As Object, e As EventArgs) Handles _33PengajuanUangMukaPerajin.Click
        Form_UangMuka.MdiParent = Me
        Form_UangMuka.Show()
    End Sub



    Private Sub _53DraftPackingListDPL_Click(sender As Object, e As EventArgs) Handles _53DraftPackingListDPL.Click
        Form_GD_DPL.MdiParent = Me
        Form_GD_DPL.Show()
    End Sub

    Private Sub _55PackingListInvoice_Click(sender As Object, e As EventArgs) Handles _55PackingListInvoice.Click
        Form_GD_PackingListInvoice.MdiParent = Me
        Form_GD_PackingListInvoice.Show()
    End Sub

    Private Sub _57ReturBarang_Click(sender As Object, e As EventArgs) Handles _57ReturBarang.Click
        Form_GD_Retur.MdiParent = Me
        Form_GD_Retur.Show()
    End Sub

    Private Sub _56MetodePengiriman_Click(sender As Object, e As EventArgs) Handles _56MetodePengiriman.Click
        Form_MetodePengiriman.MdiParent = Me
        Form_MetodePengiriman.Show()
    End Sub

    Private Sub _101JurnalUmum_Click(sender As Object, e As EventArgs) Handles _101JurnalUmum.Click
        Form_KeuJurnalUmum.MdiParent = Me
        Form_KeuJurnalUmum.Show()
    End Sub

    Private Sub _105KodeGL_Click(sender As Object, e As EventArgs) Handles _105KodeGL.Click
        Form_KeuKodeGL.MdiParent = Me
        Form_KeuKodeGL.Show()
    End Sub

    Private Sub ExploreFileFotoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExploreFileFotoToolStripMenuItem.Click
        Form_ExploreFoto.MdiParent = Me
        Form_ExploreFoto.Show()
    End Sub

    Private Sub _102KasBankKeluar_Click(sender As Object, e As EventArgs) Handles _102KasBankKeluar.Click
        Form_KeuJurnalKeluar.MdiParent = Me
        Form_KeuJurnalKeluar.Show()
    End Sub
End Class



'https://stackoverflow.com/questions/22482709/programs-and-features-icon-for-win-application-deployed-with-clickonce
'Custom icon for ClickOnce application in 'Add or Remove Programs'
'https://stackoverflow.com/questions/16228550/add-or-remove-programs-icon-for-a-c-sharp-clickonce-application
'Add or Remove Programs' icon for a C# ClickOnce application