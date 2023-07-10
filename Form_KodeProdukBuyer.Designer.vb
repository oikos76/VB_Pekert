<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KodeProdukBuyer
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelKanan = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cmdCari = New System.Windows.Forms.Button()
        Me.tCari = New System.Windows.Forms.TextBox()
        Me.NilaiPesanan = New System.Windows.Forms.TextBox()
        Me.PanelPicture = New System.Windows.Forms.Panel()
        Me.LocGmb1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TotalQTY = New System.Windows.Forms.TextBox()
        Me.TotalMacam = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PanelKiri = New System.Windows.Forms.Panel()
        Me.DGView2 = New System.Windows.Forms.DataGridView()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelAtas = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.KODEPRODUK_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KODE_BY_IMPORTIR_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DESKRIPSI_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.PanelKanan.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.PanelPicture.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelKiri.SuspendLayout()
        CType(Me.DGView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelAtas.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelKanan)
        Me.Panel1.Controls.Add(Me.PanelKiri)
        Me.Panel1.Controls.Add(Me.PanelAtas)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(884, 641)
        Me.Panel1.TabIndex = 0
        '
        'PanelKanan
        '
        Me.PanelKanan.Controls.Add(Me.Panel3)
        Me.PanelKanan.Controls.Add(Me.NilaiPesanan)
        Me.PanelKanan.Controls.Add(Me.PanelPicture)
        Me.PanelKanan.Controls.Add(Me.TotalQTY)
        Me.PanelKanan.Controls.Add(Me.TotalMacam)
        Me.PanelKanan.Controls.Add(Me.Label1)
        Me.PanelKanan.Controls.Add(Me.Label2)
        Me.PanelKanan.Controls.Add(Me.Label3)
        Me.PanelKanan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelKanan.Location = New System.Drawing.Point(572, 31)
        Me.PanelKanan.Name = "PanelKanan"
        Me.PanelKanan.Size = New System.Drawing.Size(312, 583)
        Me.PanelKanan.TabIndex = 193
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.cmdCari)
        Me.Panel3.Controls.Add(Me.tCari)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 295)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(312, 34)
        Me.Panel3.TabIndex = 197
        '
        'cmdCari
        '
        Me.cmdCari.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdCari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCari.Location = New System.Drawing.Point(0, 0)
        Me.cmdCari.Name = "cmdCari"
        Me.cmdCari.Size = New System.Drawing.Size(72, 34)
        Me.cmdCari.TabIndex = 192
        Me.cmdCari.Text = "&Cari"
        Me.cmdCari.UseVisualStyleBackColor = True
        '
        'tCari
        '
        Me.tCari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tCari.Location = New System.Drawing.Point(78, 6)
        Me.tCari.Name = "tCari"
        Me.tCari.Size = New System.Drawing.Size(222, 22)
        Me.tCari.TabIndex = 193
        '
        'NilaiPesanan
        '
        Me.NilaiPesanan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NilaiPesanan.Location = New System.Drawing.Point(43, 470)
        Me.NilaiPesanan.Name = "NilaiPesanan"
        Me.NilaiPesanan.Size = New System.Drawing.Size(183, 22)
        Me.NilaiPesanan.TabIndex = 196
        '
        'PanelPicture
        '
        Me.PanelPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelPicture.Controls.Add(Me.LocGmb1)
        Me.PanelPicture.Controls.Add(Me.PictureBox1)
        Me.PanelPicture.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelPicture.Location = New System.Drawing.Point(0, 0)
        Me.PanelPicture.Name = "PanelPicture"
        Me.PanelPicture.Size = New System.Drawing.Size(312, 295)
        Me.PanelPicture.TabIndex = 188
        '
        'LocGmb1
        '
        Me.LocGmb1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LocGmb1.Location = New System.Drawing.Point(0, 277)
        Me.LocGmb1.Name = "LocGmb1"
        Me.LocGmb1.Size = New System.Drawing.Size(310, 16)
        Me.LocGmb1.TabIndex = 3
        Me.LocGmb1.Text = "LocGmb1"
        Me.LocGmb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(310, 293)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'TotalQTY
        '
        Me.TotalQTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalQTY.Location = New System.Drawing.Point(43, 426)
        Me.TotalQTY.Name = "TotalQTY"
        Me.TotalQTY.Size = New System.Drawing.Size(183, 22)
        Me.TotalQTY.TabIndex = 195
        '
        'TotalMacam
        '
        Me.TotalMacam.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalMacam.Location = New System.Drawing.Point(43, 382)
        Me.TotalMacam.Name = "TotalMacam"
        Me.TotalMacam.Size = New System.Drawing.Size(183, 22)
        Me.TotalMacam.TabIndex = 194
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 363)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 16)
        Me.Label1.TabIndex = 189
        Me.Label1.Text = "Total macam kode pernah di pesan buyer ini :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 407)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(184, 16)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "Total kuantitas pesanan (pcs)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 451)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(162, 16)
        Me.Label3.TabIndex = 191
        Me.Label3.Text = "Total nilai pesanan (USD)"
        '
        'PanelKiri
        '
        Me.PanelKiri.Controls.Add(Me.DGView2)
        Me.PanelKiri.Controls.Add(Me.DGView)
        Me.PanelKiri.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelKiri.Location = New System.Drawing.Point(0, 31)
        Me.PanelKiri.Name = "PanelKiri"
        Me.PanelKiri.Size = New System.Drawing.Size(572, 583)
        Me.PanelKiri.TabIndex = 192
        '
        'DGView2
        '
        Me.DGView2.AllowUserToAddRows = False
        Me.DGView2.AllowUserToDeleteRows = False
        Me.DGView2.AllowUserToOrderColumns = True
        Me.DGView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KODEPRODUK_, Me.KODE_BY_IMPORTIR_, Me.DESKRIPSI_})
        Me.DGView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView2.Location = New System.Drawing.Point(0, 272)
        Me.DGView2.Margin = New System.Windows.Forms.Padding(4)
        Me.DGView2.Name = "DGView2"
        Me.DGView2.ReadOnly = True
        Me.DGView2.RowHeadersVisible = False
        Me.DGView2.RowTemplate.ReadOnly = True
        Me.DGView2.Size = New System.Drawing.Size(572, 311)
        Me.DGView2.TabIndex = 158
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column5, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column13, Me.Column15, Me.Column16, Me.Column17, Me.Column18, Me.Column19, Me.Column20, Me.Column21, Me.Column22, Me.Column23})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Top
        Me.DGView.Location = New System.Drawing.Point(0, 0)
        Me.DGView.Margin = New System.Windows.Forms.Padding(4)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.ReadOnly = True
        Me.DGView.Size = New System.Drawing.Size(572, 272)
        Me.DGView.TabIndex = 157
        '
        'Column4
        '
        Me.Column4.HeaderText = "Nama Importir"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Kode Importir"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Negara Asal"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Jenis"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Alamat"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Alamat Kirim"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "Notify"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column15
        '
        Me.Column15.HeaderText = "Port of Discharge"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Catatan"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column17
        '
        Me.Column17.HeaderText = "Telephon"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        '
        'Column18
        '
        Me.Column18.HeaderText = "Fax"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        '
        'Column19
        '
        Me.Column19.HeaderText = "Email"
        Me.Column19.Name = "Column19"
        Me.Column19.ReadOnly = True
        '
        'Column20
        '
        Me.Column20.HeaderText = "Contact Person"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        '
        'Column21
        '
        Me.Column21.HeaderText = "Masih Membeli (Y/N)"
        Me.Column21.Name = "Column21"
        Me.Column21.ReadOnly = True
        '
        'Column22
        '
        Me.Column22.HeaderText = "Tgl Masuk"
        Me.Column22.Name = "Column22"
        Me.Column22.ReadOnly = True
        '
        'Column23
        '
        Me.Column23.HeaderText = "Tgl Terakhir Edit"
        Me.Column23.Name = "Column23"
        Me.Column23.ReadOnly = True
        '
        'PanelAtas
        '
        Me.PanelAtas.Controls.Add(Me.Label5)
        Me.PanelAtas.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAtas.Location = New System.Drawing.Point(0, 0)
        Me.PanelAtas.Name = "PanelAtas"
        Me.PanelAtas.Size = New System.Drawing.Size(884, 31)
        Me.PanelAtas.TabIndex = 190
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(884, 31)
        Me.Label5.TabIndex = 192
        Me.Label5.Text = "Kode Produk Menurut Importir"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 614)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(884, 27)
        Me.Panel2.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(884, 27)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Catatan : Data mengacu pada Proforma Invoice, dengan asumsi total pesanan terpenu" &
    "hi semua."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'KODEPRODUK_
        '
        Me.KODEPRODUK_.HeaderText = "Kode Produk"
        Me.KODEPRODUK_.Name = "KODEPRODUK_"
        Me.KODEPRODUK_.ReadOnly = True
        Me.KODEPRODUK_.Width = 130
        '
        'KODE_BY_IMPORTIR_
        '
        Me.KODE_BY_IMPORTIR_.HeaderText = "Kode Importir"
        Me.KODE_BY_IMPORTIR_.Name = "KODE_BY_IMPORTIR_"
        Me.KODE_BY_IMPORTIR_.ReadOnly = True
        Me.KODE_BY_IMPORTIR_.Width = 150
        '
        'DESKRIPSI_
        '
        Me.DESKRIPSI_.HeaderText = "Deskripsi"
        Me.DESKRIPSI_.Name = "DESKRIPSI_"
        Me.DESKRIPSI_.ReadOnly = True
        Me.DESKRIPSI_.Width = 260
        '
        'Form_KodeProdukBuyer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 641)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form_KodeProdukBuyer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kode Produk Menurut Importir"
        Me.Panel1.ResumeLayout(False)
        Me.PanelKanan.ResumeLayout(False)
        Me.PanelKanan.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.PanelPicture.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelKiri.ResumeLayout(False)
        CType(Me.DGView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelAtas.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelPicture As Panel
    Friend WithEvents LocGmb1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents tCari As TextBox
    Friend WithEvents cmdCari As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PanelAtas As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents NilaiPesanan As TextBox
    Friend WithEvents TotalQTY As TextBox
    Friend WithEvents TotalMacam As TextBox
    Friend WithEvents PanelKanan As Panel
    Friend WithEvents PanelKiri As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents DGView As DataGridView
    Friend WithEvents DGView2 As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewTextBoxColumn
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
    Friend WithEvents Column18 As DataGridViewTextBoxColumn
    Friend WithEvents Column19 As DataGridViewTextBoxColumn
    Friend WithEvents Column20 As DataGridViewTextBoxColumn
    Friend WithEvents Column21 As DataGridViewTextBoxColumn
    Friend WithEvents Column22 As DataGridViewTextBoxColumn
    Friend WithEvents Column23 As DataGridViewTextBoxColumn
    Friend WithEvents KODEPRODUK_ As DataGridViewTextBoxColumn
    Friend WithEvents KODE_BY_IMPORTIR_ As DataGridViewTextBoxColumn
    Friend WithEvents DESKRIPSI_ As DataGridViewTextBoxColumn
End Class
