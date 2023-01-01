<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_DaftarBarang
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_DaftarBarang))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tCari = New System.Windows.Forms.TextBox()
        Me.kode_toko = New System.Windows.Forms.TextBox()
        Me.JenisTR = New System.Windows.Forms.TextBox()
        Me.btnPreviousPage = New System.Windows.Forms.Button()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnFirstPage = New System.Windows.Forms.Button()
        Me.btnLastPage = New System.Windows.Forms.Button()
        Me.txtDisplayPageNo = New System.Windows.Forms.TextBox()
        Me.txt_Nama_Barang = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtQuery = New System.Windows.Forms.TextBox()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Param1 = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.tCari)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(953, 38)
        Me.Panel1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Cari :"
        '
        'tCari
        '
        Me.tCari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tCari.Location = New System.Drawing.Point(70, 10)
        Me.tCari.Name = "tCari"
        Me.tCari.Size = New System.Drawing.Size(245, 22)
        Me.tCari.TabIndex = 1
        '
        'kode_toko
        '
        Me.kode_toko.Location = New System.Drawing.Point(287, 6)
        Me.kode_toko.Name = "kode_toko"
        Me.kode_toko.Size = New System.Drawing.Size(119, 20)
        Me.kode_toko.TabIndex = 10
        Me.kode_toko.Text = "kodeToko"
        Me.kode_toko.Visible = False
        '
        'JenisTR
        '
        Me.JenisTR.Location = New System.Drawing.Point(425, 6)
        Me.JenisTR.Name = "JenisTR"
        Me.JenisTR.ReadOnly = True
        Me.JenisTR.Size = New System.Drawing.Size(119, 20)
        Me.JenisTR.TabIndex = 9
        Me.JenisTR.Text = "<jenis_transaksi>"
        Me.JenisTR.Visible = False
        '
        'btnPreviousPage
        '
        Me.btnPreviousPage.Image = CType(resources.GetObject("btnPreviousPage.Image"), System.Drawing.Image)
        Me.btnPreviousPage.Location = New System.Drawing.Point(639, 3)
        Me.btnPreviousPage.Name = "btnPreviousPage"
        Me.btnPreviousPage.Size = New System.Drawing.Size(48, 23)
        Me.btnPreviousPage.TabIndex = 2
        Me.btnPreviousPage.UseVisualStyleBackColor = True
        '
        'btnNextPage
        '
        Me.btnNextPage.Image = CType(resources.GetObject("btnNextPage.Image"), System.Drawing.Image)
        Me.btnNextPage.Location = New System.Drawing.Point(687, 3)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(48, 23)
        Me.btnNextPage.TabIndex = 3
        Me.btnNextPage.UseVisualStyleBackColor = True
        '
        'btnFirstPage
        '
        Me.btnFirstPage.Image = CType(resources.GetObject("btnFirstPage.Image"), System.Drawing.Image)
        Me.btnFirstPage.Location = New System.Drawing.Point(591, 3)
        Me.btnFirstPage.Name = "btnFirstPage"
        Me.btnFirstPage.Size = New System.Drawing.Size(48, 23)
        Me.btnFirstPage.TabIndex = 1
        Me.btnFirstPage.UseVisualStyleBackColor = True
        '
        'btnLastPage
        '
        Me.btnLastPage.Image = CType(resources.GetObject("btnLastPage.Image"), System.Drawing.Image)
        Me.btnLastPage.Location = New System.Drawing.Point(735, 3)
        Me.btnLastPage.Name = "btnLastPage"
        Me.btnLastPage.Size = New System.Drawing.Size(48, 23)
        Me.btnLastPage.TabIndex = 4
        Me.btnLastPage.UseVisualStyleBackColor = True
        '
        'txtDisplayPageNo
        '
        Me.txtDisplayPageNo.Location = New System.Drawing.Point(855, 6)
        Me.txtDisplayPageNo.Name = "txtDisplayPageNo"
        Me.txtDisplayPageNo.Size = New System.Drawing.Size(100, 20)
        Me.txtDisplayPageNo.TabIndex = 6
        '
        'txt_Nama_Barang
        '
        Me.txt_Nama_Barang.Location = New System.Drawing.Point(448, 5)
        Me.txt_Nama_Barang.Name = "txt_Nama_Barang"
        Me.txt_Nama_Barang.Size = New System.Drawing.Size(96, 20)
        Me.txt_Nama_Barang.TabIndex = 5
        Me.txt_Nama_Barang.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 526)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(953, 28)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.txt_Nama_Barang)
        Me.Panel3.Controls.Add(Me.txtQuery)
        Me.Panel3.Controls.Add(Me.DGView)
        Me.Panel3.Controls.Add(Me.Param1)
        Me.Panel3.Controls.Add(Me.JenisTR)
        Me.Panel3.Controls.Add(Me.kode_toko)
        Me.Panel3.Controls.Add(Me.btnPreviousPage)
        Me.Panel3.Controls.Add(Me.txtDisplayPageNo)
        Me.Panel3.Controls.Add(Me.btnNextPage)
        Me.Panel3.Controls.Add(Me.btnLastPage)
        Me.Panel3.Controls.Add(Me.btnFirstPage)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 38)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(953, 488)
        Me.Panel3.TabIndex = 2
        '
        'txtQuery
        '
        Me.txtQuery.Location = New System.Drawing.Point(12, 168)
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Size = New System.Drawing.Size(430, 20)
        Me.txtQuery.TabIndex = 13
        Me.txtQuery.Visible = False
        '
        'DGView
        '
        Me.DGView.AllowUserToAddRows = False
        Me.DGView.AllowUserToDeleteRows = False
        Me.DGView.AllowUserToOrderColumns = True
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DGView.Location = New System.Drawing.Point(0, 0)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.RowHeadersVisible = False
        Me.DGView.Size = New System.Drawing.Size(953, 488)
        Me.DGView.TabIndex = 0
        '
        'Param1
        '
        Me.Param1.Location = New System.Drawing.Point(425, 32)
        Me.Param1.Name = "Param1"
        Me.Param1.ReadOnly = True
        Me.Param1.Size = New System.Drawing.Size(119, 20)
        Me.Param1.TabIndex = 11
        Me.Param1.Text = "<param1>"
        Me.Param1.Visible = False
        '
        'Form_DaftarBarang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 554)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form_DaftarBarang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daftar Barang"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txt_Nama_Barang As TextBox
    Friend WithEvents txtDisplayPageNo As TextBox
    Friend WithEvents btnLastPage As Button
    Friend WithEvents btnFirstPage As Button
    Friend WithEvents btnPreviousPage As Button
    Friend WithEvents btnNextPage As Button
    Friend WithEvents tCari As TextBox
    Friend WithEvents JenisTR As TextBox
    Friend WithEvents kode_toko As TextBox
    Friend WithEvents Param1 As TextBox
    Friend WithEvents DGView As DataGridView
    Friend WithEvents txtQuery As TextBox
    Friend WithEvents Label1 As Label
End Class
