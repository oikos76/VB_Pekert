<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_DataTerima
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_DataTerima))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdTerima = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnDefaultFolder = New System.Windows.Forms.Button()
        Me.cariFolder = New System.Windows.Forms.Button()
        Me.NamaFile = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.KodeTokoAsal = New System.Windows.Forms.TextBox()
        Me.NamaTokoAsal = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Kode_Toko = New System.Windows.Forms.TextBox()
        Me.NamaToko = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.jenisData = New System.Windows.Forms.Label()
        Me.idRecord = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Nama File"
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
        Me.cmdBatal.Size = New System.Drawing.Size(80, 30)
        Me.cmdBatal.TabIndex = 26
        Me.cmdBatal.Text = "Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = False
        '
        'cmdTerima
        '
        Me.cmdTerima.BackColor = System.Drawing.Color.Transparent
        Me.cmdTerima.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdTerima.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdTerima.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTerima.Image = CType(resources.GetObject("cmdTerima.Image"), System.Drawing.Image)
        Me.cmdTerima.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTerima.Location = New System.Drawing.Point(383, 0)
        Me.cmdTerima.Name = "cmdTerima"
        Me.cmdTerima.Size = New System.Drawing.Size(90, 30)
        Me.cmdTerima.TabIndex = 25
        Me.cmdTerima.Text = "Terima"
        Me.cmdTerima.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTerima.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnDefaultFolder)
        Me.Panel1.Controls.Add(Me.cmdTerima)
        Me.Panel1.Controls.Add(Me.cmdBatal)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 175)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(477, 34)
        Me.Panel1.TabIndex = 29
        '
        'btnDefaultFolder
        '
        Me.btnDefaultFolder.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDefaultFolder.Image = CType(resources.GetObject("btnDefaultFolder.Image"), System.Drawing.Image)
        Me.btnDefaultFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDefaultFolder.Location = New System.Drawing.Point(80, 0)
        Me.btnDefaultFolder.Name = "btnDefaultFolder"
        Me.btnDefaultFolder.Size = New System.Drawing.Size(100, 30)
        Me.btnDefaultFolder.TabIndex = 31
        Me.btnDefaultFolder.Text = "Default Folder"
        Me.btnDefaultFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDefaultFolder.UseVisualStyleBackColor = True
        '
        'cariFolder
        '
        Me.cariFolder.Image = CType(resources.GetObject("cariFolder.Image"), System.Drawing.Image)
        Me.cariFolder.Location = New System.Drawing.Point(411, 11)
        Me.cariFolder.Name = "cariFolder"
        Me.cariFolder.Size = New System.Drawing.Size(26, 23)
        Me.cariFolder.TabIndex = 30
        Me.cariFolder.UseVisualStyleBackColor = True
        '
        'NamaFile
        '
        Me.NamaFile.Location = New System.Drawing.Point(139, 13)
        Me.NamaFile.Name = "NamaFile"
        Me.NamaFile.ReadOnly = True
        Me.NamaFile.Size = New System.Drawing.Size(270, 20)
        Me.NamaFile.TabIndex = 31
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(115, 43)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(11, 16)
        Me.Label9.TabIndex = 164
        Me.Label9.Text = ":"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(115, 71)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 16)
        Me.Label8.TabIndex = 163
        Me.Label8.Text = ":"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 16)
        Me.Label1.TabIndex = 162
        Me.Label1.Text = "Asal"
        '
        'KodeTokoAsal
        '
        Me.KodeTokoAsal.BackColor = System.Drawing.SystemColors.Window
        Me.KodeTokoAsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeTokoAsal.Location = New System.Drawing.Point(139, 40)
        Me.KodeTokoAsal.MaxLength = 100
        Me.KodeTokoAsal.Name = "KodeTokoAsal"
        Me.KodeTokoAsal.ReadOnly = True
        Me.KodeTokoAsal.Size = New System.Drawing.Size(57, 22)
        Me.KodeTokoAsal.TabIndex = 157
        Me.KodeTokoAsal.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'NamaTokoAsal
        '
        Me.NamaTokoAsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaTokoAsal.Location = New System.Drawing.Point(202, 40)
        Me.NamaTokoAsal.MaxLength = 100
        Me.NamaTokoAsal.Name = "NamaTokoAsal"
        Me.NamaTokoAsal.ReadOnly = True
        Me.NamaTokoAsal.Size = New System.Drawing.Size(235, 22)
        Me.NamaTokoAsal.TabIndex = 161
        Me.NamaTokoAsal.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 16)
        Me.Label5.TabIndex = 160
        Me.Label5.Text = "Tujuan"
        '
        'Kode_Toko
        '
        Me.Kode_Toko.BackColor = System.Drawing.SystemColors.Window
        Me.Kode_Toko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kode_Toko.Location = New System.Drawing.Point(139, 68)
        Me.Kode_Toko.MaxLength = 100
        Me.Kode_Toko.Name = "Kode_Toko"
        Me.Kode_Toko.ReadOnly = True
        Me.Kode_Toko.Size = New System.Drawing.Size(57, 22)
        Me.Kode_Toko.TabIndex = 158
        Me.Kode_Toko.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'NamaToko
        '
        Me.NamaToko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaToko.Location = New System.Drawing.Point(202, 68)
        Me.NamaToko.MaxLength = 100
        Me.NamaToko.Name = "NamaToko"
        Me.NamaToko.ReadOnly = True
        Me.NamaToko.Size = New System.Drawing.Size(235, 22)
        Me.NamaToko.TabIndex = 159
        Me.NamaToko.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(115, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 16)
        Me.Label3.TabIndex = 165
        Me.Label3.Text = ":"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'jenisData
        '
        Me.jenisData.AutoSize = True
        Me.jenisData.Location = New System.Drawing.Point(136, 105)
        Me.jenisData.Name = "jenisData"
        Me.jenisData.Size = New System.Drawing.Size(55, 13)
        Me.jenisData.TabIndex = 166
        Me.jenisData.Text = "Jenis data"
        '
        'idRecord
        '
        Me.idRecord.AutoSize = True
        Me.idRecord.Location = New System.Drawing.Point(137, 129)
        Me.idRecord.Name = "idRecord"
        Me.idRecord.Size = New System.Drawing.Size(53, 13)
        Me.idRecord.TabIndex = 167
        Me.idRecord.Text = "id.Record"
        '
        'Form_DataTerima
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 209)
        Me.Controls.Add(Me.idRecord)
        Me.Controls.Add(Me.jenisData)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.KodeTokoAsal)
        Me.Controls.Add(Me.NamaTokoAsal)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Kode_Toko)
        Me.Controls.Add(Me.NamaToko)
        Me.Controls.Add(Me.cariFolder)
        Me.Controls.Add(Me.NamaFile)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_DataTerima"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Terima Data"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdTerima As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cariFolder As Button
    Friend WithEvents NamaFile As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents KodeTokoAsal As TextBox
    Friend WithEvents NamaTokoAsal As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Kode_Toko As TextBox
    Friend WithEvents NamaToko As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents jenisData As Label
    Friend WithEvents idRecord As Label
    Friend WithEvents btnDefaultFolder As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
