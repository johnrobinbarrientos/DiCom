Public Class Frm_Masterdata_User_Register
    Dim value_person As String
    Dim empid As Integer
    Dim internet_connection As Boolean

    Private Sub lblclose_MouseHover(sender As Object, e As EventArgs) Handles lblclose.MouseHover
        lblclose.Visible = False
        lblclose2.Visible = True
    End Sub

    Private Sub lblclose2_Click(sender As Object, e As EventArgs) Handles lblclose2.Click
        Me.Close()
    End Sub

    Private Sub lblclose2_MouseLeave(sender As Object, e As EventArgs) Handles lblclose2.MouseLeave
        lblclose.Visible = True
        lblclose2.Visible = False
    End Sub

    Private Sub Frm_add_user_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call initialize()
    End Sub

    Private Sub initialize()
        txtsearch.Text = ""
        txtsearch_employee.Text = ""
        txtfirst_name.Text = ""
        txtmid_name.Text = ""
        txtlast_name.Text = ""
        txtusername.Text = ""
        txtpassword.Text = ""

        bttnsave.Text = "SAVE"

        ExecuteQuery("SELECT tbl_employeemaster.LName,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeeuser.username,tbl_employeeuser.password,tbl_employeeuser.EmployeeID FROM tbl_employeeuser LEFT JOIN tbl_employeemaster ON tbl_employeeuser.EmployeeID=tbl_employeemaster.EmployeeID ORDER BY LName ASC")
        datareader = cmd.ExecuteReader
        ListView2.Items.Clear()
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("LName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("username"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("password"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmployeeID"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()
        ExecuteQuery("SELECT tbl_employeemaster.LName,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeeuser.username,tbl_employeeuser.password,tbl_employeeuser.EmployeeID FROM tbl_employeeuser LEFT JOIN tbl_employeemaster ON tbl_employeeuser.EmployeeID=tbl_employeemaster.EmployeeID WHERE LName LIKE '" & txtsearch.Text & "%' ORDER BY LName ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("LName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("username"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("password"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmployeeID"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub txtsearch_employee_TextChanged(sender As Object, e As EventArgs) Handles txtsearch_employee.TextChanged
        If txtsearch_employee.Text = "" Then
            ListView1.Visible = False

        Else

            ListView1.Visible = True
            ExecuteQuery("SELECT EmployeeID,CONCAT(LName,', ',FName,' ',MName) as fullname FROM tbl_employeemaster WHERE (LName LIKE '" & txtsearch_employee.Text.Replace("'", "''") & "%' OR FName LIKE '%" & txtsearch_employee.Text.Replace("'", "''") & "%') AND (JobTitle<>'SFA PURPOSES ONLY' OR JobTitle IS NUll) AND InActiveID=0")
            datareader = cmd.ExecuteReader


        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("EmployeeID"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("fullname"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        ExecuteQuery("SELECT FName,MName,LName,Email,EmployeeID FROM tbl_employeemaster WHERE EmployeeID ='" & ListView1.SelectedItems.Item(0).Text & "'")
        datareader = cmd.ExecuteReader

        If datareader.HasRows Then
            While (datareader.Read)
                txtfirst_name.Text = datareader("FName")
                txtmid_name.Text = datareader("MName")
                txtlast_name.Text = datareader("LName")
                txtusername.Text = datareader("Email")
                empid = datareader("EmployeeID")
            End While
        End If
        ListView1.Visible = False
        conn.Close()

    End Sub

    Private Sub txtsearch_employee_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch_employee.KeyDown
        If ListView1.Items.Count = 0 Then
        Else
            If e.KeyCode = Keys.Down Then
                ListView1.Items(0).Selected = True
                ListView1.Select()
            End If

        End If
    End Sub

    Private Sub ListView1_KeyUp(sender As Object, e As KeyEventArgs) Handles ListView1.KeyUp
        If ListView1.Items(0).Selected = True Then
            If e.KeyCode = Keys.Up Then
                txtsearch_employee.Select()
            End If
        End If
    End Sub

    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        Me.Close()
    End Sub

    Private Sub bttnsave_Click(sender As Object, e As EventArgs) Handles bttnsave.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            Call trap()
        End If
    End Sub

    Private Sub trap()
        Dim count_employee As Integer
        If txtpassword.Text = "" Then
            MessageBox.Show("Please Enter Password")
            txtpassword.Select()

        Else
            ExecuteQuery("SELECT COUNT(*) as count_employee FROM tbl_employeeuser WHERE EmployeeID=" & empid & "")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_employee = datareader("count_employee")
                End While
            End If
            conn.Close()

            If count_employee = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Employee Exist")
                End If
            End If
        End If

    End Sub

    Private Sub save()

        If bttnsave.Text = "SAVE" Then

            Dim n As String = MsgBox("Add New Record?", MsgBoxStyle.YesNo, "")
            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection
                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else

                    ExecuteQuery("INSERT INTO tbl_employeeuser (EmployeeID,username,password) VALUES(" & empid & ",'" & txtusername.Text.Replace("'", "''") & "','" & txtpassword.Text.Replace("'", "''") & "')")
                    MessageBox.Show("Successfuly Save")
                    conn.Close()
                    Call initialize()
                End If

            End If


        Else

            Dim n As String = MsgBox("Save Changes?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then
                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("UPDATE tbl_employeeuser SET password='" & txtpassword.Text.Replace("'", "''") & "' WHERE EmployeeID='" & value_person & "'")
                    MessageBox.Show("Successfuly Updated")
                    conn.Close()
                    Call initialize()
                End If

            End If


        End If



    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        txtlast_name.Text = ListView2.SelectedItems(0).Text
        txtfirst_name.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtmid_name.Text = ListView2.SelectedItems(0).SubItems(2).Text
        txtusername.Text = ListView2.SelectedItems(0).SubItems(3).Text
        txtpassword.Text = ListView2.SelectedItems(0).SubItems(4).Text

        value_person = ListView2.SelectedItems(0).SubItems(5).Text

        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtsearch_employee.Select()
    End Sub


    Private Sub ListView2_KeyUp(sender As Object, e As KeyEventArgs) Handles ListView2.KeyUp
        If ListView2.Items(0).Selected = True Then
            If e.KeyCode = Keys.Up Then
                txtsearch_employee.Select()
            End If
        End If
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ExecuteQuery("SELECT FName,MName,LName,Email,EmployeeID FROM tbl_employeemaster WHERE EmployeeID ='" & ListView1.SelectedItems.Item(0).Text & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    txtfirst_name.Text = datareader("FName")
                    txtmid_name.Text = datareader("MName")
                    txtlast_name.Text = datareader("LName")
                    txtusername.Text = datareader("Email")
                    empid = datareader("EmployeeID")
                End While
            End If
            ListView1.Visible = False
            conn.Close()
        End If
    End Sub
End Class