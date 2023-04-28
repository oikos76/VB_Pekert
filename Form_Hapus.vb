Public Class Form_Hapus
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, SQL As String, MsgSQL As String
    Dim UserID As String = FrmMenuUtama.TsPengguna.Text

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        If OptSebagian.Checked = False And OptSemua.Checked = False Then
            MsgBox("Jenis data yang mau di hapus belom di pilih !", vbCritical + vbOKOnly, ".:Mau hapus semua atau sebagian...?")
            Exit Sub
        End If
        If OptSebagian.Checked = True And OptSemua.Checked = False Then
            HapusSebagian()
        Else
            HapusSemua()
        End If
        Me.Close()
    End Sub

    Private Sub HapusSemua()
        If Me.Text = "Hapus SP" Then
            If MsgBox("Apakah anda yakin hapus SP: " & tIDSemua.Text & "  ?", vbYesNo + vbInformation, ".:YAKIN SP INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_SP SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where NoSP = '" & tIDSemua.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus DPB" Then
            If MsgBox("Apakah anda yakin hapus DPB: " & tIDSemua.Text & "  ?", vbYesNo + vbInformation, ".:YAKIN DPB INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_DPB SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where NoDPB = '" & tIDSemua.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus Katalog" Then
            If MsgBox("Yakin hapus Katalog " & tIDSemua.Text & "  ?", vbInformation + vbYesNo, ".:Confirmation!") = vbYes Then
                MsgSQL = "Update T_KatalogProduk Set AktifYN = 'N', Lastupd = getdate(), userid = '" & UserID & "' " &
                "Where NamaFile = '" & tIDSemua.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus Pra LHP" Then
            If MsgBox("Apakah anda yakin hapus Pra LHP: " & tIDSemua.Text & "  ?", vbYesNo + vbInformation, ".:YAKIN DPB INI DI HAPUS?") = vbYes Then
                MsgSQL = "Delete t_PraLHP Where NoPraLHP = '" & tIDSemua.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus LHP" Then
            If MsgBox("Apakah anda yakin hapus LHP: " & Trim(tIDSemua.Text) & "  ?", vbYesNo + vbInformation, ".:YAKIN DPB INI DI HAPUS?") = vbYes Then
                MsgSQL = "Delete t_LHP Where NoLHP = '" & tIDSemua.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus DPL" Then
            If MsgBox("Apakah anda yakin hapus DPL: " & tIDSemua.Text & "  ?", vbYesNo + vbInformation, ".:YAKIN DPB INI DI HAPUS?") = vbYes Then
                MsgSQL = "Delete t_DPL Where NoDPL = '" & tIDSemua.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        End If

    End Sub

    Private Sub HapusSebagian()
        If Me.Text = "Hapus SP" Then
            If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation, ".:YAKIN BAGIAN INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_SP SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & tIDSebagian.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.text = "Hapus DPB" Then
            If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation, ".:YAKIN BAGIAN INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_DPB SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & tIDSebagian.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.text = "Hapus Pra LHP" Then
            If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation, ".:YAKIN BAGIAN INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_PraLHP SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & tIDSebagian.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.text = "Hapus LHP" Then
            If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation, ".:YAKIN BAGIAN INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_LHP SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & tIDSebagian.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus DPL" Then
            If MsgBox("Apakah anda yakin hapus record ini?", vbYesNo + vbInformation, ".:YAKIN BAGIAN INI DI HAPUS?") = vbYes Then
                MsgSQL = "Update t_DPL SET  " &
                " AktifYN = 'N', " &
                "  UserID = '" & UserID & "', " &
                " LastUPD = GetDate(), " &
                " TransferYN = 'N'  " &
                "Where IDRec = '" & tIDSebagian.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        ElseIf Me.Text = "Hapus Katalog" Then
            If MsgBox("Yakin hapus data produk " & tIDSebagian.Text & " ini?", vbInformation + vbYesNo, ".:Confirmation!") = vbYes Then
                MsgSQL = "Update T_KatalogProduk Set AktifYN = 'N', userid = '" & UserID & "', Lastupd = getdate() " &
                "Where NamaFile = '" & tIDSemua.Text & "' " &
                "  And IdRec = '" & tIDSebagian.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgBox("Data berhasil di hapus", vbInformation + vbOKOnly, ".:Information")
            End If
        End If
    End Sub

End Class