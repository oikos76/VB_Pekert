<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KeuJurnalMasuk
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KeuJurnalMasuk))
        Me.PanelTotal_ = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TotalKredit = New System.Windows.Forms.TextBox()
        Me.TotalDebet = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LMataUang = New System.Windows.Forms.Label()
        Me.NilaiJurnal = New System.Windows.Forms.TextBox()
        Me.Kurs = New System.Windows.Forms.TextBox()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.LKurs = New System.Windows.Forms.Label()
        Me.cmbMataUang = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.NoUrut = New System.Windows.Forms.TextBox()
        Me.cmbDK = New System.Windows.Forms.ComboBox()
        Me.Jumlah = New System.Windows.Forms.TextBox()
        Me.LNilaiJurnal = New System.Windows.Forms.Label()
        Me.KetAccCode1 = New System.Windows.Forms.Label()
        Me.IDRecord = New System.Windows.Forms.TextBox()
        Me.PanelTombol_ = New System.Windows.Forms.Panel()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdDaftar = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.PanelEntry_ = New System.Windows.Forms.Panel()
        Me.NoPackingList = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NoPI = New System.Windows.Forms.TextBox()
        Me.LabelNoPO = New System.Windows.Forms.Label()
        Me.Importir = New System.Windows.Forms.TextBox()
        Me.Kode_Importir = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.AccCode1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Uraian = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TglTr = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Status = New System.Windows.Forms.Label()
        Me.PanelTotal_.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTombol_.SuspendLayout()
        Me.PanelEntry_.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTotal_
        '
        Me.PanelTotal_.Controls.Add(Me.Label7)
        Me.PanelTotal_.Controls.Add(Me.TotalKredit)
        Me.PanelTotal_.Controls.Add(Me.TotalDebet)
        Me.PanelTotal_.Controls.Add(Me.Label1)
        Me.PanelTotal_.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTotal_.Location = New System.Drawing.Point(0, 538)
        Me.PanelTotal_.Name = "PanelTotal_"
        Me.PanelTotal_.Size = New System.Drawing.Size(1007, 29)
        Me.PanelTotal_.TabIndex = 231
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(618, 6)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 16)
        Me.Label7.TabIndex = 225
        Me.Label7.Text = "Kredit :"
        '
        'TotalKredit
        '
        Me.TotalKredit.BackColor = System.Drawing.SystemColors.Window
        Me.TotalKredit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalKredit.Location = New System.Drawing.Point(672, 3)
        Me.TotalKredit.Margin = New System.Windows.Forms.Padding(4)
        Me.TotalKredit.MaxLength = 4
        Me.TotalKredit.Name = "TotalKredit"
        Me.TotalKredit.ReadOnly = True
        Me.TotalKredit.Size = New System.Drawing.Size(108, 22)
        Me.TotalKredit.TabIndex = 223
        Me.TotalKredit.Text = "123.456.789.000"
        '
        'TotalDebet
        '
        Me.TotalDebet.BackColor = System.Drawing.SystemColors.Window
        Me.TotalDebet.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalDebet.Location = New System.Drawing.Point(493, 3)
        Me.TotalDebet.Margin = New System.Windows.Forms.Padding(4)
        Me.TotalDebet.MaxLength = 4
        Me.TotalDebet.Name = "TotalDebet"
        Me.TotalDebet.ReadOnly = True
        Me.TotalDebet.Size = New System.Drawing.Size(104, 22)
        Me.TotalDebet.TabIndex = 222
        Me.TotalDebet.Text = "123.456.789.000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(407, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 16)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Total Debet :"
        '
        'LMataUang
        '
        Me.LMataUang.AutoSize = True
        Me.LMataUang.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LMataUang.Location = New System.Drawing.Point(313, 192)
        Me.LMataUang.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LMataUang.Name = "LMataUang"
        Me.LMataUang.Size = New System.Drawing.Size(104, 16)
        Me.LMataUang.TabIndex = 305
        Me.LMataUang.Text = "Euro  =  Rp."
        '
        'NilaiJurnal
        '
        Me.NilaiJurnal.BackColor = System.Drawing.SystemColors.Window
        Me.NilaiJurnal.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NilaiJurnal.Location = New System.Drawing.Point(171, 188)
        Me.NilaiJurnal.Margin = New System.Windows.Forms.Padding(4)
        Me.NilaiJurnal.MaxLength = 50
        Me.NilaiJurnal.Name = "NilaiJurnal"
        Me.NilaiJurnal.ReadOnly = True
        Me.NilaiJurnal.Size = New System.Drawing.Size(134, 22)
        Me.NilaiJurnal.TabIndex = 304
        '
        'Kurs
        '
        Me.Kurs.BackColor = System.Drawing.SystemColors.Window
        Me.Kurs.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kurs.Location = New System.Drawing.Point(424, 163)
        Me.Kurs.Margin = New System.Windows.Forms.Padding(4)
        Me.Kurs.MaxLength = 50
        Me.Kurs.Name = "Kurs"
        Me.Kurs.ReadOnly = True
        Me.Kurs.Size = New System.Drawing.Size(137, 22)
        Me.Kurs.TabIndex = 303
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column9, Me.Column10, Me.Column11, Me.Column12, Me.Column3, Me.Column4, Me.Column5, Me.Column13, Me.Column1, Me.Column2})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.Location = New System.Drawing.Point(0, 227)
        Me.DGView.Margin = New System.Windows.Forms.Padding(4)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.ReadOnly = True
        Me.DGView.Size = New System.Drawing.Size(1007, 311)
        Me.DGView.TabIndex = 232
        '
        'Column9
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column9.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column9.HeaderText = "No."
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 50
        '
        'Column10
        '
        Me.Column10.HeaderText = "Uraian"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 180
        '
        'Column11
        '
        Me.Column11.HeaderText = "Account Code"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 110
        '
        'Column12
        '
        Me.Column12.HeaderText = "Ket.Account"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 170
        '
        'Column3
        '
        Me.Column3.HeaderText = "Mata Uang"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 90
        '
        'Column4
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column4.HeaderText = "Kurs"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 80
        '
        'Column5
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column5.HeaderText = "Jumlah"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column13
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column13.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column13.HeaderText = "Debet"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column1
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column1.HeaderText = "Kredit"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Hapus"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'LKurs
        '
        Me.LKurs.AutoSize = True
        Me.LKurs.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LKurs.Location = New System.Drawing.Point(312, 165)
        Me.LKurs.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LKurs.Name = "LKurs"
        Me.LKurs.Size = New System.Drawing.Size(104, 16)
        Me.LKurs.TabIndex = 302
        Me.LKurs.Text = "Nilai Kurs :"
        '
        'cmbMataUang
        '
        Me.cmbMataUang.BackColor = System.Drawing.SystemColors.Window
        Me.cmbMataUang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMataUang.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMataUang.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbMataUang.FormattingEnabled = True
        Me.cmbMataUang.Location = New System.Drawing.Point(171, 161)
        Me.cmbMataUang.Name = "cmbMataUang"
        Me.cmbMataUang.Size = New System.Drawing.Size(134, 24)
        Me.cmbMataUang.TabIndex = 301
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(11, 165)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(152, 16)
        Me.Label18.TabIndex = 300
        Me.Label18.Text = "Mata Uang        :"
        '
        'btnAdd
        '
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(745, 185)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(33, 28)
        Me.btnAdd.TabIndex = 233
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'NoUrut
        '
        Me.NoUrut.Location = New System.Drawing.Point(606, 161)
        Me.NoUrut.Name = "NoUrut"
        Me.NoUrut.Size = New System.Drawing.Size(51, 20)
        Me.NoUrut.TabIndex = 232
        Me.NoUrut.Visible = False
        '
        'cmbDK
        '
        Me.cmbDK.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDK.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDK.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDK.FormattingEnabled = True
        Me.cmbDK.Location = New System.Drawing.Point(606, 187)
        Me.cmbDK.Name = "cmbDK"
        Me.cmbDK.Size = New System.Drawing.Size(134, 24)
        Me.cmbDK.TabIndex = 231
        '
        'Jumlah
        '
        Me.Jumlah.BackColor = System.Drawing.SystemColors.Window
        Me.Jumlah.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Jumlah.Location = New System.Drawing.Point(424, 189)
        Me.Jumlah.Margin = New System.Windows.Forms.Padding(4)
        Me.Jumlah.MaxLength = 50
        Me.Jumlah.Name = "Jumlah"
        Me.Jumlah.ReadOnly = True
        Me.Jumlah.Size = New System.Drawing.Size(137, 22)
        Me.Jumlah.TabIndex = 226
        '
        'LNilaiJurnal
        '
        Me.LNilaiJurnal.AutoSize = True
        Me.LNilaiJurnal.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LNilaiJurnal.Location = New System.Drawing.Point(11, 191)
        Me.LNilaiJurnal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LNilaiJurnal.Name = "LNilaiJurnal"
        Me.LNilaiJurnal.Size = New System.Drawing.Size(152, 16)
        Me.LNilaiJurnal.TabIndex = 225
        Me.LNilaiJurnal.Text = "Jumlah           :"
        '
        'KetAccCode1
        '
        Me.KetAccCode1.AutoSize = True
        Me.KetAccCode1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KetAccCode1.Location = New System.Drawing.Point(313, 139)
        Me.KetAccCode1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.KetAccCode1.Name = "KetAccCode1"
        Me.KetAccCode1.Size = New System.Drawing.Size(96, 16)
        Me.KetAccCode1.TabIndex = 224
        Me.KetAccCode1.Text = "KetAccCode1"
        '
        'IDRecord
        '
        Me.IDRecord.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.IDRecord.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IDRecord.Location = New System.Drawing.Point(171, 8)
        Me.IDRecord.Margin = New System.Windows.Forms.Padding(4)
        Me.IDRecord.MaxLength = 4
        Me.IDRecord.Name = "IDRecord"
        Me.IDRecord.ReadOnly = True
        Me.IDRecord.Size = New System.Drawing.Size(137, 22)
        Me.IDRecord.TabIndex = 223
        '
        'PanelTombol_
        '
        Me.PanelTombol_.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTombol_.Controls.Add(Me.cmdExit)
        Me.PanelTombol_.Controls.Add(Me.cmdDaftar)
        Me.PanelTombol_.Controls.Add(Me.cmdPrint)
        Me.PanelTombol_.Controls.Add(Me.cmdHapus)
        Me.PanelTombol_.Controls.Add(Me.cmdEdit)
        Me.PanelTombol_.Controls.Add(Me.cmdTambah)
        Me.PanelTombol_.Controls.Add(Me.cmdBatal)
        Me.PanelTombol_.Controls.Add(Me.cmdSimpan)
        Me.PanelTombol_.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTombol_.Location = New System.Drawing.Point(0, 567)
        Me.PanelTombol_.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelTombol_.Name = "PanelTombol_"
        Me.PanelTombol_.Size = New System.Drawing.Size(1007, 34)
        Me.PanelTombol_.TabIndex = 229
        '
        'cmdExit
        '
        Me.cmdExit.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(469, 0)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 32)
        Me.cmdExit.TabIndex = 226
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdDaftar
        '
        Me.cmdDaftar.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdDaftar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDaftar.Image = CType(resources.GetObject("cmdDaftar.Image"), System.Drawing.Image)
        Me.cmdDaftar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDaftar.Location = New System.Drawing.Point(392, 0)
        Me.cmdDaftar.Name = "cmdDaftar"
        Me.cmdDaftar.Size = New System.Drawing.Size(77, 32)
        Me.cmdDaftar.TabIndex = 10
        Me.cmdDaftar.Text = "&Daftar"
        Me.cmdDaftar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdDaftar.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(315, 0)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(77, 32)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPrint.UseVisualStyleBackColor = True
        Me.cmdPrint.Visible = False
        '
        'cmdHapus
        '
        Me.cmdHapus.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdHapus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(238, 0)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(77, 32)
        Me.cmdHapus.TabIndex = 8
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(161, 0)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 32)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.Text = "&Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdTambah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(77, 0)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(84, 32)
        Me.cmdTambah.TabIndex = 0
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 32)
        Me.cmdBatal.TabIndex = 4
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        Me.cmdBatal.Visible = False
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(916, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(89, 32)
        Me.cmdSimpan.TabIndex = 225
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        Me.cmdSimpan.Visible = False
        '
        'PanelEntry_
        '
        Me.PanelEntry_.Controls.Add(Me.Status)
        Me.PanelEntry_.Controls.Add(Me.NoPackingList)
        Me.PanelEntry_.Controls.Add(Me.Label5)
        Me.PanelEntry_.Controls.Add(Me.NoPI)
        Me.PanelEntry_.Controls.Add(Me.LabelNoPO)
        Me.PanelEntry_.Controls.Add(Me.Importir)
        Me.PanelEntry_.Controls.Add(Me.Kode_Importir)
        Me.PanelEntry_.Controls.Add(Me.Label17)
        Me.PanelEntry_.Controls.Add(Me.LMataUang)
        Me.PanelEntry_.Controls.Add(Me.NilaiJurnal)
        Me.PanelEntry_.Controls.Add(Me.Kurs)
        Me.PanelEntry_.Controls.Add(Me.LKurs)
        Me.PanelEntry_.Controls.Add(Me.cmbMataUang)
        Me.PanelEntry_.Controls.Add(Me.Label18)
        Me.PanelEntry_.Controls.Add(Me.btnAdd)
        Me.PanelEntry_.Controls.Add(Me.NoUrut)
        Me.PanelEntry_.Controls.Add(Me.cmbDK)
        Me.PanelEntry_.Controls.Add(Me.Jumlah)
        Me.PanelEntry_.Controls.Add(Me.LNilaiJurnal)
        Me.PanelEntry_.Controls.Add(Me.KetAccCode1)
        Me.PanelEntry_.Controls.Add(Me.IDRecord)
        Me.PanelEntry_.Controls.Add(Me.AccCode1)
        Me.PanelEntry_.Controls.Add(Me.Label4)
        Me.PanelEntry_.Controls.Add(Me.Label3)
        Me.PanelEntry_.Controls.Add(Me.Uraian)
        Me.PanelEntry_.Controls.Add(Me.Label6)
        Me.PanelEntry_.Controls.Add(Me.TglTr)
        Me.PanelEntry_.Controls.Add(Me.Label2)
        Me.PanelEntry_.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEntry_.Location = New System.Drawing.Point(0, 0)
        Me.PanelEntry_.Name = "PanelEntry_"
        Me.PanelEntry_.Size = New System.Drawing.Size(1007, 227)
        Me.PanelEntry_.TabIndex = 230
        '
        'NoPackingList
        '
        Me.NoPackingList.BackColor = System.Drawing.SystemColors.Window
        Me.NoPackingList.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoPackingList.Location = New System.Drawing.Point(315, 111)
        Me.NoPackingList.Margin = New System.Windows.Forms.Padding(4)
        Me.NoPackingList.Name = "NoPackingList"
        Me.NoPackingList.ReadOnly = True
        Me.NoPackingList.Size = New System.Drawing.Size(134, 22)
        Me.NoPackingList.TabIndex = 363
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(292, 114)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 362
        Me.Label5.Text = "/"
        '
        'NoPI
        '
        Me.NoPI.BackColor = System.Drawing.SystemColors.Window
        Me.NoPI.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoPI.Location = New System.Drawing.Point(171, 111)
        Me.NoPI.Margin = New System.Windows.Forms.Padding(4)
        Me.NoPI.Name = "NoPI"
        Me.NoPI.ReadOnly = True
        Me.NoPI.Size = New System.Drawing.Size(113, 22)
        Me.NoPI.TabIndex = 314
        '
        'LabelNoPO
        '
        Me.LabelNoPO.AutoSize = True
        Me.LabelNoPO.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNoPO.Location = New System.Drawing.Point(11, 114)
        Me.LabelNoPO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelNoPO.Name = "LabelNoPO"
        Me.LabelNoPO.Size = New System.Drawing.Size(152, 16)
        Me.LabelNoPO.TabIndex = 313
        Me.LabelNoPO.Text = "No. PI/Pack.List :"
        '
        'Importir
        '
        Me.Importir.BackColor = System.Drawing.SystemColors.Window
        Me.Importir.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Importir.Location = New System.Drawing.Point(228, 85)
        Me.Importir.Margin = New System.Windows.Forms.Padding(4)
        Me.Importir.MaxLength = 4
        Me.Importir.Name = "Importir"
        Me.Importir.ReadOnly = True
        Me.Importir.Size = New System.Drawing.Size(270, 22)
        Me.Importir.TabIndex = 307
        '
        'Kode_Importir
        '
        Me.Kode_Importir.BackColor = System.Drawing.SystemColors.Window
        Me.Kode_Importir.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kode_Importir.Location = New System.Drawing.Point(171, 85)
        Me.Kode_Importir.Margin = New System.Windows.Forms.Padding(4)
        Me.Kode_Importir.Name = "Kode_Importir"
        Me.Kode_Importir.ReadOnly = True
        Me.Kode_Importir.Size = New System.Drawing.Size(49, 22)
        Me.Kode_Importir.TabIndex = 306
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(11, 88)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(152, 16)
        Me.Label17.TabIndex = 308
        Me.Label17.Text = "Importir         :"
        '
        'AccCode1
        '
        Me.AccCode1.BackColor = System.Drawing.SystemColors.Window
        Me.AccCode1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccCode1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.AccCode1.Location = New System.Drawing.Point(171, 136)
        Me.AccCode1.Margin = New System.Windows.Forms.Padding(4)
        Me.AccCode1.MaxLength = 255
        Me.AccCode1.Name = "AccCode1"
        Me.AccCode1.ReadOnly = True
        Me.AccCode1.Size = New System.Drawing.Size(134, 22)
        Me.AccCode1.TabIndex = 221
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 11)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 16)
        Me.Label4.TabIndex = 220
        Me.Label4.Text = "No. Jurnal       :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 139)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 16)
        Me.Label3.TabIndex = 218
        Me.Label3.Text = "No. Perkiraan    :"
        '
        'Uraian
        '
        Me.Uraian.BackColor = System.Drawing.SystemColors.Window
        Me.Uraian.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Uraian.Location = New System.Drawing.Point(171, 60)
        Me.Uraian.Margin = New System.Windows.Forms.Padding(4)
        Me.Uraian.MaxLength = 100
        Me.Uraian.Name = "Uraian"
        Me.Uraian.ReadOnly = True
        Me.Uraian.Size = New System.Drawing.Size(616, 22)
        Me.Uraian.TabIndex = 216
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 63)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 16)
        Me.Label6.TabIndex = 217
        Me.Label6.Text = "Keterangan       :"
        '
        'TglTr
        '
        Me.TglTr.CustomFormat = "dd-MM-yyyy"
        Me.TglTr.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglTr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TglTr.Location = New System.Drawing.Point(171, 33)
        Me.TglTr.Margin = New System.Windows.Forms.Padding(4)
        Me.TglTr.Name = "TglTr"
        Me.TglTr.Size = New System.Drawing.Size(137, 24)
        Me.TglTr.TabIndex = 175
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 36)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 16)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "Tanggal          :"
        '
        'Status
        '
        Me.Status.AutoSize = True
        Me.Status.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status.Location = New System.Drawing.Point(315, 11)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(51, 16)
        Me.Status.TabIndex = 364
        Me.Status.Text = "Status"
        '
        'Form_KeuJurnalMasuk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 601)
        Me.Controls.Add(Me.DGView)
        Me.Controls.Add(Me.PanelTotal_)
        Me.Controls.Add(Me.PanelTombol_)
        Me.Controls.Add(Me.PanelEntry_)
        Me.Name = "Form_KeuJurnalMasuk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jurnal Kas/Bank Masuk"
        Me.PanelTotal_.ResumeLayout(False)
        Me.PanelTotal_.PerformLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTombol_.ResumeLayout(False)
        Me.PanelEntry_.ResumeLayout(False)
        Me.PanelEntry_.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTotal_ As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents TotalKredit As TextBox
    Friend WithEvents TotalDebet As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LMataUang As Label
    Friend WithEvents NilaiJurnal As TextBox
    Friend WithEvents Kurs As TextBox
    Friend WithEvents DGView As DataGridView
    Friend WithEvents LKurs As Label
    Friend WithEvents cmbMataUang As ComboBox
    Friend WithEvents Label18 As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents NoUrut As TextBox
    Friend WithEvents cmbDK As ComboBox
    Friend WithEvents Jumlah As TextBox
    Friend WithEvents LNilaiJurnal As Label
    Friend WithEvents KetAccCode1 As Label
    Friend WithEvents IDRecord As TextBox
    Friend WithEvents PanelTombol_ As Panel
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdDaftar As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents PanelEntry_ As Panel
    Friend WithEvents AccCode1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Uraian As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TglTr As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Importir As TextBox
    Friend WithEvents Kode_Importir As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents NoPI As TextBox
    Friend WithEvents LabelNoPO As Label
    Friend WithEvents NoPackingList As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewButtonColumn
    Friend WithEvents Status As Label
End Class
