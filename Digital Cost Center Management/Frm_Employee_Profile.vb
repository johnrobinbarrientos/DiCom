Public Class Frm_employee_profile
    Dim value_person, supervisor As String
    Public value_previouspositionID, value_awardsID, value_seminarID, value_separationID, value_educbackgroundID, value_emphistoryID As String
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

    Private Sub Frm_employee_profile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call initialize()
    End Sub

    Public Sub initialize()
        Dim date_hired As DateTime
        Dim tenure_year, tenure_month, tenure As Integer
        'Dim arrImage() As Byte
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            value_person = Frm_Employee_Search_Employee.value_person
            ExecuteQuery("SELECT EmpPicPath,FName,MName,LName,ExtName,Email,Phone,BloodType,BirthDate,Gender,MaritalStatus,HireDate,tbl_employeejobtitle.JobTitle,EmpInternalID,EmpExternalID,TIN,SSS,PHIC,HDMF,ContactPerson,ContactNumber,Comments,InActiveID,Branch,Department,CPC,JobClass,EmployeeStatus,EmploymentType,LevelOfEducation,MaritalStatusDetail,Company,Barangay,City,Province,ZipCode,PurokSitio,Area FROM tbl_employeemaster " &
                             "LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobclass ON tbl_employeemaster.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeestatus ON tbl_employeemaster.EmpStatusID=tbl_employeestatus.EmpStatusID LEFT JOIN tbl_employeetype ON tbl_employeemaster.EmploymentTypeID=tbl_employeetype.EmploymentTypeID " &
                             "LEFT JOIN tbl_employeelevelofeducation ON tbl_employeemaster.JobLevel=tbl_employeelevelofeducation.LevelOfEducationID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemasterimages ON tbl_employeemaster.EmployeeID=tbl_employeemasterimages.EmployeeID LEFT JOIN tbl_employeearea ON tbl_employeemaster.AreaID=tbl_employeearea.AreaID WHERE tbl_employeemaster.EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                txtfirst_name.Text = datareader("FName")
                txtmid_name.Text = datareader("MName")
                txtlast_name.Text = datareader("LName")
                txtsuffix.Text = datareader("ExtName")
                txtemail.Text = datareader("Email")
                txtcontact_no.Text = datareader("Phone")
                txtblood_type.Text = datareader("BloodType")
                If datareader("BirthDate") = Nothing Then
                    txtbirth_date.Text = ""
                Else
                    txtbirth_date.Text = datareader("BirthDate")
                End If


                txtgender.Text = datareader("Gender")
                txtmarital_status.Text = datareader("MaritalStatus")
                If datareader("HireDate") = Nothing Then
                    txtdate_hired.Text = ""
                Else
                    txtdate_hired.Text = datareader("HireDate")
                End If

                'If IsDBNull(datareader("EmpPic")) Then
                '    PictureBox1.Image = My.Resources.ResourceManager.GetObject("profpicdefault")
                'Else
                '    arrImage = datareader("EmpPic")
                '    Dim lstr As New System.IO.MemoryStream(arrImage)
                'End If


                If IsDBNull(datareader("EmpPicPath")) Then
                    PictureBox1.Image = My.Resources.ResourceManager.GetObject("profpicdefault")
                Else
                    Try
                        PictureBox1.Image = Image.FromFile(datareader("EmpPicPath"))
                    Catch ex As Exception
                        PictureBox1.Image = My.Resources.ResourceManager.GetObject("profpicdefault")
                    End Try
                End If

                If IsDBNull(datareader("JobTitle")) Then
                    txtjob_title.Text = ""
                Else
                    txtjob_title.Text = datareader("JobTitle")
                End If

                If IsDBNull(datareader("Area")) Then
                    txtarea.Text = ""
                Else
                    txtarea.Text = datareader("Area")
                End If

                txtnetsuite_id.Text = datareader("EmpInternalID")
                txtemployee_id.Text = datareader("EmpExternalID")
                txttin.Text = datareader("TIN")
                txtsss_no.Text = datareader("SSS")
                txtphealth_no.Text = datareader("PHIC")
                txtpagibig_no.Text = datareader("HDMF")
                txtcontact_person.Text = datareader("ContactPerson")
                txtcontactperson_no.Text = datareader("ContactNumber")
                txtcomment.Text = datareader("Comments")
                txtmarital_status_detail.Text = datareader("MaritalStatusDetail").ToString
                txtcompany.Text = datareader("Company").ToString
                txtbarangay.Text = datareader("Barangay").ToString
                txtcity.Text = datareader("City").ToString
                txtprovince.Text = datareader("Province").ToString
                txtzip_code.Text = datareader("ZipCode").ToString
                txtpurok.Text = datareader("PurokSitio").ToString


                txtbranch.Text = datareader("Branch")
                txtdepartment.Text = datareader("Department")
                txtcpc.Text = datareader("CPC")

                txtjob_class.Text = datareader("JobClass")
                txtemp_status.Text = datareader("EmployeeStatus")
                txtemp_type.Text = datareader("EmploymentType")
                txteduc_attainment.Text = datareader("LevelOfEducation")

                If datareader("InActiveID") = 0 Then
                    lblactive.Text = "Active"
                    lblactive.ForeColor = Color.LimeGreen
                    bttnset_status.Image = My.Resources.ResourceManager.GetObject("icons8-inactive-state-48")
                Else
                    lblactive.Text = "Separated"
                    lblactive.ForeColor = Color.Orange
                    bttnset_status.Image = My.Resources.ResourceManager.GetObject("icons8-active-state-48")
                End If

            End While
        End If

        conn.Close()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            supervisor = Frm_Employee_Search_Employee.supervisor
            ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & supervisor & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                txtsupervisor.Text = datareader("fullname")

            End While
        End If
        conn.Close()

        If lblactive.Text = "Active" Then
            If txtdate_hired.Text = "" Then
                lbltenure.Text = ""
            Else
                date_hired = txtdate_hired.Text
                tenure = DateDiff(DateInterval.Year, date_hired, Now)
                tenure_year = Int(DateDiff(DateInterval.Month, date_hired, Now) / 12)
                tenure_month = DateDiff(DateInterval.Month, date_hired, Now) Mod 12
                lbltenure.Text = CStr(tenure)

                If tenure < 1 Then
                    tenure = DateDiff(DateInterval.Month, date_hired, Now)
                    If tenure = 1 Then
                        lbltenure.Text = CStr(tenure) + " month"
                    ElseIf tenure < 1 Then
                        tenure = DateDiff(DateInterval.Day, date_hired, Now)
                        lbltenure.Text = CStr(tenure) + " day/s"
                    ElseIf tenure > 1 Then
                        lbltenure.Text = CStr(tenure) + " months"
                    End If

                Else

                    If tenure_year = 1 And tenure_month = 0 Then
                        lbltenure.Text = CStr(tenure_year) + " year"
                    ElseIf tenure_year = 1 And tenure_month = 1 Then
                        lbltenure.Text = CStr(tenure_year) + " year" + " And " + CStr(tenure_month) + " month"
                    ElseIf tenure_year = 1 And tenure_month > 1 Then
                        lbltenure.Text = CStr(tenure_year) + " year" + " And " + CStr(tenure_month) + " months"
                    ElseIf tenure_year > 1 And tenure_month = 1 Then
                        lbltenure.Text = CStr(tenure_year) + " years" + " And " + CStr(tenure_month) + " month"
                    Else
                        lbltenure.Text = CStr(tenure_year) + " years" + " And " + CStr(tenure_month) + " months"
                    End If

                End If
            End If

        Else
            lbltenure.Text = ""
        End If

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            ListView3.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_employeepreviousposition LEFT JOIN tbl_distributorcpc ON tbl_employeepreviousposition.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeepreviousposition.JobTitleID=tbl_employeejobtitle.JobTitleID  WHERE EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If


        If datareader.HasRows Then
            While (datareader.Read)
                ListView3.Items.Add(datareader("DateHired"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("CPC"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("PreviousPositionID"))
            End While
        End If
        conn.Close()


        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            ListView2.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_employeenewawards WHERE EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("Awards"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("DateGiven"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("GivenBy"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpAwardsID"))
            End While
        End If
        conn.Close()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            ListView1.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_employeenewseminar WHERE EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("Seminar"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("DateConducted"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("ConductedBy"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("EmpSeminarID"))
            End While
        End If
        conn.Close()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            ListView4.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_employeeseparationdetails LEFT JOIN tbl_employeejobtitle ON tbl_employeeseparationdetails.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView4.Items.Add(datareader("ReasonForSeparation"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("TypeOfSeparation"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("DateOfSeparation"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("DateHired"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("SeparationDetail_ID"))

            End While
        End If
        conn.Close()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            ListView5.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_employeeeducationalbackground WHERE EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView5.Items.Add(datareader("NameOfInstitution"))
                ListView5.Items(ListView5.Items.Count - 1).SubItems.Add(datareader("YearStarted"))
                ListView5.Items(ListView5.Items.Count - 1).SubItems.Add(datareader("YearEnded"))
                ListView5.Items(ListView5.Items.Count - 1).SubItems.Add(datareader("Address"))
                ListView5.Items(ListView5.Items.Count - 1).SubItems.Add(datareader("FieldOfStudy"))
                ListView5.Items(ListView5.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView5.Items(ListView5.Items.Count - 1).SubItems.Add(datareader("Educ_backgroundID"))

            End While
        End If
        conn.Close()

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
            Me.Close()
        Else
            ListView6.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_employeeemploymenthistory WHERE EmployeeID='" & value_person & "'")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView6.Items.Add(datareader("Company"))
                ListView6.Items(ListView6.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                ListView6.Items(ListView6.Items.Count - 1).SubItems.Add(datareader("YearStarted"))
                ListView6.Items(ListView6.Items.Count - 1).SubItems.Add(datareader("YearEnded"))
                ListView6.Items(ListView6.Items.Count - 1).SubItems.Add(datareader("CompanyAddress"))
                ListView6.Items(ListView6.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView6.Items(ListView6.Items.Count - 1).SubItems.Add(datareader("EmploymentHistoryID"))

            End While
        End If
        conn.Close()
    End Sub

    Private Sub bttn_update_employee_Click(sender As Object, e As EventArgs) Handles bttn_update_employee.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            Frm_Employee_Add.MdiParent = Me.MdiParent
            Frm_Employee_Add.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Add.Show()
            Me.Close()
        End If

    End Sub

    Private Sub bttnset_status_Click(sender As Object, e As EventArgs) Handles bttnset_status.Click
        If lblactive.Text = "Active" Then
            Dim n As String = MsgBox("Set Employee as Separated?", MsgBoxStyle.YesNo, "")
            If n = vbYes Then
                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("UPDATE tbl_employeemaster SET InActiveID=1 WHERE EmployeeID='" & value_person & "'")
                    datareader = cmd.ExecuteReader
                    conn.Close()
                    Frm_employee_profile_Load(e, e)
                End If

            End If
        Else

            Dim n As String = MsgBox("Set Employee as Active?", MsgBoxStyle.YesNo, "")
            If n = vbYes Then
                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then
                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub
                Else
                    ExecuteQuery("UPDATE tbl_employeemaster SET InActiveID=0 WHERE EmployeeID='" & value_person & "'")
                    datareader = cmd.ExecuteReader
                    conn.Close()
                    Frm_employee_profile_Load(e, e)
                End If

            End If

        End If

    End Sub

    Private Sub bttn_awards_Click(sender As Object, e As EventArgs) Handles bttn_awards.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_awardsID = ""
            Frm_Employee_Awards.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Awards.ShowDialog()
        End If

    End Sub

    Private Sub bttn_seminar_Click(sender As Object, e As EventArgs) Handles bttn_seminar.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_seminarID = ""
            Frm_Employee_Seminar.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Seminar.ShowDialog()
        End If

    End Sub

    Private Sub bttn_separation_details_Click(sender As Object, e As EventArgs) Handles bttn_separation_details.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_separationID = ""
            Frm_Employee_Separation_Detail.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Separation_Detail.ShowDialog()
        End If


    End Sub

    Private Sub bttn_previous_position_Click(sender As Object, e As EventArgs) Handles bttn_previous_position.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_previouspositionID = ""
            Frm_Employee_previous_position.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_previous_position.ShowDialog()
        End If

    End Sub

    Private Sub bttn_educational_background_Click(sender As Object, e As EventArgs) Handles bttn_educational_background.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_educbackgroundID = ""
            Frm_Employee_Educational_Background.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Educational_Background.ShowDialog()
        End If

    End Sub

    Private Sub bttn_employment_history_Click(sender As Object, e As EventArgs) Handles bttn_employment_history.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_emphistoryID = ""
            Frm_Employee_Employment_History.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Employment_History.ShowDialog()
        End If

    End Sub

    Private Sub ListView3_DoubleClick(sender As Object, e As EventArgs) Handles ListView3.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_previouspositionID = ListView3.SelectedItems(0).SubItems(4).Text
            Frm_Employee_previous_position.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_previous_position.ShowDialog()
        End If

    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection
        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_awardsID = ListView2.SelectedItems(0).SubItems(4).Text
            Frm_Employee_Awards.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Awards.ShowDialog()
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection
        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_seminarID = ListView1.SelectedItems(0).SubItems(4).Text
            Frm_Employee_Seminar.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Seminar.ShowDialog()
        End If
    End Sub

    Private Sub ListView4_DoubleClick(sender As Object, e As EventArgs) Handles ListView4.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection
        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_separationID = ListView4.SelectedItems(0).SubItems(6).Text
            Frm_Employee_Separation_Detail.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Separation_Detail.ShowDialog()
        End If

    End Sub

    Private Sub ListView5_DoubleClick(sender As Object, e As EventArgs) Handles ListView5.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_educbackgroundID = ListView5.SelectedItems(0).SubItems(6).Text
            Frm_Employee_Educational_Background.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Educational_Background.ShowDialog()
        End If

    End Sub

    Private Sub ListView6_DoubleClick(sender As Object, e As EventArgs) Handles ListView6.DoubleClick
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            value_emphistoryID = ListView6.SelectedItems(0).SubItems(6).Text
            Frm_Employee_Employment_History.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Employment_History.ShowDialog()
        End If

    End Sub
End Class