<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_GiroCair
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_GiroCair))
        Me.idRec = New System.Windows.Forms.TextBox()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.TglCair = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Bank = New System.Windows.Forms.TextBox()
        Me.NoGiro = New System.Windows.Forms.TextBox()
        Me.TglJatuhTempo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PanelReject = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Keterangan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PanelAccept = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.PanelReject.SuspendLayout()
        Me.PanelAccept.SuspendLayout()
        Me.SuspendLayout()
        '
        'idRec
        '
        Me.idRec.Location = New System.Drawing.Point(144, 5)
        Me.idRec.Name = "idRec"
        Me.idRec.ReadOnly = True
        Me.idRec.Size = New System.Drawing.Size(120, 20)
        Me.idRec.TabIndex = 0
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(196, 3)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(98, 32)
        Me.cmdSimpan.TabIndex = 139
        Me.cmdSimpan.Text = "&Accept"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(3, 3)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 28)
        Me.cmdBatal.TabIndex = 138
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'TglCair
        '
        Me.TglCair.CustomFormat = "dd-MM-yyyy"
        Me.TglCair.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TglCair.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TglCair.Location = New System.Drawing.Point(144, 5)
        Me.TglCair.Name = "TglCair"
        Me.TglCair.Size = New System.Drawing.Size(122, 22)
        Me.TglCair.TabIndex = 140
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 16)
        Me.Label6.TabIndex = 141
        Me.Label6.Text = "Tgl. di terima"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 142
        Me.Label1.Text = "Id Bayar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 16)
        Me.Label2.TabIndex = 143
        Me.Label2.Text = "Bank"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 16)
        Me.Label3.TabIndex = 144
        Me.Label3.Text = "No Giro"
        '
        'Bank
        '
        Me.Bank.Location = New System.Drawing.Point(144, 31)
        Me.Bank.Name = "Bank"
        Me.Bank.ReadOnly = True
        Me.Bank.Size = New System.Drawing.Size(120, 20)
        Me.Bank.TabIndex = 145
        '
        'NoGiro
        '
        Me.NoGiro.Location = New System.Drawing.Point(144, 57)
        Me.NoGiro.Name = "NoGiro"
        Me.NoGiro.ReadOnly = True
        Me.NoGiro.Size = New System.Drawing.Size(120, 20)
        Me.NoGiro.TabIndex = 146
        '
        'TglJatuhTempo
        '
        Me.TglJatuhTempo.Location = New System.Drawing.Point(144, 83)
        Me.TglJatuhTempo.Name = "TglJatuhTempo"
        Me.TglJatuhTempo.ReadOnly = True
        Me.TglJatuhTempo.Size = New System.Drawing.Size(120, 20)
        Me.TglJatuhTempo.TabIndex = 147
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 16)
        Me.Label4.TabIndex = 148
        Me.Label4.Text = "Tgl Jatuh Tempo"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdSimpan)
        Me.Panel1.Controls.Add(Me.cmdBatal)
        Me.Panel1.Location = New System.Drawing.Point(12, 205)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 38)
        Me.Panel1.TabIndex = 149
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.idRec)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.TglJatuhTempo)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.NoGiro)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Bank)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(12, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(295, 109)
        Me.Panel2.TabIndex = 150
        '
        'PanelReject
        '
        Me.PanelReject.Controls.Add(Me.Label7)
        Me.PanelReject.Controls.Add(Me.DateTimePicker1)
        Me.PanelReject.Controls.Add(Me.Keterangan)
        Me.PanelReject.Controls.Add(Me.Label5)
        Me.PanelReject.Location = New System.Drawing.Point(12, 114)
        Me.PanelReject.Name = "PanelReject"
        Me.PanelReject.Size = New System.Drawing.Size(295, 90)
        Me.PanelReject.TabIndex = 151
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 16)
        Me.Label7.TabIndex = 145
        Me.Label7.Text = "Tgl. di tolak"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd-MM-yyyy"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(144, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(122, 22)
        Me.DateTimePicker1.TabIndex = 144
        '
        'Keterangan
        '
        Me.Keterangan.Location = New System.Drawing.Point(24, 50)
        Me.Keterangan.Name = "Keterangan"
        Me.Keterangan.Size = New System.Drawing.Size(242, 20)
        Me.Keterangan.TabIndex = 143
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 142
        Me.Label5.Text = "Alasan di tolak"
        '
        'PanelAccept
        '
        Me.PanelAccept.Controls.Add(Me.TglCair)
        Me.PanelAccept.Controls.Add(Me.Label6)
        Me.PanelAccept.Location = New System.Drawing.Point(12, 114)
        Me.PanelAccept.Name = "PanelAccept"
        Me.PanelAccept.Size = New System.Drawing.Size(295, 87)
        Me.PanelAccept.TabIndex = 152
        '
        'Form_GiroCair
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(315, 247)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelReject)
        Me.Controls.Add(Me.PanelAccept)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_GiroCair"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pencairan Giro "
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.PanelReject.ResumeLayout(False)
        Me.PanelReject.PerformLayout()
        Me.PanelAccept.ResumeLayout(False)
        Me.PanelAccept.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents idRec As TextBox
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents cmdBatal As Button
    Friend WithEvents TglCair As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Bank As TextBox
    Friend WithEvents NoGiro As TextBox
    Friend WithEvents TglJatuhTempo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PanelReject As Panel
    Friend WithEvents PanelAccept As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Keterangan As TextBox
    Friend WithEvents Label5 As Label
End Class
