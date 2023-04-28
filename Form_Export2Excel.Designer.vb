<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Export2Excel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Export2Excel))
        Me.cariFolder = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdProses = New System.Windows.Forms.Button()
        Me.locFile = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PanelTombol = New System.Windows.Forms.Panel()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.idRec = New System.Windows.Forms.TextBox()
        Me.JenisTR = New System.Windows.Forms.TextBox()
        Me.PanelTombol.SuspendLayout()
        Me.SuspendLayout()
        '
        'cariFolder
        '
        Me.cariFolder.Image = CType(resources.GetObject("cariFolder.Image"), System.Drawing.Image)
        Me.cariFolder.Location = New System.Drawing.Point(359, 48)
        Me.cariFolder.Name = "cariFolder"
        Me.cariFolder.Size = New System.Drawing.Size(26, 22)
        Me.cariFolder.TabIndex = 41
        Me.cariFolder.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 15)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Folder name  :"
        '
        'cmdProses
        '
        Me.cmdProses.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdProses.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProses.Image = CType(resources.GetObject("cmdProses.Image"), System.Drawing.Image)
        Me.cmdProses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdProses.Location = New System.Drawing.Point(324, 0)
        Me.cmdProses.Name = "cmdProses"
        Me.cmdProses.Size = New System.Drawing.Size(77, 31)
        Me.cmdProses.TabIndex = 38
        Me.cmdProses.Text = "&Proses"
        Me.cmdProses.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdProses.UseVisualStyleBackColor = True
        '
        'locFile
        '
        Me.locFile.BackColor = System.Drawing.SystemColors.Window
        Me.locFile.Location = New System.Drawing.Point(112, 49)
        Me.locFile.Name = "locFile"
        Me.locFile.ReadOnly = True
        Me.locFile.Size = New System.Drawing.Size(246, 20)
        Me.locFile.TabIndex = 40
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PanelTombol
        '
        Me.PanelTombol.Controls.Add(Me.cmdBatal)
        Me.PanelTombol.Controls.Add(Me.cmdProses)
        Me.PanelTombol.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelTombol.Location = New System.Drawing.Point(0, 100)
        Me.PanelTombol.Name = "PanelTombol"
        Me.PanelTombol.Size = New System.Drawing.Size(401, 31)
        Me.PanelTombol.TabIndex = 42
        '
        'cmdBatal
        '
        Me.cmdBatal.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(0, 0)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 31)
        Me.cmdBatal.TabIndex = 39
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Id Record  :"
        '
        'idRec
        '
        Me.idRec.BackColor = System.Drawing.SystemColors.Window
        Me.idRec.Location = New System.Drawing.Point(112, 23)
        Me.idRec.Name = "idRec"
        Me.idRec.ReadOnly = True
        Me.idRec.Size = New System.Drawing.Size(116, 20)
        Me.idRec.TabIndex = 44
        '
        'JenisTR
        '
        Me.JenisTR.BackColor = System.Drawing.SystemColors.Window
        Me.JenisTR.Location = New System.Drawing.Point(234, 24)
        Me.JenisTR.Name = "JenisTR"
        Me.JenisTR.ReadOnly = True
        Me.JenisTR.Size = New System.Drawing.Size(116, 20)
        Me.JenisTR.TabIndex = 45
        '
        'Form_Export2Excel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 131)
        Me.Controls.Add(Me.JenisTR)
        Me.Controls.Add(Me.idRec)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PanelTombol)
        Me.Controls.Add(Me.cariFolder)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.locFile)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Export2Excel"
        Me.Text = "Export ke Excel"
        Me.PanelTombol.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cariFolder As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents cmdProses As Button
    Friend WithEvents locFile As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents PanelTombol As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents idRec As TextBox
    Friend WithEvents cmdBatal As Button
    Friend WithEvents JenisTR As TextBox
End Class
