Public Class Form_UserAkses
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim AddLevel As Boolean, EditLevel As Boolean
    Dim AddUser As Boolean, EditUser As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable

    Sub Data_UserAkses()
        Dim dbUser As DataTable, mMenu As String
        DGUser.Rows.Clear()
        DG_User.Rows.Clear()
        If Trim(UserID.Text) <> "" Then
            Me.Cursor = Cursors.WaitCursor
            SQL = "select menu from m_usermenu " &
                " order by menu "
            dbTable = Proses.ExecuteQuery(SQL)
            For a = 0 To dbTable.Rows.Count - 1
                mMenu = dbTable.Rows(a)!menu
                SQL = "Select menu, Akses, Baru, Edit, Hapus, Laporan " &
                        "From M_UserAkses " &
                        "Where AktifYN = 'Y' " &
                        "  And User_ID = '" & Trim(UserID.Text) & "' " &
                        "  and menu = '" & mMenu & "' " &
                        "Order By User_ID, menu"
                dbUser = Proses.ExecuteQuery(SQL)
                If dbUser.Rows.Count <> 0 Then
                    DGUser.Rows.Add(dbUser.Rows(0)!menu,
                        dbUser.Rows(0)!Akses, dbUser.Rows(0)!Baru,
                        dbUser.Rows(0)!Edit, dbUser.Rows(0)!Hapus,
                        dbUser.Rows(0)!Laporan)
                    If AddUser Then
                        DG_User.Rows.Add(dbUser.Rows(0)!menu,
                        dbUser.Rows(0)!Akses, dbUser.Rows(0)!Baru,
                        dbUser.Rows(0)!Edit, dbUser.Rows(0)!Hapus,
                        dbUser.Rows(0)!Laporan)
                    End If
                Else
                    DGUser.Rows.Add(mMenu, 0, 0, 0, 0, 0)
                    If AddUser Then
                        DG_User.Rows.Add(mMenu, 0, 0, 0, 0, 0)
                    End If

                End If
            Next (a)
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Sub Data_UserMenu()
        SQL = "Select menu,0 , 0 ,0 ,0 ,0 " &
            "From M_UserMenu Order By Menu"
        dbTable = Proses.ExecuteQuery(SQL)
        DGMenu.Refresh()
        DGMenu.DataSource = dbTable
        DGMenu.Columns(0).Width = 232
        DGMenu.Columns(1).Visible = False
        DGMenu.Columns(2).Visible = False
        DGMenu.Columns(3).Visible = False
        DGMenu.Columns(4).Visible = False
        DGMenu.Columns(5).Visible = False
        DGMenu.Refresh()
    End Sub


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LAdd = True
        LEdit = False
        AturTombolLevel(False)
        'cmbUserID.Focus()
    End Sub

    Private Sub Form_User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserID.Text = ""
        User_ID.Text = ""
        Data_UserAkses()
        AturTombol(True)

        DGUser.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGUser.BackgroundColor = Color.LightGray
        DGUser.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGUser.DefaultCellStyle.SelectionForeColor = Color.White
        DGUser.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGUser.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGUser.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGUser.Columns(0).Width = 220
        DGUser.Columns(1).Width = 50
        DGUser.Columns(2).Width = 50
        DGUser.Columns(3).Width = 50
        DGUser.Columns(4).Width = 50
        DGUser.Columns(5).Width = 50

        DG_User.CellBorderStyle = DataGridViewCellBorderStyle.None
        DG_User.BackgroundColor = Color.LightGray
        DG_User.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DG_User.DefaultCellStyle.SelectionForeColor = Color.White
        DG_User.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DG_User.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DG_User.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DG_User.Columns(0).Width = 220
        DG_User.Columns(1).Width = 50
        DG_User.Columns(2).Width = 50
        DG_User.Columns(3).Width = 50
        DG_User.Columns(4).Width = 50
        DG_User.Columns(5).Width = 50

        'Menu
        DGMenu.GridColor = Color.Red
        DGMenu.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGMenu.BackgroundColor = Color.LightGray

        DGMenu.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGMenu.DefaultCellStyle.SelectionForeColor = Color.White

        DGMenu.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        DGMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGMenu.AllowUserToResizeColumns = False

        DGMenu.RowsDefaultCellStyle.BackColor = Color.LightCyan           'Color.Bisque
        DGMenu.AlternatingRowsDefaultCellStyle.BackColor = Color.White


        With Me.DGUser.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With

        With Me.DG_User.RowTemplate
            .Height = 30
            .MinimumHeight = 30
        End With

        With Me.DGMenu.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With

    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdExitUser.Enabled = tAktif
        cmdTambahUser.Enabled = tAktif
        cmdEditUser.Enabled = tAktif
        cmdHapusUser.Enabled = tAktif
        cmdExitUser.Enabled = tAktif
        cmdSimpanMenu.Enabled = tAktif

        cmdSimpanUser.Enabled = Not tAktif
        If AddUser Then DGUser.Rows.Clear()
        If tAktif = True Then
            If TabUser.TabCount > 1 Then
                TabUser.TabPages.RemoveAt(1)
            End If
            DG_User.Rows.Clear()
        End If
        cmdBatalUser.Visible = Not tAktif
        cmdSimpanUser.Visible = Not tAktif
    End Sub

    Private Sub cmbUserID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Data_UserAkses()
    End Sub


    Private Sub txtMenu_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMenu.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(32) Then e.KeyChar = Chr(95)
        If e.KeyChar = Chr(13) Then cmdSimpanMenu.Focus()
        If Len(txtMenu.Text) > 0 Then
            cmdSimpanMenu.Enabled = True
        Else
            cmdSimpanMenu.Enabled = False
        End If
    End Sub


    Private Sub cmdSimpanMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpanMenu.Click
        SQL = "Select Menu From m_UserMenu " &
                "Where Menu = '" & Trim(txtMenu.Text) & "'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            txtMenu.Focus()
            MsgBox("Nama Menu Sudah ADA!", vbCritical, "Warning!")
            Exit Sub
        End If
        SQL = "Insert into m_UserMenu (Menu) " &
            "values ('" & txtMenu.Text & "') "
        Proses.ExecuteNonQuery(SQL)
        txtMenu.Text = ""
        Data_UserMenu()
        txtMenu.Focus()
    End Sub


    Private Sub DGMenu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGMenu.KeyDown
        If e.KeyCode = Keys.Delete Then
            SQL = "Delete From m_UserMenu Where Menu = '" & txtMenu.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            SQL = "Delete From m_UserAkses Where menu = '" & txtMenu.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            txtMenu.Text = ""
            Data_UserMenu()
            txtMenu.Focus()
        End If
    End Sub

    Private Sub DGUser_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If DGUser.RowCount = 0 Then Exit Sub
        If e.ColumnIndex = 1 Then
            If DGUser.Rows(e.RowIndex).Cells(1).Value = True Then
                DGUser.Rows(e.RowIndex).Cells(1).Value = False
                DGUser.Rows(e.RowIndex).Cells(2).Value = False
                DGUser.Rows(e.RowIndex).Cells(3).Value = False
                DGUser.Rows(e.RowIndex).Cells(4).Value = False
                DGUser.Rows(e.RowIndex).Cells(5).Value = False
            Else
                DGUser.Rows(e.RowIndex).Cells(1).Value = True
                DGUser.Rows(e.RowIndex).Cells(2).Value = True
                DGUser.Rows(e.RowIndex).Cells(3).Value = True
                DGUser.Rows(e.RowIndex).Cells(4).Value = True
                DGUser.Rows(e.RowIndex).Cells(5).Value = True
            End If
        ElseIf e.ColumnIndex = 2 Then
            If DGUser.Rows(e.RowIndex).Cells(2).Value = True Then
                DGUser.Rows(e.RowIndex).Cells(2).Value = False
            Else
                DGUser.Rows(e.RowIndex).Cells(2).Value = True
            End If
        ElseIf e.ColumnIndex = 3 Then
            If DGUser.Rows(e.RowIndex).Cells(3).Value = True Then
                DGUser.Rows(e.RowIndex).Cells(3).Value = False
            Else
                DGUser.Rows(e.RowIndex).Cells(3).Value = True
            End If
        ElseIf e.ColumnIndex = 4 Then
            If DGUser.Rows(e.RowIndex).Cells(4).Value = True Then
                DGUser.Rows(e.RowIndex).Cells(4).Value = False
            Else
                DGUser.Rows(e.RowIndex).Cells(4).Value = True
            End If
        ElseIf e.ColumnIndex = 5 Then
            If DGUser.Rows(e.RowIndex).Cells(5).Value = True Then
                DGUser.Rows(e.RowIndex).Cells(5).Value = False
            Else
                DGUser.Rows(e.RowIndex).Cells(5).Value = True
            End If
        End If
    End Sub

    Private Sub DGMenu_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGMenu.KeyUp
        txtMenu.Text = DGMenu.Rows(DGMenu.CurrentCell.RowIndex).Cells(0).Value
    End Sub
    Public Sub AturTombolLevel(ByVal tAktif As Boolean)

        cmdExitUser.Enabled = tAktif
        cmdSimpanUser.Enabled = tAktif
        cmdSimpanMenu.Enabled = tAktif

    End Sub




    Private Sub cmdExit_Click_1(sender As Object, e As EventArgs) Handles cmdExitUser.Click
        Me.Close()
    End Sub
    Private Sub DG_User_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_User.CellClick

    End Sub
    Private Sub cmdSimpanUser_Click(sender As Object, e As EventArgs) Handles cmdSimpanUser.Click
        Dim i As Byte, j As Byte, tValue(7) As String

        SQL = "Update m_UserAkses set aktifYN = 'X' " &
            "Where User_id = '" & User_ID.Text & "' "
        Proses.ExecuteNonQuery(SQL)

        If Trim(User_ID.Text) <> "" Then
            For i = 0 To DG_User.Rows.Count - 1
                For j = 0 To DG_User.Columns.Count - 1
                    If j = 0 Then
                        tValue(j) = DG_User.Rows(i).Cells(j).Value
                    Else
                        tValue(j) = IIf(DG_User.Rows(i).Cells(j).Value, 1, 0)
                    End If
                Next
                SQL = "Insert into m_UserAkses(User_ID, menu, Akses, Baru, " &
                    "Edit, Hapus, Laporan, AktifYN, area) Values ('" & User_ID.Text & "', " &
                    "'" & tValue(0) & "', " & tValue(1) & ", " & tValue(2) & ", " &
                    "" & tValue(3) & ", " & tValue(4) & ", " & tValue(5) & ", 'Y', '" & FrmMenuUtama.Kode_Toko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next
        End If
        AddLevel = False
        EditLevel = False
        AturTombol(True)
        SQL = "Delete From m_UserAkses " &
            "Where User_id = '" & User_ID.Text & "' and AktifYN = 'X' "
        Proses.ExecuteNonQuery(SQL)
        UserID.Text = User_ID.Text
        Data_UserAkses()


        'MsgBox("Data Berahsil di simpan!", vbOKOnly, "Congratulation!")
    End Sub

    Private Sub cmdTambahUser_Click(sender As Object, e As EventArgs) Handles cmdTambahUser.Click
        AddUser = True
        EditUser = False
        AturTombol(False)
        If TabUser.TabCount = 1 Then
            TabUser.TabPages.Insert(1, TabEntry)
        End If
        TabUser.SelectedTab = TabEntry
        btnFindUser.Enabled = True
        User_ID.ReadOnly = False
        DG_User.Rows.Clear()
        User_ID.Text = ""
        User_ID.Focus()
    End Sub

    Private Sub btnFindUser_Click(sender As Object, e As EventArgs) Handles btnFindUser.Click
        Form_Daftar.txtQuery.Text = "Select * " &
                " From m_User " &
                "Where AktifYN = 'Y' " &
                "Order By username "
        Form_Daftar.Text = "Daftar User"
        Form_Daftar.DGView.Focus()
        Form_Daftar.ShowDialog()
        User_ID.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        SQL = "Select * From m_User " &
           "Where userid = '" & User_ID.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            User_ID.Text = dbTable.Rows(0) !userid
            Data_UserAkses()
        Else
            User_ID.Text = ""
        End If
    End Sub

    Private Sub btnCariUser_Click(sender As Object, e As EventArgs) Handles btnCariUser.Click
        Form_Daftar.txtQuery.Text = "Select * " &
                " From m_User " &
                "Where AktifYN = 'Y' and userid <> 'EKO_K' " &
                "Order By userid "
        Form_Daftar.Text = "Daftar User"
        Form_Daftar.DGView.Focus()
        Form_Daftar.ShowDialog()
        UserID.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        SQL = "Select * From m_User " &
           "Where userid = '" & UserID.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            UserID.Text = dbTable.Rows(0) !userid
            Data_UserAkses()
        Else
            UserID.Text = ""
        End If
    End Sub

    Private Sub cmdBatalUser_Click(sender As Object, e As EventArgs) Handles cmdBatalUser.Click
        Data_UserAkses()
        AturTombol(True)
        AddUser = False
        EditUser = False
    End Sub

    Private Sub cmdEditUser_Click(sender As Object, e As EventArgs) Handles cmdEditUser.Click
        Dim mMenu As String = "", dbUser As DataTable
        If DGUser.Rows.Count <> 0 Then
            If Trim(UserID.Text) <> "" Then
                Me.Cursor = Cursors.WaitCursor
                DG_User.Rows.Clear()
                SQL = "select menu from m_usermenu " &
                    " order by menu "
                dbTable = Proses.ExecuteQuery(SQL)
                For a = 0 To dbTable.Rows.Count - 1
                    mMenu = dbTable.Rows(a) !menu
                    SQL = "Select menu, Akses, Baru, Edit, Hapus, Laporan " &
                        "From M_UserAkses " &
                        "Where AktifYN = 'Y' " &
                        "  And User_ID = '" & Trim(UserID.Text) & "' " &
                        "  and menu = '" & mMenu & "' " &
                        "Order By User_ID, menu"
                    dbUser = Proses.ExecuteQuery(SQL)
                    If dbUser.Rows.Count <> 0 Then
                        DG_User.Rows.Add(dbUser.Rows(0) !menu,
                        dbUser.Rows(0) !Akses, dbUser.Rows(0) !Baru,
                        dbUser.Rows(0) !Edit, dbUser.Rows(0) !Hapus,
                        dbUser.Rows(0) !Laporan)
                    Else
                        DG_User.Rows.Add(mMenu, 0, 0, 0, 0, 0)
                    End If
                Next (a)
                DG_User.ReadOnly = False
                Me.Cursor = Cursors.Default
                AddUser = False
                EditUser = True
                AturTombol(False)
                If TabUser.TabCount = 1 Then
                    TabUser.TabPages.Insert(1, TabEntry)
                End If
                TabUser.SelectedTab = TabEntry
                User_ID.Text = Trim(UserID.Text)
                User_ID.ReadOnly = True
                btnFindUser.Enabled = False
            Else
                MsgBox("User id yang di edit belum dipilih", MsgBoxStyle.OkOnly + vbInformation, ".:Pilih user id nya dahulu !")
            End If
        Else
            MsgBox("User id yang di edit belum dipilih", MsgBoxStyle.OkOnly + vbInformation, ".:Pilih user id nya dahulu !")
        End If
    End Sub

    Private Sub cmdHapusUser_Click(sender As Object, e As EventArgs) Handles cmdHapusUser.Click
        If UserID.Text = "" Then
            MsgBox("Pilih userid yg akan di hapus terlebih dahulu !", vbCritical, ".:User Id yg di hapus belum di pilih !")
            UserID.Focus()
            Exit Sub
        End If
        If MsgBox("Yakin hapus user akses " & UserID.Text & " ? ", vbYesNo + vbInformation, ".: Confirm") = vbYes Then
            SQL = "delete m_UserAkses " &
                " Where User_id = '" & UserID.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            Data_UserAkses()
            DGUser.Rows.Clear()
        End If
    End Sub

    Private Sub DG_User_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DG_User.CellContentClick
        If DG_User.RowCount = 0 Then Exit Sub
        If e.ColumnIndex = 1 Then
            If DG_User.Rows(e.RowIndex).Cells(1).Value = True Then
                DG_User.Rows(e.RowIndex).Cells(1).Value = False
                DG_User.Rows(e.RowIndex).Cells(2).Value = False
                DG_User.Rows(e.RowIndex).Cells(3).Value = False
                DG_User.Rows(e.RowIndex).Cells(4).Value = False
                DG_User.Rows(e.RowIndex).Cells(5).Value = False
            Else
                DG_User.Rows(e.RowIndex).Cells(1).Value = True
                DG_User.Rows(e.RowIndex).Cells(2).Value = True
                DG_User.Rows(e.RowIndex).Cells(3).Value = True
                DG_User.Rows(e.RowIndex).Cells(4).Value = True
                DG_User.Rows(e.RowIndex).Cells(5).Value = True
            End If
        ElseIf e.ColumnIndex = 2 Then
            If DG_User.Rows(e.RowIndex).Cells(2).Value = True Then
                DG_User.Rows(e.RowIndex).Cells(2).Value = False
            Else
                DG_User.Rows(e.RowIndex).Cells(2).Value = True
            End If
        ElseIf e.ColumnIndex = 3 Then
            If DG_User.Rows(e.RowIndex).Cells(3).Value = True Then
                DG_User.Rows(e.RowIndex).Cells(3).Value = False
            Else
                DG_User.Rows(e.RowIndex).Cells(3).Value = True
            End If
        ElseIf e.ColumnIndex = 4 Then
            If DG_User.Rows(e.RowIndex).Cells(4).Value = True Then
                DG_User.Rows(e.RowIndex).Cells(4).Value = False
            Else
                DG_User.Rows(e.RowIndex).Cells(4).Value = True
            End If
        ElseIf e.ColumnIndex = 5 Then
            If DG_User.Rows(e.RowIndex).Cells(5).Value = True Then
                DG_User.Rows(e.RowIndex).Cells(5).Value = False
            Else
                DG_User.Rows(e.RowIndex).Cells(5).Value = True
            End If
        End If
    End Sub


    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = 0 Then
        ElseIf e.TabPageIndex = 1 Then
            Data_UserMenu()
        End If
    End Sub

    Private Sub DGUser_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGUser.CellContentClick

    End Sub

    Private Sub UserID_TextChanged(sender As Object, e As EventArgs) Handles UserID.TextChanged

    End Sub

    Private Sub DGRequest_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub UserID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UserID.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select * From m_User " &
              "Where userid = '" & UserID.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                UserID.Text = dbTable.Rows(0) !userid
                Data_UserAkses()
            Else
                UserID.Text = ""
            End If
        End If
    End Sub

    Private Sub DGMLevel_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DGMenu_Click(sender As Object, e As EventArgs) Handles DGMenu.Click
        txtMenu.Text = DGMenu.Rows(DGMenu.CurrentCell.RowIndex).Cells(0).Value
    End Sub
End Class