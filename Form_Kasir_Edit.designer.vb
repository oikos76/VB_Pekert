<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Kasir_Edit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Kasir_Edit))
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.satuan = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SubTotal = New System.Windows.Forms.TextBox()
        Me.Pack = New System.Windows.Forms.TextBox()
        Me.KodeBrg = New System.Windows.Forms.TextBox()
        Me.Disc = New System.Windows.Forms.TextBox()
        Me.NamaBrg = New System.Windows.Forms.Label()
        Me.PsDisc1 = New System.Windows.Forms.TextBox()
        Me.QTY = New System.Windows.Forms.TextBox()
        Me.HargaSatuan = New System.Windows.Forms.TextBox()
        Me.RowIndex = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbSatuanB = New System.Windows.Forms.ComboBox()
        Me.HargaSatuan_Asli = New System.Windows.Forms.TextBox()
        Me.HargaModal = New System.Windows.Forms.TextBox()
        Me.IsiSatB = New System.Windows.Forms.TextBox()
        Me.IsiSatT = New System.Windows.Forms.TextBox()
        Me.harga3 = New System.Windows.Forms.TextBox()
        Me.harga2 = New System.Windows.Forms.TextBox()
        Me.harga1 = New System.Windows.Forms.TextBox()
        Me.Flag = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(12, 290)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 28)
        Me.cmdBatal.TabIndex = 6
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(248, 277)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(95, 41)
        Me.cmdSimpan.TabIndex = 7
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'satuan
        '
        Me.satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.satuan.Location = New System.Drawing.Point(199, 66)
        Me.satuan.Name = "satuan"
        Me.satuan.Size = New System.Drawing.Size(77, 20)
        Me.satuan.TabIndex = 106
        Me.satuan.Text = "satuan"
        Me.satuan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(160, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 20)
        Me.Label6.TabIndex = 104
        Me.Label6.Text = "%"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(106, 205)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 20)
        Me.Label5.TabIndex = 103
        Me.Label5.Text = "Rp."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SubTotal
        '
        Me.SubTotal.BackColor = System.Drawing.SystemColors.Control
        Me.SubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubTotal.Location = New System.Drawing.Point(110, 234)
        Me.SubTotal.MaxLength = 100
        Me.SubTotal.Name = "SubTotal"
        Me.SubTotal.ReadOnly = True
        Me.SubTotal.Size = New System.Drawing.Size(142, 26)
        Me.SubTotal.TabIndex = 101
        Me.SubTotal.Text = "0"
        Me.SubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Pack
        '
        Me.Pack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pack.Location = New System.Drawing.Point(110, 96)
        Me.Pack.MaxLength = 100
        Me.Pack.Name = "Pack"
        Me.Pack.Size = New System.Drawing.Size(83, 26)
        Me.Pack.TabIndex = 100
        Me.Pack.Text = "99"
        '
        'KodeBrg
        '
        Me.KodeBrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KodeBrg.Location = New System.Drawing.Point(110, 12)
        Me.KodeBrg.MaxLength = 100
        Me.KodeBrg.Name = "KodeBrg"
        Me.KodeBrg.ReadOnly = True
        Me.KodeBrg.Size = New System.Drawing.Size(142, 26)
        Me.KodeBrg.TabIndex = 95
        Me.KodeBrg.Text = "8992775000526"
        '
        'Disc
        '
        Me.Disc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Disc.Location = New System.Drawing.Point(141, 202)
        Me.Disc.MaxLength = 100
        Me.Disc.Name = "Disc"
        Me.Disc.Size = New System.Drawing.Size(111, 26)
        Me.Disc.TabIndex = 99
        Me.Disc.Text = "99"
        '
        'NamaBrg
        '
        Me.NamaBrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NamaBrg.Location = New System.Drawing.Point(106, 41)
        Me.NamaBrg.Name = "NamaBrg"
        Me.NamaBrg.Size = New System.Drawing.Size(229, 20)
        Me.NamaBrg.TabIndex = 94
        Me.NamaBrg.Text = "Nama Barang"
        Me.NamaBrg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PsDisc1
        '
        Me.PsDisc1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PsDisc1.Location = New System.Drawing.Point(110, 170)
        Me.PsDisc1.MaxLength = 100
        Me.PsDisc1.Name = "PsDisc1"
        Me.PsDisc1.Size = New System.Drawing.Size(44, 26)
        Me.PsDisc1.TabIndex = 98
        Me.PsDisc1.Text = "99"
        '
        'QTY
        '
        Me.QTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QTY.Location = New System.Drawing.Point(110, 64)
        Me.QTY.MaxLength = 100
        Me.QTY.Name = "QTY"
        Me.QTY.Size = New System.Drawing.Size(83, 26)
        Me.QTY.TabIndex = 96
        Me.QTY.Text = "1"
        '
        'HargaSatuan
        '
        Me.HargaSatuan.BackColor = System.Drawing.SystemColors.Window
        Me.HargaSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HargaSatuan.Location = New System.Drawing.Point(110, 138)
        Me.HargaSatuan.MaxLength = 100
        Me.HargaSatuan.Name = "HargaSatuan"
        Me.HargaSatuan.ReadOnly = True
        Me.HargaSatuan.Size = New System.Drawing.Size(142, 26)
        Me.HargaSatuan.TabIndex = 97
        Me.HargaSatuan.Text = "1"
        Me.HargaSatuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RowIndex
        '
        Me.RowIndex.AutoSize = True
        Me.RowIndex.Location = New System.Drawing.Point(107, 271)
        Me.RowIndex.Name = "RowIndex"
        Me.RowIndex.Size = New System.Drawing.Size(55, 13)
        Me.RowIndex.TabIndex = 108
        Me.RowIndex.Text = "RowIndex"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 20)
        Me.Label1.TabIndex = 109
        Me.Label1.Text = "Kode Barang"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 20)
        Me.Label2.TabIndex = 110
        Me.Label2.Text = "QTY"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 20)
        Me.Label3.TabIndex = 111
        Me.Label3.Text = "Harga"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 173)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 20)
        Me.Label7.TabIndex = 112
        Me.Label7.Text = "Disc 1"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 206)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 20)
        Me.Label8.TabIndex = 113
        Me.Label8.Text = "Discount "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 20)
        Me.Label9.TabIndex = 114
        Me.Label9.Text = "Pack"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 238)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(92, 20)
        Me.Label11.TabIndex = 116
        Me.Label11.Text = "Sub Total"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSatuanB
        '
        Me.cmbSatuanB.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSatuanB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSatuanB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSatuanB.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSatuanB.FormattingEnabled = True
        Me.cmbSatuanB.Location = New System.Drawing.Point(203, 96)
        Me.cmbSatuanB.Name = "cmbSatuanB"
        Me.cmbSatuanB.Size = New System.Drawing.Size(103, 26)
        Me.cmbSatuanB.TabIndex = 117
        '
        'HargaSatuan_Asli
        '
        Me.HargaSatuan_Asli.BackColor = System.Drawing.SystemColors.Control
        Me.HargaSatuan_Asli.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HargaSatuan_Asli.Location = New System.Drawing.Point(81, 366)
        Me.HargaSatuan_Asli.MaxLength = 100
        Me.HargaSatuan_Asli.Name = "HargaSatuan_Asli"
        Me.HargaSatuan_Asli.ReadOnly = True
        Me.HargaSatuan_Asli.Size = New System.Drawing.Size(87, 20)
        Me.HargaSatuan_Asli.TabIndex = 118
        Me.HargaSatuan_Asli.Text = "1"
        '
        'HargaModal
        '
        Me.HargaModal.BackColor = System.Drawing.SystemColors.Control
        Me.HargaModal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HargaModal.Location = New System.Drawing.Point(174, 366)
        Me.HargaModal.MaxLength = 100
        Me.HargaModal.Name = "HargaModal"
        Me.HargaModal.ReadOnly = True
        Me.HargaModal.Size = New System.Drawing.Size(87, 20)
        Me.HargaModal.TabIndex = 119
        Me.HargaModal.Text = "1"
        '
        'IsiSatB
        '
        Me.IsiSatB.Location = New System.Drawing.Point(273, 340)
        Me.IsiSatB.Name = "IsiSatB"
        Me.IsiSatB.Size = New System.Drawing.Size(55, 20)
        Me.IsiSatB.TabIndex = 124
        '
        'IsiSatT
        '
        Me.IsiSatT.Location = New System.Drawing.Point(214, 340)
        Me.IsiSatT.Name = "IsiSatT"
        Me.IsiSatT.Size = New System.Drawing.Size(55, 20)
        Me.IsiSatT.TabIndex = 123
        '
        'harga3
        '
        Me.harga3.Location = New System.Drawing.Point(141, 340)
        Me.harga3.Name = "harga3"
        Me.harga3.Size = New System.Drawing.Size(55, 20)
        Me.harga3.TabIndex = 122
        '
        'harga2
        '
        Me.harga2.Location = New System.Drawing.Point(81, 340)
        Me.harga2.Name = "harga2"
        Me.harga2.Size = New System.Drawing.Size(55, 20)
        Me.harga2.TabIndex = 121
        '
        'harga1
        '
        Me.harga1.Location = New System.Drawing.Point(22, 340)
        Me.harga1.Name = "harga1"
        Me.harga1.Size = New System.Drawing.Size(55, 20)
        Me.harga1.TabIndex = 120
        '
        'Flag
        '
        Me.Flag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Flag.Location = New System.Drawing.Point(271, 366)
        Me.Flag.MaxLength = 100
        Me.Flag.Name = "Flag"
        Me.Flag.Size = New System.Drawing.Size(35, 20)
        Me.Flag.TabIndex = 125
        '
        'Form_Kasir_Edit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 333)
        Me.ControlBox = False
        Me.Controls.Add(Me.Flag)
        Me.Controls.Add(Me.IsiSatB)
        Me.Controls.Add(Me.IsiSatT)
        Me.Controls.Add(Me.harga3)
        Me.Controls.Add(Me.harga2)
        Me.Controls.Add(Me.harga1)
        Me.Controls.Add(Me.HargaModal)
        Me.Controls.Add(Me.HargaSatuan_Asli)
        Me.Controls.Add(Me.cmbSatuanB)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RowIndex)
        Me.Controls.Add(Me.satuan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.SubTotal)
        Me.Controls.Add(Me.Pack)
        Me.Controls.Add(Me.KodeBrg)
        Me.Controls.Add(Me.Disc)
        Me.Controls.Add(Me.NamaBrg)
        Me.Controls.Add(Me.PsDisc1)
        Me.Controls.Add(Me.QTY)
        Me.Controls.Add(Me.HargaSatuan)
        Me.Controls.Add(Me.cmdSimpan)
        Me.Controls.Add(Me.cmdBatal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Kasir_Edit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents satuan As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents SubTotal As TextBox
    Friend WithEvents Pack As TextBox
    Friend WithEvents KodeBrg As TextBox
    Friend WithEvents Disc As TextBox
    Friend WithEvents NamaBrg As Label
    Friend WithEvents PsDisc1 As TextBox
    Friend WithEvents QTY As TextBox
    Friend WithEvents HargaSatuan As TextBox
    Friend WithEvents RowIndex As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents cmbSatuanB As ComboBox
    Friend WithEvents HargaSatuan_Asli As TextBox
    Friend WithEvents HargaModal As TextBox
    Friend WithEvents IsiSatB As TextBox
    Friend WithEvents IsiSatT As TextBox
    Friend WithEvents harga3 As TextBox
    Friend WithEvents harga2 As TextBox
    Friend WithEvents harga1 As TextBox
    Friend WithEvents Flag As TextBox
End Class
