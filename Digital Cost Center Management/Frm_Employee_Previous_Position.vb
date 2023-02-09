Public Class Frm_Employee_previous_position
    Dim value_person, value_previouspositionID As String
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

    Private Sub Frm_previous_position_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim cpc_id, jobtitle_value As Integer
        value_person = Frm_Employee_Search_Employee.value_person
        value_previouspositionID = Frm_employee_profile.value_previouspositionID

        If value_previouspositionID = "" Then
            Call initialize()
        Else
            bttnsave.Text = "UPDATE"
            ExecuteQuery("SELECT * from tbl_employeepreviousposition WHERE PreviousPositionID='" & value_previouspositionID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    date_hired.Text = datareader("DateHired")
                    txtremarks.Text = datareader("Remarks")
                    cpc_id = datareader("CPCID")
                    jobtitle_value = datareader("JobTitleID")
                End While
            End If
            conn.Close()

            Dim table_cpc As New DataTable

            ExecuteQuery("SELECT * from tbl_distributorcpc")
            datareader = cmd.ExecuteReader
            table_cpc.Load(datareader)
            cbocpc.DisplayMember = "CPC"
            cbocpc.ValueMember = "CPCID"
            cbocpc.DataSource = table_cpc
            cbocpc.SelectedValue = cpc_id
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
        date_hired.Value = Now
        txtremarks.Text = ""
        bttnsave.Text = "SAVE"

        Dim table_cpc As New DataTable

        ExecuteQuery("SELECT * from tbl_distributorcpc")
        datareader = cmd.ExecuteReader
        table_cpc.Load(datareader)
        cbocpc.DisplayMember = "CPC"
        cbocpc.ValueMember = "CPCID"
        cbocpc.DataSource = table_cpc
        conn.Close()

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
                    ExecuteQuery("INSERT INTO tbl_employeepreviousposition (EmployeeID,CPCID,DateHired,JobTitleID,Remarks) VALUES('" & value_person & "','" & cbocpc.SelectedValue & "','" & date_hired.Text & "','" & cbojobtitle.SelectedValue & "','" & txtremarks.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_employeepreviousposition SET CPCID='" & cbocpc.SelectedValue & "', DateHired='" & date_hired.Text & "', JobTitleID='" & cbojobtitle.SelectedValue & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "' WHERE PreviousPositionID='" & value_previouspositionID & "'")
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
            MessageBox.Show("Position not Exist")

        Else

            Dim n As String = MsgBox("Delete Position?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then
                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("DELETE FROM tbl_employeepreviousposition WHERE PreviousPositionID='" & value_previouspositionID & "'")
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