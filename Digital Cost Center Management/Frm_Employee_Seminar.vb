Public Class Frm_Employee_Seminar
    Dim value_person, value_seminarID As String
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

    Private Sub Frm_seminar_Load(sender As Object, e As EventArgs) Handles Me.Load

        value_person = Frm_Employee_Search_Employee.value_person
        value_seminarID = Frm_employee_profile.value_seminarID

        If value_seminarID = "" Then
            Call initialize()
        Else
            bttnsave.Text = "UPDATE"
            ExecuteQuery("SELECT * from tbl_employeenewseminar WHERE EmpSeminarID='" & value_seminarID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    txtseminar.Text = datareader("Seminar")
                    date_conducted.Text = datareader("DateConducted")
                    txtconducted_by.Text = datareader("ConductedBy")
                    txtremarks.Text = datareader("Remarks")
                End While
            End If
            conn.Close()
        End If

    End Sub


    Private Sub initialize()
        txtseminar.Text = ""
        date_conducted.Value = Now
        txtseminar.Text = ""
        txtconducted_by.Text = ""
        txtremarks.Text = ""
        bttnsave.Text = "SAVE"
    End Sub

    Private Sub trap()
        If txtseminar.Text = "" Then
            MessageBox.Show("Please Enter Seminar")
            txtseminar.Select()
        ElseIf txtconducted_by.Text = "" Then
            MessageBox.Show("Please Enter Conducted By")
            txtconducted_by.Select()
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
                    ExecuteQuery("INSERT INTO tbl_employeenewseminar (EmployeeID,Seminar,DateConducted,ConductedBy,Remarks) VALUES('" & value_person & "','" & txtseminar.Text.Replace("'", "''") & "','" & date_conducted.Text & "','" & txtconducted_by.Text.Replace("'", "''") & "','" & txtremarks.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_employeenewseminar SET Seminar='" & txtseminar.Text.Replace("'", "''") & "', DateConducted='" & date_conducted.Text & "', ConductedBy='" & txtconducted_by.Text.Replace("'", "''") & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "' WHERE EmpSeminarID='" & value_seminarID & "'")
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
            MessageBox.Show("Seminar not Exist")

        Else

            Dim n As String = MsgBox("Delete Seminar?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("DELETE FROM tbl_employeenewseminar WHERE EmpSeminarID='" & value_seminarID & "'")
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