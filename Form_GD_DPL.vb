Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Public Class Form_GD_DPL
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        lKoordinator As String, lPemeriksa As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Private CN As SqlConnection



    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        LTambahKode = False
        AturTombol(False)
        ClearTextBoxes()
        NoDPL.Text = Proses.MaxYNoUrut("NoDPL", "t_DPL", "DPL")
        OptSemuaData.Checked = True
        OptLaut.Checked = True
        NoBoks.Focus()
    End Sub

    Private Sub TambahDPL()
        If Trim(NoDPL.Text) = "" Then
            MsgBox("No DPL masih kosong!", vbCritical + vbCritical, ".:ERROR!")
            Exit Sub
        End If
        LTambahKode = True
        LAdd = False
        LEdit = False
        AturTombol(False)
        NoBoks.Focus()
    End Sub

    Private Sub CetakDPLTotalJumlah()
        Dim MsgSQL As String, NoBox As String = "", tKodeProduk As String = "",
            KodePImportir As String = "", JumTot As Integer,
            tNoPO As String = "", Importir As String = ""

        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable

        Me.Cursor = Cursors.WaitCursor

        MsgSQL = "Select * From RPT_DPLTotalJumlah"
        dbTable = Proses.ExecuteQuery(MsgSQL)

        If dbTable.Rows.Count <> 0 Then
            MsgSQL = "Truncate table RPT_DPLTotalJumlah"
            Proses.ExecuteNonQuery(MsgSQL)
        End If
        NoBox = ""
        tKodeProduk = ""

        Dim RS05 As New DataTable
        MsgSQL = "SELECT t_DPL.NoDPL, t_DPL.NoBoksAwal, t_DPL.NoBoksAkhir, t_DPL.JumlahBoks * " &
            "   t_DPL.TotalTiapBoks as JumTot, t_DPL.NoPO, t_DPL.KodePImportir, t_DPL.Importir,  " &
            "   t_DPL.KodeProduk, t_DPL.Produk " &
            "FROM Pekerti.dbo.t_DPL t_DPL  " &
            "Where t_DPL.NoDPL = '" & NoDPL.Text & "' " &
            "  And T_DPL.AktifYN = 'Y' " &
            "Order By KodeProduk, noboksawal "
        RS05 = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To RS05.Rows.Count - 1
            Application.DoEvents()
            If tKodeProduk <> RS05.Rows(i) !KodeProduk Then
                If tKodeProduk <> "" Then
                    MsgSQL = "INSERT INTO RPT_DPLTotalJumlah (NoDPL, Importir, " &
                        "KodeProduk, KodePImportir, JumTot, NoPO, NoBox) VALUES ( " &
                        "'" & RS05.Rows(i) !NoDPL & "', '" & RS05.Rows(i) !Importir & "', " &
                        "'" & tKodeProduk & "', '" & KodePImportir & "', " &
                        "" & JumTot & ", '" & tNoPO & "', '" & NoBox & "')"
                    Proses.ExecuteNonQuery(MsgSQL)
                End If
                NoBox = RS05.Rows(i) !NoBoksAwal + "-" + RS05.Rows(i) !NoBoksAkhir
                KodePImportir = RS05.Rows(i) !KodePImportir
                JumTot = RS05.Rows(i) !JumTot
                tNoPO = RS05.Rows(i) !NoPO
                Importir = RS05.Rows(i) !Importir
            Else
                NoBox = NoBox + ", " + RS05.Rows(i) !NoBoksAwal + "-" + RS05.Rows(i) !NoBoksAkhir
                JumTot = JumTot + RS05.Rows(i) !JumTot
            End If
            tKodeProduk = RS05.Rows(i) !KodeProduk
            'RS05.MoveNext
            If i = RS05.Rows.Count - 1 Then
                MsgSQL = "INSERT INTO RPT_DPLTotalJumlah (NoDPL, Importir, " &
                    "KodeProduk, KodePImportir, JumTot, NoPO, NoBox) VALUES ( " &
                    "'" & NoDPL.Text & "', '" & Importir & "', " &
                    "'" & tKodeProduk & "', '" & KodePImportir & "', " &
                    "" & JumTot & ", '" & tNoPO & "', '" & NoBox & "')"
                Proses.ExecuteNonQuery(MsgSQL)
            End If
        Next i
        Proses.OpenConn(CN)
        dttable = New DataTable
        MsgSQL = "SELECT RPT_DPLTotalJumlah.NoDPL, RPT_DPLTotalJumlah.Importir, " &
            "RPT_DPLTotalJumlah.KodeProduk, RPT_DPLTotalJumlah.KodePImportir, " &
            "RPT_DPLTotalJumlah.JumTot, RPT_DPLTotalJumlah.NoPO, RPT_DPLTotalJumlah.NoBox " &
            "FROM Pekerti.dbo.RPT_DPLTotalJumlah RPT_DPLTotalJumlah " &
            "Order by KodePImportir, KodeProduk "
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_DPLDaftarTotalJumlahProduk
            objRep.SetDataSource(dttable)
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
        MsgSQL = "Delete RPT_DPLTotalJumlah Where NODPL = '" & NoDPL.Text & "' "
        ' Proses.ExecuteNonQuery(MsgSQL)
    End Sub

    Private Sub cetakDPL()
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim tExpedisi As String = "", mRevisi As String = "", tanggal As String = ""

        Me.Cursor = Cursors.WaitCursor

        Proses.OpenConn(CN)
        dttable = New DataTable
        SQL = "SELECT t_DPL.NoDPL, t_DPL.NoBoksAwal, t_DPL.NoBoksAkhir, t_DPL.JumlahBoks, " &
            "   t_DPL.TotalTiapBoks, t_DPL.NoPO, t_DPL.KodeImportir, t_DPL.Importir,  " &
            "   t_DPL.KodeProduk, t_DPL.Produk, t_DPL.Panjang, t_DPL.Lebar, t_DPL.Tinggi,  " &
            "   t_DPL.Bruto , t_DPL.Netto, t_DPL.TotalJmlBoks, CargoLaut, CargoUdara, VolContainer,  " &
            "   right('0000000000'+noboksawal,10) + right('0000000000' + noboksakhir, 10) NnoBox " &
            "FROM Pekerti.dbo.t_DPL t_DPL  " &
            "Where t_DPL.NoDPL = '" & NoDPL.Text & "' " &
            "  And T_DPL.AktifYN = 'Y' " &
            "order by right('0000000000'+noboksawal,10), right('0000000000' + noboksakhir, 10), idrec "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            If optDaftarIsi.Checked = True Then
                objRep = New Rpt_DPL_DaftarIsi
            Else
                objRep = New Rpt_DPL
            End If
            objRep.SetDataSource(dttable)
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
    Private Sub cmdTambahDPL_Click(sender As Object, e As EventArgs) Handles cmdTambahDPL.Click
        TambahDPL()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        If optDaftarIsi.Checked = False _
            And optTotalJumlah.Checked = False _
            And optDPL.Checked = False Then
            MsgBox("Opsi Pencetakan Belum di pilih !", vbCritical + vbOKOnly, ".:Warning !")
            optDPL.Focus()
            Exit Sub
        End If
        If optTotalJumlah.Checked = True Then
            CetakDPLTotalJumlah()
        Else
            cetakDPL()
        End If
    End Sub

    Private Cmd As SqlCommand

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

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPL " &
            "Where AktifYN = 'Y' " &
            "  And IDRec > '" & idRec.Text & "' " &
            "  And NODPL = '" & NoDPL.Text & "' " &
            "ORDER BY tglDPL, IDRec  "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            idRec.Text = RSNav.Rows(0) !IDRec
            Call Isi_DPL
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPL " &
            "Where AktifYN = 'Y' " &
            "  And IDRec < '" & idRec.Text & "' " &
            "  And NODPL = '" & NoDPL.Text & "' " &
            "ORDER BY tglDPL desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            idRec.Text = RSNav.Rows(0) !IDRec
            Call Isi_DPL()
        End If
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPL " &
            "Where AktifYN = 'Y' " &
            "  And NoDPL = '" & NoDPL.Text & "' " &
            "ORDER BY TglDPL, IDRec "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            idRec.Text = RSNav.Rows(0) !IDRec
            Call Isi_DPL()
        End If
    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_DPL " &
            "Where AktifYN = 'Y' " &
            "  And NODPL = '" & NoDPL.Text & "' " &
            "ORDER BY tglDPL desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            idRec.Text = RSNav.Rows(0) !IDRec
            Call Isi_DPL()
        End If
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Isi_DPL()
        Dim MsgSQL As String, rsc As New DataTable
        MsgSQL = "Select * From T_DPL " &
            "Where idrec = '" & Trim(idRec.Text) & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 Then
            idRec.Text = rsc.Rows(0) !IDRec
            NoDPL.Text = rsc.Rows(0) !NoDPL
            tglDPL.Value = rsc.Rows(0) !tglDPL
            OptSemuaData.Checked = rsc.Rows(0) !CakupSemuaData
            OptSebagian.Checked = rsc.Rows(0) !CakupSebagian
            OptLaut.Checked = rsc.Rows(0) !CargoLaut
            OptUdara.Checked = rsc.Rows(0) !CargoUdara
            NoBoks.Text = rsc.Rows(0) !NoBoksAwal
            NoBoks2.Text = rsc.Rows(0) !NoBoksAkhir
            JumlahBoks.Text = rsc.Rows(0) !JumlahBoks
            TotalTiapBoks.Text = rsc.Rows(0) !TotalTiapBoks
            NoPO.Text = rsc.Rows(0) !NoPO
            KodeImportir.Text = rsc.Rows(0) !KodeImportir
            Importir.Text = rsc.Rows(0) !Importir
            KodeProduk.Text = rsc.Rows(0) !KodeProduk

            Produk.Text = rsc.Rows(0) !Produk
            KodePImportir.Text = rsc.Rows(0) !KodePImportir
            JumlahTiapBoks.Text = rsc.Rows(0) !JmlTiapBoks
            ShipmentDate.Value = rsc.Rows(0) !ShipmentDate
            Panjang.Text = rsc.Rows(0) !Panjang
            Lebar.Text = rsc.Rows(0) !Lebar
            Tinggi.Text = rsc.Rows(0) !Tinggi
            TotalVolumeBoks.Text = rsc.Rows(0) !TotalVolBoks
            Bruto.Text = rsc.Rows(0) !Bruto
            Netto.Text = rsc.Rows(0) !Netto
            VolContainer.Text = rsc.Rows(0) !VolContainer
            TotalVolDPL.Text = rsc.Rows(0) !TotalVolDPL
            TotalJumlahBoksDPL.Text = JumProduk(NoDPL.Text)
            optDPL.Checked = rsc.Rows(0) !OpsiDPL
            optDaftarIsi.Checked = rsc.Rows(0) !OpsiDaftarIsi
            optTotalJumlah.Checked = rsc.Rows(0) !OpsiTotalJumlah

            LocGmb1.Text = Trim(KodeProduk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
        End If
        Dim TotJumlah As Double = 0
        MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
        " m_KodeImportir.Nama, t_PO.Jumlah " &
        " From t_PO inner join m_KodeProduk ON " &
        " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
        " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
        "Where KodeProduk = '" & KodeProduk.Text & "' " &
        "  And t_PO.AktifYN = 'Y' " &
        "  And T_PO.NOPO = '" & NoPO.Text & "' "
        TotJumlah = Proses.ExecuteSingleDblQuery(MsgSQL)
        QTYPO.Text = Format(TotJumlah, "###,##0")

        MsgSQL = "Select Jumlah From t_PI " &
        "Where NOPO = '" & NoPO.Text & "' " &
        "  And AktifYN = 'Y' " &
        "  And Kode_Produk = '" & KodeProduk.Text & "' "
        TotJumlah = Proses.ExecuteSingleDblQuery(MsgSQL)
        QTYPI.Text = Format(TotJumlah, "###,##0")

    End Sub

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

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
        cmdTambahDPL.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True

        'Atur Readonly
        idRec.ReadOnly = True
        NoDPL.ReadOnly = tAktif
        PanelCakupanData.Enabled = Not tAktif
        PanelCargo.Enabled = Not tAktif
        NoBoks.ReadOnly = tAktif
        NoBoks2.ReadOnly = tAktif
        JumlahBoks.ReadOnly = True
        TotalTiapBoks.ReadOnly = tAktif
        tglDPL.Enabled = Not tAktif
        TotalTiapBoks.ReadOnly = tAktif
        NoPO.ReadOnly = tAktif
        KodeImportir.ReadOnly = tAktif
        Importir.ReadOnly = True
        KodeProduk.ReadOnly = tAktif
        Produk.ReadOnly = True
        KodePImportir.ReadOnly = tAktif
        JumlahTiapBoks.ReadOnly = tAktif
        ShipmentDate.Enabled = Not tAktif
        cmbJenisBox.Enabled = Not tAktif

        Panjang.ReadOnly = tAktif
        Lebar.ReadOnly = tAktif
        Tinggi.ReadOnly = tAktif
        TotalVolumeBoks.ReadOnly = tAktif
        VolContainer.ReadOnly = tAktif
        TotalVolDPL.ReadOnly = tAktif
        TotalJumlahBoksDPL.ReadOnly = tAktif
        HargaFOB.ReadOnly = True
        QTYPO.ReadOnly = True
        QTYPI.ReadOnly = True
        Bruto.ReadOnly = tAktif
        Netto.ReadOnly = tAktif

        Me.Text = "Draft Packing List (DPL)"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbJenisBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisBox.SelectedIndexChanged
        If Trim(cmbJenisBox.Text) <> "" Then
            IsiPLT()
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
        Netto.Text = 0
        Bruto.Text = 0
        VolContainer.Text = 0
        ShipmentDate.Value = Now
        optDPL.Checked = False
        optDaftarIsi.Checked = False
        optTotalJumlah.Checked = False
        OptSebagian.Checked = False
        OptSemuaData.Checked = False
        OptLaut.Checked = False
        OptUdara.Checked = False
        tglDPL.Value = Now
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

    Private Sub JumlahTiapBoks_TextChanged(sender As Object, e As EventArgs) Handles JumlahTiapBoks.TextChanged
        If Trim(JumlahTiapBoks.Text) = "" Then JumlahTiapBoks.Text = 0
        If IsNumeric(JumlahTiapBoks.Text) Then
            Dim temp As Double = JumlahTiapBoks.Text
            JumlahTiapBoks.Text = Format(temp, "###,##0")
            JumlahTiapBoks.SelectionStart = JumlahTiapBoks.TextLength
        Else
            JumlahTiapBoks.Text = 0
        End If
    End Sub

    Private Sub Form_GD_DPL_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String
        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        ClearTextBoxes()
        Dim Rs As New DataTable

        cmbCari.Items.Add("No PO")
        cmbCari.Items.Add("No DPL")
        cmbCari.Items.Add("Kode Barang")
        IsiJenisBox()
        MsgSQL = "SELECT top 1 IDrec From T_DPL " &
            "Where AktifYN = 'Y' " &
            "ORDER BY tglDPL Desc, IDRec Desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            idRec.Text = Rs.Rows(0) !IDRec
        Else
            idRec.Text = ""
        End If
        Call Isi_DPL()
        optDPL.Checked = True
        tTambah = Proses.UserAksesTombol(UserID, "53_DPL", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "53_DPL", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "53_DPL", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "53_DPL", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarDPL()
        idRec.Visible = False
    End Sub

    Private Sub IsiJenisBox()
        Dim MsgSQL As String, Rs As New DataTable
        MsgSQL = "Select * From M_DimensiBOX Order By JenisBox"
        Rs = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To Rs.Rows.Count - 1
            Application.DoEvents()
            cmbJenisBox.Items.Add(Microsoft.VisualBasic.Left(Rs.Rows(i) !JenisBox & Space(50), 50) &
                Microsoft.VisualBasic.Left(Format(Rs.Rows(i) !Panjang, "###,##0.00") & Space(20), 20) &
                Microsoft.VisualBasic.Left(Format(Rs.Rows(i) !Lebar, "###,##0.00") & Space(20), 20) &
                Microsoft.VisualBasic.Left(Format(Rs.Rows(i) !Tinggi, "###,##0.00") & Space(20), 20) &
                Microsoft.VisualBasic.Left(Format(Rs.Rows(i) !Panjang * Rs.Rows(i) !Lebar * Rs.Rows(i) !Tinggi / 1000000, "###,##0.00") & Space(20), 20)
            )
        Next i
        Proses.CloseConn()
    End Sub

    Private Sub TotalTiapBoks_TextChanged(sender As Object, e As EventArgs) Handles TotalTiapBoks.TextChanged
        If Trim(TotalTiapBoks.Text) = "" Then TotalTiapBoks.Text = 0
        If IsNumeric(TotalTiapBoks.Text) Then
            Dim temp As Double = TotalTiapBoks.Text
            TotalTiapBoks.Text = Format(temp, "###,##0")
            TotalTiapBoks.SelectionStart = TotalTiapBoks.TextLength
        Else
            TotalTiapBoks.Text = 0
        End If
        JumlahTiapBoks.Text = TotalTiapBoks.Text
    End Sub

    Private Sub DaftarDPL()
        Dim rsdaftar As New DataTable
        Dim MsgSQL As String, mKondisi As String
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        mKondisi = ""
        If Trim(tCari.Text) <> "" Then
            If cmbCari.Text = "No PO" Then
                mKondisi = " and nopo like '%" & tCari.Text & "%' "
            ElseIf cmbCari.Text = "No DPL" Then
                mKondisi = " and nodpl like '%" & tCari.Text & "%' "
            ElseIf cmbCari.Text = "Kode Barang" Then
                mKondisi = " and kodeProduk like '%" & tCari.Text & "%' "
            End If
        End If

        MsgSQL = "SELECT NoDPL, TglDPL, max(NoPO) NoPO, Max(ShipmentDate) ShipmentDate, max(Importir) Importir " &
            " FROM t_DPL " &
            "WHERE AktifYN = 'Y' " & mKondisi & " AND nopo <> '' " &
            "  AND Year(tglDPL) >= (year(getdate())-3) " &
            "GROUP By NoDPL, TglDPL " &
            "ORDER BY TGLDPL DESC, RIGHT(NODPL,2) + LEFT(NODPL,3) desc "

        rsdaftar = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To rsdaftar.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(rsdaftar.Rows(a) !NoDPL,
                   Format(rsdaftar.Rows(a) !tglDPL, "dd-MM-yyyy"),
                   rsdaftar.Rows(a) !NoPO,
                   Format(rsdaftar.Rows(a) !ShipmentDate, "dd-MM-yyyy"),
                   rsdaftar.Rows(a) !Importir)
        Next a
        Me.Cursor = Cursors.Default
        DGView.Visible = True
    End Sub
    Private Function QTY_SudahDPL() As Double
        Dim MsgSQL As String, RSJ As New DataTable, mKondisi As String
        mKondisi = ""
        If LEdit Then
            mKondisi = " And IDRec <> '" & idRec.Text & "' "
        End If
        MsgSQL = "select isnull(sum(jumlahboks * TotalTiapBoks),0) QTYDPL, " &
            "sum(jumlahboks * JmlTiapBoks) QDPL From t_DPL " &
            "Where nopo = '" & NoPO.Text & "' and aktifYN = 'Y' " &
            " AND KodeProduk = '" & KodeProduk.Text & "' " & mKondisi & " "
        RSJ = Proses.ExecuteQuery(MsgSQL)
        If RSJ.Rows.Count <> 0 Then
            QTY_SudahDPL = RSJ.Rows(0) !qtydpl
        Else
            QTY_SudahDPL = 0
        End If
    End Function

    Private Sub NoPO_TextChanged(sender As Object, e As EventArgs) Handles NoPO.TextChanged

    End Sub

    Private Function JumProduk(tDPL As String) As Integer
        Dim MsgSQL As String, RSJ As New DataTable, JProduk As Integer
        JProduk = 0
        MsgSQL = "Select NoDPL, KodeProduk From T_DPL " &
        "Where NoDPL = '" & NoDPL.Text & "' " &
        " And AktifYN = 'Y' " &
        "Group By NoDPL, KodeProduk"
        RSJ = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSJ.Rows.Count - 1
            Application.DoEvents()
            JProduk = JProduk + 1
        Next a
        JumProduk = JProduk
    End Function

    Public Function MaxNoUrutDPL(tField As String, tTable As String, Kode As String, tGL As String) As String
        Dim MsgSQL As String, RsMax As New DataTable
        MsgSQL = "Select convert(Char(2), GetDate(), 12) TGL, isnull(Max(RIGHT(" & tField & ",5)),0) + 1000001 RecId " &
            " From " & tTable & " " &
            "Where LEFT(" & tField & ",4) = '" & tGL & "' and len(idrec) = 12 "
        RsMax = Proses.ExecuteQuery(MsgSQL)
        MaxNoUrutDPL = Trim(tGL) + Kode + Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 5)
    End Function
    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim TMP As Double
        If JumlahTiapBoks.Text = "" Then JumlahTiapBoks.Text = 0
        If JumlahBoks.Text = "" Then JumlahBoks.Text = 0
        If TotalTiapBoks.Text = "" Then TotalTiapBoks.Text = 0
        If QTYPI.Text = "" Then TotalTiapBoks.Text = 0
        TMP = QTY_SudahDPL() + ((JumlahTiapBoks.Text * 1) * (JumlahBoks.Text * 1))
        If TMP > (QTYPI.Text * 1) Then
            MsgBox("DPL untuk Produk ini melebihi QTY PI", vbCritical + vbOKOnly, ".:Error!")
            JumlahTiapBoks.Focus()
            Exit Sub
        End If
        TotalJumlahBoksDPL.Text = JumProduk(NoDPL.Text)
        If trim(cmbJenisBox.Text) = "" Then
            MsgBox("Jenis Box belum di pilih !", vbCritical + vbOKOnly, ".:Error !")
            cmbJenisBox.Select()
            cmbJenisBox.Focus()
            Exit Sub
        End If
        If LAdd Then
            NoDPL.Text = Proses.MaxYNoUrut("NoDPL", "t_DPL", "DPL")
        End If

        If LAdd Or LTambahKode Then

            idRec.Text = MaxNoUrutDPL("IDRec", "t_DPL", "DPL", Format(Now, "yyMM"))
            If VolContainer.Text = "" Then VolContainer.Text = 0
            ' Error disini request by marno !
            If TotalVolumeBoks.Text = "" Then
                TotalVolumeBoks.Text = 0
                MsgBox("Total Volume Boks Salah!", vbOKOnly + vbCritical, ".:data tidak boleh kosong!")
                TotalVolumeBoks.Focus()
                Exit Sub
            End If
            MsgSQL = "INSERT INTO Pekerti.dbo.t_DPL(IDRec, NoDPL, TglDPL, " &
                    "CakupSemuaData, CakupSebagian, CargoLaut, CargoUdara, NoBoksAwal, " &
                    "NoBoksAkhir, JumlahBoks, TotalTiapBoks, NoPO, KodeImportir, " &
                    "Importir, KodeProduk, Produk, KodePImportir, JmlTiapBoks, " &
                    "ShipmentDate, Panjang, Lebar, Tinggi, TotalVolBoks, Bruto, " &
                    "Netto, VolContainer, TotalVolDPL, TotalJmlBoks, OpsiDPL, " &
                    "OpsiDaftarIsi, OpsiTotalJumlah, FotoLoc, transferYN, AktifYN, " &
                    "UserID, LastUPD) VALUES ('" & idRec.Text & "', '" & NoDPL.Text & "', " &
                    "'" & Format(tglDPL.Value, "yyyy-MM-dd") & "', " &
                    "" & IIf(OptSemuaData.Checked, 1, 0) & ", " & IIf(OptSebagian.Checked, 1, 0) & ", " &
                    "" & IIf(OptLaut.Checked, 1, 0) & ", " & IIf(OptUdara.Checked, 1, 0) & ", " &
                    "'" & Trim(NoBoks.Text) & "', '" & Trim(NoBoks2.Text) & "', " &
                    "" & Trim(JumlahBoks.Text) * 1 & " , " & TotalTiapBoks.Text * 1 & ", " &
                    "'" & Trim(NoPO.Text) & "', '" & Trim(KodeImportir.Text) & "', " &
                    "'" & Trim(Importir.Text) & "', '" & Trim(KodeProduk.Text) & "', " &
                    "'" & Trim(Produk.Text) & "', '" & Trim(KodePImportir.Text) & "', " &
                    "" & JumlahTiapBoks.Text * 1 & ", '" & Format(ShipmentDate.Value, "yyyy-MM-dd") & "', " &
                    "" & Panjang.Text * 1 & ", " & Lebar.Text * 1 & ", " & Tinggi.Text * 1 & ", " &
                    "" & TotalVolumeBoks.Text * 1 & ", " & Bruto.Text * 1 & ", " & Netto.Text * 1 & ", " &
                    "" & VolContainer.Text * 1 & "," & TotalVolDPL.Text * 1 & ", " & TotalJumlahBoksDPL.Text * 1 & ", " &
                    "" & IIf(optDPL.Checked, 1, 0) & ", " & IIf(optDaftarIsi.Checked, 1, 0) & ", " &
                    "" & IIf(optTotalJumlah.Checked, 1, 0) & ", '" & Trim(LocGmb1.Text) & "', " &
                    "'N', 'Y', '" & UserID & "', GetDate())"
            Proses.ExecuteNonQuery(MsgSQL)
            TambahDPL()
            KodeProduk.Text = ""
        ElseIf LEdit Then
            MsgSQL = "Update t_DPL Set " &
                    "TglDPL = '" & Format(tglDPL.Value, "yyyy-MM-dd") & "', " &
                    "CakupSemuaData = " & IIf(OptSemuaData.Checked, 1, 0) & ", CakupSebagian = " & IIf(OptSebagian.Checked, 1, 0) & ", " &
                    "  CargoLaut = " & IIf(OptLaut.Checked, 1, 0) & ", CargoUdara = " & IIf(OptUdara.Checked, 1, 0) & ", " &
                    " NoBoksAwal = '" & Trim(NoBoks.Text) & "', NoBoksAkhir = '" & Trim(NoBoks2.Text) & "', " &
                    " JumlahBoks = " & Trim(JumlahBoks.Text) * 1 & " , TotalTiapBoks = " & TotalTiapBoks.Text * 1 & ", " &
                    "    NoPO = '" & Trim(NoPO.Text) & "', KodeImportir = '" & Trim(KodeImportir.Text) & "', " &
                    "Importir = '" & Trim(Importir.Text) & "', KodeProduk = '" & Trim(KodeProduk.Text) & "', " &
                    "  Produk = '" & Trim(Produk.Text) & "', KodePImportir = '" & Trim(KodePImportir.Text) & "', " &
                    "JmlTiapBoks = " & JumlahTiapBoks.Text * 1 & ", ShipmentDate = '" & Format(ShipmentDate.Value, "yyyy-MM-dd") & "', " &
                    "    Panjang = " & Panjang.Text * 1 & ", Lebar = " & Lebar.Text * 1 & ", Tinggi = " & Tinggi.Text * 1 & ", " &
                    "TotalVolBoks = " & TotalVolumeBoks.Text * 1 & ", Bruto = " & Bruto.Text * 1 & ", Netto = " & Netto.Text * 1 & ", " &
                    "VolContainer = " & VolContainer.Text * 1 & ", TotalVolDPL = " & TotalVolDPL.Text * 1 & ",  " &
                    "TotalJmlBoks = " & TotalJumlahBoksDPL.Text * 1 & ", " &
                    "OpsiDPL = " & IIf(optDPL.Checked, 1, 0) & ", OpsiDaftarIsi = " & IIf(optDaftarIsi.Checked, 1, 0) & ", " &
                    "OpsiTotalJumlah = " & IIf(optTotalJumlah.Checked, 1, 0) & ", " &
                    "FotoLoc = '" & Trim(LocGmb1.Text) & "', TransferYN = 'N', " &
                    " UserID = '" & UserID & "', LastUPD = GetDate() " &
                    " Where IdRec = '" & idRec.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            MsgSQL = "Update t_DPL Set " &
                "TransferYN = 'N', UserID = '" & UserID & "', " &
                "   LastUPD = GetDate() " &
                " Where NoDPL = '" & NoDPL.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            LTambahKode = False
            LAdd = False
            LEdit = False
            AturTombol(True)
        End If

    End Sub

    Private Sub KodeImportir_TextChanged(sender As Object, e As EventArgs) Handles KodeImportir.TextChanged
        If Len(KodeImportir.Text) < 1 Then
            KodeImportir.Text = ""
            Importir.Text = ""
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String
        Dim RSL As New DataTable
        If DGView.Rows.Count = 0 Then Exit Sub
        DGView2.Rows.Clear()
        DGView2.Visible = False
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        MsgSQL = "SELECT  * " &
            " FROM t_DPL " &
            "Where AktifYN = 'Y' " &
            "  And NoDPL = '" & tCari & "' " &
            "order by right('0000000000'+noboksawal,10), right('0000000000' + noboksakhir, 10), idrec "
        RSL = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSL.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSL.Rows(a) !idrec,
                    RSL.Rows(a) !NoBoksAwal,
                    RSL.Rows(a) !NoBoksAkhir,
                    Format(RSL.Rows(a) !JumlahBoks, "###,##0"),
                    Format(RSL.Rows(a) !TotalTiapBoks, "###,##0"),
                    RSL.Rows(a) !KodeProduk & " " & RSL.Rows(a) !Produk,
                    Format(RSL.Rows(a) !JmlTiapBoks, "###,##0"),
                    RSL.Rows(a) !NoPO,
                    RSL.Rows(a) !KodePImportir, "History")
        Next a
        DGView2.Visible = True
        If DGView2.Rows.Count <> 0 Then
            DGView2_CellClick(sender, e)
        End If
    End Sub

    Private Sub KodeProduk_TextChanged(sender As Object, e As EventArgs) Handles KodeProduk.TextChanged
        If Len(KodeProduk.Text) < 1 Then
            KodeProduk.Text = ""
            Produk.Text = ""
            ShowFoto("")
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

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        If DGView2.Rows.Count = 0 Then Exit Sub
        idRec.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        Isi_DPL()
        'Dim tID As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        'idRec.Text = tID
        If DGView2.Rows.Count <> 0 Then
            If e.ColumnIndex = 9 Then 'History
                MsgSQL = "select nopo, NoDPL, NoBoksAwal,NoBoksAkhir, JumlahBoks, TotalTiapBoks, JumlahBoks * TotalTiapBoks QTYDPL " &
                   " From t_DPL " &
                   "Where nopo = '" & NoPO.Text & "' and aktifYN = 'Y' " &
                   " AND KodeProduk = '" & KodeProduk.Text & "' "
                Form_Daftar.txtQuery.Text = MsgSQL
                Form_Daftar.Text = "History DPL"
                Form_Daftar.ShowDialog()
                FrmMenuUtama.TSKeterangan.Text = ""
            End If
        End If

    End Sub
    Private Sub IsiPLT()
        Panjang.Text = Trim(Mid(cmbJenisBox.Text, 51, 20))
        Lebar.Text = Trim(Mid(cmbJenisBox.Text, 71, 20))
        Tinggi.Text = Trim(Mid(cmbJenisBox.Text, 91, 20))
        TotalVolumeBoks.Text = Trim(Mid(cmbJenisBox.Text, 101, 20))
        HitungVolume()
    End Sub
    Private Sub cmbJenisBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenisBox.KeyPress
        If e.KeyChar = Chr(13) Then
            IsiPLT()
            Bruto.Focus()
        End If
    End Sub

    Private Sub NoBoks_TextChanged(sender As Object, e As EventArgs) Handles NoBoks.TextChanged
        If Trim(NoBoks.Text) = "" Then NoBoks.Text = 0
        If IsNumeric(NoBoks.Text) Then
            Dim temp As Double = NoBoks.Text
            NoBoks.Text = Format(temp, "###,##0")
            NoBoks.SelectionStart = NoBoks.TextLength
            If NoBoks2.Text = "" Then NoBoks2.Text = 0
            JumlahBoks.Text = (NoBoks2.Text * 1) - (NoBoks.Text * 1) + 1
        Else
            NoBoks.Text = 0
        End If
    End Sub

    Private Sub HitungVolume()
        Dim HitVol As Double, RSHIT As New DataTable, MsgSQL As String
        If Trim(Panjang.Text) = "" Then Panjang.Text = 0
        If Trim(Tinggi.Text) = "" Then Tinggi.Text = 0
        If Trim(Lebar.Text) = "" Then Lebar.Text = 0
        If Trim(JumlahBoks.Text) = "" Then JumlahBoks.Text = 0
        If OptLaut.Checked = True Then
            HitVol = (Panjang.Text * 1) * (Tinggi.Text * 1) * (Lebar.Text * 1) * (JumlahBoks.Text * 1) / 1000000
        ElseIf OptUdara.Checked = True Then
            HitVol = (Panjang.Text * 1) * (Tinggi.Text * 1) * (Lebar.Text * 1) * (JumlahBoks.Text * 1) / 6000
        ElseIf OptUdara.Checked = False And OptLaut.Checked = False Then
            '        MsgBox "cargo belum dipilih mas...!", vbCritical + vbOKOnly, ".:Nganggo cargo opo mas...!"
            '        Exit Sub
            HitVol = (Panjang.Text * 1) * (Tinggi.Text * 1) * (Lebar.Text * 1) * (JumlahBoks.Text * 1)
        End If
        TotalVolumeBoks.Text = Format(HitVol, "###,##0.000")
        MsgSQL = "Select isnull(Sum(TotalVolBoks),0) TotVolDPL, " &
            "isnull(Sum(JumlahBoks),0) TotJumBoks " &
            " From t_DPL " &
            "Where aktifYN = 'Y' AND NoDPL = '" & NoDPL.Text & "' "
        RSHIT = Proses.ExecuteQuery(MsgSQL)
        If RSHIT.Rows.Count <> 0 Then
            If (RSHIT.Rows(0) !totvoldpl) = 0 Then
                TotalVolDPL.Text = Format(0 + (TotalVolumeBoks.Text * 1), "###,##0.0000")
            Else
                TotalVolDPL.Text = Format(RSHIT.Rows(0) !totvoldpl + (TotalVolumeBoks.Text * 1), "###,##0.000")
            End If
            If (RSHIT.Rows(0) !totjumboks) = 0 Then
                TotalJumlahBoksDPL.Text = 1
            Else
                TotalJumlahBoksDPL.Text = Format(JumProduk(NoDPL.Text), "###")
            End If
        End If
    End Sub

    Private Sub JumlahTiapBoks_GotFocus(sender As Object, e As EventArgs) Handles JumlahTiapBoks.GotFocus
        With JumlahTiapBoks
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub JumlahTiapBoks_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JumlahTiapBoks.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If JumlahTiapBoks.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(JumlahTiapBoks.Text) Then
                Dim temp As Double = JumlahTiapBoks.Text
                JumlahTiapBoks.Text = Format(temp, "###,##0")
                JumlahTiapBoks.SelectionStart = JumlahTiapBoks.TextLength
            Else
                JumlahTiapBoks.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then ShipmentDate.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub TotalTiapBoks_GotFocus(sender As Object, e As EventArgs) Handles TotalTiapBoks.GotFocus
        With TotalTiapBoks
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub TotalTiapBoks_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TotalTiapBoks.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If TotalTiapBoks.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(TotalTiapBoks.Text) Then
                Dim temp As Double = TotalTiapBoks.Text
                TotalTiapBoks.Text = Format(temp, "###,##0")
                TotalTiapBoks.SelectionStart = TotalTiapBoks.TextLength
            Else
                TotalTiapBoks.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then NoPO.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub ShipmentDate_ValueChanged(sender As Object, e As EventArgs) Handles ShipmentDate.ValueChanged

    End Sub

    Private Sub NoPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPO.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim RSN1 As New DataTable
            MsgSQL = "Select NoPO, Tglpo, TglKirim, Nama, Kode_Importir " &
                " From T_PO inner join m_KodeImportir ON KodeImportir = Kode_Importir " &
                "Where NOPO = '" & NoPO.Text & "' "
            RSN1 = Proses.ExecuteQuery(MsgSQL)
            If RSN1.Rows.Count <> 0 Then
                KodeImportir.Text = RSN1.Rows(0) !Kode_Importir
                Importir.Text = RSN1.Rows(0) !Nama
                KodeProduk.Focus()
            Else
                If LTambahKode Then
                    NoPO.Text = FindPO_DPL(NoPO.Text, KodeImportir.Text)
                Else
                    NoPO.Text = FindPO(NoPO.Text)
                End If

                MsgSQL = "Select NoPO, TglPO, TglKirim, Nama, Kode_Importir " &
                    "From T_PO inner join m_KodeImportir ON KodeImportir = Kode_Importir " &
                    "Where NOPO = '" & NoPO.Text & "' "
                RSN1 = Proses.ExecuteQuery(MsgSQL)
                If RSN1.Rows.Count <> 0 Then
                    KodeImportir.Text = RSN1.Rows(0) !Kode_Importir
                    Importir.Text = RSN1.Rows(0) !Nama
                    KodeProduk.Focus()
                Else
                    MsgBox("NO PO tidak boleh kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                    NoPO.Focus()
                    Exit Sub
                End If
            End If
            Proses.CloseConn()
        End If
    End Sub

    Private Sub Bruto_TextChanged(sender As Object, e As EventArgs) Handles Bruto.TextChanged
        If Trim(Bruto.Text) = "" Then Bruto.Text = 0
        If IsNumeric(Bruto.Text) Then
            Dim temp As Double = Bruto.Text
            Bruto.Text = Format(temp, "###,##0")
            Bruto.SelectionStart = Bruto.TextLength
            Netto.Text = Format(temp - 2, "###,##0")
        Else
            Bruto.Text = 0
        End If
    End Sub

    Public Function FindPO(Cari As String) As String
        Dim RSD As New DataTable, mKondisi As String,
            MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoPO like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO, KodeImportir, max(tglkirim) tglkirim " &
            " From T_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
            "Where T_PO.AktifYN = 'Y' " &
            "  " & mKondisi & " " &
            "Group By NoPO, m_KodeImportir.Nama, TglPO, KodeImportir " &
            "Order By TglPO Desc, NoPO Desc "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar PO"
        Form_Daftar.ShowDialog()
        FindPO = FrmMenuUtama.TSKeterangan.Text
    End Function

    Public Function FindPO_DPL(Cari As String, tImportir As String) As String
        Dim mKondisi As String, MsgSQL As String
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoPO like '%" & Trim(Cari) & "%' "
        End If
        mKondisi = mKondisi + " And Kode_Importir = '" & Trim(tImportir) & "' "
        MsgSQL = "Select NoPO, m_KodeImportir.Nama Importir, TglPO, KodeImportir, max(t_PO.tglKirim) tglKirim " &
            " FROM T_PO Inner Join m_KodeImportir on Kode_Importir = KodeImportir " &
            "WHERE T_PO.AktifYN = 'Y' " &
            "  " & mKondisi & " " &
            "GROUP BY NoPO, m_KodeImportir.Nama, TglPO, KodeImportir " &
            "ORDER BY TglPO Desc, NoPO Desc "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar PO-DPL"
        Form_Daftar.ShowDialog()
        FindPO_DPL = FrmMenuUtama.TSKeterangan.Text

    End Function

    Private Sub KodeImportir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeImportir.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_kodeImportir " &
              " Where KodeImportir = '" & KodeImportir.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Importir.Text = dbTable.Rows(0) !nama
                KodeProduk.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_KodeImportir " &
                    "Where AktifYN = 'Y' " &
                    "  And ( KodeImportir Like '%" & KodeImportir.Text & "%' or nama Like '%" & KodeImportir.Text & "%') " &
                    "Order By nama "
                Form_Daftar.Text = "Daftar Importir"
                Form_Daftar.ShowDialog()

                KodeImportir.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nama From m_KodeImportir " &
                   " Where KodeImportir = '" & KodeImportir.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Importir.Text = dbTable.Rows(0) !nama
                    KodeProduk.Focus()
                Else
                    KodeImportir.Text = ""
                    Importir.Text = ""
                    KodeImportir.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub VolContainer_TextChanged(sender As Object, e As EventArgs) Handles VolContainer.TextChanged

    End Sub

    Private Sub KodeProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeProduk.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim RSP As New DataTable, rs05 As New DataTable
            Me.Cursor = Cursors.WaitCursor
            MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                "     m_KodeImportir.Nama, t_PO.Jumlah, file_foto " &
                "From t_PO inner join m_KodeProduk ON " &
                "     m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                "     inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                "Where KodeProduk = '" & KodeProduk.Text & "' " &
                "  And t_PO.AktifYN = 'Y' " &
                "  And T_PO.NOPO = '" & NoPO.Text & "' "
            RSP = Proses.ExecuteQuery(MsgSQL)
            If RSP.Rows.Count <> 0 Then
                Produk.Text = Replace(RSP.Rows(0) !Deskripsi, "'", "`")
                KodeImportir.Text = RSP.Rows(0) !Kode_Importir
                Importir.Text = RSP.Rows(0) !Nama
                KodePImportir.Text = RSP.Rows(0) !Kode_Buyer
                QTYPO.Text = RSP.Rows(0) !Jumlah
                JumlahTiapBoks.Focus()
                LocGmb1.Text = RSP.Rows(0) !file_foto
                If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                    ShowFoto("")
                Else
                    ShowFoto(LocGmb1.Text)
                End If
            Else
                Dim mKondisi As String = ""
                If Trim(KodeProduk.Text) = "" Then
                    mKondisi = ""
                Else
                    mKondisi = "And Deskripsi like '%" & Trim(KodeProduk.Text) & "%' "
                End If
                MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                    " m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah " &
                    " From t_PO inner join m_KodeProduk ON " &
                    " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & NoPO.Text & "' " &
                    " " & mKondisi & " order by kode_buyer"
                Form_Daftar.txtQuery.Text = MsgSQL
                Form_Daftar.Text = "Daftar Produk DPL"
                Form_Daftar.ShowDialog()
                KodeProduk.Text = FrmMenuUtama.TSKeterangan.Text
                MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
                    "     m_KodeImportir.Nama, t_PO.Jumlah, file_foto " &
                    "From t_PO inner join m_KodeProduk ON " &
                    "     m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
                    "     inner join m_KodeImportir on Kode_Importir = KodeImportir " &
                    "Where KodeProduk = '" & KodeProduk.Text & "' " &
                    "  And t_PO.AktifYN = 'Y' " &
                    "  And T_PO.NOPO = '" & NoPO.Text & "' "
                RSP = Proses.ExecuteQuery(MsgSQL)
                If RSP.Rows.Count <> 0 Then
                    Produk.Text = Replace(RSP.Rows(0) !Deskripsi, "'", "`")
                    KodeImportir.Text = RSP.Rows(0) !Kode_Importir
                    Importir.Text = RSP.Rows(0) !Nama
                    KodePImportir.Text = RSP.Rows(0) !Kode_Buyer
                    QTYPO.Text = RSP.Rows(0) !Jumlah
                    JumlahTiapBoks.Focus()
                    LocGmb1.Text = RSP.Rows(0) !file_foto
                    If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                        ShowFoto("")
                    Else
                        ShowFoto(LocGmb1.Text)
                    End If
                Else
                    KodeProduk.Text = ""
                    Produk.Text = ""
                    KodePImportir.Text = ""
                    KodeImportir.Text = ""
                    Importir.Text = ""
                    KodePImportir.Text = ""
                    QTYPO.Text = ""
                    ShowFoto("")
                End If
            End If
            IsiQTYPi()

            If LAdd Or LEdit Then
                If Produk.Text = "" Then
                    KodeProduk.Focus()
                Else
                    JumlahTiapBoks.Focus()
                End If
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub TotalVolDPL_TextChanged(sender As Object, e As EventArgs) Handles TotalVolDPL.TextChanged

    End Sub

    Private Sub TotalJumlahBoksDPL_TextChanged(sender As Object, e As EventArgs) Handles TotalJumlahBoksDPL.TextChanged
        With TotalJumlahBoksDPL
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub IsiQTYPi()
        Dim MsgSQL As String, rsc As New DataTable
        MsgSQL = "Select isnull(Jumlah, 0) Jumlah  From t_PI " &
            "Where NOPO = '" & NoPO.Text & "' " &
            "  And AktifYN = 'Y' " &
            "  And Kode_Produk = '" & KodeProduk.Text & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 Then
            QTYPI.Text = rsc.Rows(0) !Jumlah
        Else
            QTYPI.Text = 0
        End If
    End Sub

    Private Sub NoBoks_GotFocus(sender As Object, e As EventArgs) Handles NoBoks.GotFocus
        With NoBoks
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub NoBoks_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoBoks.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If NoBoks.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(NoBoks.Text) Then
                If NoBoks.Text = "" Then NoBoks.Text = 0
                If NoBoks2.Text = "" Then NoBoks2.Text = 0
                Dim temp As Double = NoBoks.Text
                NoBoks.Text = Format(temp, "###,##0")
                NoBoks.SelectionStart = NoBoks.TextLength
                If NoBoks.Text = 0 And NoBoks2.Text = 0 Then
                    JumlahBoks.Text = 0
                Else
                    JumlahBoks.Text = (NoBoks2.Text * 1) - (NoBoks.Text * 1) + 1
                End If
                HitungVolume()
            Else
                NoBoks.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then NoBoks2.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub NoBoks2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoBoks2.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If NoBoks2.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(NoBoks.Text) Then
                If NoBoks.Text = "" Then NoBoks.Text = 0
                If NoBoks2.Text = "" Then NoBoks2.Text = 0

                If Trim(NoBoks2.Text) = 0 Then
                    JumlahBoks.Text = 0
                    MsgBox("Jumlah box ke dua tidak boleh nol/kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                    NoBoks2.Focus()
                    Exit Sub
                ElseIf NoBoks.Text = 0 Then
                    JumlahBoks.Text = 0
                    MsgBox("Jumlah box pertama tidak boleh nol/kosong!", vbCritical + vbOKOnly, ".:ERROR!")
                    NoBoks.Focus()
                    Exit Sub
                End If
                Dim temp As Double = NoBoks.Text
                NoBoks.Text = Format(temp, "###,##0")
                NoBoks.SelectionStart = NoBoks.TextLength
                If NoBoks.Text = 0 And NoBoks2.Text = 0 Then
                    JumlahBoks.Text = 0
                Else
                    JumlahBoks.Text = (NoBoks2.Text * 1) - (NoBoks.Text * 1) + 1
                End If
                HitungVolume()
            Else
                NoBoks.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then TotalTiapBoks.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(idRec.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical + vbOKOnly, "Warning!")
            Exit Sub
        Else
            ' IsiKodeProduk(KodeProduk.Text)
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        LTambahKode = False
        cmdSimpan.Visible = tEdit

        '---------------
        'If Trim(IdRec.Text) = "" Then
        '    MsgBox "Data yang akan di edit belum di pilih!", vbCritical, ".:Empty Data!"
        'Exit Sub
        'End If
        'LAdd = False
        'LEdit = True
        'LTambahKode = False
        'DisableTombol
    End Sub

    Private Sub NoBoks2_GotFocus(sender As Object, e As EventArgs) Handles NoBoks2.GotFocus
        With NoBoks2
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Tinggi_TextChanged(sender As Object, e As EventArgs) Handles Tinggi.TextChanged
        If Trim(Tinggi.Text) = "" Then Tinggi.Text = 0
        If IsNumeric(Tinggi.Text) Then
            Dim temp As Double = Tinggi.Text
            Tinggi.Text = Format(temp, "###,##0")
            Tinggi.SelectionStart = Tinggi.TextLength
        Else
            Tinggi.Text = 0
        End If
    End Sub

    Private Sub NoBoks2_TextChanged(sender As Object, e As EventArgs) Handles NoBoks2.TextChanged
        If Trim(NoBoks2.Text) = "" Then NoBoks.Text = 0
        If IsNumeric(NoBoks2.Text) Then
            Dim temp As Double = NoBoks2.Text
            NoBoks2.Text = Format(temp, "###,##0")
            NoBoks2.SelectionStart = NoBoks2.TextLength
            If NoBoks.Text = "" Then NoBoks.Text = 0
            JumlahBoks.Text = (NoBoks2.Text * 1) - (NoBoks.Text * 1) + 1
        Else
            NoBoks2.Text = 0
        End If
    End Sub

    Private Sub Lebar_TextChanged(sender As Object, e As EventArgs) Handles Lebar.TextChanged
        If Trim(Lebar.Text) = "" Then Lebar.Text = 0
        If IsNumeric(Lebar.Text) Then
            Dim temp As Double = Lebar.Text
            Lebar.Text = Format(temp, "###,##0")
            Lebar.SelectionStart = Lebar.TextLength
        Else
            Lebar.Text = 0
        End If
    End Sub

    Private Sub Panjang_TextChanged(sender As Object, e As EventArgs) Handles Panjang.TextChanged
        If Trim(Panjang.Text) = "" Then Panjang.Text = 0
        If IsNumeric(Panjang.Text) Then
            Dim temp As Double = Panjang.Text
            Panjang.Text = Format(temp, "###,##0")
            Panjang.SelectionStart = Panjang.TextLength
        Else
            Panjang.Text = 0
        End If
    End Sub

    Private Sub TotalVolumeBoks_TextChanged(sender As Object, e As EventArgs) Handles TotalVolumeBoks.TextChanged
        If Trim(TotalVolumeBoks.Text) = "" Then TotalVolumeBoks.Text = 0
        If IsNumeric(Tinggi.Text) Then
            Dim temp As Double = TotalVolumeBoks.Text
            TotalVolumeBoks.Text = Format(temp, "###,##0.00")
            TotalVolumeBoks.SelectionStart = TotalVolumeBoks.TextLength
        Else
            TotalVolumeBoks.Text = 0
        End If
    End Sub

    Private Sub ShipmentDate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ShipmentDate.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbJenisBox.Focus()
        End If
    End Sub

    Private Sub Bruto_GotFocus(sender As Object, e As EventArgs) Handles Bruto.GotFocus
        With Bruto
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Bruto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bruto.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Bruto.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(NoBoks.Text) Then
                If Bruto.Text = "" Then Bruto.Text = 0
                Dim temp As Double = Bruto.Text
                Bruto.Text = Format(temp, "###,##0")
                Bruto.SelectionStart = Bruto.TextLength
            Else
                Bruto.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Netto.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Netto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Netto.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Netto.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(NoBoks.Text) Then
                If Netto.Text = "" Then Netto.Text = 0
                Dim temp As Double = Netto.Text
                Netto.Text = Format(temp, "###,##0")
                Netto.SelectionStart = Bruto.TextLength
            Else
                Netto.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then VolContainer.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub VolContainer_GotFocus(sender As Object, e As EventArgs) Handles VolContainer.GotFocus
        With VolContainer
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click

        If Trim(idRec.Text) = "" Then
            MsgBox("Data yang akan di hapus belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If

        Form_Hapus.Left = Me.Left
        Form_Hapus.Top = Me.Top
        Form_Hapus.tIDSebagian.Text = idRec.Text
        Form_Hapus.tIDSemua.Text = NoDPL.Text
        Form_Hapus.Text = "Hapus DPL"
        Form_Hapus.ShowDialog()
        ClearTextBoxes()
    End Sub

    Private Sub TotalVolDPL_GotFocus(sender As Object, e As EventArgs) Handles TotalVolDPL.GotFocus
        With TotalVolDPL
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub TotalVolDPL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TotalVolDPL.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If TotalVolDPL.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(TotalVolDPL.Text) Then
                If TotalVolDPL.Text = "" Then TotalVolDPL.Text = 0
                Dim temp As Double = TotalVolDPL.Text
                TotalVolDPL.Text = Format(temp, "###,##0.000")
            Else
                TotalVolDPL.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub VolContainer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles VolContainer.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If VolContainer.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(VolContainer.Text) Then
                If VolContainer.Text = "" Then VolContainer.Text = 0
                Dim temp As Double = VolContainer.Text
                VolContainer.Text = Format(temp, "###,##0")
            Else
                VolContainer.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then TotalVolDPL.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarDPL()
        End If
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = 0 Then
        ElseIf e.TabPageIndex = 1 Then
            DaftarDPL()
        End If
    End Sub

    Private Sub Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tinggi.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Panjang.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Tinggi.Text) Then
                Dim temp As Double = Tinggi.Text
                Tinggi.Text = Format(temp, "###,##0")
                Tinggi.SelectionStart = Tinggi.TextLength
                HitungVolume()
            Else
                Tinggi.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Bruto.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Panjang.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Panjang.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Panjang.Text) Then
                Dim temp As Double = Panjang.Text
                Panjang.Text = Format(temp, "###,##0")
                Panjang.SelectionStart = Panjang.TextLength
                HitungVolume()
            Else
                Panjang.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Lebar.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lebar.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Panjang.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Lebar.Text) Then
                Dim temp As Double = Lebar.Text
                Lebar.Text = Format(temp, "###,##0")
                Lebar.SelectionStart = Lebar.TextLength
                HitungVolume()
            Else
                Lebar.Text = 0
            End If
            If LAdd Or LEdit Or LTambahKode Then Tinggi.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Panjang_GotFocus(sender As Object, e As EventArgs) Handles Panjang.GotFocus
        With Panjang
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Lebar_GotFocus(sender As Object, e As EventArgs) Handles Lebar.GotFocus
        With Lebar
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Tinggi_GotFocus(sender As Object, e As EventArgs) Handles Tinggi.GotFocus
        With Tinggi
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub
End Class