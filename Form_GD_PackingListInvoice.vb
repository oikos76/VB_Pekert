Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Public Class Form_GD_PackingListInvoice
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        lKoordinator As String, lPemeriksa As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter

    Private Function CekPI() As Boolean
        Dim Rs As New DataTable, rs03 As New DataTable,
            MsgSQL As String, NoPOTidakAda As Boolean

        NoPOTidakAda = False
        MsgSQL = "Select * from t_DPL " &
            "Where NoDPL = '" & txtNoDPL.Text & "' " &
            " And AktifYN = 'Y' order by idrec "
        Rs = Proses.ExecuteQuery(MsgSQL)

        For i = 0 To Rs.Rows.Count - 1
            Application.DoEvents()
            MsgSQL = "Select * from t_PI " &
            "WHere NoPO = '" & Rs.Rows(i) !NoPO & "' " &
            "  AND Kode_Produk = '" & Rs.Rows(0) !KodeProduk & "' " &
            "  And AktifYN = 'Y' "
            rs03 = Proses.ExecuteQuery(MsgSQL)
            If rs03.Rows.Count <> 0 Then 
            Else
                MsgBox("PO NO : " & Rs.Rows(i) !NoPO & " Kode Produk : " &
                        Rs.Rows(i) !KodeProduk & " di PI tidak ada!" & vbCrLf & "Harga FOB NOL!!", vbCritical + vbOKOnly, ".:Warning!")
                HargaFOB.Text = 0
                NoPOTidakAda = True
                Exit For
            End If
        Next i
        CekPI = NoPOTidakAda
    End Function
    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim Rs As New DataTable, RS05 As New DataTable, tTotal As Double,
            JumADD As Double, tNoPO As String, mNoPackList As String
        Dim mTgl As String = ""
        If LAdd Or LEdit Then

            If CekPI Then
                MsgBox("Tidak bisa simpan, ada  PI yang bermasalah di harga/kode produk" & vbCrLf &
                    "Catat kode produk, PI yg bermasalah tadi!", vbCritical + vbOKOnly, ".:Warning!")
                Exit Sub
            End If

            If LAdd Then
                MsgSQL = "Select * From t_PackingList Where NoPackingList = '" & Trim(NoPackingList.Text) & "' "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                If RS05.Rows.Count <> 0 Then
                    MsgBox("No Packing List " & Trim(NoPackingList.Text) & " SUDAH pernah di buat!", vbCritical + vbOKOnly, ".:Warning!")
                    Exit Sub
                End If
            End If
            If LEdit Then
                MsgSQL = "Update t_PackingList set " &
                    " AktifYN = 'N',  " &
                    " UserID = '" & UserID & "', LastUPD = GetDate() " &
                    "where NoPackingList = '" & Trim(NoPackingList.Text) & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            Me.Cursor = Cursors.WaitCursor
            MsgSQL = "Select * from t_DPL " &
                "Where NoDPL = '" & txtNoDPL.Text & "' " &
                " And AktifYN = 'Y' order by idrec "
            Rs = Proses.ExecuteQuery(MsgSQL)
            For i = 0 To Rs.Rows.Count - 1
                Application.DoEvents()
                'If Rs.rows!KodeProduk = "0229-20-4000A" Then
                '       Debug.Print Rs!KodeProduk
                'End If

                MsgSQL = "Select * from t_PI " &
                    "WHere NoPO = '" & Rs.Rows(i) !NoPO & "' " &
                    "  AND Kode_Produk = '" & Rs.Rows(i) !KodeProduk & "' " &
                    " And AktifYN = 'Y' "
                RS05 = Proses.ExecuteQuery(MsgSQL)
                If RS05.Rows.Count <> 0 Then
                    HargaFOB.Text = RS05.Rows(0) !HargaFOB
                Else
                    MsgBox("PO NO : " & Rs.Rows(i) !NoPO &
                           " Kode Produk : " & Rs.Rows(i) !KodeProduk & " di PI tidak ada!" & vbCrLf & "Harga FOB NOL!!", vbCritical + vbOKOnly, ".:Warning!")
                    HargaFOB.Text = 0
                End If
                IDRec.Text = Proses.MaxNoUrut("IDRec", "t_PackingList", Format(TglPL.Value, "MM"))
                MsgSQL = "INSERT INTO t_PackingList(IdRec, NoPackingLIst, NoDPL, KirimLaut, KirimUdara, " &
                    "JPackingList, JInvoice, TglPL, TglKapal, NoBoks1, NoBoks2, JumlahBoks, " &
                    "JumlahTiapBoks, StatusPL, NoPI, Kode_Produk, Produk, QtyTiapBoks, MataUang, " &
                    "HargaFOB, Uang3Digit, NoPO, Kode_Importir, Importir, KodePImportir, CatatanPL, " &
                    "FotoLoc, TransferYN, AktifYN, UserID, LastUPD) VALUES('" & IDRec.Text & "', " &
                    "'" & Trim(NoPackingList.Text) & "', '" & txtNoDPL.Text & "', " & IIf(Rs.Rows(i) !CargoLaut = True, 1, 0) & ",  " &
                    "" & IIf(Rs.Rows(i) !CargoUdara = True, 1, 0) & ", " & IIf(OptPackingList.Checked, 1, 0) & ", " &
                    "" & IIf(OptInvoice.Checked, 1, 0) & " , '" & Format(TglPL.Value, "yyyy-MM-dd") & "', " &
                    "'" & Format(TglKapal.Value, "yyyy-MM-dd") & "', '" & Rs.Rows(i) !NoBoksAwal & "', " &
                    "'" & Rs.Rows(i) !NoBoksAkhir & "', " & Rs.Rows(i) !JumlahBoks & ", " & Rs.Rows(i) !TotalTiapBoks & ", " &
                    "'" & StatusPL.Text & "', '" & NoPackingList.Text & "', '" & Rs.Rows(i) !KodeProduk & "', " &
                    "'" & Rs.Rows(i) !Produk & "', " & Rs.Rows(i) !JmlTiapBoks & ", " &
                    "'" & cmbMataUang.Text & "', " & HargaFOB.Text * 1 & ",  " &
                    "" & IIf(chk3Digit.Checked, 1, 0) & ", '" & Rs.Rows(i) !NoPO & "', " &
                    "'" & Microsoft.VisualBasic.Left(Rs.Rows(i) !KodeImportir, 5) & "',  " &
                    "'" & Rs.Rows(i) !Importir & "', '" & Rs.Rows(i) !KodePImportir & "', " &
                    "'" & Trim(CatatanPackingList.Text) & "', '" & Trim(LocGmb1.Text) & "', " &
                    "'Y', 'N', '" & UserID & "', GetDate())"
                Proses.ExecuteNonQuery(MsgSQL)
            Next i
            Me.Cursor = Cursors.Default
            If LEdit Then
                MsgSQL = "Delete T_PackingList " &
                    "Where NoPackingList = '" & Trim(NoPackingList.Text) & "' " &
                    "  And AktifYN = 'N' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "Update T_PackingList Set TransferYN = 'N' " &
                "Where NoPackingList = '" & Trim(NoPackingList.Text) & "' " &
                "  And AktifYN = 'N' "
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            LAdd = False
            LEdit = False
            AturTombol(True)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        PanelNavigate.Enabled = False
        PanelExp2Excel.Enabled = False

        If Opt_Invoice.Checked = False And Opt_PackingList.Checked = False Then
            MsgBox("Jenis Packing List yang mau di export ke excel belum di pilih !", vbCritical + vbOKOnly, ".:Warning !")
            PanelNavigate.Enabled = True
            PanelExp2Excel.Enabled = True
            Exit Sub
        ElseIf Opt_Invoice.Checked = True And Opt_PackingList.Checked = False Then
            Form_Export2Excel.JenisTR.Text = "PL_Invoice"
            Form_Export2Excel.idRec.Text = NoPackingList.Text
        ElseIf Opt_Invoice.Checked = False And Opt_PackingList.Checked = True Then
            Form_Export2Excel.JenisTR.Text = "PL_PackingList"
            Form_Export2Excel.idRec.Text = NoPackingList.Text
        End If

        Form_Export2Excel.ShowDialog()
        PanelExp2Excel.Enabled = True
        PanelNavigate.Enabled = True

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        Dim RS As New DataTable
        CatatanPackingList.Text = "THE MERCHANDISE INCLUDED ON THIS INVOICE IS WHOLY THE GROWTH, OR THE MANUFACTURE OF INDONESIA"
        SQL = "SELECT GetDate() Tgl FROM T_PackingList "
        RS = Proses.ExecuteQuery(SQL)
        If RS.Rows.Count <> 0 Then
            TglPL.Value = RS.Rows(0) !tGL
            TglKapal.Value = RS.Rows(0) !tGL
        End If
        Me.Cursor = Cursors.WaitCursor
        DaftarDPL("")
        Me.Cursor = Cursors.Default
        If LAdd Or LEdit Then TglKapal.Focus()
    End Sub

    Private Sub DaftarDPL(Cari As String)
        Dim NoDPL As String = "", RS04 As New DataTable,
            mKondisi As String, MsgSQL As String

        Me.Cursor = Cursors.WaitCursor
        If Trim(Cari) = "" Then
            mKondisi = " "
        Else
            mKondisi = " And T_DPL.NoDPL like '%" & Trim(Cari) & "%' "
        End If

        MsgSQL = "Select Distinct NoDPL, tglDPL, max(T_DPL.NoPO) NOPO, T_DPL.Importir, RIGHT(T_DPL.NoDPL,2) + LEFT(T_DPL.NoDPL,3)  " &
            " From t_DPL INNER JOIN t_PO ON t_DPL.NoPO = t_PO.NoPO AND t_DPL.KodeProduk = t_PO.Kode_Produk INNER JOIN " &
            "      t_PI ON t_PO.NoPO = t_PI.NoPO AND t_PO.Kode_Produk = t_PI.Kode_Produk " &
            "Where T_DPL.AktifYN = 'Y' " & mKondisi & " " &
            "Group By NoDPL, TglDPL, T_DPL.Importir, RIGHT(T_DPL.NoDPL,2) + LEFT(T_DPL.NoDPL,3) " &
            "Order By TglDPL DESC  "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar DPL"
        Form_Daftar.ShowDialog()
        NoDPL = Trim(FrmMenuUtama.TSKeterangan.Text)

        MsgSQL = "Select T_DPL.NoBoksAwal, T_DPL.NoBoksAkhir, T_DPL.JumlahBoks, T_DPL.TotalTiapBoks, T_PI.NoPI, " &
            " T_DPL.KodeProduk, T_DPL.Produk, T_DPL.JmlTiapBoks, t_PI.HargaFOB, t_PI.Digit3YN, t_DPL.NoPO, " &
            " T_DPL.KodeImportir, T_DPL.Importir, NoDPL, t_PO.MataUang " &
            " From T_DPL inner join t_PO on t_DPL.NoPO = t_PO.NoPO " &
            "      Inner Join t_PI on t_PO.NoPO = t_PI.NoPO " &
            "Where NoDPL = '" & NoDPL & "' "
        RS04 = Proses.ExecuteQuery(MsgSQL)
        If RS04.Rows.Count <> 0 Then
            NoPackingList.Text = NoDPL
            NoBoks1.Text = RS04.Rows(0) !NoBoksAwal
            NoBoks2.Text = RS04.Rows(0) !NoBoksAkhir
            JumlahBoks.Text = RS04.Rows(0) !JumlahBoks
            JumlahTiapBoks.Text = RS04.Rows(0) !TotalTiapBoks
            NoPI.Text = RS04.Rows(0) !NoPI
            txtNoDPL.Text = RS04.Rows(0) !NoDPL
            Kode_Produk.Text = RS04.Rows(0) !KodeProduk
            Produk.Text = RS04.Rows(0) !Produk
            JumlahKodeTiapBoks.Text = RS04.Rows(0) !JmlTiapBoks
            HargaFOB.Text = Format(RS04.Rows(0) !HargaFOB, "###,##0")
            If RS04.Rows(0) !digit3yn = "N" Then
                chk3Digit.Checked = 0
            Else
                chk3Digit.Checked = 1
            End If
            NoPO.Text = RS04.Rows(0) !NoPO
            Kode_Importir.Text = RS04.Rows(0) !KodeImportir
            Importir.Text = RS04.Rows(0) !Importir
            cmbMataUang.Text = RS04.Rows(0) !MataUang
        End If
        NoPackingList.Text = Proses.MaxYNoUrut("NoPackingList", "t_PackingList", Format(TglPL.Value, "MM"))
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable

        Dim MsgSQL As String, TCetak As String, rsc As New DataTable
        Dim terbilang As String = "", tb As New Terbilang

        If Opt_PackingList.Checked = False And Opt_Invoice.Checked = False Then
            MsgBox("Jenis Packing list yang akan di cetak belom di pilih !", vbCritical + vbOKOnly, ".:Warning !")
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        Proses.OpenConn(CN)
        dttable = New DataTable

        MsgSQL = "Select isnull(Sum(JumlahBoks * JumlahTiapBoks * HargaFOB), 0) SubTotal " &
            " From t_PackingList " &
            "Where t_PackingList.NoPackingList = '" & NoPackingList.Text & "' "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count <> 0 Then
            terbilang = " " & tb.Terbilang(CDbl(rsc.Rows(0) !SubTotal)) & " "
        End If
        Proses.CloseConn()
        TCetak = "Jakarta, " & Proses.TglIndo(Format(TglPL.Value, "dd MMNN yyyy"))


        If Opt_PackingList.Checked = True Then
            MsgSQL = "SELECT t_PackingList.NoBoks1, t_PackingList.NoBoks2, " &
                "t_PackingList.JumlahBoks, t_PackingList.JumlahTiapBoks,  " &
                "t_PackingList.NoPI, t_PackingList.Kode_Produk, t_PackingList.QtyTiapBoks,  " &
                "t_PackingList.NoPO, t_PackingList.KodePImportir, m_KodeProduk.descript,  " &
                "m_KodeBahan.NamaInggris, NoPackingList, CatatanPL, m_KodeImportir.Nama, m_KodeImportir.Alamat   " &
                "FROM Pekerti.dbo.t_PackingList t_PackingList " &
                "   INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
                "   t_PackingList.Kode_Importir = m_KodeImportir.KodeImportir  " &
                "   INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON  " &
                "   t_PackingList.Kode_Produk = m_KodeProduk.KodeProduk  " &
                "   INNER JOIN Pekerti.dbo.m_KodeBahan m_KodeBahan ON  " &
                "m_KodeProduk.Kode_Bahan = m_KodeBahan.KodeBahan  " &
                "Where t_PackingList.NoPackingList = '" & NoPackingList.Text & "' " &
                "  And t_PackingList.aktifYN = 'Y' " &
                "Order By right('0000000000'+noboks1,10), right('0000000000' + noboks2, 10), idrec  "
        ElseIf Opt_Invoice.Checked = True Then
            MsgSQL = "select t_PI.IdRec as IDPI, t_PackingList.IdRec as IDPL, " &
                    "t_PackingList.Kode_Produk, NoPackingList " &
                    " From t_PI inner join t_PackingList on t_PackingList.NoPI = t_PI.NoPI " &
                    "     And t_PI.Kode_Produk = t_PackingList.Kode_produk " &
                    "Where t_PackingList.NoPI = '" & NoPI.Text & "' " &
                    "  and NoPackingList = '" & NoPackingList.Text & "' " &
                    "  and T_PackingList.AktifYN = 'Y' " &
                    "  and t_PI.AktifYN = 'Y' " &
                    "Order By t_PI.IdRec "
            rsc = Proses.ExecuteQuery(MsgSQL)
            For i = 0 To rsc.Rows.Count - 1
                Application.DoEvents()
                MsgSQL = "Update T_PackingList Set FotoLoc = '" & rsc.Rows(i) !idpi & "' " &
                        "Where IdRec = '" & rsc.Rows(i) !idpl & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next i

            MsgSQL = "SELECT t_PackingList.NoPackingList, t_PackingList.NoPI, t_PackingList.Kode_Produk, " &
                "     t_PackingList.QtyTiapBoks, t_PackingList.HargaFOB, t_PackingList.KodePImportir, " &
                "     t_PackingList.CatatanPL, m_KodeProduk.Descript, t_PackingList.NoPO, " &
                "     t_PackingList.jumlahboks, t_PackingList.JumlahTiapBoks, m_KodeImportir.Nama, m_KodeImportir.Alamat " &
                "FROM Pekerti.dbo.t_PackingList t_PackingList INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
                "     t_PackingList.Kode_Importir = m_KodeImportir.KodeImportir  " &
                "     INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON t_PackingList.Kode_Produk = m_KodeProduk.KodeProduk  " &
                "Where t_PackingList.NoPackingList = '" & NoPackingList.Text & "' " &
                "  And t_PackingList.aktifYN = 'Y' " &
                "Order By t_PackingList.NoPO, t_PackingList.FotoLoc, t_PackingList.Kode_Produk, right('0000000000'+noboks1,10), right('0000000000' + noboks2, 10) "
        End If
        DTadapter = New SqlDataAdapter(MsgSQL, CN)
        Try
            DTadapter.Fill(dttable)
            If Opt_PackingList.Checked Then
                objRep = New Rpt_PackingList
            ElseIf Opt_Invoice.Checked = True Then
                objRep = New Rpt_PackingList_INV
                'objRep.SetDataSource(dttable)
                'objRep.SetParameterValue("terbilang", terbilang)
            End If
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("TANGGAL", TCetak)
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

    Private Sub tNoPO_TextChanged(sender As Object, e As EventArgs) Handles tNoPO.TextChanged

    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        LTambahKode = False
        AturTombol(True)
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click

        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_PACKINGLIST " &
            "Where AktifYN = 'Y' " &
            " And NoPackingList = '" & NoPackingList.Text & "' " &
            "ORDER BY tglpl, IDRec "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            tNoPO.Text = ""
            tKodeBrg.Text = ""
            IDRec.Text = RSNav.Rows(0) !IdRec
            Call IsiPL(IDRec.Text)
        End If

    End Sub

    Private Sub btnButtom_Click(sender As Object, e As EventArgs) Handles btnButtom.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_PACKINGLIST " &
            "Where AktifYN = 'Y' " &
            " And NoPackingList = '" & NoPackingList.Text & "' " &
            "ORDER BY tglpl DESC, IDRec DESC "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            tNoPO.Text = ""
            tKodeBrg.Text = ""
            IDRec.Text = RSNav.Rows(0) !IdRec
            Call IsiPL(IDRec.Text)
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_PACKINGLIST " &
            "Where AktifYN = 'Y' " &
            "  And IDRec < '" & IDRec.Text & "' " &
            "  And NOPACKINGLIST = '" & NoPackingList.Text & "' " &
            "ORDER BY tglpl desc, IDRec desc "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            tNoPO.Text = ""
            tKodeBrg.Text = ""
            IDRec.Text = RSNav.Rows(0) !IdRec
            Call IsiPL(IDRec.Text)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim MsgSQL As String, RSNav As New DataTable
        MsgSQL = "SELECT top 1 IDrec From T_PACKINGLIST " &
            "Where AktifYN = 'Y' " &
            "  And IDRec > '" & IDRec.Text & "' " &
            "  And NOPACKINGLIST = '" & NoPackingList.Text & "' " &
            "ORDER BY tglpl, IDRec  "
        RSNav = Proses.ExecuteQuery(MsgSQL)
        If RSNav.Rows.Count <> 0 Then
            tNoPO.Text = ""
            tKodeBrg.Text = ""
            IDRec.Text = RSNav.Rows(0) !IdRec
            Call IsiPL(IDRec.Text)
        End If
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If MsgBox("Hapus data ini?", vbCritical + vbYesNo, "Confirm!") = vbYes Then
            MsgSQL = "Delete t_PackingList Where NoPackingList = '" & Trim(NoPackingList.Text) & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            ClearTextBoxes()
        End If
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(IDRec.Text) = "" Then
            MsgBox("Data yang akan di edit belum di pilih!", vbCritical, ".:Empty Data!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        LTambahKode = False
        AturTombol(False)
    End Sub

    Private Sub Form_GD_PackingListInvoice_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String
        LAdd = False
        LEdit = False
        TabControl1.SelectedTab = TabPageFormEntry_
        SetDataGrid()
        UserID = FrmMenuUtama.TsPengguna.Text
        Dim Rs As New DataTable
        ClearTextBoxes()
        cmbMataUang.Items.Clear()

        MsgSQL = "Select * from m_Currency Where AktifYN = 'Y'"
        Rs = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To Rs.Rows.Count - 1
            Application.DoEvents()
            cmbMataUang.Items.Add(Rs.Rows(i) !Kode)
        Next i
        MsgSQL = "Select Top 1 * From t_PackingList " &
            "where AktifYN = 'Y' " &
            "Order By TglPL Desc, IdRec desc "
        Rs = Proses.ExecuteQuery(MsgSQL)
        If Rs.Rows.Count <> 0 Then
            IDRec.Text = Rs.Rows(0) !IDRec
        Else
            IDRec.Text = ""
        End If
        Call IsiPL(IDRec.Text)
        tTambah = Proses.UserAksesTombol(UserID, "55_PACKING_LIST_INV", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "55_PACKING_LIST_INV", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "55_PACKING_LIST_INV", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "55_PACKING_LIST_INV", "laporan")
        AturTombol(True)
        Me.Cursor = Cursors.Default
        DaftarPL()
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
        TglPL.Value = Now
        TglKapal.Value = Now
        OptPackingList.Checked = False
        OptInvoice.Checked = False
        OptLaut.Checked = False
        OptUdara.Checked = False
        OptInvoice.Checked = False
        Opt_PackingList.Checked = False
        Opt_Invoice.Checked = False
        cmbMataUang.SelectedItem = -1
        chk3Digit.Checked = False
        ShowFoto("")
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
        PanelNavigate.Visible = tAktif
        cmdExit.Visible = tAktif
        TabPageDaftar_.Enabled = True
        TabPageFormEntry_.Enabled = True
        PanelExp2Excel.Visible = tAktif

        PanelJenisPI.Enabled = Not tAktif
        NoPackingList.ReadOnly = tAktif
        PanelMetodKirim.Enabled = Not tAktif


        TglPL.Enabled = Not tAktif
        txtNoDPL.ReadOnly = tAktif
        TglKapal.Enabled = Not tAktif
        NoBoks1.ReadOnly = tAktif
        NoBoks2.ReadOnly = tAktif
        JumlahBoks.ReadOnly = tAktif
        JumlahTiapBoks.ReadOnly = tAktif
        StatusPL.ReadOnly = tAktif
        NoPI.ReadOnly = tAktif
        Kode_Produk.ReadOnly = tAktif
        Produk.ReadOnly = True
        JumlahKodeTiapBoks.ReadOnly = tAktif
        cmbMataUang.Enabled = Not tAktif
        HargaFOB.ReadOnly = tAktif
        chk3Digit.Enabled = Not tAktif
        NoPO.ReadOnly = tAktif
        Kode_Importir.ReadOnly = tAktif
        Importir.ReadOnly = tAktif
        KodePImportir.ReadOnly = tAktif
        CatatanPackingList.ReadOnly = tAktif
        Me.Text = "Packing List Invoice"
    End Sub
    Private Sub IsiPL(tIdRec)
        Dim rs05 As New DataTable
        MsgSQL = "Select * From t_PackingList " &
        "Where AktifYN = 'Y' " &
        " AND idrec = '" & IDRec.Text & "' "
        rs05 = Proses.ExecuteQuery(MsgSQL)
        If rs05.Rows.Count <> 0 Then
            IDRec.Text = rs05.Rows(0) !IdRec
            NoPackingList.Text = rs05.Rows(0) !NoPackingList
            OptLaut.Checked = rs05.Rows(0) !KirimLaut
            OptUdara.Checked = rs05.Rows(0) !KirimUdara
            OptPackingList.Checked = rs05.Rows(0) !JPackingList
            OptInvoice.Checked = rs05.Rows(0) !JInvoice
            TglPL.Value = rs05.Rows(0) !TglPL
            txtNoDPL.Text = rs05.Rows(0) !NoDPL
            TglKapal.Value = rs05.Rows(0) !TglKapal
            NoBoks1.Text = rs05.Rows(0) !NoBoks1
            NoBoks2.Text = rs05.Rows(0) !NoBoks2
            JumlahBoks.Text = rs05.Rows(0) !JumlahBoks
            JumlahTiapBoks.Text = rs05.Rows(0) !JumlahTiapBoks
            StatusPL.Text = rs05.Rows(0) !StatusPL
            NoPI.Text = rs05.Rows(0) !NoPI
            Kode_Produk.Text = rs05.Rows(0) !Kode_Produk
            Produk.Text = rs05.Rows(0) !Produk
            JumlahKodeTiapBoks.Text = rs05.Rows(0) !QtyTiapBoks
            If rs05.Rows(0) !MataUang = "" Then
                cmbMataUang.SelectedIndex = -1
            Else
                cmbMataUang.Text = rs05.Rows(0) !MataUang
            End If
            HargaFOB.Text = rs05.Rows(0) !HargaFOB
            chk3Digit.Checked = IIf(rs05.Rows(0) !Uang3Digit, 1, 0)
            NoPO.Text = rs05.Rows(0) !NoPO
            Kode_Importir.Text = rs05.Rows(0) !Kode_Importir
            Importir.Text = rs05.Rows(0) !Importir
            KodePImportir.Text = rs05.Rows(0) !KodePImportir
            CatatanPackingList.Text = rs05.Rows(0) !CatatanPL
            LocGmb1.Text = Trim(Kode_Produk.Text) + ".jpg"
            If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                ShowFoto("")
            Else
                ShowFoto(LocGmb1.Text)
            End If
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

    Private Sub DaftarPL()
        Dim RS05 As New DataTable, mKondisi As String
        DGView.Rows.Clear()
        DGView2.Rows.Clear()
        DGView.Visible = False
        mKondisi = ""
        JumProd.Text = ""
        TProd.Text = ""
        Harga.Text = ""
        If Trim(tNoPO.Text) <> "" Then
            mKondisi = " And NoPO like '%" & tNoPO.Text & "%' "
        ElseIf Trim(tKodeBrg.Text) <> "" Then
            mKondisi = " And Kode_Produk like '%" & tKodeBrg.Text & "%' "
        End If
        MsgSQL = "SELECT NoPackingList, TglPL,  MIN(NoPI) NoPI, TglKapal, Importir " &
            " FROM t_PackingList " &
            "Where AktifYN = 'Y' " & mKondisi & " " &
            "Group By NoPackingList, TglPL, TglKapal, Importir " &
            "Order By TglPL Desc, right(NoPackingList ,2) + left(nopackinglist,3) Desc "
        RS05 = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To RS05.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(RS05.Rows(i) !NoPackingList,
                   Format(RS05.Rows(i) !TglPL, "dd-MM-yyyy"),
                   RS05.Rows(i) !NoPI,
                   Format(RS05.Rows(i) !TglKapal, "dd-MM-yyyy"),
                   RS05.Rows(i) !Importir)
        Next i
        Proses.CloseConn()
        DGView.Visible = True
    End Sub

    Private Sub DGView_Click(sender As Object, e As EventArgs) Handles DGView.Click

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tCari As String, RSL As New DataTable,
            HargaFOB As Double = 0, curr As String = ""
        DGView2.Rows.Clear()
        DGView2.Visible = False
        tCari = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value

        'CekDouble tCari

        MsgSQL = "Select * From t_PackingList " &
            "Where NoPackingList = '" & tCari & "' and aktifYN = 'Y' " &
            "Order By right('0000000000'+noboks1,10), right('0000000000' + noboks2, 10) "
        RSL = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RSL.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSL.Rows(a) !idrec,
                    RSL.Rows(a) !Kode_Produk,
                    RSL.Rows(a) !NoBoks1, RSL.Rows(a) !NoBoks2,
                    Format(RSL.Rows(a) !JumlahBoks, "###,##0"),
                    Format(RSL.Rows(a) !JumlahTiapBoks, "###,##0"),
                    RSL.Rows(a) !NoPO,
                    RSL.Rows(a) !KodePImportir,
                    Format(RSL.Rows(a) !HargaFOB, "###,##0.00"))
            curr = RSL.Rows(a) !MataUang
        Next a

        '"Total Nilai Pesanan ( Rp.)  :"
        Label10.Text = "Total Nilai Pesanan (" & curr & ") :"
        MsgSQL = "Select isnull(Sum(hargaFOB * qtytiapboks * jumlahboks),0) Harga, " &
            "     isnull(sum(qtytiapboks * jumlahboks),0) TProd, isnull(count(kode_produk),0) JProd " &
            "From t_PackingList " &
            "Where nopackinglist = '" & tCari & "' "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        If dbTable.Rows.Count <> 0 Then
            Harga.Text = Format(dbTable.Rows(0) !Harga, "###,##0.00")
            TProd.Text = Format(dbTable.Rows(0) !TProd, "###,##0")
            JumProd.Text = Format(dbTable.Rows(0) !jprod, "###,##0")
        End If

        If DGView2.Rows.Count <> 0 Then
            DGView2_CellClick(sender, e)
        End If
        Proses.CloseConn()
        DGView2.Visible = True
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        If DGView2.Rows.Count = 0 Then Exit Sub
        IDRec.Text = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        IsiPL(IDRec.Text)
    End Sub

    Private Sub tNoPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tNoPO.KeyPress
        If e.KeyChar = Chr(13) Then
            DaftarPL()
        End If
    End Sub
End Class