Imports System.Data.SqlClient

Public Class ClsKoneksi
    Protected tblPengguna = New DataTable
    Protected SQL As String
    Protected Ds As DataSet
    Protected Dt As DataTable

    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter

    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected userid As String = My.Settings.UserID
    Public Shared database As String = My.Settings.Database  'String.Empty 'My.Settings.Database



    Public Sub initConn()
        'CN = New SqlConnection("Password=root;Persist Security Info=True;User ID=sa;Initial Catalog=master;Data Source=.")
        CN = New SqlConnection("Password=" & pwd & ";Persist Security Info=True;
            User ID=" & userid & ";Initial Catalog=master;Data Source=" & ipserver & "")
        CN.Open()
        Cmd = New SqlCommand()
        Cmd.Connection = CN
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = "if OBJECT_ID(N'msconfig', N'U') is null begin create table msconfig (idrec int not null identity(1,1), nama varchar(50), aktifYN varchar(1)); insert into msconfig (nama, aktifyn) values ('PrimaJaya2018', 'Y') end"
        Cmd.ExecuteNonQuery()
        CN.Close()
    End Sub

    Public Function OpenConn(Optional ByRef CN2 As SqlConnection = Nothing) As Boolean
        'If database = "" Then
        '    CN = New SqlConnection("Initial Catalog=" & database & "; " &
        '        "user id=" & userid & ";password=" & pwd & "; " &
        '        "Persist Security Info=True;" &
        '        "Data Source=" & ipserver & ";")
        '    'CN = New SqlConnection("Password=root;Persist Security Info=True;User ID=sa;Initial Catalog=master;Data Source=.")
        '    CN.Open()
        '    Cmd = New SqlCommand()
        '    Cmd.Connection = CN
        '    Cmd.CommandType = CommandType.Text
        '    Cmd.CommandText = "if exists (select 1 from INFORMATION_SCHEMA.tables where TABLE_TYPE = 'BASE TABLE' and TABLE_NAME='msconfig') select 1 else select 0"
        '    Dim exists As Byte = Cmd.ExecuteScalar()
        '    CN.Close()

        '    If exists = 0 Then
        '        initConn()
        '        If FrmMenuUtama.CompCode.Text = "DM" Then
        '            database = "Domino2018"
        '        Else
        '            database = "PrimaJaya2018"
        '        End If

        '    Else
        '        CN.Open()
        '        Cmd.CommandText = "select nama from master..msconfig where aktifYN = 'Y'"
        '        database = Cmd.ExecuteScalar()
        '        CN.Close()
        '    End If
        'End If


        Cmd = Nothing
        CN = New SqlConnection("Initial Catalog=" & database & "; " &
                "user id=" & userid & ";password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";")

        Try
            If CN.State = ConnectionState.Closed Then
                CN.Open()
                Console.WriteLine("State: {0}", CN.State)
                Console.WriteLine("ConnectionTimeout: {0}", CN.ConnectionTimeout)
                CN2 = CN
                Console.WriteLine("State: {0}", CN2.State)
                Console.WriteLine("ConnectionTimeout: {0}", CN2.ConnectionTimeout)
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

    Public Sub CloseConn(Optional ByRef CN2 As SqlConnection = Nothing)
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
        If Not IsNothing(CN2) Then
            CN2.Dispose()
            CN2.Close()
            CN2 = Nothing
        End If
    End Sub


    Public Function ExecuteQuery(ByVal Query As String) As DataTable
        If Not OpenConn() Then
            MsgBox("Koneksi Gagal..!!", MsgBoxStyle.Critical, "Access Failed")
            Return Nothing
            Exit Function
        End If

        Cmd = New SqlCommand(Query, CN)
        DA = New SqlDataAdapter()
        DA.SelectCommand = Cmd
        Ds = New Data.DataSet
        DA.Fill(Ds)
        Dt = Ds.Tables(0)
        Ds = Nothing
        DA = Nothing
        Cmd = Nothing
        CloseConn()
        Return Dt
    End Function

    Public Function ProsesClosing() As Boolean
        Dim Hasil As Boolean = False, mCek As String
        SQL = "Select dbCloud from m_Company"
        mCek = ExecuteSingleStrQuery(SQL)
        If mCek = "PROSES" Then
            Hasil = True
        Else
            Hasil = False
        End If
        ProsesClosing = Hasil
    End Function

    Public Sub ExecuteNonQuery(ByVal Query As String)

        If Not OpenConn() Then
            MsgBox("Koneksi Gagal..!!", MsgBoxStyle.Critical, "Access Failed..!!")
            Exit Sub
        End If
        Cmd = New SqlCommand()
        Cmd.Connection = CN
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Query
        Cmd.ExecuteNonQuery()
        Cmd = Nothing
        CloseConn()
    End Sub
    Public Function MaxNoUrut(tField As String, tTable As String, Kode As String) As String
        Dim MsgSQL As String, RsMax As New DataTable
        Dim Proses As New ClsKoneksi
        MsgSQL = "Select convert(Char(4), GetDate(), 12) TGL, isnull(Max(Right(" & tField & ",4)),0) + 10001 RecId " &
            " From " & tTable & " " &
            "Where Left(" & tField & ",4) = convert(Char(4), GetDate(), 12) and aktifYN = 'Y' "
        RsMax = Proses.ExecuteQuery(MsgSQL)
        MaxNoUrut = RsMax.Rows(0) !TGL + Kode +
            Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 4)
    End Function
    Public Function GetMaxId(ByVal nTable As String, nField As String, nKodeToko As String) As String
        Dim SQL As String, Prefik As String = "", IdRec As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        SQL = "Select convert(Char(2), GetDate(), 12) Tahun, 
	       Bulan = CASE month(GetDate())  
             WHEN 1 THEN 'A'  WHEN 2 THEN 'B' WHEN 3 THEN 'C'
             WHEN 4 THEN 'D'  WHEN 5 THEN 'E' WHEN 6 THEN 'F'
		     WHEN 7 THEN 'G'  WHEN 8 THEN 'H' WHEN 9 THEN 'I'
             WHEN 10 THEN 'J' WHEN 11 THEN 'K' WHEN 12 THEN 'L'
           END "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Prefik = dbTable.Rows(0)!Tahun + dbTable.Rows(0)!bulan + nKodeToko
        End If
        'YYB TK TR 99999
        'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
        'SQL = "Select max(right(" & nField & ",5)) + 100001 as TID " &
        '    " From " & nTable & " " &
        '    "Where Left(" & nField & ",7) = '" & Prefik & "'"
        SQL = "Select max(right(" & nField & ",5)) + 100001 as TID " &
            " From " & nTable & " " &
            "Where Left(" & nField & ",2) = '" & dbTable.Rows(0)!Tahun & "'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If IsDBNull(dbTable.Rows(0)!tid) Then
                IdRec = Prefik + "00001"
            Else
                IdRec = Prefik + Right(dbTable.Rows(0)!tid, 5)
            End If
        End If
        GetMaxId = IdRec
    End Function

    Public Function GetMaxId_Transaksi(ByVal nTable As String, nField As String, nKodeToko As String) As String
        Dim SQL As String, Prefik As String = "", IdRec As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        SQL = "Select convert(Char(2), GetDate(), 12) Tahun, 
	       Bulan = CASE month(GetDate())  
             WHEN 1 THEN 'A'  WHEN 2 THEN 'B' WHEN 3 THEN 'C'
             WHEN 4 THEN 'D'  WHEN 5 THEN 'E' WHEN 6 THEN 'F'
		     WHEN 7 THEN 'G'  WHEN 8 THEN 'H' WHEN 9 THEN 'I'
             WHEN 10 THEN 'J' WHEN 11 THEN 'K' WHEN 12 THEN 'L'
           END "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Prefik = dbTable.Rows(0)!Tahun + dbTable.Rows(0)!bulan + nKodeToko
        End If
        'YYB TK TR 99999
        'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
        'SQL = "Select max(right(" & nField & ",5)) + 100001 as TID " &
        '    " From " & nTable & " " &
        '    "Where Left(" & nField & ",7) = '" & Prefik & "'"
        SQL = "Select max(right(" & nField & ",5)) + 100001 as TID " &
            " From " & nTable & " " &
            "Where Left(" & nField & ",7) = '" & Prefik & "'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If IsDBNull(dbTable.Rows(0)!tid) Then
                IdRec = Prefik + "00001"
            Else
                IdRec = Prefik + Right(dbTable.Rows(0)!tid, 5)
            End If
        End If
        GetMaxId_Transaksi = IdRec
    End Function

    Public Function GetMaxIdTrans(ByVal nTable As String, nField As String, nKodeToko As String) As String
        Dim SQL As String, Prefik As String = "", IdRec As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        Prefik = nKodeToko + "-"
        SQL = "Select max(right(" & nField & ",6)) + 1000001 as TID " &
            " From " & nTable & " " &
            "Where Left(" & nField & ",3) = '" & Left(Prefik, 3) & "'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If IsDBNull(dbTable.Rows(0) !tid) Then
                IdRec = Prefik + "000001"
            Else
                IdRec = Prefik + Right(dbTable.Rows(0) !tid, 6)
            End If
        End If
        GetMaxIdTrans = IdRec
    End Function

    Public Function GetMaxID_int(ByVal nTable, field) As Integer
        Dim IdRec As Integer, DBTable As DataTable, Proses As New ClsKoneksi
        SQL = "Select max(" & field & ") + 1 as TID " &
            " From " & nTable & " "
        DBTable = Proses.ExecuteQuery(SQL)
        If DBTable.Rows.Count <> 0 Then
            If IsDBNull(DBTable.Rows(0) !tid) Then
                IdRec = 1
            Else
                IdRec = Right(DBTable.Rows(0) !tid, 5)
            End If
        End If
        GetMaxID_int = IdRec
    End Function
    Public Function UserAksesTombol(ByVal tUser As String, ByVal NamaForm As String, ByVal Tombol As String) As Boolean
        Dim Result As Boolean = False
        Dim SQL As String
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        SQL = "Select " & Tombol & " akses From M_UserAkses " &
            "Where User_ID = '" & tUser & "' " &
            "  And Menu = '" & NamaForm & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            Result = dbTable.Rows(0) !akses
        Else
            Result = False
        End If
        UserAksesTombol = Result
    End Function

    Public Function UserAksesMenu(ByVal tUser As String, ByVal JenisTR As String) As Boolean
        Dim Result As Boolean = False
        Dim SQL As String
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        SQL = "Select Akses  From M_UserAkses " &
            "Where User_ID = '" & tUser & "' " &
            "  And menu = '" & JenisTR & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            If dbTable.Rows(0) !Akses = 1 Then
                Result = True
            Else
                Result = False
            End If
        Else
            Result = False
        End If
        UserAksesMenu = Result
    End Function

    Public Function ExecuteSingleDblQuery(ByVal Query As String) As Double
        If Not OpenConn() Then
            MsgBox("Koneksi Gagal..!!", MsgBoxStyle.Critical, "Access Failed")
            Return Nothing
            Exit Function
        End If

        Cmd = New SqlCommand(Query, CN)
        DA = New SqlDataAdapter()
        DA.SelectCommand = Cmd

        Ds = New Data.DataSet
        DA.Fill(Ds)

        Dt = Ds.Tables(0)

        Ds = Nothing
        DA = Nothing
        Cmd = Nothing

        CloseConn()
        Try
            Return Dt.Rows(0)(0)
        Catch ex As Exception
            Return 0
        End Try

        Dt = Nothing

    End Function


    Public Function ConvertCurrencyToEnglish(ByVal MyNumber As Double) As String
        Dim Temp As String
        Dim Dollars, Cents As String
        Dim DecimalPlace, Count As Integer
        Dim Place(9) As String
        Dim Numb As String
        Place(2) = " Thousand " : Place(3) = " Million " : Place(4) = " Billion " : Place(5) = " Trillion "
        ' Convert Numb to a string, trimming extra spaces.
        Numb = Trim(Str(MyNumber))
        ' Find decimal place.
        DecimalPlace = InStr(Numb, ".")
        ' If we find decimal place...
        If DecimalPlace > 0 Then
            ' Convert cents
            Temp = Left(Mid(Numb, DecimalPlace + 1) & "00", 2)
            Cents = ConvertTens(Temp)
            ' Strip off cents from remainder to convert.
            Numb = Trim(Left(Numb, DecimalPlace - 1))
        End If
        Count = 1
        Do While Numb <> ""
            ' Convert last 3 digits of Numb to English dollars.
            Temp = ConvertHundreds(Right(Numb, 3))
            If Temp <> "" Then Dollars = Temp & Place(Count) & Dollars
            If Len(Numb) > 3 Then
                ' Remove last 3 converted digits from Numb.
                Numb = Left(Numb, Len(Numb) - 3)
            Else
                Numb = ""
            End If
            Count = Count + 1
        Loop

        ' Clean up dollars.
        Select Case Dollars
            Case "" : Dollars = "No "
            Case "One" : Dollars = "One"
            Case Else : Dollars = Dollars & ""
        End Select

        ' Clean up cents.
        Select Case Cents
            Case "" : Cents = ""
            Case "One" : Cents = " And One Cent"
            Case Else : Cents = " And Cents " & Cents & ""
        End Select
        ConvertCurrencyToEnglish = Dollars & Cents
    End Function

    Private Function ConvertHundreds(ByVal MyNumber As String) As String
        Dim Result As String = ""
        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function
        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)
        ' Do we have a hundreds place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundred "
        End If
        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If
        ConvertHundreds = Trim(Result)
    End Function

    Private Function ConvertTens(ByVal MyTens As String) As String
        Dim Result As String = ""
        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select
            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If
        ConvertTens = Result
    End Function

    Private Function ConvertDigit(ByVal MyDigit As String) As String
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function


    Public Function ExecuteSingleStrQuery(ByVal Query As String) As String
        If Not OpenConn() Then
            MsgBox("Koneksi Gagal..!!", MsgBoxStyle.Critical, "Access Failed")
            Return Nothing
            Exit Function
        End If

        Cmd = New SqlCommand(Query, CN)
        DA = New SqlDataAdapter()
        DA.SelectCommand = Cmd

        Ds = New Data.DataSet
        DA.Fill(Ds)

        Dt = Ds.Tables(0)

        Ds = Nothing
        DA = Nothing
        Cmd = Nothing

        CloseConn()
        Try
            Return Dt.Rows(0)(0)
        Catch ex As Exception
            Return ""
        End Try

        Dt = Nothing

    End Function

    Public Function GetPapersizeID(ByVal PrinterName As String, ByVal PaperSizeName As String) As Integer
        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        Dim PaperSizeID As Integer = 0
        Dim ppname As String = ""
        Dim s As String = ""
        doctoprint.PrinterSettings.PrinterName = PrinterName '"HP LaserJet P1006" '(ex."EpsonSQ-1170ESC/P2")
        For i As Integer = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            Dim rawKind As Integer
            ppname = PaperSizeName '"1/2 A4"
            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = ppname Then
                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                PaperSizeID = rawKind
                Exit For
            End If
        Next
        Return PaperSizeID
    End Function


    Public Function ExecuteQueryALLDB(ByVal Query As String) As DataTable
        If Not OpenConnDefault() Then
            MsgBox("Koneksi Gagal..!!", MsgBoxStyle.Critical, "Access Failed")
            Return Nothing
            Exit Function
        End If

        Cmd = New SqlCommand(Query, CN)
        DA = New SqlDataAdapter()
        DA.SelectCommand = Cmd
        Ds = New Data.DataSet
        DA.Fill(Ds)
        Dt = Ds.Tables(0)
        Ds = Nothing
        DA = Nothing
        Cmd = Nothing
        CloseConn()
        Return Dt
    End Function

    Public Function OpenConnDefault(Optional ByRef CN2 As SqlConnection = Nothing) As Boolean
        Cmd = Nothing
        CN = New SqlConnection("Initial Catalog=master; " &
                "user id=sa;password=" & pwd & "; " &
                "Persist Security Info=True;" &
                "Data Source=" & ipserver & ";")
        Try
            If CN.State = ConnectionState.Closed Then
                CN.Open()
                Console.WriteLine("State: {0}", CN.State)
                Console.WriteLine("ConnectionTimeout: {0}", CN.ConnectionTimeout)
                CN2 = CN
                Console.WriteLine("State: {0}", CN2.State)
                Console.WriteLine("ConnectionTimeout: {0}", CN2.ConnectionTimeout)
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

    Public Function MaxYNoUrut(tField As String, tTable As String, Kode As String) As String
        Dim MsgSQL As String, RsMax As New DataTable
        MsgSQL = "Select convert(Char(2), GetDate(), 12) TGL, isnull(Max(left(" & tField & ",3)),0) + 1000001 RecId " &
            " From " & tTable & " " &
            "Where Right(" & tField & ",2) = convert(Char(2), GetDate(), 12) and aktifYN = 'Y'  "
        RsMax = ExecuteQuery(MsgSQL)
        MaxYNoUrut = Microsoft.VisualBasic.Right(RsMax.Rows(0) !recid, 3) + "/" + Kode + "/" +
            Trim(Str(RsMax.Rows(0) !tGL))
    End Function
    Public Function FindKodeProdukSP(Cari As String, tNoSP As String) As String
        Dim MsgSQL As String, tKodeBrg As String = "", mKondisi As String = ""
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And Produk like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select NoPO, Importir, KodeImportir, HargaBeliRP, Produk, KodeProduk " &
            " From T_SP " &
            "Where AktifYN = 'Y' " &
            " " & mKondisi & " " &
            " And NoSP = '" & tNoSP & "' "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar Produk SP"
        Form_Daftar.param1.Text = tNoSP
        Form_Daftar.ShowDialog()
        tKodeBrg = FrmMenuUtama.TSKeterangan.Text
        FindKodeProdukSP = tKodeBrg
    End Function
    Public Function FindSP(Cari, tKodePerajin) As String
        Dim tNoSP As String = "", mKondisi As String = "", MsgSQL As String = ""

        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And NoSP like '%" & Trim(Cari) & "%' "
        End If
        If Trim(tKodePerajin) <> "" Then
            mKondisi = mKondisi & " AND Kode_Perajin = '" & tKodePerajin & "' "
        End If
        MsgSQL = "Select NoSP, Kode_Perajin, Perajin  " &
            " From T_SP " &
            "Where T_SP.AktifYN = 'Y' " &
            "  " & mKondisi & " " &
            "Group By NoSP, Kode_Perajin, Perajin " &
            "Order By right(NoSP,2) Desc, nosp desc "
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar SP"
        Form_Daftar.ShowDialog()
        tNoSP = FrmMenuUtama.TSKeterangan.Text
        FindSP = tNoSP
    End Function

    Public Function QTYProdukSP(tKodeProduk As String, tNoSP As String) As Double
        Dim MsgSQL As String, RSCek As New DataTable
        MsgSQL = "Select isnull(Sum(Jumlah),0) QTY From t_SP " &
            "Where NoPO = '" & tNoSP & "' " &
            " And KodeProduk = '" & tKodeProduk & "' " &
            " And AktifYN = 'Y' "
        RSCek = ExecuteQuery(MsgSQL)
        If RSCek.Rows.Count <> 0 Then
            QTYProdukSP = RSCek.Rows(0) !qty
        Else
            QTYProdukSP = 0
        End If
    End Function

    Public Function TglIndo(sTgl As String) As String
        Dim hasil As String = ""
        If Mid(sTgl, 4, 2) = "01" Then
            hasil = Left(sTgl, 2) + " Januari " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "02" Then
            hasil = Left(sTgl, 2) + " Februari " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "03" Then
            hasil = Left(sTgl, 2) + " Maret " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "04" Then
            hasil = Left(sTgl, 2) + " April " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "05" Then
            hasil = Left(sTgl, 2) + " Mei " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "06" Then
            hasil = Left(sTgl, 2) + " Juni " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "07" Then
            hasil = Left(sTgl, 2) + " Juli " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "08" Then
            hasil = Left(sTgl, 2) + " Agustus " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "09" Then
            hasil = Left(sTgl, 2) + " September " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "10" Then
            hasil = Left(sTgl, 2) + " Oktober " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "11" Then
            hasil = Left(sTgl, 2) + " Nopember " + Right(sTgl, 4)
        ElseIf Mid(sTgl, 4, 2) = "12" Then
            hasil = Left(sTgl, 2) + " Desember " + Right(sTgl, 4)
        End If
        TglIndo = hasil
    End Function
    Public Function FindKodeProdukPO_SP(Cari As String, tNoPO As String) As String
        Dim RSD As New DataTable, mKondisi As String
        Dim MsgSQL As String, tKodeProduk As String = ""
        If Trim(Cari) = "" Then
            mKondisi = ""
        Else
            mKondisi = "And Deskripsi like '%" & Trim(Cari) & "%' "
        End If
        MsgSQL = "Select Deskripsi, Kode_Buyer, Kode_Produk, Kode_Importir, " &
            " m_KodeImportir.Nama, T_PO.NoPO, t_PO.Jumlah " &
            " From t_PO inner join m_KodeProduk ON " &
            " m_KodeProduk.KodeProduk = t_PO.Kode_produk " &
            " inner join m_KodeImportir on Kode_Importir = KodeImportir " &
            "Where t_PO.AktifYN = 'Y' " &
            "  And T_PO.NOPO = '" & tNoPO & "' " &
            " " & mKondisi & " "

        Form_Daftar.param1.Text = tNoPO
        Form_Daftar.txtQuery.Text = MsgSQL
        Form_Daftar.Text = "Daftar Produk PO (SP)"
        Form_Daftar.ShowDialog()
        tKodeProduk = FrmMenuUtama.TSKeterangan.Text
        FindKodeProdukPO_SP = tKodeProduk
    End Function
End Class
