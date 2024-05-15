Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop


Public Class Form_GD_Retur
    Private CN As SqlConnection
    Protected Dt As DataTable
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        lKoordinator As String, lPemeriksa As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_Retur " &
            "Where AktifYN = 'Y' " &
            "  And NoRetur = '" & NoRetur.Text & "' " &
            "ORDER BY tglretur Desc, NoRetur Desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiRetur(RSNav.Rows(0) !IdRec)
        End If
        Proses.CloseConn()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_Retur " &
            "Where AktifYN = 'Y' " &
            "  And IDRec > '" & idRec.Text & "' " &
            "  And NoRetur = '" & NoRetur.Text & "' " &
            "ORDER BY IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiRetur(RSNav.Rows(0) !IdRec)
        End If
        Proses.CloseConn()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_Retur " &
            "Where AktifYN = 'Y' " &
            "  And IDRec < '" & idRec.Text & "' " &
            "  And NoRetur = '" & NoRetur.Text & "' " &
            "ORDER BY IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiRetur(RSNav.Rows(0) !IdRec)
        End If
        Proses.CloseConn()
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_Retur " &
            "Where AktifYN = 'Y' " &
            "  And NoRetur = '" & NoRetur.Text & "' " &
            "ORDER BY tglretur, NoRetur, IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiRetur(RSNav.Rows(0) !IdRec)
        End If
        Proses.CloseConn()
    End Sub


    Private Sub IsiRetur(tCari As String)
        Dim MsgSQL As String, Rs As New DataTable
        On Error GoTo ErrMSG
        MsgSQL = "Select * " &
            " From T_Retur " &
            "Where AktifYN = 'Y' " &
            "  and idrec = '" & tCari & "' "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            idRec.Text = Rs.Rows(0) !IdRec
            NoRetur.Text = Rs.Rows(0) !NoRetur
            tglRetur.Value = Rs.Rows(0) !tglRetur
            NoLHP.Text = Rs.Rows(0) !NoLHP
            Kode_Produk.Text = Rs.Rows(0) !KodeProduk
            Produk.Text = Rs.Rows(0) !Produk
            JumlahRetur.Text = Rs.Rows(0) !JumlahRetur
            NoSP.Text = Rs.Rows(0) !NoSP
            Kode_Perajin.Text = Rs.Rows(0) !KodePerajin
            Perajin.Text = Rs.Rows(0) !Perajin
            Kargo.Text = Rs.Rows(0) !Kargo
            JumlahKoli.Text = Rs.Rows(0) !JumlahKoli
            Pemeriksa.Text = Rs.Rows(0) !Pemeriksa
            BagianRetur.Text = Rs.Rows(0) !BagianRetur
            Sebabditolak.Text = Rs.Rows(0) !Sebabditolak

            LocGmb1.Text = Trim(Kode_Produk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
        End If
ErrMSG:
        If Err.Number <> 0 Then
            MsgBox(Err.Description, vbCritical + vbOKOnly, ".:Warning!")
        End If
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
        Kode_Produk.Visible = True
        JumlahRetur.Visible = True
        NoSP.Visible = True
        Sebabditolak.Visible = True
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub cmdTambahRetur_Click(sender As Object, e As EventArgs) Handles cmdTambahRetur.Click
        LAdd = False
        LEdit = False
        LTambahKode = True
        AturTombol(False)
        NoLHP.Text = ""
        Kode_Produk.Text = ""
        JumlahRetur.Text = 0
        NoSP.Text = ""
        JumlahKoli.Text = 0
        Sebabditolak.Text = ""
        Produk.Text = ""
        NoLHP.Focus()
        Kode_Produk.Visible = True
        JumlahRetur.Visible = True
        NoSP.Visible = True
        Sebabditolak.Visible = True
    End Sub


    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        LAdd = False
        LEdit = False
        LTambahKode = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
        Kode_Produk.Visible = False
        JumlahRetur.Visible = False
        NoSP.Visible = False
        Sebabditolak.Visible = False
        NoLHP.Text = ""
        Kode_Produk.Text = ""
        JumlahRetur.Text = 0
        NoSP.Text = ""
        JumlahKoli.Text = 0
        Sebabditolak.Text = ""
        Produk.Text = ""
        NoLHP.Focus()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub NoLHP_TextChanged(sender As Object, e As EventArgs) Handles NoLHP.TextChanged
        If Len(NoLHP.Text) < 1 Then
            If LTambahKode Then Exit Sub
            Kode_Perajin.Text = ""
            Perajin.Text = ""
            JumlahKoli.Text = ""
            Kode_Produk.Text = ""
            Produk.Text = ""
            Sebabditolak.Text = ""
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        AturTombol(False)
        ClearTextBoxes()

        Kode_Produk.Visible = False
        JumlahRetur.Visible = False
        NoSP.Visible = False
        Sebabditolak.Visible = False

        NoRetur.Text = Proses.MaxYNoUrut("NoRetur", "T_RETUR", "RET")
        NoLHP.Focus()
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
        tglRetur.Value = Now
        ShowFoto("")
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

    Private Sub JumlahRetur_TextChanged(sender As Object, e As EventArgs) Handles JumlahRetur.TextChanged
        If Trim(JumlahRetur.Text) = "" Then JumlahRetur.Text = 0
        If IsNumeric(JumlahRetur.Text) Then
            Dim temp As Double = JumlahRetur.Text
            JumlahRetur.SelectionStart = JumlahRetur.TextLength
        Else
            JumlahRetur.Text = 0
        End If
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim RS05 As New DataTable, MsgSQL As String
        If LAdd Or LEdit Or LTambahKode Then
            If JumlahKoli.Text = "" Then
                MsgBox("Jumlah koli masih kosong!", vbCritical + vbOKOnly, ".:Qarning !")
                JumlahKoli.Focus()
                Exit Sub
            End If
            If LAdd Then
                MsgSQL = "Select NoRetur From t_Retur " &
                    "Where Noretur = '" & NoRetur.Text & "' " &
                    "  And AktifYN = 'Y' "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                If RS05.Rows.Count <> 0 Then
                    MsgBox("No Retur sudah ada!", vbCritical + vbOKOnly, ".:Data Double !")
                    NoRetur.Focus()
                    Exit Sub
                End If

                MsgSQL = "Select * From t_LHP " &
                    "Where NoLHP = '" & NoLHP.Text & "' " &
                    " And AktifYN = 'Y' " &
                    " And JumlahTolak <> 0 "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                For i = 0 To RS05.Rows.Count - 1
                    idRec.Text = Proses.MaxNoUrut("IDRec", "t_Retur", "RB")
                    MsgSQL = "INSERT INTO t_Retur(IDRec, NoRetur, TglRetur, NoLHP, KodeProduk, " &
                        "Produk, JumlahRetur, NoSP, KodePerajin, Perajin, Kargo, JumlahKoli, " &
                        "Pemeriksa, BagianRetur, SebabdiTolak, FotoLoc, AktifYN, UserID, " &
                        "LastUPD, TransferYN) VALUES ('" & idRec.Text & "', '" & NoRetur.Text & "', " &
                        "'" & Format(tglRetur.Value, "yyyy-MM-dd") & "', '" & Trim(RS05.Rows(i) !NoLHP) & "', " &
                        "'" & Trim(RS05.Rows(i) !Kode_Produk) & "', '" & Trim(RS05.Rows(i) !Produk) & "', " &
                        "" & RS05.Rows(i) !jumlahtolak & ", '" & Trim(RS05.Rows(i) !NoSP) & "', " &
                        "'" & Kode_Perajin.Text & "', '" & Trim(RS05.Rows(i) !NamaPerajin) & "', " &
                        "'" & Trim(Kargo.Text) & "', " & JumlahKoli.Text & " , " &
                        "'" & Trim(Pemeriksa.Text) & "', '" & Trim(BagianRetur.Text) & "', " &
                        "'" & Trim(RS05.Rows(i) !AlasanDiTolak) & "','" & Trim(LocGmb1.Text) & "', " &
                        "'Y', '" & UserID & "', GetDate(), 'N')"
                    Proses.ExecuteNonQuery(MsgSQL)
                Next i
                LAdd = False
                LEdit = False
                LTambahKode = False
                AturTombol(True)
            ElseIf LEdit Then
                MsgSQL = "Delete T_Retur Where IDrec = '" & idRec.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "Select * From t_LHP " &
                "Where NoLHP = '" & NoLHP.Text & "' " &
                " And AktifYN = 'Y' " &
                " And JumlahTolak <> 0 "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                For i = 0 To RS05.Rows.Count - 1
                    MsgSQL = "INSERT INTO t_Retur(IDRec, NoRetur, TglRetur, NoLHP, KodeProduk, " &
                        "Produk, JumlahRetur, NoSP, KodePerajin, Perajin, Kargo, JumlahKoli, " &
                        "Pemeriksa, BagianRetur, SebabdiTolak, FotoLoc, AktifYN, UserID, " &
                        "LastUPD, TransferYN) VALUES ('" & idRec.Text & "', '" & NoRetur.Text & "', " &
                        "'" & Format(tglRetur.Value, "yyyy-MM-dd") & "', '" & Trim(RS05.Rows(i) !NoLHP) & "', " &
                        "'" & Trim(RS05.Rows(i) !Kode_Produk) & "', '" & Trim(RS05.Rows(i) !Produk) & "', " &
                        "" & RS05.Rows(i) !jumlahtolak & ", '" & Trim(RS05.Rows(i) !NoSP) & "', " &
                        "'" & Kode_Perajin.Text & "', '" & Trim(RS05.Rows(i) !NamaPerajin) & "', " &
                        "'" & Trim(Kargo.Text) & "', " & JumlahKoli.Text & " , " &
                        "'" & Trim(Pemeriksa.Text) & "', '" & Trim(BagianRetur.Text) & "', " &
                        "'" & Trim(RS05.Rows(i) !AlasanDiTolak) & "','" & Trim(LocGmb1.Text) & "', " &
                        "'Y', '" & UserID & "', GetDate(), 'N')"
                    Proses.ExecuteNonQuery(MsgSQL)
                Next i
                LAdd = False
                LEdit = False
                LTambahKode = False
                AturTombol(True)
            ElseIf LTambahKode Then
                MsgSQL = "INSERT INTO t_Retur(IDRec, NoRetur, TglRetur, " &
                    "NoLHP, KodeProduk, Produk, JumlahRetur, NoSP, KodePerajin, " &
                    "Perajin, Kargo, JumlahKoli, Pemeriksa, BagianRetur, " &
                    "SebabdiTolak, FotoLoc, AktifYN, UserID, LastUPD, TransferYN) " &
                    "VALUES ('" & idRec.Text & "', '" & NoRetur.Text & "', " &
                    "'" & Format(tglRetur.Value, "yyyy-MM-dd") & "', '" & NoLHP.Text & "', " &
                    "'" & Kode_Produk.Text & "', '" & Trim(Produk.Text) & "', " &
                    "" & JumlahRetur.Text * 1 & ", '" & Trim(NoSP.Text) & "', " &
                    "'" & Kode_Perajin.Text & "', '" & Trim(Perajin.Text) & "', " &
                    "'" & Trim(Kargo.Text) & "', " & JumlahKoli.Text & " , " &
                    "'" & Trim(Pemeriksa.Text) & "', '" & Trim(BagianRetur.Text) & "', " &
                    "'" & Trim(Sebabditolak.Text) & "','" & Trim(LocGmb1.Text) & "', " &
                    "'Y', '" & UserID & "', GetDate(), 'N')"
                Proses.ExecuteNonQuery(MsgSQL)
                LTambahKode = True
                LAdd = False
                LEdit = False
                Kode_Produk.Text = ""
                Produk.Text = ""
                JumlahRetur.Text = 0
                NoSP.Text = ""
                Kode_Produk.Focus()
            End If
        End If
        Kode_Produk.Visible = True
        JumlahRetur.Visible = True
        NoSP.Visible = True
        Sebabditolak.Visible = True
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim KabagGudang As String = ""

        MsgSQL = "Select Retur From m_Company"
        KabagGudang = Proses.ExecuteSingleStrQuery(MsgSQL)
        Me.Cursor = Cursors.WaitCursor

        Proses.OpenConn(CN)
        dttable = New DataTable

        MsgSQL = "SELECT t_Retur.IDRec, t_Retur.NoRetur, t_Retur.TglRetur, " &
            "t_Retur.NoLHP, t_Retur.KodeProduk, t_Retur.Produk, t_Retur.JumlahRetur, " &
            "t_Retur.NoSP, t_Retur.Perajin, t_Retur.Kargo, t_Retur.JumlahKoli, " &
            "t_Retur.Pemeriksa, t_Retur.BagianRetur, t_Retur.SebabdiTolak " &
            "FROM Pekerti.dbo.t_Retur t_Retur " &
            "Where NoRetur = '" & NoRetur.Text & "' " &
            "  And AktifYN = 'Y' " &
            "ORDER BY t_Retur.NoSP ASC, T_Retur.NoLHP "
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)

            objRep = New Rpt_Retur
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("KabagGudang", KabagGudang)
            Form_Report.Text = "Cetak Retur"
            Form_Report.CrystalReportViewer1.ShowExportButton = True
            Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_Report.CrystalReportViewer1.Refresh()
            Form_Report.CrystalReportViewer1.ReportSource = objRep
            Form_Report.CrystalReportViewer1.ShowRefreshButton = True
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
        cmdTambahRetur.Visible = tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True

        NoRetur.ReadOnly = tAktif
        tglRetur.Enabled = Not tAktif
        NoLHP.ReadOnly = tAktif
        JumlahRetur.ReadOnly = tAktif
        NoSP.ReadOnly = tAktif
        Kode_Perajin.ReadOnly = tAktif
        Perajin.ReadOnly = True
        Kargo.ReadOnly = tAktif
        JumlahKoli.ReadOnly = tAktif
        Pemeriksa.ReadOnly = tAktif
        BagianRetur.ReadOnly = tAktif
        Sebabditolak.ReadOnly = tAktif
        Produk.ReadOnly = True
        Me.Text = "Retur Barang"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub Form_GD_Retur_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String
        LAdd = False
        LEdit = False
        LTambahKode = False
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Dim rs05 As New DataTable
        MsgSQL = "Select KoordinatorLHP, pemeriksa From M_Company "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            lKoordinator = rs05.Rows(0) !KoordinatorLHP
            lPemeriksa = rs05.Rows(0) !Pemeriksa
        End If
        Dim Rs As New DataTable
        MsgSQL = "Select Top 1 * From t_retur " &
            "where AktifYN = 'Y' " &
            "Order By tglRetur Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            idRec.Text = Rs.Rows(0) !IDRec
        Else
            idRec.Text = ""
        End If
        Call IsiRetur(idRec.Text)
        tTambah = Proses.UserAksesTombol(UserID, "57_RETUR", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "57_RETUR", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "57_RETUR", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "57_RETUR", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        IsiReturBarang()
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
    End Sub


    Private Sub IsiReturBarang()
        Dim MsgSQL As String, rsdaftar As New DataTable
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select NoRetur, TglRetur, Perajin " &
            "  From T_Retur " &
            " Where AktifYN = 'Y' " &
            " group by NoRetur, TglRetur, Perajin  " &
            " order By TglRetur desc, NoRetur desc "
        rsdaftar = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsdaftar.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsdaftar.Rows(a) !NoRetur,
                   Format(rsdaftar.Rows(a) !tglRetur, "dd-MM-yyyy"),
                   rsdaftar.Rows(a) !Perajin)
        Next a
        Me.Cursor = Cursors.Default
        DGView.Visible = True
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String
        Dim RSL As New DataTable
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Rows.Clear()
        DGView2.Visible = False

        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "Select * " &
            "From t_Retur " &
            "Where AktifYN = 'Y' " &
            "  And NoRetur = '" & tCari & "' " &
            "ORDER BY NoRetur, IDRec "
        RSL = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSL.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSL.Rows(a) !idrec,
                   Microsoft.VisualBasic.Left(RSL.Rows(a) !KodeProduk & Space(15), 15) & RSL.Rows(a) !Produk,
                   Format(RSL.Rows(a) !JumlahRetur, "###,##0"),
                   RSL.Rows(a) !NoLHP,
                   RSL.Rows(a) !NoSP,
                   RSL.Rows(a) !Kargo)
        Next a
        DGView2.Visible = True
        If DGView2.Rows.Count <> 0 Then
            DGView2_CellClick(sender, e)
        End If
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        If DGView2.Rows.Count = 0 Then Exit Sub
        idRec.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        IsiRetur(idRec.Text)
    End Sub

    Private Sub NoLHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoLHP.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then

            MsgSQL = "Select KodePerajin, NamaPerajin, NOSP, TGLTERIMA " &
            " From T_LHP a " &
            "Where NoLHP = '" & NoLHP.Text & "' "
            dbTable = Proses.ExecuteQuery(MsgSQL)
            If dbTable.Rows.Count <> 0 Then
                Perajin.Text = dbTable.Rows(0) !NamaPerajin
                Kode_Perajin.Text = KodePerajin(dbTable.Rows(0) !NoSP)
                If LTambahKode Then
                    Kode_Produk.Focus()
                Else
                    Kargo.Focus()
                End If
            Else
                Dim rsn2 As New DataTable
                NoLHP.Text = FindLHP_Perajin(NoLHP.Text, Kode_Perajin.Text)
                MsgSQL = "Select NamaPerajin, NoSPB, KodePerajin, Kargo, NoSP " &
                    " From T_LHP a  " &
                    "Where NoLHP = '" & NoLHP.Text & "' "
                rsn2 = Proses.ExecuteQuery(MsgSQL)

                If rsn2.Rows.Count <> 0 Then
                    Perajin.Text = rsn2.Rows(0) !NamaPerajin
                    Kode_Perajin.Text = KodePerajin(rsn2.Rows(0) !NoSP)
                    If LTambahKode Then
                        Kode_Produk.Focus()
                    Else
                        Kargo.Focus()
                    End If
                Else
                    NoLHP.Text = ""
                    MsgBox("NO LHP tidak boleh kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                    NoLHP.Focus()
                    Exit Sub
                End If
            End If
            Proses.CloseConn()
        End If
    End Sub
    Private Function FindLHP_Perajin(tNOLHP As String, tPerajin As String) As String
        Dim RSD As New DataTable, mKondisi As String = ""
        Dim MsgSQL As String
        If Trim(tNOLHP) = "" Then
            mKondisi = "And NoLHP Like '%" & Trim(tNOLHP) & "%' "
        End If
        If Trim(tPerajin) <> "" Then
            mKondisi = " And KodePerajin = '" & tPerajin & "' "
        End If
        MsgSQL = "Select NoLHP, a.NamaPerajin, a.TglLHP " &
            " From T_LHP a  " &
            "Where a.AktifYN = 'Y' " & mKondisi & " " &
            "Group By NoLHP, a.NamaPerajin, a.TglLHP " &
            "Order By a.tglLHP desc, NoLHP Desc "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar LHP"
        Form_Daftar.ShowDialog()
        FindLHP_Perajin = Trim(FrmMenuUtama.TSKeterangan.Text)
        FrmMenuUtama.TSKeterangan.Text = ""
    End Function
    Private Function KodePerajin(NoSP As String) As String
        Dim MsgSQL As String
        MsgSQL = "select Kode_Perajin from t_SP where nosp = '" & NoSP & "' "
        KodePerajin = Proses.ExecuteSingleStrQuery(MsgSQL)
        MsgSQL = "Select nama From m_KodePerajin " &
            " Where KodePerajin = '" & KodePerajin & "' " &
            " and aktifyn = 'Y' "
        Perajin.Text = Proses.ExecuteSingleStrQuery(MsgSQL)
    End Function

    Private Sub JumlahRetur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JumlahRetur.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If JumlahRetur.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(JumlahRetur.Text) Then
                Dim temp As Double = JumlahRetur.Text
                JumlahRetur.Text = Format(temp, "###,##0.00")
                JumlahRetur.SelectionStart = JumlahRetur.TextLength
            Else
                JumlahRetur.Text = 0
            End If
            If LAdd Or LEdit Then Kargo.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
End Class