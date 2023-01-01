Public Class Form_KoreksiStock
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim UserID As String = FrmMenuUtama.TsPengguna.Text

    Private Sub HitungTotal()
        Dim sum = (From row As DataGridViewRow In DGRequest.Rows.Cast(Of DataGridViewRow)()
                Select CDec(row.Cells(7).Value)).Sum
        SubTotal.Text = Format(sum, "###,##0")

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
        NamaToko.Text = ""
        DGRequest.Rows.Clear()
        TglKoreksi.Value = Now
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdTambah.Enabled = tAktif
        'cmdEdit.Enabled = tAktif
        cmdHapus.Enabled = tAktif
        cmdCetak.Enabled = tAktif
        cmdSimpan.Visible = Not tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif
        tCari.Visible = tAktif
        Cari.Visible = tAktif
    End Sub

    Private Sub Data_Record()
        Dim a As Integer
        SQL = "Select IDRec, TglKoreksi, KodeBrg, NamaBrg, " &
            "         QTYKoreksi, Keterangan, kode_toko " &
            " From t_KoreksiStock " &
            "Where t_KoreksiStock.AktifYN = 'Y' " &
            "Order By  IDRec desc, TglKoreksi desc "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        'DGView.Columns(6).HeaderText = ""
        'DGView.Columns(6).Width = 100
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a)!IDRec, Format(.Table.Rows(a)!TglKoreksi, "dd-MMM-yy"),
                    .Table.Rows(a)!KodeBrg, .Table.Rows(a)!NamaBrg,
                    Format(.Table.Rows(a)!QTYKoreksi, "###,##0"),
                    Trim(.Table.Rows(a)!Keterangan))
            Next (a)
        End With
    End Sub

    Private Sub Isi_Data()
        Dim mGudang As String = ""
        DGView.Enabled = False
        SQL = "Select IDRec, date_format(TglKoreksi, '%m-%d-%Y') as TglKoreksi," &
            " KodeBrg, NamaBrg, QTYKoreksi, Keterangan, Kode_Toko, m_Toko.Nama Nama_Toko  " &
            " From t_KoreksiStock inner join m_Toko on Kode_Toko = m_Toko.idrec " &
            "Where t_KoreksiStock.AktifYN = 'Y' " &
            " and t_KoreksiStock.IDRec = '" & IDRec.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            kodeToko.Text = dbTable.Rows(0)!Kode_Toko
            NamaToko.Text = dbTable.Rows(0)!Nama_Toko
        End If

        DGRequest.Rows.Clear()

        SQL = "Select t_KoreksiStock.*, m_Barang.Nama as NamaBarang " & _
            " From t_KoreksiStock inner Join m_Barang on KodeBrg = m_Barang.IDRec " & _
            "Where t_KoreksiStock.IDRec = '" & IDRec.Text & "' and t_KoreksiStock.AktifYN = 'Y' " & _
            "Order By NoUrut "
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGRequest.Rows.Add(.Table.Rows(a)!KodeBrg, _
                    .Table.Rows(a)!NamaBarang, _
                    Format(.Table.Rows(a)!qtyKomputer, "###,##0.00"), _
                    .Table.Rows(a)!Satuan, _
                    Format(.Table.Rows(a)!qtyFisik, "###,##0.00"), _
                    Format(.Table.Rows(a)!qtyKoreksi, "###,##0.00"), _
                    Format(.Table.Rows(a)!harga, "###,##0"), _
                    Format(.Table.Rows(a)!subtotal, "###,##0"), _
                    .Table.Rows(a)!Keterangan)
            Next (a)
        End With
        HitungTotal()
        DGView.Enabled = True
    End Sub
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        TabControl1.SelectedTab = TabPage2
        TabControl1.TabPages.Insert(1, TabPage2)
        ClearTextBoxes()
        TglKoreksi.Focus()
        kodeToko.Text = FrmMenuUtama.Kode_Toko.Text
        NamaToko.Text = Proses.ExecuteSingleStrQuery("Select nama from m_toko where idrec = '" & kodeToko.Text & "' ")
    End Sub



    Private Sub CekTable()
        SQL = "SELECT *  FROM information_schema.TABLES " &
            "WHERE table_name = 't_KoreksiStock'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "CREATE TABLE t_KoreksiStock ( " &
                " IDRec varchar(10) DEFAULT '', " &
                " TglKoreksi date DEFAULT (getDate()), " &
                " NoUrut varchar(5) DEFAULT '000', " &
                " KodeBrg varchar(15) DEFAULT '', " &
                " NamaBrg varchar(100) DEFAULT '', " &
                " QTYKomputer real DEFAULT 0, " &
                " QTYFisik real DEFAULT 0, " &
                " QTYKoreksi real DEFAULT 0, " &
                " Satuan varchar(20) DEFAULT 'PCS', " &
                " Harga money DEFAULT 0, " &
                " SubTotal money DEFAULT 0, " &
                " Kode_Toko varchar(5) DEFAULT '000', " &
                " Keterangan varchar(254) DEFAULT '', " &
                " TransferYN char(1) DEFAULT 'N', " &
                " AktifYN char(1) DEFAULT 'Y', " &
                " UserID varchar(10) DEFAULT '', " &
                " LastUPD datetime DEFAULT (GetDate()) ) "
            Proses.ExecuteNonQuery(SQL)
        End If
    End Sub


    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If DGView.Rows.Count <> 0 Then
            IDRec.Text = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            Exit Sub
        End If
        Isi_Data()

        LAdd = False
        LEdit = True
        AturTombol(False)
        If TabControl1.TabCount = 1 Then
            TabControl1.TabPages.Insert(1, TabPage2)
        End If
        TabControl1.SelectedTab = TabPage2
        DGRequest.Focus()
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
        If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            SQL = "Select * From t_KoreksiStock " & _
                "where IDRec = '" & IDRec.Text & "' " & _ 
                "  And AktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            With dbTable.Columns(0)
                For a = 0 To dbTable.Rows.Count - 1
                    QTYB = .Table.Rows(a)!qtykoreksi
                    KodeBrg = .Table.Rows(a)!KodeBrg
                    SQL = "Update M_Barang Set " & _
                        " QTY = QTY - " & QTYB * 1 & " " & _
                        "Where IDRec = '" & Trim(KodeBrg) & "'  "
                    Proses.ExecuteNonQuery(SQL)
                Next (a)
            End With
            SQL = "Update t_KoreksiStock Set AktifYN = 'N' " & _
                "where IDRec = '" & IDRec.Text & "' " & _ 
                "  And AktifYN = 'Y' "
            Proses.ExecuteNonQuery(SQL)
        End If
        Data_Record()
    End Sub

    Private Sub cmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimpan.Click
        Dim tNo As String = "", tKode As String = ""
        Dim KodeBrg As String = "", tKeterangan As String = ""
        Dim QTY As Double = 0, Unit As String = ""
        Dim QTYB As Double = 0, UnitB As String = ""
        Dim Harga As Double = 0, tSubTotal As Double = 0
        Dim idtr As String = "", saldo As Integer = 0
        Dim QTYFisik As Double = 0, QTYKoreksi As Double = 0, mAlasan As String = ""

        'Call Proses.TambahFieldArea("t_KoreksiStock")

        If kodeToko.Text = "" Then
            MsgBox("Counter/Toko tidak boleh kosong!", vbCritical + vbOKOnly, ".:Error!")
            kodeToko.Focus()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        cmdSimpan.Enabled = False
        If LAdd Then
            'YYB TK TR 99999
            'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
            IDRec.Text = Proses.GetMaxId("t_KoreksiStock", "IdRec",
                                         Mid(kodeToko.Text, 4, 2) & "KR")
            tNo = ""
            For i As Integer = 0 To DGRequest.Rows.Count - 1
                If i >= DGRequest.Rows.Count - 1 Then Exit For
                tNo = Microsoft.VisualBasic.Right(101 + i, 2)

                KodeBrg = DGRequest.Rows(i).Cells(0).Value
                tKeterangan = DGRequest.Rows(i).Cells(1).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
                    QTYB = 0
                Else
                    QTYB = DGRequest.Rows(i).Cells(2).Value
                End If
                Unit = DGRequest.Rows(i).Cells(3).Value

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
                    QTYFisik = 0
                Else
                    QTYFisik = DGRequest.Rows(i).Cells(4).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(5).Value) Then
                    QTYKoreksi = 0
                Else
                    QTYKoreksi = DGRequest.Rows(i).Cells(5).Value
                End If

                If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
                    Harga = 0
                Else
                    Harga = DGRequest.Rows(i).Cells(6).Value
                End If

                tSubTotal = QTYB * Harga

                mAlasan = Replace(DGRequest.Rows(i).Cells(8).Value, "'", "`")
                SQL = "INSERT INTO t_KoreksiStock (IDReC, TglKoreksi, NoUrut, KodeBrg, NamaBrg, " &
                    " QTYKomputer, QTYFisik, QTYKoreksi, Satuan, Harga, SubTotal, Kode_Toko, " &
                    " Keterangan, UserID, AktifYN, LastUPD, TransferYN) VALUES ( '" & IDRec.Text & "', " &
                    " '" & Format(TglKoreksi.Value, "yyyy-MM-dd") & "', '" & tNo & "', '" & KodeBrg & "', " &
                    " '" & Replace(tKeterangan, "'", "`") & "', " & QTYB * 1 & ", " & QTYFisik * 1 & ", " & QTYKoreksi * 1 & ", " &
                    " '" & UnitB & "', " & Harga * 1 & ", " & tSubTotal * 1 & ", '" & kodeToko.Text & "', " &
                    " '" & mAlasan & "', '" & UserID & "',  'Y', GetDate(), 'N' )"
                Proses.ExecuteNonQuery(SQL)

                SQL = "Update M_Barang Set " &
                    " stock" & Mid(kodeToko.Text, 4, 2) & "  = stock" & Mid(kodeToko.Text, 4, 2) & " + " & QTYKoreksi * 1 & " " &
                    "Where IDRec = '" & Trim(KodeBrg) & "' "
                Proses.ExecuteNonQuery(SQL)

                'YYB TK TR 99999
                'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber

                idtr = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", Mid(kodeToko.Text, 4, 2) & "TR")

                SQL = "Select stock" & Mid(kodeToko.Text, 4, 2) & " " &
                    "  from m_barang " &
                    " where idrec = '" & Trim(KodeBrg) & "'  "
                saldo = Proses.ExecuteSingleDblQuery(SQL)
                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & idtr & "', '" & kodeToko.Text & "', 'KOREKSI', '" & IDRec.Text & "', " &
                    "'" & Format(TglKoreksi.Value, "yyyy-MM-dd") & "', '" & DGRequest.Rows(i).Cells(0).Value & "',  " &
                    "" & QTYKoreksi & ", 0, " & saldo & ",  '" & Unit & "',  " & QTYKoreksi & ",  " & Harga & ", " &
                    "" & tSubTotal & ", '" & UserID & "', GetDate(), '" & kodeToko.Text & "') "
                Proses.ExecuteNonQuery(SQL)
            Next
            Me.Cursor = Cursors.Default
            cmdSimpan.Enabled = True
            'If MsgBox("Data berhasil disimpan, mau tambah data lagi?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
            ClearTextBoxes()
            'Else
            LAdd = False
            LEdit = False
            AturTombol(True)
            TabControl1.TabPages.RemoveAt(1)
            TabControl1.SelectedTab = TabPage1
            'End If
        ElseIf LEdit Then
            'SQL = "Select * From t_KoreksiStock " &
            '    "where IDRec = '" & IDRec.Text & "' " &
            '    "  And kode_toko = '" & kodeToko.Text & "' " &
            '    "  And AktifYN = 'Y' "
            'dbTable = Proses.ExecuteQuery(SQL)
            'With dbTable.Columns(0)
            '    For a = 0 To dbTable.Rows.Count - 1
            '        QTYB = .Table.Rows(a)!QtyKoreksi
            '        KodeBrg = .Table.Rows(a)!KodeBrg
            '        SQL = "Update M_Barang Set " & _
            '            " QTY = QTY - " & QTYB * 1 & " " & _
            '            "Where IDRec = '" & Trim(KodeBrg) & "'  "
            '        Proses.ExecuteNonQuery(SQL)
            '    Next (a)
            'End With


            'SQL = "Update t_KoreksiStock set " & _
            '    "  AktifYN = 'E', " & _
            '    "   UserID = '" & UserID & "', " & _
            '    "  LastUPD = Now() " & _
            '    "where IdRec = '" & IDRec.Text & "' " & _
            '    "  And Gudang = '" & mGudang & "' " & _
            '    "  And AktifYN = 'Y' "
            'Proses.ExecuteNonQuery(SQL)

            'tNo = ""

            'For i As Integer = 0 To DGRequest.Rows.Count - 1
            '    If i >= DGRequest.Rows.Count - 1 Then Exit For
            '    tNo = Microsoft.VisualBasic.Right(101 + i, 2)

            '    KodeBrg = DGRequest.Rows(i).Cells(0).Value
            '    tKeterangan = DGRequest.Rows(i).Cells(1).Value

            '    If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
            '        QTYB = 0
            '    Else
            '        QTYB = DGRequest.Rows(i).Cells(2).Value
            '    End If
            '    UnitB = DGRequest.Rows(i).Cells(3).Value

            '    If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
            '        QTYFisik = 0
            '    Else
            '        QTYFisik = DGRequest.Rows(i).Cells(4).Value
            '    End If

            '    If Not Information.IsNumeric(DGRequest.Rows(i).Cells(5).Value) Then
            '        QTYKoreksi = 0
            '    Else
            '        QTYKoreksi = DGRequest.Rows(i).Cells(5).Value
            '    End If

            '    If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
            '        Harga = 0
            '    Else
            '        Harga = DGRequest.Rows(i).Cells(6).Value
            '    End If

            '    tSubTotal = QTYB * Harga

            '    mAlasan = DGRequest.Rows(i).Cells(8).Value

            '    SQL = "INSERT INTO t_KoreksiStock (IDReC, TglKoreksi, NoUrut, KodeBrg, NamaBrg, " & _
            '        " QTYKomputer, QTYFisik, QTYKoreksi, Satuan, Harga, SubTotal, Gudang, " & _
            '        " Keterangan, UserID, AktifYN, LastUPD, Area, TransferYN) VALUES ( '" & IDRec.Text & "', " & _
            '        " '" & Format(TglKoreksi.Value, "yyyy-MM-dd") & "', '" & tNo & "', '" & KodeBrg & "', " & _
            '        " '" & tKeterangan & "', " & QTYB * 1 & ", " & QTYFisik * 1 & ", " & QTYKoreksi * 1 & ", " & _
            '        " '" & UnitB & "', " & Harga * 1 & ", " & tSubTotal * 1 & ", '" & mGudang & "', " & _
            '        " '" & mAlasan & "', '" & UserID & "',  'Y', Now(), '', 'N' )"
            '    Proses.ExecuteNonQuery(SQL)

            '    SQL = "Update M_Barang Set " & _
            '        " QTY = QTY  + " & QTYKoreksi * 1 & " " & _
            '        "Where IDRec = '" & Trim(KodeBrg) & "' "
            '    Proses.ExecuteNonQuery(SQL)

            'Next i

            'LAdd = False
            'LEdit = False
            'AturTombol(True)
            'TabControl1.TabPages.RemoveAt(1)
            'TabControl1.SelectedTab = TabPage1
        End If

        Call Data_Record()
    End Sub

    Private Sub cmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
        TabControl1.TabPages.RemoveAt(1)
        TabControl1.SelectedTab = TabPage1
    End Sub


    Private Sub DGRequest_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGRequest.CellEndEdit
        Dim tID As String = "", tQTY As String = 0, mGudang As String = "", tQTYFisik As Double = 0
        Dim cRow As Integer = DGRequest.CurrentCell.RowIndex
        Dim cCol As Integer = DGRequest.CurrentCell.ColumnIndex
        Dim QTYB As Double = 0, QTYK As Double = 0, tQTYK As Double = 0, tNo As Integer = 0
        Dim IsiUnit As Double = 0, SatuanB As String = "", tHarga As Double = 0, tKoreksi As Double = 0
        'If cCol = 0 Then
        SendKeys.Send("{up}")
        SendKeys.Send("{right}")
        'SendKeys.Send("{right}")
        If Trim(kodeToko.Text) = "" Then
            MsgBox("Counter/Toko belum di pilih!", vbCritical + vbOKOnly, ".:Warning!")
            kodeToko.Focus()
            Exit Sub
        End If

        tID = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
        If cCol = 0 Then
            SQL = "Select IDRec, Nama, Satuan,  " &
                "         stock" & Mid(kodeToko.Text, 4, 2) & " as QTY, PriceList " &
                "    From M_Barang " &
                "   Where AktifYN = 'Y' " &
                "     And idRec = '" & tID & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0)!IDRec
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0)!nama
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = Format(dbTable.Rows(0)!QTY, "###,##0")
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0)!satuan
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = 0
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = 0
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0)!PriceList, "###,##0")
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(dbTable.Rows(0)!PriceList, "###,##0")

                'SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")
            Else
                If Trim(tID) = "*" Then tID = ""
                Me.Cursor = Cursors.WaitCursor
                Form_DaftarBarang.Text = "Daftar Barang " & NamaToko.Text
                Form_DaftarBarang.kode_toko.Text = Mid(kodeToko.Text, 4, 2)
                Form_DaftarBarang.tCari.Text = tID
                Form_DaftarBarang.Cari()
                Form_DaftarBarang.ShowDialog()
                tID = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                Form_DaftarBarang.Dispose()
                Me.Cursor = Cursors.Default
                SQL = "Select IDRec, Nama, Satuan,  " &
                    "         Stock" & Mid(kodeToko.Text, 4, 2) & " as QTY, PriceList " &
                    "    From M_Barang " &
                    "   Where AktifYN = 'Y' " &
                    "     And idRec = '" & tID & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = tID
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0)!nama
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = Format(dbTable.Rows(0)!QTY, "###,##0")
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0)!satuan 'dbTable.Rows(0)!UnitBesar
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = 0
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = 0
                    If IsDBNull(dbTable.Rows(0)!pricelist) Then
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = "0"
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = "0"
                    Else
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0)!pricelist, "###,##0")
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(dbTable.Rows(0)!pricelist, "###,##0")
                    End If

                    SendKeys.Send("{up}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")
                Else
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                    SendKeys.Send("{up}")
                End If
            End If
        ElseIf cCol = 2 Then
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                tQTY = 0
                Exit Sub
            Else
                tQTY = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If Trim(tQTY) = "" Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(tQTY * 1, "###,##0")
                End If
            End If

            'SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            'SendKeys.Send("{right}")
        ElseIf cCol = 4 Then
            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                SendKeys.Send("{up}")
                tQTYFisik = 0
                Exit Sub
            Else
                tQTYFisik = DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value * 1
                If Trim(tQTYFisik) = "" Then
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(0, "###,##0")
                Else
                    DGRequest.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(tQTYFisik * 1, "###,##0")
                End If
            End If

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(2).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(2).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(2).Value = 0
                SendKeys.Send("{up}")
                tQTY = 0
                Exit Sub
            Else
                tQTY = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value * 1
            End If
            tKoreksi = tQTYFisik - tQTY
            DGRequest.Rows(e.RowIndex).Cells(5).Value = Format(tKoreksi, "###,##0")

            If Not Information.IsNumeric(DGRequest.Rows(e.RowIndex).Cells(6).Value) Then
                MsgBox((DGRequest.Rows(e.RowIndex).Cells(4).Value) & " bukan angka numeric.", vbCritical, "Please enter numeric value")
                DGRequest.Rows(e.RowIndex).Cells(4).Value = 0
                SendKeys.Send("{up}")
                tHarga = 0
                Exit Sub
            Else
                tHarga = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value * 1
            End If
            DGRequest.Rows(e.RowIndex).Cells(7).Value = Format(tKoreksi * tHarga, "###,##0")
            'SendKeys.Send("{up}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
            SendKeys.Send("{right}")
            HitungTotal()
        ElseIf cCol = 8 Then
            SendKeys.Send("{home}")
            SendKeys.Send("{down}")

            HitungTotal()
        End If
    End Sub

    Private Sub kodeToko_TextChanged(sender As Object, e As EventArgs) Handles kodeToko.TextChanged
        If Trim(kodeToko.Text) = "" Then
            NamaToko.Text = ""
        End If
    End Sub

    Private Sub DGRequest_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellContentClick

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub DGRequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGRequest.KeyDown
        Dim cRow As Integer = DGRequest.CurrentCell.RowIndex, mGudang As String = ""
        Dim cCol As Integer = DGRequest.CurrentCell.ColumnIndex
        Dim tID As String = "", tUnit As String = "", isiUnit As Double = 0, SatuanB As String = ""
        Dim QTYB As Integer = 0, QTYK As Integer = 0, tQTYK As Integer = 0, tIdRap As String = ""
        Dim tNo As Integer = 0, NoLama As Integer = 0, tJenis As String = ""
        If e.KeyCode = Keys.Enter Then
            If cCol = 0 Then
                If Trim(kodeToko.Text) = "" Then
                    MsgBox("Counter/Toko belum di pilih!", vbCritical + vbOKOnly, ".:Warning!")
                    kodeToko.Focus()
                    Exit Sub
                End If
                SQL = "Select IDRec, Nama, Satuan,  
                              stock" & Mid(kodeToko.Text, 4, 2) & " As QTY, PriceList 
                        From M_Barang 
                    Where idrec = '" & tID & "' and idrec <> '' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0) !idrec
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0) !nama
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = Format(dbTable.Rows(0) !QTY, "###,##0")
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0)!satuan 'dbTable.Rows(0)!UnitBesar
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = 0
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = 0
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0)!PriceList, "###,##0")
                    DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(dbTable.Rows(0)!PriceList, "###,##0")

                    'SendKeys.Send("{up}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")
                    SendKeys.Send("{right}")
                Else
                    If Trim(tID) = "*" Then tID = ""
                    Me.Cursor = Cursors.WaitCursor
                    Form_DaftarBarang.Kode_Toko.Text = kodeToko.Text
                    Form_DaftarBarang.Text = "Daftar Barang " & NamaToko.Text
                    Form_DaftarBarang.kode_toko.Text = Mid(kodeToko.Text, 4, 2)
                    Form_DaftarBarang.tCari.Text = tID
                    Form_DaftarBarang.Cari()
                    Form_DaftarBarang.ShowDialog()
                    tID = FrmMenuUtama.TSKeterangan.Text
                    FrmMenuUtama.TSKeterangan.Text = ""
                    Form_DaftarBarang.Dispose()
                    Me.Cursor = Cursors.Default
                    SQL = "Select IDRec, Nama, Satuan,  
                              stock" & Mid(kodeToko.Text, 4, 2) & " As QTY, PriceList 
                          From M_Barang  
                         Where idRec = '" & tID & "' "
                    dbTable = Proses.ExecuteQuery(SQL)
                    If dbTable.Rows.Count <> 0 Then
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value = dbTable.Rows(0) !idrec
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = dbTable.Rows(0) !nama
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value = Format(dbTable.Rows(0) !QTY, "###,##0")
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value = dbTable.Rows(0)!satuan
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value = 0
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value = 0
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = Format(dbTable.Rows(0)!PriceList, "###,##0")
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = Format(dbTable.Rows(0)!PriceList, "###,##0")

                        'SendKeys.Send("{up}")
                        SendKeys.Send("{right}")
                        SendKeys.Send("{right}")
                        SendKeys.Send("{right}")
                        SendKeys.Send("{right}")
                    Else
                        DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value = ""
                        SendKeys.Send("{up}")
                    End If
                End If
            ElseIf cCol = 2 Then
                'SendKeys.Send("{up}")
                SendKeys.Send("{right}")
                SendKeys.Send("{right}")
            ElseIf cCol = 4 Or cCol = 8 Then
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
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value = 0
                DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value = 0
            End If
            HitungTotal()
        ElseIf e.KeyData = Keys.Tab Then
            MsgBox("Silakan tekan tombol enter!", MsgBoxStyle.Critical + vbInformation, "Jangan pakai tab!")
            SendKeys.Send("{home}")
        End If
    End Sub

    Private Sub cmdCetak_Click(sender As Object, e As EventArgs) Handles cmdCetak.Click

    End Sub

    Private Sub kodeToko_KeyPress(sender As Object, e As KeyPressEventArgs) Handles kodeToko.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select nama From m_Toko " &
               "Where idrec = '" & kodeToko.Text & "' and aktifYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                NamaToko.Text = dbTable.Rows(0) !nama
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_Toko " &
                    "Where AktifYN = 'Y' " &
                    "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                kodeToko.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select nama From m_Toko " &
                   "Where idrec = '" & kodeToko.Text & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    NamaToko.Text = dbTable.Rows(0) !nama
                    DGRequest.Focus()
                Else
                    kodeToko.Text = ""
                    NamaToko.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub Form_KoreksiStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearTextBoxes()
        CekTable()
        With Me.DGView.RowTemplate
            .Height = 30
            .MinimumHeight = 20
        End With
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


        DGRequest.GridColor = Color.Red
        DGRequest.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGRequest.BackgroundColor = Color.LightGray

        DGRequest.DefaultCellStyle.SelectionBackColor = Color.LightSalmon
        DGRequest.DefaultCellStyle.SelectionForeColor = Color.White

        DGRequest.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]

        'DGRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False

        DGRequest.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGRequest.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        'DGView2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DGRequest.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        DGRequest.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Data_Record()

        LAdd = False
        LEdit = False

        'TabControl1.Location = New System.Drawing.Point(87, 6)
        TabPage1.Text() = "Daftar Koreksi Stock"
        AturTombol(True)
        TabControl1.SelectedTab = TabPage1
        TabControl1.TabPages.RemoveAt(1)
        TabControl1.SelectedTab = TabPage1

        Dim tID As String = ""
        If DGView.RowCount <> 0 Then
            tID = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(0).Value
        Else
            tID = ""
        End If
        If FrmMenuUtama.Kode_Toko.Text = "AG020" Then
            kodeToko.ReadOnly = False
        Else
            kodeToko.ReadOnly = True
        End If
    End Sub
End Class