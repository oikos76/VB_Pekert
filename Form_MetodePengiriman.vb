Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Form_MetodePengiriman
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

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        If Trim(CaraKirim.Text) = "" Then
            MsgBox("Cara Pengiriman masih kosong !", vbCritical + vbOKOnly, ".:Warning!")
            CaraKirim.Focus()
            Exit Sub
        End If
        If LAdd Then
            SQL = "Select *
                 From m_Kirim 
                Where CaraKirim = '" & Trim(CaraKirim.Text) & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                CaraKirim.Focus()
                MsgBox("Cara Kirim : " & CaraKirim.Text & " Sudah ADA!", vbCritical, "Warning!")
                Exit Sub
            Else
                IdRec.Text = Proses.GetMaxID_int("m_Kirim", "IdKirim")
            End If
            SQL = "INSERT INTO m_Kirim (IdKirim, CaraKirim) VALUES " &
                "('" & IdRec.Text & "', '" & Trim(CaraKirim.Text) & "' ) "
            Proses.ExecuteNonQuery(SQL)
        ElseIf LEdit Then
            SQL = "Update m_Kirim SET " &
                    "CaraKirim = '" & CaraKirim.Text & "'  " &
                    "where IdKirim = '" & IdRec.Text & "' "
            Proses.ExecuteNonQuery(SQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
        Call DataRecord()
    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Dim objRep As New ReportDocument

    Private Sub CaraKirim_TextChanged(sender As Object, e As EventArgs) Handles CaraKirim.TextChanged

    End Sub

    Private Sub Form_MetodePengiriman_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        CaraKirim.Text = ""
        IdRec.Text = ""
        IdRec.Visible = False
        DataRecord()
        AturTombol(True)
    End Sub

    Private Sub DataRecord()
        Dim mKondisi As String = ""
        Me.Cursor = Cursors.WaitCursor
        SQL = "SELECT * FROM m_Kirim  " &
            "ORDER BY CaraKirim"
            dbTable = Proses.ExecuteQuery(SQL)
        DGView.Rows.Clear()
        With dbTable.Columns(0)
            For a = 0 To dbTable.Rows.Count - 1
                    DGView.Rows.Add(.Table.Rows(a) !IdKirim,
                    .Table.Rows(a) !CaraKirim, "Edit", "Hapus")
                Next (a)
        End With
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdTambah.Visible = tAktif
        PanelSimpan.Visible = Not tAktif
        CaraKirim.ReadOnly = tAktif

    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        AturTombol(True)
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        AturTombol(False)
        LAdd = True
        LEdit = False
        CaraKirim.Text = ""
        IdRec.Text = ""
        CaraKirim.Focus()
    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        If e.RowIndex >= 0 Then
            IdRec.Text = DGView.Rows(e.RowIndex).Cells(0).Value
            CaraKirim.Text = DGView.Rows(e.RowIndex).Cells(1).Value
        End If
        If e.ColumnIndex = 2 Then 'Edit
            'tEdit = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "edit")
            'If tEdit = False Then
            '    MsgBox("Anda tidak punya akses untuk edit data ini !", vbCritical + vbOKOnly, ".:Warning !")
            '    Exit Sub
            'End If
            If Trim(IdRec.Text) = "" Then
                MsgBox("Metode Pengriman Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            LAdd = False
            LEdit = True
            AturTombol(False)
            CaraKirim.Focus()
        ElseIf e.ColumnIndex = 3 Then 'Hapus
            'tHapus = Proses.UserAksesTombol(UserID, "22_KODIF_FUNGSI_PRODUK", "hapus")
            'If tHapus = False Then
            '    MsgBox("Anda tidak punya akses untuk menghapus data ini !", vbCritical + vbOKOnly, ".:Warning !")
            '    Exit Sub
            'End If
            If Trim(IdRec.Text) = "" Then
                MsgBox("Metode Pengriman Belum di pilih!", vbCritical, ".:ERROR!")
                DGView.Focus()
                Exit Sub
            End If
            If MsgBox("Yakin hapus data " & Trim(DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value) & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                SQL = "Delete m_Kirim  " &
                    "Where IdKirim = '" & IdRec.Text & "' "
                Proses.ExecuteNonQuery(SQL)
                IdRec.Text = ""
                CaraKirim.Text = ""
                IdRec.Text = ""
                IdRec.Text = ""
                DataRecord()
            End If
        End If
    End Sub

    Private Sub CaraKirim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CaraKirim.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(39) Then e.KeyChar = Chr(96)
        If e.KeyChar = Chr(13) Then
            If LAdd Or LEdit Then cmdSimpan.Focus()
        End If
    End Sub
End Class