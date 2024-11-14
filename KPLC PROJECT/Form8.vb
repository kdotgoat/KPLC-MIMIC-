Imports MySql.Data.MySqlClient

Public Class Form8
    Dim connectionString As String = "Server=localhost;Database=kplc;User Id=root;Password=Wi0797563115#;"
    Dim conn As New MySqlConnection(connectionString)
    Private Sub LoadData()


        Dim query As String = "SELECT * FROM customers"

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
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            ' Open the connection
            conn.Open()

            ' Define the SQL query to delete a customer by MtrNumber
            Dim query As String = "DELETE FROM customers WHERE MtrNumber = @MtrNumber"

            ' Create the command and add the parameter for MtrNumber
            Using cmd As New MySqlCommand(query, conn)
                ' Validate and convert MtrNumber to Decimal
                Dim mtrNumber As Decimal
                If Not Decimal.TryParse(TextBox1.Text, mtrNumber) Then
                    MessageBox.Show("Invalid MtrNumber. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                cmd.Parameters.AddWithValue("@MtrNumber", mtrNumber)

                ' Execute the query to delete the customer
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                ' Check if any rows were affected (meaning the customer was deleted)
                If rowsAffected > 0 Then
                    MessageBox.Show("Customer deleted successfully!")
                Else
                    MessageBox.Show("Customer with the specified MtrNumber not found.")
                End If

                ' Optionally, clear the textbox after deletion
                TextBox1.Clear()
            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur
            MessageBox.Show("Error deleting customer: " & ex.Message)
        Finally
            ' Ensure the connection is closed
            conn.Close()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Open the connection
            conn.Open()

            ' Define the SQL query to insert a new customer into the customers table
            Dim query As String = "INSERT INTO customers (MtrNumber, First_name, Sir_name, Last_name, Amount) " &
                          "VALUES (@MtrNumber, @First_Name, @Sir_Name, @Last_Name, @Amount)"

            ' Create the command and add parameters for the customer details
            Using cmd As New MySqlCommand(query, conn)
                ' Add values from the TextBoxes, ensuring MtrNumber is treated as Decimal
                Dim mtrNumber As Decimal
                If Decimal.TryParse(TextBox1.Text, mtrNumber) Then
                    cmd.Parameters.AddWithValue("@MtrNumber", mtrNumber) ' MtrNumber in TextBox1 as Decimal
                Else
                    MessageBox.Show("Invalid MtrNumber. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                cmd.Parameters.AddWithValue("@First_Name", TextBox2.Text) ' First_name in TextBox2
                cmd.Parameters.AddWithValue("@Sir_Name", TextBox3.Text) ' Sir_name in TextBox3
                cmd.Parameters.AddWithValue("@Last_Name", TextBox4.Text) ' Last_name in TextBox4

                ' Ensure Amount is valid
                Dim amount As Decimal
                If Decimal.TryParse(TextBox5.Text, amount) Then
                    cmd.Parameters.AddWithValue("@Amount", amount) ' Amount in TextBox5 as Decimal
                Else
                    MessageBox.Show("Invalid Amount. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' Execute the query to insert the new customer
                cmd.ExecuteNonQuery()

                ' Show a message indicating the customer was added successfully
                MessageBox.Show("Customer added successfully!")

                ' Optionally, you can clear the textboxes after adding the customer
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox5.Clear()

            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur
            MessageBox.Show("Error adding customer: " & ex.Message)
        Finally
            ' Ensure the connection is closed
            conn.Close()
        End Try


    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Open the connection
            conn.Open()

            ' Define the SQL query to update the customer information based on MtrNumber
            Dim query As String = "UPDATE customers SET First_name = @FirstName, Sir_name = @SirName, " &
                          "Last_name = @LastName, Amount = @Amount WHERE MtrNumber = @MtrNumber"

            ' Create the command and add parameters for the customer details
            Using cmd As New MySqlCommand(query, conn)
                ' Validate and convert MtrNumber to Decimal
                Dim mtrNumber As Decimal
                If Not Decimal.TryParse(TextBox1.Text, mtrNumber) Then
                    MessageBox.Show("Invalid MtrNumber. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                cmd.Parameters.AddWithValue("@MtrNumber", mtrNumber)

                ' Get the values from the textboxes and add them as parameters for the update
                cmd.Parameters.AddWithValue("@FirstName", TextBox2.Text)
                cmd.Parameters.AddWithValue("@SirName", TextBox3.Text)
                cmd.Parameters.AddWithValue("@LastName", TextBox4.Text)

                ' Validate and convert Amount to Decimal
                Dim amount As Decimal
                If Not Decimal.TryParse(TextBox5.Text, amount) Then
                    MessageBox.Show("Invalid Amount. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                cmd.Parameters.AddWithValue("@Amount", amount)

                ' Execute the update query
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                ' Check if any rows were updated
                If rowsAffected > 0 Then
                    MessageBox.Show("Customer information updated successfully!")
                Else
                    MessageBox.Show("Customer with the specified MtrNumber not found or no changes made.")
                End If

            End Using
        Catch ex As Exception
            ' Handle any exceptions that occur
            MessageBox.Show("Error updating customer: " & ex.Message)
        Finally
            ' Ensure the connection is closed
            conn.Close()
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form6.Show()
    End Sub
End Class