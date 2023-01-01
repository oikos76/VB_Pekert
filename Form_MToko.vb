Public Class Form_MToko
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String

    Sub Data_Record()
        SQL = "Select IdRec, Nama, Alamat1 as Alamat, Kota, ContactPerson as PIC, TlpCP as TLP, aktifyn " &
            " From M_Toko " &
            "Order By IdRec "
        dbTable = Proses.ExecuteQuery(SQL)
        'DGView.DataSource = dbTable
        'DGView.Columns(1).Width = 150
        'DGView.Columns(0).Width = 100
        'IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !IDRec,
                    .Table.Rows(a) !Nama, .Table.Rows(a) !alamat,
                    .Table.Rows(a) !kota, .Table.Rows(a) !pic,
                    .Table.Rows(a) !tlp)
                If .Table.Rows(a) !aktifyn = "N" Then
                    Me.DGView.Rows(a).Cells("_idtoko").Style.ForeColor = Color.LightPink
                    Me.DGView.Rows(a).Cells("_namatoko").Style.ForeColor = Color.LightPink
                End If
            Next (a)
        End With
    End Sub

    Sub Cari_Data()
        SQL = "Select IdRec, Nama, Alamat1 as Alamat, Kota, ContactPerson as PIC, TlpCP as TLP, aktifyn " &
            " From M_Toko " &
            "Where Nama Like '%" & tCari.Text & "%' or idrec like '%" & tCari.Text & "%'  " &
            "Order By Nama "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !IDRec,
                    .Table.Rows(a) !Nama, .Table.Rows(a) !alamat,
                    .Table.Rows(a) !kota, .Table.Rows(a) !pic,
                    .Table.Rows(a) !tlp)
                If .Table.Rows(a) !aktifyn = "N" Then
                    Me.DGView.Rows(a).Cells("_idtoko").Style.ForeColor = Color.LightPink
                    Me.DGView.Rows(a).Cells("_namatoko").Style.ForeColor = Color.LightPink
                End If
            Next (a)
        End With
    End Sub

    Sub Isi_Data()
        SQL = "Select * " &
            " From M_Toko " &
            "Where IDRec = '" & IDRec.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Nama.Text = dbTable.Rows(0) !Nama
            Alamat1.Text = dbTable.Rows(0) !Alamat1
            Alamat2.Text = IIf(IsDBNull(dbTable.Rows(0)!Alamat2), "", dbTable.Rows(0)!Alamat2)
            Kota.Text = IIf(IsDBNull(dbTable.Rows(0)!KOTA), "", dbTable.Rows(0)!Kota)
            Prov.Text = IIf(IsDBNull(dbTable.Rows(0)!PROV), "", dbTable.Rows(0)!prov)
            KodePos.Text = IIf(IsDBNull(dbTable.Rows(0)!KODEPOS), "", dbTable.Rows(0)!Kodepos)
            Phone.Text = IIf(IsDBNull(dbTable.Rows(0)!PHONE), "", dbTable.Rows(0)!phone)
            Email.Text = IIf(IsDBNull(dbTable.Rows(0)!EMAIL), "", dbTable.Rows(0)!email)
            ContactPerson.Text = IIf(IsDBNull(dbTable.Rows(0)!CONTACTPERSON), "", dbTable.Rows(0)!contactperson)
            Jabatan.Text = IIf(IsDBNull(dbTable.Rows(0)!JABATAN), "", dbTable.Rows(0)!jabatan)
            TlpCP.Text = IIf(IsDBNull(dbTable.Rows(0)!TLPCP), "", dbTable.Rows(0)!tlpcp)
        End If
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        If TabControl1.TabCount = 1 Then
            TabControl1.TabPages.Insert(1, TabPage2)
        End If
        TabControl1.SelectedTab = TabPage2
        Nama.Focus()
        ClearTextBoxes()
        IDRec.ReadOnly = False
        IDRec.Focus()
        'SQL = "Select CompCode " &
        '    " From M_Company "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count <> 0 Then
        '    IDRec.Text = dbTable.Rows(0) !compcode
        'End If
    End Sub

    Private Sub Form_MCustomer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'cektable()
        With Me.DGView.RowTemplate
            .Height = 30
            .MinimumHeight = 24
        End With
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Data_Record()
        AturTombol(True)
        TabControl1.TabPages.RemoveAt(1)
    End Sub

    Private Sub cektable()
        Dim nStock = "stock" + IDRec.Text
        SQL = "select column_name
          from INFORMATION_SCHEMA.columns
         where table_name = 'm_barang'
           and column_name = '" & nStock & "'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "ALTER TABLE m_barang ADD " &
                "" & nStock & " int "
            Proses.ExecuteNonQuery(SQL)
            SQL = "Update m_barang set " & nStock & " = 0"
            Proses.ExecuteNonQuery(SQL)
        End If
    End Sub
    Private Sub CektableName()
        'SQL = "SELECT *  FROM information_schema.COLUMNS " &
        '     "WHERE TABLE_NAME = 'm_toko'  "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "CREATE TABLE [dbo].[m_toko] ( " &
        '        "[IdRec] [varchar](7) NULL," &
        '        "[Nama] [varchar](100) NULL," &
        '        "[Alamat1] [varchar](255) NULL, " &
        '        "[Alamat2] [varchar](255) NULL," &
        '        "[Kota] [varchar](100) NULL," &
        '        "[Prov] [varchar](100) NULL," &
        '        "[KodePos] [varchar](50) NULL, " &
        '        "[Phone] [varchar](100) NULL," &
        '        "[Email] [varchar](100) NULL," &
        '        "[ContactPerson] [varchar](100) NULL," &
        '        "[tlpCP] [varchar](50) NULL," &
        '        "[Jabatan] [varchar](100) NULL," &
        '        "[AktifYN] [char](1) NULL," &
        '        "[UserID] [varchar](20) NULL," &
        '        "[LastUPD] [datetime] NULL) "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
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
        cmdSimpan.Enabled = Not tAktif
        cmdBatal.Enabled = Not tAktif
        cmdExit.Enabled = tAktif
    End Sub


    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click

        If Trim(Nama.Text) = "" Then
            MsgBox("Nama TOKO tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning!")
            Nama.Focus()
            Exit Sub
        End If
        If Trim(Alamat1.Text) = "" Then
            MsgBox("Alamat TOKO tidak boleh kosong !", vbCritical + vbOKOnly, ".:Warning!")
            Nama.Focus()
            Exit Sub
        End If
        If Trim(IDRec.Text) = "" Then
            MsgBox("Kode Toko masih kosong !" & dbTable.Rows(0)!nama, vbInformation + vbOKOnly, ".:Data tidak dapat di simpan !!")
            IDRec.Focus()
            Exit Sub
        End If
        If Len(Trim(IDRec.Text)) <> 5 Then
            MsgBox("Panjang karakter kode toko harus 5 karakter ! " & vbCrLf &
                   "Terdiri dari 2 Huruf & 3 Angka ", vbCritical + vbOKOnly, ".:WARNING!")
            IDRec.Focus()
            Exit Sub
        End If
        If LAdd Then

            SQL = "Select Nama From M_Toko Where Nama = '" & Nama.Text & "' and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Nama.Focus()
                MsgBox("Nama Toko " & Nama.Text & " Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            Else
                'tKode = UCase(Replace(Replace(Replace(Nama.Text, " ", ""), ".", ""), "-", ""))
                'IDRec.Text = Microsoft.VisualBasic.Left(IDRec.Text, 2)
                SQL = "Select idrec, nama " &
                    " From M_Toko " &
                    "Where IDRec = '" & Trim(IDRec.Text) & "' " &
                    "  And aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    MsgBox("Kode Toko " & IDRec.Text & " sudah di gunakan toko : " & dbTable.Rows(0)!nama, vbInformation + vbOKOnly, ".:Data tidak dapat di simpan !!")
                    Exit Sub
                    'If IsDBNull(dbTable.Rows(0) !tid) Then
                    '    IDRec.Text = IDRec.Text + "001"
                    'Else
                    '    IDRec.Text = IDRec.Text +
                    '            Microsoft.VisualBasic.Right(dbTable.Rows(0) !TID, 3)
                    'End If
                End If
            End If

            SQL = "Insert into M_Toko (IdRec, nama, alamat1, alamat2, Kota, Prov, " &
                "KodePos, Phone, Email, ContactPerson, Jabatan, TlpCP, AktifYN, UserID,  " &
                "LastUPD) values ('" & IDRec.Text & "', '" & Nama.Text & "', " &
                " '" & Alamat1.Text & "', '" & Alamat2.Text & "', '" & Kota.Text & "', " &
                " '" & Prov.Text & "', '" & KodePos.Text & "', '" & Phone.Text & "', " &
                " '" & Email.Text & "', '" & ContactPerson.Text & "', '" & Jabatan.Text & "', " &
                " '" & TlpCP.Text & "', 'Y' , '" & UserID & "', GetDate() ) "
            Proses.ExecuteNonQuery(SQL)
            'If MsgBox("Data berhasil disimpan, mau tambah data lagi?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            '    TabControl1.SelectedTab = TabPage2
            '    tKode = Microsoft.VisualBasic.Left(IDRec.Text, 2)
            '    ClearTextBoxes()
            '    IDRec.Text = tKode
            '    Nama.Focus()
            'Else
            LAdd = False
            LEdit = False
            AturTombol(True)
            TabControl1.TabPages.RemoveAt(1)
            TabControl1.SelectedTab = TabPage1
            'End If
            cektable()
        ElseIf LEdit Then
            SQL = "Update M_Toko set nama = '" & Nama.Text & "', " &
                    "alamat1 = '" & Alamat1.Text & "', " &
                    "alamat2 = '" & Alamat2.Text & "', " &
                    "   Kota = '" & Kota.Text & "', " &
                    "   Prov = '" & Prov.Text & "', " &
                    "KodePos = '" & KodePos.Text & "', " &
                    "  Phone = '" & Phone.Text & "', " &
                    "  Email = '" & Email.Text & "', " &
                    "ContactPerson = '" & ContactPerson.Text & "', " &
                    " Jabatan = '" & Jabatan.Text & "', " &
                    "   tlpcp = '" & TlpCP.Text & "', " &
                    "   UserID = '" & UserID & "', " &
                    "  LastUPD = GetDate() " &
                    "where IdRec = '" & IDRec.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            LAdd = False
            LEdit = False
            AturTombol(True)
            TabControl1.TabPages.RemoveAt(1)
            TabControl1.SelectedTab = TabPage1
        End If
        Call Data_Record()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
        TabControl1.TabPages.RemoveAt(1)
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub Nama_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Nama.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Alamat1.Focus()
        End If
    End Sub

    Private Sub Alamat1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Alamat1.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Alamat2.Focus()
    End Sub

    Private Sub Alamat2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Alamat2.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Kota.Focus()
    End Sub

    Private Sub Kota_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Kota.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Prov.Focus()
    End Sub

    Private Sub Prov_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Prov.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then KodePos.Focus()
    End Sub

    Private Sub KodePos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles KodePos.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Phone.Focus()
    End Sub

    Private Sub Phone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Phone.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Email.Focus()
    End Sub

    Private Sub ContactPerson_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ContactPerson.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then TlpCP.Focus()
    End Sub

    Private Sub Email_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Email.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then ContactPerson.Focus()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        If Trim(IDRec.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        IDRec.ReadOnly = True
        If TabControl1.TabCount = 1 Then TabControl1.TabPages.Insert(1, TabPage2)
        TabControl1.SelectedTab = TabPage2
        SQL = "select aktifYN from m_toko  " &
            "Where IDRec = '" & IDRec.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If dbTable.Rows(0) !aktifyn = "N" Then
                cmdSimpan.Visible = False
            Else
                cmdSimpan.Visible = True
            End If
        End If
        Nama.Focus()
    End Sub

    Private Sub DGView_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView.CellClick
        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        Isi_Data()
    End Sub

    Private Sub DGView_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGView.KeyUp
        IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
    End Sub

    Private Sub cmdHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHapus.Click
        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        If Trim(IDRec.Text) = "" Then
            MsgBox("ID Data Belum di pilih!", vbCritical, ".:ERROR!")
            TabControl1.SelectedTab = TabPage1
            DGView.Focus()
        End If
        If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update M_Toko Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate() " &
                    "Where IDRec = '" & IDRec.Text & "' "
            Proses.ExecuteNonQuery(SQL)

            Dim nStock = "stock" + IDRec.Text
            SQL = "select column_name
              from INFORMATION_SCHEMA.columns
             where table_name = 'm_barang'
               and column_name = '" & nStock & "'"
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                SQL = "ALTER TABLE m_barang  " &
                "DROP COLUMN " & nStock & " "
                Proses.ExecuteNonQuery(SQL)
            End If

            ClearTextBoxes()
            Data_Record()
        End If
    End Sub

    Private Sub DGView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        cmdEdit_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub IDRec_TextChanged(sender As Object, e As EventArgs) Handles IDRec.TextChanged

    End Sub

    Private Sub tCari_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            Cari_Data()
        End If
    End Sub

    Private Sub TlpCP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TlpCP.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then Jabatan.Focus()
    End Sub

    Private Sub Jabatan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Jabatan.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then cmdSimpan.Focus()
    End Sub

    Private Sub IDRec_KeyPress(sender As Object, e As KeyPressEventArgs) Handles IDRec.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Nama.Focus()
        End If
    End Sub
End Class