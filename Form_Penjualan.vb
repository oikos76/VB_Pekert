Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Form_Penjualan
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim UserID As String = FrmMenuUtama.TsPengguna.Text
    Dim KodeToko As String = ""

    Dim NoMaxNota As String = "", NoAkhir As String = ""
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tCetak As Boolean

    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Protected CN As SqlConnection
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database
    Dim hapusSQL As String = String.Empty

    Dim SelectedItemIndex As Integer = -1, CmbJenisBayarIndex As Integer = 1
    Private satuanbrg() As String
    Private Sub HitungTotal()
        Dim sum = (From row As DataGridViewRow In DGRequest.Rows.Cast(Of DataGridViewRow)()
                   Select CDec(row.Cells(9).Value)).Sum
        sub_total.Text = Format(sum, "###,##0")
        If PsPPN.Text = "" Then PsPPN.Text = 0
        If PPN.Text = "" Then PPN.Text = 0
        If Discount.Text = "" Then Discount.Text = 0
        If PsDisc.Text = "" Or Trim(PsDisc.Text) = "0" Then     PsDisc.Text = 0
        Total.Text = Format(sum, "###,##0")

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
        DGRequest.Rows.Clear()
        TglJual.Value = Now
        TglJatuhTempo.Value = Now
        DP.Text = 0
        expedisi.Text = ""
        LamaHutang.Text = ""
        PsDisc.Text = 0
        Discount.Text = 0
        PsPPN.Text = 0
        PPN.Text = 0
        SubTotal.Text = 0
        Total.Text = 0
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Enabled = False
        Else
            cmdTambah.Enabled = tAktif
        End If
        If tEdit = False Then
            cmdEdit.Enabled = False
        Else
            cmdEdit.Enabled = tAktif
        End If
        If tHapus = False Then
            cmdHapus.Enabled = False
        Else
            cmdHapus.Enabled = tAktif
        End If

        cmdBatal.Enabled = Not tAktif
        cmdExit.Enabled = tAktif
        If tCetak = False Then
            cmdCetak.Enabled = False
        Else
            cmdCetak.Enabled = tAktif
        End If

        cmdSimpan.Visible = Not tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif
        tCari.Visible = tAktif
        Cari.Visible = tAktif
    End Sub

    Private Sub CekTable()
        SQL = "SELECT *  FROM information_schema.COLUMNS " &
             "WHERE TABLE_NAME = 't_SOD'  " &
             "  And column_name = 'HargaJualAwal' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "ALTER TABLE t_SOD ADD HargaJualAwal Money Default 0 "
            Proses.ExecuteNonQuery(SQL)
            SQL = "Update t_SOD set HargaJualAwal = Harga "
            Proses.ExecuteNonQuery(SQL)
        End If
    End Sub
    Private Sub Form_Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cektable()
        ClearTextBoxes()
        Dim lastdaymonth As Integer = Date.DaysInMonth(Date.Now.Year, Date.Now.Month)
        tglCari1.Value = New Date(Date.Now.Year, Date.Now.Month, "1")
        tglCari2.Value = New Date(Date.Now.Year, Date.Now.Month, lastdaymonth)
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        'DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        DGRequest.GridColor = Color.Red
        DGRequest.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGRequest.BackgroundColor = Color.LightGray
        DGRequest.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGRequest.DefaultCellStyle.SelectionForeColor = Color.White
        DGRequest.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGRequest.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGRequest.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGRequest.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView2.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        LAdd = False
        LEdit = False
        Kode_Toko.Text = FrmMenuUtama.Kode_Toko.Text
        KodeToko = Mid(Kode_Toko.Text, 4, 2)
        cmbJenisBayar.SelectedIndex = 1
        'TabControl1.Location = New System.Drawing.Point(87, 6)
        TabPage1.Text() = "Daftar Penjualan"

        TabControl1.SelectedTab = TabPage1
        TabControl1.TabPages.RemoveAt(1)
        TabControl1.SelectedTab = TabPage1

        Dim tID As String = ""
        If DGView.RowCount <> 0 Then
            tID = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            tID = ""
        End If
        DGView.Font = New Font("Arial", 10, FontStyle.Regular)
        DGView2.Font = New Font("Arial", 10, FontStyle.Regular)
        With Me.DGView.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With
        With Me.DGView2.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With
        SetupToolTip()
        Data_Record()
        tTambah = Proses.UserAksesTombol(UserID, "FAKTUR_PENJUALAN", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "FAKTUR_PENJUALAN", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "FAKTUR_PENJUALAN", "hapus")
        tCetak = Proses.UserAksesTombol(UserID, "FAKTUR_PENJUALAN", "laporan")
        If FrmMenuUtama.CompCode.Text = "KM" Then
            Label18.Visible = True
            PsDisc.Visible = True
            DGRequest.Columns(7).Width = 80
            DGRequest.Columns(8).Width = 90
            DGView2.Columns(7).Width = 80
            DGView2.Columns(8).Width = 90
        Else
            Label18.Visible = False
            PsDisc.Visible = False
            DGRequest.Columns(7).Width = 5
            DGRequest.Columns(8).Width = 5
            DGView2.Columns(7).Width = 5
            DGView2.Columns(8).Width = 5
        End If
        AturTombol(True)
    End Sub

    Public Sub SetupToolTip()
        With Me.ToolTip1
            .AutomaticDelay = 0
            .AutoPopDelay = 30000
            .BackColor = System.Drawing.Color.AntiqueWhite
            .InitialDelay = 50
            .IsBalloon = False
            .ReshowDelay = 50
            .ShowAlways = True
            .Active = False
            .Active = True
            .SetToolTip(Me.KreditLimit, "Batas Limit Hutang")
            .SetToolTip(Me.TotHutang, "Total hutang yang belum lunas saat ini.")

        End With
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)

        TabControl1.TabPages.Insert(1, TabPage2)
        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.SelectedTab = TabPage2

        ClearTextBoxes()
        Kode_Toko.Text = FrmMenuUtama.Kode_Toko.Text
        NamaToko.Text = FrmMenuUtama.Nama_Toko.Text
        Kode_Toko.ReadOnly = False

        TglJual.Focus()
        cmdSimpan.Enabled = True
        KodeBrg.Text = "<Kode_Brg>"
        KodeBrg.ForeColor = Color.Gray
        KodeBrg.BackColor = Color.LightGoldenrodYellow
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
        cmdSimpan.Visible = tEdit
        Isi_Data()
        isiHutangCustomer()
        TotHutang.Text = Format((TotHutang.Text * 1) + (Total.Text * 1), "###,##0")

        Dim dbcek As String = ""
        SQL = "select id_rec from t_TagihCustD where no_nota = '" & IDRec.Text & "' and aktifYN = 'Y' "
        dbcek = Proses.ExecuteSingleStrQuery(SQL)
        If dbcek <> "" Then
            MsgBox("Nota ini sudah di buatkan tanda terima, tidak bisa edit/hapus." & vbCrLf &
                "Id Tagihan : " & dbcek, vbCritical + vbOKOnly, "Warning !")
            cmdSimpan.Visible = False
        End If
        Dim maxDay As Double = 0
        SQL = "Select jumlahHari 
            from m_customer inner join m_bayar on idbayar = m_bayar.IDRec and m_bayar.AktifYN = 'Y'
           where m_customer.AktifYN = 'Y'  
             and m_customer.idrec = '" & Id_Cust.Text & "' "
        maxDay = Proses.ExecuteSingleDblQuery(SQL)
        LamaHutang.Text = maxDay
        TabControl1.TabPages.Insert(1, TabPage2)
        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Isi_Data()
        DGView.Enabled = False
        SQL = "SELECT t_SOH.IdRec, t_SOH.AlamatKirim, TglPenjualan, idcust, m_customer.kreditlimit, " &
            "         m_Customer.nama Customer, tgljatuhtempo, carabayar, SubTotal, TotalSales, m_Customer.expedisi, " &
            "         Keterangan, PsDisc, Disc, PsPPN, PPN, DP, PostingYN, t_SOH.idsales, namasales " &
            " FROM t_SOH inner join m_customer on idcust = m_customer.idrec " &
            " Left join m_sales on t_SOH.IDSales = m_sales.idrec " &
            "WHERE t_SOH.AktifYN = 'Y' " &
            " AND t_SOH.IDRec = '" & IDRec.Text & "' " &
            "Order by TglPenjualan desc, IdRec Desc "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            AlamatKirim.Text = IIf(IsDBNull(dbTable.Rows(0)!AlamatKirim), "", dbTable.Rows(0)!AlamatKirim)
            TglJual.Value = dbTable.Rows(0)!tglpenjualan
            Id_Cust.Text = dbTable.Rows(0)!idcust
            Customer.Text = dbTable.Rows(0) !Customer
            expedisi.Text = dbTable.Rows(0) !expedisi
            Keterangan.Text = dbTable.Rows(0)!Keterangan
            TglJatuhTempo.Value = dbTable.Rows(0)!tgljatuhtempo
            cmbJenisBayar.Text = dbTable.Rows(0)!carabayar
            sub_total.Text = Format(dbTable.Rows(0)!SubTotal, "###,##0")
            PsPPN.Text = Format(dbTable.Rows(0)!PsPPN, "###,##0")
            PPN.Text = Format(dbTable.Rows(0)!PPN, "###,##0")
            PsDisc.Text = Format(dbTable.Rows(0)!PsDisc, "###,##0")
            Discount.Text = Format(dbTable.Rows(0)!disc, "###,##0")
            Total.Text = Format(dbTable.Rows(0)!TotalSales, "###,##0")
            DP.Text = Format(dbTable.Rows(0)!dp, "###,##0")
            If dbTable.Rows(0)!PostingYN.ToString = "Y" Then cmdSimpan.Enabled = False
            IDSales.Text = dbTable.Rows(0)!idsales
            NamaSales.Text = dbTable.Rows(0)!namasales.ToString

            KreditLimit.Text = Format(dbTable.Rows(0)!kreditLimit, "###,##0")
            'Dim Bayar As Double = 0, Penjualan As Double = 0
            ''kutu kupret
            'SQL = "select sum(nilai_bayar)
            '         from t_BayarCustH H 
            '              inner join t_BayarCustD D on 
            '                    idrec = id_rec 
            '                    and h.aktifYN = 'Y' and d.AktifYN = 'Y'
            '                    and h.kode_toko = d.kode_toko  
            '                    and h.kode_toko = '" & Kode_Toko.Text & "'    
            '                    and id_cust = '" & Id_Cust.Text & "'"
            'Bayar = Proses.ExecuteSingleDblQuery(SQL)


            'SQL = "Select sum(totalsales-dp) from t_SOH " &
            '    "where idcust =  '" & Id_Cust.Text & "' " &
            '    "  And aktifyn = 'Y' and kode_toko = '" & Kode_Toko.Text & "'  "
            'Penjualan = Proses.ExecuteSingleDblQuery(SQL)

            'TotHutang.Text = Format(Bayar - Penjualan, "###,##0")
        End If
        'Customer.Text = Proses.ExecuteSingleStrQuery("Select nama from m_Customer where idrec = '" & Id_Cust.Text & "' ")
        DGView2.Rows.Clear()
        DGRequest.Rows.Clear()
        DGView2.Visible = False
        SQL = "SELECT ID_Rec, KodeBrg, NamaBrg, QtyB, SatB, QTY, Satuan, hargajualawal, " &
            " PsDisc1, Disc1, harga, sub_total, flag, HargaSatuan_Asli, HargaModal " &
            " FROM t_SOD  " &
            "WHERE ID_Rec = '" & IDRec.Text & "' " &
            "  And t_SOD.AktifYN = 'Y' " &
            "Order By NoUrut "
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView2.Rows.Add(.Table.Rows(a)!KodeBrg,
                    .Table.Rows(a)!NamaBrg,
                    Format(.Table.Rows(a)!qtyb, "###,##0.00"),
                   .Table.Rows(a)!SatB,
                    Format(.Table.Rows(a)!qty, "###,##0.00"),
                   .Table.Rows(a)!Satuan,
                    Format(.Table.Rows(a)!hargajualawal, "###,##0"),
                    Format(.Table.Rows(a)!psdisc1, "###,##0"),
                    Format(.Table.Rows(a)!harga, "###,##0"),
                    Format(.Table.Rows(a)!sub_total, "###,##0"))
                DGRequest.Rows.Add(.Table.Rows(a)!KodeBrg,
                    .Table.Rows(a)!NamaBrg,
                    Format(.Table.Rows(a)!qtyb, "###,##0.00"),
                   .Table.Rows(a)!SatB,
                    Format(.Table.Rows(a)!qty, "###,##0.00"),
                   .Table.Rows(a)!Satuan,
                    Format(.Table.Rows(a)!hargajualawal, "###,##0"),
                    Format(.Table.Rows(a)!psdisc1, "###,##0"),
                    Format(.Table.Rows(a)!harga, "###,##0"),
                    Format(.Table.Rows(a)!sub_total, "###,##0"), "Hapus",
                   .Table.Rows(a)!flag,
                    Format(.Table.Rows(a)!hargaSatuan_Asli, "##0"),
                    Format(.Table.Rows(a)!hargaModal, "##0"))
            Next (a)
        End With
        DGView2.Visible = True
        HitungTotal()
        DGView.Enabled = True
    End Sub

    Private Sub Data_Record()
        Dim a As Integer, mBayar As String = "", mKondisi As String = ""
        btnCari.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        If tCari.Text <> "" Then
            mKondisi = " and t_SOH.idrec like '%" & tCari.Text & "%' "
        End If
        If tCustomer.Text <> "" Then
            mKondisi = mKondisi & " and m_customer.nama like '%" & tCustomer.Text & "%' "
        End If
        SQL = "Select t_SOH.IdRec, TglPenjualan, m_Customer.nama Customer, m_sales.namasales, " &
            "TotalSales, Keterangan, DP, carabayar, tgljatuhtempo, TTerimaYN, PrintCTR  " &
            "From t_SOH inner join m_customer on idcust = m_customer.idrec " &
            "left join m_sales on t_SOH.idsales = m_sales.idrec " &
            "Where t_SOH.AktifYN = 'Y' " & mKondisi & " " &
            "  And convert(varchar(8),tglpenjualan,112) Between '" & Format(tglCari1.Value, "yyyyMMdd") & "' " &
            "  And '" & Format(tglCari2.Value, "yyyyMMdd") & "' " &
            "Order by TglPenjualan desc, IdRec Desc "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()

        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                mBayar = .Table.Rows(a) !carabayar

                DGView.Rows.Add(.Table.Rows(a)!IDRec,
                    Format(.Table.Rows(a)!TglPenjualan, "dd-MM-yyyy"),
                    .Table.Rows(a)!Customer,
                    .Table.Rows(a)!namasales,
                    Format(.Table.Rows(a)!TotalSales, "###,##0"), mBayar,
                    Format(.Table.Rows(a)!dp, "###,##0"),
                    IIf(.Table.Rows(a)!TTerimaYN.ToString = "", "N", .Table.Rows(a)!TTerimaYN.ToString),
                    Format(.Table.Rows(a)!printCtr, "###,##0"))
            Next (a)
        End With
        DGView2.Rows.Clear()
        Me.Cursor = Cursors.Default
        btnCari.Enabled = True
    End Sub

    Private Sub Id_Cust_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Id_Cust.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama, idrec, alamat1, kreditlimit, IdSales, expedisi From m_customer " &
              " Where IDRec = '" & Id_Cust.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Id_Cust.Text = dbTable.Rows(0) !idrec
                Customer.Text = dbTable.Rows(0) !nama
                AlamatKirim.Text = dbTable.Rows(0)!alamat1
                expedisi.Text = dbTable.Rows(0)!expedisi
                KreditLimit.Text = Format(dbTable.Rows(0) !kreditlimit, "###,##0")
                IDSales.Text = dbTable.Rows(0) !idSales
                SQL = "Select NamaSales From M_Sales where IdRec = '" & IDSales.Text & "' and aktifYN = 'Y' "
                Dim nSales As String = ""
                nSales = Proses.ExecuteSingleStrQuery(SQL)
                If nSales = "" Then
                    MsgBox("Sales tidak ada di database", vbCritical + vbOKOnly, ".:Warning!")
                    IDSales.Focus()
                    NamaSales.Text = ""
                    IDSales.Text = ""
                Else
                    NamaSales.Text = nSales
                End If
                If IDSales.Text <> "" Then
                    KodeBrg.Focus()
                Else
                    IDSales.Focus()
                End If
            Else
                Form_Daftar.txtQuery.Text = "Select idrec, kode_toko, nama, alamat1, alamat2, Kota, Prov, KodePos, Phone " &
                    " From m_Customer " &
                    "Where AktifYN = 'Y' " &
                    "  And ( idrec Like '%" & Id_Cust.Text & "%' or nama Like '" & Id_Cust.Text & "%') " &
                    "Order By Nama "
                Form_Daftar.Text = "Daftar Customer"
                Form_Daftar.ShowDialog()

                Id_Cust.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nama, idrec, alamat1, kreditlimit,idSales, expedisi From m_customer " &
                   " Where IDRec = '" & Id_Cust.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Id_Cust.Text = dbTable.Rows(0) !idrec
                    Customer.Text = dbTable.Rows(0) !nama
                    AlamatKirim.Text = dbTable.Rows(0)!alamat1
                    expedisi.Text = dbTable.Rows(0)!expedisi
                    KreditLimit.Text = Format(dbTable.Rows(0) !kreditlimit, "###,##0")
                    IDSales.Text = dbTable.Rows(0) !idSales
                    SQL = "Select NamaSales From M_Sales where IdRec = '" & IDSales.Text & "' and aktifYN = 'Y' "
                    Dim nSales As String = ""
                    nSales = Proses.ExecuteSingleStrQuery(SQL)
                    If nSales = "" Then
                        MsgBox("Sales tidak ada di database", vbCritical + vbOKOnly, ".:Warning!")
                        IDSales.Focus()
                        NamaSales.Text = ""
                        IDSales.Text = ""
                    Else
                        NamaSales.Text = nSales
                    End If
                    If IDSales.Text <> "" Then
                        KodeBrg.Focus()
                    Else
                        IDSales.Focus()
                    End If
                Else
                    Customer.Text = ""
                    expedisi.Text = ""
                    KreditLimit.Text = 0
                    TotHutang.Text = 0
                    Id_Cust.Focus()
                End If
                Proses.CloseConn()
            End If
            Dim cekfaktur As Boolean = cekFaktur120()
            If cekfaktur = False Then
                Id_Cust.Focus()
                Exit Sub
            Else
                IDSales.Focus()
            End If
            isiHutangCustomer()
        End If
    End Sub
    Private Sub isiHutangCustomer()
        Dim Bayar As Double = 0, maxDay As Double = 0

        SQL = "Update t_SOH set NilaiRetur = TotalRetur
                From t_SOH inner join  (select h.nonota, sum( TotalRetur) TotalRetur
	                  From t_ReturJualH h Inner Join t_SOH on h.NoNota = t_SOH.IdRec 
                           and t_SOH.IdCust = '" & Id_Cust.Text & "'
	                 Where h.AktifYN = 'Y' Group By h.NoNota) R on t_SOH.IdRec = R.nonota "
        Proses.ExecuteNonQuery(SQL)

        SQL = "Update t_SOH set MBayar = bayar
                From t_SOH Inner Join (Select no_nota, sum(nilai_bayar) Bayar
                     From t_BayarCustH H Inner Join t_BayarCustD D on          
	                      idrec = id_rec  and h.aktifYN = 'Y' and d.AktifYN = 'Y' 
                          and id_cust = '" & Id_Cust.Text & "'
                 Group By no_nota) R on t_SOH.IdRec = r.no_nota "
        Proses.ExecuteNonQuery(SQL)

        SQL = "Select SUM(isnull(mbayar,0) - TotalSales + NilaiRetur) Bayar 
                     From t_SOH where  AktifYN = 'Y' and idcust = '" & Id_Cust.Text & "'"
        Bayar = Proses.ExecuteSingleDblQuery(SQL)
        TotHutang.Text = Format(Bayar, "###,##0")
        Proses.CloseConn()
    End Sub
    Private Sub Id_Cust_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Id_Cust.TextChanged
        If Len(Id_Cust.Text) < 1 Then
            Customer.Text = ""
            KreditLimit.Text = 0
            TotHutang.Text = 0
            expedisi.Text = ""
            LamaHutang.Text = ""
            IDSales.Text = ""
            IDSales.Text = ""
        End If
    End Sub

    Function cekFaktur120() As Boolean
        Dim maxDay As Double = 0
        If Trim(Id_Cust.Text) = "" Then
            MessageBox.Show("Kode Customer belum di isi..!!", "Empty Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        SQL = "Select jumlahHari 
            from m_customer inner join m_bayar on idbayar = m_bayar.IDRec and m_bayar.AktifYN = 'Y'
           where m_customer.AktifYN = 'Y'  
             and m_customer.idrec = '" & Id_Cust.Text & "' "
        maxDay = Proses.ExecuteSingleDblQuery(SQL)
        If maxDay > 0 Then
            Dim dttable As DataTable
            SQL = "Select datediff(day, convert(date, TglPenjualan), convert(date, getdate())) , " &
              "    tglPenjualan, a.IdRec, a.SubTotal, nilai_bayar" &
              " From t_SOH a left join t_bayarCustD b on a.idrec = b.no_nota " &
              "Where IdCust = '" & Id_Cust.Text & "' and a.AktifYN = 'Y' " &
              "   and datediff(day, convert(date, TglPenjualan), convert(date, getdate())) >= " & maxDay & " " &
              "   And SubTotal > isnull(nilai_bayar,0) " &
              " order by TglPenjualan "
            dttable = Proses.ExecuteQuery(SQL)
            If dttable.Rows.Count > 0 Then
                Dim NoFaktur As String = ""
                For b = 0 To dttable.Rows.Count - 1
                    SQL = " select coalesce(sum(nilai_bayar),0) mbayar, coalesce(max(nilai_invoice), 0) ninvoice
                         From t_BayarCustD 
                        Where no_nota = '" & dttable.Rows(b) !IdRec & "'
                          And aktifYN = 'Y' "
                    dbTable = Proses.ExecuteQuery(SQL)
                    If dbTable.Rows(0) !mbayar = 0 Then
                        NoFaktur += dttable.Rows(b) !IdRec + " " + Format(dttable.Rows(b) !TglPenjualan, "dd MMM yyyy") + vbCrLf
                    ElseIf dbTable.Rows(0) !mbayar < dbTable.Rows(0) !ninvoice Then
                        NoFaktur += dttable.Rows(b) !IdRec + " " + Format(dttable.Rows(b) !TglPenjualan, "dd MMM yyyy") + vbCrLf
                    End If
                Next b
                If Trim(NoFaktur) = "" Then
                    LamaHutang.Text = ""
                    Return True
                Else
                    Dim tblLogin As New DataTable
                    MsgBox("Customer ini memiliki faktur yang belum lunas lebih dari batas lama bayar." & vbCrLf &
                       NoFaktur, vbCritical + vbOKOnly, ".:Warning !")
                    FrmMenuUtama.TSKeterangan.Text = ""
                    Form_LoginMenu.PswTxt.Text = ""
                    FrmMenuUtama.UserLoginMenu.Text = ""
                    FrmMenuUtama.PasswordLoginMenu.Text = ""
                    Form_LoginMenu.ShowDialog()
                    Dim ans As String = FrmMenuUtama.PasswordLoginMenu.Text
                    Dim Acak As New Crypto
                    Dim encryptpassword As String = ""
                    If Trim(ans) = "" Then
                        MessageBox.Show("Password belum di isi..!!", "Empty Passowrd", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LamaHutang.Text = maxDay
                        Return False
                    End If
                    encryptpassword = Acak.Encrypt(Trim(ans))
                    tblLogin = Proses.ExecuteQuery("Select * From m_User " &
                                                    "Where UserID = '" & FrmMenuUtama.UserLoginMenu.Text & "' " &
                                                    "  and password ='" & encryptpassword & "'")
                    If tblLogin.Rows.Count = 0 Then
                        MessageBox.Show("Password salah atau user name tidak terdaftar..!!", ".:Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LamaHutang.Text = maxDay
                        Return False
                    End If
                    Dim akses As Boolean = Proses.UserAksesMenu(FrmMenuUtama.UserLoginMenu.Text, "MAX_LAMA_BAYAR")
                    If Not akses Then
                        MessageBox.Show("User login ANDA tidak mempunyai akses untuk menyetujui transaksi yang melebihi maximum bayar !", ".:Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LamaHutang.Text = maxDay
                        Return False
                    Else
                        LamaHutang.Text = ""
                        Return True
                    End If
                End If
            Else
                LamaHutang.Text = ""
                Return True
            End If
        Else
            LamaHutang.Text = ""
            Return True
        End If
    End Function

    Private Sub DGRequest_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGRequest.CellEndEdit
        Dim tID As String = ""
        Dim cRow As Integer = DGRequest.CurrentCell.RowIndex
        Dim cCol As Integer = DGRequest.CurrentCell.ColumnIndex
        Dim QTY As Double = 0, tQTYB As Double = 0, tNo As Integer = 0
        Dim IsiUnit As Double = 0, tSatuan As String = ""
        Dim tHarga As Double = 0, Pengali As Integer = 0
        Dim tPsDisc As Double = 0, tDisc As Double = 0
        Dim tSubTot As Double = 0
        tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
        KodeToko = Mid(Kode_Toko.Text, 4, 2)
        If cCol = 0 Then
            SQL = "Select IDRec, Nama, Satuan, IsiSatB, isiSatT, SatuanB, SatuanT, " &
                "         Stock" & KodeToko & " As QTY, PriceList, PriceList2, PriceList3, HPP, HargaBeli " &
                "    From M_Barang " &
                "   Where AktifYN = 'Y' " &
                "     And idRec = '" & tID & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0) !idrec
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0) !nama
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 1
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0) !SatuanB
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = dbTable.Rows(0) !IsiSatB
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = dbTable.Rows(0) !Satuan
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(0, "###,##0") 'PsDisc
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value = Format(0, "###,##0") 'isc

                isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatB), 0, dbTable.Rows(0) !isisatB)
                IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatT), 0, dbTable.Rows(0) !isisatT)
                SatB.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanB), "", dbTable.Rows(0) !satuanB)
                SatT.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanT), "", dbTable.Rows(0) !satuanT)
                SatK.Text = IIf(IsDBNull(dbTable.Rows(0) !satuan), "", dbTable.Rows(0) !satuan)

                HargaSatuan.Text = Format(dbTable.Rows(0) !PriceList3, "###,##0")
                harga1.Text = Format(dbTable.Rows(0)!PriceList, "###,##0")
                harga2.Text = Format(dbTable.Rows(0)!PriceList2, "###,##0")
                harga3.Text = Format(dbTable.Rows(0)!PriceList3, "###,##0")

                HargaModal.Text = dbTable.Rows(0) !HargaBeli 'dbTable.Rows(0) !HPP
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = HargaSatuan.Text
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = HargaSatuan.Text
                SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")
            Else
                Form_DaftarBarang.kode_toko.Text = Kode_Toko.Text
                Form_DaftarBarang.tCari.Text = tID
                Form_DaftarBarang.JenisTR.Text = "PENJUALAN"
                Form_DaftarBarang.Cari()
                Form_DaftarBarang.ShowDialog()
                tID = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""

                If Trim(tID) <> "" Then
                    SQL = "Select IDRec, Nama, Satuan, IsiSatB, isiSatT, SatuanB, SatuanT, " &
                        "         Stock" & KodeToko & " as QTY,  PriceList, PriceList2, PriceList3, HPP, HargaBeli  " &
                        "    From M_Barang " &
                        "   Where AktifYN = 'Y' " &
                        "     And idRec = '" & tID & "' "
                    dbTable = Proses.ExecuteQuery(SQL)
                    If dbTable.Rows.Count <> 0 Then
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0)!idrec
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0)!nama
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 1
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0)!SatuanB
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = dbTable.Rows(0)!IsiSatB
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = dbTable.Rows(0)!Satuan
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(0, "###,##0") 'PsDisc
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value = Format(0, "###,##0") 'isc

                        isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0)!isisatB), 0, dbTable.Rows(0)!isisatB)
                        IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0)!isisatT), 0, dbTable.Rows(0)!isisatT)
                        SatB.Text = IIf(IsDBNull(dbTable.Rows(0)!satuanB), "", dbTable.Rows(0)!satuanB)
                        SatT.Text = IIf(IsDBNull(dbTable.Rows(0)!satuanT), "", dbTable.Rows(0)!satuanT)
                        SatK.Text = IIf(IsDBNull(dbTable.Rows(0)!satuan), "", dbTable.Rows(0)!satuan)

                        HargaSatuan.Text = Format(dbTable.Rows(0) !PriceList3, "###,##0")
                        harga1.Text = Format(dbTable.Rows(0)!PriceList, "###,##0")
                        harga2.Text = Format(dbTable.Rows(0)!PriceList2, "###,##0")
                        harga3.Text = Format(dbTable.Rows(0)!PriceList3, "###,##0")

                        HargaModal.Text = dbTable.Rows(0) !HargaBeli 'dbTable.Rows(0)!HPP
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = HargaSatuan.Text
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = HargaSatuan.Text
                        SendKeys.Send("{up}")
                        SendKeys.Send("{right}")
                        SendKeys.Send("{right}")
                    Else
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                        SendKeys.Send("{up}")
                    End If
                Else
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                    SendKeys.Send("{up}")
                End If
            End If
        ElseIf cCol = 1 Then 'Nama Brg
        ElseIf cCol = 2 Then 'QTY_B
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                QTY = 0
                Exit Sub
            Else
                QTY = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If QTY = 0 Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(QTY * 1, "###,##0")
                End If
            End If
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(6).Value) Then
                ' MsgBox((DGRequest.Rows(e.RowIndex).Cells(6).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(6).Value = 0
                SendKeys.Send("{up}")
                tHarga = 0
                Exit Sub
            Else
                tHarga = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value * 1
            End If
            tSatuan = UCase(DGRequest.Rows(e.RowIndex).Cells(3).Value)
            If tSatuan = SatK.Text Then
                Pengali = 1
                tHarga = harga1.Text
            ElseIf tSatuan = SatT.Text Then
                Pengali = IsiSatT.Text * 1
                tHarga = harga2.Text
            ElseIf tSatuan = SatB.Text Then
                Pengali = isiSatB.Text * 1
                tHarga = harga3.Text
            End If
            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format(QTY * tHarga, "###,##0")
            tSatuan = UCase(DGRequest.Rows(e.RowIndex).Cells(3).Value)

            DGRequest.Rows(e.RowIndex).Cells(4).Value = Format(QTY * Pengali, "###,##0")
            HitungTotal()
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
        ElseIf cCol = 3 Then  'Satuan_B  
            tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
            If DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty Then
                tSatuan = CariSatuan(tID)
                SendKeys.Send("{up}")
                'Exit Sub
            Else
                tSatuan = UCase(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                If tSatuan = UCase(SatK.Text) Then
                    tSatuan = SatK.Text
                ElseIf tSatuan = UCase(SatT.Text) Then
                    tSatuan = SatT.Text
                ElseIf tSatuan = UCase(SatB.Text) Then
                    tSatuan = SatB.Text
                Else
                    tSatuan = CariSatuan(tID)
                End If
            End If
            DGRequest.Rows(e.RowIndex).Cells(3).Value = tSatuan
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(2).Value) Then
                ' MsgBox((DGRequest.Rows(e.RowIndex).Cells(6).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(6).Value = 0
                SendKeys.Send("{up}")
                QTY = 0
                Exit Sub
            Else
                QTY = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value * 1
            End If
            If tSatuan = SatK.Text Then
                Pengali = 1
                tHarga = harga1.Text
            ElseIf tSatuan = SatT.Text Then
                Pengali = IsiSatT.Text * 1
                tHarga = harga2.Text
            ElseIf tSatuan = SatB.Text Then
                Pengali = isiSatB.Text * 1
                tHarga = harga3.Text
            End If
            'DGRequest.Rows(e.RowIndex).Cells(2).Value = Format(QTY, "###,##0")
            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format(QTY * tHarga, "###,##0")
            DGRequest.Rows(e.RowIndex).Cells(4).Value = Format(QTY * Pengali, "###,##0")
            DGRequest.Rows(e.RowIndex).Cells(6).Value = tHarga
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            HitungTotal()
        ElseIf cCol = 4 Then  'QTY K
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                QTY = 0
                Exit Sub
            Else
                QTY = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If QTY = 0 Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(QTY * 1, "###,##0")
                End If
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(6).Value) Then
                ' MsgBox((DGRequest.Rows(e.RowIndex).Cells(6).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(6).Value = 0
                SendKeys.Send("{up}")
                tHarga = 0
                Exit Sub
            Else
                tHarga = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value * 1
            End If

            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format((QTY / isiSatB.Text) * tHarga, "###,##0")

            DGRequest.Rows(e.RowIndex).Cells(2).Value = Format(QTY / isiSatB.Text, "###,##0")
            HitungTotal()
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
        ElseIf cCol = 5 Then  'Satuan_K not_yet
        ElseIf cCol = 6 Then  'Harga
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                tHarga = 0
                Exit Sub
            Else
                tHarga = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If tHarga = 0 Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(tHarga * 1, "###,##0")
                End If
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(2).Value) Then
                DGRequest.Rows(e.RowIndex).Cells(2).Value = 0
                SendKeys.Send("{up}")
                QTY = 0
                Exit Sub
            Else
                QTY = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value * 1
            End If
            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format(QTY * tHarga, "###,##0")
            HitungTotal()
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
        ElseIf cCol = 7 Then  'PsDisc
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                tPsDisc = 0
                Exit Sub
            Else
                tPsDisc = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If tPsDisc = 0 Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(tPsDisc * 1, "###,##0")
                End If
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(6).Value) Then
                ' MsgBox((DGRequest.Rows(e.RowIndex).Cells(6).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(6).Value = 0
                SendKeys.Send("{up}")
                tHarga = 0
                Exit Sub
            Else
                tHarga = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value * 1
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(2).Value) Then
                DGRequest.Rows(e.RowIndex).Cells(2).Value = 0
                SendKeys.Send("{up}")
                QTY = 0
                Exit Sub
            Else
                QTY = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value * 1
            End If
            DGRequest.Rows(e.RowIndex).Cells(8).Value = Format(tPsDisc * tHarga / 100, "###,##0")
            tSubTot = (tHarga - (tPsDisc * tHarga / 100)) * QTY
            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format(tSubTot, "###,##0")
            HitungTotal()
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
        ElseIf cCol = 8 Then  'Disc
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                tDisc = 0
                Exit Sub
            Else
                tDisc = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If tDisc = 0 Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(tDisc * 1, "###,##0")
                End If
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(6).Value) Then
                ' MsgBox((DGRequest.Rows(e.RowIndex).Cells(6).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(6).Value = 0
                SendKeys.Send("{up}")
                tHarga = 0
                Exit Sub
            Else
                tHarga = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value * 1
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(2).Value) Then
                DGRequest.Rows(e.RowIndex).Cells(2).Value = 0
                SendKeys.Send("{up}")
                QTY = 0
                Exit Sub
            Else
                QTY = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value * 1
            End If
            DGRequest.Rows(e.RowIndex).Cells(7).Value = Format((tDisc / tHarga * 100), "###,##0.00")
            tSubTot = (tHarga - tDisc) * QTY
            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format(tSubTot, "###,##0")
            HitungTotal()
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")

        ElseIf cCol = 9 Then  'SubTotal
        End If
    End Sub

    Private Sub DGRequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGRequest.KeyDown
        If DGRequest.Rows.Count = 0 Then Exit Sub
        Dim cRow As Integer = DGRequest.CurrentCell.RowIndex, mGudang As String = ""
        Dim cCol As Integer = DGRequest.CurrentCell.ColumnIndex
        Dim tID As String = "", tUnit As String = "", isiUnit As Double = 0, SatuanB As String = ""
        Dim QTYB As Integer = 0, QTYK As Integer = 0, tQTYK As Integer = 0, tIdRap As String = ""
        Dim tNo As Integer = 0, NoLama As Integer = 0, tJenis As String = ""
        If e.KeyCode = Keys.Enter Then
            If cmbJenisBayar.Text = "HUTANG" Then
                If Format(TglJual.Value, "YYmmdd") = Format(TglJatuhTempo.Value, "YYmmdd") Then
                    MsgBox("Tgl Jual tidak boleh sama dengan tgl jatuh tempo " & vbCrLf & "karena jenis pembayarannya hutang ", vbCritical, ".:Warning!")
                    TglJatuhTempo.Focus()
                    Exit Sub
                End If
            End If
            KodeToko = Mid(Kode_Toko.Text, 4, 2)
            tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
            If cCol = 0 Then
                SQL = "Select IDRec, Nama, Satuan, IsiSatB, isiSatT, SatuanB, SatuanT, " &
                "         Stock" & KodeToko & " as QTY, PriceList, PriceList2, PriceList3, HPP, HargaBeli " &
                "    From M_Barang " &
                "   Where AktifYN = 'Y' " &
                "     And idRec = '" & tID & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0) !idrec
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0) !nama
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 1
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0) !SatuanB
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = dbTable.Rows(0) !IsiSatB
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = dbTable.Rows(0) !Satuan
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(0, "###,##0") 'PsDisc
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value = Format(0, "###,##0") 'isc
                    isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatB), 0, dbTable.Rows(0) !isisatB)
                    IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatT), 0, dbTable.Rows(0) !isisatT)
                    SatB.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanB), "", dbTable.Rows(0) !satuanB)
                    SatT.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanT), "", dbTable.Rows(0) !satuanT)
                    SatK.Text = IIf(IsDBNull(dbTable.Rows(0) !satuan), "", dbTable.Rows(0) !satuan)

                    HargaSatuan.Text = Format(dbTable.Rows(0) !PriceList, "###,##0")
                    harga1.Text = Format(dbTable.Rows(0)!PriceList, "###,##0")
                    harga2.Text = Format(dbTable.Rows(0)!PriceList2, "###,##0")
                    harga3.Text = Format(dbTable.Rows(0)!PriceList3, "###,##0")

                    HargaModal.Text = dbTable.Rows(0) !HargaBeli 'dbTable.Rows(0)!HPP 
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = HargaSatuan.Text
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = HargaSatuan.Text
                    SendKeys.Send("{up}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")

                Else
                    Form_DaftarBarang.kode_toko.Text = Kode_Toko.Text
                    Form_DaftarBarang.tCari.Text = tID
                    Form_DaftarBarang.JenisTR.Text = "PENJUALAN"
                    Form_DaftarBarang.Cari()
                    Form_DaftarBarang.ShowDialog()
                    tID = FrmMenuUtama.TSKeterangan.Text
                    FrmMenuUtama.TSKeterangan.Text = ""

                    If Trim(tID) <> "" Then
                        SQL = "Select IDRec, Nama, Satuan, IsiSatB, isiSatT, SatuanB, SatuanT, " &
                        "         Stock" & KodeToko & " as QTY, hargabeli " &
                        "    From M_Barang " &
                        "   Where AktifYN = 'Y' " &
                        "     And idRec = '" & tID & "' "
                        dbTable = Proses.ExecuteQuery(SQL)
                        If dbTable.Rows.Count <> 0 Then
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0)!idrec
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0)!nama
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 1
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0)!SatuanB
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = dbTable.Rows(0)!IsiSatB
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = dbTable.Rows(0)!Satuan
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(0, "###,##0") 'PsDisc
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value = Format(0, "###,##0") 'isc
                            If IsDBNull(dbTable.Rows(0)!hargabeli) Then
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(0, "###,##0") 'harga
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(0, "###,##0") 'subtotal
                            Else
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0)!hargabeli, "###,##0")
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(dbTable.Rows(0)!hargabeli, "###,##0")
                            End If
                            isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0)!isisatB), 0, dbTable.Rows(0)!isisatB)
                            IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0)!isisatT), 0, dbTable.Rows(0)!isisatT)
                            SatB.Text = IIf(IsDBNull(dbTable.Rows(0)!satuanB), "", dbTable.Rows(0)!satuanB)
                            SatT.Text = IIf(IsDBNull(dbTable.Rows(0)!satuanT), "", dbTable.Rows(0)!satuanT)
                            SatK.Text = IIf(IsDBNull(dbTable.Rows(0)!satuan), "", dbTable.Rows(0)!satuan)
                            SendKeys.Send("{up}")
                            SendKeys.Send("{right}")
                            SendKeys.Send("{right}")
                        Else
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                            SendKeys.Send("{up}")
                        End If
                    Else
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                        SendKeys.Send("{up}")
                    End If
                End If
            ElseIf cCol = 2 Then
                'SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")
            ElseIf cCol = 4 Then
                SendKeys.Send("{home}")
                SendKeys.Send("{down}")
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            If Not DGRequest.CurrentRow.IsNewRow Then
                DGRequest.Rows.Remove(DGRequest.CurrentRow)
                SendKeys.Send("{home}")
            Else
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = ""
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 0
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = ""
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = 0
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = 0
            End If
        ElseIf e.KeyData = Keys.Tab Then
            MsgBox("Silakan tekan tombol enter!", MsgBoxStyle.Critical + vbInformation, "Jangan pakai tab!")
            SendKeys.Send("{home}")
        End If
    End Sub

    Private Sub cmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatal.Click
        If hapusSQL <> "" Then
            Proses.ExecuteNonQuery(hapusSQL)
            hapusSQL = ""
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        TabControl1.TabPages.Insert(0, TabPage1)
        TabControl1.TabPages.Remove(TabPage2)
        TabControl1.SelectedTab = TabPage1

    End Sub

    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click
        Dim tNo As String = "", Unit1 As String = "", Unit2 As String = "", Harga As Double = 0, HJualAkhir As Double = 0
        Dim qty As Double = 0, qtyB As Double = 0, SubTotal As Double = 0, flag As String = ""
        Dim ps_disc1 As Double = 0, disc1 As Double = 0, HargaModal As Double = 0, HargaSatAsli As Double = 0
        Dim tgl_jatuhtempo As String = ""
        If Trim(DP.Text) = "" Then DP.Text = 0
        If Trim(KreditLimit.Text) = "" Then KreditLimit.Text = 0
        If Proses.ProsesClosing = True Then
            MsgBox("SEDANG ADA PROSES HITUNG STOCK....", vbCritical + vbOKOnly, ".:Tidak bisa simpan data !")
            Exit Sub
        End If
        If Trim(Id_Cust.Text) = "" Then
            MsgBox("Customer tidak boleh kosong!", vbCritical + vbOKOnly, ".:Error!")
            Id_Cust.Focus()
            Exit Sub
        Else
            Dim CustId As String = ""
            CustId = Proses.ExecuteSingleStrQuery("Select nama from m_customer where idrec = '" & Id_Cust.Text & "' ")
            If CustId = "" Then
                MsgBox("Customer belum terdaftar di data master customer", vbCritical + vbOKOnly, ".:Call Ferry !")
                Id_Cust.Focus()
                Exit Sub
            End If
        End If

        If cmbJenisBayar.Text = "HUTANG" Then
            If DP.Text = "" Then DP.Text = 0
            If TotHutang.Text = "" Then TotHutang.Text = 0
            If KreditLimit.Text = "" Then KreditLimit.Text = 0
            If ((Total.Text * 1) - (DP.Text * 1)) > ((KreditLimit.Text * 1) + (TotHutang.Text * 1)) Then
                MsgBox("Sisa kredit limit anda " & Format((KreditLimit.Text * 1) + (TotHutang.Text * 1), "###,##0") & " !", vbCritical + vbOKOnly, ".:Kredit Limit tidak mencukupi!")
                Exit Sub
            End If
        End If
        If Trim(Kode_Toko.Text) = "" And NamaToko.Text = "" Then
            MsgBox("Kode Toko Belum di Pilih !", vbCritical + vbOKOnly, ".:Warning!")
            Kode_Toko.Focus()
            Exit Sub
        Else
            KodeToko = Mid(Kode_Toko.Text, 4, 2)
        End If

        HitungTotal()
        If (Trim(Total.Text) = "") Or (Trim(Total.Text) = "0") Then
            MsgBox("Total penjualan kosong", vbCritical + vbOKOnly, ".: Salah total !")
            Exit Sub
        End If
        If Trim(LamaHutang.Text) <> "" Then
            Dim cekfaktur As Boolean = cekFaktur120()
            If cekfaktur = False Then
                Id_Cust.Focus()
                Exit Sub
            End If
        End If
        If LAdd Then
            'YYB TK TR 99999
            'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
            IDRec.Text = Proses.GetMaxId("t_SOH", "IdRec", "FKT-")

            AlamatKirim.Text = Replace(AlamatKirim.Text, "'", "`")
            SQL = "INSERT INTO dbo.t_SOH (IdRec,TglPenjualan, IdComputer, IdCust, alamatkirim, CaraBayar, " &
                " SubTotal, PsDisc, Disc, PPN, PsPPN, TotalSales, TglJatuhTempo, AktifYN,  LastUPD, UserID,  " &
                " TransferYN, Kode_Toko, NoRetur, NilaiRetur, Keterangan, DP, IDSales, PostingYN, TTerimaYN, " &
                " SalesCommYN, PrintCtr, SjCtr) VALUES ( '" & IDRec.Text & "', " &
                " '" & Format(TglJual.Value, "yyyy-MM-dd") & "', 0, '" & Id_Cust.Text & "', " &
                " '" & AlamatKirim.Text & "', '" & cmbJenisBayar.Text & "', " & sub_total.Text * 1 & ", " & PsDisc.Text * 1 & ", " &
                " " & Discount.Text * 1 & ", " & PPN.Text * 1 & ", " & PsPPN.Text * 1 & ",  " & Total.Text * 1 & ",  " &
                " '" & tgl_jatuhtempo & "',  'Y', GetDate(), '" & UserID & "', 'N', '" & Kode_Toko.Text & "', '', 0, " &
                " '" & Replace(Trim(Keterangan.Text), "'", "`") & "', " & DP.Text * 1 & ", '" & IDSales.Text & "',  " &
                " 'N', 'N', 'N', 0, 0) "
            Proses.ExecuteNonQuery(SQL)

            For i As Integer = 0 To DGRequest.Rows.Count - 1
                If i = DGRequest.Rows.Count Then Exit For
                If Trim(DGRequest.Rows(i).Cells(0).Value) = "" Then Exit For

                tNo = Microsoft.VisualBasic.Right(101 + i, 2)

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
                    qty = 0
                Else
                    qty = DGRequest.Rows(i).Cells(2).Value
                End If

                Unit1 = DGRequest.Rows(i).Cells(3).Value
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
                    qtyB = 0
                Else
                    qtyB = DGRequest.Rows(i).Cells(4).Value
                End If
                Unit2 = DGRequest.Rows(i).Cells(5).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
                    Harga = 0
                Else
                    Harga = DGRequest.Rows(i).Cells(6).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(7).Value) Then
                    ps_disc1 = 0
                    disc1 = 0
                Else
                    ps_disc1 = DGRequest.Rows(i).Cells(7).Value
                    disc1 = ps_disc1 / 100 * Harga
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(8).Value) Then
                    HJualAkhir = 0
                Else
                    HJualAkhir = DGRequest.Rows(i).Cells(8).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(9).Value) Then
                    SubTotal = 0
                Else
                    SubTotal = DGRequest.Rows(i).Cells(9).Value
                End If

                flag = DGRequest.Rows(i).Cells(11).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(12).Value) Then
                    HargaSatAsli = 0
                Else
                    HargaSatAsli = DGRequest.Rows(i).Cells(12).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(13).Value) Then
                    HargaModal = 0
                Else
                    HargaModal = DGRequest.Rows(i).Cells(13).Value
                End If
                SQL = "INSERT INTO dbo.t_SOD (Id_Rec, NoUrut ,KodeBrg, NamaBrg, QTY, satuan, " &
                    "QTYB, SatB, HargaJualAwal, PsDisc1, Disc1, Harga, Sub_Total, Flag, HargaSatuan_Asli, " &
                    "HargaModal,  AktifYN, LastUpd, UserID, TransferYN, Kode_Toko, QTYRetur) values ( " &
                    "'" & IDRec.Text & "', '" & tNo & "', '" & DGRequest.Rows(i).Cells(0).Value & "', " &
                    "'" & DGRequest.Rows(i).Cells(1).Value & "', " & qtyB & ", '" & Unit2 & "', " &
                    "" & qty & ", '" & Unit1 & "', " & Harga & ", " & ps_disc1 & ", " & disc1 & ", " &
                    "" & HJualAkhir & ", " & SubTotal & ", '" & flag & "', " & HargaSatAsli & ", " &
                    "" & HargaModal & ", 'Y', GetDate(), '" & UserID & "', 'N', '" & Kode_Toko.Text & "', 0) "
                Proses.ExecuteNonQuery(SQL)

                SQL = "Update m_Barang Set lastupd=getdate(), " &
                    "         Stock" & KodeToko & " = Stock" & KodeToko & " - " & qtyB * 1 & " " &
                    "Where IDRec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "' "
                Proses.ExecuteNonQuery(SQL)

                'YYB TK TR 99999
                'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
                Dim idtr As String = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", KodeToko & "TR")
                'ambil harga satuan untuk mutasi stock (cek harga pricelist untuk mutasi stock)
                Dim saldo As Double = 0
                SQL = "Select stock" & Mid(Kode_Toko.Text, 4, 2) & " as saldo, PriceList " &
                      "  From m_barang " &
                      " Where idrec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "'  "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    If IsDBNull(dbTable.Rows(0)!PriceList) Then
                        Harga = 0
                    Else
                        Harga = dbTable.Rows(0)!PriceList
                    End If
                    If IsDBNull(dbTable.Rows(0)!saldo) Then
                        saldo = 0
                    Else
                        saldo = dbTable.Rows(0)!saldo
                    End If
                End If
                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & idtr & "', '" & Kode_Toko.Text & "', 'Penjualan', '" & IDRec.Text & "', " &
                    "'" & Format(TglJual.Value, "yyyy-MM-dd") & "', '" & DGRequest.Rows(i).Cells(0).Value & "',  " &
                    "0, " & qtyB & ", " & saldo & ",  '" & Unit2 & "',  " & qtyB & ",  " & Harga & ", " &
                    "" & SubTotal & ", '" & UserID & "', GetDate(), '" & Kode_Toko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next i

            LAdd = False
            LEdit = False
            AturTombol(True)

        ElseIf LEdit Then
            If (cmbJenisBayar.Text = "HUTANG") Then
                tgl_jatuhtempo = Format(TglJatuhTempo.Value, "yyyy-MM-dd")
            Else
                tgl_jatuhtempo = "1900-01-01"
            End If
            SQL = "Update dbo.t_SOH set " &
                "TglPenjualan = '" & Format(TglJual.Value, "yyyy-MM-dd") & "', " &
                "IdCust = '" & Id_Cust.Text & "', " &
                "alamatkirim = '" & AlamatKirim.Text & "', " &
                "SubTotal = " & sub_total.Text * 1 & ", " &
                "PsDisc = " & PsDisc.Text * 1 & ", " &
                "Disc = " & Discount.Text * 1 & ", " &
                "PPN = " & PPN.Text * 1 & ", " &
                "PsPPN = " & PsPPN.Text * 1 & ", " &
                "TotalSales = " & Total.Text * 1 & ", " &
                "TglJatuhTempo = '" & tgl_jatuhtempo & "', " &
                "LastUPD = GetDate(), " &
                "UserID = '" & UserID & "', " &
                "Keterangan = '" & Replace(Trim(Keterangan.Text), "'", "`") & "', " &
                "DP = " & DP.Text * 1 & ", " &
                "IDSales = '" & IDSales.Text & "' " &
                "where idrec = '" & IDRec.Text & "'"
            Proses.ExecuteNonQuery(SQL)
            UpdateStock()
            SQL = "Update t_SOD set aktifYN = 'E' where id_rec = '" & IDRec.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            tNo = ""
            Harga = 0
            qty = 0
            ps_disc1 = 0
            Dim Unit As String = ""
            Dim dSubTotal As Double = 0, dTotalDisc As Double = 0
            Dim ps_disc2 As Double = 0, ps_disc3 As Double = 0
            Dim idtr As String = "", saldo As Integer = 0, dAdjust As Double = 0

            For i As Integer = 0 To DGRequest.Rows.Count - 1
                If i = DGRequest.Rows.Count Then Exit For
                If Trim(DGRequest.Rows(i).Cells(0).Value) = "" Then Exit For

                tNo = Microsoft.VisualBasic.Right(101 + i, 2)

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
                    qty = 0
                Else
                    qty = DGRequest.Rows(i).Cells(2).Value
                End If

                Unit1 = DGRequest.Rows(i).Cells(3).Value
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
                    qtyB = 0
                Else
                    qtyB = DGRequest.Rows(i).Cells(4).Value
                End If
                Unit2 = DGRequest.Rows(i).Cells(5).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
                    Harga = 0
                Else
                    Harga = DGRequest.Rows(i).Cells(6).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(7).Value) Then
                    ps_disc1 = 0
                    disc1 = 0
                Else
                    ps_disc1 = DGRequest.Rows(i).Cells(7).Value
                    disc1 = ps_disc1 / 100 * Harga
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(8).Value) Then
                    HJualAkhir = 0
                Else
                    HJualAkhir = DGRequest.Rows(i).Cells(8).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(9).Value) Then
                    SubTotal = 0
                Else
                    SubTotal = DGRequest.Rows(i).Cells(9).Value
                End If

                flag = DGRequest.Rows(i).Cells(11).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(12).Value) Then
                    HargaSatAsli = 0
                Else
                    HargaSatAsli = DGRequest.Rows(i).Cells(12).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(13).Value) Then
                    HargaModal = 0
                Else
                    HargaModal = DGRequest.Rows(i).Cells(13).Value
                End If
                SQL = "INSERT INTO dbo.t_SOD (Id_Rec, NoUrut ,KodeBrg, NamaBrg, QTY, satuan, " &
                    "QTYB, SatB, HargaJualAwal, PsDisc1, Disc1, Harga, Sub_Total, Flag, HargaSatuan_Asli, " &
                    "HargaModal,  AktifYN, LastUpd, UserID, TransferYN, Kode_Toko, QTYRetur) values ( " &
                    "'" & IDRec.Text & "', '" & tNo & "', '" & DGRequest.Rows(i).Cells(0).Value & "', " &
                    "'" & DGRequest.Rows(i).Cells(1).Value & "', " & qtyB & ", '" & Unit2 & "', " &
                    "" & qty & ", '" & Unit1 & "', " & Harga & ", " & ps_disc1 & ", " & disc1 & ", " &
                    "" & HJualAkhir & ", " & SubTotal & ", '" & flag & "', " & HargaSatAsli & ", " &
                    "" & HargaModal & ", 'Y', GetDate(), '" & UserID & "', 'N', '" & Kode_Toko.Text & "', 0) "
                Proses.ExecuteNonQuery(SQL)

                SQL = "Update m_Barang Set lastupd=getdate(), " &
                    "         Stock" & KodeToko & " = Stock" & KodeToko & " - " & qtyB * 1 & " " &
                    "Where IDRec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "' "
                Proses.ExecuteNonQuery(SQL)

                'YYB TK TR 99999
                'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
                idtr = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", KodeToko & "TR")
                'ambil harga satuan untuk mutasi stock (cek harga pricelist untuk mutasi stock)
                saldo = 0
                SQL = "Select stock" & Mid(Kode_Toko.Text, 4, 2) & " as saldo, PriceList " &
                      "  From m_barang " &
                      " Where idrec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "'  "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    If IsDBNull(dbTable.Rows(0)!PriceList) Then
                        Harga = 0
                    Else
                        Harga = dbTable.Rows(0)!PriceList
                    End If
                    If IsDBNull(dbTable.Rows(0)!saldo) Then
                        saldo = 0
                    Else
                        saldo = dbTable.Rows(0)!saldo
                    End If
                End If

                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & idtr & "', '" & Kode_Toko.Text & "', 'Penjualan', '" & IDRec.Text & "', " &
                    "'" & Format(TglJual.Value, "yyyy-MM-dd") & "', '" & DGRequest.Rows(i).Cells(0).Value & "',  " &
                    "0, " & qtyB & ", " & saldo & ",  '" & Unit2 & "',  " & qtyB & ",  " & Harga & ", " &
                    "" & SubTotal & ", '" & UserID & "', GetDate(), '" & Kode_Toko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next i

            SQL = "Delete t_SOD where id_rec = '" & IDRec.Text & "' and aktifYN = 'E' "
            Proses.ExecuteNonQuery(SQL)

            LAdd = False
            LEdit = False
            AturTombol(True)
        End If
        TabControl1.TabPages.Insert(0, TabPage1)
        TabControl1.TabPages.Remove(TabPage2)
        TabControl1.SelectedTab = TabPage1
        cmdCetak_Click(sender, New System.EventArgs)
        Call Data_Record()
    End Sub

    Private Sub UpdateStock()

        Dim IdTR As String
        Dim QTYB As Double, KodeBrg As String, Unit As String, Harga As Double, tSubTotal As Double, Saldo As Double

        SQL = "Select qty, kodebrg, satuan, harga, sub_total From t_SOD " &
                "Where ID_Rec = '" & IDRec.Text & "' " &
                "  And AktifYN = 'Y' "
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                QTYB = .Table.Rows(a) !qty
                KodeBrg = .Table.Rows(a) !KodeBrg
                Unit = .Table.Rows(a) !satuan
                Harga = .Table.Rows(a) !harga
                tSubTotal = .Table.Rows(a) !sub_total
                IdTR = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", KodeToko & "TR")
                SQL = "Update m_Barang " &
                        " Set stock" & KodeToko & " = stock" & KodeToko & " + " & QTYB * 1 & "  
                    Where IDRec = '" & Trim(KodeBrg) & "' "
                Proses.ExecuteNonQuery(SQL)

                SQL = "Select stock" & KodeToko & " " &
                        "  from m_barang " &
                        " where idrec = '" & Trim(KodeBrg) & "'  "
                Saldo = Proses.ExecuteSingleDblQuery(SQL)

                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & IdTR & "', '" & Kode_Toko.Text & "', 'Adjust Edit Kasir', '" & IDRec.Text & "', " &
                    "'" & Format(TglJual.Value, "yyyy-MM-dd") & "', '" & KodeBrg & "',  " &
                    "" & QTYB & ", 0, " & Saldo & ",  '" & Unit & "',  " & QTYB & ",  " & Harga & ", " &
                    "" & tSubTotal & ", '" & UserID & "', GetDate(), '" & Kode_Toko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next (a)
        End With
    End Sub

    Private Sub DGView_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim tID As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        IDRec.Text = tID
        If LEdit Or LAdd Then
        Else
            Isi_Data()
        End If
    End Sub

    Private Sub DGView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        cmdEdit_Click(sender, e)
    End Sub



    Private Sub PsPPN_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PsPPN.TextChanged
        If Trim(PsPPN.Text) = "" Then
            PsPPN.Text = 0
            PPN.Text = 0
        ElseIf Trim(PsDisc.Text) = "" Then
            PsDisc.Text = 0
            Discount.Text = 0
        Else
            HitungTotal()
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbJenisBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisBayar.SelectedIndexChanged

        If CmbJenisBayarIndex = cmbJenisBayar.SelectedIndex Then
            SelectedItemIndex = -1
            'tbxSelectedItem.Text = ""
        Else
            CmbJenisBayarIndex = cmbJenisBayar.SelectedIndex
            'tbxSelectedItem.Text = ComboBox1.Items(SelectedItemIndex)
            Dim value As Integer = cmbJenisBayar.SelectedIndex
            Select Case value
                Case 0 'Lunas
                    lblJatuhTempo.Visible = False
                    TglJatuhTempo.Visible = False
                    DP.ForeColor = Color.Black
                    DP.BackColor = Color.White
                    DP.Text = 0
                    DP.Visible = False
                    LabelDP.Visible = False
                    DGRequest.Focus()
                Case Else 'hutang
                    lblJatuhTempo.Visible = True
                    TglJatuhTempo.Visible = True
                    DP.ForeColor = Color.White
                    DP.BackColor = Color.Red
                    DP.Visible = True
                    LabelDP.Visible = True
                    TglJatuhTempo.Focus()
            End Select
        End If
    End Sub

    Private Sub cmdHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHapus.Click
        Dim QTYB As Double = 0, QTYK As Double = 0, QTY As Double = 0, KodeBrg As String = ""

        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        If Trim(IDRec.Text) = "" Then
            MsgBox("ID Data Belum di pilih!", vbCritical, ".:ERROR!")
            TabControl1.SelectedTab = TabPage1
            DGView.Focus()
        End If
        SQL = "select postingyn from t_soh where idrec = '" & IDRec.Text & "'"
        Dim postyn As String = Proses.ExecuteSingleStrQuery(SQL)
        If postyn = "Y" Then
            MsgBox("Invoice penjualan sudah dibayar, tidak dapat dihapus", vbCritical + vbOKOnly, "Warning !")
            Exit Sub
        End If
        'SQL = "select tterimaYN from t_soh where idrec = '" & IDRec.Text & "'"
        'postyn = Proses.ExecuteSingleStrQuery(SQL)
        'If postyn = "Y" Then
        '    MsgBox("Sudah ada tanda terima penjualan, tidak dapat dihapus")
        '    Exit Sub
        'End If
        Dim dbcek As String = ""
        SQL = "select id_rec from t_TagihCustD where no_nota = '" & IDRec.Text & "' and aktifYN = 'Y' "
        dbcek = Proses.ExecuteSingleStrQuery(SQL)
        If dbcek <> "" Then
            MsgBox("Nota ini sudah di buatkan tanda terima, tidak bisa edit/hapus." & vbCrLf &
                "Id Tagihan : " & dbcek, vbCritical + vbOKOnly, "Warning !")
            Exit Sub
        End If
        If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update t_soh set AktifYN = 'N', " &
                " UserID = '" & UserID & "', " &
                "LastUPD = GetDate() " &
                "where idrec = '" & IDRec.Text & "' and Kode_toko = '" & Kode_Toko.Text & "' "
            Proses.ExecuteNonQuery(SQL)

            SQL = "Select * From t_sod " &
                "Where ID_Rec = '" & IDRec.Text & "' and Kode_toko = '" & Kode_Toko.Text & "'  " &
                "  And AktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            With dbTable.Columns(0)
                For a = 0 To dbTable.Rows.Count - 1
                    QTY = .Table.Rows(a) !QTYB

                    KodeBrg = .Table.Rows(a) !KodeBrg
                    SQL = "Update m_Barang Set lastupd=getdate(), " &
                         "  Stock" & KodeToko & " = Stock" & KodeToko & " + " & QTY * 1 & " " &
                        "Where IDRec = '" & Trim(KodeBrg) & "' "
                    Proses.ExecuteNonQuery(SQL)
                Next (a)
            End With


            SQL = "Update t_sod set AktifYN = 'N', " &
                " UserID = '" & UserID & "', " &
                "LastUPD = GetDate() " &
                "where id_rec = '" & IDRec.Text & "' and Kode_toko = '" & Kode_Toko.Text & "' "
            Proses.ExecuteNonQuery(SQL)

            Data_Record()
        End If
    End Sub

    Public Function CariSatuan(tmp As String) As String
        Form_Daftar.txtQuery.Text = "Select Satuan,  isnull(satuant, '') SatuanT,  isnull(satuanb, '') SatuanB" &
            "  From m_Barang " &
            " Where IDRec = '" & tmp & "' " &
            "   And AktifYN = 'Y' "
        Form_Daftar.Text = "Daftar Satuan"
        Form_Daftar.ShowDialog()
        CariSatuan = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
    End Function

    Private Sub NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            TglJual.Focus()
        End If
    End Sub

    Private Sub DGRequest_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellContentClick
        If DGRequest.Rows.Count <> 0 Then
            isiItemBarang()
        End If
    End Sub
    Private Sub isiItemBarang()
        indexbrg.Text = DGRequest.CurrentCell.RowIndex
        KodeBrg.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
        isiKodeBrg(KodeBrg.Text)
        KodeBrg.ForeColor = Color.Black
        KodeBrg.BackColor = Color.White
        QtyB.Text = DGRequest.Rows(indexbrg.Text).Cells(2).Value
        cmbSatuanB.Text = DGRequest.Rows(indexbrg.Text).Cells(3).Value
        QTY.Text = DGRequest.Rows(indexbrg.Text).Cells(4).Value
        satuan.Text = DGRequest.Rows(indexbrg.Text).Cells(5).Value
        HargaSatuan.Text = DGRequest.Rows(indexbrg.Text).Cells(6).Value
        PsDisc.Text = DGRequest.Rows(indexbrg.Text).Cells(7).Value
        HSatAkhir.Text = DGRequest.Rows(indexbrg.Text).Cells(8).Value
        SubTotal.Text = DGRequest.Rows(indexbrg.Text).Cells(9).Value
        Flag.Text = DGRequest.Rows(indexbrg.Text).Cells(11).Value
        HargaSatuan_Asli.Text = DGRequest.Rows(indexbrg.Text).Cells(12).Value
        HargaModal.Text = DGRequest.Rows(indexbrg.Text).Cells(13).Value
    End Sub
    Private Sub TglJual_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglJual.KeyPress
        If e.KeyChar = Chr(13) Then
            Id_Cust.Focus()
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Data_Record()
    End Sub

    Private Sub TglJatuhTempo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglJatuhTempo.KeyPress
        If e.KeyChar = Chr(13) Then
            KodeBrg.Focus()
        End If
    End Sub

    Private Sub cmbJenisBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenisBayar.KeyPress
        If e.KeyChar = Chr(13) Then
            If cmbJenisBayar.Text = "HUTANG" Then
                lblJatuhTempo.Visible = True
                TglJatuhTempo.Visible = True
                TglJatuhTempo.Focus()
            Else
                lblJatuhTempo.Visible = False
                TglJatuhTempo.Visible = False
                DGRequest.Focus()
            End If
        End If
    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click

        If IDRec.Text <> "" Then
            cmdCetak.Enabled = False
            Dim nPrinter As String = "", nKertas As String = "", nPrintYN As String = ""
            Dim Proses As New ClsKoneksi, mId As String = "", mUserId As String = ""
            Dim tb As New Terbilang, terbilang As String = ""
            Dim bank As String = "", atasnama As String = ""
            FrmMenuUtama.TSKeterangan.Text = ""
            Form_Cetak.PanelTipe.Visible = True
            Form_Cetak.ShowDialog()
            nPrintYN = FrmMenuUtama.TSKeterangan.Text
            If nPrintYN = "" Then
                cmdCetak.Enabled = True
                Exit Sub
            ElseIf nPrintYN = "PRINTER" And (FrmMenuUtama.TipeCetakan.Text = "Faktur" Or FrmMenuUtama.TipeCetakan.Text = "Surat Jalan") Then

                If FrmMenuUtama.TipeCetakan.Text = "Faktur" Then
                    SQL = "select coalesce(PrintCtr,0) as printctr From t_soh where idrec = '" & IDRec.Text & "'"
                Else
                    SQL = "select coalesce(SjCtr,0) as sjctr From t_soh where idrec = '" & IDRec.Text & "'"
                End If

                Dim printctr As Integer = Proses.ExecuteSingleDblQuery(SQL)
                If printctr > 0 Then
                    Dim ans As String = ""  'InputBox("Masukkan password", "Security", "")
                    Dim tblLogin As New DataTable
                    FrmMenuUtama.TSKeterangan.Text = ""
                    FrmMenuUtama.UserLoginMenu.Text = ""
                    FrmMenuUtama.PasswordLoginMenu.Text = ""
                    Form_LoginMenu.PswTxt.Text = ""
                    Form_LoginMenu.KdPenggunaTxt.Text = ""
                    Form_LoginMenu.KdPenggunaTxt.Focus()
                    Form_LoginMenu.ShowDialog()
                    If FrmMenuUtama.TSKeterangan.Text = "OK" Then
                        ans = FrmMenuUtama.PasswordLoginMenu.Text
                    Else
                        ans = ""
                    End If
                    Dim Acak As New Crypto
                    Dim encryptpassword As String = ""
                    If Trim(ans) = "" Then
                        MessageBox.Show("Password belum di isi..!!", "Empty Passowrd", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmdCetak.Enabled = True
                        Exit Sub
                    End If

                    encryptpassword = Acak.Encrypt(Trim(ans)) 'CETAK_ULANG

                    tblLogin = Proses.ExecuteQuery("Select * From m_User " &
                                                "Where UserID = '" & FrmMenuUtama.UserLoginMenu.Text & "' " &
                                                "  and password ='" & encryptpassword & "'")
                    If tblLogin.Rows.Count = 0 Then
                        MessageBox.Show("Password salah atau user name tidak terdaftar..!!", ".:Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmdCetak.Enabled = True
                        Exit Sub
                    End If
                    Dim akses As Boolean = False
                    akses = Proses.UserAksesTombol(FrmMenuUtama.UserLoginMenu.Text, "CETAK_ULANG", "AKSES")
                    If Not akses Then
                        MessageBox.Show("User name ANDA tidak mempunyai akses untuk memcetak ulang faktur !", ".:Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmdCetak.Enabled = True
                        Exit Sub
                    End If
                End If
            End If
            Me.Cursor = Cursors.WaitCursor

            terbilang = "#" + tb.Terbilang(CDbl(Total.Text)) + " Rupiah #"
            nPrinter = My.Settings.NamaPrinter
            nKertas = My.Settings.NamaKertas
            Proses.OpenConn(CN)
            'Call OpenConn()
            dttable = New DataTable

            mId = "'No. '+ t_SOH.IdRec  "
            mUserId = " '( ' + t_SOH.userid +' )' "

            If FrmMenuUtama.TipeCetakan.Text = "Surat Jalan" Then
                SQL = "select a.idrec, c.Nama, a.TglPenjualan, d.NamaSales, a.TotalSales, e.Nama as namacust, e.Alamat1, e.Alamat2, 
                       b.NamaBrg, b.qtyb QTY, b.satb satuan, e.phone, IIF(e.expedisi='', '', 'exp: '+e.expedisi) expedisi
                       from t_soh a left join t_sod b on a.IdRec = b.Id_Rec
                       left join m_toko c on a.Kode_Toko = c.IdRec
                       left join m_Sales d on a.IDSales = d.IDRec
                       left join m_customer e on a.idcust = e.idrec 
                       Where a.AktifYN = 'Y'
                       And b.AktifYN = 'Y'
                       And a.idrec = '" & IDRec.Text & "' 
                       Order By a.IdRec, b.nourut "
            ElseIf FrmMenuUtama.TipeCetakan.Text = "Faktur" Then
                If FrmMenuUtama.CompCode.Text = "PJ" Or FrmMenuUtama.CompCode.Text = "DM" Then
                    SQL = "Select a.idrec, c.Nama As NamaToko, a.TglPenjualan, d.NamaSales, a.TotalSales, " &
                        "  e.Nama As namacust, e.Alamat1, e.Alamat2, e.Phone As TlpCP, b.HargaJualAwal harga, " &
                        "  b.NamaBrg, b.qtyb QTY, b.satb satuan, a.CaraBayar, b.psdisc1, b.Sub_Total, e.phone," &
                        "IIf(carabayar = 'HUTANG', convert(varchar(20), tglJatuhTempo, 106) , '' ) AS JatuhTempo, " &
                        "IIf(isiSatB > 1, '1 ' + SatuanB + ' = ' +  CAST(IsiSatB/IsiSatT as varchar(3)) + ' ' + SatuanT,'') KetIsi  " &
                        "From t_soh a inner Join t_sod b on a.IdRec = b.Id_Rec  " &
                        "   Left Join m_toko c on a.Kode_Toko = c.IdRec " &
                        "   Left Join m_Sales d on a.IDSales = d.IDRec " &
                        "   Left Join m_customer e on a.idcust = e.idrec  " &
                        "   inner join m_barang On m_barang.IDRec = b.KodeBrg " &
                        "Where a.AktifYN = 'Y' " &
                        "  And b.AktifYN = 'Y' " &
                        "  And a.idrec = '" & IDRec.Text & "'  " &
                        "Order By a.IdRec, b.nourut "
                Else
                    SQL = "Select a.idrec, c.Nama As NamaToko, a.TglPenjualan, d.NamaSales, a.TotalSales, " &
                        "  e.Nama As namacust, e.Alamat1, e.Alamat2, e.Phone as TlpCP, b.HargaJualAwal harga, " &
                        "  b.NamaBrg, b.qtyb QTY, b.satb satuan, a.CaraBayar, b.psdisc1, b.Sub_Total, e.phone," &
                        "IIf(carabayar = 'HUTANG', convert(varchar(20), tglJatuhTempo, 106) , '' ) AS JatuhTempo, " &
                        "CASE WHEN b.SatB = m_Barang.SatuanB and m_barang.satuant = m_barang.satuanB and isisatB > 0 THEN " &
                        "           CAST( b.qtyb / (isisatB * b.qtyb) as varchar(24)) " &
                        "     WHEN b.SatB = m_Barang.SatuanB and m_barang.satuant <> m_barang.satuanB and isisatB > 0 THEN  " &
                        "          CAST( b.qtyb / (m_barang.isisatT * b.qtyb) as varchar(24))  " &
                        "     WHEN b.SatB = m_Barang.SatuanT and m_barang.satuant <> m_barang.satuanB and isisatB > 0 THEN   " &
                        "          CAST( b.qtyb / ( (b.qtyb * m_barang.isisatT) / IsiSatB ) As varchar(24))   " &
                        "     WHEN b.SatB = m_Barang.Satuan and m_barang.satuant <> m_barang.satuan and isisatT > 0 then " &
                        "           CAST( b.qtyb / (b.qtyb / m_barang.isisatB)  As varchar(24))   " &
                        "     WHEN b.SatB = m_Barang.Satuan and m_barang.satuant = m_barang.satuan then " &
                        "           CAST(b.qtyb / (b.qtyb)  As varchar(24))   " &
                        "Else ''  " &
                        "End as IsiSatuan,  " &
                        "CASE WHEN b.SatB = m_Barang.SatuanB and m_barang.satuant = m_barang.satuanB and isisatB > 0 THEN " &
                        "          CAST(isisatB * b.qtyb as varchar(24)) + ' ' + m_barang.Satuan " &
                        "     WHEN b.SatB = m_Barang.SatuanB and m_barang.satuant <> m_barang.satuanB and isisatB > 0 THEN  " &
                        "          CAST(m_barang.isisatT * b.qtyb as varchar(24)) + ' ' + m_barang.SatuanT " &
                        "     WHEN b.SatB = m_Barang.SatuanT and m_barang.satuant <> m_barang.satuanB and isisatB > 0 THEN   " &
                        "          CAST((b.qtyb * m_barang.isisatT) / IsiSatB As varchar(24)) + ' ' + m_barang.SatuanB  " &
                        "     WHEN b.SatB = m_Barang.Satuan and m_barang.satuant <> m_barang.satuan and isisatB > 0 then " &
                        "           CAST(ROUND((b.qtyb / m_barang.isisatB),2)   As varchar(24)) + ' ' + m_barang.SatuanB  " &
                        "     WHEN b.SatB = m_Barang.Satuan and m_barang.satuant = m_barang.satuan then " &
                        "           CAST((b.qtyb)  As varchar(24)) + ' ' + m_barang.Satuan  " &
                        "Else ''  " &
                        "End as KetIsi  " &
                        "From t_soh a inner Join t_sod b On a.IdRec = b.Id_Rec  " &
                        "   Left Join m_toko c On a.Kode_Toko = c.IdRec " &
                        "   Left Join m_Sales d On a.IDSales = d.IDRec " &
                        "   Left Join m_customer e On a.idcust = e.idrec  " &
                        "   inner join m_barang On m_barang.IDRec = b.KodeBrg " &
                        "Where a.AktifYN = 'Y' " &
                        "  And b.AktifYN = 'Y' " &
                        "  And a.idrec = '" & IDRec.Text & "'  " &
                        "Order By a.IdRec, b.nourut "
                End If
            End If

            DTadapter = New SqlDataAdapter(SQL, CN)
            Try
                DTadapter.Fill(dttable)
                If FrmMenuUtama.TipeCetakan.Text = "Surat Jalan" Then
                    'If FrmMenuUtama.CompCode.Text = "PJ" Then
                    '    objRep = New Rpt_SuratJalan
                    'Else
                    '    objRep = New Rpt_SuratJalanKMDM
                    'End If

                    objRep.SetDataSource(dttable)
                    objRep.SetParameterValue("cetakan", "")
                    objRep.SetParameterValue("toko", FrmMenuUtama.CompCode.Text)
                ElseIf FrmMenuUtama.TipeCetakan.Text = "Faktur" Then
                    'If FrmMenuUtama.CompCode.Text = "PJ" Or FrmMenuUtama.CompCode.Text = "DM" Or FrmMenuUtama.CompCode.Text = "OL" Then
                    '    objRep = New Rpt_Penjualan
                    'Else
                    '    objRep = New Rpt_Penjualan_KM
                    'End If
                    objRep.SetDataSource(dttable)
                    objRep.SetParameterValue("terbilang", terbilang)
                    objRep.SetParameterValue("userid", UserID)
                    objRep.SetParameterValue("toko", FrmMenuUtama.CompCode.Text)
                    SQL = "select coalesce(PrintCtr,0) as printctr From t_soh where idrec = '" & IDRec.Text & "'"
                    Dim printctr As Integer = Proses.ExecuteSingleDblQuery(SQL)
                    If printctr > 0 Then
                        objRep.SetParameterValue("cetakan", "Cetakan ke " + Format(printctr + 1, "##0"))
                    Else
                        objRep.SetParameterValue("cetakan", "")
                    End If
                End If

                objRep.PrintOptions.PaperSize = Proses.GetPapersizeID(nPrinter, nKertas)
                If nPrintYN = "PRINTER" Then
                    If FrmMenuUtama.TipeCetakan.Text = "Faktur" Then
                        SQL = "select coalesce(PrintCtr,0) as printctr From t_soh where idrec = '" & IDRec.Text & "'"
                    Else
                        SQL = "select coalesce(SjCtr,0) as printctr From t_soh where idrec = '" & IDRec.Text & "'"
                    End If

                    Dim printctr As Integer = Proses.ExecuteSingleDblQuery(SQL)
                    If printctr > 0 Then
                        objRep.SetParameterValue("cetakan", "Cetakan ke " + Format(printctr + 1, "##0"))
                    Else
                        objRep.SetParameterValue("cetakan", "")
                    End If
                    objRep.PrintToPrinter(1, False, 0, 0)
                    If FrmMenuUtama.TipeCetakan.Text = "Faktur" Then
                        SQL = "update t_soh set printctr = '" & printctr + 1 & "' where idrec = '" & IDRec.Text & "'"
                    Else
                        SQL = "update t_soh set SjCtr = '" & printctr + 1 & "' where idrec = '" & IDRec.Text & "'"
                    End If

                    Proses.ExecuteNonQuery(SQL)
                Else
                    If FrmMenuUtama.TipeCetakan.Text = "Surat Jalan" Then
                        Form_Report.Text = "Cetak Surat Jalan"
                    ElseIf FrmMenuUtama.TipeCetakan.Text = "Faktur" Then
                        Form_Report.Text = "Cetak Nota Penjualan"
                    End If
                    Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                    Form_Report.CrystalReportViewer1.Refresh()
                    Form_Report.CrystalReportViewer1.ShowRefreshButton = False
                    Form_Report.CrystalReportViewer1.ShowPrintButton = False
                    Form_Report.CrystalReportViewer1.ShowParameterPanelButton = False
                    Form_Report.CrystalReportViewer1.ReportSource = objRep
                    Form_Report.ShowDialog()
                End If
                dttable.Dispose()
                DTadapter.Dispose()
                Proses.CloseConn(CN)
                'CloseConn()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error")
            End Try
            Me.Cursor = Cursors.Default
            cmdCetak.Enabled = True
        Else
            MsgBox("No Penjualan Masih Kosong !", vbCritical + vbOKOnly, ".: Warning !")
            Exit Sub
        End If
    End Sub

    'Private Sub isiKodeBrg(IdBrg)
    '    Dim dbT As DataTable
    '    SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
    '            "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
    '            "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori, HPP " &
    '            " From M_Barang " &
    '            "Where AktifYN = 'Y' " &
    '            "  And idRec = '" & IdBrg & "' "
    '    dbT = Proses.ExecuteQuery(SQL)
    '    If dbT.Rows.Count <> 0 Then
    '        'NamaBrg.Text = dbT.Rows(0)!Nama
    '        'KodeBrg.Text = dbT.Rows(0)!IDRec
    '        'QTY.Text = 1
    '        'satuan.Text = dbT.Rows(0)!satuan
    '        'QtyB.Text = 1
    '        'cmbSatuanB.Items.Add(dbT.Rows(0)!satuan)
    '        'cmbSatuanB.Items.Add(dbT.Rows(0)!satuanT)
    '        'cmbSatuanB.Items.Add(dbT.Rows(0)!satuanB)
    '        'cmbSatuanB.Text = satuan.Text
    '        isiSatB.Text = Format(dbT.Rows(0)!IsiSatB, "###,##0")
    '        IsiSatT.Text = Format(dbT.Rows(0)!IsiSatT, "###,##0")
    '        If xHarga = "GUDANG" Then
    '            HargaSatuan.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
    '            harga1.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
    '            harga2.Text = Format(dbT.Rows(0)!PriceList2, "###,##0")
    '            harga3.Text = Format(dbT.Rows(0)!PriceList3, "###,##0")
    '        ElseIf xHarga = "TOKO" Then
    '            HargaSatuan.Text = Format(dbT.Rows(0)!HargaToko1, "###,##0")
    '            harga1.Text = Format(dbT.Rows(0)!HargaToko1, "###,##0")
    '            harga2.Text = Format(dbT.Rows(0)!HargaToko2, "###,##0")
    '            harga3.Text = Format(dbT.Rows(0)!HargaToko3, "###,##0")
    '        ElseIf xHarga = "MALL" Then
    '            HargaSatuan.Text = Format(dbT.Rows(0)!HargaMall1, "###,##0")
    '            harga1.Text = Format(dbT.Rows(0)!HargaMall1, "###,##0")
    '            harga2.Text = Format(dbT.Rows(0)!HargaMall2, "###,##0")
    '            harga3.Text = Format(dbT.Rows(0)!HargaMall3, "###,##0")
    '        End If
    '        HargaModal.Text = dbT.Rows(0)!HPP
    '    End If
    'End Sub

    Private Sub tglCari1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglCari1.KeyPress
        If e.KeyChar = Chr(13) Then
            tglCari2.Focus()
            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub AlamatKirim_TextChanged(sender As Object, e As EventArgs) Handles AlamatKirim.TextChanged

    End Sub

    Private Sub tglCari2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglCari2.KeyPress
        If e.KeyChar = Chr(13) Then
            tCari.Focus()
            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub NoNota_TextChanged(sender As Object, e As EventArgs)

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

    Private Sub HitungSubTotal()
        Dim harga1 As Double = 0
        Dim disc1 As Double = 0, xQTY As Double = 0
        If Trim(QTY.Text) = "" Then QTY.Text = 1
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If Trim(HargaSatuan_Asli.Text) = "" Then HargaSatuan_Asli.Text = 0
        If Trim(PsDisc.Text) = "" Then PsDisc.Text = 0
        If Trim(HSatAkhir.Text) = "" Then HSatAkhir.Text = 0

        Dim value As Integer = cmbSatuanB.SelectedIndex
        Select Case value
            Case 0
                xQTY = QtyB.Text * 1
            Case 1
                xQTY = QtyB.Text * 1
            Case 2
                xQTY = QtyB.Text * 1
            Case Else
                xQTY = QTY.Text * 1
        End Select



        If Trim(PsDisc.Text) <> 0 Then
            disc1 = (PsDisc.Text * 1 / 100) * (HargaSatuan.Text * 1)
            HSatAkhir.Text = (HargaSatuan.Text * 1) - disc1
        Else
            HSatAkhir.Text = HargaSatuan.Text * 1
        End If
        harga1 = xQTY * (HSatAkhir.Text * 1)
        If xQTY * (HargaSatuan_Asli.Text * 1) <> harga1 Then
            Flag.Text = "*"
        Else
            Flag.Text = ""
        End If
        SubTotal.Text = Format(harga1, "###,##0")
    End Sub

    Private Sub AddItem()
        Dim ada As Boolean = False, i As Integer = 0
        Dim dt As New DataTable
        'SQL = "select kreditlimit from m_Customer where idrec = '" & Id_Cust.Text & "'"
        'Dim ceklimit As Double = Proses.ExecuteSingleDblQuery(SQL)
        'If ceklimit < Total.Text * 1 Then
        '    MsgBox("Limit melebihi batas untuk customer ini")
        '    Exit Sub
        'End If
        SQL = "select * from m_barang where idrec = '" & KodeBrg.Text & "'"
        dt = Proses.ExecuteQuery(SQL)

        If CDbl(HargaSatuan.Text) < dt.Rows(0) !PriceList Then
            If MessageBox.Show("Harga barang lebih kecil dari harga master barang, lanjut?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                Exit Sub
            End If
        End If


        For i = 0 To DGRequest.Rows.Count - 1
            If Trim(KodeBrg.Text) = Trim(DGRequest.Rows(i).Cells(0).Value) Then
                ada = True
                Exit For
            End If
        Next
        If ada Then
            If (MsgBox("Kode Barang " & KodeBrg.Text & " " & NamaBrg.Text & " SUDAH ADA, " & vbCrLf &
                       "Apakah " & KodeBrg.Text & " mau di ganti harga / jumlah nya ?",
                       vbInformation + vbYesNo, ".:Warning !") = vbYes) Then
                indexbrg.Text = i
                DGRequest.Rows(indexbrg.Text).Cells(0).Value = KodeBrg.Text
                DGRequest.Rows(indexbrg.Text).Cells(1).Value = NamaBrg.Text
                DGRequest.Rows(indexbrg.Text).Cells(2).Value = QtyB.Text
                DGRequest.Rows(indexbrg.Text).Cells(3).Value = cmbSatuanB.Text
                DGRequest.Rows(indexbrg.Text).Cells(4).Value = QTY.Text
                DGRequest.Rows(indexbrg.Text).Cells(5).Value = satuan.Text
                DGRequest.Rows(indexbrg.Text).Cells(6).Value = HargaSatuan.Text
                DGRequest.Rows(indexbrg.Text).Cells(7).Value = PsDisc.Text
                DGRequest.Rows(indexbrg.Text).Cells(8).Value = HSatAkhir.Text
                DGRequest.Rows(indexbrg.Text).Cells(9).Value = SubTotal.Text

                DGRequest.Rows(indexbrg.Text).Cells(11).Value = Flag.Text
                DGRequest.Rows(indexbrg.Text).Cells(12).Value = HargaSatuan_Asli.Text
                DGRequest.Rows(indexbrg.Text).Cells(13).Value = HargaModal.Text
                DGRequest.Rows(indexbrg.Text).Selected = True
            End If
        Else
            If DGRequest.Rows.Count >= 12 Then
                MsgBox("1 faktur hanya boleh 12 barang", vbOKOnly + vbCritical, ".:Warning!")
                Exit Sub
            End If
            DGRequest.Rows.Add(KodeBrg.Text,
                            NamaBrg.Text,
                            QtyB.Text,
                            cmbSatuanB.Text,
                            QTY.Text,
                            satuan.Text,
                            HargaSatuan.Text,
                            PsDisc.Text,
                            HSatAkhir.Text,
                            SubTotal.Text, "Hapus",
                            Flag.Text,
                            HargaSatuan_Asli.Text,
                            HargaModal.Text)
        End If
        clearBarcode()
        HitungTotal()
        KodeBrg.Text = "<Kode_Brg>"
        KodeBrg.ForeColor = Color.Gray
        KodeBrg.BackColor = Color.LightGoldenrodYellow
        KodeBrg.Focus()
    End Sub

    Private Sub HargaSatuan_GotFocus(sender As Object, e As EventArgs) Handles HargaSatuan.GotFocus
        With HargaSatuan
            .SelectionStart = 0
            .SelectionLength = .TextLength
            KodeBrg.ForeColor = Color.Black
            KodeBrg.BackColor = Color.White
        End With
    End Sub
    Private Sub HargaSatuan_LostFocus(sender As Object, e As EventArgs) Handles HargaSatuan.LostFocus
        If HargaSatuan.Text = Nothing Then
            HargaSatuan.Text = "0"
            HargaSatuan.ForeColor = Color.Gray
            HargaSatuan.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub
    Private Sub HargaSatuan_TextChanged(sender As Object, e As EventArgs) Handles HargaSatuan.TextChanged
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If IsNumeric(HargaSatuan.Text) Then
            'Dim temp As Double = HargaSatuan.Text
            'HargaSatuan.Text = Format(temp, "###,##0")
            'HargaSatuan.SelectionStart = HargaSatuan.TextLength
        End If
    End Sub

    Private Sub PsDisc_TextChanged(sender As Object, e As EventArgs) Handles PsDisc.TextChanged
        If Trim(QtyB.Text) = "" Then QtyB.Text = 0
        If Trim(PsDisc.Text) = "" Then PsDisc.Text = 0
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If Trim(HSatAkhir.Text) = "" Then HSatAkhir.Text = 0
        If Trim(SubTotal.Text) = "" Then SubTotal.Text = 0
        If IsNumeric(PsDisc.Text) Then
            Dim temp As Double = PsDisc.Text
            PsDisc.Text = Format(temp, "###,##0")
            Disc.Text = temp / 100 * (HargaSatuan.Text * 1)
            HSatAkhir.Text = (HargaSatuan.Text * 1) - (Disc.Text * 1)
            SubTotal.Text = Format((QtyB.Text * 1) * (HSatAkhir.Text * 1), "###,##0")
            PsDisc.SelectionStart = PsDisc.TextLength
        End If
    End Sub
    Private Sub PsDisc_GotFocus(sender As Object, e As EventArgs) Handles PsDisc.GotFocus
        With PsDisc
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .ForeColor = Color.Black
            .BackColor = Color.White
            Disc.ForeColor = Color.Black
            Disc.BackColor = Color.White
        End With
    End Sub
    Private Sub PsDisc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PsDisc.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If PsDisc.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            HitungSubTotal()
            AddItem()
            KodeBrg.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub PsDisc_LostFocus(sender As Object, e As EventArgs) Handles PsDisc.LostFocus
        If PsDisc.Text = Nothing Then
            PsDisc.Text = "0"
            PsDisc.ForeColor = Color.Gray
            PsDisc.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub Disc_GotFocus(sender As Object, e As EventArgs) Handles Disc.GotFocus
        With Disc
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
    End Sub

    Private Sub Disc_LostFocus(sender As Object, e As EventArgs) Handles Disc.LostFocus
        If Disc.Text = Nothing Then
            Disc.Text = "0"
            Disc.ForeColor = Color.Gray
            Disc.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub
    Private Sub Disc_TextChanged(sender As Object, e As EventArgs) Handles Disc.TextChanged
        If Trim(Disc.Text) = "" Then Disc.Text = 0
        If IsNumeric(Disc.Text) Then
            Dim temp As Double = Disc.Text
            Disc.Text = Format(temp, "###,##0")
            Disc.SelectionStart = Disc.TextLength
        End If
    End Sub

    Private Sub Disc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Disc.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If PsDisc1.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            HitungSubTotal()
            AddItem()
            KodeBrg.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub isiKodeBrg(IdBrg)
        Dim dbT As DataTable
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)
        cmbSatuanB.Items.Clear()
        SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
                "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
                "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori, HPP, psDisc " &
                " From M_Barang " &
                "Where AktifYN = 'Y' " &
                "  And idRec = '" & KodeBrg.Text & "' "
        dbT = Proses.ExecuteQuery(SQL)
        If dbT.Rows.Count <> 0 Then
            NamaBrg.Text = dbT.Rows(0) !Nama
            KodeBrg.Text = dbT.Rows(0) !IDRec
            QTY.Text = 1
            satuan.Text = dbT.Rows(0) !satuan
            QtyB.Text = 1
            cmbSatuanB.Items.Clear()
            cmbSatuanB.Items.Add(dbT.Rows(0) !satuan)
            cmbSatuanB.Items.Add(IIf(IsDBNull(dbT.Rows(0) !satuanT), "", dbT.Rows(0) !satuanT))
            cmbSatuanB.Items.Add(IIf(IsDBNull(dbT.Rows(0) !satuanB), "", dbT.Rows(0) !satuanB))
            cmbSatuanB.Text = satuan.Text
            isiSatB.Text = Format(dbT.Rows(0) !IsiSatB, "###,##0")
            IsiSatT.Text = Format(dbT.Rows(0) !IsiSatT, "###,##0")

            HargaSatuan.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
            HSatAkhir.Text = Format(dbT.Rows(0)!PriceList, "###,##0")

            harga1.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
            harga2.Text = Format(dbT.Rows(0)!PriceList2, "###,##0")
            harga3.Text = Format(dbT.Rows(0)!PriceList3, "###,##0")

            HargaModal.Text = dbT.Rows(0) !HPP
            HargaSatuan_Asli.Text = HargaSatuan.Text
            If FrmMenuUtama.CompCode.Text = "KM" Then
                PsDisc.Text = Format(dbT.Rows(0)!psdisc, "###,##0")
                If (dbT.Rows(0)!psdisc > 0) Then
                    Dim hargaAkhir As Double = dbT.Rows(0)!PriceList - (dbT.Rows(0)!PriceList * dbT.Rows(0)!psdisc / 100)
                    HSatAkhir.Text = Format(hargaAkhir, "###,##0")
                    SubTotal.Text = Format(hargaAkhir, "###,##0")
                Else
                    HSatAkhir.Text = dbT.Rows(0)!PriceList
                    SubTotal.Text = HargaSatuan.Text
                End If
            Else
                HSatAkhir.Text = dbT.Rows(0)!PriceList
                SubTotal.Text = HargaSatuan.Text
            End If
        End If
    End Sub
    Private Sub KodeBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeBrg.KeyPress
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)
        If e.KeyChar = Chr(13) Then
            'If Trim(KodeBrg.Text) = "" Then Exit Sub
            SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
                "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
                "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori " &
                " From M_Barang " &
                "Where AktifYN = 'Y' " &
                "  And idRec = '" & KodeBrg.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                isiKodeBrg(dbTable.Rows(0) !idrec)
                QtyB.Focus()
            Else
                Form_DaftarBarang.kode_toko.Text = Mid(Kode_Toko.Text, 4, 2)
                Form_DaftarBarang.txt_Nama_Barang.Text = KodeBrg.Text
                Form_DaftarBarang.Cari()
                Form_DaftarBarang.ShowDialog()
                KodeBrg.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                KodeBrg.ForeColor = Color.Black
                KodeBrg.BackColor = Color.White
                If Trim(KodeBrg.Text) = "" Then
                    KodeBrg.Text = ""
                Else
                    SQL = "Select IDRec " &
                        " From M_Barang " &
                        "Where AktifYN = 'Y' " &
                        "  And idrec = '" & KodeBrg.Text & "' "
                    dbTable = Proses.ExecuteQuery(SQL)
                    If dbTable.Rows.Count <> 0 Then
                        isiKodeBrg(dbTable.Rows(0) !idrec)
                        QtyB.Focus()
                    Else
                        KodeBrg.Text = ""
                        KodeBrg.ForeColor = Color.Black
                        KodeBrg.BackColor = Color.White
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        HitungSubTotal()
        AddItem()
        KodeBrg.Focus()
    End Sub


    Private Sub HargaSatuan_Click(sender As Object, e As EventArgs) Handles HargaSatuan.Click
        HargaSatuan.SelectionStart = HargaSatuan.TextLength
    End Sub
    Private Sub HargaSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaSatuan.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HargaSatuan.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            Dim temp As Double = HargaSatuan.Text
            HargaSatuan.Text = Format(temp, "###,##0")
            HitungSubTotal()
            If FrmMenuUtama.CompCode.Text = "KM" Then
                PsDisc.Focus()
            Else
                btnAdd.Focus()
            End If
        Else
                e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
    End Sub

    Private Sub btnCariKodeBrg_Click(sender As Object, e As EventArgs) Handles btnCariKodeBrg.Click
        KodeToko = Mid(Kode_Toko.Text, 4, 2)
        btnCariKodeBrg.Enabled = False
        Dim mCari As String = ""
        If Trim(KodeBrg.Text) = "<Kode_Brg>" Then
            mCari = ""
        Else
            mCari = Trim(KodeBrg.Text)
        End If
        Form_DaftarBarang.kode_toko.Text = KodeToko
        Form_DaftarBarang.tCari.Text = mCari
        Form_DaftarBarang.Cari()
        Form_DaftarBarang.ShowDialog()
        KodeBrg.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        KodeBrg.ForeColor = Color.Black
        KodeBrg.BackColor = Color.White
        If Trim(KodeBrg.Text) = "" Then
            KodeBrg.Text = ""
        Else
            SQL = "Select IDRec " &
                " From M_Barang " &
                "Where AktifYN = 'Y' " &
                "  And idrec = '" & KodeBrg.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                isiKodeBrg(dbTable.Rows(0) !idrec)
                QTY.Focus()
            Else
                KodeBrg.Text = ""
                KodeBrg.ForeColor = Color.Black
                KodeBrg.BackColor = Color.White
            End If
        End If
        btnCariKodeBrg.Enabled = True
    End Sub

    Private Sub cmbSatuanB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSatuanB.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbSatuanB_SelectedIndexChanged(sender, e)
            HargaSatuan.Focus()
        End If
    End Sub
    Private Sub cmbSatuanB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSatuanB.SelectedIndexChanged
        If SelectedItemIndex = cmbSatuanB.SelectedIndex Then
            SelectedItemIndex = -1
            'tbxSelectedItem.Text = ""
        Else
            SelectedItemIndex = cmbSatuanB.SelectedIndex
            'tbxSelectedItem.Text = ComboBox1.Items(SelectedItemIndex)
            Dim value As Integer = cmbSatuanB.SelectedIndex
            Select Case value
                Case 0
                    HargaSatuan.Text = harga1.Text
                    QTY.Text = QtyB.Text
                Case 1
                    HargaSatuan.Text = harga2.Text
                    QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0")
                Case 2
                    HargaSatuan.Text = harga3.Text
                    QTY.Text = Format((QtyB.Text * 1) * (isiSatB.Text * 1), "###,##0")
                Case Else
                    HargaSatuan.Text = harga1.Text
                    QTY.Text = QtyB.Text
            End Select
            HitungSubTotal()
            HargaSatuan_Asli.Text = HargaSatuan.Text
            If satuan.Text = cmbSatuanB.Text Then
                QTY.Text = QtyB.Text
            End If
        End If
        'QTY.Focus()
    End Sub

    Private Sub QtyB_LostFocus(sender As Object, e As EventArgs) Handles QtyB.LostFocus
        If QtyB.Text = Nothing Then
            QtyB.Text = "1"
            QtyB.ForeColor = Color.Gray
            QtyB.BackColor = Color.LightGoldenrodYellow
        Else
            If Trim(satuan.Text) = Trim(cmbSatuanB.Text) Then
                QTY.Text = QtyB.Text
            ElseIf Trim(satuan.Text) <> Trim(cmbSatuanB.Text) Then
                Dim value As Integer = cmbSatuanB.SelectedIndex
                Select Case value
                    Case 0
                        HargaSatuan.Text = harga1.Text
                        QTY.Text = QtyB.Text
                    Case 1
                        HargaSatuan.Text = harga2.Text
                        QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0.00")
                    Case 2
                        HargaSatuan.Text = harga3.Text
                        QTY.Text = Format((QtyB.Text * 1) * (isiSatB.Text * 1), "###,##0.00")
                    Case Else
                        HargaSatuan.Text = harga1.Text
                        QTY.Text = QtyB.Text
                End Select
                HitungSubTotal()
            End If
        End If
    End Sub
    Private Sub QtyB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyB.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If QtyB.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(QtyB.Text) Then
                Dim temp As Double = QtyB.Text
                QtyB.Text = Format(temp, "###,##0.00")
                QtyB.SelectionStart = QtyB.TextLength
            Else
                QtyB.Text = 0
            End If
            HitungSubTotal()
            SendKeys.SendWait("{Tab}")
            'cmbSatuanB.Focus()
            cmbSatuanB.Select()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub QtyB_TextChanged(sender As Object, e As EventArgs) Handles QtyB.TextChanged
        If Trim(QtyB.Text) = "" Then QtyB.Text = 0
        If IsNumeric(QtyB.Text) Then
            Dim temp As Double = QtyB.Text
            'QtyB.Text = Format(temp, "###,##0")
            QtyB.SelectionStart = QtyB.TextLength
        Else
            QtyB.Text = 0
        End If
    End Sub

    Private Sub QtyB_GotFocus(sender As Object, e As EventArgs) Handles QtyB.GotFocus
        With QtyB
            .SelectionStart = 0
            .SelectionLength = .TextLength
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
    End Sub

    Private Sub QTY_TextChanged(sender As Object, e As EventArgs) Handles QTY.TextChanged
        'If Trim(QTY.Text) = "" Then QTY.Text = 0
        'If IsNumeric(QTY.Text) Then
        '    Dim temp As Double = QTY.Text
        '    QTY.Text = Format(temp, "###,##0")
        '    QTY.SelectionStart = QTY.TextLength
        'End If
    End Sub
    Private Sub QTY_GotFocus(sender As Object, e As EventArgs) Handles QTY.GotFocus
        With QTY
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
    End Sub

    Private Sub QTY_LostFocus(sender As Object, e As EventArgs) Handles QTY.LostFocus
        If QTY.Text = Nothing Then
            QTY.Text = "1"
            QTY.ForeColor = Color.Gray
            QTY.BackColor = Color.LightGoldenrodYellow
        Else

            If Trim(satuan.Text) = Trim(cmbSatuanB.Text) Then
                QtyB.Text = QTY.Text
            ElseIf Trim(satuan.Text) <> Trim(cmbSatuanB.Text) Then
                Dim value As Integer = cmbSatuanB.SelectedIndex
                Select Case value
                    Case 0
                        HargaSatuan.Text = harga1.Text
                        QtyB.Text = QTY.Text
                    Case 1
                        HargaSatuan.Text = harga2.Text
                        QtyB.Text = Replace(Format((QTY.Text * 1) / (IsiSatT.Text * 1), "###,##0.00"), ".00", "")
                    Case 2
                        HargaSatuan.Text = harga3.Text
                        QtyB.Text = Replace(Format((QTY.Text * 1) / (isiSatB.Text * 1), "###,##0.00"), ".00", "")
                    Case Else
                        HargaSatuan.Text = harga1.Text
                        QtyB.Text = QTY.Text
                End Select
                HitungSubTotal()
            End If
        End If
    End Sub

    Private Sub QTY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QTY.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If QTY.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then

            Dim value As Integer = cmbSatuanB.SelectedIndex
            Select Case value
                Case 0
                    HargaSatuan.Text = harga1.Text
                    QtyB.Text = QTY.Text
                Case 1
                    HargaSatuan.Text = harga2.Text
                    QtyB.Text = Replace(Format((QTY.Text * 1) / (IsiSatT.Text * 1), "###,##0.00"), ".00", "")
                Case 2
                    HargaSatuan.Text = harga3.Text
                    QtyB.Text = Replace(Format((QTY.Text * 1) / (isiSatB.Text * 1), "###,##0.00"), ".00", "")
                Case Else
                    HargaSatuan.Text = harga1.Text
                    QTY.Text = QtyB.Text
            End Select
            HitungSubTotal()
            HargaSatuan_Asli.Text = HargaSatuan.Text
            If satuan.Text = cmbSatuanB.Text Then
                QtyB.Text = QTY.Text
            End If
            HitungSubTotal()
            HargaSatuan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub AlamatKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AlamatKirim.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
    End Sub

    Private Sub clearBarcode()
        KodeBrg.Text = "<Kode_Brg>"
        KodeBrg.ForeColor = Color.Gray
        KodeBrg.BackColor = Color.LightGoldenrodYellow
        NamaBrg.Text = ""

        QTY.Text = 1
        QTY.ForeColor = Color.Gray
        satuan.Text = "PCS"
        QtyB.Text = 1
        QtyB.ForeColor = Color.Gray
        cmbSatuanB.SelectedIndex = -1
        PsDisc.Text = 0
        Disc.Text = 0
        HSatAkhir.Text = 0
        HargaSatuan.Text = 0
        HargaModal.Text = 0
        SubTotal.Text = 0

        Flag.Text = ""
        HargaSatuan_Asli.Text = 0
        HargaModal.Text = 0
        isiSatB.Text = 0
        IsiSatT.Text = 0
        harga1.Text = 0
        harga2.Text = 0
        harga3.Text = 0
    End Sub

    Private Sub DGRequest_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellClick
        If DGRequest.Rows.Count = 0 Then Exit Sub
        indexbrg.Text = DGRequest.CurrentCell.RowIndex
        Dim tNamaBrg As String = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value + ", " +
            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value + " " +
            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value
        If e.ColumnIndex = 10 Then 'Hapus
            If Trim(tNamaBrg) <> "" Then
                '    MsgBox("Kas BON ini sudah di buatkan Invoice." & vbCrLf & " Tidak Bisa Edit/Hapus!", vbCritical + vbOKOnly, ".:Warning!")
                If LEdit = True Then
                    SQL = "select postingyn from t_soh where idrec = '" & IDRec.Text & "'"
                    Dim postyn As String = Proses.ExecuteSingleStrQuery(SQL)
                    If postyn = "Y" Then
                        MsgBox("Invoice sudah dibayar, tidak dapat dihapus")
                        Exit Sub
                    End If
                End If

                If MsgBox("Yakin hapus " & tNamaBrg & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                    DGRequest.Rows.RemoveAt(e.RowIndex)
                    HitungTotal()
                End If
            End If
        End If
    End Sub

    Private Sub IsiKodeBrg_Edit(mKodeBrg As String)
        Dim dbT As DataTable
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)
        SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
            "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
            "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori, HPP " &
            " From M_Barang " &
            "Where AktifYN = 'Y' " &
            "  And idRec = '" & mKodeBrg & "' "
        dbT = Proses.ExecuteQuery(SQL)
        If dbT.Rows.Count <> 0 Then
            With Form_Kasir_Edit
                .cmbSatuanB.Items.Clear()
                .cmbSatuanB.Items.Add(dbT.Rows(0) !satuan)
                .cmbSatuanB.Items.Add(dbT.Rows(0) !satuanT)
                .cmbSatuanB.Items.Add(dbT.Rows(0) !satuanB)
                .cmbSatuanB.Text = satuan.Text
                .IsiSatB.Text = Format(dbT.Rows(0) !IsiSatB, "###,##0")
                .IsiSatT.Text = Format(dbT.Rows(0) !IsiSatT, "###,##0")

                .HargaSatuan.Text = Format(dbT.Rows(0) !PriceList, "###,##0")
                .harga1.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
                .harga2.Text = Format(dbT.Rows(0)!PriceList2, "###,##0")
                .harga3.Text = Format(dbT.Rows(0)!PriceList3, "###,##0")

                .HargaModal.Text = dbT.Rows(0) !HPP
                .HargaSatuan_Asli.Text = HargaSatuan.Text
                .SubTotal.Text = HargaSatuan.Text
            End With
        End If
    End Sub

    Private Sub PPN_TextChanged(sender As Object, e As EventArgs) Handles PPN.TextChanged

    End Sub

    Private Sub DP_TextChanged(sender As Object, e As EventArgs) Handles DP.TextChanged
        If Trim(DP.Text) = "" Then DP.Text = 0
        If IsNumeric(DP.Text) Then
            Dim temp As Double = DP.Text
            DP.Text = Format(temp, "###,##0")
            DP.SelectionStart = DP.TextLength
        End If
    End Sub

    Private Sub KodeBrg_TextChanged(sender As Object, e As EventArgs) Handles KodeBrg.TextChanged
        If Len(Trim(KodeBrg.Text)) < 1 Then
            NamaBrg.Text = ""
            QTY.Text = 1
            QTY.ForeColor = Color.Gray
            satuan.Text = "PCS"
            QtyB.Text = 1
            QtyB.ForeColor = Color.Gray
            cmbSatuanB.SelectedIndex = -1
            PsDisc1.Text = 0
            PsDisc1.ForeColor = Color.Gray
            Disc.Text = 0
            Disc.ForeColor = Color.Gray
            HargaSatuan.Text = 0
            HargaModal.Text = 0
            SubTotal.Text = 0
            Flag.Text = ""
            HargaSatuan_Asli.Text = 0
            isiSatB.Text = 0
            IsiSatT.Text = 0
            harga1.Text = 0
            harga2.Text = 0
            harga3.Text = 0
        End If
    End Sub

    Private Sub KodeBrg_GotFocus(sender As Object, e As EventArgs) Handles KodeBrg.GotFocus
        If KodeBrg.Text = "<Kode_Brg>" Then
            KodeBrg.Text = ""
            KodeBrg.ForeColor = Color.Black
            KodeBrg.BackColor = Color.White
        End If
    End Sub

    Private Sub KodeBrg_LostFocus(sender As Object, e As EventArgs) Handles KodeBrg.LostFocus
        If KodeBrg.Text = Nothing Then
            KodeBrg.Text = "<Kode_Brg>"
            KodeBrg.ForeColor = Color.Gray
            KodeBrg.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub DP_GotFocus(sender As Object, e As EventArgs) Handles DP.GotFocus
        If DP.Text = Nothing Then
            DP.Text = "0"

            DP.ForeColor = Color.DarkRed
            DP.BackColor = Color.White
        End If
    End Sub

    Private Sub DP_LostFocus(sender As Object, e As EventArgs) Handles DP.LostFocus
        If DP.Text = Nothing Then
            DP.Text = "0"
            DP.ForeColor = Color.Gray
            DP.BackColor = Color.LightGoldenrodYellow
        Else
            DP.ForeColor = Color.DarkRed
            DP.BackColor = Color.White
        End If
    End Sub

    Private Sub Kode_Toko_TextChanged(sender As Object, e As EventArgs) Handles Kode_Toko.TextChanged
        If Len(Trim(Kode_Toko.Text)) = 0 Then
            NamaToko.Text = ""
        End If
    End Sub

    Private Sub DP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DP.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HargaSatuan.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub DP_Click(sender As Object, e As EventArgs) Handles DP.Click
        If Trim(DP.Text) = "" Then DP.Text = 0
        If IsNumeric(DP.Text) Then
            Dim temp As Double = DP.Text
            DP.Text = Format(temp, "###,##0")
            DP.SelectionStart = DP.TextLength
        End If
    End Sub

    Private Sub cmbSatuanB_GotFocus(sender As Object, e As EventArgs) Handles cmbSatuanB.GotFocus
        cmbSatuanB.DroppedDown = True
    End Sub

    Private Sub Kode_Toko_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Toko.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select * From m_Toko " &
                "Where idrec = '" & Kode_Toko.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Kode_Toko.Text = dbTable.Rows(0) !idrec
                NamaToko.Text = dbTable.Rows(0) !nama
                TglJual.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                     " From m_Toko " &
                     "Where AktifYN = 'Y' " &
                     "  And nama Like '%" & Kode_Toko.Text & "' " &
                     "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                Kode_Toko.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select * From m_Toko " &
                    "Where idrec = '" & Kode_Toko.Text & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Kode_Toko.Text = dbTable.Rows(0) !idrec
                    NamaToko.Text = dbTable.Rows(0) !nama
                    TglJual.Focus()
                Else
                    Kode_Toko.Text = ""
                    NamaToko.Text = ""
                    TglJual.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub cmbJenisBayar_GotFocus(sender As Object, e As EventArgs) Handles cmbJenisBayar.GotFocus
        cmbJenisBayar.DroppedDown = True
    End Sub

    Private Sub Id_Cust_DoubleClick(sender As Object, e As EventArgs) Handles Id_Cust.DoubleClick
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub IDSales_DoubleClick(sender As Object, e As EventArgs) Handles IDSales.DoubleClick
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub IDSales_KeyPress(sender As Object, e As KeyPressEventArgs) Handles IDSales.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select namasales, idrec From m_sales " &
              " Where IDRec = '" & IDSales.Text & "' " &
              " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                IDSales.Text = dbTable.Rows(0)!idrec
                NamaSales.Text = dbTable.Rows(0)!namasales
                KodeBrg.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select idrec, namasales " &
                    " From m_sales " &
                    "Where AktifYN = 'Y' " &
                    "  And ( idrec Like '%" & IDSales.Text & "%' or namasales Like '%" & IDSales.Text & "%') " &
                    "Order By NamaSales "
                Form_Daftar.Text = "Daftar Sales"
                Form_Daftar.ShowDialog()

                IDSales.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select namasales, idrec From m_sales " &
                   " Where IDRec = '" & IDSales.Text & "' " &
                   " and aktifyn = 'Y' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    IDSales.Text = dbTable.Rows(0)!idrec
                    NamaSales.Text = dbTable.Rows(0)!namasales
                    KodeBrg.Focus()
                Else
                    NamaSales.Text = ""
                    IDSales.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            tCustomer.Focus()
            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub tCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCustomer.KeyPress
        If e.KeyChar = Chr(13) Then

            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub IDSales_TextChanged(sender As Object, e As EventArgs) Handles IDSales.TextChanged
        If Len(IDSales.Text) < 1 Then
            NamaSales.Text = ""
        End If
    End Sub

    Private Sub tCustomer_TextChanged(sender As Object, e As EventArgs) Handles tCustomer.TextChanged

    End Sub

    Private Sub cmdSetting_Click(sender As Object, e As EventArgs)
        'Form_Penjualan_CompId.ShowDialog()
        'cmdExit_Click(sender, e)
    End Sub

    Private Sub Form_Penjualan_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If hapusSQL <> "" Then
            Proses.ExecuteNonQuery(hapusSQL)
            hapusSQL = ""
        End If
    End Sub


    Private Sub Kode_Toko_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Kode_Toko.MouseDoubleClick
        If FrmMenuUtama.Kode_Toko.Text = "AG020" Then
            SendKeys.Send("{enter}")
        End If
    End Sub



    Private Sub DGRequest_Click(sender As Object, e As EventArgs) Handles DGRequest.Click
        If DGRequest.Rows.Count > 0 Then
            isiItemBarang()
        End If
    End Sub

    Private Sub Id_Cust_LostFocus(sender As Object, e As EventArgs) Handles Id_Cust.LostFocus
        If Trim(Id_Cust.Text) <> "" Then
            'Dim cekfaktur As Boolean = cekFaktur120()
            'If cekfaktur = False Then
            '    Id_Cust.Focus()
            '    Exit Sub
            'Else
            '    IDSales.Focus()
            'End If
        End If
    End Sub
    'Dim cekpiutang As String = " 
    ' Select  sum(totalsales)
    '  From t_SOH
    ' Where IdCust = '1801MA00688'

    ' select   sum(nilai_bayar)
    '    from t_SOH 
    '   left join t_BayarCustD on t_SOH.idrec = t_BayarCustD.no_nota
    '  where t_SOH.IdCust = '1801MA00688'

    ' select   sum(TotalSales)
    '    from t_SOH 
    '   left join t_BayarCustD on t_SOH.idrec = t_BayarCustD.no_nota
    '  where t_SOH.IdCust = '1801MA00688'
    '    and id_rec is null
    '"
End Class