Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports MySql.Data.Types

Public Class Form4

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Meterno As Integer = Convert.ToDouble(TextBox1.Text)
        Dim token As Integer = Convert.ToDouble(TextBox2.Text)
        Dim unit As Decimal
        unit = 25
        Dim total As Decimal
        total = token / unit
        If Not (RadioButton1.Checked Or RadioButton2.Checked) Then

            TextBox6.Enabled = False
            MessageBox.Show("KINDLY SELECT A PAYMENT OPTION.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Form3.Show()

        Else
            TextBox6.Enabled = True

        End If




        If TextBox6.Text.Length = 4 Then
            Dim value As Integer = Integer.Parse(TextBox1.Text)

            ListBox1.Items.Add("Mtr: " & TextBox1.Text)
            ListBox1.Items.Add("Token: 23423652785228825")
            ListBox1.Items.Add("Units: " & total)
            ListBox1.Items.Add("Amount:Ksh " & TextBox2.Text)

        Else MessageBox.Show("KINDLY ENTER A 4 DIGIT PIN!.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Form3.Show()
        End If

    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then

            TextBox1.Enabled = True
        Else
            CheckIfRadioButtonSelected()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then

            TextBox6.Enabled = True
        Else
            CheckIfRadioButtonSelected()
        End If
    End Sub
    Private Sub CheckIfRadioButtonSelected()
        If Not RadioButton1.Checked And Not RadioButton2.Checked Then
            MessageBox.Show("PLEASE SELECT A PAYMENT OPTION", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox6.Enabled = False
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim mtr As Decimal = Convert.ToString(TextBox1.Text)
        Dim firstName As String = TextBox3.Text
        Dim lastName As String = TextBox5.Text
        Dim sirName As String = TextBox4.Text
        Dim connectionString As String = "Server=localhost;Database=kplc;User Id=root;Password=Wi0797563115#;"

        Dim mySqlConn As New MySqlConnection(connectionString)
        Dim command As New MySqlCommand("INSERT INTO customers (MtrNumber,First_Name, Last_Name, Sir_Name) VALUES (@MtrNumber,@First_Name, @Last_Name, @Sir_Name)", mySqlConn)
        command.Parameters.AddWithValue("@MtrNumber", mtr)
        command.Parameters.AddWithValue("@First_Name", firstName)
        command.Parameters.AddWithValue("@Last_Name", lastName)
        command.Parameters.AddWithValue("@Sir_Name", sirName)

        Try
            mySqlConn.Open()
            command.ExecuteNonQuery()
            MessageBox.Show("Data inserted successfully!")

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            mySqlConn.Close()
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim amount As Decimal
        Dim meterNumber As String = TextBox1.Text.Trim()

        If Not Decimal.TryParse(TextBox2.Text, amount) OrElse amount <= 0 Then
            MessageBox.Show("Please enter a valid amount.", "Input Error")
            Exit Sub
        End If

        If String.IsNullOrEmpty(meterNumber) Then
            MessageBox.Show("Please enter a valid meter number.", "Input Error")
            Exit Sub
        End If


        Dim connectionString As String = "Server=localhost;Database=kplc;User Id=root;Password=Wi0797563115#;"


        Dim insertTransactionQuery As String = "INSERT INTO transactions (MtrNumber, Transaction_Amount) VALUES (@MtrNumber, @Amount)"
        Dim updateCustomerQuery As String = "UPDATE customers SET Amount = Amount + @Amount WHERE MtrNumber = @MtrNumber"

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()


                Using cmd As New MySqlCommand(insertTransactionQuery, connection)
                    cmd.Parameters.AddWithValue("@MtrNumber", meterNumber)
                    cmd.Parameters.AddWithValue("@Amount", amount)
                    cmd.ExecuteNonQuery()
                End Using


                Using cmd As New MySqlCommand(updateCustomerQuery, connection)
                    cmd.Parameters.AddWithValue("@Amount", amount)
                    cmd.Parameters.AddWithValue("@MtrNumber", meterNumber)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Transaction processed successfully.", "Success")

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message, "Error")
            End Try
        End Using

    End Sub
End Class