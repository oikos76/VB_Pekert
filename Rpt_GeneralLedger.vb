Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Rpt_GeneralLedger

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

    Private Sub Rpt_GeneralLedger_Load(sender As Object, e As EventArgs) Handles Me.Load
        Tgl1.Value = DateAdd(DateInterval.Month, -3, Now)
        Tgl2.Value = Now
        cmbJenisLaporan.Items.Clear()
        cmbJenisLaporan.Items.Add("1. BUKU BESAR")
        cmbJenisLaporan.Items.Add("2. Per Kode GL")
        AccCode1.Text = ""
        KetAccCode1.Text = ""
        AccCode1.Visible = False
    End Sub

    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
    End Sub


    Private Sub Ledger()
        Dim Proses As New ClsKoneksi, RS05 As DataTable
        cmdCetak.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        '--- Start here
        Dim Periode As String = "", MsgSQL As String, title As String, tSQL As String, tAccount As String
        Dim TMPRpt As String, mKondisi As String, mKondisi1A As String, mKondisi1B As String
        Dim TmpRpt1 As String, mKondisi2 As String, tSaldo As Double, mKondisi3 As String
        Randomize()
        'mPeriode = Format(DateAdd("m", -1, Tgl1.Value), "YYYY-MM")
        TMPRpt = Microsoft.VisualBasic.Left("TMPBB1" & Trim(Replace(Str(100000 * Rnd(1000)), ".", Format(Now, "HHMMSS"))), 30)
        TmpRpt1 = Microsoft.VisualBasic.Left("TMPBB2" & Trim(Replace(Str(100000 * Rnd(1000)), ".", Format(Now, "HHMMSS"))), 30)
        MsgSQL = "SELECT *  FROM information_schema.COLUMNS " &
             "WHERE TABLE_NAME = 'TMP_RPTBukuBesar'  "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        If dbTable.Rows.Count <> 0 Then
            MsgSQL = "Delete tmp_RptBukuBesar "
        Else
            MsgSQL = "CREATE TABLE [dbo].[TMP_RPTBukuBesar] ( " &
                "[IDRpt] [varchar] (30)  NULL , " &
                "[idrec] [varchar] (10)  NOT NULL , " &
                "[NoUrut] INT  Default 0, " &
                "[tanggal] [datetime] NOT NULL , " &
                "[nobukti] [varchar] (20)  NOT NULL , " &
                "[uraian] [varchar] (255)  NOT NULL , " &
                "[AccountCode1] [varchar] (15)  NOT NULL , " &
                "[NM_SUB] [varchar] (50)  NOT NULL, " &
                "[IDReg] [varchar] (15)  NOT NULL , " &
                "[Reg] [varchar] (50)  NOT NULL , " &
                "[debet] [money] NOT NULL , " &
                "[kredit] [money] NOT NULL , " &
                "[lastupd] [datetime] NOT NULL , " &
                "[userid] [varchar] (10)  NOT NULL , " &
                "[Saldo] [money] NOT NULL ) "
        End If
        Proses.ExecuteNonQuery(MsgSQL)

        If AccCode1.Text = "" Then
            mKondisi = ""
            mKondisi1A = ""
            mKondisi1B = ""
            mKondisi2 = ""
        Else
            mKondisi = " AND TMP_RPTBukuBesar.AccountCode1 = '" & AccCode1.Text & "' "
            mKondisi1A = " AND t_Jurnal.AccountCode = '" & AccCode1.Text & "' "
            mKondisi1B = " AND t_Jurnal.AccountCode = '" & AccCode1.Text & "' "
            mKondisi2 = " And COA = '" & AccCode1.Text & "' "
        End If

        MsgSQL = "Insert Into TMP_RPTBukuBesar " &
            "SELECT '" & TMPRpt & "', t_Jurnal.idrec, NoUrut, t_Jurnal.tanggal, " &
            "t_Jurnal.idrec, t_Jurnal.uraian, t_Jurnal.AccountCode, " &
            "t_Jurnal.KetAccCode, 'IDRegD', 'RegisterD', " &
            "t_Jurnal.debet, 0, t_Jurnal.LastUPD, t_Jurnal.UserID, 0 " &
            " From t_Jurnal t_Jurnal  " &
            "Where AktifYN = 'Y' and t_Jurnal.Debet <> 0 " &
            "  " & mKondisi1A & " " &
            "  And Convert(varchar(8), t_Jurnal.Tanggal, 112) " &
            "       Between '" & Format(Tgl1.Value, "yyyyMM01") & "' " &
            "           and '" & Format(Tgl2.Value, "yyyyMMdd") & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Insert Into TMP_RPTBukuBesar " &
        "SELECT '" & TMPRpt & "', t_Jurnal.idrec, NoUrut, t_Jurnal.tanggal, " &
        "t_Jurnal.idrec, t_Jurnal.uraian, t_Jurnal.AccountCode, " &
        "t_Jurnal.KetAccCode, 'IDRegK', 'RegisterK', " &
        "0, t_Jurnal.Kredit, t_Jurnal.LastUPD, t_Jurnal.UserID, 0 " &
        " From t_Jurnal t_Jurnal  " &
        "Where AktifYN = 'Y' and t_Jurnal.Kredit <> 0 " &
        "  " & mKondisi1B & " " &
        "  And Convert(varchar(8), t_Jurnal.Tanggal, 112) " &
        "       Between '" & Format(Tgl1.Value, "yyyyMM01") & "' " &
        "           and '" & Format(Tgl2.Value, "yyyyMMdd") & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        title = "BUKU BESAR "

        'isi saldo awal -----
        MsgSQL = "Insert Into Tmp_RPTBukuBesar SELECT '" & TMPRpt & "', " &
            "'~', 0, '" & Format(Tgl1.Value, "yyyy-MM") & "-01', " &
            "'', 'Saldo Awal', Coa, Nama, '', '', " &
            "0, 0, '" & Format(Tgl1.Value, "yyyy-MM") & "-01', '" & UserID & "', Saldo " &
            " From M_SaldoAwalCompany  " &
            "Where AktifYN = 'Y' and Saldo < 0 " &
            "  " & mKondisi2 & " " &
            "  And Periode = '" & Format(DateAdd("m", -1, Tgl1.Value), "MM-yyyy") & "'"
        Proses.ExecuteNonQuery(MsgSQL)

        MsgSQL = "Insert Into Tmp_RPTBukuBesar SELECT '" & TMPRpt & "', " &
        "'~', 0, '" & Format(Tgl1.Value, "yyyy-MM") & "-01', " &
        "'', 'Saldo Awal', Coa, Nama, '', '', " &
        "0, 0, '" & Format(Tgl1.Value, "yyyy-MM") & "-01', '" & UserID & "', Saldo " &
        " From M_SaldoAwalCompany  " &
        "Where AktifYN = 'Y' and Saldo > 0 " &
        "  " & mKondisi2 & " " &
        "  And Periode = '" & Format(DateAdd("m", -1, Tgl1.Value), "MM-yyyy") & "'"
        Proses.ExecuteNonQuery(MsgSQL)
        '----- end of isi saldo awal

        'Hitung saldo jika tgl awal <> = 1
        If Format(Tgl1.Value, "DD") <> "01" Then
            If AccCode1.Text = "" Then
                mKondisi3 = ""
            Else
                mKondisi3 = " AND TMP_RPTBukuBesar.AccountCode1 = '" & AccCode1.Text & "' "
            End If
            MsgSQL = "Select AccountCode1, NM_SUB, Sum(Debet) - Sum(Kredit) SaldoAkhir " &
                " INTO " & TmpRpt1 & " " &
                " From TMP_RPTBukuBesar " &
                "Where Convert(varchar(8), TMP_RPTBukuBesar.Tanggal, 112)  >= '" & Format(Tgl1.Value, "yyyyMM01") & "' " &
                "  and Convert(varchar(8), TMP_RPTBukuBesar.Tanggal, 112)  < '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
                " " & mKondisi3 & " and IDRec <> '~' " &
                " GROUP BY AccountCode1, NM_SUB "
            Proses.ExecuteNonQuery(MsgSQL)

            MsgSQL = "Update tmp_RptBukuBesar " &
            "Set Saldo = Saldo + SaldoAkhir, Uraian = 'Saldo Akhir' " &
            " From TMP_RPTBukuBesar INNER JOIN " & TmpRpt1 & " b ON " &
            "      TMP_RPTBukuBesar.AccountCode1 = b.AccountCode1 " &
            "Where TMP_RPTBukuBesar.IDRec = '~' "
            Proses.ExecuteNonQuery(MsgSQL)

            MsgSQL = "Drop Table " & TmpRpt1 & " "
            Proses.ExecuteNonQuery(MsgSQL)
        End If
        '-------------end of Hitung saldo jika tgl awal <> = 1

        '--hitung saldo
        tSaldo = 0
        tAccount = ""
        MsgSQL = "SELECT TMP_RPTBukuBesar.IDRpt, TMP_RPTBukuBesar.tanggal, TMP_RPTBukuBesar.IDRec, " &
            "TMP_RPTBukuBesar.nobukti, TMP_RPTBukuBesar.uraian, TMP_RPTBukuBesar.AccountCode1, TMP_RPTBukuBesar.NoUrut, " &
            "TMP_RPTBukuBesar.NM_SUB, TMP_RPTBukuBesar.Debet, TMP_RPTBukuBesar.Kredit, TMP_RPTBukuBesar.Saldo " &
            " FROM TMP_RPTBukuBesar TMP_RPTBukuBesar " &
            "Where Convert(varchar(8), TMP_RPTBukuBesar.tanggal, 112) " &
            "       Between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
            "           and '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
            "  or IDRec = '~' " & mKondisi & " " &
            "Order By TMP_RPTBukuBesar.AccountCode1,  " &
            "      TMP_RPTBukuBesar.Tanggal, TMP_RPTBukuBesar.nobukti, TMP_RPTBukuBesar.NoUrut "
        RS05 = Proses.ExecuteQuery(MsgSQL)

        For a = 0 To RS05.Rows.Count - 1
            Application.DoEvents()
            If tAccount <> Trim(RS05.Rows(a) !accountcode1) Then
                tSaldo = 0
            End If
            If RS05.Rows(a) !IDRec = "~" Then
                tSaldo = RS05.Rows(a) !Saldo
            Else
                tSaldo = tSaldo + RS05.Rows(a) !Debet - RS05.Rows(a) !Kredit
            End If
            '        Debug.Print tSaldo
            tSQL = "UPDATE TMP_RPTBukuBesar Set Saldo = " & tSaldo & " " &
                    "Where tanggal = '" & RS05.Rows(a) !Tanggal & "' " &
                    "  And NoBukti = '" & RS05.Rows(a) !NoBukti & "' " &
                    "  And AccountCode1 = '" & RS05.Rows(a) !accountcode1 & "' " &
                    "  And IDRec = '" & RS05.Rows(a) !IDRec & "' " &
                    "  AND NOURUT = " & RS05.Rows(a) !NoUrut & " "
            If RS05.Rows(a) !IDRec <> "~" Then
                Proses.ExecuteNonQuery(tSQL)
            End If
            tAccount = Trim(RS05.Rows(a) !accountcode1)
        Next (a)
        'end of hitung saldo-- 

        If Format(Tgl1.Value, "yyMMdd") = Format(Tgl2.Value, "yyMMdd") Then
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy")
        Else
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy") + " s.d " +
                      Format(Tgl2.Value, "dd MMM yyyy")
        End If

        Call OpenConn()
        dttable = New DataTable
        SQL = "SELECT TMP_RPTBukuBesar.IDRpt, TMP_RPTBukuBesar.tanggal, " &
            "TMP_RPTBukuBesar.nobukti, TMP_RPTBukuBesar.uraian, " &
            "TMP_RPTBukuBesar.AccountCode1, TMP_RPTBukuBesar.NM_SUB, TMP_RPTBukuBesar.debet, " &
            "TMP_RPTBukuBesar.kredit, TMP_RPTBukuBesar.Saldo " &
            "From PEKERTI.dbo.TMP_RPTBukuBesar  TMP_RPTBukuBesar " &
            "Where convert(Char(8), Tanggal,112) between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
            "      And '" & Format(Tgl2.Value, "yyyyMMdd") & "' or idrec = '~' " &
            "ORDER BY TMP_RPTBukuBesar.AccountCode1,   " &
            "      TMP_RPTBukuBesar.Tanggal, TMP_RPTBukuBesar.nobukti, TMP_RPTBukuBesar.NoUrut "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            objRep = New Rpt_BukuBesar
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Periode", Periode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            ' CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
            CrystalReportViewer1.Refresh()
            CrystalReportViewer1.ReportSource = objRep
            dttable.Dispose()
            DTadapter.Dispose()
            CloseConn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
        Me.Cursor = Cursors.Default
        cmdCetak.Enabled = True
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

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        Ledger()
    End Sub

    Private Sub AccCode1_TextChanged(sender As Object, e As EventArgs) Handles AccCode1.TextChanged
        If Len(AccCode1.Text) <= 1 Then
            KetAccCode1.Text = ""
        End If
    End Sub

    Private Sub AccCode1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AccCode1.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim MsgSQL As String, RS01 As New DataTable
            MsgSQL = "Select * From M_PERKIRAAN " &
                    "where aktifYN = 'Y' " &
                    " And NO_PERKIRAAN = '" & AccCode1.Text & "'"
            RS01 = Proses.ExecuteQuery(MsgSQL)
            If RS01.Rows.Count <> 0 Then
                AccCode1.Text = Trim(RS01.Rows(0) !no_PERKIRAAN)
                KetAccCode1.Text = Trim(RS01.Rows(0) !NM_PERKIRAAN)
                cmdCetak.Focus()
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
                    cmdCetak.Focus()
                Else
                    MsgBox("Account Code tidak ditemukan!", vbQuestion + vbCritical, ".:Warning !")
                    AccCode1.Text = ""
                    AccCode1.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmbJenisLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisLaporan.SelectedIndexChanged
        If Mid(cmbJenisLaporan.Text, 1, 1) = "2" Then
            AccCode1.Visible = True
        Else
            AccCode1.Visible = False
        End If
    End Sub

End Class