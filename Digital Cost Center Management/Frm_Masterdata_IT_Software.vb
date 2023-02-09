Public Class Frm_Masterdata_IT_Software
    Dim value_softwareID As String
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
        txtsoftware.Text = ""
        bttnsave.Text = "SAVE"
        txtsearch.Text = ""

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetsoftware ORDER BY Software ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("SoftwareID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Software"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_software As Integer

        If txtsoftware.Text = "" Then
            MessageBox.Show("Please Enter Software")
            txtsoftware.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_software FROM tbl_itassetsoftware WHERE Software='" & txtsoftware.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_software = datareader("count_software")
                End While
            End If
            conn.Close()

            If count_software = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Software Exist")
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
                    ExecuteQuery("INSERT INTO tbl_itassetsoftware (Software) VALUES('" & txtsoftware.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_itassetsoftware SET Software='" & txtsoftware.Text.Replace("'", "''") & "' WHERE SoftwareID='" & value_softwareID & "'")
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
        value_softwareID = ListView2.SelectedItems(0).Text
        txtsoftware.Text = ListView2.SelectedItems(0).SubItems(1).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtsoftware.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_itassetsoftware WHERE Software LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY Software ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("SoftwareID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Software"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_IT_Software_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub

End Class