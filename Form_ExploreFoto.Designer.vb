<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_ExploreFoto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_ExploreFoto))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ext = New System.Windows.Forms.TextBox()
        Me.locFile = New System.Windows.Forms.TextBox()
        Me.cariFolder = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnRename = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnRename)
        Me.Panel1.Controls.Add(Me.locFile)
        Me.Panel1.Controls.Add(Me.cariFolder)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ext)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(584, 34)
        Me.Panel1.TabIndex = 4
        '
        'ext
        '
        Me.ext.Location = New System.Drawing.Point(537, 7)
        Me.ext.Name = "ext"
        Me.ext.ReadOnly = True
        Me.ext.Size = New System.Drawing.Size(44, 20)
        Me.ext.TabIndex = 44
        Me.ext.Text = "*.jpg"
        Me.ext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ext.Visible = False
        '
        'locFile
        '
        Me.locFile.Location = New System.Drawing.Point(102, 7)
        Me.locFile.Name = "locFile"
        Me.locFile.ReadOnly = True
        Me.locFile.Size = New System.Drawing.Size(331, 20)
        Me.locFile.TabIndex = 43
        '
        'cariFolder
        '
        Me.cariFolder.Image = CType(resources.GetObject("cariFolder.Image"), System.Drawing.Image)
        Me.cariFolder.Location = New System.Drawing.Point(439, 7)
        Me.cariFolder.Name = "cariFolder"
        Me.cariFolder.Size = New System.Drawing.Size(26, 22)
        Me.cariFolder.TabIndex = 42
        Me.cariFolder.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Nama Folder :"
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 34)
        Me.DGView.Name = "DGView"
        Me.DGView.RowHeadersVisible = False
        Me.DGView.Size = New System.Drawing.Size(584, 527)
        Me.DGView.TabIndex = 5
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnRename
        '
        Me.btnRename.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRename.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRename.Location = New System.Drawing.Point(506, 0)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(78, 34)
        Me.btnRename.TabIndex = 45
        Me.btnRename.Text = "Rename"
        Me.btnRename.UseVisualStyleBackColor = True
        Me.btnRename.Visible = False
        '
        'Form_ExploreFoto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.DGView)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form_ExploreFoto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Explorer File Foto"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents DGView As DataGridView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents cariFolder As Button
    Friend WithEvents locFile As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ext As TextBox
    Friend WithEvents btnRename As Button
End Class
