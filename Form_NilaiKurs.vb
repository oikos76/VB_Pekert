Public Class Form_NilaiKurs
    Dim SQL As String, LAdd As Boolean, LEdit As Boolean
    Dim Proses As New ClsKoneksi
    Dim tblData As New DataTable
    Private Sub Form_NilaiKurs_Load(sender As Object, e As EventArgs) Handles Me.Load

        DGView.Rows.Clear()
        DGView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGView.BackgroundColor = Color.LightGray

        DGView.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen     'LightSkyBlue
        DGView.DefaultCellStyle.SelectionForeColor = Color.White

        DGView.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        DGView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGView.AllowUserToResizeColumns = True

        DGView.RowsDefaultCellStyle.BackColor = Color.LightCyan
        DGView.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        DGView.ColumnHeadersDefaultCellStyle().Alignment = DataGridViewContentAlignment.MiddleCenter

        SQL = "SELECT * FROM m_Currency"
        tblData = Proses.ExecuteQuery(SQL)

        With Me.DGView.RowTemplate
            .Height = 32
            .MinimumHeight = 32
        End With

        With tblData.Columns(0)
            For a = 0 To tblData.Rows.Count - 1
                Application.DoEvents()
                DGView.Rows.Add(.Table.Rows(a) !Nama,
                .Table.Rows(a) !Symbol,
                Format(.Table.Rows(a) !ERCurrent, "###,##0.0000"), "EDIT")
            Next (a)
        End With
    End Sub
End Class