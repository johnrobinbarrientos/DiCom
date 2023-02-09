Public Class Frm_Masterdata_Fleet_Vehicle_Type
    Dim value_vehicletypeID As String
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

    Private Sub initialize()
        txtvehicletype.Text = ""
        txtpg_code.Text = ""
        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_fleetvehicletype ORDER BY VehicleType ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("VehicleTypeID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("VehicleType"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("PG_Code_VehicleType"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_vehicletype As Integer

        If txtvehicletype.Text = "" Then
            MessageBox.Show("Please Enter Vehicle Type")
            txtvehicletype.Select()
        ElseIf txtpg_code.Text = "" Then
            MessageBox.Show("Please Enter P&G Code")
            txtpg_code.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_vehicletype FROM tbl_fleetvehicletype WHERE VehicleType='" & txtvehicletype.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_vehicletype = datareader("count_vehicletype")
                End While
            End If
            conn.Close()

            If count_vehicletype = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Vehicle Type Exist")
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
                    ExecuteQuery("INSERT INTO tbl_fleetvehicletype (VehicleType,PG_Code_VehicleType) VALUES('" & txtvehicletype.Text.Replace("'", "''") & "','" & txtpg_code.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_fleetvehicletype SET VehicleType='" & txtvehicletype.Text.Replace("'", "''") & "', PG_Code_VehicleType='" & txtpg_code.Text.Replace("'", "''") & "' WHERE VehicleTypeID='" & value_vehicletypeID & "'")
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
        value_vehicletypeID = ListView2.SelectedItems(0).Text
        txtvehicletype.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtpg_code.Text = ListView2.SelectedItems(0).SubItems(2).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtvehicletype.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_fleetvehicletype WHERE VehicleType LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY VehicleType ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("VehicleTypeID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("VehicleType"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("PG_Code_VehicleType"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_Fleet_Vehicle_Type_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class