Public Class Frm_Employee_Educational_Background
    Dim value_person, value_educbackgroundID, year_started, year_ended As String
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

    Private Sub Frm_educational_background_Load(sender As Object, e As EventArgs) Handles Me.Load

        value_person = Frm_Employee_Search_Employee.value_person
        value_educbackgroundID = Frm_employee_profile.value_educbackgroundID

        If value_educbackgroundID = "" Then
            Call initialize()
        Else
            cboyear_started.Items.Clear()
            For i As Integer = 1980 To Date.Now.Year
                cboyear_started.Items.Add(i)
            Next i

            cboyear_ended.Items.Clear()
            For i As Integer = 1980 To Date.Now.Year
                cboyear_ended.Items.Add(i)
            Next i

            bttnsave.Text = "UPDATE"
            ExecuteQuery("SELECT * from tbl_employeeeducationalbackground WHERE Educ_backgroundID='" & value_educbackgroundID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    txtinstitution.Text = datareader("NameOfInstitution")
                    cboyear_started.Text = datareader("YearStarted")
                    cboyear_ended.Text = datareader("YearEnded")
                    txtaddress.Text = datareader("Address")
                    txtfieldstudy.Text = datareader("FieldOfStudy")
                    txtremarks.Text = datareader("Remarks")
                End While
            End If
            conn.Close()

        End If
    End Sub

    Private Sub initialize()
        cboyear_started.Items.Clear()
        For i As Integer = 1980 To Date.Now.Year
            cboyear_started.Items.Add(i)
        Next i
        cboyear_started.SelectedItem = Date.Now.Year

        cboyear_ended.Items.Clear()
        For i As Integer = 1980 To Date.Now.Year
            cboyear_ended.Items.Add(i)
        Next i
        cboyear_ended.SelectedItem = Date.Now.Year

        txtinstitution.Text = ""
        txtaddress.Text = ""
        txtfieldstudy.Text = ""
        txtremarks.Text = ""
        bttnsave.Text = "SAVE"
    End Sub

    Private Sub trap()
        If txtinstitution.Text = "" Then
            MessageBox.Show("Please Enter Institution")
            txtinstitution.Select()
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
                    ExecuteQuery("INSERT INTO tbl_employeeeducationalbackground (EmployeeID,NameOfInstitution,YearStarted,YearEnded,Address,FieldOfStudy,Remarks) VALUES('" & value_person & "','" & txtinstitution.Text.Replace("'", "''") & "','" & cboyear_started.Text & "','" & cboyear_ended.Text & "','" & txtaddress.Text.Replace("'", "''") & "','" & txtfieldstudy.Text.Replace("'", "''") & "','" & txtremarks.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_employeeeducationalbackground SET NameOfInstitution='" & txtinstitution.Text.Replace("'", "''") & "', YearStarted='" & cboyear_started.Text & "', YearEnded='" & cboyear_ended.Text & "', Address='" & txtaddress.Text.Replace("'", "''") & "', FieldOfStudy='" & txtfieldstudy.Text.Replace("'", "''") & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "' WHERE Educ_backgroundID='" & value_educbackgroundID & "'")
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

            MessageBox.Show("Educational Background not Exist")

        Else

            Dim n As String = MsgBox("Delete Educational Background?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("DELETE FROM tbl_employeeeducationalbackground WHERE Educ_backgroundID='" & value_educbackgroundID & "'")
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