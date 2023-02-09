Public Class Frm_Masterdata_Finance_BIX_Area_Rate_Backup

    Dim value_areaID As String
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
        txtarea.Text = ""
        txtstatus.Text = ""
        txtcpc.Text = ""
        txtcpcid.Text = ""
        txtbranch_code.Text = ""

        txtrate.Text = ""
        txtsss.Text = ""
        txtpagibig.Text = ""
        txtphilhealth.Text = ""
        txtincentive.Text = ""

        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_employeearearate_backup ORDER BY Area ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("Area"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Status"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AreaRate"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("sss_premium"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("pagibig_premium"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("philhealth_premium"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("incentive_leave"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AreaRateBackup_ID"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_area As Integer
        Dim num1 As Single

        If txtarea.Text = "" Then
            MessageBox.Show("Please Enter Area")
            txtarea.Select()
        ElseIf txtstatus.Text = "" Then
            MessageBox.Show("Please Enter Status")
            txtstatus.Select()
        ElseIf txtcpc.Text = "" Then
            MessageBox.Show("Please Enter CPC")
            txtcpc.Select()
        ElseIf txtcpcid.Text = "" Then
            MessageBox.Show("Please Enter CPCID")
            txtcpcid.Select()
        ElseIf txtbranch_code.Text = "" Then
            MessageBox.Show("Please Enter Branch Code")
            txtbranch_code.Select()
        ElseIf Not Single.TryParse(txtrate.Text, num1) Then
            MessageBox.Show("Rate should be number")
            txtrate.Select()
        ElseIf Not Single.TryParse(txtsss.Text, num1) Then
            MessageBox.Show("SSS should be number")
            txtsss.Select()
        ElseIf Not Single.TryParse(txtpagibig.Text, num1) Then
            MessageBox.Show("Pag-IBIG should be number")
            txtpagibig.Select()
        ElseIf Not Single.TryParse(txtphilhealth.Text, num1) Then
            MessageBox.Show("PhilHealth should be number")
            txtphilhealth.Select()
        ElseIf Not Single.TryParse(txtincentive.Text, num1) Then
            MessageBox.Show("Incentive should be number")
            txtincentive.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_area FROM tbl_employeearearate_backup WHERE Area='" & txtarea.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_area = datareader("count_area")
                End While
            End If
            conn.Close()

            If count_area = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Area Exist")
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
                    ExecuteQuery("INSERT INTO tbl_employeearearate_backup (Area,Status,CPC,CPCID_NI,BranchCode_NI,AreaRate,sss_premium,pagibig_premium,philhealth_premium,incentive_leave) VALUES('" & txtarea.Text.Replace("'", "''") & "','" & txtstatus.Text.Replace("'", "''") & "','" & txtcpc.Text.Replace("'", "''") & "','" & txtcpcid.Text.Replace("'", "''") & "','" & txtbranch_code.Text.Replace("'", "''") & "'," & txtrate.Text & "," & txtsss.Text & "," & txtpagibig.Text & "," & txtphilhealth.Text & "," & txtincentive.Text & ")")
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
                    ExecuteQuery("UPDATE tbl_employeearearate_backup SET Area='" & txtarea.Text.Replace("'", "''") & "', Status='" & txtstatus.Text.Replace("'", "''") & "' , CPC='" & txtcpc.Text.Replace("'", "''") & "', CPCID_NI='" & txtcpcid.Text.Replace("'", "''") & "', BranchCode_NI='" & txtbranch_code.Text.Replace("'", "''") & "', AreaRate=" & txtrate.Text & ", sss_premium=" & txtsss.Text & ", pagibig_premium=" & txtpagibig.Text & ", philhealth_premium=" & txtphilhealth.Text & ", incentive_leave=" & txtincentive.Text & " WHERE AreaRateBackup_ID='" & value_areaID & "'")
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
        txtarea.Text = ListView2.SelectedItems(0).Text
        txtstatus.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtcpc.Text = ListView2.SelectedItems(0).SubItems(2).Text
        txtcpcid.Text = ListView2.SelectedItems(0).SubItems(3).Text
        txtbranch_code.Text = ListView2.SelectedItems(0).SubItems(4).Text
        txtrate.Text = ListView2.SelectedItems(0).SubItems(5).Text
        txtsss.Text = ListView2.SelectedItems(0).SubItems(6).Text
        txtpagibig.Text = ListView2.SelectedItems(0).SubItems(7).Text
        txtphilhealth.Text = ListView2.SelectedItems(0).SubItems(8).Text
        txtincentive.Text = ListView2.SelectedItems(0).SubItems(9).Text
        value_areaID = ListView2.SelectedItems(0).SubItems(10).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtarea.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_employeearearate_backup WHERE Area LIKE '" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY Area ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("Area"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Status"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AreaRate"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("sss_premium"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("pagibig_premium"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("philhealth_premium"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("incentive_leave"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AreaRateBackup_ID"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_Finance_BIX_Area_Rate_Backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class