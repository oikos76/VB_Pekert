<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_UploadFoto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_UploadFoto))
        Me.PanelGambar = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.namaFile = New System.Windows.Forms.TextBox()
        Me.btnBrowseImg = New System.Windows.Forms.Button()
        Me.NamaFileO = New System.Windows.Forms.TextBox()
        Me.LastEdit = New System.Windows.Forms.Label()
        Me.LastUPD = New System.Windows.Forms.Label()
        Me.userUpdate = New System.Windows.Forms.Label()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PanelGambar.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelGambar
        '
        Me.PanelGambar.Controls.Add(Me.PictureBox1)
        Me.PanelGambar.Controls.Add(Me.namaFile)
        Me.PanelGambar.Controls.Add(Me.btnBrowseImg)
        Me.PanelGambar.Controls.Add(Me.NamaFileO)
        Me.PanelGambar.Controls.Add(Me.LastEdit)
        Me.PanelGambar.Controls.Add(Me.LastUPD)
        Me.PanelGambar.Controls.Add(Me.userUpdate)
        Me.PanelGambar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelGambar.Location = New System.Drawing.Point(0, 0)
        Me.PanelGambar.Name = "PanelGambar"
        Me.PanelGambar.Size = New System.Drawing.Size(170, 229)
        Me.PanelGambar.TabIndex = 105
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Location = New System.Drawing.Point(13, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(151, 144)
        Me.PictureBox1.TabIndex = 15
        Me.PictureBox1.TabStop = False
        '
        'namaFile
        '
        Me.namaFile.BackColor = System.Drawing.SystemColors.Window
        Me.namaFile.Location = New System.Drawing.Point(13, 158)
        Me.namaFile.Name = "namaFile"
        Me.namaFile.ReadOnly = True
        Me.namaFile.Size = New System.Drawing.Size(122, 20)
        Me.namaFile.TabIndex = 16
        '
        'btnBrowseImg
        '
        Me.btnBrowseImg.Image = CType(resources.GetObject("btnBrowseImg.Image"), System.Drawing.Image)
        Me.btnBrowseImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowseImg.Location = New System.Drawing.Point(141, 158)
        Me.btnBrowseImg.Name = "btnBrowseImg"
        Me.btnBrowseImg.Size = New System.Drawing.Size(23, 20)
        Me.btnBrowseImg.TabIndex = 17
        Me.btnBrowseImg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBrowseImg.UseVisualStyleBackColor = True
        '
        'NamaFileO
        '
        Me.NamaFileO.BackColor = System.Drawing.SystemColors.Info
        Me.NamaFileO.Location = New System.Drawing.Point(13, 184)
        Me.NamaFileO.Name = "NamaFileO"
        Me.NamaFileO.Size = New System.Drawing.Size(156, 20)
        Me.NamaFileO.TabIndex = 86
        Me.NamaFileO.Text = "1234567890"
        Me.NamaFileO.Visible = False
        '
        'LastEdit
        '
        Me.LastEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastEdit.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LastEdit.Location = New System.Drawing.Point(105, 278)
        Me.LastEdit.Name = "LastEdit"
        Me.LastEdit.Size = New System.Drawing.Size(88, 13)
        Me.LastEdit.TabIndex = 94
        Me.LastEdit.Text = "LastEdit"
        Me.LastEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LastEdit.Visible = False
        '
        'LastUPD
        '
        Me.LastUPD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastUPD.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LastUPD.Location = New System.Drawing.Point(107, 260)
        Me.LastUPD.Name = "LastUPD"
        Me.LastUPD.Size = New System.Drawing.Size(88, 13)
        Me.LastUPD.TabIndex = 88
        Me.LastUPD.Text = "LastUpd"
        Me.LastUPD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'userUpdate
        '
        Me.userUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.userUpdate.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.userUpdate.Location = New System.Drawing.Point(39, 278)
        Me.userUpdate.Name = "userUpdate"
        Me.userUpdate.Size = New System.Drawing.Size(62, 13)
        Me.userUpdate.TabIndex = 89
        Me.userUpdate.Text = "userUpdate"
        Me.userUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.userUpdate.Visible = False
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(0, 229)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(170, 32)
        Me.cmdSimpan.TabIndex = 106
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'Form_UploadFoto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(170, 261)
        Me.Controls.Add(Me.PanelGambar)
        Me.Controls.Add(Me.cmdSimpan)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_UploadFoto"
        Me.Text = "Upload Foto"
        Me.PanelGambar.ResumeLayout(False)
        Me.PanelGambar.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelGambar As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents namaFile As TextBox
    Friend WithEvents btnBrowseImg As Button
    Friend WithEvents NamaFileO As TextBox
    Friend WithEvents LastEdit As Label
    Friend WithEvents LastUPD As Label
    Friend WithEvents userUpdate As Label
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
