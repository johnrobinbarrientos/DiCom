Public Class Frm_Fleet_Add
    Dim value_fleetvehicleID As String
    Dim internet_connection As Boolean

    Private Sub lblclose_MouseHover(sender As Object, e As EventArgs) Handles lblclose.MouseHover
        lblclose.Visible = False
        lblclose2.Visible = True
    End Sub

    Private Sub lblclose2_Click(sender As Object, e As EventArgs) Handles lblclose2.Click
        If bttnsave.Text = "SAVE" Then
            Me.Close()
            value_fleetvehicleID = ""
        Else
            Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
            Frm_Fleet_Search.Show()
            value_fleetvehicleID = ""
            Me.Close()
        End If

    End Sub

    Private Sub lblclose2_MouseLeave(sender As Object, e As EventArgs) Handles lblclose2.MouseLeave
        lblclose.Visible = True
        lblclose2.Visible = False
    End Sub

    Private Sub Frm_fleet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
        value_fleetvehicleID = Frm_Fleet_Search.value_fleetvehicleID

        If value_fleetvehicleID = "" Then
            Call initialize()
        Else
            Dim vehicle_typeID, branch_code, cpc_id, brand_id As Integer

            bttnsave.Text = "UPDATE"
            lblform_name.Text = "Update Vehicle"

            ExecuteQuery("SELECT * from tbl_fleetmaster WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    cbovehicle.Text = datareader("Vehicle")
                    cbofunction.Text = datareader("Function")
                    cboclass.Text = datareader("VehicleClass")
                    cbostatus.Text = datareader("Status")
                    cbofuel_type.Text = datareader("FuelType")
                    txtplate_no.Text = datareader("PlateNo")
                    date_acquisition.Text = datareader("DateAcquisition")
                    txtremarks.Text = datareader("Remarks")
                    vehicle_typeID = datareader("VehicleTypeID")
                    branch_code = datareader("BranchCode")
                    cpc_id = datareader("CPCID")
                    brand_id = datareader("BrandID")
                    txtmodel.Text = datareader("Model")

                End While
            End If
            conn.Close()


            Dim table_fleettype As New DataTable
            ExecuteQuery("SELECT * from tbl_fleetvehicletype")
            datareader = cmd.ExecuteReader
            table_fleettype.Load(datareader)
            cbotype.DisplayMember = "VehicleType"
            cbotype.ValueMember = "VehicleTypeID"
            cbotype.DataSource = table_fleettype
            cbotype.SelectedValue = vehicle_typeID
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

            Dim table_branch As New DataTable
            ExecuteQuery("SELECT * from tbl_distributorbranch")
            datareader = cmd.ExecuteReader
            table_branch.Load(datareader)
            cbobranch.DisplayMember = "Branch"
            cbobranch.ValueMember = "BranchCode"
            cbobranch.DataSource = table_branch
            cbobranch.SelectedValue = branch_code
            conn.Close()

            Dim table_brand As New DataTable
            ExecuteQuery("SELECT * from tbl_fleetbrand")
            datareader = cmd.ExecuteReader
            table_brand.Load(datareader)
            cbobrand.DisplayMember = "Brand"
            cbobrand.ValueMember = "BrandID"
            cbobrand.DataSource = table_brand
            cbobrand.SelectedValue = brand_id
            conn.Close()

        End If


    End Sub

    Private Sub initialize()
        cbovehicle.SelectedItem = "Service"
        cbobranch.SelectedItem = "CDO"
        cbofunction.SelectedItem = "COLLECTOR"
        cbofuel_type.SelectedItem = "DIESEL"
        cbostatus.SelectedItem = "ACTIVE"
        cboclass.SelectedItem = "Brand New"
        date_acquisition.Text = Now
        txtplate_no.Text = ""
        txtremarks.Text = ""
        txtmodel.Text = ""
        lblform_name.Text = "Add Vehicle"
        bttnsave.Text = "SAVE"

        Dim table_fleettype As New DataTable
        ExecuteQuery("SELECT * from tbl_fleetvehicletype")
        datareader = cmd.ExecuteReader
        table_fleettype.Load(datareader)
        cbotype.DisplayMember = "VehicleType"
        cbotype.ValueMember = "VehicleTypeID"
        cbotype.DataSource = table_fleettype
        conn.Close()

        Dim table_cpc As New DataTable

        ExecuteQuery("SELECT * from tbl_distributorcpc")
        datareader = cmd.ExecuteReader
        table_cpc.Load(datareader)
        cbocpc.DisplayMember = "CPC"
        cbocpc.ValueMember = "CPCID"
        cbocpc.DataSource = table_cpc
        conn.Close()

        Dim table_branch As New DataTable
        ExecuteQuery("SELECT * from tbl_distributorbranch")
        datareader = cmd.ExecuteReader
        table_branch.Load(datareader)
        cbobranch.DisplayMember = "Branch"
        cbobranch.ValueMember = "BranchCode"
        cbobranch.DataSource = table_branch
        conn.Close()

        Dim table_brand As New DataTable
        ExecuteQuery("SELECT * from tbl_fleetbrand")
        datareader = cmd.ExecuteReader
        table_brand.Load(datareader)
        cbobrand.DisplayMember = "Brand"
        cbobrand.ValueMember = "BrandID"
        cbobrand.DataSource = table_brand
        conn.Close()

    End Sub
    Private Sub trap()
        Dim count_emp As Integer
        If txtplate_no.Text = "" Then
            MessageBox.Show("Please Enter Plate Number")
            txtplate_no.Select()
        Else
            If bttnsave.Text = "SAVE" Then
                ExecuteQuery("SELECT COUNT(*) as count_PlateNo from tbl_fleetmaster WHERE PlateNo='" & txtplate_no.Text.Replace("'", "''") & "'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        count_emp = datareader("count_PlateNo")
                    End While
                End If
                conn.Close()

                If count_emp = 0 Then
                    Call save()
                Else
                    MessageBox.Show("Vehicle Exist")
                End If

            Else
                Call save()
            End If
        End If
    End Sub

    Private Sub save()
        Dim trap_update As Integer

        If bttnsave.Text = "SAVE" Then

            Dim n As String = MsgBox("Add New Record?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then
                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("INSERT INTO tbl_fleetmaster (Vehicle,BranchCode,Function,VehicleTypeID,CPCID,Status,FuelType,PlateNo,DateAcquisition,VehicleClass,Remarks,Istag,BrandID,Model) VALUES('" & cbovehicle.Text & "','" & cbobranch.SelectedValue & "','" & cbofunction.Text & "','" & cbotype.SelectedValue & "','" & cbocpc.SelectedValue & "','" & cbostatus.Text & "','" & cbofuel_type.Text & "','" & txtplate_no.Text.Replace("'", "''") & "','" & date_acquisition.Text & "','" & cboclass.Text & "','" & txtremarks.Text.Replace("'", "''") & "','0','" & cbobrand.SelectedValue & "','" & txtmodel.Text.Replace("'", "''") & "')")
                    MessageBox.Show("Successfuly Save")
                    conn.Close()
                    Call initialize()
                    Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
                    Frm_Fleet_Search.Show()
                    Me.Close()
                End If

            End If

        Else

            ExecuteQuery("SELECT EmployeeID FROM tbl_fleetmaster WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    If IsDBNull(datareader("EmployeeID")) Then
                        trap_update = 0
                    Else
                        trap_update = 1
                    End If
                End While
            End If
            conn.Close()

            If trap_update = 0 Then

                Dim n As String = MsgBox("Save Changes?", MsgBoxStyle.YesNo, "")

                If n = vbYes Then

                    Call Connection.checkconnection()
                    internet_connection = Connection.internet_connection

                    If internet_connection = False Then
                        MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                        Exit Sub
                    Else
                        ExecuteQuery("UPDATE tbl_fleetmaster SET Vehicle='" & cbovehicle.Text & "', BranchCode='" & cbobranch.SelectedValue & "', Function='" & cbofunction.Text & "', CPCID='" & cbocpc.SelectedValue & "', VehicleTypeID='" & cbotype.SelectedValue & "', Status='" & cbostatus.Text & "', FuelType='" & cbofuel_type.Text & "', PlateNo='" & txtplate_no.Text.Replace("'", "''") & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "', DateAcquisition='" & date_acquisition.Text & "', VehicleClass='" & cboclass.Text & "', BrandID='" & cbobrand.SelectedValue & "', Model='" & txtmodel.Text.Replace("'", "''") & "' WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
                        MessageBox.Show("Successfuly Updated")
                        conn.Close()
                        Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
                        Frm_Fleet_Search.Show()
                        Me.Close()
                    End If

                End If

            Else

                If cbostatus.Text = "INACTIVE" Then

                    MessageBox.Show("Vehicle still assigned please unassign vehicle first")

                Else

                    Dim n As String = MsgBox("Save Changes?", MsgBoxStyle.YesNo, "")

                    If n = vbYes Then
                        Call Connection.checkconnection()
                        internet_connection = Connection.internet_connection

                        If internet_connection = False Then
                            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                            Exit Sub
                        Else
                            ExecuteQuery("UPDATE tbl_fleetmaster SET Vehicle='" & cbovehicle.Text & "', BranchCode='" & cbobranch.SelectedValue & "', Function='" & cbofunction.Text & "', CPCID='" & cbocpc.SelectedValue & "', VehicleTypeID='" & cbotype.SelectedValue & "', Status='" & cbostatus.Text & "', FuelType='" & cbofuel_type.Text & "', PlateNo='" & txtplate_no.Text.Replace("'", "''") & "', Remarks='" & txtremarks.Text.Replace("'", "''") & "', DateAcquisition='" & date_acquisition.Text & "', VehicleClass='" & cboclass.Text & "', BrandID='" & cbobrand.SelectedValue & "', Model='" & txtmodel.Text.Replace("'", "''") & "' WHERE FleetVehicleID='" & value_fleetvehicleID & "'")
                            MessageBox.Show("Successfuly Updated")
                            conn.Close()
                            Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
                            Frm_Fleet_Search.Show()
                            Me.Close()
                        End If

                    End If
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
End Class