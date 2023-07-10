Imports System.ComponentModel
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Public Class Form_KodeProdukBuyer
    Dim FotoLoc As String = My.Settings.path_foto
    Dim LAdd As Boolean, LEdit As Boolean, LTambahKode As Boolean, LUangMuka As Boolean
    Dim tTambah As Boolean, tEdit As Boolean, tHapus As Boolean, tLaporan As Boolean
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable, UserID As String, SQL As String, MsgSQL As String
    Private CN As SqlConnection
    Private Cmd As SqlCommand
    Private DA As SqlDataAdapter
    Private UsedVariables() As String
    Protected Ds As DataSet

    Private Sub DGView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellContentClick

    End Sub

    Protected Dt As DataTable

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick

    End Sub

    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter



    Dim objRep As New ReportDocument

    Private Sub cmdCari_Click(sender As Object, e As EventArgs) Handles cmdCari.Click
        tCari.Focus()
    End Sub

    Private Sub tCari_TextChanged(sender As Object, e As EventArgs) Handles tCari.TextChanged

    End Sub

    Private Sub Form_KodeProdukBuyer_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearTextBoxes()
        SetDataGrid()
        DaftarImportir()
    End Sub
    Private Sub SetDataGrid()
        With Me.DGView.RowTemplate
            .Height = 30
            .MinimumHeight = 30
        End With
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView.BackgroundColor = Color.LightGray
        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView.DefaultCellStyle.SelectionForeColor = Color.White
        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect        'DGView.AllowUserToResizeColumns = False
        DGView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        With Me.DGView2.RowTemplate
            .Height = 30
            .MinimumHeight = 30
        End With
        DGView2.CellBorderStyle = DataGridViewCellBorderStyle.Raised
        DGView2.BackgroundColor = Color.LightGray
        DGView2.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen
        DGView2.DefaultCellStyle.SelectionForeColor = Color.White
        DGView2.DefaultCellStyle.WrapMode = DataGridViewTriState.[True]
        DGView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'DGView.AllowUserToResizeColumns = False
        DGView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGView2.RowsDefaultCellStyle.BackColor = Color.LightCyan      'LightGoldenrodYellow
        DGView2.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DGView2.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub DaftarImportir()
        Dim mKondisi As String = "", RSD As New DataTable
        Dim MsgSQL As String
        ClearTextBoxes()
        MsgSQL = "Select A.* " &
        " From m_KodeImportir A " &
        " Where A.AktifYN = 'Y' " & mKondisi & " " &
        " Order By Nama "
        RSD = Proses.ExecuteQuery(MsgSQL)
        DGView.Rows.Clear()
        DGView.Visible = False
        For a = 0 To RSD.Rows.Count - 1
            Application.DoEvents()
            DGView.Rows.Add(RSD.Rows(a) !Nama, RSD.Rows(a) !KodeImportir, RSD.Rows(a) !NegaraAsal,
                            RSD.Rows(a) !Jenis, RSD.Rows(a) !Alamat, RSD.Rows(a) !AlamatKirim,
                            RSD.Rows(a) !Notify, RSD.Rows(a) !Port, RSD.Rows(a) !Catatan,
                            RSD.Rows(a) !Telepon, RSD.Rows(a) !Fax, RSD.Rows(a) !Email,
                            RSD.Rows(a) !ContactPerson, RSD.Rows(a) !BeliYN,
                            Format(RSD.Rows(a) !tglMasuk, "dd-MM-yyyy"),
                            Format(RSD.Rows(a) !LastUPD, "dd-MM-yyyy"))
        Next a
        Proses.CloseConn()
        DGView.Visible = True
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form_KodifProduk_Image.PictureBox1.Image = Nothing
        Form_KodifProduk_Image.LocGmb1.Text = LocGmb1.Text
        Form_KodifProduk_Image.ShowFoto(LocGmb1.Text)
        Form_KodifProduk_Image.ShowDialog()
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
        ShowFoto("")
    End Sub
    Private Sub ShowFoto(NamaFileJPG As String)
        If NamaFileJPG = "" Then
            LocGmb1.Text = ""
            PictureBox1.Image = Nothing
        End If
        Dim filename As String = System.IO.Path.Combine(FotoLoc, NamaFileJPG)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.ImageLocation = filename
        With LocGmb1
            .Location = New Point(PanelPicture.Width \ 2, LocGmb1.Location.Y)
        End With
    End Sub

    Private Sub DGView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView2.CellClick
        Dim tCode As String
        If DGView2.Rows.Count = 0 Then Exit Sub
        tCode = DGView2.Rows(DGView2.CurrentCell.RowIndex).Cells(0).Value
        If tCode = "" Then Exit Sub
        LocGmb1.Text = Trim(tCode) + ".jpg"
        If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
            ShowFoto("")
        Else
            ShowFoto(LocGmb1.Text)
        End If

    End Sub

    Private Sub DGView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellClick
        Dim MsgSQL As String, tKode As String = "", RSD As New DataTable
        If DGView.Rows.Count <> 0 Then
            tKode = DGView.Rows(DGView.CurrentCell.RowIndex).Cells(1).Value
        End If
        MsgSQL = "Select distinct Kode_Buyer, a.Kode_Produk, Deskripsi " &
            " From t_PO A inner join M_KodeProduk ON KodeProduk = Kode_Produk " &
            " Where A.AktifYN = 'Y' " &
            "   And Kode_importir = '" & tKode & "' " &
            " Order By Kode_Buyer Desc "
        RSD = Proses.ExecuteQuery(MsgSQL)
        DGView2.Rows.Clear()
        DGView2.Visible = False
        For a = 0 To RSD.Rows.Count - 1
            Application.DoEvents()
            DGView2.Rows.Add(RSD.Rows(a) !Kode_Produk,
                            RSD.Rows(a) !Kode_Buyer,
                            RSD.Rows(a) !Deskripsi)
        Next a
        DGView2.Visible = True
        MsgSQL = "select count(kode_produk) JKode, isnull(sum(HargaFOB * Jumlah),0) Harga, " &
                "isnull(sum(jumlah),0) Jumlah from t_PI " &
                "where Kode_importir = '" & tKode & "' "
        RSD = Proses.ExecuteQuery(MsgSQL)
        If RSD.Rows.Count <> 0 Then
            TotalMacam.Text = RSD.Rows(0) !JKode
            TotalQTY.Text = Format(RSD.Rows(0) !Jumlah, "###,##0")
            NilaiPesanan.Text = Format(RSD.Rows(0) !Harga, "###,##0")
        End If
        Proses.CloseConn()
        ShowFoto("")
    End Sub

    Private Sub tCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tCari.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim rowindex As Double
            Dim found As Boolean = False
            ShowFoto("")
            DGView2.ClearSelection()
            'DGView2.CurrentCell = Nothing
            For Each row As DataGridViewRow In DGView2.Rows
                If row.Cells.Item("KODE_BY_IMPORTIR_").Value = tCari.Text Then
                    rowindex = row.Index
                    found = True
                    DGView2.Rows(row.Index).Selected = True
                    DGView2.Select()
                    DGView2.FirstDisplayedScrollingRowIndex = rowindex
                    'Dim actie As String = row.Cells("DESKRIPSI_").Value.ToString()
                    'MsgBox(actie)
                    Exit For
                End If
            Next
            If Not found Then
                MsgBox("Item not found")
                tCari.Focus()
            Else
                DGView2.Focus()
                Dim tcode As String = ""
                tcode = DGView2.Rows(rowindex).Cells(0).Value
                If tCode = "" Then Exit Sub
                LocGmb1.Text = Trim(tCode) + ".jpg"
                If Trim(Dir(FotoLoc + "\" + Trim(LocGmb1.Text))) = "" Or Trim(LocGmb1.Text) = "" Then
                    ShowFoto("")
                Else
                    ShowFoto(LocGmb1.Text)
                End If
            End If
        End If
    End Sub
End Class