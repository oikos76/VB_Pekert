Public Class Form_GiroCair
    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim SQL As String
        Dim Proses As New ClsKoneksi

        If cmdSimpan.Text = "Accept" Then
            SQL = "Update t_BayarCustH " &
                "  Set StatusGiro = 'APPROVED', Keterangan = '',  " &
                "    TglCair =  '" & Format(TglCair.Value, "yyyy-MM-dd") & "'  " &
                "WHere  IDRec = '" & idRec.Text & "' "
        Else
            SQL = "Update t_BayarCustH " &
                "  Set StatusGiro = 'REJECT', TglCair = '1900-01-01', " &
                "  Keterangan = '" & Keterangan.Text & "' " &
                "WHere  IDRec = '" & idRec.Text & "' "
        End If
        Proses.ExecuteNonQuery(SQL)
        Me.Close()
    End Sub
End Class