Imports System.Data.SqlClient
Imports System.Data
Imports System.IO.Directory
Imports Microsoft.Office.Interop.Excel 'Before you add this refrence to your project you need to install Microsoft Office and find last version of this file.
Imports Microsoft.Office.Interop

Public Class Form_Export2Excel
    Dim MsgSQL As String = ""
    Dim Proses As New ClsKoneksi
    Private Sub cariFolder_Click(sender As Object, e As EventArgs) Handles cariFolder.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            locFile.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Form_Export2Excel_Load(sender As Object, e As EventArgs) Handles Me.Load
        locFile.Text = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        cmdBatal.Visible = True
        cmdProses.Visible = True
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub cmdProses_Click(sender As Object, e As EventArgs) Handles cmdProses.Click
        PanelTombol.Enabled = False
        If JenisTR.Text = "DPB" Then
            ProsesDPB()
        ElseIf JenisTR.Text = "LHP" Then
            ProsesLHP()
        ElseIf JenisTR.Text = "PL_PackingList" Then
            ProsesPacking_List()
        ElseIf JenisTR.Text = "PL_Invoice" Then
            ProsesPackingList_Invoice()
        End If
        PanelTombol.Enabled = True
    End Sub
    Private Sub ProsesPacking_List()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        oSheet.Cells(4, 2) = "Packing List No " + idRec.Text
        oSheet.Range("B4:B4").Font.Bold = True
        oSheet.Cells(6, 1) = "Box No."
        oSheet.Cells(6, 2) = "Our Code"
        oSheet.Cells(6, 3) = "Your Code"
        oSheet.Cells(6, 4) = "Description"
        oSheet.Cells(6, 5) = "QTY"
        oSheet.Cells(6, 6) = "Material"

        Cursor = Cursors.WaitCursor
        Dim NoUrut As Integer = 1, i As Integer = 8, tQTY As Double = 0,
            mTglPL As Date, mNoPO As String = "", NoBoks As String = "", boxes As String = ""

        MsgSQL = "SELECT t_PackingList.NoBoks1, t_PackingList.NoBoks2, " &
            "t_PackingList.JumlahBoks, t_PackingList.JumlahTiapBoks,  " &
            "t_PackingList.NoPI, t_PackingList.Kode_Produk, t_PackingList.QtyTiapBoks,  " &
            "t_PackingList.NoPO, t_PackingList.KodePImportir, m_KodeProduk.descript,  " &
            "m_KodeBahan.NamaInggris, m_KodeImportir.Nama, m_KodeImportir.Alamat, tglpl, " &
            "t_PackingList.NoPackingList, t_PackingList.HargaFOB,  t_PackingList.CatatanPL " &
            "FROM Pekerti.dbo.t_PackingList t_PackingList " &
            "   INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
            "   t_PackingList.Kode_Importir = m_KodeImportir.KodeImportir  " &
            "   INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON  " &
            "   t_PackingList.Kode_Produk = m_KodeProduk.KodeProduk  " &
            "   INNER JOIN Pekerti.dbo.m_KodeBahan m_KodeBahan ON  " &
            "m_KodeProduk.Kode_Bahan = m_KodeBahan.KodeBahan  " &
            "Where t_PackingList.NoPackingList = '" & idRec.Text & "' " &
            "Order By right('0000000000'+noboks1,10), right('0000000000' + noboks2, 10), idrec  "
        dbTable = Proses.ExecuteQuery(MsgSQL)

        If dbTable.Rows.Count <> 0 Then
            oSheet.Cells(1, 1) = dbTable.Rows(0) !Nama
            oSheet.Cells(2, 1) = dbTable.Rows(0) !Alamat.Replace(vbCrLf, "")
            oSheet.Range("A1:F1").Merge()
            oSheet.Range("A2:F2").Merge()
            oSheet.Range("A1:F2").Font.Bold = True
            mTglPL = dbTable.Rows(0) !TglPL
        End If

        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1

            If mNoPO <> dbTable.Rows(a) !NoPO Then
                oSheet.Cells(i, 2) = "No. PO : " + IIf(dbTable.Rows(a) !NoPO = "", "PEKERTI", dbTable.Rows(a) !NoPO)
                i = i + 1
            End If

            If boxes <> Trim(Str(dbTable.Rows(a) !JumlahBoks)) Then
                boxes = IIf(dbTable.Rows(a) !JumlahBoks = 1, "",
                    "(" + Format(dbTable.Rows(a) !JumlahBoks, "##0") +
                    " boxes / " + Format(dbTable.Rows(a) !JumlahTiapBoks, "##0") + " pcs each)")
                If boxes <> "" Then
                    oSheet.Cells(i, 1) = boxes
                    i = i + 1
                End If
            End If
            NoBoks = Trim(dbTable.Rows(a) !NoBoks1) + " - " + Trim(dbTable.Rows(a) !NoBoks2)
            oSheet.Cells(i, 1) = "'" + NoBoks
            oSheet.Cells(i, 2) = dbTable.Rows(a) !Kode_Produk
            oSheet.Cells(i, 3) = dbTable.Rows(a) !KodePImportir
            oSheet.Cells(i, 4) = dbTable.Rows(a) !Descript
            oSheet.Cells(i, 5) = Format(dbTable.Rows(a) !JumlahBoks * dbTable.Rows(a) !JumlahTiapBoks, "###,##0.00")
            oSheet.Cells(i, 6) = dbTable.Rows(a) !NamaInggris
            tQTY = tQTY + (dbTable.Rows(a) !JumlahBoks * dbTable.Rows(a) !JumlahTiapBoks)
            mNoPO = dbTable.Rows(a) !NoPO
            i += 1
        Next (a)

        i = i + 2
        oSheet.Cells(i, 4) = "Total :"
        oSheet.Cells(i, 4).HorizontalAlignment = Excel.Constants.xlRight
        oSheet.Cells(i, 5) = Format(tQTY, "###,##0")
        i = i + 2
        oSheet.Cells(i, 6) = "Jakarta, " & Format(mTglPL, "dd MMMM yyyy")
        'Dim mRange As String = "E" + Format(i, "##0") + ":F" + Format(i, "##0")
        'oSheet.Range(mRange).Merge()
        i = i + 6
        oSheet.Cells(i, 6) = "RUDIONO"
        i = i + 1
        oSheet.Cells(i, 6) = "EXPORT DEPT"

        Dim fileName As String = locFile.Text & "\PL_" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"
        'oSheet.Range("A1:L3").Font.Bold = True
        'oSheet.Range("A1:D1").Merge()
        oSheet.Columns.AutoFit()
        oBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        MsgBox("File Berhasil di simpan di : " & fileName, vbInformation + vbOKOnly, ".:Information ")
        Cursor = Cursors.Default
    End Sub

    Private Sub ProsesPackingList_Invoice()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)
        Dim tTotal As Double = 0
        MsgSQL = "Select Sum (JumlahBoks * JumlahTiapBoks * HargaFOB) SubTotal " &
            " From t_PackingList " &
            "Where t_PackingList.NoPackingList = '" & idRec.Text & "' "
        tTotal = Proses.ExecuteSingleDblQuery(MsgSQL)

        oSheet.Cells(4, 2) = "Packing List No " + idRec.Text
        oSheet.Range("B4:B4").Font.Bold = True

        oSheet.Cells(6, 1) = "No."
        oSheet.Cells(6, 2) = "Our Code"
        oSheet.Cells(6, 3) = "Your Code"
        oSheet.Cells(6, 4) = "Description"
        oSheet.Cells(6, 5) = "QTY"
        oSheet.Cells(6, 6) = "FOB Price (USD)"
        oSheet.Cells(7, 6) = "Unit"
        oSheet.Cells(7, 7) = "Total"
        oSheet.Range("F6:G6").Merge()
        oSheet.Cells(6, 6).HorizontalAlignment = Excel.Constants.xlCenter
        Cursor = Cursors.WaitCursor

        Dim NoUrut As Integer = 1, i As Integer = 8, tQTY As Double = 0, mSubTot As Double = 0,
            mTglPL As Date, mNoPO As String = "", NoBoks As String = "", boxes As String = ""

        MsgSQL = "SELECT Kode_Produk, NoPO, KodePImportir, Descript, t_PackingList.HargaFOB, " &
            " Sum(t_PackingList.JumlahBoks * t_PackingList.JumlahTiapBoks) as QTY, " &
            " Sum(t_PackingList.HargaFOB * t_PackingList.JumlahBoks * t_PackingList.JumlahTiapBoks) as SubTot, " &
            " m_KodeImportir.Nama, m_KodeImportir.Alamat, TglPL " &
            "FROM Pekerti.dbo.t_PackingList t_PackingList INNER JOIN Pekerti.dbo.m_KodeImportir m_KodeImportir ON " &
            "     t_PackingList.Kode_Importir = m_KodeImportir.KodeImportir  " &
            "     INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON t_PackingList.Kode_Produk = m_KodeProduk.KodeProduk  " &
            "Where t_PackingList.NoPackingList = '" & idRec.Text & "' " &
            "Group By t_PackingList.NoPO, t_PackingList.FotoLoc, t_PackingList.Kode_Produk, HargaFOB, " &
            "         KodePImportir, Descript,  m_KodeImportir.Nama, m_KodeImportir.Alamat, TglPL  " &
            "Order By t_PackingList.NoPO, t_PackingList.FotoLoc, t_PackingList.Kode_Produk "

        dbTable = Proses.ExecuteQuery(MsgSQL)
        If dbTable.Rows.Count <> 0 Then
            oSheet.Cells(1, 1) = dbTable.Rows(0) !Nama
            oSheet.Cells(2, 1) = dbTable.Rows(0) !Alamat.Replace(vbCrLf, "")
            oSheet.Range("A1:F1").Merge()
            oSheet.Range("A2:F2").Merge()
            oSheet.Range("A1:F2").Font.Bold = True
            mTglPL = dbTable.Rows(0) !TglPL
        End If

        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1
            If mNoPO <> dbTable.Rows(a) !NoPO Then
                If mNoPO <> "" Then
                    oSheet.Cells(i, 6) = "Sub Total PO No : " & mNoPO & " : "
                    oSheet.Cells(i, 7) = Format(mSubTot, "###,##0.00")
                    i = i + 1
                End If
                oSheet.Cells(i, 2) = "No. PO : " + IIf(dbTable.Rows(a) !NoPO = "", "PEKERTI", dbTable.Rows(a) !NoPO)
                i = i + 1
                mSubTot = 0
            End If

            oSheet.Cells(i, 1) = Format(NoUrut, "###,##0")
            oSheet.Cells(i, 2) = dbTable.Rows(a) !Kode_Produk
            oSheet.Cells(i, 3) = dbTable.Rows(a) !KodePImportir
            oSheet.Cells(i, 4) = dbTable.Rows(a) !Descript
            oSheet.Cells(i, 5) = Format(dbTable.Rows(a) !qty, "###,##0.00")
            oSheet.Cells(i, 6) = Format(dbTable.Rows(a) !HargaFOB, "###,##0.00")
            oSheet.Cells(i, 7) = Format(dbTable.Rows(a) !SubTot, "###,##0.00")
            mSubTot = mSubTot + dbTable.Rows(a) !SubTot
            mNoPO = dbTable.Rows(a) !NoPO
            If a = dbTable.Rows.Count - 1 Then
                i += 1
                oSheet.Cells(i, 6) = "Sub Total PO No : " & mNoPO & " : "
                oSheet.Cells(i, 6).HorizontalAlignment = Excel.Constants.xlRight
                oSheet.Cells(i, 7) = Format(mSubTot, "###,##0.00")
            End If
            i += 1
            NoUrut += 1
        Next (a)

        i = i + 1
        oSheet.Cells(i, 6) = "Total :"
        oSheet.Cells(i, 6).HorizontalAlignment = Excel.Constants.xlRight
        oSheet.Cells(i, 7) = Format(tTotal, "###,##0")
        i = i + 2
        oSheet.Cells(i, 6) = "Jakarta, " & Format(mTglPL, "dd MMMM yyyy")
        i = i + 6
        oSheet.Cells(i, 6) = "RUDIONO"
        i = i + 1
        oSheet.Cells(i, 6) = "EXPORT DEPT"

        Dim fileName As String = locFile.Text & "\PLi_" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"
        'oSheet.Range("A1:L3").Font.Bold = True
        'oSheet.Range("A1:D1").Merge()
        oSheet.Columns.AutoFit()
        oBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        MsgBox("File Berhasil di simpan di : " & fileName, vbInformation + vbOKOnly, ".:Information ")
        Cursor = Cursors.Default
    End Sub

    Private Sub ProsesDPB()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        'oSheet.Cells(1, 1) = "1,1"
        'oSheet.Cells(1, 2) = "1,2"
        'oSheet.Cells(1, 3) = "1,3"
        'oSheet.Cells(2, 1) = "2,1"
        'oSheet.Cells(3, 1) = "3,1"
        'oSheet.Cells(4, 1) = "4,1"
        oSheet.Cells(1, 1) = "DAFTAR PENERIMAAN BARANG"
        oSheet.Cells(3, 1) = "No. DPB "
        oSheet.Cells(3, 2) = "Perajin"
        oSheet.Cells(3, 3) = "LHP"
        oSheet.Cells(3, 4) = "No SP"
        oSheet.Cells(3, 5) = "Kode Produk"
        oSheet.Cells(3, 6) = "Nama Barang"
        oSheet.Cells(3, 7) = "Jumlah"
        oSheet.Cells(3, 8) = "Harga"
        oSheet.Cells(3, 9) = "Sub Total"
        oSheet.Cells(3, 10) = "Terima"
        oSheet.Cells(3, 11) = "Batas"
        Cursor = Cursors.WaitCursor
        Dim NoUrut As Integer = 1,
            i As Integer = 4,
            Perajin As String = ""

        MsgSQL = "SELECT t_DPB.NoDPB, t_DPB.TglDPB, t_DPB.NoSP, t_DPB.NoLHP, NoSP, " &
            " t_DPB.Kode_Produk, t_DPB.NamaProduk, t_DPB.KodePerajin, KodePerajin2, " &
            " t_DPB.Jumlah, t_DPB.HargaBeli, t_DPB.DeadlineSP, t_DPB.Pengirim, " &
            " t_DPB.TglTerima , t_DPB.tglCetak, m_KodeProduk.Satuan, OngKir " &
            " FROM Pekerti.dbo.t_DPB t_DPB INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk ON " &
            "    t_DPB.Kode_Produk = m_KodeProduk.KodeProduk " &
            "Where t_DPB.NoDPB = '" & idRec.Text & "' and t_DPB.Jumlah <> 0 " &
            "  And t_DPB.AktifYN = 'Y' order by t_DPB.NoSP, t_DPB.NoLHP, " &
            " t_DPB.IDREC, t_DPB.KODE_PRODUK "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1
            oSheet.Range("A" + Format(i, "##0")).Value = dbTable.Rows(a) !nodpb
            MsgSQL = "Select Nama From M_KodePerajin " &
                "Where aktifYN = 'Y' " &
                "  And KodePerajin = '" & Microsoft.VisualBasic.Left(dbTable.Rows(a) !Kode_Produk, 4) & "' "
            Perajin = Proses.ExecuteSingleStrQuery(MsgSQL)
            oSheet.Range("B" + Format(i, "##0")).Value = Perajin
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !NoLHP
            oSheet.Range("D" + Format(i, "##0")).Value = IIf(Trim(dbTable.Rows(a) !NoSP) = "", "PEKERTI", dbTable.Rows(a) !NoSP)
            oSheet.Range("E" + Format(i, "##0")).Value = "'" + dbTable.Rows(a) !Kode_Produk
            oSheet.Range("F" + Format(i, "##0")).Value = Trim(dbTable.Rows(a) !NamaProduk).Replace(Environment.NewLine, String.Empty)
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Jumlah, "###,##0")
            oSheet.Range("H" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !HargaBeli, "###,##0.000")
            oSheet.Range("I" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Jumlah * dbTable.Rows(a) !HargaBeli, "###,##0.000")
            oSheet.Range("J" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !tglTerima, "dd-MM-yyyy")
            oSheet.Range("K" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !DeadlineSP, "dd-MM-yyyy")
            i += 1
            NoUrut = NoUrut + 1
        Next (a)

        Dim fileName As String = locFile.Text & "\DPB" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"
        oSheet.Range("A1:L3").Font.Bold = True
        oSheet.Range("A1:D1").Merge()
        oSheet.Columns.AutoFit()
        oBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        MsgBox("File Berhasil di simpan di : " & fileName, vbInformation + vbOKOnly, ".:Information ")
        Cursor = Cursors.Default
    End Sub
    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Private Sub ProsesLHP()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)
        oSheet.Cells(1, 1) = "LAPORAN HASIL PEMERIKSAAN"
        oSheet.Cells(3, 1) = "No LHP"
        oSheet.Cells(3, 2) = "No Pra LHP"
        oSheet.Cells(3, 3) = "Perajin"
        oSheet.Cells(3, 4) = "SP"
        oSheet.Cells(3, 5) = "Kode Produk"
        oSheet.Cells(3, 6) = "Produk"
        oSheet.Cells(3, 7) = "Menurut SP"
        oSheet.Cells(3, 8) = "Tercantum di SPB"
        oSheet.Cells(3, 9) = "Jumlah yang Ada"
        oSheet.Cells(3, 10) = "Jumlah yang Baik"
        oSheet.Cells(3, 11) = "Jumlah Retur"
        oSheet.Cells(3, 12) = "Keterangan"
        oSheet.Cells(3, 13) = "Mulai Periksa"
        oSheet.Cells(3, 14) = "Selesai Periksa"

        Cursor = Cursors.WaitCursor
        Dim i As Integer = 4,
            Perajin As String = ""

        MsgSQL = "SELECT Distinct t_LHP.IDRec, t_LHP.NoLHP, t_LHP.NoPraLHP, t_LHP.Kode_Produk, " &
            "    t_LHP.Produk, t_LHP.JumlahPack, t_LHP.Kirim, t_LHP.JumlahHitung, t_PraLHP.NamaPerajin, " &
            "    t_LHP.JumlahBaik, t_LHP.JumlahTolak, t_LHP.Pemeriksa, t_LHP.TglMulaiPeriksa,  " &
            "    t_LHP.TglSelesaiPeriksa, t_LHP.Koordinator, t_LHP.Keterangan, t_LHP.NoSP, t_LHP.TglTerima,  " &
            "    t_PraLHP.Kargo, t_PraLHP.SuratPengantar, t_PraLHP.JumlahKoli, AlasanDiTolak  " &
            "FROM Pekerti.dbo.t_LHP t_LHP INNER JOIN Pekerti.dbo.t_PraLHP t_PraLHP ON  " &
            "        t_LHP.NoPraLHP = t_PraLHP.NoPraLHP AND t_LHP.NoSP = t_PraLHP.NoSP AND  " &
            "        t_LHP.Kode_Produk = t_PraLHP.Kode_Produk " &
            "Where t_LHP.NoLHP = '" & idRec.Text & "' " &
            "  And t_LHP.AktifYN = 'Y' And t_PraLHP.AktifYN = 'Y' " &
            "ORDER BY t_LHP.NoSP, t_LHP.IDRec ASC "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1
            oSheet.Range("A" + Format(i, "##0")).Value = dbTable.Rows(a) !NoLHP
            oSheet.Range("B" + Format(i, "##0")).Value = IIf(Trim(dbTable.Rows(a) !NoPraLHP) = "", "PEKERTI", dbTable.Rows(a) !NoPraLHP)
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !NamaPerajin
            oSheet.Range("D" + Format(i, "##0")).Value = IIf(Trim(dbTable.Rows(a) !NoSP) = "", "PEKERTI", dbTable.Rows(a) !NoSP)
            oSheet.Range("E" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Produk
            oSheet.Range("F" + Format(i, "##0")).Value = dbTable.Rows(a) !Produk
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahPack, "###,##0.000")
            oSheet.Range("H" + Format(i, "##0")).Value = dbTable.Rows(a) !kirim
            oSheet.Range("I" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahHitung, "###,##0.000")
            oSheet.Range("J" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !jumlahbaik, "###,##0.000")
            oSheet.Range("K" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !jumlahtolak, "###,##0.000")
            oSheet.Range("L" + Format(i, "##0")).Value = dbTable.Rows(a) !AlasanDiTolak
            oSheet.Range("M" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !tglMulaiPeriksa, "dd-MM-yyyy")
            oSheet.Range("N" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !TglSelesaiPeriksa, "dd-MM-yyyy")
            i += 1
        Next (a)

        Dim fileName As String = locFile.Text & "\LHP" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"
        oSheet.Range("A1:N3").Font.Bold = True
        oSheet.Range("A1:D1").Merge()
        oSheet.Columns.AutoFit()
        oBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        MsgBox("File Berhasil di simpan di : " & fileName, vbInformation + vbOKOnly, ".:Information ")
        Cursor = Cursors.Default
    End Sub
    Private Sub ProsesLHP_X()
        Dim excel As New Excel.Application
        Dim Proses As New ClsKoneksi
        Dim dbTable As Data.DataTable
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        Dim fileName As String = "C:\LHP_" + Replace(idRec.Text, "/", "-") + "_" + Format(Now, "yymmdd_hhmmss") + ".xls"
        'Dim SubTotal As String = "", Disc As String = "", PPH As String = "", PPN As String = "", Total As String = ""
        Dim NoUrut As Double = 1
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add

        'Add data to cells of the first worksheet in the new workbook    
        oSheet = oBook.Worksheets(1)
        oSheet.Cells(1, 1) = "LAPORAN HASIL PEMERIKSAAN"
        oSheet.Cells(3, 1) = "No LHP"
        oSheet.Cells(3, 2) = "No Pra LHP"
        oSheet.Cells(3, 3) = "Perajin"
        oSheet.Cells(3, 4) = "SP"
        oSheet.Cells(3, 5) = "Kode Produk"
        oSheet.Cells(3, 6) = "Produk"
        oSheet.Cells(3, 7) = "Menurut SP"
        oSheet.Cells(3, 8) = "Tercantum di SPB"
        oSheet.Cells(3, 9) = "Jumlah yang Ada"
        oSheet.Cells(3, 10) = "Jumlah yang Baik"
        oSheet.Cells(3, 11) = "Jumlah Retur"
        oSheet.Cells(3, 12) = "Keterangan"
        oSheet.Cells(3, 13) = "Mulai Periksa"
        oSheet.Cells(3, 14) = "Selesai Periksa"

        Dim i As Integer = 4

        MsgSQL = "SELECT Distinct t_LHP.IDRec, t_LHP.NoLHP, t_LHP.NoPraLHP, t_LHP.Kode_Produk, " &
            "    t_LHP.Produk, t_LHP.JumlahPack, t_LHP.Kirim, t_LHP.JumlahHitung, t_PraLHP.NamaPerajin, " &
            "    t_LHP.JumlahBaik, t_LHP.JumlahTolak, t_LHP.Pemeriksa, t_LHP.TglMulaiPeriksa,  " &
            "    t_LHP.TglSelesaiPeriksa, t_LHP.Koordinator, t_LHP.Keterangan, t_LHP.NoSP, t_LHP.TglTerima,  " &
            "    t_PraLHP.Kargo, t_PraLHP.SuratPengantar, t_PraLHP.JumlahKoli, AlasanDiTolak  " &
            "FROM Pekerti.dbo.t_LHP t_LHP INNER JOIN Pekerti.dbo.t_PraLHP t_PraLHP ON  " &
            "        t_LHP.NoPraLHP = t_PraLHP.NoPraLHP AND t_LHP.NoSP = t_PraLHP.NoSP AND  " &
            "        t_LHP.Kode_Produk = t_PraLHP.Kode_Produk " &
            "Where t_LHP.NoLHP = '" & idRec.Text & "' " &
            "  And t_LHP.AktifYN = 'Y' And t_PraLHP.AktifYN = 'Y' " &
            "ORDER BY t_LHP.NoSP, t_LHP.IDRec ASC "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1

            oSheet.Range("A" + Format(i, "##0")).Value = dbTable.Rows(a) !NoLHP
            oSheet.Range("B" + Format(i, "##0")).Value = IIf(Trim(dbTable.Rows(a) !NoPraLHP) = "", "PEKERTI", dbTable.Rows(a) !NoPraLHP)
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !NamaPerajin
            oSheet.Range("D" + Format(i, "##0")).Value = IIf(Trim(dbTable.Rows(a) !NoSP) = "", "PEKERTI", dbTable.Rows(a) !NoSP)
            oSheet.Range("E" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Produk
            oSheet.Range("F" + Format(i, "##0")).Value = dbTable.Rows(a) !Produk
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahPack, "###,##0.000")
            oSheet.Range("H" + Format(i, "##0")).Value = dbTable.Rows(a) !kirim
            oSheet.Range("I" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahHitung, "###,##0.000")
            oSheet.Range("J" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !jumlahbaik, "###,##0.000")
            oSheet.Range("K" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !jumlahtolak, "###,##0.000")
            oSheet.Range("L" + Format(i, "##0")).Value = dbTable.Rows(a) !AlasanDiTolak
            oSheet.Range("M" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !tglMulaiPeriksa, "dd-MM-yyyy")
            oSheet.Range("N" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !TglSelesaiPeriksa, "dd-MM-yyyy")

            i += 1
            NoUrut = NoUrut + 1
        Next (a)


        oSheet.Columns.AutoFit()
        'oSheet.range("E11: E11").HorizontalAlignment = xlCenter
        'oSheet.range("F11:G11").HorizontalAlignment = xlCenter
        'oSheet.Range("A13").ColumnWidth = 5
        'oSheet.Range("G12").ColumnWidth = 10

        Dim strFileName As String = fileName
        Dim blnFileOpen As Boolean = False
        Try
            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
            fileTemp.Close()
        Catch ex As Exception
            blnFileOpen = False
        End Try
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
        Cursor = Cursors.Default
        oExcel.Visible = True
        Proses.CloseConn()
    End Sub
End Class