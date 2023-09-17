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

    Private Sub PeriodeSaldo_ValueChanged(sender As Object, e As EventArgs) Handles PeriodeSaldo.ValueChanged

    End Sub

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
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        LAdd = False
        LEdit = True
        AturTombol(False)
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub btnSimpanSetup_Click(sender As Object, e As EventArgs) Handles btnSimpanSetup.Click
        SQL = "DELETE m_Periode where right(Periode,4) = '" & Format(PeriodeSaldo.Value, "yyyy") & "'"
        Proses.ExecuteNonQuery(SQL)
        For i = 1 To 12
            SQL = "INSERT INTO m_Periode (Periode) values ('" & Mid(Format(i + 100, "##0"), 2, 2) & "-" & Format(PeriodeSaldo.Value, "yyyy") & "' )"
            Proses.ExecuteNonQuery(SQL)
            tPeriode.Items.Add(Mid(Format(i + 100, "##0"), 2, 2) & "-" & Format(PeriodeSaldo.Value, "yyyy"))
        Next i
        tPeriode.Text = (Format(PeriodeSaldo.Value, "MM-yyyy"))
        PanelSetupPeriode.Visible = False
        PanelTombol.Enabled = True
        tPeriode.SelectedIndex = -1

    End Sub


    Private Sub btnBatalSetup_Click(sender As Object, e As EventArgs) Handles btnBatalSetup.Click
        PanelSetupPeriode.Visible = False
        PanelTombol.Enabled = False
    End Sub

    Private Sub Form_SaldoAwal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim MsgSQL As String, RSL As New DataTable
        Dim RS02 As New DataTable
        PanelSetupPeriode.Visible = False
        PanelTombol.Enabled = True
        tPeriode.Items.Clear()
        DGView.Rows.Clear()
        PeriodeSaldo.Value = Format(Now, "MM-yyyy")

        Me.Cursor = Cursors.WaitCursor
        MsgSQL = "Select * From M_Periode " &
            "Order By Right(periode,4) + left(periode,2) "
        RS02 = Proses.ExecuteQuery(MsgSQL)
        If RS02.Rows.Count = 0 Then
            PanelSetupPeriode.Visible = True
            PanelTombol.Enabled = False
        Else
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
        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        'DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub IsiSaldoAwal()
        Dim MsgSQL As String, tIdRec As String = ""
        Me.Cursor = Cursors.WaitCursor

        MsgSQL = " select * From m_Perkiraan " &
            "where aktifYN = 'Y' " &
            "  And no_Perkiraan not in " &
            "      (select coa " &
            "         from m_SaldoawalCompany inner join m_Perkiraan " &
            "              on coa = no_Perkiraan " &
            "        where periode = '" & tPeriode.Text & "' and m_SaldoAwalCompany.aktifYN = 'Y' ) "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            MsgSQL = "Select isnull(Max(right(IDRec,6)),0) + 100000001 IDRec " &
                " From m_SaldoAwalCompany Where left(idrec,4) = '" & Format(Now(), "YYYY") & "' "
            tIdRec = Proses.ExecuteSingleStrQuery(MsgSQL)
            MsgSQL = "INSERT INTO m_SaldoAwalCompany(IDRec, " &
                "Periode, COA, Nama, Saldo, AktifYN, LastUPD, UserId, " &
                "AREA) VALUES ('" & tIdRec & "', '" & tPeriode.Text & "', " &
                " '" & dbTable.Rows(0) !no_PERKIRAAN & "', '" & dbTable.Rows(0) !NM_PERKIRAAN & "', " &
                " 0, 'Y', GetDate(), '" & UserID & "', " &
                " '" & FrmMenuUtama.CompCode.Text & "')"
        Next (a)
        DGView.Rows.Clear()
        DGView.Visible = False
        MsgSQL = "Select * " &
            " From M_SaldoAwalCompany " &
            "Where AktifYN = 'Y'   " &
            "  And Periode = '" & tPeriode.Text & "' " &
            "Order By COA "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(dbTable.Rows(a) !COA,
                    dbTable.Rows(a) !Nama,
                    Format(dbTable.Rows(a) !Saldo, "###,##0"))
        Next (a)
        DGView.Visible = True
        Proses.CloseConn()
        Me.Cursor = Cursors.Default
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
            DGView.Rows.Add(RS01.Rows(a) !no_PERKIRAAN,
                    RS01.Rows(a) !NM_PERKIRAAN,
                    Format(0, "###,##0"))
        Next (a)
        Proses.CloseConn()
        DGView.Visible = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PeriodeSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PeriodeSaldo.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub
End Class