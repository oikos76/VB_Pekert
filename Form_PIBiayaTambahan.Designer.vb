<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_PIBiayaTambahan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_PIBiayaTambahan))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.PanelNavigate = New System.Windows.Forms.Panel()
        Me.btnButtom = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnTop = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.NoPI = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Importir = New System.Windows.Forms.TextBox()
        Me.Kode_Importir = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.LabelNoPO = New System.Windows.Forms.Label()
        Me.Nopo = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.KodeProduk = New System.Windows.Forms.TextBox()
        Me.Produk = New System.Windows.Forms.TextBox()
        Me.KodePImportir = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.chk3Digit = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.PanelNavigate.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdSimpan)
        Me.Panel1.Controls.Add(Me.cmdExit)
        Me.Panel1.Controls.Add(Me.PanelNavigate)
        Me.Panel1.Controls.Add(Me.cmdHapus)
        Me.Panel1.Controls.Add(Me.cmdTambah)
        Me.Panel1.Controls.Add(Me.cmdEdit)
        Me.Panel1.Controls.Add(Me.cmdBatal)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 252)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(710, 36)
        Me.Panel1.TabIndex = 0
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(480, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(76, 36)
        Me.cmdSimpan.TabIndex = 5
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        Me.cmdSimpan.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(213, 3)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(70, 28)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'PanelNavigate
        '
        Me.PanelNavigate.Controls.Add(Me.btnButtom)
        Me.PanelNavigate.Controls.Add(Me.btnNext)
        Me.PanelNavigate.Controls.Add(Me.btnPrev)
        Me.PanelNavigate.Controls.Add(Me.btnTop)
        Me.PanelNavigate.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelNavigate.Location = New System.Drawing.Point(556, 0)
        Me.PanelNavigate.Name = "PanelNavigate"
        Me.PanelNavigate.Size = New System.Drawing.Size(154, 36)
        Me.PanelNavigate.TabIndex = 63
        '
        'btnButtom
        '
        Me.btnButtom.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnButtom.Image = CType(resources.GetObject("btnButtom.Image"), System.Drawing.Image)
        Me.btnButtom.Location = New System.Drawing.Point(114, 0)
        Me.btnButtom.Name = "btnButtom"
        Me.btnButtom.Size = New System.Drawing.Size(38, 36)
        Me.btnButtom.TabIndex = 3
        Me.btnButtom.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.Location = New System.Drawing.Point(76, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(38, 36)
        Me.btnNext.TabIndex = 2
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPrev.Image = CType(resources.GetObject("btnPrev.Image"), System.Drawing.Image)
        Me.btnPrev.Location = New System.Drawing.Point(38, 0)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(38, 36)
        Me.btnPrev.TabIndex = 1
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnTop
        '
        Me.btnTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnTop.Image = CType(resources.GetObject("btnTop.Image"), System.Drawing.Image)
        Me.btnTop.Location = New System.Drawing.Point(0, 0)
        Me.btnTop.Name = "btnTop"
        Me.btnTop.Size = New System.Drawing.Size(38, 36)
        Me.btnTop.TabIndex = 0
        Me.btnTop.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(143, 3)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(70, 28)
        Me.cmdHapus.TabIndex = 2
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(3, 3)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(70, 28)
        Me.cmdTambah.TabIndex = 0
        Me.cmdTambah.Text = "&Baru"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(73, 3)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(70, 28)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.Text = "&Edit"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(70, 36)
        Me.cmdBatal.TabIndex = 4
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        Me.cmdBatal.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chk3Digit)
        Me.Panel2.Controls.Add(Me.TextBox4)
        Me.Panel2.Controls.Add(Me.TextBox3)
        Me.Panel2.Controls.Add(Me.TextBox2)
        Me.Panel2.Controls.Add(Me.KodePImportir)
        Me.Panel2.Controls.Add(Me.KodeProduk)
        Me.Panel2.Controls.Add(Me.Produk)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.NoPI)
        Me.Panel2.Controls.Add(Me.Label29)
        Me.Panel2.Controls.Add(Me.Importir)
        Me.Panel2.Controls.Add(Me.Kode_Importir)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.LabelNoPO)
        Me.Panel2.Controls.Add(Me.Nopo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(710, 252)
        Me.Panel2.TabIndex = 0
        '
        'NoPI
        '
        Me.NoPI.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoPI.Location = New System.Drawing.Point(231, 9)
        Me.NoPI.Margin = New System.Windows.Forms.Padding(4)
        Me.NoPI.Name = "NoPI"
        Me.NoPI.Size = New System.Drawing.Size(113, 22)
        Me.NoPI.TabIndex = 319
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(5, 37)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(224, 16)
        Me.Label29.TabIndex = 318
        Me.Label29.Text = "Nomor  PO                 :"
        '
        'Importir
        '
        Me.Importir.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Importir.Location = New System.Drawing.Point(282, 60)
        Me.Importir.Margin = New System.Windows.Forms.Padding(4)
        Me.Importir.MaxLength = 4
        Me.Importir.Name = "Importir"
        Me.Importir.Size = New System.Drawing.Size(270, 22)
        Me.Importir.TabIndex = 316
        '
        'Kode_Importir
        '
        Me.Kode_Importir.BackColor = System.Drawing.SystemColors.Window
        Me.Kode_Importir.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kode_Importir.Location = New System.Drawing.Point(231, 60)
        Me.Kode_Importir.Margin = New System.Windows.Forms.Padding(4)
        Me.Kode_Importir.Name = "Kode_Importir"
        Me.Kode_Importir.Size = New System.Drawing.Size(49, 22)
        Me.Kode_Importir.TabIndex = 315
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(5, 63)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(224, 16)
        Me.Label17.TabIndex = 317
        Me.Label17.Text = "Nama Importir             :"
        '
        'LabelNoPO
        '
        Me.LabelNoPO.AutoSize = True
        Me.LabelNoPO.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNoPO.Location = New System.Drawing.Point(5, 12)
        Me.LabelNoPO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelNoPO.Name = "LabelNoPO"
        Me.LabelNoPO.Size = New System.Drawing.Size(224, 16)
        Me.LabelNoPO.TabIndex = 314
        Me.LabelNoPO.Text = "Nomor  PI                 :"
        '
        'Nopo
        '
        Me.Nopo.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Nopo.Location = New System.Drawing.Point(231, 34)
        Me.Nopo.Margin = New System.Windows.Forms.Padding(4)
        Me.Nopo.Name = "Nopo"
        Me.Nopo.Size = New System.Drawing.Size(113, 22)
        Me.Nopo.TabIndex = 313
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(231, 190)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(130, 22)
        Me.TextBox1.TabIndex = 321
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 218)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 16)
        Me.Label1.TabIndex = 320
        Me.Label1.Text = "Phytosanitary Certificate :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 167)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(224, 16)
        Me.Label2.TabIndex = 324
        Me.Label2.Text = "Special Packaging         :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 193)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(224, 16)
        Me.Label3.TabIndex = 323
        Me.Label3.Text = "Fumigation                :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 142)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(224, 16)
        Me.Label4.TabIndex = 322
        Me.Label4.Text = "Labeling Cost             :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 116)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(224, 16)
        Me.Label5.TabIndex = 326
        Me.Label5.Text = "Kode Impor                :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 89)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(224, 16)
        Me.Label6.TabIndex = 325
        Me.Label6.Text = "Kode Produk               :"
        '
        'KodeProduk
        '
        Me.KodeProduk.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeProduk.Location = New System.Drawing.Point(231, 86)
        Me.KodeProduk.Margin = New System.Windows.Forms.Padding(4)
        Me.KodeProduk.Name = "KodeProduk"
        Me.KodeProduk.Size = New System.Drawing.Size(130, 22)
        Me.KodeProduk.TabIndex = 327
        Me.KodeProduk.Text = "0120-22-1182A"
        '
        'Produk
        '
        Me.Produk.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Produk.Location = New System.Drawing.Point(369, 86)
        Me.Produk.Margin = New System.Windows.Forms.Padding(4)
        Me.Produk.Name = "Produk"
        Me.Produk.Size = New System.Drawing.Size(320, 22)
        Me.Produk.TabIndex = 328
        '
        'KodePImportir
        '
        Me.KodePImportir.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodePImportir.Location = New System.Drawing.Point(231, 113)
        Me.KodePImportir.Margin = New System.Windows.Forms.Padding(4)
        Me.KodePImportir.Name = "KodePImportir"
        Me.KodePImportir.Size = New System.Drawing.Size(130, 22)
        Me.KodePImportir.TabIndex = 329
        Me.KodePImportir.Text = "10.03.2381"
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(231, 139)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(130, 22)
        Me.TextBox2.TabIndex = 330
        Me.TextBox2.Text = "10.03.2381"
        '
        'TextBox3
        '
        Me.TextBox3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(231, 164)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(130, 22)
        Me.TextBox3.TabIndex = 331
        Me.TextBox3.Text = "10.03.2381"
        '
        'TextBox4
        '
        Me.TextBox4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(231, 215)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(130, 22)
        Me.TextBox4.TabIndex = 332
        '
        'chk3Digit
        '
        Me.chk3Digit.AutoSize = True
        Me.chk3Digit.Location = New System.Drawing.Point(369, 116)
        Me.chk3Digit.Name = "chk3Digit"
        Me.chk3Digit.Size = New System.Drawing.Size(56, 17)
        Me.chk3Digit.TabIndex = 333
        Me.chk3Digit.Text = "3 Digit"
        Me.chk3Digit.UseVisualStyleBackColor = True
        '
        'Form_PIBiayaTambahan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 288)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form_PIBiayaTambahan"
        Me.Text = "Biaya Tambahan PI"
        Me.Panel1.ResumeLayout(False)
        Me.PanelNavigate.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents PanelNavigate As Panel
    Friend WithEvents btnButtom As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents btnPrev As Button
    Friend WithEvents btnTop As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents NoPI As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Importir As TextBox
    Friend WithEvents Kode_Importir As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents LabelNoPO As Label
    Friend WithEvents Nopo As TextBox
    Friend WithEvents KodeProduk As TextBox
    Friend WithEvents Produk As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents KodePImportir As TextBox
    Friend WithEvents chk3Digit As CheckBox
End Class
