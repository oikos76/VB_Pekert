Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Form_KodifPerajin
    Dim kodeGLHutangPerajin As String = "",
        KodeGLPiutangPerajin As String = ""
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

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If DGView.Rows.Count <> 0 Then
            KodePerajin.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
        Else
            Exit Sub
        End If
        If Trim(KodePerajin.Text) = "" Then
            MsgBox("Data Perajin Belum di pilih!", vbCritical, ".:ERROR!")
            DGView.Focus()
        End If
        If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update M_KodePerajin Set AktifYN = 'N', UserID = '" & UserID & "', LastUPD = GetDate() " &
                    "Where KodePerajin  = '" & KodePerajin.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            ClearTextBoxes()
            Data_Record()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim mJenis As String = "", mProduksiYN As String = ""

        If chkProduksi.Checked = True Then
            mProduksiYN = "Y"
        ElseIf chkProduksi.Checked = True Then
            mProduksiYN = "N"
        End If

        NamaPerajin.Text = Replace(NamaPerajin.Text, "'", "`")
        Alamat.Text = Replace(Alamat.Text, "'", "`")
        Catatan.Text = Replace(Catatan.Text, "'", "`")

        If LAdd Then
            Dim dbCek As New DataTable
            SQL = "Select Nama 
                From M_KodePerajin 
                Where aktifYN='Y'
                  and Nama = '" & Trim(NamaPerajin.Text) & "'"
            dbCek = Proses.ExecuteQuery(SQL)
            If dbCek.Rows.Count <> 0 Then
                MsgBox("Nama Perajin : " & NamaPerajin.Text & " sudah ada !", vbCritical + vbOKOnly, ".:Warning")
                NamaPerajin.Focus()
                Exit Sub
            End If
            SQL = "Select * " &
                " From M_KodePerajin " &
                "Where KodePerajin = '" & KodePerajin.Text & "' "
            dbCek = Proses.ExecuteQuery(SQL)
            If dbCek.Rows.Count <> 0 Then
                KodePerajin.Focus()
                MsgBox("Kode Perajin " & KodePerajin.Text & " Sudah ADA!", vbCritical + vbOKOnly, "Warning!")
                Exit Sub
            End If
            Proses.CloseConn()

            SQL = "INSERT INTO m_KodePerajin (WilayahProduksi, KodePerajin, " &
                "Nama, Alamat, Email, Telepon, Fax, Bank, Rekening, TahunMasuk, " &
                "ProduksiYN, Catatan, LastUPD, UserID, AktifYN, TransferYN, " &
                "KodeHutang, KodePiutang) VALUES ( " &
                "'" & Trim(Microsoft.VisualBasic.Left(CmbWilayah.Text, 100)) & "', " &
                "'" & KodePerajin.Text & "', " &
                "'" & NamaPerajin.Text & "', '" & Alamat.Text & "', " &
                "'" & Email.Text & "', '" & Telepon.Text & "', '" & Fax.Text & "', " &
                "'" & Bank.Text & "', '" & Rekening.Text & "', " &
                "'" & Format(tglMasuk.Value, "yyyy-MM-dd") & "', " &
                "'" & mProduksiYN & "', '" & Trim(Catatan.Text) & "', " &
                "GetDate(), '" & UserID & "', 'Y', 'N', '" & kodeGLHutangPerajin & "', " &
                "'" & KodeGLPiutangPerajin & "')"
            Proses.ExecuteNonQuery(SQL)
            SQL = "INSERT INTO m_Perkiraan(NO_PERKIRAAN, NO_SUB, " &
               "NM_PERKIRAAN, AKTIFYN, LASTUPD, SAkhir)VALUES( " &
               "'20.10.05.01." & KodePerajin.Text & "', " &
               "'20.10.05.01', 'Hutang Dagang " & Trim(NamaPerajin.Text) & "'," &
               "'Y', GetDate(), 0)"
            Proses.ExecuteNonQuery(SQL)

            SQL = "INSERT INTO m_Perkiraan(NO_PERKIRAAN, NO_SUB, " &
                "NM_PERKIRAAN, AKTIFYN, LASTUPD, SAkhir)VALUES( " &
                "'10.10.15.01." & KodePerajin.Text & "', " &
                "'10.10.15.01', 'Piutang Dagang " & Trim(NamaPerajin.Text) & "'," &
                "'Y', GetDate(), 0)"
            Proses.ExecuteNonQuery(SQL)

            SQL = "INSERT INTO m_Perkiraan(NO_PERKIRAAN, NO_SUB, " &
                "NM_PERKIRAAN, AKTIFYN, LASTUPD, SAkhir)VALUES( " &
                "'50.10.01.01." & KodePerajin.Text & "', " &
                "'50.10.01.01', 'Pembelian dari " & Trim(NamaPerajin.Text) & "'," &
                "'Y', GetDate(), 0)"
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update M_KodePerajin Set " &
                "WilayahProduksi = '" & Trim(Microsoft.VisualBasic.Left(CmbWilayah.Text, 100)) & "', " &
                "   nama = '" & NamaPerajin.Text & "', " &
                " Alamat = '" & Alamat.Text & "', " &
                "  Email = '" & Email.Text & "', " &
                "Telepon = '" & Telepon.Text & "', " &
                "    Fax = '" & Fax.Text & "' , " &
                "   Bank = '" & Bank.Text & "', " &
                "Rekening = '" & Rekening.Text & "', " &
                "TahunMasuk = '" & Format(tglMasuk.Value, "yyyy-MM-dd") & "', " &
                "ProduksiYN = '" & mProduksiYN & "', " &
                "Catatan = '" & Trim(Catatan.Text) & "', " &
                "KodeHutang = '" & kodeGLHutangPerajin & "', " &
                "KodePiutang = '" & KodeGLPiutangPerajin & "', " &
                "TransferYN = 'N', LastUPD = GetDate() , " &
                "UserID = '" & UserID & "' " &
                "Where KodePerajin = '" & KodePerajin.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call Data_Record()
    End Sub

    Private Sub Data_Record()
        Dim mKondisi As String = ""
        btnCari.Enabled = False
        If Trim(tCari.Text) <> "" Then
            mKondisi = " And Nama Like '" & Trim(tCari.Text) & "%' "
        End If
        Me.Cursor = Cursors.WaitCursor
        SQL = "Select A.*, isnull(kodepiutang,'') kode_piutang, isnull(kodehutang,'') kode_hutang " &
            " From m_KodePerajin A " &
            " Where A.AktifYN = 'Y' " & mKondisi & " " &
            " Order By Nama "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        DGView.Visible = False
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                DGView.Rows.Add(.Table.Rows(a) !Nama,
                    .Table.Rows(a) !KodePerajin,
                    .Table.Rows(a) !WilayahProduksi,
                    .Table.Rows(a) !Alamat,
                     .Table.Rows(a) !Email,
                    .Table.Rows(a) !Telepon,
                    .Table.Rows(a) !fax,
                    .Table.Rows(a) !bank,
                    .Table.Rows(a) !rekening,
                    Format(.Table.Rows(a) !TahunMasuk, "dd-MM-yyyy"),
                    .Table.Rows(a) !ProduksiYN,
                    .Table.Rows(a) !Catatan,
                    Format(.Table.Rows(a) !LastUPD, "dd-MM-yyyy"),
                    .Table.Rows(a) !kode_piutang,
                    .Table.Rows(a) !kode_hutang)
            Next (a)
        End With
        DGView.Visible = True
        Me.Cursor = Cursors.Default
        btnCari.Enabled = True
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

    Private Sub CmbWilayah_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbWilayah.SelectedIndexChanged
        If LAdd Or LEdit Then
            If CmbWilayah.Text <> "" Then
                MaxKodePerajin()
                NamaPerajin.Focus()
            End If
        Else

        End If
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

    Private Sub isi_data()
        Dim dbIsi As New DataTable, Ada As Boolean
        Dim mBeliYN As String = "", mJenis As String = "", tWilayah As String = ""
        SQL = "Select m_KodePerajin.*  " &
            " From m_KodePerajin " &
            "Where m_KodePerajin.AktifYN = 'Y' " &
            "  and KodePerajin = '" & KodePerajin.Text & "' "
        dbIsi = Proses.ExecuteQuery(SQL)
        If dbIsi.Rows.Count <> 0 Then
            With dbIsi.Columns(0)
                For a = 0 To dbIsi.Rows.Count - 1
                    tWilayah = .Table.Rows(a) !WilayahProduksi
                    For i = 0 To CmbWilayah.Items.Count - 1
                        CmbWilayah.SelectedIndex = i
                        If Trim(Microsoft.VisualBasic.Left(CmbWilayah.Text, 50)) = Trim(tWilayah) Then
                            Ada = True
                            Exit For
                        End If
                    Next i
                    If Not Ada Then CmbWilayah.SelectedIndex = -1

                    NamaPerajin.Text = .Table.Rows(a) !Nama
                    KodePerajin.Text = .Table.Rows(a) !KodePerajin
                    Alamat.Text = .Table.Rows(a) !Alamat
                    Email.Text = .Table.Rows(a) !Email
                    Telepon.Text = .Table.Rows(a) !Telepon
                    Fax.Text = .Table.Rows(a) !fax
                    Bank.Text = .Table.Rows(a) !bank
                    Rekening.Text = .Table.Rows(a) !rekening
                    tglMasuk.Value = .Table.Rows(a) !TahunMasuk
                    lastUpd.Text = Format(.Table.Rows(a) !lastupd, "dd-MM-yy HH:mm")
                    mBeliYN = .Table.Rows(a) !ProduksiYN
                    If mBeliYN = "Y" Then
                        chkProduksi.Checked = True
                    Else
                        chkProduksi.Checked = False
                    End If
                    Catatan.Text = .Table.Rows(a) !Catatan
                    KodeGLHutang.Text = .Table.Rows(a) !KodeHutang
                    KodeGLPiutang.Text = .Table.Rows(a) !KodePiutang
                Next
            End With
        End If
        Proses.CloseConn()
    End Sub

    Private Sub KodePerajin_TextChanged(sender As Object, e As EventArgs) Handles KodePerajin.TextChanged

    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        ClearTextBoxes()
        CmbWilayah.SelectedIndex = -1
        AturTombol(False)
        KodePerajin.ReadOnly = False
        CmbWilayah.Select()
        CmbWilayah.Focus()
    End Sub

    Private Sub Alamat_TextChanged(sender As Object, e As EventArgs) Handles Alamat.TextChanged

    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If DGView.Rows.Count <> 0 Then
            KodePerajin.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
            isi_data()
        Else
            Exit Sub
        End If
        If Trim(KodePerajin.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        End If
        KodePerajin.ReadOnly = True
        LAdd = False
        LEdit = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
    End Sub

    Private Sub Email_TextChanged(sender As Object, e As EventArgs) Handles Email.TextChanged

    End Sub

    Private Sub Form_KodifPerajin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String
        Dim tWilayah As String = ""
        KodePerajin.ReadOnly = True
        lastUpd.ReadOnly = True
        MsgSQL = "Select * " &
            "From M_KodeWilayah Where AktifYN = 'Y' order by Wilayah "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        DGView.Rows.Clear()
        CmbWilayah.Items.Clear()
        Me.Cursor = Cursors.WaitCursor
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                tWilayah = Microsoft.VisualBasic.Left(.Table.Rows(a) !Wilayah & Space(100), 100) +
                           Microsoft.VisualBasic.Right(Space(5) + .Table.Rows(a) !KodeWilayah, 5)
                CmbWilayah.Items.Add(tWilayah)
            Next (a)
        End With
        tglMasuk.Value = Now()
        lastUpd.Text = ""

        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        Me.Cursor = Cursors.Default
        kodeGLHutangPerajin = My.Settings.KodeGLHutangPerajin
        KodeGLPiutangPerajin = My.Settings.KodeGLPiutangPerajin
        Me.Cursor = Cursors.Default

        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        KodeGLPiutangPerajin = My.Settings.KodeGLPiutangPerajin
        kodeGLHutangPerajin = My.Settings.KodeGLHutangPerajin
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

        tTambah = Proses.UserAksesTombol(UserID, "24_KODIF_PERAJIN", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "24_KODIF_PERAJIN", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "24_KODIF_PERAJIN", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "24_KODIF_PERAJIN", "laporan")
        AturTombol(True)
        cmdSimpan.Visible = False
    End Sub

    Private Sub Telepon_TextChanged(sender As Object, e As EventArgs) Handles Telepon.TextChanged

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim tID As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
        KodePerajin.Text = tID
        If LEdit Or LAdd Then

        Else
            isi_data()
        End If
    End Sub

    Private Sub Fax_TextChanged(sender As Object, e As EventArgs) Handles Fax.TextChanged

    End Sub

    Private Sub DGView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        TabControl1.SelectedTab = TabPageFormEntry_
    End Sub

    Private Sub Bank_TextChanged(sender As Object, e As EventArgs) Handles Bank.TextChanged

    End Sub

    Private Sub CmbWilayah_Click(sender As Object, e As EventArgs) Handles CmbWilayah.Click
        If LAdd Or LEdit Then
            If CmbWilayah.Text <> "" Then
                MaxKodePerajin()
            End If
        Else

        End If
    End Sub

    Private Sub Rekening_TextChanged(sender As Object, e As EventArgs) Handles Rekening.TextChanged

    End Sub

    Private Sub MaxKodePerajin()
        If LAdd Then
            Dim MsgSQL As String, dbMax As New DataTable
            MsgSQL = "Select isnull(Max(Right(KodePerajin,5)),0) + 10001 RecId " &
                " From M_KodePerajin Where KodePerajin <> 'LOKL' " &
                "  and Left(KodePerajin,2) = '" & Microsoft.VisualBasic.Right(CmbWilayah.Text, 2) & "'  "
            dbMax = Proses.ExecuteQuery(MsgSQL)
            If dbMax.Rows.Count <> 0 Then
                KodePerajin.Text = Microsoft.VisualBasic.Right(dbMax.Rows(0) !recid, 4)
            End If
        End If
    End Sub

    Private Sub tglMasuk_ValueChanged(sender As Object, e As EventArgs) Handles tglMasuk.ValueChanged

    End Sub

    Private Sub KodePerajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodePerajin.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaPerajin.Focus()
        End If
    End Sub

    Private Sub chkProduksi_CheckedChanged(sender As Object, e As EventArgs) Handles chkProduksi.CheckedChanged

    End Sub

    Private Sub Alamat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Alamat.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Email.Focus()
        End If
    End Sub

    Private Sub Catatan_TextChanged(sender As Object, e As EventArgs) Handles Catatan.TextChanged

    End Sub

    Private Sub Email_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Email.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Telepon.Focus()
        End If
    End Sub

    Private Sub NamaPerajin_TextChanged(sender As Object, e As EventArgs) Handles NamaPerajin.TextChanged

    End Sub

    Private Sub Telepon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Telepon.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Fax.Focus()
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Data_Record()
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub Fax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Fax.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Bank.Focus()
        End If
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Bank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bank.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Rekening.Focus()
        End If
    End Sub

    Private Sub Rekening_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Rekening.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            tglMasuk.Focus()
        End If
    End Sub

    Private Sub tglMasuk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglMasuk.KeyPress
        If e.KeyChar = Chr(13) Then
            chkProduksi.Focus()
        End If
    End Sub

    Private Sub chkProduksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chkProduksi.KeyPress
        If e.KeyChar = Chr(13) Then
            Catatan.Focus()
        End If
    End Sub

    Private Sub Catatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Catatan.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub

    Private Sub NamaPerajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaPerajin.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Alamat.Focus()
        End If
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            If Trim(tCari.Text) <> "" Then Data_Record()
        End If
    End Sub
End Class