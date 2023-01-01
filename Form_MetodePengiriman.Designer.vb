<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_MetodePengiriman
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_MetodePengiriman))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.IdRec = New System.Windows.Forms.TextBox()
        Me.PanelSimpan = New System.Windows.Forms.Panel()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.CaraKirim = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.ID_Request = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Siswa_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Panel7.SuspendLayout()
        Me.PanelSimpan.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.IdRec)
        Me.Panel7.Controls.Add(Me.PanelSimpan)
        Me.Panel7.Controls.Add(Me.cmdTambah)
        Me.Panel7.Controls.Add(Me.CaraKirim)
        Me.Panel7.Controls.Add(Me.Label33)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(584, 58)
        Me.Panel7.TabIndex = 9
        '
        'IdRec
        '
        Me.IdRec.Location = New System.Drawing.Point(379, 17)
        Me.IdRec.MaxLength = 100
        Me.IdRec.Name = "IdRec"
        Me.IdRec.Size = New System.Drawing.Size(27, 20)
        Me.IdRec.TabIndex = 133
        '
        'PanelSimpan
        '
        Me.PanelSimpan.Controls.Add(Me.cmdBatal)
        Me.PanelSimpan.Controls.Add(Me.cmdSimpan)
        Me.PanelSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSimpan.Location = New System.Drawing.Point(425, 0)
        Me.PanelSimpan.Name = "PanelSimpan"
        Me.PanelSimpan.Size = New System.Drawing.Size(85, 56)
        Me.PanelSimpan.TabIndex = 132
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(85, 28)
        Me.cmdBatal.TabIndex = 61
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(0, 28)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(85, 28)
        Me.cmdSimpan.TabIndex = 60
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdTambah.Location = New System.Drawing.Point(510, 0)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(72, 56)
        Me.cmdTambah.TabIndex = 4
        Me.cmdTambah.Text = "Buat Baru"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'CaraKirim
        '
        Me.CaraKirim.Location = New System.Drawing.Point(131, 17)
        Me.CaraKirim.MaxLength = 100
        Me.CaraKirim.Name = "CaraKirim"
        Me.CaraKirim.Size = New System.Drawing.Size(275, 20)
        Me.CaraKirim.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(21, 19)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(104, 16)
        Me.Label33.TabIndex = 131
        Me.Label33.Text = "Cara Kirim :"
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID_Request, Me.Siswa_, Me.Column1, Me.Column2})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGView.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 58)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGView.RowHeadersVisible = False
        Me.DGView.Size = New System.Drawing.Size(584, 503)
        Me.DGView.TabIndex = 10
        '
        'ID_Request
        '
        Me.ID_Request.HeaderText = "Id Record"
        Me.ID_Request.Name = "ID_Request"
        Me.ID_Request.ReadOnly = True
        Me.ID_Request.Width = 110
        '
        'Siswa_
        '
        Me.Siswa_.HeaderText = "Cara Kirim"
        Me.Siswa_.Name = "Siswa_"
        Me.Siswa_.ReadOnly = True
        Me.Siswa_.Width = 250
        '
        'Column1
        '
        Me.Column1.HeaderText = "Edit"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 90
        '
        'Column2
        '
        Me.Column2.HeaderText = "Hapus"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Form_MetodePengiriman
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.DGView)
        Me.Controls.Add(Me.Panel7)
        Me.Name = "Form_MetodePengiriman"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Metode Pengiriman"
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.PanelSimpan.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel7 As Panel
    Friend WithEvents cmdTambah As Button
    Friend WithEvents CaraKirim As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents DGView As DataGridView
    Friend WithEvents ID_Request As DataGridViewTextBoxColumn
    Friend WithEvents Siswa_ As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewButtonColumn
    Friend WithEvents Column2 As DataGridViewButtonColumn
    Friend WithEvents PanelSimpan As Panel
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents IdRec As TextBox
End Class
