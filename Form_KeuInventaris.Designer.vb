<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KeuInventaris
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KeuInventaris))
        Me.PanelTombol_ = New System.Windows.Forms.Panel()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.PanelEntry_ = New System.Windows.Forms.Panel()
        Me.KodeGL = New System.Windows.Forms.TextBox()
        Me.LabeltglKirim = New System.Windows.Forms.Label()
        Me.HargaBeli = New System.Windows.Forms.TextBox()
        Me.Penyusutan = New System.Windows.Forms.TextBox()
        Me.TglBeli = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.idRecord = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbNoKelompok = New System.Windows.Forms.ComboBox()
        Me.NoKelompok = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NamaAset = New System.Windows.Forms.TextBox()
        Me.cmbKodeGL = New System.Windows.Forms.ComboBox()
        Me.PanelTombol_.SuspendLayout()
        Me.PanelEntry_.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTombol_
        '
        Me.PanelTombol_.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTombol_.Controls.Add(Me.cmdExit)
        Me.PanelTombol_.Controls.Add(Me.cmdPrint)
        Me.PanelTombol_.Controls.Add(Me.cmdHapus)
        Me.PanelTombol_.Controls.Add(Me.cmdEdit)
        Me.PanelTombol_.Controls.Add(Me.cmdTambah)
        Me.PanelTombol_.Controls.Add(Me.cmdSimpan)
        Me.PanelTombol_.Controls.Add(Me.cmdBatal)
        Me.PanelTombol_.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTombol_.Location = New System.Drawing.Point(0, 202)
        Me.PanelTombol_.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelTombol_.Name = "PanelTombol_"
        Me.PanelTombol_.Size = New System.Drawing.Size(642, 36)
        Me.PanelTombol_.TabIndex = 149
        '
        'cmdExit
        '
        Me.cmdExit.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(385, 0)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 34)
        Me.cmdExit.TabIndex = 12
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(308, 0)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(77, 34)
        Me.cmdPrint.TabIndex = 11
        Me.cmdPrint.Text = "&Daftar"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(231, 0)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(77, 34)
        Me.cmdHapus.TabIndex = 10
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(154, 0)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 34)
        Me.cmdEdit.TabIndex = 9
        Me.cmdEdit.Text = "&Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(77, 0)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(77, 34)
        Me.cmdTambah.TabIndex = 8
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(551, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(89, 34)
        Me.cmdSimpan.TabIndex = 7
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        Me.cmdSimpan.Visible = False
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
        Me.PanelEntry_.Controls.Add(Me.KodeGL)
        Me.PanelEntry_.Controls.Add(Me.LabeltglKirim)
        Me.PanelEntry_.Controls.Add(Me.HargaBeli)
        Me.PanelEntry_.Controls.Add(Me.Penyusutan)
        Me.PanelEntry_.Controls.Add(Me.TglBeli)
        Me.PanelEntry_.Controls.Add(Me.Label4)
        Me.PanelEntry_.Controls.Add(Me.Label5)
        Me.PanelEntry_.Controls.Add(Me.Label2)
        Me.PanelEntry_.Controls.Add(Me.idRecord)
        Me.PanelEntry_.Controls.Add(Me.Label1)
        Me.PanelEntry_.Controls.Add(Me.Label3)
        Me.PanelEntry_.Controls.Add(Me.CmbNoKelompok)
        Me.PanelEntry_.Controls.Add(Me.NoKelompok)
        Me.PanelEntry_.Controls.Add(Me.Label6)
        Me.PanelEntry_.Controls.Add(Me.NamaAset)
        Me.PanelEntry_.Controls.Add(Me.cmbKodeGL)
        Me.PanelEntry_.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEntry_.Location = New System.Drawing.Point(0, 0)
        Me.PanelEntry_.Name = "PanelEntry_"
        Me.PanelEntry_.Size = New System.Drawing.Size(642, 202)
        Me.PanelEntry_.TabIndex = 150
        '
        'KodeGL
        '
        Me.KodeGL.BackColor = System.Drawing.SystemColors.Window
        Me.KodeGL.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeGL.Location = New System.Drawing.Point(582, 74)
        Me.KodeGL.Margin = New System.Windows.Forms.Padding(4)
        Me.KodeGL.Name = "KodeGL"
        Me.KodeGL.ReadOnly = True
        Me.KodeGL.Size = New System.Drawing.Size(52, 24)
        Me.KodeGL.TabIndex = 285
        Me.KodeGL.Visible = False
        '
        'LabeltglKirim
        '
        Me.LabeltglKirim.AutoSize = True
        Me.LabeltglKirim.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabeltglKirim.Location = New System.Drawing.Point(12, 141)
        Me.LabeltglKirim.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabeltglKirim.Name = "LabeltglKirim"
        Me.LabeltglKirim.Size = New System.Drawing.Size(136, 16)
        Me.LabeltglKirim.TabIndex = 283
        Me.LabeltglKirim.Text = "Harga Beli     :"
        '
        'HargaBeli
        '
        Me.HargaBeli.BackColor = System.Drawing.SystemColors.Window
        Me.HargaBeli.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HargaBeli.Location = New System.Drawing.Point(154, 138)
        Me.HargaBeli.Margin = New System.Windows.Forms.Padding(4)
        Me.HargaBeli.Name = "HargaBeli"
        Me.HargaBeli.ReadOnly = True
        Me.HargaBeli.Size = New System.Drawing.Size(151, 22)
        Me.HargaBeli.TabIndex = 282
        '
        'Penyusutan
        '
        Me.Penyusutan.BackColor = System.Drawing.SystemColors.Window
        Me.Penyusutan.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Penyusutan.Location = New System.Drawing.Point(154, 168)
        Me.Penyusutan.Margin = New System.Windows.Forms.Padding(4)
        Me.Penyusutan.MaxLength = 50
        Me.Penyusutan.Name = "Penyusutan"
        Me.Penyusutan.ReadOnly = True
        Me.Penyusutan.Size = New System.Drawing.Size(48, 22)
        Me.Penyusutan.TabIndex = 236
        Me.Penyusutan.Text = "12.34"
        '
        'TglBeli
        '
        Me.TglBeli.CustomFormat = "dd-MM-yyyy"
        Me.TglBeli.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglBeli.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TglBeli.Location = New System.Drawing.Point(154, 106)
        Me.TglBeli.Margin = New System.Windows.Forms.Padding(4)
        Me.TglBeli.Name = "TglBeli"
        Me.TglBeli.Size = New System.Drawing.Size(151, 24)
        Me.TglBeli.TabIndex = 280
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 16)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 16)
        Me.Label4.TabIndex = 238
        Me.Label4.Text = "Id. Record     :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 171)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 16)
        Me.Label5.TabIndex = 229
        Me.Label5.Text = "Penyusutan     :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 113)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 16)
        Me.Label2.TabIndex = 225
        Me.Label2.Text = "Tgl. Beli      :"
        '
        'idRecord
        '
        Me.idRecord.BackColor = System.Drawing.SystemColors.Window
        Me.idRecord.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.idRecord.Location = New System.Drawing.Point(154, 13)
        Me.idRecord.Margin = New System.Windows.Forms.Padding(4)
        Me.idRecord.MaxLength = 100
        Me.idRecord.Name = "idRecord"
        Me.idRecord.ReadOnly = True
        Me.idRecord.Size = New System.Drawing.Size(151, 22)
        Me.idRecord.TabIndex = 222
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 79)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Nama Asset     :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 47)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 16)
        Me.Label3.TabIndex = 218
        Me.Label3.Text = "Kelompok       :"
        '
        'CmbNoKelompok
        '
        Me.CmbNoKelompok.BackColor = System.Drawing.SystemColors.Window
        Me.CmbNoKelompok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNoKelompok.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNoKelompok.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CmbNoKelompok.FormattingEnabled = True
        Me.CmbNoKelompok.Location = New System.Drawing.Point(154, 42)
        Me.CmbNoKelompok.Name = "CmbNoKelompok"
        Me.CmbNoKelompok.Size = New System.Drawing.Size(421, 26)
        Me.CmbNoKelompok.TabIndex = 231
        '
        'NoKelompok
        '
        Me.NoKelompok.BackColor = System.Drawing.SystemColors.Window
        Me.NoKelompok.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoKelompok.Location = New System.Drawing.Point(155, 44)
        Me.NoKelompok.Margin = New System.Windows.Forms.Padding(4)
        Me.NoKelompok.MaxLength = 50
        Me.NoKelompok.Name = "NoKelompok"
        Me.NoKelompok.ReadOnly = True
        Me.NoKelompok.Size = New System.Drawing.Size(321, 22)
        Me.NoKelompok.TabIndex = 235
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(203, 169)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 20)
        Me.Label6.TabIndex = 281
        Me.Label6.Text = "%"
        '
        'NamaAset
        '
        Me.NamaAset.BackColor = System.Drawing.SystemColors.Window
        Me.NamaAset.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaAset.Location = New System.Drawing.Point(155, 74)
        Me.NamaAset.Margin = New System.Windows.Forms.Padding(4)
        Me.NamaAset.Name = "NamaAset"
        Me.NamaAset.ReadOnly = True
        Me.NamaAset.Size = New System.Drawing.Size(419, 24)
        Me.NamaAset.TabIndex = 284
        '
        'cmbKodeGL
        '
        Me.cmbKodeGL.BackColor = System.Drawing.SystemColors.Window
        Me.cmbKodeGL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKodeGL.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbKodeGL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbKodeGL.FormattingEnabled = True
        Me.cmbKodeGL.Location = New System.Drawing.Point(154, 73)
        Me.cmbKodeGL.Name = "cmbKodeGL"
        Me.cmbKodeGL.Size = New System.Drawing.Size(421, 26)
        Me.cmbKodeGL.TabIndex = 237
        '
        'Form_KeuInventaris
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 238)
        Me.Controls.Add(Me.PanelEntry_)
        Me.Controls.Add(Me.PanelTombol_)
        Me.Name = "Form_KeuInventaris"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventaris"
        Me.PanelTombol_.ResumeLayout(False)
        Me.PanelEntry_.ResumeLayout(False)
        Me.PanelEntry_.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTombol_ As Panel
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents PanelEntry_ As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents idRecord As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CmbNoKelompok As ComboBox
    Friend WithEvents NoKelompok As TextBox
    Friend WithEvents Penyusutan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbKodeGL As ComboBox
    Friend WithEvents TglBeli As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents HargaBeli As TextBox
    Friend WithEvents LabeltglKirim As Label
    Friend WithEvents NamaAset As TextBox
    Friend WithEvents KodeGL As TextBox
End Class
