<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Backup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DGView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbasal = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTujuan = New System.Windows.Forms.TextBox()
        Me.btnBackup = New System.Windows.Forms.Button()
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGView
        '
        Me.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        Me.DGView.Location = New System.Drawing.Point(21, 12)
        Me.DGView.Name = "DGView"
        Me.DGView.ReadOnly = True
        Me.DGView.Size = New System.Drawing.Size(539, 217)
        Me.DGView.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Nama Database"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "AktifYN"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Set Active"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Text = ""
        '
        'Column4
        '
        Me.Column4.HeaderText = "Delete"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 244)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Asal"
        '
        'cbasal
        '
        Me.cbasal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbasal.FormattingEnabled = True
        Me.cbasal.Location = New System.Drawing.Point(87, 240)
        Me.cbasal.Name = "cbasal"
        Me.cbasal.Size = New System.Drawing.Size(124, 21)
        Me.cbasal.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 269)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tujuan"
        '
        'txtTujuan
        '
        Me.txtTujuan.Location = New System.Drawing.Point(87, 265)
        Me.txtTujuan.Name = "txtTujuan"
        Me.txtTujuan.ReadOnly = True
        Me.txtTujuan.Size = New System.Drawing.Size(124, 20)
        Me.txtTujuan.TabIndex = 4
        Me.txtTujuan.Text = "PrimaJaya"
        '
        'btnBackup
        '
        Me.btnBackup.Location = New System.Drawing.Point(239, 240)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(101, 41)
        Me.btnBackup.TabIndex = 6
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.UseVisualStyleBackColor = True
        '
        'Form_Backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 294)
        Me.Controls.Add(Me.btnBackup)
        Me.Controls.Add(Me.txtTujuan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbasal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGView)
        Me.Name = "Form_Backup"
        Me.Text = "Form_Backup"
        CType(Me.DGView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGView As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cbasal As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTujuan As TextBox
    Friend WithEvents btnBackup As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewButtonColumn
    Friend WithEvents Column4 As DataGridViewButtonColumn
End Class
