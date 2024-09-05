Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Analisa_01Pemasaran
    Dim SQL As String
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Dim UserID As String = FrmMenuUtama.TsPengguna.Text
    Protected CN As SqlConnection
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database

    Private Sub Analisa_01Pemasaran_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmbJenisLaporan.Items.Clear()
        cmbJenisLaporan.Items.Add("1." + Space(3) + "10 Besar pesanan buyer berdasarkan PI")
        cmbJenisLaporan.Items.Add("2." + Space(3) + "5 Besar penjualan produk (QTY) PI")
        cmbJenisLaporan.Items.Add("3." + Space(3) + "Penjualan berdasarkan fungsi produk")
        cmbJenisLaporan.Items.Add("4." + Space(3) + "Perbandingan order buyer per periode")
        cmbJenisLaporan.Items.Add("5." + Space(3) + "Keterlambatan pengiriam PO")
        cmbJenisLaporan.Items.Add("6." + Space(3) + "Turun PO per-bulan")
        cmbJenisLaporan.Items.Add("7." + Space(3) + "Perbandingan per tahun antara PI dan Invoice")
        cmbJenisLaporan.Items.Add("8." + Space(3) + "Perbandingan pembelian buyer berdasar invoice")
        cmbJenisLaporan.Items.Add("9." + Space(3) + "Daftar Invoice Per-Buyer")
        cmbJenisLaporan.Items.Add("10." + Space(1) + "Daftar P I  Per-Buyer")
        cmbJenisLaporan.SelectedIndex = 9

    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        Panel1.Enabled = False
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
        If Mid(cmbJenisLaporan.Text, 1, 2) = "10" Then
            RPT_DaftarPIPerBuyer
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "1" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "2" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "3" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "4" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "5" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "6" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "7" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "8" Then
        ElseIf Mid(cmbJenisLaporan.Text, 1, 1) = "9" Then

        End If

        'If Left(cmbJenisLaporan.Text, 2) = "10" Then
        '    RPT_DaftarPIPerBuyer
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "1" Then
        '    RptPemasaranTopTen
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "2" Then
        '    RptPemasaranTopFive
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "3" Then
        '    RptPemasaranFungsi
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "4" Then
        '    CekTablePIPL
        '    Rpt_PI_PL
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "5" Then
        '    CekTablePITelat
        '    Rpt_PITelat
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "6" Then
        '    If Trim(Target.Text) = "" Or Trim(Target.Text) = "0" Then
        '        MsgBox "Target belum di isi/tidak boleh nol!", vbCritical, ".:Warning!"
        '    cmdOK.Enabled = True
        '        cmdBatal.Enabled = True
        '        Me.MousePointer = 0
        '        Exit Sub
        '    End If
        '    If Trim(Kurs.Text) = "" Or Trim(Kurs.Text) = "0" Then
        '        MsgBox "Kurs belum di isi/tidak boleh nol!", vbCritical, ".:Warning!"
        '    cmdOK.Enabled = True
        '        cmdBatal.Enabled = True
        '        Me.MousePointer = 0
        '        Exit Sub
        '    End If
        '    CekTablePIPL
        '    Rpt_PIPerBulan
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "7" Then
        '    CekTablePIPL
        '    Rpt_PI_PL_Tahun
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "8" Then
        '    RPT_PembelianBuyer
        'ElseIf Left(cmbJenisLaporan.Text, 1) = "9" Then
        '    RPT_DaftarInvoicePerBuyer
        'End If

        Panel1.Enabled = True
    End Sub


    Private Sub RPT_DaftarPIPerBuyer()
        Dim MsgSQL As String, tNilaiPI As Double, SQL As String, Konv As Double

        '    MsgSQL = "SELECT t_PI.NoPI, t_PI.NoPO, t_PI.Kode_Importir, MataUang, " &
        '        "t_PI.Importir, t_PI.Kode_Produk , t_PI.Jumlah, t_PI.HargaFOB " &
        '        "FROM Pekerti.dbo.t_PI t_PI " &
        '        "Where AktifYN = 'Y' " &
        '        "  And year(ShipmentDateKode) = " & Year(Tgl1.Value) & " " &
        '        "ORDER BY t_PI.Importir ASC, right(t_PI.NoPI,2) + Left(NoPI,3) ASC, t_PI.Kode_Produk "
        '    SQL = MsgSQL
        '    RS05.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        'Do While Not RS05.EOF
        '        DoEvents
        '        If RS05!MataUang = "USD" Then
        '            tNilaiPI = RS05!Jumlah * RS05!HargaFOB
        '        ElseIf RS05!MataUang = "EURO" Then
        '            MsgSQL = "Select PembagiEuro From t_PO " &
        '                "where NOPO = '" & RS05!NoPO & "' " &
        '                "  and aktifYN = 'Y' and Kode_Produk = '" & RS05!Kode_Produk & "' "
        '            RS04.Open MsgSQL, ConnSQL, adOpenForwardOnly, adLockReadOnly
        '        If Not RS04.EOF Then
        '                Konv = RS04!PembagiEuro
        '            Else
        '                Konv = 1.375
        '            End If
        '            RS04.Close
        '            tNilaiPI = RS05!Jumlah * RS05!HargaFOB * Konv
        '        End If
        '        MsgSQL = "Update t_PI set NilaiPI = " & tNilaiPI & " " &
        '            "Where NoPI ='" & RS05!NoPI & "' " &
        '            "  And Kode_produk =  '" & RS05!Kode_Produk & "' "
        '        ConnSQL.Execute MsgSQL
        '    RS05.MoveNext
        '    Loop
        '    RS05.Close
        '    With CrIns
        '        .Reset
        '        .LogOnServer "PDSODBC.DLL", "DBPEKERTI", "PEKERTI", "USER01", pwd
        '    .SQLQuery = SQL
        '        .ReportFileName = Left(RptLoc, Len(Trim(RptLoc)) - 1) & "\Rpt_AnalisaDaftarPI.rpt"
        '        .Formulas(1) = "periode= 'Periode " & Format(Tgl1.Value, "YYYY") & "' "
        '        .WindowState = crptMaximized
        '        .WindowShowSearchBtn = True
        '        .WindowShowCloseBtn = True
        '        .WindowShowNavigationCtls = True
        '        .WindowShowProgressCtls = True
        '        .WindowShowPrintSetupBtn = True
        '        .WindowShowPrintBtn = True
        '        .WindowAllowDrillDown = True
        '        .Destination = crptToWindow
        '        .Action = 1
        '    End With


        Dim Proses As New ClsKoneksi
        Dim Periode As String = "", mKondisi As String = ""
        cmdCetak.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        If Format(Tgl1.Value, "yyMMdd") = Format(Tgl2.Value, "yyMMdd") Then
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy")
        Else
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy") + " s.d " +
                      Format(Tgl2.Value, "dd MMM yyyy")
        End If
        If cmbJenisLaporan.Text <> "" Then
            mKondisi = " AND JenisJurnal = '" & cmbJenisLaporan.Text & "' "
        End If
        Call OpenConn()
        dttable = New DataTable
        'SQL = "SELECT * FROM T_JURNAL " &
        '    "WHERE AKTIFYN = 'Y' " &
        '    " AND Convert(varchar(8), tanggal, 112) Between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
        '     "      And '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
        '     " " & mKondisi & " " &
        '    "ORDER BY tanggal, idrec, NoUrut "

        SQL = "SELECT t_PI.NoPI, t_PI.NoPO, t_PI.tglPI, t_PI.Kode_Importir, MataUang, " &
            "     t_PI.Importir, t_PI.Kode_Produk , t_PI.Jumlah, t_PI.HargaFOB, nilaiPI " &
            "From Pekerti.dbo.t_PI t_PI " &
            "WHERE AktifYN = 'Y' " &
            "  AND Convert(varchar(8), ShipmentDateKode, 112) Between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
            "      And '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
            "ORDER BY t_PI.Importir ASC, right(t_PI.NoPI,2) + Left(NoPI,3) ASC, idrec, t_PI.Kode_Produk "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_AnalisaDaftarPI
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Periode", Periode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            'CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
            CrystalReportViewer1.Refresh()
            CrystalReportViewer1.ReportSource = objRep
            CrystalReportViewer1.Zoom(1)
            dttable.Dispose()
            DTadapter.Dispose()
            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
        Me.Cursor = Cursors.Default
        cmdCetak.Enabled = True
    End Sub

    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
    End Sub

    Private Function OpenConn() As Boolean
        CN = New SqlConnection

        SQL = "Initial Catalog=" & db & "; " &
            "user id=" & dbUserId & ";password=" & pwd & "; " &
            "Persist Security Info=True;" &
            "Data Source=" & ipserver & ";"

        CN.ConnectionString = SQL

        Try
            If CN.State = ConnectionState.Closed Then
                CN.Open()
                Return True
            Else
                CN.Close()
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try
    End Function

End Class