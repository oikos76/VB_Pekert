<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_BackupRestore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_BackupRestore))
        Me.cariFolder = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.locFile = New System.Windows.Forms.TextBox()
        Me.cmdRestore = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'cariFolder
        '
        Me.cariFolder.Image = CType(resources.GetObject("cariFolder.Image"), System.Drawing.Image)
        Me.cariFolder.Location = New System.Drawing.Point(522, 32)
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
        Me.Label6.Location = New System.Drawing.Point(21, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 15)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "File name  :"
        '
        'locFile
        '
        Me.locFile.Location = New System.Drawing.Point(98, 34)
        Me.locFile.Name = "locFile"
        Me.locFile.ReadOnly = True
        Me.locFile.Size = New System.Drawing.Size(418, 20)
        Me.locFile.TabIndex = 40
        '
        'cmdRestore
        '
        Me.cmdRestore.Image = CType(resources.GetObject("cmdRestore.Image"), System.Drawing.Image)
        Me.cmdRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRestore.Location = New System.Drawing.Point(98, 60)
        Me.cmdRestore.Name = "cmdRestore"
        Me.cmdRestore.Size = New System.Drawing.Size(77, 28)
        Me.cmdRestore.TabIndex = 43
        Me.cmdRestore.Text = "&Restore"
        Me.cmdRestore.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdRestore.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Form_BackupRestore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 109)
        Me.Controls.Add(Me.cmdRestore)
        Me.Controls.Add(Me.cariFolder)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.locFile)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_BackupRestore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Restore Database"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cariFolder As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents locFile As TextBox
    Friend WithEvents cmdRestore As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
