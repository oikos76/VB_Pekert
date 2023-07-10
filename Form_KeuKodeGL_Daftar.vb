
Public Class Form_KeuKodeGL_Daftar
    Dim SQL As String
    Dim Proses As New ClsKoneksi
    Dim tblData As New DataTable

    Private Sub Form_KeuKodeGL_Daftar_Load(sender As Object, e As EventArgs) Handles Me.Load
        FrmMenuUtama.TSKeterangan.Visible = True
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen     'LightSkyBlue
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGView.AllowUserToResizeColumns = True

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        With Me.DGView.RowTemplate
            .Height = 30
            .MinimumHeight = 30
        End With
        DGView.Font = New Font("Arial", 10, FontStyle.Regular)
        tCari.Text = ""
        Data_Refresh()
        tCari.Visible = True
    End Sub
    Private Sub Data_Refresh()
        Dim dbKel As New DataTable, dbKode As New DataTable,
            dbSub As New DataTable, dbPerk As New DataTable


        DGView.ColumnCount = 5
        DGView.Columns(0).Visible = True
        Me.Cursor = Cursors.WaitCursor
        DGView.Rows.Clear()
        DGView.Columns(0).HeaderText = "ID Rec"
        DGView.Columns(0).Width = 5
        DGView.Columns(1).HeaderText = "Kode"
        DGView.Columns(1).Width = 200
        DGView.Columns(2).HeaderText = "Nama"
        DGView.Columns(2).Width = 550

        SQL = "SELECT NO_Gol, NM_Gol FROM m_Golongan WHERE AktifYN = 'Y' ORDER BY NO_Gol "
        tblData = Proses.ExecuteQuery(SQL)
        With tblData.Columns(0)
            For a = 0 To tblData.Rows.Count - 1
                Application.DoEvents()
                DGView.Rows.Add("GOLONGAN|" + .Table.Rows(a) !NO_Gol,
                    .Table.Rows(a) !NO_Gol,
                    .Table.Rows(a) !NM_Gol)
                'Kelompok----
                SQL = "SELECT  NO_Klp, NM_Klp " &
                    "FROM M_KELOMPOK  " &
                    "WHERE AKTIFYN = 'Y'  " &
                    "  AND NO_GOL = '" & .Table.Rows(a) !NO_Gol & "'  " &
                    "ORDER by NO_KLP "
                dbKel = Proses.ExecuteQuery(SQL)
                For b = 0 To dbKel.Rows.Count - 1
                    DGView.Rows.Add("KELOMPOK|" + dbKel.Rows(b) !NO_Klp,
                        Space(3) + dbKel.Rows(b) !NO_Klp,
                        Space(3) + dbKel.Rows(b) !NM_Klp)
                    'Kode-----
                    SQL = "SELECT  NO_KODE, NM_KODE " &
                       "FROM M_KODE  " &
                       "WHERE AKTIFYN = 'Y'  " &
                       "  AND NO_KLP = '" & dbKel.Rows(b) !NO_Klp & "'  " &
                       "ORDER by NO_KODE "
                    dbKode = Proses.ExecuteQuery(SQL)
                    For c = 0 To dbKode.Rows.Count - 1
                        DGView.Rows.Add("KODE|" + dbKode.Rows(c) !NO_KODE,
                           Space(6) + dbKode.Rows(c) !NO_KODE,
                           Space(6) + dbKode.Rows(c) !NM_Kode)
                        'SubPerkiraan -------
                        SQL = "SELECT NO_SUB, NM_SUB " &
                           "FROM m_subperkiraan  " &
                           "WHERE AKTIFYN = 'Y'  " &
                           "  And NO_KODE = '" & dbKode.Rows(c) !NO_KODE & "'  " &
                           "ORDER by  NO_SUB, NM_SUB  "
                        dbSub = Proses.ExecuteQuery(SQL)
                        For d = 0 To dbSub.Rows.Count - 1
                            DGView.Rows.Add("SUB PERKIRAAN|" + dbSub.Rows(d) !NO_SUB,
                               Space(9) + dbSub.Rows(d) !NO_SUB,
                               Space(9) + dbSub.Rows(d) !NM_SUB)

                            'Perkiraan-----------
                            SQL = "SELECT NO_PERKIRAAN, NM_PERKIRAAN  " &
                                 "FROM m_Perkiraan  " &
                                 "WHERE AKTIFYN = 'Y'  " &
                                 "  And NO_SUB = '" & dbSub.Rows(d) !NO_SUB & "'  " &
                                 "ORDER by NO_PERKIRAAN, NM_PERKIRAAN  "
                            dbPerk = Proses.ExecuteQuery(SQL)
                            For e = 0 To dbPerk.Rows.Count - 1
                                DGView.Rows.Add("PERKIRAAN|" + dbPerk.Rows(e) !NO_PERKIRAAN,
                                       Space(12) + dbPerk.Rows(e) !NO_PERKIRAAN,
                                       Space(12) + dbPerk.Rows(e) !NM_PERKIRAAN)
                            Next (e)
                        Next (d)
                    Next (c)
                Next (b)
            Next a
        End With
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub DGView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        If DGView.Rows.Count = 0 Then Exit Sub
        Dim strTemp As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Dim tArray As String()
        'tArray = strTemp.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        tArray = strTemp.Split("|".ToCharArray())
        Dim JenisKode As String = ""
        JenisKode = tArray(0)
        With Form_KeuKodeGL
            .NoPerkiraan.Text = ""
            .NamaPerkiraan.Text = ""
            .NoSubPerkiraan.Text = ""
            .NamaSubPerkiraan.Text = ""
            .NoKode.Text = ""
            .NamaKode.Text = ""
            .NoKelompok.Text = ""
            .NamaKelompok.Text = ""
            .NoGolongan.Text = ""
            .NamaGolongan.Text = ""
            .cmbJenisTabel.Text = Trim(JenisKode)
            .AturComboBox()
            If JenisKode = "PERKIRAAN" Then
                .NoPerkiraan.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value)
                .NamaPerkiraan.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value)
            ElseIf JenisKode = "SUB PERKIRAAN" Then
                .NoSubPerkiraan.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value)
                .NamaSubPerkiraan.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value)
            ElseIf JenisKode = "KODE" Then
                .NoKode.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value)
                .NamaKode.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value)
            ElseIf JenisKode = "KELOMPOK" Then
                .NoKelompok.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value)
                .NamaKelompok.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value)
            ElseIf JenisKode = "GOLONGAN" Then
                .NoGolongan.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value)
                .NamaGolongan.Text = Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(2).Value)
            End If
            .isi_COA
        End With
        Me.Close()
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then

        End If
    End Sub
End Class