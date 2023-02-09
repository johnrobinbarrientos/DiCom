Public Class Frm_Masterdata_Employee_Branch
    Dim value_branch As String
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
        txtbranch_code.Text = ""
        txtbranch_code_ni.Text = ""
        txtbranch.Text = ""
        txtbranch_ns.Text = ""
        txtregion_code.Text = ""
        txtbranch_prefix.Text = ""
        txtmw_id.Text = ""
        txtbranch_address.Text = ""

        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_distributorbranch ORDER BY Branch ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("BranchCode"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("RegionCode"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchPrefix"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchAddress"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MainWarehouseID"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_branch As Integer
        Dim num1 As Single


        If Not Single.TryParse(txtbranch_code.Text, num1) Then
            MessageBox.Show("Branch Code should be number")
            txtbranch_code.Select()
        ElseIf Not Single.TryParse(txtbranch_code_ni.Text, num1) Then
            MessageBox.Show("Branch Code NI should be number")
            txtbranch_code_ni.Select()
        ElseIf txtbranch.Text = "" Then
            MessageBox.Show("Please Enter Branch")
            txtbranch.Select()
        ElseIf txtbranch_ns.Text = "" Then
            MessageBox.Show("Please Enter Branch NS")
            txtbranch_ns.Select()
        ElseIf Not Single.TryParse(txtregion_code.Text, num1) Then
            MessageBox.Show("Region Code should be number")
            txtregion_code.Select()
        ElseIf txtbranch_prefix.Text = "" Then
            MessageBox.Show("Please Enter Branch Prefix")
            txtbranch_prefix.Select()
        ElseIf txtbranch_address.Text = "" Then
            MessageBox.Show("Please Enter Branch Address")
            txtbranch_address.Select()
        ElseIf Not Single.TryParse(txtmw_id.Text, num1) Then
            MessageBox.Show("MW ID should be number")
            txtmw_id.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_branch FROM tbl_distributorbranch WHERE Branch='" & txtbranch.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_branch = datareader("count_branch")
                End While
            End If
            conn.Close()

            If count_branch = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Branch Exist")
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

                    ExecuteQuery("INSERT INTO tbl_distributorbranch (BranchCode,BranchCode_NI,Branch,Branch_NS,RegionCode,BranchPrefix,BranchAddress,MainWarehouseID) VALUES(" & txtbranch_code.Text & "," & txtbranch_code_ni.Text & ",'" & txtbranch.Text.Replace("'", "''") & "','" & txtbranch_ns.Text.Replace("'", "''") & "'," & txtregion_code.Text & ",'" & txtbranch_prefix.Text.Replace("'", "''") & "','" & txtbranch_address.Text.Replace("'", "''") & "'," & txtmw_id.Text & ")")
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
                    ExecuteQuery("UPDATE tbl_distributorbranch SET BranchCode=" & txtbranch_code.Text & ", BranchCode_NI=" & txtbranch_code_ni.Text & ", Branch='" & txtbranch.Text.Replace("'", "''") & "', Branch_NS='" & txtbranch_ns.Text.Replace("'", "''") & "', RegionCode=" & txtregion_code.Text & ", BranchPrefix='" & txtbranch_prefix.Text.Replace("'", "''") & "', BranchAddress='" & txtbranch_address.Text.Replace("'", "''") & "', MainWarehouseID=" & txtmw_id.Text & " WHERE Branch='" & value_branch & "'")
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
        value_branch = ListView2.SelectedItems(0).SubItems(2).Text
        txtbranch.Text = ListView2.SelectedItems(0).SubItems(2).Text

        txtbranch_code.Text = ListView2.SelectedItems(0).Text
        txtbranch_code_ni.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtbranch_ns.Text = ListView2.SelectedItems(0).SubItems(3).Text
        txtregion_code.Text = ListView2.SelectedItems(0).SubItems(4).Text
        txtbranch_prefix.Text = ListView2.SelectedItems(0).SubItems(5).Text
        txtbranch_address.Text = ListView2.SelectedItems(0).SubItems(6).Text
        txtmw_id.Text = ListView2.SelectedItems(0).SubItems(7).Text

        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtbranch_code.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_distributorbranch WHERE Branch LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY Branch ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("BranchCode"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("RegionCode"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchPrefix"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchAddress"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MainWarehouseID"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_Employee_Branch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class