Public Class Frm_Employee_Employment_History
    Dim value_person, value_emphistoryID As String
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

    Private Sub Frm_employment_history_Load(sender As Object, e As EventArgs) Handles Me.Load
        value_person = Frm_Employee_Search_Employee.value_person
        value_emphistoryID = Frm_employee_profile.value_emphistoryID

        If value_emphistoryID = "" Then
            Call initialize()
        Else
            cboyear_started.Items.Clear()
            For i As Integer = 2000 To Date.Now.Year
                cboyear_started.Items.Add(i)
            Next i

            cboyear_ended.Items.Clear()
            For i As Integer = 2000 To Date.Now.Year
                cboyear_ended.Items.Add(i)
            Next i

            bttnsave.Text = "UPDATE"
            ExecuteQuery("SELECT * from tbl_employeeemploymenthistory WHERE EmploymentHistoryID='" & value_emphistoryID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    txtcompany.Text = datareader("Company")
                    txtjob_title.Text = datareader("JobTitle")
                    cboyear_started.Text = datareader("YearStarted")
                    cboyear_ended.Text = datareader("YearEnded")
                    txtaddress.Text = datareader("CompanyAddress")
                    txtremarks.Text = datareader("Remarks")
                End While
            End If
            conn.Close()
        End If
    End Sub

    Private Sub initialize()
        cboyear_started.Items.Clear()
        For i As Integer = 2000 To Date.Now.Year
            cboyear_started.Items.Add(i)
        Next i
        cboyear_started.SelectedItem = Date.Now.Year

        cboyear_ended.Items.Clear()
        For i As Integer = 2000 To Date.Now.Year
            cboyear_ended.Items.Add(i)
        Next i
        cboyear_ended.SelectedItem = Date.Now.Year

        txtcompany.Text = ""
        txtjob_title.Text = ""
        txtaddress.Text = ""
        txtremarks.Text = ""
        bttnsave.Text = "SAVE"
    End Sub

    Private Sub trap()
        If txtcompany.Text = "" Then
            MessageBox.Show("Please Enter Company")
            txtcompany.Select()
        ElseIf txtjob_title.Text = "" Then
            MessageBox.Show("Please Enter Job Title")
            txtjob_title.Select()
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
                    ExecuteQuery("INSERT INTO tbl_employeeemploymenthistory (EmployeeID,Company,JobTitle,YearStarted,YearEnded,CompanyAddress,Remarks) VALUES('" & value_person & "','" & txtcompany.Text.Replace("'", "''") & "','" & txtjob_title.Text.Replace("'", "''") & "','" & cboyear_started.Text & "','" & cboyear_ended.Text & "','" & txtaddress.Text.Replace("'", "''") & "','" & txtremarks.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_employeeemploymenthistory SET Company='" & txtcompany.Text.Replace("'", "''") & "', JobTitle='" & txtjob_title.Text.Replace("'", "''") & "', YearStarted='" & cboyear_started.Text & "', YearEnded='" & cboyear_ended.Text & "', CompanyAddress='" & txtaddress.Text.Replace("'", "''") & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "' WHERE EmploymentHistoryID='" & value_emphistoryID & "'")
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

            MessageBox.Show("Employment History not Exist")

        Else

            Dim n As String = MsgBox("Delete Employment History?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("DELETE FROM tbl_employeeemploymenthistory WHERE EmploymentHistoryID='" & value_emphistoryID & "'")
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