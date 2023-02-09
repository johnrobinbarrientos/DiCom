Imports Excel = Microsoft.Office.Interop.Excel
Public Class Frm_report_employee
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

    Private Sub Frm_report_employee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim table_branch As New DataTable
        ExecuteQuery("SELECT * from tbl_distributorbranch")
        datareader = cmd.ExecuteReader
        table_branch.Load(datareader)
        cbobranch.DisplayMember = "Branch"
        cbobranch.ValueMember = "BranchCode"
        cbobranch.DataSource = table_branch
        conn.Close()

        Dim table_department As New DataTable

        ExecuteQuery("SELECT * from tbl_employeedept")
        datareader = cmd.ExecuteReader
        table_department.Load(datareader)
        cbodepartment.DisplayMember = "Department"
        cbodepartment.ValueMember = "DeptID"
        cbodepartment.DataSource = table_department
        conn.Close()

        cbocompany.SelectedItem = "OGDI"
        cbostatus.SelectedItem = "ACTIVE"

        ListView2.Items.Clear()
        ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("EmpExternalID"))
                If IsDBNull(datareader("Company")) Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                If IsDBNull(datareader("JobTitle")) Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                If datareader("InActiveID") = 0 Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                End If

                If IsDBNull(datareader("JobTitle_PG")) Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay") & "," & datareader("City") & "," & datareader("Province") & "," & datareader("ZipCode"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))

                If IsDBNull(datareader("Area")) Then
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                Else
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                End If


            End While
        End If
        conn.Close()
    End Sub

    Private Sub bttnsave_Click(sender As Object, e As EventArgs) Handles bttnsave.Click

        If ListView2.Items.Count = 0 Then
            MessageBox.Show("No Employee List, Please select from Search Filter")
        Else
            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

            Dim xlWorkbook As Excel.Workbook
            Dim xlWorksheet1 As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlrange As Excel.Range
            Dim counter, maximum_data, file_exist_counter As Integer
            Dim filePath As String


            xlWorkbook = xlApp.Workbooks.Add(misValue)
            xlWorksheet1 = xlWorkbook.Sheets("sheet1")


            xlrange = xlWorksheet1.Range("A1", "V1")
            xlrange.Font.Bold = True
            xlrange.Font.Color = Color.White
            xlrange.Interior.ColorIndex = 49
            xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            xlrange.ColumnWidth = 20
            xlrange.Value = {"ID No.", "Company", "Job Class", "Last Name", "Middle Name", "First Name", "Job Title", "Immediate Head", "Department", "Branch", "Branch Code", "CPC", "CPC Code", "Status", "P&G Code", "Netsuite ID", "Barangay", "City", "Province", "Zip Code", "TIN", "Area"}
            xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

            xlWorkbook.Application.Visible = False



            MDIParent1.ToolStripProgressBar1.Visible = True
            MDIParent1.ToolStripStatusLabel5.Visible = True
            MDIParent1.ToolStripStatusLabel5.Text = "Downloading..."
            maximum_data = ListView2.Items.Count
            counter = 2

            For i = 0 To ListView2.Items.Count - 1
                xlWorksheet1.Cells(counter, 1) = ListView2.Items.Item(i).Text.ToString
                xlWorksheet1.Cells(counter, 2) = ListView2.Items.Item(i).SubItems(1).Text
                xlWorksheet1.Cells(counter, 3) = ListView2.Items.Item(i).SubItems(2).Text
                xlWorksheet1.Cells(counter, 4) = ListView2.Items.Item(i).SubItems(3).Text
                xlWorksheet1.Cells(counter, 5) = ListView2.Items.Item(i).SubItems(4).Text
                xlWorksheet1.Cells(counter, 6) = ListView2.Items.Item(i).SubItems(5).Text
                xlWorksheet1.Cells(counter, 7) = ListView2.Items.Item(i).SubItems(6).Text
                xlWorksheet1.Cells(counter, 8) = ListView2.Items.Item(i).SubItems(7).Text
                xlWorksheet1.Cells(counter, 9) = ListView2.Items.Item(i).SubItems(8).Text
                xlWorksheet1.Cells(counter, 10) = ListView2.Items.Item(i).SubItems(9).Text
                xlWorksheet1.Cells(counter, 11) = ListView2.Items.Item(i).SubItems(10).Text
                xlWorksheet1.Cells(counter, 12) = ListView2.Items.Item(i).SubItems(11).Text
                xlWorksheet1.Cells(counter, 13) = ListView2.Items.Item(i).SubItems(12).Text
                xlWorksheet1.Cells(counter, 14) = ListView2.Items.Item(i).SubItems(13).Text
                xlWorksheet1.Cells(counter, 15) = ListView2.Items.Item(i).SubItems(14).Text
                xlWorksheet1.Cells(counter, 16) = ListView2.Items.Item(i).SubItems(15).Text
                xlWorksheet1.Cells(counter, 17) = ListView2.Items.Item(i).SubItems(16).Text
                xlWorksheet1.Cells(counter, 18) = ListView2.Items.Item(i).SubItems(17).Text
                xlWorksheet1.Cells(counter, 19) = ListView2.Items.Item(i).SubItems(18).Text
                xlWorksheet1.Cells(counter, 20) = ListView2.Items.Item(i).SubItems(19).Text
                xlWorksheet1.Cells(counter, 22) = ListView2.Items.Item(i).SubItems(21).Text

                MDIParent1.ToolStripProgressBar1.Value = (i / maximum_data) * 100
                MDIParent1.ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"
                counter = counter + 1
            Next

            MDIParent1.ToolStripProgressBar1.Visible = False
            MDIParent1.ToolStripStatusLabel5.Visible = False

            'ExecuteQuery("SELECT tbl_employeemaster.EmployeeID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department from tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID")
            'datareader = cmd.ExecuteReader

            'counter = 2
            'If datareader.HasRows Then
            '    While (datareader.Read)

            '        xlWorksheet.Cells(counter, 1) = datareader("FName").ToString
            '        xlWorksheet.Cells(counter, 2) = datareader("MName").ToString
            '        xlWorksheet.Cells(counter, 3) = datareader("LName").ToString
            '        xlWorksheet.Cells(counter, 4) = datareader("Department").ToString
            '        xlWorksheet.Cells(counter, 5) = datareader("Branch").ToString


            '        'xlWorksheet.Cells(counter, 1).Borders.LineStyle = Excel.XlLineStyle.xlContinuous



            '        'xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

            '        counter = counter + 1
            '    End While
            'End If

            file_exist_counter = 0

            Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop
            filePath = desktopPath & "/DCoM Employee.xls"

            If System.IO.File.Exists(filePath) Then

                Do
                    file_exist_counter += 1
                    filePath = desktopPath & "/DiCom Employee" & "(" & CStr(file_exist_counter) & ")" & ".xls"
                Loop Until Not System.IO.File.Exists(filePath)

                xlWorkbook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkbook.Close(True, misValue, misValue)
                xlApp.Quit()

            Else
                xlWorkbook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkbook.Close(True, misValue, misValue)
                xlApp.Quit()
            End If



            MessageBox.Show("Download Successful")
            xlrange = Nothing
            xlWorksheet1 = Nothing
            xlWorkbook = Nothing
        End If
    End Sub

    Private Sub cbobranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbobranch.SelectedIndexChanged
        conn.Close()
        If CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If

                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If

                    End While
                End If
                conn.Close()

            End If

        ElseIf CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If


        ElseIf CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If


        ElseIf CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SSELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If


        ElseIf CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        End If
    End Sub

    Private Sub cbodepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbodepartment.SelectedIndexChanged
        conn.Close()

        If CheckBox1.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If

        ElseIf CheckBox1.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If


        ElseIf CheckBox1.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SSELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()


        End If
    End Sub

    Private Sub cbocompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbocompany.SelectedIndexChanged
        conn.Close()
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox4.Checked = False Then
            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        End If
    End Sub

    Private Sub cbostatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbostatus.SelectedIndexChanged
        conn.Close()
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            ListView2.Items.Clear()

            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If

        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            cbobranch.Enabled = False
        Else
            cbobranch.Enabled = True
        End If

        If CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL)")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

            'ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True Then
            '    ListView2.Items.Clear()
            '    ExecuteQuery("SELECT employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
            '    datareader = cmd.ExecuteReader
            '    If datareader.HasRows Then
            '        While (datareader.Read)
            '            ListView2.Items.Add(datareader("EmpExternalID"))
            '            If IsDBNull(datareader("Company")) Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
            '            End If

            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

            '            If IsDBNull(datareader("JobTitle")) Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
            '            End If

            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
            '            If datareader("InActiveID") = 0 Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
            '            End If

            '            If IsDBNull(datareader("JobTitle_PG")) Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
            '            End If
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
            '        End While
            '    End If
            '    conn.Close()

            'ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
            '    ListView2.Items.Clear()
            '    ExecuteQuery("SELECT employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            '    datareader = cmd.ExecuteReader
            '    If datareader.HasRows Then
            '        While (datareader.Read)
            '            ListView2.Items.Add(datareader("EmpExternalID"))
            '            If IsDBNull(datareader("Company")) Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
            '            End If

            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

            '            If IsDBNull(datareader("JobTitle")) Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
            '            End If

            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
            '            If datareader("InActiveID") = 0 Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
            '            End If

            '            If IsDBNull(datareader("JobTitle_PG")) Then
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
            '            Else
            '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
            '            End If
            '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
            '        End While
            '    End If
            '    conn.Close()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            cbodepartment.Enabled = False
        Else
            cbodepartment.Enabled = True
        End If

        If CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL)")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()
        End If


        'If CheckBox1.Checked = True And CheckBox2.Checked = True Then
        '    ListView2.Items.Clear()
        '    ExecuteQuery("SELECT employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL)")
        '    datareader = cmd.ExecuteReader
        '    If datareader.HasRows Then
        '        While (datareader.Read)
        '            ListView2.Items.Add(datareader("EmpExternalID"))
        '            If IsDBNull(datareader("Company")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

        '            If IsDBNull(datareader("JobTitle")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
        '            If datareader("InActiveID") = 0 Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
        '            End If

        '            If IsDBNull(datareader("JobTitle_PG")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
        '            End If
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
        '        End While
        '    End If
        '    conn.Close()

        'ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False Then

        '    ListView2.Items.Clear()
        '    ExecuteQuery("SELECT employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
        '    datareader = cmd.ExecuteReader
        '    If datareader.HasRows Then
        '        While (datareader.Read)
        '            ListView2.Items.Add(datareader("EmpExternalID"))
        '            If IsDBNull(datareader("Company")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

        '            If IsDBNull(datareader("JobTitle")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
        '            If datareader("InActiveID") = 0 Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
        '            End If

        '            If IsDBNull(datareader("JobTitle_PG")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
        '            End If
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
        '        End While
        '    End If
        '    conn.Close()

        'ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True Then

        '    ListView2.Items.Clear()
        '    ExecuteQuery("SELECT employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
        '    datareader = cmd.ExecuteReader
        '    If datareader.HasRows Then
        '        While (datareader.Read)
        '            ListView2.Items.Add(datareader("EmpExternalID"))
        '            If IsDBNull(datareader("Company")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

        '            If IsDBNull(datareader("JobTitle")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
        '            If datareader("InActiveID") = 0 Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
        '            End If

        '            If IsDBNull(datareader("JobTitle_PG")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
        '            End If
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
        '        End While
        '    End If
        '    conn.Close()

        'ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
        '    ListView2.Items.Clear()
        '    ExecuteQuery("SELECT employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
        '    datareader = cmd.ExecuteReader
        '    If datareader.HasRows Then
        '        While (datareader.Read)
        '            ListView2.Items.Add(datareader("EmpExternalID"))
        '            If IsDBNull(datareader("Company")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

        '            If IsDBNull(datareader("JobTitle")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
        '            End If

        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
        '            If datareader("InActiveID") = 0 Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
        '            End If

        '            If IsDBNull(datareader("JobTitle_PG")) Then
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
        '            Else
        '                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
        '            End If
        '            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
        '        End While
        '    End If
        '    conn.Close()
        'End If

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            cbocompany.Enabled = False
        Else
            cbocompany.Enabled = True
        End If
        If CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL)")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If

                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()
        End If

    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            cbostatus.Enabled = False
        Else
            cbostatus.Enabled = True
        End If

        If CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL)")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else

                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.Company='" & cbocompany.Text & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then

            ListView2.Items.Clear()
            If cbostatus.Text = "ACTIVE" Then


                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='0'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()

            Else
                ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.BranchCode='" & cbobranch.SelectedValue & "' AND employeemasterA.InActiveID='1'")
                datareader = cmd.ExecuteReader
                If datareader.HasRows Then
                    While (datareader.Read)
                        ListView2.Items.Add(datareader("EmpExternalID"))
                        If IsDBNull(datareader("Company")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                        If IsDBNull(datareader("JobTitle")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                        End If

                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                        If datareader("InActiveID") = 0 Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                        End If

                        If IsDBNull(datareader("JobTitle_PG")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                        End If
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                        If IsDBNull(datareader("Area")) Then
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                        Else
                            ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                        End If
                    End While
                End If
                conn.Close()
            End If


        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then

            ListView2.Items.Clear()
            ExecuteQuery("SELECT tbl_employeearea.Area,employeemasterA.TIN,employeemasterA.Barangay,employeemasterA.City,employeemasterA.Province,employeemasterA.ZipCode,employeemasterA.EmpExternalID,employeemasterA.EmpInternalID,employeemasterA.Company,JobClass,employeemasterA.LName,employeemasterA.MName,employeemasterA.FName,tbl_employeejobtitle.JobTitle,tbl_employeejobtitle.JobTitle_PG,Department,Branch_NS,BranchCode_NI,CPC,CPCID_NI,employeemasterA.InActiveID,CONCAT(employeemasterB.LName,' ',employeemasterB.FName) AS ImmediateHead FROM tbl_employeemaster employeemasterA LEFT JOIN tbl_distributorcpc ON employeemasterA.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeedept ON employeemasterA.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorbranch ON employeemasterA.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeejobclass ON employeemasterA.JobClassID=tbl_employeejobclass.JobClassID LEFT JOIN tbl_employeejobtitle ON employeemasterA.JobTitleID=tbl_employeejobtitle.JobTitleID LEFT JOIN tbl_employeemaster employeemasterB ON employeemasterA.SupID=employeemasterB.EmpInternalID LEFT JOIN tbl_employeearea ON employeemasterA.AreaID=tbl_employeearea.AreaID WHERE (employeemasterA.JobTitle<>'SFA PURPOSES ONLY' OR employeemasterA.JobTitle IS NULL) AND employeemasterA.DeptID='" & cbodepartment.SelectedValue & "' AND employeemasterA.Company='" & cbocompany.Text & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView2.Items.Add(datareader("EmpExternalID"))
                    If IsDBNull(datareader("Company")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Company"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobClass"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("FName"))

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If

                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ImmediateHead").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Branch_NS"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("BranchCode_NI"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPC").ToString)
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("CPCID_NI").ToString)
                    If datareader("InActiveID") = 0 Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("ACTIVE")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("INACTIVE")
                    End If

                    If IsDBNull(datareader("JobTitle_PG")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("JobTitle_PG"))
                    End If
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("EmpInternalID"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Barangay"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("City"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Province"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ZipCode"))
                    ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("TIN"))
                    If IsDBNull(datareader("Area")) Then
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Area"))
                    End If
                End While
            End If
            conn.Close()

        End If

    End Sub
End Class