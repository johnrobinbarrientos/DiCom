Imports MySql.Data.MySqlClient

Module Connection

    'Public conn As New MySqlConnection("Server=10.10.2.200;User Id=ogdi;password=techsup@ids;SslMode=none;Database=phpmy1_orogrande_com_ph")
    Public conn As New MySqlConnection("Server=localhost;User id=root;password=local;SslMode=none;Database=db_test;convert zero datetime=True")
    'Public conn As New MySqlConnection("Server=10.147.18.178;User id=root;password=local;SslMode=none;Database=db_test")

    Public datareader As MySqlDataReader
    Public cmd As New MySqlCommand
    Public table As New DataTable
    Public internet_connection As Boolean


    Public Sub ExecuteQuery(query As String)
        'Dim cmd As New MySqlCommand(query, conn)
        conn.Open()
        cmd.CommandText = query
        cmd.Connection = conn
        cmd.ExecuteNonQuery()
    End Sub

    Public Sub checkconnection()
        Try
            conn.Open()
            internet_connection = True
            conn.Close()
        Catch ex As Exception
            internet_connection = False
        End Try
    End Sub

End Module
