<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KeuKodeGL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KeuKodeGL))
        Me.PanelTombol_ = New System.Windows.Forms.Panel()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.PanelEntry_ = New System.Windows.Forms.Panel()
        Me.cmbJenisTabel = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NoPerkiraan = New System.Windows.Forms.TextBox()
        Me.NamaPerkiraan = New System.Windows.Forms.TextBox()
        Me.NamaSubPerkiraan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NamaKode = New System.Windows.Forms.TextBox()
        Me.NamaKelompok = New System.Windows.Forms.TextBox()
        Me.NamaGolongan = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbSubPerkiraan = New System.Windows.Forms.ComboBox()
        Me.cmbNoKode = New System.Windows.Forms.ComboBox()
        Me.CmbNoKelompok = New System.Windows.Forms.ComboBox()
        Me.cmbNoGolongan = New System.Windows.Forms.ComboBox()
        Me.NoKelompok = New System.Windows.Forms.TextBox()
        Me.NoKode = New System.Windows.Forms.TextBox()
        Me.NoSubPerkiraan = New System.Windows.Forms.TextBox()
        Me.NoGolongan = New System.Windows.Forms.TextBox()
        Me.PanelTombol_.SuspendLayout()
        Me.PanelEntry_.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTombol_
        '
        Me.PanelTombol_.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTombol_.Controls.Add(Me.cmdPrint)
        Me.PanelTombol_.Controls.Add(Me.cmdSimpan)
        Me.PanelTombol_.Controls.Add(Me.cmdEdit)
        Me.PanelTombol_.Controls.Add(Me.cmdHapus)
        Me.PanelTombol_.Controls.Add(Me.cmdExit)
        Me.PanelTombol_.Controls.Add(Me.cmdTambah)
        Me.PanelTombol_.Controls.Add(Me.cmdBatal)
        Me.PanelTombol_.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTombol_.Location = New System.Drawing.Point(0, 378)
        Me.PanelTombol_.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelTombol_.Name = "PanelTombol_"
        Me.PanelTombol_.Size = New System.Drawing.Size(591, 36)
        Me.PanelTombol_.TabIndex = 148
        '
        'cmdPrint
        '
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(254, 3)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(77, 28)
        Me.cmdPrint.TabIndex = 8
        Me.cmdPrint.Text = "&Daftar"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(500, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(89, 34)
        Me.cmdSimpan.TabIndex = 7
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        Me.cmdSimpan.Visible = False
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(88, 3)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 28)
        Me.cmdEdit.TabIndex = 1
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
        Me.cmdHapus.TabIndex = 2
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(334, 3)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 28)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(5, 3)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(77, 28)
        Me.cmdTambah.TabIndex = 0
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 34)
        Me.cmdBatal.TabIndex = 4
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        Me.cmdBatal.Visible = False
        '
        'PanelEntry_
        '
        Me.PanelEntry_.Controls.Add(Me.cmbJenisTabel)
        Me.PanelEntry_.Controls.Add(Me.Label5)
        Me.PanelEntry_.Controls.Add(Me.NoPerkiraan)
        Me.PanelEntry_.Controls.Add(Me.NamaPerkiraan)
        Me.PanelEntry_.Controls.Add(Me.NamaSubPerkiraan)
        Me.PanelEntry_.Controls.Add(Me.Label2)
        Me.PanelEntry_.Controls.Add(Me.NamaKode)
        Me.PanelEntry_.Controls.Add(Me.NamaKelompok)
        Me.PanelEntry_.Controls.Add(Me.NamaGolongan)
        Me.PanelEntry_.Controls.Add(Me.Label4)
        Me.PanelEntry_.Controls.Add(Me.Label1)
        Me.PanelEntry_.Controls.Add(Me.Label3)
        Me.PanelEntry_.Controls.Add(Me.Label6)
        Me.PanelEntry_.Controls.Add(Me.cmbSubPerkiraan)
        Me.PanelEntry_.Controls.Add(Me.cmbNoKode)
        Me.PanelEntry_.Controls.Add(Me.CmbNoKelompok)
        Me.PanelEntry_.Controls.Add(Me.cmbNoGolongan)
        Me.PanelEntry_.Controls.Add(Me.NoKelompok)
        Me.PanelEntry_.Controls.Add(Me.NoKode)
        Me.PanelEntry_.Controls.Add(Me.NoSubPerkiraan)
        Me.PanelEntry_.Controls.Add(Me.NoGolongan)
        Me.PanelEntry_.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEntry_.Location = New System.Drawing.Point(0, 0)
        Me.PanelEntry_.Name = "PanelEntry_"
        Me.PanelEntry_.Size = New System.Drawing.Size(591, 378)
        Me.PanelEntry_.TabIndex = 149
        '
        'cmbJenisTabel
        '
        Me.cmbJenisTabel.BackColor = System.Drawing.SystemColors.Window
        Me.cmbJenisTabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJenisTabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJenisTabel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbJenisTabel.FormattingEnabled = True
        Me.cmbJenisTabel.Location = New System.Drawing.Point(172, 12)
        Me.cmbJenisTabel.Name = "cmbJenisTabel"
        Me.cmbJenisTabel.Size = New System.Drawing.Size(322, 26)
        Me.cmbJenisTabel.TabIndex = 230
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(27, 293)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 16)
        Me.Label5.TabIndex = 229
        Me.Label5.Text = "Perkiraan      :"
        '
        'NoPerkiraan
        '
        Me.NoPerkiraan.BackColor = System.Drawing.SystemColors.Window
        Me.NoPerkiraan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoPerkiraan.Location = New System.Drawing.Point(172, 290)
        Me.NoPerkiraan.Margin = New System.Windows.Forms.Padding(4)
        Me.NoPerkiraan.MaxLength = 50
        Me.NoPerkiraan.Name = "NoPerkiraan"
        Me.NoPerkiraan.ReadOnly = True
        Me.NoPerkiraan.Size = New System.Drawing.Size(321, 22)
        Me.NoPerkiraan.TabIndex = 228
        '
        'NamaPerkiraan
        '
        Me.NamaPerkiraan.BackColor = System.Drawing.SystemColors.Window
        Me.NamaPerkiraan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaPerkiraan.Location = New System.Drawing.Point(172, 320)
        Me.NamaPerkiraan.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaPerkiraan.MaxLength = 100
        Me.NamaPerkiraan.Name = "NamaPerkiraan"
        Me.NamaPerkiraan.ReadOnly = True
        Me.NamaPerkiraan.Size = New System.Drawing.Size(321, 22)
        Me.NamaPerkiraan.TabIndex = 227
        '
        'NamaSubPerkiraan
        '
        Me.NamaSubPerkiraan.BackColor = System.Drawing.SystemColors.Window
        Me.NamaSubPerkiraan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaSubPerkiraan.Location = New System.Drawing.Point(172, 260)
        Me.NamaSubPerkiraan.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaSubPerkiraan.MaxLength = 100
        Me.NamaSubPerkiraan.Name = "NamaSubPerkiraan"
        Me.NamaSubPerkiraan.ReadOnly = True
        Me.NamaSubPerkiraan.Size = New System.Drawing.Size(321, 22)
        Me.NamaSubPerkiraan.TabIndex = 226
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 234)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 16)
        Me.Label2.TabIndex = 225
        Me.Label2.Text = "Sub Perkiraan  :"
        '
        'NamaKode
        '
        Me.NamaKode.BackColor = System.Drawing.SystemColors.Window
        Me.NamaKode.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaKode.Location = New System.Drawing.Point(172, 199)
        Me.NamaKode.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaKode.MaxLength = 100
        Me.NamaKode.Name = "NamaKode"
        Me.NamaKode.ReadOnly = True
        Me.NamaKode.Size = New System.Drawing.Size(321, 22)
        Me.NamaKode.TabIndex = 224
        '
        'NamaKelompok
        '
        Me.NamaKelompok.BackColor = System.Drawing.SystemColors.Window
        Me.NamaKelompok.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaKelompok.Location = New System.Drawing.Point(172, 137)
        Me.NamaKelompok.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaKelompok.MaxLength = 100
        Me.NamaKelompok.Name = "NamaKelompok"
        Me.NamaKelompok.ReadOnly = True
        Me.NamaKelompok.Size = New System.Drawing.Size(321, 22)
        Me.NamaKelompok.TabIndex = 222
        '
        'NamaGolongan
        '
        Me.NamaGolongan.BackColor = System.Drawing.SystemColors.Window
        Me.NamaGolongan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaGolongan.Location = New System.Drawing.Point(172, 76)
        Me.NamaGolongan.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaGolongan.MaxLength = 100
        Me.NamaGolongan.Name = "NamaGolongan"
        Me.NamaGolongan.ReadOnly = True
        Me.NamaGolongan.Size = New System.Drawing.Size(321, 22)
        Me.NamaGolongan.TabIndex = 221
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(29, 18)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 16)
        Me.Label4.TabIndex = 220
        Me.Label4.Text = "Jenis Tabel    :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 172)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Kode           :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(28, 111)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 16)
        Me.Label3.TabIndex = 218
        Me.Label3.Text = "Kelompok       :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(28, 50)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 16)
        Me.Label6.TabIndex = 217
        Me.Label6.Text = "Golongan       :"
        '
        'cmbSubPerkiraan
        '
        Me.cmbSubPerkiraan.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSubPerkiraan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSubPerkiraan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSubPerkiraan.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSubPerkiraan.FormattingEnabled = True
        Me.cmbSubPerkiraan.Location = New System.Drawing.Point(172, 228)
        Me.cmbSubPerkiraan.Name = "cmbSubPerkiraan"
        Me.cmbSubPerkiraan.Size = New System.Drawing.Size(322, 26)
        Me.cmbSubPerkiraan.TabIndex = 233
        '
        'cmbNoKode
        '
        Me.cmbNoKode.BackColor = System.Drawing.SystemColors.Window
        Me.cmbNoKode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoKode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNoKode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbNoKode.FormattingEnabled = True
        Me.cmbNoKode.Location = New System.Drawing.Point(172, 166)
        Me.cmbNoKode.Name = "cmbNoKode"
        Me.cmbNoKode.Size = New System.Drawing.Size(321, 26)
        Me.cmbNoKode.TabIndex = 232
        '
        'CmbNoKelompok
        '
        Me.CmbNoKelompok.BackColor = System.Drawing.SystemColors.Window
        Me.CmbNoKelompok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNoKelompok.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNoKelompok.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CmbNoKelompok.FormattingEnabled = True
        Me.CmbNoKelompok.Location = New System.Drawing.Point(172, 105)
        Me.CmbNoKelompok.Name = "CmbNoKelompok"
        Me.CmbNoKelompok.Size = New System.Drawing.Size(322, 26)
        Me.CmbNoKelompok.TabIndex = 231
        '
        'cmbNoGolongan
        '
        Me.cmbNoGolongan.BackColor = System.Drawing.SystemColors.Window
        Me.cmbNoGolongan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoGolongan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNoGolongan.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbNoGolongan.FormattingEnabled = True
        Me.cmbNoGolongan.Location = New System.Drawing.Point(172, 44)
        Me.cmbNoGolongan.Name = "cmbNoGolongan"
        Me.cmbNoGolongan.Size = New System.Drawing.Size(322, 26)
        Me.cmbNoGolongan.TabIndex = 234
        '
        'NoKelompok
        '
        Me.NoKelompok.BackColor = System.Drawing.SystemColors.Window
        Me.NoKelompok.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoKelompok.Location = New System.Drawing.Point(172, 107)
        Me.NoKelompok.Margin = New System.Windows.Forms.Padding(4)
        Me.NoKelompok.MaxLength = 50
        Me.NoKelompok.Name = "NoKelompok"
        Me.NoKelompok.ReadOnly = True
        Me.NoKelompok.Size = New System.Drawing.Size(321, 22)
        Me.NoKelompok.TabIndex = 235
        '
        'NoKode
        '
        Me.NoKode.BackColor = System.Drawing.SystemColors.Window
        Me.NoKode.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoKode.Location = New System.Drawing.Point(172, 169)
        Me.NoKode.Margin = New System.Windows.Forms.Padding(4)
        Me.NoKode.MaxLength = 50
        Me.NoKode.Name = "NoKode"
        Me.NoKode.ReadOnly = True
        Me.NoKode.Size = New System.Drawing.Size(321, 22)
        Me.NoKode.TabIndex = 236
        '
        'NoSubPerkiraan
        '
        Me.NoSubPerkiraan.BackColor = System.Drawing.SystemColors.Window
        Me.NoSubPerkiraan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoSubPerkiraan.Location = New System.Drawing.Point(172, 230)
        Me.NoSubPerkiraan.Margin = New System.Windows.Forms.Padding(4)
        Me.NoSubPerkiraan.MaxLength = 50
        Me.NoSubPerkiraan.Name = "NoSubPerkiraan"
        Me.NoSubPerkiraan.ReadOnly = True
        Me.NoSubPerkiraan.Size = New System.Drawing.Size(321, 22)
        Me.NoSubPerkiraan.TabIndex = 237
        '
        'NoGolongan
        '
        Me.NoGolongan.BackColor = System.Drawing.SystemColors.Window
        Me.NoGolongan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoGolongan.Location = New System.Drawing.Point(172, 46)
        Me.NoGolongan.Margin = New System.Windows.Forms.Padding(4)
        Me.NoGolongan.MaxLength = 50
        Me.NoGolongan.Name = "NoGolongan"
        Me.NoGolongan.ReadOnly = True
        Me.NoGolongan.Size = New System.Drawing.Size(321, 22)
        Me.NoGolongan.TabIndex = 216
        '
        'Form_KeuKodeGL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 414)
        Me.Controls.Add(Me.PanelEntry_)
        Me.Controls.Add(Me.PanelTombol_)
        Me.Name = "Form_KeuKodeGL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kode GL"
        Me.PanelTombol_.ResumeLayout(False)
        Me.PanelEntry_.ResumeLayout(False)
        Me.PanelEntry_.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTombol_ As Panel
    Friend WithEvents cmdPrint As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents PanelEntry_ As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents NoPerkiraan As TextBox
    Friend WithEvents NamaPerkiraan As TextBox
    Friend WithEvents NamaSubPerkiraan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NamaKode As TextBox
    Friend WithEvents NamaKelompok As TextBox
    Friend WithEvents NamaGolongan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents NoGolongan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbSubPerkiraan As ComboBox
    Friend WithEvents cmbNoKode As ComboBox
    Friend WithEvents CmbNoKelompok As ComboBox
    Friend WithEvents cmbJenisTabel As ComboBox
    Friend WithEvents cmbNoGolongan As ComboBox
    Friend WithEvents NoKelompok As TextBox
    Friend WithEvents NoKode As TextBox
    Friend WithEvents NoSubPerkiraan As TextBox
End Class
