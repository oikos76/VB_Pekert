<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KoreksiStock
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KoreksiStock))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdCetak = New System.Windows.Forms.Button()
        Me.Cari = New System.Windows.Forms.Label()
        Me.tCari = New System.Windows.Forms.TextBox()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.ID_Request = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TglKoreksi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KodeBrg_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NamaBrg_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QtyKoreksi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlasanKoreksi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.DGRequest = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty_K = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Satuan_K = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Harga_Sat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sub_Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HargaSatuan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SubTotal = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.NamaToko = New System.Windows.Forms.Label()
        Me.kodeToko = New System.Windows.Forms.TextBox()
        Me.IDRec = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TglKoreksi = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.DGRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdCetak)
        Me.Panel1.Controls.Add(Me.Cari)
        Me.Panel1.Controls.Add(Me.tCari)
        Me.Panel1.Controls.Add(Me.cmdBatal)
        Me.Panel1.Controls.Add(Me.cmdSimpan)
        Me.Panel1.Controls.Add(Me.cmdExit)
        Me.Panel1.Controls.Add(Me.cmdTambah)
        Me.Panel1.Controls.Add(Me.cmdHapus)
        Me.Panel1.Controls.Add(Me.cmdEdit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(93, 556)
        Me.Panel1.TabIndex = 0
        '
        'cmdCetak
        '
        Me.cmdCetak.Image = CType(resources.GetObject("cmdCetak.Image"), System.Drawing.Image)
        Me.cmdCetak.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCetak.Location = New System.Drawing.Point(5, 148)
        Me.cmdCetak.Name = "cmdCetak"
        Me.cmdCetak.Size = New System.Drawing.Size(77, 28)
        Me.cmdCetak.TabIndex = 85
        Me.cmdCetak.Text = "Cetak"
        Me.cmdCetak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCetak.UseVisualStyleBackColor = True
        '
        'Cari
        '
        Me.Cari.AutoSize = True
        Me.Cari.Location = New System.Drawing.Point(2, 15)
        Me.Cari.Name = "Cari"
        Me.Cari.Size = New System.Drawing.Size(63, 13)
        Me.Cari.TabIndex = 77
        Me.Cari.Text = "Cari Koreksi"
        '
        'tCari
        '
        Me.tCari.Location = New System.Drawing.Point(10, 34)
        Me.tCari.Name = "tCari"
        Me.tCari.Size = New System.Drawing.Size(73, 20)
        Me.tCari.TabIndex = 79
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(5, 524)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 28)
        Me.cmdBatal.TabIndex = 80
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        Me.cmdBatal.Visible = False
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(5, 214)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(77, 28)
        Me.cmdSimpan.TabIndex = 78
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        Me.cmdSimpan.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(5, 180)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 28)
        Me.cmdExit.TabIndex = 84
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(6, 84)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(77, 28)
        Me.cmdTambah.TabIndex = 81
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(6, 116)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(77, 28)
        Me.cmdHapus.TabIndex = 83
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(6, 300)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 28)
        Me.cmdEdit.TabIndex = 82
        Me.cmdEdit.Text = "&Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        Me.cmdEdit.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(93, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(911, 556)
        Me.Panel2.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(911, 556)
        Me.TabControl1.TabIndex = 78
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGView)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(903, 530)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Daftar Koreksi Stock"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.AllowUserToResizeRows = False
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID_Request, Me.TglKoreksi_, Me.KodeBrg_, Me.NamaBrg_, Me.QtyKoreksi_, Me.AlasanKoreksi_})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(3, 3)
        Me.DGView.Name = "DGView"
        Me.DGView.RowHeadersVisible = False
        Me.DGView.Size = New System.Drawing.Size(897, 524)
        Me.DGView.TabIndex = 5
        '
        'ID_Request
        '
        Me.ID_Request.HeaderText = "Id Record"
        Me.ID_Request.Name = "ID_Request"
        Me.ID_Request.ReadOnly = True
        Me.ID_Request.Width = 80
        '
        'TglKoreksi_
        '
        Me.TglKoreksi_.HeaderText = "Tgl.Koreksi"
        Me.TglKoreksi_.Name = "TglKoreksi_"
        Me.TglKoreksi_.ReadOnly = True
        Me.TglKoreksi_.Width = 75
        '
        'KodeBrg_
        '
        Me.KodeBrg_.HeaderText = "Kode Brg"
        Me.KodeBrg_.Name = "KodeBrg_"
        Me.KodeBrg_.ReadOnly = True
        Me.KodeBrg_.Width = 80
        '
        'NamaBrg_
        '
        Me.NamaBrg_.HeaderText = "Nama Barang"
        Me.NamaBrg_.Name = "NamaBrg_"
        Me.NamaBrg_.ReadOnly = True
        Me.NamaBrg_.Width = 275
        '
        'QtyKoreksi_
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.QtyKoreksi_.DefaultCellStyle = DataGridViewCellStyle7
        Me.QtyKoreksi_.HeaderText = "Qty Koreksi"
        Me.QtyKoreksi_.Name = "QtyKoreksi_"
        Me.QtyKoreksi_.ReadOnly = True
        '
        'AlasanKoreksi_
        '
        Me.AlasanKoreksi_.HeaderText = "Alasan Koreksi"
        Me.AlasanKoreksi_.Name = "AlasanKoreksi_"
        Me.AlasanKoreksi_.ReadOnly = True
        Me.AlasanKoreksi_.Width = 200
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel5)
        Me.TabPage2.Controls.Add(Me.Panel4)
        Me.TabPage2.Controls.Add(Me.Panel3)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(903, 530)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Koreksi Stock"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Controls.Add(Me.DGRequest)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 83)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(897, 413)
        Me.Panel5.TabIndex = 145
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(0, 273)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(0, 13)
        Me.Label25.TabIndex = 106
        '
        'DGRequest
        '
        Me.DGRequest.AllowDrop = True
        Me.DGRequest.AllowUserToOrderColumns = True
        Me.DGRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGRequest.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.Qty_K, Me.Satuan_K, Me.Harga_Sat, Me.Sub_Total, Me.HargaSatuan_, Me.Column1, Me.Column2})
        Me.DGRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGRequest.Location = New System.Drawing.Point(0, 0)
        Me.DGRequest.Name = "DGRequest"
        Me.DGRequest.RowHeadersVisible = False
        Me.DGRequest.RowTemplate.Height = 24
        Me.DGRequest.Size = New System.Drawing.Size(897, 413)
        Me.DGRequest.TabIndex = 138
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Kode Barang"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Nama Barang"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 250
        '
        'Qty_K
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Qty_K.DefaultCellStyle = DataGridViewCellStyle8
        Me.Qty_K.HeaderText = "QTY Computer"
        Me.Qty_K.Name = "Qty_K"
        Me.Qty_K.ReadOnly = True
        '
        'Satuan_K
        '
        Me.Satuan_K.HeaderText = "Satuan"
        Me.Satuan_K.Name = "Satuan_K"
        Me.Satuan_K.ReadOnly = True
        Me.Satuan_K.Width = 80
        '
        'Harga_Sat
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Harga_Sat.DefaultCellStyle = DataGridViewCellStyle9
        Me.Harga_Sat.HeaderText = "QTY Fisik"
        Me.Harga_Sat.Name = "Harga_Sat"
        '
        'Sub_Total
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Sub_Total.DefaultCellStyle = DataGridViewCellStyle10
        Me.Sub_Total.HeaderText = "QTY Koreksi"
        Me.Sub_Total.Name = "Sub_Total"
        Me.Sub_Total.ReadOnly = True
        '
        'HargaSatuan_
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.HargaSatuan_.DefaultCellStyle = DataGridViewCellStyle11
        Me.HargaSatuan_.HeaderText = "Harga Jual"
        Me.HargaSatuan_.Name = "HargaSatuan_"
        Me.HargaSatuan_.ReadOnly = True
        '
        'Column1
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle12
        Me.Column1.HeaderText = "Sub Total"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Alasan"
        Me.Column2.Name = "Column2"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.SubTotal)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(3, 496)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(897, 31)
        Me.Panel4.TabIndex = 144
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(453, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 128
        Me.Label7.Text = "Sub Total"
        '
        'SubTotal
        '
        Me.SubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.SubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubTotal.Location = New System.Drawing.Point(512, 6)
        Me.SubTotal.Name = "SubTotal"
        Me.SubTotal.ReadOnly = True
        Me.SubTotal.Size = New System.Drawing.Size(149, 20)
        Me.SubTotal.TabIndex = 12
        Me.SubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.NamaToko)
        Me.Panel3.Controls.Add(Me.kodeToko)
        Me.Panel3.Controls.Add(Me.IDRec)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.TglKoreksi)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(897, 80)
        Me.Panel3.TabIndex = 143
        '
        'NamaToko
        '
        Me.NamaToko.AutoSize = True
        Me.NamaToko.Location = New System.Drawing.Point(197, 56)
        Me.NamaToko.Name = "NamaToko"
        Me.NamaToko.Size = New System.Drawing.Size(74, 13)
        Me.NamaToko.TabIndex = 144
        Me.NamaToko.Text = "Toko/Counter"
        '
        'kodeToko
        '
        Me.kodeToko.Location = New System.Drawing.Point(105, 53)
        Me.kodeToko.Name = "kodeToko"
        Me.kodeToko.Size = New System.Drawing.Size(86, 20)
        Me.kodeToko.TabIndex = 143
        '
        'IDRec
        '
        Me.IDRec.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.IDRec.Location = New System.Drawing.Point(105, 1)
        Me.IDRec.Name = "IDRec"
        Me.IDRec.ReadOnly = True
        Me.IDRec.Size = New System.Drawing.Size(87, 20)
        Me.IDRec.TabIndex = 0
        Me.IDRec.Text = "1234567890"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 142
        Me.Label8.Text = "Toko/Counter"
        '
        'TglKoreksi
        '
        Me.TglKoreksi.CustomFormat = "dd-MM-yyyy"
        Me.TglKoreksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TglKoreksi.Location = New System.Drawing.Point(105, 27)
        Me.TglKoreksi.Name = "TglKoreksi"
        Me.TglKoreksi.Size = New System.Drawing.Size(87, 20)
        Me.TglKoreksi.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "Id Koreksi"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 123
        Me.Label6.Text = "Tgl. Koreksi"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 0
        '
        'Form_KoreksiStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 556)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form_KoreksiStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Koreksi Stock"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.DGRequest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdCetak As System.Windows.Forms.Button
    Friend WithEvents Cari As System.Windows.Forms.Label
    Friend WithEvents tCari As System.Windows.Forms.TextBox
    Friend WithEvents cmdBatal As System.Windows.Forms.Button
    Friend WithEvents cmdSimpan As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdTambah As System.Windows.Forms.Button
    Friend WithEvents cmdHapus As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DGView As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DGRequest As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents SubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TglKoreksi As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents IDRec As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents kodeToko As TextBox
    Friend WithEvents NamaToko As Label
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents Qty_K As DataGridViewTextBoxColumn
    Friend WithEvents Satuan_K As DataGridViewTextBoxColumn
    Friend WithEvents Harga_Sat As DataGridViewTextBoxColumn
    Friend WithEvents Sub_Total As DataGridViewTextBoxColumn
    Friend WithEvents HargaSatuan_ As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents ID_Request As DataGridViewTextBoxColumn
    Friend WithEvents TglKoreksi_ As DataGridViewTextBoxColumn
    Friend WithEvents KodeBrg_ As DataGridViewTextBoxColumn
    Friend WithEvents NamaBrg_ As DataGridViewTextBoxColumn
    Friend WithEvents QtyKoreksi_ As DataGridViewTextBoxColumn
    Friend WithEvents AlasanKoreksi_ As DataGridViewTextBoxColumn
End Class
