<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_UserAkses
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_UserAkses))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.TabUser = New System.Windows.Forms.TabControl()
        Me.TabDaftar = New System.Windows.Forms.TabPage()
        Me.DGUser = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.btnCariUser = New System.Windows.Forms.Button()
        Me.UserID = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabEntry = New System.Windows.Forms.TabPage()
        Me.DG_User = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnFindUser = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.User_ID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cmdBatalUser = New System.Windows.Forms.Button()
        Me.cmdEditUser = New System.Windows.Forms.Button()
        Me.cmdSimpanUser = New System.Windows.Forms.Button()
        Me.cmdTambahUser = New System.Windows.Forms.Button()
        Me.cmdHapusUser = New System.Windows.Forms.Button()
        Me.cmdExitUser = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DGMenu = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdSimpanMenu = New System.Windows.Forms.Button()
        Me.txtMenu = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TabUser.SuspendLayout()
        Me.TabDaftar.SuspendLayout()
        CType(Me.DGUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.TabEntry.SuspendLayout()
        CType(Me.DG_User, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DGMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(616, 581)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel5)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(608, 555)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pengaturan User"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.TabUser)
        Me.Panel5.Controls.Add(Me.Panel3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(602, 549)
        Me.Panel5.TabIndex = 32
        '
        'TabUser
        '
        Me.TabUser.Controls.Add(Me.TabDaftar)
        Me.TabUser.Controls.Add(Me.TabEntry)
        Me.TabUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabUser.Location = New System.Drawing.Point(84, 0)
        Me.TabUser.Name = "TabUser"
        Me.TabUser.SelectedIndex = 0
        Me.TabUser.Size = New System.Drawing.Size(518, 549)
        Me.TabUser.TabIndex = 32
        '
        'TabDaftar
        '
        Me.TabDaftar.Controls.Add(Me.DGUser)
        Me.TabDaftar.Controls.Add(Me.Panel8)
        Me.TabDaftar.Location = New System.Drawing.Point(4, 22)
        Me.TabDaftar.Name = "TabDaftar"
        Me.TabDaftar.Padding = New System.Windows.Forms.Padding(3)
        Me.TabDaftar.Size = New System.Drawing.Size(510, 523)
        Me.TabDaftar.TabIndex = 0
        Me.TabDaftar.Text = "Daftar User Akses"
        Me.TabDaftar.UseVisualStyleBackColor = True
        '
        'DGUser
        '
        Me.DGUser.AllowUserToAddRows = False
        Me.DGUser.AllowUserToDeleteRows = False
        Me.DGUser.AllowUserToOrderColumns = True
        Me.DGUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGUser.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column6, Me.Column3, Me.Column4, Me.Column5})
        Me.DGUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGUser.Location = New System.Drawing.Point(3, 34)
        Me.DGUser.Name = "DGUser"
        Me.DGUser.ReadOnly = True
        Me.DGUser.RowHeadersVisible = False
        Me.DGUser.Size = New System.Drawing.Size(504, 486)
        Me.DGUser.TabIndex = 33
        '
        'Column1
        '
        Me.Column1.HeaderText = "Jenis Transaksi"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 200
        '
        'Column2
        '
        Me.Column2.HeaderText = "Akses"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column2.Width = 60
        '
        'Column6
        '
        Me.Column6.HeaderText = "Tambah"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column6.Width = 60
        '
        'Column3
        '
        Me.Column3.HeaderText = "Edit"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column3.Width = 60
        '
        'Column4
        '
        Me.Column4.HeaderText = "Hapus"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column4.Width = 60
        '
        'Column5
        '
        Me.Column5.HeaderText = "Cetak"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column5.Width = 60
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.btnCariUser)
        Me.Panel8.Controls.Add(Me.UserID)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(3, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(504, 31)
        Me.Panel8.TabIndex = 32
        '
        'btnCariUser
        '
        Me.btnCariUser.Image = CType(resources.GetObject("btnCariUser.Image"), System.Drawing.Image)
        Me.btnCariUser.Location = New System.Drawing.Point(205, 2)
        Me.btnCariUser.Name = "btnCariUser"
        Me.btnCariUser.Size = New System.Drawing.Size(30, 25)
        Me.btnCariUser.TabIndex = 38
        Me.btnCariUser.UseVisualStyleBackColor = True
        '
        'UserID
        '
        Me.UserID.Location = New System.Drawing.Point(68, 4)
        Me.UserID.Name = "UserID"
        Me.UserID.Size = New System.Drawing.Size(136, 20)
        Me.UserID.TabIndex = 27
        Me.UserID.Text = "12345678901234567890"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "User ID :"
        '
        'TabEntry
        '
        Me.TabEntry.Controls.Add(Me.DG_User)
        Me.TabEntry.Controls.Add(Me.Panel4)
        Me.TabEntry.Location = New System.Drawing.Point(4, 22)
        Me.TabEntry.Name = "TabEntry"
        Me.TabEntry.Padding = New System.Windows.Forms.Padding(3)
        Me.TabEntry.Size = New System.Drawing.Size(510, 523)
        Me.TabEntry.TabIndex = 1
        Me.TabEntry.Text = "Entry Pengaturan User"
        Me.TabEntry.UseVisualStyleBackColor = True
        '
        'DG_User
        '
        Me.DG_User.AllowUserToAddRows = False
        Me.DG_User.AllowUserToDeleteRows = False
        Me.DG_User.AllowUserToOrderColumns = True
        Me.DG_User.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_User.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewCheckBoxColumn2, Me.DataGridViewCheckBoxColumn3, Me.DataGridViewCheckBoxColumn4, Me.DataGridViewCheckBoxColumn5, Me.DataGridViewCheckBoxColumn6})
        Me.DG_User.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_User.Location = New System.Drawing.Point(3, 34)
        Me.DG_User.Name = "DG_User"
        Me.DG_User.ReadOnly = True
        Me.DG_User.RowHeadersVisible = False
        Me.DG_User.Size = New System.Drawing.Size(504, 486)
        Me.DG_User.TabIndex = 32
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Jenis Transaksi"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.HeaderText = "Akses"
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        Me.DataGridViewCheckBoxColumn2.ReadOnly = True
        Me.DataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn2.Width = 60
        '
        'DataGridViewCheckBoxColumn3
        '
        Me.DataGridViewCheckBoxColumn3.HeaderText = "Tambah"
        Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        Me.DataGridViewCheckBoxColumn3.ReadOnly = True
        Me.DataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn3.Width = 60
        '
        'DataGridViewCheckBoxColumn4
        '
        Me.DataGridViewCheckBoxColumn4.HeaderText = "Edit"
        Me.DataGridViewCheckBoxColumn4.Name = "DataGridViewCheckBoxColumn4"
        Me.DataGridViewCheckBoxColumn4.ReadOnly = True
        Me.DataGridViewCheckBoxColumn4.Width = 60
        '
        'DataGridViewCheckBoxColumn5
        '
        Me.DataGridViewCheckBoxColumn5.HeaderText = "Hapus"
        Me.DataGridViewCheckBoxColumn5.Name = "DataGridViewCheckBoxColumn5"
        Me.DataGridViewCheckBoxColumn5.ReadOnly = True
        Me.DataGridViewCheckBoxColumn5.Width = 60
        '
        'DataGridViewCheckBoxColumn6
        '
        Me.DataGridViewCheckBoxColumn6.HeaderText = "Cetak"
        Me.DataGridViewCheckBoxColumn6.Name = "DataGridViewCheckBoxColumn6"
        Me.DataGridViewCheckBoxColumn6.ReadOnly = True
        Me.DataGridViewCheckBoxColumn6.Width = 60
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnFindUser)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.User_ID)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(504, 31)
        Me.Panel4.TabIndex = 31
        '
        'btnFindUser
        '
        Me.btnFindUser.Image = CType(resources.GetObject("btnFindUser.Image"), System.Drawing.Image)
        Me.btnFindUser.Location = New System.Drawing.Point(205, 2)
        Me.btnFindUser.Name = "btnFindUser"
        Me.btnFindUser.Size = New System.Drawing.Size(30, 25)
        Me.btnFindUser.TabIndex = 37
        Me.btnFindUser.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(51, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 13)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = ":"
        '
        'User_ID
        '
        Me.User_ID.Location = New System.Drawing.Point(68, 4)
        Me.User_ID.Name = "User_ID"
        Me.User_ID.Size = New System.Drawing.Size(136, 20)
        Me.User_ID.TabIndex = 34
        Me.User_ID.Text = "12345678901234567890"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "User ID"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.cmdBatalUser)
        Me.Panel3.Controls.Add(Me.cmdEditUser)
        Me.Panel3.Controls.Add(Me.cmdSimpanUser)
        Me.Panel3.Controls.Add(Me.cmdTambahUser)
        Me.Panel3.Controls.Add(Me.cmdHapusUser)
        Me.Panel3.Controls.Add(Me.cmdExitUser)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(84, 549)
        Me.Panel3.TabIndex = 30
        '
        'cmdBatalUser
        '
        Me.cmdBatalUser.Image = CType(resources.GetObject("cmdBatalUser.Image"), System.Drawing.Image)
        Me.cmdBatalUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBatalUser.Location = New System.Drawing.Point(3, 518)
        Me.cmdBatalUser.Name = "cmdBatalUser"
        Me.cmdBatalUser.Size = New System.Drawing.Size(76, 28)
        Me.cmdBatalUser.TabIndex = 34
        Me.cmdBatalUser.Text = "&Batal"
        Me.cmdBatalUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdBatalUser.UseVisualStyleBackColor = True
        Me.cmdBatalUser.Visible = False
        '
        'cmdEditUser
        '
        Me.cmdEditUser.Image = CType(resources.GetObject("cmdEditUser.Image"), System.Drawing.Image)
        Me.cmdEditUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEditUser.Location = New System.Drawing.Point(3, 31)
        Me.cmdEditUser.Name = "cmdEditUser"
        Me.cmdEditUser.Size = New System.Drawing.Size(77, 28)
        Me.cmdEditUser.TabIndex = 31
        Me.cmdEditUser.Text = "&Ubah"
        Me.cmdEditUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdEditUser.UseVisualStyleBackColor = True
        '
        'cmdSimpanUser
        '
        Me.cmdSimpanUser.Enabled = False
        Me.cmdSimpanUser.Image = CType(resources.GetObject("cmdSimpanUser.Image"), System.Drawing.Image)
        Me.cmdSimpanUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpanUser.Location = New System.Drawing.Point(3, 121)
        Me.cmdSimpanUser.Name = "cmdSimpanUser"
        Me.cmdSimpanUser.Size = New System.Drawing.Size(76, 29)
        Me.cmdSimpanUser.TabIndex = 33
        Me.cmdSimpanUser.Text = "Simpan"
        Me.cmdSimpanUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpanUser.UseVisualStyleBackColor = True
        Me.cmdSimpanUser.Visible = False
        '
        'cmdTambahUser
        '
        Me.cmdTambahUser.Image = CType(resources.GetObject("cmdTambahUser.Image"), System.Drawing.Image)
        Me.cmdTambahUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTambahUser.Location = New System.Drawing.Point(3, 3)
        Me.cmdTambahUser.Name = "cmdTambahUser"
        Me.cmdTambahUser.Size = New System.Drawing.Size(77, 28)
        Me.cmdTambahUser.TabIndex = 30
        Me.cmdTambahUser.Text = "&Tambah"
        Me.cmdTambahUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTambahUser.UseVisualStyleBackColor = True
        '
        'cmdHapusUser
        '
        Me.cmdHapusUser.Image = CType(resources.GetObject("cmdHapusUser.Image"), System.Drawing.Image)
        Me.cmdHapusUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHapusUser.Location = New System.Drawing.Point(3, 59)
        Me.cmdHapusUser.Name = "cmdHapusUser"
        Me.cmdHapusUser.Size = New System.Drawing.Size(77, 28)
        Me.cmdHapusUser.TabIndex = 32
        Me.cmdHapusUser.Text = "&Hapus"
        Me.cmdHapusUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdHapusUser.UseVisualStyleBackColor = True
        '
        'cmdExitUser
        '
        Me.cmdExitUser.Image = CType(resources.GetObject("cmdExitUser.Image"), System.Drawing.Image)
        Me.cmdExitUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExitUser.Location = New System.Drawing.Point(3, 87)
        Me.cmdExitUser.Name = "cmdExitUser"
        Me.cmdExitUser.Size = New System.Drawing.Size(77, 28)
        Me.cmdExitUser.TabIndex = 29
        Me.cmdExitUser.Text = "E&xit"
        Me.cmdExitUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExitUser.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(608, 555)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Daftar Menu Program"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DGMenu)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 43)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(602, 509)
        Me.Panel2.TabIndex = 31
        '
        'DGMenu
        '
        Me.DGMenu.AllowUserToAddRows = False
        Me.DGMenu.AllowUserToDeleteRows = False
        Me.DGMenu.AllowUserToOrderColumns = True
        Me.DGMenu.AllowUserToResizeRows = False
        Me.DGMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGMenu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGMenu.Location = New System.Drawing.Point(0, 0)
        Me.DGMenu.Name = "DGMenu"
        Me.DGMenu.RowHeadersVisible = False
        Me.DGMenu.Size = New System.Drawing.Size(602, 509)
        Me.DGMenu.TabIndex = 29
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmdSimpanMenu)
        Me.Panel1.Controls.Add(Me.txtMenu)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(602, 40)
        Me.Panel1.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(39, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Menu :"
        '
        'cmdSimpanMenu
        '
        Me.cmdSimpanMenu.Enabled = False
        Me.cmdSimpanMenu.Image = CType(resources.GetObject("cmdSimpanMenu.Image"), System.Drawing.Image)
        Me.cmdSimpanMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSimpanMenu.Location = New System.Drawing.Point(383, 5)
        Me.cmdSimpanMenu.Name = "cmdSimpanMenu"
        Me.cmdSimpanMenu.Size = New System.Drawing.Size(68, 29)
        Me.cmdSimpanMenu.TabIndex = 28
        Me.cmdSimpanMenu.Text = "Simpan"
        Me.cmdSimpanMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSimpanMenu.UseVisualStyleBackColor = True
        '
        'txtMenu
        '
        Me.txtMenu.Location = New System.Drawing.Point(85, 10)
        Me.txtMenu.MaxLength = 50
        Me.txtMenu.Name = "txtMenu"
        Me.txtMenu.Size = New System.Drawing.Size(193, 20)
        Me.txtMenu.TabIndex = 0
        '
        'Form_UserAkses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 581)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_UserAkses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hak Akses User"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.TabUser.ResumeLayout(False)
        Me.TabDaftar.ResumeLayout(False)
        CType(Me.DGUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.TabEntry.ResumeLayout(False)
        CType(Me.DG_User, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.DGMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DGMenu As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSimpanMenu As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMenu As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents cmdExitUser As Button
    Friend WithEvents cmdEditUser As Button
    Friend WithEvents cmdSimpanUser As Button
    Friend WithEvents cmdTambahUser As Button
    Friend WithEvents cmdHapusUser As Button
    Friend WithEvents cmdBatalUser As Button
    Friend WithEvents TabUser As TabControl
    Friend WithEvents TabDaftar As TabPage
    Friend WithEvents TabEntry As TabPage
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents UserID As TextBox
    Friend WithEvents User_ID As TextBox
    Friend WithEvents DGUser As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents btnFindUser As Button
    Friend WithEvents btnCariUser As Button
    Friend WithEvents DG_User As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewCheckBoxColumn
    Friend WithEvents Column6 As DataGridViewCheckBoxColumn
    Friend WithEvents Column3 As DataGridViewCheckBoxColumn
    Friend WithEvents Column4 As DataGridViewCheckBoxColumn
    Friend WithEvents Column5 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn4 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn5 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn6 As DataGridViewCheckBoxColumn
End Class
