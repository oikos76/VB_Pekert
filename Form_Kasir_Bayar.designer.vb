<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Kasir_Bayar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Kasir_Bayar))
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.SubTotal = New System.Windows.Forms.TextBox()
        Me.Discount = New System.Windows.Forms.TextBox()
        Me.PsDisc = New System.Windows.Forms.TextBox()
        Me.Pembulatan = New System.Windows.Forms.TextBox()
        Me.TotalSales = New System.Windows.Forms.TextBox()
        Me.Kredit = New System.Windows.Forms.TextBox()
        Me.Debet = New System.Windows.Forms.TextBox()
        Me.Tunai = New System.Windows.Forms.TextBox()
        Me.Kembali = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.NoKartuDebet = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.NoKartuKredit = New System.Windows.Forms.TextBox()
        Me.PsCharge = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Charge = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(29, 405)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 28)
        Me.cmdBatal.TabIndex = 12
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(298, 392)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(95, 41)
        Me.cmdSimpan.TabIndex = 13
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'SubTotal
        '
        Me.SubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.SubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubTotal.Location = New System.Drawing.Point(221, 49)
        Me.SubTotal.Name = "SubTotal"
        Me.SubTotal.ReadOnly = True
        Me.SubTotal.Size = New System.Drawing.Size(173, 26)
        Me.SubTotal.TabIndex = 0
        Me.SubTotal.Text = "0"
        Me.SubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Discount
        '
        Me.Discount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Discount.Location = New System.Drawing.Point(221, 81)
        Me.Discount.Name = "Discount"
        Me.Discount.Size = New System.Drawing.Size(173, 26)
        Me.Discount.TabIndex = 2
        Me.Discount.Text = "0"
        Me.Discount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PsDisc
        '
        Me.PsDisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PsDisc.Location = New System.Drawing.Point(171, 81)
        Me.PsDisc.Name = "PsDisc"
        Me.PsDisc.Size = New System.Drawing.Size(44, 26)
        Me.PsDisc.TabIndex = 1
        Me.PsDisc.Text = "0"
        Me.PsDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Pembulatan
        '
        Me.Pembulatan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pembulatan.Location = New System.Drawing.Point(337, 128)
        Me.Pembulatan.Name = "Pembulatan"
        Me.Pembulatan.ReadOnly = True
        Me.Pembulatan.Size = New System.Drawing.Size(56, 20)
        Me.Pembulatan.TabIndex = 9
        Me.Pembulatan.Text = "0"
        Me.Pembulatan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Pembulatan.Visible = False
        '
        'TotalSales
        '
        Me.TotalSales.BackColor = System.Drawing.Color.DimGray
        Me.TotalSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalSales.ForeColor = System.Drawing.Color.Gold
        Me.TotalSales.Location = New System.Drawing.Point(171, 117)
        Me.TotalSales.Name = "TotalSales"
        Me.TotalSales.ReadOnly = True
        Me.TotalSales.Size = New System.Drawing.Size(223, 38)
        Me.TotalSales.TabIndex = 3
        Me.TotalSales.Text = "Total Sales"
        Me.TotalSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Kredit
        '
        Me.Kredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kredit.Location = New System.Drawing.Point(220, 257)
        Me.Kredit.Name = "Kredit"
        Me.Kredit.Size = New System.Drawing.Size(173, 26)
        Me.Kredit.TabIndex = 7
        Me.Kredit.Text = "100,000,000"
        '
        'Debet
        '
        Me.Debet.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Debet.Location = New System.Drawing.Point(220, 193)
        Me.Debet.Name = "Debet"
        Me.Debet.Size = New System.Drawing.Size(173, 26)
        Me.Debet.TabIndex = 5
        Me.Debet.Text = "0"
        '
        'Tunai
        '
        Me.Tunai.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tunai.Location = New System.Drawing.Point(220, 161)
        Me.Tunai.Name = "Tunai"
        Me.Tunai.Size = New System.Drawing.Size(173, 26)
        Me.Tunai.TabIndex = 4
        Me.Tunai.Text = "0"
        '
        'Kembali
        '
        Me.Kembali.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kembali.Location = New System.Drawing.Point(171, 348)
        Me.Kembali.Name = "Kembali"
        Me.Kembali.ReadOnly = True
        Me.Kembali.Size = New System.Drawing.Size(223, 29)
        Me.Kembali.TabIndex = 11
        Me.Kembali.Text = "0"
        Me.Kembali.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(26, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 18)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Sub Total"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 18)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "%  |  Discount"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(179, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Pembulatan"
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 31)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Total"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 18)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Tunai"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(25, 197)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 18)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Debet"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(26, 352)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 18)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "Kembali"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(25, 261)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 18)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Kredit"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(151, 53)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(12, 18)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(151, 85)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 18)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = ":"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(150, 261)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(12, 18)
        Me.Label12.TabIndex = 47
        Me.Label12.Text = ":"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(150, 197)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(12, 18)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = ":"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(150, 165)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(12, 18)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = ":"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(151, 352)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(12, 18)
        Me.Label15.TabIndex = 48
        Me.Label15.Text = ":"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(146, 121)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(23, 31)
        Me.Label16.TabIndex = 49
        Me.Label16.Text = ":"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.DimGray
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Gold
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(423, 32)
        Me.Label17.TabIndex = 50
        Me.Label17.Text = "= PEMBAYARAN="
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NoKartuDebet
        '
        Me.NoKartuDebet.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoKartuDebet.Location = New System.Drawing.Point(220, 225)
        Me.NoKartuDebet.Name = "NoKartuDebet"
        Me.NoKartuDebet.Size = New System.Drawing.Size(173, 26)
        Me.NoKartuDebet.TabIndex = 6
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(26, 229)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(110, 18)
        Me.Label18.TabIndex = 52
        Me.Label18.Text = "No Kartu Debet"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(150, 229)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(12, 18)
        Me.Label19.TabIndex = 53
        Me.Label19.Text = ":"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(151, 320)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(12, 18)
        Me.Label20.TabIndex = 56
        Me.Label20.Text = ":"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(26, 320)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(109, 18)
        Me.Label21.TabIndex = 55
        Me.Label21.Text = "No Kartu Kredit"
        '
        'NoKartuKredit
        '
        Me.NoKartuKredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoKartuKredit.Location = New System.Drawing.Point(220, 316)
        Me.NoKartuKredit.Name = "NoKartuKredit"
        Me.NoKartuKredit.Size = New System.Drawing.Size(172, 26)
        Me.NoKartuKredit.TabIndex = 10
        '
        'PsCharge
        '
        Me.PsCharge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PsCharge.Location = New System.Drawing.Point(220, 286)
        Me.PsCharge.Name = "PsCharge"
        Me.PsCharge.Size = New System.Drawing.Size(44, 26)
        Me.PsCharge.TabIndex = 8
        Me.PsCharge.Text = "0"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(270, 290)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(21, 18)
        Me.Label22.TabIndex = 58
        Me.Label22.Text = "%"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(150, 290)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(12, 18)
        Me.Label23.TabIndex = 59
        Me.Label23.Text = ":"
        '
        'Charge
        '
        Me.Charge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Charge.Location = New System.Drawing.Point(297, 286)
        Me.Charge.Name = "Charge"
        Me.Charge.Size = New System.Drawing.Size(96, 26)
        Me.Charge.TabIndex = 9
        Me.Charge.Text = "100,000,000"
        Me.Charge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(25, 290)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(56, 18)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Charge"
        '
        'Form_Kasir_Bayar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 446)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Charge)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.PsCharge)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.NoKartuKredit)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.NoKartuDebet)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Kembali)
        Me.Controls.Add(Me.Kredit)
        Me.Controls.Add(Me.Debet)
        Me.Controls.Add(Me.Tunai)
        Me.Controls.Add(Me.TotalSales)
        Me.Controls.Add(Me.Pembulatan)
        Me.Controls.Add(Me.PsDisc)
        Me.Controls.Add(Me.Discount)
        Me.Controls.Add(Me.SubTotal)
        Me.Controls.Add(Me.cmdBatal)
        Me.Controls.Add(Me.cmdSimpan)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "Form_Kasir_Bayar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents SubTotal As TextBox
    Friend WithEvents Discount As TextBox
    Friend WithEvents PsDisc As TextBox
    Friend WithEvents Pembulatan As TextBox
    Friend WithEvents TotalSales As TextBox
    Friend WithEvents Kredit As TextBox
    Friend WithEvents Debet As TextBox
    Friend WithEvents Tunai As TextBox
    Friend WithEvents Kembali As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents NoKartuDebet As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents NoKartuKredit As TextBox
    Friend WithEvents PsCharge As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Charge As TextBox
    Friend WithEvents Label24 As Label
End Class
