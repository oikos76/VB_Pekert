<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rpt_GeneralLedger
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rpt_GeneralLedger))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.KetAccCode1 = New System.Windows.Forms.Label()
        Me.AccCode1 = New System.Windows.Forms.TextBox()
        Me.cmbJenisLaporan = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdCetak = New System.Windows.Forms.Button()
        Me.LStrip = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.KetAccCode1)
        Me.Panel1.Controls.Add(Me.AccCode1)
        Me.Panel1.Controls.Add(Me.cmbJenisLaporan)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.LStrip)
        Me.Panel1.Controls.Add(Me.Tgl2)
        Me.Panel1.Controls.Add(Me.Tgl1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1004, 63)
        Me.Panel1.TabIndex = 6
        '
        'KetAccCode1
        '
        Me.KetAccCode1.AutoSize = True
        Me.KetAccCode1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KetAccCode1.Location = New System.Drawing.Point(390, 39)
        Me.KetAccCode1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.KetAccCode1.Name = "KetAccCode1"
        Me.KetAccCode1.Size = New System.Drawing.Size(90, 16)
        Me.KetAccCode1.TabIndex = 226
        Me.KetAccCode1.Text = "KetAccCode1"
        '
        'AccCode1
        '
        Me.AccCode1.BackColor = System.Drawing.SystemColors.Window
        Me.AccCode1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccCode1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.AccCode1.Location = New System.Drawing.Point(248, 36)
        Me.AccCode1.Margin = New System.Windows.Forms.Padding(4)
        Me.AccCode1.MaxLength = 255
        Me.AccCode1.Name = "AccCode1"
        Me.AccCode1.Size = New System.Drawing.Size(134, 22)
        Me.AccCode1.TabIndex = 225
        '
        'cmbJenisLaporan
        '
        Me.cmbJenisLaporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJenisLaporan.FormattingEnabled = True
        Me.cmbJenisLaporan.Location = New System.Drawing.Point(121, 37)
        Me.cmbJenisLaporan.Name = "cmbJenisLaporan"
        Me.cmbJenisLaporan.Size = New System.Drawing.Size(120, 21)
        Me.cmbJenisLaporan.TabIndex = 208
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(105, 16)
        Me.Label7.TabIndex = 207
        Me.Label7.Text = "Jenis Laporan   :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cmdCetak)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(933, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(71, 63)
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
        'LStrip
        '
        Me.LStrip.AutoSize = True
        Me.LStrip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LStrip.Location = New System.Drawing.Point(215, 10)
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
        Me.Tgl2.Location = New System.Drawing.Point(246, 9)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(87, 21)
        Me.Tgl2.TabIndex = 153
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd-MM-yyyy"
        Me.Tgl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(122, 8)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(87, 21)
        Me.Tgl1.TabIndex = 152
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(105, 9)
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
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 63)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ShowCloseButton = False
        Me.CrystalReportViewer1.ShowExportButton = False
        Me.CrystalReportViewer1.ShowGroupTreeButton = False
        Me.CrystalReportViewer1.ShowLogo = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1004, 518)
        Me.CrystalReportViewer1.TabIndex = 7
        '
        'Rpt_GeneralLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 581)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Rpt_GeneralLedger"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "General Ledger"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents cmbJenisLaporan As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmdCetak As Button
    Friend WithEvents LStrip As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CrystalReportViewer1 As CrystalReportViewer
    Friend WithEvents KetAccCode1 As Label
    Friend WithEvents AccCode1 As TextBox
End Class
