Public Class Frm_Masterdata_IT_Software_Optional
    Dim value_softwareoptionalID As String
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
        txtsoftware_optional.Text = ""
        bttnsave.Text = "SAVE"
        txtsearch.Text = ""

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetsoftwareoptional ORDER BY SoftwareOptional ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("SoftwareOptionalID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("SoftwareOptional"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_software_optional As Integer

        If txtsoftware_optional.Text = "" Then
            MessageBox.Show("Please Enter Software Optional")
            txtsoftware_optional.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_software_optional FROM tbl_itassetsoftwareoptional WHERE SoftwareOptional='" & txtsoftware_optional.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_software_optional = datareader("count_software_optional")
                End While
            End If
            conn.Close()

            If count_software_optional = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Software Optional Exist")
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
                    ExecuteQuery("INSERT INTO tbl_itassetsoftwareoptional (SoftwareOptional) VALUES('" & txtsoftware_optional.Text.Replace("'", "''") & "')")
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
                    ExecuteQuery("UPDATE tbl_itassetsoftwareoptional SET SoftwareOptional='" & txtsoftware_optional.Text.Replace("'", "''") & "' WHERE SoftwareOptionalID='" & value_softwareoptionalID & "'")
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
        value_softwareoptionalID = ListView2.SelectedItems(0).Text
        txtsoftware_optional.Text = ListView2.SelectedItems(0).SubItems(1).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtsoftware_optional.Select()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_itassetsoftwareoptional WHERE SoftwareOptional LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY SoftwareOptional ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("SoftwareOptionalID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("SoftwareOptional"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_IT_Software_Optional_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class