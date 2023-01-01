Public Class Form_DaftarBarang
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean, tKode As String, tIndex As Integer
    Dim Proses As New ClsKoneksi
    Dim tblData As New DataTable

    Private Sub FillColor()
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.DarkGoldenrod      'DeepSkyBlue     'LightSkyBlue
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGView.AllowUserToResizeColumns = True

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        With DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 35
        End With

        DGView.Columns(0).HeaderText = "Kode Barang"
        DGView.Columns(0).Width = 80
        DGView.Columns(1).HeaderText = "Nama Barang"
        DGView.Columns(1).Width = 300
        DGView.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DGView.Columns(2).HeaderText = "QTY"
        DGView.Columns(2).Width = 80
        DGView.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGView.Columns(2).DefaultCellStyle.Format = "N"
        DGView.Columns(3).HeaderText = "Satuan"
        DGView.Columns(3).Width = 80
        DGView.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DGView.Columns(4).HeaderText = "Harga"
        DGView.Columns(4).Width = 80
        DGView.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGView.Columns(4).DefaultCellStyle.Format = "N"

        DGView.Columns(5).HeaderText = "Isi Satuan"
        DGView.Columns(5).Width = 150
        DGView.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        DGView.Columns(6).HeaderText = "Isi Satuan B"
        DGView.Columns(6).Width = 150
        DGView.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        DGView.Columns(7).HeaderText = ""
        DGView.Columns(8).HeaderText = ""
        DGView.Columns(9).HeaderText = ""
        DGView.Columns(10).HeaderText = ""
        DGView.Font = New Font("Arial", 10, FontStyle.Regular)
        With tCari

            .ForeColor = Color.Gray
            .BackColor = Color.LightGoldenrodYellow
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
        ' If DGView.Rows.Count <> 0 Then DGView.Focus()
    End Sub

    Private Sub Form_DaftarBarang_Load(sender As Object, e As EventArgs) Handles Me.Load
        FillColor()
        txtQuery.Visible = False
    End Sub

    Public Sub Cari()
        Dim mCari As String = ""
        Dim xHarga As String = "", mHarga As String
        Dim KodeToko As String = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)
        If FrmMenuUtama.Kode_Toko.Text = "KM003" Then
            KodeToko = kode_toko.Text
        End If

        If txt_Nama_Barang.Text <> "" Then
            mCari = txt_Nama_Barang.Text
            txt_Nama_Barang.Text = ""
        Else
            mCari = tCari.Text
        End If

        'Set the DataAdapter's query.
        mHarga = " "

        If JenisTR.Text = "PEMBELIAN" Then
            xHarga = " isnull(hargabeli, 0) hargabeli "
        Else
            xHarga = ""
        End If

        txtQuery.Text = "Select IDRec, Nama, Stock" & KodeToko & " as QTY, Satuan, " &
            " isnull(PriceList, 0) as [PriceList], " & xHarga & " " &
            "'1 ' + SatuanT + ' = ' +  convert(varchar , isisatt) + ' ' + satuan as [isiSatSedang], " &
            "'1 ' + SatuanB + ' = ' +  convert(varchar , isisatb) + ' ' + satuan as [isiSatBesar] " &
            " From M_Barang " &
            "Where AktifYN = 'Y' " &
            " and (nama like '" & mCari & "%' " &
            "        or idrec like '" & mCari & "%') " &
            "Order by nama "

        If JenisTR.Text = "Daftar Barang Faktur Penjualan" Then
            txtQuery.Text = "Select  m_barang.IDRec, m_barang.Nama, Stock" & KodeToko & " as QTY, M_Barang.Satuan, " &
                " " & mHarga & " as [PriceList], " & xHarga & " " &
                "'1 ' + SatuanT + ' = ' +  convert(varchar , isisatt) + ' ' + M_Barang.satuan as [isiSatSedang], " &
                "'1 ' + SatuanB + ' = ' +  convert(varchar , isisatb) + ' ' + M_Barang.satuan as [isiSatBesar] " &
                " From M_Barang inner join t_SOD on m_barang.idrec = t_SOD.KodeBrg  " &
                "               and t_SOD.Id_Rec = '" & Param1.Text & "' " &
                "Where T_SOD.AktifYN = 'Y' " &
                "  " & mCari & " " &
                "Order by nama "
        End If

        Data_Refresh()
    End Sub

    Private Sub txtCari_GotFocus(sender As Object, e As EventArgs) Handles tCari.GotFocus
        'With tCari
        '    .ForeColor = Color.Black
        '    .BackColor = Color.White
        '    .SelectionStart = 0
        '    .SelectionLength = Len(.Text)
        'End With
    End Sub


    Private Sub txtCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            Cari()
            ' DGView.Focus()
        End If
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged
        If Len(Trim(tCari.Text)) = 0 Then
            DGView.Rows.Clear()
        ElseIf Len(Trim(tCari.Text)) > 1 Then
            Cari()
            tCari.Focus()
        End If
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub txtPageSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Nama_Barang.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso txt_Nama_Barang.Text.IndexOf(".") > 0 _
            AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub DGView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        FrmMenuUtama.TSKeterangan.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Me.Close()
    End Sub

    Private Sub DGView_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DGView.PreviewKeyDown
        If e.KeyData = Keys.Enter Then
            e.IsInputKey = True
            If DGView.Rows.Count <> 0 Then
                DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Selected = True
                FrmMenuUtama.TSKeterangan.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
            End If
            Me.Close()
        End If
    End Sub

    Sub Data_Refresh()
        DGView.ColumnCount = 20
        ' DGView.Visible = False
        ' Me.Cursor = Cursors.WaitCursor
        tblData = Proses.ExecuteQuery(txtQuery.Text)
        DGView.Rows.Clear()
        DGView.Visible = False
        tCari.Enabled = False
        With tblData.Columns(0)
            For a = 0 To tblData.Rows.Count - 1
                Application.DoEvents
                DGView.Rows.Add(.Table.Rows(a) !idrec, .Table.Rows(a) !nama,
                                Format(.Table.Rows(a) !QTY, "###,##0.00"), .Table.Rows(a) !Satuan,
                                Format(.Table.Rows(a) !PriceList, "###,##0"),
                                .Table.Rows(a) !isiSatSedang, .Table.Rows(a) !isiSatBesar)
            Next (a)
        End With

        DGView.Columns(0).ReadOnly = True
        ' Me.Cursor = Cursors.Default
        If DGView.RowCount <> 0 Then
            FrmMenuUtama.TSKeterangan.Text = DGView.Rows(0).Cells(0).Value
        End If
        DGView.Visible = True
        tCari.Enabled = True
        ' If DGView.Rows.Count <> 0 Then DGView.Focus()
    End Sub

    Private Sub Form_DaftarBarang_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        tCari.Text = ""
        Param1.Text = ""
        txtQuery.Text = ""
        'FrmMenuUtama.TSKeterangan.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
    End Sub

    Private Sub Form_DaftarBarang_Leave(sender As Object, e As EventArgs) Handles Me.Leave

    End Sub
End Class