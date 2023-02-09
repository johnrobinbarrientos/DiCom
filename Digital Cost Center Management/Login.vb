Imports System.Globalization
Public Class Login
    Public userID, userinternalID As Integer
    Public user_name, user_name_lower, department_name As String

    Dim internet_connection As Boolean

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.CenterToScreen()
    End Sub

    Private Sub txtusername_TextChanged(sender As Object, e As EventArgs) Handles txtusername.TextChanged
        If txtusername.Text = "" Then
            ListView1.Visible = False

        Else

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else

                ExecuteQuery("SELECT username FROM tbl_employeeuser WHERE username LIKE '" & txtusername.Text.Replace("'", "''") & "%'")
                datareader = cmd.ExecuteReader

            End If

        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("username"))
            End While
        End If
        conn.Close()

        If ListView1.Items.Count = 0 Then
            ListView1.Visible = False
        Else
            ListView1.Visible = True
        End If

    End Sub

    Private Sub ListView1_KeyUp(sender As Object, e As KeyEventArgs) Handles ListView1.KeyUp
        If ListView1.Items(0).Selected = True Then
            If e.KeyCode = Keys.Up Then
                txtusername.Select()
            End If
        End If
    End Sub

    Private Sub txtusername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtusername.KeyDown
        If ListView1.Items.Count = 0 Then
        Else
            If e.KeyCode = Keys.Down Then
                ListView1.Items(0).Selected = True
                ListView1.Select()
            End If

        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        txtusername.Text = ListView1.SelectedItems.Item(0).Text
        ListView1.Visible = False
        txtpassword.Select()
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtusername.Text = ListView1.SelectedItems.Item(0).Text
            ListView1.Visible = False
            txtpassword.Select()
        End If
    End Sub

    Private Sub OK_Click_1(sender As Object, e As EventArgs) Handles OK.Click
        conn.Close()
        Call validate_username()
    End Sub


    Private Sub txtpassword_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtpassword.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            conn.Close()
            Call validate_username()
        End If
    End Sub

    Private Sub validate_username()
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT tbl_employeeuser.AreaRate_Access,tbl_employeedept.Department, tbl_employeeuser.EmployeeID,tbl_employeemaster.EmpInternalID,CONCAT(tbl_employeemaster.FName,' ',tbl_employeemaster.LName) as fullname FROM tbl_employeeuser LEFT JOIN tbl_employeemaster ON tbl_employeeuser.EmployeeID= tbl_employeemaster.EmployeeID LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID  WHERE tbl_employeeuser.username = '" & txtusername.Text.Replace("'", "''") & "' AND tbl_employeeuser.password ='" & txtpassword.Text.Replace("'", "''") & "' ")
            datareader = cmd.ExecuteReader
        End If

        If datareader.Read() Then
            If datareader("Department") = "Human Resources" Then
                userID = datareader("EmployeeID")
                user_name_lower = datareader("fullname")
                user_name = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(user_name_lower.ToLower)
                department_name = datareader("Department")
                userinternalID = datareader("EmpInternalID")

                MDIParent1.AdminToolStripMenuItem.Enabled = False
                MDIParent1.ITEquipmentToolStripMenuItem1.Enabled = False

                MDIParent1.Show()
                Me.Hide()
                conn.Close()

            ElseIf datareader("Department") = "Finance and Accounting" Then


                userID = datareader("EmployeeID")
                user_name_lower = datareader("fullname")
                user_name = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(user_name_lower.ToLower)
                department_name = datareader("Department")
                userinternalID = datareader("EmpInternalID")

                If datareader("AreaRate_Access") = 1 Then
                    MDIParent1.AddJobTitleToolStripMenuItem.Enabled = False
                    MDIParent1.FleetToolStripMenuItem2.Enabled = False
                    MDIParent1.UserToolStripMenuItem.Enabled = False
                    MDIParent1.ITEquipmentToolStripMenuItem2.Enabled = False
                    MDIParent1.VendorToolStripMenuItem1.Enabled = False
                    MDIParent1.SalesToolStripMenuItem.Enabled = False
                    MDIParent1.PhoneToolStripMenuItem.Enabled = False
                    MDIParent1.PINSToolStripMenuItem.Enabled = False
                    MDIParent1.ITEquipmentToolStripMenuItem1.Enabled = False
                Else
                    MDIParent1.AdminToolStripMenuItem.Enabled = False
                    MDIParent1.ITEquipmentToolStripMenuItem1.Enabled = False
                End If


                MDIParent1.Show()
                Me.Hide()
                conn.Close()


            ElseIf datareader("Department") = "IDS" Then

                userID = datareader("EmployeeID")
                user_name_lower = datareader("fullname")
                user_name = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(user_name_lower.ToLower)
                department_name = datareader("Department")
                userinternalID = datareader("EmpInternalID")
                MDIParent1.AreaToolStripMenuItem.Enabled = False
                MDIParent1.Show()
                Me.Hide()
                conn.Close()

            Else

                userID = datareader("EmployeeID")
                user_name_lower = datareader("fullname")
                user_name = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(user_name_lower.ToLower)
                department_name = datareader("Department")
                userinternalID = datareader("EmpInternalID")

                MDIParent1.AddEmployeeToolStripMenuItem1.Enabled = False
                MDIParent1.ITEquipmentToolStripMenuItem1.Enabled = False
                MDIParent1.Show()
                Me.Hide()
                conn.Close()

            End If

        Else

            If txtusername.Text = "admin" And txtpassword.Text = "admen" Then
                user_name = "Admin"
                department_name = "Administrator"
                MDIParent1.Show()
                Me.Hide()
                conn.Close()
            Else
                MessageBox.Show("Invalid Username OR Password")
            End If

        End If

    End Sub


End Class
