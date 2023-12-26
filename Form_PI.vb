Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO


Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Public Class Form_PI
    Dim FotoLoc As String = My.Settings.path_foto
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean, LUangMuka As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Private UsedVariables() As String
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
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

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        Nopo.ReadOnly = False
        Nopo.Focus()
        ClearTextBoxes()
        AturTombol(False)
        Nopo.ReadOnly = False
        NoPI.Text = Max_NoUrut("NoPI", "t_PI", Format(Now, "MM"))
    End Sub

    Public Function Max_NoUrut(tField As String, tTable As String, Kode As String) As String
        Dim MsgSQL As String, RsMax As New DataTable
        MsgSQL = "Select convert(Char(2), GetDate(), 12) TGL, isnull(Max(left(" & tField & ",3)),0) + 1000001 RecId " &
        " From " & tTable & " " &
        "Where Right(" & tField & ",2) = convert(Char(2), GetDate(), 12) And AktifYN = 'Y' "
        RsMax = Proses.ExecuteQuery(MsgSQL)
        Max_NoUrut = Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 3) + "/" + Kode + "/" + Trim(Str(RsMax.Rows(0) !tGL))
    End Function

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If idRecord.Text = "" Then
            MsgBox("Data PI yang akan di edit belum di pilih!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LAdd = False
        LTambahKode = False
        LEdit = True
        AturTombol(False)
        Nopo.ReadOnly = True
        Jumlah.Focus()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If Trim(idRecord.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            cmdHapus.Focus()
            Exit Sub
        End If
        If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation) = vbYes Then
            MsgSQL = "Update t_PI Set " &
                "   AktifYN = 'N', " &
                "TransferYN = 'N', " &
                "    UserID = '" & UserID & "', " &
                "   LastUPD = GetDate() " &
                "Where IDRec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            ClearTextBoxes()
        End If
    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged
        If Len(KodeProduk.Text) < 1 Then
            KodeProduk.Text = ""
            Produk.Text = ""
            ShowFoto("")
            Jumlah.Text = "0"
            HargaFOB.Text = "0"
            KodePImportir.Text = ""
        ElseIf Len(KodeProduk.Text) = 5 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
        ElseIf Len(KodeProduk.Text) = 8 Then
            KodeProduk.Text = KodeProduk.Text + "-"
            KodeProduk.SelectionStart = Len(Trim(KodeProduk.Text)) + 1
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
        cmdTambahanBiayaPI.Visible = tAktif
        cmdTambahKode.Visible = tAktif
        cmdUangMuka.Visible = tAktif
        cmdTutupPI.Visible = tAktif
        KodeProduk.BackColor = Color.White
        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True
        If LAdd Then
            KodeProduk.Visible = False
            Produk.Visible = False
            KodePImportir.Visible = False
            Jumlah.Visible = False
            QTYOSO.Visible = False
            cmbMataUang.Visible = False
            HargaFOB.Visible = False
            chk3Digit.Visible = False
            ShipmentDate.Visible = False
            PermintaanPO1.Visible = False
            PermintaanPO2.Visible = False
            PermintaanPO3.Visible = False
            Pemenuhan1.Visible = False
            Pemenuhan2.Visible = False
        ElseIf LTambahKode Then

            NoPI.Enabled = False
            Nopo.Enabled = False
            Kode_Importir.Enabled = False
            Importir.Enabled = False
            tglPO.Enabled = False
            TglKirim.Enabled = False
            Pelabuhan.Enabled = False
            cmbCaraKirim.Enabled = False
            KodeProduk.BackColor = Color.LightSeaGreen
        Else
            KodeProduk.Visible = tAktif
            Produk.Visible = tAktif
            KodePImportir.Visible = tAktif
            Jumlah.Visible = tAktif
            QTYOSO.Visible = tAktif
            cmbMataUang.Visible = tAktif
            HargaFOB.Visible = tAktif
            chk3Digit.Visible = tAktif
            ShipmentDate.Visible = tAktif
            PermintaanPO1.Visible = tAktif
            PermintaanPO2.Visible = tAktif
            PermintaanPO3.Visible = tAktif
            Pemenuhan1.Visible = tAktif
            Pemenuhan2.Visible = True
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
        tglPO.Value = Now
        tglPI.Value = Now
        TglKirim.Value = Now
        ShipmentDate.Value = Now
        chk3Digit.Checked = False
        FOBOSO.Text = 0
        HargaFOB.Text = 0
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

    Private Sub Nopo_TextChanged(sender As Object, e As EventArgs) Handles Nopo.TextChanged

    End Sub

    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress
        KodeProduk.BackColor = Color.White
        If e.KeyChar = Chr(13) Then
            Dim rs05 As New DataTable, mKondisi As String = ""
            Me.Cursor = Cursors.WaitCursor

            MsgSQL = "Select Deskripsi, Kode_Buyer, FOBBuyer, MataUang, " &
                    "        Kode_Produk, Kode_Importir, file_foto, " &
                    "        m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah " &
                    " From t_PO inner join m_KodeProduk ON " &
                    "      m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    "      inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & Nopo.Text & "' " &
                    "  And Kode_Produk = '" & KodeProduk.Text & "' "
            rs05 = Proses.ExecuteQuery(MsgSQL)
            If rs05.Rows.Count <> 0 Then
                Produk.Text = Replace(rs05.Rows(0) !Deskripsi, "'", "`")
                KodePImportir.Text = rs05.Rows(0) !Kode_Buyer
                Jumlah.Text = Format(rs05.Rows(0) !Jumlah, "###,##0")
                HargaFOB.Text = Format(rs05.Rows(0) !FOBBuyer, "###,##0.00")
                If Trim(cmbMataUang.Text) = "" Then
                    cmbMataUang.Text = "USD"
                Else
                    cmbMataUang.Text = rs05.Rows(0) !MataUang
                End If
                LocGmb1.Text = rs05.Rows(0) !file_foto
                If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                    ShowFoto("")
                Else
                    ShowFoto(LocGmb1.Text)
                End If
            Else


                If Trim(KodeProduk.Text) = "" Then
                    mKondisi = ""
                Else
                    mKondisi = "And Deskripsi like '%" & Trim(KodeProduk.Text) & "%' "
                End If


                SQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                    " m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah " &
                    " From t_PO inner join m_KodeProduk ON " &
                    " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & Nopo.Text & "' " &
                    " " & mKondisi & " "
                Form_Daftar.txtQuery.Text = SQL
                Form_Daftar.Text = "Daftar Produk PO"
                Form_Daftar.param1.Text = Nopo.Text
                Form_Daftar.ShowDialog()
                KodeProduk.Text = FrmMenuUtama.TSKeterangan.Text

                MsgSQL = "Select Deskripsi, Kode_Buyer, FOBBuyer, MataUang, " &
                    "      Kode_Produk, Kode_Importir, file_foto, " &
                    "      m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah " &
                    " From t_PO inner join m_KodeProduk ON " &
                    " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & Nopo.Text & "' " &
                    "  And Kode_Produk = '" & KodeProduk.Text & "' "
                rs05 = Proses.ExecuteQuery(MsgSQL)
                If rs05.Rows.Count <> 0 Then

                    Produk.Text = Replace(rs05.Rows(0) !Deskripsi, "'", "`")
                    KodePImportir.Text = rs05.Rows(0) !Kode_Buyer
                    Jumlah.Text = Format(rs05.Rows(0) !Jumlah, "###,##0")
                    HargaFOB.Text = Format(rs05.Rows(0) !FOBBuyer, "###,##0.00")
                    If Trim(cmbMataUang.Text) = "" Then
                        cmbMataUang.Text = "USD"
                    Else
                        cmbMataUang.Text = rs05.Rows(0) !MataUang
                    End If
                    LocGmb1.Text = rs05.Rows(0) !file_foto
                    If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                        ShowFoto("")
                    Else
                        ShowFoto(LocGmb1.Text)
                    End If
                Else
                    KodeProduk.Text = ""
                    Produk.Text = ""
                    HargaFOB.Text = "0"
                    FOBOSO.Text = "0"
                    KodePImportir.Text = ""
                    ShowFoto("")
                End If
            End If
            If LAdd Or LEdit Then
                If Produk.Text = "" Then
                    KodeProduk.Focus()
                Else
                    KodePImportir.Focus()
                End If
            ElseIf LTambahKode Then
                If CekDouble() Then
                    KodeProduk.Text = ""
                    KodeProduk.Focus()
                Else
                    KodePImportir.Focus()
                End If
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub


    Public Sub SetupToolTip()
        With Me.ToolTip1
            .AutomaticDelay = 0
            .AutoPopDelay = 30000
            .BackColor = System.Drawing.Color.AntiqueWhite
            .InitialDelay = 50
            .IsBalloon = False
            .ReshowDelay = 50
            .ShowAlways = True
            .Active = False
            .Active = True
            .SetToolTip(Me.btnWord, "Copy to Ms.Word")
            .SetToolTip(Me.btnExcel, "Copy to Ms.Excel")

        End With
    End Sub
    Private Sub TambahKode()
        If Trim(NoPI.Text) = "" Then
            MsgBox("No PI masih kosong!", vbCritical, ".:ERROR!")
            NoPI.Focus()
            Exit Sub
        End If
        LAdd = False
        LEdit = False
        LTambahKode = True
        AturTombol(False)

        KodeProduk.Text = ""
        KodePImportir.Text = ""
        Jumlah.Text = ""
        QTYOSO.Text = ""
        HargaFOB.Text = ""
        PermintaanPO1.Text = ""
        PermintaanPO2.Text = ""
        PermintaanPO3.Text = ""
        Pemenuhan1.Text = ""
        Pemenuhan2.Text = ""

        ShowFoto("")
        KodeProduk.Focus()

    End Sub
    Private Sub cmdTambahKode_Click(sender As Object, e As EventArgs) Handles cmdTambahKode.Click
        TambahKode()
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
    Private Sub Form_PI_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetupToolTip()
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
        cmbCaraKirim.Items.Clear()
        MsgSQL = "Select * From M_Kirim Order By CaraKirim "
        RS = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RS.Rows.Count - 1
            Application.DoEvents()
            cmbCaraKirim.Items.Add(RS.Rows(a) !CaraKirim)
        Next a
        UserID = FrmMenuUtama.TsPengguna.Text
        MsgSQL = "Select IDRec " &
            "From t_PI " &
            "Where AktifYN = 'Y' " &
            "Order By TglPI Desc, IDRec Desc "
        RS = Proses.ExecuteQuery(MsgSQL)
        If RS.Rows.Count <> 0 Then
            tIdRec = RS.Rows(0) !IDRec
        Else
            tIdRec = ""
            tKodeProduk = ""
        End If
        Call IsiPI(tIdRec)
        tTambah = Proses.UserAksesTombol(UserID, "45_PROFORMA_INVOICE", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "45_PROFORMA_INVOICE", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "45_PROFORMA_INVOICE", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "45_PROFORMA_INVOICE", "laporan")
        Me.Cursor = Cursors.Default
        DaftarPI("")
        AturTombol(True)
        GBoxUangMuka.Visible = False
    End Sub
    Private Sub DaftarPI(tCari As String)
        Dim MsgSQL As String
        Dim mKondisi As String, mNoPO As String, mKodeBrg As String
        Dim mNoPI As String = "", rsDaf As New DataTable
        If tCari = "" Then
            mKondisi = ""
        Else
            mKondisi = " And NoPI like '%" & tCari & "%' "
        End If
        If Trim(tNoPI.Text) <> "" Then
            If Trim(mKondisi) = "" Then mNoPI = " And NoPI like '%" & tCari & "%' "
        Else
            mNoPI = ""
        End If
        If Trim(tNoPO.Text) = "" Then
            mNoPO = ""
        Else
            mNoPO = " and NoPO like '%" & Trim(tNoPO.Text) & "%' "
        End If

        If Trim(tKodeBrg.Text) = "" Then
            mKodeBrg = ""
        Else
            mKodeBrg = " and Kode_Produk like '%" & Trim(tKodeBrg.Text) & "%' "
        End If
        DGView.Rows.Clear()
        MsgSQL = "Select Distinct NoPI, Right(NoPI,2), TglPI, NoPO, Importir, ShipmentDate " &
            "From t_PI " &
            "Where AktifYN = 'Y' " & mKondisi & mNoPI & mNoPO & mKodeBrg & " " &
            "Order By TGLPI Desc, Right(NoPI,2) Desc, NoPI Desc "
        rsDaf = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsDaf.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsDaf.Rows(a) !NoPi,
                rsDaf.Rows(a) !tglPi,
                rsDaf.Rows(a) !nopo, rsDaf.Rows(a) !importir,
                rsDaf.Rows(a) !ShipmentDate)
        Next a
        If DGView.Rows.Count <> 0 Then
            DGView.Columns(1).DefaultCellStyle.Format = "dd'-'MM'-'yyyy"
            DGView.Columns(4).DefaultCellStyle.Format = "dd'-'MM'-'yyyy"
        End If
    End Sub
    Private Sub IsiPI(tCode As String)
        Dim rsc As New DataTable
        MsgSQL = "SELECT * " &
            " FROM t_PI " &
            "Where T_PI.AktifYN = 'Y' " &
            "  And IDRec = '" & tCode & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 Then
            idRecord.Text = tCode
            NoPI.Text = rsc.Rows(0) !NoPI
            Nopo.Text = rsc.Rows(0) !NoPO
            tglPI.Value = rsc.Rows(0) !TglPI
            StatusPI.Text = rsc.Rows(0) !StatusPI
            Kode_Importir.Text = rsc.Rows(0) !Kode_Importir
            Importir.Text = rsc.Rows(0) !Importir
            tglPO.Value = rsc.Rows(0) !tglPO
            ShipmentDate.Value = rsc.Rows(0) !ShipmentDateKode
            TglKirim.Value = rsc.Rows(0) !ShipmentDate
            Pelabuhan.Text = rsc.Rows(0) !Pelabuhan
            CatatanPI.Text = rsc.Rows(0) !CatatanPI
            KodeProduk.Text = rsc.Rows(0) !Kode_Produk
            For i = 0 To cmbCaraKirim.Items.Count - 1
                cmbCaraKirim.SelectedIndex = i
                If cmbCaraKirim.Text = Trim(rsc.Rows(0) !CaraKirim) Then
                    Exit For
                End If
            Next i

            LocGmb1.Text = Trim(rsc.Rows(0) !Kode_Produk) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If

            Produk.Text = rsc.Rows(0) !Produk
            KodePImportir.Text = rsc.Rows(0) !Kode_PImport
            Jumlah.Text = rsc.Rows(0) !Jumlah
            QTYOSO.Text = rsc.Rows(0) !QTYOSO
            FOBOSO.Text = rsc.Rows(0) !FOBOSO
            If rsc.Rows(0) !MataUang = "" Then
                cmbMataUang.SelectedIndex = -1
            Else
                cmbMataUang.Text = rsc.Rows(0) !MataUang
            End If
            If rsc.Rows(0) !digit3yn = "Y" Then
                chk3Digit.Checked = True
                HargaFOB.Text = Format(rsc.Rows(0) !HargaFOB, "###,##0.000")
            Else
                chk3Digit.Checked = False
                HargaFOB.Text = Format(rsc.Rows(0) !HargaFOB, "###,##0.00")
            End If
            PermintaanPO1.Text = rsc.Rows(0) !PermintaanPO1
            PermintaanPO2.Text = rsc.Rows(0) !PermintaanPO2
            PermintaanPO3.Text = rsc.Rows(0) !PermintaanPO3
            Pemenuhan1.Text = rsc.Rows(0) !Pemenuhan1
            Pemenuhan2.Text = rsc.Rows(0) !Pemenuhan2
        End If
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSQL As String, rsc As New DataTable
        Dim RS05 As New DataTable, RSL As New DataTable
        Dim tigadigit As String = "N"
        If HargaFOB.Text = "" Then HargaFOB.Text = 0
        If LAdd Or LEdit Or LUangMuka Then
            If Trim(NoPI.Text) = "" Then
                MsgBox("No PI Tidak Boleh Kosong!", vbCritical, ".:ERROR!")
                NoPI.Focus()
                Exit Sub
            End If
            If Trim(cmbCaraKirim.Text) = "" Then
                MsgBox("Metode Pengiriman Masih Kosong!", vbCritical, ".:Error!")
                cmbCaraKirim.Focus()
                Exit Sub
            End If
            MsgSQL = "Select Kode_Produk, MataUang From t_PO Where NOPO = '" & Nopo.Text & "' and aktifYN = 'Y' and matauang = '' "
            RSL = Proses.ExecuteQuery(MsgSQL)
            If RSL.Rows.Count <> 0 Then
                MsgBox("Mata Uang " & RSL.Rows(0) !Kode_Produk & " Belum ada!", vbCritical, "Warning!")
                Exit Sub
            End If

            If LAdd Then
                MsgSQL = "Select Kode_produk From T_PI " &
                    "Where aktifYN = 'Y' " &
                    "  And noPo = '" & Nopo.Text & "' "
                RSL = Proses.ExecuteQuery(MsgSQL)
                If RSL.Rows.Count <> 0 Then
                    MsgBox("Maaf, NO PO sudah pernah di buatkan PI", vbCritical, "Double Cek!")
                    Nopo.Focus()
                    Exit Sub
                End If
                If Trim(Nopo.Text) = "" Then
                    MsgBox("No PO tidak boleh kosong!", vbCritical, ".:ERROR!")
                    Nopo.Focus()
                End If
                NoPI.Text = Max_NoUrut("NoPI", "t_PI", Format(Now, "MM"))
                MsgSQL = "Select t_PO.IDRec, m_KodeProduk.deskripsi, t_PO.Kode_Produk, " &
                    " m_KodeImportir.Nama AS Importir, t_PO.Kode_Importir, t_PO.Kode_Perajin, " &
                    " t_PO.Kode_Buyer, t_PO.Jumlah, t_PO.TglKirim, t_PO.MataUang, t_PO.FOBBuyer, " &
                    " t_PO.FOBUmum, t_PO.Digit3YN, t_PO.CatatanPO, t_PO.CatatanProduk, t_PO.FotoLoc, " &
                    " t_PO.StatusPO, t_PO.TglPO, t_PO.NoPO, t_PO.PembagiEuro " &
                    " From t_PO INNER JOIN m_KodeProduk ON " &
                    "   t_PO.Kode_Produk = m_KodeProduk.KodeProduk " &
                    "   INNER JOIN m_KodeImportir ON " &
                    "   m_KodeImportir.KodeImportir = t_PO.Kode_Importir " &
                    "Where t_PO.AktifYN = 'Y' " &
                    "  And m_KodeImportir.AktifYN = 'Y' " &
                    "  And m_KodeProduk.AktifYN = 'Y' " &
                    "  And NOPO = '" & Nopo.Text & "' " &
                    "Order By IDRec "
                RS05 = Proses.ExecuteQuery(MsgSQL)

                For a = 0 To RS05.Rows.Count - 1
                    Application.DoEvents()
                    idRecord.Text = Proses.MaxNoUrut("IDRec", "t_PI", "PI")
                    MsgSQL = "INSERT INTO t_PI(IDRec, NoPI, NoPO, TglPI, StatusPI, Kode_Importir, Importir, " &
                        "TglPO, ShipmentDate, Pelabuhan, CaraKirim, Kode_Produk, Produk, Kode_PImport, " &
                        "Jumlah, MataUang, HargaFOB, Digit3YN, ShipmentDateKode, PermintaanPO1, PermintaanPO2, " &
                        "PermintaanPO3, Pemenuhan1, Pemenuhan2, AktifYN, TransferYN, UserID, LastUPD, " &
                        "NilaiPI, UangMuka, KurangBayar, LabelingCost, SpecialPackaging, Fumigation, " &
                        "Phytosanitary, CatatanPI, QTYOSO, Konversi, FOBOSO, IdCompany) VALUES( '" & idRecord.Text & "',   " &
                        "'" & NoPI.Text & "', '" & Nopo.Text & "', '" & Format(tglPI.Value, "yyyy-MM-dd") & "', '', " &
                        "'" & RS05.Rows(a) !Kode_Importir & "', '" & RS05.Rows(a) !Importir & "', '" & RS05.Rows(a) !tglPO & "', " &
                        "'" & Format(TglKirim.Value, "yyyy-MM-dd") & "', '" & Trim(Pelabuhan.Text) & "', " &
                        "'" & cmbCaraKirim.Text & "', '" & RS05.Rows(a) !Kode_Produk & "', " &
                        "'" & Trim(Replace(RS05.Rows(a) !Deskripsi, "'", "`")) & "', '" & RS05.Rows(a) !Kode_Buyer & "', " &
                        "" & RS05.Rows(a) !Jumlah & ", '" & RS05.Rows(a) !MataUang & "', " & RS05.Rows(a) !FOBBuyer & ", " &
                        "'" & RS05.Rows(a) !digit3yn & "', '" & Format(TglKirim.Value, "yyyy-MM-dd") & "', " &
                        "'', '', '', '', '', 'Y', 'N', '" & UserID & "', GetDate(), 0, 0, 0, 0, 0, 0, 0, " &
                        "'" & Trim(CatatanPI.Text) & "', " & RS05.Rows(a) !Jumlah & ", " & RS05.Rows(a) !PembagiEuro & ", " &
                        "" & RS05.Rows(a) !FOBBuyer & ", 'PEKERTI' )"
                    Proses.ExecuteNonQuery(MsgSQL)
                Next a
                LAdd = False
                LEdit = False
                LTambahKode = False
                AturTombol(True)
            ElseIf LUangMuka Then
                Dim tNilaiPI As Double = 0
                MsgSQL = "Select Sum(t_PI.Jumlah * t_PI.HargaFOB) JValue " &
                    "From Pekerti.dbo.t_PI  " &
                    "Where t_PI.NoPI = '" & NoPI.Text & "' " &
                    "  And T_PI.AktifYn = 'Y' "
                tNilaiPI = Proses.ExecuteSingleDblQuery(MsgSQL)
                NilaiPI.Text = Format(tNilaiPI, "###,##0")
                MsgSQL = "Update T_PI Set " &
                    " NilaiPI = " & NilaiPI.Text * 1 & ", " &
                    "UangMuka = " & UangMuka.Text * 1 & ", " &
                    "KurangBayar = " & KurangBayar.Text * 1 & " " &
                    "Where t_PI.NoPI = '" & NoPI.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Uang Muka berhasil disimpan !", vbInformation + vbOKOnly, ".:Just Info !")
                GBoxUangMuka.Visible = False
            ElseIf LEdit Then
                MsgSQL = "Update t_PI Set " &
                    "       NoPO = '" & Nopo.Text & "', matauang = '" & Trim(cmbMataUang.Text) & "', " &
                    " ShipmentDate = '" & Format(TglKirim.Value, "YYYY-MM-DD") & "', " &
                    "      TglPI = '" & Format(tglPI.Value, "YYYY-MM-DD") & "', " &
                    "  Pelabuhan = '" & Trim(Pelabuhan.Text) & "', " &
                    "  CaraKirim = '" & cmbCaraKirim.Text & "', " &
                    "  CatatanPI = '" & Trim(CatatanPI.Text) & "', " &
                    " TransferYN = 'N',  UserID = '" & UserID & "', " &
                    "   LastUPD = GetDate() " &
                    "Where NoPI = '" & NoPI.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                If chk3Digit.Checked = True Then
                    tigadigit = "Y"
                Else
                    tigadigit = "N"
                End If
                'update by mbak UUk, untuk repot analisa tgl shipment date diambil dari
                'shipment date per kode
                MsgSQL = "Update t_PI Set " &
                    " ShipmentDateKode = '" & Format(ShipmentDate.Value, "YYYY-MM-DD") & "', " &
                    "Kode_Produk = '" & Trim(KodeProduk.Text) & "', " &
                    "     Produk = '" & Trim(Replace(Produk.Text, "'", "`")) & "', " &
                    "Kode_PImport = '" & KodePImportir.Text & "', " &
                    "     Jumlah = " & Jumlah.Text * 1 & ", HargaFOB = " & HargaFOB.Text * 1 & ", " &
                    "     QTYOSO = " & QTYOSO.Text * 1 & ", FOBOSO = " & FOBOSO.Text * 1 & ", " &
                    " TransferYN = 'N', Digit3YN = '" & tigadigit & "', " &
                    "     UserID = '" & UserID & "', " &
                    "    LastUPD = GetDate() " &
                    "Where IDRec = '" & idRecord.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            LAdd = False
            LEdit = False
            LTambahKode = False
            AturTombol(True)
        ElseIf LTambahKode Then
            If Trim(KodeProduk.Text) = "" Then
                MsgBox("Kode Produk tidak boleh kosong!", vbCritical, ".:Empty Field!")
                KodeProduk.Focus()
                Exit Sub
            End If
            If Trim(Jumlah.Text) = "" Then
                MsgBox("Jumlah tidak boleh Kosong!", vbCritical, ".:Empty Field!")
                Jumlah.Focus()
                Exit Sub
            End If
            MsgSQL = "Select Kode_produk, Produk From T_PI " &
                "Where aktifYN = 'Y' " &
                "  And Kode_Produk = '" & KodeProduk.Text & "' " &
                "  And noPo = '" & Nopo.Text & "' "
            RSL = Proses.ExecuteQuery(MsgSQL)
            If RSL.Rows.Count <> 0 Then
                MsgBox(KodeProduk.Text & " sudah pernah ada di PI ini", vbCritical, "Double Cek!")
                KodeProduk.Focus()
                Exit Sub
            End If
            TambahPO_diPI()
            TambahKode()
        End If
    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Private Sub TambahPO_diPI()
        Dim MsgSQL As String
        Dim RS05 As New DataTable
        MsgSQL = "Select t_PO.IDRec, m_KodeProduk.deskripsi, t_PO.Kode_Produk, " &
            " m_KodeImportir.Nama AS Importir, t_PO.Kode_Importir, t_PO.Kode_Perajin, " &
            " t_PO.Kode_Buyer, t_PO.Jumlah, t_PO.TglKirim, t_PO.MataUang, t_PO.FOBBuyer, " &
            " t_PO.FOBUmum, t_PO.Digit3YN, t_PO.CatatanPO, t_PO.CatatanProduk, t_PO.FotoLoc, " &
            " t_PO.StatusPO, t_PO.TglPO, t_PO.NoPO, PembagiEuro " &
            " From t_PO INNER JOIN m_KodeProduk ON " &
            "   t_PO.Kode_Produk = m_KodeProduk.KodeProduk " &
            "   INNER JOIN m_KodeImportir ON " &
            "   m_KodeImportir.KodeImportir = t_PO.Kode_Importir " &
            "Where t_PO.AktifYN = 'Y' " &
            "  And m_KodeImportir.AktifYN = 'Y' " &
            "  And m_KodeProduk.AktifYN = 'Y' " &
            "  And t_PO.Kode_Produk = '" & KodeProduk.Text & "' " &
            "  And NOPO = '" & Nopo.Text & "' " &
            "Order By IDRec "

        RS05 = Proses.ExecuteQuery(MsgSQL)
        If RS05.Rows.Count <> 0 Then
            Application.DoEvents()
            idRecord.Text = Proses.MaxNoUrut("IDRec", "t_PI", "PI")

            MsgSQL = "INSERT INTO t_PI(IDRec, NoPI, NoPO, TglPI, StatusPI, Kode_Importir, Importir,  " &
                "TglPO, ShipmentDate, Pelabuhan, CaraKirim, Kode_Produk, Produk, Kode_PImport, " &
                "Jumlah, MataUang, HargaFOB, Digit3YN, ShipmentDateKode, PermintaanPO1, PermintaanPO2, " &
                "PermintaanPO3, Pemenuhan1, Pemenuhan2, AktifYN, TransferYN, UserID, LastUPD, " &
                "NilaiPI, UangMuka, KurangBayar, LabelingCost, SpecialPackaging, Fumigation, " &
                "Phytosanitary, CatatanPI, QTYOSO, Konversi, FOBOSO, IdCompany) VALUES( '" & idRecord.Text & "',   " &
                "'" & NoPI.Text & "', '" & Nopo.Text & "', '" & Format(tglPI.Value, "yyyy-MM-dd") & "', '', " &
                " '" & RS05.Rows(0) !Kode_Importir & "', '" & RS05.Rows(0) !Importir & "', '" & RS05.Rows(0) !tglPO & "', " &
                "'" & Format(TglKirim.Value, "yyyy-MM-dd") & "', '" & Trim(Pelabuhan.Text) & "', " &
                "'" & cmbCaraKirim.Text & "', '" & RS05.Rows(0) !Kode_Produk & "', " &
                "'" & Trim(Replace(RS05.Rows(0) !Deskripsi, "'", "`")) & "', '" & RS05.Rows(0) !Kode_Buyer & "', " &
                "" & RS05.Rows(0) !Jumlah & ", '" & RS05.Rows(0) !MataUang & "', " & RS05.Rows(0) !FOBBuyer & ", " &
                "'" & RS05.Rows(0) !digit3yn & "', '" & Format(TglKirim.Value, "yyyy-MM-dd") & "', '', '', '', '', '',  " &
                "'Y', 'N', '" & UserID & "', GetDate(), 0, 0, 0, 0, 0, 0, 0, '" & Trim(CatatanPI.Text) & "', " &
                "" & RS05.Rows(0) !Jumlah & ", " & RS05.Rows(0) !PembagiEuro & ", " & RS05.Rows(0) !FOBBuyer & ", 'PEKERTI')"
            Proses.ExecuteNonQuery(MsgSQL)

        End If
        Proses.CloseConn()
    End Sub

    Private Sub tNoPI_TextChanged(sender As Object, e As EventArgs) Handles tNoPI.TextChanged

    End Sub

    Private Sub Nopo_LostFocus(sender As Object, e As EventArgs) Handles Nopo.LostFocus
        'Dim MsgSQL As String, RSL As New DataTable
        'If LAdd Then
        '    On Error Resume Next
        '    If Trim(Nopo.Text) = "" Then Exit Sub

        'End If
    End Sub

    Private Sub cmdUangMuka_Click(sender As Object, e As EventArgs) Handles cmdUangMuka.Click
        If Trim(NoPI.Text) = "" Then
            MsgBox("No PI yang di buat uang muka belum dipilih !", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        GBoxUangMuka.Visible = True
        LAdd = False
        LEdit = False
        LUangMuka = True
        AturTombol(False)
        Dim tNilaiPI As Double = 0
        MsgSQL = "Select Sum(t_PI.Jumlah * t_PI.HargaFOB) JValue " &
            "From Pekerti.dbo.t_PI  " &
            "Where t_PI.NoPI = '" & NoPI.Text & "' " &
            "  And T_PI.AktifYn = 'Y' "
        tNilaiPI = Proses.ExecuteSingleDblQuery(MsgSQL)
        NilaiPI.Text = Format(tNilaiPI, "###,##0")
        UangMuka.Focus()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        btnExcel.Enabled = False
        Dim excel As New Excel.Application
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
        oSheet.Cells(1, 1) = "PI No."
        oSheet.Cells(1, 2) = "'" & NoPI.Text
        oSheet.Cells(2, 1) = "PO No."
        oSheet.Cells(2, 2) = "'" & Nopo.Text
        oSheet.Cells(3, 1) = "PO Date"
        oSheet.Cells(3, 2) = " " + Format(tglPO.Value, "dd-MM-yyyy")
        oSheet.Cells(4, 1) = "Importir"
        oSheet.Cells(4, 2) = "'" + Importir.Text
        oSheet.Cells(5, 1) = "Shipment Date"
        oSheet.Cells(5, 2) = " " + Format(ShipmentDate.Value, "dd-MM-yyyy")
        oSheet.Cells(7, 1) = "No."
        oSheet.Cells(7, 2) = "Code"
        oSheet.Cells(7, 3) = "Description"
        oSheet.Cells(7, 4) = "Importir Code"
        oSheet.Cells(7, 5) = "QTY"
        oSheet.Cells(7, 6) = "FOB Price"
        oSheet.Cells(7, 7) = "Total Value"

        Dim i As Integer = 8

        MsgSQL = "Select t_PI.*, Descript " &
            " From t_PI inner join m_KodeProduk on Kode_Produk = KodeProduk " &
            "Where t_PI.AktifYN = 'Y' " &
            "  And NoPI = '" & NoPI.Text & "' " &
            "order by idrec "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            oSheet.Range("A" + Format(i, "##0")).Value = NoUrut
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Produk
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !Descript
            oSheet.Range("D" + Format(i, "##0")).Value = "'" + dbTable.Rows(a) !Kode_Pimport
            oSheet.Range("E" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Jumlah, "###,##0")
            oSheet.Range("F" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !HargaFOB, "###,##0.000")
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Jumlah * dbTable.Rows(a) !HargaFOB, "###,##0.000")
            i += 1
            NoUrut = NoUrut + 1
        Next (a)


        oSheet.Columns.AutoFit()
        'oSheet.range("E11:E11").HorizontalAlignment = xlCenter
        'oSheet.range("F11:G11").HorizontalAlignment = xlCenter
        'oSheet.Range("A13").ColumnWidth = 5
        'oSheet.Range("G12").ColumnWidth = 10

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
        btnExcel.Enabled = True
        Proses.CloseConn()
    End Sub
    Private Sub Export2WordExample()
        Dim oWord As New Word.Application
        Dim oDoc As New Word.Document
        Dim oTable As Word.Table
        Dim oPara1 As Word.Paragraph, oPara2 As Word.Paragraph
        Dim oPara3 As Word.Paragraph, oPara4 As Word.Paragraph
        Dim oRng As Word.Range
        Dim oShape As Word.InlineShape
        Dim oChart As Object
        Dim Pos As Double

        'Start Word and open the document template.
        oWord = CreateObject("Word.Application")
        oWord.Visible = True
        oDoc = oWord.Documents.Add

        'Insert a paragraph at the beginning of the document.
        oPara1 = oDoc.Content.Paragraphs.Add
        oPara1.Range.Text = "Heading 1"
        oPara1.Range.Font.Bold = True
        oPara1.Format.SpaceAfter = 24    '24 pt spacing after paragraph.
        oPara1.Range.InsertParagraphAfter()

        'Insert a paragraph at the end of the document.
        '** \endofdoc is a predefined bookmark.
        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara2.Range.Text = "Heading 2"
        oPara2.Format.SpaceAfter = 6
        oPara2.Range.InsertParagraphAfter()

        'Insert another paragraph.
        oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:"
        oPara3.Range.Font.Bold = False
        oPara3.Format.SpaceAfter = 24
        oPara3.Range.InsertParagraphAfter()

        'Insert a 3 x 5 table, fill it with data, and make the first row
        'bold and italic.
        Dim r As Integer, c As Integer
        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 3, 5)
        oTable.Range.ParagraphFormat.SpaceAfter = 6
        For r = 1 To 3
            For c = 1 To 5
                oTable.Cell(r, c).Range.Text = "r" & r & "c" & c
            Next
        Next
        oTable.Rows.Item(1).Range.Font.Bold = True
        oTable.Rows.Item(1).Range.Font.Italic = True

        'Add some text after the table.
        'oTable.Range.InsertParagraphAfter()
        oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara4.Range.InsertParagraphBefore()
        oPara4.Range.Text = "And here's another table:"
        oPara4.Format.SpaceAfter = 24
        oPara4.Range.InsertParagraphAfter()

        'Insert a 5 x 2 table, fill it with data, and change the column widths.
        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 5, 2)
        oTable.Range.ParagraphFormat.SpaceAfter = 6
        For r = 1 To 5
            For c = 1 To 2
                oTable.Cell(r, c).Range.Text = "r" & r & "c" & c
            Next
        Next
        oTable.Columns.Item(1).Width = oWord.InchesToPoints(2)   'Change width of columns 1 & 2
        oTable.Columns.Item(2).Width = oWord.InchesToPoints(3)

        'Keep inserting text. When you get to 7 inches from top of the
        'document, insert a hard page break.
        Pos = oWord.InchesToPoints(7)
        oDoc.Bookmarks.Item("\endofdoc").Range.InsertParagraphAfter()
        Do
            oRng = oDoc.Bookmarks.Item("\endofdoc").Range
            oRng.ParagraphFormat.SpaceAfter = 6
            oRng.InsertAfter("A line of text")
            oRng.InsertParagraphAfter()
        Loop While Pos >= oRng.Information(Word.WdInformation.wdVerticalPositionRelativeToPage)
        oRng.Collapse(Word.WdCollapseDirection.wdCollapseEnd)
        oRng.InsertBreak(Word.WdBreakType.wdPageBreak)
        oRng.Collapse(Word.WdCollapseDirection.wdCollapseEnd)
        oRng.InsertAfter("We're now on page 2. Here's my chart:")
        oRng.InsertParagraphAfter()

        'Insert a chart and change the chart.
        oShape = oDoc.Bookmarks.Item("\endofdoc").Range.InlineShapes.AddOLEObject(
            ClassType:="MSGraph.Chart.8", FileName _
            :="", LinkToFile:=False, DisplayAsIcon:=False)
        oChart = oShape.OLEFormat.Object
        oChart.charttype = 4 'xlLine = 4
        oChart.Application.Update()
        oChart.Application.Quit()
        'If desired, you can proceed from here using the Microsoft Graph 
        'Object model on the oChart object to make additional changes to the
        'chart.
        oShape.Width = oWord.InchesToPoints(6.25)
        oShape.Height = oWord.InchesToPoints(3.57)

        'Add text after the chart.
        oRng = oDoc.Bookmarks.Item("\endofdoc").Range
        oRng.InsertParagraphAfter()
        oRng.InsertAfter("THE END.")
    End Sub

    'Private Function GetVariableValue(Variable As String) As String
    '    Dim i As Integer
    '    For i = 0 To UBound(UsedVariables)
    '        If Microsoft.VisualBasic.Left(UsedVariables(i), Len(Variable)) = Variable Then
    '            GetVariableValue = Microsoft.VisualBasic.Right(UsedVariables(i), Len(UsedVariables(i)) - Len(Variable))
    '            Exit For
    '        End If
    '    Next
    'End Function

    'Private Sub AddNewVariable(Variable As String, TheValue As String)
    '    Dim ArraySize As Integer
    '    ArraySize = UBound(UsedVariables)
    '    ReDim Preserve UsedVariables(ArraySize + 1)
    '    UsedVariables(ArraySize) = Variable & TheValue
    'End Sub

    'Private Function CheckUsedVariable(Variable As String) As Boolean
    '    Dim i As Integer
    '    For i = 0 To UBound(UsedVariables)
    '        If Microsoft.VisualBasic.Left(UsedVariables(i), Len(Variable)) = Variable Then
    '            CheckUsedVariable = True
    '            Exit For
    '        End If
    '    Next
    'End Function
    'Private Function GetNewResult(wField As Word.Field, WordDoc As Word.Document) As String
    '    Dim RSW As New DataTable, i As Integer, MataUang As String = "", Total As Double = 0
    '    Dim StopPos As Long
    '    Dim Variable As String
    '    Dim UsedVariable As String = ""
    '    Dim VariableValue As String = ""
    '    Dim wRange As Word.Range

    '    Debug.Print(wField.Code.ToString)

    '    ' These three lines strip down the field code to find
    '    ' out it's name
    '    StopPos = InStrRev(wField.Code.ToString, "\*")
    '    If StopPos > 0 Then
    '        Variable = Microsoft.VisualBasic.Left(wField.Code.ToString, StopPos - 3)
    '        Variable = Microsoft.VisualBasic.Right(Variable, Len(Variable) - 14)
    '    Else
    '        Variable = ""
    '    End If
    '    ' Check this field hasn't already appeared in this
    '    ' document.
    '    If CheckUsedVariable(Variable) Then
    '        VariableValue = GetVariableValue(Variable)

    '    Else

    '        Select Case UCase(Variable)

    '            ' I don't simply want to insert a string -
    '            ' I wish to insert a table at the Product Field.
    '            Case "PRODUCT"

    '                ' Get the range (location) of the product field
    '                wRange = wField.Code
    '                ' Delete the field, as any text will be inserted into the
    '                ' {} of the existing field.
    '                wField.Delete()

    '                ' Enter our table information including headers.
    '                ' Ideally, I would get this data from an ADO recordset
    '                ' using GetString().
    '                With wRange

    '                    .Text = "No." & vbTab & "Our Code" & vbTab & "Your Code" & vbTab & "Our Description" & vbTab & "QTY" & vbTab & "Unit Price" & vbTab & "Total" & vbCrLf
    '                    MsgSQL = "SELECT t_PI.IDRec, t_PI.NoPI, t_PI.NoPO, t_PI.ShipmentDate, " &
    '                    "t_PI.Pelabuhan, t_PI.Sea, t_PI.Air, t_PI.Kode_Produk, t_PI.Produk, " &
    '                    "t_PI.Kode_PImport, t_PI.Jumlah, t_PI.MataUang, t_PI.HargaFOB, " &
    '                    "m_KodeImportir.Nama, m_KodeImportir.Alamat, m_KodeProduk.Satuan " &
    '                    "FROM  Pekerti.dbo.t_PI t_PI INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON " &
    '                    "    t_PI.Kode_Produk = m_KodeProduk.KodeProduk " &
    '                    " INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
    '                    "    t_PI.Kode_Importir = m_KodeImportir.KodeImportir " &
    '                    "Where t_PI.NoPI = '" & NoPI.Text & "' " &
    '                    "  And T_PI.AktifYn = 'Y' "
    '                    RSW = Proses.ExecuteQuery(MsgSQL)
    '                    i = 1
    '                    For a = 0 To RSW.Rows.Count - 1
    '                        .Text = .Text + Microsoft.VisualBasic.Right(Space(4) & Format(i, "###"), 4) & vbTab &
    '                            RSW.Rows(a) !Kode_Produk & vbTab & RSW.Rows(a) !Kode_PImport & vbTab & RSW.Rows(a) !Produk & vbTab &
    '                            Microsoft.VisualBasic.Right(Space(10) & Format(RSW.Rows(a) !Jumlah, "###,##0"), 10) & " " & RSW.Rows(a) !Satuan & vbTab &
    '                            RSW.Rows(a) !MataUang & Microsoft.VisualBasic.Right(Space(15) & Format(RSW.Rows(a) !HargaFOB, "###,##0"), 15) & vbTab &
    '                            RSW.Rows(a) !MataUang & Microsoft.VisualBasic.Right(Space(15) & Format(RSW.Rows(a) !HargaFOB * RSW.Rows(a) !Jumlah, "###,##0.00"), 20) & vbCrLf
    '                        Total = Total + (RSW.Rows(a) !Jumlah * RSW.Rows(a) !HargaFOB)
    '                        MataUang = RSW.Rows(a) !MataUang
    '                        i = i + 1
    '                    Next a
    '                    .FormattedText.Font.Name = "Arial"
    '                    .FormattedText.Font.Size = "8"
    '                    .Text = .Text + vbCrLf + vbTab + vbTab + vbTab + vbTab + vbTab + "Total : " +
    '                    vbTab + MataUang + Microsoft.VisualBasic.Right(Space(15) & Format(Total, "###,##0.00"), 20)

    '                    ' Once the data is there, we can convert it to a table
    '                    ' structure and format it to look pretty!
    '                    .ConvertToTable(vbTab, , , , Word.WdTableFormat.wdTableFormatColorful2)
    '                End With

    '                ' Send back blank string as field does not exist anymore
    '                VariableValue = ""

    '            Case Else

    '                ' Get the value of the field from the user
    '                'VariableValue = InputBox("Enter value for: " & Variable, "Value not recognised for Despatch Note!")
    '                If Variable = "NoPI" Then
    '                    VariableValue = "No : " & NoPI.Text
    '                ElseIf Variable = "DeliveryDate" Then
    '                    VariableValue = Format(TglKirim.Value, "dd-MM-yyyy")
    '                ElseIf Variable = "DeliveryMethod" Then
    '                    '                    If OptSea.Value = True And OptAir.Value = False Then
    '                    '                        VariableValue = "SEA FREIGHT"
    '                    '                    ElseIf OptSea.Value = False And OptAir.Value = True Then
    '                    '                        VariableValue = "AIR FREIGHT"
    '                    '                    End If
    '                    VariableValue = cmbCaraKirim.Text
    '                ElseIf Variable = "PortOFDepature" Then
    '                    VariableValue = Pelabuhan.Text
    '                ElseIf Variable = "MataUang" Then
    '                    VariableValue = cmbMataUang.Text
    '                ElseIf Variable = "NoPO" Then
    '                    VariableValue = Nopo.Text
    '                ElseIf Variable = "Nama_PO" Then
    '                    VariableValue = Importir.Text
    '                ElseIf Variable = "Alamat" Then
    '                    VariableValue = Alamat.Text
    '                End If
    '                AddNewVariable(Variable, VariableValue)
    '        End Select

    '    End If

    '    GetNewResult = VariableValue

    'End Function


    '    Private Sub btnWord_XXX(sender As Object, e As EventArgs)
    '        btnWord.Enabled = False
    '        Dim oWord As New Word.Application
    '        Dim oDoc As New Word.Document
    '        Dim oTable As Word.Table
    '        Dim oPara1 As Word.Paragraph, oPara2 As Word.Paragraph
    '        Dim oPara3 As Word.Paragraph, oPara4 As Word.Paragraph
    '        Dim oRng As Word.Range
    '        Dim oShape As Word.InlineShape
    '        Dim oChart As Object
    '        Dim Pos As Double
    '        Dim NewResult As String

    '        ''Start Word and open the document template.
    '        ''oWord = CreateObject("Word.Application")
    '        ''oWord.Visible = True
    '        ''oDoc = oWord.Documents.Add

    '        Dim i As Integer, j As Integer
    '        Dim RSW As New DataTable, RS05 As New DataTable
    '        If Trim(NoPI.Text) = "" Then
    '            MsgBox("No PI yang akan di convert ke word belum di pilih!", vbCritical, ".:ERROR!")
    '            Exit Sub
    '        End If
    '        On Error GoTo ErrHandler

    '        MsgSQL = "SELECT t_PI.IDRec, t_PI.NoPI, t_PI.NoPO, t_PI.ShipmentDate, " &
    '           "t_PI.Pelabuhan, t_PI.Sea, t_PI.Air, t_PI.Kode_Produk, t_PI.Produk, " &
    '           "t_PI.Kode_PImport, t_PI.Jumlah, t_PI.MataUang, t_PI.HargaFOB, " &
    '           "m_KodeImportir.Nama, m_KodeImportir.Alamat, m_KodeProduk.Satuan, CaraKirim " &
    '           "FROM  Pekerti.dbo.t_PI t_PI INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON " &
    '           "    t_PI.Kode_Produk = m_KodeProduk.KodeProduk " &
    '           " INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
    '           "    t_PI.Kode_Importir = m_KodeImportir.KodeImportir " &
    '           "Where t_PI.NoPI = '" & NoPI.Text & "' " &
    '           "  And T_PI.AktifYn = 'Y' "
    '        RS05 = Proses.ExecuteQuery(MsgSQL)
    '        If RS05.Rows.Count <> 0 Then
    '            Alamat.Text = RS05.Rows(0) !Alamat
    '            TglKirim.Value = RS05.Rows(0) !ShipmentDate
    '            If RS05.Rows(0) !CaraKirim = "SEA FREIGHT" Then
    '                OptSea.Checked = True
    '                OptAir.Checked = False
    '            Else
    '                OptSea.Checked = False
    '                OptAir.Checked = True
    '            End If
    '            Pelabuhan.Text = RS05.Rows(0) !Pelabuhan
    '        End If
    '        ReDim UsedVariables(0)
    '        Dim WordApp As New Word.Application
    '        Dim WordDoc As New Word.Document
    '        Dim resourcePath As String = My.Application.Info.DirectoryPath.ToString + "\TemplatePI.doc"
    '        WordApp = CreateObject("Word.Application")
    '        WordDoc = WordApp.Documents.Open(resourcePath)


    '        '    ' For each section (header and footer)
    '        For i = 1 To WordDoc.Sections.Count
    '            '        ' Headers
    '            '  Debug.Print "Fields In Header" & WordDoc.Sections(i).Headers(wdHeaderFooterPrimary).Range.Fields.Count
    '            For j = 1 To WordDoc.Sections(i).Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Fields.Count

    '                If WordDoc.Sections(i).Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Fields(j).Type = Word.WdFieldType.wdFieldDocVariable Then
    '                    ' Get the text for the field from the user
    '                    NewResult = GetNewResult(WordDoc.Sections(i).Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Fields(j), WordDoc)
    '                    'Insert New Text into the field
    '                    If NewResult <> "" Then
    '                        WordDoc.Sections(i).Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Fields(j).Result.Text = NewResult
    '                    End If

    '                End If

    '            Next
    '        Next

    '        '        ' In main body
    '        '        Debug.Print "Fields in main body " & WordDoc.Fields.Count
    '        '    For i = 1 To WordDoc.Fields.Count

    '        '            If WordDoc.Fields(i).Type = wdFieldDocVariable Then

    '        '                ' Get the text for the field from the user
    '        '                NewResult = GetNewResult(WordDoc.Fields(i), WordDoc)
    '        '                'Insert New Text into the field
    '        '                If NewResult <> "" Then
    '        '                    WordDoc.Fields(i).result.Text = NewResult + "TOTAL...."
    '        '                End If

    '        '            End If

    '        '        Next
    '        '        ' lock the document to stop changes
    '        '        '    WordDoc.Protect wdAllowOnlyComments, , "jd837djh82"

    '        '        WordDoc.SaveAs App.Path & "\PI" & Format(Of Date, "YYMMDDss")() & ".doc"

    '        '    WordDoc.Close

    '        '        wordApp.Quit
    '        '    Set WordDoc = Nothing
    '        '    Set wordApp = Nothing

    '        '    MsgBox("Nama File Hasil export to word  " & vbCrLf & "  " + App.Path & "\PI" & Format(Of Date, "YYMMDDss")() & ".doc", vbOKOnly + vbInformation, "Finished!")

    '        '        Exit Sub
    '        MsgBox("File Berhasil di Copy ke Word", vbInformation + vbOKOnly, ".Information....")
    '        btnWord.Enabled = False
    'ErrHandler:
    '        MsgBox("Unhanled Error " & Err.Description, vbCritical + vbOKOnly, ".:Error ...")
    '    End Sub

    Private Sub btnWord_Click(sender As Object, e As EventArgs) Handles btnWord.Click
        If Trim(NoPI.Text) = "" Then
            MsgBox("No PI yang akan di convert ke word belum di pilih!", vbCritical, ".:ERROR!")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        btnWord.Enabled = False
        Proses.OpenConn(CN)
        dttable = New DataTable
        MsgSQL = "SELECT t_PI.IDRec, t_PI.NoPI, t_PI.NoPO, t_PI.ShipmentDate,  t_PI.CaraKirim,
            t_PI.Pelabuhan, t_PI.Sea, t_PI.Air, t_PI.Kode_Produk, t_PI.Produk,  
            t_PI.Kode_PImport, t_PI.Jumlah, t_PI.MataUang, t_PI.HargaFOB,  
            m_KodeImportir.Nama, m_KodeImportir.Alamat, m_KodeProduk.Satuan  
            FROM  Pekerti.dbo.t_PI t_PI INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON  
                t_PI.Kode_Produk = m_KodeProduk.KodeProduk  
             INNER JOIN Pekerti.dbo.m_Company m_Company ON   
                t_PI.IDCompany = m_Company.CompCode  
             INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON  
                t_PI.Kode_Importir = m_KodeImportir.KodeImportir  
            Where t_PI.NoPI = '" & NoPI.Text & "'  
              And T_PI.AktifYn = 'Y'   
            Order By T_PI.IDRec "
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_PI
            objRep.SetDataSource(dttable)
            'source: http://vb.net-informations.com/crystal-report/vb.net_crystal_report_export_pdf.htm
            'to xls: http://vb.net-informations.com/crystal-report/vb.net_crystal_report_export_excel.htm
            Dim DefaultFolder As String = "C:\me"
            'DefaultFolder = Proses.ExecuteSingleStrQuery(SQL)
            Dim FileName As String = DefaultFolder & "\PI_" & Replace(NoPI.Text, "/", "-") & "_" & Format(Now, "ddMMyyHHmm") & ".doc"
            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            CrDiskFileDestinationOptions.DiskFileName = FileName
            CrExportOptions = objRep.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.WordForWindows
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            objRep.Export()
            MsgBox("File Berhasil di simpan di " & FileName, vbInformation + vbOKOnly, ".:Success !")
            'https://www.aspsnippets.com/Articles/Export-Crystal-Report-on-Button-Click-to-Word-Excel-PDF-and-CSV-in-ASPNet.aspx
            dttable.Dispose()
            DTadapter.Dispose()
            Proses.CloseConn(CN)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
        Me.Cursor = Cursors.Default
        btnWord.Enabled = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub cmdTambahanBiayaPI_Click(sender As Object, e As EventArgs) Handles cmdTambahanBiayaPI.Click
        Form_PIBiayaTambahan.ShowDialog()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable, clsTerbilang As New Terbilang
        Dim terbilang As String = "", DeliveryDate As String = "", totalPI As Double = 0, MataUang As String = ""

        MsgSQL = "Select isNull(Sum(t_PI.Jumlah * t_PI.HargaFOB),0) JValue, max(MataUang) MataUang " &
            "From Pekerti.dbo.t_PI  " &
            "Where t_PI.NoPI = '" & NoPI.Text & "' " &
            "  And T_PI.AktifYn = 'Y' "
        dttable = Proses.ExecuteQuery(MsgSQL)
        If dttable.Rows.Count <> 0 Then
            totalPI = dttable.Rows(0) !Jvalue
            MataUang = dttable.Rows(0) !matauang
        Else
            totalPI = 0
            MataUang = ""
        End If
        ' terbilang = "- " + Proses.ConvertCurrencyToEnglish(totalPI) + " " + MataUang + " -"
        terbilang = "- " + clsTerbilang.CurrencyText(totalPI, MataUang) + " -"

        DeliveryDate = Format(TglKirim.Value, "dd MMMM yyyy") + " (Leave Indonesia)"
        Me.Cursor = Cursors.WaitCursor
        Proses.OpenConn(CN)
        dttable = New DataTable

        MsgSQL = "Select t_PI.IDRec, t_PI.NoPI, t_PI.NoPO, t_PI.ShipmentDate,  t_PI.CaraKirim,
                t_PI.Pelabuhan, t_PI.Sea, t_PI.Air, t_PI.Kode_Produk, m_KodeProduk.descript Produk,  
                t_PI.Kode_PImport, t_PI.Jumlah, t_PI.MataUang, t_PI.HargaFOB,  
                m_KodeImportir.Nama, m_KodeImportir.Alamat, m_KodeProduk.Satuan,
                convert(varchar(20), tglpi, 106) TglPI, CatatanPI, m_Company.direksi, m_Company.TTDireksi
            FROM Pekerti.dbo.t_PI t_PI INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON  
                t_PI.Kode_Produk = m_KodeProduk.KodeProduk  
             INNER JOIN Pekerti.dbo.m_Company m_Company ON   
                t_PI.IDCompany = m_Company.CompCode  
             INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON  
                t_PI.Kode_Importir = m_KodeImportir.KodeImportir  
            Where t_PI.NoPI = '" & NoPI.Text & "'  
              And T_PI.AktifYn = 'Y'   
            Order By T_PI.IDRec "
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_PI
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Terbilang", terbilang)
            Form_Report.CrystalReportViewer1.ShowExportButton = True
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
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Nopo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Nopo.KeyPress
        Dim RSN1 As New DataTable, RSN2 As New DataTable
        If e.KeyChar = Chr(13) Then
            MsgSQL = "Select NoPO, Tglpo, TglKirim, Nama, Kode_Importir " &
                "From T_PO inner join m_KodeImportir ON KodeImportir = Kode_Importir " &
                "Where NOPO = '" & Nopo.Text & "' " &
                "  And T_PO.AktifYN = 'Y' "
            RSN1 = Proses.ExecuteQuery(MsgSQL)
            If RSN1.Rows.Count <> 0 Then
                ShipmentDate.Value = RSN1.Rows(0) !tglKirim
                TglKirim.Value = RSN1.Rows(0) !tglKirim
                Kode_Importir.Text = RSN1.Rows(0) !Kode_Importir
                Importir.Text = RSN1.Rows(0) !Nama
                tglPO.Value = RSN1.Rows(0) !tglPO
            Else
                Me.Cursor = Cursors.WaitCursor
                SQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO, max(TglKirim) TglKirim, KodeImportir " &
                    " From T_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where T_PO.AktifYN = 'Y' " &
                    "  and nopo like '%" & Nopo.Text & "%' " &
                    "Group By NoPO, m_KodeImportir.Nama, TglPO, KodeImportir " &
                    "Order By TglPO Desc, NoPO Desc "
                Form_Daftar.txtQuery.Text = SQL
                Form_Daftar.Text = "Daftar PO"
                Form_Daftar.ShowDialog()
                Nopo.Text = FrmMenuUtama.TSKeterangan.Text
                MsgSQL = "Select NoPO, TglPO, TglKirim, Nama, Kode_Importir " &
                    "From T_PO inner join m_KodeImportir ON KodeImportir = Kode_Importir " &
                    "Where NOPO = '" & Nopo.Text & "' " &
                    "  And T_PO.AktifYN = 'Y' "
                RSN2 = Proses.ExecuteQuery(MsgSQL)
                If RSN2.Rows.Count <> 0 Then
                    ShipmentDate.Value = RSN2.Rows(0) !tglKirim
                    TglKirim.Value = RSN2.Rows(0) !tglKirim
                    Kode_Importir.Text = RSN2.Rows(0) !Kode_Importir
                    Importir.Text = RSN2.Rows(0) !Nama
                    tglPO.Value = RSN2.Rows(0) !tglPO
                    Pelabuhan.Focus()
                Else
                    MsgBox("NO PO tidak boleh kosong!", vbCritical, ".:ERROR!")
                    Nopo.Focus()
                    Exit Sub
                End If
                Me.Cursor = Cursors.Default
            End If

            If LAdd Then
                MsgSQL = "Select Kode_produk From T_PI " &
                    "Where aktifYN = 'Y' " &
                    "  And noPo = '" & Nopo.Text & "' "
                RSN1 = Proses.ExecuteQuery(MsgSQL)
                If RSN1.Rows.Count <> 0 Then
                    MsgBox("Maaf, NO PO sudah pernah di buatkan PI", vbCritical, "Double Cek!")
                    Nopo.Focus()
                Else
                    Pelabuhan.Focus()
                End If
            ElseIf LEdit Then
                Pelabuhan.Focus()
            End If
            Proses.CloseConn()
        End If
    End Sub

    Private Sub CatatanPI_TextChanged(sender As Object, e As EventArgs) Handles CatatanPI.TextChanged

    End Sub

    Private Function CekDouble() As Boolean
        Dim MsgSQL As String, RSL As New DataTable, Hasil As Boolean = False
        If LTambahKode Then
            MsgSQL = "Select Kode_produk, Produk From T_PI " &
                "Where aktifYN = 'Y' " &
                "  And Kode_Produk = '" & KodeProduk.Text & "' " &
                "  And noPo = '" & Nopo.Text & "' "
            RSL = Proses.ExecuteQuery(MsgSQL)
            If RSL.Rows.Count <> 0 Then
                MsgBox(KodeProduk.Text & " sudah pernah ada di PI ini", vbCritical, "Double Cek!")
                Hasil = True
            Else
                Hasil = False
            End If
            Proses.CloseConn()
        End If
        CekDouble = Hasil
    End Function

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PI " &
            "Where AktifYN = 'Y' " &
            "  And NoPI = '" & Trim(NoPI.Text) & "' " &
            "  And IDRec > '" & idRecord.Text & "' " &
            "ORDER BY right(nopi,2) + Left(NoPI,3), IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPI(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PI " &
            "Where AktifYN = 'Y' " &
            "  And NoPI = '" & Trim(NoPI.Text) & "' " &
            "  And IDRec < '" & idRecord.Text & "' " &
            "ORDER BY  IDRec desc"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPI(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PI " &
            "Where AktifYN = 'Y' " &
            "  And NoPI = '" & Trim(NoPI.Text) & "' " &
            "ORDER BY tglPI, NoPI, IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPI(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From t_PI " &
            "Where AktifYN = 'Y' " &
            "  And NoPI = '" & Trim(NoPI.Text) & "' " &
            "ORDER BY tglPI, NoPI, IDRec"
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            Call IsiPI(RSNav.Rows(0) !IdRec)
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim xMacam As Integer, xQTY As Integer, xTotNilai As Double
        Dim MsgSQL As String, tCari As String, rsP As New DataTable
        Dim JMacam As Integer = 0, JTotal As Double = 0, JNilai As Double = 0
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Rows.Clear()
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "SELECT  IDrec, Kode_Produk, Produk, Kode_PImport, Jumlah, QTYOSO, MataUang, HargaFOB " &
            " FROM t_PI " &
            "Where T_PI.AktifYN = 'Y' " &
            "  And NoPI = '" & tCari & "' " &
            "Order By IDRec "
        xTotNilai = 0
        xQTY = 0
        xMacam = 0
        rsP = Proses.ExecuteQuery(MsgSQL)
        If rsP.Rows.Count <> 0 Then
            tCari = rsP.Rows(0) !idrec
        End If
        For a = 0 To rsP.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(rsP.Rows(a) !idrec,
                            rsP.Rows(a) !Kode_Produk,
                            rsP.Rows(a) !Kode_PImport,
                            Format(rsP.Rows(a) !Jumlah, "###,##0"),
                            Format(rsP.Rows(a) !QTYOSO, "###,##0"),
                            rsP.Rows(a) !MataUang,
                            Format(rsP.Rows(a) !HargaFOB, "###,##0.00"))
            xQTY = xQTY + rsP.Rows(a) !Jumlah
            xTotNilai = xTotNilai + (rsP.Rows(a) !Jumlah * rsP.Rows(a) !HargaFOB)
        Next a
        If DGView2.Rows.Count <> 0 Then
            IsiPI(tCari)
            JumMacam.Text = 0
            JQTY.Text = Format(xQTY, "###,##0")
            JumTotal.Text = Format(xTotNilai, "###,##0.00")
        End If
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Dim tCode As String
        If DGView2.Rows.Count = 0 Then Exit Sub
        tCode = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        If tCode = "" Then Exit Sub
        Call IsiPI(tCode)
    End Sub

    Private Sub tNoPI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tNoPI.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarPI(tNoPI.Text)
        End If
    End Sub


    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = 0 Then
        ElseIf e.TabPageIndex = 1 Then
            DaftarPI("")
        End If
    End Sub

    Private Sub CatatanPI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CatatanPI.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
    End Sub
End Class