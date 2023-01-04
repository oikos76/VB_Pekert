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
            Dim m3Digit As String = "0"
            If chk3Digit.Checked Then
                m3Digit = "1"
            Else
                m3Digit = "0"
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
                    "UserID, LastUPD, PembagiEuro, TransferYN, JClose) VALUES ('" & idRecord.Text & "', " &
                    "'" & Nopo.Text & "', '" & Format(tglPO.Value, "yyyy-MM-dd") & "', " &
                    "'OPEN', '" & Kode_Importir.Text & "', " &
                    "'" & KodeProduk.Text & "', '" & Kode_Buyer.Text & "', " &
                    "" & Jumlah.Text * 1 & ", '" & Format(tglKirim.Value, "yyyy-MM-dd") & "'," &
                    "'" & cmbMataUang.Text & "', " & FOBBuyer.Text * 1 & ", " &
                    "" & FOBUmum.Text * 1 & ", '" & m3Digit & "', " &
                    "'" & Kode_Perajin.Text & "', '" & Trim(Replace(CatatanPO.Text, "'", "`")) & "', " &
                    "'" & Trim(Replace(CatatanPO.Text, "'", "`")) & "', '" & Trim(LocGmb1.Text) & "', " &
                    "'Y', '" & UserID & "', GetDate(), " & PembagiEuro.Text * 1 & ", 'N', '0')"
                Proses.ExecuteNonQuery(MsgSQL)
                '            If MsgBox("Tambah kode produk untuk PO ini?", vbInformation + vbYesNo, "Confirm!") = vbYes Then
                PenambahanKode()
                '            Else
                '                LTambahKode = False
                '                LAdd = False
                '                LEdit = False
                '                EnableTombol
                '            End If
            ElseIf LEdit Then
                MsgSQL = "Update t_PO Set " &
                    " NoPO = '" & Nopo.Text & "', " &
                    "TglPO = '" & Format(tglPO.Value, "yyyy-MM-dd") & "', " &
                    "StatusPO = 'OPEN', Kode_Importir = '" & Kode_Importir.Text & "', " &
                    "Kode_Produk = '" & KodeProduk.Text & "', " &
                    " Kode_Buyer = '" & Kode_Buyer.Text & "', " &
                    "   Jumlah = " & Jumlah.Text * 1 & ", " &
                    " TglKirim = '" & Format(tglKirim.Value, "yyyy-MM-dd") & "'," &
                    " MataUang = '" & cmbMataUang.Text & "', " &
                    " FOBBuyer = " & FOBBuyer.Text * 1 & ", PembagiEuro = " & PembagiEuro.Text * 1 & ", " &
                    "  FOBUmum = " & FOBUmum.Text * 1 & ", " &
                    " Digit3YN = '" & m3Digit & "', " &
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
            DaftarPO("")
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
        cmdAnalisaHarga.Visible = tAktif
        cmdClosePO.Visible = tAktif
        cmdRiwayatHarga.Visible = tAktif

        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True
        Me.Text = "PO"
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
            Me.Cursor = Cursors.WaitCursor
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
            If LAdd Or LEdit Or LTambahKode Then
                If Produk.Text = "" Then
                    KodeProduk.Focus()
                Else
                    Kode_Buyer.Focus()
                End If
            End If
            Me.Cursor = Cursors.Default
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
        Dim oMataUang As String = "", tmp As Double = 0
        MsgSQL = "Select descript, cur_usd, tamb_sp, perajin, kode_perajin, file_foto " &
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
                If tmp <> 0 Then MsgBox("Nilai FOB Buyer tidak sama dengan FOB yang ada " & Replace(Format(tmp, "###,##0.00"), ".00", ""), vbInformation, ".:Warning!")
            Else
                FOBBuyer.Text = Format(RS05.Rows(0) !Cur_USD, "###,##0.00)")
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
            tmp = FOBBuyer.Text * 1
            If chk3Digit.Checked = 1 Then
                FOBBuyer.Text = Format(tmp, "###,##0.000")
            Else
                FOBBuyer.Text = Format(tmp, "###,##0.00")
            End If
            CatatanProduk.Text = RS05.Rows(0) !tamb_SP
            Perajin.Text = RS05.Rows(0) !Perajin
            Kode_Perajin.Text = RS05.Rows(0) !Kode_Perajin
            PesanKode.Text = OrderProduk(KodeProduk.Text)
        End If


    End Sub

    Private Sub chk3Digit_CheckedChanged(sender As Object, e As EventArgs) Handles chk3Digit.CheckedChanged
        Dim tmp As Double = 0
        If FOBBuyer.Text = "" Then tmp = 0
        tmp = FOBBuyer.Text * 1
        If chk3Digit.Checked = True Then
            FOBBuyer.Text = Format(tmp, "###,##0.000")
        Else
            FOBBuyer.Text = Format(tmp, "###,##0.00")
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
        FOBLama = Proses.ExecuteSingleDblQuery(MsgSQL)
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
        Kode_Buyer.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
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
            If LTambahKode Then
                If cmbMataUang.Enabled Then
                    cmbMataUang.Focus()
                Else
                    FOBBuyer.Focus()
                End If
            End If
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
        Nopo.ReadOnly = False
        Nopo.Focus()
        ClearTextBoxes()
        AturTombol(False)
        StatusPO.Text = "OPEN"
    End Sub

    Private Sub PenambahanKode()
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

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PO " &
            "Where AktifYN = 'Y' " &
            "  And NOPO = '" & Nopo.Text & "' " &
            "ORDER BY tglpo, NoPO, IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPO(RSNav.Rows(0) !IdRec)
        End If
        Proses.CloseConn()
    End Sub
    Private Sub IsiPO(tCode As String)
        Dim RSP As New DataTable, rs04 As New DataTable
        If LAdd Or LEdit Then Exit Sub
        MsgSQL = "SELECT * " &
            " FROM t_PO " &
            "Where AktifYN = 'Y' " &
            "  And IDRec = '" & tCode & "' "
        RSP = Proses.ExecuteQuery(MsgSQL)
        If RSP.Rows.Count <> 0 Then
            idRecord.Text = tCode
            Nopo.Text = RSP.Rows(0) !NoPO
            tglPO.Value = RSP.Rows(0) !tglPO
            StatusPO.Text = RSP.Rows(0) !StatusPO
            Kode_Importir.Text = RSP.Rows(0) !Kode_Importir
            PembagiEuro.Text = RSP.Rows(0) !PembagiEuro
            KodeProduk.Text = RSP.Rows(0) !Kode_Produk

            LocGmb1.Text = Trim(RSP.Rows(0) !Kode_Produk) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
            Kode_Buyer.Text = RSP.Rows(0) !Kode_Buyer
            Jumlah.Text = RSP.Rows(0) !Jumlah
            tglKirim.Value = RSP.Rows(0) !tglKirim
            If RSP.Rows(0) !MataUang = "" Then
                cmbMataUang.SelectedIndex = -1
            Else
                cmbMataUang.Text = RSP.Rows(0) !MataUang
            End If
            FOBUmum.Text = RSP.Rows(0) !FOBUmum
            If RSP.Rows(0) !digit3yn = "1" Or RSP.Rows(0) !digit3yn = "Y" Then
                chk3Digit.Checked = 1
                FOBBuyer.Text = Format(RSP.Rows(0) !FOBBuyer, "###,##0.000")
            Else
                chk3Digit.Checked = 0
                FOBBuyer.Text = Format(RSP.Rows(0) !FOBBuyer, "###,##0.00")
            End If
            Kode_Perajin.Text = RSP.Rows(0) !Kode_Perajin

            CatatanPO.Text = RSP.Rows(0) !CatatanPO
            CatatanProduk.Text = RSP.Rows(0) !CatatanProduk

            MsgSQL = "Select descript From m_KodeProduk Where KodeProduk = '" & KodeProduk.Text & "' "
            Produk.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
            MsgSQL = "Select Nama From m_KodeImportir Where KodeImportir = '" & Kode_Importir.Text & "' "
            Importir.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
            MsgSQL = "Select Nama From m_KodePerajin Where KodePerajin = '" & Kode_Perajin.Text & "' "
            Perajin.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PO " &
            "Where AktifYN = 'Y' " &
            "  And IDRec > '" & idRecord.Text & "' " &
            "  And NOPO = '" & Nopo.Text & "' " &
            "ORDER BY IDRec "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPO(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PO " &
            "Where AktifYN = 'Y' " &
            "  And IDRec < '" & idRecord.Text & "' " &
            "  And NOPO = '" & Nopo.Text & "' " &
            "ORDER BY IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPO(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PO " &
            "Where AktifYN = 'Y' " &
            "  And NOPO = '" & Nopo.Text & "' " &
            "ORDER BY tglpo desc, NoPO Desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPO(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdRiwayatHarga_Click(sender As Object, e As EventArgs) Handles cmdRiwayatHarga.Click
        MsgSQL = "Select * From T_PI " &
            "Where AktifYN = 'Y' " &
            " And Kode_Produk = '" & KodeProduk.Text & "'  " &
            "order by tglpi, idrec "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Riwayat Harga"
        Form_Daftar.ShowDialog()

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

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
            If cmbMataUang.Enabled Then cmbMataUang.Focus() Else FOBBuyer.Focus()
        End If
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub tglPO_ValueChanged(sender As Object, e As EventArgs) Handles tglPO.ValueChanged

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

    Private Sub tPO_TextChanged(sender As Object, e As EventArgs) Handles tPO.TextChanged

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
            mKondisi = " And NoPO Like '%" & tCari & "%' "
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

    Private Sub cmdPenambahanKode_Click(sender As Object, e As EventArgs) Handles cmdPenambahanKode.Click
        PenambahanKode()
    End Sub

    Private Sub cmdClosePO_Click(sender As Object, e As EventArgs) Handles cmdClosePO.Click
        Dim MsgSQL As String
        If MsgBox("Anda Yakin tutup PO ini?", vbYesNo + vbInformation, "Hallo " & UserID & "....") = vbYes Then
            MsgSQL = "Update t_PO SET  " &
                " StatusPO = 'CLOSED', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where NoPO = '" & Nopo.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            StatusPO.Text = "CLOSED"

        End If
    End Sub

    Private Sub cmdAnalisaHarga_Click(sender As Object, e As EventArgs) Handles cmdAnalisaHarga.Click
        Const xlCenter = -4108

        cmdAnalisaHarga.Enabled = False
        Dim excel As New Microsoft.Office.Interop.Excel.Application
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        Dim fileName As String = "C:\PO_" + Nopo.Text + "_" + Format(Now, "yymmdd_hhmmss") + ".xls"
        'Dim SubTotal As String = "", Disc As String = "", PPH As String = "", PPN As String = "", Total As String = ""
        Dim NoUrut As Double = 1
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add

        'Add data to cells of the first worksheet in the new workbook    
        oSheet = oBook.Worksheets(1)
        oSheet.Range("A1").Value = "PURCHASE ORDER"
        oSheet.Range("A1:G1").Merge
        oSheet.Range("A1:G1").HorizontalAlignment = xlCenter

        oSheet.Range("A2").Value = "NO: " & Nopo.Text
        oSheet.Range("A2:G2").Merge
        oSheet.Range("A2:G2").HorizontalAlignment = xlCenter

        oSheet.Cells(4, 1) = Importir.Text
        oSheet.Cells(5, 1) = "Purchase Order NO : " & Nopo.Text
        oSheet.Cells(6, 1) = "Delivery Date : "
        oSheet.Cells(7, 1) = "Delivery Method : "
        oSheet.Cells(8, 1) = "Port of Departure : "

        oSheet.Cells(4, 5) = "Bila PO ini sudah di buat SP,harga beli mengacu pada harga beli di SP bersangkutan."
        oSheet.Cells(5, 5) = "Bila PO ini belum di buat SP,harga beli mengacu pada harga beli tertinggi yang pernah ada."
        oSheet.Cells(6, 5) = "Sesuaikan kurs pada cell H8 dengan kurs ketetapan Anda sendiri !"
        oSheet.Range("E4:N4").Merge
        oSheet.Range("E5:N5").Merge
        oSheet.Range("E6:N6").Merge
        oSheet.Range("E5:N5").Font.ColorIndex = 5
        oSheet.Range("E6:N6").Font.ColorIndex = 3

        oSheet.Cells(8, 5) = "1  USD  ="
        oSheet.Cells(8, 6) = 9000
        oSheet.Range("E8:F8").Interior.ColorIndex = 8

        oSheet.Cells(11, 1) = "NO"
        oSheet.Cells(11, 2) = "Our Code"
        oSheet.Cells(11, 3) = "Your Code"
        oSheet.Cells(11, 4) = "Our Description"
        oSheet.Cells(11, 5) = "QTY"
        oSheet.Cells(11, 6) = "FOB Price (USD)"
        oSheet.Cells(12, 6) = "Unit Price"
        oSheet.Cells(12, 7) = "Total"

        oSheet.Range("A11:G12").Interior.ColorIndex = 7
        oSheet.Range("A11:G12").Font.ColorIndex = 2

        oSheet.Range("A11:A12").Merge
        oSheet.Range("A11:A12").VerticalAlignment = xlCenter
        oSheet.Range("B11:B12").Merge
        oSheet.Range("B11:B12").VerticalAlignment = xlCenter
        oSheet.Range("C11:C12").Merge
        oSheet.Range("C11:C12").VerticalAlignment = xlCenter
        oSheet.Range("D11:D12").Merge
        oSheet.Range("D11:D12").VerticalAlignment = xlCenter
        oSheet.Range("E11:E12").Merge
        oSheet.Range("E11:E12").VerticalAlignment = xlCenter
        oSheet.Range("F11:G11").Merge

        Dim i As Integer = 13

        SQL = "Select a.Kode_produk, a.kode_buyer, a.Jumlah, a.FOBBuyer, Deskripsi From t_PO a inner join m_KodeProduk on KodeProduk = kode_Produk " &
            "Where a.AktifYN = 'Y' " &
            "  And nopo = '" & Nopo.Text & "' " &
            "Order By IDRec "
        dbTable = Proses.ExecuteQuery(SQL)
        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            oSheet.Range("A" + Format(i, "##0")).Value = NoUrut
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Produk
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Buyer
            oSheet.Range("D" + Format(i, "##0")).Value = dbTable.Rows(a) !Deskripsi
            oSheet.Range("E" + Format(i, "##0")).Value = dbTable.Rows(a) !Jumlah
            oSheet.Range("F" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !FOBBuyer, "###,##0.000")
            i += 1
            NoUrut = NoUrut + 1
        Next (a)


        oSheet.Columns.AutoFit()
        oSheet.range("E11:E11").HorizontalAlignment = xlCenter
        oSheet.range("F11:G11").HorizontalAlignment = xlCenter
        oSheet.Range("A13").ColumnWidth = 5
        oSheet.Range("G12").ColumnWidth = 10

        Dim strFileName As String = fileName
        Dim blnFileOpen As Boolean = False
        Try
            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
            fileTemp.Close()
        Catch ex As Exception
            blnFileOpen = False
        End Try
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
        Cursor = Cursors.Default
        'oSheet.Protect("Matius28:19")
        oExcel.Visible = True
        cmdAnalisaHarga.Enabled = True
        Proses.CloseConn()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If Trim(idRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            cmdHapus.Focus()
            Exit Sub
        End If
        If StatusPO.Text = "CLOSED" Then
            MsgBox("PO ini sudah di close!", vbCritical, "Tidak bisa di hapus!")
            cmdHapus.Focus()
            Exit Sub
        End If
        If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation) = vbYes Then
            MsgSQL = "Update t_PO SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            ClearTextBoxes()
            btnTop_Click(sender, e)
        End If
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If idRecord.Text = "" Then
            MsgBox("SP yang akan di edit belum di pilih!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LAdd = False
        LTambahKode = False
        LEdit = True
        AturTombol(False)
        Nopo.ReadOnly = True
        Jumlah.Focus()
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
    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 30
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
        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        'With Me.LstPO.RowTemplate
        '    .Height = 30
        '    .MinimumHeight = 30
        'End With
        'LstPO.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        'lstPO.BackgroundColor = Color.LightGray
        'lstPO.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        'lstPO.DefaultCellStyle.SelectionForeColor = Color.White
        'LstPO.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        ''lstPO.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        'LstPO.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'lstPO.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        'LstPO.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        'LstPO.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter


        With Me.DGView2.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView2.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
    Private Sub Form_PO_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearTextBoxes()
        'NoSPLama.Visible = False
        'PanelDataDariPO.Visible = False
        LAdd = False
        LEdit = False
        LTambahKode = False
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        Dim MsgSQL As String, RS As New DataTable
        Dim tIdRec As String = "", tKodeProduk As String = ""
        cmbMataUang.Items.Clear()
        MsgSQL = "Select Kode from m_Currency Where AktifYN = 'Y'"
        RS = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RS.Rows.Count - 1
            Application.DoEvents()
            cmbMataUang.Items.Add(RS.Rows(a) !kode)
        Next a

        UserID = FrmMenuUtama.TsPengguna.Text
        MsgSQL = "Select IDRec " &
            "From t_PO " &
            "Where AktifYN = 'Y' " &
            "Order By TglPO Desc, IDRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
            tKodeProduk = ""
        End If
        Call IsiPO(tIdRec)
        tTambah = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "laporan")
        Me.Cursor = Cursors.Default
        DaftarPO("")
        AturTombol(True)
    End Sub

    Private Sub DGView_CellContextMenuStripNeeded(sender As Object, e As DataGridViewCellContextMenuStripNeededEventArgs) Handles DGView.CellContextMenuStripNeeded

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String, rsc As New DataTable
        Dim JMacam As Integer, JTotal As Double, JNilai As Double
        If DGView.Rows.Count = 0 Then Exit Sub

        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "SELECT  t_PO.IDRec, m_KodeProduk.deskripsi, t_PO.Kode_Produk, " &
            "m_KodeImportir.Nama AS Importir, t_PO.Kode_Importir, m_KodePerajin.Nama AS Perajin,  " &
            "t_PO.Kode_Perajin , t_PO.Kode_Buyer, t_PO.Jumlah, t_PO.TglKirim, t_PO.MataUang, t_PO.FOBBuyer, t_PO.FOBUmum " &
            "FROM t_PO INNER JOIN " &
            "    m_KodeProduk ON t_PO.Kode_Produk = m_KodeProduk.KodeProduk INNER JOIN " &
            "    m_KodePerajin ON m_KodePerajin.KodePerajin = t_PO.Kode_Perajin INNER JOIN " &
            "    m_KodeImportir ON m_KodeImportir.KodeImportir = t_PO.Kode_Importir " &
            "Where T_PO.AktifYN = 'Y' " &
            "  And NoPo = '" & tCari & "' " &
            "Order BY t_PO.IDRec "
        rsc = Proses.ExecuteQuery(MsgSQL)
        JMacam = 0
        JTotal = 0
        JNilai = 0
        DGView2.Rows.Clear()
        'DGView2.Visible = False
        For a = 0 To rsc.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(rsc.Rows(a) !IdRec,
                    rsc.Rows(a) !Kode_Produk,
                    Format(rsc.Rows(a) !Jumlah, "###,##0"),
                    Format(rsc.Rows(a) !FOBBuyer, "###,##0.000"),
                    Format(rsc.Rows(a) !FOBUmum, "###,##0.000"),
                    Microsoft.VisualBasic.Left(rsc.Rows(a) !Perajin & Space(100), 100) + " - " + rsc.Rows(a) !Kode_Perajin)
            JMacam = JMacam + 1
            JTotal = JTotal + rsc.Rows(a) !Jumlah
            JNilai = JNilai + (rsc.Rows(a) !Jumlah * rsc.Rows(a) !FOBBuyer)
        Next (a)
        'DGView2.Visible = True
        JumMacam.Text = Format(JMacam, "###,##0")
        JQTY.Text = Format(JTotal, "###,##0")
        JumTotal.Text = Format(JNilai, "###,##0")
        If DGView2.Rows.Count <> 0 Then
            IsiPO(DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value)
        End If
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Dim tCode As String
        If DGView2.Rows.Count = 0 Then Exit Sub
        tCode = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        If tCode = "" Then Exit Sub
        Call IsiPO(tCode)
    End Sub

    Private Sub tglPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglPO.KeyPress
        If e.KeyChar = Chr(13) Then
            Kode_Importir.Focus()
        End If
    End Sub

    Private Sub tPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tPO.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarPO(Trim(tPO.Text))
        End If
    End Sub
End Class
