Public Class Frm_Employee_Separation_Detail
    Dim value_person, value_separationID As String
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

    Private Sub Frm_separation_detail_Load(sender As Object, e As EventArgs) Handles Me.Load
        value_person = Frm_Employee_Search_Employee.value_person
        value_separationID = Frm_employee_profile.value_separationID

        If value_separationID = "" Then
            Call initialize()
        Else
            Dim jobtitle_value As Integer
            bttnsave.Text = "UPDATE"
            ExecuteQuery("SELECT * from tbl_employeeseparationdetails WHERE SeparationDetail_ID='" & value_separationID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    cboreason_sep.SelectedItem = datareader("ReasonForSeparation")
                    cbotype_sep.SelectedItem = datareader("TypeOfSeparation")
                    date_hired.Text = datareader("DateHired")
                    date_separation.Text = datareader("DateOfSeparation")
                    txtremarks.Text = datareader("Remarks")
                    jobtitle_value = datareader("JobTitleID")
                End While
            End If
            conn.Close()

            Dim table_jobtitle As New DataTable

            ExecuteQuery("SELECT * from tbl_employeejobtitle")
            datareader = cmd.ExecuteReader
            table_jobtitle.Load(datareader)
            cbojobtitle.DisplayMember = "JobTitle"
            cbojobtitle.ValueMember = "JobTitleID"
            cbojobtitle.DataSource = table_jobtitle
            cbojobtitle.SelectedValue = jobtitle_value
            conn.Close()

        End If
    End Sub


    Private Sub initialize()
        cboreason_sep.SelectedItem = "Career Opportunity"
        cbotype_sep.SelectedItem = "Resignation"
        date_hired.Value = Now
        date_separation.Value = Now
        txtremarks.Text = ""
        bttnsave.Text = "SAVE"

        Dim table_jobtitle As New DataTable

        ExecuteQuery("SELECT * from tbl_employeejobtitle")
        datareader = cmd.ExecuteReader
        table_jobtitle.Load(datareader)
        cbojobtitle.DisplayMember = "JobTitle"
        cbojobtitle.ValueMember = "JobTitleID"
        cbojobtitle.DataSource = table_jobtitle
        conn.Close()
    End Sub

    Private Sub bttnsave_Click(sender As Object, e As EventArgs) Handles bttnsave.Click
        If bttnsave.Text = "SAVE" Then
            Dim n As String = MsgBox("Add New Record?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("INSERT INTO tbl_employeeseparationdetails (EmployeeID,ReasonForSeparation,TypeOfSeparation,DateHired,DateOfSeparation,JobTitleID,Remarks) VALUES('" & value_person & "','" & cboreason_sep.Text & "','" & cbotype_sep.Text & "','" & date_hired.Text & "','" & date_separation.Text & "','" & cbojobtitle.SelectedValue & "','" & txtremarks.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_employeeseparationdetails SET ReasonForSeparation='" & cboreason_sep.Text & "', TypeOfSeparation='" & cbotype_sep.Text & "', DateHired='" & date_hired.Text & "', DateOfSeparation='" & date_separation.Text & "', JobTitleID='" & cbojobtitle.SelectedValue & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "' WHERE SeparationDetail_ID='" & value_separationID & "'")
                    MessageBox.Show("Successfuly Updated")
                    conn.Close()
                    Call Frm_employee_profile.initialize()
                    Me.Close()
                End If

            End If

        End If
    End Sub

    Private Sub bttndelete_Click(sender As Object, e As EventArgs) Handles bttndelete.Click
        If bttnsave.Text = "SAVE" Then

            MessageBox.Show("Separation Details not Exist")

        Else

            Dim n As String = MsgBox("Delete Separation Details?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then
                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("DELETE FROM tbl_employeeseparationdetails WHERE SeparationDetail_ID='" & value_separationID & "'")
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