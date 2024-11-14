Imports MySql.Data.MySqlClient
Public Class Form2
    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString =
            "Server=localhost;Database=kplc;User Id=root;Password=Wi0797563115#;"
        Dim READER As MySqlDataReader
        Try
            ' Open the connection
            MysqlConn.Open()
            Dim query As String
            query = "SELECT * FROM kplc.employees WHERE User_name = '" & TextBox1.Text & "' AND Passsword = '" & TextBox2.Text & "'"
            COMMAND = New MySqlCommand(query, MysqlConn)
            READER = COMMAND.ExecuteReader
            Dim count As Integer
            count = 0
            While READER.Read
                count = count + 1
            End While
            If count = 1 Then
                MessageBox.Show("LOGGED IN SUCCESFULLY!")
                Me.Close()
                Form6.Show()
            ElseIf count > 1 Then
                MessageBox.Show("USERNAME AND PASSWORD ARE DUPLICATE")
            Else
                MessageBox.Show("USERNAME AND PASSWORD ARE INCORRECT!")
            End If
            MysqlConn.Close()
        Catch ex As MySqlException
            ' Handle connection errors
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub
End Class