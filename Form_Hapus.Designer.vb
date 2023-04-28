<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Hapus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Hapus))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tIDSemua = New System.Windows.Forms.TextBox()
        Me.tIDSebagian = New System.Windows.Forms.TextBox()
        Me.OptSebagian = New System.Windows.Forms.RadioButton()
        Me.OptSemua = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tIDSemua)
        Me.GroupBox1.Controls.Add(Me.tIDSebagian)
        Me.GroupBox1.Controls.Add(Me.OptSebagian)
        Me.GroupBox1.Controls.Add(Me.OptSemua)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5)
        Me.GroupBox1.Size = New System.Drawing.Size(332, 165)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Jenis data yang akan di hapus :"
        '
        'tIDSemua
        '
        Me.tIDSemua.BackColor = System.Drawing.SystemColors.Window
        Me.tIDSemua.Location = New System.Drawing.Point(183, 46)
        Me.tIDSemua.Name = "tIDSemua"
        Me.tIDSemua.ReadOnly = True
        Me.tIDSemua.Size = New System.Drawing.Size(124, 26)
        Me.tIDSemua.TabIndex = 3
        Me.tIDSemua.Text = "2102LH00004"
        '
        'tIDSebagian
        '
        Me.tIDSebagian.BackColor = System.Drawing.SystemColors.Window
        Me.tIDSebagian.Location = New System.Drawing.Point(183, 99)
        Me.tIDSebagian.Name = "tIDSebagian"
        Me.tIDSebagian.ReadOnly = True
        Me.tIDSebagian.Size = New System.Drawing.Size(124, 26)
        Me.tIDSebagian.TabIndex = 2
        Me.tIDSebagian.Text = "2102LH00004"
        '
        'OptSebagian
        '
        Me.OptSebagian.AutoSize = True
        Me.OptSebagian.Location = New System.Drawing.Point(33, 102)
        Me.OptSebagian.Name = "OptSebagian"
        Me.OptSebagian.Size = New System.Drawing.Size(146, 24)
        Me.OptSebagian.TabIndex = 1
        Me.OptSebagian.TabStop = True
        Me.OptSebagian.Text = "Hapus Sebagian"
        Me.OptSebagian.UseVisualStyleBackColor = True
        '
        'OptSemua
        '
        Me.OptSemua.AutoSize = True
        Me.OptSemua.Location = New System.Drawing.Point(33, 49)
        Me.OptSemua.Name = "OptSemua"
        Me.OptSemua.Size = New System.Drawing.Size(129, 24)
        Me.OptSemua.TabIndex = 0
        Me.OptSemua.TabStop = True
        Me.OptSemua.Text = "Hapus Semua"
        Me.OptSemua.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdCancel)
        Me.Panel1.Controls.Add(Me.cmdHapus)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 167)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(334, 33)
        Me.Panel1.TabIndex = 1
        '
        'cmdCancel
        '
        Me.cmdCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(268, 0)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(66, 33)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmdHapus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHapus.Location = New System.Drawing.Point(0, 0)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(66, 33)
        Me.cmdHapus.TabIndex = 0
        Me.cmdHapus.Text = "Hapus"
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(334, 167)
        Me.Panel2.TabIndex = 2
        '
        'Form_Hapus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 200)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Hapus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hapus"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tIDSemua As TextBox
    Friend WithEvents tIDSebagian As TextBox
    Friend WithEvents OptSebagian As RadioButton
    Friend WithEvents OptSemua As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cmdCancel As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents Panel2 As Panel
End Class
