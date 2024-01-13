Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class Form_UangMuka
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        lKoordinator As String, lPemeriksa As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub
    'Private DA As SqlDataAdapter
    'Private CN As SqlConnection
    'Private Cmd As SqlCommand
    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        cmbJenisBayar.Focus()
        cmbJenisBayar.SelectedItem = 0
    End Sub

    Private Sub nodpb_TextChanged(sender As Object, e As EventArgs) Handles nodpb.TextChanged

    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim tUangMuka As Double = 0, tNilaiBayar As Double = 0
        If Trim(UangMuka.Text) = "" Then UangMuka.Text = 0
        If cmbJenisBayar.Text = "UANG MUKA" Then
            tUangMuka = UangMuka.Text * 1
            tNilaiBayar = 0
        ElseIf cmbJenisBayar.Text = "PELUNASAN" Then
            tUangMuka = 0
            tNilaiBayar = UangMuka.Text * 1
        End If
        If LAdd Then
            If Trim(UangMuka.Text) = "" Then UangMuka.Text = 0
            If Trim(Realisasi.Text) = "" Then Realisasi.Text = 0
            If Trim(NilaiSP.Text) = "" Then NilaiSP.Text = 0

            If (Trim(UangMuka.Text) * 1) + (Trim(Realisasi.Text) * 1) > (Trim(NilaiSP.Text) * 1) Then
                MsgBox("Nilai Bayar melebihi Nilai SP!", vbCritical + vbOKOnly, ".:ERROR!")
                UangMuka.Focus()
                Exit Sub
            End If

            If Not ValidBayar() Then
                MsgBox("Sudah dibilang ga boleh lebih dari 50% koq maksa!", vbCritical, "Bandel yo sampean...!")
                Exit Sub
            End If

            idRecord.Text = Proses.MaxNoUrut("IDRec", "t_UangMuka", "BY")
            If Trim(tPotongan.Text) = "" Then tPotongan.Text = 0
            If Trim(Realisasi.Text) = "" Then Realisasi.Text = 0
            If Trim(KurangBayar.Text) = "" Then KurangBayar.Text = 0
            MsgSQL = "INSERT INTO t_UangMuka(IDRec, tglPengajuan, JenisBayar, NoSP, " &
                "NoDPB, Perajin, Kode_Perajin, NilaiSP, NilaiDPB, TglSP, Pengajuan, " &
                "PembayaranKe, Realisasi, BuktiBayar, TglBayar, Keterangan, " &
                "AktifYN, UserID, LastUPD, TransferYN, TotalPotongan, " &
                "TotalUM, TotalBayar, Pelunasan) VALUES ('" & idRecord.Text & "', " &
                "'" & Format(TglPengajuan.Value, "yyyy-MM-dd") & "', " &
                "'" & Trim(cmbJenisBayar.Text) & "', '" & NoSP.Text & "', " &
                "'" & nodpb.Text & "', '" & Trim(nama.Text) & "', " &
                "'" & Kode_Perajin.Text & "', " & NilaiSP.Text * 1 & ", " &
                "" & NilaiDPB.Text * 1 & ", '" & Format(TglSP.Value, "yyyy-MM-dd") & "', " &
                "" & UangMuka.Text * 1 & ", " & PembayaranKe.Text * 1 & ", " &
                "0, '', '', '" & Trim(Keterangan.Text) & "', 'Y', '" & UserID & "', " &
                "GetDate(), 'N', " & tPotongan.Text * 1 & ", " & Realisasi.Text * 1 & ", " &
                "" & KurangBayar.Text * 1 & ", 0 )"
            Proses.ExecuteNonQuery(MsgSQL)
        ElseIf LEdit Then
            'MsgSQL = "Update t_UangMuka SET " & _
            '"JenisBayar = '" & cmbJenisBayar.Text & "', " & _
            '" NoSP = '" & NoSP.Text & "', " & _
            '" NilaiSP = " & NilaiSP.Text * 1 & ", " & _
            '" Nama = '" & Trim(Nama.Text) & "', " & _
            '" TglSP = '" & Format(TglSP.Value, "yyyy-mm-dd") & "', " & _
            '" NoDPB = '" & nodpb.Text & "', " & _
            '" UangMuka = " & UangMuka.Text * 1 & ", " & _
            '" NilaiBayar = " & NilaiBayar.Text * 1 & "," & _
            '" PembayaranKe = " & PembayaranKe.Text * 1 & ", tglPengajuan = '" & Format(TglPengajuan.Value, "yyyy-mm-dd") & "', " & _
            '" BuktiBayar = '" & Trim(BuktiBayar.Text) & "', Keterangan = '" & Trim(Keterangan.Text) & "', " & _
            '" UserID = '" & UserId & "', LastUPD = GetDate() " & _
            '" Where IDRec = '" & Trim(IDRecord.Text) & "' "
            'ConnSQL.Execute MsgSQL
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub
    Private Function ValidBayar() As Boolean
        Dim P50 As Double, P70 As Double
        If NilaiSP.Text = "" Then
            NilaiSP.Text = 0
            ValidBayar = True
            MsgBox("Nilai SP Kosong BOS...!", vbCritical + vbOKOnly, "Field tidak boleh kosong!")
            Exit Function
        End If
        ValidBayar = True
        P50 = NilaiSP.Text * 50 / 100
        P70 = NilaiSP.Text * 70 / 100

        If cmbJenisBayar.Text = "UANG MUKA" Then
            If UangMuka.Text * 1 > P50 Then
                ValidBayar = False
                MsgBox("Nilai yang di ajukan tidak boleh melebihi 50% dari nilai SP" & vbCrLf & "Cuma dapet  Rp." & Format(P50, "###,##0"), vbCritical + vbOKOnly, "uakeh tenan bos.. bagi dunk!")
                Exit Function
            End If
        ElseIf cmbJenisBayar.Text = "UANG MUKA BERIKUTNYA" Then
            If (UangMuka.Text * 1) + (Realisasi.Text * 1) > P70 Then
                ValidBayar = False
                MsgBox("Nilai yang di ajukan tidak boleh melebihi " & Format(NilaiSP.Text * 0.7, "###,##0") & " (70% dari nilai SP)" + vbCrLf +
                "Dulu pernah minta uang muka sebesar : " + Format(Realisasi.Text * 1, "###,##0") + vbCrLf +
                "Masih ada sisa : " & Format((NilaiSP.Text * 1) - (Realisasi.Text * 1), "###,##0") + vbCrLf +
                "sekarang koq minta lageee... kurang yo?", vbCritical + vbOKOnly, "uakeh tenan bos.. bagi dunk!")
                Exit Function
            End If
        ElseIf cmbJenisBayar.Text = "PELUNASAN" Then
            ValidBayar = False
            Exit Function
        End If
    End Function
    Private Sub Form_UangMuka_Load(sender As Object, e As EventArgs) Handles Me.Load
        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Dim Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_UangMuka " &
            "where AktifYN = 'Y' " &
            "Order By tglPengajuan Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiUangMuka()
        tTambah = Proses.UserAksesTombol(UserID, "33_UANG_MUKA_PERAJIN", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "33_UANG_MUKA_PERAJIN", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "33_UANG_MUKA_PERAJIN", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "33_UANG_MUKA_PERAJIN", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarUangMuka()
        cmbJenisBayar.Items.Clear()
        cmbJenisBayar.Items.Add("UANG MUKA")
        cmbJenisBayar.Items.Add("UANG MUKA BERIKUTNYA")
        cmbJenisBayar.Items.Add("PELUNASAN")
    End Sub

    Private Sub DaftarUangMuka()
        Dim MsgSQL As String, rs05 As New DataTable
        lstUM.Rows.Clear()
        lstBiaya.Rows.Clear()
        MsgSQL = "Select NoSP, Perajin, max(TglSP) TglSP From T_UangMuka " &
            "Where AktifYN = 'Y' " &
            "Group By NoSP, Perajin "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rs05.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rs05.Rows(a) !NoSP,
                            rs05.Rows(a) !Perajin,
                Format(rs05.Rows(a) !TglSP, "dd-MM-yyyy"))
        Next a
        Proses.CloseConn()
    End Sub
    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 33
            .MinimumHeight = 33
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
        With Me.DGView2.RowTemplate
            .Height = 33
            .MinimumHeight = 33
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

        With Me.lstUM.RowTemplate
            .Height = 33
            .MinimumHeight = 33
        End With
        lstUM.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        lstUM.BackgroundColor = Color.LightGray
        lstUM.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        lstUM.DefaultCellStyle.SelectionForeColor = Color.White
        lstUM.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        lstUM.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        lstUM.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        lstUM.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        lstUM.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        lstUM.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
        With Me.lstBiaya.RowTemplate
            .Height = 33
            .MinimumHeight = 33
        End With
        lstBiaya.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        lstBiaya.BackgroundColor = Color.LightGray
        lstBiaya.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        lstBiaya.DefaultCellStyle.SelectionForeColor = Color.White
        lstBiaya.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        lstBiaya.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        lstBiaya.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        lstBiaya.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        lstBiaya.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        lstBiaya.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
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

    Private Sub NoSP_TextChanged(sender As Object, e As EventArgs) Handles NoSP.TextChanged

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
        cmdBatal.Visible = Not tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True
        TglSP.Enabled = False
        TglPengajuan.Enabled = Not tAktif

    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String, rs05 As New DataTable
        Dim TglBayar As String = ""
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Rows.Clear()
        tCariNoSP.Text = ""
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value

        MsgSQL = "SELECT isnull(SUM(Jumlah * HargaBeliRP),0) ValueSP From T_SP Where NOSP = '" & tCari & "' "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            NilaiSP.Text = Format(rs05.Rows(0) !ValueSP, "###,##0")
        End If

        MsgSQL = "Select isnull(Sum(Realisasi),0) SdhBayar from t_UangMuka " &
            "Where nosp = '" & tCari & "' " &
            "  And AktifYN = 'Y' "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            TotalSdhBayar.Text = Format(rs05.Rows(0) !sdhbayar, "###,##0")
        Else
            TotalSdhBayar.Text = 0
        End If

        MsgSQL = "Select * From T_UangMUka " &
            "Where NoSP = '" & tCari & "' " &
            "  And AktifYN = 'Y' "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rs05.Rows.Count - 1
            Application.DoEvents()
            If rs05.Rows(a) !tglBayar = "1/1/1900" Then
                TglBayar = ""
            Else
                TglBayar = Format(rs05.Rows(a) !tglBayar, "dd-MM-yyyy")
            End If
            DGView2.Rows.Add(rs05.Rows(a) !IdRec,
                rs05.Rows(a) !jenisbayar,
                Format(rs05.Rows(a) !pengajuan, "###,##0"),
                Format(rs05.Rows(a) !Realisasi, "###,##0"),
                rs05.Rows(a) !NoSP,
                rs05.Rows(a) !nodpb,
                rs05.Rows(a) !PembayaranKe,
                Format(rs05.Rows(a) !TglPengajuan, "dd-MM-yyyy"),
                rs05.Rows(a) !BuktiBayar, TglBayar,
                rs05.Rows(a) !Keterangan)
        Next a
    End Sub
    Private Function CekNOSP() As Boolean
        Dim MsgSQL As String, rsc As New DataTable, hasil As Boolean
        If LAdd Or LEdit Then
            If cmbJenisBayar.Text = "UANG MUKA" Then
                MsgSQL = "Select * From t_UangMuka " &
                "Where JenisBayar = 'UANG MUKA' " &
                "  And NoSP = '" & Trim(NoSP.Text) & "' "
                rsc = Proses.ExecuteQuery(MsgSQL)
                If  rsc.Rows.Count <> 0 Then
                    hasil = False
                Else
                    hasil = True
                End If
            Else
                hasil = True
            End If
        Else
            hasil = True
        End If
        CekNOSP = hasil
    End Function

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If Trim(NoSP.Text) = "" Then
            MsgBox("No SP yang akan di hapus masih kosong!", vbCritical + vbOKOnly, ".:ERROR!")
            Exit Sub
        End If
        If MsgBox("Yakin hapus data ini?", vbInformation + vbYesNo, "Confirm!") = vbYes Then
            MsgSQL = "Delete t_UangMuka " &
                "Where IDRec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            ClearTextBoxes()
        End If
    End Sub

    Private Sub IsiUangMuka()
        Dim tUM As Double, rsUM As New DataTable, mTglBayar As String = ""
        If CekNOSP() = False Then
            MsgBox("No SP " & NoSP.Text & " pernah di buatkan pengajuan uang muka!",
                vbCritical + vbOKOnly, "kowe ojo nakal yo ... !")
            UangMuka.Visible = False
            Exit Sub
        Else
            UangMuka.Visible = True
        End If
        lstUM.Rows.Clear()
        lstUM.Visible = False
        tUM = 0
        MsgSQL = "Select * From T_UangMuka " &
            "Where NoSP = '" & NoSP.Text & "' " &
            "  And Left(JenisBayar,9) = 'UANG MUKA' " &
            "  And AktifYN = 'Y' "
        rsUM = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsUM.Rows.Count - 1
            Application.DoEvents()
            If Format(rsUM.Rows(a) !tglBayar, "yyyyMMdd") = "19000101" Then
                mTglBayar = ""
            Else
                mTglBayar = Format(rsUM.Rows(a) !tglBayar, "dd-MM-yyyy")
            End If
            lstUM.Rows.Add(rsUM.Rows(a) !IdRec,
                Format(rsUM.Rows(a) !TglPengajuan, "dd-MM-yyyy"),
                Format(rsUM.Rows(a) !NamaInggrispengajuan, "###,##0"),
                Format(rsUM.Rows(a) !Realisasi, "###,##0"),
                Format(rsUM.Rows(a) !Pelunasan, "###,##0"),
                rsUM.Rows(a) !PembayaranKe,
                rsUM.Rows(a) !BuktiBayar,
                mTglBayar,
                rsUM.Rows(a) !keterangan)
            tUM = tUM + rsUM.Rows(a) !Realisasi
        Next a
        Realisasi.Text = Format(tUM, "###,##0")
        lstUM.Visible = True
    End Sub

    Private Sub IsiPotongan()
        Dim MsgSQL As String, RSD As New DataTable
        Dim tNilai As Double
        lstBiaya.Rows.Clear()
        lstBiaya.Visible = False
        tNilai = 0
        MsgSQL = "Select * From T_Biaya " &
            "Where kode_Perajin = '" & Kode_Perajin.Text & "' " &
            " And AktifYN = 'Y' " &
            "Order By TglKlaim Desc, IDRec "
        RSD = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSD.Rows.Count - 1
            Application.DoEvents()
            lstUM.Rows.Add(RSD.Rows(a) !IdRec,
                Format(RSD.Rows(a) !TglPengajuan, "dd-MM-yyyy"),
                RSD.Rows(a) !NoNota,
                RSD.Rows(a) !Keterangan,
                Format(RSD.Rows(a) !Permintaan, "###,##0"),
                Format(RSD.Rows(a) !Realisasi, "###,##0"),
                Format(RSD.Rows(a) !Pelunasan, "###,##0"),
                RSD.Rows(a) !NoVoucher,
                Format(RSD.Rows(a) !tglRealisasi, "dd-MM-yyyy"))
            tNilai = tNilai + RSD.Rows(a) !Realisasi
        Next a
        LstBiaya.Visible = True
        tPotongan.Text = Format(tNilai, "###,##0")
    End Sub

    Private Sub nodpb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nodpb.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim MsgSQL As String, mKondisi As String = "", RS05 As DataTable
            'If KeyAscii = 13 Then
            If Trim(NoSP.Text) = "" Then
                mKondisi = ""
            ElseIf Trim(NoSP.Text) <> "" Then
                mKondisi = " And NoSP = '" & NoSP.Text & "' "
            End If
            MsgSQL = "Select * From t_DPB " &
                    "Where NoDPB = '" & nodpb.Text & "' " &
                    "  " & mKondisi & " "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            If RS05.Rows.Count <> 0 Then
                If Trim(NoSP.Text) = "" Then
                    NoSP.Text = RS05.Rows(0) !NoSP
                End If
                IsiNOSP
                IsiPotongan()
                IsiUangMuka()
                IsiNilaiDPB(nodpb.Text)
                UangMuka.Focus()
            Else
                nodpb.Text = FindDPB(NoSP.Text)
                MsgSQL = "Select * From t_DPB " &
                    "Where NoDPB = '" & nodpb.Text & "' " &
                    "  " & mKondisi & " "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                If RS05.Rows.Count <> 0 Then
                    NoSP.Text = RS05.Rows(0) !NoSP
                    IsiNilaiDPB(nodpb.Text)
                    IsiNOSP
                    IsiPotongan()
                    IsiUangMuka()
                    UangMuka.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub IsiNilaiDPB(nodpb As String)
        Dim MsgSQL As String, RSI As New DataTable
        MsgSQL = "Select sum(Jumlah * HargaBeli) tDPB From t_DPB " &
            "Where NoDPB = '" & nodpb & "' " &
            "  And AktifYN = 'Y' "
        RSI = Proses.ExecuteQuery(MsgSQL)
        If RSI.Rows.Count <> 0 Then
            NilaiDPB.Text = Format(RSI.Rows(0) !tdpb, "###,##0")
        Else
            NilaiDPB.Text = 0
        End If
        Proses.CloseConn()
    End Sub
    Private Sub IsiNOSP()
        Dim MsgSQL As String, RSN1 As New DataTable

        MsgSQL = "Select NoSP, Kode_Perajin, Perajin, TglSP " &
            " From T_SP " &
            "Where NoSP = '" & NoSP.Text & "' "
        RSN1 = Proses.ExecuteQuery(MsgSQL)
        If RSN1.Rows.Count <> 0 Then
            Kode_Perajin.Text = RSN1.Rows(0) !Kode_Perajin
            nama.Text = RSN1.Rows(0) !Perajin
            TglSP.Value = RSN1.Rows(0) !TglSP
            PembayaranKe.Text = JumBayar(NoSP.Text)
        End If
    End Sub


    Private Function JumBayar(tNoSP As String) As Integer
        Dim MsgSQL As String, RSJ As New DataTable
        MsgSQL = "Select Count(*) JumRec from t_UangMuka " &
            "Where nosp = '" & NoSP.Text & "' " &
            "  And AktifYN = 'Y' "
        RSJ = Proses.ExecuteQuery(MsgSQL)
        If RSJ.Rows.Count <> 0 Then
            JumBayar = RSJ.Rows(0) !jumrec + 1
        Else
            JumBayar = 0
        End If

        MsgSQL = "Select isnull(Sum(Realisasi),0) SdhBayar from t_UangMuka " &
            "Where nosp = '" & NoSP.Text & "' " &
            "  And Left(JenisBayar,9) = 'UANG MUKA' " &
            "  And AktifYN = 'Y' "
        RSJ = Proses.ExecuteQuery(MsgSQL)
        If RSJ.Rows.Count <> 0 Then
            TotalSdhBayar.Text = Format(RSJ.Rows(0) !sdhbayar, "###,##0")
        Else
            TotalSdhBayar.Text = 0
        End If

        MsgSQL = "Select isnull(Sum(Realisasi), 0) SdhBayar from t_Biaya " &
            "Where Kode_Perajin = '" & Kode_Perajin.Text & "' " &
            "  And AktifYN = 'Y' "
        RSJ = Proses.ExecuteQuery(MsgSQL)
        If RSJ.Rows.Count <> 0 Then
            tPotongan.Text = Format(RSJ.Rows(0) !sdhbayar, "###,##0")
        Else
            tPotongan.Text = 0
        End If


        MsgSQL = "SELECT isnull( SUM(Jumlah * HargaBeliRP), 0) ValueSP From T_SP Where NOSP = '" & NoSP.Text & "' "
        RSJ = Proses.ExecuteQuery(MsgSQL)
        If RSJ.Rows.Count <> 0 Then
            NilaiSP.Text = Format(RSJ.Rows(0) !ValueSP, "###,##0")
        End If

        If Trim(TotalSdhBayar.Text) = "" Then TotalSdhBayar.Text = 0
        If Trim(NilaiSP.Text) = "" Then NilaiSP.Text = 0
        tKurangBayar
    End Function
    Private Sub tKurangBayar()
        If Trim(tPotongan.Text) = "" Then tPotongan.Text = 0
        If Trim(NilaiSP.Text) = "" Then NilaiSP.Text = 0
        If Trim(NilaiDPB.Text) = "" Then NilaiDPB.Text = 0
        If Trim(TotalSdhBayar.Text) = "" Then TotalSdhBayar.Text = 0
        KurangBayar.Text = Format((NilaiDPB.Text * 1) - (TotalSdhBayar.Text * 1) - (tPotongan.Text * 1), "###,##0")
    End Sub
    Private Function findDPB(Cari As String) As String
        Dim RSD As New DataTable, mKondisi As String
        Dim MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoDPB Like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select distinct NoDPB, TglDPB, t_DPB.NoSP, NoLHP, Perajin, right(NoDPB,2) + left(NoDPB,3)  " &
            " From t_DPB Inner Join T_SP on T_DPB.NoSP = T_SP.NoSP " &
            "Where t_DPB.AktifYN = 'Y' " & mKondisi & " and year(TglDPB) > 2000 " &
            "Order By right(NoDPB,2) + left(NoDPB,3) Desc"
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar DPB"
        Form_Daftar.ShowDialog()
        findDPB = Trim(FrmMenuUtama.TSKeterangan.Text)
        FrmMenuUtama.TSKeterangan.Text = ""
    End Function
    'Private Function FindSP(Cari As String) As String
    '    Dim RSD As New DataTable, mKondisi As String
    '    Dim MsgSQL As String
    '    If Trim(Cari) = "" Then
    '        mKondisi = ""
    '    Else
    '        mKondisi = "And NoDPB Like '%" & Trim(Cari) & "%' "
    '    End If
    '    MsgSQL = "Select distinct NoDPB, TglDPB, t_DPB.NoSP, NoLHP, Perajin, right(NoDPB,2) + left(NoDPB,3)  " &
    '        " From t_DPB Inner Join T_SP on T_DPB.NoSP = T_SP.NoSP " &
    '        "Where t_DPB.AktifYN = 'Y' " & mKondisi & " and year(TglDPB) > 2000 " &
    '        "Order By right(NoDPB,2) + left(NoDPB,3) Desc"
    '    Form_Daftar.txtQuery.Text = MsgSQL
    '    Form_Daftar.Text = "Daftar SP"
    '    Form_Daftar.ShowDialog()
    '    FindSP = Trim(FrmMenuUtama.TSKeterangan.Text)
    '    FrmMenuUtama.TSKeterangan.Text = ""
    'End Function
    Private Sub NoSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoSP.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim RSN1 As New DataTable
            MsgSQL = "Select NoSP, Kode_Perajin, Perajin, TglSP " &
            " From T_SP " &
            "Where NoSP = '" & NoSP.Text & "' "
            RSN1 = Proses.ExecuteQuery(MsgSQL)
            If RSN1.Rows.Count <> 0 Then
                Kode_Perajin.Text = RSN1.Rows(0) !Kode_Perajin
                nama.Text = RSN1.Rows(0) !Perajin
                TglSP.Value = RSN1.Rows(0) !TglSP
                PembayaranKe.Text = JumBayar(NoSP.Text)
                IsiPotongan()
                IsiUangMuka()
            Else
                NoSP.Text = Proses.FindSP(NoSP.Text, Kode_Perajin.Text)
                MsgSQL = "Select NoSP, Kode_Perajin, Perajin, TglSP " &
                    " From T_SP " &
                    "Where NOSP = '" & NoSP.Text & "' "
                RSN1 = Proses.ExecuteQuery(MsgSQL)
                If RSN1.Rows.Count <> 0 Then
                    Kode_Perajin.Text = RSN1.Rows(0) !Kode_Perajin
                    nama.Text = RSN1.Rows(0) !Perajin
                    TglSP.Value = RSN1.Rows(0) !TglSP
                    PembayaranKe.Text = JumBayar(NoSP.Text)
                    IsiPotongan()
                    IsiUangMuka()
                Else
                    MsgBox("NO SP tidak boleh kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                    Proses.CloseConn()
                    NoSP.Focus()
                    Exit Sub
                End If
            End If
            Proses.CloseConn()
            If LAdd Or LEdit Then
                If Microsoft.VisualBasic.Left(cmbJenisBayar.Text, 9) = "UANG MUKA" Then
                    If CekNOSP() = False Then
                        NoSP.Focus()
                    Else
                        UangMuka.Visible = True
                        UangMuka.Enabled = True
                        UangMuka.Focus()
                    End If
                ElseIf cmbJenisBayar.Text = "PELUNASAN" Then
                    nodpb.Enabled = True
                    nodpb.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Dim MsgSQL As String, tIdRec As String,
            RSJ As New DataTable, rs05 As New DataTable
        If DGView2.Rows.Count = 0 Then Exit Sub
        tIdRec = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "Select * From T_UangMuka " &
            "Where AktifYN = 'Y' " &
            "  AND IDRec = '" & tIdRec & "'"
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            idRecord.Text = tIdRec
            cmbJenisBayar.Text = rs05.Rows(0) !jenisbayar
            NoSP.Text = rs05.Rows(0) !NoSP

            nodpb.Text = rs05.Rows(0) !nodpb
            If nodpb.Text <> "" Then
                IsiNilaiDPB(nodpb.Text)
            Else
                NilaiDPB.Text = 0
            End If
            NilaiSP.Text = Format(rs05.Rows(0) !NilaiSP, "###,##0")
            nama.Text = rs05.Rows(0) !Perajin
            Kode_Perajin.Text = rs05.Rows(0) !Kode_Perajin
            TglSP.Value = rs05.Rows(0) !TglSP
            'tglRealisasi = Format(rs05.Rows(0) !tglBayar, "dd-MM-yyyy")
            UangMuka.Text = rs05.Rows(0) !pengajuan
            Realisasi.Text = rs05.Rows(0) !Realisasi
            PembayaranKe.Text = rs05.Rows(0) !PembayaranKe 'JumBayar(tIdRec)
            TglPengajuan.Value = rs05.Rows(0) !TglPengajuan
            'BuktiBayar.Text = rs05.Rows(0) !BuktiBayar
            Keterangan.Text = rs05.Rows(0) !Keterangan
        End If
        IsiTotalSdhBayar
    End Sub

    Private Sub IsiTotalSdhBayar()
        Dim MsgSQL As String, RSJ As New DataTable
        MsgSQL = "Select isnull(Sum(realisasi), 0) SdhBayar from t_UangMuka " &
        "Where nosp = '" & NoSP.Text & "' " &
        "  And AktifYN = 'Y' "
        RSJ = Proses.ExecuteQuery(MsgSQL)
        If RSJ.Rows.Count <> 0 Then
            TotalSdhBayar.Text = Format(RSJ.Rows(0) !sdhbayar, "###,##0")
        Else
            TotalSdhBayar.Text = 0
        End If
        If Trim(NilaiSP.Text) = "" Then NilaiSP.Text = 0
        tKurangBayar()
    End Sub

End Class