Imports System.Data.SqlClient
Public Class Form_Backup
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim SQL As String

    Private Sub Form_Backup_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        Data_Record()
        isiCombo()
    End Sub

    Private Sub Data_Record()
        DGView.Rows.Clear()
        SQL = "Select nama, aktifyn from master..msconfig order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1

                DGView.Rows.Add(.Table.Rows(a) !Nama,
                    .Table.Rows(a) !AktifYN, "Active")
            Next (a)
        End With
    End Sub

    Private Sub isiCombo()
        cbasal.Items.Clear()
        SQL = "Select nama, aktifyn from master..msconfig order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                cbasal.Items.Add(.Table.Rows(a) !Nama)
            Next (a)
        End With
        txtTujuan.Text = "PrimaJaya" & Year(Now) + 1

    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        If txtTujuan.Text = cbasal.Text Then
            MsgBox("Asal dengan tujuan tidak boleh sama")
            Exit Sub
        End If
        If txtTujuan.Text = "" Then
            MsgBox("Database tujuan kosong")
            Exit Sub
        End If

        SQL = "if not exists (select 1 from sys.databases where name = '" & txtTujuan.Text & "') begin "
        SQL &= "create database " & txtTujuan.Text & "; end "
        Proses.ExecuteNonQuery(SQL)

        SQL = "select * into " & txtTujuan.Text & ".dbo.m_barang from m_barang"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_barang_foto from m_barang_foto"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_barang_lokasi from m_barang_lokasi"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_Barang_Supplier from m_Barang_Supplier"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_Barang_SupplierD from m_Barang_SupplierD"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_bayar from m_bayar"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_company from m_company"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_customer from m_customer"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_kategori from m_kategori"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_KeySearch from m_KeySearch"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_RakGudang from m_RakGudang"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_RegBon from m_RegBon"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_sales from m_sales"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_satuan from m_satuan"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_SetupKasir from m_SetupKasir"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_supplier from m_supplier"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_TableTransfer from m_TableTransfer"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_toko from m_toko"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_user from m_user"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_userakses from m_userakses"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_userlevel from m_userlevel"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.m_usermenu from m_usermenu"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_barang_supplier from mst_barang_supplier"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_barangAJ from mst_barangAJ"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_BarangKO from mst_BarangKO"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_barangPZ from mst_barangPZ"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_kategori from mst_kategori"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_Outlet from mst_Outlet"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.mst_satuan from mst_satuan"
        Proses.ExecuteNonQuery(SQL)

        SQL = "select * into " & txtTujuan.Text & ".dbo.t_BayarCustD from t_BayarCustD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_BayarCustH from t_BayarCustH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_KasirD from t_KasirD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_KasirH from t_KasirH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_KoreksiHargaBarang from t_KoreksiHargaBarang where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_KoreksiStock from t_KoreksiStock where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_ORD from t_ORD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_ORH from t_ORH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_pod from t_pod where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_poh from t_poh where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_ReturJualD from t_ReturJualD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_ReturJualH from t_ReturJualH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_SJD from t_SJD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_SJH from t_SJH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_SOD from t_SOD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_SOH from t_SOH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_TagihCustD from t_TagihCustD where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_TagihCustH from t_TagihCustH where 1=2"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.t_transaksi from t_transaksi where 1=2"
        Proses.ExecuteNonQuery(SQL)

        SQL = "select * into " & txtTujuan.Text & ".dbo.tmp_RptStock from tmp_RptStock"
        Proses.ExecuteNonQuery(SQL)
        SQL = "select * into " & txtTujuan.Text & ".dbo.tmp_transaksi from tmp_transaksi"
        Proses.ExecuteNonQuery(SQL)

        SQL = "update master..msconfig set aktifYN = 'N' where aktifYN = 'Y'"
        Proses.ExecuteNonQuery(SQL)
        SQL = "insert master..msconfig (nama, aktifYN) values ('" & txtTujuan.Text & "', 'Y')"
        Proses.ExecuteNonQuery(SQL)
        Data_Record()
        MsgBox("Finish")
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If e.ColumnIndex = 2 Then 'active
            SQL = "update master..msconfig set aktifYN = 'N' where aktifYN = 'Y'"
            Proses.ExecuteNonQuery(SQL)
            Dim tDB As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
            SQL = "update master..msconfig set aktifYN = 'Y' where nama = '" & tDB & "'"
            Proses.ExecuteNonQuery(SQL)
            Data_Record()
        ElseIf e.ColumnIndex = 3 Then 'delete
            Dim tDB As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
            If Microsoft.VisualBasic.Right(tDB, 4) = Today.Year().ToString Then
                MsgBox("Tahun ini tidak bisa didelete")
                Exit Sub
            End If
            If MessageBox.Show("Yakin delete?", "Pertanyaan", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                SQL = "delete from master..msconfig where nama = '" & tDB & "'"
                Proses.ExecuteNonQuery(SQL)
                SQL = "update master..msconfig set aktifYN = 'Y' where nama = 'PrimaJaya" & Today.Year.ToString & "'"
                Proses.ExecuteNonQuery(SQL)
                SQL = "alter database " & tDB & " set single_user with rollback immediate"
                Proses.ExecuteNonQuery(SQL)
                SQL = "drop database " & tDB
                Proses.ExecuteNonQuery(SQL)
                Data_Record()
                MsgBox("Finish")
            End If
        End If
    End Sub
End Class