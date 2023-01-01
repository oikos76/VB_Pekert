Public Class Form_Kasir_Bayar
    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        If Trim(Tunai.Text) <> 0 Then
            NoKartuDebet.Text = ""
            Debet.Text = 0
            NoKartuKredit.Text = ""
            Kredit.Text = 0
        End If
        If Trim(Debet.Text) <> 0 Then
            Tunai.Text = 0
            Kredit.Text = 0
            NoKartuKredit.Text = ""
            If Trim(NoKartuDebet.Text) = "" Then
                MsgBox("Pembayaran dengan Kartu DEBET, harus mengisi no kartunya !", vbCritical + vbOKOnly, ".:No Kartu belum di isi !")
                NoKartuDebet.Focus()
                Exit Sub
            End If
        End If
        If Trim(Kredit.Text) <> 0 Then
            Tunai.Text = 0
            Debet.Text = 0
            NoKartuDebet.Text = ""
            If Kredit.Text = Nothing Then
                Kredit.Text = "0"
            Else
                If Trim(PsCharge.Text) = "" Then PsCharge.Text = 0
                Charge.Text = (PsCharge.Text * 1 / 100) * (Kredit.Text * 1)
            End If
            If Trim(NoKartuKredit.Text) = "" Then
                MsgBox("Pembayaran dengan Kartu Kredit, harus mengisi no kartunya !", vbCritical + vbOKOnly, ".:No Kartu belum di isi !")
                NoKartuKredit.Focus()
                Exit Sub
            End If
        End If
        If Trim(Tunai.Text) = 0 And Trim(Debet.Text) = 0 And Trim(Kredit.Text) = 0 Then
            MsgBox("Bayar dulu ya....", vbCritical + vbOKOnly, ".:Tidak terima hutang!")
            Tunai.Focus()
            Exit Sub
        End If
        cmdSimpan.Enabled = False
        With Form_KasirAG
            .PsDisc.Text = PsDisc.Text
            .Discount.Text = Discount.Text
            .Pembulatan.Text = Pembulatan.Text
            .TotalSales.Text = TotalSales.Text
            .Tunai.Text = Tunai.Text
            .Debet.Text = Debet.Text
            .NoKartuDebet.Text = NoKartuDebet.Text
            .Kredit.Text = Kredit.Text
            .NoKartuKredit.Text = NoKartuKredit.Text
            .PsCharge.Text = PsCharge.Text
            .Charge.Text = Charge.Text
            .Kembali.Text = Kembali.Text
            .SimpanKasir()
        End With
        cmdSimpan.Enabled = True
        Me.Close()
    End Sub

    Private Sub HitungKembali()
        If Trim(PsDisc.Text) = "" Then PsDisc.Text = 0
        If Trim(Discount.Text) = "" Then Discount.Text = 0
        If Trim(Pembulatan.Text) = "" Then Pembulatan.Text = 0
        If Trim(TotalSales.Text) = "" Then TotalSales.Text = 0
        If Trim(Tunai.Text) = "" Then Tunai.Text = 0
        If Trim(Debet.Text) = "" Then Debet.Text = 0
        If Trim(Kredit.Text) = "" Then Kredit.Text = 0
        If Trim(PsDisc.Text) <> 0 Then
            Discount.Text = (PsDisc.Text * 1 / 100) * (SubTotal.Text * 1)
        End If
        TotalSales.Text = Format((SubTotal.Text * 1) - (Discount.Text * 1), "###,##0")
        If Pembulatan.Text <> 0 Then
            TotalSales.Text = Format((SubTotal.Text * 1) - (Discount.Text * 1) - (Pembulatan.Text * 1), "###,##0")
        End If
        If (Trim(Tunai.Text) <> "0") Then
            Kembali.Text = Format((Tunai.Text * 1) - (TotalSales.Text * 1), "###,##0")
        ElseIf (Trim(Debet.Text)) <> "0" Then
            Kembali.Text = Format(0, "###,##0")
        ElseIf (Trim(Kredit.Text)) <> "0" Then
            Kembali.Text = Format(0, "###,##0")
        End If
    End Sub

    Private Sub PsDisc_TextChanged(sender As Object, e As EventArgs) Handles PsDisc.TextChanged
        If Trim(PsDisc.Text) = "" Then PsDisc.Text = 0
        If IsNumeric(PsDisc.Text) Then
            Dim temp As Double = PsDisc.Text
            PsDisc.Text = Format(temp, "###,##0")
            PsDisc.SelectionStart = PsDisc.TextLength
        End If
    End Sub

    Private Sub PsDisc_LostFocus(sender As Object, e As EventArgs) Handles PsDisc.LostFocus
        If PsDisc.Text = Nothing Then
            PsDisc.Text = "0"
        Else
            If Trim(PsDisc.Text) = "0" Then Discount.Text = 0
            HitungKembali()
        End If
    End Sub

    Private Sub PsDisc_GotFocus(sender As Object, e As EventArgs) Handles PsDisc.GotFocus
        With PsDisc
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub PsDisc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PsDisc.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If PsDisc.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            Discount.Focus()
            HitungKembali()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Discount_TextChanged(sender As Object, e As EventArgs) Handles Discount.TextChanged
        If Trim(Discount.Text) = "" Then Discount.Text = 0
        If IsNumeric(Discount.Text) Then
            Dim temp As Double = Discount.Text
            Discount.Text = Format(temp, "###,##0")
            Discount.SelectionStart = Discount.TextLength
        End If
    End Sub

    Private Sub Discount_LostFocus(sender As Object, e As EventArgs) Handles Discount.LostFocus
        If Discount.Text = Nothing Then
            Discount.Text = "0"
        Else
            HitungKembali()
        End If
    End Sub

    Private Sub Discount_GotFocus(sender As Object, e As EventArgs) Handles Discount.GotFocus
        With Discount
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Discount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Discount.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            HitungKembali()
            Tunai.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Pembulatan_TextChanged(sender As Object, e As EventArgs) Handles Pembulatan.TextChanged
        If Trim(Pembulatan.Text) = "" Then Pembulatan.Text = 0
        If IsNumeric(Pembulatan.Text) Then
            Dim temp As Double = Pembulatan.Text
            Pembulatan.Text = Format(temp, "###,##0")
            Pembulatan.SelectionStart = Pembulatan.TextLength ' - 3
        End If
    End Sub

    Private Sub Pembulatan_GotFocus(sender As Object, e As EventArgs) Handles Pembulatan.GotFocus
        With Pembulatan
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub Pembulatan_LostFocus(sender As Object, e As EventArgs) Handles Pembulatan.LostFocus
        If Pembulatan.Text = Nothing Then
            Pembulatan.Text = "0"
        Else
            HitungKembali()
        End If
    End Sub

    Private Sub Pembulatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pembulatan.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Pembulatan.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            HitungKembali()
            Tunai.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Tunai_LostFocus(sender As Object, e As EventArgs) Handles Tunai.LostFocus
        If Tunai.Text = Nothing Then
            Tunai.Text = "0"
        Else
            Kredit.Text = 0
            NoKartuKredit.Text = ""
            Debet.Text = 0
            NoKartuDebet.Text = ""
            HitungKembali()
        End If
        'PsDisc.TextAlign = HorizontalAlignment.Right
    End Sub
    Private Sub Tunai_GotFocus(sender As Object, e As EventArgs) Handles Tunai.GotFocus
        With Tunai
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            '    .TextAlign = HorizontalAlignment.Left
        End With
    End Sub

    Private Sub Tunai_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tunai.KeyPress

        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Tunai.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            HitungKembali()
            cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If

    End Sub

    Private Sub Tunai_TextChanged(sender As Object, e As EventArgs) Handles Tunai.TextChanged
        If Trim(Tunai.Text) = "" Then Tunai.Text = 0
        If IsNumeric(Tunai.Text) Then
            Dim temp As Double = Tunai.Text
            Tunai.Text = Format(temp, "###,##0")
            Tunai.SelectionStart = Tunai.TextLength
        End If
    End Sub

    Private Sub Debet_TextChanged(sender As Object, e As EventArgs) Handles Debet.TextChanged
        If Trim(Debet.Text) = "" Then Debet.Text = 0
        If IsNumeric(Debet.Text) Then
            Dim temp As Double = Debet.Text
            Debet.Text = Format(temp, "###,##0")
            Debet.SelectionStart = Debet.TextLength
        End If
    End Sub


    Private Sub Debet_GotFocus(sender As Object, e As EventArgs) Handles Debet.GotFocus
        With Debet
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            '    .TextAlign = HorizontalAlignment.Left
        End With
    End Sub

    Private Sub Debet_LostFocus(sender As Object, e As EventArgs) Handles Debet.LostFocus
        If Debet.Text = Nothing Then
            Debet.Text = "0"
        Else
            'Kredit.Text = 0
            'NoKartuKredit.Text = ""
            'Tunai.Text = 0
            HitungKembali()
        End If
    End Sub
    Private Sub Debet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Debet.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Debet.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            'Debet.TextAlign = HorizontalAlignment.Right
            HitungKembali()
            NoKartuDebet.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Kredit_TextChanged(sender As Object, e As EventArgs) Handles Kredit.TextChanged
        If Trim(Kredit.Text) = "" Then Kredit.Text = 0
        If IsNumeric(Kredit.Text) Then
            Dim temp As Double = Kredit.Text
            Kredit.Text = Format(temp, "###,##0")
            Kredit.SelectionStart = Kredit.TextLength
        End If
    End Sub

    Private Sub Kredit_GotFocus(sender As Object, e As EventArgs) Handles Kredit.GotFocus
        With Kredit
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .TextAlign = HorizontalAlignment.Left
        End With
    End Sub

    Private Sub Kredit_LostFocus(sender As Object, e As EventArgs) Handles Kredit.LostFocus
        If Kredit.Text = Nothing Then
            Kredit.Text = "0"
        Else
            Charge.Text = (PsCharge.Text * 1 / 100) * (Kredit.Text * 1)
            'Debet.Text = 0
            'NoKartuDebet.Text = ""
            'Tunai.Text = 0
            HitungKembali()
        End If
        'Kredit.TextAlign = HorizontalAlignment.Right
    End Sub

    Private Sub Kredit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kredit.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Kredit.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If PsCharge.Text = "" Then PsCharge.Text = 0
            If Kredit.Text = "" Then Kredit.Text = 0
            Charge.Text = (PsCharge.Text * 1 / 100) * (Kredit.Text * 1)
            'Kredit.TextAlign = HorizontalAlignment.Right
            HitungKembali()
            NoKartuKredit.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub NoKartuDebet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoKartuDebet.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub


    Private Sub NoKartuKredit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoKartuKredit.KeyPress
        If e.KeyChar = Chr(13) Then
            cmdSimpan.Focus()
        End If
    End Sub

    Private Sub PsCharge_TextChanged(sender As Object, e As EventArgs) Handles PsCharge.TextChanged
        If Trim(PsCharge.Text) = "" Then PsCharge.Text = 0
        If IsNumeric(PsCharge.Text) Then
            Dim temp As Double = PsCharge.Text
            PsCharge.Text = Format(temp, "###,##0.00")
            ' psCharge.SelectionStart = psCharge.TextLength - 3
        End If
    End Sub

    Private Sub PsCharge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PsCharge.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If PsCharge.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If PsCharge.Text = "" Then PsCharge.Text = 0
            If Kredit.Text = "" Then Kredit.Text = 0
            Charge.Text = (PsCharge.Text * 1 / 100) * (Kredit.Text * 1)
            NoKartuKredit.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub PsCharge_LostFocus(sender As Object, e As EventArgs) Handles PsCharge.LostFocus
        If PsCharge.Text = Nothing Then
            PsCharge.Text = "0"
        Else
            If PsCharge.Text = "" Then PsCharge.Text = 0
            If Kredit.Text = "" Then Kredit.Text = 0
            Charge.Text = (PsCharge.Text * 1 / 100) * (Kredit.Text * 1)
            HitungKembali()
        End If
        'PsCharge.TextAlign = Right
    End Sub

    Private Sub PsCharge_GotFocus(sender As Object, e As EventArgs) Handles PsCharge.GotFocus
        With PsCharge
            .SelectionStart = 0
            .SelectionLength = .TextLength
            '  .TextAlign = Left
        End With
    End Sub

    Private Sub Form_Kasir_Bayar_Load(sender As Object, e As EventArgs) Handles Me.Load
        Tunai.Focus()
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub SubTotal_TextChanged(sender As Object, e As EventArgs) Handles SubTotal.TextChanged

    End Sub

    Private Sub SubTotal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SubTotal.KeyPress
        If e.KeyChar = Chr(13) Then
            Tunai.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub



    Private Sub Charge_LostFocus(sender As Object, e As EventArgs) Handles Charge.LostFocus
        If Charge.Text = Nothing Then
            Charge.Text = "0"
        Else
            HitungKembali()
        End If
    End Sub
End Class