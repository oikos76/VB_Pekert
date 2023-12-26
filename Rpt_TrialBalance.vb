Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient


Public Class Rpt_TrialBalance

    Dim SQL As String
    Dim Proses As New ClsKoneksi
    Dim dbTable As DataTable
    Dim dttable As New DataTable
    Dim DTadapter As New SqlDataAdapter
    Dim objRep As New ReportDocument
    Dim UserID As String = FrmMenuUtama.TsPengguna.Text
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

End Class