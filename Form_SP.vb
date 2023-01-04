Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_SP
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
    Protected Ds As DataSet
    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        ClearTextBoxes()
        AturTombol(False)
        optPO.Focus()
        NoPO.Enabled = True
        optPO.Checked = True
        Kode_Perajin.ReadOnly = False
        Perajin.ReadOnly = False
        NoPO.ReadOnly = False
        ' NoSP.Text = Proses.MaxYNoUrut("NoPraLHP", "t_PraLHP", "PLHP")
    End Sub

    Private Sub Kode_Produk_TextChanged(sender As Object, e As EventArgs) Handles Kode_Produk.TextChanged
        If Len(Trim(Kode_Produk.Text)) < 1 Then
            Kode_Produk.Text = ""
            Produk.Text = ""
            Jumlah.Text = 0
            HargaBeliRp.Text = 0
            HargaBeliUS.Text = 0
            CatatanProduk.Text = ""
            CatatanSP.Text = ""
            ShowFoto("")
        ElseIf Len(Trim(Kode_Produk.Text)) = 4 Then
            Kode_Produk.Text = Kode_Produk.Text + "-"
            Kode_Produk.SelectionStart = Len(Trim(Kode_Produk.Text)) + 1
        ElseIf Len(Trim(Kode_Produk.Text)) = 7 Then
            Kode_Produk.Text = Kode_Produk.Text + "-"
            Kode_Produk.SelectionStart = Len(Trim(Kode_Produk.Text)) + 1
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
        TglSP.Value = Now
        TglKirimPerajin.Value = Now
        TglMasukGudang.Value = Now
        optPO.Checked = False
        optTPO.Checked = False
        PanelDataDariPO.Visible = False
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
    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click

        If optPO.Checked = False And optTPO.Checked = False Then
            MsgBox("Bentuk SP belum di pilih!", vbCritical + vbOKOnly, ".:ERROR!")
            Exit Sub
        End If
        If optPO.Checked And Trim(NoPO.Text) = "" Then
            MsgBox("Anda memilih jenis SP ber PO tapi nomor PO nya masih kosong !" & vbCrLf &
                   "Silakan lengkapi Nomor PO !", vbCritical + vbOKOnly, "Hi " & UserID + " apa kabar ?")
            NoPO.Focus()
            Exit Sub
        End If
        If Trim(Jumlah.Text) = "" Then
            MsgBox("Jumlah tidak boleh kosong!", vbCritical + vbOKOnly, ".:Warning!")
            Jumlah.Focus()
            Exit Sub
        End If
        If Trim(Kode_Produk.Text) = "" And PanelDataDariPO.Visible = False Then
            MsgBox("Kode Produk tidak boleh kosong!", vbCritical + vbOKOnly, ".:Halo " & UserID & "...!")
            Kode_Produk.Focus()
            Exit Sub
        End If
        Dim rs05 As New DataTable, tmp As String = ""
        Dim rsi As New DataTable, tJumlah As String, QTYSP As Double = 0
        If LEdit Then
            MsgSQL = "select count(IdRec) JumRec From t_SP where idrec  = '" & idRecord.Text & "' "
            rs05 = Proses.ExecuteQuery(MsgSQL)
            If Not rs05.Rows.Count <> 0 Then
                If rs05.Rows(0) !jumrec > 1 Then
                    Randomize()
                    TMP = "TMP_EDITSP" & Trim(Replace(Str(Rnd(1000) * 1000), ".", Format(Now, "HHMMSS")))
                    MsgSQL = "Select distinct * into " & tmp & " from t_SP where idrec  = '" & idRecord.Text & "' "
                    Proses.ExecuteNonQuery(MsgSQL)
                    MsgSQL = "Delete t_SP where idrec  = '" & idRecord.Text & "' "
                    Proses.ExecuteNonQuery(MsgSQL)
                    MsgSQL = "Insert into t_SP Select * from " & TMP & ""
                    Proses.ExecuteNonQuery(MsgSQL)
                    MsgSQL = "Drop table " & TMP & " "
                    Proses.ExecuteNonQuery(MsgSQL)
                End If
            End If
            Proses.CloseConn()
            MsgSQL = "Update t_SP Set AktifYN = 'E' Where IDRec = '" & idRecord.Text & "'"
            Proses.ExecuteNonQuery(MsgSQL)
        End If
        If optPO.Checked = True And optTPO.Checked = False Then
            MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                " m_KodeImportir.Nama, isnull(Jumlah,0) Jumlah, Tamb_SP, Panjang, Lebar, " &
                " Diameter, tebal, Berat, Tinggi, cur_rp, KodePerajin2 " &
                " From t_PO inner join m_KodeProduk ON " &
                " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                "Where KodeProduk = '" & Kode_Produk.Text & "' " &
                "  And t_PO.AktifYN = 'Y' " &
                "  And T_PO.NOPO = '" & NoPO.Text & "' "
            rsi = Proses.ExecuteQuery(MsgSQL)
            If rsi.Rows.Count <> 0 Then
                tJumlah = rsi.Rows(0) !Jumlah
            Else
                tJumlah = 0
            End If

            If CEKProdukSP(Kode_Produk.Text, NoPO.Text) Then
                QTYSP = tJumlah - Proses.QTYProdukSP(Kode_Produk.Text, NoPO.Text)
                If QTYSP <= 0 Then
                    MsgBox(Kode_Produk.Text + " Sudah Pernah di input untuk SP ini!", vbCritical, ".:can't be saved!")
                    Exit Sub
                End If
            End If
        End If

        MsgSQL = "Select Nama From m_KodeImportir Where KodeImportir = '" & Kode_Importir.Text & "' "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            Importir.Text = Replace(rs05.Rows(0) !Nama, "'", "`")
        Else
            MsgBox("SORRY MAS... Kode Importir salah!", vbCritical + vbOKOnly, ".:SALAH Input Kode Importir!")
            Kode_Importir.Focus()
            Exit Sub
        End If
        MsgSQL = "Select Nama From m_KodePerajin Where KodePerajin = '" & Kode_Perajin.Text & "' "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            Perajin.Text = Replace(rs05.Rows(0) !Nama, "'", "`")
        Else
            MsgBox("SORRY MAS... Kode Perajin salah!", vbCritical + vbOKOnly, ".:SALAH Input Kode Perajin!")
            Kode_Perajin.Focus()
            Exit Sub
        End If

        If Jumlah.Text = "" And PanelDataDariPO.Visible = False Then
            MsgBox("Maaf mas.. jumlah tidak boleh kosong!", vbCritical + vbOKOnly, ".:Jumlah belum di isi !")
            Jumlah.Focus()
            Exit Sub
        End If

        If CekDouble(Kode_Produk.Text, NoSP.Text) Then
            MsgBox(Kode_Produk.Text + " Sudah Pernah di input untuk SP ini!", vbCritical, ".:Kode Ganda!")
            Exit Sub
        End If
        If LAdd Or LTambahKode Then

            Dim ada As Boolean = False
            If PanelDataDariPO.Visible = True Then
                ada = False
                For i = 0 To LstPO.Rows.Count - 1
                    If i = LstPO.Rows.Count Then Exit For
                    If LstPO.Rows(i).Cells(0).Value = True Then
                        ada = True
                        Exit For
                    End If
                Next i
                If Not ada Then
                    MsgBox("Katanya mau semua data, koq ga ada yg dipilih!", vbCritical, "yang bener bos... !")
                    LstPO.Focus()
                    Exit Sub
                End If
                If LstPO.Rows.Count <> 0 Then
                    For i = 0 To LstPO.Rows.Count - 1
                        If LstPO.Rows(i).Cells(0).Value = True Then
                            idRecord.Text = Proses.MaxNoUrut("IDRec", "t_SP", "SP")
                            If Trim(LstPO.Rows(i).Cells(4).Value) = "0" Or Trim(LstPO.Rows(i).Cells(4).Value) = "" Then
                                MsgBox("Jumlah tidak boleh kosong!", vbCritical, ".:Angka NOL/Kosong")
                                LstPO.Focus()
                                Exit Sub
                            End If
                            If Trim(LstPO.Rows(i).Cells(5).Value) = "0" Or Trim(LstPO.Rows(i).Cells(5).Value) = "" Then
                                MsgBox("Nilai Harga tidak boleh kosong!", vbCritical, ".:Angka NOL/Kosong")
                                LstPO.Focus()
                                Exit Sub
                            End If
                            MsgSQL = "INSERT INTO t_SP(IDRec, NoSP, TglSP, TPO, PO, " &
                                "NoPO, KodeImportir, Importir, ShipmentDate, Kode_Perajin, " &
                                "Perajin, KodeProduk, Produk, KodePerajin, Jumlah, HargaBeliRP," &
                                "HargaBeliUS, NilaiKurs, TglKirimPerajin, TglMasukGudang, " &
                                "CatatanProduk, CatatanSP, StatusSP, FotoLoc, AktifYN, UserID, " &
                                "LastUPD, Pernyataan, TransferYN, CatatanTambahan) VALUES('" & idRecord.Text & "', " &
                                "'" & Trim(NoSP.Text) & "', '" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                                "" & IIf(optTPO.Checked, 1, 0) & ", " & IIf(optPO.Checked, 1, 0) & ", " &
                                " '" & Trim(NoPO.Text) & "', " &
                                "'" & Kode_Importir.Text & "' , '" & Importir.Text & "', " &
                                "'" & Format(ShipmentDate.Value, "yyyy-MM-dd") & "', " &
                                "'" & Kode_Perajin.Text & "','" & Perajin.Text & "'," &
                                " '" & LstPO.Rows(i).Cells(1).Value & "', " &
                                " '" & Trim(LstPO.Rows(i).Cells(2).Value) & "', " &
                                " '" & LstPO.Rows(i).Cells(3).Value & "', " &
                                " " & LstPO.Rows(i).Cells(4).Value * 1 & ", " &
                                " " & LstPO.Rows(i).Cells(5).Value * 1 & ", " &
                                " " & LstPO.Rows(i).Cells(6).Value * 1 & ", " &
                                " " & LstPO.Rows(i).Cells(7).Value * 1 & ", " &
                                " '" & Format(TglKirimPerajin.Value, "yyyy-MM-dd") & "', " &
                                "'" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                                "'" & LstPO.Rows(i).Cells(8).Value & "', '" & Trim(CatatanSP.Text) & "', " &
                                "'', '" & LocGmb1.Text & "', 'Y', '" & UserID & "', GetDate(), '', 'N', " &
                                "'" & Trim(CatatanTambahan.Text) & "')"
                            Proses.ExecuteNonQuery(MsgSQL)
                        End If
                    Next i
                End If
                LTambahKode = False
                LAdd = False
                LEdit = False
                AturTombol(True)
                PanelDataDariPO.Visible = False
            Else
                If Trim(Jumlah.Text) = "" Or Trim(Jumlah.Text) = "0" Then
                    MsgBox("Jumlah Tidak Boleh Kosong!", vbCritical + vbOKOnly, ".:Zero value!")
                    Jumlah.Focus()
                    Exit Sub
                End If
                If Trim(HargaBeliRp.Text) = "" Or Trim(HargaBeliRp.Text) = "0" Then
                    MsgBox("Harga Beli Rp Tidak Boleh Kosong!", vbCritical + vbOKOnly, ".:Zero value!")
                    HargaBeliRp.Focus()
                    Exit Sub
                End If
                idRecord.Text = Proses.MaxNoUrut("IDRec", "t_SP", "SP")
                MsgSQL = "INSERT INTO t_SP(IDRec, NoSP, TglSP, TPO, PO, " &
                    "NoPO, KodeImportir, Importir, ShipmentDate, Kode_Perajin, " &
                    "Perajin, KodeProduk, Produk, KodePerajin, Jumlah, HargaBeliRP, " &
                    "HargaBeliUS, NilaiKurs, TglKirimPerajin, TglMasukGudang, " &
                    "CatatanProduk, CatatanSP, StatusSP, FotoLoc, AktifYN, UserID," &
                    "LastUPD, Pernyataan, TransferYN, CatatanTambahan) VALUES(" &
                    "'" & idRecord.Text & "', '" & Trim(NoSP.Text) & "', " &
                    "'" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                    "" & IIf(optTPO.Checked, 1, 0) & ", " & IIf(optPO.Checked, 1, 0) & ",  " &
                    "'" & Trim(NoPO.Text) & "', '" & Kode_Importir.Text & "' , '" & Importir.Text & "', " &
                    "'" & Format(ShipmentDate.Value, "yyyy-MM-dd") & "', " &
                    "'" & Kode_Perajin.Text & "','" & Perajin.Text & "'," &
                    "'" & Kode_Produk.Text & "', '" & Produk.Text & "', " &
                    "'" & KodePerajin.Text & "', " & Jumlah.Text * 1 & ", " &
                    "" & HargaBeliRp.Text * 1 & ", " & HargaBeliUS.Text * 1 & ", " &
                    "" & NilaiKurs.Text * 1 & ", '" & Format(TglKirimPerajin.Value, "yyyy-MM-dd") & "', " &
                    "'" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                    "'" & Trim(CatatanProduk.Text) & "', '" & Trim(CatatanSP.Text) & "', " &
                    "'', '" & LocGmb1.Text & "', 'Y', '" & UserID & "', GetDate(), '', 'N', " &
                    "'" & Trim(CatatanTambahan.Text) & "')"
                Proses.ExecuteNonQuery(MsgSQL)
                TambahKode_Click
            End If
        ElseIf LEdit Then
            If Trim(NoSP.Text) <> Trim(NoSPLama.Text) Then
                If MsgBox("No SP Berubah!" & vbCrLf & "Apakah sampean yakin untuk ganti no SP?", vbCritical + vbYesNo, ".:Confirm!") = vbYes Then
                    MsgSQL = "Update T_SP SET " &
                        "NoSP = '" & NoSP.Text & "', PO = " & IIf(optPO.Checked, 1, 0) & ", TPO = " & IIf(optPO.Checked, 1, 0) & " " &
                        "Where NoSP = '" & NoSPLama.Text & "' "
                    Proses.ExecuteNonQuery(MsgSQL)
                End If
            End If
            MsgSQL = "Update t_SP  Set  NoPO = '" & Trim(NoPO.Text) & "'," &
                "       Jumlah = " & Jumlah.Text * 1 & ",  " &
                "  HargaBeliRP = " & HargaBeliRp.Text * 1 & ", " &
                "  HargaBeliUS = " & HargaBeliUS.Text * 1 & ", " &
                "    NilaiKurs = " & NilaiKurs.Text * 1 & ",   " &
                "CatatanProduk = '" & Trim(CatatanProduk.Text) & "', " &
                "      FotoLoc = '" & LocGmb1.Text & "', " &
                "   KodeProduk = '" & Kode_Produk.Text & "', " &
                "       Produk = '" & Produk.Text & "', AktifYN = 'Y', " &
                "       UserID = '" & UserID & "', LastUPD = GetDate() " &
                "  Where IDRec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            MsgSQL = "Update T_SP Set NoPO = '" & Trim(NoPO.Text) & "', " &
                " TglSP = '" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                " KodeImportir = '" & Kode_Importir.Text & "', " &
                " Importir = '" & Importir.Text & "', " &
                " ShipmentDate = '" & Format(ShipmentDate.Value, "yyyy-MM-dd") & "', " &
                " KodePerajin = '" & KodePerajin.Text & "', " &
                " Kode_Perajin = '" & Kode_Perajin.Text & "', " &
                " Perajin = '" & Perajin.Text & "', " &
                " TglKirimPerajin = '" & Format(TglKirimPerajin.Value, "yyyy-MM-dd") & "', " &
                " TglMasukGudang = '" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                " CatatanTambahan = '" & Trim(CatatanTambahan.Text) & "', " &
                " CatatanSP = '" & Trim(CatatanSP.Text) & "', TransferYN = 'N', LastUPD = GetDate() " &
                " Where NoSP = '" & NoSP.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)

            MsgSQL = "Delete t_SP Where IDRec = '" & idRecord.Text & "' and AktifYN = 'E' "
            Proses.ExecuteNonQuery(MsgSQL)

            LTambahKode = False
            LAdd = False
            LEdit = False
            AturTombol(True)
        End If
    End Sub


    Private Sub TambahKode_Click()
        If Trim(NoSP.Text) = "" Then
            MsgBox("No SP masih kosong!", vbCritical + vbOKOnly, ".:Warning!")
            Exit Sub
        End If
        LTambahKode = True
        AturTombol(False)
        Produk.Text = ""
        Kode_Produk.Text = ""
        KodePerajin.Text = ""
        Jumlah.Text = ""
        Kode_Perajin.ReadOnly = True
        Perajin.ReadOnly = True
        HargaBeliRp.Text = 0
        HargaBeliUS.Text = 0
        NilaiKurs.Text = 0
        CatatanSP.Text = ""
        CatatanProduk.Text = ""
        ShowFoto("")
    End Sub
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

        With Me.LstPO.RowTemplate
            .Height = 30
            .MinimumHeight = 30
        End With
        LstPO.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        lstPO.BackgroundColor = Color.LightGray
        lstPO.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        lstPO.DefaultCellStyle.SelectionForeColor = Color.White
        LstPO.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        'lstPO.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        LstPO.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        lstPO.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        LstPO.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        LstPO.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter


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

    Private Sub NoPO_TextChanged(sender As Object, e As EventArgs) Handles NoPO.TextChanged
        If Len(Trim(NoPO.Text)) < 1 Then
            Kode_Produk.Text = ""
        End If
    End Sub

    Private Sub Form_SP_Load(sender As Object, e As EventArgs) Handles Me.Load

        ClearTextBoxes()
        NoSPLama.Visible = False
        PanelDataDariPO.Visible = False
        LAdd = False
        LEdit = False
        LTambahKode = False
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String = "", tKodeProduk As String = ""
        MsgSQL = "Select Distinct IDRec, KodeProduk, NoSP, TglSP, NoPO, Importir, " &
            "Perajin, ShipmentDate " &
            "From t_SP " &
            "Where AktifYN = 'Y' " &
            "Order By TglSP Desc, NoSP Desc, IDRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
            tKodeProduk = Rs.Rows(0) !KodeProduk
        Else
            tIdRec = ""
            tKodeProduk = ""
        End If
        Call IsiSP(tIdRec, tKodeProduk)
        tTambah = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "31_SURAT_PESANAN", "laporan")
        Me.Cursor = Cursors.Default
        DaftarSP("")
        AturTombol(True)
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub IsiSP(tIdRec As String, tKodeProduk As String)
        Dim rsc As New DataTable
        On Error GoTo ErrMSG
        MsgSQL = "SELECT * " &
            " FROM t_SP " &
            "Where AktifYN = 'Y' " &
            "  And IDRec = '" & tIdRec & "' " &
            "  And KodeProduk = '" & tKodeProduk & "'  "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 Then
            optTPO.Checked = IIf(rsc.Rows(0) !tPO = True, 1, 0)
            optPO.Checked = IIf(rsc.Rows(0) !PO = True, 1, 0)
            idRecord.Text = rsc.Rows(0) !IdRec
            NoSP.Text = rsc.Rows(0) !NoSP
            TglSP.Value = rsc.Rows(0) !TglSP
            NoPO.Text = rsc.Rows(0) !NoPO
            Kode_Importir.Text = rsc.Rows(0) !KodeImportir
            Importir.Text = rsc.Rows(0) !Importir
            If UCase(rsc.Rows(0) !KodeImportir) = "PT" Then
                Kode_Importir.Text = "9999A"
                MsgSQL = "Select Nama " &
                    " From m_KodeImportir A " &
                    "Where A.AktifYN = 'Y' " &
                    "  And KodeImportir = '" & Kode_Importir.Text & "' " &
                    "Order By Nama "
                Importir.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
            End If
            ShipmentDate.Value = rsc.Rows(0) !ShipmentDate
            Kode_Perajin.Text = rsc.Rows(0) !Kode_Perajin
            Perajin.Text = rsc.Rows(0) !Perajin
            Kode_Produk.Text = rsc.Rows(0) !KodeProduk
            Produk.Text = rsc.Rows(0) !Produk
            If Produk.Text = "" Then
                MsgSQL = "Select Deskripsi " &
                    "From m_KodeProduk Where KodeProduk = '" & Kode_Produk.Text & "' "
                Produk.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
            End If
            KodePerajin.Text = rsc.Rows(0) !KodePerajin
            Jumlah.Text = rsc.Rows(0) !Jumlah
            HargaBeliRp.Text = rsc.Rows(0) !HargaBeliRp
            HargaBeliUS.Text = rsc.Rows(0) !HargaBeliUS
            NilaiKurs.Text = rsc.Rows(0) !NilaiKurs
            TglKirimPerajin.Value = rsc.Rows(0) !TglKirimPerajin
            TglMasukGudang.Value = rsc.Rows(0) !TglMasukGudang
            CatatanProduk.Text = rsc.Rows(0) !CatatanProduk
            CatatanSP.Text = rsc.Rows(0) !CatatanSP
            CatatanTambahan.Text = rsc.Rows(0) !CatatanTambahan
            StatusSP.Text = rsc.Rows(0) !StatusSP
            LocGmb1.Text = Trim(Kode_Produk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
        End If
        Proses.CloseConn()
ErrMSG:
        If Err.Number <> 0 Then
            MsgBox(Err.Description, vbCritical + vbOKOnly, ".:Error")
        End If
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub DaftarSP(tCari As String)
        Dim MsgSQL As String, RS05 As New DataTable
        Dim mKondisi As String = ""
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        QTYMacam.Text = ""
        QTYPesan.Text = ""
        NilaiPesan.Text = ""

        If tSP.Text <> "" Then
            mKondisi = " and NOSP like '%" & tSP.Text & "%' "
        ElseIf tPO.Text <> "" Then
            mKondisi = " and NOPO like '%" & tPO.Text & "%' "
        ElseIf tKodeBrg.Text <> "" Then
            mKondisi = " and KodeProduk like '%" & tKodeBrg.Text & "%' "
        End If

        MsgSQL = "Select Distinct NoSP, TglSP, NoPO, Importir, Perajin, " &
            "      ShipmentDate, TglMasukGudang, right(NoSP,2) + left(nosp,3) " &
            " From t_SP " &
            "Where AktifYN = 'Y' " & mKondisi & " " &
            "Order By right(NoSP,2) + left(nosp,3) Desc "
        RS05 = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RS05.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(RS05.Rows(a) !NoSP,
                Format(RS05.Rows(a) !TglSP, "dd-MM-yyyy"),
                RS05.Rows(a) !Perajin,
                RS05.Rows(a) !NoPO,
                RS05.Rows(a) !Importir,
                Format(RS05.Rows(a) !ShipmentDate, "dd-MM-yyyy"),
                Format(RS05.Rows(a) !TglMasukGudang, "dd-MM-yyyy"))
        Next (a)
        DGView.Visible = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub tSP_TextChanged(sender As Object, e As EventArgs) Handles tSP.TextChanged

    End Sub

    Private Sub Kode_Produk_GotFocus(sender As Object, e As EventArgs) Handles Kode_Produk.GotFocus
        With Kode_Produk
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
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
                Jumlah.Text = Format(temp, "###,##0.00")
                Jumlah.SelectionStart = Jumlah.TextLength
            Else
                Jumlah.Text = 0
            End If
            If LAdd Or LEdit Then HargaBeliRp.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub tPO_TextChanged(sender As Object, e As EventArgs) Handles tPO.TextChanged

    End Sub

    Private Sub Jumlah_GotFocus(sender As Object, e As EventArgs) Handles Jumlah.GotFocus
        With Jumlah
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub tKodeBrg_TextChanged(sender As Object, e As EventArgs) Handles tKodeBrg.TextChanged

    End Sub

    Private Sub Kode_Produk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Produk.KeyPress
        Dim RSI As New DataTable, RSF As New DataTable, RSL As New DataTable
        Dim Ukuran As String = "", QTYSP As Double = 0
        If e.KeyChar = Chr(13) Then
            If optPO.Checked = False And optTPO.Checked = False Then
                MsgBox("Jenis PO Belum di pilih!", vbCritical + vbOKOnly, ".:Pilih jenis PO nya dulu ya!")
                Exit Sub
            End If
            If optPO.Checked = True Then
                MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                    " m_KodeImportir.Nama, Jumlah, Tamb_SP, Panjang, Lebar, TAMB_SP" &
                    " Diameter, tebal, Berat, Tinggi, cur_rp, isnull(KodePerajin2,'') KodePerajin2 " &
                    " From t_PO inner join m_KodeProduk ON " &
                    " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where KodeProduk = '" & Kode_Produk.Text & "' " &
                    "  And t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & NoPO.Text & "' "
                RSI = Proses.ExecuteQuery(MsgSQL)
                If RSI.Rows.Count <> 0 Then
                    If CEKProdukSP(Kode_Produk.Text, NoPO.Text) Then
                        QTYSP = RSI.Rows(0) !Jumlah - Proses.QTYProdukSP(Kode_Produk.Text, NoPO.Text)
                        If QTYSP <= 0 Then
                            MsgBox(Kode_Produk.Text + " Sudah Pernah di input untuk SP ini!", vbCritical, ".:Double Input!")
                            Kode_Produk.Text = ""
                            Produk.Text = ""
                            Jumlah.Text = 0
                            CatatanProduk.Text = ""
                            CatatanSP.Text = ""
                            HargaBeliRp.Text = 0
                            Exit Sub
                        Else
                            Jumlah.Text = Format(QTYSP, "###,##0")
                        End If
                    End If
                    Ukuran = ""
                    If RSI.Rows(0) !Panjang <> 0 Then Ukuran = Ukuran + "P = " & Format(RSI.Rows(0) !Panjang, "###,##0.00") + " "
                    If RSI.Rows(0) !Lebar <> 0 Then Ukuran = Ukuran + "L = " & Format(RSI.Rows(0) !Lebar, "###,##0.00") + " "
                    If RSI.Rows(0) !Tinggi <> 0 Then Ukuran = Ukuran + "T = " & Format(RSI.Rows(0) !Tinggi, "###,##0.00") + " "
                    If RSI.Rows(0) !Diameter <> 0 Then Ukuran = Ukuran + "Diameter = " & Format(RSI.Rows(0) !Diameter, "###,##0.00") + " "
                    If RSI.Rows(0) !Tebal <> 0 Then Ukuran = Ukuran + "Tebal = " & Format(RSI.Rows(0) !Tebal, "###,##0.00") + " "
                    If RSI.Rows(0) !Berat <> 0 Then Ukuran = Ukuran + "Berat = " & Format(RSI.Rows(0) !Berat, "###,##0.00") + " "
                    CatatanProduk.Text = RSI.Rows(0) !tamb_SP
                    Produk.Text = Replace(RSI.Rows(0) !Deskripsi, "'", "`")
                    '                CatatanTambahan.Text = RSI!tamb_SP
                    '                If Trim(Jumlah.Text) = "" Or Jumlah.Text = 0 Then
                    Jumlah.Text = RSI.Rows(0) !Jumlah
                    '                End If
                    KodePerajin.Text = RSI.Rows(0) !KodePerajin2
                Else
                    Kode_Produk.Text = Proses.FindKodeProdukPO_SP(Kode_Produk.Text, NoPO.Text)
                    MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                        " m_KodeImportir.Nama, T_PO.Jumlah, Tamb_SP, Panjang, Lebar, " &
                        " Diameter, tebal, Berat, Tinggi, cur_rp, isnull(KodePerajin2,'') KodePerajin2, TAMB_SP " &
                        " From t_PO inner join m_KodeProduk ON " &
                        " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                        " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                        "Where KodeProduk = '" & Kode_Produk.Text & "' " &
                        "  And t_PO.AktifYN = 'Y' " &
                        "  And T_PO.NOPO = '" & NoPO.Text & "' "
                    RSF = Proses.ExecuteQuery(MsgSQL)
                    If RSF.Rows.Count <> 0 Then
                        If CEKProdukSP(Kode_Produk.Text, NoPO.Text) Then
                            QTYSP = RSF.Rows(0) !Jumlah - Proses.QTYProdukSP(Kode_Produk.Text, NoPO.Text)
                            If QTYSP <= 0 Then
                                MsgBox(Kode_Produk.Text + " Sudah Pernah di input untuk SP ini!", vbCritical, ".:Double Input!")
                                Kode_Produk.Text = ""
                                Produk.Text = ""
                                Jumlah.Text = 0
                                CatatanProduk.Text = ""
                                CatatanSP.Text = ""
                                HargaBeliRp.Text = 0
                                Exit Sub
                            Else
                                Jumlah.Text = QTYSP
                            End If
                        End If
                        Ukuran = ""
                        If RSF.Rows(0) !Panjang <> 0 Then Ukuran = Ukuran + "P = " & Format(RSF.Rows(0) !Panjang, "###,##0.00") + " "
                        If RSF.Rows(0) !Lebar <> 0 Then Ukuran = Ukuran + "L = " & Format(RSF.Rows(0) !Lebar, "###,##0.00") + " "
                        If RSF.Rows(0) !Tinggi <> 0 Then Ukuran = Ukuran + "T = " & Format(RSF.Rows(0) !Tinggi, "###,##0.00") + " "
                        If RSF.Rows(0) !Diameter <> 0 Then Ukuran = Ukuran + "Diameter = " & Format(RSF.Rows(0) !Diameter, "###,##0.00") + " "
                        If RSF.Rows(0) !Tebal <> 0 Then Ukuran = Ukuran + "Tebal = " & Format(RSF.Rows(0) !Tebal, "###,##0.00") + " "
                        If RSF.Rows(0) !Berat <> 0 Then Ukuran = Ukuran + "Berat = " & Format(RSF.Rows(0) !Berat, "###,##0.00") + " "
                        CatatanProduk.Text = RSF.Rows(0) !tamb_SP
                        Produk.Text = Replace(RSF.Rows(0) !Deskripsi, "'", "`")
                        If Jumlah.Text = "0" Or Trim(Jumlah.Text) = "" Then
                            Jumlah.Text = RSF.Rows(0) !Jumlah
                        End If
                        HargaBeliRp.Text = RSF.Rows(0) !cur_rp
                        KodePerajin.Text = RSF.Rows(0) !KodePerajin2
                        'CatatanTambahan.Text = RSF!tamb_SP
                    End If
                End If
            ElseIf optTPO.Checked = True Then
                '----tanpa po
                MsgSQL = "Select * " &
                    " From m_KodeProduk A " &
                    " Where A.AktifYN = 'Y' " &
                    "  And KodeProduk = '" & Kode_Produk.Text & "' " &
                    " Order By descript "
                RSI = Proses.ExecuteQuery(MsgSQL)
                If RSI.Rows.Count <> 0 Then
                    MsgSQL = "Select Kodeproduk From T_SP " &
                        "Where aktifYN = 'Y' " &
                        "  And noSP  = '" & NoSP.Text & "' " &
                        "  And kodeproduk ='" & Kode_Produk.Text & "' "
                    RSL = Proses.ExecuteQuery(MsgSQL)
                    If RSL.Rows.Count <> 0 Then
                        MsgBox("Maaf, pengisian kode ganda dalam SP yang sama tidak di ijinkan", vbCritical, "Double Cek!")
                        Kode_Produk.Focus()
                        Kode_Produk.Text = ""
                        HargaBeliRp.Text = "0"
                        Produk.Text = ""
                        Exit Sub
                    End If
                    Ukuran = ""
                    If RSI.Rows(0) !Panjang <> 0 Then Ukuran = Ukuran + "P = " & Format(RSI.Rows(0) !Panjang, "###,##0.00") + " "
                    If RSI.Rows(0) !Lebar <> 0 Then Ukuran = Ukuran + "L = " & Format(RSI.Rows(0) !Lebar, "###,##0.00") + " "
                    If RSI.Rows(0) !Tinggi <> 0 Then Ukuran = Ukuran + "T = " & Format(RSI.Rows(0) !Tinggi, "###,##0.00") + " "
                    If RSI.Rows(0) !Diameter <> 0 Then Ukuran = Ukuran + "Diameter = " & Format(RSI.Rows(0) !Diameter, "###,##0.00") + " "
                    If RSI.Rows(0) !Tebal <> 0 Then Ukuran = Ukuran + "Tebal = " & Format(RSI.Rows(0) !Tebal, "###,##0.00") + " "
                    If RSI.Rows(0) !Berat <> 0 Then Ukuran = Ukuran + "Berat = " & Format(RSI.Rows(0) !Berat, "###,##0.00") + " "
                    CatatanProduk.Text = IIf(IsDBNull(RSI.Rows(0) !tamb_SP), "", RSI.Rows(0) !tamb_SP)
                    Produk.Text = Replace(RSI.Rows(0) !Deskripsi, "'", "`")
                    HargaBeliRp.Text = RSI.Rows(0) !cur_rp
                End If
                'tanpa po------
            End If
            If LAdd Or LEdit Or LTambahKode Then
                If Trim(Kode_Produk.Text) = "" Or Trim(Kode_Produk.Text) = "" Then
                    Kode_Produk.Focus()
                ElseIf Trim(Kode_Produk.Text) <> "" Or Trim(Kode_Produk.Text) <> "" Then
                    Jumlah.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        CetakSP
    End Sub
    Private Sub CetakSP()
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable

        Dim MsgSQL As String, rsc As New DataTable, Pernyataan As String
        Dim TCetak As String, tSQL As String, mPO As String, mRevisi As String
        Dim terbilang As String = "", tb As New Terbilang
        If Trim(NoSP.Text) = "" Then
            MsgBox("No SP yang akan di cetak belum di pilih!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        mRevisi = StatusSP.Text

        If Proses.UserAksesMenu(UserID, "SP_CETAK") Then
            If mRevisi = "" Then
                If MsgBox("Apakah SP ini Revisi?", vbYesNo + vbInformation, ".:Confirm!") = vbYes Then
                    MsgSQL = "Update T_SP Set StatusSP = 'REVISI' " &
                    "  Where NoSP = '" & NoSP.Text & "' "
                    Proses.ExecuteNonQuery(MsgSQL)
                    StatusSP.Text = "REVISI"
                    mRevisi = "REVISI"
                Else
                    mRevisi = ""
                End If
            End If
        End If
        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select isnull(Sum(t_SP.Jumlah * t_SP.HargaBeliRP),0) JValue " &
        "From Pekerti.dbo.t_SP  " &
        "Where t_SP.AktifYN = 'Y' " &
        "  And t_SP.NoSP = '" & NoSP.Text & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)

        If rsc.Rows.Count <> 0 Then
            terbilang = " " & tb.Terbilang(CDbl(IIf(rsc.Rows(0) !jvalue, 0, rsc.Rows(0) !jvalue))) & " "
        End If
        Proses.CloseConn()
        TCetak = "Jakarta, " & Proses.TglIndo(Format(TglSP.Value, "dd-MM-yyyy"))
        MsgSQL = "SELECT t_SP.IDRec, t_SP.NoSP, t_SP.NoPO, t_SP.Kode_Perajin, " &
            "t_SP.Perajin, t_SP.KodeProduk, t_SP.Produk, t_SP.KodePerajin, " &
            "t_SP.Jumlah, t_SP.HargaBeliRP, t_SP.CatatanSP, m_KodeProduk.Panjang, " &
            "m_KodeProduk.Lebar, m_KodeProduk.Tinggi " &
            "FROM Pekerti.dbo.t_SP t_SP INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON " &
            "   t_SP.KodeProduk = m_KodeProduk.KodeProduk  " &
            "Where t_SP.AktifYN = 'Y' " &
            "  And t_SP.NoSP = '" & NoSP.Text & "' " &
            "ORDER BY t_SP.IDRec, t_SP.NoPO ASC "
        If optTPO.Checked = True Then
            mPO = ""
        Else
            mPO = " untuk PO No. " & Trim(NoPO.Text) & " "
        End If
        Pernyataan = "Setelah menerima dan mempelajari Surat Pesanan No. " & NoSP.Text &
        " tertanggal " & Proses.TglIndo(Format(TglSP.Value, "dd-MM-yyyy")) &
        mPO & ", dengan ini kami menyatakan " &
        " kesanggupan untuk memenuhi pesanan tersebut sesuai dengan syarat atau kondisi seperti terinci pada lembar SP ini. "
        If Len(Pernyataan) > 254 Then
            Pernyataan = Replace(Pernyataan, Proses.TglIndo(Format(TglSP.Value, "dd-MM-yyyy")), Format(TglSP.Value, "dd-MM-yyyy"))
        End If

        Dim pernyataan2 As String = "Antara kami dan Pekerti karenanya terikat pada perjanjian bersama untuk melaksanakan pesanan tersebut dengan baik sesuai dengan tugas, hak, dan kewajiban masing-masing pihak. "
        tSQL = "Update T_SP Set Pernyataan = '" & Trim(Microsoft.VisualBasic.Left(Pernyataan, 254)) & "' " &
            "Where NoSP = '" & NoSP.Text & "' "
        Proses.ExecuteNonQuery(tSQL)
        DTadapter = New SqlDataAdapter(SQL, CN)
        'terbilang = "# " + tb.Terbilang(CDbl(Total.Text)) + " Rupiah #"
        Try
            DTadapter.Fill(dttable)
            'RPT belum di convert !!!
            If Proses.UserAksesMenu(UserID, "SP_CETAK") Then
                If MsgBox("Mau pakai tanda tangan?", vbYesNo + vbInformation, ".:Signature!") = vbYes Then
                    ' objRep = New Rpt_sptt.rpt
                Else
                    ' objRep = New Rpt_sp.rpt
                End If
            Else
                ' objRep = New Rpt_sp.rpt
            End If
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("terbilang", terbilang)
            objRep.SetParameterValue("userid", UserID)
            objRep.SetParameterValue("toko", FrmMenuUtama.CompCode.Text)

            Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_Report.CrystalReportViewer1.Refresh()
            Form_Report.CrystalReportViewer1.ReportSource = objRep
            Form_Report.CrystalReportViewer1.ShowRefreshButton = False
            Form_Report.CrystalReportViewer1.ShowPrintButton = False
            Form_Report.CrystalReportViewer1.ShowParameterPanelButton = False
            Form_Report.ShowDialog()

            dttable.Dispose()
            DTadapter.Dispose()
            Proses.CloseConn(CN)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error")
        End Try
        'With CrIns
        '    .Reset
        '    .LogOnServer "PDSODBC.DLL", "DBPEKERTI", "PEKERTI", Usr, PWD
        'If UserRights(UserID, "SP_CETAK") Then
        '        If MsgBox("Mau pakai tanda tangan?", vbYesNo + vbInformation, ".:Signature!") = vbYes Then
        '            .ReportFileName = Left(RptLoc, Len(Trim(RptLoc)) - 1) & "\Rpt_sptt.rpt"
        '        Else
        '            .ReportFileName = Left(RptLoc, Len(Trim(RptLoc)) - 1) & "\Rpt_sp.rpt"
        '        End If
        '    Else
        '        .ReportFileName = Left(RptLoc, Len(Trim(RptLoc)) - 1) & "\Rpt_sp.rpt"
        '    End If
        '    .Formulas(1) = "terbilang = '" & Terbilang & "' "
        '    .Formulas(2) = "Pernyataan = '" & Left(Pernyataan, 254) & "' "
        '    .Formulas(3) = "Pernyataan2 = '" & pernyataan2 & "' "
        '    .Formulas(4) = "TglCetak = '" & TCetak & "' "
        '    .Formulas(5) = "MRevisi = '" & mRevisi & "' "
        '    .SQLQuery = MsgSQL
        '    .WindowState = crptMaximized
        '    .WindowShowGroupTree = False
        '    .WindowShowSearchBtn = True
        '    .WindowShowCloseBtn = True
        '    .WindowShowNavigationCtls = True
        '    .WindowShowProgressCtls = True
        '    .WindowShowPrintSetupBtn = True
        '    .WindowShowPrintBtn = True
        '    .WindowAllowDrillDown = True
        '    .Action = 1
        'End With
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub optPO_CheckedChanged(sender As Object, e As EventArgs) Handles optPO.CheckedChanged
        If optPO.Checked = True Then
            NoPO.Visible = True
            lNoPO.Visible = True
            ShipmentDate.Visible = True
            lShipmentDate.Visible = True
            Kode_Importir.Text = ""
            If LAdd Then
                NoSP.Text = Proses.MaxYNoUrut("NoSP", "t_SP", "E")
            ElseIf LEdit Then
            End If
        End If
    End Sub

    Private Sub cmdPanelDataDariPO_Click(sender As Object, e As EventArgs) Handles cmdPanelDataDariPO.Click
        If optPO.Checked = True And optTPO.Checked = False Then
            If PanelDataDariPO.Visible = False Then
                PanelDataDariPO.Visible = True
                optSemua.Checked = True
                PanelDataDariPO.Location = New Point(14, 215)
            End If
        End If
    End Sub

    Private Sub optSebagian_CheckedChanged(sender As Object, e As EventArgs) Handles optSebagian.CheckedChanged
        If optSemua.Checked = False And optSebagian.Checked = True Then
            PanelDataDariPO.Visible = False
        End If
    End Sub

    Private Sub Kode_Perajin_TextChanged(sender As Object, e As EventArgs) Handles Kode_Perajin.TextChanged
        If Len(Kode_Perajin.Text) < 1 Then
            Kode_Perajin.Text = ""
            Perajin.Text = ""
        End If
    End Sub

    Private Function CEKProdukSP(tKodeProduk As String, tNoSP As String) As Boolean
        Dim MsgSQL As String, IdSP As String = ""
        MsgSQL = "Select IDRec From t_SP " &
            "Where NoPO = '" & tNoSP & "' " &
            " And KodeProduk = '" & tKodeProduk & "' " &
            " And AktifYN = 'Y' "
        IdSP = Proses.ExecuteSingleStrQuery(MsgSQL)
        If IdSP <> "" Then
            CEKProdukSP = True
        Else
            CEKProdukSP = False
        End If
    End Function


    Private Sub NoPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPO.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO, kodeimportir " &
                " From T_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
                "Where T_PO.AktifYN = 'Y' " &
                "  and nopo = '" & NoPO.Text & "' " &
                "Group By NoPO, m_KodeImportir.Nama, TglPO, kodeimportir " &
                "Order By TglPO Desc, NoPO Desc "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NoPO.Text = dbTable.Rows(0) !nopo
                Kode_Importir.Text = dbTable.Rows(0) !kodeimportir
                Importir.Text = dbTable.Rows(0) !importir
                KetNoPO.Text = "Ambil data dari PO No." & NoPO.Text
                ShipmentDate.Focus()
            Else
                Dim dbpo As New DataTable
                SQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO, KodeImportir " &
                    " From T_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where T_PO.AktifYN = 'Y' " &
                    "  and nopo like '%" & NoPO.Text & "%' " &
                    "Group By NoPO, m_KodeImportir.Nama, TglPO, KodeImportir " &
                    "Order By TglPO Desc, NoPO Desc "
                Form_Daftar.txtQuery.Text = SQL
                Form_Daftar.Text = "Daftar PO"
                Form_Daftar.ShowDialog()
                NoPO.Text = FrmMenuUtama.TSKeterangan.Text
                SQL = "SELECT nopo, kodeimportir, m_KodeImportir.Nama Importir " &
                    "FROM t_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "WHERE nopo = '" & NoPO.Text & "' AND t_PO.aktifYN = 'Y' "
                dbpo = Proses.ExecuteQuery(SQL)
                If dbpo.Rows.Count <> 0 Then
                    NoPO.Text = dbpo.Rows(0) !nopo
                    Kode_Importir.Text = dbpo.Rows(0) !kodeimportir
                    Importir.Text = dbpo.Rows(0) !importir
                    KetNoPO.Text = "Ambil data dari PO No." & NoPO.Text
                    ShipmentDate.Focus()
                Else
                    NoPO.Text = ""
                    Kode_Importir.Text = ""
                    Importir.Text = ""
                    NoPO.Focus()
                End If

            End If
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim mKondisi As String = "", rsp As New DataTable, tId As String = ""
        Dim TMacam As Double = 0, TPesan As Double = 0, tNilai As Double = 0, tKodeProduk As String = ""
        If DGView.Rows.Count = 0 Then Exit Sub
        If LAdd Or LEdit Or LTambahKode Then Exit Sub
        Dim tCari As String = ""
        QTYMacam.Text = ""
        QTYPesan.Text = ""
        NilaiPesan.Text = ""
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        If Trim(tSP.Text) = "" Then
            mKondisi = ""
        Else
            mKondisi = ""
        End If
        DGView2.Rows.Clear()
        DGView2.Visible = False
        MsgSQL = "SELECT  IDrec, KodeProduk, Produk, HargaBeliRp, Jumlah, HargaBeliUS, NilaiKurs " &
            " FROM t_SP " &
            "WHERE AktifYN = 'Y' " &
            "  AND NOSP like '%" & tCari & "%'  " &
            "  " & mKondisi & " "
        rsp = Proses.ExecuteQuery(MsgSQL)
        If rsp.Rows.Count <> 0 Then
            tId = rsp.Rows(0) !idrec
            tKodeProduk = rsp.Rows(0) !kodeproduk
        End If
        For a = 0 To rsp.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(rsp.Rows(a) !IdRec,
                Microsoft.VisualBasic.Left(rsp.Rows(a) !KodeProduk + Space(15), 15) + Microsoft.VisualBasic.Left(rsp.Rows(a) !Produk & Space(50), 50),
                Format(rsp.Rows(a) !Jumlah, "###,##0"),
                Format(rsp.Rows(a) !HargaBeliRp, "###,##0"),
                Format(rsp.Rows(a) !Jumlah * rsp.Rows(a) !HargaBeliRp, "###,##0"),
                Format(rsp.Rows(a) !HargaBeliUS, "###,##0"),
                Format(rsp.Rows(a) !NilaiKurs, "###,##0"))
            TMacam = TMacam + 1
            TPesan = TPesan + rsp.Rows(a) !Jumlah
            tNilai = tNilai + (rsp.Rows(a) !Jumlah * rsp.Rows(a) !HargaBeliRp)
        Next (a)
        DGView2.Visible = True

        QTYMacam.Text = Format(TMacam, "###,##0")
        QTYPesan.Text = Format(TPesan, "###,##0")
        NilaiPesan.Text = Format(tNilai, "###,##0")
        If DGView2.Rows.Count <> 0 Then
            IsiSP(tId, tKodeProduk)
        End If
    End Sub

    Private Sub ShipmentDate_ValueChanged(sender As Object, e As EventArgs) Handles ShipmentDate.ValueChanged

    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Dim tCode As String, tKodeProduk As String = ""
        If LAdd Or LEdit Or LTambahKode Then Exit Sub
        If DGView2.Rows.Count = 0 Then Exit Sub
        tCode = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        tKodeProduk = Trim(Microsoft.VisualBasic.Left(DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(1).Value, 15))
        If tCode = "" Then Exit Sub
        Call IsiSP(tCode, tKodeProduk)
    End Sub

    Private Sub LstPO_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles LstPO.CellContentClick
        If LstPO.RowCount = 0 Then Exit Sub
        If e.ColumnIndex = 0 Then
            If LstPO.Rows(e.RowIndex).Cells(0).Value = True Then
                LstPO.Rows(e.RowIndex).Cells(0).Value = False
            Else
                LstPO.Rows(e.RowIndex).Cells(0).Value = True
            End If
        End If
    End Sub

    Private Sub KodePerajin_TextChanged(sender As Object, e As EventArgs) Handles KodePerajin.TextChanged

    End Sub

    Private Sub tSP_KeyDown(sender As Object, e As KeyEventArgs) Handles tSP.KeyDown

    End Sub

    Private Sub Jumlah_TextChanged(sender As Object, e As EventArgs) Handles Jumlah.TextChanged
        If Trim(Jumlah.Text) = "" Then Jumlah.Text = 0
        If IsNumeric(Jumlah.Text) Then
            Dim temp As Double = Jumlah.Text
            Jumlah.SelectionStart = Jumlah.TextLength
        Else
            Jumlah.Text = 0
        End If
    End Sub

    Private Sub HargaBeliRp_TextChanged(sender As Object, e As EventArgs) Handles HargaBeliRp.TextChanged
        If Trim(HargaBeliRp.Text) = "" Then HargaBeliRp.Text = 0
        If IsNumeric(HargaBeliRp.Text) Then
            Dim temp As Double = HargaBeliRp.Text
            HargaBeliRp.SelectionStart = HargaBeliRp.TextLength
        Else
            HargaBeliRp.Text = 0
        End If
    End Sub

    Private Sub tSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tSP.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarSP("")
        End If
    End Sub

    Private Sub HargaBeliUS_TextChanged(sender As Object, e As EventArgs) Handles HargaBeliUS.TextChanged

    End Sub

    Private Sub tPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tPO.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarSP("")
        End If
    End Sub

    Private Sub NilaiKurs_TextChanged(sender As Object, e As EventArgs) Handles NilaiKurs.TextChanged

    End Sub

    Private Sub tKodeBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tKodeBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarSP("")
        End If
    End Sub

    Private Sub optTPO_CheckedChanged(sender As Object, e As EventArgs) Handles optTPO.CheckedChanged
        If optTPO.Checked = True Then
            NoPO.Visible = False
            lNoPO.Visible = False
            ShipmentDate.Visible = False
            lShipmentDate.Visible = False
            NoPO.Text = ""
            If LAdd Or LEdit Then
                If LAdd Then NoSP.Text = Proses.MaxYNoUrut("NoSP", "t_SP", "L")
                Kode_Importir.Text = "9999A"
                SQL = "Select nama From m_kodeImportir " &
                  " Where KodeImportir = '" & Kode_Importir.Text & "' " &
                  " and aktifyn = 'Y' "
                Importir.Text = Proses.ExecuteSingleStrQuery(SQL)
                NoPO.Text = ""
            End If
        End If
    End Sub

    Private Sub Kode_Perajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Perajin.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_KodePerajin " &
              " Where KodePerajin = '" & Kode_Perajin.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Perajin.Text = dbTable.Rows(0) !nama
                If optPO.Checked = True Then
                    cmdPanelDataDariPO.Visible = True
                    ISIPO2SP()
                Else
                    cmdPanelDataDariPO.Visible = False
                    Kode_Produk.Focus()
                End If
                Kode_Produk.Focus()
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
                    If optPO.Checked = True Then
                        cmdPanelDataDariPO.Visible = True
                        ISIPO2SP()
                    Else
                        cmdPanelDataDariPO.Visible = False
                        Kode_Produk.Focus()
                    End If
                Else
                    Kode_Perajin.Text = ""
                    Perajin.Text = ""
                    Kode_Perajin.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Kode_Importir_TextChanged(sender As Object, e As EventArgs) Handles Kode_Importir.TextChanged
        If Len(Kode_Importir.Text) < 1 Then
            Kode_Importir.Text = ""
            Importir.Text = ""
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ISIPO2SP()
        Dim MsgSQL As String, RSF As New DataTable
        Dim QTYSP As Double, ukuran As String = ""
        LstPO.Rows.Clear()
        LstPO.Visible = False
        'order by lastupd
        MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
            " m_KodeImportir.Nama, T_PO.Jumlah, Tamb_SP, Panjang, Lebar, " &
            " Diameter, tebal, Berat, Tinggi, cur_rp, KodePerajin2 " &
            " From t_PO inner join m_KodeProduk ON " &
            " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
            " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
            " Where t_PO.AktifYN = 'Y' " &
            "  And T_PO.NOPO = '" & NoPO.Text & "' " &
            " ORDER BY T_PO.lastupd  "
        RSF = Proses.ExecuteQuery(MsgSQL)
        If RSF.Rows.Count = 0 Then
            MsgBox("No PO Salah/Tidak Terdaftar !", vbCritical + vbOKOnly, ".:Warning !")
            NoPO.Focus()
            PanelDataDariPO.Visible = False
            Exit Sub
        End If
        PanelDataDariPO.Visible = True
        For a = 0 To RSF.Rows.Count - 1
            Application.DoEvents()
            QTYSP = RSF.Rows(a) !Jumlah - Proses.QTYProdukSP(RSF.Rows(a) !Kode_Produk, NoPO.Text)
            If QTYSP <> 0 Then
                ukuran = ""
                If RSF.Rows(a) !Panjang <> 0 Then ukuran = ukuran + "P = " & Format(RSF.Rows(a) !Panjang, "###,##0.00") + " "
                If RSF.Rows(a) !Lebar <> 0 Then ukuran = ukuran + "L = " & Format(RSF.Rows(a) !Lebar, "###,##0.00") + " "
                If RSF.Rows(a) !Tinggi <> 0 Then ukuran = ukuran + "T = " & Format(RSF.Rows(a) !Tinggi, "###,##0.00") + " "
                If RSF.Rows(a) !Diameter <> 0 Then ukuran = ukuran + "Diameter = " & Format(RSF.Rows(a) !Diameter, "###,##0.00") + " "
                If RSF.Rows(a) !Tebal <> 0 Then ukuran = ukuran + "Tebal = " & Format(RSF.Rows(a) !Tebal, "###,##0.00") + " "
                If RSF.Rows(a) !Berat <> 0 Then ukuran = ukuran + "Berat = " & Format(RSF.Rows(a) !Berat, "###,##0.00") + " "
                CatatanProduk.Text = ""
                Kode_Produk.Text = ""
                Produk.Text = ""
                Jumlah.Text = ""
                HargaBeliRp.Text = ""
                KodePerajin.Text = ""
                LstPO.Rows.Add(0, RSF.Rows(a) !Kode_Produk,
                                Replace(RSF.Rows(a) !Deskripsi, "'", "`"),
                                RSF.Rows(a) !KodePerajin2,
                                Format(QTYSP, "###,##0"),
                                Format(RSF.Rows(a) !cur_rp, "###,##0"), 0, 1,
                                ukuran + RSF.Rows(a) !tamb_SP)
            End If
        Next (a)
        Proses.CloseConn()
        PanelDataDariPO.Location = New Point(14, 215)
        LstPO.ReadOnly = False
        LstPO.Visible = True
    End Sub

    Private Sub Form_SP_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If idRecord.Text = "" Then
            MsgBox("SP yang akan di edit belum di pilih!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        NoSPLama.Text = NoSP.Text
        LAdd = False
        LTambahKode = False
        LEdit = True
        AturTombol(False)
        NoPO.ReadOnly = True
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click

    End Sub

    Private Sub ShipmentDate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ShipmentDate.KeyPress
        If e.KeyChar = Chr(13) Then
            Kode_Perajin.Focus()
        End If
    End Sub

    Private Sub KodePerajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodePerajin.KeyPress
        If e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Or LTambahKode Then Jumlah.Focus()
        End If
    End Sub

    Private Sub HargaBeliRp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaBeliRp.KeyPress
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
                Dim temp As Double = HargaBeliRp.Text
                HargaBeliRp.Text = Format(temp, "###,##0.00")
                HargaBeliRp.SelectionStart = HargaBeliRp.TextLength
            Else
                Jumlah.Text = 0
            End If
            If LAdd Or LEdit Then HargaBeliUS.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub HargaBeliUS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaBeliUS.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HargaBeliUS.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(HargaBeliUS.Text) Then
                Dim temp As Double = HargaBeliUS.Text
                HargaBeliUS.Text = Format(temp, "###,##0.00")
                HargaBeliUS.SelectionStart = HargaBeliUS.TextLength
            Else
                HargaBeliUS.Text = 0
            End If
            If LAdd Or LEdit Then NilaiKurs.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub NilaiKurs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NilaiKurs.KeyPress
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
                Dim temp As Double = NilaiKurs.Text
                NilaiKurs.Text = Format(temp, "###,##0.00")
                NilaiKurs.SelectionStart = NilaiKurs.TextLength
            Else
                Jumlah.Text = 0
            End If
            If LAdd Or LEdit Then TglKirimPerajin.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub


    Private Sub Kode_Importir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Importir.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_kodeImportir " &
              " Where KodeImportir = '" & Kode_Importir.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Importir.Text = dbTable.Rows(0) !nama
                Kode_Perajin.Focus()
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
                    Kode_Perajin.Focus()
                Else
                    Kode_Importir.Text = ""
                    Importir.Text = ""
                    Kode_Importir.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub optPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles optPO.KeyPress
        If e.KeyChar = Chr(13) Then
            NoPO.Focus()
        End If
    End Sub
    Private Function CekDouble(tKodeProduk As String, tNoSP As String) As Double
        Dim MsgSQL As String, RSCek As New DataTable
        MsgSQL = "Select IDRec From t_SP " &
        "Where NoSP = '" & tNoSP & "' " &
        " And KodeProduk = '" & tKodeProduk & "' " &
        " And AktifYN = 'Y' "
        RSCek = Proses.ExecuteQuery(MsgSQL)
        If RSCek.Rows.Count <> 0 Then
            CekDouble = True
        Else
            CekDouble = False
        End If
        Proses.CloseConn()
    End Function

End Class