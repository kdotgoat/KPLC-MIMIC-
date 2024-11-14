Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports Mysqlx
Public Class Form7
    Dim connectionString As String = "Server=localhost;Database=kplc;User Id=root;Password=Wi0797563115#;"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub LoadData()


        Dim query As String = "SELECT * FROM employees"

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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            conn.Open()
            Using cmd As New MySqlCommand("INSERT INTO employees (First_Name, Sir_Name, Last_Name, User_name, Passsword, Department) VALUES (@First_Name, @Sir_Name, @Last_Name, @User_name, @Passsword, @Department)", conn)
                cmd.Parameters.AddWithValue("@First_Name", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Sir_Name", TextBox3.Text)
                cmd.Parameters.AddWithValue("@Last_Name", TextBox4.Text)
                cmd.Parameters.AddWithValue("@User_name", TextBox6.Text)
                cmd.Parameters.AddWithValue("@Passsword", TextBox7.Text)
                cmd.Parameters.AddWithValue("@Department", TextBox5.Text)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Employee added successfully!")
                LoadData()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error adding employee: " & ex.Message)
        Finally
            conn.Close()
        End Try


    End Sub
    Private Sub ClearFields()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conn.Open()


            Using cmd As New MySqlCommand("UPDATE employees SET First_Name = @First_Name, Sir_Name = @Sir_Name, Last_Name = @Last_Name, Passsword = @Passsword, Department = @Department WHERE User_name = @User_name", conn)
                ' Add parameters for updating
                cmd.Parameters.AddWithValue("@First_Name", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Sir_Name", TextBox3.Text)
                cmd.Parameters.AddWithValue("@Last_Name", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Passsword", TextBox7.Text)
                cmd.Parameters.AddWithValue("@Department", TextBox5.Text)
                cmd.Parameters.AddWithValue("@User_name", TextBox6.Text)

                ' Execute the command
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MessageBox.Show("Employee updated successfully!")
                Else
                    MessageBox.Show("No matching record found to update.")
                End If

                ' Refresh data in DataGridView if applicable
                LoadData()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating employee: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Open the connection
            conn.Open()

            ' Define the SQL query to search for an employee by employee_id
            Dim query As String = "SELECT * FROM employees WHERE employee_id = @Employee_id"

            ' Create the command and add the parameter for employee_id
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Employee_id", TextBox1.Text) ' Assuming TextBox1 is where you input the employee_id

                ' Execute the query and get the result
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    ' Check if the employee exists
                    If reader.HasRows Then
                        ' Read the employee data
                        reader.Read()

                        ' Display the data in the corresponding textboxes
                        TextBox2.Text = reader("First_Name").ToString()
                        TextBox3.Text = reader("Sir_Name").ToString()
                        TextBox4.Text = reader("Last_Name").ToString()
                        TextBox6.Text = reader("User_name").ToString()
                        TextBox7.Text = reader("Passsword").ToString()
                        TextBox5.Text = reader("Department").ToString()

                        ' Optionally, you can display a success message
                        MessageBox.Show("Employee found!")
                    Else
                        ' Show message if no employee was found
                        MessageBox.Show("Employee not found.")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Ensure the connection is closed
            conn.Close()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try

            conn.Open()

            ' Define the SQL query to delete an employee by employee_id
            Dim query As String = "DELETE FROM employees WHERE employee_id = @Employee_id"

            ' Create the command and add the parameter for employee_id
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Employee_id", TextBox1.Text) ' Assuming TextBox1 is where you input the employee_id

                ' Execute the query to delete the employee
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                ' Check if any rows were affected (meaning the employee was deleted)
                If rowsAffected > 0 Then
                    MessageBox.Show("Employee deleted successfully!")
                Else
                    MessageBox.Show("Employee not found.")
                End If
            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Ensure the connection is closed
            conn.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        Form6.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class