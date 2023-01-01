

Public Class Form_User
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable

    Sub Data_Record()
        'date_Format(LastLogin, '%d-%m-%Y %T') as
        Dim a As Integer, mKondisi As String = ""
        If Trim(txtCari.Text) <> "" Then
            mKondisi = " and M_User.UserID like '%" & txtCari.Text & "%' "
        End If
        SQL = "Select M_User.UserID, M_User.UserName,  " &
            "convert(varchar(19), M_User.LastLogin, 13)  LastLogin " &
            " From M_User " &
            "Where M_User.AktifYN = 'Y' " & mKondisi & " " &
            "Order By M_User.UserID, M_User.UserName"

        dbTable = Proses.ExecuteQuery(SQL)
        DGUser.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGUser.Rows.Add(.Table.Rows(a) !userid,
                                .Table.Rows(a) !Username,
                                .Table.Rows(a) !lastlogin,
                                "Reset Password")
            Next (a)
        End With
        'DGUser.DataSource = dbTable
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        UserID.Focus()
        ClearTextBoxes()
        kode_toko.Text = FrmMenuUtama.Kode_Toko.Text
        SQL = "Select nama From m_Toko " &
               "Where idrec = '" & kode_toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaToko.Text = dbTable.Rows(0) !nama
        End If
        UserID.ReadOnly = False
    End Sub

    Private Sub Form_User_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Call Data_Record()
    End Sub

    Private Sub Form_User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tUserID As String = FrmMenuUtama.TsPengguna.Text
        DGUser.GridColor = Color.Red
        DGUser.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGUser.BackgroundColor = Color.LightGray

        DGUser.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGUser.DefaultCellStyle.SelectionForeColor = Color.White

        DGUser.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        DGUser.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGUser.AllowUserToResizeColumns = False

        DGUser.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGUser.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        ClearTextBoxes()
        AturTombol(True)

        With DGUser.RowTemplate
            .Height = 35
            .MinimumHeight = 25
        End With
        If tUserID = "EKO_K" Then
            cmdTambah.Enabled = True
            cmdEdit.Enabled = True
            cmdHapus.Enabled = True
        Else
            cmdTambah.Enabled = Proses.UserAksesTombol(tUserID, "82_USER_BARU", "baru")
            cmdEdit.Enabled = Proses.UserAksesTombol(tUserID, "82_USER_BARU", "edit")
            cmdHapus.Enabled = Proses.UserAksesTombol(tUserID, "82_USER_BARU", "hapus")
        End If

    End Sub

    Private Sub ClearTextBoxes(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        If ctlcol Is Nothing Then ctlcol = Me.Controls
        For Each ctl As Control In ctlcol
            If TypeOf (ctl) Is TextBox Then
                DirectCast(ctl, TextBox).Clear()
            Else
                If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                    ClearTextBoxes(ctl.Controls)
                End If
            End If
        Next
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdTambah.Enabled = tAktif
        cmdEdit.Enabled = tAktif
        cmdHapus.Enabled = tAktif
        cmdSimpan.Visible = Not tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif

        DGUser.Visible = tAktif
        Panel_Tambah.Visible = Not tAktif
        Panel_Tambah.Enabled = Not tAktif
    End Sub

    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click

        If UserID.Text = "" Then
            MsgBox("User id tidak boleh kosong!", vbCritical, ".:Warning!")
            UserID.Focus()
            Exit Sub
        End If
        If UserName.Text = "" Then
            MsgBox("User name tidak boleh kosong !", vbCritical, ".:Warning!")
            UserName.Focus()
            Exit Sub
        End If

        If Trim(Password.Text) = "" Then
            MsgBox("Password tidak boleh kosong !", vbCritical, ".:Warning")
            Password.Focus()
            Exit Sub
        End If
        If Len(Trim(Password.Text)) < 5 Then
            MsgBox("Panjang password kurang dari 5 karakter", vbCritical, ".:Warning!")
            Password.Focus()
            Exit Sub
        End If

        Dim Acak As New Crypto, encryptpassword As String = ""
        encryptpassword = Acak.Encrypt(Password.Text)
        If LAdd Then
            SQL = "Select UserID From m_user " &
                "Where UserID = '" & UserID.Text & "' " &
                "  and aktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                UserID.Focus()
                MsgBox("User ID Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            End If
            SQL = "Insert into m_User (userid, username, Password, AktifYN, LastLogin, " &
                    "Kode_Toko, lastupd, TransferYN) values ('" & UserID.Text & "', " &
                    " '" & UserName.Text & "', '" & encryptpassword & "', " &
                    " 'Y' ,GetDate(), '" & kode_toko.Text & "', GetDate(), 'N' ) "
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update m_user Set username = '" & UserName.Text & "', " &
                    "Password = '" & encryptpassword & "', " &
                    "Kode_Toko = '" & kode_toko.Text & "' " &
                    "Where UserID = '" & UserID.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Private Sub cmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub UserID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UserID.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(32) Then e.KeyChar = Chr(95)
        If e.KeyChar = Chr(13) Then UserName.Focus()
    End Sub

    Private Sub UserID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles UserID.LostFocus
        If LAdd Then
            SQL = "Select UserID From m_user " &
                "Where UserID = '" & UserID.Text & "' " &
                "  And aktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count = 0 Then
                UserName.Focus()
            Else
                MessageBox.Show("User ID sudah ada..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                UserID.Focus()
            End If
        End If
    End Sub

    Private Sub UserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UserName.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(32) Then e.KeyChar = Chr(95)
        If e.KeyChar = Chr(13) Then Password.Focus()
    End Sub

    Private Sub Password_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Password.KeyPress
        'e.KeyChar = UCase(e.KeyChar)
        'If e.KeyChar = Chr(32) Then e.KeyChar = Chr(95)
        If e.KeyChar = Chr(13) Then kode_toko.Focus()
    End Sub

    Private Sub DGUser_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGUser.CellClick
        If e.RowIndex >= 0 Then
            UserID.Text = DGUser.Rows(e.RowIndex).Cells(0).Value
            UserName.Text = DGUser.Rows(e.RowIndex).Cells(1).Value
            Password.Text = DGUser.Rows(e.RowIndex).Cells(3).Value
        End If
        If e.ColumnIndex = 3 Then 'Reset Password
            Dim tUserID As String = DGUser.Rows(DGUser.CurrentCell.RowIndex).Cells(0).Value
            Dim Acak As New Crypto, encryptpassword As String = ""
            If Trim(tUserID) <> "" Then
                '    MsgBox("Kas BON ini sudah di buatkan Invoice." & vbCrLf & " Tidak Bisa Edit/Hapus!", vbCritical + vbOKOnly, ".:Warning!")

                If MsgBox("Yakin password " & Trim(tUserID) & " di reset ?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                    encryptpassword = Acak.Encrypt("12345")
                    SQL = "Update m_user Set  " &
                        "Password = '" & encryptpassword & "', lastupd = GetDate() " &
                        "Where UserID = '" & tUserID & "' "
                    Proses.ExecuteNonQuery(SQL)
                    Data_Record()
                    MsgBox("Selamat, Password '" & Trim(tUserID) & "' menjadi : 12345", vbInformation + vbOKOnly, ".:Lain kali janga lupa lagi ya....")

                End If
            End If
        End If
    End Sub

    Private Sub DGUser_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGUser.CellContentClick

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If Proses.UserAksesTombol(FrmMenuUtama.TsPengguna.Text, "82_USER_BARU", "edit") = False Then
            MsgBox("Maaf, user anda TIDAK MEMPUNYAI akses " & vbCrLf & "untuk edit/lihat password user " & UserID.Text & " !", vbCritical, ".: Warning!")
            Exit Sub
        End If
        If Trim(UserID.Text) = "" Then
            MsgBox("User ID Belum di pilih!", vbCritical, ".:ERROR!")
            DGUser.Focus()
        End If
        LAdd = False
        LEdit = True
        SQL = "SELECT password FROM m_User where userid = '" & UserID.Text & "' and aktifYN='Y' "
        Password.Text = Proses.ExecuteSingleStrQuery(SQL)
        UserID.ReadOnly = True
        Dim Acak As New Crypto, Decryptpassword As String = ""
        Decryptpassword = Acak.Decrypt(Password.Text)
        Password.Text = Decryptpassword
        If UCase(Trim(UserID.Text)) = "EKO_K" Or UCase(Trim(UserID.Text)) = "ADMIN" Then
            Password.PasswordChar = "#"
        Else
            Password.PasswordChar = ""
        End If
        AturTombol(False)
        UserName.Focus()
    End Sub

    Private Sub cmdHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHapus.Click
        If Trim(UserID.Text) = "" Then
            MsgBox("User ID Belum di pilih!", vbCritical, ".:ERROR!")
            DGUser.Focus()
        End If
        If MsgBox("Yakin hapus data ini?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update m_user Set AktifYN = 'N' " &
                    "Where UserID = '" & UserID.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            ClearTextBoxes()
            Data_Record()
        End If
    End Sub

    Private Sub UserID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserID.TextChanged

    End Sub

    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click
        Form_Daftar.txtQuery.Text = "Select * " &
        " From m_Toko " &
        "Where AktifYN = 'Y' " &
        "Order By idRec "
        Form_Daftar.Text = "Daftar Toko"
        Form_Daftar.DGView.Focus()
        Form_Daftar.ShowDialog()
        kode_toko.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        SQL = "Select * From m_Toko " &
           "Where idrec = '" & kode_toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaToko.Text = dbTable.Rows(0) !nama
        Else
            NamaToko.Text = ""
            kode_toko.Text = ""
        End If
    End Sub

    Private Sub kode_toko_TextChanged(sender As Object, e As EventArgs) Handles kode_toko.TextChanged

    End Sub

    Private Sub UserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserName.TextChanged

    End Sub

    Private Sub Password_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Password.TextChanged

    End Sub

    Private Sub kode_toko_KeyPress(sender As Object, e As KeyPressEventArgs) Handles kode_toko.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaToko.Text = ""
            SQL = "Select * From m_Toko " &
               "Where idrec = '" & kode_toko.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaToko.Text = dbTable.Rows(0) !nama
                cmdSimpan.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_Toko " &
                    "Where AktifYN = 'Y' " &
                    " and nama like '%" & kode_toko.Text & "%' " &
                    "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                kode_toko.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select * From m_Toko " &
                    "Where idrec = '" & kode_toko.Text & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    NamaToko.Text = dbTable.Rows(0) !nama
                    cmdSimpan.Focus()
                Else
                    kode_toko.Text = ""
                    NamaToko.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub PanelTombol_Paint(sender As Object, e As PaintEventArgs) Handles PanelTombol.Paint

    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged

    End Sub

    Private Sub DGUser_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGUser.CellDoubleClick

    End Sub

    Private Sub DGUser_DoubleClick(sender As Object, e As EventArgs) Handles DGUser.DoubleClick
        cmdEdit_Click(sender, e)
    End Sub

    Private Sub txtCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCari.KeyPress
        If e.KeyChar = Chr(13) Then
            Data_Record()
        End If
    End Sub
End Class

