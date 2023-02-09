Public Class Frm_Fleet_Search
    Public value_fleetvehicleID As String
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

    Private Sub Frm_search_vehicle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbofilter.SelectedItem = "ALL"

        ListView4.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID")
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

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID WHERE PlateNo LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
            datareader = cmd.ExecuteReader
        End If

        ListView4.Items.Clear()

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

    Private Sub bttn_update_vehicle_Click(sender As Object, e As EventArgs) Handles bttn_update_vehicle.Click
        If ListView4.SelectedItems.Count = 0 Then
            MessageBox.Show("Please Select Vehicle")
        Else
            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                value_fleetvehicleID = ListView4.SelectedItems(0).SubItems(13).Text
                Frm_Fleet_Add.MdiParent = Me.MdiParent
                Frm_Fleet_Add.StartPosition = FormStartPosition.CenterScreen
                Frm_Fleet_Add.Show()
                Me.Close()
            End If

        End If
    End Sub

    Private Sub cbofilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbofilter.SelectedIndexChanged
        If cbofilter.Text = "ALL" Then

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID")
                datareader = cmd.ExecuteReader
            End If

            ListView4.Items.Clear()

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
        Else

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT * FROM tbl_fleetmaster LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID WHERE Status='" & cbofilter.Text & "'")
                datareader = cmd.ExecuteReader
            End If


            ListView4.Items.Clear()

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

    Private Sub ListView4_DoubleClick(sender As Object, e As EventArgs) Handles ListView4.DoubleClick
        If ListView4.SelectedItems(0).Text = "INACTIVE" Then
            MessageBox.Show("Please update status as ACTIVE")
        Else
            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                value_fleetvehicleID = ListView4.SelectedItems(0).SubItems(13).Text
                Frm_Fleet_Tag.MdiParent = Me.MdiParent
                Frm_Fleet_Tag.StartPosition = FormStartPosition.CenterScreen
                Frm_Fleet_Tag.Show()
            End If

        End If

    End Sub

    Private Sub bttnadd_Click(sender As Object, e As EventArgs) Handles bttnadd.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_fleetvehicleID = ""
            Frm_Fleet_Add.MdiParent = Me.MdiParent
            Frm_Fleet_Add.StartPosition = FormStartPosition.CenterScreen
            Frm_Fleet_Add.Show()
            Me.Close()
        End If

    End Sub

End Class