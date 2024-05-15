Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Form_SaldoAwal
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter

    Dim objRep As New ReportDocument

    Private Sub tPeriode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tPeriode.SelectedIndexChanged
        Dim RS02 As New DataTable
        Me.Cursor = Cursors.WaitCursor
        PanelTombol.Enabled = False
        SQL = "Select IdRec From M_SaldoAwalCompany " &
            "where Periode = '" & tPeriode.Text & "' " &
            "  And AktifYN = 'Y' "
        RS02 = Proses.ExecuteQuery(SQL)
        If RS02.Rows.Count <> 0 Then
            IDRec.Text = RS02.Rows(0) !idrec
            IsiSaldoAwal()
        Else
            IDRec.Text = ""
            IsiKodeGL()
        End If
        Me.Cursor = Cursors.Default
        PanelTombol.Enabled = True
    End Sub
    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdEdit.Enabled = tAktif
        cmdHapus.Enabled = tAktif
        cmdSimpan.Visible = Not tAktif
        cmdBatal.Visible = Not tAktif
        cmdPrint.Enabled = tAktif
        cmdExit.Visible = tAktif
        cmdImport.Visible = tAktif

        DGView.ReadOnly = tAktif
        DGView.Columns(0).ReadOnly = True
        DGView.Columns(1).ReadOnly = True
        DGView.Columns(2).ReadOnly = tAktif



    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If tPeriode.Text = "" Then
            MsgBox("Periode Saldo Belum di Pilih !", vbCritical + vbOKOnly, ".:Warning !")
            tPeriode.Focus()
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        DGView.Focus()
        MoveCell(0, 2)
    End Sub

    Sub MoveCell(ByVal crow As Integer, ByVal cCol As Integer)
        If crow <= 0 Then crow = 0
        DGView.CurrentCell.Selected = False
        If crow = DGView.RowCount Then
            DGView.Rows.Add()
        End If
        DGView.Rows(crow).Cells(cCol).Selected = True
        DGView.CurrentCell = DGView.SelectedCells(0)
        DGView.Refresh()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim tNO As String = "", KodeGL As String = "", NamaGL As String = "", saldo As Double = 0, tID As String = ""
        If LEdit Then
            DGView.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            IDRec.Text = ""
            IDRec.Visible = True
            PanelTombol.Enabled = False
            For i As Integer = 0 To DGView.Rows.Count - 1
                Application.DoEvents()
                If i = DGView.Rows.Count Then Exit For
                If Trim(DGView.Rows(i).Cells(0).Value) = "" Then Exit For

                tNO = Microsoft.VisualBasic.Right(101 + i, 2)

                If Not Information.IsNumeric(DGView.Rows(i).Cells(2).Value) Then
                    saldo = 0
                Else
                    saldo = DGView.Rows(i).Cells(2).Value * 1
                End If
                KodeGL = Trim(DGView.Rows(i).Cells(0).Value)
                NamaGL = Trim(DGView.Rows(i).Cells(1).Value)
                SQL = "Select RIGHT(isnull(Max(right(IDRec,6)),0) + 100000001,6) IDRec " &
                    " From m_SaldoAwalCompany " &
                    "Where left(idrec,4) = '" & Mid(tPeriode.Text, 4, 4) & "' and area = '" & FrmMenuUtama.Kode_Toko.Text & "' "
                IDRec.Text = Mid(tPeriode.Text, 4, 4) + Proses.ExecuteSingleStrQuery(SQL)

                SQL = "Select IDRec From m_SaldoAwalCompany " &
                    "where Periode = '" & tPeriode.Text & "' " &
                    "  And COA = '" & Trim(KodeGL) & "' " &
                    "  And AktifYN = 'Y' "
                tID = Proses.ExecuteSingleStrQuery(SQL)
                If Trim(tID) = "" Then
                    SQL = "INSERT INTO m_SaldoAwalCompany(IDRec, Periode, " &
                        "COA, Nama, Saldo, Area, AktifYN, LastUPD, UserId) " &
                        "VALUES ('" & IDRec.Text & "', " &
                        " '" & tPeriode.Text & "', '" & Trim(KodeGL) & "', " &
                        " '" & Trim(Replace(NamaGL, "'", "`")) & "', " & saldo & ", " &
                        " '" & FrmMenuUtama.Kode_Toko.Text & "', " &
                        " 'Y', GetDate(), '" & UserID & "') "
                Else
                    IDRec.Text = "#" + tID
                    SQL = "Update m_SaldoAwalCompany Set " &
                        "Saldo = " & saldo & ",  " &
                        " UserId = '" & UserID & "', " &
                        "LastUPD = getdate() " &
                        "where Periode = '" & tPeriode.Text & "' " &
                        "  And COA = '" & Trim(KodeGL) & "' " &
                        "  And AktifYN = 'Y' "
                End If
                Proses.ExecuteNonQuery(SQL)
                tCari.Text = Format(i, "###,##0") & " / " & Format(DGView.Rows.Count - 1, "###,##0")
            Next i
            tCari.Text = ""
            Me.Cursor = Cursors.Default
            DGView.Enabled = True
        End If
        IDRec.Text = ""
        PanelTombol.Enabled = True
        IDRec.Visible = False
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub btnSimpanSetup_Click(sender As Object, e As EventArgs) Handles btnSimpanSetup.Click
        SQL = "DELETE m_Periode where right(Periode,4) = '" & Format(PeriodeSaldo.Value, "yyyy") & "'"
        Proses.ExecuteNonQuery(SQL)
        For i = 1 To 12
            SQL = "INSERT INTO m_Periode (Periode) values ('" & Mid(Format(i + 100, "##0"), 2, 2) & "-" & Format(PeriodeSaldo.Value, "yyyy") & "' )"
            Proses.ExecuteNonQuery(SQL)
            tPeriode.Items.Add(Mid(Format(i + 100, "##0"), 2, 2) & "-" & Format(PeriodeSaldo.Value, "yyyy"))
        Next i
        PanelSetupPeriode.Visible = False
        PanelTombol.Enabled = True
        tPeriode.SelectedIndex = -1

    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If tPeriode.Text = "" Then
            MsgBox("Periode Saldo Belum di Pilih !", vbCritical + vbOKOnly, ".:Warning !")
            tPeriode.Focus()
            Exit Sub
        End If

        If MsgBox("Yakin anda hapus Saldo Akhir periode " & tPeriode.Text & " ?", vbYesNo + vbQuestion, ".:CONFIRM!") = vbYes Then
            MsgSQL = "DELETE m_SaldoAwalCompany  " &
                "where Periode = '" & tPeriode.Text & "' and area = '" & FrmMenuUtama.Kode_Toko.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            tPeriode.SelectedItem = -1
            DGView.Rows.Clear()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        PanelTombol.Enabled = False
        CetakSaldoAkhir
        PanelTombol.Enabled = True
    End Sub
    Private Sub CetakSaldoAkhir()
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim tExpedisi As String = "", mRevisi As String = "", tanggal As String = ""

        Me.Cursor = Cursors.WaitCursor

        Proses.OpenConn(CN)
        dttable = New DataTable
        SQL = "SELECT m_SaldoAwalCompany.IDRec, " &
            "      m_SaldoAwalCompany.Periode, m_SaldoAwalCompany.COA, " &
            "      m_SaldoAwalCompany.Nama , m_SaldoAwalCompany.Saldo " &
            " FROM Pekerti.dbo.m_SaldoAwalCompany m_SaldoAwalCompany " &
            "Where AktifYN = 'Y' " &
            "  And Periode = '" & tPeriode.Text & "' " &
            "ORDER BY Coa "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_SaldoAkhir
            objRep.SetDataSource(dttable)
            Form_Report.Text = "Cetak Saldo"
            Form_Report.CrystalReportViewer1.ShowExportButton = True
            Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
            Form_Report.CrystalReportViewer1.Refresh()
            Form_Report.CrystalReportViewer1.ReportSource = objRep
            Form_Report.CrystalReportViewer1.ShowRefreshButton = True
            Form_Report.CrystalReportViewer1.ShowPrintButton = True
            Form_Report.CrystalReportViewer1.ShowGroupTreeButton = True
            Form_Report.CrystalReportViewer1.ShowParameterPanelButton = False
            Form_Report.CrystalReportViewer1.Zoom(1)
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

    Private Sub btnBatalSetup_Click(sender As Object, e As EventArgs) Handles btnBatalSetup.Click
        PanelSetupPeriode.Visible = False
        PanelTombol.Enabled = False
    End Sub

    Private Sub btnBatalEdit_Click(sender As Object, e As EventArgs) Handles btnBatalEdit.Click
        PanelEditSaldo.Visible = False
    End Sub

    Private Sub btnSimpanEdit_Click(sender As Object, e As EventArgs) Handles btnSimpanEdit.Click
        If eNilaiSaldo.Text = "" Then eNilaiSaldo.Text = 0
        SQL = "Update m_SaldoAwalCompany Set " &
            "Saldo = " & eNilaiSaldo.Text * 1 & ",  " &
            " UserId = '" & UserID & "', " &
            "LastUPD = getdate() " &
            "where Periode = '" & Trim(tPeriode.Text) & "' " &
            "  And COA = '" & Trim(eKodeGL.Text) & "' " &
            "  And AktifYN = 'Y' "
        Proses.ExecuteNonQuery(SQL)
        DGView.Rows(eIndex.Text).Cells(2).Value = eNilaiSaldo.Text
        eNilaiSaldo.Text = 0
        eKodeGL.Text = ""
        eNamaGL.Text = ""
        PanelEditSaldo.Visible = False
    End Sub

    Private Sub eNilaiSaldo_TextChanged(sender As Object, e As EventArgs) Handles eNilaiSaldo.TextChanged
        If Trim(eNilaiSaldo.Text) = "" Then eNilaiSaldo.Text = 0
        If IsNumeric(eNilaiSaldo.Text) Then
            Dim temp As Double = eNilaiSaldo.Text
            eNilaiSaldo.Text = Format(temp, "###,##0")
            eNilaiSaldo.SelectionStart = eNilaiSaldo.TextLength

        End If

    End Sub

    Private Sub Form_SaldoAwal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String, RSL As New DataTable
        Dim RS02 As New DataTable

        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        'DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView.Font = New Font("Arial", 12, FontStyle.Regular)
        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 35
        End With


        PanelEditSaldo.Visible = False
        PanelSetupPeriode.Visible = False
        PanelTombol.Enabled = True
        tPeriode.Items.Clear()
        DGView.Rows.Clear()
        PeriodeSaldo.Value = Format(Now, "MM-yyyy")
        IDRec.Visible = False
        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select * From M_Periode " &
            "WHERE Right(periode,4) + left(periode,2) = '" & Format(Now, "yyyyMM") & "' " &
            "Order By Right(periode,4) + left(periode,2) "
        RS02 = Proses.ExecuteQuery(MsgSQL)
        If RS02.Rows.Count = 0 Then
            PanelSetupPeriode.Visible = True
            PanelTombol.Enabled = False
        Else
            MsgSQL = "Select * From M_Periode " &
                "WHERE Right(periode,4) + left(periode,2) < '" & Format(Now, "yyyyMM") & "' " &
                "Order By Right(periode,4) + left(periode,2) "
            RS02 = Proses.ExecuteQuery(MsgSQL)
            For i = 0 To RS02.Rows.Count - 1
                Application.DoEvents()
                tPeriode.Items.Add(RS02.Rows(i) !Periode)
            Next i
            tPeriode.SelectedIndex = 0
        End If

        MsgSQL = "Select  Max(Right(Periode,4) + left(Periode,2)) Periode " &
            " From M_SaldoAwalCompany " &
            "Where AktifYN = 'Y' "
        RS02 = Proses.ExecuteQuery(MsgSQL)
        If RS02.Rows.Count <> 0 Then
            If IsDBNull(RS02.Rows(0) !Periode) Then
                tPeriode.SelectedIndex = -1
            Else
                tPeriode.Text = Microsoft.VisualBasic.Right(RS02.Rows(0) !Periode, 2) + "-" + Microsoft.VisualBasic.Left(RS02.Rows(0) !Periode, 4)
            End If
        End If
        DGView.Rows.Clear()
        MsgSQL = "Select IdRec From M_SaldoAwalCompany " &
            "where Periode = '" & tPeriode.Text & "' " &
            "  And AktifYN = 'Y' "
        RS02 = Proses.ExecuteQuery(MsgSQL)
        If RS02.Rows.Count <> 0 Then
            IDRec.Text = RS02.Rows(0) !idrec
            IsiSaldoAwal()
        Else
            IDRec.Text = ""
            'IsiKodeGL()
        End If

        UserID = FrmMenuUtama.TsPengguna.Text
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub IsiSaldoAwal()
        Dim MsgSQL As String, tIdRec As String = ""
        Me.Cursor = Cursors.WaitCursor

        MsgSQL = " select * From m_Perkiraan " &
            "where aktifYN = 'Y' " &
            "  And no_Perkiraan not in " &
            "      (Select coa " &
            "         From m_SaldoawalCompany inner join m_Perkiraan " &
            "              on coa = no_Perkiraan " &
            "        Where periode = '" & tPeriode.Text & "'  " &
            "          And m_SaldoAwalCompany.aktifYN = 'Y' ) "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            tCari.Text = Format(a, "###,##0") & " / " & Format(DGView.Rows.Count - 1, "###,##0")
            SQL = "Select IDRec From m_SaldoAwalCompany " &
                    "where Periode = '" & tPeriode.Text & "' " &
                    "  And COA = '" & Trim(dbTable.Rows(a) !no_PERKIRAAN) & "' " &
                    "  And AktifYN = 'Y' "
            tIdRec = Proses.ExecuteSingleStrQuery(SQL)
            If Trim(tIdRec) = "" Then
                MsgSQL = "Select isnull(Max(right(IDRec,6)),0) + 100000001 IDRec " &
                    " From m_SaldoAwalCompany Where left(idrec,4) = '" & Format(Now(), "YYYY") & "' "
                tIdRec = Proses.ExecuteSingleStrQuery(MsgSQL)
                MsgSQL = "INSERT INTO m_SaldoAwalCompany(IDRec, " &
                "Periode, COA, Nama, Saldo, AktifYN, LastUPD, UserId, " &
                "AREA) VALUES ('" & tIdRec & "', '" & tPeriode.Text & "', " &
                " '" & Trim(dbTable.Rows(a) !no_PERKIRAAN) & "', " &
                " '" & dbTable.Rows(a) !NM_PERKIRAAN & "', " &
                " 0, 'Y', GetDate(), '" & UserID & "', " &
                " '" & FrmMenuUtama.CompCode.Text & "')"
                Proses.ExecuteNonQuery(MsgSQL)
            Else

            End If
        Next (a)
        DGView.Rows.Clear()
        DGView.Visible = False


        MsgSQL = "Select * " &
            " From M_SaldoAwalCompany " &
            "Where AktifYN = 'Y'   " &
            "  And Periode = '" & tPeriode.Text & "' " &
            "  And Nama like '%" & tCari.Text & "%' " &
            "Order By COA "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            tCari.Text = Format(a, "###,##0") & " / " & Format(DGView.Rows.Count - 1, "###,##0")
            DGView.Rows.Add(dbTable.Rows(a) !COA,
                    dbTable.Rows(a) !Nama,
                    Format(dbTable.Rows(a) !Saldo, "###,##0"), "EDIT")
        Next (a)
        tCari.Text = ""
        DGView.Visible = True
        DGView.Columns(3).Visible = True
        Proses.CloseConn()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub IsiKodeGL()
        Dim MsgSQL As String, RS01 As New DataTable
        Me.Cursor = Cursors.WaitCursor
        DGView.Rows.Clear()
        DGView.Visible = False
        MsgSQL = "Select NO_PERKIRAAN, NM_PERKIRAAN " &
            "From M_PERKIRAAN " &
            "Where AktifYN = 'Y' " &
            "Order By NO_PERKIRAAN "
        RS01 = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To RS01.Rows.Count - 1
            Application.DoEvents()
            tCari.Text = Format(a, "###,##0") & " / " & Format(DGView.Rows.Count - 1, "###,##0")
            DGView.Rows.Add(RS01.Rows(a) !no_PERKIRAAN,
                    RS01.Rows(a) !NM_PERKIRAAN,
                    Format(0, "###,##0"))
        Next (a)
        tCari.Text = ""
        Proses.CloseConn()
        DGView.Visible = True
        Me.Cursor = Cursors.Default
        DGView.Columns(3).Visible = False
    End Sub

    Private Sub PeriodeSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PeriodeSaldo.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub

    Private Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
        OpenFileDialog1.Filter = "Excel Files (*.xls, *.xlsx)|*.xlsx; *.xls"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.DefaultExt() = "*.xlsx"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            tCari.Text = OpenFileDialog1.FileName
            cmdImport.Enabled = False
            ImportSaldo()
        Else
            tCari.Text = ""
        End If
    End Sub

    Private Sub importSaldo()
        Dim i As Integer
        Dim dbCOA As New DataTable, dbCek As New DataTable
        'Dim APP As New excel.Application
        'Dim worksheet As Excel.Worksheet
        'Dim workbook As Excel.Workbook

        Dim dbTable As New DataTable
        Dim oExcel As New Object
        Dim oBook As New Object
        Dim oSheet As New Object
        oExcel = CreateObject("Excel.Application")
        tCari.Enabled = False
        If Trim(tCari.Text) <> "" Then
            DGView.Rows.Clear()
            oBook = oExcel.Workbooks.Open(tCari.Text)
            oSheet = oBook.Sheets.Item(1)
            Me.Cursor = Cursors.WaitCursor
            Dim xlRange
            xlRange = oSheet.UsedRange

            Dim mPeriode As String,
                mKodeGL As String = "", mNamaGL As String = "",
                mSaldo As Double = 0
            cmdImport.Enabled = False
            For i = 1 To xlRange.Rows.Count
                Application.DoEvents()
                If i = 1 Then
                    Try
                        mPeriode = Trim(Mid(oSheet.Cells(i, 1).Value, 23))
                        tPeriode.Text = mPeriode
                    Catch ex As Exception
                        mPeriode = ""
                    End Try
                End If
                Try
                    mKodeGL = oSheet.Cells(i, 2).Value
                Catch ex As Exception
                    mKodeGL = ""
                End Try
                Try
                    mSaldo = oSheet.Cells(i, 4).Value
                Catch ex As Exception
                    mSaldo = 0
                End Try
                If i > 2 Then
                    SQL = " select * From m_Perkiraan " &
                        "where aktifYN = 'Y' " &
                        "  And no_Perkiraan = '" & mKodeGL & "' "
                    dbCOA = Proses.ExecuteQuery(SQL)
                    If dbCOA.Rows.Count <> 0 Then
                        mNamaGL = dbCOA.Rows(0) !NM_PERKIRAAN
                        SQL = "Select IDRec,Saldo From m_SaldoAwalCompany " &
                            "where Periode = '" & tPeriode.Text & "' " &
                            "  And COA = '" & Trim(mKodeGL) & "' " &
                            "  And AktifYN = 'Y' "
                        dbCek = Proses.ExecuteQuery(SQL)
                        If dbCek.Rows.Count <> 0 Then
                            SQL = "Update m_SaldoAwalCompany Set " &
                                "  Saldo = " & mSaldo & ",  " &
                                " UserId = '" & UserID & "', " &
                                "LastUPD = getdate() " &
                                "where Periode = '" & tPeriode.Text & "' " &
                                "  And COA = '" & Trim(mKodeGL) & "' " &
                                "  And AktifYN = 'Y' "
                        Else
                            SQL = "Select isnull(Max(right(IDRec,6)),0) + 100000001 IDRec " &
                                " From m_SaldoAwalCompany Where left(idrec,4) = '" & Format(Now(), "YYYY") & "' "
                            IDRec.Text = Proses.ExecuteSingleStrQuery(SQL)
                            SQL = "INSERT INTO m_SaldoAwalCompany(IDRec, Periode, " &
                               "COA, Nama, Saldo, Area, AktifYN, LastUPD, UserId) " &
                               "VALUES ('" & IDRec.Text & "', " &
                               " '" & tPeriode.Text & "', '" & Trim(mKodeGL) & "', " &
                               " '" & Trim(Replace(mNamaGL, "'", "`")) & "', " & mSaldo & ", " &
                               " '" & FrmMenuUtama.Kode_Toko.Text & "', " &
                               " 'Y', GetDate(), '" & UserID & "') "
                        End If
                        Proses.ExecuteNonQuery(SQL)
                        DGView.Rows.Add(mKodeGL,
                            mNamaGL,
                            Format(mSaldo, "###,##0"), "EDIT")
                    End If
                End If
                tCari.Text = Format(i, "###,##0") & " / " & Format(xlRange.Rows.Count, "###,##0")
            Next i
            oExcel.ActiveWorkbook.Close(False, tCari.Text)
            oExcel.Quit()
        End If
        MsgBox(Format(i, "###,##0") & " record's", vbOKOnly + vbInformation, ".:Success !")
        Me.Cursor = Cursors.Default
        tCari.Text = ""
        tCari.Enabled = True
        cmdImport.Enabled = True
    End Sub

    Private Sub DGView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellEndEdit
        Dim tID As String = ""
        Dim cCol As Integer = DGView.CurrentCell.ColumnIndex
        Dim cRow As Integer = DGView.CurrentCell.RowIndex
        Dim kodetoko As String = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)
        'Apply to certin columns        
        If LAdd = False And LEdit = False Then Exit Sub
        tID = UCase(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value)
        If cCol = 0 Then

        ElseIf cCol = 2 Then  'Nilai Saldo
            Dim mHarga As Double = 0
            If Not Information.IsNumeric(DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0 'String.Empty
                SendKeys.Send("{up}")
                mHarga = 0
                Exit Sub
            Else
                If Trim(DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = "" Then
                    mHarga = 0
                    DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    mHarga = DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                    DGView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(mHarga, "###,##0")
                End If
            End If
            'SendKeys.Send("{up}")
            'SendKeys.Send("{right}")
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If DGView.Rows.Count = 0 Then Exit Sub
        eIndex.Text = DGView.CurrentCell.RowIndex
        Dim tCOA As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value,
            tNamaGL As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value,
            tSaldo As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value
        If e.ColumnIndex = 3 Then 'Edit
            'MsgBox(tCOA & vbCrLf & tNamaGL & vbCrLf & tSaldo)
            eKodeGL.Text = tCOA
            eNamaGL.Text = tNamaGL
            eNilaiSaldo.Text = tSaldo
            PanelEditSaldo.Visible = True
            eNilaiSaldo.Focus()
        End If
    End Sub

    Private Sub eNilaiSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles eNilaiSaldo.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If eNilaiSaldo.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(eNilaiSaldo.Text) Then
                Dim temp As Double = eNilaiSaldo.Text
                eNilaiSaldo.Text = Format(temp, "###,##0")
                eNilaiSaldo.SelectionStart = eNilaiSaldo.TextLength
            Else
                eNilaiSaldo.Text = 0
            End If
            btnSimpanEdit.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub eNilaiSaldo_GotFocus(sender As Object, e As EventArgs) Handles eNilaiSaldo.GotFocus
        With eNilaiSaldo
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            IsiSaldoAwal()
        End If
    End Sub

    Private Sub tPeriode_KeyUp(sender As Object, e As KeyEventArgs) Handles tPeriode.KeyUp
        If e.KeyCode = Keys.Enter Then
            IsiSaldoAwal()
        End If
    End Sub
End Class
