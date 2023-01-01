Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class Form_KasirAG
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable

    Dim SelectedItemIndex As Integer = -1
    Dim footer1 As String = "", footer2 As String = "", footer3 As String = ""
    Private Cmd As SqlCommand

    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Protected CN As SqlConnection
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database


    Private Function OpenConn() As Boolean
        CN = New SqlConnection

        SQL = "Initial Catalog=" & db & "; " &
            "user id=" & dbUserId & ";password=" & pwd & "; " &
            "Persist Security Info=True;" &
            "Data Source=" & ipserver & ";"

        CN.ConnectionString = SQL

        Try
            If CN.State = ConnectionState.Closed Then
                CN.Open()
                Return True
            Else
                CN.Close()
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try
    End Function

    Private Sub CloseConn()
        If Not IsNothing(CN) Then
            CN.Dispose()
            CN.Close()
            CN = Nothing
        End If
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
        DGRequest.Rows.Clear()
        IdRec.Text = ""
        KodeBrg.Text = "<Kode_Brg>"
        NamaBrg.Text = ""
        If txtStatus.Text = "CLOSE" Then
            Tgl.Value = DateAdd("d", 1, Now())
        Else
            Tgl.Value = Now()
        End If
        KodeBrg.ForeColor = Color.Gray
        KodeBrg.BackColor = Color.LightGoldenrodYellow
        NoNota.Text = "<no_nota>"
        NoNota.ForeColor = Color.Gray
        QTY.Text = 1
        QTY.ForeColor = Color.Gray
        satuan.Text = "PCS"
        QtyB.Text = 1
        QtyB.ForeColor = Color.Gray
        cmbSatuanB.SelectedIndex = -1
        PsDisc1.Text = 0
        PsDisc1.ForeColor = Color.Gray
        Disc.Text = 0
        Disc.ForeColor = Color.Gray
        HargaSatuan.Text = 0
        HargaModal.Text = 0
        SubTotal.Text = 0
        Total.Text = 0
        id_cust.Text = "CA00001"
        customer.Text = "Cashier"
    End Sub
    Private Sub clearBarcode()
        KodeBrg.Text = "<Kode_Brg>"
        KodeBrg.ForeColor = Color.Gray
        KodeBrg.BackColor = Color.LightGoldenrodYellow
        NamaBrg.Text = ""
        'NoNota.Text = "<no_nota>"
        'NoNota.ForeColor = Color.Gray
        QTY.Text = 1
        QTY.ForeColor = Color.Gray
        satuan.Text = "PCS"
        QtyB.Text = 1
        QtyB.ForeColor = Color.Gray
        cmbSatuanB.SelectedIndex = -1
        PsDisc1.Text = 0
        PsDisc1.ForeColor = Color.Gray
        Disc.Text = 0
        Disc.ForeColor = Color.Gray
        HargaSatuan.Text = 0
        HargaModal.Text = 0
        SubTotal.Text = 0
        'Total.Text = 0
        Flag.Text = ""
        HargaSatuan_Asli.Text = 0
        HargaModal.Text = 0
        IsiSatB.Text = 0
        IsiSatT.Text = 0
        harga1.Text = 0
        harga2.Text = 0
        harga3.Text = 0
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        'SQL = "Select * From m_setupkasir " &
        '        "Where Kode_Toko = '" & Kode_Toko.Text & "' " &
        '        " and status = 'OPEN' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count <> 0 Then
        '    MsgBox("Status kasir " & dbTable.Rows(0) !userid & " masih belum di TUTUP !", vbCritical + vbOKOnly, ".:Warning !")
        '    Form_Kasir_SetAwal.ShowDialog()
        'End If
        Me.Close()
    End Sub

    Private Sub hitungSubTotal()
        Dim harga1 As Double = 0
        Dim disc1 As Double = 0, xQTY As Double = 0
        If Trim(QTY.Text) = "" Then QTY.Text = 1
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If Trim(HargaSatuan_Asli.Text) = "" Then HargaSatuan_Asli.Text = 0
        If Trim(PsDisc1.Text) = "" Then PsDisc1.Text = 0
        If Trim(Disc.Text) = "" Then Disc.Text = 0

        Dim value As Integer = cmbSatuanB.SelectedIndex
        Select Case value
            Case 0
                xQTY = QtyB.Text * 1
            Case 1
                xQTY = QtyB.Text * 1
            Case 2
                xQTY = QtyB.Text * 1
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
            .SelectionLength = Len(.Text)
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
                QtyB.Text = QTY.Text
            ElseIf Trim(satuan.Text) <> Trim(cmbSatuanB.Text) Then
                Dim value As Integer = cmbSatuanB.SelectedIndex
                Select Case value
                    Case 0
                        HargaSatuan.Text = harga1.Text
                        QtyB.Text = QTY.Text
                    Case 1
                        HargaSatuan.Text = harga2.Text
                        QtyB.Text = Format((QTY.Text * 1) / (IsiSatT.Text * 1), "###,##0.00")
                    Case 2
                        HargaSatuan.Text = harga3.Text
                        QtyB.Text = Format((QTY.Text * 1) / (IsiSatB.Text * 1), "###,##0.00")
                    Case Else
                        HargaSatuan.Text = harga1.Text
                        QtyB.Text = QTY.Text
                End Select
                hitungSubTotal()
            End If
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
            'hitungSubTotal()
            QtyB.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub


    Private Sub AddItem()
        Dim ada As Boolean = False
        For i As Integer = 0 To DGRequest.Rows.Count - 1

            If Trim(KodeBrg.Text) = Trim(DGRequest.Rows(i).Cells(0).Value) Then
                ada = True
                Exit For
            End If
        Next
        If DGRequest.Columns(10).Visible = False Then
            DGRequest.Columns(10).Visible = True
            DGRequest.Columns(11).Visible = True
        End If
        If ada Then
            MsgBox("Kode Barang " & KodeBrg.Text & " " & NamaBrg.Text & " SUDAH pernah di input !", vbCritical + vbOKOnly, ".:Warning !")

        Else
            DGRequest.Rows.Add(KodeBrg.Text,
                            NamaBrg.Text,
                            QTY.Text,
                            satuan.Text,
                            QtyB.Text,
                            cmbSatuanB.Text,
                            HargaSatuan.Text,
                            PsDisc1.Text,
                            Disc.Text,
                            SubTotal.Text,
                             "Edit", "Hapus",
                           Flag.Text,
                           HargaSatuan_Asli.Text,
                           HargaModal.Text)
            clearBarcode()
            HitungTotal()
            KodeBrg.Text = "<Kode_Brg>"
            KodeBrg.Focus()
        End If
    End Sub
    Private Sub HitungTotal()
        Dim sum = (From row As DataGridViewRow In DGRequest.Rows.Cast(Of DataGridViewRow)()
                   Select CDec(row.Cells(9).Value)).Sum
        Total.Text = Format(sum, "###,##0")
    End Sub

    Private Sub KodeBrg_LostFocus(sender As Object, e As EventArgs) Handles KodeBrg.LostFocus
        If KodeBrg.Text = Nothing Then
            KodeBrg.Text = "<Kode_Brg>"
            KodeBrg.ForeColor = Color.Gray
            KodeBrg.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub
    Private Sub KodeBrg_GotFocus(sender As Object, e As EventArgs) Handles KodeBrg.GotFocus
        If KodeBrg.Text = "<Kode_Brg>" Then
            KodeBrg.Text = ""
            KodeBrg.ForeColor = Color.Black
            KodeBrg.BackColor = Color.White
        End If
    End Sub

    Private Sub HargaSatuan_Click(sender As Object, e As EventArgs) Handles HargaSatuan.Click
        HargaSatuan.SelectionStart = HargaSatuan.TextLength
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
    Private Sub HargaSatuan_GotFocus(sender As Object, e As EventArgs) Handles HargaSatuan.GotFocus
        With HargaSatuan
            .SelectionStart = 0
            .SelectionLength = .TextLength
            KodeBrg.ForeColor = Color.Black
            KodeBrg.BackColor = Color.White
        End With
    End Sub
    Private Sub HargaSatuan_LostFocus(sender As Object, e As EventArgs) Handles HargaSatuan.LostFocus
        If HargaSatuan.Text = Nothing Then
            HargaSatuan.Text = "0"
            HargaSatuan.ForeColor = Color.Gray
            HargaSatuan.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub
    Private Sub HargaSatuan_TextChanged(sender As Object, e As EventArgs) Handles HargaSatuan.TextChanged
        If Trim(HargaSatuan.Text) = "" Then HargaSatuan.Text = 0
        If IsNumeric(HargaSatuan.Text) Then
            Dim temp As Double = HargaSatuan.Text
            HargaSatuan.Text = Format(temp, "###,##0")
            HargaSatuan.SelectionStart = HargaSatuan.TextLength
        End If
    End Sub

    Private Sub isiKodeBrg(IdBrg)
        Dim dbT As DataTable
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)
        cmbSatuanB.Items.Clear()
        SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
                "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
                "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori, HPP " &
                " From M_Barang " &
                "Where AktifYN = 'Y' " &
                "  And idRec = '" & KodeBrg.Text & "' "
        dbT = Proses.ExecuteQuery(SQL)
        If dbT.Rows.Count <> 0 Then
            NamaBrg.Text = dbT.Rows(0)!Nama
            KodeBrg.Text = dbT.Rows(0)!IDRec
            QTY.Text = 1
            satuan.Text = dbT.Rows(0)!satuan
            QtyB.Text = 1
            cmbSatuanB.Items.Add(dbT.Rows(0)!satuan)
            cmbSatuanB.Items.Add(IIf(IsDBNull(dbT.Rows(0)!satuanT), "", dbT.Rows(0)!satuanT))
            cmbSatuanB.Items.Add(IIf(IsDBNull(dbT.Rows(0)!satuanB), "", dbT.Rows(0)!satuanB))
            cmbSatuanB.Text = satuan.Text
            IsiSatB.Text = Format(dbT.Rows(0)!IsiSatB, "###,##0")
            IsiSatT.Text = Format(dbT.Rows(0)!IsiSatT, "###,##0")

            HargaSatuan.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
            harga1.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
            harga2.Text = Format(dbT.Rows(0)!PriceList2, "###,##0")
            harga3.Text = Format(dbT.Rows(0)!PriceList3, "###,##0")

            HargaModal.Text = dbT.Rows(0)!HPP
            HargaSatuan_Asli.Text = HargaSatuan.Text
            SubTotal.Text = HargaSatuan.Text
        End If
    End Sub
    Private Sub KodeBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles KodeBrg.KeyPress
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)
        If e.KeyChar = Chr(13) Then
            If Trim(KodeBrg.Text) = "" Then Exit Sub
            SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
                "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
                "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori " &
                " From M_Barang " &
                "Where AktifYN = 'Y' " &
                "  And idRec = '" & KodeBrg.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                isiKodeBrg(dbTable.Rows(0) !idrec)
                QTY.Focus()
            Else
                Form_DaftarBarang.kode_toko.Text = KodeToko
                Form_DaftarBarang.txt_Nama_Barang.Text = KodeBrg.Text
                Form_DaftarBarang.Cari()
                Form_DaftarBarang.ShowDialog()
                KodeBrg.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                KodeBrg.ForeColor = Color.Black
                KodeBrg.BackColor = Color.White
                If Trim(KodeBrg.Text) = "" Then
                    KodeBrg.Text = ""
                Else
                    SQL = "Select IDRec " &
                        " From M_Barang " &
                        "Where AktifYN = 'Y' " &
                        "  And idrec = '" & KodeBrg.Text & "' "
                    dbTable = Proses.ExecuteQuery(SQL)
                    If dbTable.Rows.Count <> 0 Then
                        isiKodeBrg(dbTable.Rows(0) !idrec)
                        QTY.Focus()
                    Else
                        KodeBrg.Text = ""
                        KodeBrg.ForeColor = Color.Black
                        KodeBrg.BackColor = Color.White
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Form_KasirAG_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearTextBoxes()

        Kode_Toko.Text = FrmMenuUtama.Kode_Toko.Text
        UserID.Text = FrmMenuUtama.TsPengguna.Text


        'SQL = "Select * From m_setupkasir " &
        '    "Where Kode_Toko = '" & Kode_Toko.Text & "' " &
        '    " And Convert(varchar(8), tgl, 112) = Convert(varchar(8), GetDate(), 112) " &
        '    "order by tgl desc, idrec desc "
        '' " And Convert(varchar(8), tgl, 112) = Convert(varchar(8), GetDate(), 112) "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count <> 0 Then
        '    txtStatus.Text = dbTable.Rows(0) !status
        'Else
        '    MsgBox("Status kasir hari ini belum di buka !", vbCritical + vbOKOnly, ".:Warning !")
        '    AturTombol(False)
        '    cmdBayar.Visible = False
        '    cmdBatal.Visible = False
        '    cmdExit.Enabled = True
        'End If

        'txtStatus.Text = My.Settings.Kasir
        'If txtStatus.Text = "CLOSE" Then
        '    If (MsgBox("Status kasir = CLOSE, " & vbCrLf + vbCrLf & " Lanjut untuk tanggal besok ? ", vbCritical + vbYesNo, ".:Warning !") = vbYes) Then
        '        Tgl.Value = DateAdd("d", 1, Now())
        '    Else
        '        txtStatus.BackColor = Color.Red
        '        txtStatus.ForeColor = Color.LightYellow
        '        AturTombol(False)
        '        cmdBayar.Visible = False
        '        cmdBatal.Visible = False
        '        cmdExit.Enabled = True
        '    End If

        'Else
        txtStatus.BackColor = SystemColors.Control
            txtStatus.ForeColor = SystemColors.ControlText
        'End If
        PanelPembayaran.Visible = False
        Panel2.Enabled = False
        Panel6.Enabled = False
        cmdBatal.Visible = False
        cmdBayar.Visible = False

        Tgl.Enabled = False
        SQL = "Select kode_toko, m_toko.nama, company, m_toko.alamat1, website, getdate() hariini, " &
             "     psCharge, convert(char(8), getdate(), 112) hari_ini, footer1, footer2, footer3  " &
            " From M_Company inner join m_toko on kode_toko = idrec " &
            "Where aktifYN = 'Y' " &
            "  and kode_toko = '" & Kode_Toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            ' Me.Text = dbTable.Rows(0) !company
            Kode_Toko.Text = dbTable.Rows(0) !kode_toko
            Nama_Toko.Text = dbTable.Rows(0) !nama + ", " + dbTable.Rows(0) !alamat1
            If dbTable.Rows(0) !hari_ini <> Format(Now, "yyyyMMdd") Then
                MsgBox("Tanggal Komputer ini tidak sama dengan Tanggal SERVER !", vbCritical, ".: Call 911")
                Me.Close()
            End If
            'Tgl.Value = dbTable.Rows(0) !hariini
            WebSite.Text = dbTable.Rows(0) !website
            PsCharge.Text = dbTable.Rows(0) !pscharge
            footer1 = IIf(IsDBNull(dbTable.Rows(0) !footer1), "", dbTable.Rows(0) !footer1)
            footer2 = IIf(IsDBNull(dbTable.Rows(0) !footer1), "", dbTable.Rows(0) !footer2)
            footer3 = IIf(IsDBNull(dbTable.Rows(0) !footer1), "", dbTable.Rows(0) !footer3)
        End If
        txtStatusTgl.Visible = False
        DGRequest.Font = New Font("Arial", 10, FontStyle.Regular)
        DGRequest.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGRequest.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

        DGRequest.GridColor = Color.DarkBlue
        DGRequest.CellBorderStyle = DataGridViewCellBorderStyle.Single
        DGRequest.BackgroundColor = Color.LightGray
        DGRequest.DefaultCellStyle.SelectionBackColor = Color.LightCyan
        DGRequest.DefaultCellStyle.SelectionForeColor = Color.Black
        DGRequest.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGRequest.RowsDefaultCellStyle.BackColor = Color.LightCyan          'LightGoldenrodYellow
        DGRequest.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        With Me.DGRequest.RowTemplate
            .Height = 35
            .MinimumHeight = 25
        End With
        Me.KeyPreview = True

        Dim toolTip1 As New ToolTip()
        toolTip1.ShowAlways = True
        toolTip1.SetToolTip(Me.PsDisc1, "% Disc")
        toolTip1.SetToolTip(Me.Disc, "Discount (Rp)")
        toolTip1.SetToolTip(Me.HargaSatuan, "Harga Satuan" & vbCrLf & "Jgn lupa enter disini yoo...!")
        toolTip1.SetToolTip(Me.cmbSatuanB, "Satuan Besar")
    End Sub

    Private Sub QtyB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyB.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then 'Allows only numbers
            e.KeyChar = e.KeyChar 'Allows only numbers
        ElseIf Asc(e.KeyChar) = 8 Then
            e.KeyChar = e.KeyChar 'Allows "Backspace" to be used
        ElseIf e.KeyChar = " "c Then
            e.KeyChar = " "c 'Allows "Spacebar" to be used
        ElseIf e.KeyChar = ","c Then
            e.KeyChar = ","c
        ElseIf e.KeyChar = "." Then
            If QtyB.Text.IndexOf(".") > -1 Then 'Allows " . " and prevents more than 1 " . "
                e.Handled = True
                Beep()
            End If
        ElseIf e.KeyChar = Chr(13) Then
            If IsNumeric(QtyB.Text) Then
                Dim temp As Double = QtyB.Text
                QtyB.Text = Format(temp, "###,##0.00")
                QtyB.SelectionStart = QtyB.TextLength
            Else
                QtyB.Text = 0
            End If
            hitungSubTotal()
            cmbSatuanB.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub QtyB_LostFocus(sender As Object, e As EventArgs) Handles QtyB.LostFocus
        If QtyB.Text = Nothing Then
            QtyB.Text = "1"
            QtyB.ForeColor = Color.Gray
            QtyB.BackColor = Color.LightGoldenrodYellow
        Else
            'If IsNumeric(QtyB.Text) Then
            '    Dim temp As Double = QtyB.Text
            '    QtyB.Text = Format(temp, "###,##0.00")
            '    QtyB.SelectionStart = QtyB.TextLength
            'Else
            '    QtyB.Text = 0
            'End If
            If Trim(satuan.Text) = Trim(cmbSatuanB.Text) Then
                QTY.Text = QtyB.Text
            ElseIf Trim(satuan.Text) <> Trim(cmbSatuanB.Text) Then
                Dim value As Integer = cmbSatuanB.SelectedIndex
                Select Case value
                    Case 0
                        HargaSatuan.Text = harga1.Text
                        QTY.Text = QtyB.Text
                    Case 1
                        HargaSatuan.Text = harga2.Text
                        QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0.00")
                    Case 2
                        HargaSatuan.Text = harga3.Text
                        QTY.Text = Format((QtyB.Text * 1) * (IsiSatB.Text * 1), "###,##0.00")
                    Case Else
                        HargaSatuan.Text = harga1.Text
                        QTY.Text = QtyB.Text
                End Select
                hitungSubTotal()
            End If
        End If
    End Sub
    Private Sub QtyB_TextChanged(sender As Object, e As EventArgs) Handles QtyB.TextChanged
        If Trim(QtyB.Text) = "" Then QtyB.Text = 0
        If IsNumeric(QtyB.Text) Then
            Dim temp As Double = QtyB.Text
            'QtyB.Text = Format(temp, "###,##0")
            QtyB.SelectionStart = QtyB.TextLength
        Else
            QtyB.Text = 0
        End If
    End Sub

    Private Sub QtyB_GotFocus(sender As Object, e As EventArgs) Handles QtyB.GotFocus
        With QtyB
            .SelectionStart = 0
            .SelectionLength = .TextLength
            .ForeColor = Color.Black
            .BackColor = Color.White
        End With
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
                AddItem()
                KodeBrg.Focus()
            Else
                Disc.Focus()
            End If
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub
    Private Sub PsDisc1_LostFocus(sender As Object, e As EventArgs) Handles PsDisc1.LostFocus
        If PsDisc1.Text = Nothing Then
            PsDisc1.Text = "0"
            PsDisc1.ForeColor = Color.Gray
            PsDisc1.BackColor = Color.LightGoldenrodYellow
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

    Private Sub Disc_LostFocus(sender As Object, e As EventArgs) Handles Disc.LostFocus
        If Disc.Text = Nothing Then
            Disc.Text = "0"
            Disc.ForeColor = Color.Gray
            Disc.BackColor = Color.LightGoldenrodYellow
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
            AddItem()
            KodeBrg.Focus()
        Else
            e.Handled = True  'Disallows all other characters from being used on txtNights.Text
            Beep()
        End If
    End Sub

    Private Sub cmbSatuanB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSatuanB.SelectedIndexChanged
        Dim value As Integer = cmbSatuanB.SelectedIndex
        Select Case value
            Case 0
                HargaSatuan.Text = harga1.Text
                QTY.Text = QtyB.Text
            Case 1
                HargaSatuan.Text = harga2.Text
                QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0")
            Case 2
                HargaSatuan.Text = harga3.Text
                QTY.Text = Format((QtyB.Text * 1) * (IsiSatB.Text * 1), "###,##0")
            Case Else
                HargaSatuan.Text = harga1.Text
                QTY.Text = QtyB.Text
        End Select
        hitungSubTotal()
        HargaSatuan_Asli.Text = HargaSatuan.Text
        If satuan.Text = cmbSatuanB.Text Then
            QTY.Text = QtyB.Text
        Else

        End If
    End Sub
    Private Sub cmbSatuanB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSatuanB.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbSatuanB_SelectedIndexChanged(sender, New EventArgs)
            HargaSatuan.Focus()
        End If
    End Sub

    Private Sub cmbSatuanB_Click(sender As Object, e As EventArgs) Handles cmbSatuanB.Click
        Dim value As Integer = cmbSatuanB.SelectedIndex
        If Trim(QtyB.Text) = "" Then QtyB.Text = 0
        Select Case value
            Case 0
                HargaSatuan.Text = harga1.Text
                QTY.Text = QtyB.Text
            Case 1
                HargaSatuan.Text = harga2.Text
                QTY.Text = Format((QtyB.Text * 1) * (IsiSatT.Text * 1), "###,##0")
            Case 2
                HargaSatuan.Text = harga3.Text
                QTY.Text = Format((QtyB.Text * 1) * (IsiSatB.Text * 1), "###,##0")
            Case Else
                HargaSatuan.Text = harga1.Text
                QTY.Text = QtyB.Text
        End Select
        HargaSatuan_Asli.Text = HargaSatuan.Text
        If satuan.Text = cmbSatuanB.Text Then
            QTY.Text = QtyB.Text
        End If
    End Sub
    Function Bulatkan(nValue As Double, nDigits As Integer) As Double
        Bulatkan = nValue Mod (10 ^ nDigits)
        ' Int(nValue * (10 ^ nDigits) + 0.5) / (10 ^ nDigits) 
    End Function
    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        ClearTextBoxes()
        KodeBrg.ForeColor = Color.Black
        KodeBrg.BackColor = Color.White
        AturTombol(True)
        LAdd = False
        LEdit = False
    End Sub
    Private Sub cmdBayar_Click(sender As Object, e As EventArgs) Handles cmdBayar.Click
        'Form_KasirAG_KeyDown(sender, New System.EventArgs())
        Dim Pembulatan As Double = 0, TotalSales As Double = 0
        'Pembulatan = Bulatkan(Total.Text * 1, 2)
        TotalSales = (Total.Text * 1) ' - (Pembulatan)
        If TotalSales <= 0 Then
            MsgBox("Tidak bisa simpan nota tanpa ada item barangnya. ", vbCritical + vbOKOnly, "Penjualan belum di input!")
            Exit Sub
        End If
        Form_Kasir_Bayar.SubTotal.Text = Total.Text
        Form_Kasir_Bayar.PsDisc.Text = 0
        Form_Kasir_Bayar.Discount.Text = 0
        Form_Kasir_Bayar.Pembulatan.Text = 0 'Pembulatan
        Form_Kasir_Bayar.TotalSales.Text = Format(TotalSales, "###,##0")
        Form_Kasir_Bayar.Tunai.Text = 0
        Form_Kasir_Bayar.Debet.Text = 0
        Form_Kasir_Bayar.Kredit.Text = 0
        Form_Kasir_Bayar.Kembali.Text = 0
        Form_Kasir_Bayar.PsCharge.Text = PsCharge.Text
        Form_Kasir_Bayar.Charge.Text = 0
        Form_Kasir_Bayar.Tunai.Focus()
        Form_Kasir_Bayar.ShowDialog()
        Form_Kasir_Bayar.Close()
    End Sub


    Private Sub IsiPenjualan()
        Dim tblData As New DataTable
        Me.Cursor = Cursors.WaitCursor
        DGRequest.Rows.Clear()
        SQL = "Select * From t_KasirH inner Join t_KasirD on " &
            "idrec = id_rec and t_kasirH.Kode_Toko = t_kasirD.Kode_Toko  " &
            "Where t_kasirD.aktifYN = 'Y' " &
            "  And idrec = '" & IdRec.Text & "' " &
            "Order By t_KasirD.nourut "
        tblData = Proses.ExecuteQuery(SQL)
        With tblData.Columns(0)
            If tblData.Rows.Count <> 0 Then
                IdRec.Text = tblData.Rows(0) !IDRec
                NoNota.Text = tblData.Rows(0) !nonota
                Tgl.Text = tblData.Rows(0) !tglPenjualan
                UserID.Text = tblData.Rows(0) !userid
                Kode_Toko.Text = tblData.Rows(0) !kode_toko
                id_cust.Text = tblData.Rows(0) !idcust
            End If
            For a = 0 To tblData.Rows.Count - 1
                DGRequest.Rows.Add(.Table.Rows(a) !KodeBrg, .Table.Rows(a) !NamaBrg,
                            Format(.Table.Rows(a) !QTY, "###,##0"), .Table.Rows(a) !Satuan,
                            Format(.Table.Rows(a) !QTYB, "###,##0"), .Table.Rows(a) !SatB,
                            Format(.Table.Rows(a) !Harga, "###,##0"),
                            Format(.Table.Rows(a) !PsDisc1, "###,##0"),
                            Format(.Table.Rows(a) !Disc1, "###,##0"),
                            Format(.Table.Rows(a) !Sub_Total, "###,##0"))
                '            ,
                '            "Edit", "Hapus", .Table.Rows(a) !Flag,
                '            .Table.Rows(a) !HargaSatuan_Asli,
                '            .Table.Rows(a) !HargaModal)
            Next (a)
            DGRequest.Columns(10).Visible = False
            DGRequest.Columns(11).Visible = False
        End With
        Nama_Toko.Text = Proses.ExecuteSingleStrQuery("Select nama from m_toko where idrec = '" & Kode_Toko.Text & "' ")
        customer.Text = Proses.ExecuteSingleStrQuery("Select nama from m_Customer where idrec = '" & id_cust.Text & "' ")
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub CetakStrukKasir()
        Dim nPrinter As String = "", nKertas As String = "", nPrintYN As String = ""
        Dim Proses As New ClsKoneksi
        Dim dbTable As DataTable
        Me.Cursor = Cursors.WaitCursor
        Dim NamaPerusahaan As String = "", telp As String = ""
        Dim AlamatPerusahaan As String = "", AlamatPerusahaan2 As String = ""
        SQL = "Select * From M_Toko where idRec = '" & Kode_Toko.Text & "' "
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count <> 0 Then
            NamaPerusahaan = dbTable.Rows(0)!nama
            AlamatPerusahaan = dbTable.Rows(0)!alamat1
            AlamatPerusahaan2 = IIf(IsDBNull(dbTable.Rows(0)!alamat2), "", dbTable.Rows(0)!alamat2)
            telp = IIf(IsDBNull(dbTable.Rows(0) !phone), "", dbTable.Rows(0) !phone)
        Else
            NamaPerusahaan = ""
            AlamatPerusahaan = ""
            AlamatPerusahaan2 = ""
            telp = ""
        End If

        ' terbilang = "#" + tb.Terbilang(CDbl(Form_InvoiceCustomer.Total.Text)) + " Rupiah #"
        nPrinter = My.Settings.NamaPrinter
        nKertas = My.Settings.NamaKertas

        Call OpenConn()

        dttable = New DataTable

        SQL = "select * from t_KasirH inner Join t_KasirD on " &
            "idrec = id_rec and t_kasirH.Kode_Toko = t_kasirD.Kode_Toko  " &
            "Where t_kasirD.aktifYN = 'Y' " &
            " and idrec = '" & IdRec.Text & "' " &
            "Order By t_KasirD.nourut "

        DTadapter = New SqlDataAdapter(SQL, CN)
        Try
            DTadapter.Fill(dttable)
            'If FrmMenuUtama.CompCode.Text = "AG" Then
            '    objRep = New Rpt_StrukKasir
            'Else
            '    objRep = New Rpt_StrukKasirPajak
            'End If
            objRep.SetDataSource(dttable)
            objRep.SetParameterValue("Nama_Perusahaan", NamaPerusahaan)
            objRep.SetParameterValue("Alamat_Perusahaan", AlamatPerusahaan)
            objRep.SetParameterValue("telp", telp)
            objRep.SetParameterValue("website", WebSite.Text)
            objRep.SetParameterValue("footer1", footer1)
            objRep.SetParameterValue("footer2", footer2)
            objRep.SetParameterValue("footer3", footer3)
            objRep.PrintOptions.PaperSize = Proses.GetPapersizeID(NPrinter, NKertas)
            If nPrintYN = "Yes" Then
                objRep.PrintToPrinter(1, False, 0, 0)
            Else
                'Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                'Form_Report.CrystalReportViewer1.Refresh()
                'Form_Report.CrystalReportViewer1.ReportSource = objRep


                Form_Report.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                Form_Report.CrystalReportViewer1.Refresh()
                Form_Report.CrystalReportViewer1.ReportSource = objRep
                Form_Report.CrystalReportViewer1.ShowRefreshButton = False
                Form_Report.CrystalReportViewer1.ShowPrintButton = False
                'Form_Report.CrystalReportViewer1.ShowExportButton = True
                Form_Report.CrystalReportViewer1.ShowParameterPanelButton = False
                Form_Report.ShowDialog()

            End If
            dttable.Dispose()
            DTadapter.Dispose()
            CloseConn()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Sub Form_KasirAG_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F9 Then
            cmdBayar_Click(sender, New System.EventArgs)
        ElseIf e.KeyCode = Keys.Escape Then
            If LAdd Or LEdit Then
                cmdBatal_Click(sender, New System.EventArgs)
            Else
                cmdExit_Click(sender, New System.EventArgs)
            End If
        End If
    End Sub

    Private Sub DGRequest_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellContentClick

    End Sub

    Public Sub SimpanKasir()
        'YYB TK TR 99999
        'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber

        IdRec.Text = Proses.GetMaxId("t_KasirH", "IdRec", Mid(Kode_Toko.Text, 4, 2) & "K.")
        Dim CaraBayar As String = "", TotalBayar As Double = 0, totalBayarCC As Double = 0
        If Trim(Tunai.Text) <> "0" Then
            CaraBayar = "Tunai"
            TotalBayar = Tunai.Text * 1
        ElseIf Trim(Debet.Text) <> "0" Then
            CaraBayar = "Debet"
            TotalBayar = Debet.Text * 1
        ElseIf Trim(Kredit.Text) <> "0" Then
            CaraBayar = "Kredit"
            TotalBayar = Kredit.Text * 1
            Charge.Text = (PsCharge.Text * 1 / 100) * (Kredit.Text * 1)
            totalBayarCC = (Charge.Text * 1) + (Kredit.Text * 1)
        Else
            CaraBayar = "???"
        End If
        If NoNota.Text = "<no_nota>" Then NoNota.Text = ""
        If Trim(Charge.Text) = "" Then Charge.Text = 0

        SQL = "INSERT INTO dbo.t_KasirH (IdRec,TglPenjualan, IdComputer, IdCust, NoNota, CaraBayar, SubTotal, " &
            " PsDisc, Disc, PPN, PsPPN, Pembulatan, TotalSales, Tunai, NoKartuDebet, BayarDebet,  " &
            " NoKartuKredit, BayarKredit, PsCharge, Charge, TBayarKredit, TotalBayar, Kembali, AktifYN, " &
            " LastUPD, UserID, TransferYN, Kode_Toko, NoRetur, NilaiRetur) VALUES ( '" & IdRec.Text & "', " &
            " '" & Format(Tgl.Value, "yyyy-MM-dd") & "', '0', '" & id_cust.Text & "', " &
            " '" & NoNota.Text & "', '" & CaraBayar & "', " & Total.Text * 1 & ", " & PsDisc.Text * 1 & ", " &
            " " & Discount.Text * 1 & ", 0, 0, " & Pembulatan.Text * 1 & ", " & TotalSales.Text * 1 & ",  " &
            " " & Tunai.Text * 1 & ", '" & NoKartuDebet.Text & "', " & Debet.Text * 1 & ", " &
            " '" & NoKartuKredit.Text & "', " & Kredit.Text * 1 & ", " & PsCharge.Text * 1 & "," &
            " " & Charge.Text * 1 & ", " & totalBayarCC & ", " & TotalBayar & ", " & Kembali.Text * 1 & ", " &
            " 'Y', GetDate(), '" & UserID.Text & "', 'N', '" & Kode_Toko.Text & "', '', 0) "
        Proses.ExecuteNonQuery(SQL)
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)
        Dim tNo As String = "", Unit1 As String = "", Unit2 As String = "", Harga As Double = 0
        Dim qty As Double = 0, qtyB As Double = 0, tSubTotal As Double = 0, flag As String = ""
        Dim ps_disc1 As Double = 0, disc1 As Double = 0, HargaModal As Double = 0, HargaSatAsli As Double = 0
        For i As Integer = 0 To DGRequest.Rows.Count - 1
            If i = DGRequest.Rows.Count Then Exit For
            ' If Trim(DGRequest.Rows(i).Cells(0).Value) = "" Then Exit For

            tNo = Microsoft.VisualBasic.Right(101 + i, 2)

            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(2).Value) Then
                qty = 0
            Else
                qty = DGRequest.Rows(i).Cells(2).Value
            End If

            Unit1 = DGRequest.Rows(i).Cells(3).Value
            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(4).Value) Then
                qtyB = 0
            Else
                qtyB = DGRequest.Rows(i).Cells(4).Value
            End If
            Unit2 = DGRequest.Rows(i).Cells(5).Value

            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(6).Value) Then
                Harga = 0
            Else
                Harga = DGRequest.Rows(i).Cells(6).Value
            End If

            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(7).Value) Then
                ps_disc1 = 0
            Else
                ps_disc1 = DGRequest.Rows(i).Cells(7).Value
            End If

            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(8).Value) Then
                disc1 = 0
            Else
                disc1 = DGRequest.Rows(i).Cells(8).Value
            End If
            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(9).Value) Then
                tSubTotal = 0
            Else
                tSubTotal = DGRequest.Rows(i).Cells(9).Value
            End If

            flag = DGRequest.Rows(i).Cells(12).Value

            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(13).Value) Then
                HargaSatAsli = 0
            Else
                HargaSatAsli = DGRequest.Rows(i).Cells(13).Value
            End If

            If Not Information.IsNumeric(DGRequest.Rows(i).Cells(14).Value) Then
                HargaModal = 0
            Else
                HargaModal = DGRequest.Rows(i).Cells(14).Value
            End If
            SQL = "INSERT INTO dbo.t_KasirD (Id_Rec, NoUrut ,KodeBrg, NamaBrg, QTYB, " &
                "SatB, QTY, satuan, PsDisc1, Disc1, Harga, Sub_Total, Flag, HargaSatuan_Asli, " &
                "HargaModal,  AktifYN, LastUpd, UserID, TransferYN, Kode_Toko, QTYRetur) values (" &
                "'" & IdRec.Text & "',  '" & tNo & "', '" & DGRequest.Rows(i).Cells(0).Value & "', " &
                "'" & DGRequest.Rows(i).Cells(1).Value & "', " & qtyB & ", '" & Unit2 & "',  " &
                "" & qty & ", '" & Unit1 & "', " & ps_disc1 & ", " & disc1 & ",  " &
                "" & Harga & ", " & tSubTotal & ", '" & flag & "', " & HargaSatAsli & ", " &
                "" & HargaModal & ", 'Y', GetDate(), '" & UserID.Text & "', 'N', " &
                "'" & Kode_Toko.Text & "', 0) "
            Proses.ExecuteNonQuery(SQL)

            SQL = "Update m_Barang Set Stock" & KodeToko & " = Stock" & KodeToko & " - " & qty * 1 & "  
                Where IDRec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "' "
            Proses.ExecuteNonQuery(SQL)

            'YYB TK TR 99999
            'YY = Tahun; B = Bulan; TK = Toko; TR = JenisTR; 99999 = RunNumber
            Dim idtr As String = Proses.GetMaxId_Transaksi("t_transaksi", "idtr", KodeToko & "TR")
            Dim saldo As Double = 0
            SQL = "Select stock" & Mid(Kode_Toko.Text, 4, 2) & ", pricelist " &
                "  from m_barang " &
                " where idrec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "'  "

            SQL = "Select stock" & Mid(Kode_Toko.Text, 4, 2) & " as saldo, PriceList " &
                  "  From m_barang " &
                  " Where idrec = '" & Trim(DGRequest.Rows(i).Cells(0).Value) & "'  "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                If IsDBNull(dbTable.Rows(0) !PriceList) Then
                    Harga = 0
                Else
                    Harga = dbTable.Rows(0) !PriceList
                End If
                If IsDBNull(dbTable.Rows(0) !saldo) Then
                    saldo = 0
                Else
                    saldo = dbTable.Rows(0) !saldo
                End If
            End If
            'saldo = Proses.ExecuteSingleDblQuery(SQL)
            SQL = "INSERT INTO t_transaksi (idtr, kd_toko, jenistr, idrec, tgltr ,kodebrg, " &
                    "stockin, stockout, saldo, satuan, qty, harga, subtotal, userid, lastupd, kode_Toko) " &
                    "VALUES ( '" & idtr & "', '" & Kode_Toko.Text & "', 'KASIR', '" & IdRec.Text & "', " &
                    "'" & Format(Tgl.Value, "yyyy-MM-dd") & "', '" & DGRequest.Rows(i).Cells(0).Value & "',  " &
                    "0, " & qty & ", " & saldo & ",  '" & Unit1 & "',  " & qty & ",  " & Harga & ", " &
                    "" & tSubTotal & ", '" & UserID.Text & "', GetDate(), '" & Kode_Toko.Text & "') "
            Proses.ExecuteNonQuery(SQL)
        Next i
        CetakStrukKasir()
        cmdBatal_Click(Me, New System.EventArgs)
    End Sub


    Private Sub IsiKodeBrg_Edit(mKodeBrg As String)
        Dim dbT As DataTable
        Dim KodeToko As String = Mid(Kode_Toko.Text, 4, 2)

        SQL = "Select IDRec, Nama, Satuan, SatuanT, SatuanB, Stock" & KodeToko & " as QTY, " &
            "IsiSatT, IsiSatB, PriceList, PriceList2, PriceList3, HargaToko1, HargaToko2, " &
            "HargaToko3, HargaMall1, HargaMall2, HargaMall3, Kategori, HPP " &
            " From M_Barang " &
            "Where AktifYN = 'Y' " &
            "  And idRec = '" & mKodeBrg & "' "
        dbT = Proses.ExecuteQuery(SQL)
        If dbT.Rows.Count <> 0 Then
            With Form_Kasir_Edit
                .cmbSatuanB.Items.Clear()
                .cmbSatuanB.Items.Add(dbT.Rows(0) !satuan)
                .cmbSatuanB.Items.Add(dbT.Rows(0) !satuanT)
                .cmbSatuanB.Items.Add(dbT.Rows(0) !satuanB)
                .cmbSatuanB.Text = satuan.Text
                .IsiSatB.Text = Format(dbT.Rows(0) !IsiSatB, "###,##0")
                .IsiSatT.Text = Format(dbT.Rows(0) !IsiSatT, "###,##0")

                .HargaSatuan.Text = Format(dbT.Rows(0) !PriceList, "###,##0")
                .harga1.Text = Format(dbT.Rows(0)!PriceList, "###,##0")
                .harga2.Text = Format(dbT.Rows(0)!PriceList2, "###,##0")
                .harga3.Text = Format(dbT.Rows(0)!PriceList3, "###,##0")

                .HargaModal.Text = dbT.Rows(0) !HPP
                .HargaSatuan_Asli.Text = HargaSatuan.Text
                .SubTotal.Text = HargaSatuan.Text
            End With
        End If
    End Sub
    Private Sub DGRequest_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGRequest.CellClick
        If DGRequest.Rows.Count = 0 Then Exit Sub

        Dim tNamaBrg As String = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value + ", " +
            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value + " " +
            DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value
        If e.ColumnIndex = 10 Then 'Edit
            If Trim(tNamaBrg) <> "" Then
                With Form_Kasir_Edit
                    .RowIndex.Text = DGRequest.CurrentCell.RowIndex
                    .KodeBrg.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value
                    .NamaBrg.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(1).Value
                    .QTY.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(2).Value
                    .satuan.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(3).Value
                    .Pack.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(4).Value    'qtyb
                    .HargaSatuan.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(6).Value
                    .PsDisc1.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(7).Value
                    .Disc.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(8).Value
                    .SubTotal.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(9).Value
                    .Flag.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(12).Value
                    .HargaSatuan_Asli.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(13).Value
                    .HargaModal.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(14).Value

                    IsiKodeBrg_Edit(DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(0).Value)

                    .cmbSatuanB.Text = DGRequest.Rows(DGRequest.CurrentCell.RowIndex).Cells(5).Value 'satuanb

                End With
                Form_Kasir_Edit.ShowDialog()
                HitungTotal()
            End If
        ElseIf e.ColumnIndex = 11 Then 'Hapus
            If Trim(tNamaBrg) <> "" Then
                '    MsgBox("Kas BON ini sudah di buatkan Invoice." & vbCrLf & " Tidak Bisa Edit/Hapus!", vbCritical + vbOKOnly, ".:Warning!")

                If MsgBox("Yakin hapus " & tNamaBrg & "?", vbYesNo + vbInformation, "Confirm!") = vbYes Then
                    DGRequest.Rows.RemoveAt(e.RowIndex)
                    HitungTotal()
                End If
            End If
        End If
    End Sub

    Private Sub NoNota_GotFocus(sender As Object, e As EventArgs) Handles NoNota.GotFocus
        If NoNota.Text = "<no_nota>" Then
            NoNota.Text = ""
            NoNota.ForeColor = Color.Black
            NoNota.BackColor = Color.White
        End If
    End Sub

    Private Sub KodeBrg_TextChanged(sender As Object, e As EventArgs) Handles KodeBrg.TextChanged
        If Len(Trim(KodeBrg.Text)) < 1 Then
            'clearBarcode()
            NamaBrg.Text = ""
            QTY.Text = 1
            QTY.ForeColor = Color.Gray
            satuan.Text = "PCS"
            QtyB.Text = 1
            QtyB.ForeColor = Color.Gray
            cmbSatuanB.SelectedIndex = -1
            PsDisc1.Text = 0
            PsDisc1.ForeColor = Color.Gray
            Disc.Text = 0
            Disc.ForeColor = Color.Gray
            HargaSatuan.Text = 0
            HargaModal.Text = 0
            SubTotal.Text = 0
            Flag.Text = ""
            HargaSatuan_Asli.Text = 0
            HargaModal.Text = 0
            IsiSatB.Text = 0
            IsiSatT.Text = 0
            harga1.Text = 0
            harga2.Text = 0
            harga3.Text = 0
        End If
    End Sub

    Private Sub cmdDaftar_Click(sender As Object, e As EventArgs) Handles cmdDaftar.Click
        'SQL = "Select t_KasirH.IdRec, TglPenjualan, KodeBrg, NamaBrg, QTY, Satuan, " &
        '    " QtyB, SatB, Harga, Sub_Total, idCust, m_Customer.Nama, nonota " &
        '    "From t_KasirH inner Join t_KasirD On " &
        '    "idrec = id_rec And t_KasirH.Kode_Toko = t_kasirD.Kode_Toko  " &
        '    " inner join m_Customer on idCust = m_Customer.IdRec " &
        '    "Where t_KasirD.aktifYN = 'Y' " &
        '    "  and t_KasirH.kode_toko = '" & Kode_Toko.Text & "' " &
        '    "  and convert(varchar(8), tglpenjualan, 112) >=  convert(varchar(8), dateadd(d,-14, getdate()),112) " &
        '    "Order By t_KasirH.idrec desc, t_KasirD.nourut "
        'SQL = "Select IDRec, tglPenjualan, SubTotal, Disc, TotalSales From t_KasirH " &
        '    " Where t_KasirH.kode_toko = '" & Kode_Toko.Text & "' " &
        '    "  and convert(varchar(8), tglpenjualan, 112) >=  convert(varchar(8), dateadd(d,-21, getdate()),112) " &
        '    "Order By t_KasirH.idrec desc "
        'With Form_Kasir_Daftar
        '    .txtQuery.Text = SQL
        '    .Kode_Toko.Text = FrmMenuUtama.Kode_Toko.Text
        '    .nama_toko.Text = FrmMenuUtama.Nama_Toko.Text
        '    .Text = "Daftar Penjualan Kasir"
        '    If Kode_Toko.Text = "AG020" Then
        '        .Kode_Toko.ReadOnly = False
        '        .BackColor = Color.AliceBlue
        '    Else
        '        .Kode_Toko.ReadOnly = True
        '        .BackColor = Color.White
        '    End If
        '    .DGView.Focus()
        '    .ShowDialog()
        'End With
        IdRec.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        IsiPenjualan()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        If IdRec.Text <> "" Then
            cmdPrint.Enabled = False
            CetakStrukKasir()
            cmdPrint.Enabled = True
        Else
            MsgBox("No Penjualan Masih Kosong !", vbCritical + vbOKOnly, ".: Warning !")
            Exit Sub
        End If
    End Sub

    Private Sub btnCariKodeBrg_Click(sender As Object, e As EventArgs) Handles btnCariKodeBrg.Click
        btnCariKodeBrg.Enabled = False
        Dim mCari As String = ""
        If Trim(KodeBrg.Text) = "<Kode_Brg>" Then
            mCari = ""
        Else
            mCari = Trim(KodeBrg.Text)
        End If

        Form_DaftarBarang.kode_toko.Text = Mid(Kode_Toko.Text, 4, 2)
        Form_DaftarBarang.tCari.Text = mCari
        Form_DaftarBarang.Cari()
        Form_DaftarBarang.ShowDialog()
        KodeBrg.Text = FrmMenuUtama.TSKeterangan.Text
        FrmMenuUtama.TSKeterangan.Text = ""
        KodeBrg.ForeColor = Color.Black
        KodeBrg.BackColor = Color.White
        If Trim(KodeBrg.Text) = "" Then
            KodeBrg.Text = ""
        Else
            SQL = "Select IDRec " &
                " From M_Barang " &
                "Where AktifYN = 'Y' " &
                "  And idrec = '" & KodeBrg.Text & "' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                isiKodeBrg(dbTable.Rows(0)!idrec)
                QTY.Focus()
            Else
                KodeBrg.Text = ""
                KodeBrg.ForeColor = Color.Black
                KodeBrg.BackColor = Color.White
            End If
        End If
        btnCariKodeBrg.Enabled = True
    End Sub

    Private Sub id_cust_TextChanged(sender As Object, e As EventArgs) Handles id_cust.TextChanged
        If Trim(id_cust.Text) = "" Then
            customer.Text = ""
        End If
    End Sub

    Private Sub cmdBaru_Click(sender As Object, e As EventArgs) Handles cmdBaru.Click
        ClearTextBoxes()
        AturTombol(False)
        KodeBrg.ForeColor = Color.Black
        KodeBrg.BackColor = Color.White
        LAdd = True
        LEdit = False
        KodeBrg.Focus()
    End Sub

    Public Sub AturTombol(ByVal tAktif As Boolean)
        cmdBaru.Enabled = tAktif
        cmdDaftar.Enabled = tAktif
        cmdPrint.Enabled = tAktif
        idComp.Enabled = tAktif
        btnAdd.Visible = Not tAktif
        cmdBayar.Visible = Not tAktif
        cmdBatal.Visible = Not tAktif
        cmdExit.Enabled = tAktif
        Panel2.Enabled = Not tAktif
        Panel6.Enabled = Not tAktif
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub SubTotal_TextChanged(sender As Object, e As EventArgs) Handles SubTotal.TextChanged

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        hitungSubTotal()
        AddItem()
        KodeBrg.Focus()
    End Sub

    Private Sub Kode_Toko_Click(sender As Object, e As EventArgs) Handles Kode_Toko.Click

    End Sub

    Private Sub idComp_Click(sender As Object, e As EventArgs) Handles idComp.Click
        Dim userid As String = FrmMenuUtama.TsPengguna.Text
        Dim akses As Boolean = Proses.UserAksesMenu(UserID, "SETTING_KASIR")
        If akses Then
            'Form_Kasir_SetAwal.ShowDialog()
            cmdExit_Click(sender, e)
        End If
    End Sub

    Private Sub NoNota_LostFocus(sender As Object, e As EventArgs) Handles NoNota.LostFocus
        If NoNota.Text = Nothing Then
            NoNota.Text = "<no_nota>"
            NoNota.ForeColor = Color.Gray
            NoNota.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub NoNota_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NoNota.KeyPress
        If e.KeyChar = Chr(13) Then KodeBrg.Focus()
    End Sub

    Private Sub id_cust_KeyPress(sender As Object, e As KeyPressEventArgs) Handles id_cust.KeyPress
        If e.KeyChar = Chr(13) Then
            'Form_Daftar.txtQuery.Text = "Select idrec, kode_toko, nama, alamat1, alamat2, Kota, Prov, KodePos, Phone " &
            '    " From m_Customer " &
            '    "Where AktifYN = 'Y' " &
            '    "  And ( idrec Like '%" & id_cust.Text & "%' or nama Like '%" & id_cust.Text & "%') " &
            '    "Order By Nama "
            'Form_Daftar.Text = "Daftar Customer"
            'Form_Daftar.ShowDialog()
            'id_cust.Text = FrmMenuUtama.TSKeterangan.Text
            'FrmMenuUtama.TSKeterangan.Text = ""
            If id_cust.Text <> "CA00001" Then id_cust.Text = "CA00001"
            SQL = "Select nama From m_customer " &
               " Where IDRec = '" & id_cust.Text & "' " &
               " and aktifyn = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                customer.Text = dbTable.Rows(0) !nama
                KodeBrg.Focus()
            Else
                customer.Text = ""
                id_cust.Focus()
            End If
        End If
    End Sub

    Private Sub cmbSatuanB_GotFocus(sender As Object, e As EventArgs) Handles cmbSatuanB.GotFocus
        cmbSatuanB.DroppedDown = True
    End Sub
End Class