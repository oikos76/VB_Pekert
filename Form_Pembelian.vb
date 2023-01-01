Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Form_Pembelian
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean, UserID As String = FrmMenuUtama.TsPengguna.Text
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim KodeToko As String = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)
    Dim xHarga As String = "GUDANG" 'My.Settings.DefaultHarga
    Dim SelectedItemIndex As Integer = -1
    Dim hapusSQL As String = String.Empty
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tCetak As Boolean

    Private Sub HitungTotal()
        Dim sum = (From row As DataGridViewRow In DGRequest.Rows.Cast(Of DataGridViewRow)()
                   Select CDec(row.Cells(9).Value)).Sum
        sub_total.Text = Format(sum, "###,##0.00")

        If PsPPN.Text = "" Then PsPPN.Text = 0
        If PPN.Text = "" Then PPN.Text = 0
        If Discount.Text = "" Then Discount.Text = 0
        If PsDisc.Text = "" Or Trim(PsDisc.Text) = "0" Then
            PsDisc.Text = 0
            Discount.Text = 0
        Else
            Discount.Text = Format((PsDisc.Text * 1 / 100) * (sub_total.Text * 1), "###,##0")
        End If
        PPN.Text = Format((PsPPN.Text * 1 / 100) * ((sub_total.Text * 1) - (Discount.Text * 1)), "###,##0")
        Total.Text = Format((sub_total.Text * 1) - (Discount.Text * 1) + (PPN.Text * 1), "###,##0.00")
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
        TglPO.Value = Now
        PsDisc.Text = 0
        Discount.Text = 0
        PsPPN.Text = 0
        PPN.Text = 0
        sub_total.Text = 0
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
            cmdSimpan.Enabled = Not tAktif
        End If

        If tHapus = False Then
            cmdHapus.Enabled = False
        Else
            cmdHapus.Enabled = tAktif
        End If
        cmdSetting.Visible = tAktif

        If tCetak = False Then
            cmdCetak.Enabled = False
        Else
            cmdCetak.Enabled = tAktif
        End If
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif
        tCari.Visible = tAktif
        Cari.Visible = tAktif
    End Sub

    Private Sub Data_Record()
        Dim a As Integer, mKondisi As String = ""
        If tCari.Text <> "" Then
            mKondisi = " and T_POH.idrec like '%" & tCari.Text & "%' "
        End If
        If tSupplier.Text <> "" Then
            mKondisi += " and m_Supplier.Nama like '" & tSupplier.Text & "%' "
        End If
        SQL = "Select T_POH.IDRec, TglPO, NoFaktur, T_POH.Keterangan, " &
            "         Total, m_Supplier.Nama as vendor, postingYN, tterimaYN " &
            " From T_POH inner join m_supplier on id_supplier = m_supplier.idrec " &
            "  And convert(varchar(8),TglPO,112) Between '" & Format(tglCari1.Value, "yyyyMMdd") & "' " &
            "      And '" & Format(tglCari2.Value, "yyyyMMdd") & "' " &
            "Where T_POH.AktifYN = 'Y' " & mKondisi & " " &
            "Order By T_POH.IDRec desc, TglPO desc "
        dbTable = Proses.ExecuteQuery(SQL)

        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a)!IDRec,
                    Format(.Table.Rows(a)!TglPO, "dd-MM-yyyy"), .Table.Rows(a)!NoFaktur,
                    .Table.Rows(a)!Vendor, Format(.Table.Rows(a)!Total, "###,##0"),
                    .Table.Rows(a)!tterimaYN, .Table.Rows(a)!postingYN)
            Next (a)
        End With
        DGView2.Rows.Clear()
    End Sub

    Private Sub Isi_Data()
        DGView.Enabled = False
        SQL = " Select t_POH.IDRec, tglPO, t_POH.Keterangan, " &
            " ID_Supplier, M_Supplier.Nama as NamaVendor, t_POH.Keterangan, NoFaktur, " &
            " t_POH.Total, t_POH.SubTotal, t_POH.PPN, t_POH.PsPPN, t_POH.PsDisc, t_POH.Disc " &
            " From t_POH Inner Join M_Supplier on ID_Supplier = M_Supplier.IDRec " &
            "Where t_POH.IDRec = '" & IDRec.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Keterangan.Text = dbTable.Rows(0) !Keterangan
            IDVendor.Text = dbTable.Rows(0) !ID_Supplier
            NamaVendor.Text = dbTable.Rows(0) !NamaVendor
            NoFaktur.Text = dbTable.Rows(0) !NoFaktur
            Keterangan.Text = dbTable.Rows(0) !Keterangan
            sub_total.Text = Format(dbTable.Rows(0) !SubTotal, "###,##0")
            PsPPN.Text = Format(dbTable.Rows(0) !PsPPN, "###,##0")
            PPN.Text = Format(dbTable.Rows(0) !PPN, "###,##0")
            PsDisc.Text = Format(dbTable.Rows(0) !PsDisc, "###,##0")
            Discount.Text = Format(dbTable.Rows(0) !disc, "###,##0")
            Total.Text = Format(dbTable.Rows(0)!Total, "###,##0")
            TglPO.Value = dbTable.Rows(0) !tglpo
        End If

        DGView2.Rows.Clear()
        DGRequest.Rows.Clear()

        SQL = "Select ID_Rec, KodeBrg, m_Barang.Nama, t_POD.QTY, t_POD.Satuan, " &
            " t_POD.QTYB, t_POD.SatuanB, t_POD.PsDisc, t_POD.Disc, t_POD.harga, t_POD.subtotal, t_POD.hargajual " &
            "From t_POD inner Join m_Barang on KodeBrg = m_Barang.IDRec " &
            "Where ID_Rec = '" & IDRec.Text & "' and t_POD.AktifYN = 'Y' " &
            "Order By NoUrut "
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView2.Rows.Add(.Table.Rows(a)!KodeBrg,
                    .Table.Rows(a)!Nama,
                    Format(.Table.Rows(a)!qtyB, "###,##0.00"), .Table.Rows(a)!SatuanB,
                    Format(.Table.Rows(a)!qty, "###,##0.00"), .Table.Rows(a)!Satuan,
                    Format(.Table.Rows(a)!harga, "###,##0"),
                    Format(.Table.Rows(a)!PsDisc, "###,##0"), Format(.Table.Rows(a)!Disc, "###,##0"),
                    Format(.Table.Rows(a)!subtotal, "###,##0"))
                DGRequest.Rows.Add(.Table.Rows(a)!KodeBrg,
                    .Table.Rows(a)!Nama,
                    Format(.Table.Rows(a)!qtyB, "###,##0.00"), .Table.Rows(a)!SatuanB,
                    Format(.Table.Rows(a)!qty, "###,##0.00"), .Table.Rows(a)!Satuan,
                    Format(.Table.Rows(a)!harga, "###,##0"),
                    Format(.Table.Rows(a)!PsDisc, "###,##0"), Format(.Table.Rows(a)!Disc, "###,##0"),
                    Format(.Table.Rows(a)!subtotal, "###,##0"), "Hapus",
                    Format(.Table.Rows(a)!HargaJual, "###,##0"))
            Next (a)
        End With
        HitungTotal()
        DGView.Enabled = True
    End Sub

    Private Sub Form_Pembelian_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClearTextBoxes()
        CekTable()
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGRequest.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGRequest.BackgroundColor = Color.LightGray
        DGRequest.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGRequest.DefaultCellStyle.SelectionForeColor = Color.White
        DGRequest.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        'DGRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False

        DGRequest.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGRequest.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        With Me.DGView.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With
        With Me.DGView2.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With
        With Me.DGRequest.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With
        Dim lastdaymonth As Integer = Date.DaysInMonth(Date.Now.Year, Date.Now.Month)
        tglCari1.Value = New Date(Date.Now.Year, Date.Now.Month, "1")
        tglCari2.Value = New Date(Date.Now.Year, Date.Now.Month, lastdaymonth)

        LAdd = False
        LEdit = False
        Kode_Toko.Text = FrmMenuUtama.Kode_Toko.Text
        KodeToko = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)
        'TabControl1.Location = New System.Drawing.Point(87, 6)

        tTambah = Proses.UserAksesTombol(UserID, "FAKTUR_PEMBELIAN", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "FAKTUR_PEMBELIAN", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "FAKTUR_PEMBELIAN", "hapus")
        tCetak = Proses.UserAksesTombol(UserID, "FAKTUR_PEMBELIAN", "laporan")

        Dim kolomIdTagihan As Boolean = Proses.UserAksesMenu(UserID, "TANDA_TERIMA_TAGIHAN_SUPPLIER")
        Dim kolomPosting As Boolean = Proses.UserAksesMenu(UserID, "POSTING_FAKTUR_PEMBELIAN")
        DGView.Columns("_idttd").Visible = kolomIdTagihan
        DGView.Columns("_postingyn").Visible = kolomPosting

        TabPage1.Text() = "Daftar Purchase Order"
        AturTombol(True)
        TabControl1.SelectedTab = TabPage1
        TabControl1.TabPages.RemoveAt(1)
        TabControl1.SelectedTab = TabPage1
        Data_Record()
        Dim tID As String = ""
        If DGView.RowCount <> 0 Then
            tID = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            tID = ""
        End If
        DGView.Font = New Font("Arial", 10, FontStyle.Regular)
        DGView2.Font = New Font("Arial", 10, FontStyle.Regular)


        DGRequest.Font = New Font("Arial", 10, FontStyle.Regular)

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        'If (FrmMenuUtama.Kode_Toko.Text <> "AG020") Then
        '    MsgBox("Kode Toko bukan Gudang Pusat !", vbCritical + vbOKOnly, ".:Warning! ")
        '    Exit Sub
        'End If

        LAdd = True
        LEdit = False
        AturTombol(False)
        cmdSimpan.Enabled = True
        TabControl1.TabPages.Insert(1, tabPage2)
        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.SelectedTab = tabPage2

        ClearTextBoxes()
        NoFaktur.Focus()
        Kode_Toko.Text = FrmMenuUtama.Kode_Toko.Text
        NamaToko.Text = FrmMenuUtama.Nama_Toko.Text
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        SQL = "select postingYN From t_poh where idrec = '" & IDRec.Text & "'"
        Dim postingYN As String = Proses.ExecuteSingleStrQuery(SQL)
        If postingYN = "Y" Then
            MsgBox("Sudah diposting, tidak bisa diedit")
            Exit Sub
        End If
        Isi_Data()

        LAdd = False
        LEdit = True
        AturTombol(False)
        'cmdSimpan.Enabled = False
        cmdSimpan.Visible = tEdit

        TabControl1.TabPages.Insert(1, tabPage2)
        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.SelectedTab = tabPage2
    End Sub

    Private Sub cmdHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHapus.Click
        Dim QTYB As Double = 0, KodeBrg As String = "", mGudang As String = ""

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
        SQL = "select postingyn from t_poh where idrec = '" & IDRec.Text & "'"
        Dim postyn As String = Proses.ExecuteSingleStrQuery(SQL)
        If postyn = "Y" Then
            MsgBox("Invoice pembelian sudah dibayar, tidak dapat dihapus")
            Exit Sub
        End If
        SQL = "select tterimaYN from t_poh where idrec = '" & IDRec.Text & "'"
        postyn = Proses.ExecuteSingleStrQuery(SQL)
        If postyn = "Y" Then
            MsgBox("Sudah ada tanda terima pembelian, tidak dapat dihapus")
            Exit Sub
        End If
        If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Update t_POH set AktifYN = 'N', " &
                " UserID = '" & UserID & "', " &
                "LastUPD = GetDate() " &
                "where idrec = '" & IDRec.Text & "'"
            Proses.ExecuteNonQuery(SQL)

            SQL = "Select * From t_POD " &
                            "Where ID_Rec = '" & IDRec.Text & "' " &
                            "  And AktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            With dbTable.Columns(0)
                For a = 0 To dbTable.Rows.Count - 1
                    QTYB = .Table.Rows(a) !qty
                    KodeBrg = .Table.Rows(a) !KodeBrg
                    SQL = "Update m_Barang Set " &
                        " stock" & KodeToko & " = stock" & KodeToko & " - " & QTYB * 1 & ", LastUPD = GetDate() " &
                        "Where IDRec = '" & Trim(KodeBrg) & "' "
                    Proses.ExecuteNonQuery(SQL)
                Next (a)
            End With


            SQL = "Update t_POD set AktifYN = 'N', " &
                " UserID = '" & UserID & "', " &
                "LastUPD = GetDate() " &
                "where id_rec = '" & IDRec.Text & "'"
            Proses.ExecuteNonQuery(SQL)

            Data_Record()
        End If
    End Sub

    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click
        Dim tNo As String = "", tKode As String = ""
        Dim KodeBrg As String = "", tKeterangan As String = ""
        Dim QTY As Double = 0, Satuan As String = ""
        Dim QTYB As Double = 0, SatuanB As String = ""
        Dim Harga As Double = 0, tSubTotal As Double = 0
        Dim PriceList As Double = 0
        Dim tPsDisc As Double = 0, tDisc As Double = 0
        If Proses.ProsesClosing = True Then
            MsgBox("SEDANG ADA PROSES HITUNG STOCK....", vbCritical + vbOKOnly, ".:Tidak bisa simpan data !")
            Exit Sub
        End If
        If Trim(IDVendor.Text) = "" Then
            MsgBox("Vendor tidak boleh kosong!", vbCritical + vbOKOnly, ".:Error!")
            IDVendor.Focus()
            Exit Sub
        Else
            SQL = "Select idrec, Nama From M_Supplier " &
                    "Where IdRec = '" & IDVendor.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaVendor.Text = dbTable.Rows(0) !Nama
                IDVendor.Text = dbTable.Rows(0) !idrec
            Else
                MsgBox("Id Vendor tidak terdaftar/salah input !", vbCritical + vbOKOnly, ".:Warning!")
                NamaVendor.Text = ""
                IDVendor.Focus()
                Exit Sub
            End If
        End If

        If LAdd Then

            'YYB TK TR 99999
            'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber

            IDRec.Text = Proses.GetMaxId("t_POH", "idrec", "FPB-")
            'IDRec.Text = Proses.GetMaxIdTrans("t_POH", "idrec", "FPB")
            SQL = "Insert into t_POH (idrec, NoFaktur, TglPO, ID_Supplier, Supplier, " &
                " Keterangan, SubTotal, PsPPN, PPN, PsDisc, Disc, Total, " &
                " AktifYN, UserID, LastUPD, PostingYN, TTerimaYN) values ('" & IDRec.Text & "','" & NoFaktur.Text & "',  " &
                " '" & Format(TglPO.Value, "yyyy-MM-dd") & "','" & IDVendor.Text & "',  " &
                " '" & NamaVendor.Text & "',  '" & Replace(Trim(Keterangan.Text), "'", "`") & "', " &
                " " & sub_total.Text * 1 & ", " & PsPPN.Text * 1 & ", " & PPN.Text * 1 & ", " &
                " " & PsDisc.Text * 1 & ", " & Discount.Text * 1 & ", " & Total.Text * 1 & ", " &
                " 'Y', '" & UserID & "', GetDate(), 'N', 'N')"
            Proses.ExecuteNonQuery(SQL)

            tNo = ""
            For i As Integer = 0 To DGRequest.Rows.Count - 1
                If i >= DGRequest.Rows.Count Then Exit For
                tNo = Microsoft.VisualBasic.Right(101 + i, 2)

                KodeBrg = DGRequest.Rows(i).Cells(0).Value
                tKeterangan = DGRequest.Rows(i).Cells(1).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
                    QTYB = 0
                Else
                    QTYB = DGRequest.Rows(i).Cells(2).Value
                End If
                SatuanB = DGRequest.Rows(i).Cells(3).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
                    QTY = 0
                Else
                    QTY = DGRequest.Rows(i).Cells(4).Value
                End If
                Satuan = DGRequest.Rows(i).Cells(5).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
                    Harga = 0
                Else
                    Harga = DGRequest.Rows(i).Cells(6).Value
                End If


                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(7).Value) Then
                    tPsDisc = 0
                Else
                    tPsDisc = DGRequest.Rows(i).Cells(7).Value
                End If
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(8).Value) Then
                    tDisc = 0
                Else
                    tDisc = DGRequest.Rows(i).Cells(8).Value
                End If
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(9).Value) Then
                    tSubTotal = 0
                Else
                    tSubTotal = DGRequest.Rows(i).Cells(9).Value
                End If
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(11).Value) Then
                    PriceList = 0
                Else
                    PriceList = DGRequest.Rows(i).Cells(11).Value
                End If
                SQL = "INSERT INTO t_POD (ID_ReC, NoUrut, KodeBrg, NamaBrg, QTYB, SatuanB, " &
                    " QTY, Satuan, Harga, PsDisc, Disc, SubTotal, hargaJual, UserID, AktifYN, LastUPD) " &
                    " VALUES ( '" & IDRec.Text & "', '" & tNo & "', '" & KodeBrg & "', " &
                    " '" & tKeterangan & "', " & QTYB & ", '" & SatuanB & "', " &
                    " " & QTY & ", '" & Satuan & "', " & Harga & ", " & tPsDisc & ", " &
                    " " & tDisc & ", " & tSubTotal & ", " & PriceList & ", '" & UserID & "',  'Y', GetDate())"
                Proses.ExecuteNonQuery(SQL)

                'HargaBeli = " & Harga & ", HPP = " & Harga & ", 
                HargaModal.Text = Harga / (QTY / QTYB)
                SQL = "Update m_Barang Set  " &
                    " stock" & KodeToko & " = stock" & KodeToko & " + " & QTY * 1 & ", " &
                    " hargabeli = " & HargaModal.Text * 1 & ", " &
                    "    HPP =  " & HargaModal.Text * 1 & ", " &
                    " userid = '" & UserID & "' " &
                    "Where IDRec = '" & Trim(KodeBrg) & "' "
                Proses.ExecuteNonQuery(SQL)


                Dim idtr As String = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) & "TR")
                Dim saldo As Double = 0
                SQL = "Select stock" & Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) & " " &
                    "  from m_barang " &
                    " where idrec = '" & Trim(KodeBrg) & "'  "
                saldo = Proses.ExecuteSingleDblQuery(SQL)
                tSubTotal = QTY * PriceList
                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & idtr & "', '" & FrmMenuUtama.Kode_Toko.Text & "', 'Pembelian', '" & IDRec.Text & "', " &
                    "'" & Format(TglPO.Value, "yyyy-MM-dd") & "', '" & DGRequest.Rows(i).Cells(0).Value & "',  " &
                    "" & QTY & ", 0, " & saldo & ",  '" & Satuan & "',  " & QTY & ",  " & PriceList & ", " &
                    "" & tSubTotal & ", '" & UserID & "', GetDate(), '" & FrmMenuUtama.Kode_Toko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next

            If MsgBox("Data berhasil disimpan, mau tambah data lagi?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                ClearTextBoxes()
            Else
                LAdd = False
                LEdit = False
                AturTombol(True)
                TabControl1.TabPages.Insert(0, TabPage1)
                TabControl1.TabPages.Remove(tabPage2)
                TabControl1.SelectedTab = TabPage1
            End If
        ElseIf LEdit Then

            SQL = "Update t_poh set " &
                "        TglPO = '" & Format(TglPO.Value, "yyyy-MM-dd") & "', " &
                "     NoFaktur = '" & NoFaktur.Text & "', " &
                "  ID_Supplier = '" & IDVendor.Text & "', " &
                "     Keterangan = '" & Replace(Trim(Keterangan.Text), "'", "`") & "', " &
                "       SubTotal = " & sub_total.Text * 1 & ", " &
                "  PsDisc = " & PsDisc.Text * 1 & ", Disc = " & Discount.Text * 1 & ", " &
                "  PsPPN = " & PsPPN.Text * 1 & ", PPN = " & PPN.Text * 1 & ", " &
                "  Total = " & Total.Text * 1 & ", " &
                " UserID = '" & UserID & "', LastUPD = GetDate() " &
                "Where idrec = '" & IDRec.Text & "' " &
                "  "
            Proses.ExecuteNonQuery(SQL)


            SQL = "Select * From t_POD " &
                "Where ID_Rec = '" & IDRec.Text & "' " &
                "  And AktifYN = 'Y'  "
            dbTable = Proses.ExecuteQuery(SQL)
            With dbTable.Columns(0)
                For a = 0 To dbTable.Rows.Count - 1
                    QTY = .Table.Rows(a) !qty
                    KodeBrg = .Table.Rows(a) !KodeBrg
                    SQL = "Update m_Barang Set " &
                        " stock" & KodeToko & " = stock" & KodeToko & " - " & QTY * 1 & " " &
                        "Where IDRec = '" & Trim(KodeBrg) & "'  "
                    Proses.ExecuteNonQuery(SQL)

                    Dim idtr As String = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) & "TR")
                    Dim saldo As Double = 0
                    SQL = "Select stock" & Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) & " " &
                        "  from m_barang " &
                        " where idrec = '" & Trim(KodeBrg) & "'  "
                    saldo = Proses.ExecuteSingleDblQuery(SQL)
                    tSubTotal = QTY * .Table.Rows(a)!hargaJual
                    SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                        "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                        "VALUES ( '" & idtr & "', '" & FrmMenuUtama.Kode_Toko.Text & "', 'Edit Pembelian',  " &
                        "'" & IDRec.Text & "', '" & Format(TglPO.Value, "yyyy-MM-dd") & "', '" & Trim(KodeBrg) & "',  " &
                        "0, " & QTY & ", " & saldo & ",  '" & .Table.Rows(a)!Satuan & "',  " & QTY & ",  " & .Table.Rows(a)!hargaJual & ", " &
                        "" & tSubTotal & ", '" & UserID & "', GetDate(), '" & FrmMenuUtama.Kode_Toko.Text & "') "
                    Proses.ExecuteNonQuery(SQL)
                Next (a)
            End With


            SQL = "Delete From t_POD " &
                "Where ID_Rec = '" & IDRec.Text & "' "
            Proses.ExecuteNonQuery(SQL)

            tNo = ""
            For i As Integer = 0 To DGRequest.Rows.Count - 1
                If i >= DGRequest.Rows.Count Then Exit For
                tNo = Microsoft.VisualBasic.Right(101 + i, 2)

                KodeBrg = DGRequest.Rows(i).Cells(0).Value
                tKeterangan = DGRequest.Rows(i).Cells(1).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
                    QTYB = 0
                Else
                    QTYB = DGRequest.Rows(i).Cells(2).Value
                End If
                SatuanB = DGRequest.Rows(i).Cells(3).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
                    QTY = 0
                Else
                    QTY = DGRequest.Rows(i).Cells(4).Value
                End If
                Satuan = DGRequest.Rows(i).Cells(5).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
                    Harga = 0
                Else
                    Harga = DGRequest.Rows(i).Cells(6).Value
                End If


                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(7).Value) Then
                    tPsDisc = 0
                Else
                    tPsDisc = DGRequest.Rows(i).Cells(7).Value
                End If
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(8).Value) Then
                    tDisc = 0
                Else
                    tDisc = DGRequest.Rows(i).Cells(8).Value
                End If
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(9).Value) Then
                    tSubTotal = 0
                Else
                    tSubTotal = DGRequest.Rows(i).Cells(9).Value
                End If
                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(11).Value) Then
                    PriceList = 0
                Else
                    PriceList = DGRequest.Rows(i).Cells(11).Value
                End If

                SQL = "INSERT INTO t_POD (ID_ReC, NoUrut, KodeBrg, NamaBrg, QTYB, SatuanB, " &
                    " QTY, Satuan, Harga, PsDisc, Disc, SubTotal, hargaJual, UserID, AktifYN, LastUPD) " &
                    " VALUES ( '" & IDRec.Text & "', '" & tNo & "', '" & KodeBrg & "', " &
                    " '" & tKeterangan & "', " & QTYB & ", '" & SatuanB & "', " &
                    " " & QTY & ", '" & Satuan & "', " & Harga & ", " & tPsDisc & ", " &
                    " " & tDisc & ", " & tSubTotal & ", " & PriceList & ", '" & UserID & "',  'Y', GetDate())"
                Proses.ExecuteNonQuery(SQL)

                HargaModal.Text = Harga / (QTY / QTYB)
                SQL = "Update m_Barang Set  " &
                    " stock" & KodeToko & " = stock" & KodeToko & " + " & QTY * 1 & ", " &
                    " hargabeli = " & HargaModal.Text * 1 & ", " &
                    "    HPP =  " & HargaModal.Text * 1 & ", " &
                    " userid = '" & UserID & "' " &
                    "Where IDRec = '" & Trim(KodeBrg) & "' "
                Proses.ExecuteNonQuery(SQL)


                Dim idtr As String = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) & "TR")
                Dim saldo As Double = 0
                SQL = "Select stock" & Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) & " " &
                    "  from m_barang " &
                    " where idrec = '" & Trim(KodeBrg) & "'  "
                saldo = Proses.ExecuteSingleDblQuery(SQL)
                tSubTotal = QTY * PriceList
                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & idtr & "', '" & FrmMenuUtama.Kode_Toko.Text & "', 'Pembelian', '" & IDRec.Text & "', " &
                    "'" & Format(TglPO.Value, "yyyy-MM-dd") & "', '" & DGRequest.Rows(i).Cells(0).Value & "',  " &
                    "" & QTY & ", 0, " & saldo & ",  '" & Satuan & "',  " & QTY & ",  " & PriceList & ", " &
                    "" & tSubTotal & ", '" & UserID & "', GetDate(), '" & FrmMenuUtama.Kode_Toko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next

            LAdd = False
            LEdit = False
            AturTombol(True)
            TabControl1.TabPages.Insert(0, TabPage1)
            TabControl1.TabPages.Remove(tabPage2)
            TabControl1.SelectedTab = TabPage1
        End If

        Call Data_Record()
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
        TabControl1.TabPages.Remove(tabPage2)
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub IDVendor_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles IDVendor.KeyPress

        If e.KeyChar = Chr(13) Then
            Form_Daftar.txtQuery.Text = "Select IdRec, Nama, Alamat1, Phone, kota " &
                " From M_Supplier " &
                "Where AktifYN = 'Y' " &
                "  And Nama Like '" & IDVendor.Text & "%' " &
                "Order By Nama "
            Form_Daftar.Text = "Daftar Supplier"
            Form_Daftar.ShowDialog()
            IDVendor.Text = FrmMenuUtama.TSKeterangan.Text
            FrmMenuUtama.TSKeterangan.Text = ""
            If Trim(IDVendor.Text) = "" Then
                NamaVendor.Text = ""
                IDVendor.Focus()
            Else
                SQL = "Select Nama From M_Supplier " &
                    "Where IdRec = '" & IDVendor.Text & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    NamaVendor.Text = dbTable.Rows(0) !Nama
                    KodeBrg.Focus()
                Else
                    NamaVendor.Text = ""
                    IDVendor.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub IDVendor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IDVendor.TextChanged
        If Len(IDVendor.Text) < 1 Then
            NamaVendor.Text = ""
        End If
    End Sub


    Private Sub TglPO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TglPO.KeyDown
        If e.KeyCode = 13 Then IDVendor.Focus()
    End Sub

    Private Sub PsDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PsDisc.TextChanged
        If Trim(PsDisc.Text) = "" Then
            PsDisc.Text = 0
            Discount.Text = 0
        ElseIf Trim(PsPPN.Text) = "" Then
            PsPPN.Text = 0
            PPN.Text = 0
        Else
            HitungTotal()
        End If
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

    Private Sub NoFaktur_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NoFaktur.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then TglPO.Focus()
    End Sub

    Private Sub Keterangan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Keterangan.KeyPress
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
    End Sub

    Private Sub DGView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim tID As String = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        IDRec.Text = tID
        Isi_Data()
    End Sub

    Private Sub DGView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView.CellDoubleClick
        cmdEdit_Click(sender, e)
    End Sub

    Private Sub DGRequest_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGRequest.CellEndEdit
        Dim tID As String = ""
        Dim cRow As Integer = DGRequest.CurrentCell.RowIndex
        Dim cCol As Integer = DGRequest.CurrentCell.ColumnIndex
        Dim QTY As Double = 0, tQTYB As Double = 0, tNo As Integer = 0
        Dim IsiUnit As Double = 0, tSatuan As String = ""
        Dim tHarga As Double = 0, Pengali As Integer = 0
        Dim tPsDisc As Double = 0, tDisc As Double = 0
        Dim tSubTot As Double = 0
        tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
        If cCol = 0 Then
            SQL = "Select IDRec, Nama, Satuan, IsiSatB, isiSatT, SatuanB, SatuanT, " &
                "         Stock" & KodeToko & " as QTY, hargabeli " &
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
                If IsDBNull(dbTable.Rows(0) !hargabeli) Then
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(0, "###,##0") 'harga
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(0, "###,##0") 'subtotal
                Else
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                End If
                isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatB), 0, dbTable.Rows(0) !isisatB)
                IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatT), 0, dbTable.Rows(0) !isisatT)
                SatB.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanB), "", dbTable.Rows(0) !satuanB)
                SatT.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanT), "", dbTable.Rows(0) !satuanT)
                SatK.Text = IIf(IsDBNull(dbTable.Rows(0) !satuan), "", dbTable.Rows(0) !satuan)
                SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")

            Else
                Form_DaftarBarang.kode_toko.Text = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) '"20" 'Id_Asal.Text
                Form_DaftarBarang.tCari.Text = tID
                Form_DaftarBarang.JenisTR.Text = "PEMBELIAN"
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
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0) !idrec
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0) !nama
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 1
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0) !SatuanB
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = dbTable.Rows(0) !IsiSatB
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = dbTable.Rows(0) !Satuan
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(0, "###,##0") 'PsDisc
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value = Format(0, "###,##0") 'isc
                        If IsDBNull(dbTable.Rows(0) !hargabeli) Then
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(0, "###,##0") 'harga
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(0, "###,##0") 'subtotal
                        Else
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                        End If
                        isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatB), 0, dbTable.Rows(0) !isisatB)
                        IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatT), 0, dbTable.Rows(0) !isisatT)
                        SatB.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanB), "", dbTable.Rows(0) !satuanB)
                        SatT.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanT), "", dbTable.Rows(0) !satuanT)
                        SatK.Text = IIf(IsDBNull(dbTable.Rows(0) !satuan), "", dbTable.Rows(0) !satuan)
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

            DGRequest.Rows(e.RowIndex).Cells(9).Value = Format(QTY * tHarga, "###,##0")

            DGRequest.Rows(e.RowIndex).Cells(4).Value = Format(QTY * isiSatB.Text, "###,##0")
            HitungTotal()
            SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
        ElseIf cCol = 3 Then  'Satuan_B  not_yet
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
            ElseIf tSatuan = SatT.Text Then
                Pengali = IsiSatT.Text * 1
            ElseIf tSatuan = SatB.Text Then
                Pengali = isiSatB.Text * 1
            End If
            'DGRequest.Rows(e.RowIndex).Cells(2).Value = Format(QTY, "###,##0")
            DGRequest.Rows(e.RowIndex).Cells(4).Value = Format(QTY * Pengali, "###,##0")
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


    Private Sub btnCariKodeBrg_Click(sender As Object, e As EventArgs) Handles btnCariKodeBrg.Click
        KodeToko = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)
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

    Private Sub DGRequest_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGRequest.KeyDown
        If DGRequest.Rows.Count = 0 Then Exit Sub
        Dim cRow As Integer = DGRequest.CurrentCell.RowIndex, mGudang As String = ""
        Dim cCol As Integer = DGRequest.CurrentCell.ColumnIndex
        Dim tID As String = "", tUnit As String = "", isiUnit As Double = 0, SatuanB As String = ""
        Dim QTYB As Integer = 0, QTYK As Integer = 0, tQTYK As Integer = 0, tIdRap As String = ""
        Dim tNo As Integer = 0, NoLama As Integer = 0, tJenis As String = ""
        If e.KeyCode = Keys.Enter Then
            tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
            If cCol = 0 Then
                SQL = "Select IDRec, Nama, Satuan, IsiSatB, isiSatT, SatuanB, SatuanT, " &
                "         Stock" & KodeToko & " as QTY, hargabeli " &
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
                    If IsDBNull(dbTable.Rows(0) !hargabeli) Then
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(0, "###,##0") 'harga
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(0, "###,##0") 'subtotal
                    Else
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                    End If
                    isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatB), 0, dbTable.Rows(0) !isisatB)
                    IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatT), 0, dbTable.Rows(0) !isisatT)
                    SatB.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanB), "", dbTable.Rows(0) !satuanB)
                    SatT.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanT), "", dbTable.Rows(0) !satuanT)
                    SatK.Text = IIf(IsDBNull(dbTable.Rows(0) !satuan), "", dbTable.Rows(0) !satuan)
                    SendKeys.Send("{up}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")

                Else
                    Form_DaftarBarang.kode_toko.Text = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)  '"20"
                    Form_DaftarBarang.tCari.Text = tID
                    Form_DaftarBarang.JenisTR.Text = "PEMBELIAN"
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
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0) !idrec
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0) !nama
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = 1
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0) !SatuanB
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = dbTable.Rows(0) !IsiSatB
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = dbTable.Rows(0) !Satuan
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(0, "###,##0") 'PsDisc
                            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value = Format(0, "###,##0") 'isc
                            If IsDBNull(dbTable.Rows(0) !hargabeli) Then
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(0, "###,##0") 'harga
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(0, "###,##0") 'subtotal
                            Else
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value = Format(dbTable.Rows(0) !hargabeli, "###,##0")
                            End If
                            isiSatB.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatB), 0, dbTable.Rows(0) !isisatB)
                            IsiSatT.Text = IIf(IsDBNull(dbTable.Rows(0) !isisatT), 0, dbTable.Rows(0) !isisatT)
                            SatB.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanB), "", dbTable.Rows(0) !satuanB)
                            SatT.Text = IIf(IsDBNull(dbTable.Rows(0) !satuanT), "", dbTable.Rows(0) !satuanT)
                            SatK.Text = IIf(IsDBNull(dbTable.Rows(0) !satuan), "", dbTable.Rows(0) !satuan)
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
                SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")
            ElseIf cCol = 3 Then
                Dim tSatuan As String = ""
                Dim QTY As Double = 0, Pengali As Integer = 0
                tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
                If DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(DGRequest.CurrentCell.ColumnIndex).Value = String.Empty Then
                    tSatuan = CariSatuan(tID)
                    SendKeys.Send("{up}")
                    'Exit Sub
                Else
                    tSatuan = UCase(DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(DGRequest.CurrentCell.ColumnIndex).Value)
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
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = tSatuan
                If Not Information.IsNumeric(DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value) Then
                    ' MsgBox((DGRequest.Rows(e.RowIndex).Cells(6).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = 0
                    SendKeys.Send("{up}")
                    QTY = 0
                    Exit Sub
                Else
                    QTY = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value * 1
                End If
                If tSatuan = SatK.Text Then
                    Pengali = 1
                ElseIf tSatuan = SatT.Text Then
                    Pengali = IsiSatT.Text * 1
                ElseIf tSatuan = SatB.Text Then
                    Pengali = isiSatB.Text * 1
                End If
                'DGRequest.Rows(e.RowIndex).Cells(2).Value = Format(QTY, "###,##0")
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = Format(QTY * Pengali, "###,##0")
                SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                HitungTotal()
            ElseIf cCol >= 4 And cCol < 8 Then
                SendKeys.Send("{up}")
                SendKeys.Send("{right}")
            Else
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

    Private Sub CekTable()
        Dim SQL As String
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        SQL = "SELECT *  FROM information_schema.COLUMNS " &
             "WHERE TABLE_NAME = 't_POH'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "CREATE TABLE [t_POH] (
                  [IDRec] [varchar](10),
                  [NoFaktur] [varchar](100),
                  [TglPO] [DateTime],
                  [ID_Supplier] [varchar](10) ,
                  [Supplier] [varchar](100),
                  [Keterangan] [varchar](254) ,
                  [SubTotal] [money],
                  [PsPPN] [real] ,
                  [PPN] [money],
                  [PsDisc] [real],
                  [Disc] [money],
                  [Total] [money], 
                  [AktifYN] [Char](1),
                  [UserID] [varchar](10) ,
                  [LastUPD] [DateTime] ) "
            Proses.ExecuteNonQuery(SQL)
            SQL = "CREATE TABLE [t_pod] (
                  [ID_Rec] [varchar](10),
                  [NoUrut] [varchar](5) ,
                  [KodeBrg] [varchar](20),
                  [NamaBrg] [varchar](100) ,
                  [QTY] [float](11),
                  [Satuan] [varchar](20) ,
                  [Harga] [money],
                  [SubTotal] [money] , 
                  [AktifYN] [Char](1),
                  [UserID] [varchar](10) ,
                  [LastUPD] [DateTime] ) "
            Proses.ExecuteNonQuery(SQL)
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
            HargaModal.Text = 0
            isiSatB.Text = 0
            IsiSatT.Text = 0
            harga1.Text = 0
            harga2.Text = 0
            harga3.Text = 0
        End If
    End Sub

    Private Sub KodeBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeBrg.KeyPress
        Dim KodeToko As String = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)  '"20" ' Mid(Kode_Toko.Text, 4, 2)
        If e.KeyChar = Chr(13) Then
            If Trim(KodeBrg.Text) = "" Then Exit Sub
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
                Form_DaftarBarang.kode_toko.Text = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2) '"20" 'Kode_Toko.Text
                '  Form_DaftarBarang.txt_Nama_Barang.Text = KodeBrg.Text
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

    Private Sub KodeBrg_LostFocus(sender As Object, e As EventArgs) Handles KodeBrg.LostFocus
        If KodeBrg.Text = Nothing Then
            KodeBrg.Text = "<Kode_Brg>"
            KodeBrg.ForeColor = Color.Gray
            KodeBrg.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub KodeBrg_GotFocus(sender As Object, e As EventArgs) Handles KodeBrg.GotFocus
        If KodeBrg.Text = "<Kode_Brg>" Then
            KodeBrg.Text = ""
            KodeBrg.ForeColor = Color.Black
            KodeBrg.BackColor = Color.White
        End If
    End Sub
    'kodebrg
    Private Sub isiKodeBrg(IdBrg)
        Dim dbT As DataTable
        Dim KodeToko As String = Mid(FrmMenuUtama.Kode_Toko.Text, 4, 2)  '"20" ' Mid(Kode_Toko.Text, 4, 2)
        cmbSatuanB.Items.Clear()
        SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
                "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
                "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori, HPP " &
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
            If (dbT.Rows(0)!IsiSatB + dbT.Rows(0)!IsiSatt) = 0 Then
                MsgBox("Isi satuan konversi ada yang salah, cek di master barang dulu.", vbCritical + vbOKOnly, ".:Warning !")
            End If
            HargaSatuan.Text = Format(dbT.Rows(0) !HPP, "###,##0")
            harga1.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
            harga2.Text = Format(dbT.Rows(0)!PriceList2, "###,##0")
            harga3.Text = Format(dbT.Rows(0)!PriceList3, "###,##0")

            HargaSatuan_Asli.Text = HargaSatuan.Text
            HargaModal.Text = Format(dbT.Rows(0)!HPP, "###,##0.00")
            'HargaSatuan.Text = dbT.Rows(0) !HPP
            SubTotal.Text = HargaSatuan.Text
        End If
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
                HargaSatuan_Asli.Text = harga1.Text
                Select Case value
                    Case 0
                        'HargaSatuan_Asli.Text = harga1.Text
                        QTY.Text = QtyB.Text
                    Case 1
                        'HargaSatuan_Asli.Text = harga2.Text
                        QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0.00")
                    Case 2
                        'HargaSatuan_Asli.Text = harga3.Text
                        QTY.Text = Format((QtyB.Text * 1) * (isiSatB.Text * 1), "###,##0.00")
                    Case Else
                        'HargaSatuan_Asli.Text = harga1.Text
                        QTY.Text = QtyB.Text
                End Select
                HargaSatuan.Text = HargaModal.Text
                hitungSubTotal()
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
            hitungSubTotal()
            SendKeys.SendWait("{Tab}")
            'cmbSatuanB.Focus()
            cmbSatuanB.Select()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub QtyB_TextChanged(sender As Object, e As EventArgs) Handles QtyB.TextChanged
        'If Trim(QtyB.Text) = "" Then QtyB.Text = 0
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
                HargaSatuan_Asli.Text = harga1.Text
                Select Case value
                    Case 0
                        'HargaSatuan_Asli.Text = harga1.Text
                        QtyB.Text = QTY.Text
                    Case 1
                        'HargaSatuan_Asli.Text = harga2.Text
                        QtyB.Text = Replace(Format((QTY.Text * 1) / (IsiSatT.Text * 1), "###,##0.00"), ".00", "")
                    Case 2
                        'HargaSatuan_Asli.Text = harga3.Text
                        QtyB.Text = Replace(Format((QTY.Text * 1) / (isiSatB.Text * 1), "###,##0.00"), ".00", "")
                    Case Else
                        'HargaSatuan_Asli.Text = harga1.Text
                        QtyB.Text = QTY.Text
                End Select
                HargaSatuan.Text = HargaModal.Text
                hitungSubTotal()
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
            HargaSatuan_Asli.Text = harga1.Text
            Dim value As Integer = cmbSatuanB.SelectedIndex
            Select Case value
                Case 0
                    'HargaSatuan_Asli.Text = harga1.Text
                    QtyB.Text = QTY.Text
                Case 1
                    ' HargaSatuan_Asli.Text = harga2.Text
                    QtyB.Text = Replace(Format((QTY.Text * 1) / (IsiSatT.Text * 1), "###,##0.00"), ".00", "")
                Case 2
                    'HargaSatuan_Asli.Text = harga3.Text
                    QtyB.Text = Replace(Format((QTY.Text * 1) / (isiSatB.Text * 1), "###,##0.00"), ".00", "")
                Case Else
                    ' HargaSatuan_Asli.Text = harga1.Text
                    QTY.Text = QtyB.Text
            End Select
            HargaSatuan.Text = HargaModal.Text
            If satuan.Text = cmbSatuanB.Text Then
                QtyB.Text = QTY.Text
            End If
            hitungSubTotal()
            HargaSatuan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
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
            HargaSatuan_Asli.Text = harga1.Text
            Dim value As Integer = cmbSatuanB.SelectedIndex
            Select Case value
                Case 0
                    'HargaSatuan_Asli.Text = harga1.Text
                    QTY.Text = QtyB.Text
                Case 1
                    ' HargaSatuan_Asli.Text = harga2.Text
                    QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0")
                Case 2
                    ' HargaSatuan_Asli.Text = harga3.Text
                    QTY.Text = Format((QtyB.Text * 1) * (isiSatB.Text * 1), "###,##0")
                Case Else
                    '  HargaSatuan_Asli.Text = harga1.Text
                    QTY.Text = QtyB.Text
            End Select

            HargaSatuan.Text = HargaModal.Text
            If satuan.Text = cmbSatuanB.Text Then
                QTY.Text = QtyB.Text
            End If
            hitungSubTotal()
        End If

    End Sub

    Private Sub cmbSatuanB_GotFocus(sender As Object, e As EventArgs) Handles cmbSatuanB.GotFocus
        cmbSatuanB.DroppedDown = True
    End Sub

    Private Sub HargaSatuan_TextChanged(sender As Object, e As EventArgs) Handles HargaSatuan.TextChanged
        'If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If IsNumeric(HargaSatuan.Text) Then
            'Dim temp As Double = HargaSatuan.Text
            'HargaSatuan.Text = Format(temp, "###,##0")
            HargaSatuan.SelectionStart = HargaSatuan.TextLength
        End If
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
    Private Sub hitungSubTotal()
        Dim harga1 As Double = 0
        Dim disc1 As Double = 0, xQTY As Double = 0
        If Trim(QTY.Text) = "" Then QTY.Text = 1
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If Trim(HargaSatuan_Asli.Text) = "" Then HargaSatuan_Asli.Text = 0
        If Trim(PsDisc1.Text) = "" Then PsDisc1.Text = 0
        If Trim(Disc.Text) = "" Then Disc.Text = 0

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

        If Trim(PsDisc1.Text) <> 0 Then
            disc1 = (PsDisc1.Text * 1 / 100) * (HargaSatuan.Text * 1)
            Disc.Text = Format(disc1, "###,##0")
        Else
            disc1 = Disc.Text * 1
        End If
        harga1 = xQTY * ((HargaSatuan.Text * 1) - disc1)
        If xQTY * (HargaSatuan_Asli.Text * 1) <> harga1 Then
            Flag.Text = "*"
        Else
            Flag.Text = ""
        End If
        SubTotal.Text = Format(harga1, "###,##0.00")
    End Sub

    Private Sub AddItem()
        Dim ada As Boolean = False
        If Trim(QtyB.Text) = "" Then QtyB.Text = 0
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        For i As Integer = 0 To DGRequest.Rows.Count - 1
            If Trim(KodeBrg.Text) = Trim(DGRequest.Rows(i).Cells(0).Value) Then
                ada = True
                Exit For
            End If
        Next
        'If ada Then
        '    MsgBox("Kode Barang " & KodeBrg.Text & " " & NamaBrg.Text & " SUDAH pernah di input !", vbCritical + vbOKOnly, ".:Warning !")
        'Else
        DGRequest.Rows.Add(KodeBrg.Text,
                        NamaBrg.Text,
                        QtyB.Text,
                        cmbSatuanB.Text,
                        QTY.Text,
                        satuan.Text,
                        HargaSatuan.Text,
                        PsDisc1.Text,
                        Disc.Text,
                        SubTotal.Text,
                        "Hapus",
                        HargaSatuan_Asli.Text)
        clearBarcode()
        HitungTotal()
        KodeBrg.Text = "<Kode_Brg>"
        KodeBrg.ForeColor = Color.Gray
        KodeBrg.BackColor = Color.LightGoldenrodYellow
        KodeBrg.Focus()
        'End If
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
        PsDisc1.Text = 0
        PsDisc1.ForeColor = Color.Gray
        Disc.Text = 0
        Disc.ForeColor = Color.Gray
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
    Private Sub PsDisc1_TextChanged(sender As Object, e As EventArgs) Handles PsDisc1.TextChanged
        If Trim(PsDisc1.Text) = "" Then PsDisc1.Text = 0
        If Trim(sub_total.Text) = "" Then sub_total.Text = 0
        If IsNumeric(PsDisc1.Text) Then
            Dim temp As Double = PsDisc1.Text
            PsDisc1.Text = Format(temp, "###,##0")
            Disc.Text = Format(temp * 1 / 100 * (sub_total.Text * 1), "###,##0")
            PsDisc1.SelectionStart = PsDisc1.TextLength
        End If
    End Sub
    Private Sub PsDisc1_GotFocus(sender As Object, e As EventArgs) Handles PsDisc1.GotFocus
        With PsDisc1
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .ForeColor = Color.Black
            .BackColor = Color.White
            Disc.ForeColor = Color.Black
            Disc.BackColor = Color.White
        End With
    End Sub
    Private Sub PsDisc1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PsDisc1.KeyPress
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
            If PsDisc1.Text <> 0 Then
                hitungSubTotal()
                AddItem()
                KodeBrg.Focus()
            Else
                Disc.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub PsDisc1_LostFocus(sender As Object, e As EventArgs) Handles PsDisc1.LostFocus
        If PsDisc1.Text = Nothing Then
            PsDisc1.Text = "0"
            PsDisc1.ForeColor = Color.Gray
            PsDisc1.BackColor = Color.LightGoldenrodYellow
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If (isiSatB.Text * 1) + (IsiSatT.Text * 1) = 0 Then
            MsgBox("Konversi kode barang ini salah, cek di master barang !", vbCritical + vbOKOnly, ".:Warning: Kode barang ini tidak bisa di tambahkan !")
            Exit Sub
        End If
        hitungSubTotal()
        AddItem()
        KodeBrg.Focus()
    End Sub

    Private Sub Disc_LostFocus(sender As Object, e As EventArgs) Handles Disc.LostFocus
        If Disc.Text = Nothing Then
            Disc.Text = "0"
            Disc.ForeColor = Color.Gray
            Disc.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub DGRequest_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellContentClick
        If DGRequest.Rows.Count <> 0 Then
            isiItemBarang()
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

    Private Sub Form_Pembelian_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If hapusSQL <> "" Then
            Proses.ExecuteNonQuery(hapusSQL)
            hapusSQL = ""
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Data_Record()
    End Sub

    Private Sub tglCari1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglCari1.KeyPress
        If e.KeyChar = Chr(13) Then
            tglCari2.Focus()
            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub tglCari2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tglCari2.KeyPress
        If e.KeyChar = Chr(13) Then
            tCari.Focus()
            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            btnCari.Focus()
            btnCari_Click(sender, e)
        End If
    End Sub

    Private Sub cmdSetting_Click(sender As Object, e As EventArgs) Handles cmdSetting.Click
        'Form_Penjualan_CompId.ShowDialog()
        cmdExit_Click(sender, e)
    End Sub

    Private Sub SubTotal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SubTotal.KeyPress
        If e.KeyChar = Chr(13) Then
            hitungSubTotal()
            btnAdd.Focus()
        End If
    End Sub

    Private Sub tSupplier_TextChanged(sender As Object, e As EventArgs) Handles tSupplier.TextChanged

    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click
        If IDRec.Text <> "" Then
            cmdCetak.Enabled = False
            CetakPembelian()
            cmdCetak.Enabled = True
        Else
            MsgBox("No Faktur Pembelian yang mau di cetak belum di pilihh !", vbCritical + vbOKOnly, ".: Warning !")
            Exit Sub
        End If
    End Sub
    Private Sub CetakPembelian()
        Dim DTadapter As New SqlDataAdapter
        Dim objRep As New ReportDocument
        Dim CN As New SqlConnection
        Dim dttable As New DataTable
        Dim footer1 As String = "", footer2 As String = "", footer3 As String = ""
        Dim nPrinter As String = "", nKertas As String = "", nPrintYN As String = ""

        Me.Cursor = Cursors.WaitCursor

        Form_Cetak.PanelTipe.Visible = False
        Form_Cetak.ShowDialog()
        nPrintYN = FrmMenuUtama.TSKeterangan.Text

        Form_Report.Text = "Pembelian"
        ' terbilang = "#" + tb.Terbilang(CDbl(Form_InvoiceCustomer.Total.Text)) + " Rupiah #"
        nPrinter = My.Settings.NamaPrinter
        nKertas = My.Settings.NamaKertas
        nPrintYN = FrmMenuUtama.TSKeterangan.Text
        Proses.OpenConn(CN)

        dttable = New DataTable


        SQL = "SELECT a.idrec, a.TglPO, a.Total, e.Nama as namasupp, e.Alamat1, e.Alamat2, " &
              "       b.NamaBrg, b.QTY, b.satuan, b.harga, b.SubTotal, " &
              "     CASE WHEN b.satuanB = m_Barang.SatuanB And m_barang.satuant = m_barang.satuanB And isisatB > 0 THEN  " &
              "          CAST(b.qtyb / (isiSatB * b.qtyb) As varchar(24))  " &
              "      WHEN b.satuanB = m_Barang.SatuanB And m_barang.satuant <> m_barang.satuanB And isisatB > 0 THEN    " &
              "          CAST(b.qtyb / (m_barang.isisatT * b.qtyb) As varchar(24))    " &
              "      WHEN b.satuanB = m_Barang.SatuanT And m_barang.satuant <> m_barang.satuanB And isisatB > 0 THEN     " &
              "          CAST(b.qtyb / ((b.qtyb * m_barang.isisatT) / isiSatB) As varchar(24))     " &
              "      WHEN b.satuanB = m_Barang.Satuan And m_barang.satuant <> m_barang.satuan And isisatT > 0 then   " &
              "          CAST(b.qtyb / (b.qtyb / m_barang.isisatB)  As varchar(24))     " &
              "      WHEN b.satuanB = m_Barang.Satuan And m_barang.satuant = m_barang.satuan then   " &
              "          CAST(b.qtyb / (b.qtyb)  As varchar(24))     " &
              "      Else ''    " &
              " End As IsiSatuan, " &
              " Case WHEN b.satuanB = m_Barang.SatuanB And m_barang.satuant = m_barang.satuanB And isisatB > 0 THEN  " &
              "         CAST(isiSatB * b.qtyb As varchar(24)) + ' ' + m_barang.Satuan  " &
              "      WHEN b.satuanB = m_Barang.SatuanB And m_barang.satuant <> m_barang.satuanB And isisatB > 0 THEN   " &
              "         CAST(m_barang.isisatT * b.qtyb As varchar(24)) + ' ' + m_barang.SatuanT  " &
              "      WHEN b.satuanB = m_Barang.SatuanT And m_barang.satuant <> m_barang.satuanB And isisatB > 0 THEN    " &
              "         CAST((b.qtyb * m_barang.isisatT) / isiSatB As varchar(24)) + ' ' + m_barang.SatuanB   " &
              "      WHEN b.satuanB = m_Barang.Satuan And m_barang.satuant <> m_barang.satuan And isisatB > 0 then  " &
              "         CAST(ROUND((b.qtyb / m_barang.isisatB), 2)   As varchar(24)) + ' ' + m_barang.SatuanB   " &
              "      WHEN b.satuanB = m_Barang.Satuan And m_barang.satuant = m_barang.satuan then  " &
              "         CAST((b.qtyb)  As varchar(24)) + ' ' + m_barang.Satuan   " &
              "      Else ''   " &
              " End As KetIsi " &
              "FROM t_poh a left join t_pod b On a.IdRec = b.Id_Rec " &
              "     left join m_supplier e On a.ID_Supplier = e.idrec  " &
              "     inner join m_barang On m_barang.idrec = b.kodebrg " &
              "WHERE a.AktifYN = 'Y' AND b.AktifYN = 'Y' And a.idrec = '" & IDRec.Text & "' " &
              "ORDER BY a.IdRec, b.NoUrut "
        DTadapter = New SqlDataAdapter(SQL, CN)
        Dim tb As New Terbilang, terbilang As String = ""
        terbilang = "# " + tb.Terbilang(CDbl(Total.Text)) + " Rupiah #"
        Try
            DTadapter.Fill(dttable)
            'If FrmMenuUtama.CompCode.Text = "KM" Then
            '    objRep = New Rpt_PembelianFakturKM
            'Else
            '    objRep = New Rpt_PembelianFaktur
            'End If

            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("terbilang", terbilang)
            objRep.SetParameterValue("userid", UserID)
            objRep.SetParameterValue("toko", FrmMenuUtama.CompCode.Text)
            'objRep.PrintOptions.PaperSize = Proses.GetPapersizeID(nPrinter, nKertas)
            If nPrintYN = "PRINTER" Then
                objRep.PrintToPrinter(1, False, 0, 0)
            Else
                Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                Form_Report.CrystalReportViewer1.Refresh()
                Form_Report.CrystalReportViewer1.ReportSource = objRep
                Form_Report.CrystalReportViewer1.ShowRefreshButton = False
                Form_Report.CrystalReportViewer1.ShowPrintButton = False
                Form_Report.CrystalReportViewer1.ShowParameterPanelButton = False
                Form_Report.ShowDialog()
            End If
            dttable.Dispose()
            DTadapter.Dispose()
            Proses.CloseConn(CN)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

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
            hitungSubTotal()
            AddItem()
            KodeBrg.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
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
            HargaSatuan.Text = Format(temp, "###,##0.00")
            hitungSubTotal()
            SubTotal.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub DGRequest_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellClick
        If DGRequest.Rows.Count = 0 Then Exit Sub
        indexbrg.Text = DGRequest.CurrentCell.RowIndex

        Dim tNamaBrg As String = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value + ", " +
            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value + " " +
            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value
        If e.ColumnIndex = 10 Then 'Hapus
            If Trim(tNamaBrg) <> "" Then
                If LEdit = True Then
                    SQL = "select postingyn from t_poh where idrec = '" & IDRec.Text & "'"
                    Dim postyn As String = Proses.ExecuteSingleStrQuery(SQL)
                    If postyn = "Y" Then
                        MsgBox("Invoice sudah dibayar, tidak dapat dihapus")
                        Exit Sub
                    End If
                    SQL = "select tterimaYN from t_poh where idrec = '" & IDRec.Text & "'"
                    postyn = Proses.ExecuteSingleStrQuery(SQL)
                    If postyn = "Y" Then
                        MsgBox("Sudah ada tanda terima pembelian, tidak dapat dihapus")
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

    Private Sub tSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            btnCari.Focus()
            btnCari_Click(sender, e)
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
        PsDisc1.Text = DGRequest.Rows(indexbrg.Text).Cells(7).Value
        Disc.Text = DGRequest.Rows(indexbrg.Text).Cells(8).Value
        SubTotal.Text = DGRequest.Rows(indexbrg.Text).Cells(9).Value
        Flag.Text = DGRequest.Rows(indexbrg.Text).Cells(11).Value
        ''HargaSatuan_Asli.Text = DGRequest.Rows(indexbrg.Text).Cells(12).Value
        ''HargaModal.Text = DGRequest.Rows(indexbrg.Text).Cells(13).Value
    End Sub

    Private Sub DGRequest_Click(sender As Object, e As EventArgs) Handles DGRequest.Click
        If DGRequest.Rows.Count <> 0 Then
            isiItemBarang()
        End If
    End Sub
End Class

