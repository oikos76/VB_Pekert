Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_PO
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter

    Private Sub cmbMataUang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMataUang.SelectedIndexChanged
        MataUang.Text = cmbMataUang.Text
        Label11.Visible = True
        PembagiEuro.Visible = True
        If cmbMataUang.Text = "EURO" Then
            Label_11.Text = "Pembagi Euro : "
            If Trim(PembagiEuro.Text) = "" Then PembagiEuro.Text = 1.375
            If (PembagiEuro.Text * 1) = 0 Then PembagiEuro.Text = 1.375
        ElseIf cmbMataUang.Text = "RP" Then
            Label_11.Text = "Pengali Rupiah : "
            If Trim(PembagiEuro.Text) = "" Then PembagiEuro.Text = 9000
            If (PembagiEuro.Text * 1) = 0 Then PembagiEuro.Text = 9000
        Else
            PembagiEuro.Text = 0
            Label11.Visible = False
            PembagiEuro.Visible = False
        End If
    End Sub

    Protected Ds As DataSet

    Private Sub Jumlah_TextChanged(sender As Object, e As EventArgs) Handles Jumlah.TextChanged
        If Trim(Jumlah.Text) = "" Then Jumlah.Text = 0
        If IsNumeric(Jumlah.Text) Then
            Dim temp As Double = Jumlah.Text
            Jumlah.SelectionStart = Jumlah.TextLength
        Else
            Jumlah.Text = 0
        End If
    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged
        If Len(KodeProduk.Text) < 1 Then
            KodeProduk.Text = ""
            Produk.Text = ""
            FOBBuyer.Text = "0"
            FOBUmum.Text = "0"
            CatatanPO.Text = ""
            Perajin.Text = ""
            Kode_Perajin.Text = ""
            PesanKode.Text = ""
            FOBTerakhir.Text = ""
            Kode_Buyer.Text = ""
            ShowFoto("")
        ElseIf Len(KodeProduk.Text) = 4 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        ElseIf Len(KodeProduk.Text) = 7 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        End If
    End Sub

    Private Sub PembagiEuro_TextChanged(sender As Object, e As EventArgs) Handles PembagiEuro.TextChanged

    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim TMataUang As String = "", RSL As New DataTable
        If LAdd Or LEdit Or LTambahKode Then
            If LocGmb1.Text <> "" Then
                LocGmb1.Text = Dir(LocGmb1.Text)
            End If
            If Kode_Importir.Text = "" Then
                MsgBox("Importir tidak boleh kosong!", vbCritical, ".:ERROR!")
                Kode_Importir.Focus()
                Exit Sub
            End If
            If Trim(Nopo.Text) = "" Then
                MsgBox("No PO Tidak Boleh Kosong!", vbCritical + vbOKOnly, ".:Kolom tidak boleh Kosong!")
                Nopo.Focus()
                Exit Sub
            End If
            If Trim(cmbMataUang.Text) = "" Then
                MsgBox("Mata Uang belunm di pilh!", vbCritical + vbOKOnly, ".:Empty Currency!")
                cmbMataUang.Focus()
                Exit Sub
            End If
            If LAdd Or LTambahKode Then
                MsgSQL = "Select Kode_produk From T_PO " &
                    "Where aktifYN = 'Y' " &
                    "  And noPo = '" & Nopo.Text & "' " &
                    "  And kode_produk ='" & KodeProduk.Text & "' "
                RSL = Proses.ExecuteQuery(MsgSQL)
                If RSL.Rows.Count <> 0 Then
                    MsgBox("Maaf, pengisian kode ganda dalam PO yang sama tidak di ijinkan", vbCritical, "Double Cek!")
                    KodeProduk.Focus()
                    Jumlah.Text = 0
                    Kode_Buyer.Text = ""
                    KodeProduk.Text = ""
                    PesanKode.Text = ""
                    FOBTerakhir.Text = ""
                    ShowFoto("")
                    Exit Sub
                End If
                idRecord.Text = Proses.MaxNoUrut("IDRec", "t_PO", "PO")
                If Trim(Jumlah.Text) = "" Then
                    MsgBox("Jumlah tidak boleh kosong!", vbCritical, ".:ERROR!")
                    Jumlah.Focus()
                    Exit Sub
                End If
                If Trim(FOBBuyer.Text) = "" Then
                    MsgBox("FOB Buyer tidak boleh kosong!", vbCritical, ".:ERROR!")
                    FOBBuyer.Focus()
                    Exit Sub
                End If
                If Trim(FOBUmum.Text) = "" Then
                    MsgBox("FOB Umum tidak boleh kosong!", vbCritical, ".:ERROR!")
                    FOBUmum.Focus()
                    Exit Sub
                End If
                If PembagiEuro.Text = "" Then PembagiEuro.Text = 0
                MsgSQL = "INSERT INTO t_PO(IDRec, NoPO, TglPO, StatusPO, " &
                    "Kode_Importir, Kode_Produk, Kode_Buyer, Jumlah, " &
                    "TglKirim, MataUang, FOBBuyer, FOBUmum, Digit3YN, " &
                    "Kode_Perajin, CatatanPO, CatatanProduk, FotoLoc, AktifYN, " &
                    "UserID, LastUPD, PembagiEuro, TransferYN) VALUES ('" & idRecord.Text & "', " &
                    "'" & Nopo.Text & "', '" & Format(tglPO.Value, "yyyy-MM-dd") & "', " &
                    "'OPEN', '" & Kode_Importir.Text & "', " &
                    "'" & KodeProduk.Text & "', '" & Kode_Buyer.Text & "', " &
                    "" & Jumlah.Text * 1 & ", '" & Format(tglKirim.Value, "YYYY-MM-DD") & "'," &
                    "'" & cmbMataUang.Text & "', " & FOBBuyer.Text * 1 & ", " &
                    "" & FOBUmum.Text * 1 & ", '" & chk3Digit.Checked & "', " &
                    "'" & Kode_Perajin.Text & "', '" & Trim(Replace(CatatanPO.Text, "'", "`")) & "', " &
                    "'" & Trim(Replace(CatatanPO.Text, "'", "`")) & "', '" & Trim(LocGmb1.Text) & "', " &
                    "'Y', '" & UserID & "', GetDate(), " & PembagiEuro.Text * 1 & ", 'N')"
                Proses.ExecuteNonQuery(MsgSQL)
                '            If MsgBox("Tambah kode produk untuk PO ini?", vbInformation + vbYesNo, "Confirm!") = vbYes Then
                TambahKode_Click()
                '            Else
                '                LTambahKode = False
                '                LAdd = False
                '                LEdit = False
                '                EnableTombol
                '            End If
            ElseIf LEdit Then
                MsgSQL = "Update t_PO Set " &
                    " NoPO = '" & Nopo.Text & "', " &
                    "TglPO = '" & Format(tglPO.Value, "YYYY-MM-DD") & "', " &
                    "StatusPO = 'OPEN', Kode_Importir = '" & Kode_Importir.Text & "', " &
                    "Kode_Produk = '" & KodeProduk.Text & "', " &
                    " Kode_Buyer = '" & Kode_Buyer.Text & "', " &
                    "   Jumlah = " & Jumlah.Text * 1 & ", " &
                    " TglKirim = '" & Format(tglKirim.Value, "YYYY-MM-DD") & "'," &
                    " MataUang = '" & cmbMataUang.Text & "', " &
                    " FOBBuyer = " & FOBBuyer.Text * 1 & ", PembagiEuro = " & PembagiEuro.Text * 1 & ", " &
                    "  FOBUmum = " & FOBUmum.Text * 1 & ", " &
                    " Digit3YN = '" & chk3Digit.Checked & "', " &
                    " Kode_Perajin = '" & Kode_Perajin.Text & "', " &
                    "    CatatanPO = '" & Trim(Replace(CatatanPO.Text, "'", "`")) & "', " &
                    "CatatanProduk = '" & Trim(Replace(CatatanPO.Text, "'", "`")) & "', " &
                    "    FotoLoc = '" & Trim(LocGmb1.Text) & "' " &
                    "Where IDRec = '" & idRecord.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "update t_PO Set TransferYN = 'N', UserID = '" & UserId & "', LastUPD = GetDate()  " &
                "Where NoPO = '" & NoPO.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)

                LAdd = False
                LEdit = False
                AturTombol(True)
            End If
            DaftarPO ""
        End If
    End Sub

    Private Sub Kode_Perajin_TextChanged(sender As Object, e As EventArgs) Handles Kode_Perajin.TextChanged
        If Len(Kode_Perajin.Text) < 1 Then
            Kode_Perajin.Text = ""
            Perajin.Text = ""
        End If
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Enabled = False
        Else
            cmdTambah.Visible = tAktif
        End If
        If tEdit = False Then
            cmdEdit.Enabled = False
            cmdSimpan.Visible = False
        Else
            cmdEdit.Visible = tAktif
            cmdSimpan.Visible = Not tAktif
        End If
        If tHapus = False Then
            cmdHapus.Enabled = False
        Else
            cmdHapus.Visible = tAktif
        End If
        cmdPenambahanKode.Visible = tAktif

        cmdRiwayatHarga.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True
        Me.Text = "SP"
    End Sub

    Private Sub Kode_Importir_TextChanged(sender As Object, e As EventArgs) Handles Kode_Importir.TextChanged
        If Len(Kode_Importir.Text) < 1 Then
            Kode_Importir.Text = ""
            Importir.Text = ""
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
        ShowFoto("")
        FOBTerakhir.Text = ""
        PesanKode.Text = ""
        tglPO.Value = Now
        tglKirim.Value = Now
        chk3Digit.Checked = False
        FOBUmum.Text = 0
        FOBBuyer.Text = 0
    End Sub

    Private Sub Nopo_TextChanged(sender As Object, e As EventArgs) Handles Nopo.TextChanged

    End Sub

    Private Sub ShowFoto(NamaFileJPG As String)
        If NamaFileJPG = "" Then
            LocGmb1.Text = ""
            PictureBox1.Image = Nothing
        End If
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.ImageLocation = filename
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub

    Private Sub Kode_Buyer_TextChanged(sender As Object, e As EventArgs) Handles Kode_Buyer.TextChanged

    End Sub

    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim rs05 As New DataTable
            MsgSQL = "Select deskripsi, cur_rp, tamb_sp, perajin, kode_perajin, " &
                    "Panjang, Lebar, Tinggi, Diameter, Tebal, Berat " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' and kodeproduk <> '' "
            rs05 = Proses.ExecuteQuery(MsgSQL)
            If rs05.Rows.Count <> 0 Then
                IsiProdukPO()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodeProduk " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodeProduk Like '%" & KodeProduk.Text & "%' or Deskripsi Like '%" & KodeProduk.Text & "%') " &
                    "Order By Deskripsi "
                Form_Daftar.Text = "Daftar Produk"
                Form_Daftar.ShowDialog()
                KodeProduk.Text = FrmMenuUtama.TSKeterangan.Text
                MsgSQL = "Select deskripsi, cur_rp, tamb_sp, perajin, kode_perajin " &
                    "Panjang, Lebar, Tinggi, Diameter, Tebal, Berat " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' "
                rs05 = Proses.ExecuteQuery(MsgSQL)
                If rs05.Rows.Count <> 0 Then
                    IsiProdukPO()
                Else
                    KodeProduk.Text = ""
                    Produk.Text = ""
                    FOBBuyer.Text = "0"
                    FOBUmum.Text = "0"
                    CatatanPO.Text = ""
                    Perajin.Text = ""
                    Kode_Perajin.Text = ""
                    PesanKode.Text = ""
                    FOBTerakhir.Text = ""
                    Kode_Buyer.Text = ""
                    ShowFoto("")
                End If
            End If
        End If
    End Sub

    Private Sub tglKirim_ValueChanged(sender As Object, e As EventArgs) Handles tglKirim.ValueChanged

    End Sub

    Private Sub IsiProdukPO()
        Dim RS05 As New DataTable, Ukuran As String = ""
        Dim RSL As New DataTable
        MsgSQL = "Select Kode_produk From T_PO " &
                "Where aktifYN = 'Y' " &
                "  And noPo = '" & Nopo.Text & "' " &
                "  And kode_produk ='" & KodeProduk.Text & "' "
        RSL = Proses.ExecuteQuery(MsgSQL)
        If RSL.Rows.Count <> 0 Then
            MsgBox("Maaf, pengisian kode ganda dalam PO yang sama tidak di ijinkan", vbCritical, "Double Product !")
            KodeProduk.Focus()
            KodeProduk.Text = ""
            Produk.Text = ""
            FOBBuyer.Text = "0"
            FOBUmum.Text = "0"
            CatatanPO.Text = ""
            Perajin.Text = ""
            Kode_Perajin.Text = ""
            PesanKode.Text = ""
            FOBTerakhir.Text = ""
            Kode_Buyer.Text = ""
            ShowFoto("")
            Exit Sub
        End If
        Dim oMataUang As String = "", tmp As String = ""
        MsgSQL = "Select descript, cur_usd, tamb_sp, perajin, kode_perajin " &
            " From m_KodeProduk " &
            "Where KodeProduk = '" & KodeProduk.Text & "' "
        RS05 = Proses.ExecuteQuery(MsgSQL)
        If RS05.Rows.Count <> 0 Then
            Produk.Text = Replace(RS05.Rows(0) !Descript, "'", "`")
            FOBUmum.Text = Format(RS05.Rows(0) !Cur_USD, "###,##0.00")
            FOBBuyer.Text = Format(RS05.Rows(0) !Cur_USD, "###,##0.00")
            tmp = FOBLama(KodeProduk.Text)
            If RS05.Rows(0) !Cur_USD <> tmp Then
                FOBBuyer.Text = tmp
                If tmp <> 0 Then MsgBox("Nilai FOB Buyer tidak sama dengan FOB yang ada " & Format(tmp, "###,##0.0000"), vbInformation, ".:Warning!")
            Else
                FOBBuyer.Text = RS05.Rows(0) !Cur_USD
            End If

            LocGmb1.Text = RS05.Rows(0) !file_foto
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
            If Trim(cmbMataUang.Text) = "" Then
                cmbMataUang.Text = "USD"
            ElseIf Trim(cmbMataUang.Text) <> oMataUang And oMataUang <> "" Then
                cmbMataUang.Text = oMataUang
                PembagiEuroEnter
            End If
            If chk3Digit.Checked = 1 Then
                FOBBuyer.Text = Format(FOBBuyer.Text, "###,##0.000")
            Else
                FOBBuyer.Text = Format(FOBBuyer.Text, "###,##0.00")
            End If
            CatatanProduk.Text = RS05.Rows(0) !tamb_SP
            Perajin.Text = RS05.Rows(0) !Perajin
            Kode_Perajin.Text = RS05.Rows(0) !Kode_Perajin
            PesanKode.Text = OrderProduk(KodeProduk.Text)
        End If


    End Sub

    Private Sub chk3Digit_CheckedChanged(sender As Object, e As EventArgs) Handles chk3Digit.CheckedChanged
        If chk3Digit.Checked = True Then
            FOBBuyer.Text = Format(FOBBuyer.Text, "###,##0.000")
        Else
            FOBBuyer.Text = Format(FOBBuyer.Text, "###,##0.00")
        End If
    End Sub

    Private Sub FOBBuyer_TextChanged(sender As Object, e As EventArgs) Handles FOBBuyer.TextChanged

    End Sub

    Private Function FOBLama(tKode As String) As Double
        Dim MsgSQL As String, RSF As New DataTable
        MsgSQL = "Select FOBBuyer From t_PO " &
            "Where Kode_Produk = '" & tKode & "' " &
            "  And AktifYN = 'Y' " &
            "Order By TglPO Desc, NoPO Desc "
        RSF = Proses.ExecuteQuery(MsgSQL)
        If RSF.Rows.Count <> 0 Then
            FOBLama = RSF.Rows(0) !FOBBuyer
        Else
            FOBLama = 0
        End If
        If Trim(Kode_Importir.Text) = "" Then
            MsgBox("Hallo " & UserID & ", importirnya masih kosong!", vbCritical, ".:Proses pengisian data salah!")
            Kode_Importir.Focus()
            Exit Function
        End If
        MsgSQL = "Select kode_buyer From t_PO " &
            "Where Kode_Produk = '" & tKode & "' " &
            "  And Kode_Importir = '" & Trim(Kode_Importir.Text) & "' " &
            "  And AktifYN = 'Y' " &
            "Order By TglPO Desc, NoPO Desc "
        RSF = Proses.ExecuteQuery(MsgSQL)
        If Not RSF.Rows.Count <> 0 Then
            Kode_Buyer.Text = RSF.Rows(0) !Kode_Buyer
        Else
            Kode_Buyer.Text = ""
        End If
        Proses.CloseConn()
    End Function

    Private Sub FOBUmum_TextChanged(sender As Object, e As EventArgs) Handles FOBUmum.TextChanged

    End Sub

    Private Sub Jumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Jumlah.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Jumlah.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Jumlah.Text) Then
                Dim temp As Double = Jumlah.Text
                Jumlah.Text = Replace(Format(temp, "###,##0.00"), ".00", "")
                Jumlah.SelectionStart = Jumlah.TextLength
            Else
                Jumlah.Text = 0
            End If
            If LAdd Or LEdit Then tglKirim.Focus()
            If LTambahKode Then cmbMataUang.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub PembagiEuro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PembagiEuro.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Jumlah.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Or LTambahKode Then
                PembagiEuroEnter
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub PembagiEuroEnter()
        If Trim(FOBBuyer.Text) = "" Then FOBBuyer.Text = 1
        PembagiEuro.Text = Format(PembagiEuro.Text, "###,##0.000")
        If cmbMataUang.Text = "RP" Then
            If Trim(PembagiEuro.Text) = "" Then PembagiEuro.Text = 9000
            If (PembagiEuro.Text * 1) = 0 Then PembagiEuro.Text = 9000
            FOBBuyer.Text = Format((FOBUmum.Text * 1) * (PembagiEuro.Text * 1), "###,##0.000")
        ElseIf cmbMataUang.Text = "EURO" Then
            If Trim(PembagiEuro.Text) = "" Then PembagiEuro.Text = 1.375
            If (PembagiEuro.Text * 1) = 0 Then PembagiEuro.Text = 1.375
            FOBBuyer.Text = Format((FOBUmum.Text * 1) / (PembagiEuro.Text * 1), "###,##0.000")
        End If
        If chk3Digit.Checked = 1 Then
            FOBBuyer.Text = Format(FOBBuyer.Text, "###,##0.000")
        Else
            FOBBuyer.Text = Format(FOBBuyer.Text, "###,##0.00")
        End If
        FOBBuyer.Focus()
    End Sub

    Private Sub Kode_Perajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Perajin.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_KodePerajin " &
              " Where KodePerajin = '" & Kode_Perajin.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Perajin.Text = dbTable.Rows(0) !nama
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodePerajin " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodePerajin Like '%" & Kode_Perajin.Text & "%' or nama Like '%" & Kode_Perajin.Text & "%') " &
                    "Order By nama "
                Form_Daftar.Text = "Daftar Perajin"
                Form_Daftar.ShowDialog()

                Kode_Perajin.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nama From m_KodePerajin " &
                   " Where KodePerajin = '" & Kode_Perajin.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Perajin.Text = dbTable.Rows(0) !nama
                Else
                    Kode_Perajin.Text = ""
                    Perajin.Text = ""
                    Kode_Perajin.Focus()
                End If
            End If
            If LAdd Or LEdit Or LTambahKode Then
                If Trim(Kode_Perajin.Text) = "" Or Trim(Perajin.Text) = "" Then
                    Kode_Perajin.Focus()
                ElseIf Trim(Kode_Perajin.Text) <> "" Or Trim(Perajin.Text) <> "" Then
                    CatatanPO.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        Nopo.Focus()
        ClearTextBoxes()
        AturTombol(False)
        StatusPO.Text = "OPEN"
    End Sub

    Private Sub TambahKode_Click()
        If Trim(Nopo.Text) = "" Then
            MsgBox("No PO masih kosong!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        AturTombol(False)
        PesanKode.Text = ""
        KodeProduk.Text = ""
        Kode_Buyer.Text = ""
        Jumlah.Text = ""
        cmbMataUang.Enabled = False
        FOBBuyer.Text = ""
        '    chk3Digit.Value = 0
        FOBUmum.Text = "0"
        FOBBuyer.Text = "0"
        '    PembagiEuro.Text = "0"
        Kode_Perajin.Text = ""
        Perajin.Text = ""
        CatatanPO.Text = ""
        CatatanProduk.Text = ""
        LocGmb1.Text = ""
        KodeProduk.Focus()
    End Sub
    Private Sub Kode_Importir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Importir.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_kodeImportir " &
              " Where KodeImportir = '" & Kode_Importir.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Importir.Text = dbTable.Rows(0) !nama
                KodeProduk.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodeImportir " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodeImportir Like '%" & Kode_Importir.Text & "%' or nama Like '%" & Kode_Importir.Text & "%') " &
                    "Order By nama "
                Form_Daftar.Text = "Daftar Importir"
                Form_Daftar.ShowDialog()

                Kode_Importir.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nama From m_KodeImportir " &
                   " Where KodeImportir = '" & Kode_Importir.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Importir.Text = dbTable.Rows(0) !nama
                    KodeProduk.Focus()
                Else
                    Kode_Importir.Text = ""
                    Importir.Text = ""
                    Kode_Importir.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Nopo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Nopo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            tglPO.Focus()
        End If
    End Sub

    Private Sub Kode_Buyer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Buyer.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            Jumlah.Focus()
        End If
    End Sub

    Private Sub tglKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglKirim.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbMataUang.Focus()
        End If
    End Sub

    Private Sub FOBBuyer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FOBBuyer.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If FOBBuyer.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(FOBBuyer.Text) Then
                Dim temp As Double = FOBBuyer.Text
                If LAdd Or LEdit Or LTambahKode Then
                    If chk3Digit.Checked = 1 Then
                        FOBBuyer.Text = Format(temp, "###,##0.000")
                    Else
                        FOBBuyer.Text = Format(temp, "###,##0.00")
                    End If
                    FOBBuyer.SelectionStart = FOBBuyer.TextLength
                    FOBUmum.Focus()
                End If
            Else
                FOBBuyer.Text = 0
                FOBBuyer.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub FOBUmum_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FOBUmum.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If FOBUmum.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(FOBUmum.Text) Then
                Dim temp As Double = FOBUmum.Text
                If LAdd Or LEdit Or LTambahKode Then
                    FOBUmum.Text = Format(temp, "###,##0.000")
                    FOBUmum.SelectionStart = FOBUmum.TextLength
                    Kode_Perajin.Focus()
                End If
            Else
                FOBUmum.Text = 0
                FOBUmum.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub DaftarPO(tCari As String)
        Dim MsgSQL As String, RSDaf As New DataTable
        Dim mKondisi As String
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        If Trim(tCari) = "" Then
            mKondisi = ""
        Else
            mKondisi = " and NoPO like '%" & tCari & "%' "
        End If
        MsgSQL = "Select distinct NoPO, TglPO, Kode_Importir, m_KodeImportir.Nama " &
            " From t_PO inner join m_KodeImportir on Kode_Importir = KodeImportir " &
            "Where t_PO.AktifYN = 'Y' " & mKondisi & " " &
            "Order By TglPO desc, NoPO desc "
        RSDaf = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSDaf.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(RSDaf.Rows(a) !NoPO,
                Format(RSDaf.Rows(a) !tglPO, "dd-MM-yyyy"),
                       Microsoft.VisualBasic.Left(RSDaf.Rows(a) !Nama & Space(50), 50) +
                            RSDaf.Rows(a) !Kode_Importir)
        Next a
        DGView.Visible = True
    End Sub

    Private Function OrderProduk(KodeProduk As String) As String
        Dim MsgSQL As String, rsc As New DataTable, oMataUang As String = ""
        MsgSQL = "Select Count(IDRec) JumRec " &
            " From t_PO " &
            "Where AktifYN = 'Y' and Kode_Produk = '" & KodeProduk & "'  and kode_importir = '" & Kode_Importir.Text & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 And rsc.Rows(0) !jumrec <> 0 Then
            OrderProduk = "Pesan kode ini ke " & Format(rsc.Rows(0) !jumrec, "###,##0") & " kali"
        Else
            OrderProduk = "PERTAMA KALI - BUKAN dari SAMPLE"
        End If
        oMataUang = cmbMataUang.Text
        MsgSQL = "Select HargaFOB, MataUang, digit3yn, NoPI " &
            " From t_PI " &
            "Where AktifYN = 'Y' and Kode_Produk = '" & KodeProduk & "' and kode_importir = '" & Kode_Importir.Text & "' " &
            "Order By IDRec Desc"
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 Then
            FOBTerakhir.Text = "Harga FOB Terkahir buyer ini = " &
                rsc.Rows(0) !MataUang & " " & Format(rsc.Rows(0) !HargaFOB, "###,##0.000")
            If rsc.Rows(0) !MataUang = "" Then
                cmbMataUang.SelectedIndex = -1
                MsgBox("Mata Uang " & KodeProduk & " PI No " & rsc.Rows(0) !NoPI & " belum di pilih!", vbCritical + vbOKOnly, ".:Warning!")
            Else
                If rsc.Rows(0) !MataUang = oMataUang Then
                    cmbMataUang.Text = rsc.Rows(0) !MataUang
                Else
                    cmbMataUang.Text = oMataUang
                End If
            End If
            If rsc.Rows(0) !digit3yn = "0" Or rsc.Rows(0) !digit3yn = "N" Then
                chk3Digit.Checked = False
                FOBBuyer.Text = Format(rsc.Rows(0) !HargaFOB, "###,##0.00")
            Else
                chk3Digit.Checked = True
                FOBBuyer.Text = Format(rsc.Rows(0) !HargaFOB, "###,##0.000")
            End If
        Else
            FOBTerakhir.Text = "Buyer ini belum pernah beli"
        End If
    End Function
End Class
