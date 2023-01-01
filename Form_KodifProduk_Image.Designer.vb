<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_KodifProduk_Image
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
        Me.PanelPicture = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LocGmb1 = New System.Windows.Forms.Label()
        Me.PanelPicture.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelPicture
        '
        Me.PanelPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelPicture.Controls.Add(Me.LocGmb1)
        Me.PanelPicture.Controls.Add(Me.PictureBox1)
        Me.PanelPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelPicture.Location = New System.Drawing.Point(0, 0)
        Me.PanelPicture.Name = "PanelPicture"
        Me.PanelPicture.Size = New System.Drawing.Size(584, 561)
        Me.PanelPicture.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(580, 557)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'LocGmb1
        '
        Me.LocGmb1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LocGmb1.Location = New System.Drawing.Point(0, 541)
        Me.LocGmb1.Name = "LocGmb1"
        Me.LocGmb1.Size = New System.Drawing.Size(580, 16)
        Me.LocGmb1.TabIndex = 3
        Me.LocGmb1.Text = "LocGmb1"
        Me.LocGmb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form_KodifProduk_Image
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.PanelPicture)
        Me.Name = "Form_KodifProduk_Image"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview Image"
        Me.PanelPicture.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelPicture As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LocGmb1 As Label
End Class
