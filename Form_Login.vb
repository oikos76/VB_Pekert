Public Class Form_Login
    'Friend WithEvents SkinEngine1 As Sunisoft.IrisSkin.SkinEngine
    Dim sql As String
    Dim proses As New ClsKoneksi
    Dim tblLogin As DataTable

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        End
    End Sub

    Sub bersih()
        KdPenggunaTxt.Text = ""
        PswTxt.Text = ""
        KdPenggunaTxt.Focus()
    End Sub

    Sub Login()
        Dim mKondisi As String = ""
        'her   230102
        'uni   2468321
        'tati  1345724
        'ADE   7536743  174F90238A7640C4
        'MILA  8942665  EB4315A0DD89FE80
        'VINA  123569  EB239DAFFCD3CAD9
        'IIN   123789  F2473682DA43D525
        'NUR   090194  EEE9645166F98EA5  
        Dim Acak As New Crypto
        Dim encryptpassword As String = ""
        If KdPenggunaTxt.Text = "" Then KdPenggunaTxt.Focus() : Exit Sub
        If PswTxt.Text = "" Then PswTxt.Focus() : Exit Sub
        encryptpassword = Acak.Encrypt(PswTxt.Text)
        If UCase(KdPenggunaTxt.Text) = "EKO_K" Then
            mKondisi = ""
        Else
            mKondisi = " and aktifYN = 'Y' "
        End If
        sql = "Select * From m_User " &
                "Where UserID = '" & KdPenggunaTxt.Text & "' " &
                "  and password ='" & encryptpassword & "' " &
                " " & mKondisi & " "
        tblLogin = proses.ExecuteQuery(sql)
        If tblLogin.Rows.Count = 0 Then
            MessageBox.Show("Login tidak berhasil..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            KdPenggunaTxt.Focus()
        Else
            FrmMenuUtama.TsPengguna.Text = UCase(KdPenggunaTxt.Text)
            sql = "Update m_User Set LastLogin = GetDate() " &
                "where userid = '" & KdPenggunaTxt.Text & "' "
            proses.ExecuteNonQuery(sql)
            'SkinEngine1.Active = False
            Me.Close()
            FrmMenuUtama.Show()
        End If
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Call Login()
    End Sub


    Private Sub PswTxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PswTxt.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Login()
        End If
    End Sub

    Private Sub KdPenggunaTxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles KdPenggunaTxt.KeyPress
        If e.KeyChar = Chr(13) Then
            tblLogin = proses.ExecuteQuery("Select * From m_user Where UserID = '" & KdPenggunaTxt.Text & "'")
            If tblLogin.Rows.Count = 0 Then
                MessageBox.Show("User ID tidak ditemukan..!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                KdPenggunaTxt.Focus()
            Else
                PswTxt.Focus()
            End If
        End If
    End Sub

    Private Sub Form_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim toolTip1 As New ToolTip()

        ' Set up the delays for the ToolTip.
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        ' Force the ToolTip text to be displayed whether or not the form is active.
        toolTip1.ShowAlways = True
        cmdOK.Enabled = True
        ' Set up the ToolTip text for the Button and Checkbox.

        toolTip1.SetToolTip(Me.cmdOK, "Login to " + My.Application.Info.CompanyName)
        Me.Text = "Login to " + My.Application.Info.CompanyName

        'If UCase(My.Settings.IPServer) = "LOCALHOST" Then
        '    'cmdSetting.Visible = True
        '    cmdOK.Enabled = False
        '    toolTip1.SetToolTip(Me.cmdSetting, "Configuration Setting")
        'Else
        '    cmdOK.Enabled = True
        '    'cmdSetting.Visible = False
        'End If

        'Me.SkinEngine1 = New Sunisoft.IrisSkin.SkinEngine()
        'SkinEngine1.SerialNumber = "kUb2DF5pvGF3X9dKPFvIdkXQ0sE8LkAVp9fMme9wCnjZ+ArdRVlxKw=="
        'SkinEngine1.SkinFile = "Skins\deep\deepcyan.ssk"
        'SkinEngine1.ApplyMainBuiltInSkin()
        'SkinEngine1.Active = True
    End Sub


    Private Function CekIP(ByRef xIP As String) As Boolean
        Dim ip() As Net.IPAddress = System.Net.Dns.GetHostAddresses("")
        Dim result As Boolean = False
        If ip.Count > 0 Then
            For Each ipadd As Net.IPAddress In ip
                'Console.WriteLine(ipadd.ToString)
                If ipadd.ToString = Trim(xIP) Then
                    result = True
                    Exit For
                End If
            Next
        End If
        CekIP = result
    End Function

    Private Sub KdPenggunaTxt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KdPenggunaTxt.TextChanged
        If Len(KdPenggunaTxt.Text) = 5 Then
            If Trim(UCase(KdPenggunaTxt.Text)) = "EKO_K" Or Trim(UCase(KdPenggunaTxt.Text)) = "HERMA" Then
                cmdSetting.Visible = True
            End If
        End If
    End Sub

    Private Sub cmdSetting_Click(sender As Object, e As EventArgs) Handles cmdSetting.Click
        Form_ConnectionSetting.ShowDialog()
    End Sub

    Private Sub PswTxt_TextChanged(sender As Object, e As EventArgs) Handles PswTxt.TextChanged

    End Sub
End Class
