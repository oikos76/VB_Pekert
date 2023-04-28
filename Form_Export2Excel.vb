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
        End If
        PanelTombol.Enabled = True
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