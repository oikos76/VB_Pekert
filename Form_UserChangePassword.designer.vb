<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_UserChangePassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_UserChangePassword))
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PasswordBaru = New System.Windows.Forms.TextBox()
        Me.PasswordLama = New System.Windows.Forms.TextBox()
        Me.UserID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdBatal
        '
        Me.cmdBatal.BackColor = System.Drawing.Color.Transparent
        Me.cmdBatal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(132, 111)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(80, 42)
        Me.cmdBatal.TabIndex = 20
        Me.cmdBatal.Text = "Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Password Baru"
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.Transparent
        Me.cmdSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(53, 111)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 42)
        Me.cmdSave.TabIndex = 19
        Me.cmdSave.Text = "Simpan"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Password Lama"
        '
        'PasswordBaru
        '
        Me.PasswordBaru.Location = New System.Drawing.Point(132, 73)
        Me.PasswordBaru.Name = "PasswordBaru"
        Me.PasswordBaru.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordBaru.Size = New System.Drawing.Size(149, 20)
        Me.PasswordBaru.TabIndex = 16
        '
        'PasswordLama
        '
        Me.PasswordLama.Location = New System.Drawing.Point(132, 47)
        Me.PasswordLama.Name = "PasswordLama"
        Me.PasswordLama.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordLama.Size = New System.Drawing.Size(149, 20)
        Me.PasswordLama.TabIndex = 15
        '
        'UserID
        '
        Me.UserID.Location = New System.Drawing.Point(132, 21)
        Me.UserID.Name = "UserID"
        Me.UserID.Size = New System.Drawing.Size(149, 20)
        Me.UserID.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "User ID"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(222, 111)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(59, 20)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "Encrypt"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(222, 133)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(60, 20)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "Decrypt"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Form_UserChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(307, 174)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdBatal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PasswordBaru)
        Me.Controls.Add(Me.PasswordLama)
        Me.Controls.Add(Me.UserID)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_UserChangePassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ganti Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdBatal As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PasswordBaru As System.Windows.Forms.TextBox
    Friend WithEvents PasswordLama As System.Windows.Forms.TextBox
    Friend WithEvents UserID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
