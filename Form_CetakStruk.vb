
Public Class Form_CetakStruk
    'mendeklarasikan sebuah bangunan persegi...
    Dim vKolomBarang, vKolomQty, vKolomHarga, vKolomTotal As New Rectangle
    'mendeklarasikan array, yang nantinya akan digunakan sebagai record...
    Dim vRecBarang() As String, vRecQty(), vRecHarga(), vRecTotal(), vTotal As Integer

    Private Sub Form_CetakStruk_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'atur posisi dan ukuran masing-masing persegi...
        vKolomBarang = New Rectangle(10, 10, 100, 30)
        vKolomQty = New Rectangle(vKolomBarang.Right + 10, vKolomBarang.Top, 50, 30)
        vKolomHarga = New Rectangle(vKolomQty.Right + 10, vKolomBarang.Top, 70, 30)
        vKolomTotal = New Rectangle(vKolomHarga.Right + 10, vKolomHarga.Top, 100, 30)
        'isi sebuah nilai pada masing-masing array...
        vRecBarang = New String() {"Biskuit Roma", "Biskuit Khong Guan", "Jahe Keraton", "Gulaku", "Permen Sugus", "Shampo Clear"}
        vRecQty = New Integer() {1, 1, 10, 1, 20, 2}
        vRecHarga = New Integer() {40000, 50000, 2000, 9000, 500, 5000}
        Dim getTotal(vRecBarang.Length - 1) As Integer
        Dim GrandTotal As Integer
        For a As Integer = 0 To vRecBarang.Length - 1
            getTotal(a) = vRecQty(a) * vRecHarga(a)
            GrandTotal += getTotal(a)
        Next
        vRecTotal = getTotal
        vTotal = GrandTotal
        'hubungkan property Document pada PrintPreviewControl1 kepada PrintDocument1...
        Me.PrintPreviewControl1.Document = Me.PrintDocument1
        'ukuran PrintPreview1 sesuai dengan ukuran form secara otomatis...
        Me.PrintPreviewControl1.Dock = DockStyle.Fill
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'deklarasikan variabel graphics...
        Dim g As Graphics = e.Graphics
        'gambar masing-masing kolom...
        g.DrawRectangle(Pens.Red, vKolomBarang)
        g.DrawRectangle(Pens.Blue, vKolomQty)
        g.DrawRectangle(Pens.Orange, vKolomHarga)
        g.DrawRectangle(Pens.Green, vKolomTotal)
        'mendeklarasikan string format...
        Using sf As New StringFormat
            'atur nilai rata tulisan mendatar, yaitu menengah...
            sf.Alignment = StringAlignment.Center
            'atur nilai rata tulisan menurun, yaitu menengah...
            sf.LineAlignment = StringAlignment.Center
            'tulis masing-masing kolom, sesuai dengan persegi yang telah dideklarasikan, dan juga string format-nya...
            g.DrawString("Nama Barang", New Font("arial black", 8), Brushes.Black, vKolomBarang, sf)
            g.DrawString("Qty", New Font("comic sans ms", 8, FontStyle.Bold), Brushes.Black, vKolomQty, sf)
            g.DrawString("Harga", New Font("times new roman", 8, FontStyle.Bold), Brushes.Black, vKolomHarga, sf)
            g.DrawString("Total Harga", New Font("tahoma", 8, FontStyle.Bold), Brushes.Black, vKolomTotal, sf)
        End Using
        'mendeklarasikan persegi yang akan bertindak sebagai record...
        Dim vCopyKolomBarang(vRecBarang.Length - 1) As Rectangle
        Dim vCopyKolomQty(vRecBarang.Length - 1) As Rectangle
        Dim vCopyKolomHarga(vRecBarang.Length - 1) As Rectangle
        Dim vCopyKolomTotal(vRecBarang.Length - 1) As Rectangle
        'buat looping sejumlah record barang yang tertulis...
        For a As Integer = 0 To vRecBarang.Length - 1
            'copy masing-masing persegi ke kolom terawal...
            vCopyKolomBarang(a) = vKolomBarang
            vCopyKolomQty(a) = vKolomQty
            vCopyKolomHarga(a) = vKolomHarga
            vCopyKolomTotal(a) = vKolomTotal
            'bila ini adalah index awal...
            If a = 0 Then
                'maka record awal akan berada dibawah kolom judul...
                vCopyKolomBarang(a).Y = vKolomBarang.Bottom + 4
                vCopyKolomQty(a).Y = vKolomQty.Bottom + 4
                vCopyKolomHarga(a).Y = vKolomHarga.Bottom + 4
                vCopyKolomTotal(a).Y = vKolomTotal.Bottom + 4
            Else
                'sedangkan record berikutnya akan berada dibawah record terdahulu/sebelumnya...
                vCopyKolomBarang(a).Y = vCopyKolomBarang(a - 1).Bottom + 4
                vCopyKolomQty(a).Y = vCopyKolomQty(a - 1).Bottom + 4
                vCopyKolomHarga(a).Y = vCopyKolomHarga(a - 1).Bottom + 4
                vCopyKolomTotal(a).Y = vCopyKolomTotal(a - 1).Bottom + 4
            End If
            'mendeklarasikan string format...
            Using sf As New StringFormat
                'atur nilai rata tulisan menurun, yaitu bawah...
                sf.LineAlignment = StringAlignment.Far
                'tulis record barang...
                g.DrawString(vRecBarang(a), New Font("arial", 8), Brushes.Black, vCopyKolomBarang(a), sf)
                'atur nilai rata tulisan mendatar, yaitu tengah...
                sf.Alignment = StringAlignment.Center
                'tulis record quantity...
                g.DrawString(vRecQty(a), New Font("ocr a extended", 8), Brushes.Black, vCopyKolomQty(a), sf)
                'atur nilai rata tulisan mendatar, yaitu kanan...
                sf.Alignment = StringAlignment.Far
                'tulis record harga...
                g.DrawString(Format(vRecHarga(a), "#,##") & ",-", New Font("comic sans ms", 8), Brushes.Black, vCopyKolomHarga(a), sf)
                'tulis record total...
                g.DrawString(Format(vRecTotal(a), "#,##") & ",-", New Font("tahoma", 8, FontStyle.Bold Or FontStyle.Italic), Brushes.Red, vCopyKolomTotal(a), sf)
            End Using
            'gambar garis bawah, mulai dari kolom barang hingga kolom total...
            g.DrawLine(Pens.Red, New Point(vCopyKolomBarang(a).Left, vCopyKolomBarang(a).Bottom), New Point(vCopyKolomTotal(a).Right, vCopyKolomTotal(a).Bottom))
        Next
        'buat persegi untuk kolom total semuanya...
        Dim vRectTotal As Rectangle = vKolomTotal
        'atur posisinya dibawah kolom terakhir...
        vRectTotal.Y = vCopyKolomTotal(vRecBarang.Length - 1).Bottom + 4
        'kasih warna merah...
        g.FillRectangle(Brushes.Red, vRectTotal)
        'mendeklarasikan string format...
        Using sf As New StringFormat
            'atur rata mendatar, yaitu kanan...
            sf.Alignment = StringAlignment.Far
            'atur rata menurun, yaitu tengah...
            sf.LineAlignment = StringAlignment.Center
            'tulis record total...
            g.DrawString(Format(vTotal, "#,##") & ",-", New Font("comic sans ms", 8, FontStyle.Bold), Brushes.White, vRectTotal, sf)
        End Using
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PrintDocument1.Print()
    End Sub
End Class