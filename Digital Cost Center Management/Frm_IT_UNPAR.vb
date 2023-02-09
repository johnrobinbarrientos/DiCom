Public Class Frm_IT_UNPAR
    Dim userinternalID, seqno, qtty, itprice As Integer
    Dim value_assetid, value_person, supervisor, value_hr, parunparID As String
    Dim internet_connection As Boolean

    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        Me.Close()
    End Sub

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

    Private Sub Frm_unpar_IT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub
    Private Sub initialize()
        Dim PARinternal_id As String

        userinternalID = Login.userinternalID
        value_assetid = Frm_IT_PAR.value_assetid
        lblassetid.Text = value_assetid

        PARinternal_id = ""

        ExecuteQuery("SELECT PAREmpInternalID,Qty,Price FROM tbl_itassetmaster WHERE AssetID=" & value_assetid & "")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                PARinternal_id = datareader("PAREmpInternalID")
                qtty = datareader("Qty") * -1
                itprice = datareader("Price") * -1
            End While
        End If
        conn.Close()

        ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.SupID,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmpInternalID ='" & PARinternal_id & "'")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)


                txtfirst_name.Text = datareader("FName")
                txtmiddle_name.Text = datareader("MName")
                txtlast_name.Text = datareader("LName")

                txtbranch.Text = datareader("Branch")
                txtdepartment.Text = datareader("Department")

                txtcpc.Text = datareader("CPC")

                If IsDBNull(datareader("JobTitle")) Then
                    txtjob_title.Text = ""
                Else
                    txtjob_title.Text = datareader("JobTitle")
                End If
                value_person = datareader("EmpInternalID")

            End While
        End If
        conn.Close()

        ExecuteQuery("SELECT ReceivedByHRAdmin,ApprovedBy FROM tbl_itassetpar WHERE AssetID=" & value_assetid & " ORDER BY PARDate DESC")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                value_hr = datareader("ReceivedByHRAdmin")
                supervisor = datareader("ApprovedBy")
            End While
        End If
        conn.Close()

        ExecuteQuery("SELECT SeqNo FROM tbl_itassetpar ORDER BY SeqNo DESC")
        seqno = cmd.ExecuteScalar
        conn.Close()

        parunparID = Format(Now, "MM") + Format(Now, "dd") + Format(Now, "yy") + Format(Now, "HH") + Format(Now, "mm") + CStr(seqno + 1)


    End Sub

    Private Sub bttnUnPAR_Click(sender As Object, e As EventArgs) Handles bttnUnPAR.Click
        Dim n As String = MsgBox("UnPAR Employee?", MsgBoxStyle.YesNo, "")

        If n = vbYes Then

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then

                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else

                ExecuteQuery("INSERT INTO tbl_itassetpar (PARNo,PARDate,PARType,PAREmpInternalID,AssetID,Qty,Amount,ApprovedBy,ReceivedByHRAdmin,DateProcessed,ProcessedBy,DateUpdated,UpdatedBy,Remarks) VALUES('" & "UNP_" + parunparID & "','" & Format(Now, "yyyy-MM-dd") & "','UNPAR'," & value_person & "," & value_assetid & "," & qtty & "," & itprice & "," & supervisor & "," & value_hr & ",'" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "'," & userinternalID & ",'" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "'," & userinternalID & ",'" & txtremarks.Text.Replace("'", "''") & "')")
                conn.Close()

                ExecuteQuery("UPDATE tbl_itassetmaster SET PAREmpInternalID=NULL WHERE AssetID=" & value_assetid & "")
                conn.Close()

                MessageBox.Show("Successfuly UnPAR")
                Call Frm_IT_PAR.initialize()
                Me.Close()
            End If

        End If
    End Sub


End Class