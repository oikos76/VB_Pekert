Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Form_DPBContoh
    Dim kodeGLHutangPerajin As String = "",
      KodeGLPiutangPerajin As String = ""
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Dim FotoLoc As String = My.Settings.path_foto
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call DaftarDPBSample()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSQL As String, dbS As New DataTable
        If LAdd Then
            MsgSQL = "Select convert(Char(2), GetDate(), 12) thn, 
                             isnull(Max(substring(NoDPB,8,3)),0) + 10001 RecId  
                        From t_DPBSample Where right(nodpb,2) = convert(Char(2), GetDate(), 12) "
            dbS = Proses.ExecuteQuery(MsgSQL)
            If Not dbS.Rows.Count <> 0 Then
                nodpb.Text = "DPBSPL-" + Microsoft.VisualBasic.Right(dbS.Rows(0) !recid, 3) + "/" + dbS.Rows(0) !thn
            End If

            MsgSQL = "Select Kode_Produk, Jumlah, HargaBeli, Deskripsi " &
                "From T_SPContoh inner join m_KodeProduk on " &
                "      Kode_Produk = KodeProduk " &
                "Where NoSP = '" & NoSP.Text & "' And T_SPContoh.AktifYN = 'Y' "
            dbS = Proses.ExecuteQuery(MsgSQL)
            For A = 0 To dbS.Rows.Count - 1
                Application.DoEvents()
                IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_DPBSample", "PB")
                KodeProduk.Text = dbS.Rows(A) !Kode_Produk
                Produk.Text = dbS.Rows(A) !Deskripsi
                SatuanHBeli.Text = dbS.Rows(A) !HargaBeli
                MsgSQL = "INSERT INTO t_DPBSample(IDRec, NoDPB, TglDPB, " &
                    "NoSP, KodePerajin, Perajin, KodeProduk, Produk, Jumlah, SatuanHBeli, " &
                    "OngKir, AktifYN, UserID, LastUPD, UserRev, TglRev, TransferYN) " &
                    "VALUES('" & IDRecord.Text & "', '" & Trim(nodpb.Text) & "', " &
                    "'" & Format(TglDPB.Value, "yyyy-MM-dd") & "', '" & Trim(NoSP.Text) & "', " &
                    "'" & Trim(Kode_Perajin.Text) & "', '" & Trim(Perajin.Text) & "'," &
                    "'" & Trim(dbS.Rows(A) !Kode_Produk) & "', '" & Trim(dbS.Rows(A) !Deskripsi) & "', " &
                    "" & dbS.Rows(A) !Jumlah & ", " & Trim(dbS.Rows(A) !HargaBeli) & ", " &
                    "" & OngKir.Text * 1 & ",'Y', '" & UserID & "', GetDate(), '', '', 'N') "
                Proses.ExecuteNonQuery(MsgSQL)
            Next A
        Else
            MsgSQL = "Update t_DPBSample " &
                "Set AktifYN = 'N' " &
                "Where NoDPB = '" & nodpb.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            MsgSQL = "Select Kode_Produk, Jumlah, HargaBeli, Deskripsi " &
                "From T_SPContoh inner join m_KodeProduk on " &
                "      Kode_Produk = KodeProduk " &
                "Where NoSP = '" & NoSP.Text & "' And T_SPContoh.AktifYN = 'Y' "
            dbS = Proses.ExecuteQuery(MsgSQL)
            For A = 0 To dbS.Rows.Count - 1
                Application.DoEvents()
                IDRecord.Text = Proses.MaxNoUrut("IDRec", "t_DPBSample", "PB")
                KodeProduk.Text = dbS.Rows(A) !Kode_Produk
                Produk.Text = dbS.Rows(A) !Deskripsi
                SatuanHBeli.Text = dbS.Rows(A) !HargaBeli
                MsgSQL = "INSERT INTO t_DPBSample(IDRec, NoDPB, TglDPB, " &
                    "NoSP, KodePerajin, Perajin, KodeProduk, Produk, Jumlah, SatuanHBeli, " &
                    "OngKir, AktifYN, UserID, LastUPD, UserRev, TglRev, TransferYN) " &
                    "VALUES('" & IDRecord.Text & "', '" & Trim(nodpb.Text) & "', " &
                    "'" & Format(TglDPB.Value, "yyyy-MM-dd") & "', '" & Trim(NoSP.Text) & "', " &
                    "'" & Trim(Kode_Perajin.Text) & "', '" & Trim(Perajin.Text) & "'," &
                    "'" & Trim(dbS.Rows(A) !Kode_Produk) & "', '" & Trim(dbS.Rows(A) !Deskripsi) & "', " &
                    "" & dbS.Rows(A) !Jumlah & ", " & Trim(dbS.Rows(A) !HargaBeli) & ", " &
                    "" & OngKir.Text * 1 & ",'Y', '" & UserID & "', GetDate(), '', '', 'N') "
                Proses.ExecuteNonQuery(MsgSQL)
            Next A
            MsgSQL = "Delete t_DPBSample " &
                "Where AktifYN = 'N' " &
                " AND NoDPB = '" & nodpb.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        End If

        LAdd = False
        LEdit = False
        AturTombol(True)
        Call DaftarDPBSample()
    End Sub
    Private Sub DaftarDPBSample()
        Dim mKondisi As String = ""
        If Trim(xJumlah.Text) <> "" Then
            mKondisi = " And Nama Like '" & Trim(xJumlah.Text) & "%' "
        End If
        Me.Cursor = Cursors.WaitCursor
        SQL = "Select Distinct  NoDPB, Perajin, TglDPB, NoSP, right(nodpb,2) + subString(nodpb, 8,3)  " &
            " From t_DPBSample " &
            "Where AktifYN = 'Y' " &
            "Order By right(nodpb,2) + subString(nodpb, 8,3) desc "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView2.Rows.Clear()
        DGView.Rows.Clear()
        DGView.Visible = False
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                DGView.Rows.Add(.Table.Rows(a) !nodpb,
                    .Table.Rows(a) !Perajin,
                    Format(.Table.Rows(a) !TglDPB, "dd-MM-yyyy"),
                    .Table.Rows(a) !NoSP)
            Next (a)
        End With
        DGView.Visible = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_DPBSample " &
            "Where AktifYN = 'Y' " &
            "  And NoDPB = '" & nodpb.Text & "' " &
            "ORDER BY TglDPB desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            IDRecord.Text = RSNav.Rows(0) !IDRec
            Call IsiDPBSample(IDRecord.Text, "")
        End If
        Proses.CloseConn()
    End Sub

    Private Sub nodpb_TextChanged(sender As Object, e As EventArgs) Handles nodpb.TextChanged

    End Sub

    Private Sub NoSP_TextChanged(sender As Object, e As EventArgs) Handles NoSP.TextChanged
        If Len(NoSP.Text) < 1 Then
            OngKir.Text = 0
            Perajin.Text = ""
            Kode_Perajin.Text = ""
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
    End Sub

    Private Sub OngKir_TextChanged(sender As Object, e As EventArgs) Handles OngKir.TextChanged
        If Trim(OngKir.Text) = "" Then OngKir.Text = 0
        If IsNumeric(OngKir.Text) Then
            Dim temp As Double = OngKir.Text
            OngKir.SelectionStart = OngKir.TextLength
        Else
            OngKir.Text = 0
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
        PanelProduk.Visible = tAktif
        PanelNavigate.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif
    End Sub

    Private Sub IsiDPBSample(tIdRec As String, tKode As String)
        Dim dbIsi As New DataTable, mKondisi As String = ""
        If tKode = "" Then
            mKondisi = ""
        Else
            mKondisi = " AND t_dpbsample.KodeProduk = '" & tKode & "' "
        End If
        SQL = "SELECT IDRec, nodpb, TglDPB, nosp, kodeperajin, t_dpbsample.kodeproduk, " &
            "         deskripsi, jumlah, satuanHBeli, ongkir, file_foto " &
            "FROM T_DPBSample inner join m_KodeProduk on " &
            "     t_dpbsample.KodeProduk = m_kodeproduk.KodeProduk " &
            "WHERE t_dpbsample.AktifYN = 'Y' " &
            " AND t_dpbsample.idrec = '" & tIdRec & "' " &
            " " & mKondisi & " "
        dbIsi = Proses.ExecuteQuery(SQL)
        If dbIsi.Rows.Count <> 0 Then
            IDRecord.Text = dbIsi.Rows(0) !IDRec
            nodpb.Text = dbIsi.Rows(0) !nodpb
            TglDPB.Value = dbIsi.Rows(0) !TglDPB
            NoSP.Text = dbIsi.Rows(0) !NoSP
            Kode_Perajin.Text = dbIsi.Rows(0) !KodePerajin
            KodeProduk.Text = dbIsi.Rows(0) !KodeProduk
            Produk.Text = dbIsi.Rows(0) !deskripsi
            Jumlah.Text = dbIsi.Rows(0) !Jumlah
            SatuanHBeli.Text = Format(dbIsi.Rows(0) !SatuanHBeli, "###,##0")
            OngKir.Text = Format(dbIsi.Rows(0) !OngKir, "###,##0")
            LocGmb1.Text = dbIsi.Rows(0) !file_foto
            SQL = "select Nama from m_KodePerajin where kodePerajin = '" & Kode_Perajin.Text & "' "
            Perajin.Text = Proses.ExecuteSingleStrQuery(SQL)
        End If
        Proses.CloseConn()

        If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
            ShowFoto("")
        Else
            ShowFoto(LocGmb1.Text)
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        ClearTextBoxes()
        AturTombol(False)

        SQL = "Select convert(Char(2), GetDate(), 12) thn, isnull(Max(substring(NoDPB,8,3)),0) + 10001 RecId  From t_DPBSample Where right(nodpb,2) = convert(Char(2), GetDate(), 12) "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            nodpb.Text = "DPBSPL-" + Microsoft.VisualBasic.Right(dbTable.Rows(0) !recid, 3) + "/" + dbTable.Rows(0) !thn
        End If
        Proses.CloseConn()
        NoSP.Focus()

    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(nodpb.Text) = "" Then
            MsgBox("Silakan cari no DPB yg akan di revisi!", vbCritical + vbOKOnly, "No DPB masih kosong!")
            Exit Sub
        Else
            Call IsiDPBSample(IDRecord.Text, "")
        End If
        Kode_Perajin.ReadOnly = True
        LAdd = False
        LEdit = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click

        If Trim(nodpb.Text) = "" Then
            MsgBox("Silakan cari no DPB yg akan di HAPUS!", vbCritical + vbOKOnly, "No DPB masih kosong!")
            Exit Sub
        End If
        If MsgBox("Yakin hapus data " & Trim(nodpb.Text) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update t_DPBSample SET AktifYN= 'N', " &
                     " UserID = '" & UserID & "', LastUPD = GetDate() " &
                 "Where NoDPB = '" & nodpb.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            ClearTextBoxes()
            DaftarDPBSample()
        End If
    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged

    End Sub

    Private Sub Form_DPBContoh_Load(sender As Object, e As EventArgs) Handles Me.Load

        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        TglDPB.Value = Now()

        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        Me.Cursor = Cursors.Default

        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With

        With Me.DGView2.RowTemplate
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

        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        DaftarDPBSample()
        SQL = "Select Top 1 * From t_DPBSample " &
            " where aktifYN = 'Y' " &
            "Order By TglDPB, IDRec  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            IDRecord.Text = dbTable.Rows(0) !IDRec
        Else
            IDRecord.Text = ""
        End If
        Call IsiDPBSample(IDRecord.Text, "")
        tTambah = Proses.UserAksesTombol(UserID, "27_DPB_CONTOH", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "27_DPB_CONTOH", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "27_DPB_CONTOH", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "27_DPB_CONTOH", "laporan")
        AturTombol(True)
    End Sub

    Private Sub SatuanHBeli_TextChanged(sender As Object, e As EventArgs) Handles SatuanHBeli.TextChanged

    End Sub

    Private Sub NoSP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoSP.KeyPress
        SQL = "Select * From T_SPContoh " &
            "Where AktifYN = 'Y' " &
            " And NoSP = '" & NoSP.Text & "' "
        If e.KeyChar = Chr(13) Then
            SQL = "Select nosp, kode_perajin From T_SPContoh " &
                "Where AktifYN = 'Y' " &
                " And NoSP = '" & NoSP.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NoSP.Text = dbTable.Rows(0) !nosp
                Kode_Perajin.Text = dbTable.Rows(0) !Kode_Perajin
                SQL = "select Nama from m_KodePerajin where kodePerajin = '" & Kode_Perajin.Text & "' "
                Perajin.Text = Proses.ExecuteSingleStrQuery(SQL)
                OngKir.Focus()
            Else
                Dim mKondisi As String = ""
                If NoSP.Text = "" Then
                    mKondisi = ""
                Else
                    mKondisi = "  And ( nosp Like '%" & NoSP.Text & "%' ) "
                End If
                SQL = "Select NoSP, T_SPContoh.Kode_Perajin, m_KodePerajin.Nama Perajin  " &
                    " From T_SPContoh inner join m_KodePerajin on T_SPContoh.Kode_Perajin = m_KodePerajin.KodePerajin " &
                    "Where T_SPContoh.AktifYN = 'Y' " &
                    "  " & mKondisi & " " &
                    "Group By NoSP, Kode_Perajin, m_KodePerajin.Nama, TglSP, right(nosp,3)+left(nosp,3)  " &
                    "Order By TglSP desc, right(nosp,3)+left(nosp,3) desc "
                Form_Daftar.txtQuery.Text = SQL
                Form_Daftar.Text = "Daftar SP Contoh"
                Form_Daftar.ShowDialog()
                NoSP.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nosp, kode_perajin From T_SPContoh " &
                    "Where AktifYN = 'Y' " &
                    " And NoSP = '" & NoSP.Text & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    NoSP.Text = dbTable.Rows(0) !nosp
                    Kode_Perajin.Text = dbTable.Rows(0) !Kode_Perajin
                    SQL = "select Nama from m_KodePerajin where kodePerajin = '" & Kode_Perajin.Text & "' "
                    Perajin.Text = Proses.ExecuteSingleStrQuery(SQL)
                    OngKir.Focus()
                Else
                    OngKir.Text = 0
                    Perajin.Text = ""
                    Kode_Perajin.Text = ""
                    NoSP.Text = ""
                    NoSP.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, Rs As New DataTable
        Dim tIdRec As String
        MsgSQL = "Select Top 1 * From t_DPBSample " &
            " where nodpb = '" & nodpb.Text & "'  and aktifYN = 'Y' " &
            "Order By TglDPB, IDRec  "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            tIdRec = Rs.Rows(0) !IDRec
        Else
            tIdRec = ""
        End If
        Call IsiDPBSample(tIdRec, "")
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
            If LAdd Or LEdit Then SatuanHBeli.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Jumlah_GotFocus(sender As Object, e As EventArgs) Handles Jumlah.GotFocus
        With Jumlah
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub OngKir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles OngKir.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If OngKir.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(OngKir.Text) Then
                Dim temp As Double = OngKir.Text
                OngKir.Text = Format(temp, "###,##0.00")
                OngKir.SelectionStart = OngKir.TextLength
            Else
                OngKir.Text = 0
            End If
            If LAdd Or LEdit Then cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_DPBSample " &
            "Where AktifYN = 'Y' " &
            "  And IDRec < '" & IDRecord.Text & "' " &
            "  And NoDPB = '" & nodpb.Text & "' " &
            "ORDER BY TglDPB Desc, IDRec Desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            IDRecord.Text = RSNav.Rows(0) !IDRec
        Else
            IDRecord.Text = ""
        End If
        If RSNav.Rows.Count <> 0 Then
            IDRecord.Text = RSNav.Rows(0) !IDRec
            Call IsiDPBSample(IDRecord.Text, "")
        End If
        Proses.CloseConn()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub OngKir_GotFocus(sender As Object, e As EventArgs) Handles OngKir.GotFocus
        With OngKir
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress

        If e.KeyChar = Chr(13) Then
            SQL = "SELECT Deskripsi FROM m_KodeProduk " &
                "WHERE AktifYN = 'Y' " &
                "  AND  kodeproduk = '" & KodeProduk.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Produk.Text = dbTable.Rows(0) !deskripsi
                Jumlah.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                        " From m_KodeProduk " &
                        "Where AktifYN = 'Y' " &
                        "  And ( KodeProduk Like '%" & KodeProduk.Text & "%' or Deskripsi Like '%" & KodeProduk.Text & "%') " &
                        "Order By Deskripsi "
                Form_Daftar.Text = "Daftar Produk"
                Form_Daftar.ShowDialog()
                KodeProduk.Text = FrmMenuUtama.TSKeterangan.Text
                SQL = "Select deskripsi " &
                    " From m_KodeProduk " &
                    "Where aktifyn = 'Y' " &
                    " AND KodeProduk = '" & KodeProduk.Text & "' "
                Produk.Text = Proses.ExecuteSingleStrQuery(SQL)
                If Trim(Produk.Text) = "" Then
                    KodeProduk.Text = ""
                    Produk.Text = ""
                    KodeProduk.Focus()
                Else
                    Jumlah.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub SatuanHBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SatuanHBeli.KeyPress
        If e.KeyChar = Chr(13) Then
            OngKir.Focus()
        End If
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

    Private Sub TabControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseClick
        If TabControl1.SelectedIndex.ToString = "1" Then
            DaftarDPBSample()
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, RSP As New DataTable
        Dim tJumlah As Double = 0, totQTY As Double = 0, totNilai As Double = 0
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Visible = False
        DGView2.Rows.Clear()
        nodpb.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "Select * From T_DPBSample " &
            "Where AktifYN = 'Y' " &
            "  And NoDPB = '" & nodpb.Text & "' "
        RSP = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSP.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSP.Rows(a) !IDRec,
                RSP.Rows(a) !KodeProduk,
                RSP.Rows(a) !Produk,
                Format(RSP.Rows(a) !Jumlah, "###,##0"),
                Format(RSP.Rows(a) !SatuanHBeli, "###,##0"),
                Format(RSP.Rows(a) !OngKir, "###,##0"))
            tJumlah = tJumlah + 1
            totQTY = totQTY + RSP.Rows(a) !Jumlah
            totNilai = totNilai + RSP.Rows(a) !SatuanHBeli
        Next (a)
        xNilai.Text = Format(totNilai, "###,##0")
        xQty.Text = Format(totQTY, "###,##0")
        xJumlah.Text = Format(tJumlah, "###,##0")
        If DGView2.Rows.Count <> 0 Then
            DGView2.Focus()
            IDRecord.Text = DGView2.Rows(0).Cells(0).Value
            KodeProduk.Text = DGView2.Rows(0).Cells(1).Value
            IsiDPBSample(IDRecord.Text, KodeProduk.Text)
        End If
        DGView2.Visible = True
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_DPBSample " &
            "Where AktifYN = 'Y' " &
            "  And IDRec > '" & IDRecord.Text & "' " &
            "  And NoDPB = '" & nodpb.Text & "' " &
            "ORDER BY TglDPB, IDRec  "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            IDRecord.Text = RSNav.Rows(0) !IDRec
        Else
            IDRecord.Text = ""
        End If
        If RSNav.Rows.Count <> 0 Then
            IDRecord.Text = RSNav.Rows(0) !IDRec
            Call IsiDPBSample(IDRecord.Text, "")
        End If
        Proses.CloseConn()
    End Sub
End Class