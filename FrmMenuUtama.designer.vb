<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMenuUtama
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMenuUtama))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TsPengguna = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TsTanggal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TsJam = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TsCompany = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Kode_Toko = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSKeterangan = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Nama_Toko = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tServer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ServerName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TipeCetakan = New System.Windows.Forms.ToolStripStatusLabel()
        Me.CompCode = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UserLoginMenu = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PasswordLoginMenu = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me._11Keuangan = New System.Windows.Forms.ToolStripMenuItem()
        Me.JurnalUmumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KasBankKeluarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KasBankMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventearisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.KodeGLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaldoAKhirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.DaftarJurnalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GeneralLedgerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrialBalanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabaRugiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NeracaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProsesAkhirBulanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.PengajuanPotonganPerajinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UangMukaLanjutanPelunasanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me._12BagianContoh = New System.Windows.Forms.ToolStripMenuItem()
        Me._21KodifikasiBahanBaku = New System.Windows.Forms.ToolStripMenuItem()
        Me._22KodifikasiFungsiProduk = New System.Windows.Forms.ToolStripMenuItem()
        Me._23KodifikasiDaerah = New System.Windows.Forms.ToolStripMenuItem()
        Me._24KodifikasiPerajin = New System.Windows.Forms.ToolStripMenuItem()
        Me._25KodifikasiProduk = New System.Windows.Forms.ToolStripMenuItem()
        Me._26SPDenganKode = New System.Windows.Forms.ToolStripMenuItem()
        Me._27DaftarPenerimaanBarang = New System.Windows.Forms.ToolStripMenuItem()
        Me._28Katalog = New System.Windows.Forms.ToolStripMenuItem()
        Me._13BagianPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me._31SuratPesanan = New System.Windows.Forms.ToolStripMenuItem()
        Me._32DPB = New System.Windows.Forms.ToolStripMenuItem()
        Me._33PengajuanUangMukaPerajin = New System.Windows.Forms.ToolStripMenuItem()
        Me._14BagianPemasaran = New System.Windows.Forms.ToolStripMenuItem()
        Me._41KodifikasiNegara = New System.Windows.Forms.ToolStripMenuItem()
        Me._42KodifikasiImportir = New System.Windows.Forms.ToolStripMenuItem()
        Me._43KodeProdukBuyer = New System.Windows.Forms.ToolStripMenuItem()
        Me._44PurchasingOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me._45ProformaInvoice = New System.Windows.Forms.ToolStripMenuItem()
        Me._46PembayaranDariBuyer = New System.Windows.Forms.ToolStripMenuItem()
        Me._15BagianGudang = New System.Windows.Forms.ToolStripMenuItem()
        Me._51PraLHP = New System.Windows.Forms.ToolStripMenuItem()
        Me._52LHP = New System.Windows.Forms.ToolStripMenuItem()
        Me._53DraftPackingListDPL = New System.Windows.Forms.ToolStripMenuItem()
        Me._54JenisBox = New System.Windows.Forms.ToolStripMenuItem()
        Me._55PackingListInvoice = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetodePengirimanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturBarangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me._16Monitoring = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScheduleShipmentDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DaftarSPYangBelumDiPraLHPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DaftarPOYangBelumDiBuatSPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me._17AnalisaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.PemasaranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GudangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembelianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BagianContohToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeuanganPerajinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me._18Utility = New System.Windows.Forms.ToolStripMenuItem()
        Me._81_GANTI_PASSWORD = New System.Windows.Forms.ToolStripMenuItem()
        Me._82_USER_BARU = New System.Windows.Forms.ToolStripMenuItem()
        Me._83_PENGATURAN_USER = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me._84_COMPANY_SETUP = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.KirimDataKeGudangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AmbilDataDariGudangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.KirimDataKeWaruToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AmbilDataDariWaruToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me._8A_BACKUP_DATA = New System.Windows.Forms.ToolStripMenuItem()
        Me._8B_RESTORE_DATA = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuKosongData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.AllowDrop = True
        Me.ContentPanel.AutoScroll = True
        Me.ContentPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ContentPanel.Size = New System.Drawing.Size(676, 256)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsPengguna, Me.TsTanggal, Me.TsJam, Me.TsCompany, Me.Kode_Toko, Me.TSKeterangan, Me.Nama_Toko, Me.tServer, Me.Status, Me.ServerName, Me.TipeCetakan, Me.CompCode, Me.UserLoginMenu, Me.PasswordLoginMenu})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 685)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1184, 24)
        Me.StatusStrip1.Stretch = False
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TsPengguna
        '
        Me.TsPengguna.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TsPengguna.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.TsPengguna.Name = "TsPengguna"
        Me.TsPengguna.Size = New System.Drawing.Size(65, 19)
        Me.TsPengguna.Text = "Pengguna"
        '
        'TsTanggal
        '
        Me.TsTanggal.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TsTanggal.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.TsTanggal.Name = "TsTanggal"
        Me.TsTanggal.Size = New System.Drawing.Size(52, 19)
        Me.TsTanggal.Text = "Tanggal"
        '
        'TsJam
        '
        Me.TsJam.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TsJam.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.TsJam.Name = "TsJam"
        Me.TsJam.Size = New System.Drawing.Size(32, 19)
        Me.TsJam.Text = "Jam"
        '
        'TsCompany
        '
        Me.TsCompany.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TsCompany.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.TsCompany.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TsCompany.Name = "TsCompany"
        Me.TsCompany.Size = New System.Drawing.Size(63, 19)
        Me.TsCompany.Text = "Company"
        '
        'Kode_Toko
        '
        Me.Kode_Toko.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.Kode_Toko.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.Kode_Toko.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Kode_Toko.Name = "Kode_Toko"
        Me.Kode_Toko.Size = New System.Drawing.Size(68, 19)
        Me.Kode_Toko.Text = "Kode_Toko"
        '
        'TSKeterangan
        '
        Me.TSKeterangan.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.TSKeterangan.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.TSKeterangan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSKeterangan.Name = "TSKeterangan"
        Me.TSKeterangan.Size = New System.Drawing.Size(71, 19)
        Me.TSKeterangan.Text = "Keterangan"
        Me.TSKeterangan.Visible = False
        '
        'Nama_Toko
        '
        Me.Nama_Toko.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.Nama_Toko.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.Nama_Toko.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Nama_Toko.Name = "Nama_Toko"
        Me.Nama_Toko.Size = New System.Drawing.Size(73, 19)
        Me.Nama_Toko.Text = "Nama_Toko"
        Me.Nama_Toko.Visible = False
        '
        'tServer
        '
        Me.tServer.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.tServer.Name = "tServer"
        Me.tServer.Size = New System.Drawing.Size(0, 19)
        '
        'Status
        '
        Me.Status.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.Status.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(46, 19)
        Me.Status.Text = "CLOSE"
        Me.Status.Visible = False
        '
        'ServerName
        '
        Me.ServerName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ServerName.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.ServerName.Name = "ServerName"
        Me.ServerName.Size = New System.Drawing.Size(43, 19)
        Me.ServerName.Text = "Server"
        '
        'TipeCetakan
        '
        Me.TipeCetakan.Name = "TipeCetakan"
        Me.TipeCetakan.Size = New System.Drawing.Size(72, 19)
        Me.TipeCetakan.Text = "TipeCetakan"
        Me.TipeCetakan.Visible = False
        '
        'CompCode
        '
        Me.CompCode.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.CompCode.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.CompCode.Name = "CompCode"
        Me.CompCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CompCode.Size = New System.Drawing.Size(72, 19)
        Me.CompCode.Text = "CompCode"
        '
        'UserLoginMenu
        '
        Me.UserLoginMenu.Name = "UserLoginMenu"
        Me.UserLoginMenu.Size = New System.Drawing.Size(91, 19)
        Me.UserLoginMenu.Text = "UserLoginMenu"
        Me.UserLoginMenu.Visible = False
        '
        'PasswordLoginMenu
        '
        Me.PasswordLoginMenu.Name = "PasswordLoginMenu"
        Me.PasswordLoginMenu.Size = New System.Drawing.Size(118, 19)
        Me.PasswordLoginMenu.Text = "PasswordLoginMenu"
        Me.PasswordLoginMenu.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._11Keuangan, Me._12BagianContoh, Me._13BagianPembelian, Me._14BagianPemasaran, Me._15BagianGudang, Me._16Monitoring, Me._17AnalisaData, Me._18Utility, Me.mnuExit})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1184, 30)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '_11Keuangan
        '
        Me._11Keuangan.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JurnalUmumToolStripMenuItem, Me.KasBankKeluarToolStripMenuItem, Me.KasBankMasukToolStripMenuItem, Me.InventearisToolStripMenuItem, Me.ToolStripSeparator5, Me.KodeGLToolStripMenuItem, Me.SaldoAKhirToolStripMenuItem, Me.ToolStripSeparator6, Me.DaftarJurnalToolStripMenuItem, Me.GeneralLedgerToolStripMenuItem, Me.TrialBalanceToolStripMenuItem, Me.LabaRugiToolStripMenuItem, Me.NeracaToolStripMenuItem, Me.ProsesAkhirBulanToolStripMenuItem, Me.ToolStripSeparator7, Me.ToolStripMenuItem8, Me.ToolStripSeparator8, Me.PengajuanPotonganPerajinToolStripMenuItem, Me.UangMukaLanjutanPelunasanToolStripMenuItem})
        Me._11Keuangan.Name = "_11Keuangan"
        Me._11Keuangan.Size = New System.Drawing.Size(72, 26)
        Me._11Keuangan.Text = "Keuangan"
        '
        'JurnalUmumToolStripMenuItem
        '
        Me.JurnalUmumToolStripMenuItem.Name = "JurnalUmumToolStripMenuItem"
        Me.JurnalUmumToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.JurnalUmumToolStripMenuItem.Text = "Jurnal Umum"
        '
        'KasBankKeluarToolStripMenuItem
        '
        Me.KasBankKeluarToolStripMenuItem.Name = "KasBankKeluarToolStripMenuItem"
        Me.KasBankKeluarToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.KasBankKeluarToolStripMenuItem.Text = "Kas/Bank Keluar"
        '
        'KasBankMasukToolStripMenuItem
        '
        Me.KasBankMasukToolStripMenuItem.Name = "KasBankMasukToolStripMenuItem"
        Me.KasBankMasukToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.KasBankMasukToolStripMenuItem.Text = "Kas/Bank Masuk"
        '
        'InventearisToolStripMenuItem
        '
        Me.InventearisToolStripMenuItem.Name = "InventearisToolStripMenuItem"
        Me.InventearisToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.InventearisToolStripMenuItem.Text = "Inventearis"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(241, 6)
        '
        'KodeGLToolStripMenuItem
        '
        Me.KodeGLToolStripMenuItem.Name = "KodeGLToolStripMenuItem"
        Me.KodeGLToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.KodeGLToolStripMenuItem.Text = "Kode GL"
        '
        'SaldoAKhirToolStripMenuItem
        '
        Me.SaldoAKhirToolStripMenuItem.Name = "SaldoAKhirToolStripMenuItem"
        Me.SaldoAKhirToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.SaldoAKhirToolStripMenuItem.Text = "Saldo Akhir"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(241, 6)
        '
        'DaftarJurnalToolStripMenuItem
        '
        Me.DaftarJurnalToolStripMenuItem.Name = "DaftarJurnalToolStripMenuItem"
        Me.DaftarJurnalToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.DaftarJurnalToolStripMenuItem.Text = "Daftar Jurnal"
        '
        'GeneralLedgerToolStripMenuItem
        '
        Me.GeneralLedgerToolStripMenuItem.Name = "GeneralLedgerToolStripMenuItem"
        Me.GeneralLedgerToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.GeneralLedgerToolStripMenuItem.Text = "General Ledger"
        '
        'TrialBalanceToolStripMenuItem
        '
        Me.TrialBalanceToolStripMenuItem.Name = "TrialBalanceToolStripMenuItem"
        Me.TrialBalanceToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.TrialBalanceToolStripMenuItem.Text = "Trial balance"
        '
        'LabaRugiToolStripMenuItem
        '
        Me.LabaRugiToolStripMenuItem.Name = "LabaRugiToolStripMenuItem"
        Me.LabaRugiToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.LabaRugiToolStripMenuItem.Text = "Laba Rugi"
        '
        'NeracaToolStripMenuItem
        '
        Me.NeracaToolStripMenuItem.Name = "NeracaToolStripMenuItem"
        Me.NeracaToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.NeracaToolStripMenuItem.Text = "Neraca"
        '
        'ProsesAkhirBulanToolStripMenuItem
        '
        Me.ProsesAkhirBulanToolStripMenuItem.Name = "ProsesAkhirBulanToolStripMenuItem"
        Me.ProsesAkhirBulanToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.ProsesAkhirBulanToolStripMenuItem.Text = "Neraca"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(241, 6)
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(244, 22)
        Me.ToolStripMenuItem8.Text = "Proses Akhir Bulan"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(241, 6)
        '
        'PengajuanPotonganPerajinToolStripMenuItem
        '
        Me.PengajuanPotonganPerajinToolStripMenuItem.Name = "PengajuanPotonganPerajinToolStripMenuItem"
        Me.PengajuanPotonganPerajinToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.PengajuanPotonganPerajinToolStripMenuItem.Text = "Pengajuan Potongan Perajin"
        '
        'UangMukaLanjutanPelunasanToolStripMenuItem
        '
        Me.UangMukaLanjutanPelunasanToolStripMenuItem.Name = "UangMukaLanjutanPelunasanToolStripMenuItem"
        Me.UangMukaLanjutanPelunasanToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.UangMukaLanjutanPelunasanToolStripMenuItem.Text = "Uang Muka Lanjutan & Pelunasan"
        '
        '_12BagianContoh
        '
        Me._12BagianContoh.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._21KodifikasiBahanBaku, Me._22KodifikasiFungsiProduk, Me._23KodifikasiDaerah, Me._24KodifikasiPerajin, Me._25KodifikasiProduk, Me._26SPDenganKode, Me._27DaftarPenerimaanBarang, Me._28Katalog})
        Me._12BagianContoh.Name = "_12BagianContoh"
        Me._12BagianContoh.Size = New System.Drawing.Size(98, 26)
        Me._12BagianContoh.Text = "Bagian Contoh"
        '
        '_21KodifikasiBahanBaku
        '
        Me._21KodifikasiBahanBaku.Image = CType(resources.GetObject("_21KodifikasiBahanBaku.Image"), System.Drawing.Image)
        Me._21KodifikasiBahanBaku.Name = "_21KodifikasiBahanBaku"
        Me._21KodifikasiBahanBaku.Size = New System.Drawing.Size(212, 22)
        Me._21KodifikasiBahanBaku.Text = "Kodifikasi Bahan Baku"
        '
        '_22KodifikasiFungsiProduk
        '
        Me._22KodifikasiFungsiProduk.Image = CType(resources.GetObject("_22KodifikasiFungsiProduk.Image"), System.Drawing.Image)
        Me._22KodifikasiFungsiProduk.Name = "_22KodifikasiFungsiProduk"
        Me._22KodifikasiFungsiProduk.Size = New System.Drawing.Size(212, 22)
        Me._22KodifikasiFungsiProduk.Text = "Kodifikasi Fungsi Produk"
        '
        '_23KodifikasiDaerah
        '
        Me._23KodifikasiDaerah.Image = CType(resources.GetObject("_23KodifikasiDaerah.Image"), System.Drawing.Image)
        Me._23KodifikasiDaerah.Name = "_23KodifikasiDaerah"
        Me._23KodifikasiDaerah.Size = New System.Drawing.Size(212, 22)
        Me._23KodifikasiDaerah.Text = "Kodifikasi Daerah"
        '
        '_24KodifikasiPerajin
        '
        Me._24KodifikasiPerajin.Image = CType(resources.GetObject("_24KodifikasiPerajin.Image"), System.Drawing.Image)
        Me._24KodifikasiPerajin.Name = "_24KodifikasiPerajin"
        Me._24KodifikasiPerajin.Size = New System.Drawing.Size(212, 22)
        Me._24KodifikasiPerajin.Text = "Kodifikasi Perajin"
        '
        '_25KodifikasiProduk
        '
        Me._25KodifikasiProduk.Image = CType(resources.GetObject("_25KodifikasiProduk.Image"), System.Drawing.Image)
        Me._25KodifikasiProduk.Name = "_25KodifikasiProduk"
        Me._25KodifikasiProduk.Size = New System.Drawing.Size(212, 22)
        Me._25KodifikasiProduk.Text = "Kodifikasi Produk"
        '
        '_26SPDenganKode
        '
        Me._26SPDenganKode.Image = CType(resources.GetObject("_26SPDenganKode.Image"), System.Drawing.Image)
        Me._26SPDenganKode.Name = "_26SPDenganKode"
        Me._26SPDenganKode.Size = New System.Drawing.Size(212, 22)
        Me._26SPDenganKode.Text = "SP dengan Kode"
        '
        '_27DaftarPenerimaanBarang
        '
        Me._27DaftarPenerimaanBarang.Image = CType(resources.GetObject("_27DaftarPenerimaanBarang.Image"), System.Drawing.Image)
        Me._27DaftarPenerimaanBarang.Name = "_27DaftarPenerimaanBarang"
        Me._27DaftarPenerimaanBarang.Size = New System.Drawing.Size(212, 22)
        Me._27DaftarPenerimaanBarang.Text = "Daftar Penerimaan Barang"
        '
        '_28Katalog
        '
        Me._28Katalog.Image = CType(resources.GetObject("_28Katalog.Image"), System.Drawing.Image)
        Me._28Katalog.Name = "_28Katalog"
        Me._28Katalog.Size = New System.Drawing.Size(212, 22)
        Me._28Katalog.Text = "Katalog"
        '
        '_13BagianPembelian
        '
        Me._13BagianPembelian.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._31SuratPesanan, Me._32DPB, Me._33PengajuanUangMukaPerajin})
        Me._13BagianPembelian.Name = "_13BagianPembelian"
        Me._13BagianPembelian.Size = New System.Drawing.Size(114, 26)
        Me._13BagianPembelian.Text = "Bagian Pembelian"
        '
        '_31SuratPesanan
        '
        Me._31SuratPesanan.Image = CType(resources.GetObject("_31SuratPesanan.Image"), System.Drawing.Image)
        Me._31SuratPesanan.Name = "_31SuratPesanan"
        Me._31SuratPesanan.Size = New System.Drawing.Size(245, 22)
        Me._31SuratPesanan.Text = "Surat Pesanan"
        '
        '_32DPB
        '
        Me._32DPB.Image = CType(resources.GetObject("_32DPB.Image"), System.Drawing.Image)
        Me._32DPB.Name = "_32DPB"
        Me._32DPB.Size = New System.Drawing.Size(245, 22)
        Me._32DPB.Text = "Daftar Penerimaan Barang / DPB"
        '
        '_33PengajuanUangMukaPerajin
        '
        Me._33PengajuanUangMukaPerajin.Image = CType(resources.GetObject("_33PengajuanUangMukaPerajin.Image"), System.Drawing.Image)
        Me._33PengajuanUangMukaPerajin.Name = "_33PengajuanUangMukaPerajin"
        Me._33PengajuanUangMukaPerajin.Size = New System.Drawing.Size(245, 22)
        Me._33PengajuanUangMukaPerajin.Text = "Pengajuan Uang Muka Perajin"
        '
        '_14BagianPemasaran
        '
        Me._14BagianPemasaran.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._41KodifikasiNegara, Me._42KodifikasiImportir, Me._43KodeProdukBuyer, Me._44PurchasingOrder, Me._45ProformaInvoice, Me._46PembayaranDariBuyer})
        Me._14BagianPemasaran.Name = "_14BagianPemasaran"
        Me._14BagianPemasaran.Size = New System.Drawing.Size(116, 26)
        Me._14BagianPemasaran.Text = "Bagian Pemasaran"
        '
        '_41KodifikasiNegara
        '
        Me._41KodifikasiNegara.Image = CType(resources.GetObject("_41KodifikasiNegara.Image"), System.Drawing.Image)
        Me._41KodifikasiNegara.Name = "_41KodifikasiNegara"
        Me._41KodifikasiNegara.Size = New System.Drawing.Size(196, 22)
        Me._41KodifikasiNegara.Text = "Kodifikasi Negara"
        '
        '_42KodifikasiImportir
        '
        Me._42KodifikasiImportir.Image = CType(resources.GetObject("_42KodifikasiImportir.Image"), System.Drawing.Image)
        Me._42KodifikasiImportir.Name = "_42KodifikasiImportir"
        Me._42KodifikasiImportir.Size = New System.Drawing.Size(196, 22)
        Me._42KodifikasiImportir.Text = "Kodifikasi Importir"
        '
        '_43KodeProdukBuyer
        '
        Me._43KodeProdukBuyer.Image = CType(resources.GetObject("_43KodeProdukBuyer.Image"), System.Drawing.Image)
        Me._43KodeProdukBuyer.Name = "_43KodeProdukBuyer"
        Me._43KodeProdukBuyer.Size = New System.Drawing.Size(196, 22)
        Me._43KodeProdukBuyer.Text = "Kode Produk Buyer"
        '
        '_44PurchasingOrder
        '
        Me._44PurchasingOrder.Image = CType(resources.GetObject("_44PurchasingOrder.Image"), System.Drawing.Image)
        Me._44PurchasingOrder.Name = "_44PurchasingOrder"
        Me._44PurchasingOrder.Size = New System.Drawing.Size(196, 22)
        Me._44PurchasingOrder.Text = "Purchasing Order (PO)"
        '
        '_45ProformaInvoice
        '
        Me._45ProformaInvoice.Image = CType(resources.GetObject("_45ProformaInvoice.Image"), System.Drawing.Image)
        Me._45ProformaInvoice.Name = "_45ProformaInvoice"
        Me._45ProformaInvoice.Size = New System.Drawing.Size(196, 22)
        Me._45ProformaInvoice.Text = "Proforma Invoice (PI)"
        '
        '_46PembayaranDariBuyer
        '
        Me._46PembayaranDariBuyer.Name = "_46PembayaranDariBuyer"
        Me._46PembayaranDariBuyer.Size = New System.Drawing.Size(196, 22)
        Me._46PembayaranDariBuyer.Text = "Pembayaran dari Buyer"
        '
        '_15BagianGudang
        '
        Me._15BagianGudang.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._51PraLHP, Me._52LHP, Me._53DraftPackingListDPL, Me._54JenisBox, Me._55PackingListInvoice, Me.MetodePengirimanToolStripMenuItem, Me.ReturBarangToolStripMenuItem})
        Me._15BagianGudang.Name = "_15BagianGudang"
        Me._15BagianGudang.Size = New System.Drawing.Size(100, 26)
        Me._15BagianGudang.Text = "Bagian Gudang"
        '
        '_51PraLHP
        '
        Me._51PraLHP.Image = CType(resources.GetObject("_51PraLHP.Image"), System.Drawing.Image)
        Me._51PraLHP.Name = "_51PraLHP"
        Me._51PraLHP.Size = New System.Drawing.Size(249, 22)
        Me._51PraLHP.Text = "Pra LHP"
        '
        '_52LHP
        '
        Me._52LHP.Image = CType(resources.GetObject("_52LHP.Image"), System.Drawing.Image)
        Me._52LHP.Name = "_52LHP"
        Me._52LHP.Size = New System.Drawing.Size(249, 22)
        Me._52LHP.Text = "Laporan Hasil Pemeriksaan (LHP)"
        '
        '_53DraftPackingListDPL
        '
        Me._53DraftPackingListDPL.Image = CType(resources.GetObject("_53DraftPackingListDPL.Image"), System.Drawing.Image)
        Me._53DraftPackingListDPL.Name = "_53DraftPackingListDPL"
        Me._53DraftPackingListDPL.Size = New System.Drawing.Size(249, 22)
        Me._53DraftPackingListDPL.Text = "Draft Packing List (DPL)"
        '
        '_54JenisBox
        '
        Me._54JenisBox.Image = CType(resources.GetObject("_54JenisBox.Image"), System.Drawing.Image)
        Me._54JenisBox.Name = "_54JenisBox"
        Me._54JenisBox.Size = New System.Drawing.Size(249, 22)
        Me._54JenisBox.Text = "Jenis Box"
        '
        '_55PackingListInvoice
        '
        Me._55PackingListInvoice.Name = "_55PackingListInvoice"
        Me._55PackingListInvoice.Size = New System.Drawing.Size(249, 22)
        Me._55PackingListInvoice.Text = "PackingList && Invoice"
        '
        'MetodePengirimanToolStripMenuItem
        '
        Me.MetodePengirimanToolStripMenuItem.Image = CType(resources.GetObject("MetodePengirimanToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MetodePengirimanToolStripMenuItem.Name = "MetodePengirimanToolStripMenuItem"
        Me.MetodePengirimanToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.MetodePengirimanToolStripMenuItem.Text = "Metode Pengiriman"
        '
        'ReturBarangToolStripMenuItem
        '
        Me.ReturBarangToolStripMenuItem.Name = "ReturBarangToolStripMenuItem"
        Me.ReturBarangToolStripMenuItem.Size = New System.Drawing.Size(249, 22)
        Me.ReturBarangToolStripMenuItem.Text = "Retur Barang"
        '
        '_16Monitoring
        '
        Me._16Monitoring.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScheduleShipmentDateToolStripMenuItem, Me.DaftarSPYangBelumDiPraLHPToolStripMenuItem, Me.DaftarPOYangBelumDiBuatSPToolStripMenuItem})
        Me._16Monitoring.Name = "_16Monitoring"
        Me._16Monitoring.Size = New System.Drawing.Size(79, 26)
        Me._16Monitoring.Text = "Monitoring"
        '
        'ScheduleShipmentDateToolStripMenuItem
        '
        Me.ScheduleShipmentDateToolStripMenuItem.Name = "ScheduleShipmentDateToolStripMenuItem"
        Me.ScheduleShipmentDateToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ScheduleShipmentDateToolStripMenuItem.Text = "Schedule Shipment Date"
        '
        'DaftarSPYangBelumDiPraLHPToolStripMenuItem
        '
        Me.DaftarSPYangBelumDiPraLHPToolStripMenuItem.Name = "DaftarSPYangBelumDiPraLHPToolStripMenuItem"
        Me.DaftarSPYangBelumDiPraLHPToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.DaftarSPYangBelumDiPraLHPToolStripMenuItem.Text = "Daftar SP yang belum di Pra LHP"
        '
        'DaftarPOYangBelumDiBuatSPToolStripMenuItem
        '
        Me.DaftarPOYangBelumDiBuatSPToolStripMenuItem.Name = "DaftarPOYangBelumDiBuatSPToolStripMenuItem"
        Me.DaftarPOYangBelumDiBuatSPToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.DaftarPOYangBelumDiBuatSPToolStripMenuItem.Text = "Daftar PO yang belum di buat SP"
        '
        '_17AnalisaData
        '
        Me._17AnalisaData.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PemasaranToolStripMenuItem, Me.GudangToolStripMenuItem, Me.PembelianToolStripMenuItem, Me.BagianContohToolStripMenuItem, Me.KeuanganPerajinToolStripMenuItem})
        Me._17AnalisaData.Name = "_17AnalisaData"
        Me._17AnalisaData.Size = New System.Drawing.Size(84, 26)
        Me._17AnalisaData.Text = "Analisa Data"
        '
        'PemasaranToolStripMenuItem
        '
        Me.PemasaranToolStripMenuItem.Name = "PemasaranToolStripMenuItem"
        Me.PemasaranToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.PemasaranToolStripMenuItem.Text = "Pemasaran"
        '
        'GudangToolStripMenuItem
        '
        Me.GudangToolStripMenuItem.Name = "GudangToolStripMenuItem"
        Me.GudangToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.GudangToolStripMenuItem.Text = "Gudang"
        '
        'PembelianToolStripMenuItem
        '
        Me.PembelianToolStripMenuItem.Name = "PembelianToolStripMenuItem"
        Me.PembelianToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.PembelianToolStripMenuItem.Text = "Pembelian"
        '
        'BagianContohToolStripMenuItem
        '
        Me.BagianContohToolStripMenuItem.Name = "BagianContohToolStripMenuItem"
        Me.BagianContohToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.BagianContohToolStripMenuItem.Text = "Bagian Contoh"
        '
        'KeuanganPerajinToolStripMenuItem
        '
        Me.KeuanganPerajinToolStripMenuItem.Name = "KeuanganPerajinToolStripMenuItem"
        Me.KeuanganPerajinToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.KeuanganPerajinToolStripMenuItem.Text = "Keuangan Perajin"
        '
        '_18Utility
        '
        Me._18Utility.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._81_GANTI_PASSWORD, Me._82_USER_BARU, Me._83_PENGATURAN_USER, Me.ToolStripSeparator1, Me._84_COMPANY_SETUP, Me.ToolStripSeparator4, Me.KirimDataKeGudangToolStripMenuItem, Me.AmbilDataDariGudangToolStripMenuItem, Me.ToolStripSeparator2, Me.KirimDataKeWaruToolStripMenuItem, Me.AmbilDataDariWaruToolStripMenuItem, Me.ToolStripSeparator3, Me._8A_BACKUP_DATA, Me._8B_RESTORE_DATA, Me.mnuKosongData})
        Me._18Utility.Name = "_18Utility"
        Me._18Utility.Size = New System.Drawing.Size(50, 26)
        Me._18Utility.Text = "Utility"
        '
        '_81_GANTI_PASSWORD
        '
        Me._81_GANTI_PASSWORD.Name = "_81_GANTI_PASSWORD"
        Me._81_GANTI_PASSWORD.Size = New System.Drawing.Size(200, 22)
        Me._81_GANTI_PASSWORD.Text = "Ganti Password"
        '
        '_82_USER_BARU
        '
        Me._82_USER_BARU.Name = "_82_USER_BARU"
        Me._82_USER_BARU.Size = New System.Drawing.Size(200, 22)
        Me._82_USER_BARU.Text = "User Baru"
        '
        '_83_PENGATURAN_USER
        '
        Me._83_PENGATURAN_USER.Name = "_83_PENGATURAN_USER"
        Me._83_PENGATURAN_USER.Size = New System.Drawing.Size(200, 22)
        Me._83_PENGATURAN_USER.Text = "Pengaturan User"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(197, 6)
        '
        '_84_COMPANY_SETUP
        '
        Me._84_COMPANY_SETUP.Name = "_84_COMPANY_SETUP"
        Me._84_COMPANY_SETUP.Size = New System.Drawing.Size(200, 22)
        Me._84_COMPANY_SETUP.Text = "Company Setup"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(197, 6)
        '
        'KirimDataKeGudangToolStripMenuItem
        '
        Me.KirimDataKeGudangToolStripMenuItem.Name = "KirimDataKeGudangToolStripMenuItem"
        Me.KirimDataKeGudangToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.KirimDataKeGudangToolStripMenuItem.Text = "Kirim data ke Gudang"
        '
        'AmbilDataDariGudangToolStripMenuItem
        '
        Me.AmbilDataDariGudangToolStripMenuItem.Name = "AmbilDataDariGudangToolStripMenuItem"
        Me.AmbilDataDariGudangToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.AmbilDataDariGudangToolStripMenuItem.Text = "Ambil data dari Gudang"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(197, 6)
        '
        'KirimDataKeWaruToolStripMenuItem
        '
        Me.KirimDataKeWaruToolStripMenuItem.Name = "KirimDataKeWaruToolStripMenuItem"
        Me.KirimDataKeWaruToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.KirimDataKeWaruToolStripMenuItem.Text = "Kirim data ke Waru"
        '
        'AmbilDataDariWaruToolStripMenuItem
        '
        Me.AmbilDataDariWaruToolStripMenuItem.Name = "AmbilDataDariWaruToolStripMenuItem"
        Me.AmbilDataDariWaruToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.AmbilDataDariWaruToolStripMenuItem.Text = "Ambil data dari Waru"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(197, 6)
        '
        '_8A_BACKUP_DATA
        '
        Me._8A_BACKUP_DATA.Name = "_8A_BACKUP_DATA"
        Me._8A_BACKUP_DATA.Size = New System.Drawing.Size(200, 22)
        Me._8A_BACKUP_DATA.Text = "Backup Data"
        '
        '_8B_RESTORE_DATA
        '
        Me._8B_RESTORE_DATA.Name = "_8B_RESTORE_DATA"
        Me._8B_RESTORE_DATA.Size = New System.Drawing.Size(200, 22)
        Me._8B_RESTORE_DATA.Text = "Restore Data"
        '
        'mnuKosongData
        '
        Me.mnuKosongData.Name = "mnuKosongData"
        Me.mnuKosongData.Size = New System.Drawing.Size(200, 22)
        Me.mnuKosongData.Text = "Kosong Data"
        Me.mnuKosongData.Visible = False
        '
        'mnuExit
        '
        Me.mnuExit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuExit.Image = CType(resources.GetObject("mnuExit.Image"), System.Drawing.Image)
        Me.mnuExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuExit.Size = New System.Drawing.Size(61, 26)
        Me.mnuExit.Text = "E&xit"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "BG01.jpg")
        Me.ImageList1.Images.SetKeyName(1, "BG02.jpg")
        Me.ImageList1.Images.SetKeyName(2, "BG03.jpg")
        Me.ImageList1.Images.SetKeyName(3, "BG04.jpg")
        Me.ImageList1.Images.SetKeyName(4, "BG05.jpg")
        Me.ImageList1.Images.SetKeyName(5, "BG06.jpg")
        Me.ImageList1.Images.SetKeyName(6, "BG07.jpg")
        '
        'FrmMenuUtama
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1184, 709)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmMenuUtama"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pekerti Integrated Database System"
        Me.TransparencyKey = System.Drawing.Color.Transparent
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents TsPengguna As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TsTanggal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TsJam As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSKeterangan As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TsCompany As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents _18Utility As ToolStripMenuItem
    Friend WithEvents _84_COMPANY_SETUP As ToolStripMenuItem
    Friend WithEvents Kode_Toko As ToolStripStatusLabel
    Friend WithEvents Nama_Toko As ToolStripStatusLabel
    Friend WithEvents _81_GANTI_PASSWORD As ToolStripMenuItem
    Friend WithEvents _82_USER_BARU As ToolStripMenuItem
    Friend WithEvents _83_PENGATURAN_USER As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tServer As ToolStripStatusLabel
    Friend WithEvents ServerName As ToolStripStatusLabel
    Friend WithEvents Status As ToolStripStatusLabel
    Friend WithEvents CompCode As ToolStripStatusLabel
    Friend WithEvents TipeCetakan As ToolStripStatusLabel
    Friend WithEvents mnuKosongData As ToolStripMenuItem
    Friend WithEvents UserLoginMenu As ToolStripStatusLabel
    Friend WithEvents PasswordLoginMenu As ToolStripStatusLabel
    Friend WithEvents _8A_BACKUP_DATA As ToolStripMenuItem
    Friend WithEvents _8B_RESTORE_DATA As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents KirimDataKeGudangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AmbilDataDariGudangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents KirimDataKeWaruToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AmbilDataDariWaruToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents _11Keuangan As ToolStripMenuItem
    Friend WithEvents JurnalUmumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KasBankKeluarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KasBankMasukToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InventearisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents KodeGLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaldoAKhirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents DaftarJurnalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GeneralLedgerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TrialBalanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LabaRugiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NeracaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProsesAkhirBulanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem8 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents PengajuanPotonganPerajinToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UangMukaLanjutanPelunasanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents _12BagianContoh As ToolStripMenuItem
    Friend WithEvents _21KodifikasiBahanBaku As ToolStripMenuItem
    Friend WithEvents _22KodifikasiFungsiProduk As ToolStripMenuItem
    Friend WithEvents _23KodifikasiDaerah As ToolStripMenuItem
    Friend WithEvents _24KodifikasiPerajin As ToolStripMenuItem
    Friend WithEvents _25KodifikasiProduk As ToolStripMenuItem
    Friend WithEvents _26SPDenganKode As ToolStripMenuItem
    Friend WithEvents _27DaftarPenerimaanBarang As ToolStripMenuItem
    Friend WithEvents _28Katalog As ToolStripMenuItem
    Friend WithEvents _13BagianPembelian As ToolStripMenuItem
    Friend WithEvents _31SuratPesanan As ToolStripMenuItem
    Friend WithEvents _32DPB As ToolStripMenuItem
    Friend WithEvents _33PengajuanUangMukaPerajin As ToolStripMenuItem
    Friend WithEvents _14BagianPemasaran As ToolStripMenuItem
    Friend WithEvents _41KodifikasiNegara As ToolStripMenuItem
    Friend WithEvents _42KodifikasiImportir As ToolStripMenuItem
    Friend WithEvents _43KodeProdukBuyer As ToolStripMenuItem
    Friend WithEvents _44PurchasingOrder As ToolStripMenuItem
    Friend WithEvents _45ProformaInvoice As ToolStripMenuItem
    Friend WithEvents _46PembayaranDariBuyer As ToolStripMenuItem
    Friend WithEvents _15BagianGudang As ToolStripMenuItem
    Friend WithEvents _51PraLHP As ToolStripMenuItem
    Friend WithEvents _52LHP As ToolStripMenuItem
    Friend WithEvents _53DraftPackingListDPL As ToolStripMenuItem
    Friend WithEvents _54JenisBox As ToolStripMenuItem
    Friend WithEvents _55PackingListInvoice As ToolStripMenuItem
    Friend WithEvents MetodePengirimanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReturBarangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents _16Monitoring As ToolStripMenuItem
    Friend WithEvents ScheduleShipmentDateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DaftarSPYangBelumDiPraLHPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DaftarPOYangBelumDiBuatSPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents _17AnalisaData As ToolStripMenuItem
    Friend WithEvents PemasaranToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GudangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PembelianToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BagianContohToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KeuanganPerajinToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
End Class
