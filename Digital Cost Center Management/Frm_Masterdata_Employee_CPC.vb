Public Class Frm_Masterdata_Employee_CPC
    Dim value_cpc As String
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
        txtcpc.Text = ""
        txtcpcid.Text = ""
        txtcpcid_ni.Text = ""
        txtsearch.Text = ""
        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_distributorcpc ORDER BY CPC ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("CPCID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_cpc As Integer
        Dim num1 As Single


        If Not Single.TryParse(txtcpcid.Text, num1) Then
            MessageBox.Show("CPCID should be number")
            txtcpcid.Select()
        ElseIf txtcpc.Text = "" Then
            MessageBox.Show("Please Enter CPC")
            txtcpc.Select()
        ElseIf Not Single.TryParse(txtcpcid_ni.Text, num1) Then
            MessageBox.Show("CPCID NI should be number")
            txtcpcid_ni.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_cpc FROM tbl_distributorcpc WHERE CPC='" & txtcpc.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_cpc = datareader("count_cpc")
                End While
            End If
            conn.Close()

            If count_cpc = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("CPC Exist")
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

                    ExecuteQuery("INSERT INTO tbl_distributorcpc (CPCID,CPC,CPCID_NI) VALUES(" & txtcpcid.Text & ",'" & txtcpc.Text.Replace("'", "''") & "'," & txtcpcid_ni.Text & ")")
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
                    ExecuteQuery("UPDATE tbl_distributorcpc SET CPCID=" & txtcpcid.Text & ", CPC='" & txtcpc.Text.Replace("'", "''") & "', CPCID_NI=" & txtcpcid_ni.Text & " WHERE CPC='" & value_cpc & "'")
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
        value_cpc = ListView2.SelectedItems(0).SubItems(1).Text
        txtcpc.Text = ListView2.SelectedItems(0).SubItems(1).Text

        txtcpcid.Text = ListView2.SelectedItems(0).Text
        txtcpcid_ni.Text = ListView2.SelectedItems(0).SubItems(2).Text

        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtcpcid.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_distributorcpc WHERE CPC LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY CPC ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("CPCID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_Employee_CPC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub

End Class