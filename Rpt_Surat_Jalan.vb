Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Public Class Rpt_Surat_Jalan
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
    Private Sub kd_asal_DoubleClick(sender As Object, e As EventArgs) Handles kd_asal.DoubleClick
        kd_asal.Text = ""
        SendKeys.Send("{enter}")
    End Sub

    Private Sub kd_asal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles kd_asal.KeyPress
        If e.KeyChar = Chr(13) Then
            Sql = "Select * From m_Toko " &
                "Where idrec = '" & kd_asal.Text & "' "
            dbTable = Proses.ExecuteQuery(Sql)
            If dbTable.Rows.Count <> 0 Then
                kd_asal.Text = dbTable.Rows(0) !idrec
                TkAsal.Text = dbTable.Rows(0) !nama
                cmdCetak.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                     " From m_Toko " &
                     "Where AktifYN = 'Y' " &
                     "  And nama Like '%" & kd_asal.Text & "%' " &
                     "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                kd_asal.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                Sql = "Select * From m_Toko " &
                    "Where idrec = '" & kd_asal.Text & "' "
                dbTable = Proses.ExecuteQuery(Sql)
                If dbTable.Rows.Count <> 0 Then
                    kd_asal.Text = dbTable.Rows(0) !idrec
                    TkAsal.Text = dbTable.Rows(0) !nama
                    cmdCetak.Focus()
                Else
                    kd_asal.Text = ""
                    Toko7an.Text = ""
                    Kd_Toko7an.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub kd_asal_TextChanged(sender As Object, e As EventArgs) Handles kd_asal.TextChanged
        If Trim(kd_asal.Text) = "" Then
            TkAsal.Text = ""
        End If
    End Sub

    Private Sub Kd_Toko7an_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kd_Toko7an.KeyPress
        If e.KeyChar = Chr(13) Then
            Sql = "Select * From m_Toko " &
                "Where idrec = '" & Kd_Toko7an.Text & "' "
            dbTable = Proses.ExecuteQuery(Sql)
            If dbTable.Rows.Count <> 0 Then
                Kd_Toko7an.Text = dbTable.Rows(0) !idrec
                Toko7an.Text = dbTable.Rows(0) !nama
                cmdCetak.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                     " From m_Toko " &
                     "Where AktifYN = 'Y' " &
                     "  And nama Like '%" & Kd_Toko7an.Text & "%' " &
                     "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                Kd_Toko7an.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                Sql = "Select * From m_Toko " &
                    "Where idrec = '" & Kd_Toko7an.Text & "' "
                dbTable = Proses.ExecuteQuery(Sql)
                If dbTable.Rows.Count <> 0 Then
                    Kd_Toko7an.Text = dbTable.Rows(0) !idrec
                    Toko7an.Text = dbTable.Rows(0) !nama
                    cmdCetak.Focus()
                Else
                    Kd_Toko7an.Text = ""
                    Toko7an.Text = ""
                    Kd_Toko7an.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Kd_Toko7an_TextChanged(sender As Object, e As EventArgs) Handles Kd_Toko7an.TextChanged
        If Len(Trim(Kd_Toko7an.Text)) = 0 Then
            Toko7an.Text = ""
        End If
    End Sub

    Private Sub Rpt_Surat_Jalan_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Tgl1.Value = DateAdd(DateInterval.Month, -1, Now)
        'Tgl2.Value = Now
        kd_asal.Text = ""
        TkAsal.Text = ""
        IdRecord.Text = ""
        Kd_Toko7an.Text = FrmMenuUtama.Kode_Toko.Text
        Toko7an.Text = FrmMenuUtama.Nama_Toko.Text
        Me.Cursor = Cursors.WaitCursor
        SQL = "Select * " &
            " From m_Kategori " &
            "Where AktifYN = 'Y' and kategori <> '' Order By kategori "
        dbTable = Proses.ExecuteQuery(SQL)
        cmb_Kategori.Items.Clear()
        cmb_Kategori.Items.Add("")
        cmb_Kategori.Items.Add("<ALL KATEGORI>")
        For a = 0 To dbTable.Rows.Count - 1
            cmb_Kategori.Items.Add(dbTable.Rows(a)!kategori)
        Next (a)
        SQL = "Select * " &
            " From m_Kategori_Sub " &
            "Where AktifYN = 'Y' and Subkategori <>'' Order By Subkategori "
        dbTable = Proses.ExecuteQuery(SQL)
        Me.Cursor = Cursors.WaitCursor
        cmb_SubKategori.Items.Clear()
        cmb_SubKategori.Items.Add("")
        cmb_SubKategori.Items.Add("<ALL SUB KATEGORI>")
        For a = 0 To dbTable.Rows.Count - 1
            cmb_SubKategori.Items.Add(dbTable.Rows(a) !Subkategori)
        Next (a)
        cmbStatusSJ.Items.Clear()
        cmbStatusSJ.Items.Add("di terima")
        cmbStatusSJ.Items.Add("belum di terima")
        cmbStatusSJ.Items.Add("")
        cmbStatusSJ.SelectedIndex = 0

        cmbJenisLaporan.Items.Clear()
        cmbJenisLaporan.Items.Add("Status Penerimaan")
        cmbJenisLaporan.Items.Add("Kategori/Sub-Kategori")
        cmbJenisLaporan.SelectedIndex = 0

        Tgl1.Value = DateAdd(DateInterval.Month, -3, Now)
        Tgl2.Value = Now

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.ReportSource = Nothing
        If FrmMenuUtama.Kode_Toko.Text = "AG020" Or FrmMenuUtama.Kode_Toko.Text = "AG018" Or UCase(UserID) = "EKO_K" Then
        Else
            If Format(Tgl1.Value, "yyMMdd") < Format(DateAdd(DateInterval.Day, -3, Now()), "yyMMdd") Then
                Tgl1.Value = DateAdd(DateInterval.Day, -3, Now())
            End If
        End If

        If Trim(cmbJenisLaporan.Text) = "Status Penerimaan" Then
            SJ_ByStatus()
        ElseIf Trim(cmbJenisLaporan.Text) = "Kategori/Sub-Kategori" Then
            SJ_PerKategori()
        End If
    End Sub
    Private Sub SJ_PerKategori()

        Dim mKategori As String = "", mSubKategori As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        Dim NamaPerusahaan As String = ""
        Dim Periode As String = "", mKondisi As String = ""
        cmdCetak.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        SQL = "Select * From M_Toko where idRec = '" & FrmMenuUtama.Kode_Toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaPerusahaan = dbTable.Rows(0) !nama
        Else
            NamaPerusahaan = ""
        End If
        If Format(Tgl1.Value, "yyMMdd") = Format(Tgl2.Value, "yyMMdd") Then
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy")
        Else
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy") + " s.d " +
                      Format(Tgl2.Value, "dd MMM yyyy")
        End If
        If Trim(kd_asal.Text) <> "" Then
            mKondisi = " and H.id_Asal = '" & kd_asal.Text & "' "
        End If
        If Trim(Kd_Toko7an.Text) <> "" Then
            mKondisi = mKondisi + " and H.id_Tujuan = '" & Kd_Toko7an.Text & "' "
        End If
        If Trim(IdRecord.Text) <> "" Then
            mKondisi = mKondisi + " and H.IdRec = '" & IdRecord.Text & "' "
        End If
        If Trim(cmb_Kategori.Text) <> "<ALL KATEGORI>" Then
            mKategori = " and m_barang.kategori = '" & cmb_Kategori.Text & "' "
        End If
        If Trim(cmb_SubKategori.Text) <> "<ALL SUB KATEGORI>" Then
            mKategori = mKategori + " and (m_barang.subkategori = '" & cmb_SubKategori.Text & "') "
        End If
        Call OpenConn()
        dttable = New DataTable

        If Trim(cmb_Kategori.Text) <> "<ALL KATEGORI>" Then
            If Trim(cmb_SubKategori.Text) = "<ALL SUB KATEGORI>" Then
                mSubKategori = ", m_barang.subkategori "
            Else
                mSubKategori = " "
            End If
        Else
            If Trim(cmb_SubKategori.Text) = "<ALL SUB KATEGORI>" Then
                mSubKategori = ", m_barang.subkategori "
            Else
                mSubKategori = " "
            End If
        End If

        SQL = "Select id_asal, toko_asal, Id_Tujuan, Toko_Tujuan, kategori  " & mSubKategori & ", " &
            "       sum(harga * qtyb) STotal, sum (disc1 * qtyb) disc, sum(d.sub_total) sub_total " &
             " From t_SJH H inner Join t_SJD D on H.idrec = d.id_rec " &
             "      And h.Kode_Toko = d.Kode_Toko And h.AktifYN= 'Y' and d.AktifYN = 'Y' " &
             "      inner join m_Barang on m_barang.idrec = D.KodeBrg " &
             "Where Convert(varchar(8), H.tglSJ, 112) Between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
             "      And '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
             "  " & mKondisi & " " & mKategori & " " &
             "group by id_asal, toko_asal, Id_Tujuan, Toko_Tujuan, kategori" & mSubKategori & " " &
             "Order by toko_asal, kategori "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            'If Trim(cmb_Kategori.Text) <> "<ALL KATEGORI>" Then
            '    If Trim(cmb_SubKategori.Text) = "<ALL SUB KATEGORI>" Then
            '        objRep = New Rpt_Surat_Jalan_RSubKat
            '    Else
            '        objRep = New Rpt_Surat_Jalan_R
            '    End If
            'Else
            '    If Trim(cmb_SubKategori.Text) = "<ALL SUB KATEGORI>" Then
            '        objRep = New Rpt_Surat_Jalan_RSubKat
            '    Else
            '        objRep = New Rpt_Surat_Jalan_R
            '    End If
            'End If

            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("nama_perusahaan", NamaPerusahaan)
            objRep.SetParameterValue("Periode", Periode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            If Trim(kd_asal.Text) = "" And Trim(Kd_Toko7an.Text) = "" Then
                If IdRecord.Text <> "" Then
                    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
                Else
                    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
                End If

            Else
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            End If
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
    Private Sub SJ_ByStatus()
        Dim mKategori As String = "", mOrder As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        Dim NamaPerusahaan As String = ""
        Dim Periode As String = "", mKondisi As String = ""
        cmdCetak.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        SQL = "Select * From M_Toko where idRec = '" & FrmMenuUtama.Kode_Toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaPerusahaan = dbTable.Rows(0) !nama
        Else
            NamaPerusahaan = ""
        End If
        If Format(Tgl1.Value, "yyMMdd") = Format(Tgl2.Value, "yyMMdd") Then
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy")
        Else
            Periode = "Periode : " + Format(Tgl1.Value, "dd MMM yyyy") + " s.d " +
                      Format(Tgl2.Value, "dd MMM yyyy")
        End If
        If Trim(kd_asal.Text) <> "" Then
            mKondisi = " and H.id_Asal = '" & kd_asal.Text & "' "
        End If
        If Trim(Kd_Toko7an.Text) <> "" Then
            mKondisi = mKondisi + " and H.id_Tujuan = '" & Kd_Toko7an.Text & "' "
        End If
        If Trim(IdRecord.Text) <> "" Then
            mKondisi = mKondisi + " and H.IdRec = '" & IdRecord.Text & "' "
        End If
        If Trim(cmbStatusSJ.Text) <> "" Then
            If cmbStatusSJ.Text = "di terima" Then
                mKondisi = mKondisi + " and H.terimaYN = 'Y' "
            ElseIf cmbStatusSJ.Text = "belum di terima" Then
                mKondisi = mKondisi + " and H.terimaYN = 'N' "
            End If
        End If
        Call OpenConn()
        If chkRekap.Checked = True Then
            mOrder = urutKodeToko()
        End If
        dttable = New DataTable
        SQL = "Select * " &
             " From t_SJH H inner Join t_SJD D on H.idrec = d.id_rec " &
             "      And h.Kode_Toko = d.Kode_Toko and h.AktifYN= 'Y' and d.AktifYN = 'Y' " &
             "      inner join m_Barang on m_barang.idrec = D.KodeBrg " &
             "Where Convert(varchar(8), H.tglSJ, 112) Between '" & Format(Tgl1.Value, "yyyyMMdd") & "' " &
             "      And '" & Format(Tgl2.Value, "yyyyMMdd") & "' " &
             "  " & mKondisi & " " &
             "Order by " & mOrder & " H.tglSJ, H.IdRec "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            'If chkRekap.Checked = True Then
            '    objRep = New Rpt_Surat_Jalan_Rekap
            'Else
            '    objRep = New Rpt_Surat_Jalan_D
            'End If
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("nama_perusahaan", NamaPerusahaan)
            objRep.SetParameterValue("Periode", Periode)
            CrystalReportViewer1.ShowGroupTreeButton = True
            CrystalReportViewer1.ShowExportButton = True
            If Trim(kd_asal.Text) = "" And Trim(Kd_Toko7an.Text) = "" Then
                If IdRecord.Text <> "" Then
                    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
                Else
                    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.GroupTree
                End If

            Else
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None
            End If
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

    Private Function urutKodeToko() As String
        Dim mUrut As String = ""
        If Trim(kd_asal.Text) = "" And Trim(Kd_Toko7an.Text) <> "" Then
            mUrut = " H.id_Asal, "
        ElseIf Trim(kd_asal.Text) <> "" And Trim(Kd_Toko7an.Text) = "" Then
            mUrut = "  H.id_Tujuan, "
        ElseIf Trim(kd_asal.Text) <> "" And Trim(Kd_Toko7an.Text) <> "" Then
            mUrut = "  H.id_Asal, H.id_Tujuan, "
        End If
        urutKodeToko = mUrut
    End Function
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

    Private Sub cmb_Kategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Kategori.SelectedIndexChanged
        cmb_SubKategori.Items.Clear()
        If Trim(cmb_Kategori.Text) <> "" Then
            SQL = "Select * " &
                " From m_Kategori_SubKategori " &
                "Where AktifYN = 'Y' " &
                " And SubKategori <> '' " &
                " And Kategori = '" & cmb_Kategori.Text & "' " &
                " Order By SubKategori "
            dbTable = Proses.ExecuteQuery(SQL)
            cmb_SubKategori.Items.Add("")
            If dbTable.Rows.Count() > 0 Then
                cmb_SubKategori.Items.Add("<ALL SUB KATEGORI>")
            End If
            Me.Cursor = Cursors.WaitCursor
            For a = 0 To dbTable.Rows.Count - 1
                cmb_SubKategori.Items.Add(dbTable.Rows(a)!SubKategori)
            Next (a)
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub cmbJenisLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisLaporan.SelectedIndexChanged
        If chkRekap.Checked = False Then chkRekap.Checked = True
        If Trim(cmbJenisLaporan.Text) = "Status Penerimaan" Then
            PanelKategori.Visible = False
            LabelStatusSJ.Visible = True
            cmbStatusSJ.Visible = True
            chkRekap.Visible = True
        ElseIf Trim(cmbJenisLaporan.Text) = "Kategori/Sub-Kategori" Then
            PanelKategori.Visible = True
            LabelStatusSJ.Visible = False
            cmbStatusSJ.Visible = False
            chkRekap.Visible = False
        End If

    End Sub
End Class