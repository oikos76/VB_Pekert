Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Public Class Form_KeuInventaris
    Protected Dt As DataTable
    Private CN As SqlConnection
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Dim LAdd As Boolean, LEdit As Boolean, PEdit As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean

    Private Sub CmbNoKelompok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNoKelompok.SelectedIndexChanged
        If LAdd Or LEdit Then IsiCmbKodeGL()
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If MsgBox("Apakah ANDA yakin HAPUS Inventaris ini ?", vbCritical + vbYesNo, ".:Confirm!") = vbYes Then
            MsgSQL = "UPDATE m_Penyusutan SET " &
                    "AktifYN='N', " &
                    "Lastupd = GetDate(), " &
                    "userid = '" & UserID & "' " &
                    "WHERE idrec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            MsgSQL = "UPDATE m_PERKIRAAN  SET " &
                "AktifYN='N', " &
                "Lastupd = GetDate() " &
                "WHERE NO_PERKIRAAN = '" & Trim(KodeGL.Text) & "' "
            Proses.ExecuteNonQuery(MsgSQL)
            ClearTextBoxes()
        End If
    End Sub

    Private Sub cmbKodeGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKodeGL.SelectedIndexChanged
        If LAdd Or LEdit Then
            If Mid(cmbKodeGL.Text, 1, 1) = "<" Then
                KodeGL.Text = MaxKodeGL()
                KodeGL.Visible = True
                cmbKodeGL.Visible = False
                NamaAset.ReadOnly = False
                NamaAset.Visible = True
                NamaAset.Text = ""
                NamaAset.Focus()
            Else
                KodeGL.Text = ""
                KodeGL.Visible = False
                cmbKodeGL.Visible = True
                NamaAset.ReadOnly = True
                NamaAset.Visible = False
                NamaAset.Text = ""
                cmbKodeGL.Focus()
            End If
        End If
    End Sub

    Private Sub HargaBeli_TextChanged(sender As Object, e As EventArgs) Handles HargaBeli.TextChanged
        If Trim(HargaBeli.Text) = "" Then HargaBeli.Text = 0
        If IsNumeric(HargaBeli.Text) Then
            Dim temp As Double = HargaBeli.Text
            HargaBeli.Text = Format(temp, "###,##0")
            HargaBeli.SelectionStart = HargaBeli.TextLength
        End If
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        If Trim(idRecord.Text) = "" Then
            MsgBox("Inventaris yang mau di edit belum di pilih!", vbCritical + vbOKOnly, ".:Warning!")
            Exit Sub
        End If
        LAdd = False
        LEdit = True
        AturTombol(False)
    End Sub

    Private Sub Penyusutan_TextChanged(sender As Object, e As EventArgs) Handles Penyusutan.TextChanged

    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim tIdRec As String = "",
            MsgSQL As String,
            tempKodeGL As String = ""
        If CmbNoKelompok.Text = "" Then
            MsgBox("Kelompok GL belum di pilih !", vbCritical + vbOKOnly, ".:Warning!")
            CmbNoKelompok.Focus()
            Exit Sub
        End If
        If LAdd Then
            MsgSQL = "Select isnull(Max(Right(IDRec,6)),0) + 1000001 RecId " &
                " From m_Penyusutan "
            tIdRec = Proses.ExecuteSingleStrQuery(MsgSQL)
            idRecord.Text = Microsoft.VisualBasic.Right(tIdRec, 6)
            If Mid(cmbKodeGL.Text, 1, 1) = "<" Then
                tempKodeGL = MaxKodeGL()
                KodeGL.Text = Trim(Mid(CmbNoKelompok.Text, 1, 11)) & "." & Trim(tempKodeGL)
                NamaAset.Text = Replace(Trim(NamaAset.Text), "'", "`")
            Else
                KodeGL.Text = Trim(Mid(cmbKodeGL.Text, 1, 16))
                NamaAset.Text = Trim(Mid(cmbKodeGL.Text, 17, 100))
            End If

            MsgSQL = "INSERT INTO m_Penyusutan(IDRec, KodeGL, NamaAccount, " &
                "TglBeli, HargaBeli, Penyusutan, AktifYN, LastUPD, UserID, sisa, " &
                "nilaisusut, lamaPakai) VALUES('" & idRecord.Text & "', '" & KodeGL.Text & "', " &
                "'" & Trim(NamaAset.Text) & "', '" & Format(TglBeli.Value, "yyyy-MM-dd") & "', " &
                "" & HargaBeli.Text * 1 & ", " & Penyusutan.Text * 1 & "," &
                "'Y', GetDate(), '" & UserID & "', 0, 0, 0) "
            Proses.ExecuteNonQuery(MsgSQL)
            If NamaAset.Visible = True And cmbKodeGL.Visible = False Then
                MsgSQL = "Insert Into m_PERKIRAAN (NO_PERKIRAAN, NO_SUB, NM_PERKIRAAN, AktifYN, LastUPD, " &
                    "SAkhir) Values('" & Trim(KodeGL.Text) & "', '" & Trim(Mid(CmbNoKelompok.Text, 1, 11)) & "', " &
                    "'" & NamaAset.Text & "', 'Y', GetDate(), 0) "
                Proses.ExecuteNonQuery(MsgSQL)
            End If
        ElseIf LEdit Then
            '"     KodeGL = '" & Trim(Mid(KodeGL.Text, 1, 16)) & "',  " &
            '"NamaAccount = '" & Trim(Microsoft.VisualBasic.Right(cmbKodeGL.Text, 100)) & "', " &
            MsgSQL = "Update m_Penyusutan Set  " &
                "    TglBeli = '" & Format(TglBeli.Value, "yyyy-MM-dd") & "', " &
                "  HargaBeli = " & HargaBeli.Text * 1 & ", " &
                " Penyusutan = " & Penyusutan.Text * 1 & "," &
                "    LastUPD = GetDate(), UserID = '" & UserID & "' " &
                "Where IDRec = '" & idRecord.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        End If
        LAdd = False
        LEdit = False
        AturTombol(True)
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Form_Daftar.txtQuery.Text = "Select * " &
                    " From m_Penyusutan " &
                    "Where AktifYN = 'Y' " &
                    "Order By kodegl "
        Form_Daftar.Text = "Daftar Inventaris"
        Form_Daftar.ShowDialog()

        idRecord.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        SQL = "Select * From m_Penyusutan " &
           " Where idrec = '" & idRecord.Text & "' " &
           " and aktifyn = 'Y' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            For i = 0 To CmbNoKelompok.Items.Count - 1
                CmbNoKelompok.SelectedIndex = i
                If Mid(CmbNoKelompok.Text, 1, 11) = Mid(dbTable.Rows(0) !kodegl, 1, 11) Then
                    IsiCmbKodeGL()
                    Exit For
                End If
            Next
            KodeGL.Text = dbTable.Rows(0) !kodegl
            NamaAset.Text = Trim(dbTable.Rows(0) !kodegl) + " " + dbTable.Rows(0) !namaAccount
            For i = 0 To cmbKodeGL.Items.Count - 1
                cmbKodeGL.SelectedIndex = i
                If Mid(cmbKodeGL.Text, 1, 15) = Trim(dbTable.Rows(0) !kodegl) Then

                    Exit For
                End If
            Next
            NamaAset.Visible = True
            cmbKodeGL.Visible = False
            HargaBeli.Text = Format(dbTable.Rows(0) !hargabeli, "###,##0")
            TglBeli.Value = dbTable.Rows(0) !tglbeli
            Penyusutan.Text = Replace(Format(dbTable.Rows(0) !penyusutan, "###,##0.00"), ".00", "")
        Else
            idRecord.Text = ""
            HargaBeli.Text = "0"
            TglBeli.Value = "1900-01-01"
            Penyusutan.Text = "0"
        End If
        cmdPrint.Focus()
    End Sub

    Private Sub NamaAset_TextChanged(sender As Object, e As EventArgs) Handles NamaAset.TextChanged

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub TglBeli_ValueChanged(sender As Object, e As EventArgs) Handles TglBeli.ValueChanged

    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        CmbNoKelompok.Focus()
        cmbKodeGL.Items.Clear()
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        If tTambah = False Then
            cmdTambah.Enabled = False
        Else
            cmdTambah.Visible = tAktif
        End If
        If tEdit = False Then
            cmdEdit.Enabled = False
            cmdSimpan.Visible = False
        Else
            cmdEdit.Visible = tAktif
            cmdSimpan.Visible = Not tAktif
        End If
        If tHapus = False Then
            cmdHapus.Enabled = False
        Else
            cmdHapus.Visible = tAktif
        End If
        cmdPrint.Visible = tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Visible = tAktif
        CmbNoKelompok.Enabled = Not tAktif
        cmbKodeGL.Visible = Not tAktif
        NamaAset.Visible = tAktif
        TglBeli.Enabled = Not tAktif
        NoKelompok.ReadOnly = tAktif
        HargaBeli.ReadOnly = tAktif
        Penyusutan.ReadOnly = tAktif
        Me.Text = "Inventaris"
    End Sub

    Private Sub Form_KeuInventaris_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearTextBoxes()
        UserID = FrmMenuUtama.TsPengguna.Text
        tTambah = Proses.UserAksesTombol(UserID, "104_INVENTARIS", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "104_INVENTARIS", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "104_INVENTARIS", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "104_INVENTARIS", "laporan")

        MsgSQL = "Select * From M_SubPerkiraan " &
            "Where AktifYN = 'Y' " &
            " And No_Kode = '10.20.01' "
        dbTable = Proses.ExecuteQuery(MsgSQL)
        For a = 0 To dbTable.Rows.Count - 1
            Application.DoEvents()
            CmbNoKelompok.Items.Add(Mid(dbTable.Rows(a) !No_Sub & Space(11), 1, 11) & " " & dbTable.Rows(a) !nm_Sub)
        Next a
        Me.Cursor = Cursors.Default
        AturTombol(True)
    End Sub

    Private Sub ClearTextBoxes(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        If ctlcol Is Nothing Then ctlcol = Me.Controls
        For Each ctl As Control In ctlcol
            If TypeOf (ctl) Is TextBox Then
                DirectCast(ctl, TextBox).Clear()
            Else
                If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                    ClearTextBoxes(ctl.Controls)
                End If
            End If
        Next
        CmbNoKelompok.SelectedIndex = -1
    End Sub

    Private Sub IsiCmbKodeGL()
        Dim rsK As New DataTable

        NamaAset.Visible = False
        NamaAset.Text = ""
        KodeGL.Visible = False
        KodeGL.Text = ""
        cmbKodeGL.Items.Clear()
        cmbKodeGL.Items.Add("<Kode GL BARU>")
        MsgSQL = "Select No_Perkiraan as kode, NM_Perkiraan Nama " &
            " from m_Perkiraan " &
            "Where AktifYN = 'Y' and no_Sub = '" & Trim(Mid(CmbNoKelompok.Text, 1, 11)) & "' " &
            "Order By No_Perkiraan "
        rsK = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To rsK.Rows.Count - 1
            Application.DoEvents()
            cmbKodeGL.Items.Add(Mid(rsK.Rows(i) !kode & Space(16), 1, 16) &
                                Mid(rsK.Rows(i) !Nama & Space(100), 1, 100))
        Next
        cmbKodeGL.Visible = True
    End Sub

    Private Function MaxKodeGL() As String
        Dim MsgSQL As String, tempMax As String = ""
        If CmbNoKelompok.Text = "" Then
            MaxKodeGL = ""
            Exit Function
        Else
            If CmbNoKelompok.Text = "" Then
                MsgBox("Kelompok GL belum di pilih !", vbCritical + vbOKOnly, ".:Warning!")
                CmbNoKelompok.Focus()
                Exit Function
            End If
            MsgSQL = "Select ISNULL(max(substring(No_Perkiraan,13,3)),0) + 1001 KodeGL " &
                " From m_Perkiraan " &
                "Where No_Sub = '" & Mid(CmbNoKelompok.Text, 1, 11) & "' "
            tempMax = Proses.ExecuteSingleStrQuery(MsgSQL)
            MaxKodeGL = Microsoft.VisualBasic.Right(tempMax, 3)
        End If
    End Function

    Private Sub HargaBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles HargaBeli.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If HargaBeli.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(HargaBeli.Text) Then
                Dim temp As Double = HargaBeli.Text
                HargaBeli.Text = Replace(Format(temp, "###,##0.00"), ".00", "")
                HargaBeli.SelectionStart = HargaBeli.TextLength
            Else
                HargaBeli.Text = 0
            End If
            If LAdd Or LEdit Then Penyusutan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub Penyusutan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Penyusutan.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If Penyusutan.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(Penyusutan.Text) Then
                Dim temp As Double = Penyusutan.Text
                Penyusutan.Text = Replace(Format(temp, "###,##0.00"), ".00", "")
                Penyusutan.SelectionStart = Penyusutan.TextLength
            Else
                Penyusutan.Text = 0
            End If
            If LAdd Or LEdit Then cmdSimpan.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub NamaAset_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NamaAset.KeyPress
        If LEdit Or LAdd Then
            If e.KeyChar = Chr(13) Then
                TglBeli.Focus()
            End If
        End If
    End Sub

    Private Sub TglBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TglBeli.KeyPress
        If LEdit Or LAdd Then
            If e.KeyChar = Chr(13) Then
                HargaBeli.Focus()
            End If
        End If
    End Sub
End Class