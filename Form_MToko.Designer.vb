<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_MToko
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_MToko))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me._idtoko = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._namatoko = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.tCari = New System.Windows.Forms.TextBox()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TlpCP = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Jabatan = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.Alamat2 = New System.Windows.Forms.TextBox()
        Me.KodePos = New System.Windows.Forms.TextBox()
        Me.Prov = New System.Windows.Forms.TextBox()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.ContactPerson = New System.Windows.Forms.TextBox()
        Me.Phone = New System.Windows.Forms.TextBox()
        Me.Kota = New System.Windows.Forms.TextBox()
        Me.Alamat1 = New System.Windows.Forms.TextBox()
        Me.Nama = New System.Windows.Forms.TextBox()
        Me.IDRec = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(709, 538)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(701, 512)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Daftar Toko"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DGView)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(98, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(600, 506)
        Me.Panel2.TabIndex = 7
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.AllowUserToResizeRows = False
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me._idtoko, Me._namatoko, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 0)
        Me.DGView.Name = "DGView"
        Me.DGView.RowHeadersVisible = False
        Me.DGView.Size = New System.Drawing.Size(600, 506)
        Me.DGView.TabIndex = 1
        '
        '_idtoko
        '
        Me._idtoko.HeaderText = "Id Toko"
        Me._idtoko.Name = "_idtoko"
        Me._idtoko.Width = 70
        '
        '_namatoko
        '
        Me._namatoko.HeaderText = "Nama Toko"
        Me._namatoko.Name = "_namatoko"
        Me._namatoko.Width = 220
        '
        'Column3
        '
        Me.Column3.HeaderText = "Alamat"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 240
        '
        'Column4
        '
        Me.Column4.HeaderText = "Kota"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "PIC"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 80
        '
        'Column6
        '
        Me.Column6.HeaderText = "Tlp"
        Me.Column6.Name = "Column6"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmdExit)
        Me.Panel1.Controls.Add(Me.tCari)
        Me.Panel1.Controls.Add(Me.cmdTambah)
        Me.Panel1.Controls.Add(Me.cmdEdit)
        Me.Panel1.Controls.Add(Me.cmdHapus)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(95, 506)
        Me.Panel1.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Cari"
        '
        'cmdExit
        '
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(6, 169)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 28)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "E&xit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'tCari
        '
        Me.tCari.Location = New System.Drawing.Point(6, 27)
        Me.tCari.Name = "tCari"
        Me.tCari.Size = New System.Drawing.Size(77, 20)
        Me.tCari.TabIndex = 0
        '
        'cmdTambah
        '
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(6, 85)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(77, 28)
        Me.cmdTambah.TabIndex = 2
        Me.cmdTambah.Text = "&Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(6, 113)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(77, 28)
        Me.cmdEdit.TabIndex = 3
        Me.cmdEdit.Text = "&Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(6, 141)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(77, 28)
        Me.cmdHapus.TabIndex = 4
        Me.cmdHapus.Text = "&Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TlpCP)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.Jabatan)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.cmdBatal)
        Me.TabPage2.Controls.Add(Me.cmdSimpan)
        Me.TabPage2.Controls.Add(Me.Alamat2)
        Me.TabPage2.Controls.Add(Me.KodePos)
        Me.TabPage2.Controls.Add(Me.Prov)
        Me.TabPage2.Controls.Add(Me.Email)
        Me.TabPage2.Controls.Add(Me.ContactPerson)
        Me.TabPage2.Controls.Add(Me.Phone)
        Me.TabPage2.Controls.Add(Me.Kota)
        Me.TabPage2.Controls.Add(Me.Alamat1)
        Me.TabPage2.Controls.Add(Me.Nama)
        Me.TabPage2.Controls.Add(Me.IDRec)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(701, 512)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Data Toko"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TlpCP
        '
        Me.TlpCP.Location = New System.Drawing.Point(512, 203)
        Me.TlpCP.MaxLength = 100
        Me.TlpCP.Name = "TlpCP"
        Me.TlpCP.Size = New System.Drawing.Size(152, 20)
        Me.TlpCP.TabIndex = 39
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(461, 206)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "HP/Tlp."
        '
        'Jabatan
        '
        Me.Jabatan.Location = New System.Drawing.Point(213, 229)
        Me.Jabatan.MaxLength = 100
        Me.Jabatan.Name = "Jabatan"
        Me.Jabatan.Size = New System.Drawing.Size(231, 20)
        Me.Jabatan.TabIndex = 32
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(147, 232)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(48, 13)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Jabatan "
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(104, 430)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(77, 28)
        Me.cmdBatal.TabIndex = 22
        Me.cmdBatal.Text = "&Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(27, 430)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(77, 28)
        Me.cmdSimpan.TabIndex = 21
        Me.cmdSimpan.Text = "&Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'Alamat2
        '
        Me.Alamat2.Location = New System.Drawing.Point(149, 97)
        Me.Alamat2.MaxLength = 255
        Me.Alamat2.Name = "Alamat2"
        Me.Alamat2.Size = New System.Drawing.Size(515, 20)
        Me.Alamat2.TabIndex = 9
        '
        'KodePos
        '
        Me.KodePos.Location = New System.Drawing.Point(556, 123)
        Me.KodePos.MaxLength = 50
        Me.KodePos.Name = "KodePos"
        Me.KodePos.Size = New System.Drawing.Size(108, 20)
        Me.KodePos.TabIndex = 12
        '
        'Prov
        '
        Me.Prov.Location = New System.Drawing.Point(386, 123)
        Me.Prov.MaxLength = 100
        Me.Prov.Name = "Prov"
        Me.Prov.Size = New System.Drawing.Size(164, 20)
        Me.Prov.TabIndex = 11
        '
        'Email
        '
        Me.Email.Location = New System.Drawing.Point(149, 177)
        Me.Email.MaxLength = 100
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(515, 20)
        Me.Email.TabIndex = 15
        '
        'ContactPerson
        '
        Me.ContactPerson.Location = New System.Drawing.Point(150, 203)
        Me.ContactPerson.MaxLength = 100
        Me.ContactPerson.Name = "ContactPerson"
        Me.ContactPerson.Size = New System.Drawing.Size(295, 20)
        Me.ContactPerson.TabIndex = 14
        '
        'Phone
        '
        Me.Phone.Location = New System.Drawing.Point(149, 149)
        Me.Phone.MaxLength = 100
        Me.Phone.Name = "Phone"
        Me.Phone.Size = New System.Drawing.Size(515, 20)
        Me.Phone.TabIndex = 13
        '
        'Kota
        '
        Me.Kota.Location = New System.Drawing.Point(149, 123)
        Me.Kota.MaxLength = 100
        Me.Kota.Name = "Kota"
        Me.Kota.Size = New System.Drawing.Size(231, 20)
        Me.Kota.TabIndex = 10
        '
        'Alamat1
        '
        Me.Alamat1.Location = New System.Drawing.Point(149, 71)
        Me.Alamat1.MaxLength = 255
        Me.Alamat1.Name = "Alamat1"
        Me.Alamat1.Size = New System.Drawing.Size(515, 20)
        Me.Alamat1.TabIndex = 8
        '
        'Nama
        '
        Me.Nama.Location = New System.Drawing.Point(149, 45)
        Me.Nama.MaxLength = 100
        Me.Nama.Name = "Nama"
        Me.Nama.Size = New System.Drawing.Size(515, 20)
        Me.Nama.TabIndex = 7
        '
        'IDRec
        '
        Me.IDRec.BackColor = System.Drawing.SystemColors.Info
        Me.IDRec.Location = New System.Drawing.Point(149, 19)
        Me.IDRec.MaxLength = 5
        Me.IDRec.Name = "IDRec"
        Me.IDRec.Size = New System.Drawing.Size(94, 20)
        Me.IDRec.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(24, 180)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Email"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(24, 206)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Contact Person"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Phone"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Kota/Prov/Zip.Code"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Alamat"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Nama Toko"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Kode Toko"
        '
        'Form_MToko
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(709, 538)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form_MToko"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Toko"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DGView As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdExit As Button
    Friend WithEvents tCari As TextBox
    Friend WithEvents cmdTambah As Button
    Friend WithEvents cmdEdit As Button
    Friend WithEvents cmdHapus As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TlpCP As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Jabatan As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents cmdBatal As Button
    Friend WithEvents cmdSimpan As Button
    Friend WithEvents Alamat2 As TextBox
    Friend WithEvents KodePos As TextBox
    Friend WithEvents Prov As TextBox
    Friend WithEvents Email As TextBox
    Friend WithEvents ContactPerson As TextBox
    Friend WithEvents Phone As TextBox
    Friend WithEvents Kota As TextBox
    Friend WithEvents Alamat1 As TextBox
    Friend WithEvents Nama As TextBox
    Friend WithEvents IDRec As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents _idtoko As DataGridViewTextBoxColumn
    Friend WithEvents _namatoko As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
End Class
