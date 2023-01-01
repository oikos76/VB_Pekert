<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_JenisBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_JenisBox))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lebar = New System.Windows.Forms.TextBox()
        Me.Panjang = New System.Windows.Forms.TextBox()
        Me.Tinggi = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IdRec = New System.Windows.Forms.TextBox()
        Me.PanelSimpan = New System.Windows.Forms.Panel()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.JenisBox = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.ID_Request = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._JenisBox_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._Panjang_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._Lebar_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._Tinggi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Panel7.SuspendLayout()
        Me.PanelSimpan.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Lebar)
        Me.Panel7.Controls.Add(Me.Panjang)
        Me.Panel7.Controls.Add(Me.Tinggi)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Controls.Add(Me.IdRec)
        Me.Panel7.Controls.Add(Me.PanelSimpan)
        Me.Panel7.Controls.Add(Me.JenisBox)
        Me.Panel7.Controls.Add(Me.Label33)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(704, 120)
        Me.Panel7.TabIndex = 11
        '
        'Lebar
        '
        Me.Lebar.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lebar.Location = New System.Drawing.Point(133, 59)
        Me.Lebar.Margin = New System.Windows.Forms.Padding(4)
        Me.Lebar.Name = "Lebar"
        Me.Lebar.Size = New System.Drawing.Size(72, 22)
        Me.Lebar.TabIndex = 137
        '
        'Panjang
        '
        Me.Panjang.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panjang.Location = New System.Drawing.Point(133, 34)
        Me.Panjang.Margin = New System.Windows.Forms.Padding(4)
        Me.Panjang.Name = "Panjang"
        Me.Panjang.Size = New System.Drawing.Size(72, 22)
        Me.Panjang.TabIndex = 138
        '
        'Tinggi
        '
        Me.Tinggi.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tinggi.Location = New System.Drawing.Point(133, 83)
        Me.Tinggi.Margin = New System.Windows.Forms.Padding(4)
        Me.Tinggi.Name = "Tinggi"
        Me.Tinggi.Size = New System.Drawing.Size(72, 22)
        Me.Tinggi.TabIndex = 139
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 136
        Me.Label2.Text = "Tinggi    :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 135
        Me.Label3.Text = "Lebar     :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "Panjang   :"
        '
        'IdRec
        '
        Me.IdRec.Location = New System.Drawing.Point(381, 11)
        Me.IdRec.MaxLength = 100
        Me.IdRec.Name = "IdRec"
        Me.IdRec.Size = New System.Drawing.Size(27, 20)
        Me.IdRec.TabIndex = 133
        '
        'PanelSimpan
        '
        Me.PanelSimpan.Controls.Add(Me.cmdBatal)
        Me.PanelSimpan.Controls.Add(Me.cmdTambah)
        Me.PanelSimpan.Controls.Add(Me.cmdSimpan)
        Me.PanelSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelSimpan.Location = New System.Drawing.Point(632, 0)
        Me.PanelSimpan.Name = "PanelSimpan"
        Me.PanelSimpan.Size = New System.Drawing.Size(70, 118)
        Me.PanelSimpan.TabIndex = 132
        '
        'cmdTambah
        '
        Me.cmdTambah.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdTambah.Location = New System.Drawing.Point(0, 0)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(70, 56)
        Me.cmdTambah.TabIndex = 4
        Me.cmdTambah.Text = "Buat Baru"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 56)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(70, 28)
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
        Me.cmdSimpan.Location = New System.Drawing.Point(0, 90)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(70, 28)
        Me.cmdSimpan.TabIndex = 60
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'JenisBox
        '
        Me.JenisBox.Location = New System.Drawing.Point(133, 11)
        Me.JenisBox.MaxLength = 100
        Me.JenisBox.Name = "JenisBox"
        Me.JenisBox.Size = New System.Drawing.Size(275, 20)
        Me.JenisBox.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(23, 13)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 16)
        Me.Label33.TabIndex = 131
        Me.Label33.Text = "Jenis Box :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DGView)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 120)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(704, 441)
        Me.Panel1.TabIndex = 12
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
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID_Request, Me._JenisBox_, Me._Panjang_, Me._Lebar_, Me._Tinggi_, Me.Column1, Me.Column2})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGView.DefaultCellStyle = DataGridViewCellStyle5
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 0)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGView.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DGView.RowHeadersVisible = False
        Me.DGView.Size = New System.Drawing.Size(704, 441)
        Me.DGView.TabIndex = 13
        '
        'ID_Request
        '
        Me.ID_Request.HeaderText = "Id Record"
        Me.ID_Request.MinimumWidth = 2
        Me.ID_Request.Name = "ID_Request"
        Me.ID_Request.ReadOnly = True
        Me.ID_Request.Width = 2
        '
        '_JenisBox_
        '
        Me._JenisBox_.HeaderText = "Jenis Box"
        Me._JenisBox_.Name = "_JenisBox_"
        Me._JenisBox_.ReadOnly = True
        Me._JenisBox_.Width = 200
        '
        '_Panjang_
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me._Panjang_.DefaultCellStyle = DataGridViewCellStyle2
        Me._Panjang_.HeaderText = "Panjang"
        Me._Panjang_.Name = "_Panjang_"
        Me._Panjang_.ReadOnly = True
        '
        '_Lebar_
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me._Lebar_.DefaultCellStyle = DataGridViewCellStyle3
        Me._Lebar_.HeaderText = "Lebar"
        Me._Lebar_.Name = "_Lebar_"
        Me._Lebar_.ReadOnly = True
        '
        '_Tinggi_
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me._Tinggi_.DefaultCellStyle = DataGridViewCellStyle4
        Me._Tinggi_.HeaderText = "Tinggi"
        Me._Tinggi_.Name = "_Tinggi_"
        Me._Tinggi_.ReadOnly = True
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
        'Form_JenisBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 561)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel7)
        Me.Name = "Form_JenisBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jenis Dimensi Box"
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.PanelSimpan.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel7 As Panel
    Friend WithEvents IdRec As TextBox
    Friend WithEvents PanelSimpan As Panel
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents JenisBox As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Lebar As TextBox
    Friend WithEvents Panjang As TextBox
    Friend WithEvents Tinggi As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DGView As DataGridView
    Friend WithEvents ID_Request As DataGridViewTextBoxColumn
    Friend WithEvents _JenisBox_ As DataGridViewTextBoxColumn
    Friend WithEvents _Panjang_ As DataGridViewTextBoxColumn
    Friend WithEvents _Lebar_ As DataGridViewTextBoxColumn
    Friend WithEvents _Tinggi_ As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewButtonColumn
    Friend WithEvents Column2 As DataGridViewButtonColumn
End Class
