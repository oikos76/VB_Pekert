<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_ProsesAkhirBulan
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdProses = New System.Windows.Forms.Button()
        Me.status = New System.Windows.Forms.Label()
        Me.xGL = New System.Windows.Forms.DateTimePicker()
        Me.chkPersediaan = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(25, 18)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 17)
        Me.Label6.TabIndex = 169
        Me.Label6.Text = "Periode :"
        '
        'cmdProses
        '
        Me.cmdProses.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdProses.Location = New System.Drawing.Point(119, 72)
        Me.cmdProses.Name = "cmdProses"
        Me.cmdProses.Size = New System.Drawing.Size(107, 34)
        Me.cmdProses.TabIndex = 171
        Me.cmdProses.Text = "&Proses"
        Me.cmdProses.UseVisualStyleBackColor = True
        '
        'status
        '
        Me.status.AutoSize = True
        Me.status.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status.Location = New System.Drawing.Point(25, 107)
        Me.status.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(36, 16)
        Me.status.TabIndex = 172
        Me.status.Text = "Status"
        '
        'xGL
        '
        Me.xGL.CustomFormat = "MM-yyyy"
        Me.xGL.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xGL.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.xGL.Location = New System.Drawing.Point(119, 13)
        Me.xGL.Margin = New System.Windows.Forms.Padding(4)
        Me.xGL.Name = "xGL"
        Me.xGL.Size = New System.Drawing.Size(107, 24)
        Me.xGL.TabIndex = 176
        '
        'chkPersediaan
        '
        Me.chkPersediaan.AutoSize = True
        Me.chkPersediaan.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPersediaan.Location = New System.Drawing.Point(119, 44)
        Me.chkPersediaan.Name = "chkPersediaan"
        Me.chkPersediaan.Size = New System.Drawing.Size(101, 22)
        Me.chkPersediaan.TabIndex = 177
        Me.chkPersediaan.Text = "Persediaan"
        Me.chkPersediaan.UseVisualStyleBackColor = True
        Me.chkPersediaan.Visible = False
        '
        'Form_ProsesAkhirBulan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 132)
        Me.Controls.Add(Me.chkPersediaan)
        Me.Controls.Add(Me.xGL)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.cmdProses)
        Me.Controls.Add(Me.Label6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_ProsesAkhirBulan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Proses Akhir Bulan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents cmdProses As Button
    Friend WithEvents status As Label
    Friend WithEvents xGL As DateTimePicker
    Friend WithEvents chkPersediaan As CheckBox
End Class
