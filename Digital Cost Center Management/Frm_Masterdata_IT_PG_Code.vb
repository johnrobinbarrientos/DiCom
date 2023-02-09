Public Class Frm_Masterdata_IT_PG_Code
    Dim value_pgID As String
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
        txtpg_code.Text = ""
        bttnsave.Text = "SAVE"
        cbotype.SelectedItem = "IT"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetpgcode ORDER BY PG_Code ASC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("AssetPG_ID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("PG_Code"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AssetType"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub trap()
        Dim count_pgcode As Integer

        If txtpg_code.Text = "" Then
            MessageBox.Show("Please Enter P&G Code")
            txtpg_code.Select()
        Else

            ExecuteQuery("SELECT COUNT(*) as count_pgcode FROM tbl_itassetpgcode WHERE PG_Code='" & txtpg_code.Text.Replace("'", "''") & "'")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    count_pgcode = datareader("count_pgcode")
                End While
            End If
            conn.Close()

            If count_pgcode = 0 Then
                Call save()
            Else
                If bttnsave.Text = "UPDATE" Then
                    Call save()
                Else
                    MessageBox.Show("P&G Code Exist")
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
                    ExecuteQuery("INSERT INTO tbl_itassetpgcode (PG_Code,AssetType) VALUES('" & txtpg_code.Text.Replace("'", "''") & "','" & cbotype.Text & "')")
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
                    ExecuteQuery("UPDATE tbl_itassetpgcode SET PG_Code='" & txtpg_code.Text.Replace("'", "''") & "', AssetType='" & cbotype.Text & "' WHERE AssetPG_ID='" & value_pgID & "'")
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
        value_pgID = ListView2.SelectedItems(0).Text
        txtpg_code.Text = ListView2.SelectedItems(0).SubItems(1).Text
        cbotype.SelectedItem = ListView2.SelectedItems(0).SubItems(2).Text
        bttnsave.Text = "UPDATE"
    End Sub

    Private Sub bttnnew_Click(sender As Object, e As EventArgs) Handles bttnnew.Click
        Call initialize()
        txtpg_code.Select()
    End Sub


    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        ListView2.Items.Clear()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT * FROM tbl_itassetpgcode WHERE PG_Code LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' ORDER BY PG_Code ASC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("AssetPG_ID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("PG_Code"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AssetType"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub Frm_Masterdata_IT_PG_Code_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
End Class