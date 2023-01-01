Public Class Form_Kasir_Edit
    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        With Form_KasirAG

            .DGRequest.Rows(RowIndex.Text).Cells(0).Value = KodeBrg.Text
            .DGRequest.Rows(RowIndex.Text).Cells(1).Value = NamaBrg.Text
            .DGRequest.Rows(RowIndex.Text).Cells(2).Value = QTY.Text
            .DGRequest.Rows(RowIndex.Text).Cells(3).Value = satuan.Text
            .DGRequest.Rows(RowIndex.Text).Cells(4).Value = Pack.Text
            .DGRequest.Rows(RowIndex.Text).Cells(5).Value = cmbSatuanB.Text
            .DGRequest.Rows(RowIndex.Text).Cells(6).Value = HargaSatuan.Text
            .DGRequest.Rows(RowIndex.Text).Cells(7).Value = PsDisc1.Text
            .DGRequest.Rows(RowIndex.Text).Cells(8).Value = Disc.Text
            .DGRequest.Rows(RowIndex.Text).Cells(9).Value = SubTotal.Text
            .DGRequest.Rows(RowIndex.Text).Cells(12).Value = Flag.Text
            .DGRequest.Rows(RowIndex.Text).Cells(13).Value = HargaSatuan_Asli.Text
            .DGRequest.Rows(RowIndex.Text).Cells(14).Value = HargaModal.Text
        End With
        Me.Close()
    End Sub

    Private Sub hitungSubTotal()
        Dim disc1 As Double = 0, harga1 As Double = 0
        Dim xQTY As Double = 0
        If Trim(QTY.Text) = "" Then QTY.Text = 1
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If Trim(HargaSatuan_Asli.Text) = "" Then HargaSatuan_Asli.Text = 0
        If Trim(PsDisc1.Text) = "" Then PsDisc1.Text = 0
        If Trim(Disc.Text) = "" Then Disc.Text = 0
        Dim value As Integer = cmbSatuanB.SelectedIndex
        Select Case value
            Case 0
                xQTY = QTY.Text * 1
            Case 1
                xQTY = Pack.Text * 1
            Case 2
                xQTY = Pack.Text * 1
            Case Else
                xQTY = QTY.Text * 1
        End Select
        If Trim(PsDisc1.Text) <> 0 Then
            disc1 = (PsDisc1.Text * 1 / 100) * (HargaSatuan.Text * 1)
            Disc.Text = Format(disc1, "###,##0")
        Else
            disc1 = Disc.Text * 1
        End If
        harga1 = xQTY * ((HargaSatuan.Text * 1) - disc1)
        If xQTY * (HargaSatuan_Asli.Text * 1) <> harga1 Then
            Flag.Text = "*"
        Else
            Flag.Text = ""
        End If
        SubTotal.Text = Format(harga1, "###,##0")
    End Sub

    Private Sub cmbSatuanB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSatuanB.SelectedIndexChanged
        Dim value As Integer = cmbSatuanB.SelectedIndex
        Select Case value
            Case 0
                HargaSatuan.Text = harga1.Text
                QTY.Text = Pack.Text
            Case 1
                HargaSatuan.Text = harga2.Text
                QTY.Text = Format((Pack.Text * 1) * (IsiSatT.Text * 1), "###,##0")
            Case 2
                HargaSatuan.Text = harga3.Text
                QTY.Text = Format((Pack.Text * 1) * (IsiSatB.Text * 1), "###,##0")
            Case Else
                HargaSatuan.Text = harga1.Text
                QTY.Text = Pack.Text
        End Select
        hitungSubTotal()
        HargaSatuan_Asli.Text = HargaSatuan.Text
        If satuan.Text = cmbSatuanB.Text Then
            QTY.Text = Pack.Text
        End If
    End Sub

    Private Sub cmbSatuanB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSatuanB.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbSatuanB_SelectedIndexChanged(sender, New EventArgs)
            HargaSatuan.Focus()
        End If
    End Sub
    Private Sub QTY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QTY.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If QTY.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            hitungSubTotal()
            If IsNumeric(QTY.Text) Then
                Dim temp As Double = QTY.Text
                QTY.Text = Format(temp, "###,##0.00")
                QTY.SelectionStart = QTY.TextLength
            Else
                QTY.Text = 0
            End If
            Pack.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub QTY_TextChanged(sender As Object, e As EventArgs) Handles QTY.TextChanged
        If Trim(QTY.Text) = "" Then QTY.Text = 1
        If IsNumeric(QTY.Text) Then
            Dim temp As Double = QTY.Text
            QTY.Text = Format(temp, "###,##0")
            QTY.SelectionStart = QTY.TextLength
        End If
    End Sub
    Private Sub QTY_GotFocus(sender As Object, e As EventArgs) Handles QTY.GotFocus
        With QTY
            .SelectionStart = 0
            .SelectionLength = .TextLength
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
    End Sub
    Private Sub QTY_LostFocus(sender As Object, e As EventArgs) Handles QTY.LostFocus
        If QTY.Text = Nothing Then
            QTY.Text = "1"
            QTY.ForeColor = Color.Gray
            QTY.BackColor = Color.LightGoldenrodYellow
        Else
            If Trim(satuan.Text) = Trim(cmbSatuanB.Text) Then
                Pack.Text = QTY.Text
            ElseIf Trim(satuan.Text) <> Trim(cmbSatuanB.Text) Then
                Dim value As Integer = cmbSatuanB.SelectedIndex
                Select Case value
                    Case 0
                        HargaSatuan.Text = harga1.Text
                        Pack.Text = QTY.Text
                    Case 1
                        HargaSatuan.Text = harga2.Text
                        Pack.Text = Format((QTY.Text * 1) / (IsiSatT.Text * 1), "###,##0.00")
                    Case 2
                        HargaSatuan.Text = harga3.Text
                        Pack.Text = Format((QTY.Text * 1) / (IsiSatB.Text * 1), "###,##0.00")
                    Case Else
                        HargaSatuan.Text = harga1.Text
                        Pack.Text = QTY.Text
                End Select
                hitungSubTotal()
            End If
        End If
    End Sub

    Private Sub Pack_TextChanged(sender As Object, e As EventArgs) Handles Pack.TextChanged
        If Trim(Pack.Text) = "" Then Pack.Text = 1
        If IsNumeric(Pack.Text) Then
            Dim temp As Double = Pack.Text
            'Pack.Text = Format(temp, "###,##0.00")
            Pack.SelectionStart = Pack.TextLength
        End If
    End Sub
    Private Sub Pack_LostFocus(sender As Object, e As EventArgs) Handles Pack.LostFocus
        If Pack.Text = Nothing Then
            Pack.Text = "1"
            Pack.ForeColor = Color.Gray
            Pack.BackColor = Color.LightGoldenrodYellow
        Else
            If Trim(satuan.Text) = Trim(cmbSatuanB.Text) Then
                QTY.Text = Pack.Text
            ElseIf Trim(satuan.Text) <> Trim(cmbSatuanB.Text) Then
                Dim value As Integer = cmbSatuanB.SelectedIndex
                Select Case value
                    Case 0
                        HargaSatuan.Text = harga1.Text
                        QTY.Text = Pack.Text
                    Case 1
                        HargaSatuan.Text = harga2.Text
                        QTY.Text = Format((Pack.Text * 1) * (IsiSatT.Text * 1), "###,##0.00")
                    Case 2
                        HargaSatuan.Text = harga3.Text
                        QTY.Text = Format((Pack.Text * 1) * (IsiSatB.Text * 1), "###,##0.00")
                    Case Else
                        HargaSatuan.Text = harga1.Text
                        QTY.Text = Pack.Text
                End Select
                hitungSubTotal()
            End If
        End If
    End Sub
    Private Sub Pack_GotFocus(sender As Object, e As EventArgs) Handles Pack.GotFocus
        With Pack
            .SelectionStart = 0
            .SelectionLength = .TextLength
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
    End Sub

    Private Sub Pack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pack.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Pack.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            hitungSubTotal()
            If IsNumeric(Pack.Text) Then
                Dim temp As Double = Pack.Text
                Pack.Text = Format(temp, "###,##0.00")
                Pack.SelectionStart = Pack.TextLength
            Else
                Pack.Text = 0
            End If
            cmbSatuanB.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub PsDisc1_TextChanged(sender As Object, e As EventArgs) Handles PsDisc1.TextChanged
        If Trim(PsDisc1.Text) = "" Then PsDisc1.Text = 0
        If Trim(SubTotal.Text) = "" Then SubTotal.Text = 0
        If IsNumeric(PsDisc1.Text) Then
            Dim temp As Double = PsDisc1.Text
            PsDisc1.Text = Format(temp, "###,##0")
            Disc.Text = Format(temp * 1 / 100 * (SubTotal.Text * 1), "###,##0")
            PsDisc1.SelectionStart = PsDisc1.TextLength
        End If
    End Sub
    Private Sub PsDisc1_GotFocus(sender As Object, e As EventArgs) Handles PsDisc1.GotFocus
        With PsDisc1
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .ForeColor = Color.Black
            .BackColor = Color.White
            Disc.ForeColor = Color.Black
            Disc.BackColor = Color.White
        End With
    End Sub
    Private Sub PsDisc1_LostFocus(sender As Object, e As EventArgs) Handles PsDisc1.LostFocus
        If PsDisc1.Text = Nothing Then
            PsDisc1.Text = "0"
            PsDisc1.ForeColor = Color.Gray
            PsDisc1.BackColor = Color.LightGoldenrodYellow
        Else
            hitungSubTotal()
        End If
    End Sub

    Private Sub PsDisc1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PsDisc1.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If PsDisc1.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If PsDisc1.Text <> 0 Then
                hitungSubTotal()
                cmdSimpan.Focus()
            Else
                Disc.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Disc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Disc.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If PsDisc1.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            hitungSubTotal()
            cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Disc_TextChanged(sender As Object, e As EventArgs) Handles Disc.TextChanged
        If Trim(Disc.Text) = "" Then Disc.Text = 0
        If IsNumeric(Disc.Text) Then
            Dim temp As Double = Disc.Text
            Disc.Text = Format(temp, "###,##0")
            Disc.SelectionStart = Disc.TextLength
        End If
    End Sub

    Private Sub Disc_LostFocus(sender As Object, e As EventArgs) Handles Disc.LostFocus
        If Disc.Text = Nothing Then
            Disc.Text = "0"
            Disc.ForeColor = Color.Gray
            Disc.BackColor = Color.LightGoldenrodYellow
        Else
            hitungSubTotal()
        End If
    End Sub

    Private Sub Disc_GotFocus(sender As Object, e As EventArgs) Handles Disc.GotFocus
        With Disc
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
    End Sub

    Private Sub HargaSatuan_TextChanged(sender As Object, e As EventArgs) Handles HargaSatuan.TextChanged
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If IsNumeric(HargaSatuan.Text) Then
            Dim temp As Double = HargaSatuan.Text
            HargaSatuan.Text = Format(temp, "###,##0")
            HargaSatuan.SelectionStart = HargaSatuan.TextLength
        End If
    End Sub

    Private Sub HargaSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaSatuan.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HargaSatuan.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            hitungSubTotal()
            PsDisc1.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

End Class