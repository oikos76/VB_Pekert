<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_User
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_User))
        Me.DGUser = New System.Windows.Forms.DataGridView()
        Me.IDUser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.User_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastLogin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnResetPassword = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.cmdSimpan = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdBatal = New System.Windows.Forms.Button()
        Me.cmdHapus = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdTambah = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelTombol = New System.Windows.Forms.Panel()
        Me.txtCari = New System.Windows.Forms.TextBox()
        Me.Panel_Browse = New System.Windows.Forms.Panel()
        Me.Panel_Tambah = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NamaToko = New System.Windows.Forms.TextBox()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.kode_toko = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.UserName = New System.Windows.Forms.TextBox()
        Me.UserID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DGUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTombol.SuspendLayout()
        Me.Panel_Browse.SuspendLayout()
        Me.Panel_Tambah.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGUser
        '
        Me.DGUser.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.DGUser.AllowUserToAddRows = False
        Me.DGUser.AllowUserToDeleteRows = False
        Me.DGUser.AllowUserToOrderColumns = True
        Me.DGUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGUser.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDUser, Me.User_Name, Me.LastLogin, Me.btnResetPassword})
        Me.DGUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGUser.Location = New System.Drawing.Point(0, 0)
        Me.DGUser.MultiSelect = False
        Me.DGUser.Name = "DGUser"
        Me.DGUser.ReadOnly = True
        Me.DGUser.RowHeadersVisible = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGUser.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGUser.Size = New System.Drawing.Size(639, 581)
        Me.DGUser.TabIndex = 11
        '
        'IDUser
        '
        Me.IDUser.HeaderText = "User ID"
        Me.IDUser.Name = "IDUser"
        Me.IDUser.ReadOnly = True
        Me.IDUser.Width = 150
        '
        'User_Name
        '
        Me.User_Name.HeaderText = "Nama User"
        Me.User_Name.Name = "User_Name"
        Me.User_Name.ReadOnly = True
        Me.User_Name.Width = 150
        '
        'LastLogin
        '
        Me.LastLogin.HeaderText = "Login Terkahir"
        Me.LastLogin.Name = "LastLogin"
        Me.LastLogin.ReadOnly = True
        Me.LastLogin.Width = 150
        '
        'btnResetPassword
        '
        Me.btnResetPassword.HeaderText = "Reset Password"
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.ReadOnly = True
        Me.btnResetPassword.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.btnResetPassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.btnResetPassword.Width = 150
        '
        'cmdSimpan
        '
        Me.cmdSimpan.Image = CType(resources.GetObject("cmdSimpan.Image"), System.Drawing.Image)
        Me.cmdSimpan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpan.Location = New System.Drawing.Point(119, 179)
        Me.cmdSimpan.Name = "cmdSimpan"
        Me.cmdSimpan.Size = New System.Drawing.Size(85, 36)
        Me.cmdSimpan.TabIndex = 9
        Me.cmdSimpan.Text = "Simpan"
        Me.cmdSimpan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpan.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = CType(resources.GetObject("cmdEdit.Image"), System.Drawing.Image)
        Me.cmdEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEdit.Location = New System.Drawing.Point(3, 104)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(85, 41)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.Text = "Ubah"
        Me.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdBatal
        '
        Me.cmdBatal.Image = CType(resources.GetObject("cmdBatal.Image"), System.Drawing.Image)
        Me.cmdBatal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatal.Location = New System.Drawing.Point(210, 179)
        Me.cmdBatal.Name = "cmdBatal"
        Me.cmdBatal.Size = New System.Drawing.Size(85, 36)
        Me.cmdBatal.TabIndex = 10
        Me.cmdBatal.Text = "Batal"
        Me.cmdBatal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatal.UseVisualStyleBackColor = True
        '
        'cmdHapus
        '
        Me.cmdHapus.Image = CType(resources.GetObject("cmdHapus.Image"), System.Drawing.Image)
        Me.cmdHapus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapus.Location = New System.Drawing.Point(3, 144)
        Me.cmdHapus.Name = "cmdHapus"
        Me.cmdHapus.Size = New System.Drawing.Size(85, 41)
        Me.cmdHapus.TabIndex = 2
        Me.cmdHapus.Text = "Hapus"
        Me.cmdHapus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapus.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(3, 185)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(85, 41)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdTambah
        '
        Me.cmdTambah.Image = CType(resources.GetObject("cmdTambah.Image"), System.Drawing.Image)
        Me.cmdTambah.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambah.Location = New System.Drawing.Point(3, 64)
        Me.cmdTambah.Name = "cmdTambah"
        Me.cmdTambah.Size = New System.Drawing.Size(85, 41)
        Me.cmdTambah.TabIndex = 0
        Me.cmdTambah.Text = "Tambah"
        Me.cmdTambah.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambah.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "User ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "User Name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Login Terkahir"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Password"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'PanelTombol
        '
        Me.PanelTombol.Controls.Add(Me.txtCari)
        Me.PanelTombol.Controls.Add(Me.cmdTambah)
        Me.PanelTombol.Controls.Add(Me.cmdEdit)
        Me.PanelTombol.Controls.Add(Me.cmdExit)
        Me.PanelTombol.Controls.Add(Me.cmdHapus)
        Me.PanelTombol.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelTombol.Location = New System.Drawing.Point(0, 0)
        Me.PanelTombol.Name = "PanelTombol"
        Me.PanelTombol.Size = New System.Drawing.Size(96, 581)
        Me.PanelTombol.TabIndex = 19
        '
        'txtCari
        '
        Me.txtCari.Location = New System.Drawing.Point(12, 16)
        Me.txtCari.Name = "txtCari"
        Me.txtCari.Size = New System.Drawing.Size(75, 20)
        Me.txtCari.TabIndex = 4
        '
        'Panel_Browse
        '
        Me.Panel_Browse.Controls.Add(Me.Panel_Tambah)
        Me.Panel_Browse.Controls.Add(Me.DGUser)
        Me.Panel_Browse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Browse.Location = New System.Drawing.Point(96, 0)
        Me.Panel_Browse.Name = "Panel_Browse"
        Me.Panel_Browse.Size = New System.Drawing.Size(639, 581)
        Me.Panel_Browse.TabIndex = 20
        '
        'Panel_Tambah
        '
        Me.Panel_Tambah.Controls.Add(Me.Label6)
        Me.Panel_Tambah.Controls.Add(Me.cmdSimpan)
        Me.Panel_Tambah.Controls.Add(Me.Label5)
        Me.Panel_Tambah.Controls.Add(Me.Label4)
        Me.Panel_Tambah.Controls.Add(Me.cmdBatal)
        Me.Panel_Tambah.Controls.Add(Me.NamaToko)
        Me.Panel_Tambah.Controls.Add(Me.BtnBrowse)
        Me.Panel_Tambah.Controls.Add(Me.Label12)
        Me.Panel_Tambah.Controls.Add(Me.kode_toko)
        Me.Panel_Tambah.Controls.Add(Me.Label11)
        Me.Panel_Tambah.Controls.Add(Me.Password)
        Me.Panel_Tambah.Controls.Add(Me.UserName)
        Me.Panel_Tambah.Controls.Add(Me.UserID)
        Me.Panel_Tambah.Controls.Add(Me.Label3)
        Me.Panel_Tambah.Controls.Add(Me.Label2)
        Me.Panel_Tambah.Controls.Add(Me.Label1)
        Me.Panel_Tambah.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Tambah.Enabled = False
        Me.Panel_Tambah.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Tambah.Name = "Panel_Tambah"
        Me.Panel_Tambah.Size = New System.Drawing.Size(639, 581)
        Me.Panel_Tambah.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(103, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = ":"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(103, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = ":"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(103, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = ":"
        '
        'NamaToko
        '
        Me.NamaToko.Location = New System.Drawing.Point(209, 126)
        Me.NamaToko.MaxLength = 100
        Me.NamaToko.Name = "NamaToko"
        Me.NamaToko.ReadOnly = True
        Me.NamaToko.Size = New System.Drawing.Size(282, 20)
        Me.NamaToko.TabIndex = 8
        '
        'BtnBrowse
        '
        Me.BtnBrowse.Image = CType(resources.GetObject("BtnBrowse.Image"), System.Drawing.Image)
        Me.BtnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBrowse.Location = New System.Drawing.Point(185, 125)
        Me.BtnBrowse.Name = "BtnBrowse"
        Me.BtnBrowse.Size = New System.Drawing.Size(24, 23)
        Me.BtnBrowse.TabIndex = 7
        Me.BtnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnBrowse.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(103, 127)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 13)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = ":"
        '
        'kode_toko
        '
        Me.kode_toko.Location = New System.Drawing.Point(119, 125)
        Me.kode_toko.MaxLength = 255
        Me.kode_toko.Name = "kode_toko"
        Me.kode_toko.Size = New System.Drawing.Size(65, 20)
        Me.kode_toko.TabIndex = 115
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(32, 128)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(29, 13)
        Me.Label11.TabIndex = 114
        Me.Label11.Text = "Area"
        '
        'Password
        '
        Me.Password.Location = New System.Drawing.Point(119, 97)
        Me.Password.MaxLength = 20
        Me.Password.Name = "Password"
        Me.Password.Size = New System.Drawing.Size(134, 20)
        Me.Password.TabIndex = 1
        '
        'UserName
        '
        Me.UserName.Location = New System.Drawing.Point(119, 71)
        Me.UserName.MaxLength = 50
        Me.UserName.Name = "UserName"
        Me.UserName.Size = New System.Drawing.Size(134, 20)
        Me.UserName.TabIndex = 0
        '
        'UserID
        '
        Me.UserID.Location = New System.Drawing.Point(119, 45)
        Me.UserID.MaxLength = 20
        Me.UserID.Name = "UserID"
        Me.UserID.Size = New System.Drawing.Size(134, 20)
        Me.UserID.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nama User"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "User"
        '
        'Form_User
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 581)
        Me.Controls.Add(Me.Panel_Browse)
        Me.Controls.Add(Me.PanelTombol)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_User"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pengguna"
        CType(Me.DGUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTombol.ResumeLayout(False)
        Me.PanelTombol.PerformLayout()
        Me.Panel_Browse.ResumeLayout(False)
        Me.Panel_Tambah.ResumeLayout(False)
        Me.Panel_Tambah.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdSimpan As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdBatal As System.Windows.Forms.Button
    Friend WithEvents cmdHapus As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdTambah As System.Windows.Forms.Button
    Public WithEvents DGUser As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PanelTombol As Panel
    Friend WithEvents Panel_Browse As Panel
    Friend WithEvents Panel_Tambah As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents NamaToko As TextBox
    Friend WithEvents BtnBrowse As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents kode_toko As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Password As TextBox
    Friend WithEvents UserName As TextBox
    Friend WithEvents UserID As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCari As TextBox
    Friend WithEvents IDUser As DataGridViewTextBoxColumn
    Friend WithEvents User_Name As DataGridViewTextBoxColumn
    Friend WithEvents LastLogin As DataGridViewTextBoxColumn
    Friend WithEvents btnResetPassword As DataGridViewButtonColumn
End Class
