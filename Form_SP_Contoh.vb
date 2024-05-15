Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_SP_Contoh
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable


    Private Sub cmdPenambahanKode_Click(sender As Object, e As EventArgs) Handles cmdPenambahanKode.Click
        TambahKode()
    End Sub

    Private Sub TambahKode()
        If Trim(NoSP.Text) = "" Then
            MsgBox("No SP masih kosong!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        AturTombol(False)
        KodeProduk.Text = ""
        Produk.Text = ""
        Jumlah.Text = "0"
        TglKirimPerajin.Value = Now()
        HargaBeli.Text = "0"
        TglMasukGudang.Value = Now()
        CatatanSP.Text = ""
        CatatanProduk.Text = ""
        LocGmb1.Text = ""
        ShowFoto("")

        If NoPO.Text = "" Then
            KodeProduk.Focus()
        Else
            NoPO.Focus()
        End If
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        Else
            ' IsiKodeProduk(KodeProduk.Text)
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        NoSP.ReadOnly = True
        cmdSimpan.Visible = tEdit
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_SPContoh " &
            "Where IDRec > '" & IDRecord.Text & "' " &
            " And NoSP = '" & NoSP.Text & "' and aktifYN = 'Y' " &
            "Order By IdRec "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiSP(tIdRec)
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_SPContoh " &
            "where NoSP = '" & NoSP.Text & "' And aktifYN = 'Y' " &
            "Order By tglSP Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiSP(tIdRec)
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_SPContoh " &
            "Where NoSP = '" & NoSP.Text & "' " &
            " And IDRec < '" & IDRecord.Text & "'  and aktifYN = 'Y' " &
            "Order By IdRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiSP(tIdRec)
    End Sub
    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_SPContoh " &
            " where NoSP = '" & NoSP.Text & "'  and aktifYN = 'Y' " &
            "Order By TglSP, IdRec  "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiSP(tIdRec)
    End Sub
    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim BentukSP As Byte = 0, MsgSQL As String
        If SPPO.Checked = True And SPPO_T.Checked = False Then
            BentukSP = 1
        ElseIf SPPO.Checked = False And SPPO_T.Checked = True Then
            BentukSP = 2
        ElseIf SPPO.Checked = False And SPPO_T.Checked = False Then
            MsgBox("Bentuk SP Belum dipilih!", vbCritical + vbOKOnly, ".:ERROR!")
            Exit Sub
        End If
        If Trim(Kode_Importir.Text) = "" Then
            MsgBox("Importir Belum di pilih !", vbCritical + vbOKOnly, ".:Warn!ng...")
            Kode_Importir.Focus()
            Exit Sub
        Else
            SQL = "Select nama From m_kodeImportir " &
              " Where KodeImportir = '" & Kode_Importir.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Importir.Text = dbTable.Rows(0) !nama
            Else
                MsgBox("Importir salah/tidak terdaftar di pilih !", vbCritical + vbOKOnly, ".:Warn!ng...")
                Kode_Importir.Focus()
                Exit Sub
            End If
        End If

        If LAdd Then
            Dim rs04 As New DataTable
            MsgSQL = "Select NoSP From t_SPContoh " &
                    "Where NoSP = '" & NoSP.Text & "' " &
                    "  and aktifYN = 'Y' "
            rs04 = Proses.ExecuteQuery(MsgSQL)
            If rs04.Rows.Count <> 0 Then
                MsgBox("Hi " & Trim(UserID) & "... No SP Contoh : " & NoSP.Text & " sudah pernah di buat", vbCritical, ".:Error!")
                Exit Sub
            End If
        End If
        If LAdd Or LTambahKode Then
            If Trim(NoSP.Text) = "" Then
                MsgBox("No SP tidak boleh kosong !", vbCritical + vbOKOnly, ".:ERROR!")
                Exit Sub
            End If
            If CekKodeProduk() = True Then
                MsgBox("Kode Produk ini sudah pernah di input di SP ini!" & vbCrLf & "Kode ini tidak bisa disimpan!", vbCritical + vbOKOnly, ".:Warning!")
                KodeProduk.Focus()
                Exit Sub
            End If
            If Trim(Jumlah.Text) = "" Then
                MsgBox("Jumlah tidak boleh kosong!", vbCritical, ".:Warning!")
                Exit Sub
            End If
            IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_SPContoh", "SP")
            MsgSQL = "INSERT INTO t_SPContoh(IDRec, NoSP, TglSP, StatusSP, " &
                "BentukSP, No_PO, Kode_Importir, ShipmentDate, Kode_Perajin, Kode_Produk, " &
                "Jumlah, HargaBeli, TglKirimPerajin, TglMasukGudang, CatatanProduk, " &
                "CatatanSP, FotoLoc, UserID, LastUPD, AktifYN, TransferYN, IdCompany) VALUES (" &
                "'" & IDRecord.Text & "', '" & NoSP.Text & "', '" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                "'" & Trim(StatusSP.Text) & "', '" & BentukSP & "', '" & Trim(NoPO.Text) & "', " &
                "'" & Kode_Importir.Text & "','" & Format(tglKirim.Value, "yyyy-MM-dd") & "', " &
                "'" & Kode_Perajin.Text & "', '" & KodeProduk.Text & "', " &
                "" & Jumlah.Text * 1 & ", " & HargaBeli.Text * 1 & ", " &
                "'" & Format(TglKirimPerajin.Value, "yyyy-MM-dd") & "', " &
                "'" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                "'" & Trim(CatatanProduk.Text) & "', '" & Trim(CatatanSP.Text) & "', " &
                "'', '" & UserID & "', GetDate(), 'Y', 'N', 'PEKERTI') "
            Proses.ExecuteNonQuery(MsgSQL)
            TambahKode()
        ElseIf LEdit Then
            MsgSQL = "Update t_SPContoh SET  " &
                "     NoSP = '" & NoSP.Text & "', " &
                "    TglSP = '" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                " BentukSP = '" & BentukSP & "', " &
                "    No_PO = '" & Trim(NoPO.Text) & "', " &
                "  Kode_Importir = '" & Kode_Importir.Text & "', " &
                "   Kode_Perajin = '" & Kode_Perajin.Text & "', " &
                "    Kode_Produk = '" & KodeProduk.Text & "', " &
                "         Jumlah = " & Jumlah.Text * 1 & ", HargaBeli = " & HargaBeli.Text * 1 & ", " &
                " TglKirimPerajin = '" & Format(TglKirimPerajin.Value, "yyyy-MM-dd") & "', " &
                "  TglMasukGudang = '" & Format(TglMasukGudang.Value, "yyyy-MM-dd") & "', " &
                "   CatatanProduk = '" & Trim(CatatanProduk.Text) & "', CatatanSP = '" & Trim(CatatanSP.Text) & "', " &
                "  UserID = '" & UserID & "', LastUPD = GetDate(), TransferYN = 'N'  " &
                " Where IDRec = '" & IDRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)

            MsgSQL = "Update t_SPContoh SET  " &
                "    TglSP = '" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                " BentukSP = '" & BentukSP & "', " &
                "   ShipmentDate = '" & Format(tglKirim.Value, "yyyy-MM-dd") & "', " &
                "  UserID = '" & UserID & "', LastUPD = GetDate(), TransferYN = 'N'  " &
                " Where  NoSP = '" & NoSP.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            LTambahKode = False
            LAdd = False
            LEdit = False
            AturTombol(True)
            DaftarSP()
        End If
    End Sub

    Private Sub DaftarSP()
        Dim MsgSQL As String, rsDaftar As New DataTable
        DGView.Rows.Clear()
        DGView.Visible = False
        DGView2.Rows.Clear()
        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select Distinct NoSP, right(nosp,3) + Left(NoSP,3) , TglSP, No_PO, m_KodeImportir.Nama as Importir, " &
            "m_KodePerajin.Nama as Perajin " &
            "From t_SPContoh inner join m_KodeImportir on KodeImportir = Kode_Importir " &
            "     Inner Join m_KodePerajin on Kode_perajin = KodePerajin " &
            "Where t_SPContoh.AktifYN = 'Y' " &
            "Order By TglSP desc, right(nosp,3) + Left(NoSP,3) desc"
        rsDaftar = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsDaftar.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsDaftar.Rows(a) !NoSP,
                    Format(rsDaftar.Rows(a) !TglSP, "dd-MM-yyyy"),
                    rsDaftar.Rows(a) !Perajin,
                    rsDaftar.Rows(a) !No_PO,
                    rsDaftar.Rows(a) !Importir)
        Next (a)
        Me.Cursor = Cursors.Default
        DGView.Visible = True
    End Sub
    Private Sub IsiSP(tCode As String)
        If LAdd Or LEdit Or LTambahKode Then Exit Sub
        Dim rsc As New DataTable, msgSQL As String
        If tCode = "" Then
            Exit Sub
        End If
        msgSQL = "SELECT t_SPContoh.*, file_foto " &
            " FROM t_SPContoh inner join m_KodeProduk on KodeProduk = Kode_Produk " &
            "Where t_SPContoh.AktifYN = 'Y' " &
            "  And t_SPContoh.IDRec = '" & tCode & "' "
        rsc = Proses.ExecuteQuery(msgSQL)

        If rsc.Rows.Count <> 0 Then
            IDRecord.Text = rsc.Rows(0) !IDRec
            NoSP.Text = rsc.Rows(0) !NoSP
            TglSP.Value = rsc.Rows(0) !TglSP
            If rsc.Rows(0) !BentukSP = "1" Then
                SPPO.Checked = True
                SPPO_T.Checked = False
            Else
                SPPO.Checked = False
                SPPO_T.Checked = True
            End If
            NoPO.Text = rsc.Rows(0) !No_PO
            If rsc.Rows(0) !No_PO <> "" Then
                SPPO.Checked = True
                SPPO_T.Checked = False
            Else
                SPPO.Checked = False
                SPPO_T.Checked = True
            End If
            Kode_Importir.Text = rsc.Rows(0) !Kode_Importir
            tglKirim.Value = rsc.Rows(0) !ShipmentDate
            Kode_Perajin.Text = rsc.Rows(0) !Kode_Perajin
            KodeProduk.Text = rsc.Rows(0) !Kode_Produk
            Jumlah.Text = rsc.Rows(0) !Jumlah
            HargaBeli.Text = Format(rsc.Rows(0) !HargaBeli, "###,##0")
            TglKirimPerajin.Value = rsc.Rows(0) !TglKirimPerajin
            TglMasukGudang.Value = rsc.Rows(0) !TglMasukGudang
            CatatanProduk.Text = rsc.Rows(0) !CatatanProduk
            CatatanSP.Text = rsc.Rows(0) !CatatanSP
            LocGmb1.Text = rsc.Rows(0) !file_foto
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
            StatusSP.Text = rsc.Rows(0) !StatusSP
            msgSQL = "Select Nama From m_KodeImportir Where KodeImportir = '" & Kode_Importir.Text & "' "
            Importir.Text = Proses.ExecuteSingleStrQuery(msgSQL)
            msgSQL = "Select Nama From m_KodePerajin Where KodePerajin = '" & Kode_Perajin.Text & "' "
            Perajin.Text = Proses.ExecuteSingleStrQuery(msgSQL)
            msgSQL = "Select Deskripsi From m_KodeProduk where kodeproduk = '" & Trim(KodeProduk.Text) & "' "
            Produk.Text = Proses.ExecuteSingleStrQuery(msgSQL)
        Else
            ClearTextBoxes()
        End If
        Proses.CloseConn()
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

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

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
        If LTambahKode Then
            'If NoPO.Visible Then NoPO.Locked = True
            If tglKirim.Visible Then tglKirim.Enabled = False
            Kode_Importir.ReadOnly = True
            Importir.ReadOnly = True
        End If
        NoSP.ReadOnly = True
        Me.Text = "SP Contoh dengan Kode"

    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        ClearTextBoxes()
        AturTombol(False)
        NoSP.Text = Proses.MaxYNoUrut("NoSP", "t_SPContoh", "C")
        NoSP.ReadOnly = False
        TglSP.Focus()
    End Sub

    Private Sub SPPO_T_CheckedChanged(sender As Object, e As EventArgs) Handles SPPO_T.CheckedChanged
        If SPPO_T.Checked = True Then
            NoPO.Visible = False
            LabelNoPO.Visible = False
            tglKirim.Value = "1900-01-01"
            tglKirim.Visible = False
            LabeltglKirim.Visible = False
            If LAdd Then
                KodeProduk.Text = ""
                NoSP.Text = Proses.MaxYNoUrut("NoSP", "t_SPContoh", "C")
            End If
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
        tglKirim.Value = Now
        TglKirimPerajin.Value = Now
        TglMasukGudang.Value = Now
        TglSP.Value = Now
    End Sub

    Private Sub SPPO_CheckedChanged(sender As Object, e As EventArgs) Handles SPPO.CheckedChanged
        If SPPO.Checked = True Then
            NoPO.Visible = True
            LabelNoPO.Visible = True
            tglKirim.Value = Now
            tglKirim.Visible = True
            LabeltglKirim.Visible = True
            If LAdd Then
                KodeProduk.Text = ""
                NoSP.Text = Proses.MaxYNoUrut("NoSP", "t_SPContoh", "C")
            End If
        End If
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation) = vbYes Then
            MsgSQL = "Update t_SPContoh SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & IDRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            Dim rs As New DataTable
            MsgSQL = "Select Top 1 * From t_SPContoh " &
                "Where IDRec > '" & IDRecord.Text & "' " &
                " And NoSP = '" & NoSP.Text & "' and aktifYN = 'Y' " &
                "Order By IdRec "
            rs = Proses.ExecuteQuery(MsgSQL)
            If rs.Rows.Count <> 0 Then
                btnNext_Click(sender, e)
            Else
                btnTop_Click(sender, e)
            End If
        End If
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

    Private Function CekKodeProduk() As Boolean
        Dim MsgSQL As String, rsCek As New DataTable
        Dim tResult As Boolean = False
        If LAdd Or LEdit Or LTambahKode Then
            MsgSQL = "Select * " &
                "From t_SPContoh " &
                "Where NoSP = '" & NoSP.Text & "'" &
                " And Kode_Produk =  '" & KodeProduk.Text & "'  " &
                " And AktifYN = 'Y'"
            rsCek = Proses.ExecuteQuery(MsgSQL)
            If rsCek.Rows.Count > 0 Then
                tResult = True
            Else
                tResult = False
            End If
        End If
        CekKodeProduk = tResult
    End Function

    Private Sub HargaBeli_TextChanged(sender As Object, e As EventArgs) Handles HargaBeli.TextChanged
        If Trim(HargaBeli.Text) = "" Then HargaBeli.Text = 0
        If IsNumeric(HargaBeli.Text) Then
            Dim temp As Double = HargaBeli.Text
            HargaBeli.SelectionStart = HargaBeli.TextLength
        Else
            HargaBeli.Text = 0
        End If
    End Sub

    Private Sub Form_SP_Contoh_Load(sender As Object, e As EventArgs) Handles Me.Load
        CekTable()
        LAdd = False
        LEdit = False
        LTambahKode = False

        DGView.Rows.Clear()

        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()

        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_SPContoh " &
            "where AktifYN = 'Y' " &
            "Order By tglSP Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiSP(tIdRec)

        Me.Cursor = Cursors.WaitCursor
        If Format(Now, "YYMMDD") < "131231" Then
            MsgSQL = "SELECT t_SPContoh.IDRec AS IDSP, t_SPContoh.TglSP, " &
            "t_DPBSample.TglDPB " &
            "FROM t_SPContoh INNER JOIN t_DPBSample ON " &
            "     t_SPContoh.NoSP = t_DPBSample.NoSP " &
            "     AND t_SPContoh.Kode_Produk = t_DPBSample.KodeProduk " &
            "Where t_SPContoh.TglSP <> t_DPBSample.TglDPB " &
            "  And t_SPContoh.AktifYN = 'Y' " &
            "  And Year(TglSP) > = 2011 " &
            "ORDER BY t_SPContoh.IDRec "
            Rs = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To Rs.Rows.Count - 1
                Application.DoEvents()
                MsgSQL = "Update t_SPContoh Set " &
                    "TglSP = '" & Rs.Rows(a) !TglDPB & "' " &
                    "Where IdRec = '" & Rs.Rows(a) !idSP & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next (a)
        End If
        tTambah = Proses.UserAksesTombol(UserID, "25_KODIF_PRODUK", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "25_KODIF_PRODUK", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "25_KODIF_PRODUK", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "25_KODIF_PRODUK", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarSP()
    End Sub

    Private Sub SetDataGrid()
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
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String, RSP As New DataTable
        Dim TPesan As Double, tNilai As Double, TMacam As Integer
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Visible = False
        DGView2.Rows.Clear()
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        TMacam = 0
        TPesan = 0
        tNilai = 0
        MsgSQL = "SELECT  t_SPContoh.*, Deskripsi as produk " &
            "From t_SPContoh inner join m_KodeImportir on KodeImportir = Kode_Importir " &
            " Inner Join m_KodePerajin on Kode_perajin = KodePerajin " &
            " Inner Join m_KodeProduk ON Kode_Produk = KodeProduk " &
            "Where t_SPContoh.AktifYN = 'Y' " &
            "  And NoSP = '" & tCari & "' " &
            "Order By IDRec "
        RSP = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSP.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSP.Rows(a) !IDRec,
                RSP.Rows(a) !Kode_Produk,
                RSP.Rows(a) !Produk,
                Format(RSP.Rows(a) !Jumlah, "###,##0"),
                Format(RSP.Rows(a) !HargaBeli, "###,##0"),
                Format(RSP.Rows(a) !Jumlah * RSP.Rows(a) !HargaBeli, "###,##0"))
            TMacam = TMacam + 1
            TPesan = TPesan + RSP.Rows(a) !Jumlah
            tNilai = tNilai + (RSP.Rows(a) !Jumlah * RSP.Rows(a) !HargaBeli)
        Next (a)
        QTYMacam.Text = Format(TMacam, "###,##0")
        QTYPesan.Text = Format(TPesan, "###,##0")
        NilaiPesan.Text = Format(tNilai, "###,##0")
        If DGView2.Rows.Count <> 0 Then
            DGView2.Focus()
            tCari = DGView2.Rows(0).Cells(0).Value
            IsiSP(tCari)
        End If
        DGView2.Visible = True
    End Sub

    Private Sub Kode_Perajin_TextChanged(sender As Object, e As EventArgs) Handles Kode_Perajin.TextChanged
        If Len(Kode_Perajin.Text) < 1 Then
            Kode_Perajin.Text = ""
            Perajin.Text = ""
        End If
    End Sub

    Private Sub Kode_Importir_TextChanged(sender As Object, e As EventArgs) Handles Kode_Importir.TextChanged
        If Len(Kode_Importir.Text) < 1 Then
            Kode_Importir.Text = ""
            Importir.Text = ""
        End If
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Dim tCari As String
        If DGView2.Rows.Count <> 0 Then
            tCari = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
            If LAdd Or LEdit Or LTambahKode Then Exit Sub
            IsiSP(tCari)
        End If
    End Sub

    Private Sub NoPO_TextChanged(sender As Object, e As EventArgs) Handles NoPO.TextChanged
        If Len(Trim(NoPO.Text)) < 1 Then
            KodeProduk.Text = ""
        End If
    End Sub

    'Public Function MaxYNoUrut(tField As String, tTable As String, Kode As String) As String
    '    Dim MsgSQL As String, RsMax As New DataTable
    '    MsgSQL = "Select convert(Char(2), GetDate(), 12) TGL, isnull(Max(left(" & tField & ",3)),0) + 1000001 RecId " &
    '        " From " & tTable & " " &
    '        "Where Right(" & tField & ",2) = convert(Char(2), GetDate(), 12) and aktifYN = 'Y'  "
    '    RsMax = Proses.ExecuteQuery(MsgSQL)
    '    MaxYNoUrut = Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 3) + "/" + Kode + "/" +
    '        Trim(Str(RsMax.Rows(0) !tGL))
    'End Function

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged
        If Len(Trim(KodeProduk.Text)) < 1 Then
            KodeProduk.Text = ""
            Produk.Text = ""
            Jumlah.Text = 0
            HargaBeli.Text = 0
            CatatanProduk.Text = ""
            CatatanSP.Text = ""
        ElseIf Len(trim(KodeProduk.Text)) = 5 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        ElseIf Len(trim(KodeProduk.Text)) = 8 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        End If
    End Sub


    Private Sub Jumlah_GotFocus(sender As Object, e As EventArgs) Handles Jumlah.GotFocus
        With Jumlah
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
                Jumlah.Text = Replace(Format(temp, "###,##0.00"), ".00", "")
                Jumlah.SelectionStart = Jumlah.TextLength
            Else
                Jumlah.Text = 0
            End If
            If LAdd Or LEdit Then HargaBeli.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub HargaBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaBeli.KeyPress
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
                Dim temp As Double = HargaBeli.Text
                HargaBeli.Text = Replace(Format(temp, "###,##0.00"), ".00", "")
                HargaBeli.SelectionStart = HargaBeli.TextLength
            Else
                Jumlah.Text = 0
            End If
            If LAdd Or LEdit Then TglKirimPerajin.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub HargaBeli_GotFocus(sender As Object, e As EventArgs) Handles HargaBeli.GotFocus
        With HargaBeli
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub tglKirim_ValueChanged(sender As Object, e As EventArgs) Handles tglKirim.ValueChanged

    End Sub

    Private Sub Kode_Perajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Perajin.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_KodePerajin " &
              " Where KodePerajin = '" & Kode_Perajin.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Perajin.Text = dbTable.Rows(0) !nama
                KodeProduk.Focus()
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
                    KodeProduk.Focus()
                Else
                    Kode_Perajin.Text = ""
                    Perajin.Text = ""
                    Kode_Perajin.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub TglKirimPerajin_ValueChanged(sender As Object, e As EventArgs) Handles TglKirimPerajin.ValueChanged

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
                    "  And ( KodeImportir Like '%9999%' ) " &
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

    Private Sub TglMasukGudang_ValueChanged(sender As Object, e As EventArgs) Handles TglMasukGudang.ValueChanged

    End Sub

    Private Sub NoPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPO.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO " &
                " From T_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
                "Where T_PO.AktifYN = 'Y' " &
                "  and nopo = '" & NoPO.Text & "' " &
                "Group By NoPO, m_KodeImportir.Nama, TglPO " &
                "Order By TglPO Desc, NoPO Desc "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NoPO.Text = dbTable.Rows(0) !nopo
                Kode_Importir.Focus()
            Else
                Dim dbpo As New DataTable

                SQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO, KodeImportir, max(t_PO.tglKirim) tglKirim " &
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
                    Importir.Focus()
                Else
                    NoPO.Text = ""
                    Kode_Importir.Text = ""
                    Importir.Text = ""
                    NoPO.Focus()
                End If

            End If
        End If
    End Sub
    Private Sub IsiProdukTanpaPO()
        Dim RS As New DataTable, Ukuran As String = ""
        SQL = "Select isnull(deskripsi,'') deskripsi, cur_rp, tamb_sp, perajin, kode_perajin, " &
            "Panjang, Lebar, Tinggi, Diameter, Tebal, Berat,file_foto " &
            " From m_KodeProduk " &
            "Where KodeProduk = '" & KodeProduk.Text & "' and aktifYN ='Y' "
        RS = Proses.ExecuteQuery(SQL)
        HargaBeli.Text = Format(RS.Rows(0) !cur_rp, "###,##0")

        If RS.Rows(0) !Panjang <> 0 Then Ukuran = Ukuran + "P = " & Format(RS.Rows(0) !Panjang, "###,##0.00") + " "
        If RS.Rows(0) !Lebar <> 0 Then Ukuran = Ukuran + "L = " & Format(RS.Rows(0) !Lebar, "###,##0.00") + " "
        If RS.Rows(0) !Tinggi <> 0 Then Ukuran = Ukuran + "T = " & Format(RS.Rows(0) !Tinggi, "###,##0.00") + " "
        If RS.Rows(0) !Diameter <> 0 Then Ukuran = Ukuran + "Diameter = " & Format(RS.Rows(0) !Diameter, "###,##0.00") + " "
        If RS.Rows(0) !Tebal <> 0 Then Ukuran = Ukuran + "Tebal = " & Format(RS.Rows(0) !Tebal, "###,##0.00") + " "
        If RS.Rows(0) !Berat <> 0 Then Ukuran = Ukuran + "Berat = " & Format(RS.Rows(0) !Berat, "###,##0.00") + " "
        CatatanProduk.Text = Ukuran + RS.Rows(0) !tamb_SP
        Produk.Text = Replace(RS.Rows(0) !Deskripsi, "'", "`")
        LocGmb1.Text = RS.Rows(0) !file_foto
        If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
            ShowFoto("")
        Else
            ShowFoto(LocGmb1.Text)
        End If
    End Sub
    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress
        If e.KeyChar = Chr(13) Then

            Dim mKondisi As String = "", RS05 As New DataTable, MsgSQL As String = ""
            Dim Ukuran As String = ""
            '-------sppo_t
            Me.Cursor = Cursors.WaitCursor
            If SPPO_T.Checked = True Then
                MsgSQL = "Select deskripsi, cur_rp, tamb_sp, perajin, kode_perajin, " &
                    "Panjang, Lebar, Tinggi, Diameter, Tebal, Berat " &
                    " From m_KodeProduk " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' and kodeproduk <> '' "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                If RS05.Rows.Count <> 0 Then
                    IsiProdukTanpaPO()
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
                    RS05 = Proses.ExecuteQuery(MsgSQL)
                    If RS05.Rows.Count <> 0 Then
                        IsiProdukTanpaPO()
                    Else
                        Produk.Text = ""
                        CatatanProduk.Text = ""
                        KodeProduk.Focus()
                    End If
                End If
                '-------sppo
            ElseIf SPPO.Checked = True Then
                Dim RSP As New DataTable
                mKondisi = " "
                MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                    " m_KodeImportir.Nama, T_PO.Jumlah, Tamb_SP, Panjang, Lebar, " &
                    " Diameter, tebal, Berat, Tinggi, cur_rp, file_foto " &
                    " From t_PO inner join m_KodeProduk ON " &
                    " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' " &
                    "  And t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & Trim(NoPO.Text) & "'  "
                RSP = Proses.ExecuteQuery(MsgSQL)
                If RSP.Rows.Count <> 0 Then
                    If CEKProdukSP(KodeProduk.Text, NoPO.Text) Then
                        MsgBox(KodeProduk.Text + " Sudah Pernah di input untuk SP ini!", vbCritical, ".:Double Input!")
                        KodeProduk.Text = ""
                        Produk.Text = ""
                        Jumlah.Text = 0
                        CatatanProduk.Text = ""
                        CatatanSP.Text = ""
                        HargaBeli.Text = 0
                        Me.Cursor = Cursors.Default
                        KodeProduk.Enabled = True
                        KodeProduk.Focus()
                        Exit Sub
                    Else
                        Ukuran = ""
                        If RSP.Rows(0) !Panjang <> 0 Then Ukuran = Ukuran + "P = " & Format(RSP.Rows(0) !Panjang, "###,##0.00") + " "
                        If RSP.Rows(0) !Lebar <> 0 Then Ukuran = Ukuran + "L = " & Format(RSP.Rows(0) !Lebar, "###,##0.00") + " "
                        If RSP.Rows(0) !Tinggi <> 0 Then Ukuran = Ukuran + "T = " & Format(RSP.Rows(0) !Tinggi, "###,##0.00") + " "
                        If RSP.Rows(0) !Diameter <> 0 Then Ukuran = Ukuran + "Diameter = " & Format(RSP.Rows(0) !Diameter, "###,##0.00") + " "
                        If RSP.Rows(0) !Tebal <> 0 Then Ukuran = Ukuran + "Tebal = " & Format(RSP.Rows(0) !Tebal, "###,##0.00") + " "
                        If RSP.Rows(0) !Berat <> 0 Then Ukuran = Ukuran + "Berat = " & Format(RSP.Rows(0) !Berat, "###,##0.00") + " "
                        CatatanProduk.Text = Ukuran + RSP.Rows(0) !tamb_SP
                        Produk.Text = Replace(RSP.Rows(0) !Deskripsi, "'", "`")
                        Jumlah.Text = RSP.Rows(0) !Jumlah
                        CatatanProduk.Text = RSP.Rows(0) !tamb_SP
                        HargaBeli.Text = RSP.Rows(0) !cur_rp
                        LocGmb1.Text = RSP.Rows(0) !file_foto
                        If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                            ShowFoto("")
                        Else
                            ShowFoto(LocGmb1.Text)
                        End If
                    End If
                Else
                    KodeProduk.Text = FindKodeProdukDPL(KodeProduk.Text, NoPO.Text)
                End If
            End If
            Me.Cursor = Cursors.Default
            If LAdd Or LEdit Then
                If Produk.Text = "" Then
                    KodeProduk.Focus()
                Else
                    Jumlah.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub CekTable()
        SQL = "SELECT *  FROM information_schema.COLUMNS " &
        "WHERE TABLE_NAME = 't_SPContoh'  " &
        "  And column_name = 'IDCompany' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "ALTER TABLE t_SPContoh ADD IDCompany Varchar(10) "
            Proses.ExecuteNonQuery(SQL)
            SQL = "UPDATE t_SPContoh SET IDCompany = 'PEKERTI' "
            Proses.ExecuteNonQuery(SQL)
        End If
        SQL = "SELECT IDCompany FROM t_SPContoh WHERE idCompany is Null "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            SQL = "UPDATE t_SPContoh set idCompany = 'PEKERTI' "
            Proses.ExecuteNonQuery(SQL)
        End If

        SQL = "SELECT *  FROM information_schema.COLUMNS " &
             "WHERE TABLE_NAME = 'm_company' " &
             "  And column_name = 'BagianContoh' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "ALTER TABLE m_company ADD BagianContoh Varchar(50)  "
            Proses.ExecuteNonQuery(SQL)
            SQL = "UPDATE m_Company SET BagianContoh = 'Dhea' "
            Proses.ExecuteNonQuery(SQL)
        End If

    End Sub
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click


        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable, clsTerbilang As New Terbilang
        Dim totalSPContoh As Double = 0, tanggal As String = "", terbilang As String = ""

        Me.Cursor = Cursors.WaitCursor
        SQL = "Select isNull(Sum(Jumlah * HargaBeli),0) JValue " &
            "From Pekerti.dbo.t_SPContoh  " &
            "Where t_SPContoh.NoSP = '" & NoSP.Text & "' " &
            "  And t_SPContoh.AktifYn = 'Y' "
        dttable = Proses.ExecuteQuery(SQL)
        If dttable.Rows.Count <> 0 Then
            totalSPContoh = dttable.Rows(0) !Jvalue
        Else
            totalSPContoh = 0
        End If
        If totalSPContoh = 0 Then
            terbilang = "-"
        Else
            terbilang = "- " + clsTerbilang.CurrencyText(totalSPContoh, "RP") + " -"
        End If

        SQL = "Select tglSP " &
            "From Pekerti.dbo.t_SPContoh  " &
            "Where t_SPContoh.NoSP = '" & NoSP.Text & "' " &
            "  And t_SPContoh.AktifYn = 'Y' "
        dttable = Proses.ExecuteQuery(SQL)
        If dttable.Rows.Count <> 0 Then
            tanggal = "Jakarta, " & Proses.TglIndo(Format(dttable.Rows(0) !TglSP, "dd-MM-yyyy"))
        End If

        Proses.OpenConn(CN)
        dttable = New DataTable

        SQL = "Select t_SPContoh.IDRec, t_SPContoh.NoSP, t_SPContoh.Kode_Produk, " &
            "     Jumlah, HargaBeli, CatatanSP, m_KodeImportir.Nama Nama_Importir, " &
            "     TglSP, m_KodePerajin.Nama Nama_Perajin, m_KodePerajin.Alamat, " &
            "     m_KodeProduk.Deskripsi, m_KodeProduk.Satuan, " &
            "     m_KodeProduk.KodePerajin2, Direksi, TTDireksi, BagianContoh " &
            "FROM t_SPContoh INNER JOIN  m_KodePerajin ON " &
            "           t_SPContoh.Kode_Perajin = m_KodePerajin.KodePerajin " &
            "     INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk On " &
            "           t_SPContoh.Kode_Produk = m_KodeProduk.KodeProduk " &
            "     INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
            "           t_SPContoh.Kode_Importir = m_KodeImportir.KodeImportir " &
            "     INNER JOIN Pekerti.dbo.m_Company m_Company ON " &
            "           t_SPContoh.IDCompany = m_Company.CompCode  " &
            "WHERE T_SPContoh.NoSP = '" & NoSP.Text & "' " &
            "  AND T_SPContoh.AktifYN = 'Y' " &
            "ORDER BY TglSP "

        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_SPContoh
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("tanggal", tanggal)
            objRep.SetParameterValue("Terbilang", terbilang)
            Form_Report.Text = "Cetak SP Contoh"
            Form_Report.CrystalReportViewer1.ShowExportButton = True
            Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_Report.CrystalReportViewer1.Refresh()
            Form_Report.CrystalReportViewer1.ReportSource = objRep
            Form_Report.CrystalReportViewer1.ShowRefreshButton = False
            Form_Report.CrystalReportViewer1.ShowPrintButton = True
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
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub TglSP_ValueChanged(sender As Object, e As EventArgs) Handles TglSP.ValueChanged

    End Sub

    Public Function FindKodeProdukDPL(tKodeProduk As String, tNoPO As String) As String
        Dim RSD As New DataTable, mKondisi As String, hasil As String = ""
        Dim MsgSQL As String, ukuran As String, rKodeProduk As String = ""
        If Trim(tKodeProduk) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And Deskripsi like '%" & Trim(tKodeProduk) & "%' "
        End If
        FrmMenuUtama.TSKeterangan.Text = ""
        MsgSQL = "SELECT Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
            "      m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah, file_foto " &
            " FROM t_PO inner join m_KodeProduk ON " &
            "      m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
            "      INNER JOIN m_KodeImportir on Kode_Importir = KodeImportir " &
            "WHERE t_PO.AktifYN = 'Y' " &
            "  And T_PO.NOPO = '" & tNoPO & "' " &
            "  " & mKondisi & " " &
            "ORDER BY kode_buyer"
        Form_Daftar.Text = "Daftar Produk PO"
        Form_Daftar.param1.Text = tNoPO
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.ShowDialog()
        rKodeProduk = FrmMenuUtama.TSKeterangan.Text

        MsgSQL = "SELECT Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
            "      m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah, m_KodeProduk.* " &
            " FROM t_PO inner join m_KodeProduk On " &
            "      m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
            "      INNER JOIN m_KodeImportir On Kode_Importir = KodeImportir " &
            "WHERE t_PO.AktifYN = 'Y' " &
            "  AND T_PO.NOPO = '" & tNoPO & "' " &
            "  AND Kode_Produk = '" & rKodeProduk & "' "
        RSD = Proses.ExecuteQuery(MsgSQL)
        If RSD.Rows.Count <> 0 Then
            KodeProduk.Text = rKodeProduk
            If CEKProdukSP(KodeProduk.Text, tNoPO) Then
                MsgBox(KodeProduk.Text + " Sudah Pernah di input untuk SP ini!", vbCritical, ".:Double Input!")
                KodeProduk.Text = ""
                Produk.Text = ""
                Jumlah.Text = 0
                CatatanProduk.Text = ""
                CatatanSP.Text = ""
                HargaBeli.Text = 0
                Me.Cursor = Cursors.Default
                KodeProduk.Enabled = True
                KodeProduk.Focus()
                rKodeProduk = ""
                hasil = 0
                'Exit Function
            Else
                ukuran = ""
                If RSD.Rows(0) !Panjang <> 0 Then ukuran = ukuran + "P = " & Format(RSD.Rows(0) !Panjang, "###,##0.00") + " "
                If RSD.Rows(0) !Lebar <> 0 Then ukuran = ukuran + "L = " & Format(RSD.Rows(0) !Lebar, "###,##0.00") + " "
                If RSD.Rows(0) !Tinggi <> 0 Then ukuran = ukuran + "T = " & Format(RSD.Rows(0) !Tinggi, "###,##0.00") + " "
                If RSD.Rows(0) !Diameter <> 0 Then ukuran = ukuran + "Diameter = " & Format(RSD.Rows(0) !Diameter, "###,##0.00") + " "
                If RSD.Rows(0) !Tebal <> 0 Then ukuran = ukuran + "Tebal = " & Format(RSD.Rows(0) !Tebal, "###,##0.00") + " "
                If RSD.Rows(0) !Berat <> 0 Then ukuran = ukuran + "Berat = " & Format(RSD.Rows(0) !Berat, "###,##0.00") + " "
                CatatanProduk.Text = ukuran + RSD.Rows(0) !tamb_SP
                Produk.Text = Replace(RSD.Rows(0) !Deskripsi, "'", "`")
                Jumlah.Text = Format(RSD.Rows(0) !Jumlah, "###,##0")
                CatatanProduk.Text = RSD.Rows(0) !tamb_SP
                HargaBeli.Text = Format(RSD.Rows(0) !cur_rp, "###,##0")
                LocGmb1.Text = RSD.Rows(0) !file_foto
                If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                    ShowFoto("")
                Else
                    ShowFoto(LocGmb1.Text)
                End If
                hasil = RSD.Rows(0) !Kode_Produk
            End If

        End If
        FindKodeProdukDPL = hasil
    End Function

    Private Function CEKProdukSP(tKodeProduk As String, tNoSP As String) As Boolean
        Dim MsgSQL As String, RSCek As New DataTable
        MsgSQL = "Select IDRec From t_SPContoh " &
            "Where No_po = '" & tNoSP & "' " &
            " And Kode_Produk = '" & tKodeProduk & "' " &
            " And AktifYN = 'Y' "
        RSCek = Proses.ExecuteQuery(MsgSQL)
        If RSCek.Rows.Count <> 0 Then
            CEKProdukSP = True
        Else
            CEKProdukSP = False
        End If
    End Function

    Private Sub tglKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglKirim.KeyPress
        If e.KeyChar = Chr(13) Then
            Kode_Perajin.Focus()
        End If
    End Sub

    Private Sub TglKirimPerajin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglKirimPerajin.KeyPress
        If e.KeyChar = Chr(13) Then
            TglMasukGudang.Focus()
        End If
    End Sub

    Private Sub TglMasukGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglMasukGudang.KeyPress
        If e.KeyChar = Chr(13) Then
            CatatanProduk.Focus()
        End If
    End Sub


    Private Sub TabControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseClick
        'Try
        '    MsgBox(TabControl1.SelectedIndex.ToString)
        '    MsgBox(TabControl1.SelectedTab.Name)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        If TabControl1.SelectedIndex.ToString = "1" Then
            DaftarSP()
        End If
    End Sub

    Private Sub KodeProduk_GotFocus(sender As Object, e As EventArgs) Handles KodeProduk.GotFocus
        With KodeProduk
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub TglSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglSP.KeyPress
        If e.KeyChar = Chr(13) Then
            SPPO.Focus()

        End If
    End Sub

    Private Sub SPPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SPPO.KeyPress
        If e.KeyChar = Chr(13) Then
            NoPO.Focus()
        End If
    End Sub

    Private Sub SPPO_T_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SPPO_T.KeyPress
        If e.KeyChar = Chr(13) Then
            Kode_Importir.Focus()
        End If
    End Sub
End Class