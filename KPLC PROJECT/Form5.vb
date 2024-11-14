Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form5
    Dim bill As Integer
    Dim total As Integer
    Dim diff As Decimal


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        bill = Convert.ToDouble(TextBox7.Text)
        diff = total - bill

        If TextBox5.Text.Length = 4 Then
            Dim value As Integer = Integer.Parse(TextBox1.Text)
            MessageBox.Show("KINDLY WAIT AS WE PROCESS YOUR TRANSACTION")

        Else MessageBox.Show("KINDLY ENTER A 4 DIGIT PIN!.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Form3.Show()
        End If
        If bill > total Then
            MessageBox.Show("PAYMENT SUCCESFULL." & vbNewLine & "YOU HAVE OVERPAID BY: " & diff)
            Me.Close()
            Form3.Show()

        ElseIf bill < total Then
            MessageBox.Show("Insuffecient funds!" & vbNewLine & "YOUR PENDING BALANCE FOR PAYMENT IS: " & diff, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("PAYMENT SUCCESSFUL , YOU HAVE SUCCESSFULLY SETTLED YOUR BILLS")
            Me.Close()
            Form3.Show()
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Amount As Integer = Convert.ToDouble(TextBox1.Text)

        Dim unit As Decimal
        unit = 25

        total = Amount * unit

        ListBox1.Items.Add("Mtr: " & TextBox6.Text)
        ListBox1.Items.Add("")
        ListBox1.Items.Add("Amount consumed: " & TextBox1.Text & " kWh")
        ListBox1.Items.Add("")
        ListBox1.Items.Add("Your total bill:Ksh " & total)


    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mtr As Decimal = Convert.ToString(TextBox6.Text)
        Dim firstName As String = TextBox2.Text
        Dim lastName As String = TextBox3.Text
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
End Class