Public Class Frm_masterdata_IT_brand
    Dim value_brandID As String
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
        txtbrand.Text = ""
        bttnsave.Text = "SAVE"
        cbotype_tag.SelectedItem = "IT"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_assetbrand ORDER BY Brand ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("BrandID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BrandTag"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_brand As Integer

        If txtbrand.Text = "" Then
            MessageBox.Show("Please Enter Brand")
            txtbrand.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_brand FROM tbl_assetbrand WHERE Brand='" & txtbrand.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_brand = datareader("count_brand")
                End While
            End If
            conn.Close()

            If count_brand = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("Brand Exist")
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
                        ExecuteQuery("INSERT INTO tbl_assetbrand (Brand,BrandTag) VALUES('" & txtbrand.Text.Replace("'", "''") & "','" & cbotype_tag.Text & "')")
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
                        ExecuteQuery("UPDATE tbl_assetbrand SET Brand='" & txtbrand.Text.Replace("'", "''") & "', BrandTag='" & cbotype_tag.Text & "' WHERE BrandID='" & value_brandID & "'")
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
        value_brandID = ListView2.SelectedItems(0).Text
        txtbrand.Text = ListView2.SelectedItems(0).SubItems(1).Text
        cbotype_tag.SelectedItem = ListView2.SelectedItems(0).SubItems(2).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtbrand.Select()
    End Sub


    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_assetbrand WHERE Brand LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY Brand ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("BrandID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BrandTag"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_masterdata_IT_brand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class