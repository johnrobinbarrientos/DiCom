Public Class Frm_Employee_Sales_Tagging
    Dim value_person As String
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

    Private Sub Frm_sales_tagging_Load(sender As Object, e As EventArgs) Handles Me.Load
        If value_person = "" Then
            cbosupervisor.SelectedItem = "NO"
            cbosales_rep.SelectedItem = "NO"
            cbosfa.SelectedItem = "NO"
            cbobtdt.SelectedItem = "NO"

            Dim table_subsegment As New DataTable
            ExecuteQuery("SELECT * from tbl_storesubsegment")
            datareader = cmd.ExecuteReader
            table_subsegment.Load(datareader)
            cbosubsegment.DisplayMember = "SubSegment"
            cbosubsegment.ValueMember = "SubSegmentID"
            cbosubsegment.DataSource = table_subsegment
            conn.Close()
        Else
            Call init_employee()
        End If

    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text = "" Then
            ListView1.Visible = False

        Else
            ListView1.Visible = True
            ExecuteQuery("SELECT EmpInternalID, CONCAT(LName,', ',FName,' ',MName) as fullname from tbl_employeemaster WHERE (LName LIKE '" & txtsearch.Text.Replace("'", "''") & "%' OR FName LIKE '%" & txtsearch.Text.Replace("'", "''") & "%') AND InActiveID=0")
            datareader = cmd.ExecuteReader
        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("EmpInternalID"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("fullname"))

            End While
        End If
        conn.Close()

    End Sub
    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
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
                txtsearch.Select()
            End If
        End If

    End Sub
    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call init_employee()
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Call init_employee()
    End Sub

    Private Sub init_employee()
        Dim subsegmentid_value As Integer
        value_person = ListView1.SelectedItems.Item(0).Text

        ExecuteQuery("SELECT tbl_distributorcpc.CPC,tbl_employeemaster.SubsegmentID,tbl_employeemaster.BTDTID,tbl_employeemaster.SFAID,tbl_employeemaster.IsSalesRep,tbl_employeemaster.IsSupervisor,tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department,tbl_employeeposition.Position FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_employeeposition ON tbl_employeemaster.PositionID = tbl_employeeposition.PositionID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID WHERE tbl_employeemaster.EmpInternalID ='" & ListView1.SelectedItems.Item(0).Text & "'")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                txtfirst_name.Text = datareader("FName")
                txtmiddle_name.Text = datareader("MName")
                txtlast_name.Text = datareader("LName")

                txtbranch.Text = datareader("Branch")
                txtdepartment.Text = datareader("Department")
                txtcpc.Text = datareader("CPC")

                If IsDBNull(datareader("JobTitle")) Then
                    txtjob_title.Text = ""
                Else
                    txtjob_title.Text = datareader("JobTitle")
                End If

                txtposition.Text = datareader("Position")

                If datareader("SubsegmentID") = "" Then
                    cbosubsegment.SelectedIndex = -1
                    txtsubsegment.Text = ""
                Else
                    subsegmentid_value = CInt(datareader("SubsegmentID"))
                End If


                If datareader("IsSupervisor") = 1 Then
                    txtsupervisor.Text = "YES"
                    cbosupervisor.SelectedItem = "YES"
                Else
                    txtsupervisor.Text = "NO"
                    cbosupervisor.SelectedItem = "NO"
                End If

                If datareader("IsSalesRep") = 1 Then
                    txtsales_rep.Text = "YES"
                    cbosales_rep.SelectedItem = "YES"
                Else
                    txtsales_rep.Text = "NO"
                    cbosales_rep.SelectedItem = "NO"
                End If

                If datareader("SFAID") = 1 Then
                    txtsfa.Text = "YES"
                    cbosfa.SelectedItem = "YES"
                Else
                    txtsfa.Text = "NO"
                    cbosfa.SelectedItem = "NO"
                End If

                If datareader("BTDTID") = 1 Then
                    txtbtdt.Text = "YES"
                    cbobtdt.SelectedItem = "YES"
                Else
                    txtbtdt.Text = "NO"
                    cbobtdt.SelectedItem = "NO"
                End If


            End While
        End If

        ListView1.Visible = False
        conn.Close()

        Dim table_subsegment As New DataTable
        ExecuteQuery("SELECT * from tbl_storesubsegment")
        datareader = cmd.ExecuteReader
        table_subsegment.Load(datareader)
        cbosubsegment.DisplayMember = "SubSegment"
        cbosubsegment.ValueMember = "SubSegmentID"
        cbosubsegment.DataSource = table_subsegment
        cbosubsegment.SelectedValue = subsegmentid_value
        conn.Close()


        If cbosubsegment.SelectedIndex = -1 Then

        Else
            ExecuteQuery("SELECT SubSegment from tbl_employeemaster LEFT JOIN tbl_storesubsegment ON tbl_employeemaster.SubsegmentID=tbl_storesubsegment.SubSegmentID WHERE tbl_employeemaster.SubsegmentID='" & subsegmentid_value & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    txtsubsegment.Text = datareader("SubSegment")
                End While
            End If
            conn.Close()
        End If

    End Sub

    Private Sub bttnsave_Click(sender As Object, e As EventArgs) Handles bttnsave.Click
        Dim supervisor, salesrep, sfa_id, btdt_id As Integer
        If value_person = "" Then
            MessageBox.Show("Please Select Employee")
        Else

            If cbosubsegment.SelectedIndex = -1 Then
                MessageBox.Show("Please Select Subsegment")
            Else
                If cbosupervisor.SelectedItem = "YES" Then
                    supervisor = 1
                Else
                    supervisor = 0
                End If

                If cbosales_rep.SelectedItem = "YES" Then
                    salesrep = 1
                Else
                    salesrep = 0
                End If

                If cbosfa.SelectedItem = "YES" Then
                    sfa_id = 1
                Else
                    sfa_id = 0
                End If

                If cbobtdt.SelectedItem = "YES" Then
                    btdt_id = 1
                Else
                    btdt_id = 0
                End If

                Dim n As String = MsgBox("Are you Sure you want to Update Sales Tag?", MsgBoxStyle.YesNo, "")
                If n = vbYes Then
                    ExecuteQuery("UPDATE tbl_employeemaster SET IsSupervisor=" & supervisor & ", IsSalesRep=" & salesrep & ", SFAID=" & sfa_id & ", BTDTID=" & btdt_id & ", SubsegmentID=" & cbosubsegment.SelectedValue & " WHERE EmpInternalID='" & value_person & "'")
                    MessageBox.Show("Successfuly Updated")
                    conn.Close()
                    Frm_sales_tagging_Load(e, e)
                End If

            End If
        End If
    End Sub
End Class