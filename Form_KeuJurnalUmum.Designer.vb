<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KeuJurnalUmum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KeuJurnalUmum))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Importir = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ShipmentDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cmdPrint)
        Me.Panel4.Controls.Add(Me.cmdSimpan)
        Me.Panel4.Controls.Add(Me.cmdEdit)
        Me.Panel4.Controls.Add(Me.cmdHapus)
        Me.Panel4.Controls.Add(Me.cmdExit)
        Me.Panel4.Controls.Add(Me.cmdTambah)
        Me.Panel4.Controls.Add(Me.cmdBatal)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 565)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(784, 36)
        Me.Panel4.TabIndex = 147
        '
        'cmdPrint
        '
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(337, 3)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(77, 28)
        Me.cmdPrint.TabIndex = 8
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(693, 0)
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
        Me.cmdExit.Location = New System.Drawing.Point(254, 3)
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TextBox3)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Importir)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.ShipmentDate)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 204)
        Me.Panel1.TabIndex = 148
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(160, 30)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox3.MaxLength = 4
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(137, 15)
        Me.TextBox3.TabIndex = 223
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(160, 139)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.MaxLength = 4
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(137, 22)
        Me.TextBox2.TabIndex = 222
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(160, 112)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.MaxLength = 4
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(137, 22)
        Me.TextBox1.TabIndex = 221
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 29)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 16)
        Me.Label4.TabIndex = 220
        Me.Label4.Text = "No. Jurnal     :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 142)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Jumlah         :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 115)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 16)
        Me.Label3.TabIndex = 218
        Me.Label3.Text = "No. Perkiraan  :"
        '
        'Importir
        '
        Me.Importir.BackColor = System.Drawing.SystemColors.Window
        Me.Importir.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Importir.Location = New System.Drawing.Point(160, 85)
        Me.Importir.Margin = New System.Windows.Forms.Padding(4)
        Me.Importir.MaxLength = 4
        Me.Importir.Name = "Importir"
        Me.Importir.ReadOnly = True
        Me.Importir.Size = New System.Drawing.Size(611, 22)
        Me.Importir.TabIndex = 216
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 88)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 16)
        Me.Label6.TabIndex = 217
        Me.Label6.Text = "Keterangan     :"
        '
        'ShipmentDate
        '
        Me.ShipmentDate.CustomFormat = "dd-MM-yyyy"
        Me.ShipmentDate.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShipmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ShipmentDate.Location = New System.Drawing.Point(160, 57)
        Me.ShipmentDate.Margin = New System.Windows.Forms.Padding(4)
        Me.ShipmentDate.Name = "ShipmentDate"
        Me.ShipmentDate.Size = New System.Drawing.Size(137, 24)
        Me.ShipmentDate.TabIndex = 175
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 64)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 16)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "Tanggal        :"
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column9, Me.Column10, Me.Column11, Me.Column12, Me.Column13, Me.Column1})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.Location = New System.Drawing.Point(0, 204)
        Me.DGView.Margin = New System.Windows.Forms.Padding(4)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.ReadOnly = True
        Me.DGView.Size = New System.Drawing.Size(784, 361)
        Me.DGView.TabIndex = 157
        '
        'Column9
        '
        Me.Column9.HeaderText = "No."
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 125
        '
        'Column10
        '
        Me.Column10.HeaderText = "Uraian"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 200
        '
        'Column11
        '
        Me.Column11.HeaderText = "Account Code"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 120
        '
        'Column12
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column12.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column12.HeaderText = "Ket.Account"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 130
        '
        'Column13
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column13.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column13.HeaderText = "Debet"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Kredit"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Form_KeuJurnalUmum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 601)
        Me.Controls.Add(Me.DGView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "Form_KeuJurnalUmum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jurnal Umum"
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents cmdPrint As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DGView As DataGridView
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents ShipmentDate As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Importir As TextBox
    Friend WithEvents Label6 As Label
End Class
