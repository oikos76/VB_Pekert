Imports System.Drawing.Printing

Public Class Form_CompanySetup
    Dim SQL As String, oCompCode As String = ""
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Public Property PrinterSettings As PrinterSettings
    Public Property PaperSize As PaperSize


    Sub Data_Record()
        SQL = "Select * " &
            " From M_Company "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaPerusahaan.Text = dbTable.Rows(0) !company
            oCompCode = dbTable.Rows(0) !compcode
            Alamat.Text = dbTable.Rows(0) !alamat
            Kota.Text = dbTable.Rows(0) !KOTA
            Telepon.Text = IIf(IsDBNull(dbTable.Rows(0) !Telepon), "", dbTable.Rows(0) !Telepon)
            Email.Text = IIf(IsDBNull(dbTable.Rows(0) !Email), "", dbTable.Rows(0) !Email)
            Contact.Text = dbTable.Rows(0) !Contact
            NPWP.Text = dbTable.Rows(0) !NPWP
            BagianRetur.Text = dbTable.Rows(0) !retur
            KoodinatorLHP.Text = dbTable.Rows(0) !KoordinatorLHP
            KoodinatorPraLHP.Text = dbTable.Rows(0) !KoordinatorPRALHP
            Pemeriksa.Text = dbTable.Rows(0) !pemeriksa
            Direksi.Text = dbTable.Rows(0) !direksi
            kode_toko.Text = dbTable.Rows(0) !kode_toko
            NamaFolder.Text = dbTable.Rows(0) !fotoloc
        Else
            NamaPerusahaan.Text = ""
            oCompCode = ""
            Alamat.Text = ""
            Kota.Text = ""
            Telepon.Text = ""
            Email.Text = ""
            Contact.Text = ""
            NPWP.Text = ""
            BagianRetur.Text = ""
            KoodinatorLHP.Text = ""
            KoodinatorPraLHP.Text = ""
            Pemeriksa.Text = ""
            Direksi.Text = ""
            kode_toko.Text = ""
            NamaFolder.Text = ""
        End If
        If Trim(kode_toko.Text) = "" Then kode_toko.Text = "PKT01"
        SQL = "Select * " &
            " From M_Toko " &
            "Where IdRec = '" & kode_toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaToko.Text = dbTable.Rows(0) !nama
        End If
    End Sub

    Private Sub Form_CompanySetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cektable()
        Data_Record()
        SetToolTip()
    End Sub
    Private Sub SetToolTip()
        With Me.ToolTip1
            .AutomaticDelay = 0
            .AutoPopDelay = 30000
            .BackColor = System.Drawing.Color.AntiqueWhite
            .InitialDelay = 50
            .IsBalloon = False
            .ReshowDelay = 50
            .ShowAlways = True
            .Active = False
            .Active = True
            .SetToolTip(Me.Kota, "Kode Perusahaan : " & vbCrLf & "~ PKT01 -> Pusat" & vbCrLf & "~ PKT02 -> Gudang")
        End With
    End Sub

    Private Sub cektable()
        'SQL = "SELECT *  FROM information_schema.COLUMNS " &
        '     "WHERE TABLE_NAME = 'm_company'  " &
        '     "  And column_name = 'bank' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "ALTER TABLE m_company ADD
        '        bank varchar(30),
        '        norekening varchar(50),
        '        atasnama varchar(100) "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
    End Sub
    Private Function CekIP(ByRef xIP As String) As Boolean
        Dim ip() As Net.IPAddress = System.Net.Dns.GetHostAddresses("")
        Dim result As Boolean = False
        If ip.Count > 0 Then
            For Each ipadd As Net.IPAddress In ip
                'Console.WriteLine(ipadd.ToString)
                If ipadd.ToString = Trim(xIP) Then
                    result = True
                    Exit For
                End If
            Next
        End If
        CekIP = result
    End Function

    Private Sub cmdBatalSeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatalSeting.Click
        Me.Close()
    End Sub

    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click
        NamaPerusahaan.Text = Replace(NamaPerusahaan.Text, "'", "`")
        Alamat.Text = Replace(Alamat.Text, "'", "`")
        KoodinatorPraLHP.Text = Replace(KoodinatorPraLHP.Text, "'", "`")
        KoodinatorLHP.Text = Replace(KoodinatorLHP.Text, "'", "`")
        Pemeriksa.Text = Replace(Pemeriksa.Text, "'", "`")
        If (Trim(Kota.Text) = "") Or Len(Kota.Text) < 2 Then
            MsgBox("Panjang Kode Perusahaan Harus 2 Character", vbCritical, ".:Warning")
            Exit Sub
        End If
        If Trim(kode_toko.Text) = "" Then
            MsgBox("Toko yang di aktifkan belum di pilih !", vbCritical + vbOKOnly, ".:Warning!")
            kode_toko.Focus()
            Exit Sub
        End If

        SQL = "Select * " &
            " From M_Company " &
            "Where compcode = '" & Trim(oCompCode) & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            'update 
            SQL = "Update M_Company Set Nama='" & NamaPerusahaan.Text & "', " &
                "  Company = '" & NamaPerusahaan.Text & "', " &
                "   Alamat = '" & Alamat.Text & "', " &
                "     Kota = '" & Kota.Text & "', " &
                "  Telepon = '" & Trim(Telepon.Text) & "',  " &
                "    EMail = '" & Trim(Email.Text) & "', " &
                "     NPWP = '" & Trim(NPWP.Text) & "', " &
                "  Contact = '" & Contact.Text & "', " &
                "     Retur = '" & BagianRetur.Text & "', " &
                "    KoordinatorLHP = '" & KoodinatorLHP.Text & "', " &
                " KoordinatorPraLHP = '" & KoodinatorPraLHP.Text & "', " &
                "Pemeriksa = '" & Pemeriksa.Text & "', " &
                "  Direksi = '" & Direksi.Text & "', " &
                "kode_toko = '" & kode_toko.Text & "',  " &
                "  FotoLoc = '" & NamaFolder.Text & "'  " &
                " Where CompCode = '" & oCompCode & "' "
        End If
        Proses.ExecuteNonQuery(SQL)
        My.Settings.path_foto = NamaFolder.Text
        FrmMenuUtama.Kode_Toko.Text = kode_toko.Text
        My.Settings.Save()
        SQL = "Select * " &
                " From M_Toko " &
                "Where IdRec = '" & kode_toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            FrmMenuUtama.GantiToko(kode_toko.Text, dbTable.Rows(0)!nama, NamaPerusahaan.Text)
        End If
        Application.DoEvents()
        MsgBox("Data berhasil di simpan!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Success 4 u!")
        Me.Close()
    End Sub

    Private Sub NamaPerusahaan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NamaPerusahaan.KeyPress
        If e.KeyChar = Chr(13) Then Alamat.Focus()
    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub BtnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBrowse.Click
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
            Contact.Focus()
        Else
            NamaToko.Text = ""
            kode_toko.Text = ""
        End If

    End Sub

    Private Sub compCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Kota.KeyPress
        'e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then Telepon.Focus()
    End Sub

    Private Sub website_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Email.KeyPress
        If e.KeyChar = Chr(13) Then Contact.Focus()
    End Sub

    Private Sub Bank_TextChanged(sender As Object, e As EventArgs) Handles Contact.TextChanged

    End Sub

    Private Sub Bank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Contact.KeyPress
        'e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then NPWP.Focus()
    End Sub


    Private Sub npwp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NPWP.KeyPress
        'e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then BagianRetur.Focus()
    End Sub

    Private Sub BagianRetur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BagianRetur.KeyPress
        'e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then KoodinatorPraLHP.Focus()
    End Sub


    Private Sub KoodinatorPraLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KoodinatorPraLHP.KeyPress
        ' e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then KoodinatorLHP.Focus()
    End Sub


    Private Sub KoodinatorLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KoodinatorLHP.KeyPress
        'e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then Pemeriksa.Focus()
    End Sub

    Private Sub Footer3_TextChanged(sender As Object, e As EventArgs) Handles Pemeriksa.TextChanged

    End Sub

    Private Sub pemeriksa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pemeriksa.KeyPress
        'e.KeyChar = UCase((e.KeyChar))
        If e.KeyChar = Chr(13) Then Direksi.Focus()
    End Sub


    'Private Sub idComputer_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    e.KeyChar = UCase((e.KeyChar))
    '    If e.KeyChar = Chr(13) Then cmbYesNo.Focus()
    'End Sub

    Private Sub kode_toko_TextChanged(sender As Object, e As EventArgs) Handles kode_toko.TextChanged
        If Len(Trim(kode_toko.Text)) Then
            NamaToko.Text = ""
        End If
    End Sub

    Private Sub kode_toko_KeyPress(sender As Object, e As KeyPressEventArgs) Handles kode_toko.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select * From m_Toko " &
                "Where idrec = '" & kode_toko.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                kode_toko.Text = dbTable.Rows(0)!idrec
                NamaToko.Text = dbTable.Rows(0)!nama
                cmdSimpan.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                     " From m_Toko " &
                     "Where AktifYN = 'Y' " &
                     "  And nama Like '%" & kode_toko.Text & "%' " &
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
                    kode_toko.Text = dbTable.Rows(0)!idrec
                    NamaToko.Text = dbTable.Rows(0)!nama
                    cmdSimpan.Focus()
                Else
                    NamaToko.Text = ""
                    kode_toko.Text = ""
                    kode_toko.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub BagianRetur_TextChanged(sender As Object, e As EventArgs) Handles BagianRetur.TextChanged

    End Sub

    Private Sub NamaPerusahaan_TextChanged(sender As Object, e As EventArgs) Handles NamaPerusahaan.TextChanged

    End Sub

    Private Sub Alamat_TextChanged(sender As Object, e As EventArgs) Handles Alamat.TextChanged

    End Sub

    Private Sub kode_toko_DoubleClick(sender As Object, e As EventArgs) Handles kode_toko.DoubleClick
        kode_toko.Text = ""
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub Alamat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Alamat.KeyPress
        If e.KeyChar = Chr(13) Then Kota.Focus()
    End Sub

    Private Sub Telepon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Telepon.KeyPress
        If e.KeyChar = Chr(13) Then Email.Focus()
    End Sub

    Private Sub cariFile_Click(sender As Object, e As EventArgs) Handles cariFile.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            NamaFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Direksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Direksi.KeyPress
        If e.KeyChar = Chr(13) Then kode_toko.Focus()
    End Sub
End Class