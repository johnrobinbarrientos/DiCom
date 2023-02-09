Public Class Frm_Masterdata_IT_OS
    Dim value_os As String
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
        txtos.Text = ""
        bttnsave.Text = "SAVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetos ORDER BY OperatingSytem ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("OperatingSytem"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_os As Integer

        If txtos.Text = "" Then
            MessageBox.Show("Please Enter OS")
            txtos.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_os FROM tbl_itassetos WHERE OperatingSytem='" & txtos.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_os = datareader("count_os")
                End While
            End If
            conn.Close()

            If count_os = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("OS Exist")
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
                        ExecuteQuery("INSERT INTO tbl_itassetos (OperatingSytem) VALUES('" & txtos.Text.Replace("'", "''") & "')")
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
                        ExecuteQuery("UPDATE tbl_itassetos SET OperatingSytem='" & txtos.Text.Replace("'", "''") & "' WHERE OperatingSytem='" & value_os & "'")
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
        value_os = ListView2.SelectedItems(0).Text
        txtos.Text = ListView2.SelectedItems(0).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtos.Select()
    End Sub


    Private Sub txtsearch_TextChanged_1(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_itassetos WHERE OperatingSytem LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY OperatingSytem ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("OperatingSytem"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_IT_OS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class