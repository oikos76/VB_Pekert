<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KodifNegara
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_KodifNegara))
        Me.PanelEntry = New System.Windows.Forms.Panel()
        Me.cmbBenua = New System.Windows.Forms.ComboBox()
        Me.NamaInternasional = New System.Windows.Forms.TextBox()
        Me.NamaIndonesia = New System.Windows.Forms.TextBox()
        Me.KodeNegara = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdCetak = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.PanelEntry.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEntry
        '
        Me.PanelEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelEntry.Controls.Add(Me.cmbBenua)
        Me.PanelEntry.Controls.Add(Me.NamaInternasional)
        Me.PanelEntry.Controls.Add(Me.NamaIndonesia)
        Me.PanelEntry.Controls.Add(Me.KodeNegara)
        Me.PanelEntry.Controls.Add(Me.Label4)
        Me.PanelEntry.Controls.Add(Me.Label3)
        Me.PanelEntry.Controls.Add(Me.Label2)
        Me.PanelEntry.Controls.Add(Me.Label1)
        Me.PanelEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEntry.Location = New System.Drawing.Point(0, 0)
        Me.PanelEntry.Name = "PanelEntry"
        Me.PanelEntry.Size = New System.Drawing.Size(784, 144)
        Me.PanelEntry.TabIndex = 1
        '
        'cmbBenua
        '
        Me.cmbBenua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBenua.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBenua.FormattingEnabled = True
        Me.cmbBenua.Location = New System.Drawing.Point(236, 13)
        Me.cmbBenua.Name = "cmbBenua"
        Me.cmbBenua.Size = New System.Drawing.Size(350, 24)
        Me.cmbBenua.TabIndex = 7
        '
        'NamaInternasional
        '
        Me.NamaInternasional.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaInternasional.Location = New System.Drawing.Point(237, 109)
        Me.NamaInternasional.Name = "NamaInternasional"
        Me.NamaInternasional.Size = New System.Drawing.Size(349, 26)
        Me.NamaInternasional.TabIndex = 6
        '
        'NamaIndonesia
        '
        Me.NamaIndonesia.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaIndonesia.Location = New System.Drawing.Point(237, 77)
        Me.NamaIndonesia.Name = "NamaIndonesia"
        Me.NamaIndonesia.Size = New System.Drawing.Size(349, 26)
        Me.NamaIndonesia.TabIndex = 5
        '
        'KodeNegara
        '
        Me.KodeNegara.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeNegara.Location = New System.Drawing.Point(237, 45)
        Me.KodeNegara.MaxLength = 2
        Me.KodeNegara.Name = "KodeNegara"
        Me.KodeNegara.Size = New System.Drawing.Size(124, 26)
        Me.KodeNegara.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(218, 18)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Benua               :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(218, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nama Internasional  :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(218, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nama Indonesia      :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(218, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Kode Negara         :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cmdBatal)
        Me.Panel2.Controls.Add(Me.cmdSimpan)
        Me.Panel2.Controls.Add(Me.cmdCetak)
        Me.Panel2.Controls.Add(Me.cmdTambah)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 144)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(784, 40)
        Me.Panel2.TabIndex = 9
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
        Me.cmdSimpan.Location = New System.Drawing.Point(707, 0)
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
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.AllowUserToResizeRows = False
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column1, Me.Column2, Me.Column3, Me.Column5, Me.Column6})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 184)
        Me.DGView.Name = "DGView"
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGView.Size = New System.Drawing.Size(784, 407)
        Me.DGView.TabIndex = 10
        '
        'Column4
        '
        Me.Column4.HeaderText = "Benua"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 120
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
        Me.Column3.HeaderText = "Nama Internasional"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 200
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
        'Form_KodifNegara
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 591)
        Me.Controls.Add(Me.DGView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PanelEntry)
        Me.Name = "Form_KodifNegara"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kodifikasi Negara"
        Me.PanelEntry.ResumeLayout(False)
        Me.PanelEntry.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelEntry As Panel
    Friend WithEvents NamaInternasional As TextBox
    Friend WithEvents NamaIndonesia As TextBox
    Friend WithEvents KodeNegara As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdCetak As Button
    Friend WithEvents cmdTambah As Button
    Friend WithEvents DGView As DataGridView
    Friend WithEvents cmbBenua As ComboBox
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewButtonColumn
    Friend WithEvents Column6 As DataGridViewButtonColumn
End Class
