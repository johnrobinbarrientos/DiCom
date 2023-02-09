Public Class Frm_Fleet_Tag
    Dim value_person, supervisor, value_EmployeeIDtrap As String
    Dim value_fleetvehicleID As Integer
    Dim internet_connection As Boolean

    Private Sub lblclose_MouseHover(sender As Object, e As EventArgs) Handles lblclose.MouseHover
        lblclose.Visible = False
        lblclose2.Visible = True
    End Sub

    Private Sub lblclose2_Click(sender As Object, e As EventArgs) Handles lblclose2.Click
        Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
        Frm_Fleet_Search.Show()
        Me.Close()
        value_fleetvehicleID = 0
    End Sub

    Private Sub lblclose2_MouseLeave(sender As Object, e As EventArgs) Handles lblclose2.MouseLeave
        lblclose.Visible = True
        lblclose2.Visible = False
    End Sub


    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
        Frm_Fleet_Search.Show()
        Me.Close()
    End Sub

    Private Sub bttnassign_Click_1(sender As Object, e As EventArgs) Handles bttnassign.Click
        If ListView4.Items.Count = 0 Then

            If value_person = "" Then
                MessageBox.Show("Please Select Assignee")
            Else

                Dim n As String = MsgBox("Are you Sure you want to Assign Vehicle?", MsgBoxStyle.YesNo, "")

                If n = vbYes Then

                    Call Connection.checkconnection()
                    internet_connection = Connection.internet_connection

                    If internet_connection = False Then
                        MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                        Exit Sub
                    Else
                        ExecuteQuery("UPDATE tbl_fleetmaster SET EmployeeID=" & value_person & ", Istag='1' WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
                        MessageBox.Show("Successfuly Updated")
                        conn.Close()
                        Call initialize()
                        value_person = ""
                    End If

                End If

            End If

        Else

            If value_person = "" Then
                MessageBox.Show("Please Select Assignee")
            Else

                If value_EmployeeIDtrap = value_person Then

                    MessageBox.Show("Employee already assigned")

                Else

                    Dim n As String = MsgBox("Are you Sure you want to Assign Vehicle?", MsgBoxStyle.YesNo, "")

                    If n = vbYes Then

                        Call Connection.checkconnection()
                        internet_connection = Connection.internet_connection

                        If internet_connection = False Then
                            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                            Exit Sub
                        Else
                            ExecuteQuery("UPDATE tbl_fleetmaster SET EmployeeID=" & value_person & ", Istag='1' WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
                            MessageBox.Show("Successfuly Updated")
                            conn.Close()
                            Call initialize()
                            value_person = ""
                        End If

                    End If

                End If


            End If

        End If

    End Sub

    Private Sub bttnunassigned_Click(sender As Object, e As EventArgs) Handles bttnunassigned.Click
        If ListView4.Items.Count = 0 Then

            MessageBox.Show("No Assignee on this vehicle")

        Else

            Dim n As String = MsgBox("Unassigned Vehicle?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("UPDATE tbl_fleetmaster SET EmployeeID=null, Istag='0' WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
                    MessageBox.Show("Successfuly Updated")
                    conn.Close()
                    value_EmployeeIDtrap = ""
                    Call initialize()
                End If

            End If
        End If

    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text = "" Then
            ListView1.Visible = False

        Else

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ListView1.Visible = True
                ExecuteQuery("SELECT SupID,EmployeeID, CONCAT(LName,', ',FName,' ',MName) as fullname from tbl_employeemaster WHERE (LName LIKE '" & txtsearch.Text.Replace("'", "''") & "%' OR FName LIKE '%" & txtsearch.Text.Replace("'", "''") & "%') AND JobTitle<>'SFA PURPOSES ONLY' OR JobTitle IS NUll")
                datareader = cmd.ExecuteReader
            End If

        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("EmployeeID"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("fullname"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("SupID").ToString)

            End While
        End If
        conn.Close()
    End Sub

    Private Sub initialize()
        Dim value_EmployeeID As Integer


        value_fleetvehicleID = Frm_Fleet_Search.value_fleetvehicleID

        ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID WHERE tbl_fleetmaster.FleetVehicleID='" & value_fleetvehicleID & "'")
        datareader = cmd.ExecuteReader

        If datareader.HasRows Then
            While (datareader.Read)

                txtvehicle.Text = datareader("Vehicle")
                txtplate_no.Text = datareader("PlateNo")
                txtfunction.Text = datareader("Function")
                txtbranch_vehicle.Text = datareader("Branch")
                txtfuel_type.Text = datareader("FuelType")
                txttype.Text = datareader("VehicleType")
                txtacquisition.Text = datareader("DateAcquisition")
                txtstatus.Text = datareader("Status")
                txtremarks.Text = datareader("Remarks")
                txtvehicle_class.Text = datareader("VehicleClass")
                txtbrand.Text = datareader("Brand")
                txtmodel.Text = datareader("Model")
                txtcpc_vehicle.Text = datareader("CPC")
                If IsDBNull(datareader("EmployeeID")) Then
                    value_EmployeeID = 0
                Else
                    value_EmployeeID = datareader("EmployeeID")
                End If


            End While
        End If
        conn.Close()

        txtsearch.Text = ""
        txtfirst_name.Text = ""
        txtmiddle_name.Text = ""
        txtlast_name.Text = ""
        txtbranch.Text = ""
        txtdepartment.Text = ""
        txtjob_title.Text = ""
        txtcpc.Text = ""
        txtsupervisor.Text = ""

        If value_EmployeeID = 0 Then
            ListView4.Items.Clear()
        Else
            ListView4.Items.Clear()
            ExecuteQuery("SELECT tbl_employeemaster.EmployeeID,tbl_employeejobtitle.JobTitle,tbl_employeemaster.SupID,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmployeeID ='" & value_EmployeeID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView4.Items.Add(datareader("FName"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("CPC"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Branch"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView4.Items(ListView4.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("EmployeeID"))
                    value_EmployeeIDtrap = datareader("EmployeeID")
                End While
            End If
            conn.Close()
        End If


    End Sub



    Private Sub Frm_tag_vehicle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub


    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        value_person = ListView1.SelectedItems.Item(0).Text
        supervisor = ListView1.SelectedItems(0).SubItems(2).Text

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmployeeID ='" & ListView1.SelectedItems.Item(0).Text & "'")
            datareader = cmd.ExecuteReader
        End If

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

            End While
        End If
        conn.Close()
        ListView1.Visible = False

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & ListView1.SelectedItems(0).SubItems(2).Text & "'")
            datareader = cmd.ExecuteReader
        End If


        If datareader.HasRows Then
            While (datareader.Read)
                txtsupervisor.Text = datareader("fullname")

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
            value_person = ListView1.SelectedItems.Item(0).Text
            supervisor = ListView1.SelectedItems(0).SubItems(2).Text

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmployeeID ='" & ListView1.SelectedItems.Item(0).Text & "'")
                datareader = cmd.ExecuteReader
            End If

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

                End While
            End If
            conn.Close()
            ListView1.Visible = False

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & ListView1.SelectedItems(0).SubItems(2).Text & "'")
                datareader = cmd.ExecuteReader
            End If


            If datareader.HasRows Then
                While (datareader.Read)
                    txtsupervisor.Text = datareader("fullname")

                End While
            End If
            conn.Close()
        End If
    End Sub


End Class