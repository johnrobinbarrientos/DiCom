
Imports System.ComponentModel
Imports MySql.Data.MySqlClient

Public Class Frm_Employee_Add
    Dim value_person, value_employeeIDupdate As String
    Dim supervisor, StrPath, update_strpath As String
    Dim internet_connection As Boolean

    Private Sub frm_add_employee_Load(sender As Object, e As EventArgs) Handles Me.Load

        value_person = Frm_Employee_Search_Employee.value_person

        If value_person = "" Then
            lbladd_update.Text = "Add Employee"
            Call initialize()
        Else

            Dim branchcode_value, deptid_value, cpcid_value, jobclassid_value, empstatusid_value, employmenttypeid_value, joblevel_value, supid_value, jobtitle_value, areaid_value As Integer

            lbladd_update.Text = "Update Employee"
            bttnsave.Text = "UPDATE"
            value_person = Frm_Employee_Search_Employee.value_person
            ExecuteQuery("SELECT EmpPicPath,tbl_employeemaster.EmployeeID,FName,MName,LName,ExtName,Email,Phone,BloodType,BirthDate,Gender,MaritalStatus,HireDate,EmpInternalID,EmpExternalID,TIN,SSS,PHIC,HDMF,ContactPerson,ContactNumber,Comments,MaritalStatusDetail,Company,Barangay,City,Province,ZipCode,PurokSitio,tbl_employeemaster.JobTitleID,tbl_employeemaster.BranchCode,tbl_employeemaster.DeptID,tbl_employeemaster.CPCID,tbl_employeemaster.JobClassID,tbl_employeemaster.EmpStatusID,tbl_employeemaster.EmploymentTypeID,tbl_employeemaster.JobLevel,tbl_employeemaster.SupID,AreaID FROM tbl_employeemaster " &
                         "LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobclass ON tbl_employeemaster.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeestatus ON tbl_employeemaster.EmpStatusID=tbl_employeestatus.EmpStatusID " &
                         "LEFT JOIN tbl_employeetype ON tbl_employeemaster.EmploymentTypeID=tbl_employeetype.EmploymentTypeID LEFT JOIN tbl_employeelevelofeducation ON tbl_employeemaster.JobLevel=tbl_employeelevelofeducation.LevelOfEducationID LEFT JOIN tbl_employeemasterimages ON tbl_employeemaster.EmployeeID=tbl_employeemasterimages.EmployeeID WHERE tbl_employeemaster.EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader


            If datareader.HasRows Then
                While (datareader.Read)
                    value_employeeIDupdate = datareader("EmployeeID")
                    txtfirst_name.Text = datareader("FName")
                    txtmid_name.Text = datareader("MName")
                    txtlast_name.Text = datareader("LName")
                    txtsuffix.Text = datareader("ExtName")
                    txtemail.Text = datareader("Email")
                    txtcontact_no.Text = datareader("Phone")
                    cboblood_type.Text = datareader("BloodType")
                    birth_date.Text = datareader("BirthDate")
                    cbogender.Text = datareader("Gender")
                    cbomarital_status.Text = datareader("MaritalStatus")
                    date_hired.Text = datareader("HireDate")

                    txtnetsuite_id.Text = datareader("EmpInternalID")
                    txtemployee_id.Text = datareader("EmpExternalID")
                    txttin.Text = datareader("TIN")
                    txtsss_no.Text = datareader("SSS")
                    txtphealth_no.Text = datareader("PHIC")
                    txtpagibig_no.Text = datareader("HDMF")
                    txtcontact_person.Text = datareader("ContactPerson")
                    txtcontactperson_no.Text = datareader("ContactNumber")
                    txtcomment.Text = datareader("Comments")
                    cbostatus_detail.Text = datareader("MaritalStatusDetail").ToString
                    cbocompany.Text = datareader("Company").ToString
                    txtbarangay.Text = datareader("Barangay").ToString
                    txtcity.Text = datareader("City").ToString
                    txtprovince.Text = datareader("Province").ToString
                    txtzip_code.Text = datareader("ZipCode").ToString
                    txtpurok_sitio.Text = datareader("PurokSitio").ToString


                    branchcode_value = datareader("BranchCode")
                    deptid_value = datareader("DeptID")
                    cpcid_value = datareader("CPCID")
                    jobclassid_value = datareader("JobClassID")
                    empstatusid_value = datareader("EmpStatusID")
                    employmenttypeid_value = datareader("EmploymentTypeID")
                    joblevel_value = datareader("JobLevel")
                    supid_value = datareader("SupID")

                    If IsDBNull(datareader("JobTitleID")) Then
                        jobtitle_value = -1
                    Else
                        jobtitle_value = datareader("JobTitleID")
                    End If

                    If IsDBNull(datareader("AreaID")) Then
                        areaid_value = -1
                    Else
                        areaid_value = datareader("AreaID")
                    End If


                    If IsDBNull(datareader("EmpPicPath")) Then
                        PictureBox1.Image = My.Resources.ResourceManager.GetObject("profpicdefault")
                        update_strpath = "null"
                    Else
                        Try
                            PictureBox1.Image = Image.FromFile(datareader("EmpPicPath"))
                            update_strpath = "existing"
                        Catch ex As Exception
                            PictureBox1.Image = My.Resources.ResourceManager.GetObject("profpicdefault")
                            update_strpath = "not existing"
                        End Try
                    End If

                End While
            End If

            conn.Close()




            Dim table_branch As New DataTable
            ExecuteQuery("SELECT * from tbl_distributorbranch")
            datareader = cmd.ExecuteReader
            table_branch.Load(datareader)
            cbobranch.DisplayMember = "Branch"
            cbobranch.ValueMember = "BranchCode"
            cbobranch.DataSource = table_branch
            cbobranch.SelectedValue = branchcode_value
            conn.Close()

            Dim table_area As New DataTable
            Dim row_area As DataRow = table_area.NewRow

            ExecuteQuery("SELECT * from tbl_employeearea")
            datareader = cmd.ExecuteReader
            table_area.Load(datareader)
            cboarea.DisplayMember = "Area"
            cboarea.ValueMember = "AreaID"
            cboarea.DataSource = table_area

            row_area("Area") = ""
            row_area("AreaID") = -1
            table_area.Rows.Add(row_area)

            cboarea.SelectedValue = areaid_value
            conn.Close()



            Dim table_department As New DataTable
            ExecuteQuery("SELECT * from tbl_employeedept")
            datareader = cmd.ExecuteReader
            table_department.Load(datareader)
            cbodepartment.DisplayMember = "Department"
            cbodepartment.ValueMember = "DeptID"
            cbodepartment.DataSource = table_department
            cbodepartment.SelectedValue = deptid_value
            conn.Close()

            Dim table_cpc As New DataTable

            ExecuteQuery("SELECT * from tbl_distributorcpc")
            datareader = cmd.ExecuteReader
            table_cpc.Load(datareader)
            cbocpc.DisplayMember = "CPC"
            cbocpc.ValueMember = "CPCID"
            cbocpc.DataSource = table_cpc
            cbocpc.SelectedValue = cpcid_value
            conn.Close()

            Dim table_jobclass As New DataTable

            ExecuteQuery("SELECT * from tbl_employeejobclass")
            datareader = cmd.ExecuteReader
            table_jobclass.Load(datareader)
            cbojob_class.DisplayMember = "JobClass"
            cbojob_class.ValueMember = "JobClassID"
            cbojob_class.DataSource = table_jobclass
            cbojob_class.SelectedValue = jobclassid_value
            conn.Close()

            Dim table_employmentstatus As New DataTable

            ExecuteQuery("SELECT * from tbl_employeestatus")
            datareader = cmd.ExecuteReader
            table_employmentstatus.Load(datareader)
            cboemployment_status.DisplayMember = "EmployeeStatus"
            cboemployment_status.ValueMember = "EmpStatusID"
            cboemployment_status.DataSource = table_employmentstatus
            cboemployment_status.SelectedValue = empstatusid_value
            conn.Close()

            Dim table_employmenttype As New DataTable

            ExecuteQuery("SELECT * from tbl_employeetype")
            datareader = cmd.ExecuteReader
            table_employmenttype.Load(datareader)
            cboemployment_type.DisplayMember = "EmploymentType"
            cboemployment_type.ValueMember = "EmploymentTypeID"
            cboemployment_type.DataSource = table_employmenttype
            cboemployment_type.SelectedValue = employmenttypeid_value
            conn.Close()

            Dim table_educationalattainment As New DataTable

            ExecuteQuery("SELECT * from tbl_employeelevelofeducation")
            datareader = cmd.ExecuteReader
            table_educationalattainment.Load(datareader)
            cboeducational_attainment.DisplayMember = "LevelofEducation"
            cboeducational_attainment.ValueMember = "LevelofEducationID"
            cboeducational_attainment.DataSource = table_educationalattainment
            cboeducational_attainment.SelectedValue = joblevel_value
            conn.Close()


            Dim table_supervisor As New DataTable


            ExecuteQuery("SELECT CONCAT(tbl_employeemaster.LName,', ',tbl_employeemaster.FName) as fullname,tbl_employeemaster.EmpInternalID,tbl_employeemaster.MName from tbl_employeemaster LEFT JOIN tbl_employeejobclass ON tbl_employeemaster.JobClassID=tbl_employeejobclass.JobClassID WHERE tbl_employeemaster.JobTitle<>'SFA PURPOSES ONLY' ORDER BY tbl_employeemaster.LName ASC")
            datareader = cmd.ExecuteReader
            table_supervisor.Load(datareader)
            cbosupervisor.DisplayMember = "fullname"
            cbosupervisor.ValueMember = "EmpInternalID"
            cbosupervisor.DataSource = table_supervisor
            cbosupervisor.SelectedValue = supid_value
            conn.Close()

            Dim table_jobtitle As New DataTable

            ExecuteQuery("SELECT * from tbl_employeejobtitle")
            datareader = cmd.ExecuteReader
            table_jobtitle.Load(datareader)
            cbojobtitle.DisplayMember = "JobTitle"
            cbojobtitle.ValueMember = "JobTitleID"
            cbojobtitle.DataSource = table_jobtitle
            cbojobtitle.SelectedValue = jobtitle_value
            conn.Close()


            txtnetsuite_id.ReadOnly = True
        End If

    End Sub



    Private Sub initialize()
        cbogender.SelectedItem = "Male"
        cbomarital_status.SelectedItem = "Single"
        cbostatus_detail.SelectedItem = "S"
        cboblood_type.SelectedItem = "A+"
        cbocompany.SelectedItem = "OGDI"
        txtfirst_name.Text = ""
        txtmid_name.Text = ""
        txtlast_name.Text = ""
        txtsuffix.Text = ""
        txtemail.Text = ""
        txtcontact_no.Text = ""
        txtbarangay.Text = ""
        txtcity.Text = ""
        txtprovince.Text = ""
        txtzip_code.Text = ""
        txtpurok_sitio.Text = ""
        txtnetsuite_id.Text = ""
        txtemployee_id.Text = ""
        txttin.Text = ""
        txtsss_no.Text = ""
        txtphealth_no.Text = ""
        txtpagibig_no.Text = ""
        txtcontact_person.Text = ""
        txtcontactperson_no.Text = ""
        txtcomment.Text = ""
        birth_date.Value = Now
        date_hired.Value = Now
        PictureBox1.Image = My.Resources.ResourceManager.GetObject("profpicdefault")
        txtfirst_name.Select()

        Dim table_branch As New DataTable

        ExecuteQuery("SELECT * from tbl_distributorbranch")
        datareader = cmd.ExecuteReader
        table_branch.Load(datareader)
        cbobranch.DisplayMember = "Branch"
        cbobranch.ValueMember = "BranchCode"
        cbobranch.DataSource = table_branch
        conn.Close()

        Dim table_area As New DataTable
        Dim row_area As DataRow = table_area.NewRow

        ExecuteQuery("SELECT * from tbl_employeearea")
        datareader = cmd.ExecuteReader
        table_area.Load(datareader)
        cboarea.DisplayMember = "Area"
        cboarea.ValueMember = "AreaID"
        cboarea.DataSource = table_area
        conn.Close()

        row_area("Area") = ""
        row_area("AreaID") = -1
        table_area.Rows.Add(row_area)
        table_area.DefaultView.Sort = "Area ASC"
        table_area = table_area.DefaultView.ToTable


        Dim table_department As New DataTable

        ExecuteQuery("SELECT * from tbl_employeedept")
        datareader = cmd.ExecuteReader
        table_department.Load(datareader)
        cbodepartment.DisplayMember = "Department"
        cbodepartment.ValueMember = "DeptID"
        cbodepartment.DataSource = table_department
        conn.Close()

        Dim table_cpc As New DataTable

        ExecuteQuery("SELECT * from tbl_distributorcpc")
        datareader = cmd.ExecuteReader
        table_cpc.Load(datareader)
        cbocpc.DisplayMember = "CPC"
        cbocpc.ValueMember = "CPCID"
        cbocpc.DataSource = table_cpc
        conn.Close()

        Dim table_jobclass As New DataTable

        ExecuteQuery("SELECT * from tbl_employeejobclass")
        datareader = cmd.ExecuteReader
        table_jobclass.Load(datareader)
        cbojob_class.DisplayMember = "JobClass"
        cbojob_class.ValueMember = "JobClassID"
        cbojob_class.DataSource = table_jobclass
        conn.Close()

        Dim table_employmentstatus As New DataTable

        ExecuteQuery("SELECT * from tbl_employeestatus")
        datareader = cmd.ExecuteReader
        table_employmentstatus.Load(datareader)
        cboemployment_status.DisplayMember = "EmployeeStatus"
        cboemployment_status.ValueMember = "EmpStatusID"
        cboemployment_status.DataSource = table_employmentstatus
        conn.Close()


        Dim table_employmenttype As New DataTable

        ExecuteQuery("SELECT * from tbl_employeetype")
        datareader = cmd.ExecuteReader
        table_employmenttype.Load(datareader)
        cboemployment_type.DisplayMember = "EmploymentType"
        cboemployment_type.ValueMember = "EmploymentTypeID"
        cboemployment_type.DataSource = table_employmenttype
        conn.Close()


        Dim table_educationalattainment As New DataTable

        ExecuteQuery("SELECT * from tbl_employeelevelofeducation")
        datareader = cmd.ExecuteReader
        table_educationalattainment.Load(datareader)
        cboeducational_attainment.DisplayMember = "LevelofEducation"
        cboeducational_attainment.ValueMember = "LevelofEducationID"
        cboeducational_attainment.DataSource = table_educationalattainment
        conn.Close()

        Dim table_supervisor As New DataTable


        ExecuteQuery("SELECT CONCAT(tbl_employeemaster.LName,', ',tbl_employeemaster.FName) as fullname,tbl_employeemaster.EmpInternalID,tbl_employeemaster.MName from tbl_employeemaster LEFT JOIN tbl_employeejobclass ON tbl_employeemaster.JobClassID=tbl_employeejobclass.JobClassID WHERE (tbl_employeejobclass.JobClass='Supervisor' OR tbl_employeejobclass.JobClass='Managerial') AND (tbl_employeemaster.InActiveID=0 AND tbl_employeemaster.JobTitle<>'SFA PURPOSES ONLY') ORDER BY tbl_employeemaster.LName ASC")
        datareader = cmd.ExecuteReader
        table_supervisor.Load(datareader)
        cbosupervisor.DisplayMember = "fullname"
        cbosupervisor.ValueMember = "EmpInternalID"
        cbosupervisor.DataSource = table_supervisor
        conn.Close()

        Dim table_jobtitle As New DataTable

        ExecuteQuery("SELECT * from tbl_employeejobtitle")
        datareader = cmd.ExecuteReader
        table_jobtitle.Load(datareader)
        cbojobtitle.DisplayMember = "JobTitle"
        cbojobtitle.ValueMember = "JobTitleID"
        cbojobtitle.DataSource = table_jobtitle
        conn.Close()

        txtnetsuite_id.ReadOnly = False
        StrPath = ""
    End Sub

    Private Sub trap()
        Dim num, count_emp, count_emp_internalid As Integer

        If txtfirst_name.Text = "" Then
            MessageBox.Show("Please Enter First Name")
            txtfirst_name.Select()
        ElseIf txtlast_name.Text = "" Then
            MessageBox.Show("Please Enter Last Name")
            txtlast_name.Select()
        ElseIf txtnetsuite_id.Text = "" Then
            MessageBox.Show("Please Enter Netsuite ID")
            txtnetsuite_id.Select()
        ElseIf cboblood_type.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Blood Type")
        ElseIf cbogender.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Gender")
        ElseIf cbomarital_status.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Marital Status")
        ElseIf cbostatus_detail.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Marital Status Detail")
        ElseIf cbocompany.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Company")
        ElseIf cbobranch.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Branch")
        ElseIf cbodepartment.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Department")
        ElseIf cbocpc.SelectedIndex = -1 Then
            MessageBox.Show("Please Select CPC")
        ElseIf cbojob_class.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Job Class")
        ElseIf cboemployment_status.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Employment Status")
        ElseIf cboemployment_type.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Employment Type")
        ElseIf cboeducational_attainment.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Educational Attainment")
        ElseIf cbojobtitle.SelectedIndex = -1 Then
            MessageBox.Show("Please Select Job Title")

        Else

            If Not Integer.TryParse(txtnetsuite_id.Text, num) Then
                MessageBox.Show("Netsuite ID should be number")
                txtnetsuite_id.Select()
            ElseIf Integer.Parse(txtnetsuite_id.Text) < 1000 Then
                MessageBox.Show("Invalid Netsuite ID")
                txtnetsuite_id.Select()
            Else

                'ExecuteQuery("SELECT COUNT(*) as count_employee from tbl_employeemaster WHERE FName='" & txtfirst_name.Text.Replace("'", "''") & "' AND LName='" & txtlast_name.Text.Replace("'", "''") & "'")
                'datareader = cmd.ExecuteReader
                '    If datareader.HasRows Then
                '        While (datareader.Read)
                '            count_emp = datareader("count_employee")
                '        End While
                '    End If
                'conn.Close()


                ExecuteQuery("SELECT COUNT(*) as count_emp_internalid from tbl_employeemaster WHERE EmpInternalID=" & txtnetsuite_id.Text & "")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        count_emp_internalid = datareader("count_emp_internalid")
                    End While
                End If
                conn.Close()



                If count_emp_internalid = 0 Then
                    Call Save()
                Else
                    If bttnsave.Text = "UPDATE" Then
                        Call Save()
                    Else
                        MessageBox.Show("Netsuite ID Exist")
                    End If

                End If


            End If

        End If

    End Sub

    Private Sub Save()

        If bttnsave.Text = "SAVE" Then

            Dim value_employeeID, value_internalID As Integer
            Dim n As String = MsgBox("Add New Record?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then

                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub

                Else

                    ExecuteQuery("INSERT INTO tbl_employeemaster (EmpInternalID,EmpExternalID,FName,MName,ExtName,LName,JobClassID,JobTitleID,SupID,BirthDate,Email,Phone,DistributorCode,BranchCode,DeptID,CPCID,EmploymentTypeID,EmpStatusID,Joblevel,HireDate,InActiveID,Comments,Gender,MaritalStatus,BloodType,SSS,HDMF,PHIC,TIN,ContactPerson,ContactNumber,MaritalStatusDetail,Company,Barangay,City,Province,ZipCode,PurokSitio,SubsegmentID,IsSalesRep,IsSupervisor,SFAID,BTDTID,AreaID) " &
                                 "VALUES(" & txtnetsuite_id.Text & ",'" & txtemployee_id.Text.Replace("'", "''") & "','" & txtfirst_name.Text.Replace("'", "''") & "','" & txtmid_name.Text.Replace("'", "''") & "','" & txtsuffix.Text.Replace("'", "''") & "','" & txtlast_name.Text.Replace("'", "''") & "'," & cbojob_class.SelectedValue & ",'" & cbojobtitle.SelectedValue & "'," & cbosupervisor.SelectedValue & ",'" & birth_date.Text & "'" &
                                 ",'" & txtemail.Text.Replace("'", "''") & "','" & txtcontact_no.Text.Replace("'", "''") & "',8," & cbobranch.SelectedValue & "," & cbodepartment.SelectedValue & "," & cbocpc.SelectedValue & "," & cboemployment_type.SelectedValue & "," & cboemployment_status.SelectedValue & "," & cboeducational_attainment.SelectedValue & ",'" & date_hired.Text & "',0,'" & txtcomment.Text.Replace("'", "''") & "','" & cbogender.Text & "','" & cbomarital_status.Text & "'" &
                                 ",'" & cboblood_type.Text & "','" & txtsss_no.Text.Replace("'", "''") & "','" & txtpagibig_no.Text.Replace("'", "''") & "','" & txtphealth_no.Text.Replace("'", "''") & "','" & txttin.Text.Replace("'", "''") & "','" & txtcontact_person.Text.Replace("'", "''") & "','" & txtcontactperson_no.Text.Replace("'", "''") & "','" & cbostatus_detail.Text & "','" & cbocompany.Text & "','" & txtbarangay.Text.Replace("'", "''") & "','" & txtcity.Text.Replace("'", "''") & "','" & txtprovince.Text.Replace("'", "''") & "','" & txtzip_code.Text.Replace("'", "''") & "','" & txtpurok_sitio.Text.Replace("'", "''") & "',25,0,0,0,0," & cboarea.SelectedValue & ")")
                    conn.Close()
                    MessageBox.Show("Successfully Saved!!")



                    ExecuteQuery("SELECT EmployeeID,EmpInternalID FROM tbl_employeemaster ORDER BY EmployeeID DESC LIMIT 1")
                    datareader = cmd.ExecuteReader
                    If datareader.HasRows Then
                        While (datareader.Read)
                            value_internalID = datareader("EmpInternalID")
                            value_employeeID = datareader("EmployeeID")
                        End While
                    End If
                    conn.Close()

                    If StrPath <> "" Then
                        ExecuteQuery("INSERT INTO tbl_employeemasterimages (EmployeeID,EmpPicPath) VALUES('" & value_employeeID & "','" & StrPath.Replace("\", "\\") & "')")
                        conn.Close()
                    End If


                    ExecuteQuery("INSERT INTO tbl_employeemastertransaction (EmployeeIDUser,Transaction,EmployeeID,EmpInternalID,DateTransaction) VALUES('" & Login.userID & "','ADD','" & value_employeeID & "','" & value_internalID & "','" & Format(Now, "yyyy/MM/dd hh:mm") & "')")
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

                    If cboarea.SelectedValue = -1 Then
                        conn.Open()
                        cmd.CommandText = "UPDATE tbl_employeemaster SET FName='" & txtfirst_name.Text.Replace("'", "''") & "', MName='" & txtmid_name.Text.Replace("'", "''") & "', LName='" & txtlast_name.Text.Replace("'", "''") & "', ExtName='" & txtsuffix.Text.Replace("'", "''") & "', Email='" & txtemail.Text.Replace("'", "''") & "', Phone='" & txtcontact_no.Text.Replace("'", "''") & "', BloodType='" & cboblood_type.Text & "', BirthDate='" & birth_date.Text & "', Gender='" & cbogender.Text & "', MaritalStatus='" & cbomarital_status.Text & "', MaritalStatusDetail='" & cbostatus_detail.Text & "', Company='" & cbocompany.Text & "'" &
                                         ", Barangay='" & txtbarangay.Text.Replace("'", "''") & "', City='" & txtcity.Text.Replace("'", "''") & "', Province='" & txtprovince.Text.Replace("'", "''") & "', ZipCode='" & txtzip_code.Text.Replace("'", "''") & "', PurokSitio='" & txtpurok_sitio.Text.Replace("'", "''") & "', HireDate='" & date_hired.Text & "', BranchCode='" & cbobranch.SelectedValue & "', DeptID='" & cbodepartment.SelectedValue & "', CPCID='" & cbocpc.SelectedValue & "', JobClassID='" & cbojob_class.SelectedValue & "', JobTitleID='" & cbojobtitle.SelectedValue & "'" &
                                         ", EmpStatusID='" & cboemployment_status.SelectedValue & "', EmploymentTypeID='" & cboemployment_type.SelectedValue & "', JobLevel='" & cboeducational_attainment.SelectedValue & "', EmpInternalID='" & txtnetsuite_id.Text & "', EmpExternalID='" & txtemployee_id.Text.Replace("'", "''") & "', TIN='" & txttin.Text.Replace("'", "''") & "', SSS='" & txtsss_no.Text.Replace("'", "''") & "', PHIC='" & txtphealth_no.Text.Replace("'", "''") & "', HDMF='" & txtpagibig_no.Text.Replace("'", "''") & "', ContactPerson='" & txtcontact_person.Text.Replace("'", "''") & "', ContactNumber='" & txtcontactperson_no.Text.Replace("'", "''") & "', Comments='" & txtcomment.Text.Replace("'", "''") & "', SupID='" & cbosupervisor.SelectedValue & "',AreaID=NULL WHERE EmployeeID='" & value_person & "'"
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Successfuly Updated")
                        conn.Close()
                    Else
                        conn.Open()
                        cmd.CommandText = "UPDATE tbl_employeemaster SET FName='" & txtfirst_name.Text.Replace("'", "''") & "', MName='" & txtmid_name.Text.Replace("'", "''") & "', LName='" & txtlast_name.Text.Replace("'", "''") & "', ExtName='" & txtsuffix.Text.Replace("'", "''") & "', Email='" & txtemail.Text.Replace("'", "''") & "', Phone='" & txtcontact_no.Text.Replace("'", "''") & "', BloodType='" & cboblood_type.Text & "', BirthDate='" & birth_date.Text & "', Gender='" & cbogender.Text & "', MaritalStatus='" & cbomarital_status.Text & "', MaritalStatusDetail='" & cbostatus_detail.Text & "', Company='" & cbocompany.Text & "'" &
                                         ", Barangay='" & txtbarangay.Text.Replace("'", "''") & "', City='" & txtcity.Text.Replace("'", "''") & "', Province='" & txtprovince.Text.Replace("'", "''") & "', ZipCode='" & txtzip_code.Text.Replace("'", "''") & "', PurokSitio='" & txtpurok_sitio.Text.Replace("'", "''") & "', HireDate='" & date_hired.Text & "', BranchCode='" & cbobranch.SelectedValue & "', DeptID='" & cbodepartment.SelectedValue & "', CPCID='" & cbocpc.SelectedValue & "', JobClassID='" & cbojob_class.SelectedValue & "', JobTitleID='" & cbojobtitle.SelectedValue & "'" &
                                         ", EmpStatusID='" & cboemployment_status.SelectedValue & "', EmploymentTypeID='" & cboemployment_type.SelectedValue & "', JobLevel='" & cboeducational_attainment.SelectedValue & "', EmpInternalID='" & txtnetsuite_id.Text & "', EmpExternalID='" & txtemployee_id.Text.Replace("'", "''") & "', TIN='" & txttin.Text.Replace("'", "''") & "', SSS='" & txtsss_no.Text.Replace("'", "''") & "', PHIC='" & txtphealth_no.Text.Replace("'", "''") & "', HDMF='" & txtpagibig_no.Text.Replace("'", "''") & "', ContactPerson='" & txtcontact_person.Text.Replace("'", "''") & "', ContactNumber='" & txtcontactperson_no.Text.Replace("'", "''") & "', Comments='" & txtcomment.Text.Replace("'", "''") & "', SupID='" & cbosupervisor.SelectedValue & "',AreaID='" & cboarea.SelectedValue & "' WHERE EmployeeID='" & value_person & "'"
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Successfuly Updated")
                        conn.Close()
                    End If


                    If StrPath <> "" Then
                        If update_strpath = "null" Then
                            ExecuteQuery("INSERT INTO tbl_employeemasterimages (EmployeeID,EmpPicPath) VALUES('" & value_employeeIDupdate & "','" & StrPath.Replace("\", "\\") & "')")
                            conn.Close()
                        Else
                            ExecuteQuery("UPDATE tbl_employeemasterimages SET EmployeeID='" & value_employeeIDupdate & "', EmpPicPath='" & StrPath.Replace("\", "\\") & "'")
                            conn.Close()
                        End If

                    End If


                    ExecuteQuery("INSERT INTO tbl_employeemastertransaction (EmployeeIDUser,Transaction,EmployeeID,EmpInternalID,DateTransaction) VALUES('" & Login.userID & "','UPDATE','" & value_employeeIDupdate & "','" & txtnetsuite_id.Text & "','" & Format(Now, "yyyy/MM/dd hh:mm") & "')")
                    conn.Close()
                    Frm_employee_profile.MdiParent = Me.MdiParent
                    Frm_employee_profile.StartPosition = FormStartPosition.CenterScreen
                    Frm_employee_profile.Show()
                    Me.Close()
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

    Private Sub frm_add_employee_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        conn.Close()
    End Sub

    Private Sub lblclose_MouseHover(sender As Object, e As EventArgs) Handles lblclose.MouseHover
        lblclose.Visible = False
        lblclose2.Visible = True
    End Sub

    Private Sub lblclose2_Click(sender As Object, e As EventArgs) Handles lblclose2.Click
        If bttnsave.Text = "UPDATE" Then
            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                Frm_employee_profile.MdiParent = Me.MdiParent
                Frm_employee_profile.StartPosition = FormStartPosition.CenterScreen
                Frm_employee_profile.Show()
                Me.Close()
            End If

        Else
            Me.Close()
        End If

    End Sub

    Private Sub lblclose2_MouseLeave(sender As Object, e As EventArgs) Handles lblclose2.MouseLeave
        lblclose.Visible = True
        lblclose2.Visible = False
    End Sub

    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        If bttnsave.Text = "UPDATE" Then
            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                Frm_employee_profile.MdiParent = Me.MdiParent
                Frm_employee_profile.StartPosition = FormStartPosition.CenterScreen
                Frm_employee_profile.Show()
                Me.Close()
            End If

        Else
            Me.Close()
        End If
    End Sub


    Private Sub txtfirst_name_LostFocus(sender As Object, e As EventArgs) Handles txtfirst_name.LostFocus
        txtfirst_name.Text = txtfirst_name.Text.ToUpper()
    End Sub

    Private Sub txtmid_name_LostFocus(sender As Object, e As EventArgs) Handles txtmid_name.LostFocus
        txtmid_name.Text = txtmid_name.Text.ToUpper()
    End Sub

    Private Sub txtlast_name_LostFocus(sender As Object, e As EventArgs) Handles txtlast_name.LostFocus
        txtlast_name.Text = txtlast_name.Text.ToUpper()
    End Sub

    Private Sub txtsuffix_LostFocus(sender As Object, e As EventArgs) Handles txtsuffix.LostFocus
        txtsuffix.Text = txtsuffix.Text.ToUpper()
    End Sub

    Private Sub bttnbrowse_Click(sender As Object, e As EventArgs) Handles bttnbrowse.Click
        Dim opf As New OpenFileDialog

        opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif"

        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(opf.FileName)
            StrPath = opf.FileName.ToString
        End If

        'MessageBox.Show("Profile picture is under construction. Please wait for an update.")
    End Sub

    Private Sub txtemployee_id_LostFocus(sender As Object, e As EventArgs) Handles txtemployee_id.LostFocus
        txtemployee_id.Text = txtemployee_id.Text.ToUpper()
    End Sub

    Private Sub txtnetsuite_id_Click(sender As Object, e As EventArgs) Handles txtnetsuite_id.Click
        If bttnsave.Text = "UPDATE" Then
            MessageBox.Show("Please Contact IDS for Nestsuite ID updating")
        End If
    End Sub

End Class
