Public Class Frm_Masterdata_IT_MS_Office
    Dim value_msoffice As String
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
        txtmsoffice.Text = ""
        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetoffice ORDER BY Office ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("Office"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_office As Integer

        If txtmsoffice.Text = "" Then
            MessageBox.Show("Please Enter Antivirus")
            txtmsoffice.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_office FROM tbl_itassetoffice WHERE Office='" & txtmsoffice.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_office = datareader("count_office")
                End While
            End If
            conn.Close()

            If count_office = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("MS Office Exist")
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
                    ExecuteQuery("INSERT INTO tbl_itassetoffice (Office) VALUES('" & txtmsoffice.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_itassetoffice SET Office='" & txtmsoffice.Text.Replace("'", "''") & "' WHERE Office='" & value_msoffice & "'")
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
        value_msoffice = ListView2.SelectedItems(0).Text
        txtmsoffice.Text = ListView2.SelectedItems(0).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtmsoffice.Select()
    End Sub


    Private Sub txtsearch_TextChanged_1(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_itassetoffice WHERE Office LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY Office ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("Office"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_IT_MS_Office_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub

End Class