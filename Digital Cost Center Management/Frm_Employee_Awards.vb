Public Class Frm_Employee_Awards
    Dim value_person, value_awardsID As String
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


    Private Sub Frm_awards_Load(sender As Object, e As EventArgs) Handles Me.Load
        value_person = Frm_Employee_Search_Employee.value_person
        value_awardsID = Frm_employee_profile.value_awardsID

        If value_awardsID = "" Then
            Call initialize()
        Else
            bttnsave.Text = "UPDATE"
            ExecuteQuery("SELECT * from tbl_employeenewawards WHERE EmpAwardsID='" & value_awardsID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    txtaward.Text = datareader("Awards")
                    date_given.Text = datareader("DateGiven")
                    txtgiven_by.Text = datareader("GivenBy")
                    txtremarks.Text = datareader("Remarks")
                End While
            End If
            conn.Close()
        End If

    End Sub

    Private Sub initialize()
        txtaward.Text = ""
        date_given.Value = Now
        txtaward.Text = ""
        txtgiven_by.Text = ""
        txtremarks.Text = ""
        bttnsave.Text = "SAVE"
    End Sub

    Private Sub trap()
        If txtaward.Text = "" Then
            MessageBox.Show("Please Enter Award")
            txtaward.Select()
        ElseIf txtgiven_by.Text = "" Then
            MessageBox.Show("Please Enter Given By")
            txtgiven_by.Select()
        Else
            Call save()
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
                    ExecuteQuery("INSERT INTO tbl_employeenewawards (EmployeeID,Awards,DateGiven,GivenBy,Remarks) VALUES('" & value_person & "','" & txtaward.Text.Replace("'", "''") & "','" & date_given.Text & "','" & txtgiven_by.Text.Replace("'", "''") & "','" & txtremarks.Text.Replace("'", "''") & "')")
                    MessageBox.Show("Successfuly Save")
                    conn.Close()
                    Call Frm_employee_profile.initialize()
                    Me.Close()
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
                    ExecuteQuery("UPDATE tbl_employeenewawards SET Awards='" & txtaward.Text.Replace("'", "''") & "', DateGiven='" & date_given.Text & "', GivenBy='" & txtgiven_by.Text.Replace("'", "''") & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "' WHERE EmpAwardsID='" & value_awardsID & "'")
                    MessageBox.Show("Successfuly Updated")
                    conn.Close()
                    Call Frm_employee_profile.initialize()
                    Me.Close()
                End If

            End If

        End If
    End Sub

    Private Sub bttnsave_Click(sender As Object, e As EventArgs) Handles bttnsave.Click
        Call trap()
    End Sub

    Private Sub bttndelete_Click(sender As Object, e As EventArgs) Handles bttndelete.Click
        If bttnsave.Text = "SAVE" Then
            MessageBox.Show("Award not Exist")

        Else

            Dim n As String = MsgBox("Delete Award?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("DELETE FROM tbl_employeenewawards WHERE EmpAwardsID='" & value_awardsID & "'")
                    MessageBox.Show("Successfuly Deleted")
                    conn.Close()
                    Call Frm_employee_profile.initialize()
                    Me.Close()
                End If

            End If

        End If
    End Sub

    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        Me.Close()
    End Sub

End Class