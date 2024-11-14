Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Form9
    Dim connectionString As String = "Server=localhost;Database=kplc;User Id=root;Password=Wi0797563115#;"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub LoadData()


        Dim query As String = "SELECT * FROM transactions"

        ' Establish the connection
        Using connection As New MySqlConnection(connectionString)
            Try
                ' Open the connection
                connection.Open()

                ' Create a data adapter to retrieve data
                Dim adapter As New MySqlDataAdapter(query, connection)

                ' Create a DataTable to hold the data
                Dim dataTable As New DataTable()

                ' Fill the DataTable
                adapter.Fill(dataTable)

                ' Bind the DataTable to the DataGridView
                DataGridView1.DataSource = dataTable

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message, "Error")
            End Try
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
End Class