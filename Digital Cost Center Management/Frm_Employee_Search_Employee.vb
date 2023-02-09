Public Class Frm_Employee_Search_Employee
    Public value_person As String
    Public supervisor As String
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

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If txtsearch.Text = "" Then
            ListView1.Visible = False

        Else
            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ListView1.Visible = True
                ExecuteQuery("SELECT SupID,EmployeeID,EmpInternalID, CONCAT(LName,', ',FName,' ',MName) as fullname from tbl_employeemaster WHERE (LName LIKE '" & txtsearch.Text.Replace("'", "''") & "%' OR FName LIKE '%" & txtsearch.Text.Replace("'", "''") & "%') AND JobTitle<>'SFA PURPOSES ONLY' OR JobTitle IS NUll")
                datareader = cmd.ExecuteReader
            End If

        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("EmployeeID"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("fullname"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("SupID").ToString)
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("EmpInternalID").ToString)

            End While
        End If
        conn.Close()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection
        value_person = ListView1.SelectedItems.Item(0).Text
        supervisor = ListView1.SelectedItems(0).SubItems(2).Text

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC,tbl_employeeposition.Position FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeeposition ON tbl_employeemaster.PositionID = tbl_employeeposition.PositionID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmployeeID ='" & ListView1.SelectedItems.Item(0).Text & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                txtfirst_name.Text = datareader("FName")
                txtmiddle_name.Text = datareader("MName")
                txtlast_name.Text = datareader("LName")

                txtbranch.Text = datareader("Branch")
                txtdepartment.Text = datareader("Department")

                If IsDBNull(datareader("JobTitle")) Then
                    txtjob_title.Text = ""
                Else
                    txtjob_title.Text = datareader("JobTitle")
                End If

                txtcpc.Text = datareader("CPC")
                txtID.Text = datareader("EmpExternalID")

            End While
        End If

        ListView1.Visible = False
        conn.Close()


        ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & ListView1.SelectedItems(0).SubItems(2).Text & "'")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                txtsupervisor.Text = datareader("fullname")

            End While
        End If
        conn.Close()

        ListView2.Items.Clear()
        ExecuteQuery("SELECT tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.Model FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID WHERE tbl_itassetmaster.PAREmpInternalID='" & ListView1.SelectedItems(0).SubItems(3).Text & "'")
        datareader = cmd.ExecuteReader

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("Type"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Model"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AssetDesc"))
            End While
        End If
        conn.Close()

        ListView4.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID WHERE tbl_fleetmaster.EmployeeID='" & ListView1.SelectedItems.Item(0).Text & "'")
        datareader = cmd.ExecuteReader

        If datareader.HasRows Then
            While (datareader.Read)
                ListView4.Items.Add(datareader("Status"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Vehicle"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("PlateNo"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Function"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Branch"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("FuelType"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("VehicleType"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("DateAcquisition"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("VehicleClass"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Model"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("CPC"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("FleetVehicleID"))
            End While
        End If
        conn.Close()

    End Sub

    Private Sub bttn_show_employee_Click_1(sender As Object, e As EventArgs) Handles bttn_show_employee.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If value_person = "" Then
            MessageBox.Show("Please Select Employee")
        Else
            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else

                Frm_employee_profile.MdiParent = Me.MdiParent
                Frm_employee_profile.StartPosition = FormStartPosition.CenterScreen
                Frm_employee_profile.Show()

            End If
        End If
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
            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection
            value_person = ListView1.SelectedItems.Item(0).Text
            supervisor = ListView1.SelectedItems(0).SubItems(2).Text

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC,tbl_employeeposition.Position FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeeposition ON tbl_employeemaster.PositionID = tbl_employeeposition.PositionID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmployeeID ='" & ListView1.SelectedItems.Item(0).Text & "'")
                datareader = cmd.ExecuteReader
            End If

            If datareader.HasRows Then
                While (datareader.Read)
                    txtfirst_name.Text = datareader("FName")
                    txtmiddle_name.Text = datareader("MName")
                    txtlast_name.Text = datareader("LName")

                    txtbranch.Text = datareader("Branch")
                    txtdepartment.Text = datareader("Department")

                    If IsDBNull(datareader("JobTitle")) Then
                        txtjob_title.Text = ""
                    Else
                        txtjob_title.Text = datareader("JobTitle")
                    End If

                    txtcpc.Text = datareader("CPC")
                    txtID.Text = datareader("EmpExternalID")

                End While
            End If

            ListView1.Visible = False
            conn.Close()


            ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & ListView1.SelectedItems(0).SubItems(2).Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    txtsupervisor.Text = datareader("fullname")

                End While
            End If
            conn.Close()

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.Model FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID WHERE tbl_itassetmaster.PAREmpInternalID='" & ListView1.SelectedItems(0).SubItems(3).Text & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("Type"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Brand"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Model"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AssetDesc"))
                End While
            End If
            conn.Close()

            ListView4.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID WHERE tbl_fleetmaster.EmployeeID='" & ListView1.SelectedItems.Item(0).Text & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    ListView4.Items.Add(datareader("Status"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Vehicle"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("PlateNo"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Function"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Branch"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("FuelType"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("VehicleType"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("DateAcquisition"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("VehicleClass"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Brand"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Model"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("CPC"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("FleetVehicleID"))
                End While
            End If
            conn.Close()
        End If
    End Sub


End Class