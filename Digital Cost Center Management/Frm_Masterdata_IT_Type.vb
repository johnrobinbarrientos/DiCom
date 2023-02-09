Public Class Frm_Masterdata_IT_Type
    Dim value_typeID As String
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
        txttype.Text = ""
        bttnsave.Text = "SAVE"
        cbotype_tag.SelectedItem = "IT"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_assettype ORDER BY Type ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("TypeID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Type"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TypeTag"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_type As Integer

        If txttype.Text = "" Then
            MessageBox.Show("Please Enter Type")
            txttype.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_type FROM tbl_assettype WHERE Type='" & txttype.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_type = datareader("count_type")
                End While
            End If
            conn.Close()

            If count_type = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Type Exist")
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
                        ExecuteQuery("INSERT INTO tbl_assettype (Type,TypeTag) VALUES('" & txttype.Text.Replace("'", "''") & "','" & cbotype_tag.Text & "')")
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
                        ExecuteQuery("UPDATE tbl_assettype SET Type='" & txttype.Text.Replace("'", "''") & "', TypeTag='" & cbotype_tag.Text & "' WHERE TypeID='" & value_typeID & "'")
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
        value_typeID = ListView2.SelectedItems(0).Text
        txttype.Text = ListView2.SelectedItems(0).SubItems(1).Text
        cbotype_tag.SelectedItem = ListView2.SelectedItems(0).SubItems(2).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txttype.Select()
    End Sub


    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_assettype WHERE Type LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY Type ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("TypeID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Type"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TypeTag"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_IT_Type_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class