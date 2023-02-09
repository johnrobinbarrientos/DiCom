Public Class Frm_Masterdata_Employee_Job_Title
    Dim value_jobtitleID As String
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

    Private Sub Frm_job_title_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call initialize()
    End Sub

    Private Sub initialize()
        txtjob_title.Text = ""
        txtpg_code.Text = ""
        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * from tbl_employeejobtitle ORDER BY JobTitleID DESC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("JobTitleID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_jobtitle As Integer

        If txtjob_title.Text = "" Then
            MessageBox.Show("Please Enter Job Title")
            txtjob_title.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_jobtitle FROM tbl_employeejobtitle WHERE JobTitle='" & txtjob_title.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_jobtitle = datareader("count_jobtitle")
                End While
            End If
            conn.Close()

            If count_jobtitle = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Job Title Exist")
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
                        ExecuteQuery("INSERT INTO tbl_employeejobtitle (JobTitle,JobTitle_PG) VALUES('" & txtjob_title.Text.Replace("'", "''") & "','" & txtpg_code.Text.Replace("'", "''") & "')")
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
                        ExecuteQuery("UPDATE tbl_employeejobtitle SET JobTitle='" & txtjob_title.Text.Replace("'", "''") & "', JobTitle_PG='" & txtpg_code.Text.Replace("'", "''") & "' WHERE JobTitleID='" & value_jobtitleID & "'")
                        MessageBox.Show("Successfuly Updated")
                        conn.Close()
                        Call initialize()
                    End If

                End If

            End If

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

    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        Me.Close()
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        value_jobtitleID = ListView2.SelectedItems(0).Text
        txtjob_title.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtpg_code.Text = ListView2.SelectedItems(0).SubItems(2).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtjob_title.Select()
    End Sub

    Private Sub txtjob_title_LostFocus(sender As Object, e As EventArgs) Handles txtjob_title.LostFocus
        txtjob_title.Text = txtjob_title.Text.ToUpper()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * from tbl_employeejobtitle WHERE JobTitle LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY JobTitleID DESC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("JobTitleID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
            End While
        End If
        conn.Close()
    End Sub


    Private Sub txtpg_code_LostFocus(sender As Object, e As EventArgs) Handles txtpg_code.LostFocus
        txtpg_code.Text = txtpg_code.Text.ToUpper()
    End Sub
End Class