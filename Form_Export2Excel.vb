Imports System.Data.SqlClient
Imports System.Data
Imports System.IO.Directory
Imports System.IO
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
        ElseIf JenisTR.Text = "DPL" Then
            prosesDPL
        ElseIf JenisTR.Text = "PRA-LHP" Then
            ProsesPraLHP()
        ElseIf JenisTR.Text = "PL_PackingList" Then
            ProsesPacking_List()
        ElseIf JenisTR.Text = "PL_Invoice" Then
            ProsesPackingList_Invoice()
        ElseIf JenisTR.Text = "SP" Then
            ProsesSP()
        End If
        PanelTombol.Enabled = True
    End Sub

    Private Sub ProsesSP()
        Cursor = Cursors.WaitCursor
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        Dim NoUrut As Double = 1,
            jumlah As Double = 0,
            subTotal As Double = 0,
            produk As String = "",
            fileName As String = locFile.Text & "\SP_" + Replace(idRec.Text, "/", "") + "_" + Format(Now, "yymmdd_hhmmss") + ".xls"

        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add
        'Add data to cells of the first worksheet in the new workbook    
        oSheet = oBook.Worksheets(1)

        oSheet.Cells(1, 1) = "SURAT PESANAN"
        oSheet.Range("A1:G1").Merge()
        oSheet.Range("A1:G1").Font.Size = 14
        oSheet.Cells(1, 1).HorizontalAlignment = Excel.Constants.xlCenter

        oSheet.Cells(2, 1) = "SP No."
        oSheet.Cells(2, 2) = "'" & idRec.Text
        oSheet.Cells(3, 1) = "Tgl.SP"
        oSheet.Cells(4, 1) = "Jenis SP"

        oSheet.Cells(7, 1) = "No."
        oSheet.Cells(7, 2) = "Kode Produk"
        oSheet.Cells(7, 3) = "Nama Produk"
        oSheet.Cells(7, 4) = "Jumlah"
        oSheet.Range("D7:E7").Merge()
        oSheet.Cells(7, 6) = "Harga Satuan"
        oSheet.Cells(7, 7) = "Total"

        Dim i As Integer = 8, panjang As String = "",
            lebar As String = "", tinggi As String = "", PLT As String = "",
            tebal As String = "", diameter As String = ""

        MsgSQL = "SELECT t_SP.IDRec, t_SP.NoSP, t_SP.NoPO, t_SP.Kode_Perajin, " &
            "     t_SP.Perajin, t_SP.KodeProduk, t_SP.Produk, t_SP.KodePerajin, " &
            "     t_SP.Jumlah, t_SP.HargaBeliRP, t_SP.CatatanSP, m_KodeProduk.Panjang, " &
            "     m_KodeProduk.Lebar, m_KodeProduk.Tinggi,  m_KodeProduk.diameter, " &
            "     m_KodeProduk.tebal, m_KodeProduk.Satuan, m_KodeProduk.KodePerajin2, " &
            "     t_SP.Importir, CatatanProduk, CatatanTambahan, CatatanSP, direksi, " &
            "     t_sp.StatusSP, t_SP.tglSP, m_Company.BagianContoh " &
            "FROM Pekerti.dbo.t_SP t_SP INNER JOIN Pekerti.dbo.m_KodeProduk m_KodeProduk On " &
            "   t_SP.KodeProduk = m_KodeProduk.KodeProduk  " &
            " INNER JOIN m_Company on idCompany = CompCode " &
            "Where t_SP.AktifYN = 'Y' " &
            "  And t_SP.NoSP = '" & idRec.Text & "' " &
            "ORDER BY t_SP.IDRec, t_SP.NoPO ASC "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        Cursor = Cursors.WaitCursor
        If dbTable.Rows.Count <> 0 Then
            oSheet.Cells(3, 2) = Format(dbTable.Rows(0) !tglsp, "dd-MMM-yyyy")
            oSheet.Cells(4, 2) = IIf(Mid(dbTable.Rows(0) !NoSP, 5, 1) = "E", "EKSPOR", " ") & " " &
                IIf(dbTable.Rows(0) !StatusSP = "", "", "-" & dbTable.Rows(0) !StatusSP)
        End If
        For a = 0 To dbTable.Rows.Count - 1
            If dbTable.Rows(a) !panjang = 0 Then
                panjang = ""
            Else
                panjang = "p:" & Replace(Format(dbTable.Rows(a) !panjang, "###,##0.00"), ".00", "")
            End If

            If dbTable.Rows(a) !lebar = 0 Then
                lebar = ""
            Else
                lebar = " l:" & Replace(Format(dbTable.Rows(a) !lebar, "###,##0.00"), ".00", "")
            End If

            If dbTable.Rows(a) !tinggi = 0 Then
                tinggi = ""
            Else
                tinggi = " t:" & Replace(Format(dbTable.Rows(a) !tinggi, "###,##0.00"), ".00", "")
            End If

            If dbTable.Rows(a) !diameter = 0 Then
                diameter = ""
            Else
                diameter = " d:" & Replace(Format(dbTable.Rows(a) !diameter, "###,##0.00"), ".00", "")
            End If

            If dbTable.Rows(a) !tebal = 0 Then
                tebal = ""
            Else
                tebal = " tbl:" & Replace(Format(dbTable.Rows(a) !tebal, "###,##0.00"), ".00", "")
            End If

            If (dbTable.Rows(a) !panjang = 0 And dbTable.Rows(a) !lebar = 0 _
               And dbTable.Rows(a) !tinggi = 0) Then
                PLT = ""
            Else
                PLT = "(" + panjang + lebar + tinggi + diameter + tebal + ")"
            End If

            produk = dbTable.Rows(a) !Produk & vbCrLf & PLT


            oSheet.Range("A" + Format(i, "##0")).Value = NoUrut
            oSheet.Range("C" + Format(i, "##0")).Value = "PO : " + IIf(dbTable.Rows(a) !NoPO = "", "PEKERTI", dbTable.Rows(a) !NoPO) + " - " +
                Trim(dbTable.Rows(a) !Importir)
            i += 1
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !KodeProduk
            oSheet.Range("C" + Format(i, "##0")).Value = produk
            oSheet.Range("D" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Jumlah, "###,##0")
            oSheet.Range("E" + Format(i, "##0")).Value = "'" + dbTable.Rows(a) !satuan
            oSheet.Range("F" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !HargaBeliRP, "###,##0")
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Jumlah * dbTable.Rows(a) !HargaBeliRP, "###,##0")
            i += 1
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(0) !kodeperajin2
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !catatanProduk
            i += 1
            NoUrut = NoUrut + 1
            jumlah += dbTable.Rows(a) !Jumlah
            subTotal += (dbTable.Rows(a) !Jumlah * dbTable.Rows(a) !HargaBeliRP)
        Next (a)
        oSheet.Cells(i, 3) = "Total :"
        oSheet.Cells(i, 3).HorizontalAlignment = Excel.Constants.xlRight
        oSheet.Cells(i, 4) = Format(jumlah, "###,##0")
        oSheet.Cells(i, 7) = Format(subTotal, "###,##0")
        oSheet.Columns.AutoFit()
        oSheet.Range("C7").ColumnWidth = 50
        oBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        MsgBox("File Berhasil di simpan di : " & fileName, vbInformation + vbOKOnly, ".:Information ")
        Cursor = Cursors.Default
        Proses.CloseConn()
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
            oSheet.Cells(i, 2) = "'" + dbTable.Rows(a) !Kode_Produk
            oSheet.Cells(i, 3) = "'" + dbTable.Rows(a) !KodePImportir
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
        oSheet.Cells(i, 6) = "___________"
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
        Dim tTotal As Double = 0, tTotalQTY As Double = 0
        MsgSQL = "Select Sum (JumlahBoks * JumlahTiapBoks * HargaFOB) SubTotal " &
            " From t_PackingList " &
            "Where t_PackingList.NoPackingList = '" & idRec.Text & "' "
        tTotal = Proses.ExecuteSingleDblQuery(MsgSQL)

        Dim logoFile As String = My.Application.Info.DirectoryPath.ToString + "\biaozhi_p.jpg"

        'oSheet.Shapes.AddPicture("C:\Sample Pictures\p.jpg", False, True, 0, 0, 45, 50)


        oSheet.Cells(1, 3) = "PEKERTI NUSANTARA PT."
        oSheet.Cells(2, 3) = "JL. RAYA BEKASI TIMUR"
        oSheet.Cells(3, 3) = "KM 17. NO. 19A, JATINEGARA - CAKUNG"
        oSheet.Cells(4, 3) = "JAKARTA TIMUR 13930"
        oSheet.Cells(5, 3) = "INDONESIA"
        oSheet.Cells(6, 3) = "PH. 62 21 460 0836  FAX 62-21 460 0836"
        oSheet.Cells(7, 1) = "pekerti"
        oSheet.Range("A7:B7").Merge()

        oSheet.Cells(9, 1) = "Invoice No:"
        oSheet.Cells(10, 1) = idRec.Text
        oSheet.Range("A9:G9").Merge()
        oSheet.Range("A10:G10").Merge()

        oSheet.Cells(9, 1).HorizontalAlignment = Excel.Constants.xlCenter
        oSheet.Cells(10, 1).HorizontalAlignment = Excel.Constants.xlCenter

        oSheet.Cells(11, 1) = "No."
        oSheet.Range("A11:A12").Merge()
        oSheet.Cells(11, 2) = "Our Code"
        oSheet.Range("B11:B12").Merge()
        oSheet.Cells(11, 3) = "Your Code"
        oSheet.Range("C11:C12").Merge()
        oSheet.Cells(11, 4) = "Description"
        oSheet.Range("D11:D12").Merge()
        oSheet.Cells(11, 5) = "QTY"
        oSheet.Range("E11:E12").Merge()
        oSheet.Cells(11, 6) = "FOB Price (USD)"
        oSheet.Range("F11:G11").Merge()
        oSheet.Cells(11, 6).HorizontalAlignment = Excel.Constants.xlCenter
        oSheet.Cells(12, 6) = "Unit"
        oSheet.Cells(12, 7) = "Total"
        oSheet.Range("A11:G12").HorizontalAlignment = Excel.Constants.xlCenter

        Cursor = Cursors.WaitCursor

        Dim NoUrut As Integer = 1, i As Integer = 13, tQTY As Double = 0, mSubTot As Double = 0,
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
            oSheet.Cells(1, 7) = dbTable.Rows(0) !Nama
            oSheet.Cells(1, 7).HorizontalAlignment = Excel.Constants.xlRight
            oSheet.Cells(2, 7) = dbTable.Rows(0) !Alamat.Replace(vbCrLf, "")
            oSheet.Cells(2, 7).HorizontalAlignment = Excel.Constants.xlRight
            oSheet.Range("E2:G4").WrapText = True
            oSheet.Range("E2:G4").Merge()
            oSheet.Range("E2:G4").EntireRow.AutoFit()
            mTglPL = dbTable.Rows(0) !TglPL
        End If

        Cursor = Cursors.WaitCursor
        For a = 0 To dbTable.Rows.Count - 1
            If mNoPO <> dbTable.Rows(a) !NoPO Then
                'If mNoPO <> "" Then
                '    oSheet.Cells(i, 6) = "Sub Total PO No : " & mNoPO & " : "
                '    oSheet.Cells(i, 7) = Format(mSubTot, "###,##0.00")
                '    i = i + 1
                'End If
                oSheet.Cells(i, 4) = "No. PO : " + IIf(dbTable.Rows(a) !NoPO = "", "PEKERTI", dbTable.Rows(a) !NoPO)
                oSheet.Cells(i, 4).HorizontalAlignment = Excel.Constants.xlCenter
                i = i + 1
                mSubTot = 0
            End If

            oSheet.Cells(i, 1) = Format(NoUrut, "###,##0")
            oSheet.Cells(i, 2) = "'" & dbTable.Rows(a) !Kode_Produk.ToString
            oSheet.Cells(i, 3) = "'" & dbTable.Rows(a) !KodePImportir.ToString
            oSheet.Cells(i, 4) = dbTable.Rows(a) !Descript
            oSheet.Cells(i, 5) = Format(dbTable.Rows(a) !qty, "###,##0.00")
            oSheet.Cells(i, 6) = Format(dbTable.Rows(a) !HargaFOB, "###,##0.00")
            oSheet.Cells(i, 7) = Format(dbTable.Rows(a) !SubTot, "###,##0.00")
            mSubTot = mSubTot + dbTable.Rows(a) !SubTot
            mNoPO = dbTable.Rows(a) !NoPO
            tTotalQTY = tTotalQTY + dbTable.Rows(a) !qty
            'If a = dbTable.Rows.Count - 1 Then
            '    i += 1
            '    oSheet.Cells(i, 6) = "Sub Total PO No : " & mNoPO & " : "
            '    oSheet.Cells(i, 6).HorizontalAlignment = Excel.Constants.xlRight
            '    oSheet.Cells(i, 7) = Format(mSubTot, "###,##0.00")
            'End If
            i += 1
            NoUrut += 1
        Next (a)
        oSheet.Range("A1:G11").Font.Bold = True
        oSheet.Cells(i, 4) = "T O T A L   A M O U N T"
        oSheet.Cells(i, 4).HorizontalAlignment = Excel.Constants.xlCenter
        oSheet.Cells(i, 5) = Format(tTotalQTY, "###,##0.00")
        oSheet.Cells(i, 7) = Format(tTotal, "###,##0.00")
        oSheet.Range("A" + Format(i, "##0") & ":G" + Format(i, "##0")).Font.Bold = True
        'oSheet.Range("A11:" & "G" + Format(i, "##0")).EntireColumn.AutoFit()
        oSheet.Range("A11").ColumnWidth = 4
        oSheet.Range("B11").ColumnWidth = 15
        oSheet.Range("C11").ColumnWidth = 15
        oSheet.Range("D11").ColumnWidth = 35
        oSheet.Range("E11").ColumnWidth = 7
        oSheet.Range("F11").ColumnWidth = 9
        oSheet.Range("G11").ColumnWidth = 9
        oSheet.Range("A13:D" + Format(i, "##0")).WrapText = True

        'oSheet.Columns("D").WrapText = True

        oSheet.Range("A11:G" + Format(i, "##0")).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
        oSheet.Range("A11:G" + Format(i, "##0")).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
        i = i + 2
        oSheet.Cells(i, 7) = "Jakarta, " & Format(mTglPL, "dd MMMM yyyy")
        oSheet.Cells(i, 7).HorizontalAlignment = Excel.Constants.xlRight
        oSheet.Range("E" + Format(i, "##0") + ":G" + Format(i, "##0")).Merge()

        i = i + 5
        oSheet.Cells(i, 7) = "____________"
        oSheet.Cells(i, 7).HorizontalAlignment = Excel.Constants.xlRight
        oSheet.Range("E" + Format(i, "##0") + ":G" + Format(i, "##0")).Merge()

        i = i + 1
        oSheet.Cells(i, 7) = "EXPORT DEPT"
        oSheet.Cells(i, 7).HorizontalAlignment = Excel.Constants.xlRight
        oSheet.Range("E" + Format(i, "##0") + ":G" + Format(i, "##0")).Merge()


        Dim fileName As String = locFile.Text & "\PLi_" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"
        oBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
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
                "  And KodePerajin = '" & Microsoft.VisualBasic.Left(dbTable.Rows(a) !Kode_Produk, 5) & "' "
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
    Private Sub ProsesDPL()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        Cursor = Cursors.WaitCursor
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)
        oSheet.Cells(1, 1) = "Draft Packing List"

        oSheet.Cells(1, 1).HorizontalAlignment = Excel.Constants.xlCenter
        oSheet.Range("A1:L1").Font.Size = 14
        oSheet.Range("A1:H1").Merge()


        oSheet.Cells(3, 1) = "No DPL"
        oSheet.Cells(4, 1) = "Dokumen Intern Bagian Gudang / Bukan Dokumen Resmi"

        oSheet.Range("A1:H1").Merge()
        oSheet.Range("A4:C4").Merge()
        oSheet.Range("A1:A7").Font.Bold = True
        oSheet.Cells(9, 1) = "No."
        oSheet.Cells(9, 2) = "Kode Produk"
        oSheet.Cells(9, 3) = "Kode Importir"
        oSheet.Cells(9, 4) = "Produk"
        oSheet.Cells(9, 5) = "Jumlah Boks"
        oSheet.Cells(9, 6) = "Jumlah Produk"
        oSheet.Cells(9, 7) = "Satuan"
        oSheet.Cells(9, 8) = "Total"
        oSheet.Cells(9, 9) = "Netto"
        oSheet.Cells(9, 10) = "Total"
        oSheet.Cells(9, 11) = "Dimensi"
        oSheet.Cells(9, 12) = "Volumme"

        oSheet.Range("D8").ColumnWidth = 50
        oSheet.Range("A9:L9").Font.Bold = True

        Cursor = Cursors.WaitCursor
        Dim i As Integer = 10, dimensi As String = "", volume As Double = 0,
            tJumlahBoks As Double = 0, tJumlahProduk As Double = 0, totalJmlhProduk As Double = 0,
            totalBruto As Double = 0, totalNeto As Double = 0, totalVolume As Double = 0,
            noBoksAkhir As Double = 0, tBruto As Double = 0, VolContainer As Double = 0,
            kelebihanVolume As Double = 0

        MsgSQL = "SELECT NoDPL, t_DPL.NoBoksAwal + ' - ' + NoBoksAkhir noBoks, NoBoksAkhir, t_DPL.JumlahBoks, " &
            "  '(' +  CONVERT(varchar(10), JumlahBoks)  + ' boxes / ' + CONVERT(varchar(10), TotalTiapBoks) + ' pcs each)' boxes, " &
            "   t_DPL.TotalTiapBoks, t_DPL.NoPO, t_DPL.KodeImportir, t_DPL.Importir,  " &
            "   t_DPL.KodeProduk, t_DPL.Produk, t_DPL.Panjang, t_DPL.Lebar, t_DPL.Tinggi,  " &
            "   t_DPL.Bruto , t_DPL.Netto, t_DPL.TotalJmlBoks, CargoLaut, CargoUdara, VolContainer,  " &
            "   right('0000000000'+noboksawal,10) + right('0000000000' + noboksakhir, 10) NnoBox " &
            "FROM Pekerti.dbo.t_DPL t_DPL  " &
            "Where t_DPL.NoDPL = '" & idRec.Text & "' " &
            "  And T_DPL.AktifYN = 'Y' " &
            "order by right('0000000000'+noboksawal,10), right('0000000000' + noboksakhir, 10), KodeProduk, idrec "

        dbTable = Proses.ExecuteQuery(MsgSQL)
        If dbTable.Rows.Count <> 0 Then
            oSheet.Range("B3").Value = IIf(Trim(dbTable.Rows(0) !NoDPL) = "", "PEKERTI", dbTable.Rows(0) !NoDPL)
            oSheet.Range("A5").Value = dbTable.Rows(0) !Importir
        End If
        For a = 0 To dbTable.Rows.Count - 1
            oSheet.Range("A" + Format(i, "##0")).Value = "'" & dbTable.Rows(a) !boxes
            oSheet.Range("C" + Format(i, "##0")).Value = "No.PO : " + dbTable.Rows(a) !NoPO
            i += 1
            oSheet.Range("A" + Format(i, "##0")).Value = "'" & dbTable.Rows(a) !noBoks
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !KodeProduk
            oSheet.Range("C" + Format(i, "##0")).Value = dbTable.Rows(a) !KodeImportir
            oSheet.Range("D" + Format(i, "##0")).Value = dbTable.Rows(a) !Produk
            oSheet.Range("E" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahBoks, "###,##0")
            tJumlahProduk = dbTable.Rows(a) !TotalTiapBoks * dbTable.Rows(a) !JumlahBoks
            oSheet.Range("F" + Format(i, "##0")).Value = Format(tJumlahProduk, "###,##0")
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Bruto, "###,##0")
            tBruto = dbTable.Rows(a) !Bruto * dbTable.Rows(a) !JumlahBoks
            oSheet.Range("H" + Format(i, "##0")).Value = Format(tBruto, "###,##0")
            oSheet.Range("i" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Netto, "###,##0")
            oSheet.Range("J" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !Netto * dbTable.Rows(a) !JumlahBoks, "###,##0")

            dimensi = dbTable.Rows(a) !Panjang & " x " & dbTable.Rows(a) !Lebar & " x " & dbTable.Rows(a) !Tinggi
            oSheet.Range("K" + Format(i, "##0")).Value = dimensi

            If dbTable.Rows(a) !CargoLaut = True Then
                volume = (dbTable.Rows(a) !JumlahBoks * dbTable.Rows(a) !Panjang * dbTable.Rows(a) !Tinggi * dbTable.Rows(a) !Lebar) / 1000000
            ElseIf dbTable.Rows(a) !CargoUdara = True Then
                volume = (dbTable.Rows(a) !Panjang * dbTable.Rows(a) !Tinggi * dbTable.Rows(a) !Lebar * dbTable.Rows(a) !JumlahBoks * 1) / 6000
            Else
                volume = (dbTable.Rows(a) !Panjang * dbTable.Rows(a) !Tinggi * dbTable.Rows(a) !Lebar * dbTable.Rows(a) !JumlahBoks * 1)
            End If
            oSheet.Range("L" + Format(i, "##0")).Value = Format(volume, "###,##0.000")
            tJumlahBoks += dbTable.Rows(a) !JumlahBoks
            totalJmlhProduk += tJumlahProduk
            totalBruto += tBruto
            totalNeto += dbTable.Rows(a) !netto
            totalVolume += volume
            noBoksAkhir = dbTable.Rows(a) !NoBoksAkhir
            VolContainer = dbTable.Rows(a) !VolContainer

            i += 1
        Next (a)
        oSheet.Range("A" + Format(i, "##0")).Value = "Total : "
        oSheet.Range("A" + Format(i, "##0") + ":D" + Format(i, "##0")).Merge()
        oSheet.Range("E" + Format(i, "##0")).Value = Format(tJumlahBoks, "###,##0")
        oSheet.Range("F" + Format(i, "##0")).Value = Format(totalJmlhProduk, "###,##0")
        oSheet.Range("H" + Format(i, "##0")).Value = Format(totalBruto, "###,##0.000")
        oSheet.Range("J" + Format(i, "##0")).Value = Format(totalNeto, "###,##0.000")
        oSheet.Range("L" + Format(i, "##0")).Value = Format(totalVolume, "###,##0.000")
        oSheet.Range("A" + Format(i, "##0") + ":L" + Format(i + 2, "##0")).Font.Bold = True
        oSheet.Range("A" + Format(i, "##0") + ":L" + Format(i + 2, "##0")).Font.Size = 14

        i += 2
        oSheet.Cells(i, 1) = "RINGKASAN"
        i += 1
        oSheet.Cells(i, 1) = "Total Boks/Koli/Peti"
        oSheet.Cells(i, 2) = noBoksAkhir
        i += 1
        oSheet.Cells(i, 1) = "Total Jumlah Produk"
        oSheet.Cells(i, 2) = Format(totalJmlhProduk, "###,##0")
        i += 1
        oSheet.Cells(i, 1) = "Total Berat Kotor"
        oSheet.Cells(i, 2) = Format(totalBruto, "###,##0.000")
        i += 1
        oSheet.Cells(i, 1) = "Total Volume"
        oSheet.Cells(i, 2) = Format(totalVolume, "###,##0.000")
        i += 1
        oSheet.Cells(i, 1) = "Kontainer Terpakai"
        oSheet.Cells(i, 2) = Format(VolContainer, "###,##0.000")

        i += 1
        oSheet.Cells(i, 1) = "Ukuran Kontainer"
        oSheet.Cells(i, 2) = Format(VolContainer, "###,##0.000")

        i += 1
        oSheet.Cells(i, 1) = "Kelebihan Volume"
        'IIf({#TotalVolume}>{Command.VolContainer}, {#TotalVolume}-{Command.VolContainer}, 0)
        If totalVolume > VolContainer Then
            kelebihanVolume = totalVolume - VolContainer
        Else
            kelebihanVolume = 0
        End If
        oSheet.Cells(i, 2) = Format(kelebihanVolume, "###,##0.000")
        oSheet.Range("A" + Format(i, "##0")).Font.Bold = True

        Dim fileName As String = locFile.Text & "\DPL" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"

        oSheet.Columns.AutoFit()
        oSheet.Range("A10: H" & Format(i, "##0")).VerticalAlignment = Excel.Constants.xlCenter
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

    Private Sub ProsesLHP()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        Cursor = Cursors.WaitCursor
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)
        oSheet.Cells(1, 1) = "LAPORAN HASIL PEMERIKSAAN"

        oSheet.Cells(1, 1).HorizontalAlignment = Excel.Constants.xlCenter
        oSheet.Range("A1:H1").Font.Size = 14
        oSheet.Range("A1:H1").Merge()


        oSheet.Cells(2, 1) = "No LHP"
        oSheet.Cells(3, 1) = "No Pra LHP"
        oSheet.Cells(4, 1) = "Perajin"
        oSheet.Cells(5, 1) = "Surat Pengantar"
        oSheet.Cells(6, 1) = "Jumlah Koli"
        oSheet.Cells(7, 1) = "Kargo"
        oSheet.Range("A1:H1").Merge()
        oSheet.Range("A1:A7").Font.Bold = True
        oSheet.Cells(9, 1) = "Kode Produk"
        oSheet.Cells(9, 2) = "Produk"
        oSheet.Cells(9, 3) = "Menurut SP"
        oSheet.Cells(9, 4) = "Tercantum di SPB"
        oSheet.Cells(9, 5) = "Jumlah yang Ada"
        oSheet.Cells(9, 6) = "Jumlah yang Baik"
        oSheet.Cells(9, 7) = "Jumlah Retur"
        oSheet.Cells(9, 8) = "Keterangan"
        oSheet.Range("H8").ColumnWidth = 50
        oSheet.Range("A9:H9").Font.Bold = True

        Cursor = Cursors.WaitCursor
        Dim mulaiPeriksa As String = "", selesaiPeriksa As String = "",
            i As Integer = 10, NoSP As String = "", Perajin As String = ""

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
        If dbTable.Rows.Count <> 0 Then
            oSheet.Range("B2").Value = dbTable.Rows(0) !NoLHP
            oSheet.Range("B3").Value = IIf(Trim(dbTable.Rows(0) !NoPraLHP) = "", "PEKERTI", dbTable.Rows(0) !NoPraLHP)
            oSheet.Range("B4").Value = dbTable.Rows(0) !NamaPerajin
            oSheet.Range("B5").Value = dbTable.Rows(0) !SuratPengantar
            oSheet.Range("B6").Value = "'" & Format(dbTable.Rows(0) !JumlahKoli, "##0")
            oSheet.Range("B7").Value = dbTable.Rows(0) !Kargo
            mulaiPeriksa = "'" & Format(dbTable.Rows(0) !tglMulaiPeriksa, "dd-MM-yyyy")
            selesaiPeriksa = "'" & Format(dbTable.Rows(0) !TglSelesaiPeriksa, "dd-MM-yyyy")
        End If
        For a = 0 To dbTable.Rows.Count - 1
            If NoSP <> dbTable.Rows(a) !NoSP Then
                oSheet.Range("A" + Format(i, "##0")).Value = "No.SP : " + dbTable.Rows(a) !NoSP
                i += 1
            End If
            oSheet.Range("A" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Produk
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !Produk
            oSheet.Range("C" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahPack, "###,##0.000")
            oSheet.Range("D" + Format(i, "##0")).Value = dbTable.Rows(a) !kirim
            oSheet.Range("E" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahHitung, "###,##0.000")
            oSheet.Range("F" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !jumlahbaik, "###,##0.000")
            oSheet.Range("G" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !jumlahtolak, "###,##0.000")
            oSheet.Range("H" + Format(i, "##0")).Value = dbTable.Rows(a) !AlasanDiTolak
            NoSP = dbTable.Rows(a) !NoSP
            i += 1
        Next (a)
        i += 1
        oSheet.Cells(i, 1) = "Mulai Periksa"
        oSheet.Cells(i, 2) = mulaiPeriksa
        oSheet.Range("A" + Format(i, "##0")).Font.Bold = True
        i += 1
        oSheet.Cells(i, 1) = "Selesai Periksa"
        oSheet.Cells(i, 2) = selesaiPeriksa
        oSheet.Range("A" + Format(i, "##0")).Font.Bold = True

        Dim fileName As String = locFile.Text & "\LHP" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"

        oSheet.Columns.AutoFit()
        oSheet.Range("A10: H" & Format(i, "##0")).VerticalAlignment = Excel.Constants.xlCenter
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

    Private Sub ProsesPraLHP()
        Dim dbTable As New Data.DataTable
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        Cursor = Cursors.WaitCursor
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)
        oSheet.Cells(1, 1) = "Pra LHP"
        oSheet.Cells(1, 1).HorizontalAlignment = Excel.Constants.xlCenter
        oSheet.Range("A1:H1").Font.Size = 14
        oSheet.Range("A1:H1").Merge()

        oSheet.Cells(2, 1) = "No Pra LHP"
        oSheet.Cells(3, 1) = "Perajin"
        oSheet.Cells(4, 1) = "Surat Pengantar"
        oSheet.Cells(5, 1) = "Tgl.Terima"
        oSheet.Cells(6, 1) = "Jumlah Koli"
        oSheet.Cells(7, 1) = "Kargo"
        oSheet.Range("A1:A7").Font.Bold = True


        oSheet.Cells(9, 1) = "Kode Produk"
        oSheet.Cells(9, 2) = "Produk"
        oSheet.Cells(9, 3) = "Pack"
        oSheet.Cells(9, 4) = "Kirim"
        oSheet.Cells(9, 5) = "Ada"
        oSheet.Cells(9, 6) = "Baik"
        oSheet.Cells(9, 7) = "Retur"
        oSheet.Cells(9, 8) = "Keterangan"

        oSheet.Range("A9:H9").Font.Bold = True

        Cursor = Cursors.WaitCursor
        Dim i As Integer = 10, NoSP As String = "", Perajin As String = ""


        MsgSQL = "SELECT t_PraLHP.IDRec, t_PraLHP.NoPraLHP, t_PraLHP.Kargo, " &
            "      t_PraLHP.Kode_Produk, t_PraLHP.Produk, t_PraLHP.JumlahPack, " &
            "      t_PraLHP.Kirim, t_PraLHP.TglTerima, t_PraLHP.SuratPengantar, " &
            "      t_PraLHP.JumlahKoli, t_PraLHP.Keterangan, t_SP.NoSP, " &
            "      t_SP.KodeImportir, t_SP.Importir, t_SP.Perajin, t_PraLHP.Koordinator,  " &
            "      t_PraLHP.NoPO, t_PraLHP.SpecSP, t_pralhp.InstruksiPacking " &
            " FROM Pekerti.dbo.t_PraLHP t_PraLHP INNER JOIN Pekerti.dbo.t_SP t_SP ON " &
            "      t_PraLHP.NoSP = t_SP.NoSP  AND t_PraLHP.Kode_Produk = t_SP.KodeProduk " &
            "Where NoPraLHP = '" & idRec.Text & "' " &
            "  And T_PraLHP.AktifYN = 'Y' and t_SP.AktifYN = 'Y'  " &
            "Order by t_PraLHP.IDRec "

        dbTable = Proses.ExecuteQuery(MsgSQL)
        If dbTable.Rows.Count <> 0 Then
            oSheet.Range("B2").Value = IIf(Trim(dbTable.Rows(0) !NoPraLHP) = "", "PEKERTI", dbTable.Rows(0) !NoPraLHP)
            oSheet.Range("B3").Value = dbTable.Rows(0) !Perajin
            oSheet.Range("B4").Value = dbTable.Rows(0) !SuratPengantar
            oSheet.Range("B5").Value = "'" & Format(dbTable.Rows(0) !TglTerima, "dd-MM-yyyy")
            oSheet.Range("B6").Value = "'" & Format(dbTable.Rows(0) !JumlahKoli, "##0").ToString
            oSheet.Range("B7").Value = dbTable.Rows(0) !Kargo
        End If
        For a = 0 To dbTable.Rows.Count - 1
            If NoSP <> dbTable.Rows(a) !NoSP Then
                oSheet.Range("A" + Format(i, "##0")).Value = "No.SP : " + dbTable.Rows(a) !NoSP
                oSheet.Range("A" + Format(i, "##0")).Font.Bold = True
                i += 1
            End If
            oSheet.Range("A" + Format(i, "##0")).Value = dbTable.Rows(a) !Kode_Produk
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !Produk
            oSheet.Range("B" + Format(i, "##0")).WrapText = True
            oSheet.Range("C" + Format(i, "##0")).Value = Format(dbTable.Rows(a) !JumlahPack, "###,##0.000")
            oSheet.Range("D" + Format(i, "##0")).Value = dbTable.Rows(a) !kirim
            oSheet.Range("E" + Format(i, "##0")).Value = "" 'Format(dbTable.Rows(a) !JumlahHitung, "###,##0.000")
            oSheet.Range("F" + Format(i, "##0")).Value = "" 'Format(dbTable.Rows(a) !jumlahbaik, "###,##0.000")
            oSheet.Range("G" + Format(i, "##0")).Value = "" 'Format(dbTable.Rows(a) !jumlahtolak, "###,##0.000")
            oSheet.Range("H" + Format(i, "##0")).Value = dbTable.Rows(a) !keterangan
            NoSP = dbTable.Rows(a) !NoSP
            i += 1
            oSheet.Range("A" + Format(i, "##0")).Value = dbTable.Rows(a) !KodeImportir + " - " + dbTable.Rows(a) !Importir
            oSheet.Range("A" + Format(i, "##0")).WrapText = True
            oSheet.Range("B" + Format(i, "##0")).Value = dbTable.Rows(a) !SpecSP
            oSheet.Range("B" + Format(i, "##0")).WrapText = True
            i += 1
        Next (a)
        i += 1
        oSheet.Cells(i, 1) = "Mulai Periksa : ________________"
        oSheet.Range("A" + Format(i, "##0")).Font.Bold = True
        oSheet.Cells(i, 2) = "Selesai Periksa: ________________"
        oSheet.Range("B" + Format(i, "##0")).Font.Bold = True

        Dim fileName As String = locFile.Text & "\PraLHP" + Replace(idRec.Text, "/", "-") + "_" & Format(Now, "yymmdd_HHmmss") + ".xls"

        oSheet.Columns.AutoFit()
        oSheet.Range("A8").ColumnWidth = 40
        oSheet.Range("B8").ColumnWidth = 100
        oSheet.Range("H8").ColumnWidth = 50
        oSheet.Range("A10: H" & Format(i, "##0")).VerticalAlignment = Excel.Constants.xlCenter
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