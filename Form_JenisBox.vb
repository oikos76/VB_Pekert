Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Form_JenisBox
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String
    Dim KodeToko As String
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Protected Ds As DataSet
    Protected Dt As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter

    Private Sub JenisBox_TextChanged(sender As Object, e As EventArgs) Handles JenisBox.TextChanged

    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        AturTombol(True)
    End Sub
    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick
        If e.RowIndex >= 0 Then
            IdRec.Text = DGView.Rows(e.RowIndex).Cells(0).Value
            JenisBox.Text = DGView.Rows(e.RowIndex).Cells(1).Value
            Panjang.Text = DGView.Rows(e.RowIndex).Cells(2).Value
            Lebar.Text = DGView.Rows(e.RowIndex).Cells(3).Value
            Tinggi.Text = DGView.Rows(e.RowIndex).Cells(4).Value
        End If
        If e.ColumnIndex = 5 Then 'Edit
            If Trim(IdRec.Text) = "" Then
                MsgBox("Dimensi BOX Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            LAdd = False
            LEdit = True
            AturTombol(False)
            JenisBox.Focus()
        ElseIf e.ColumnIndex = 6 Then 'Hapus
            If Trim(IdRec.Text) = "" Then
                MsgBox("Dimensi BOX Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                SQL = "Update m_DimensiBOX SET AktifYN='N' " &
                    "Where IdRec = '" & IdRec.Text & "' "
                Proses.ExecuteNonQuery(SQL)
                IdRec.Text = ""
                JenisBox.Text = ""
                IdRec.Text = ""
                IdRec.Text = ""
                DataRecord()
            End If
        End If
    End Sub
    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        AturTombol(False)
        LAdd = True
        LEdit = False
        JenisBox.Text = ""
        IdRec.Text = ""
        JenisBox.Focus()
    End Sub
    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdTambah.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        cmdSimpan.Visible = Not tAktif
        JenisBox.ReadOnly = tAktif
        Panjang.ReadOnly = tAktif
        Lebar.ReadOnly = tAktif
        Tinggi.ReadOnly = tAktif
    End Sub
    Private Sub DataRecord()
        Dim mKondisi As String = ""
        Me.Cursor = Cursors.WaitCursor
        SQL = "SELECT * FROM m_DimensiBox " &
            "WHERE AktifYN='Y' " &
            "ORDER BY JenisBox "
        dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                DGView.Rows.Add(.Table.Rows(a) !IdRec,
                    .Table.Rows(a) !JenisBox,
                    Format(.Table.Rows(a) !Panjang, "###,##0.00"),
                    Format(.Table.Rows(a) !Lebar, "###,##0.00"),
                    Format(.Table.Rows(a) !Tinggi, "###,##0.00"),
                    "Edit", "Hapus")
            Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub Form_JenisBox_Load(sender As Object, e As EventArgs) Handles Me.Load
        With Me.DGView.RowTemplate
            .Height = 35
            .MinimumHeight = 30
        End With
        DGView.GridColor = Color.Red
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        UserID = FrmMenuUtama.TsPengguna.Text
        JenisBox.Text = ""
        IdRec.Text = ""
        IdRec.Visible = False
        DataRecord()
        AturTombol(True)
    End Sub
    Private Sub JenisBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles JenisBox.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Then Panjang.Focus()
        End If
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        If Trim(JenisBox.Text) = "" Then
            MsgBox("Jenis BOX masih kosong !", vbCritical + vbOKOnly, ".:Warning!")
            JenisBox.Focus()
            Exit Sub
        End If
        If LAdd Then
            SQL = "Select *
                 From m_DimensiBOX 
                Where JenisBox = '" & Trim(JenisBox.Text) & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                JenisBox.Focus()
                MsgBox("Jenis Dimensi Box : " & JenisBox.Text & " Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            Else
                IdRec.Text = Proses.GetMaxID_int("m_DimensiBOX", "IdRec")
            End If
            SQL = "INSERT INTO m_DimensiBOX (IdRec, JenisBox, Panjang, Lebar, Tinggi, AktifYN) VALUES " &
                "('" & IdRec.Text & "', '" & Trim(JenisBox.Text) & "', " & Panjang.Text * 1 & ", " &
                "" & Lebar.Text * 1 & ", " & Tinggi.Text * 1 & ", 'Y' ) "
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "UPDATE m_DimensiBOX SET " &
                "JenisBox = '" & JenisBox.Text & "', " &
                " Panjang =  " & Panjang.Text & " , " &
                "   Lebar =  " & Lebar.Text & " , " &
                "  Tinggi =  " & Tinggi.Text & "  " &
                "WHERE IdRec = '" & IdRec.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call DataRecord()
    End Sub

    Private Sub PanelSimpan_Paint(sender As Object, e As PaintEventArgs) Handles PanelSimpan.Paint

    End Sub

    Private Sub Panjang_TextChanged(sender As Object, e As EventArgs) Handles Panjang.TextChanged

        If IsNumeric(Panjang.Text) Then
            Dim temp As Double = Panjang.Text
            Panjang.SelectionStart = Panjang.TextLength
        Else
            Panjang.Text = 0
        End If
    End Sub

    Private Sub Panjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Panjang.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Panjang.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Panjang.Text) Then
                Dim temp As Double = Panjang.Text
                Panjang.Text = Format(temp, "###,##0.00")
                Panjang.SelectionStart = Panjang.TextLength
            Else
                Panjang.Text = 0
            End If
            If LAdd Or LEdit Then Lebar.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Lebar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lebar.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Lebar.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Lebar.Text) Then
                Dim temp As Double = Lebar.Text
                Lebar.Text = Format(temp, "###,##0.00")
                Lebar.SelectionStart = Lebar.TextLength
            Else
                Lebar.Text = 0
            End If
            If LAdd Or LEdit Then Tinggi.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Lebar_TextChanged(sender As Object, e As EventArgs) Handles Lebar.TextChanged

        If IsNumeric(Lebar.Text) Then
            Dim temp As Double = Lebar.Text
            Lebar.SelectionStart = Lebar.TextLength
        Else
            Lebar.Text = 0
        End If
    End Sub

    Private Sub Tinggi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tinggi.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Tinggi.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Tinggi.Text) Then
                Dim temp As Double = Tinggi.Text
                Tinggi.Text = Format(temp, "###,##0.00")
                Tinggi.SelectionStart = Tinggi.TextLength
            Else
                Tinggi.Text = 0
            End If
            If LAdd Or LEdit Then cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub Tinggi_TextChanged(sender As Object, e As EventArgs) Handles Tinggi.TextChanged

        If IsNumeric(Tinggi.Text) Then
            Dim temp As Double = Tinggi.Text
            Tinggi.SelectionStart = Tinggi.TextLength
        Else
            Tinggi.Text = 0
        End If
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If e.RowIndex >= 0 Then
            IdRec.Text = DGView.Rows(e.RowIndex).Cells(0).Value
            JenisBox.Text = DGView.Rows(e.RowIndex).Cells(1).Value
            Panjang.Text = DGView.Rows(e.RowIndex).Cells(2).Value
            Lebar.Text = DGView.Rows(e.RowIndex).Cells(3).Value
            Tinggi.Text = DGView.Rows(e.RowIndex).Cells(4).Value
        End If
    End Sub
End Class