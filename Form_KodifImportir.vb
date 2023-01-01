Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Form_KodifImportir
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Dim KodeGLHutangImportir As String = "", KodeGLPiutangImportir As String
    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim mJenis As String = "", mBeliYN As String = ""

        If OptCommercial.Checked = False And OptNGO.Checked = False And OptOther.Checked = False Then
            MsgBox("Jenis Importir masih belum di pilih!", vbCritical, ".:Warning!")
            Exit Sub
        Else
            If OptCommercial.Checked Then
                mJenis = "COMMERCIAL"
            ElseIf OptNGO.Checked Then
                mJenis = "NGO"
            ElseIf OptOther.Checked Then
                mJenis = "OTHER"
            End If
        End If
        If OptY.Checked = False And OptN.Checked = False Then
            MsgBox("Masih membeli atau sudah tidak memebeli, belum di pilih!", vbCritical, ".:Warning!")
            Exit Sub
        Else
            If OptY.Checked = True Then
                mBeliYN = "Y"
            ElseIf OptN.Checked = True Then
                mBeliYN = "N"
            End If
        End If
        NamaImportir.Text = Replace(NamaImportir.Text, "'", "`")
        Alamat.Text = Replace(Alamat.Text, "'", "`")
        AlamatKirim.Text = Replace(AlamatKirim.Text, "'", "`")
        Notify.Text = Replace(Notify.Text, "'", "`")
        PortOFDischarge.Text = Replace(PortOFDischarge.Text, "'", "`")
        Catatan.Text = Replace(Catatan.Text, "'", "`")
        If LAdd Then
            Dim dbCek As New DataTable
            SQL = "Select Nama 
                From m_KodeImportir 
                Where aktifYN='Y'
                  and Nama = '" & Trim(NamaImportir.Text) & "'"
            dbCek = Proses.ExecuteQuery(SQL)
            If dbCek.Rows.Count <> 0 Then
                MsgBox("Nama Importir : " & NamaImportir.Text & " sudah ada !", vbCritical + vbOKOnly, ".:Warning")
                NamaImportir.Focus()
                Exit Sub
            End If
            SQL = "Select * " &
                " From m_KodeImportir " &
                "Where KodeImportir = '" & KodeImportir.Text & "' "
            dbCek = Proses.ExecuteQuery(SQL)
            If dbCek.Rows.Count <> 0 Then
                KodeImportir.Focus()
                MsgBox("Kode Importir " & KodeImportir.Text & " Sudah ADA!", vbCritical + vbOKOnly, "Warning!")
                Exit Sub
            Else
                SQL = "Select isnull(Max(right(KodeImportir,2)),0) + 100001 RecId " &
                    " From m_KodeImportir " &
                    "Where left(KodeImportir,3) = '" & Microsoft.VisualBasic.Right(CmbNegara.Text, 3) & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    KodeImportir.Text = Microsoft.VisualBasic.Right(CmbNegara.Text, 3) +
                                        Microsoft.VisualBasic.Right(dbTable.Rows(0) !recid, 2)
                End If
            End If
            Proses.CloseConn()

            SQL = "INSERT INTO m_KodeImportir(KodeImportir, NegaraAsal, Nama, " &
                "Jenis, Alamat, AlamatKirim, Notify, Port, Catatan, Telepon, Fax, " &
                "Email, ContactPerson, BeliYN, TglMasuk, LastUPD, UserID, AktifYN, " &
                "TransferYN, KodeHutang, KodePiutang) VALUES('" & KodeImportir.Text & "', " &
                "'" & Trim(Microsoft.VisualBasic.Left(CmbNegara.Text, 50)) & "', " &
                "'" & Trim(NamaImportir.Text) & "','" & mJenis & "', '" & Trim(Alamat.Text) & "', " &
                "'" & Trim(AlamatKirim.Text) & "', '" & Trim(Notify.Text) & "', " &
                "'" & Trim(PortOFDischarge.Text) & "', '" & Trim(Catatan.Text) & "'," &
                "'" & Trim(Telepon.Text) & "', '" & Trim(Fax.Text) & "', " &
                "'" & Trim(Email.Text) & "', '" & Trim(ContactPerson.Text) & "', " &
                "'" & mBeliYN & "', '" & Format(tglMasuk.Value, "yyyy-MM-dd") & "', " &
                "GetDate(), '" & UserID & "', 'Y', 'N', '', '' )"
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update m_KodeImportir SET " &
               "NegaraAsal = '" & Trim(Microsoft.VisualBasic.Left(CmbNegara.Text, 50)) & "', " &
               "      Nama = '" & Trim(NamaImportir.Text) & "', " &
               "     Jenis = '" & Trim(mJenis) & "', " &
               "    Alamat = '" & Trim(Alamat.Text) & "', " &
               "AlamatKirim = '" & Trim(AlamatKirim.Text) & "', " &
               "    Notify = '" & Trim(Notify.Text) & "', " &
               "      Port = '" & Trim(PortOFDischarge.Text) & "', " &
               "   Catatan = '" & Trim(Catatan.Text) & "', " &
               "   Telepon = '" & Trim(Telepon.Text) & "', Fax = '" & Trim(Fax.Text) & "' , " &
               "     Email = '" & Trim(Email.Text) & "', ContactPerson = '" & Trim(ContactPerson.Text) & "' , " &
               "    BeliYN = '" & mBeliYN & "', TglMasuk = '" & Format(tglMasuk.Value, "yyyy-MM-dd") & "' , " &
               " TransferYN = 'N', LastUPD = GetDate(), UserID = '" & UserID & "' " &
               "Where KodeImportir = '" & KodeImportir.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If DGView.Rows.Count <> 0 Then
            KodeImportir.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
            Isi_Data()
        Else
            Exit Sub
        End If
        If Trim(KodeImportir.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
    End Sub
    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If DGView.Rows.Count <> 0 Then
            KodeImportir.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
        Else
            Exit Sub
        End If
        If Trim(KodeImportir.Text) = "" Then
            MsgBox("Data Importir Belum di pilih!", vbCritical, ".:ERROR!")
            DGView.Focus()
        End If
        If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update m_KodeImportir Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate() " &
                    "Where KodeImportir = '" & KodeImportir.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            ClearTextBoxes()
            Data_Record()
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        ClearTextBoxes()
        AturTombol(False)
        CmbNegara.Focus()
    End Sub


    Private Sub Isi_Data()
        Dim dbIsi As New DataTable, Ada As Boolean
        Dim mBeliYN As String = "", mJenis As String = "", tNegara As String = ""
        SQL = "Select m_KodeImportir.*  " &
            " From m_KodeImportir " &
            "Where m_KodeImportir.AktifYN = 'Y' " &
            "  and KodeImportir = '" & KodeImportir.Text & "' "
        dbIsi = Proses.ExecuteQuery(SQL)
        If dbIsi.Rows.Count <> 0 Then
            With dbIsi.Columns(0)
                For a = 0 To dbIsi.Rows.Count - 1
                    tNegara = .Table.Rows(a) !NegaraAsal
                    For i = 0 To CmbNegara.Items.Count - 1
                        CmbNegara.SelectedIndex = i
                        If Trim(Microsoft.VisualBasic.Left(CmbNegara.Text, 50)) = Trim(tNegara) Then
                            Ada = True
                            Exit For
                        End If
                    Next i
                    If Not Ada Then CmbNegara.SelectedIndex = -1
                    mJenis = .Table.Rows(a) !Jenis
                    If mJenis = "COMMERCIAL" Then
                        OptCommercial.Checked = True
                        OptNGO.Checked = False
                        OptOther.Checked = False
                    ElseIf mJenis = "NGO" Then
                        OptCommercial.Checked = False
                        OptNGO.Checked = True
                        OptOther.Checked = False
                    ElseIf mJenis = "OTHER" Then
                        OptCommercial.Checked = False
                        OptNGO.Checked = False
                        OptOther.Checked = True
                    End If
                    mBeliYN = .Table.Rows(a) !BeliYN
                    If mBeliYN = "Y" Then
                        OptY.Checked = True
                        OptN.Checked = False
                    Else
                        OptY.Checked = False
                        OptN.Checked = True
                    End If
                    tglMasuk.Value = .Table.Rows(a) !tglMasuk
                    lastUpd.Text = Format(.Table.Rows(a) !lastupd, "dd-MM-yy HH:mm")
                    NamaImportir.Text = .Table.Rows(a) !Nama
                    KodeImportir.Text = .Table.Rows(a) !KodeImportir
                    Alamat.Text = .Table.Rows(a) !Alamat
                    AlamatKirim.Text = .Table.Rows(a) !AlamatKirim
                    Notify.Text = .Table.Rows(a) !Notify
                    PortOFDischarge.Text = .Table.Rows(a) !Port
                    Catatan.Text = .Table.Rows(a) !Catatan
                    Telepon.Text = .Table.Rows(a) !Telepon
                    Fax.Text = .Table.Rows(a) !fax
                    Email.Text = .Table.Rows(a) !Email
                    ContactPerson.Text = .Table.Rows(a) !ContactPerson
                Next
            End With
        End If
        Proses.CloseConn()
    End Sub

    Private Sub CmbNegara_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNegara.SelectedIndexChanged
        If LAdd Or LEdit Then
            If CmbNegara.Text <> "" Then
                MaxKodeImportir()
                NamaImportir.Focus()
            End If
        Else

        End If
    End Sub
    Private Sub MaxKodeImportir()
        If LAdd Then
            Dim MsgSQL As String, RsMax As New DataTable
            MsgSQL = "Select isnull(Max(right(KodeImportir,2)),0) + 100001 RecId " &
                " From m_KodeImportir " &
                "Where left(KodeImportir,3) = '" & Microsoft.VisualBasic.Right(CmbNegara.Text, 3) & "' "
            RsMax = Proses.ExecuteQuery(MsgSQL)
            If RsMax.Rows.Count <> 0 Then
                KodeImportir.Text = Microsoft.VisualBasic.Right(CmbNegara.Text, 3) +
                                    Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 2)
            End If
            Proses.CloseConn()
        End If
    End Sub
    Private Sub Data_Record()
        Dim mKondisi As String = ""
        btnCari.Enabled = False
        If Trim(tCari.Text) <> "" Then
            mKondisi = " And Nama Like '" & Trim(tCari.Text) & "%' "
        End If
        'Me.Cursor = Cursors.WaitCursor
        DGView.Visible = False
        SQL = "Select m_KodeImportir.*  " &
            " From m_KodeImportir " &
            "Where m_KodeImportir.AktifYN = 'Y' " & mKondisi & "  " &
            "Order By Nama "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                DGView.Rows.Add(.Table.Rows(a) !Nama,
                    .Table.Rows(a) !KodeImportir, .Table.Rows(a) !NegaraAsal,
                    .Table.Rows(a) !Jenis, .Table.Rows(a) !Alamat,
                    .Table.Rows(a) !AlamatKirim, .Table.Rows(a) !Notify,
                    .Table.Rows(a) !Port, .Table.Rows(a) !Catatan,
                    .Table.Rows(a) !Telepon, .Table.Rows(a) !fax,
                    .Table.Rows(a) !Email, .Table.Rows(a) !ContactPerson,
                    .Table.Rows(a) !BeliYN,
                    Format(.Table.Rows(a) !tglMasuk, "dd-MM-yyyy"),
                    Format(.Table.Rows(a) !LastUPD, "dd-MM-yyyy"))
            Next (a)
        End With
        Me.Cursor = Cursors.Default
        DGView.Visible = True
        btnCari.Enabled = True
    End Sub

    Private Sub KodeImportir_TextChanged(sender As Object, e As EventArgs) Handles KodeImportir.TextChanged

    End Sub

    Private Sub Form_KodifImportir_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String
        Dim tNegara As String = ""
        KodeImportir.ReadOnly = True
        lastUpd.ReadOnly = True
        MsgSQL = "Select GetDate() tgl, NamaIndonesia, Benua, KodeNegara " &
            "From M_KodeNegara Where AktifYN = 'Y' order by NamaIndonesia "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        DGView.Rows.Clear()
        CmbNegara.Items.Clear()
        Me.Cursor = Cursors.WaitCursor
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                tNegara = Microsoft.VisualBasic.Left(.Table.Rows(a) !NamaIndonesia & Space(50), 50) &
                    .Table.Rows(a) !Benua &
                    .Table.Rows(a) !KodeNegara
                CmbNegara.Items.Add(tNegara)
            Next (a)
        End With
        tglMasuk.Value = Now()
        lastUpd.Text = ""

        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        Me.Cursor = Cursors.Default

        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Data_Record()

        tTambah = Proses.UserAksesTombol(UserID, "42_KODIF_IMPORTIR", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "42_KODIF_IMPORTIR", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "42_KODIF_IMPORTIR", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "42_KODIF_IMPORTIR", "laporan")
        AturTombol(True)
        cmdSimpan.Visible = False
        'KodeGLHutangImportir = Space(255)
        'KodeGLPiutangImportir = Space(255)
        'i = GetPrivateProfileString("MYCONFIG", "KodeGLHutangImportir", "", KodeGLHutangImportir, 255, App.Path & "\MyConfig.ini")
        'i = GetPrivateProfileString("MYCONFIG", "KodeGLPiutangImportir", "", KodeGLPiutangImportir, 255, App.Path & "\MyConfig.ini")
        'KodeGLHutangImportir = Left(Trim(KodeGLHutangImportir), Len(Trim(KodeGLHutangImportir)) - 1)
        'KodeGLPiutangImportir = Left(KodeGLPiutangImportir, Len(Trim(KodeGLPiutangImportir)) - 1)
        KodeGLHutangImportir = My.Settings.KodeGLHutangImportir
        KodeGLPiutangImportir = My.Settings.KodeGLPiutangImportir

    End Sub

    Private Sub NamaImportir_TextChanged(sender As Object, e As EventArgs) Handles NamaImportir.TextChanged

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
        CmbNegara.SelectedIndex = -1
    End Sub

    Private Sub Telepon_TextChanged(sender As Object, e As EventArgs) Handles Telepon.TextChanged

    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Enabled = False
        Else
            cmdTambah.Enabled = tAktif
        End If
        If tEdit = False Then
            cmdEdit.Enabled = False
        Else
            cmdEdit.Enabled = tAktif
            cmdSimpan.Visible = Not tAktif
        End If
        If tHapus = False Then
            cmdHapus.Enabled = False
        Else
            cmdHapus.Enabled = tAktif
        End If
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif
    End Sub

    Private Sub Fax_TextChanged(sender As Object, e As EventArgs) Handles Fax.TextChanged

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim tID As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
        KodeImportir.Text = tID
        If LEdit Or LAdd Then

        Else
            Isi_Data()
        End If
    End Sub

    Private Sub ContactPerson_TextChanged(sender As Object, e As EventArgs) Handles ContactPerson.TextChanged

    End Sub

    Private Sub DGView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        TabControl1.SelectedTab = TabPageFormEntry_
    End Sub

    Private Sub Alamat_TextChanged(sender As Object, e As EventArgs) Handles Alamat.TextChanged

    End Sub

    Private Sub PanelJenis_Paint(sender As Object, e As PaintEventArgs) Handles PanelJenis.Paint

    End Sub

    Private Sub CmbNegara_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbNegara.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaImportir.Focus()
        End If
    End Sub

    Private Sub KodeImportir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeImportir.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaImportir.Focus()
        End If
    End Sub

    Private Sub AlamatKirim_TextChanged(sender As Object, e As EventArgs) Handles AlamatKirim.TextChanged

    End Sub

    Private Sub NamaImportir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaImportir.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            PanelBeli.Focus()
        End If
    End Sub

    Private Sub Telepon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Telepon.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Fax.Focus()
        End If
    End Sub

    Private Sub Fax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Fax.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Email.Focus()
        End If
    End Sub

    Private Sub ContactPerson_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ContactPerson.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            PanelBeli.Focus()
        End If
    End Sub

    Private Sub Email_TextChanged(sender As Object, e As EventArgs) Handles Email.TextChanged

    End Sub

    Private Sub PanelJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PanelJenis.KeyPress
        If e.KeyChar = Chr(13) Then
            Alamat.Focus()
        End If
    End Sub

    Private Sub PanelBeli_Paint(sender As Object, e As PaintEventArgs) Handles PanelBeli.Paint

    End Sub

    Private Sub Alamat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Alamat.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            AlamatKirim.Focus()
        End If
    End Sub

    Private Sub tglMasuk_ValueChanged(sender As Object, e As EventArgs) Handles tglMasuk.ValueChanged

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub AlamatKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AlamatKirim.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Notify.Focus()
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Data_Record()
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub Notify_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Notify.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            PortOFDischarge.Focus()
        End If
    End Sub

    Private Sub PortOFDischarge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PortOFDischarge.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Catatan.Focus()
        End If
    End Sub

    Private Sub Catatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Catatan.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Telepon.Focus()
        End If
    End Sub

    Private Sub Email_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Email.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            ContactPerson.Focus()
        End If
    End Sub

    Private Sub PanelBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PanelBeli.KeyPress
        If e.KeyChar = Chr(13) Then
            tglMasuk.Focus()
        End If
    End Sub

    Private Sub tglMasuk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglMasuk.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub

    Private Sub CmbNegara_Click(sender As Object, e As EventArgs) Handles CmbNegara.Click
        If LAdd Or LEdit Then
            If CmbNegara.Text <> "" Then
                MaxKodeImportir()
            End If
        Else

        End If
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            If Trim(tCari.Text) <> "" Then Data_Record()
        End If
    End Sub
End Class