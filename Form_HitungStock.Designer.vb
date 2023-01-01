<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_HitungStock
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_HitungStock))
        Me.StatusHitung = New System.Windows.Forms.Label()
        Me.cmd_Hitung_Ulang = New System.Windows.Forms.Button()
        Me.NamaToko = New System.Windows.Forms.Label()
        Me.idToko = New System.Windows.Forms.TextBox()
        Me.LabelToko = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPeriode = New System.Windows.Forms.DateTimePicker()
        Me.Keterangan = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'StatusHitung
        '
        Me.StatusHitung.AutoSize = True
        Me.StatusHitung.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusHitung.Location = New System.Drawing.Point(12, 143)
        Me.StatusHitung.Name = "StatusHitung"
        Me.StatusHitung.Size = New System.Drawing.Size(123, 16)
        Me.StatusHitung.TabIndex = 169
        Me.StatusHitung.Text = "Status Hitung......"
        '
        'cmd_Hitung_Ulang
        '
        Me.cmd_Hitung_Ulang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Hitung_Ulang.Image = CType(resources.GetObject("cmd_Hitung_Ulang.Image"), System.Drawing.Image)
        Me.cmd_Hitung_Ulang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Hitung_Ulang.Location = New System.Drawing.Point(102, 82)
        Me.cmd_Hitung_Ulang.Name = "cmd_Hitung_Ulang"
        Me.cmd_Hitung_Ulang.Size = New System.Drawing.Size(143, 40)
        Me.cmd_Hitung_Ulang.TabIndex = 168
        Me.cmd_Hitung_Ulang.Text = "&Hitung Ulang"
        Me.cmd_Hitung_Ulang.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmd_Hitung_Ulang.UseVisualStyleBackColor = True
        '
        'NamaToko
        '
        Me.NamaToko.AutoSize = True
        Me.NamaToko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaToko.Location = New System.Drawing.Point(120, 193)
        Me.NamaToko.Name = "NamaToko"
        Me.NamaToko.Size = New System.Drawing.Size(80, 16)
        Me.NamaToko.TabIndex = 165
        Me.NamaToko.Text = "Nama Toko"
        '
        'idToko
        '
        Me.idToko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.idToko.Location = New System.Drawing.Point(307, 2)
        Me.idToko.MaxLength = 5
        Me.idToko.Name = "idToko"
        Me.idToko.Size = New System.Drawing.Size(52, 22)
        Me.idToko.TabIndex = 167
        Me.idToko.Text = "<Kode_Toko>"
        Me.idToko.Visible = False
        '
        'LabelToko
        '
        Me.LabelToko.AutoSize = True
        Me.LabelToko.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelToko.Location = New System.Drawing.Point(10, 193)
        Me.LabelToko.Name = "LabelToko"
        Me.LabelToko.Size = New System.Drawing.Size(46, 16)
        Me.LabelToko.TabIndex = 166
        Me.LabelToko.Text = "Toko :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 20)
        Me.Label1.TabIndex = 170
        Me.Label1.Text = "Periode :"
        '
        'cmbPeriode
        '
        Me.cmbPeriode.CustomFormat = "MM-yyyy"
        Me.cmbPeriode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPeriode.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cmbPeriode.Location = New System.Drawing.Point(102, 12)
        Me.cmbPeriode.Name = "cmbPeriode"
        Me.cmbPeriode.Size = New System.Drawing.Size(101, 26)
        Me.cmbPeriode.TabIndex = 171
        '
        'Keterangan
        '
        Me.Keterangan.AutoSize = True
        Me.Keterangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Keterangan.Location = New System.Drawing.Point(17, 53)
        Me.Keterangan.Name = "Keterangan"
        Me.Keterangan.Size = New System.Drawing.Size(87, 16)
        Me.Keterangan.TabIndex = 172
        Me.Keterangan.Text = "Keterangan"
        '
        'Form_HitungStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 180)
        Me.Controls.Add(Me.Keterangan)
        Me.Controls.Add(Me.cmbPeriode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusHitung)
        Me.Controls.Add(Me.cmd_Hitung_Ulang)
        Me.Controls.Add(Me.NamaToko)
        Me.Controls.Add(Me.idToko)
        Me.Controls.Add(Me.LabelToko)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_HitungStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hitung Stock"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusHitung As Label
    Friend WithEvents cmd_Hitung_Ulang As Button
    Friend WithEvents NamaToko As Label
    Friend WithEvents idToko As TextBox
    Friend WithEvents LabelToko As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbPeriode As DateTimePicker
    Friend WithEvents Keterangan As Label
End Class
