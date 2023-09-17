<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_SaldoAwal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_SaldoAwal))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelTombol = New System.Windows.Forms.Panel()
        Me.PanelPeriode = New System.Windows.Forms.Panel()
        Me.IDRec = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tPeriode = New System.Windows.Forms.ComboBox()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelSetupPeriode = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PeriodeSaldo = New System.Windows.Forms.DateTimePicker()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnSimpanSetup = New System.Windows.Forms.Button()
        Me.btnBatalSetup = New System.Windows.Forms.Button()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelTombol.SuspendLayout()
        Me.PanelPeriode.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PanelSetupPeriode.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelTombol
        '
        Me.PanelTombol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTombol.Controls.Add(Me.PanelPeriode)
        Me.PanelTombol.Controls.Add(Me.cmdBatal)
        Me.PanelTombol.Controls.Add(Me.cmdExit)
        Me.PanelTombol.Controls.Add(Me.cmdPrint)
        Me.PanelTombol.Controls.Add(Me.cmdHapus)
        Me.PanelTombol.Controls.Add(Me.cmdEdit)
        Me.PanelTombol.Controls.Add(Me.cmdSimpan)
        Me.PanelTombol.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTombol.Location = New System.Drawing.Point(0, 0)
        Me.PanelTombol.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelTombol.Name = "PanelTombol"
        Me.PanelTombol.Size = New System.Drawing.Size(1004, 38)
        Me.PanelTombol.TabIndex = 147
        '
        'PanelPeriode
        '
        Me.PanelPeriode.Controls.Add(Me.IDRec)
        Me.PanelPeriode.Controls.Add(Me.Label6)
        Me.PanelPeriode.Controls.Add(Me.tPeriode)
        Me.PanelPeriode.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelPeriode.Location = New System.Drawing.Point(385, 0)
        Me.PanelPeriode.Name = "PanelPeriode"
        Me.PanelPeriode.Size = New System.Drawing.Size(391, 36)
        Me.PanelPeriode.TabIndex = 66
        '
        'IDRec
        '
        Me.IDRec.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IDRec.Location = New System.Drawing.Point(220, 5)
        Me.IDRec.Margin = New System.Windows.Forms.Padding(4)
        Me.IDRec.Multiline = True
        Me.IDRec.Name = "IDRec"
        Me.IDRec.Size = New System.Drawing.Size(89, 25)
        Me.IDRec.TabIndex = 189
        Me.IDRec.Text = "123456789012345"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 11)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 16)
        Me.Label6.TabIndex = 167
        Me.Label6.Text = "Periode GL :"
        '
        'tPeriode
        '
        Me.tPeriode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tPeriode.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPeriode.FormattingEnabled = True
        Me.tPeriode.Location = New System.Drawing.Point(119, 6)
        Me.tPeriode.Margin = New System.Windows.Forms.Padding(4)
        Me.tPeriode.Name = "tPeriode"
        Me.tPeriode.Size = New System.Drawing.Size(93, 25)
        Me.tPeriode.TabIndex = 168
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(308, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 36)
        Me.cmdBatal.TabIndex = 65
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        Me.cmdBatal.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(231, 0)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 36)
        Me.cmdExit.TabIndex = 64
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(154, 0)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(77, 36)
        Me.cmdPrint.TabIndex = 63
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdHapus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(77, 0)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(77, 36)
        Me.cmdHapus.TabIndex = 62
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(0, 0)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 36)
        Me.cmdEdit.TabIndex = 61
        Me.cmdEdit.Text = "&Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(913, 0)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(89, 36)
        Me.cmdSimpan.TabIndex = 60
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        Me.cmdSimpan.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PanelSetupPeriode)
        Me.Panel1.Controls.Add(Me.DGView)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1004, 543)
        Me.Panel1.TabIndex = 148
        '
        'PanelSetupPeriode
        '
        Me.PanelSetupPeriode.BackColor = System.Drawing.Color.DarkCyan
        Me.PanelSetupPeriode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSetupPeriode.Controls.Add(Me.Panel2)
        Me.PanelSetupPeriode.Location = New System.Drawing.Point(308, 28)
        Me.PanelSetupPeriode.Name = "PanelSetupPeriode"
        Me.PanelSetupPeriode.Size = New System.Drawing.Size(382, 132)
        Me.PanelSetupPeriode.TabIndex = 67
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PeriodeSaldo)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(8, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(363, 114)
        Me.Panel2.TabIndex = 325
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 10)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(264, 17)
        Me.Label2.TabIndex = 324
        Me.Label2.Text = "Saldo Akhir BELUM pernah di buat."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 36)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(243, 17)
        Me.Label1.TabIndex = 168
        Me.Label1.Text = "buat saldo akhir untuk periode  :"
        '
        'PeriodeSaldo
        '
        Me.PeriodeSaldo.CustomFormat = "MM-yyyy"
        Me.PeriodeSaldo.Font = New System.Drawing.Font("Arial Rounded MT Bold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PeriodeSaldo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.PeriodeSaldo.Location = New System.Drawing.Point(258, 34)
        Me.PeriodeSaldo.Margin = New System.Windows.Forms.Padding(4)
        Me.PeriodeSaldo.Name = "PeriodeSaldo"
        Me.PeriodeSaldo.Size = New System.Drawing.Size(91, 25)
        Me.PeriodeSaldo.TabIndex = 322
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnSimpanSetup)
        Me.Panel3.Controls.Add(Me.btnBatalSetup)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 83)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(363, 31)
        Me.Panel3.TabIndex = 323
        '
        'btnSimpanSetup
        '
        Me.btnSimpanSetup.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSimpanSetup.Image = CType(resources.GetObject("btnSimpanSetup.Image"), System.Drawing.Image)
        Me.btnSimpanSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSimpanSetup.Location = New System.Drawing.Point(274, 0)
        Me.btnSimpanSetup.Name = "btnSimpanSetup"
        Me.btnSimpanSetup.Size = New System.Drawing.Size(89, 31)
        Me.btnSimpanSetup.TabIndex = 67
        Me.btnSimpanSetup.Text = "&Simpan"
        Me.btnSimpanSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSimpanSetup.UseVisualStyleBackColor = True
        '
        'btnBatalSetup
        '
        Me.btnBatalSetup.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBatalSetup.Image = CType(resources.GetObject("btnBatalSetup.Image"), System.Drawing.Image)
        Me.btnBatalSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBatalSetup.Location = New System.Drawing.Point(0, 0)
        Me.btnBatalSetup.Name = "btnBatalSetup"
        Me.btnBatalSetup.Size = New System.Drawing.Size(77, 31)
        Me.btnBatalSetup.TabIndex = 66
        Me.btnBatalSetup.Text = "&Batal"
        Me.btnBatalSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBatalSetup.UseVisualStyleBackColor = True
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column6, Me.Column5})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.Location = New System.Drawing.Point(0, 0)
        Me.DGView.Margin = New System.Windows.Forms.Padding(4)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.RowHeadersVisible = False
        Me.DGView.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGView.RowTemplate.ReadOnly = True
        Me.DGView.Size = New System.Drawing.Size(1002, 541)
        Me.DGView.TabIndex = 157
        '
        'Column4
        '
        Me.Column4.HeaderText = "Kode GL"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 250
        '
        'Column6
        '
        Me.Column6.HeaderText = "Keterangan"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 500
        '
        'Column5
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column5.HeaderText = "Saldo Akhir"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 200
        '
        'Form_SaldoAwal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 581)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelTombol)
        Me.Name = "Form_SaldoAwal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Saldo Akhir"
        Me.PanelTombol.ResumeLayout(False)
        Me.PanelPeriode.ResumeLayout(False)
        Me.PanelPeriode.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PanelSetupPeriode.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTombol As Panel
    Friend WithEvents cmdHapus As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelPeriode As Panel
    Friend WithEvents DGView As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Label6 As Label
    Friend WithEvents tPeriode As ComboBox
    Friend WithEvents IDRec As TextBox
    Friend WithEvents PanelSetupPeriode As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnSimpanSetup As Button
    Friend WithEvents btnBatalSetup As Button
    Friend WithEvents PeriodeSaldo As DateTimePicker
    Friend WithEvents Panel2 As Panel
End Class
