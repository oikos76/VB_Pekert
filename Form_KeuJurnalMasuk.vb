Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Public Class Form_KeuJurnalMasuk

    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean

    Private CN As SqlConnection
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String



    Private Sub cmbMataUang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMataUang.SelectedIndexChanged
        If Trim(cmbMataUang.Text) = "RP" Then
            Kurs.Text = 1
            LKurs.Visible = False
            Kurs.Visible = False
            Jumlah.Visible = False
            LMataUang.Visible = False
            Jumlah.Text = NilaiJurnal.Text
            LNilaiJurnal.Text = "Jumlah         :"
            LNilaiJurnal.Text = "Jumlah       Rp."
        Else
            LKurs.Visible = True
            Kurs.Visible = True
            Jumlah.Visible = True
            LMataUang.Visible = True
            Jumlah.Text = Format((NilaiJurnal.Text * 1) * (Kurs.Text * 1), "###,##0")
            LNilaiJurnal.Text = "Jumlah         :"
            LMataUang.Text = "Euro  =  Rp."
            LMataUang.Text = Microsoft.VisualBasic.Left(cmbMataUang.Text + Space(5), 4) + "  =  Rp."
        End If
    End Sub



    Private Sub AccCode1_TextChanged(sender As Object, e As EventArgs) Handles AccCode1.TextChanged
        If Len(AccCode1.Text) <= 1 Then
            KetAccCode1.Text = ""
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        ClearTextBoxes()
        cmbDK.SelectedIndex = -1
        AturTombol(False)
        Uraian.Focus()
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRecord.Text) = "" Then
            MsgBox("Data yang akan di edit belum dipilih!", vbCritical + vbOKOnly, ".:Error!")
            Exit Sub
        End If
        If Status.Text = "Close" Then
            MsgBox("Jurnal sudah di closing, tidak bisa edit !", vbCritical + vbOKOnly, ".:Warning !")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If IDRecord.Text = "" Then
            MsgBox("Data yang akan dihapus belum dipilih!", vbCritical + vbOKOnly, ".:Error!")
            Exit Sub
        End If
        If Status.Text = "Close" Then
            MsgBox("Jurnal sudah di closing, tidak bisa Hapus !", vbCritical + vbOKOnly, ".:Warning !")
            Exit Sub
        End If
        If MsgBox("Hapus data ini?", vbQuestion + vbYesNo, "Confirmation") = vbYes Then
            MsgSQL = "Update T_Jurnal Set " &
                    " AktifYN = 'N', LastUpd = GetDate(), " &
                    "  UserId = '" & UserID & "' " &
                    "Where idrec = '" & IDRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            ClearTextBoxes()
        End If
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub AddItem()
        Dim ada As Boolean = False, i As Integer = 0
        If Trim(Uraian.Text) = "" Then
            MsgBox("Uraian/Keterangan Jurnal belum di isi !", vbCritical + vbOKOnly, ".:Warning !")
            Uraian.Focus()
            Exit Sub
        End If
        If Trim(NilaiJurnal.Text) = "" Then NilaiJurnal.Text = 0
        If Trim(AccCode1.Text) = "" Then
            MsgBox("Kode GL belum di pilih !", vbCritical + vbOKOnly, ".:Warning !")
            Uraian.Focus()
            Exit Sub
        End If
        'For i = 0 To DGView.Rows.Count - 1
        '    If Trim(AccCode1.Text) = Trim(DGView.Rows(i).Cells(2).Value) Then
        '        ada = True
        '        Exit For
        '    End If
        'Next
        'If ada Then
        '    If (MsgBox("Kode GL " & AccCode1.Text & " " & KetAccCode1.Text & " SUDAH ADA, " & vbCrLf &
        '               "Apakah Nilai account " & AccCode1.Text & " mau di ganti ? ",
        '               vbInformation + vbYesNo, ".:Warning !") = vbYes) Then
        '        NoUrut.Text = i
        '        DGView.Rows(NoUrut.Text).Cells(0).Value = NoUrut.Text
        '        DGView.Rows(NoUrut.Text).Cells(1).Value = Trim(Uraian.Text)
        '        DGView.Rows(NoUrut.Text).Cells(2).Value = AccCode1.Text
        '        DGView.Rows(NoUrut.Text).Cells(3).Value = KetAccCode1.Text
        '        DGView.Rows(NoUrut.Text).Cells(4).Value = cmbMataUang.Text
        '        DGView.Rows(NoUrut.Text).Cells(5).Value = Kurs.Text
        '        DGView.Rows(NoUrut.Text).Cells(6).Value = NilaiJurnal.Text
        '        If cmbDK.Text = "DEBET" Then
        '            DGView.Rows(NoUrut.Text).Cells(7).Value = Jumlah.Text
        '            DGView.Rows(NoUrut.Text).Cells(8).Value = 0
        '        Else
        '            DGView.Rows(NoUrut.Text).Cells(7).Value = 0
        '            DGView.Rows(NoUrut.Text).Cells(8).Value = Jumlah.Text
        '        End If
        '    End If
        'Else
        Dim NilaiDebet As String = "", NilaiKredit As String = ""
            NoUrut.Text = Format(DGView.Rows.Count + 1, "###,##0")
        If cmbDK.Text = "DEBET" Then
            NilaiDebet = Jumlah.Text
            NilaiKredit = 0
        Else
            NilaiDebet = 0
            NilaiKredit = Jumlah.Text
        End If
        DGView.Rows.Add(NoUrut.Text, Trim(Uraian.Text),
                        AccCode1.Text, KetAccCode1.Text,
                        cmbMataUang.Text, Kurs.Text, NilaiJurnal.Text,
                        NilaiDebet,
                        NilaiKredit, "Hapus")
        'End If
        HitungTotal()
    End Sub

    Private Sub HitungTotal()
        Dim sum = (From row As DataGridViewRow In DGView.Rows.Cast(Of DataGridViewRow)()
                   Select CDec(row.Cells(7).Value)).Sum
        TotalDebet.Text = Format(sum, "###,##0")
        sum = 0
        sum = (From row As DataGridViewRow In DGView.Rows.Cast(Of DataGridViewRow)()
               Select CDec(row.Cells(8).Value)).Sum
        TotalKredit.Text = Format(sum, "###,##0")
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Trim(Uraian.Text) = "" Then
            MsgBox("Uraian/Keterangan Jurnal belum di isi !", vbCritical + vbOKOnly, ".:Warning !")
            Uraian.Focus()
            Exit Sub
        End If

        If Trim(AccCode1.Text) = "" Then
            MsgBox("Kode GL belum di pilih !", vbCritical + vbOKOnly, ".:Warning !")
            Uraian.Focus()
            Exit Sub
        End If

        Kode_Importir.ReadOnly = True
        Importir.ReadOnly = True
        NoPackingList.ReadOnly = True
        NoPI.ReadOnly = True
        AddItem()
        cmbDK.SelectedIndex = -1
        'Uraian.Text = ""
        AccCode1.Text = ""
        Jumlah.Text = "0"
        NilaiJurnal.Text = "0"
        Uraian.Focus()
    End Sub


    Private Sub Kurs_TextChanged(sender As Object, e As EventArgs) Handles Kurs.TextChanged
        If Trim(Kurs.Text) = "" Then Kurs.Text = 0
        If IsNumeric(Kurs.Text) Then
            Dim temp As Double = Kurs.Text
            Kurs.Text = Format(temp, "###,##0")
            Kurs.SelectionStart = Kurs.TextLength
        Else
            Kurs.Text = 0
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
                NoPI.Focus()
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
                    NoPI.Focus()
                Else
                    Kode_Importir.Text = ""
                    Importir.Text = ""
                    Kode_Importir.Focus()
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

    Private Sub NoPI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPI.KeyPress
        If e.KeyChar = Chr(13) Then

            SQL = "Select nopi From t_pi " &
             " Where nopi = '" & NoPI.Text & "' " &
             " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 And Trim(NoPI.Text) <> "" Then
                NoPI.Text = dbTable.Rows(0) !nopi

            Else
                Dim MKondisi As String = ""
                MKondisi = " AND nopi like '%" & NoPI.Text & "%' "
                If Kode_Importir.Text <> "" Then
                    MKondisi = MKondisi + " AND kode_importir = '" & Kode_Importir.Text & "' "
                End If
                Form_Daftar.txtQuery.Text = "Select  nopi, nopo, tglpi, statuspi, Kode_Importir, importir " &
                    " From t_pi " &
                    "Where AktifYN = 'Y' " &
                    "  " & MKondisi & " " &
                    "Group By nopi, nopo, tglpi, statuspi, Kode_Importir, importir " &
                    "ORDER BY TglPI Desc, NoPI "
                Form_Daftar.Text = "Daftar PI"
                Form_Daftar.ShowDialog()

                NoPI.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nopi From t_pi " &
                     " Where nopi = '" & NoPI.Text & "' " &
                     " and aktifyn = 'Y' "
                NoPI.Text = Proses.ExecuteSingleStrQuery(SQL)
            End If
            If NoPI.Text <> "" Then
                SQL = "SELECT NoPackingList FROM t_PackingList " &
                "WHERE aktifYN='Y' " &
                "  AND noPI = '" & NoPI.Text & "' "
                NoPackingList.Text = Proses.ExecuteSingleStrQuery(SQL)
            End If
            AccCode1.Focus()
        End If
    End Sub

    Private Sub NoPI_TextChanged(sender As Object, e As EventArgs) Handles NoPI.TextChanged
        If Len(NoPI.Text) < 1 Then
            NoPackingList.Text = ""
            NoPI.Text = ""
        End If
    End Sub

    Private Sub AccCode1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AccCode1.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim MsgSQL As String, RS01 As New DataTable
            If LAdd Or LEdit Then
                MsgSQL = "Select * From M_PERKIRAAN " &
                    "where aktifYN = 'Y' " &
                    " And NO_PERKIRAAN = '" & AccCode1.Text & "'"
                RS01 = Proses.ExecuteQuery(MsgSQL)
                If RS01.Rows.Count <> 0 Then
                    AccCode1.Text = Trim(RS01.Rows(0) !no_PERKIRAAN)
                    KetAccCode1.Text = Trim(RS01.Rows(0) !NM_PERKIRAAN)
                    cmbMataUang.Select()
                    cmbMataUang.Focus()
                Else
                    MsgSQL = "Select * From M_PERKIRAAN " &
                        "Where AktifYN = 'Y' " &
                        "  And (NM_PERKIRAAN like '%" & AccCode1.Text & "%' or " &
                        "      NO_PERKIRAAN like '%" & AccCode1.Text & "%') " &
                        " Order By NO_PERKIRAAN, NM_PERKIRAAN "
                    Form_Daftar.txtQuery.Text = MsgSQL
                    Form_Daftar.Text = "Daftar Kode GL"
                    Form_Daftar.ShowDialog()
                    AccCode1.Text = FrmMenuUtama.TSKeterangan.Text
                    MsgSQL = "Select * From M_PERKIRAAN " &
                        "where aktifYN = 'Y' " &
                        " And NO_PERKIRAAN = '" & AccCode1.Text & "'"
                    RS01 = Proses.ExecuteQuery(MsgSQL)
                    If RS01.Rows.Count <> 0 Then
                        AccCode1.Text = Trim(RS01.Rows(0) !no_PERKIRAAN)
                        KetAccCode1.Text = Trim(RS01.Rows(0) !NM_PERKIRAAN)
                        cmbMataUang.Select()
                        cmbMataUang.Focus()
                    Else
                        MsgBox("Account Code tidak ditemukan!", vbQuestion + vbCritical, ".:Warning !")
                        AccCode1.Text = ""
                        AccCode1.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Form_KeuJurnalMasuk_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGView.Rows.Clear()
        cmbDK.Items.Add("DEBET")
        cmbDK.Items.Add("KREDIT")
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        tTambah = Proses.UserAksesTombol(UserID, "101_JURNAL_UMUM", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "101_JURNAL_UMUM", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "101_JURNAL_UMUM", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "101_JURNAL_UMUM", "laporan")
        Dim rs As New DataTable
        SQL = "Select Kode from m_Currency Where AktifYN = 'Y'"
        rs = Proses.ExecuteQuery(SQL)
        For a = 0 To rs.Rows.Count - 1
            Application.DoEvents()
            cmbMataUang.Items.Add(rs.Rows(a) !kode)
        Next a
        LAdd = False
        LEdit = False
        ClearTextBoxes()
        AturTombol(True)
    End Sub

    Private Sub Jumlah_TextChanged(sender As Object, e As EventArgs) Handles Jumlah.TextChanged
        If Trim(Jumlah.Text) = "" Then Jumlah.Text = 0
        If IsNumeric(Jumlah.Text) Then
            Dim temp As Double = Jumlah.Text
            Jumlah.Text = Format(temp, "###,##0")
            Jumlah.SelectionStart = Jumlah.TextLength
        Else
            Jumlah.Text = 0
        End If
    End Sub

    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 32
            .MinimumHeight = 32
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
    End Sub


    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdPrint.Visible = False 'tAktif
        cmdDaftar.Visible = tAktif
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
        If LAdd = False And LEdit = False Then
            cmbDK.Enabled = False
        Else
            cmbDK.Enabled = True
        End If
        TglTr.Enabled = Not tAktif
        cmbMataUang.Enabled = Not tAktif
        Uraian.ReadOnly = tAktif
        Kode_Importir.ReadOnly = tAktif
        Importir.ReadOnly = tAktif
        NoPackingList.ReadOnly = tAktif
        NoPI.ReadOnly = tAktif
        AccCode1.ReadOnly = tAktif
        Jumlah.ReadOnly = True
        Kurs.ReadOnly = tAktif
        NilaiJurnal.ReadOnly = tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Visible = tAktif
        Me.Text = "Jurnal Masuk"
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        If DGView.Rows.Count = 0 Then
            MsgBox("Transaksi masih kosong!", vbCritical + vbOKOnly, ".:ERROR!")
            Uraian.Focus()
            Exit Sub
        End If
        If TotalDK() = False Then Exit Sub
        Dim MsgSQL As String, rs05 As New DataTable, tsaldo As Double = 0,
            sudahClose As Boolean
        sudahClose = Proses.StatusJurnal(Format(TglTr.Value, "yyyyMM"))
        If sudahClose Then
            MsgBox("Periode transaksi " & Format(TglTr.Value, "dd MMM yyyy") & " SUDAH di closing !", vbCritical + vbOKOnly, ".:Warning")
            Exit Sub
        End If
        If LAdd Then
            IDRecord.Text = MaxJurnal()
            For i As Integer = 0 To DGView.Rows.Count - 1
                MsgSQL = "Select SAkhir from m_Perkiraan " &
                    "Where AktifYN = 'Y' " &
                    "  And NO_Perkiraan = '" & Trim(DGView.Rows(i).Cells(2).Value) & "' "
                tsaldo = Proses.ExecuteSingleDblQuery(MsgSQL)
                MsgSQL = "Insert Into T_Jurnal (idrec, NoUrut, tanggal, Uraian, AccountCode, " &
                    "KetAccCode, MataUang, Kurs, NilaiJurnal, Debet, Kredit, Saldo, AktifYN, " &
                    "LastUpd, UserId, JenisJurnal, ClosingYN, Kode_importir, No_PI, No_PackingList, " &
                    "Kode_Perajin, No_SP, No_DPB) Values('" & IDRecord.Text & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(0).Value) & "', " &
                    "'" & Format(TglTr.Value, "yyyy-MM-dd") & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(1).Value) & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(2).Value) & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(3).Value) & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(4).Value) & "', " &
                    "" & Trim(DGView.Rows(i).Cells(5).Value) * 1 & ", " &
                    "" & Trim(DGView.Rows(i).Cells(6).Value) * 1 & ", " &
                    "" & Trim(DGView.Rows(i).Cells(7).Value) * 1 & ", " &
                    "" & Trim(DGView.Rows(i).Cells(8).Value) * 1 & ", " &
                    "" & tsaldo & ", 'Y', GetDate(), '" & UserID & "', 'JURNAL MASUK','N', " &
                    "'" & Kode_Importir.Text & "','" & NoPI.Text & "','" & NoPackingList.Text & "','','','') "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "Update m_Perkiraan set SAkhir = " & tsaldo & " " &
                    "Where NO_Perkiraan = '" & Trim(DGView.Rows(i).Cells(3).Value) & "' " &
                    "  and AktifYN = 'Y' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next i
        ElseIf LEdit Then
            MsgSQL = "Update T_Jurnal Set AktifYN = 'E' " &
                "Where idRec = '" & IDRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            For i As Integer = 0 To DGView.Rows.Count - 1
                MsgSQL = "Insert Into T_Jurnal (idrec, NoUrut, tanggal, Uraian, AccountCode, " &
                    "KetAccCode, MataUang, Kurs, NilaiJurnal, Debet, Kredit, Saldo, AktifYN, " &
                    "LastUpd, UserId, JenisJurnal, ClosingYN, Kode_importir, No_PI, No_PackingList, " &
                    "Kode_Perajin, No_SP, No_DPB) Values('" & IDRecord.Text & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(0).Value) & "', " &
                    "'" & Format(TglTr.Value, "yyyy-MM-dd") & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(1).Value) & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(2).Value) & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(3).Value) & "', " &
                    "'" & Trim(DGView.Rows(i).Cells(4).Value) & "', " &
                    "" & Trim(DGView.Rows(i).Cells(5).Value) * 1 & ", " &
                    "" & Trim(DGView.Rows(i).Cells(6).Value) * 1 & ", " &
                    "" & Trim(DGView.Rows(i).Cells(7).Value) * 1 & ", " &
                    "" & Trim(DGView.Rows(i).Cells(8).Value) * 1 & ", " &
                    "" & tsaldo & ", 'Y', GetDate(), '" & UserID & "', 'JURNAL MASUK','N', " &
                    "'" & Kode_Importir.Text & "','" & NoPI.Text & "','" & NoPackingList.Text & "','','','') "
                Proses.ExecuteNonQuery(MsgSQL)
            Next i
            MsgSQL = "DELETE T_Jurnal " &
                "WHERE idRec = '" & IDRecord.Text & "'  " &
                "  AND AktifYN = 'E' "
            Proses.ExecuteNonQuery(MsgSQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub
    Private Function MaxJurnal() As String
        Dim MsgSQL As String, RsMax As String = ""
        MsgSQL = "Select convert(varchar(4), Getdate(),  12) + right(isnull(Max(right(Idrec,6)),0) + 10000001 , 6) IdRec " &
        " From T_Jurnal " &
        "Where Left(Idrec,4) = convert(varchar(4), Getdate(),  12) "
        RsMax = Proses.ExecuteSingleStrQuery(MsgSQL)
        MaxJurnal = RsMax
    End Function

    Private Sub cmbDK_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDK.SelectedIndexChanged
        btnAdd.Focus()
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
        DGView.Rows.Clear()
        KetAccCode1.Text = ""
        TglTr.Value = Now
        TotalDebet.Text = 0
        TotalKredit.Text = 0
        cmbDK.SelectedIndex = -1
        If cmbMataUang.Items.Count <> 0 Then cmbMataUang.Text = "RP"
        Kurs.Text = 1
        NilaiJurnal.Text = 0
        Jumlah.Text = 0
        Status.Text = ""
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Kurs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kurs.KeyPress
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
            If IsNumeric(Kurs.Text) Then
                If NilaiJurnal.Text = "" Then NilaiJurnal.Text = 0
                Dim temp As Double = Kurs.Text
                Kurs.Text = Format(temp, "###,##0.00")
                Kurs.SelectionStart = Kurs.TextLength
                If cmbMataUang.Text = "RP" Then
                    Jumlah.Text = NilaiJurnal.Text
                Else
                    Jumlah.Text = (NilaiJurnal.Text * 1) * (Kurs.Text * 1)
                End If
            Else
                Kurs.Text = 0
            End If
            If LAdd Or LEdit Then NilaiJurnal.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub NilaiJurnal_GotFocus(sender As Object, e As EventArgs) Handles NilaiJurnal.GotFocus
        With NilaiJurnal
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
    End Sub

    Private Sub cmdDaftar_Click(sender As Object, e As EventArgs) Handles cmdDaftar.Click
        Dim MsgSQL As String, tPeriode As String = Format(DateAdd(DateInterval.Year, -2, Now), "yyMM")
        MsgSQL = "Select * From T_Jurnal " &
            "Where AktifYN = 'Y' " &
            " And convert(char(4), tanggal, 12) >= '" & tPeriode & "' " &
            " And JenisJurnal = 'JURNAL MASUK' " &
            "Order By tanggal desc, IdRec, NoUrut "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar Jurnal"
        Form_Daftar.param1.Text = "JURNAL MASUK"
        Form_Daftar.ShowDialog()
        IDRecord.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        DGView.Rows.Clear()
        MsgSQL = "select * from T_Jurnal " &
            "Where IdRec = '" & IDRecord.Text & "' And AktifYN = 'Y' Order BY NoUrut"
        dbTable = Proses.ExecuteQuery(MsgSQL)
        If dbTable.Rows.Count <> 0 Then
            IDRecord.Text = dbTable.Rows(0) !idrec
            TglTr.Text = dbTable.Rows(0) !tanggal
            If dbTable.Rows(0) !closingyn = "Y" Then
                Status.Text = "Close"
            Else
                Status.Text = ""
            End If
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                DGView.Rows.Add(dbTable.Rows(a) !nourut,
                    dbTable.Rows(a) !uraian,
                    dbTable.Rows(a) !accountcode,
                    dbTable.Rows(a) !KetAccCode,
                    dbTable.Rows(a) !MataUang,
                    Format(dbTable.Rows(a) !kurs, "###,##0"),
                    Replace(Format(dbTable.Rows(a) !nilaijurnal, "###,##0.00"), ".00", ""),
                    Format(dbTable.Rows(a) !debet, "###,##0"),
                    Format(dbTable.Rows(a) !kredit, "###,##0"), "Hapus")
            Next (a)
        End If
        Proses.CloseConn()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Uraian_TextChanged(sender As Object, e As EventArgs) Handles Uraian.TextChanged

    End Sub

    Private Sub cmbMataUang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMataUang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Trim(cmbMataUang.Text) = "RP" Then
                Kurs.Text = 1
                LKurs.Visible = False
                Kurs.Visible = False
                Jumlah.Visible = False
                LMataUang.Visible = False
                Jumlah.Text = NilaiJurnal.Text
                LNilaiJurnal.Text = "Jumlah         :"
                LNilaiJurnal.Text = "Jumlah       Rp."
                NilaiJurnal.Focus()
            Else
                LKurs.Visible = True
                Kurs.Visible = True
                Jumlah.Visible = True
                LMataUang.Visible = True
                Jumlah.Text = Format((NilaiJurnal.Text * 1) * (Kurs.Text * 1), "###,##0")
                LNilaiJurnal.Text = "Jumlah         :"
                Kurs.Focus()
            End If
        End If
    End Sub
    Private Function TotalDK() As Boolean
        Dim TDb As Double = 0, TKd As Double = 0
        TDb = (From row As DataGridViewRow In DGView.Rows.Cast(Of DataGridViewRow)()
               Select CDec(row.Cells(7).Value)).Sum
        TKd = (From row As DataGridViewRow In DGView.Rows.Cast(Of DataGridViewRow)()
               Select CDec(row.Cells(8).Value)).Sum

        If TDb <> TKd Then
            MsgBox("Total debet tidak sama dengan total kredit", vbCritical + vbOKOnly, ".:ERROR!")
            TotalDK = False
        Else
            TotalDK = True
        End If
    End Function

    Private Sub NilaiJurnal_TextChanged(sender As Object, e As EventArgs) Handles NilaiJurnal.TextChanged
        If IsNumeric(NilaiJurnal.Text) Then
            Dim temp As Double = NilaiJurnal.Text
            If Trim(NilaiJurnal.Text) = "" Then NilaiJurnal.Text = 0
            If Trim(Kurs.Text) = "" Then Kurs.Text = 0
            NilaiJurnal.Text = Format(temp, "###,##0")
            NilaiJurnal.SelectionStart = NilaiJurnal.TextLength
            If cmbMataUang.Text = "RP" Then
                Jumlah.Text = NilaiJurnal.Text
            Else
                Jumlah.Text = (NilaiJurnal.Text * 1) * (Kurs.Text * 1)
            End If
        End If
    End Sub

    Private Sub NilaiJurnal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NilaiJurnal.KeyPress
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
            If IsNumeric(NilaiJurnal.Text) Then
                If Trim(NilaiJurnal.Text) = "" Then NilaiJurnal.Text = 0
                If Kurs.Text = "" Then Kurs.Text = 0
                Dim temp As Double = NilaiJurnal.Text
                NilaiJurnal.Text = Format(temp, "###,##0.00")
                NilaiJurnal.SelectionStart = NilaiJurnal.TextLength
                If cmbMataUang.Text = "RP" Then
                    Jumlah.Text = NilaiJurnal.Text
                Else
                    Jumlah.Text = (NilaiJurnal.Text * 1) * (Kurs.Text * 1)
                End If
            Else
                NilaiJurnal.Text = 0
            End If
            If LAdd Or LEdit Then cmbDK.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
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
            If LAdd Or LEdit Then
                cmbDK.Select()
                cmbDK.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Uraian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Uraian.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        'e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            Kode_Importir.Focus()
        End If
    End Sub

    Private Sub cmbDK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDK.KeyPress
        If e.KeyChar = Chr(13) Then btnAdd_Click(sender, e)
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If DGView.Rows.Count = 0 Then Exit Sub
        NoUrut.Text = DGView.CurrentCell.RowIndex
        Dim tNamaBrg As String = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) + ", " +
            Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value) + " " +
            Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(3).Value)
        Uraian.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value)
        AccCode1.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value)
        KetAccCode1.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(3).Value)
        cmbMataUang.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(4).Value)
        Kurs.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(5).Value)
        NilaiJurnal.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(6).Value)
        Jumlah.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(6).Value)
        If Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(8).Value) = 0 Then
            cmbDK.Text = "DEBET"
        Else
            cmbDK.Text = "KREDIT"
        End If

        If e.ColumnIndex = 9 Then 'Hapus
            If LAdd = False And LEdit = False Then Exit Sub
            If Trim(tNamaBrg) <> "" Then
                If MsgBox("Yakin hapus " & tNamaBrg & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                    DGView.Rows.RemoveAt(e.RowIndex)
                    HitungTotal()
                    Renumbering()
                End If
            End If
        End If
    End Sub
    Private Sub Renumbering()
        For i = 0 To DGView.Rows.Count - 1
            DGView.Rows(i).Cells(0).Value = i + 1
        Next i
    End Sub

    Private Sub Jumlah_GotFocus(sender As Object, e As EventArgs) Handles Jumlah.GotFocus
        With Jumlah
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
    End Sub

    Private Sub Kurs_GotFocus(sender As Object, e As EventArgs) Handles Kurs.GotFocus
        With Kurs
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
    End Sub
End Class