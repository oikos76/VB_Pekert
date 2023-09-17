Public Class Terbilang
    'Private _Kamus As SortedDictionary(Of Integer, String)
    'Private _ArGroup() As String = {"", " Ribu", " Juta", " Milyar", " Triliun"}

    'Private Sub InitializeKamus()
    '    _Kamus = New SortedDictionary(Of Integer, String)
    '    _Kamus.Clear()
    '    _Kamus.Add(0, "")
    '    _Kamus.Add(1, " Satu")
    '    _Kamus.Add(2, " Dua")
    '    _Kamus.Add(3, " Tiga")
    '    _Kamus.Add(4, " Empat")
    '    _Kamus.Add(5, " Lima")
    '    _Kamus.Add(6, " Enam")
    '    _Kamus.Add(7, " Tujuh")
    '    _Kamus.Add(8, " Delapan")
    '    _Kamus.Add(9, " Sembilan")
    '    _Kamus.Add(10, " Sepuluh")
    '    _Kamus.Add(11, " Sebelas")
    '    _Kamus.Add(100, " Seratus")
    'End Sub

    'Public Function Terbilang(ByVal Bilangan As Double) As String
    '    Dim sRet As String = ""
    '    Dim sMinus As String = ""
    '    Dim BilCacah As Double = 0
    '    Dim BilPecahan As Integer = 0


    '    InitializeKamus()

    '    Try
    '        If Bilangan < 0 Then sMinus = "Minus "

    '        Dim grp() As String = Split(Math.Abs(Bilangan).ToString(System.Globalization.NumberFormatInfo.CurrentInfo), System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
    '        If grp.Length > 1 Then
    '            BilCacah = CDbl(grp(0))
    '            BilPecahan = CInt(grp(1))
    '        Else
    '            BilCacah = Bilangan
    '        End If

    '        Dim triple() As String = Split(BilCacah.ToString("#,##0", System.Globalization.NumberFormatInfo.CurrentInfo), System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator)
    '        Array.Reverse(triple)

    '        For i As Integer = triple.Length - 1 To 0 Step -1
    '            sRet = sRet & BacaGroupAngka(triple(i), False, IIf(i > 5, i - 5 + 1, i))
    '        Next

    '        If BilPecahan > 0 Then
    '            sRet = sRet & " Koma" & BacaGroupAngka(BilPecahan, True)
    '        End If

    '        sRet = sMinus & sRet

    '    Catch ex As Exception
    '        MsgBox(ex.Message, vbOKOnly, "Parsing Bilangan")

    '    End Try

    '    _Kamus.Clear()
    '    _Kamus = Nothing

    '    Return sRet.Trim
    'End Function

    'Private Function BacaGroupAngka(ByVal Angka As Integer, Optional ByVal IsPecahan As Boolean = False, Optional ByVal iGroup As Byte = 0) As String
    '    Dim sRet As String = ""
    '    Dim sAngka As String = Angka.ToString("000")

    '    Select Case IsPecahan
    '        Case True
    '            Try
    '                For i As Integer = 0 To sAngka.Length - 1
    '                    If CInt(sAngka.Substring(i, 1)) = 0 Then
    '                        sRet = sRet & "Nol"
    '                    Else
    '                        sRet = sRet & _Kamus(CInt(sAngka.Substring(i, 1)))
    '                    End If
    '                Next
    '            Catch ex As Exception
    '                MsgBox(ex.Message, vbOKOnly, "Baca Pecahan")
    '            End Try
    '        Case Else
    '            Try
    '                If Angka = 1 And iGroup = 1 Then
    '                    sRet = " Seribu"
    '                ElseIf _Kamus.ContainsKey(Angka) Then
    '                    sRet = _Kamus(Angka) & _ArGroup(iGroup)
    '                Else
    '                    Dim Satuan As String = _Kamus(CInt(sAngka.Substring(2, 1)))
    '                    Dim puluhan As String = ""
    '                    Dim ratusan As String = ""

    '                    If _Kamus.ContainsKey(CInt(sAngka.Substring(1, 2))) Then
    '                        puluhan = _Kamus(CInt(sAngka.Substring(1, 2)))
    '                    Else
    '                        If CInt(sAngka.Substring(1, 1)) = 0 Then
    '                            puluhan = Satuan
    '                        ElseIf CInt(sAngka.Substring(1, 1)) = 1 Then
    '                            puluhan = Satuan & " Belas"
    '                        Else
    '                            puluhan = _Kamus(CInt(sAngka.Substring(1, 1))) & " Puluh" & Satuan
    '                        End If
    '                    End If

    '                    If CInt(sAngka.Substring(0, 1)) = 0 Then
    '                        ratusan = puluhan
    '                    ElseIf CInt(sAngka.Substring(0, 1)) = 1 Then
    '                        ratusan = " Seratus" & puluhan
    '                    Else
    '                        ratusan = _Kamus(CInt(sAngka.Substring(0, 1))) & " Ratus" & puluhan
    '                    End If

    '                    sRet = ratusan & _ArGroup(iGroup)
    '                End If
    '            Catch ex As Exception
    '                MsgBox(ex.Message, vbOKOnly, "Baca Bilangan Bulat")
    '            End Try
    '    End Select

    '    Return sRet
    'End Function

    Public Function Terbilang(ByVal nilai As Long) As String
        Dim bilangan As String() = {"", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas"}
        If nilai < 0 Then
            Return " "
        ElseIf nilai < 12 Then
            Return " " & bilangan(nilai)
        ElseIf nilai < 20 Then
            Return Terbilang(nilai - 10) & " Belas"
        ElseIf nilai < 100 Then
            Return (Terbilang(CInt((nilai \ 10))) & " Puluh") + Terbilang(nilai Mod 10)
        ElseIf nilai < 200 Then
            Return " Seratus" & Terbilang(nilai - 100)
        ElseIf nilai < 1000 Then
            Return (Terbilang(CInt((nilai \ 100))) & " Ratus") + Terbilang(nilai Mod 100)
        ElseIf nilai < 2000 Then
            Return " Seribu" & Terbilang(nilai - 1000)
        ElseIf nilai < 1000000 Then
            Return (Terbilang(CInt((nilai \ 1000))) & " Ribu") + Terbilang(nilai Mod 1000)
        ElseIf nilai < 1000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000))) & " Juta") + Terbilang(nilai Mod 1000000)
        ElseIf nilai < 1000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000))) & " Milyar") + Terbilang(nilai Mod 1000000000)
        ElseIf nilai < 1000000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000000))) & " Trilyun") + Terbilang(nilai Mod 1000000000000)
        Else
            Return ""
        End If
    End Function


    Public Function CurrencyText(ByVal Amount As Double, ByVal CurName As String) As String
        Dim Amt$, Tri$, Out$
        Dim GRP As Integer = 0, Unit As String = ""
        Dim Dollars!, Cents%, tRibu
        Dim GroupNames$, UnitFraction$, CentAsFraction%
        If CurName = "RP" Then
            Unit = "Rupiah"
            UnitFraction = "Sen"
            GroupNames = "triliunmiliyarjuta   "
            CentAsFraction = 0
            If Amount < 0# Then CurrencyText = "" : Exit Function
            Amt = Format$(Amount, "000000000000000.00")
            Dollars = Fix(Val(Amt))

            For GRP = 0 To 4
                Tri = Left(Amt, 3)
                Amt = Mid(Amt, 4)
                If Val(Tri) And GRP < 4 Then
                    If Amount = 1000 Then
                        Out = "seribu "
                    Else
                        Out = Out & TriGroupText(Tri, CurName)
                        tRibu = Right(Format(Amt + 1000, "###"), 3)
                        If GRP = 3 And Len(tRibu) = 3 And Right(Out, 5) <> "ribu " Then
                            Out = Out & "ribu "
                        End If
                        If GRP < 3 Then
                            Out = Out & Trim$(Mid(GroupNames, GRP * 7 + 1, 7)) & " "
                        End If
                    End If
                ElseIf GRP = 4 Then
                    If Val(Tri) Then
                        Out = Out & TriGroupText(Tri, CurName)
                        If GRP < 3 Then Out = Out & Trim$(Mid(GroupNames, GRP * 7 + 1, 7)) & " "
                    Else
                        If Out <> "seribu " Then
                            If Right(Out, 5) = "ribu " Or
                               Right(Out, 5) = "juta " Or
                               Right(Out, 8) = "miliyar " Then
                                Out = Out
                            Else
                                Out = Out & "ribu "
                            End If
                        End If
                    End If
                End If
            Next GRP

            If Dollars Then
                Out = Out & Unit
                If Dollars = 1 Then Out = Out
            Else
                Out = Out & "nol " & Unit
            End If

            Amt = "0" & Right$(Amt, 2)
            Cents = Val(Amt)

            If Cents Then
                Out = Out & " "
                If CentAsFraction Then
                    Out = Out & Right$(Amt, 2) & "/100"
                Else
                    Out = Out & TriGroupText(Amt, CurName) & UnitFraction
                    If Cents = 1 Then Out = Out
                End If
            Else
                If CentAsFraction Then
                    Out = Out & " and 00/100"
                End If
            End If
        ElseIf CurName = "US" Or CurName = "USD" Or CurName = "SGD" _
            Or CurName = "EURO" Then


            If CurName = "US" Or CurName = "USD" Then
                Unit = "Dollar"
            ElseIf CurName = "SGD" Then
                Unit = "SGD"
            ElseIf CurName = "EURO" Then
                Unit = "Euro"
            End If
            UnitFraction = "Cent"
            GroupNames = "trillionbillion million thousandhundred  "
            CentAsFraction = 0

            If Amount < 0# Then CurrencyText = "" : Exit Function
            Amt = Format$(Amount, "000000000000000.00")
            Dollars = Fix(Val(Amt))

            For GRP = 0 To 4
                Tri = Left(Amt, 3)
                Amt = Mid(Amt, 4)
                '            Debug.Print Amt
                If Val(Tri) And GRP < 4 Then
                    Out = Out & TriGroupText(Tri, CurName)
                    If GRP = 3 And Len(Format$(Amt, "###")) <= 3 And Right(Out, 9) <> "thousand " Then
                        Out = Out & "thousand "
                    End If
                    If GRP < 3 Then
                        Out = Out & Trim$(Mid(GroupNames, GRP * 8 + 1, 8)) & " "
                    End If
                ElseIf GRP = 4 Then
                    If Val(Tri) Then
                        Out = Out & TriGroupText(Tri, CurName)
                        If GRP < 3 Then Out = Out & Trim$(Mid(GroupNames, GRP * 8 + 1, 8)) & " "
                    Else
                        'Out = Out & "thousand "
                        If Right(Out, 9) = "thousand " Or
                            Right(Out, 8) = "million " Or
                            Right(Out, 8) = "billion " Then
                            Out = Out
                        Else
                            Out = Out & "thousand "
                        End If
                    End If
                End If
            Next GRP
            '        Debug.Print Amt
            If Dollars Then
                Out = Out & Unit
                If Dollars = 1 Then Out = Out & "s"
            Else
                Out = Out & "zero " & Unit & "s"
            End If

            Amt = "0" & Right$(Amt, 2)
            Cents = Val(Amt)

            If Cents Then
                Out = Out & " and "
                If CentAsFraction Then
                    Out = Out & Right$(Amt, 2) & "/100"
                Else
                    Out = Out & TriGroupText(Amt, CurName) & UnitFraction
                    If Cents = 1 Then Out = Out & "s"
                End If
            Else
                If CentAsFraction Then
                    Out = Out & " and 00/100"
                End If
            End If
        End If
        Out = Trim$(Out)
        CurrencyText = UCase$(Left(Out, 1)) & Mid(Out, 2)
    End Function

    Private Function TriGroupText(Amt$, CurName As String) As String
        Dim Digit1 As Long, Digit10%, Digit100%, Digit1000%, Digit10000%, Digit100000%, Out$
        Dim Ones$, Tens$, Teens$, Ratus$, Ribu$, PuluhRibu$, RatusRibu$
        Dim GRP As Double = 0

        If CurName = "RP" Then
            Ones = "satu    dua     tiga    empat   lima    enam    tujuh   delapan sembilan"
            Tens = "sepuluh       dua puluh     tiga puluh    empat puluh   lima puluh    enam puluh    tujuh puluh   delapan puluh sembilan puluh"
            Teens = "sebelas       dua belas     tiga belas    empat belas   lima belas    enam belas    tujuh belas   delapan belas sembilan belas"
            Ratus = "seratus       dua ratus     tiga ratus    empat ratus   lima ratus    enam ratus    tujuh ratus   delapan ratus sembilan ratus"
            Ribu = "seribu       dua ribu     tiga ribu    empat ribu   lima ribu    enam ribu    tujuh ribu   delapan ribu sembilan ribu"
            PuluhRibu = "sepuluh ribu       dua puluh ribu     tiga puluh ribu    empat puluh ribu   lima puluh ribu    enam puluh ribu    tujuh puluh ribu   delapan puluh ribu sembilan puluh ribu"
            RatusRibu = "seratus ribu       dua ratus ribu     tiga ratus ribu    empat ratus ribu   lima ratus ribu    enam ratus ribu    tujuh ratus ribu   delapan ratus ribu sembilan ratus ribu"

            If (Len(Amt) <> 3) Or (IsNumeric(Amt) = False) Then TriGroupText = "" : Exit Function
            Digit1 = Val(Left(Amt, 1))
            If Digit1 = 1 Then
                Out = "seratus"
            ElseIf Digit1 > 0 Then
                Out = Trim$(Mid(Ones, Digit1 * 8 - 7, 8)) & " ratus"
            End If
            Digit1 = Val(Right$(Amt, 2))
            Select Case Digit1
                Case 200000 To 999999, 100000
                    Digit10000 = Digit1 \ 10000
                    Digit1000 = Digit1 \ 1000
                    Digit100 = Digit1 \ 100
                    Digit10 = Digit1 \ 10
                    Digit1 = Digit1 - (Digit10 * 10)
                    If Digit100000 Then Out = Out & " " & Trim$(Mid(RatusRibu, Digit100000 * 14 - 13, 14))
                    If Digit10000 Then Out = Out & " " & Trim$(Mid(PuluhRibu, Digit10000 * 14 - 13, 14))
                    If Digit1000 Then Out = Out & " " & Trim$(Mid(Ribu, Digit1000 * 14 - 13, 14))
                    If Digit100 Then Out = Out & " " & Trim$(Mid(Ratus, Digit100 * 14 - 13, 14))
                    If Digit10 Then Out = Out & " " & Trim$(Mid(Tens, Digit10 * 14 - 13, 14))
                    If Digit1 Then Out = Out & " " & Trim$(Mid(Ones, Digit1 * 8 - 7, 8))
                Case 20000 To 99999, 10000
                    Digit1000 = Digit1 \ 10000
                    Digit1000 = Digit1 \ 1000
                    Digit100 = Digit1 \ 100
                    Digit10 = Digit1 \ 10
                    Digit1 = Digit1 - (Digit10 * 10)
                    If Digit1000 Then Out = Out & " " & Trim$(Mid(PuluhRibu, Digit10000 * 14 - 13, 14))
                    If Digit1000 Then Out = Out & " " & Trim$(Mid(Ribu, Digit1000 * 14 - 13, 14))
                    If Digit100 Then Out = Out & " " & Trim$(Mid(Ratus, Digit100 * 14 - 13, 14))
                    If Digit10 Then Out = Out & " " & Trim$(Mid(Tens, Digit10 * 14 - 13, 14))
                    If Digit1 Then Out = Out & " " & Trim$(Mid(Ones, Digit1 * 8 - 7, 8))
                Case 2000 To 9999, 1000
                    Digit1000 = Digit1 \ 1000
                    Digit100 = Digit1 \ 100
                    Digit10 = Digit1 \ 10
                    Digit1 = Digit1 - (Digit10 * 10)
                    If Digit1000 Then Out = Out & " " & Trim$(Mid(Ribu, Digit1000 * 14 - 13, 14))
                    If Digit100 Then Out = Out & " " & Trim$(Mid(Ratus, Digit100 * 14 - 13, 14))
                    If Digit10 Then Out = Out & " " & Trim$(Mid(Tens, Digit10 * 14 - 13, 14))
                    If Digit1 Then Out = Out & " " & Trim$(Mid(Ones, Digit1 * 8 - 7, 8))
                Case 200 To 999, 100
                    Digit100 = Digit1 \ 100
                    Digit10 = Digit1 \ 10
                    Digit1 = Digit1 - (Digit10 * 10)
                    If Digit100 Then Out = Out & " " & Trim$(Mid(Ratus, Digit10 * 14 - 13, 14))
                    If Digit10 Then Out = Out & " " & Trim$(Mid(Tens, Digit10 * 14 - 13, 14))
                    If Digit1 Then Out = Out & " " & Trim$(Mid(Ones, Digit1 * 8 - 7, 8))
                Case 20 To 99, 10
                    Digit10 = Digit1 \ 10
                    Digit1 = Digit1 - (Digit10 * 10)
                    If Digit10 Then Out = Out & " " & Trim$(Mid(Tens, Digit10 * 14 - 13, 14))
                    If Digit1 Then Out = Out & " " & Trim$(Mid(Ones, Digit1 * 8 - 7, 8))
                    If GRP = 3 Then Out = Out & " " & "ribu"
                Case 1 To 9
                    Out = Out & " " & Trim$(Mid(Ones, Digit1 * 8 - 7, 8))
                    If GRP = 3 Then Out = Out & " " & "ribu"
                Case 11 To 19
                    Out = Out & " " & Trim$(Mid(Teens, (Digit1 - 11) * 14 + 1, 14))
                    If GRP = 3 Then Out = Out & " " & "ribu"
            End Select
        ElseIf CurName = "US" Or CurName = "USD" Or CurName = "SGD" Or CurName = "EURO" Then
            Ones = "one  two  threefour five six  seveneightnine "
            Tens = "ten    twenty thirty forty  fifty  sixty  seventyeighty ninety "
            Teens = "eleven   twelve   thirteen fourteen fifteen  sixteen  seventeeneighteen nineteen "

            If (Len(Amt) <> 3) Or (IsNumeric(Amt) = False) Then TriGroupText = "" : Exit Function
            Digit1 = Val(Left(Amt, 1))
            '        Digit1 = Val(Right$(Amt, 1))
            If Digit1 Then
                Out = Trim$(Mid(Ones, Digit1 * 5 - 4, 5)) & " hundred"
            ElseIf Val(Right$(Amt, 2)) = "01" Then
                Out = Trim$(Mid(Ones, 1, 5)) & " thousand"  'aslinya hundred
            End If
            Digit1 = Val(Right$(Amt, 2))
            Select Case Digit1
                Case 20 To 99, 10
                    Digit10 = Digit1 \ 10
                    Digit1 = Digit1 - (Digit10 * 10)
                    If Digit10 Then Out = Out & " " & Trim$(Mid(Tens, Digit10 * 7 - 6, 7))
                    If Digit1 Then Out = Out & " " & Trim$(Mid(Ones, Digit1 * 5 - 4, 5))
                    If GRP = 3 Then Out = Out & " " & "thousand"
                Case 1 To 9
                    If Right$(Amt, 1) <> 1 Then
                        Out = Out & " " & Trim$(Mid(Ones, Digit1 * 5 - 4, 5))
                    End If
                    If GRP = 3 Then Out = Out & " " & "thousand"
                Case 11 To 19
                    Out = Out & " " & Trim$(Mid(Teens, (Digit1 - 11) * 9 + 1, 9))
                    If GRP = 3 Then Out = Out & " " & "thousand"
            End Select
        End If
        TriGroupText = Trim$(Out) & " "
    End Function


End Class
