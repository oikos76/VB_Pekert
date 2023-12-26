<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KodifPerajin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KodifPerajin))
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageFormEntry_ = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.PanelEntry = New System.Windows.Forms.Panel()
        Me.KodeGLHutang = New System.Windows.Forms.TextBox()
        Me.KodeGLPiutang = New System.Windows.Forms.TextBox()
        Me.chkProduksi = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tglMasuk = New System.Windows.Forms.DateTimePicker()
        Me.lastUpd = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.Fax = New System.Windows.Forms.TextBox()
        Me.Telepon = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CmbWilayah = New System.Windows.Forms.ComboBox()
        Me.Bank = New System.Windows.Forms.TextBox()
        Me.KodePerajin = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Rekening = New System.Windows.Forms.TextBox()
        Me.NamaPerajin = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Alamat = New System.Windows.Forms.TextBox()
        Me.Catatan = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TabPageDaftar_ = New System.Windows.Forms.TabPage()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnCari = New System.Windows.Forms.Button()
        Me.tCari = New System.Windows.Forms.TextBox()
        Me.Cari = New System.Windows.Forms.Label()
        Me.btnGenerateCOAPerajin = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPageFormEntry_.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.PanelEntry.SuspendLayout()
        Me.TabPageDaftar_.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageFormEntry_)
        Me.TabControl1.Controls.Add(Me.TabPageDaftar_)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1004, 601)
        Me.TabControl1.TabIndex = 63
        '
        'TabPageFormEntry_
        '
        Me.TabPageFormEntry_.Controls.Add(Me.Panel1)
        Me.TabPageFormEntry_.Controls.Add(Me.Label25)
        Me.TabPageFormEntry_.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageFormEntry_.Location = New System.Drawing.Point(4, 24)
        Me.TabPageFormEntry_.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPageFormEntry_.Name = "TabPageFormEntry_"
        Me.TabPageFormEntry_.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPageFormEntry_.Size = New System.Drawing.Size(996, 573)
        Me.TabPageFormEntry_.TabIndex = 1
        Me.TabPageFormEntry_.Text = "Entry Data Perajin"
        Me.TabPageFormEntry_.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.PanelEntry)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(988, 565)
        Me.Panel1.TabIndex = 145
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btnGenerateCOAPerajin)
        Me.Panel4.Controls.Add(Me.cmdSimpan)
        Me.Panel4.Controls.Add(Me.cmdEdit)
        Me.Panel4.Controls.Add(Me.cmdHapus)
        Me.Panel4.Controls.Add(Me.cmdTambah)
        Me.Panel4.Controls.Add(Me.cmdBatal)
        Me.Panel4.Controls.Add(Me.cmdExit)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 527)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(986, 36)
        Me.Panel4.TabIndex = 146
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(895, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(89, 34)
        Me.cmdSimpan.TabIndex = 60
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(88, 3)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 28)
        Me.cmdEdit.TabIndex = 12
        Me.cmdEdit.Text = "&Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(171, 3)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(77, 28)
        Me.cmdHapus.TabIndex = 13
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(5, 3)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(77, 28)
        Me.cmdTambah.TabIndex = 11
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(337, 3)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 28)
        Me.cmdBatal.TabIndex = 15
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(254, 3)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 28)
        Me.cmdExit.TabIndex = 14
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'PanelEntry
        '
        Me.PanelEntry.Controls.Add(Me.KodeGLHutang)
        Me.PanelEntry.Controls.Add(Me.KodeGLPiutang)
        Me.PanelEntry.Controls.Add(Me.chkProduksi)
        Me.PanelEntry.Controls.Add(Me.Label8)
        Me.PanelEntry.Controls.Add(Me.Label5)
        Me.PanelEntry.Controls.Add(Me.tglMasuk)
        Me.PanelEntry.Controls.Add(Me.lastUpd)
        Me.PanelEntry.Controls.Add(Me.Label19)
        Me.PanelEntry.Controls.Add(Me.Label20)
        Me.PanelEntry.Controls.Add(Me.Label21)
        Me.PanelEntry.Controls.Add(Me.Email)
        Me.PanelEntry.Controls.Add(Me.Fax)
        Me.PanelEntry.Controls.Add(Me.Telepon)
        Me.PanelEntry.Controls.Add(Me.Label2)
        Me.PanelEntry.Controls.Add(Me.Label16)
        Me.PanelEntry.Controls.Add(Me.Label17)
        Me.PanelEntry.Controls.Add(Me.Label6)
        Me.PanelEntry.Controls.Add(Me.Label12)
        Me.PanelEntry.Controls.Add(Me.Label13)
        Me.PanelEntry.Controls.Add(Me.Label10)
        Me.PanelEntry.Controls.Add(Me.CmbWilayah)
        Me.PanelEntry.Controls.Add(Me.Bank)
        Me.PanelEntry.Controls.Add(Me.KodePerajin)
        Me.PanelEntry.Controls.Add(Me.Label3)
        Me.PanelEntry.Controls.Add(Me.Label7)
        Me.PanelEntry.Controls.Add(Me.Rekening)
        Me.PanelEntry.Controls.Add(Me.NamaPerajin)
        Me.PanelEntry.Controls.Add(Me.Label4)
        Me.PanelEntry.Controls.Add(Me.Alamat)
        Me.PanelEntry.Controls.Add(Me.Catatan)
        Me.PanelEntry.Location = New System.Drawing.Point(0, 0)
        Me.PanelEntry.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelEntry.Name = "PanelEntry"
        Me.PanelEntry.Size = New System.Drawing.Size(986, 524)
        Me.PanelEntry.TabIndex = 174
        '
        'KodeGLHutang
        '
        Me.KodeGLHutang.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.KodeGLHutang.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeGLHutang.Location = New System.Drawing.Point(178, 496)
        Me.KodeGLHutang.Margin = New System.Windows.Forms.Padding(4)
        Me.KodeGLHutang.Name = "KodeGLHutang"
        Me.KodeGLHutang.ReadOnly = True
        Me.KodeGLHutang.Size = New System.Drawing.Size(153, 22)
        Me.KodeGLHutang.TabIndex = 191
        '
        'KodeGLPiutang
        '
        Me.KodeGLPiutang.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.KodeGLPiutang.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeGLPiutang.Location = New System.Drawing.Point(178, 466)
        Me.KodeGLPiutang.Margin = New System.Windows.Forms.Padding(4)
        Me.KodeGLPiutang.Name = "KodeGLPiutang"
        Me.KodeGLPiutang.ReadOnly = True
        Me.KodeGLPiutang.Size = New System.Drawing.Size(153, 22)
        Me.KodeGLPiutang.TabIndex = 190
        '
        'chkProduksi
        '
        Me.chkProduksi.AutoSize = True
        Me.chkProduksi.Location = New System.Drawing.Point(178, 346)
        Me.chkProduksi.Name = "chkProduksi"
        Me.chkProduksi.Size = New System.Drawing.Size(43, 20)
        Me.chkProduksi.TabIndex = 189
        Me.chkProduksi.Text = "YA"
        Me.chkProduksi.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 499)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(160, 16)
        Me.Label8.TabIndex = 188
        Me.Label8.Text = "Kode GL-Hutang    :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 469)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(160, 16)
        Me.Label5.TabIndex = 187
        Me.Label5.Text = "Kode GL-Piutang   :"
        '
        'tglMasuk
        '
        Me.tglMasuk.CustomFormat = "dd-MM-yyyy"
        Me.tglMasuk.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tglMasuk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tglMasuk.Location = New System.Drawing.Point(178, 315)
        Me.tglMasuk.Margin = New System.Windows.Forms.Padding(4)
        Me.tglMasuk.Name = "tglMasuk"
        Me.tglMasuk.Size = New System.Drawing.Size(153, 24)
        Me.tglMasuk.TabIndex = 186
        '
        'lastUpd
        '
        Me.lastUpd.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lastUpd.Location = New System.Drawing.Point(178, 436)
        Me.lastUpd.Margin = New System.Windows.Forms.Padding(4)
        Me.lastUpd.Name = "lastUpd"
        Me.lastUpd.Size = New System.Drawing.Size(153, 22)
        Me.lastUpd.TabIndex = 185
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(10, 439)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(160, 16)
        Me.Label19.TabIndex = 184
        Me.Label19.Text = "Tgl.Terakhir Edit :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(8, 346)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(160, 16)
        Me.Label20.TabIndex = 182
        Me.Label20.Text = "Masih Produksi    :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(10, 318)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(160, 16)
        Me.Label21.TabIndex = 183
        Me.Label21.Text = "Tahun Masuk       :"
        '
        'Email
        '
        Me.Email.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Email.Location = New System.Drawing.Point(180, 162)
        Me.Email.Margin = New System.Windows.Forms.Padding(4)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(253, 22)
        Me.Email.TabIndex = 180
        '
        'Fax
        '
        Me.Fax.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fax.Location = New System.Drawing.Point(180, 222)
        Me.Fax.Margin = New System.Windows.Forms.Padding(4)
        Me.Fax.Name = "Fax"
        Me.Fax.Size = New System.Drawing.Size(253, 22)
        Me.Fax.TabIndex = 179
        '
        'Telepon
        '
        Me.Telepon.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Telepon.Location = New System.Drawing.Point(180, 192)
        Me.Telepon.Margin = New System.Windows.Forms.Padding(4)
        Me.Telepon.Name = "Telepon"
        Me.Telepon.Size = New System.Drawing.Size(253, 22)
        Me.Telepon.TabIndex = 178
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 195)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 16)
        Me.Label2.TabIndex = 174
        Me.Label2.Text = "Telepon           :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(10, 224)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(160, 16)
        Me.Label16.TabIndex = 175
        Me.Label16.Text = "Fax               :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(10, 165)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(160, 16)
        Me.Label17.TabIndex = 176
        Me.Label17.Text = "Email             :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 17)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 16)
        Me.Label6.TabIndex = 153
        Me.Label6.Text = "Wilayah Produksi  :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(10, 285)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(160, 16)
        Me.Label12.TabIndex = 164
        Me.Label12.Text = "Rekening          :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(10, 376)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(160, 16)
        Me.Label13.TabIndex = 165
        Me.Label13.Text = "Catatan           :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(10, 255)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(160, 16)
        Me.Label10.TabIndex = 163
        Me.Label10.Text = "Bank              :"
        '
        'CmbWilayah
        '
        Me.CmbWilayah.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbWilayah.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbWilayah.FormattingEnabled = True
        Me.CmbWilayah.Location = New System.Drawing.Point(180, 14)
        Me.CmbWilayah.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbWilayah.Name = "CmbWilayah"
        Me.CmbWilayah.Size = New System.Drawing.Size(378, 24)
        Me.CmbWilayah.TabIndex = 166
        '
        'Bank
        '
        Me.Bank.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bank.Location = New System.Drawing.Point(180, 252)
        Me.Bank.Margin = New System.Windows.Forms.Padding(4)
        Me.Bank.Name = "Bank"
        Me.Bank.Size = New System.Drawing.Size(253, 22)
        Me.Bank.TabIndex = 172
        '
        'KodePerajin
        '
        Me.KodePerajin.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodePerajin.Location = New System.Drawing.Point(180, 45)
        Me.KodePerajin.Margin = New System.Windows.Forms.Padding(4)
        Me.KodePerajin.MaxLength = 4
        Me.KodePerajin.Name = "KodePerajin"
        Me.KodePerajin.Size = New System.Drawing.Size(118, 22)
        Me.KodePerajin.TabIndex = 167
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 48)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 16)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "Kode Perajin      :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 109)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(160, 16)
        Me.Label7.TabIndex = 161
        Me.Label7.Text = "Alamat            :"
        '
        'Rekening
        '
        Me.Rekening.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Rekening.Location = New System.Drawing.Point(180, 282)
        Me.Rekening.Margin = New System.Windows.Forms.Padding(4)
        Me.Rekening.Multiline = True
        Me.Rekening.Name = "Rekening"
        Me.Rekening.Size = New System.Drawing.Size(253, 25)
        Me.Rekening.TabIndex = 171
        '
        'NamaPerajin
        '
        Me.NamaPerajin.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaPerajin.Location = New System.Drawing.Point(180, 75)
        Me.NamaPerajin.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaPerajin.Name = "NamaPerajin"
        Me.NamaPerajin.Size = New System.Drawing.Size(380, 22)
        Me.NamaPerajin.TabIndex = 168
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 78)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 16)
        Me.Label4.TabIndex = 156
        Me.Label4.Text = "Nama              :"
        '
        'Alamat
        '
        Me.Alamat.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Alamat.Location = New System.Drawing.Point(180, 106)
        Me.Alamat.Margin = New System.Windows.Forms.Padding(4)
        Me.Alamat.Multiline = True
        Me.Alamat.Name = "Alamat"
        Me.Alamat.Size = New System.Drawing.Size(380, 50)
        Me.Alamat.TabIndex = 169
        '
        'Catatan
        '
        Me.Catatan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Catatan.Location = New System.Drawing.Point(178, 373)
        Me.Catatan.Margin = New System.Windows.Forms.Padding(4)
        Me.Catatan.Multiline = True
        Me.Catatan.Name = "Catatan"
        Me.Catatan.Size = New System.Drawing.Size(380, 55)
        Me.Catatan.TabIndex = 173
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 46)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 16)
        Me.Label1.TabIndex = 146
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(8, 446)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(0, 16)
        Me.Label25.TabIndex = 106
        '
        'TabPageDaftar_
        '
        Me.TabPageDaftar_.Controls.Add(Me.DGView)
        Me.TabPageDaftar_.Controls.Add(Me.Panel3)
        Me.TabPageDaftar_.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageDaftar_.Location = New System.Drawing.Point(4, 24)
        Me.TabPageDaftar_.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPageDaftar_.Name = "TabPageDaftar_"
        Me.TabPageDaftar_.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPageDaftar_.Size = New System.Drawing.Size(996, 573)
        Me.TabPageDaftar_.TabIndex = 0
        Me.TabPageDaftar_.Text = "Daftar Perajin"
        Me.TabPageDaftar_.UseVisualStyleBackColor = True
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column5, Me.Column12, Me.Column10, Me.Column11, Me.Column6, Me.Column7, Me.Column15, Me.Column14, Me.Column9, Me.Column16, Me.Column8, Me.Column13})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.Location = New System.Drawing.Point(4, 45)
        Me.DGView.Margin = New System.Windows.Forms.Padding(4)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGView.RowTemplate.ReadOnly = True
        Me.DGView.Size = New System.Drawing.Size(988, 524)
        Me.DGView.TabIndex = 9
        '
        'Column1
        '
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column1.HeaderText = "Nama"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 200
        '
        'Column2
        '
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle11
        Me.Column2.HeaderText = "Kode Perajin"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 150
        '
        'Column3
        '
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle12
        Me.Column3.HeaderText = "Wilayah"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 150
        '
        'Column5
        '
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle13
        Me.Column5.HeaderText = "Alamat"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 200
        '
        'Column12
        '
        Me.Column12.HeaderText = "Email"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Telepon"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "Fax"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column6
        '
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle14
        Me.Column6.HeaderText = "Bank"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 200
        '
        'Column7
        '
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle15
        Me.Column7.HeaderText = "Rekening"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 150
        '
        'Column15
        '
        Me.Column15.HeaderText = "Tgl Masuk"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        Me.Column15.Width = 120
        '
        'Column14
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column14.DefaultCellStyle = DataGridViewCellStyle16
        Me.Column14.HeaderText = "Masih Produksi (Y/N)"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        Me.Column14.Width = 200
        '
        'Column9
        '
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column9.DefaultCellStyle = DataGridViewCellStyle17
        Me.Column9.HeaderText = "Catatan"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 200
        '
        'Column16
        '
        Me.Column16.HeaderText = "Last Update"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        Me.Column16.Width = 150
        '
        'Column8
        '
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle18
        Me.Column8.HeaderText = "Kode GL Piutang"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 200
        '
        'Column13
        '
        Me.Column13.HeaderText = "Kode GL Hutang"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Width = 180
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnCari)
        Me.Panel3.Controls.Add(Me.tCari)
        Me.Panel3.Controls.Add(Me.Cari)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(988, 41)
        Me.Panel3.TabIndex = 0
        '
        'btnCari
        '
        Me.btnCari.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCari.Image = CType(resources.GetObject("btnCari.Image"), System.Drawing.Image)
        Me.btnCari.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCari.Location = New System.Drawing.Point(904, 0)
        Me.btnCari.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(82, 39)
        Me.btnCari.TabIndex = 155
        Me.btnCari.Text = "Cari"
        Me.btnCari.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCari.UseVisualStyleBackColor = True
        '
        'tCari
        '
        Me.tCari.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tCari.Location = New System.Drawing.Point(84, 8)
        Me.tCari.Margin = New System.Windows.Forms.Padding(4)
        Me.tCari.Name = "tCari"
        Me.tCari.Size = New System.Drawing.Size(235, 22)
        Me.tCari.TabIndex = 50
        '
        'Cari
        '
        Me.Cari.AutoSize = True
        Me.Cari.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cari.Location = New System.Drawing.Point(20, 11)
        Me.Cari.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Cari.Name = "Cari"
        Me.Cari.Size = New System.Drawing.Size(56, 16)
        Me.Cari.TabIndex = 48
        Me.Cari.Text = "Nama :"
        '
        'btnGenerateCOAPerajin
        '
        Me.btnGenerateCOAPerajin.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnGenerateCOAPerajin.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateCOAPerajin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateCOAPerajin.Location = New System.Drawing.Point(789, 0)
        Me.btnGenerateCOAPerajin.Name = "btnGenerateCOAPerajin"
        Me.btnGenerateCOAPerajin.Size = New System.Drawing.Size(106, 34)
        Me.btnGenerateCOAPerajin.TabIndex = 61
        Me.btnGenerateCOAPerajin.Text = "Generate COA Perajin"
        Me.btnGenerateCOAPerajin.UseVisualStyleBackColor = True
        '
        'Form_KodifPerajin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 601)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form_KodifPerajin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kodifikasi Perajin"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageFormEntry_.ResumeLayout(False)
        Me.TabPageFormEntry_.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.PanelEntry.ResumeLayout(False)
        Me.PanelEntry.PerformLayout()
        Me.TabPageDaftar_.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageFormEntry_ As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents PanelEntry As Panel
    Friend WithEvents KodeGLHutang As TextBox
    Friend WithEvents KodeGLPiutang As TextBox
    Friend WithEvents chkProduksi As CheckBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents tglMasuk As DateTimePicker
    Friend WithEvents lastUpd As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Email As TextBox
    Friend WithEvents Fax As TextBox
    Friend WithEvents Telepon As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents CmbWilayah As ComboBox
    Friend WithEvents Bank As TextBox
    Friend WithEvents KodePerajin As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Rekening As TextBox
    Friend WithEvents NamaPerajin As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Alamat As TextBox
    Friend WithEvents Catatan As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents TabPageDaftar_ As TabPage
    Friend WithEvents DGView As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnCari As Button
    Friend WithEvents tCari As TextBox
    Friend WithEvents Cari As Label
    Friend WithEvents btnGenerateCOAPerajin As Button
End Class
