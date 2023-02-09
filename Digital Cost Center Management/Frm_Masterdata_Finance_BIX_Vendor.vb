Public Class Frm_Masterdata_Finance_BIX_Vendor
    Dim value_bixID As String
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
        txtinternal_vendor_id.Text = ""
        txtvendor_id.Text = ""
        txtvendor_name.Text = ""
        txtvendor_name_ns.Text = ""
        cbostatus.SelectedItem = "ACTIVE"
        cbotype_expenses.SelectedItem = "CONTRACTUAL"
        cbodiscount.SelectedItem = "NO"
        txtsearch.Text = ""

        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_bixvendor ORDER BY vendorname ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("bixvendorID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("InternalvendorID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("vendorID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("vendorname"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("vendornetsuite"))

                If datareader("status") = 0 Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("typeofexpenses"))

                If datareader("discount") = 0 Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("NO")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("YES")
                End If

            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_vendorname As Integer
        Dim num1 As Single

        If Not Single.TryParse(txtinternal_vendor_id.Text, num1) Then
            MessageBox.Show("Internal Vendor ID should be number")
            txtinternal_vendor_id.Select()
        ElseIf txtvendor_id.Text = "" Then
            MessageBox.Show("Please Enter Vendor ID")
            txtvendor_id.Select()
        ElseIf txtvendor_name.Text = "" Then
            MessageBox.Show("Please Enter Vendor Name")
            txtvendor_name.Select()
        ElseIf txtvendor_name_ns.Text = "" Then
            MessageBox.Show("Please Enter Vendor Name NS")
            txtvendor_name_ns.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_vendorname FROM tbl_bixvendor WHERE vendorname='" & txtvendor_name.Text.Replace("'", "''") & "' AND typeofexpenses='" & cbotype_expenses.Text & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_vendorname = datareader("count_vendorname")
                End While
            End If
            conn.Close()

            If count_vendorname = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Vendor Name And Type of Expense Exist")
                End If
            End If

        End If
    End Sub

    Private Sub save()
        Dim status_value, discount_value As Integer

        If cbostatus.Text = "ACTIVE" Then
            status_value = 0
        Else
            status_value = 1
        End If

        If cbodiscount.Text = "NO" Then
            discount_value = 0
        Else
            discount_value = 1
        End If


        If bttnsave.Text = "SAVE" Then

            Dim n As String = MsgBox("Add New Record?", MsgBoxStyle.YesNo, "")
            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else

                    ExecuteQuery("INSERT INTO tbl_bixvendor (InternalvendorID,vendorID,vendorname,vendornetsuite,status,typeofexpenses,discount) VALUES('" & txtinternal_vendor_id.Text & "','" & txtvendor_id.Text.Replace("'", "''") & "','" & txtvendor_name.Text.Replace("'", "''") & "','" & txtvendor_name_ns.Text.Replace("'", "''") & "'," & status_value & ",'" & cbotype_expenses.Text & "'," & discount_value & ")")
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
                    ExecuteQuery("UPDATE tbl_bixvendor SET InternalvendorID='" & txtinternal_vendor_id.Text & "', vendorID='" & txtvendor_id.Text.Replace("'", "''") & "', vendorname='" & txtvendor_name.Text.Replace("'", "''") & "', vendornetsuite='" & txtvendor_name_ns.Text.Replace("'", "''") & "', status=" & status_value & ", typeofexpenses='" & cbotype_expenses.Text & "', discount=" & discount_value & " WHERE bixvendorID='" & value_bixID & "'")
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
        value_bixID = ListView2.SelectedItems(0).Text
        txtinternal_vendor_id.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtvendor_id.Text = ListView2.SelectedItems(0).SubItems(2).Text
        txtvendor_name.Text = ListView2.SelectedItems(0).SubItems(3).Text
        txtvendor_name_ns.Text = ListView2.SelectedItems(0).SubItems(4).Text

        cbostatus.SelectedItem = ListView2.SelectedItems(0).SubItems(5).Text
        cbotype_expenses.SelectedItem = ListView2.SelectedItems(0).SubItems(6).Text
        cbodiscount.SelectedItem = ListView2.SelectedItems(0).SubItems(7).Text

        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtinternal_vendor_id.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_bixvendor WHERE vendorname LIKE '" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY vendorname ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)

                ListView2.Items.Add(datareader("bixvendorID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("InternalvendorID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("vendorID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("vendorname"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("vendornetsuite"))

                If datareader("status") = 0 Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("typeofexpenses"))

                If datareader("discount") = 0 Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("NO")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("YES")
                End If

            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_Finance_BIX_Vendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class