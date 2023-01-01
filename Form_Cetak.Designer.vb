<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Cetak
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Cetak))
        Me.btnScreen = New System.Windows.Forms.Button()
        Me.btnPrinter = New System.Windows.Forms.Button()
        Me.cbCetakan = New System.Windows.Forms.ComboBox()
        Me.lbCetakan = New System.Windows.Forms.Label()
        Me.PanelTipe = New System.Windows.Forms.Panel()
        Me.btnSetting = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.NamaPrinter = New System.Windows.Forms.TextBox()
        Me.NamaKertas = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PanelTipe.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnScreen
        '
        Me.btnScreen.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnScreen.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScreen.Image = CType(resources.GetObject("btnScreen.Image"), System.Drawing.Image)
        Me.btnScreen.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnScreen.Location = New System.Drawing.Point(0, 0)
        Me.btnScreen.Name = "btnScreen"
        Me.btnScreen.Size = New System.Drawing.Size(73, 66)
        Me.btnScreen.TabIndex = 0
        Me.btnScreen.Text = "&Layar"
        Me.btnScreen.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScreen.UseVisualStyleBackColor = True
        '
        'btnPrinter
        '
        Me.btnPrinter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPrinter.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrinter.Image = CType(resources.GetObject("btnPrinter.Image"), System.Drawing.Image)
        Me.btnPrinter.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrinter.Location = New System.Drawing.Point(0, 0)
        Me.btnPrinter.Name = "btnPrinter"
        Me.btnPrinter.Size = New System.Drawing.Size(225, 66)
        Me.btnPrinter.TabIndex = 1
        Me.btnPrinter.Text = "&Printer"
        Me.btnPrinter.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrinter.UseVisualStyleBackColor = True
        '
        'cbCetakan
        '
        Me.cbCetakan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCetakan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCetakan.FormattingEnabled = True
        Me.cbCetakan.Location = New System.Drawing.Point(105, 3)
        Me.cbCetakan.Name = "cbCetakan"
        Me.cbCetakan.Size = New System.Drawing.Size(113, 24)
        Me.cbCetakan.TabIndex = 2
        '
        'lbCetakan
        '
        Me.lbCetakan.AutoSize = True
        Me.lbCetakan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCetakan.Location = New System.Drawing.Point(7, 8)
        Me.lbCetakan.Name = "lbCetakan"
        Me.lbCetakan.Size = New System.Drawing.Size(98, 16)
        Me.lbCetakan.TabIndex = 3
        Me.lbCetakan.Text = "Tipe Cetakan : "
        '
        'PanelTipe
        '
        Me.PanelTipe.Controls.Add(Me.lbCetakan)
        Me.PanelTipe.Controls.Add(Me.cbCetakan)
        Me.PanelTipe.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTipe.Location = New System.Drawing.Point(0, 0)
        Me.PanelTipe.Name = "PanelTipe"
        Me.PanelTipe.Size = New System.Drawing.Size(225, 30)
        Me.PanelTipe.TabIndex = 4
        '
        'btnSetting
        '
        Me.btnSetting.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetting.Image = CType(resources.GetObject("btnSetting.Image"), System.Drawing.Image)
        Me.btnSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetting.Location = New System.Drawing.Point(152, 0)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.Size = New System.Drawing.Size(73, 66)
        Me.btnSetting.TabIndex = 2
        Me.btnSetting.Text = "&Setting"
        Me.btnSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetting.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 7)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(43, 13)
        Me.Label14.TabIndex = 130
        Me.Label14.Text = "Printer :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 31)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(43, 13)
        Me.Label15.TabIndex = 131
        Me.Label15.Text = "Kertas :"
        '
        'NamaPrinter
        '
        Me.NamaPrinter.Location = New System.Drawing.Point(50, 5)
        Me.NamaPrinter.MaxLength = 255
        Me.NamaPrinter.Name = "NamaPrinter"
        Me.NamaPrinter.ReadOnly = True
        Me.NamaPrinter.Size = New System.Drawing.Size(164, 20)
        Me.NamaPrinter.TabIndex = 128
        '
        'NamaKertas
        '
        Me.NamaKertas.Location = New System.Drawing.Point(50, 29)
        Me.NamaKertas.MaxLength = 255
        Me.NamaKertas.Name = "NamaKertas"
        Me.NamaKertas.ReadOnly = True
        Me.NamaKertas.Size = New System.Drawing.Size(164, 20)
        Me.NamaKertas.TabIndex = 129
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.NamaKertas)
        Me.Panel2.Controls.Add(Me.NamaPrinter)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 96)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(225, 53)
        Me.Panel2.TabIndex = 108
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnSetting)
        Me.Panel3.Controls.Add(Me.btnScreen)
        Me.Panel3.Controls.Add(Me.btnPrinter)
        Me.Panel3.Location = New System.Drawing.Point(0, 30)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 66)
        Me.Panel3.TabIndex = 109
        '
        'Form_Cetak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(225, 149)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PanelTipe)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Cetak"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cetak"
        Me.PanelTipe.ResumeLayout(False)
        Me.PanelTipe.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnScreen As Button
    Friend WithEvents btnPrinter As Button
    Friend WithEvents cbCetakan As ComboBox
    Friend WithEvents lbCetakan As Label
    Friend WithEvents PanelTipe As Panel
    Friend WithEvents btnSetting As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents NamaPrinter As TextBox
    Friend WithEvents NamaKertas As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
End Class
