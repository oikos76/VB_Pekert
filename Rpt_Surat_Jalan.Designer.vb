<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rpt_Surat_Jalan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rpt_Surat_Jalan))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbJenisLaporan = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbStatusSJ = New System.Windows.Forms.ComboBox()
        Me.LabelStatusSJ = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdCetak = New System.Windows.Forms.Button()
        Me.chkRekap = New System.Windows.Forms.CheckBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.kd_asal = New System.Windows.Forms.TextBox()
        Me.TkAsal = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Kd_Toko7an = New System.Windows.Forms.TextBox()
        Me.Toko7an = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.LStrip = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PanelKategori = New System.Windows.Forms.Panel()
        Me.cmb_Kategori = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_SubKategori = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.IdRecord = New System.Windows.Forms.TextBox()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.PanelKategori.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbJenisLaporan)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cmbStatusSJ)
        Me.Panel1.Controls.Add(Me.LabelStatusSJ)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.chkRekap)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.kd_asal)
        Me.Panel1.Controls.Add(Me.TkAsal)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.Kd_Toko7an)
        Me.Panel1.Controls.Add(Me.Toko7an)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.LStrip)
        Me.Panel1.Controls.Add(Me.Tgl2)
        Me.Panel1.Controls.Add(Me.Tgl1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PanelKategori)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(994, 92)
        Me.Panel1.TabIndex = 1
        '
        'cmbJenisLaporan
        '
        Me.cmbJenisLaporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJenisLaporan.FormattingEnabled = True
        Me.cmbJenisLaporan.Location = New System.Drawing.Point(599, 7)
        Me.cmbJenisLaporan.Name = "cmbJenisLaporan"
        Me.cmbJenisLaporan.Size = New System.Drawing.Size(281, 21)
        Me.cmbJenisLaporan.TabIndex = 208
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(488, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(105, 16)
        Me.Label7.TabIndex = 207
        Me.Label7.Text = "Jenis Laporan   :"
        '
        'cmbStatusSJ
        '
        Me.cmbStatusSJ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusSJ.FormattingEnabled = True
        Me.cmbStatusSJ.Location = New System.Drawing.Point(599, 36)
        Me.cmbStatusSJ.Name = "cmbStatusSJ"
        Me.cmbStatusSJ.Size = New System.Drawing.Size(125, 21)
        Me.cmbStatusSJ.TabIndex = 205
        '
        'LabelStatusSJ
        '
        Me.LabelStatusSJ.AutoSize = True
        Me.LabelStatusSJ.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStatusSJ.Location = New System.Drawing.Point(517, 38)
        Me.LabelStatusSJ.Name = "LabelStatusSJ"
        Me.LabelStatusSJ.Size = New System.Drawing.Size(76, 16)
        Me.LabelStatusSJ.TabIndex = 203
        Me.LabelStatusSJ.Text = "Status SJ   :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cmdCetak)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(923, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(71, 92)
        Me.Panel2.TabIndex = 202
        '
        'cmdCetak
        '
        Me.cmdCetak.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmdCetak.Image = CType(resources.GetObject("cmdCetak.Image"), System.Drawing.Image)
        Me.cmdCetak.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCetak.Location = New System.Drawing.Point(0, 0)
        Me.cmdCetak.Name = "cmdCetak"
        Me.cmdCetak.Size = New System.Drawing.Size(71, 57)
        Me.cmdCetak.TabIndex = 67
        Me.cmdCetak.Text = "Cetak"
        Me.cmdCetak.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCetak.UseVisualStyleBackColor = True
        '
        'chkRekap
        '
        Me.chkRekap.AutoSize = True
        Me.chkRekap.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRekap.Location = New System.Drawing.Point(345, 10)
        Me.chkRekap.Name = "chkRekap"
        Me.chkRekap.Size = New System.Drawing.Size(68, 20)
        Me.chkRekap.TabIndex = 198
        Me.chkRekap.Text = "Rekap"
        Me.chkRekap.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(95, 65)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(11, 16)
        Me.Label29.TabIndex = 195
        Me.Label29.Text = ":"
        '
        'kd_asal
        '
        Me.kd_asal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.kd_asal.Location = New System.Drawing.Point(112, 35)
        Me.kd_asal.MaxLength = 100
        Me.kd_asal.Name = "kd_asal"
        Me.kd_asal.Size = New System.Drawing.Size(56, 22)
        Me.kd_asal.TabIndex = 193
        Me.kd_asal.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'TkAsal
        '
        Me.TkAsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TkAsal.Location = New System.Drawing.Point(174, 35)
        Me.TkAsal.MaxLength = 100
        Me.TkAsal.Name = "TkAsal"
        Me.TkAsal.ReadOnly = True
        Me.TkAsal.Size = New System.Drawing.Size(283, 22)
        Me.TkAsal.TabIndex = 194
        Me.TkAsal.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(12, 65)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(84, 16)
        Me.Label31.TabIndex = 192
        Me.Label31.Text = "Toko Tujuan"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(95, 38)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(11, 16)
        Me.Label30.TabIndex = 191
        Me.Label30.Text = ":"
        '
        'Kd_Toko7an
        '
        Me.Kd_Toko7an.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kd_Toko7an.Location = New System.Drawing.Point(112, 62)
        Me.Kd_Toko7an.MaxLength = 100
        Me.Kd_Toko7an.Name = "Kd_Toko7an"
        Me.Kd_Toko7an.Size = New System.Drawing.Size(56, 22)
        Me.Kd_Toko7an.TabIndex = 189
        Me.Kd_Toko7an.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Toko7an
        '
        Me.Toko7an.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Toko7an.Location = New System.Drawing.Point(174, 62)
        Me.Toko7an.MaxLength = 100
        Me.Toko7an.Name = "Toko7an"
        Me.Toko7an.ReadOnly = True
        Me.Toko7an.Size = New System.Drawing.Size(283, 22)
        Me.Toko7an.TabIndex = 190
        Me.Toko7an.Text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(12, 38)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(70, 16)
        Me.Label21.TabIndex = 188
        Me.Label21.Text = "Toko Asal"
        '
        'LStrip
        '
        Me.LStrip.AutoSize = True
        Me.LStrip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LStrip.Location = New System.Drawing.Point(205, 10)
        Me.LStrip.Name = "LStrip"
        Me.LStrip.Size = New System.Drawing.Size(26, 15)
        Me.LStrip.TabIndex = 154
        Me.LStrip.Text = "s.d."
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd-MM-yyyy"
        Me.Tgl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(236, 9)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(87, 21)
        Me.Tgl2.TabIndex = 153
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd-MM-yyyy"
        Me.Tgl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(112, 8)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(87, 21)
        Me.Tgl1.TabIndex = 152
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(95, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 16)
        Me.Label3.TabIndex = 116
        Me.Label3.Text = ":"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 114
        Me.Label1.Text = "Periode"
        '
        'PanelKategori
        '
        Me.PanelKategori.Controls.Add(Me.cmb_Kategori)
        Me.PanelKategori.Controls.Add(Me.Label2)
        Me.PanelKategori.Controls.Add(Me.Label4)
        Me.PanelKategori.Controls.Add(Me.cmb_SubKategori)
        Me.PanelKategori.Controls.Add(Me.Label14)
        Me.PanelKategori.Controls.Add(Me.Label5)
        Me.PanelKategori.Location = New System.Drawing.Point(488, 31)
        Me.PanelKategori.Name = "PanelKategori"
        Me.PanelKategori.Size = New System.Drawing.Size(411, 60)
        Me.PanelKategori.TabIndex = 206
        '
        'cmb_Kategori
        '
        Me.cmb_Kategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Kategori.FormattingEnabled = True
        Me.cmb_Kategori.Location = New System.Drawing.Point(111, 6)
        Me.cmb_Kategori.Name = "cmb_Kategori"
        Me.cmb_Kategori.Size = New System.Drawing.Size(281, 21)
        Me.cmb_Kategori.TabIndex = 197
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 16)
        Me.Label2.TabIndex = 115
        Me.Label2.Text = "Kategori"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(94, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 16)
        Me.Label4.TabIndex = 117
        Me.Label4.Text = ":"
        '
        'cmb_SubKategori
        '
        Me.cmb_SubKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_SubKategori.FormattingEnabled = True
        Me.cmb_SubKategori.Location = New System.Drawing.Point(111, 32)
        Me.cmb_SubKategori.Name = "cmb_SubKategori"
        Me.cmb_SubKategori.Size = New System.Drawing.Size(281, 21)
        Me.cmb_SubKategori.TabIndex = 199
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(5, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 16)
        Me.Label14.TabIndex = 200
        Me.Label14.Text = "Sub Kategori"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(94, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 16)
        Me.Label5.TabIndex = 201
        Me.Label5.Text = ":"
        '
        'IdRecord
        '
        Me.IdRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IdRecord.Location = New System.Drawing.Point(12, 63)
        Me.IdRecord.Name = "IdRecord"
        Me.IdRecord.Size = New System.Drawing.Size(89, 21)
        Me.IdRecord.TabIndex = 155
        Me.IdRecord.Text = "211517B001"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 92)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ShowCloseButton = False
        Me.CrystalReportViewer1.ShowExportButton = False
        Me.CrystalReportViewer1.ShowGroupTreeButton = False
        Me.CrystalReportViewer1.ShowLogo = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(994, 499)
        Me.CrystalReportViewer1.TabIndex = 4
        '
        'Rpt_Surat_Jalan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 591)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.IdRecord)
        Me.Name = "Rpt_Surat_Jalan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Laporan Surat Jalan"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.PanelKategori.ResumeLayout(False)
        Me.PanelKategori.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents IdRecord As TextBox
    Friend WithEvents LStrip As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdCetak As Button
    Friend WithEvents CrystalReportViewer1 As CrystalReportViewer
    Friend WithEvents Label29 As Label
    Friend WithEvents kd_asal As TextBox
    Friend WithEvents TkAsal As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Kd_Toko7an As TextBox
    Friend WithEvents Toko7an As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents cmb_Kategori As ComboBox
    Friend WithEvents chkRekap As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents cmb_SubKategori As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmbStatusSJ As ComboBox
    Friend WithEvents LabelStatusSJ As Label
    Friend WithEvents cmbJenisLaporan As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents PanelKategori As Panel
End Class
