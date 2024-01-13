<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_DataKirim
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_DataKirim))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NamaFile = New System.Windows.Forms.TextBox()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdKirim = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Kode_Toko = New System.Windows.Forms.TextBox()
        Me.NamaToko = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.KodeTokoAsal = New System.Windows.Forms.TextBox()
        Me.NamaTokoAsal = New System.Windows.Forms.TextBox()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cariFolder = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 16)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Nama File"
        '
        'NamaFile
        '
        Me.NamaFile.Location = New System.Drawing.Point(122, 63)
        Me.NamaFile.Name = "NamaFile"
        Me.NamaFile.ReadOnly = True
        Me.NamaFile.Size = New System.Drawing.Size(270, 20)
        Me.NamaFile.TabIndex = 24
        '
        'cmdBatal
        '
        Me.cmdBatal.BackColor = System.Drawing.Color.Transparent
        Me.cmdBatal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(80, 41)
        Me.cmdBatal.TabIndex = 1
        Me.cmdBatal.Text = "Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = False
        '
        'cmdKirim
        '
        Me.cmdKirim.BackColor = System.Drawing.Color.Transparent
        Me.cmdKirim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdKirim.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdKirim.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdKirim.Image = CType(resources.GetObject("cmdKirim.Image"), System.Drawing.Image)
        Me.cmdKirim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdKirim.Location = New System.Drawing.Point(351, 0)
        Me.cmdKirim.Name = "cmdKirim"
        Me.cmdKirim.Size = New System.Drawing.Size(90, 41)
        Me.cmdKirim.TabIndex = 0
        Me.cmdKirim.Text = "Kirim"
        Me.cmdKirim.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdKirim.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 16)
        Me.Label5.TabIndex = 116
        Me.Label5.Text = "Tujuan"
        '
        'Kode_Toko
        '
        Me.Kode_Toko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kode_Toko.Location = New System.Drawing.Point(122, 35)
        Me.Kode_Toko.MaxLength = 100
        Me.Kode_Toko.Name = "Kode_Toko"
        Me.Kode_Toko.ReadOnly = True
        Me.Kode_Toko.Size = New System.Drawing.Size(57, 22)
        Me.Kode_Toko.TabIndex = 119
        Me.Kode_Toko.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'NamaToko
        '
        Me.NamaToko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaToko.Location = New System.Drawing.Point(185, 35)
        Me.NamaToko.MaxLength = 100
        Me.NamaToko.Name = "NamaToko"
        Me.NamaToko.ReadOnly = True
        Me.NamaToko.Size = New System.Drawing.Size(235, 22)
        Me.NamaToko.TabIndex = 120
        Me.NamaToko.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 16)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Asal"
        '
        'KodeTokoAsal
        '
        Me.KodeTokoAsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeTokoAsal.Location = New System.Drawing.Point(122, 7)
        Me.KodeTokoAsal.MaxLength = 100
        Me.KodeTokoAsal.Name = "KodeTokoAsal"
        Me.KodeTokoAsal.ReadOnly = True
        Me.KodeTokoAsal.Size = New System.Drawing.Size(57, 22)
        Me.KodeTokoAsal.TabIndex = 117
        Me.KodeTokoAsal.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'NamaTokoAsal
        '
        Me.NamaTokoAsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaTokoAsal.Location = New System.Drawing.Point(185, 7)
        Me.NamaTokoAsal.MaxLength = 100
        Me.NamaTokoAsal.Name = "NamaTokoAsal"
        Me.NamaTokoAsal.ReadOnly = True
        Me.NamaTokoAsal.Size = New System.Drawing.Size(235, 22)
        Me.NamaTokoAsal.TabIndex = 118
        Me.NamaTokoAsal.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd-MM-yyyy"
        Me.Tgl1.Enabled = False
        Me.Tgl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(122, 114)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(87, 22)
        Me.Tgl1.TabIndex = 3
        '
        'tgl2
        '
        Me.tgl2.CustomFormat = "dd-MM-yyyy"
        Me.tgl2.Enabled = False
        Me.tgl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tgl2.Location = New System.Drawing.Point(232, 114)
        Me.tgl2.Name = "tgl2"
        Me.tgl2.Size = New System.Drawing.Size(87, 22)
        Me.tgl2.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 151
        Me.Label3.Text = "Periode"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(98, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 16)
        Me.Label4.TabIndex = 152
        Me.Label4.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(215, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(12, 16)
        Me.Label6.TabIndex = 153
        Me.Label6.Text = "-"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(98, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 16)
        Me.Label7.TabIndex = 154
        Me.Label7.Text = ":"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(98, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 16)
        Me.Label8.TabIndex = 155
        Me.Label8.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(98, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(11, 16)
        Me.Label9.TabIndex = 156
        Me.Label9.Text = ":"
        '
        'cariFolder
        '
        Me.cariFolder.Image = CType(resources.GetObject("cariFolder.Image"), System.Drawing.Image)
        Me.cariFolder.Location = New System.Drawing.Point(394, 61)
        Me.cariFolder.Name = "cariFolder"
        Me.cariFolder.Size = New System.Drawing.Size(26, 23)
        Me.cariFolder.TabIndex = 25
        Me.cariFolder.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cmdBatal)
        Me.Panel1.Controls.Add(Me.cmdKirim)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 94)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(443, 43)
        Me.Panel1.TabIndex = 0
        '
        'Form_DataKirim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 137)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cariFolder)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tgl2)
        Me.Controls.Add(Me.Tgl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.KodeTokoAsal)
        Me.Controls.Add(Me.NamaTokoAsal)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Kode_Toko)
        Me.Controls.Add(Me.NamaToko)
        Me.Controls.Add(Me.NamaFile)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_DataKirim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kirim Data"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdKirim As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents NamaFile As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Kode_Toko As TextBox
    Friend WithEvents NamaToko As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents KodeTokoAsal As TextBox
    Friend WithEvents NamaTokoAsal As TextBox
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents cariFolder As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Panel1 As Panel
End Class
