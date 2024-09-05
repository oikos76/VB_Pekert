Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb

Public Class Form_ProsesAkhirBulan
    Protected Dt As DataTable
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean

    Private CN As SqlConnection
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String

    Private Sub Form_ProsesAkhirBulan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        status.Text = ""
        Dim MsgSQL As String, PGL As String = ""
        MsgSQL = "Select top 1 Periode From m_saldoawalcompany " &
            "order by right(periode,4) + left(periode,2) desc "
        PGL = Proses.ExecuteSingleStrQuery(MsgSQL)
        chkPersediaan.Visible = False 'True
        chkPersediaan.Checked = False
        xGL.Value = DateAdd("m", 1, CDate(PGL))
        status.Text = ""
        'If Format(xGL.Value, "MM") = "06" Or Format(xGL.Value, "MM") = "12" Then
        '    chkPersediaan.Checked = 1
        'End If
    End Sub

    Private Sub cmdProses_Click(sender As Object, e As EventArgs) Handles cmdProses.Click
        If Format(xGL.Value, "yyyyMM") <= "202212" Then
            MsgBox("Saldo Awal ada di bulan Dec 2022!" & vbCrLf &
                "Periode yang bisa di proses mulai dari bulan Jan 2023", vbCritical + vbOKOnly, ".:Salah periode!")
            xGL.Focus()
            Exit Sub
        End If
        cmdProses.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Dim tPeriode1 As String, tPeriode2 As String, MsgSQL As String, tID As String = "",
            rsc As New DataTable, rs01 As New DataTable, rs02 As New DataTable, rs03 As New DataTable

        tPeriode1 = Format(DateAdd("m", -1, xGL.Value), "MM-yyyy")
        tPeriode2 = Format(xGL.Value, "yyyyMM")
        MsgSQL = "Select Periode From M_SaldoAwalCompany " &
            "Where Periode = '" & Format(xGL.Value, "MM-yyyy") & "' " &
            "  and aktifYN = 'Y'  "
        rsc = Proses.ExecuteQuery(MsgSQL)
        If rsc.Rows.Count() <> 0 Then
            If rsc.Rows(0) !Periode = "12-2022" Then
                MsgBox("Sory ye... periode Desember 2022 tidak dapat di proses ulang!", vbCritical, ".:ERROR!")
                Exit Sub
            End If
            If MsgBox("Periode " & rsc.Rows(0) !Periode & " Sudah di closing !" & vbCrLf &
                      "Apakah Anda ingin Proses ulang ?", vbExclamation + vbYesNo, ".:Warning!") = vbYes Then
                MsgSQL = "Update M_SaldoAwalCompany Set Saldo = 0 " &
                    "Where AktifYN = 'Y' " &
                    "  And Periode = '" & rsc.Rows(0) !Periode & "' "

                MsgSQL = "DELETE M_SaldoAwalCompany " &
                    "Where AktifYN = 'Y' " &
                    "  And Periode = '" & rsc.Rows(0) !Periode & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Else
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If
        Dim tSaldoAwal As Double = 0, tDebet As Double = 0, tKredit As Double = 0,
            tSaldoAkhir As Double = 0
        MsgSQL = "Select * From m_Perkiraan " &
            "Where AktifYN = 'Y' " & ' AND  NO_PERKIRAAN like '40.10.01.01%'
            "Order By No_Perkiraan "
        rsc = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To rsc.Rows.Count - 1
            Application.DoEvents()
            tSaldoAkhir = 0
            tSaldoAwal = 0
            tDebet = 0
            tKredit = 0

            status.Text = Trim(rsc.Rows(i) !no_PERKIRAAN) + " " + rsc.Rows(i) !NM_PERKIRAAN
            MsgSQL = "Select Saldo From M_SaldoAwalCompany " &
                "Where AktifYN = 'Y' " &
                "  And COA = '" & rsc.Rows(i) !NO_PERKIRAAN & "' " &
                "  And Periode = '" & tPeriode1 & "' "
            If Format(xGL.Value, "MM") = "01" Then
                tSaldoAwal = 0
            Else
                tSaldoAwal = Proses.ExecuteSingleDblQuery(MsgSQL)
            End If

            'If Trim(rsc.Rows(i) !no_PERKIRAAN) = "40.10.01.01.9999B" Then
            '    Debug.Print(rsc.Rows(i) !no_PERKIRAAN)
            'End If

            MsgSQL = "Select Sum(Debet) tDebet From T_Jurnal " &
            "Where AKtifYN = 'Y' " &
            "  And AccountCode = '" & rsc.Rows(i) !no_PERKIRAAN & "' " &
            "  And Convert(Char(6), tanggal, 112) = '" & tPeriode2 & "' "
            tDebet = Proses.ExecuteSingleDblQuery(MsgSQL)

            MsgSQL = "Select Sum(Kredit) tKRedit From T_Jurnal " &
                "Where AKtifYN = 'Y' " &
                "  And AccountCode = '" & rsc.Rows(i) !no_PERKIRAAN & "' " &
                "  And Convert(Char(6), Tanggal, 112) = '" & tPeriode2 & "' "
            tKredit = Proses.ExecuteSingleDblQuery(MsgSQL)
            'If tKredit > 0 Then
            '    Debug.Print(tKredit)
            'End If
            If Mid(rsc.Rows(i) !no_PERKIRAAN, 1, 11) = "40.10.01.01" Or
                Mid(rsc.Rows(i) !no_PERKIRAAN, 1, 8) = "80.10.01" Or
                Trim(rsc.Rows(i) !no_PERKIRAAN) = "90.10.01.02.001" Then
                tSaldoAkhir = tSaldoAwal + tKredit - tDebet
            ElseIf Mid(rsc.Rows(i) !no_PERKIRAAN, 1, 11) = "50.10.01.01" Then
                tSaldoAkhir = tSaldoAwal + tDebet - tKredit
            Else
                tSaldoAkhir = tSaldoAwal + tDebet - tKredit
            End If

            MsgSQL = "Select convert(Char(4), GetDate(), 112) Thn, isnull(Max(right(IDRec,6)),0) + 100000001 IDRec " &
            " From m_SaldoAwalCompany " &
            "Where left(idrec,4) = '" & Format(Now(), "yyyy") & "'  "
            rs01 = Proses.ExecuteQuery(MsgSQL)
            If rs01.Rows.Count <> 0 Then
                tID = rs01.Rows(0) !thn + Microsoft.VisualBasic.Right(rs01.Rows(0) !IDRec, 6)
            End If


            'If Microsoft.VisualBasic.Left(rsc.Rows(i) !no_PERKIRAAN, 8) = "10.10.20" And chkPersediaan.Checked = 1 Then
            '    tSaldoAkhir = 0
            'Else
            MsgSQL = "Select IDRec, Periode, COA " &
                "From M_SaldoAwalCompany " &
                "Where Periode = '" & Format(xGL.Value, "MM-yyyy") & "' " &
                "  And COA = '" & rsc.Rows(i) !no_PERKIRAAN & "' "
            rs01 = Proses.ExecuteQuery(MsgSQL)
            If rs01.Rows.Count <> 0 Then
                'Debug.Print tSaldoAkhir
                MsgSQL = "Update m_SaldoAwalCompany Set " &
                    " Saldo = " & tSaldoAkhir * 1 & " " &
                    "Where Periode = '" & Format(xGL.Value, "MM-yyyy") & "' " &
                    "  And COA = '" & Trim(rsc.Rows(i) !no_PERKIRAAN) & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            Else
                MsgSQL = "INSERT INTO m_SaldoAwalCompany(IDRec, Periode, COA, " &
                    "Nama, Saldo, Area, AktifYN, LastUPD, UserId) " &
                    "VALUES ('" & tID & "', '" & Format(xGL.Value, "MM-yyyy") & "', " &
                    " '" & Trim(rsc.Rows(i) !no_PERKIRAAN) & "', '" & Replace(rsc.Rows(i) !NM_PERKIRAAN, "'", "`") & "', " &
                    " " & tSaldoAkhir * 1 & ", '" & FrmMenuUtama.Kode_Toko.Text & "', " &
                    " 'Y', GetDate(), '" & UserID & "')"
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            'End If
        Next i

        Dim idRPT As String = ""
        Randomize()
        Me.Cursor = Cursors.WaitCursor
        idRPT = Microsoft.VisualBasic.Left(Replace(Trim(Rnd(10000) * 100000), ".", Microsoft.VisualBasic.Left(Trim(UserID), 5)) & "CLOSE", 30)
        status.Text = idRPT

        Rpt_LabaRugi.HitungLabaRugi(idRPT, xGL.Value)

        MsgSQL = "SELECT kodegl, akhir " &
            " FROM tmp_RPT_LabaRugi INNER JOIN tmp_RPT_Laba_Rugi ON urut = nourut " &
            "Where IDRpt = '" & idRPT & "' " &
            "  AND tmp_RPT_LabaRugi.tUrut in ('0800', '0900') " &
            "  AND KodeGL <> '' " &
            "ORDER by KodeGL"
        rsc = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To rsc.Rows.Count - 1
            Application.DoEvents()
            MsgSQL = "Update m_SaldoAwalCompany Set " &
                   " Saldo = " & rsc.Rows(i) !Akhir * 1 & " " &
                   "Where Periode = '" & Format(xGL.Value, "MM-yyyy") & "' " &
                   "  And COA = '" & Trim(rsc.Rows(i) !KodeGL) & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        Next i


        MsgSQL = "Update T_Jurnal Set ClosingYN = 'Y' " &
        "Where Convert(Char(6), tanggal, 112) = '" & tPeriode2 & "' "
        Proses.ExecuteNonQuery(MsgSQL)

        status.Text = "Proses Closing Periode " & Format(xGL.Value, "MM-yyyy") & " Selesai!"
        MsgBox(status.Text, vbInformation + vbOKOnly, ".: Success!")

        Proses.CloseConn()
        Me.Cursor = Cursors.Default
        cmdProses.Enabled = True
        status.Text = ""
    End Sub
End Class