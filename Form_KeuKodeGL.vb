Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Public Class Form_KeuKodeGL
    Protected Dt As DataTable
    Private CN As SqlConnection
    Dim FotoLoc As String = My.Settings.path_foto
    Dim dttable As New DataTable
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Dim LAdd As Boolean, LEdit As Boolean, PEdit As String,
        tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim tNoSUB As String, tNamaSUB As String
    Dim tNoKode As String, tNamaKode As String
    Dim tNoKelompok As String, tNamaKelompok As String
    Dim tNoGolongan As String, tNamaGolongan As String

    Private Sub CmbNoKelompok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNoKelompok.SelectedIndexChanged
        If CmbNoKelompok.Visible Then
            NamaKelompok.Visible = True
            NamaKelompok.Enabled = False
            NamaKelompok.Text = Microsoft.VisualBasic.Right(CmbNoKelompok.Text, 50)
            NoKode.Text = Microsoft.VisualBasic.Left(CmbNoKelompok.Text, 5) + "."
            NoKode.SelectionStart = Len(Trim(NoKode.Text)) + 1
            IsiKode()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbNoKode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoKode.SelectedIndexChanged
        If cmbNoKode.Visible Then
            NamaKode.Visible = True
            NamaKode.Enabled = False
            NamaKode.Text = Microsoft.VisualBasic.Right(cmbNoKode.Text, 50)
            If LAdd Then
                NoSubPerkiraan.Text = Microsoft.VisualBasic.Left(cmbNoKode.Text, 8) + "."
                NoSubPerkiraan.SelectionStart = Len(Trim(NoSubPerkiraan.Text)) + 1
            End If
            IsiSubPerkiraan()
        End If
    End Sub

    Private Sub cmdTambah_Click(sender As Object, e As EventArgs) Handles cmdTambah.Click
        LAdd = True
        LEdit = False
        AturTombol(False)
        ClearTextBoxes()
        cmbJenisTabel.Enabled = True
        cmbJenisTabel.Focus()
    End Sub

    Private Sub cmbSubPerkiraan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubPerkiraan.SelectedIndexChanged
        If cmbSubPerkiraan.Visible Then
            NamaSubPerkiraan.Visible = True
            NamaSubPerkiraan.Enabled = False
            NamaSubPerkiraan.Text = Microsoft.VisualBasic.Right(cmbSubPerkiraan.Text, 50)
            If LAdd Then
                NoPerkiraan.Text = Trim(Microsoft.VisualBasic.Left(cmbSubPerkiraan.Text, 15)) + "."
                NoPerkiraan.SelectionStart = Len(Trim(NoPerkiraan.Text)) + 1
            End If
        End If
    End Sub
    Public Sub AturComboBox()
        cmbNoGolongan.Items.Clear()
        CmbNoKelompok.Items.Clear()
        cmbNoKode.Items.Clear()
        cmbSubPerkiraan.Items.Clear()

        cmbNoGolongan.Visible = False
        CmbNoKelompok.Visible = False
        cmbNoKode.Visible = False
        cmbSubPerkiraan.Visible = False

        NoGolongan.Text = ""
        NamaGolongan.Text = ""
        NoKelompok.Text = ""
        NamaKelompok.Text = ""
        NoKode.Text = ""
        NamaKode.Text = ""
        NoSubPerkiraan.Text = ""
        NamaSubPerkiraan.Text = ""

        NoPerkiraan.Text = ""
        NamaPerkiraan.Text = ""

        If cmbJenisTabel.Text = "GOLONGAN" Then
            cmbNoGolongan.Visible = False
            NoGolongan.Visible = True
            NoGolongan.Enabled = True
            NamaGolongan.Enabled = True

            NoKelompok.Visible = True
            NoKelompok.Enabled = False
            NamaKelompok.Enabled = False

            NoKode.Visible = True
            NoKode.Enabled = False
            NamaKode.Enabled = False

            NoSubPerkiraan.Visible = True
            NoSubPerkiraan.Enabled = False
            NamaSubPerkiraan.Enabled = False

            NoPerkiraan.Visible = True
            NoPerkiraan.Enabled = False
            NamaPerkiraan.Visible = True
            NamaPerkiraan.Enabled = False

            IsiGolongan()
            If LAdd Or LEdit Then NoGolongan.Focus()

        ElseIf cmbJenisTabel.Text = "KELOMPOK" Then
            If LAdd Or LEdit Then
                cmbNoGolongan.Visible = True
                NoGolongan.Visible = False
            Else
                cmbNoGolongan.Visible = False
                NoGolongan.Visible = True
            End If

            NoGolongan.Enabled = True
            NamaGolongan.Enabled = False

            'CmbNoKelompok.Visible = False
            NoKelompok.Visible = True
            NoKelompok.Enabled = True
            NamaKelompok.Enabled = True

            'cmbNoKode.Visible = False
            NoKode.Visible = True
            NoKode.Enabled = False
            NamaKode.Enabled = False

            'cmbSubPerkiraan.Visible = False
            NoSubPerkiraan.Visible = True
            NoSubPerkiraan.Enabled = False
            NamaSubPerkiraan.Enabled = False

            NoPerkiraan.Visible = True
            NoPerkiraan.Enabled = False
            NamaPerkiraan.Visible = True
            NamaPerkiraan.Enabled = False
            IsiGolongan()
            If LAdd Or LEdit Then cmbNoGolongan.Focus()

        ElseIf cmbJenisTabel.Text = "KODE" Then

            If LAdd Or LEdit Then
                cmbNoGolongan.Visible = True
                NoGolongan.Visible = False
            Else
                cmbNoGolongan.Visible = False
                NoGolongan.Visible = True
            End If

            NoGolongan.Enabled = True
            NamaGolongan.Enabled = False

            If LAdd Or LEdit Then
                CmbNoKelompok.Visible = True
                NoKelompok.Visible = False
            Else
                CmbNoKelompok.Visible = False
                NoKelompok.Visible = True
            End If
            NoKelompok.Enabled = True
            NamaKelompok.Enabled = False

            'cmbNoKode.Visible = False
            NoKode.Visible = True
            NoKode.Enabled = True
            NamaKode.Enabled = True

            'cmbSubPerkiraan.Visible = False
            NoSubPerkiraan.Visible = True
            NoSubPerkiraan.Enabled = False
            NamaSubPerkiraan.Enabled = False

            NoPerkiraan.Visible = True
            NoPerkiraan.Enabled = False
            NamaPerkiraan.Visible = True
            NamaPerkiraan.Enabled = False
            IsiGolongan()
            If LAdd Or LEdit Then cmbNoGolongan.Focus()

        ElseIf cmbJenisTabel.Text = "SUB PERKIRAAN" Then
            If LAdd Or LEdit Then
                cmbNoGolongan.Visible = True
                NoGolongan.Visible = False
            Else
                cmbNoGolongan.Visible = False
                NoGolongan.Visible = True
            End If

            NoGolongan.Enabled = True
            NamaGolongan.Enabled = False

            If LAdd Or LEdit Then
                CmbNoKelompok.Visible = True
                NoKelompok.Visible = False
            Else
                CmbNoKelompok.Visible = False
                NoKelompok.Visible = True
            End If
            NoKelompok.Enabled = True
            NamaKelompok.Enabled = False

            If LAdd Or LEdit Then
                cmbNoKode.Visible = True
                NoKode.Visible = False
            Else
                cmbNoKode.Visible = False
                NoKode.Visible = True
            End If
            NoKode.Enabled = True
            NamaKode.Enabled = False

            'cmbSubPerkiraan.Visible = False
            NoSubPerkiraan.Visible = True
            NoSubPerkiraan.Enabled = True
            NamaSubPerkiraan.Enabled = True

            NoPerkiraan.Visible = True
            NoPerkiraan.Enabled = False
            NamaPerkiraan.Visible = True
            NamaPerkiraan.Enabled = False
            IsiGolongan()
            If LAdd Or LEdit Then cmbNoGolongan.Focus()

        ElseIf cmbJenisTabel.Text = "PERKIRAAN" Then
            If LAdd Or LEdit Then
                cmbNoGolongan.Visible = True
                NoGolongan.Visible = False
            Else
                cmbNoGolongan.Visible = False
                NoGolongan.Visible = True
            End If
            NoGolongan.Enabled = True
            NamaGolongan.Enabled = False

            If LAdd Or LEdit Then
                CmbNoKelompok.Visible = True
                NoKelompok.Visible = False
            Else
                CmbNoKelompok.Visible = False
                NoKelompok.Visible = True
            End If

            NoKelompok.Enabled = True
            NamaKelompok.Enabled = False

            If LAdd Or LEdit Then
                cmbNoKode.Visible = True
                NoKode.Visible = False
            Else
                cmbNoKode.Visible = False
                NoKode.Visible = True
            End If
            NoKode.Enabled = True
            NamaKode.Enabled = False

            If LAdd Or LEdit Then
                cmbSubPerkiraan.Visible = True
                NoSubPerkiraan.Visible = False
            Else
                cmbSubPerkiraan.Visible = False
                NoSubPerkiraan.Visible = True
            End If

            NoSubPerkiraan.Enabled = True
            NamaSubPerkiraan.Enabled = False

            NoPerkiraan.Visible = True
            NoPerkiraan.Enabled = False
            NamaPerkiraan.Visible = True
            NamaPerkiraan.Enabled = False

            NoPerkiraan.Visible = True
            NoPerkiraan.Enabled = True
            NamaPerkiraan.Enabled = True
            NamaPerkiraan.Enabled = True

            IsiGolongan()
            If LAdd Or LEdit Then cmbNoGolongan.Focus()
        End If
    End Sub
    Private Sub cmbJenisTabel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisTabel.SelectedIndexChanged
        AturComboBox
    End Sub

    Private Sub cmbNoGolongan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoGolongan.SelectedIndexChanged
        If cmbNoGolongan.Visible Then
            NamaGolongan.Visible = True
            NamaGolongan.Enabled = False
            NamaGolongan.Text = Microsoft.VisualBasic.Right(cmbNoGolongan.Text, 50)
            NoKelompok.Text = Microsoft.VisualBasic.Left(cmbNoGolongan.Text, 2) + "."
            NoKelompok.SelectionStart = Len(Trim(NoKelompok.Text)) + 1
            '        NoKelompok.SetFocus
            IsiKelompok()
        End If
    End Sub
    Public Sub isi_COA()
        Dim dbCOA As New DataTable
        If Trim(NoPerkiraan.Text) <> "" Then
            SQL = "SELECT NO_SUB " &
            "FROM m_Perkiraan  " &
            "WHERE AKTIFYN = 'Y'  " &
            "  And NO_PERKIRAAN= '" & NoPerkiraan.Text & "' "
            NoSubPerkiraan.Text = Proses.ExecuteSingleStrQuery(SQL)
        End If
        SQL = "SELECT NO_SUB, NM_SUB, NO_KODE " &
            "FROM m_subperkiraan  " &
            "WHERE AKTIFYN = 'Y'  " &
            "  And NO_SUB = '" & NoSubPerkiraan.Text & "' "
        dbCOA = Proses.ExecuteQuery(SQL)
        If dbCOA.Rows.Count <> 0 Then
            NamaSubPerkiraan.Text = dbCOA.Rows(0) !nm_SUB
            NoKode.Text = dbCOA.Rows(0) !no_kode
        End If
        SQL = "SELECT  NO_KODE, NO_KLP, NM_KODE " &
            "FROM M_KODE  " &
            "WHERE AKTIFYN = 'Y'  " &
            "  AND NO_KODE = '" & NoKode.Text & "' "
        dbCOA = Proses.ExecuteQuery(SQL)
        If dbCOA.Rows.Count <> 0 Then
            NamaKode.Text = dbCOA.Rows(0) !NM_KODE
            NoKelompok.Text = dbCOA.Rows(0) !NO_KLP
        End If

        SQL = "SELECT NO_Klp, NO_GOL, NM_Klp " &
            "FROM M_KELOMPOK  " &
            "WHERE AKTIFYN = 'Y'  " &
            "  AND NO_Klp = '" & NoKelompok.Text & "' "
        dbCOA = Proses.ExecuteQuery(SQL)
        If dbCOA.Rows.Count <> 0 Then
            NamaKelompok.Text = dbCOA.Rows(0) !NM_KLP
            NoGolongan.Text = dbCOA.Rows(0) !NO_GOL
        End If
        SQL = "SELECT NM_Gol FROM m_Golongan " &
            "WHERE AktifYN = 'Y' " &
            "  AND NO_Gol = '" & NoGolongan.Text & "' "
        NamaGolongan.Text = Proses.ExecuteSingleStrQuery(SQL)

    End Sub
    Private Sub IsiGolongan()
        Dim RSIsi As New DataTable, MsgSQL As String
        cmbNoGolongan.Items.Clear()
        MsgSQL = "Select * From M_Golongan " &
            "Where AktifYN = 'Y' "
        RSIsi = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To RSIsi.Rows.Count - 1
            Application.DoEvents()
            cmbNoGolongan.Items.Add(Microsoft.VisualBasic.Left(RSIsi.Rows(i) !no_gol & Space(10), 10) &
                                    Microsoft.VisualBasic.Left(RSIsi.Rows(i) !nm_Gol & Space(50), 50))
        Next i
    End Sub

    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        AturTombol(True)
        LAdd = False
        LEdit = False
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        PanelTombol_.Enabled = False
        Form_KeuKodeGL_Daftar.ShowDialog()
        PanelTombol_.Enabled = True
    End Sub

    Private Sub cmdHapus_Click(sender As Object, e As EventArgs) Handles cmdHapus.Click
        Dim MsgSQL As String
        If MsgBox("Apakah ANDA yakin HAPUS COA ini ?", vbCritical + vbYesNo, ".:Confirm!") = vbYes Then
            If cmbJenisTabel.Text = "GOLONGAN" Then
                MsgSQL = "delete m_golongan " &
                    "where NO_GOL = '" & NoGolongan.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "delete m_Kelompok where no_gol = '" & NoGolongan.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "delete m_kode where left(no_klp,2) = '" & NoGolongan.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "delete m_Subperkiraan where left(no_kode,2) = '" & NoGolongan.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "KELOMPOK" Then
                MsgSQL = "delete  m_kelompok " &
                    "where NO_KLP = '" & NoKelompok.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "delete m_kode where NO_KLP = '" & NoKelompok.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "delete m_Subperkiraan where left(no_kode,5) = '" & NoKelompok.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "KODE" Then
                MsgSQL = "delete m_kode " &
                    "where NO_KODE = '" & NoKode.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
                MsgSQL = "delete m_Subperkiraan where NO_KODE = '" & NoKode.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "SUB PERKIRAAN" Then
                MsgSQL = "delete M_SUBPERKIRAAN " &
                    "WHERE NO_SUB = '" & NoSubPerkiraan.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "PERKIRAAN" Then
                MsgSQL = "delete M_PERKIRAAN " &
                    "WHERE NO_Perkiraan = '" & NoPerkiraan.Text & "' "
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            ClearTextBoxes()
        End If
    End Sub

    Private Sub NoPerkiraan_TextChanged(sender As Object, e As EventArgs) Handles NoPerkiraan.TextChanged

    End Sub

    Private Sub IsiKelompok()
        Dim RSIsi As New DataTable, MsgSQL As String
        CmbNoKelompok.Items.Clear()
        MsgSQL = "Select * From M_Kelompok " &
        "Where AktifYN = 'Y' " &
        " And NO_Gol = '" & Microsoft.VisualBasic.Left(cmbNoGolongan.Text, 2) & "' "
        RSIsi = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To RSIsi.Rows.Count - 1
            Application.DoEvents()
            CmbNoKelompok.Items.Add(Microsoft.VisualBasic.Left(RSIsi.Rows(i) !NO_KLP & Space(10), 10) &
                                    Microsoft.VisualBasic.Left(RSIsi.Rows(i) !NM_KLP & Space(50), 50))
        Next i
    End Sub
    Private Sub IsiSubPerkiraan()
        Dim RSIsi As New DataTable, MsgSQL As String
        cmbSubPerkiraan.Items.Clear()
        MsgSQL = "Select * From M_SubPerkiraan " &
        "Where AktifYN = 'Y' " &
        " And NO_Kode = '" & Trim(Microsoft.VisualBasic.Left(cmbNoKode.Text, 8)) & "' " &
        "Order By no_SUB "
        RSIsi = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To RSIsi.Rows.Count - 1
            Application.DoEvents()
            cmbSubPerkiraan.Items.Add(Microsoft.VisualBasic.Left(RSIsi.Rows(i) !No_Sub & Space(15), 15) &
                                    Microsoft.VisualBasic.Left(RSIsi.Rows(i) !nm_Sub & Space(50), 50))
        Next i
    End Sub

    Private Sub IsiKode()
        Dim RSIsi As New DataTable, MsgSQL As String
        cmbNoKode.Items.Clear()
        MsgSQL = "Select * From M_Kode " &
        "Where AktifYN = 'Y' " &
        " And NO_Klp = '" & Microsoft.VisualBasic.Left(CmbNoKelompok.Text, 8) & "' " &
        "Order By no_Kode "
        RSIsi = Proses.ExecuteQuery(MsgSQL)
        For i = 0 To RSIsi.Rows.Count - 1
            Application.DoEvents()
            cmbNoKode.Items.Add(Microsoft.VisualBasic.Left(RSIsi.Rows(i) !no_KODE & Space(10), 10) &
                                    Microsoft.VisualBasic.Left(RSIsi.Rows(i) !NM_KODE & Space(50), 50))
        Next i
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        Dim tKey As String
        If Trim(NoGolongan.Text) = "" Then
            MsgBox("Chart of account belum di pilih!", vbCritical + vbOKOnly, ".:ERROR!")
            Exit Sub
        End If
        'tKey = Tree.Nodes.Item(Tree.SelectedItem.Index).Key
        LAdd = False
        LEdit = True
        AturTombol(False)

        tKey = cmbJenisTabel.Text
        If tKey = "GOLONGAN" Then 'GOLONGAN
            tNoGolongan = NoGolongan.Text
            tNamaGolongan = NamaGolongan.Text
            NamaGolongan.Focus()
            PEdit = "GOLONGAN"
        ElseIf tKey = "KELOMPOK" Then 'KELOMPOK
            NoGolongan.Enabled = False
            NamaGolongan.Enabled = False
            tNoGolongan = NoGolongan.Text
            tNamaGolongan = NamaGolongan.Text
            tNoKelompok = NoKelompok.Text
            tNamaKelompok = NamaKelompok.Text
            NamaKelompok.Focus()
            PEdit = "KELOMPOK"
        ElseIf tKey = "KODE" Then 'KODE
            NoKelompok.Enabled = False
            NamaKelompok.Enabled = False
            NoGolongan.Enabled = False
            NamaGolongan.Enabled = False
            tNoGolongan = NoGolongan.Text
            tNamaGolongan = NamaGolongan.Text
            tNoKelompok = NoKelompok.Text
            tNamaKelompok = NamaKelompok.Text
            tNoKode = NoKode.Text
            tNamaKode = NamaKode.Text
            NamaKode.Focus()
            PEdit = "KODE"
        ElseIf tKey = "SUB PERKIRAAN" Then 'SUB PERKIRAAN
            NoKelompok.Enabled = False
            NamaKelompok.Enabled = False
            NoGolongan.Enabled = False
            NamaGolongan.Enabled = False
            NoKode.Enabled = False
            NamaKode.Enabled = False
            tNoGolongan = NoGolongan.Text
            tNamaGolongan = NamaGolongan.Text
            tNoKelompok = NoKelompok.Text
            tNamaKelompok = NamaKelompok.Text
            tNoKode = NoKode.Text
            tNamaKode = NamaKode.Text
            tNoSUB = NoSubPerkiraan.Text
            tNamaSUB = NamaSubPerkiraan.Text
            NamaSubPerkiraan.Focus()
            PEdit = "SUB PERKIRAAN"
        ElseIf tKey = "PERKIRAAN" Then
            NoKelompok.Enabled = False
            NamaKelompok.Enabled = False
            NoGolongan.Enabled = False
            NamaGolongan.Enabled = False
            NoKode.Enabled = False
            NamaKode.Enabled = False
            NoSubPerkiraan.Enabled = False
            NamaSubPerkiraan.Enabled = False
            NoPerkiraan.ReadOnly = True
            NamaPerkiraan.Focus()
            PEdit = "PERKIRAAN"
        End If
        cmbJenisTabel.Enabled = True
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
        cmbJenisTabel.Enabled = Not tAktif

        NoGolongan.ReadOnly = tAktif
        NamaGolongan.ReadOnly = tAktif
        NoKelompok.ReadOnly = tAktif
        NamaKelompok.ReadOnly = tAktif
        NoKode.ReadOnly = tAktif
        NamaKode.ReadOnly = tAktif
        NoSubPerkiraan.ReadOnly = tAktif
        NamaSubPerkiraan.ReadOnly = tAktif
        NoPerkiraan.ReadOnly = tAktif
        NamaPerkiraan.ReadOnly = tAktif
        If LAdd Then
            cmbNoGolongan.Visible = Not tAktif
            CmbNoKelompok.Visible = Not tAktif
            cmbNoKode.Visible = Not tAktif
            cmbSubPerkiraan.Visible = Not tAktif
        End If
        Me.Text = "Kode GL"
    End Sub

    Private Sub cmdSimpan_Click(sender As Object, e As EventArgs) Handles cmdSimpan.Click
        Dim MsgSQL As String, RSCek As New DataTable
        If LAdd Then
            Dim tmpCek As String = ""
            If cmbJenisTabel.Text = "GOLONGAN" Then
                MsgSQL = "SELECT NM_GOL FROM M_GOLONGAN " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_GOL = '" & NoGolongan.Text & "' "
                tmpCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(tmpCek) <> "" Then
                    MsgBox(NoGolongan.Text & " sudah dipakai untuk " & tmpCek, vbCritical + vbOKOnly)
                    NoGolongan.Focus()
                    Exit Sub
                End If
                MsgSQL = "Insert Into m_Golongan (NO_Gol, NM_Gol, AktifYN, LastUPD) " &
                    "Values('" & NoGolongan.Text & "', '" & NamaGolongan.Text & "', " &
                    " 'Y', GetDate())"
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "KELOMPOK" Then
                MsgSQL = "SELECT NM_KLP FROM M_KELOMPOK " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_KLP = '" & NoKelompok.Text & "' "
                tmpCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(tmpCek) <> "" Then
                    MsgBox(NoKelompok.Text & " sudah dipakai untuk " & tmpCek, vbCritical + vbOKOnly)
                    NoKelompok.Focus()
                    Exit Sub
                End If
                MsgSQL = "Insert Into m_Kelompok (NO_Klp, NO_GOL, NM_Klp, AktifYN, " &
                    "LastUPD) Values('" & NoKelompok.Text & "', '" & Trim(Mid(cmbNoGolongan.Text, 1, 8)) & "', " &
                    "'" & NamaKelompok.Text & "', 'Y', GetDate()) "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "KODE" Then
                MsgSQL = "SELECT NM_Kode FROM M_KODE " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_Kode = '" & NoKode.Text & "' "
                tmpCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(tmpCek) <> "" Then
                    MsgBox(NoKode.Text & " sudah dipakai untuk " & tmpCek, vbCritical + vbOKOnly)
                    NoKode.Focus()
                    Exit Sub
                End If
                MsgSQL = "Insert Into m_Kode (NO_Kode, NO_klp, NM_Kode, AktifYN, " &
                    "LastUPD) Values('" & NoKode.Text & "', '" & Trim(Mid(CmbNoKelompok.Text, 1, 8)) & "', " &
                    "'" & NamaKode.Text & "', 'Y', GetDate()) "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "SUB PERKIRAAN" Then
                Dim nmSubPerkiraan As String = ""
                MsgSQL = "SELECT NM_SUB FROM M_SUBPERKIRAAN " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_SUB = '" & NoSubPerkiraan.Text & "' "
                nmSubPerkiraan = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(nmSubPerkiraan) <> "" Then
                    MsgBox(NoSubPerkiraan.Text & " sudah dipakai untuk " & nmSubPerkiraan, vbCritical + vbOKOnly, ".:Warning !")
                    NoSubPerkiraan.Focus()
                    Exit Sub
                End If
                MsgSQL = "Insert Into m_SUBPERKIRAAN (NO_SUB, NO_KODE, NM_SUB, AktifYN, " &
                    "LastUPD) Values('" & NoSubPerkiraan.Text & "', '" & Mid(cmbNoKode.Text, 1, 8) & "', " &
                    "'" & NamaSubPerkiraan.Text & "', 'Y', GetDate()) "
                Proses.ExecuteNonQuery(MsgSQL)
            ElseIf cmbJenisTabel.Text = "PERKIRAAN" Then
                Dim nmPerkiraan As String = ""
                MsgSQL = "SELECT NM_PERKIRAAN FROM M_PERKIRAAN " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_PERKIRAAN = '" & NoPerkiraan.Text & "' "
                nmPerkiraan = Proses.ExecuteSingleStrQuery(MsgSQL)
                If nmPerkiraan <> "" Then
                    MsgBox(NoPerkiraan.Text & " sudah dipakai untuk " & nmPerkiraan, vbCritical + vbOKOnly, ".:Warning !")
                    NoPerkiraan.Focus()
                    Exit Sub
                End If
                MsgSQL = "Insert Into m_PERKIRAAN (NO_PERKIRAAN, NO_SUB, NM_PERKIRAAN, SAkhir, AktifYN, " &
                    "LastUPD) Values('" & NoPerkiraan.Text & "', '" & Trim(Mid(cmbSubPerkiraan.Text, 1, 15)) & "', " &
                    "'" & NamaPerkiraan.Text & "', 0, 'Y', GetDate()) "
                Proses.ExecuteNonQuery(MsgSQL)
            End If
            LAdd = False
            LEdit = False
            AturTombol(True)
        ElseIf LEdit Then
            SimpanPerubahan()
        End If
    End Sub

    Private Sub SimpanPerubahan()
        Dim MsgSQL As String, RSCek As String = ""
        If PEdit = "GOLONGAN" Then 'GOLONGAN
            If tNoGolongan <> NoGolongan.Text Then
                MsgSQL = "SELECT NM_GOL FROM M_GOLONGAN " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_GOL = '" & NoGolongan.Text & "' "
                RSCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(RSCek) <> "" Then
                    MsgBox(NoGolongan.Text & " sudah dipakai untuk " & RSCek, vbCritical + vbOKOnly)
                    NoGolongan.Focus()
                    Exit Sub
                End If
            End If
            MsgSQL = "update M_GOLONGAN set " &
                "NM_GOL = '" & NamaGolongan.Text & "', " &
                "NO_GOL = '" & NoGolongan.Text & "' " &
                "WHERE AKTIFYN = 'Y' " &
                "  AND NO_GOL = '" & tNoGolongan & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        ElseIf PEdit = "KELOMPOK" Then 'KELOMPOK
            If tNoKelompok <> NoKelompok.Text Then
                MsgSQL = "SELECT NM_KLP FROM M_KELOMPOK " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_KLP = '" & NoKelompok.Text & "' "
                RSCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(RSCek) <> "" Then
                    MsgBox(NoKelompok.Text & " sudah dipakai untuk " & RSCek, vbCritical + vbOKOnly)
                    NoKelompok.Focus()
                    Exit Sub
                End If
            End If
            MsgSQL = "update M_Kelompok set " &
                "NM_KLP = '" & NamaKelompok.Text & "', " &
                "NO_KLP = '" & NoKelompok.Text & "' " &
                "WHERE AKTIFYN = 'Y' " &
                "  AND NO_KLP = '" & tNoKelompok & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        ElseIf PEdit = "KODE" Then 'KODE
            If tNoKode <> NoKode.Text Then
                MsgSQL = "SELECT NM_Kode FROM M_KODE " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_Kode = '" & NoKode.Text & "' "
                RSCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(RSCek) <> "" Then
                    MsgBox(NoKode.Text & " sudah dipakai untuk " & RSCek, vbCritical + vbOKOnly)
                    NoKode.Focus()
                    Exit Sub
                End If
            End If
            MsgSQL = "update M_Kode set " &
                "NM_Kode = '" & NamaKode.Text & "', " &
                "NO_Kode = '" & NoKode.Text & "' " &
                "WHERE AKTIFYN = 'Y' " &
                "  AND NO_Kode = '" & tNoKode & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        ElseIf PEdit = "SUB PERKIRAAN" Then 'SUB PERKIRAAN
            If tNoSUB <> NoSubPerkiraan.Text Then
                MsgSQL = "SELECT NM_SUB FROM M_SUBPERKIRAAN " &
                    "WHERE AKTIFYN = 'Y' " &
                    "  AND NO_SUB = '" & NoSubPerkiraan.Text & "' "
                RSCek = Proses.ExecuteSingleStrQuery(MsgSQL)
                If Trim(RSCek) <> "" Then
                    MsgBox(NoSubPerkiraan.Text & " sudah dipakai untuk " & RSCek, vbCritical + vbOKOnly)
                    NoSubPerkiraan.Focus()
                    Exit Sub
                End If
            End If
            MsgSQL = "update M_SUBPERKIRAAN set " &
                "NM_SUB = '" & NamaSubPerkiraan.Text & "', " &
                "NO_SUB = '" & NoSubPerkiraan.Text & "' " &
                "WHERE AKTIFYN = 'Y' " &
                "  AND NO_SUB = '" & tNoSUB & "' "
            Proses.ExecuteNonQuery(MsgSQL)
        ElseIf PEdit = "PERKIRAAN" Then
            MsgSQL = "UPDATE M_PERKIRAAN SET " &
                "NM_PERKIRAAN = '" & NamaPerkiraan.Text & "' " &
                "WHERE AKTIFYN = 'Y' " &
                "  AND NO_PERKIRAAN = '" & NoPerkiraan.Text & "' "
            Proses.ExecuteNonQuery(MsgSQL)

            MsgSQL = "update m_Penyusutan Set " &
            "NamaAccount = NM_Perkiraan " &
            "  From m_Penyusutan inner join m_Perkiraan on " &
            " KodeGL = No_Perkiraan "
            Proses.ExecuteNonQuery(MsgSQL)

            MsgSQL = "update m_SaldoAwalCompany Set " &
            "Nama = NM_Perkiraan " &
            " From m_SaldoAwalCompany inner join m_Perkiraan on " &
            " COA = NO_Perkiraan"
            Proses.ExecuteNonQuery(MsgSQL)

        End If
        'NoGolongan.Enabled = True
        'NamaGolongan.Enabled = True
        'NoKelompok.Enabled = True
        'NamaKelompok.Enabled = True
        'NoKode.Enabled = True
        'NamaKode.Enabled = True
        LAdd = False
        LEdit = False
        AturTombol(True)
        '    LstTree
    End Sub

    Private Sub Form_KeuKodeGL_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmbJenisTabel.Items.Clear()
        cmbJenisTabel.Items.Add("GOLONGAN")
        cmbJenisTabel.Items.Add("KELOMPOK")
        cmbJenisTabel.Items.Add("KODE")
        cmbJenisTabel.Items.Add("SUB PERKIRAAN")
        cmbJenisTabel.Items.Add("PERKIRAAN")
        ClearTextBoxes()
        UserID = FrmMenuUtama.TsPengguna.Text
        tTambah = Proses.UserAksesTombol(UserID, "105_KODE_GL", "baru")
        tEdit = Proses.UserAksesTombol(UserID, "105_KODE_GL", "edit")
        tHapus = Proses.UserAksesTombol(UserID, "105_KODE_GL", "hapus")
        tLaporan = Proses.UserAksesTombol(UserID, "105_KODE_GL", "laporan")
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
        cmbJenisTabel.SelectedIndex = -1
    End Sub

    Private Sub NoPerkiraan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoPerkiraan.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaPerkiraan.Focus()
        End If
    End Sub

    Private Sub NoKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoKode.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaKode.Focus()
        End If
    End Sub

    Private Sub NoKelompok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoKelompok.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaKelompok.Focus()
        End If
    End Sub

    Private Sub NoGolongan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoGolongan.KeyPress
        If e.KeyChar = Chr(13) Then
            NamaGolongan.Focus()
        End If
    End Sub
End Class