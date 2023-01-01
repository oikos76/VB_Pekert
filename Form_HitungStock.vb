Imports System.Data
Imports System.Data.SqlClient
Public Class Form_HitungStock
    Dim dttable As New DataTable
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable

    Protected Ds As DataSet
    Protected Dt As DataTable
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Public conn As SqlConnection
    Dim SQL As String,
        UserID As String = FrmMenuUtama.TsPengguna.Text,
        Kode_Toko As String = FrmMenuUtama.Kode_Toko.Text
    Private Sub cmd_Hitung_Ulang_Click(sender As Object, e As EventArgs) Handles cmd_Hitung_Ulang.Click
        If Format(cmbPeriode.Value, "yyyyMM") < "201912" Then
            MsgBox("Periode Hitung tidak boleh sebelum Desember 2019.", vbCritical + vbOKOnly, ".: Salah Periode")
            Exit Sub
        End If
        If Format(cmbPeriode.Value, "yyyyMM") < "202101" And idToko.Text = "OL005" Then
            MsgBox("Periode Hitung tidak boleh sebelum Januari 2021.", vbCritical + vbOKOnly, ".: Periode Online mulai Jan 21")
            Exit Sub
        End If
        Dim mPeriode As String = Format(cmbPeriode.Value, "yyyyMM"), dbCek As New DataTable
        cmbPeriode.Enabled = False
        If Format(cmbPeriode.Value, "yyyyMM") <> "201912" Then
            Dim mPeriode_1 As String = Format(DateAdd(DateInterval.Month, -1, cmbPeriode.Value), "yyyyMM")
            SQL = "Select name from sysobjects where name = 't_transaksi_" & mPeriode_1 & "' "
            dbCek = Proses.ExecuteQuery(SQL)
            If dbCek.Rows.Count = 0 Then
                If MsgBox("Periode " & mPeriode_1 & " BELUM di proses." & vbCrLf & vbCrLf & "Proses " & mPeriode_1 & " dahulu !", vbCritical + vbOKOnly, ".: Periode Sebelumnya belum di proses") Then
                    Exit Sub
                End If
            End If
        End If
        cmd_Hitung_Ulang.Enabled = False
        idToko.Enabled = False
        Dim BackupTransaksi As String = "t_Transaksi_" & mPeriode
        StatusHitung.Text = "Backup Data " & BackupTransaksi
        Me.Text = ".: Hitung Stock @" & Format(Now(), "HH:mm:ss")
        SQL = "SELECT *  FROM sys.tables " &
             "WHERE name = '" & BackupTransaksi & "'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If MsgBox("Periode " & Format(cmbPeriode.Value, "MM - yyyy") & " sudah pernah di proses !" & vbCrLf &
                      "Mau proses ulang ?", vbYesNo + vbInformation, "Konfirmasi !") = vbYes Then
                SQL = "Drop Table " & BackupTransaksi & " "
                Proses.ExecuteNonQuery(SQL)
            Else
                cmd_Hitung_Ulang.Enabled = False
                Exit Sub
            End If
        End If

        Dim BackupMBarang As String = "m_Barang_" & idToko.Text & Format(Now, "_yyMMdd")
        StatusHitung.Text = "Backup Data " & BackupMBarang
        SQL = "SELECT *  FROM sys.tables " &
             "WHERE name = '" & BackupMBarang & "'  "
                dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            SQL = "Drop Table " & BackupMBarang & " "
            Proses.ExecuteNonQuery(SQL)
        End If

        SQL = "select * into " & BackupMBarang & " " &
            "From m_Barang "
        Proses.ExecuteNonQuery(SQL)

        If mPeriode = "201912" Then
            SQL = "truncate table t_hitung "
            Proses.ExecuteNonQuery(SQL)
        ElseIf mPeriode = "202101" And idToko.Text = "OL005" Then
            SQL = "truncate table t_hitung "
            Proses.ExecuteNonQuery(SQL)
        End If

        SQL = "Select GetDate() tgl "
        dbTable = Proses.ExecuteQuery(SQL)
        Dim tgl As DateTime = dbTable.Rows(0)!tgl
        Keterangan.Text = "Start Proses :  " + Format(tgl, "dd-MM-yy  HH:mm:ss")
        SQL = "insert into t_hitung (kode_toko, userid, tglProses, EndProcess, PeriodeProses) values ( " &
            "'" & idToko.Text & "', '" & UserID & "', '" & tgl & "', '', '" & Format(cmbPeriode.Value, "yyyy-MM-dd") & "' ) "
        Proses.ExecuteNonQuery(SQL)
        TempTable_Periode(idToko.Text, mPeriode)
        isi_t_Transaksi(idToko.Text)
        SQL = "update t_hitung set EndProcess = GetDate() " &
            "Where kode_toko = '" & idToko.Text & "' " &
            "  and tglproses = '" & tgl & "' "
        Proses.ExecuteNonQuery(SQL)

        If idToko.Text = "KM003" Then
            idToko.Text = "KM004"
            SQL = "insert into t_hitung (kode_toko, userid, tglProses, EndProcess, PeriodeProses) values ( " &
                "'" & idToko.Text & "', '" & UserID & "', '" & tgl & "', '', '" & Format(cmbPeriode.Value, "yyyy-MM-dd") & "') "
            Proses.ExecuteNonQuery(SQL)
            TempTable_Periode("KM004", mPeriode)
            isi_t_Transaksi("KM004")
            SQL = "update t_hitung set EndProcess = GetDate() " &
            "Where kode_toko = '" & idToko.Text & "' " &
            "  and tglproses = '" & tgl & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        SQL = "select * into " & BackupTransaksi & " " &
            "From t_Transaksi "
        Proses.ExecuteNonQuery(SQL)
        StatusHitung.Text = "Finish...@ " & Format(Now(), "HH:mm:ss")
        cmd_Hitung_Ulang.Enabled = True
        idToko.Enabled = False
    End Sub

    Private Sub GetLastProses()
        SQL = "Select tglproses, periodeproses, convert(varchar(6), periodeproses, 112) periode_proses, 
                      convert(varchar(6), getdate(),112) hariini
            from t_Hitung where kode_toko = '" & idToko.Text & "' order by periodeproses desc "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            Keterangan.Text = idToko.Text & " pertama kali di proses ulang dgn cara baru."
            cmbPeriode.Value = "2019-12-01"
        Else
            Keterangan.Text = "Terakhir di proses tgl " & Format(dbTable.Rows(0)!tglproses, "dd-MM-yyyy HH:MM")
            ' If dbTable.Rows(0)!periode_proses = dbTable.Rows(0)!hariini Then
            cmbPeriode.Value = dbTable.Rows(0)!periodeproses
            'Else
            '    cmbPeriode.Value = DateAdd(DateInterval.Month, 1, dbTable.Rows(0)!periodeproses)
            'End If

        End If
    End Sub

    Private Sub isi_t_Transaksi(KodeToko)
        Dim jenistr As String = ""
        Dim tin As Double = 0, tout As Double = 0, tQTY As Double = 0
        Dim idtr As String = "", saldo As Double = 0, mKet As String = ""
        Dim mKondisi As String = "", dbToko As New DataTable, mBarang As String = ""
        Dim kodestock As String = Mid(KodeToko, 4, 2)
        NamaToko.Text = Format(Now(), "HH:mm:ss")
        Dim mPeriode As String = Format(cmbPeriode.Value, "yyyyMM")
        If mPeriode = "201912" Then
            SQL = "Update dbo.m_Barang Set " &
            "stock" & kodestock & " = 0 "
            Proses.ExecuteNonQuery(SQL)

            SQL = "delete t_transaksi "
            mKet = " stock" & kodestock & "  +  "
        ElseIf mPeriode = "202101" And KodeToko = "OL005" Then
            SQL = "Update dbo.m_Barang Set " &
            "stock" & kodestock & " = 0 "
            Proses.ExecuteNonQuery(SQL)

            SQL = "delete t_transaksi  "
            mKet = " 0  +  "
        Else
            SQL = "delete t_transaksi where  convert(char(6), tgltr, 112) = '" & mPeriode & "' "
            mKet = " 0 + "
        End If
        Proses.ExecuteNonQuery(SQL)

        SQL = "Select * From t_transaksiD " &
            "Where kodebrg <> '' and  kodetoko = '" & KodeToko & "' " &
            "order by kodebrg, tgltr, idrec"
        'SQL = "Select * From t_transaksiD " &
        '    "Where kodebrg = '02286' and  kodetoko = '" & KodeToko & "' " &
        '    "order by kodebrg, tgltr, idrec"
        'cek 02286 error di sini 
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                saldo = 0
                If mPeriode <> "201912" Then
                    SQL = "Select saldo from t_transaksi " &
                      " Where kodebrg = '" & .Table.Rows(a)!kodebrg & "'  and  kd_toko = '" & KodeToko & "' " &
                      " Order by idtr desc, tgltr desc "
                    saldo = Proses.ExecuteSingleDblQuery(SQL)
                End If
                StatusHitung.Text = idToko.Text + " " + Format(a, "###,##0") + " / " + Format(dbTable.Rows.Count - 1, "###,##0") + " # " +
                    .Table.Rows(a)!jenistr + " | " +
                    .Table.Rows(a)!IdRec + " | " +
                    .Table.Rows(a)!kodebrg + " | " + Format(saldo, "###,##0")
                jenistr = .Table.Rows(a)!jenistr
                If Trim(.Table.Rows(a)!kodebrg) = "02286" Then
                    Debug.Print("")
                End If

                If jenistr = "Pembelian" Or
                    Mid(jenistr, 1, 10) = "Retur Jual" Or
                    jenistr = "Saldo Awal" Then
                    tin = .Table.Rows(a)!qty
                    tout = 0
                    tQTY = .Table.Rows(a)!qty
                    mKondisi = "stock" & kodestock & " =  " & saldo & " + " & tQTY & " "
                ElseIf jenistr = "Stock Opname" Then
                    tin = 0
                    tout = 0
                    tQTY = .Table.Rows(a)!akhir
                    mKondisi = " stock" & kodestock & " = " & tQTY & "  + " & saldo & " "
                ElseIf jenistr = "Penyesuaian" Then
                    tQTY = .Table.Rows(a)!qty
                    If tQTY < 0 Then
                        tout = tQTY * -1
                        tin = 0
                    Else
                        tout = 0
                        tin = tQTY
                    End If
                    mKondisi = " stock" & kodestock & " =   " & tQTY & "  + " & saldo & " "
                ElseIf Mid(jenistr, 1, 6) = "TRF ke" Then
                    tin = 0
                    tout = .Table.Rows(a)!qty
                    tQTY = .Table.Rows(a)!qty
                    mKondisi = "stock" & kodestock & " = -" & tQTY & "  + " & saldo & " "
                ElseIf Mid(jenistr, 1, 8) = "TRF dari" Then
                    tin = .Table.Rows(a)!qty
                    tout = 0
                    tQTY = .Table.Rows(a)!qty
                    mKondisi = "stock" & kodestock & " =  " & tQTY & "  + " & saldo & " "
                Else
                    tin = 0
                    tout = .Table.Rows(a)!qty
                    tQTY = - .Table.Rows(a)!qty
                    mKondisi = "stock" & kodestock & " = " & tQTY & "  + " & saldo & " "
                End If

                SQL = "Update dbo.m_Barang Set " &
                    " " & mKondisi & " " &
                    "Where IDRec = '" & Trim(.Table.Rows(a)!kodebrg) & "' "
                Proses.ExecuteNonQuery(SQL)
                idtr = GetIdTR("t_transaksi", "idtr", kodestock & "TR", Format(.Table.Rows(a)!tgltr, "yyMM"))
                saldo = 0
                SQL = "Select stock" & kodestock & " " &
                    "  From dbo.m_barang " &
                    " Where idrec = '" & Trim(.Table.Rows(a)!kodebrg) & "'  "
                saldo = Proses.ExecuteSingleDblQuery(SQL)
                SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                        "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                        "VALUES ( '" & idtr & "', '" & KodeToko & "', '" & .Table.Rows(a)!JenisTR & "', " &
                        " '" & .Table.Rows(a)!IdRec & "', '" & Format(.Table.Rows(a)!tgltr, "yyyy-MM-dd") & "',  " &
                        "'" & Trim(.Table.Rows(a)!kodebrg) & "',  " & tin & ", " & tout & ", " & saldo & ", " &
                        "'" & .Table.Rows(a)!satuan & "', " & IIf(.Table.Rows(a)!qty < 0, .Table.Rows(a)!qty * -1, .Table.Rows(a)!qty) & ",  " &
                        " " & .Table.Rows(a)!Harga & ", " & IIf(.Table.Rows(a)!qty < 0, .Table.Rows(a)!qty * -1, .Table.Rows(a)!qty) * .Table.Rows(a)!Harga & ", " &
                        "'" & .Table.Rows(a)!UserID & "', '" & .Table.Rows(a)!lastupd & "', '" & .Table.Rows(a)!kodeToko & "') "
                Proses.ExecuteNonQuery(SQL)
            Next a
        End With
    End Sub


    Public Function GetIdTR(ByVal nTable As String, nField As String, nKodeToko As String, tgl As String) As String
        Dim SQL As String, Prefik As String = "", IdRec As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable, kBulan As String = ""
        Select Case Mid(tgl, 3, 2)
            Case "01"
                kBulan = "A"
            Case "02"
                kBulan = "B"
            Case "03"
                kBulan = "C"
            Case "04"
                kBulan = "D"
            Case "05"
                kBulan = "E"
            Case "06"
                kBulan = "F"
            Case "07"
                kBulan = "G"
            Case "08"
                kBulan = "H"
            Case "09"
                kBulan = "I"
            Case "10"
                kBulan = "J"
            Case "11"
                kBulan = "K"
            Case "12"
                kBulan = "L"
        End Select

        Prefik = Mid(tgl, 1, 2) + kBulan + nKodeToko
        'Proses.GetMaxId("t_transaksi", "idtr", KodeToko & "TR")
        'YYB TK TR 99999
        'YYBTKOC99999
        'YY = Tahun; B = Bulan; TKO = Toko; C = Computer; 99999 = RunNumber
        SQL = "Select max(right(" & nField & ",5)) + 100001 As TID " &
            " From " & nTable & " " &
            "Where Left(" & nField & ",7) = '" & Prefik & "'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If IsDBNull(dbTable.Rows(0)!tid) Then
                IdRec = Prefik + "00001"
            Else
                IdRec = Prefik + Microsoft.VisualBasic.Right(dbTable.Rows(0)!tid, 5)
            End If
        End If
        GetIdTR = IdRec
    End Function

    Private Sub TempTable(kodetoko As String)
        Dim kodestock As String = Mid(kodetoko, 4, 2)
        Application.DoEvents()
        StatusHitung.Text = "Proses simpan data ke table temporary..." & kodetoko

        SQL = "Delete T_TransaksiD where kodetoko = '" & kodetoko & "' "
        Proses.ExecuteNonQuery(SQL)

        If kodetoko <> "KM004" Then
            SQL = "Insert into t_TransaksiD (Kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
                "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
                "Select '" & kodetoko & "', 'Saldo Awal', IdRec, periodeStock, " &
                "       KodeBrg, qty, satuan, 0, 0, UserID,  " &
                "       LastUPD, '" & kodetoko & "', qty, 0, qty, 0  " &
                "  From t_stockawal  " &
                " Where aktifYN = 'Y' "
            Proses.ExecuteNonQuery(SQL)

            SQL = "Insert into t_TransaksiD (Kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select '" & kodetoko & "', 'Penyesuaian', IdRec, tgl, " &
            "       KodeBrg, qty, satuan, harga, sub_total, h.UserID,  " &
            "       h.LastUPD, '" & kodetoko & "', iif(qty>0, qty, 0), iif(qty<0, qty * -1, 0), 0,  0  " &
            "  From t_PenyesuaianH H inner join t_PenyesuaianD D on idrec = id_rec and h.kode_toko = d.kode_toko  " &
            " Where h.aktifYN = 'Y' "
            Proses.ExecuteNonQuery(SQL)

            SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
                "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
                "Select '" & kodetoko & "', 'Pembelian', t_poh.IdRec, TglPO, KodeBrg, QTY, satuan, " &
                "       harga, t_pod.SubTotal, t_POH.UserID, t_POH.LastUPD,  '" & kodetoko & "', qty, 0, 0,  0 " &
                "  From t_poh  inner Join t_pod   On idrec = id_rec  " &
                " Where t_POH.aktifyn = 'Y' and t_POD.AktifYN = 'Y'  "
            Proses.ExecuteNonQuery(SQL)
        End If

        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_SOH.Kode_Toko, 'Penjualan', t_SOH.IdRec, TglPenjualan, " &
            "       KodeBrg, QTY, satuan, harga, Sub_Total, t_SOH.UserID,  " &
            "       t_SOH.LastUPD, t_SOH.Kode_Toko, 0, qty, 0,  0  " &
            "  From t_SOH inner Join t_SOD On idrec = id_rec AND t_SOD.aktifYN = 'Y'  " &
            " Where t_SOH.Kode_Toko  = '" & kodetoko & "' AND t_SOH.aktifYN = 'Y' "
        Proses.ExecuteNonQuery(SQL)

        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_koreksistock.Kode_Toko, 'Stock Opname', t_koreksistock.IdRec, " &
            "       tglkoreksi, KodeBrg, QTYKoreksi, satuan, harga, SubTotal, t_koreksistock.UserID, " &
            "       t_koreksistock.LastUPD, t_koreksistock.Kode_Toko, iif(QTYKoreksi>0, QTYKoreksi, 0), " &
            "       iif(QTYKoreksi<0, QTYKoreksi * -1, 0), QTYKomputer, QTYFisik " &
            "From t_koreksistock " &
            "Where kode_toko ='" & kodetoko & "' and aktifYN = 'Y' "
        Proses.ExecuteNonQuery(SQL)

        'If kodetoko = "KM003" Then
        'Else
        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan," &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_ReturJualH.Kode_Toko, 'Retur Jual', t_ReturJualH.IdRec, TglRetur, " &
            "       KodeBrg, QTY, satuan, harga, SubTotal, t_ReturJualH.UserID,  " &
            "       t_ReturJualH.LastUPD, t_ReturJualH.Kode_Toko, qty, 0, 0,  0   " &
            "  From t_ReturJualH inner Join t_ReturJualD On idrec = id_rec  " &
            "       And t_ReturJualD.aktifYN = 'Y' " &
            "Where t_ReturJualH.Kode_Toko ='" & kodetoko & "' and t_ReturJualD.aktifYN = 'Y' "
        Proses.ExecuteNonQuery(SQL)
        If kodetoko <> "KM004" Then
            SQL = "update t_ReturBeliH set Kode_toko = '" & kodetoko & "' where aktifYN = 'Y'"
            Proses.ExecuteNonQuery(SQL)
            SQL = "update t_ReturBeliD set Kode_toko = '" & kodetoko & "' where aktifYN = 'Y'"
            Proses.ExecuteNonQuery(SQL)
        End If
        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan," &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_ReturBeliH.Kode_Toko, 'Retur Beli', t_ReturBeliH.IdRec, TglRetur, " &
            "       KodeBrg, QTY, satuan, harga, SubTotal, t_ReturBeliH.UserID,  " &
            "       t_ReturBeliH.LastUPD, t_ReturBeliH.Kode_Toko, 0, qty, 0,  0   " &
            "  From t_ReturBeliH inner Join t_ReturBeliD On idrec = id_rec  " &
            "       And t_ReturBeliD.aktifYN = 'Y'  " &
            "Where t_ReturBeliH.Kode_Toko ='" & kodetoko & "' and t_ReturBeliH.aktifYN = 'Y' "
        Proses.ExecuteNonQuery(SQL)

        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan,
                     harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir) 
          Select  '" & kodetoko & "', 'TRF ke:' + KodeTokoTujuan, t_TransferH.IdRec, TglTransfer,
                 KodeBrg, QTY, satuan, harga, Sub_Total, t_TransferH.UserID, 
                 t_TransferH.LastUPD, t_TransferH.Kode_Toko, 0, qty, 0,  0 
            From t_TransferH inner Join t_TransferD On idrec = id_rec AND t_TransferD.aktifYN = 'Y' 
           Where t_TransferH.KodeTokoAsal  = '" & kodetoko & "'  AND t_TransferH.aktifYN = 'Y' "
        Proses.ExecuteNonQuery(SQL)

        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan,
                     harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir) 
          Select  '" & kodetoko & "', 'TRF dari:' + KodeTokoAsal, t_TransferH.IdRec, TglTransfer,
                 KodeBrg, QTY, satuan, harga, Sub_Total, t_TransferH.UserID,
                 t_TransferH.LastUPD, t_TransferH.Kode_Toko, 0, qty, 0, 0 
            From t_TransferH inner Join t_TransferD On idrec = id_rec And t_TransferD.aktifYN = 'Y' 
           Where t_TransferH.KodeTokoTujuan = '" & kodetoko & "' AND t_TransferH.aktifYN = 'Y'  "
        Proses.ExecuteNonQuery(SQL)
    End Sub

    Private Sub TempTable_Periode(kodetoko As String, Periode As String)
        Dim kodestock As String = Mid(kodetoko, 4, 2)
        Dim mKondisi As String = ""
        Application.DoEvents()
        StatusHitung.Text = "Proses simpan data ke table temporary..." & kodetoko

        SQL = "truncate table T_TransaksiD  "
        Proses.ExecuteNonQuery(SQL)

        If kodetoko <> "KM004" Then
            If Periode = "202101" And kodetoko = "OL005" Then
                SQL = "Insert into t_TransaksiD (Kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
                "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
                "Select '" & kodetoko & "', 'Saldo Awal', IdRec, periodeStock, " &
                "       KodeBrg, qty, satuan, 0, 0, UserID,  " &
                "       LastUPD, '" & kodetoko & "', qty, 0, qty, 0  " &
                "  From t_stockawal  " &
                " Where aktifYN = 'Y' and convert(varchar(6), lastupd, 112) = '" & Periode & "' "
                Proses.ExecuteNonQuery(SQL)
            End If
            If Periode = "201912" Then
                mKondisi = " and convert(varchar(6), tgl, 112) <= '" & Periode & "' "
            Else
                mKondisi = " and convert(varchar(6), tgl, 112) = '" & Periode & "' "
            End If
            SQL = "Insert into t_TransaksiD (Kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
                "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
                "Select '" & kodetoko & "', 'Penyesuaian', IdRec, tgl, " &
                "       KodeBrg, qty, satuan, harga, sub_total, h.UserID,  " &
                "       h.LastUPD, '" & kodetoko & "', iif(qty>0, qty, 0), iif(qty<0, qty * -1, 0), 0,  0  " &
                "  From t_PenyesuaianH H inner join t_PenyesuaianD D on idrec = id_rec and h.kode_toko = d.kode_toko  " &
                " Where h.aktifYN = 'Y' " & mKondisi & " "
            Proses.ExecuteNonQuery(SQL)
            If Periode = "201912" Then
                mKondisi = " and convert(varchar(6), tglpo, 112) <= '" & Periode & "' "
            Else
                mKondisi = " and convert(varchar(6), tglpo, 112) = '" & Periode & "' "
            End If
            SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
                "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
                "Select '" & kodetoko & "', 'Pembelian', t_poh.IdRec, TglPO, KodeBrg, QTY, satuan, " &
                "       harga, t_pod.SubTotal, t_POH.UserID, t_POH.LastUPD,  '" & kodetoko & "', qty, 0, 0,  0 " &
                "  From t_poh  inner Join t_pod   On idrec = id_rec  " &
                " Where t_POH.aktifyn = 'Y' and t_POD.AktifYN = 'Y' " & mKondisi & "  "
            Proses.ExecuteNonQuery(SQL)
        End If
        If Periode = "201912" Then
            mKondisi = " and convert(varchar(6), TglPenjualan, 112) <= '" & Periode & "' "
        Else
            mKondisi = " and convert(varchar(6), TglPenjualan, 112) = '" & Periode & "' "
        End If
        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_SOH.Kode_Toko, 'Penjualan', t_SOH.IdRec, TglPenjualan, " &
            "       KodeBrg, QTY, satuan, harga, Sub_Total, t_SOH.UserID,  " &
            "       t_SOH.LastUPD, t_SOH.Kode_Toko, 0, qty, 0,  0  " &
            "  From t_SOH inner Join t_SOD On idrec = id_rec AND t_SOD.aktifYN = 'Y'  " &
            " Where t_SOH.Kode_Toko  = '" & kodetoko & "' AND t_SOH.aktifYN = 'Y'  " & mKondisi & " "
        Proses.ExecuteNonQuery(SQL)

        If Periode = "201912" Then
            mKondisi = " and convert(varchar(6), tglkoreksi, 112) <= '" & Periode & "' "
        Else
            mKondisi = " and convert(varchar(6), tglkoreksi, 112) = '" & Periode & "' "
        End If
        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan, " &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_koreksistock.Kode_Toko, 'Stock Opname', t_koreksistock.IdRec, " &
            "       tglkoreksi, KodeBrg, QTYKoreksi, satuan, harga, SubTotal, t_koreksistock.UserID, " &
            "       t_koreksistock.LastUPD, t_koreksistock.Kode_Toko, iif(QTYKoreksi>0, QTYKoreksi, 0), " &
            "       iif(QTYKoreksi<0, QTYKoreksi * -1, 0), QTYKomputer, QTYFisik " &
            "From t_koreksistock " &
            "Where kode_toko ='" & kodetoko & "' and aktifYN = 'Y'  " & mKondisi & " "
        Proses.ExecuteNonQuery(SQL)

        If Periode = "201912" Then
            mKondisi = " and convert(varchar(6), TglRetur, 112) <= '" & Periode & "' "
        Else
            mKondisi = " and convert(varchar(6), TglRetur, 112) = '" & Periode & "' "
        End If
        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan," &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_ReturJualH.Kode_Toko, 'Retur Jual', t_ReturJualH.IdRec, TglRetur, " &
            "       KodeBrg, QTY, satuan, harga, SubTotal, t_ReturJualH.UserID,  " &
            "       t_ReturJualH.LastUPD, t_ReturJualH.Kode_Toko, qty, 0, 0,  0   " &
            "  From t_ReturJualH inner Join t_ReturJualD On idrec = id_rec  " &
            "       And t_ReturJualD.aktifYN = 'Y' " &
            "Where t_ReturJualH.Kode_Toko ='" & kodetoko & "' and t_ReturJualD.aktifYN = 'Y'  " & mKondisi & " "
        Proses.ExecuteNonQuery(SQL)
        If kodetoko <> "KM004" Then
            SQL = "update t_ReturBeliH set Kode_toko = '" & kodetoko & "' where aktifYN = 'Y'"
            Proses.ExecuteNonQuery(SQL)
            SQL = "update t_ReturBeliD set Kode_toko = '" & kodetoko & "' where aktifYN = 'Y'"
            Proses.ExecuteNonQuery(SQL)
        End If


        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan," &
            "   harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir)  " &
            "Select t_ReturBeliH.Kode_Toko, 'Retur Beli', t_ReturBeliH.IdRec, TglRetur, " &
            "       KodeBrg, QTY, satuan, harga, SubTotal, t_ReturBeliH.UserID,  " &
            "       t_ReturBeliH.LastUPD, t_ReturBeliH.Kode_Toko, 0, qty, 0,  0   " &
            "  From t_ReturBeliH inner Join t_ReturBeliD On idrec = id_rec  " &
            "       And t_ReturBeliD.aktifYN = 'Y'  " &
            "Where t_ReturBeliH.Kode_Toko ='" & kodetoko & "' and t_ReturBeliH.aktifYN = 'Y'  " & mKondisi & "  "
        Proses.ExecuteNonQuery(SQL)
        If Periode = "201912" Then
            mKondisi = " and convert(varchar(6), TglTransfer, 112) <= '" & Periode & "' "
        Else
            mKondisi = " and convert(varchar(6), TglTransfer, 112) = '" & Periode & "' "
        End If
        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan,
                     harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir) 
          Select  '" & kodetoko & "', 'TRF ke:' + KodeTokoTujuan, t_TransferH.IdRec, TglTransfer,
                 KodeBrg, QTY, satuan, harga, Sub_Total, t_TransferH.UserID, 
                 t_TransferH.LastUPD, t_TransferH.Kode_Toko, 0, qty, 0,  0 
            From t_TransferH inner Join t_TransferD On idrec = id_rec AND t_TransferD.aktifYN = 'Y' 
           Where t_TransferH.KodeTokoAsal  = '" & kodetoko & "'  AND t_TransferH.aktifYN = 'Y'  " & mKondisi & " "
        Proses.ExecuteNonQuery(SQL)

        SQL = "insert into t_transaksiD (kodetoko, jenistr, idrec, tgltr, kodebrg, qty, satuan,
                     harga, subtotal, userid, lastupd, kode_toko, qtyin, qtyout, awal, akhir) 
          Select  '" & kodetoko & "', 'TRF dari:' + KodeTokoAsal, t_TransferH.IdRec, TglTransfer,
                 KodeBrg, QTY, satuan, harga, Sub_Total, t_TransferH.UserID,
                 t_TransferH.LastUPD, t_TransferH.Kode_Toko, 0, qty, 0, 0 
            From t_TransferH inner Join t_TransferD On idrec = id_rec And t_TransferD.aktifYN = 'Y' 
           Where t_TransferH.KodeTokoTujuan = '" & kodetoko & "' AND t_TransferH.aktifYN = 'Y'   " & mKondisi & " "
        Proses.ExecuteNonQuery(SQL)
    End Sub
    Private Sub HitungStock(id_Toko As String)
        Dim jenistr As String = "", rId As String = ""
        Dim tin As Double = 0, tout As Double = 0, tQTY As Double = 0
        Dim idtr As String = "", saldo As Double = 0, mKet As String = ""
        Dim kodestock As String = Mid(id_Toko, 4, 2)
        Me.Cursor = Cursors.WaitCursor
        cmd_Hitung_Ulang.Enabled = False


        SQL = "SELECT *  FROM sys.tables " &
             "WHERE name = 'tmp_RekapStock'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "Select top 0 * into tmp_RekapStock From t_RekapStock "
            Proses.ExecuteNonQuery(SQL)
        Else
            SQL = "Delete tmp_RekapStock where kodeToko = '" & id_Toko & "' "
            Proses.ExecuteNonQuery(SQL)
        End If

        SQL = "insert into tmp_RekapStock " &
            "Select KodeToko, convert(CHAR(7), tglTr, 121) periode, kodebrg, sum(awal) awal, sum(qtyin) qin, " &
            "       sum(qtyout) qout, 0 Saldo " &
            "  From t_TransaksiD" &
            " Where kodeToko = '" & id_Toko & "' " &
            " Group By kodeToko, Convert(Char(7), tglTr, 121), kodebrg " &
            " Order By kodeToko, Convert(Char(7), tglTr, 121), kodebrg "
        Proses.ExecuteNonQuery(SQL)
        Me.Cursor = Cursors.Default
        StatusHitung.Text = "Insert into t_RekapStock...."

        Dim oKodeToko As String = ""
        Dim tPeriode As String = "", oPeriode As String = ""

        SQL = "Select kodebrg, kodebrg, sum(awal) awal, sum(qtyin) qin, " &
            "      sum(qtyout) qout, sum(awal) + sum(qtyin) -  sum(qtyout) Saldo  " &
            "  From t_TransaksiD  " &
            " Where kodeToko = '" & id_Toko & "'  " &
            " Group by kodebrg "
        dbTable = Proses.ExecuteQuery(SQL)
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                Application.DoEvents()
                ' id_Toko = .Table.Rows(a)!kodetoko
                If (.Table.Rows(a)!kodebrg = "03234") Then
                    MsgBox(.Table.Rows(a)!saldo)
                End If
                SQL = "Update m_barang Set stock" & kodestock & " = " & IIf(IsDBNull(.Table.Rows(a)!saldo), 0, .Table.Rows(a)!saldo) & " " &
                    "where IdRec =  '" & .Table.Rows(a)!kodebrg & "' "
                Proses.ExecuteNonQuery(SQL)
                StatusHitung.Text = Trim(idToko.Text) + "  " + Trim(.Table.Rows(a)!kodebrg) + "  -  " + Format(a, "###,##0") + " / " + Format(dbTable.Rows.Count, "###,##0")

            Next
        End With

        If Me.Text <> "Auto" Then
            StatusHitung.Text = ".:F1N15H... Start @" + Me.Text + " Finish @" + Format(Now(), "hh:mm:ss")
            MsgBox("Proses hitung ulang selesai", vbInformation + vbOKOnly, ".:Finish : " + Format(Now(), "hh:mm:ss"))
        End If
        idToko.Enabled = True


        StatusHitung.Text = "Finish !"
    End Sub

    Private Function KodeBulan(bulan) As String
        Dim kBulan As String = ""
        Select Case bulan
            Case "01"
                kBulan = "A"
            Case "02"
                kBulan = "B"
            Case "03"
                kBulan = "C"
            Case "04"
                kBulan = "D"
            Case "05"
                kBulan = "E"
            Case "06"
                kBulan = "F"
            Case "07"
                kBulan = "G"
            Case "08"
                kBulan = "H"
            Case "09"
                kBulan = "I"
            Case "10"
                kBulan = "J"
            Case "11"
                kBulan = "K"
            Case "12"
                kBulan = "L"
        End Select
        KodeBulan = kBulan
    End Function

    Private Sub idToko_TextChanged(sender As Object, e As EventArgs) Handles idToko.TextChanged
        If Len(Trim(idToko.Text)) = 0 Then
            NamaToko.Text = ""
        End If
    End Sub

    Private Sub Form_HitungStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        cektable()
        idToko.Text = FrmMenuUtama.Kode_Toko.Text
        NamaToko.Text = FrmMenuUtama.Nama_Toko.Text
        If idToko.Text = "OL005" Then
            StatusHitung.Text = "Periode Hitung Ulang " & NamaToko.Text & " : Jan 2021 "
        Else
            StatusHitung.Text = "Periode Hitung Ulang " & NamaToko.Text & " : Des 2019 "
        End If
        GetLastProses()
    End Sub

    Private Sub cektable()
        Me.Cursor = Cursors.WaitCursor

        SQL = "Select name FROM sys.tables " &
             "WHERE name = 't_Transaksi_201912'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            cmbPeriode.Value = "12-01-2019"
        Else
            Proses.CloseConn()
            SQL = "Select right(name,6) name FROM sys.tables " &
             "WHERE name like 't_Transaksi_20%' " &
             "order by name desc "
            dbTable = Proses.ExecuteQuery(SQL)
            Dim mPeriode As Date
            mPeriode = Mid(dbTable.Rows(0)!name, 5, 2) + "/01/" + Mid(dbTable.Rows(0)!name, 1, 4)
            cmbPeriode.Value = DateAdd(DateInterval.Month, 1, mPeriode)
        End If

        SQL = "Select *  FROM sys.tables " &
             "WHERE name = 't_TransaksiD'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "CREATE TABLE dbo.t_TransaksiD(
	            id bigint IDENTITY(1, 1) Not NULL,
	            idRec varchar(20) Not NULL,
                tglTr DateTime Not NULL,
                jenisTr varchar(50) NULL,
                kodeToko varchar(5) NULL,
                KodeBrg varchar(30) NULL,
                NamaBrg varchar(100) NULL,
                QTY Int NULL,
                Satuan varchar(10) NULL,
                harga money NULL,
                subtotal money NULL,
                NamaFile varchar(100) NULL,
                UserID varchar(20) NULL,
                LastUpd DateTime NULL,
                Kode_Toko varchar(10) NULL) "
        Else
            SQL = "delete t_TransaksiD where kodeToko = '" & idToko.Text & "' "
        End If
        Proses.ExecuteNonQuery(SQL)

        SQL = "Select * From sys.objects where name = 't_RekapStock' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "CREATE TABLE  t_RekapStock (
                kodeToko Varchar (5) NULL,
	            periode  varchar (7) NULL,
	            kodebrg  varchar (30) NULL,	
	            awal  float  NULL,
	            qin   float  NULL,
	            qout  float  NULL,
	            Saldo float  NULL) "
            Proses.ExecuteNonQuery(SQL)
            SQL = "CREATE TABLE  t_RekapStockD (
                kodeToko Varchar (5) NULL,
	            periode  varchar (7) NULL,
	            kodebrg  varchar (30) NULL,	
	            awal  float  NULL,
	            qin   float  NULL,
	            qout  float  NULL,
	            Saldo float  NULL) "
            Proses.ExecuteNonQuery(SQL)
        Else
            SQL = "Truncate table t_RekapStock"
            Proses.ExecuteNonQuery(SQL)
            SQL = "Truncate table t_RekapStockD"
            Proses.ExecuteNonQuery(SQL)
        End If
        SQL = "Select * From information_schema.COLUMNS Where TABLE_NAME = 't_TransaksiD' and COLUMN_NAME = 'qtyin'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "ALTER TABLE t_TransaksiD ADD qtyin real Default 0, qtyout real Default 0, awal real, akhir real "
            Proses.ExecuteNonQuery(SQL)
        End If


        SQL = "Select * From sys.objects where name = 'logfile' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "CREATE TABLE logfile (
            Id BIGINT IDENTITY(1, 1) PRIMARY KEY,
            Keterangan VARCHAR(MAX),
            UserId VARCHAR(30),
            LastUPD DATETIME NOT NULL DEFAULT GETUTCDATE() ) "
            Proses.ExecuteNonQuery(SQL)
        End If

        SQL = "SELECT *  FROM sys.tables " &
            "WHERE name = 't_Hitung'  "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = "CREATE TABLE [dbo].[t_Hitung](
	            [idrec] [int] IDENTITY(1,1) NOT NULL,
	            [kode_toko] [varchar](5) NOT NULL,
	            [userid] [varchar](20) NOT NULL,
	            [tglproses] [datetime] NOT NULL,	
	            [EndProcess] [datetime] Not NULL,
	            periodeproses datetime) "
            Proses.ExecuteNonQuery(SQL)
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub idToko_KeyPress(sender As Object, e As KeyPressEventArgs) Handles idToko.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select * From m_Toko " &
                "Where idrec = '" & idToko.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                idToko.Text = dbTable.Rows(0) !idrec
                NamaToko.Text = dbTable.Rows(0) !nama
                cmd_Hitung_Ulang.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                     " From m_Toko " &
                     "Where AktifYN = 'Y' " &
                     "  And nama Like '%" & idToko.Text & "' " &
                     "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                idToko.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select * From m_Toko " &
                    "Where idrec = '" & idToko.Text & "' "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    idToko.Text = dbTable.Rows(0) !idrec
                    NamaToko.Text = dbTable.Rows(0) !nama
                    cmd_Hitung_Ulang.Focus()
                Else
                    idToko.Text = ""
                    NamaToko.Text = ""
                    idToko.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Form_HitungStock_Click(sender As Object, e As EventArgs) Handles Me.Click

    End Sub
End Class