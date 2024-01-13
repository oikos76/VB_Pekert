Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class Form_DataKirim
    Dim SQL As String
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Protected CN As SqlConnection
    Protected ipserver As String = My.Settings.IPServer
    Protected pwd As String = My.Settings.Password
    Protected dbUserId As String = My.Settings.UserID
    Protected db As String = My.Settings.Database

    Private Sub backup(tFile As String)
        ' https://www.codeproject.com/Tips/279705/Backup-Restore-of-SQL-Server-database-using-VB-NET
        ' how to run backup microsoft sql server express from vb.net
        'str = "sqlcmd -U user01 -P Matius28:19 -S IOTA-NB\SQLEXPRESS -Q "EXEC sp_BackupDatabases @databaseName='asiagarment', @backupLocation ='d:\tmp\', @BackupType='F'""
        Me.Cursor = Cursors.WaitCursor

        Dim Proses As New ClsKoneksi
        Dim sql As String = "BACKUP DATABASE PekertiTRF TO DISK='" & tFile & "' "
        Proses.ExecuteNonQuery(sql)
        cmdKirim.Enabled = True
        Me.Cursor = Cursors.Default
        MsgBox("Database yang akan di kirim ke " & NamaToko.Text & ", di simpan di : " & tFile, vbInformation + vbOKOnly, ".:Finish !")
        Process.Start("explorer.exe", String.Format("/n, /e, {0}", My.Settings.LokasiFile))
    End Sub

    Private Sub Kode_Toko_DoubleClick(sender As Object, e As EventArgs) Handles Kode_Toko.DoubleClick
        Kode_Toko.Text = ""
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub Kode_Toko_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Kode_Toko.KeyPress
        If e.KeyChar = Chr(13) Then
            SQL = "Select * From m_Toko " &
                "Where idrec = '" & Kode_Toko.Text & "' AND AKTIFYN = 'Y' "
            dbTable = Proses.ExecuteQuery(SQL)
            If dbTable.Rows.Count <> 0 Then
                Kode_Toko.Text = dbTable.Rows(0)!idrec
                NamaToko.Text = dbTable.Rows(0)!nama
                NamaFile.Focus()
            Else
                Form_Daftar.txtQuery.Text = "Select * " &
                     " From m_Toko " &
                     "Where AktifYN = 'Y' " &
                     "Order By idRec "
                Form_Daftar.Text = "Daftar Toko"
                Form_Daftar.DGView.Focus()
                Form_Daftar.ShowDialog()
                Kode_Toko.Text = FrmMenuUtama.TSKeterangan.Text
                FrmMenuUtama.TSKeterangan.Text = ""
                SQL = "Select * From m_Toko " &
                    "Where idrec = '" & Kode_Toko.Text & "' AND AKTIFYN = 'Y'  "
                dbTable = Proses.ExecuteQuery(SQL)
                If dbTable.Rows.Count <> 0 Then
                    Kode_Toko.Text = dbTable.Rows(0)!idrec
                    NamaToko.Text = dbTable.Rows(0)!nama
                    NamaFile.Focus()
                Else
                    NamaToko.Text = ""
                    Kode_Toko.Text = ""
                    Kode_Toko.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Kode_Toko_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Kode_Toko.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub Kode_Toko_TextChanged(sender As Object, e As EventArgs) Handles Kode_Toko.TextChanged
        If Len(Kode_Toko.Text) < 1 Then
            NamaToko.Text = ""
        End If
    End Sub

    Private Sub Kode_Toko_KeyDown(sender As Object, e As KeyEventArgs) Handles Kode_Toko.KeyDown
        If e.KeyData = Keys.Tab Then
            SendKeys.Send("{ENTER}")
        End If
    End Sub

    Private Sub Form_DataKirim_Load(sender As Object, e As EventArgs) Handles Me.Load

        KodeTokoAsal.Text = FrmMenuUtama.Kode_Toko.Text
        NamaTokoAsal.Text = FrmMenuUtama.Nama_Toko.Text

        If KodeTokoAsal.Text = "PKT01" Then
            Kode_Toko.Text = "PKT02"
            NamaToko.Text = "Gudang Pekerti"
        Else
            Kode_Toko.Text = "PKT01"
            NamaToko.Text = "Pekerti Pusat - Waru"
        End If

        SQL = "Select * from sys.databases where name = 'PekertiTRF'"
        dbTable = Proses.ExecuteQuery(SQL)
        If dbTable.Rows.Count = 0 Then
            SQL = " CREATE DATABASE PekertiTRF "
            Proses.ExecuteNonQuery(SQL)
        End If
        CekTable()

        NamaFile.Text = My.Settings.LokasiFile

        If (Not System.IO.Directory.Exists(NamaFile.Text)) Then
            System.IO.Directory.CreateDirectory(NamaFile.Text)
        End If


        Tgl1.Value = DateAdd(DateInterval.Day, -7, Now())
        tgl2.Value = Now
        Tgl1.Enabled = True
        tgl2.Enabled = True
        cmdKirim.Visible = True
        cmdKirim.Focus()
    End Sub

    Private Sub CekTable()
        'Dim mNamaDB As String = "C" + KodeTokoAsal.Text
        'SQL = " SELECT * FROM " & mNamaDB & ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 't_DOH' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "CREATE Table " & mNamaDB & ".dbo.t_DOH (
        '     [IDRec] [varchar](15) NOT NULL,
        '     [TglDO] [datetime] NOT NULL,
        '     [ID_Customer] [varchar](7) NOT NULL,
        '     [Customer] [varchar](100) NOT NULL,
        '     [idTokoTujuan] [varchar](5) NOT NULL,
        '     [idTokoAsal] [varchar](5) NOT NULL,
        '     [Keterangan] [varchar](254) NOT NULL,
        '     [Total] [money] NOT NULL,
        '     [status] [char](2) NOT NULL,
        '     [nosj] [varchar](15) NOT NULL,
        '     [AktifYN] [char](1) NOT NULL,
        '     [Kode_Toko] [varchar](5) NOT NULL,
        '     [UserID] [varchar](10) NOT NULL,
        '     [LastUPD] [datetime] NOT NULL
        '    ) ON [PRIMARY] "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "CREATE Table " & mNamaDB & ".dbo.t_DOD ( 
        '     [ID_Rec] [varchar](15) NOT NULL,
        '     [NoUrut] [varchar](5) NOT NULL,
        '     [KodeBrg] [varchar](30) NOT NULL,
        '     [NamaBrg] [varchar](100) NOT NULL,
        '     [QTY] [real] NOT NULL,
        '     [Satuan] [varchar](20) NOT NULL,
        '     [Harga] [money] NOT NULL,
        '     [SubTotal] [money] NOT NULL DEFAULT ((0)),
        '     [AktifYN] [char](1) NOT NULL,
        '     [UserID] [varchar](10) NOT NULL,
        '     [Kode_Toko] [varchar](5) NOT NULL,
        '     [LastUPD] [datetime] NOT NULL
        '    ) ON [PRIMARY] "
        '    Proses.ExecuteNonQuery(SQL)
        'End If

        'SQL = " SELECT * FROM " & mNamaDB & ".INFORMATION_SCHEMA.TABLES " &
        '    "WHERE TABLE_NAME = 't_StockOpnameFisik' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count <> 0 Then
        '    SQL = "DROP Table " & mNamaDB & ".dbo.t_StockOpnameFisik  "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Select top 0 * into " & mNamaDB & ".dbo.t_StockOpnameFisik " &
        '        "From t_StockOpnameFisik "
        '    Proses.ExecuteNonQuery(SQL)
        'Else
        '    SQL = "Select top 0 * into " & mNamaDB & ".dbo.t_StockOpnameFisik " &
        '        "From t_StockOpnameFisik "
        '    Proses.ExecuteNonQuery(SQL)
        'End If


        'SQL = " SELECT * FROM " & mNamaDB & ".INFORMATION_SCHEMA.TABLES " &
        '    "WHERE TABLE_NAME = 'm_barang' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count <> 0 Then
        '    SQL = "DROP Table " & mNamaDB & ".dbo.m_barang "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Select top 0 * into " & mNamaDB & ".dbo.m_barang " &
        '        "From m_Barang "
        '    Proses.ExecuteNonQuery(SQL)
        'End If

        'SQL = "SELECT * FROM " & mNamaDB & ".INFORMATION_SCHEMA.COLUMNS 
        '    WHERE TABLE_NAME = 't_KoreksiStock'
        '    and COLUMN_NAME = 'id_SOFisik'"
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Alter table " & mNamaDB & ".dbo.t_KoreksiStock ADD id_SOFisik varchar(15) default '' "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Update " & mNamaDB & ".dbo.t_KoreksiStock set id_SOFisik = '' "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
        'SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'm_LogFile' "
        'dbTable = Proses.ExecuteQuery(SQL)
        'If dbTable.Rows.Count = 0 Then
        '    SQL = "Create Table m_LogFile ( 
        '        idRec varchar(10),
        '        tgl1 datetime,
        '        tgl2 DateTime,
        '        NamaFile varchar(100),
        '     jenistr varchar(30),
        '        userid varchar(20),
        '     lastupd DateTime) "
        '    Proses.ExecuteNonQuery(SQL)
        'End If
    End Sub

    Private Sub cariFolder_Click(sender As Object, e As EventArgs) Handles cariFolder.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            NamaFile.Text = FolderBrowserDialog1.SelectedPath
            If NamaFile.Text <> "" Then cariFolder.Focus()
        End If
    End Sub

    Private Sub cmdKirim_Click(sender As Object, e As EventArgs) Handles cmdKirim.Click
        If Kode_Toko.Text = "" Then
            MsgBox("Kode Toko Tujuan Salah !", vbCritical + vbOKOnly, ".:Warning !")
            Exit Sub
        End If
        'NamaFile.Text = My.Settings.LokasiFile
        If (Not System.IO.Directory.Exists(My.Settings.LokasiFile)) Then
            MsgBox("Folder tampungan kirim file tidak ada !", vbCritical + vbOKOnly, ".:Warning!")
            Exit Sub
        End If
        Dim tFile As String = KodeTokoAsal.Text +
            Microsoft.VisualBasic.Right(Kode_Toko.Text, 2) + "_" + Format(Now(), "yyMMdd_HHmm") + ".bak"
        NamaFile.Text = NamaFile.Text + "\" + tFile
        '----- proses copy ke DBase tampungan
        Dim mNamaDB As String = "C" + KodeTokoAsal.Text
        Dim mIdToko As String = ""
        cmdKirim.Enabled = False

        If KodeTokoAsal.Text = "PKT01" Then
            SQL = "exec tr_Waru2Klender"
            Proses.ExecuteNonQuery(SQL)
        ElseIf KodeTokoAsal.Text = "PKT02" Then
            SQL = "exec tr_Waru2Klender"
            Proses.ExecuteNonQuery(SQL)
        End If
        backup(NamaFile.Text)
        cmdKirim.Visible = False

        '    mIdToko = Kode_Toko.Text
        '    SQL = "Select * From m_TokoD " &
        '        "Where idToko = '" & Kode_Toko.Text & "' "
        '    dbTable = Proses.ExecuteQuery(SQL)
        '    For a = 0 To dbTable.Rows.Count - 1
        '        mIdToko = mIdToko + "," + dbTable.Columns(0).Table.Rows(a)!ID_Toko
        '    Next (a)
        '    '1. Surat Jalan : pusat kirim - toko terima
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_SJH "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_SJD "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert into " & mNamaDB & ".dbo.t_SJH " &
        '         " Select * " &
        '         " From t_SJH  " &
        '         "Where idTokoTujuan in (" & mIdToko & ") " &
        '         "  And IdTokoAsal = '" & KodeTokoAsal.Text & "' " &
        '         "  And AktifYN = 'Y' " &
        '         "  And convert(varchar(8), tglSJ, 112) between " &
        '         "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '         "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_SJD " &
        '        "Select * " &
        '        "  From t_SJD " &
        '        " Where Id_Rec in (Select IdRec From T_SJH  " &
        '        "                   Where idTokoTujuan in (" & mIdToko & ")" &
        '        "                     And IdTokoAsal = '" & KodeTokoAsal.Text & "' " &
        '        "                     And AktifYN = 'Y' " &
        '        "                     And convert(varchar(8), tglSJ, 112) between " &
        '        "                        '" & Format(Tgl1.Value, "yyyyMMdd") & "' And '" & Format(tgl2.Value, "yyyyMMdd") & "' ) " &
        '        "Order By id_rec, NoUrut "
        '    Proses.ExecuteNonQuery(SQL)

        '    '-----------------DO
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_DOH "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_DOD "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Insert into " & mNamaDB & ".dbo.t_DOH " &
        '         " Select * " &
        '         " From t_DOH " &
        '         "Where idTokoTujuan in (" & mIdToko & ") " &
        '         "  And IdTokoAsal = '" & KodeTokoAsal.Text & "' " &
        '         "  And AktifYN = 'Y' " &
        '         "  And convert(varchar(8), tglDO, 112) between " &
        '         "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '         "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_DOD " &
        '        "Select * " &
        '        "  From t_DOD " &
        '        " Where Id_Rec in (Select IdRec From T_DOH  " &
        '        "                   Where idTokoTujuan in (" & mIdToko & ")" &
        '        "                     And IdTokoAsal = '" & KodeTokoAsal.Text & "' " &
        '        "                     And AktifYN = 'Y' " &
        '        "                     And convert(varchar(8), tglDO, 112) between " &
        '        "                        '" & Format(Tgl1.Value, "yyyyMMdd") & "' And '" & Format(tgl2.Value, "yyyyMMdd") & "' ) " &
        '        "Order By id_rec, NoUrut "
        '    Proses.ExecuteNonQuery(SQL)



        '    '-----------------Retur 
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_ReturH "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_ReturD "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert into " & mNamaDB & ".dbo.t_ReturH " &
        '         " Select * " &
        '         " From t_ReturH  " &
        '         "Where idtokoasal in (" & mIdToko & ") " &
        '         "  And AktifYN = 'Y' " &
        '         "  And convert(varchar(8), tglRetur, 112) between " &
        '         "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '         "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_ReturD " &
        '        "Select * " &
        '        "  From t_ReturD " &
        '        " Where Id_Rec in (Select IdRec From t_ReturH " &
        '        "                   Where idtokoasal in (" & mIdToko & ") " &
        '        "                     And AktifYN = 'Y' " &
        '        "                     And convert(varchar(8), tglRetur, 112) between " &
        '        "                        '" & Format(Tgl1.Value, "yyyyMMdd") & "' And '" & Format(tgl2.Value, "yyyyMMdd") & "' ) " &
        '        "Order By id_rec, NoUrut "
        '    Proses.ExecuteNonQuery(SQL)

        '    '3. Transfer Barang
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_TransferBarang "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_TransferBarang " &
        '        "Select * " &
        '        "  From t_TransferBarang " &
        '        " Where Kode_Toko in (" & mIdToko & ") " &
        '        "   And AktifYN = 'Y' " &
        '        "   And convert(varchar(8), tglTransfer, 112) between " &
        '        "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '        "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    '4. Koreksi Stock
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_KoreksiStock "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_KoreksiStock " &
        '        "Select * " &
        '        "  From t_KoreksiStock " &
        '        " Where Kode_Toko in (" & mIdToko & ") " &
        '        "   And convert(varchar(8), tglKoreksi, 112) between " &
        '        "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '        "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    '4b. SO Fisik    10 Maret 2021
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_Stockopnamefisik "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_Stockopnamefisik " &
        '        "Select * " &
        '        "  From t_Stockopnamefisik " &
        '        " Where Kode_Toko in (" & mIdToko & ") " &
        '        "   And AktifYN = 'Y' " &
        '        "   And convert(varchar(8), tglKoreksi, 112) between " &
        '        "       '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '        "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    '5. M_Barang
        '    SQL = "Select * FROM " & mNamaDB & ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'm_Barang' "
        '    dbTable = Proses.ExecuteQuery(SQL)
        '    If dbTable.Rows.Count = 0 Then
        '        SQL = "select top 0 * into " & mNamaDB & ".dbo.m_Barang from m_barang "
        '        Proses.ExecuteNonQuery(SQL)
        '    End If
        '    SQL = "Insert into " & mNamaDB & ".dbo.m_Barang " &
        '        "Select * " &
        '        " From m_barang " &
        '        "Where convert(varchar(8), lastupd, 112) >= '" & Format(DateAdd("d", -45, Tgl1.Value), "yyyyMMdd") & "' "
        '    Proses.ExecuteNonQuery(SQL)
        'Else
        '    mIdToko = KodeTokoAsal.Text
        '    SQL = "Select * From m_TokoD " &
        '    "Where idToko = '" & KodeTokoAsal.Text & "' "
        '    dbTable = Proses.ExecuteQuery(SQL)
        '    For a = 0 To dbTable.Rows.Count - 1
        '        mIdToko = mIdToko + "," + dbTable.Columns(0).Table.Rows(a)!ID_Toko
        '    Next (a)
        '    '5. Kasir
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_KasirH "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_KasirD "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert into " & mNamaDB & ".dbo.t_KasirH " &
        '         " Select * " &
        '         " From t_KasirH  " &
        '         "Where Kode_Toko In (" & mIdToko & ") " &
        '         "  And AktifYN = 'Y' " &
        '         "  And convert(varchar(8), tglPenjualan, 112) between " &
        '         "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '         "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_KasirD " &
        '        "Select * " &
        '        "  From t_KasirD " &
        '        " Where Id_Rec in (Select IdRec From t_KasirH  " &
        '        "                   Where Kode_Toko in (" & mIdToko & ")" &
        '        "                     And AktifYN = 'Y' " &
        '        "                     And convert(varchar(8), tglPenjualan, 112) between " &
        '        "                        '" & Format(Tgl1.Value, "yyyyMMdd") & "' And '" & Format(tgl2.Value, "yyyyMMdd") & "' ) " &
        '        "Order By id_rec, NoUrut "
        '    Proses.ExecuteNonQuery(SQL)

        '    '2. Retur dari Toko ke pusat
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_ReturH "
        '    Proses.ExecuteNonQuery(SQL)
        '    SQL = "Truncate table  " & mNamaDB & ".dbo.t_ReturD "
        '    Proses.ExecuteNonQuery(SQL)

        '    SQL = "Insert into " & mNamaDB & ".dbo.t_ReturH " &
        '         " Select * " &
        '         " From t_ReturH  " &
        '         "Where idTokoTujuan = '" & Kode_Toko.Text & "' " &
        '         "  And AktifYN = 'Y' " &
        '         "  And convert(varchar(8), tglRetur, 112) between " &
        '         "      '" & Format(Tgl1.Value, "yyyyMMdd") & "' And  '" & Format(tgl2.Value, "yyyyMMdd") & "' " &
        '         "Order by idrec "
        '    Proses.ExecuteNonQuery(SQL)


        '    SQL = "Insert Into " & mNamaDB & ".dbo.t_ReturD " &
        '        "Select * " &
        '        "  From t_ReturD " &
        '        " Where Id_Rec in (Select IdRec From t_ReturH " &
        '        "                   Where idTokoTujuan  = '" & Kode_Toko.Text & "'  " &
        '        "                     And AktifYN = 'Y' " &
        '        "                     And convert(varchar(8), tglRetur, 112) between " &
        '        "                        '" & Format(Tgl1.Value, "yyyyMMdd") & "' And '" & Format(tgl2.Value, "yyyyMMdd") & "' ) " &
        '        "Order By id_rec, NoUrut "
        '    Proses.ExecuteNonQuery(SQL)

        '    If FrmMenuUtama.Kode_Toko.Text = "001" Then
        '        KirimDariPusatTokoLain(mNamaDB, mIdToko)
        '    End If

    End Sub
    Private Sub cmdBatal_Click(sender As Object, e As EventArgs) Handles cmdBatal.Click
        Me.Close()
    End Sub

    Private Sub cmdKirim_Validating(sender As Object, e As CancelEventArgs) Handles cmdKirim.Validating

    End Sub

    Private Sub KodeTokoAsal_TextChanged(sender As Object, e As EventArgs) Handles KodeTokoAsal.TextChanged
        If Len(KodeTokoAsal.Text) < 1 Then
            NamaTokoAsal.Text = ""
        End If
    End Sub

    Private Sub KodeTokoAsal_DoubleClick(sender As Object, e As EventArgs) Handles KodeTokoAsal.DoubleClick
        KodeTokoAsal.Text = ""
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub Tgl1_ValueChanged(sender As Object, e As EventArgs) Handles Tgl1.ValueChanged

    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then tgl2.Focus()
    End Sub

    Private Sub tgl2_ValueChanged(sender As Object, e As EventArgs) Handles tgl2.ValueChanged

    End Sub

    Private Sub tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tgl2.KeyPress
        If e.KeyChar = Chr(13) Then cmdKirim.Focus()
    End Sub
End Class