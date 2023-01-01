<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_BackupDb
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_BackupDb))
        Me.cariFolder = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdBackup = New System.Windows.Forms.Button()
        Me.locFile = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'cariFolder
        '
        Me.cariFolder.Image = CType(resources.GetObject("cariFolder.Image"), System.Drawing.Image)
        Me.cariFolder.Location = New System.Drawing.Point(344, 11)
        Me.cariFolder.Name = "cariFolder"
        Me.cariFolder.Size = New System.Drawing.Size(26, 22)
        Me.cariFolder.TabIndex = 37
        Me.cariFolder.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(19, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 15)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "File name  :"
        '
        'cmdBackup
        '
        Me.cmdBackup.Image = CType(resources.GetObject("cmdBackup.Image"), System.Drawing.Image)
        Me.cmdBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBackup.Location = New System.Drawing.Point(96, 38)
        Me.cmdBackup.Name = "cmdBackup"
        Me.cmdBackup.Size = New System.Drawing.Size(77, 28)
        Me.cmdBackup.TabIndex = 34
        Me.cmdBackup.Text = "&Backup"
        Me.cmdBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBackup.UseVisualStyleBackColor = True
        '
        'locFile
        '
        Me.locFile.Location = New System.Drawing.Point(96, 12)
        Me.locFile.Name = "locFile"
        Me.locFile.ReadOnly = True
        Me.locFile.Size = New System.Drawing.Size(246, 20)
        Me.locFile.TabIndex = 36
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Form_BackupDb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 83)
        Me.Controls.Add(Me.cariFolder)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmdBackup)
        Me.Controls.Add(Me.locFile)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_BackupDb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup Database"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cariFolder As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents cmdBackup As Button
    Friend WithEvents locFile As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
