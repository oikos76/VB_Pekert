Imports System.IO
Imports System.Data.SqlClient
Public Class Form_DataTerima
    Dim SQL As String, UserID As String = FrmMenuUtama.TsPengguna.Text
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Protected CN As SqlConnection
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database
    Private Sub cmdTerima_Click(sender As Object, e As EventArgs) Handles cmdTerima.Click
        'query("RESTORE DATABASE " & cmbdatabase.Text & " FROM disk='" & OpenFileDialog1.FileName & "'")

        If FrmMenuUtama.Kode_Toko.Text = KodeTokoAsal.Text Then
            MsgBox("FILE asal tidak boleh sama dengan lokasi saat ini program berada...", vbCritical + vbOKOnly, ".:Warning !")
            cariFolder.Focus()
            Exit Sub
        End If
        cmdTerima.Enabled = False
        SQL = "Select * From m_Toko " &
            "Where idrec = '" & KodeTokoAsal.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            MsgBox("File belum di pilih / format penulisan file salah !", vbCritical + vbOKOnly, ".:Warning !")
            NamaFile.Focus()
            Exit Sub
        End If

        SQL = "Select * From m_Toko " &
            "Where idrec = '" & Kode_Toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            MsgBox("File belum di pilih / format penulisan file salah !", vbCritical + vbOKOnly, ".:Warning !")
            NamaFile.Focus()
            Exit Sub
        End If
        Restore()
        cmdTerima.Enabled = True
    End Sub

    Private Sub Restore()
        Dim MsgSQL As String = "", RS05 As New DataTable, record As String = ""
        Dim dBase As String = "PekertiTRF", fieldName As String = ""
        Dim namaFolder As String = My.Settings.LokasiFile,
            mKondisi As String = ""

        Panel1.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        SQL = "RESTORE DATABASE " & dBase & "	FROM DISK='" & NamaFile.Text & "' WITH " &
            "MOVE '" & dBase & "' TO '" & namaFolder & "\" & dBase & ".mdf', " &
            "MOVE '" & dBase & "_log' TO '" & namaFolder & "\" & dBase & ".ldf', REPLACE; "
        Proses.ExecuteNonQuery(SQL)

        jenisData.Text = "Synchronize SP..."
        idRecord.Text = ""

        'MsgSQL = "Select NoSP From PekertiTRF.dbo.T_SP  Group By NoSP "
        'RS05 = Proses.ExecuteQuery(MsgSQL)
        'For a = 0 To RS05.Rows.Count - 1
        '    Application.DoEvents()
        '    idRecord.Text = RS05.Rows(a) !NoSP
        '    MsgSQL = "Delete T_SP where NoSP = '" & RS05.Rows(a) !NoSP & "' "
        '    Proses.ExecuteNonQuery(MsgSQL)
        'Next a
        If KodeTokoAsal.Text = "PKT01" And Kode_Toko.Text = "PKT02" Then
            'terima data Waru di Gudang
            MsgSQL = "Select NoPI From PekertiTRF.dbo.T_PI  Group By NoPI "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To RS05.Rows.Count - 1
                Application.DoEvents()
                idRecord.Text = RS05.Rows(a) !NoPI
                MsgSQL = "Delete T_PI where NoPI = '" & RS05.Rows(a) !NoPI & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next a
            mKondisi = ""
        ElseIf KodeTokoAsal.Text = "PKT02" And Kode_Toko.Text = "PKT01" Then
            'terima data Gudang di Waru
            MsgSQL = "Select NoPraLHP From PekertiTRF.dbo.t_PraLHP  Group By NoPraLHP "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To RS05.Rows.Count - 1
                Application.DoEvents()
                idRecord.Text = RS05.Rows(a) !NoPraLHP
                MsgSQL = "Delete t_PraLHP where NoPraLHP = '" & RS05.Rows(a) !NoPraLHP & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next a

            MsgSQL = "Select NoLHP From PekertiTRF.dbo.t_LHP  Group By NoLHP "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To RS05.Rows.Count - 1
                Application.DoEvents()
                idRecord.Text = RS05.Rows(a) !NoLHP
                MsgSQL = "Delete t_LHP where NoLHP = '" & RS05.Rows(a) !NoLHP & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next a
            MsgSQL = "Select NoRetur From PekertiTRF.dbo.t_Retur  Group By NoRetur "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To RS05.Rows.Count - 1
                Application.DoEvents()
                idRecord.Text = RS05.Rows(a) !NoRetur
                MsgSQL = "Delete t_Retur where NoRetur = '" & RS05.Rows(a) !NoRetur & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next a

            MsgSQL = "Select IdRec From PekertiTRF.dbo.t_PackingList  Group By IdRec "
            RS05 = Proses.ExecuteQuery(MsgSQL)
            For a = 0 To RS05.Rows.Count - 1
                Application.DoEvents()
                idRecord.Text = RS05.Rows(a) !IdRec
                MsgSQL = "Delete t_PackingList where IdRec = '" & RS05.Rows(a) !IdRec & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Next a
            mKondisi = " AND a.TABLE_NAME in ('t_PackingList', 't_PraLHP', 't_LHP', 't_Retur') "

        Else
            MsgBox("File yang di terima salah !", vbCritical + vbOKOnly, "Salah file !")
            Exit Sub
        End If
        SQL = "SELECT * " &
            " FROM  pekertitrf.INFORMATION_SCHEMA.TABLES a " &
            "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='PekertiTRF' " &
            "  " & mKondisi & " " &
            "ORDER BY a.TABLE_NAME "
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            jenisData.Text = dbTable.Rows(a) !TABLE_NAME

            If jenisData.Text = "m_KodeProdukVariasi" Then
                mKondisi = ""
            Else
                mKondisi = " WHERE aktifYN='Y' "
            End If

            SQL = "SELECT * FROM  PekertiTRF.dbo." & jenisData.Text & "  " & mKondisi & " "
            RS05 = Proses.ExecuteQuery(SQL)
            fieldName = RS05.Columns(0).ColumnName

            For b = 0 To RS05.Rows.Count - 1
                Application.DoEvents()
                If IsDBNull(RS05.Rows(b)(0)) Then
                    SQL = "UPDATE " & jenisData.Text & " set  " & RS05.Columns(0).ColumnName & " = '' WHERE  " & RS05.Columns(0).ColumnName & " is null "
                    Proses.ExecuteNonQuery(SQL)
                Else
                    idRecord.Text = RS05.Rows(b)(0)
                End If

                SQL = "INSERT INTO " & jenisData.Text & " " &
                    "Select * FROM PekertiTRF.dbo." & jenisData.Text & " " &
                    "WHERE " & RS05.Columns(0).ColumnName & " = '" & RS05.Rows(b)(0) & "' "
                Proses.ExecuteNonQuery(SQL)
            Next b
        Next a
        Panel1.Enabled = True
        MsgBox("Proses terima data dari " & NamaTokoAsal.Text & " selesai.", vbInformation + vbOKOnly, ".:Finish !")
        jenisData.Text = ""
        idRecord.Text = ""
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UpdateBarang(dBase As String)
        Dim DBCek As New DataTable
        jenisData.Text = "Proses Synchronize Data Master Barang"
        idRecord.Text = ""
        SQL = "Select * From " & dBase & ".dbo.m_barang where idrec <> '' order by idrec, aktifYN "
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            Dim mAlias As String = ""
            If (IsDBNull(dbTable.Rows(a)!alias)) Then
                mAlias = ""
            Else
                mAlias = Trim(dbTable.Rows(a)!alias)
            End If
            idRecord.Text = dbTable.Rows(a) !idrec & " data ke " & Format(a, "###,##0") & " dari " & Format(dbTable.Rows.Count - 1, "###,##0")
            If dbTable.Rows(a) !aktifyn = "N" Then
                SQL = " DELETE m_Barang " &
                    "WHERE idrec = '" & dbTable.Rows(a) !idrec & "' " &
                    "  AND AktifYN = 'N' "
                Proses.ExecuteNonQuery(SQL)
            End If
            SQL = "Select * from M_Barang where idrec = '" & dbTable.Rows(a) !idrec & "' order by idrec, aktifYN "
            DBCek = Proses.ExecuteQuery(SQL)
            If DBCek.Rows.Count = 0 Or DBCek.Rows.Count > 1 Then
                If DBCek.Rows.Count > 1 Then
                    SQL = " DELETE m_Barang " &
                       "WHERE idrec = '" & dbTable.Rows(a) !idrec & "' "
                    Proses.ExecuteNonQuery(SQL)
                End If
                SQL = "Insert into M_Barang (idrec, barcode, nama, id_GroupBrg, GroupBrg, id_JenisBrg, JenisBrg, alias, " &
                "kd_BrgSupp, qty, unit, id_dept, dept, id_style, style, id_texture, texture, id_motif, motif, " &
                "id_design, design, id_size, size, id_warna, warna, hargajual, hargabeli, hargapokok, " &
                "xlength, xwidth, xheight, xcbm, stockmax, stockmin, AktifYN, UserID, LastUPD) values ( " &
                "'" & dbTable.Rows(a) !idrec & "', '" & Trim(dbTable.Rows(a) !barcode) & "', '" & Trim(dbTable.Rows(a) !Nama) & "', " &
                "'" & Trim(dbTable.Rows(a) !Id_GroupBrg) & "', '" & Trim(dbTable.Rows(a) !GroupBrg) & "', '" &
                "" & Trim(dbTable.Rows(a) !id_JenisBrg) & "', '" & Trim(dbTable.Rows(a) !JenisBrg) & "'," &
                "'" & Trim(mAlias) & "', '" & Trim(dbTable.Rows(a) !kd_BrgSupp) & "', " &
                "" & dbTable.Rows(a) !qty & ", '" & Trim(dbTable.Rows(a) !unit) & "', " &
                "'" & Trim(dbTable.Rows(a) !id_Dept) & "', '" & Trim(dbTable.Rows(a) !Dept) & "', " &
                "'" & Trim(dbTable.Rows(a) !id_style) & "', '" & Trim(dbTable.Rows(a) !style) & "', " &
                "'" & Trim(dbTable.Rows(a) !id_texture) & "', '" & Trim(dbTable.Rows(a) !Texture) & "', " &
                "'" & Trim(dbTable.Rows(a) !id_Motif) & "', '" & Trim(dbTable.Rows(a) !Motif) & "', " &
                "'" & Trim(dbTable.Rows(a) !id_Design) & "', '" & Trim(dbTable.Rows(a) !Design) & "', " &
                "'" & Trim(dbTable.Rows(a) !Id_Size) & "', '" & Trim(dbTable.Rows(a) !Size) & "', " &
                "'" & Trim(dbTable.Rows(a) !id_Warna) & "', '" & Trim(dbTable.Rows(a) !Warna) & "', " &
                " " & dbTable.Rows(a) !HargaJual & ", " & dbTable.Rows(a) !HargaBeli & ", " & dbTable.Rows(a) !HargaPokok & ", " &
                " " & dbTable.Rows(a) !xLength & ", " & dbTable.Rows(a) !xWidth & ", " & dbTable.Rows(a) !xHeight & ", " &
                " " & dbTable.Rows(a) !xcbm & ", " & dbTable.Rows(a) !stockmax & ", " & dbTable.Rows(a) !StockMin & ", " &
                "'" & dbTable.Rows(a) !aktifyn & "' , '" & dbTable.Rows(a) !UserID & "', '" & dbTable.Rows(a) !lastupd & "') "
                Proses.ExecuteNonQuery(SQL)

            Else
                SQL = "Update M_Barang Set " &
                "    barcode = '" & Trim(dbTable.Rows(a)!barcode) & "', " &
                "       nama = '" & Trim(dbTable.Rows(a)!Nama) & "', " &
                "id_GroupBrg = '" & Trim(dbTable.Rows(a)!id_GroupBrg) & "', " &
                "   GroupBrg = '" & Trim(dbTable.Rows(a)!GroupBrg) & "', " &
                "id_JenisBrg = '" & Trim(dbTable.Rows(a)!id_JenisBrg) & "', " &
                "   JenisBrg = '" & Trim(dbTable.Rows(a)!JenisBrg) & "', " &
                "      alias = '" & mAlias & "', " &
                " kd_BrgSupp = '" & Trim(dbTable.Rows(a)!kd_BrgSupp) & "', " &
                "        qty = " & dbTable.Rows(a)!qty & ", unit = '" & Trim(dbTable.Rows(a)!unit) & "', " &
                "    id_dept = '" & Trim(dbTable.Rows(a)!id_dept) & "', dept = '" & Trim(dbTable.Rows(a)!dept) & "', " &
                "   id_style = '" & Trim(dbTable.Rows(a)!id_style) & "', style = '" & Trim(dbTable.Rows(a)!style) & "', " &
                " id_texture = '" & Trim(dbTable.Rows(a)!id_texture) & "', texture = '" & Trim(dbTable.Rows(a)!texture) & "', " &
                "   id_motif = '" & Trim(dbTable.Rows(a)!id_motif) & "', motif = '" & Trim(dbTable.Rows(a)!motif) & "', " &
                "  id_design = '" & Trim(dbTable.Rows(a)!id_design) & "',  design = '" & Trim(dbTable.Rows(a)!design) & "', " &
                "    id_size = '" & Trim(dbTable.Rows(a)!id_size) & "', size = '" & Trim(dbTable.Rows(a)!size) & "', " &
                "   id_warna = '" & Trim(dbTable.Rows(a)!id_warna) & "', warna = '" & Trim(dbTable.Rows(a)!warna) & "', " &
                "  hargajual = " & dbTable.Rows(a)!HargaJual & ", hargabeli = " & dbTable.Rows(a)!HargaBeli & ", " &
                " hargapokok = " & dbTable.Rows(a)!HargaPokok & ", " &
                "    xlength = " & dbTable.Rows(a)!xLength & ", " &
                "     xwidth = " & dbTable.Rows(a)!xWidth & ", " &
                "    xheight = " & dbTable.Rows(a)!xHeight & ", " &
                "       xcbm = " & dbTable.Rows(a)!xCbm & ", stockmax = " & dbTable.Rows(a)!StockMax & ", " &
                "   stockmin = " & dbTable.Rows(a)!StockMin & ", aktifYN = '" & dbTable.Rows(a)!aktifyn & "', " &
                "     UserID = '" & dbTable.Rows(a)!UserID & "', LastUPD = '" & dbTable.Rows(a)!LastUPD & "' " &
                "where idrec = '" & dbTable.Rows(a)!idrec & "' "
                Proses.ExecuteNonQuery(SQL)
            End If
            Proses.CloseConn()
        Next a
    End Sub
    Private Sub KoreksiStock(dBase As String)
        Dim tgl1 As String = "", tgl2 As String = "", a As Integer = 0
        jenisData.Text = "Proses Synchronize Data Surat Jalan "
        idRecord.Text = ""

        SQL = "Select idrec From " & dBase & ".dbo.t_KoreksiStock group by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec
            SQL = "delete t_KoreksiStock where idrec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        Next a
        SQL = "Select * From " & dBase & ".dbo.t_KoreksiStock order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec & " " & dbTable.Rows(a)!KodeBrg
            SQL = "INSERT INTO t_KoreksiStock (IDReC, TglKoreksi, NoUrut, KodeBrg, NamaBrg, " &
                " QTYKomputer, QTYFisik, QTYKoreksi, Satuan, Harga, SubTotal, Kode_Toko, " &
                " Keterangan, UserID, AktifYN, LastUPD, TransferYN, id_SOFisik) VALUES ( '" & dbTable.Rows(a)!IDRec & "', " &
                "'" & Format(dbTable.Rows(a)!TglKoreksi, "yyyy-MM-dd") & "', '" & dbTable.Rows(a)!NoUrut & "', " &
                "'" & dbTable.Rows(a)!KodeBrg & "', '" & dbTable.Rows(a)!namabrg & "'," &
                " " & dbTable.Rows(a)!QTYKomputer & ", " & dbTable.Rows(a)!QTYFisik & ", " &
                " " & dbTable.Rows(a)!QTYKoreksi * 1 & ", '" & dbTable.Rows(a)!satuan & "', " &
                " " & dbTable.Rows(a)!Harga & ", " & dbTable.Rows(a)!SubTotal & ", " &
                "'" & dbTable.Rows(a)!Kode_Toko & "', '" & dbTable.Rows(a)!Keterangan & "', " &
                "'" & dbTable.Rows(a)!UserID & "',  '" & dbTable.Rows(a)!aktifYN & "', " &
                "'" & dbTable.Rows(a)!lastupd & "', 'Y', '" & dbTable.Rows(a)!id_SOFisik & "' )"
            Proses.ExecuteNonQuery(SQL)
        Next (a)
        If a > 0 Then
            idRecord.Text = Proses.GetMaxId("m_LogFile", "idrec", KodeTokoAsal.Text & "L")
            SQL = "Insert into m_LogFile (idrec, tgl1, tgl2, NamaFile, JenisTR, Userid, LastUpd) Values " &
                "('" & idRecord.Text & "', '" & tgl1 & "', '" & tgl2 & "', '" & NamaFile.Text & "', 'Terima Koreksi Stock', " &
                "'" & UserID & "', getDate() ) "
            Proses.ExecuteNonQuery(SQL)
        End If

    End Sub
    Private Sub TransferBarang(dBase As String)
        Dim tgl1 As String = "", tgl2 As String = ""
        jenisData.Text = "Proses Synchronize Data Transfer Barang "
        idRecord.Text = ""


        SQL = "Select idrec from " & dBase & ".dbo.t_TransferBarang group by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec
            If a = 0 Then
                tgl1 = Format(dbTable.Rows(a)!TglTransfer, "yyyy-MM-dd")
            End If
            tgl2 = Format(dbTable.Rows(a)!TglTransfer, "yyyy-MM-dd")
            SQL = "delete t_TransferBarang where idrec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        Next

        SQL = "Select * from " & dBase & ".dbo.t_TransferBarang order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec
            If a = 0 Then
                tgl1 = Format(dbTable.Rows(a)!TglTransfer, "yyyy-MM-dd")
            End If
            tgl2 = Format(dbTable.Rows(a)!TglTransfer, "yyyy-MM-dd")

            SQL = "INSERT INTO t_TransferBarang (IDRec, TglTransfer, NoUrut, KodeBrg, NamaBrg, " &
                " QTY, Satuan, Harga, SubTotal, Kode_Toko, UserID, AktifYN, LastUPD, TransferYN, " &
                " Keterangan) VALUES ( '" & dbTable.Rows(a)!IDRec & "', " &
                " '" & Format(dbTable.Rows(a)!TglTransfer, "yyyy-MM-dd") & "', " &
                " '" & dbTable.Rows(a)!NoUrut & "', '" & dbTable.Rows(a)!KodeBrg & "', " &
                " '" & Replace(dbTable.Rows(a)!NamaBrg, "'", "`") & "', " & dbTable.Rows(a)!QTY & ", " &
                " '" & dbTable.Rows(a)!satuan & "', " & dbTable.Rows(a)!Harga & ", " &
                "  " & dbTable.Rows(a)!SubTotal & ", '" & dbTable.Rows(a)!kode_Toko & "', " &
                " '" & dbTable.Rows(a)!UserID & "',  '" & dbTable.Rows(a)!AktifYN & "'," &
                " '" & dbTable.Rows(a)!lastupd & "', 'Y', '" & dbTable.Rows(a)!Keterangan & "' )"
            Proses.ExecuteNonQuery(SQL)
        Next (a)

        idRecord.Text = Proses.GetMaxId("m_LogFile", "idrec", KodeTokoAsal.Text & "L")
        SQL = "Insert into m_LogFile (idrec, tgl1, tgl2, NamaFile, JenisTR, Userid, LastUpd) Values " &
            "('" & idRecord.Text & "', '" & tgl1 & "', '" & tgl2 & "', '" & NamaFile.Text & "', 'Terima Transfer Barang', " &
            "'" & UserID & "', getDate() ) "
        Proses.ExecuteNonQuery(SQL)

    End Sub
    Private Sub ReturTokoKePusat(dBase As String)
        Dim tgl1 As String = "", tgl2 As String = ""
        jenisData.Text = "Proses Synchronize Data Retur dari Toko Ke Pusat "

        Dim JumRec As Double = 0, dbCek As New DataTable
        SQL = "Select nofaktur from " & dBase & ".dbo.t_ReturH group by nofaktur "
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            SQL = "SELECT count(idRec) JumRec FROM t_ReturH WHERE nofaktur = '" & dbTable.Rows(a) !nofaktur & "' "
            JumRec = Proses.ExecuteSingleDblQuery(SQL)
            If JumRec > 1 Then
                SQL = "SELECT idrec FROM t_ReturH WHERE nofaktur = '" & dbTable.Rows(a) !nofaktur & "' "
                dbCek = Proses.ExecuteQuery(SQL)
                For i = 0 To dbCek.Rows.Count - 1
                    idRecord.Text = dbCek.Rows(i) !IDRec
                    SQL = "delete t_ReturH where idrec = '" & idRecord.Text & "' "
                    Proses.ExecuteNonQuery(SQL)
                    SQL = "delete t_ReturD where id_rec = '" & idRecord.Text & "' "
                    Proses.ExecuteNonQuery(SQL)
                Next i
            End If
        Next

        idRecord.Text = ""
        SQL = "Select idrec from " & dBase & ".dbo.t_ReturH group by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a) !IDRec
            SQL = "delete t_ReturH where idrec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)
            SQL = "delete t_ReturD where id_rec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        Next


        SQL = "Select * from " & dBase & ".dbo.t_ReturH order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec
            If a = 0 Then
                tgl1 = Format(dbTable.Rows(a)!TglRetur, "yyyy-MM-dd")
            End If
            tgl2 = Format(dbTable.Rows(a)!TglRetur, "yyyy-MM-dd")


            SQL = "INSERT INTO T_ReturH (IDRec, TglRetur, JenisRetur, NoFaktur, idTokoAsal, idTokoTujuan, " &
                "Keterangan, Total, Status, TransferYN, AktifYN, Kode_Toko, UserID, LastUPD) " &
                "values ('" & dbTable.Rows(a)!IDRec & "',  '" & Format(dbTable.Rows(a)!TglRetur, "yyyy-MM-dd") & "',  " &
                "'" & dbTable.Rows(a)!jenisRetur & "', '" & dbTable.Rows(a)!NoFaktur & "', " &
                "'" & dbTable.Rows(a)!idTokoAsal & "', '" & dbTable.Rows(a)!idTokoTujuan & "', " &
                "'" & Replace(Trim(dbTable.Rows(a)!Keterangan), "'", "`") & "', " & dbTable.Rows(a)!Total & ", " &
                "'" & dbTable.Rows(a)!status & "', 'Y', '" & dbTable.Rows(a)!aktifYN & "', " &
                "'" & dbTable.Rows(a)!Kode_Toko & "', '" & dbTable.Rows(a)!UserID & "', '" & dbTable.Rows(a)!lastupd & "')"
            Proses.ExecuteNonQuery(SQL)


        Next (a)

        SQL = "Select * from " & dBase & ".dbo.t_ReturD order by id_rec"
        dbTable = Proses.ExecuteQuery(SQL)
        jenisData.Text = "Proses Synchronize Data Detail Retur Toko ke Pusat "
        idRecord.Text = ""
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!id_rec & " " & dbTable.Rows(a)!kodebrg

            SQL = "INSERT INTO T_ReturD (ID_ReC, NoUrut, KodeBrg, NamaBrg, QTY, JenisBrg, Satuan, " &
                    " Harga, SubTotal, UserID, TransferYN, AktifYN, LastUPD, Kode_toko, QTYTerima) " &
                    " VALUES ( '" & dbTable.Rows(a)!ID_Rec & "', '" & dbTable.Rows(a)!NoUrut & "', " &
                    "'" & dbTable.Rows(a)!KodeBrg & "', '" & dbTable.Rows(a)!namabrg & "', " &
                    " " & dbTable.Rows(a)!QTY * 1 & ", '" & dbTable.Rows(a)!JenisBrg & "', " &
                    "'" & dbTable.Rows(a)!Satuan & "', " & dbTable.Rows(a)!Harga * 1 & ", " &
                    " " & dbTable.Rows(a)!SubTotal * 1 & ", '" & dbTable.Rows(a)!UserID & "', 'Y',   " &
                    "'" & dbTable.Rows(a)!AktifYN & "', '" & dbTable.Rows(a)!lastupd & "', " &
                    "'" & dbTable.Rows(a)!Kode_Toko & "', " & dbTable.Rows(a)!QTYTerima & ")"
            Proses.ExecuteNonQuery(SQL)
        Next (a)
        idRecord.Text = Proses.GetMaxId("m_LogFile", "idrec", kodeTokoAsal.Text & "L")
        SQL = "Insert into m_LogFile (idrec, tgl1, tgl2, NamaFile, JenisTR, Userid, LastUpd) Values " &
            "('" & idRecord.Text & "', '" & tgl1 & "', '" & tgl2 & "', '" & NamaFile.Text & "', 'Terima Retur Toko ke Pusat', " &
            "'" & UserID & "', getDate() ) "
        Proses.ExecuteNonQuery(SQL)
    End Sub

    Private Sub Kasir(dbase As String)
        Dim tgl1 As String = "", tgl2 As String = ""
        jenisData.Text = "Proses data Synchronize Penjualan Kasir "
        SQL = "Select * from " & dbase & ".dbo.t_KasirH order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec
            If a = 0 Then
                tgl1 = Format(dbTable.Rows(a)!TglPenjualan, "yyyy-MM-dd")
            End If
            tgl2 = Format(dbTable.Rows(a)!TglPenjualan, "yyyy-MM-dd")
            SQL = "delete t_KasirH where idrec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)

            SQL = "INSERT INTO dbo.t_KasirH (IdRec,TglPenjualan, IdComputer, IdCust, NoNota, CaraBayar, SubTotal, " &
                "PsDisc, Disc, PPN, PsPPN, Pembulatan, TotalSales, Tunai, NoKartuDebet, BayarDebet, NoKartuKredit, " &
                "BayarKredit, PsCharge, Charge, TBayarKredit, IdVoucher, Voucher, VcBayar, TotalBayar, Kembali, AktifYN, " &
                "LastUPD, UserID, TransferYN, Kode_Toko, NoRetur, NilaiRetur) VALUES ( '" & dbTable.Rows(a)!IdRec & "', " &
                "'" & Format(dbTable.Rows(a)!TglPenjualan, "yyyy-MM-dd") & "', '" & dbTable.Rows(a)!IdComputer & "', " &
                "'" & dbTable.Rows(a)!idcust & "', '" & dbTable.Rows(a)!NoNota & "', '" & dbTable.Rows(a)!CaraBayar & "', " &
                " " & dbTable.Rows(a)!SubTotal & ", " & dbTable.Rows(a)!PsDisc & ", " & dbTable.Rows(a)!Disc & ", " &
                " " & dbTable.Rows(a)!PPN & ", " & dbTable.Rows(a)!PsPPN & ", " & dbTable.Rows(a)!Pembulatan & ", " &
                " " & dbTable.Rows(a)!TotalSales & ", " & dbTable.Rows(a)!Tunai & ", '" & dbTable.Rows(a)!NoKartuDebet & "', " &
                " " & dbTable.Rows(a)!BayarDebet & ", '" & dbTable.Rows(a)!NoKartuKredit & "',  " & dbTable.Rows(a)!BayarKredit & ", " &
                " " & dbTable.Rows(a)!PsCharge & ", " & dbTable.Rows(a)!Charge & ", " & dbTable.Rows(a)!TBayarKredit & ", " &
                "'" & dbTable.Rows(a)!IdVoucher & "', " & dbTable.Rows(a)!Voucher & ",  " & dbTable.Rows(a)!vcBayar & ", " &
                " " & dbTable.Rows(a)!TotalBayar & ", " & dbTable.Rows(a)!Kembali & ", '" & dbTable.Rows(a)!aktifYN & "', " &
                "'" & dbTable.Rows(a)!LastUPD & "', '" & dbTable.Rows(a)!userid & "', 'Y', '" & dbTable.Rows(a)!Kode_Toko & "', " &
                "'" & dbTable.Rows(a)!noretur & "', " & dbTable.Rows(a)!NilaiRetur & ") "
            Proses.ExecuteNonQuery(SQL)

            SQL = "delete t_KasirD where id_rec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        Next (a)

        SQL = "Select * from " & dbase & ".dbo.t_KasirD order by id_rec, kodebrg "
        dbTable = Proses.ExecuteQuery(SQL)
        jenisData.Text = "Proses Synchronize Data Detail Kasir "
        idRecord.Text = ""
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!id_rec & " " & dbTable.Rows(a)!kodebrg
            SQL = "INSERT INTO dbo.t_KasirD (Id_Rec, NoUrut ,KodeBrg, Barcode, NamaBrg, QTYB, " &
                "SatB, QTY, satuan, PsDisc1, PsDisc2, PsDisc3, Disc, Adjust, Harga, Sub_Total, " &
                "QTYRetur, HargaModal, AktifYN, LastUpd, UserID, TransferYN, Kode_Toko) values ( " &
                "'" & dbTable.Rows(a)!Id_Rec & "', '" & dbTable.Rows(a)!NoUrut & "', " &
                "'" & dbTable.Rows(a)!KodeBrg & "', '" & dbTable.Rows(a)!barcode & "', " &
                "'" & dbTable.Rows(a)!namabrg & "', " & dbTable.Rows(a)!qtyb & ", '" & dbTable.Rows(a)!satb & "', " &
                "" & dbTable.Rows(a)!qty & ", '" & dbTable.Rows(a)!satuan & "', " & dbTable.Rows(a)!psdisc1 & ", " &
                "" & dbTable.Rows(a)!psdisc2 & ", " & dbTable.Rows(a)!psdisc3 & ", " & dbTable.Rows(a)!Disc & ", " &
                "" & dbTable.Rows(a)!Adjust & ", " & dbTable.Rows(a)!Harga & ", " & dbTable.Rows(a)!Sub_Total & ", " &
                "" & dbTable.Rows(a)!qtyretur & ", " & dbTable.Rows(a)!hargamodal & ", '" & dbTable.Rows(a)!aktifYN & "', " &
                "'" & dbTable.Rows(a)!lastupd & "', '" & dbTable.Rows(a)!UserID & "', 'Y', '" & dbTable.Rows(a)!Kode_Toko & "') "
            Proses.ExecuteNonQuery(SQL)
        Next (a)
        idRecord.Text = Proses.GetMaxId("m_LogFile", "idrec", KodeTokoAsal.Text & "L")
        SQL = "Insert into m_LogFile (idrec, tgl1, tgl2, NamaFile, JenisTR, Userid, LastUpd) Values " &
            "('" & idRecord.Text & "', '" & tgl1 & "', '" & tgl2 & "', '" & NamaFile.Text & "', 'Terima Kasir', " &
            "'" & UserID & "', getDate() ) "
        Proses.ExecuteNonQuery(SQL)
    End Sub
    Private Sub SuratJalan(dbase As String)
        Dim tgl1 As String = "", tgl2 As String = ""
        jenisData.Text = "Proses Synchronize Data Surat Jalan "
        idRecord.Text = ""
        SQL = "Select * from " & dbase & ".dbo.t_SJH order by idrec"
        dbTable = Proses.ExecuteQuery(SQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!idrec
            If a = 0 Then
                tgl1 = Format(dbTable.Rows(a)!tglSJ, "yyyy-MM-dd")
            End If
            tgl2 = Format(dbTable.Rows(a)!tglSJ, "yyyy-MM-dd")
            SQL = "delete t_SJH where idrec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)

            SQL = "INSERT INTO t_SJH (IDRec, TglSJ, TglDO, ID_Customer, Customer, idTokoTujuan, " &
                "idTokoAsal, Keterangan, Total, NoDO, AktifYN, Kode_Toko, UserID, LastUPD, TransferYN) " &
                "values ('" & idRecord.Text & "', '" & dbTable.Rows(a)!tglSJ & "', " &
                "'" & dbTable.Rows(a)!TglDO & "', '" & dbTable.Rows(a)!Id_Customer & "', " &
                "'" & dbTable.Rows(a)!Customer & "', '" & dbTable.Rows(a)!idTokoTujuan & "', " &
                "'" & dbTable.Rows(a)!idTokoAsal & "',  " &
                "'" & Replace(Trim(dbTable.Rows(a)!Keterangan), "'", "`") & "',  " & dbTable.Rows(a)!Total & ", " &
                "'" & dbTable.Rows(a)!NoDO & "', '" & dbTable.Rows(a)!aktifYN & "', '" & dbTable.Rows(a)!Kode_Toko & "', " &
                "'" & dbTable.Rows(a)!UserID & "', '" & dbTable.Rows(a)!lastupd & "', 'Y')"
            Proses.ExecuteNonQuery(SQL)
            SQL = "delete t_SJD where id_rec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        Next (a)

        SQL = "Select * from " & dbase & ".dbo.t_SJD order by id_rec"
        dbTable = Proses.ExecuteQuery(SQL)
        jenisData.Text = "Proses Synchronize Data Detail Surat Jalan "
        idRecord.Text = ""
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            idRecord.Text = dbTable.Rows(a)!id_rec & " " & dbTable.Rows(a)!kodebrg
            'If dbTable.Rows(a)!id_rec <> "TG-J180043" Then
            '    MsgBox ("a")
            'End If
            SQL = "INSERT INTO t_SJD (ID_ReC, NoUrut, KodeBrg, NamaBrg, QTY, Satuan, " &
                " Harga, PsDisc, Disc, Netto, SubTotal, UserID, AktifYN, LastUPD, " &
                " Kode_toko, TransferYN) VALUES ( '" & dbTable.Rows(a)!Id_Rec & "', " &
                "'" & dbTable.Rows(a)!NoUrut & "', '" & dbTable.Rows(a)!KodeBrg & "', " &
                " '" & dbTable.Rows(a)!NamaBrg & "', " & dbTable.Rows(a)!QTY & ", " &
                "'" & dbTable.Rows(a)!satuan & "', " & dbTable.Rows(a)!Harga & ", " &
                " " & dbTable.Rows(a)!PsDisc & ", " & dbTable.Rows(a)!Disc & ", " &
                " " & dbTable.Rows(a)!Netto & ", " & dbTable.Rows(a)!SubTotal * 1 & ", " &
                "'" & dbTable.Rows(a)!UserID & "',  '" & dbTable.Rows(a)!AktifYN & "'," &
                "'" & dbTable.Rows(a)!lastupd & "', '" & dbTable.Rows(a)!Kode_Toko & "', 'Y') "
            Proses.ExecuteNonQuery(SQL)
        Next (a)
        idRecord.Text = Proses.GetMaxId("m_LogFile", "idrec", KodeTokoAsal.Text & "L")
        SQL = "Insert into m_LogFile (idrec, tgl1, tgl2, NamaFile, JenisTR, Userid, LastUpd) Values " &
            "('" & idRecord.Text & "', '" & tgl1 & "', '" & tgl2 & "', '" & NamaFile.Text & "', 'Terima SJ', " &
            "'" & UserID & "', GetDate() ) "
        Proses.ExecuteNonQuery(SQL)
    End Sub

    Private Sub cariFolder_Click(sender As Object, e As EventArgs) Handles cariFolder.Click
        'If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
        '    locFile.Text = FolderBrowserDialog1.SelectedPath
        'End If
        Dim tNamaFile As String = ""
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        If Trim(NamaFile.Text) = "" Then
            openFileDialog1.InitialDirectory = My.Settings.LokasiDownload
        Else
            openFileDialog1.InitialDirectory = NamaFile.Text
        End If
        If FrmMenuUtama.Kode_Toko.Text = "PKT01" Then
            openFileDialog1.Filter = "backup files (*.bak)|PKT02*.bak|All files (*.*)|*.*"
        Else
            openFileDialog1.Filter = "backup files (*.bak)|PKT01*.bak|All files (*.*)|*.*"
        End If

        openFileDialog1.FilterIndex = 0
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    NamaFile.Text = openFileDialog1.FileName.ToString
                    Dim Filename As String = NamaFile.Text
                    Dim FileInfo As New FileInfo(Filename)
                    tNamaFile = FileInfo.Name
                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        End If
        KodeTokoAsal.Text = Mid(tNamaFile, 1, 3) + Mid(tNamaFile, 4, 2)
        Kode_Toko.Text = Mid(tNamaFile, 1, 3) + Mid(tNamaFile, 6, 2)

        SQL = "Select * From m_Toko " &
            "Where idrec = '" & KodeTokoAsal.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            KodeTokoAsal.Text = dbTable.Rows(0) !idrec
            NamaTokoAsal.Text = dbTable.Rows(0) !nama
        Else
            NamaTokoAsal.Text = ""
            MsgBox("File Yang di pilih Salah", vbCritical + vbOKOnly, ".:Warning!")
            Exit Sub
        End If


        SQL = "Select * From m_Toko " &
            "Where idrec = '" & Kode_Toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Kode_Toko.Text = dbTable.Rows(0) !idrec
            NamaToko.Text = dbTable.Rows(0) !nama
        Else
            NamaToko.Text = ""
            MsgBox("File Yang di pilih Salah", vbCritical + vbOKOnly, ".:Warning!")
            Exit Sub
        End If
        Kode_Toko.ReadOnly = True
        KodeTokoAsal.ReadOnly = True

        If FrmMenuUtama.Kode_Toko.Text = KodeTokoAsal.Text Then
            MsgBox("FILE asal tidak boleh sama dengan lokasi saat ini program berada...", vbCritical + vbOKOnly, ".:Warning !")
            cariFolder.Focus()
            Exit Sub
        End If

        If NamaFile.Text <> "" Then
            cmdTerima.Enabled = True
            cmdTerima.Focus()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Form_DataTerima_Load(sender As Object, e As EventArgs) Handles Me.Load
        NamaFile.Text = ""
        KodeTokoAsal.Text = ""
        NamaTokoAsal.Text = ""
        Kode_Toko.Text = ""
        NamaToko.Text = ""
        jenisData.Text = ""
        idRecord.Text = ""
        cmdTerima.Enabled = False

        If Trim(My.Settings.LokasiDownload) = "" Then
            NamaFile.Text = My.Settings.LokasiFile
        Else
            NamaFile.Text = My.Settings.LokasiDownload
        End If
        CekTable()
    End Sub

    Private Sub btnDefaultFolder_Click(sender As Object, e As EventArgs) Handles btnDefaultFolder.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            NamaFile.Text = FolderBrowserDialog1.SelectedPath
            My.Settings.LokasiDownload = NamaFile.Text
        End If
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub CekTable()
        'SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'm_TokoD' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "CREATE Table m_TokoD (
        '         IdToko  varchar(7) NULL,
        '         Id_Toko varchar(7) NULL) "
        '    Proses.ExecuteNonQuery(SQL)
        'End If

        'SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'm_LogFile' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Create Table m_LogFile ( 
        '        idRec varchar(15),
        '        tgl1 datetime,
        '        tgl2 DateTime,
        '        NamaFile varchar(100),
        '     jenistr varchar(50),
        '        userid varchar(20),
        '     lastupd DateTime) "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
    End Sub
End Class