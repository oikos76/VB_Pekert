<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KodifBahanBaku
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KodifBahanBaku))
        Me.PanelEntry = New System.Windows.Forms.Panel()
        Me.HSNo = New System.Windows.Forms.TextBox()
        Me.NamaInggris = New System.Windows.Forms.TextBox()
        Me.NamaIndonesia = New System.Windows.Forms.TextBox()
        Me.KodeBahan = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdCetak = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.PanelEntry.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEntry
        '
        Me.PanelEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelEntry.Controls.Add(Me.HSNo)
        Me.PanelEntry.Controls.Add(Me.NamaInggris)
        Me.PanelEntry.Controls.Add(Me.NamaIndonesia)
        Me.PanelEntry.Controls.Add(Me.KodeBahan)
        Me.PanelEntry.Controls.Add(Me.Label4)
        Me.PanelEntry.Controls.Add(Me.Label3)
        Me.PanelEntry.Controls.Add(Me.Label2)
        Me.PanelEntry.Controls.Add(Me.Label1)
        Me.PanelEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEntry.Location = New System.Drawing.Point(0, 0)
        Me.PanelEntry.Name = "PanelEntry"
        Me.PanelEntry.Size = New System.Drawing.Size(784, 150)
        Me.PanelEntry.TabIndex = 0
        '
        'HSNo
        '
        Me.HSNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HSNo.Location = New System.Drawing.Point(211, 110)
        Me.HSNo.Name = "HSNo"
        Me.HSNo.Size = New System.Drawing.Size(124, 26)
        Me.HSNo.TabIndex = 7
        '
        'NamaInggris
        '
        Me.NamaInggris.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaInggris.Location = New System.Drawing.Point(211, 76)
        Me.NamaInggris.Name = "NamaInggris"
        Me.NamaInggris.Size = New System.Drawing.Size(349, 26)
        Me.NamaInggris.TabIndex = 6
        '
        'NamaIndonesia
        '
        Me.NamaIndonesia.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaIndonesia.Location = New System.Drawing.Point(211, 44)
        Me.NamaIndonesia.Name = "NamaIndonesia"
        Me.NamaIndonesia.Size = New System.Drawing.Size(349, 26)
        Me.NamaIndonesia.TabIndex = 5
        '
        'KodeBahan
        '
        Me.KodeBahan.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeBahan.Location = New System.Drawing.Point(211, 10)
        Me.KodeBahan.MaxLength = 2
        Me.KodeBahan.Name = "KodeBahan"
        Me.KodeBahan.Size = New System.Drawing.Size(124, 26)
        Me.KodeBahan.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(188, 18)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "HS No.           :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(188, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nama Inggris     :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(188, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nama Indonesia   :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Kode Bahan       :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cmdBatal)
        Me.Panel2.Controls.Add(Me.cmdSimpan)
        Me.Panel2.Controls.Add(Me.cmdCetak)
        Me.Panel2.Controls.Add(Me.cmdTambah)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(780, 40)
        Me.Panel2.TabIndex = 8
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(154, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 40)
        Me.cmdBatal.TabIndex = 23
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(703, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(77, 40)
        Me.cmdSimpan.TabIndex = 22
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'cmdCetak
        '
        Me.cmdCetak.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdCetak.Image = CType(resources.GetObject("cmdCetak.Image"), System.Drawing.Image)
        Me.cmdCetak.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCetak.Location = New System.Drawing.Point(77, 0)
        Me.cmdCetak.Name = "cmdCetak"
        Me.cmdCetak.Size = New System.Drawing.Size(77, 40)
        Me.cmdCetak.TabIndex = 11
        Me.cmdCetak.Text = "Cetak"
        Me.cmdCetak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCetak.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(0, 0)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(77, 40)
        Me.cmdTambah.TabIndex = 2
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.DGView)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 150)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 441)
        Me.Panel1.TabIndex = 9
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.AllowUserToResizeRows = False
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 40)
        Me.DGView.Name = "DGView"
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGView.Size = New System.Drawing.Size(780, 397)
        Me.DGView.TabIndex = 9
        '
        'Column1
        '
        Me.Column1.DividerWidth = 1
        Me.Column1.HeaderText = "Kode"
        Me.Column1.MinimumWidth = 2
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Nama Indonesia"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 200
        '
        'Column3
        '
        Me.Column3.HeaderText = "Nama Inggris"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 200
        '
        'Column4
        '
        Me.Column4.HeaderText = "HS No."
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 120
        '
        'Column5
        '
        Me.Column5.HeaderText = "Edit"
        Me.Column5.Name = "Column5"
        Me.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column6
        '
        Me.Column6.HeaderText = "Hapus"
        Me.Column6.Name = "Column6"
        '
        'Form_KodifBahanBaku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 591)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelEntry)
        Me.Name = "Form_KodifBahanBaku"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kodifikasi Bahan Baku"
        Me.PanelEntry.ResumeLayout(False)
        Me.PanelEntry.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelEntry As Panel
    Friend WithEvents HSNo As TextBox
    Friend WithEvents NamaInggris As TextBox
    Friend WithEvents NamaIndonesia As TextBox
    Friend WithEvents KodeBahan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmdCetak As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DGView As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewButtonColumn
    Friend WithEvents Column6 As DataGridViewButtonColumn
End Class
