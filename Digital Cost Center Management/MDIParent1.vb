Imports System.ComponentModel
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel

Public Class MDIParent1
    Dim internet_connection As Boolean

    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.CenterToScreen()
        Me.WindowState = FormWindowState.Maximized
        ToolStripStatusLabel2.Text = Login.user_name
        ToolStripStatusLabel4.Text = Login.department_name
        ToolStripStatusLabel5.Text = "Version 1.8"
    End Sub

    Private Sub AddEmployeeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddEmployeeToolStripMenuItem1.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            For Each aform As Form In Me.MdiChildren
                aform.Close()
            Next
            Frm_Employee_Add.MdiParent = Me
            Frm_Employee_Add.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Add.Show()
        End If

    End Sub

    Private Sub MDIParent1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Login.txtusername.Text = ""
        Login.txtpassword.Text = ""
        Login.txtusername.Select()
        Login.Show()
    End Sub

    Private Sub SearchEmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchEmployeeToolStripMenuItem.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            For Each aform As Form In Me.MdiChildren
                aform.Close()
            Next
            For Each aform As Form In Me.MdiChildren
                aform.Close()
            Next
            Frm_Employee_Search_Employee.MdiParent = Me
            Frm_Employee_Search_Employee.StartPosition = FormStartPosition.CenterScreen
            Frm_Employee_Search_Employee.Show()
        End If

    End Sub


    Private Sub AddFleetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddFleetToolStripMenuItem.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            For Each aform As Form In Me.MdiChildren
                aform.Close()
            Next
            Frm_Fleet_Add.MdiParent = Me
            Frm_Fleet_Add.StartPosition = FormStartPosition.CenterScreen
            Frm_Fleet_Add.Show()
        End If

    End Sub

    Private Sub AssignFleetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssignFleetToolStripMenuItem.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            For Each aform As Form In Me.MdiChildren
                aform.Close()
            Next
            Frm_Fleet_Search.MdiParent = Me
            Frm_Fleet_Search.StartPosition = FormStartPosition.CenterScreen
            Frm_Fleet_Search.Show()
        End If

    End Sub

    Private Sub EmployeeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_report_employee.MdiParent = Me
        Frm_report_employee.StartPosition = FormStartPosition.CenterScreen
        Frm_report_employee.Show()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AddJobTitleToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddJobTitleToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Job_Title.MdiParent = Me
        Frm_Masterdata_Employee_Job_Title.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Job_Title.Show()
    End Sub

    Private Sub AddUserToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_User_Register.MdiParent = Me
        Frm_Masterdata_User_Register.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_User_Register.Show()
    End Sub

    Private Sub SalesTaggingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SalesTaggingToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Employee_Sales_Tagging.MdiParent = Me
        Frm_Employee_Sales_Tagging.StartPosition = FormStartPosition.CenterScreen
        Frm_Employee_Sales_Tagging.Show()
    End Sub

    Private Sub SearchEquipmentToolStripMenuItem_Click(sender As Object, e As EventArgs)
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_IT_Search.MdiParent = Me
        Frm_IT_Search.StartPosition = FormStartPosition.CenterScreen
        Frm_IT_Search.Show()
    End Sub

    Private Sub FixedAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FixedAssetToolStripMenuItem.Click
        MessageBox.Show("Fixed Asset Under Construction")
    End Sub

    Private Sub AddVehicleTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddVehicleTypeToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Fleet_Vehicle_Type.MdiParent = Me
        Frm_Masterdata_Fleet_Vehicle_Type.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Fleet_Vehicle_Type.Show()
    End Sub

    Private Sub StoreMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StoreMasterToolStripMenuItem.Click
        Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

        Dim xlWorkbook As Excel.Workbook
        Dim xlWorksheet1 As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim xlrange As Excel.Range
        Dim counter, maximum_data, file_exist_counter, i As Integer
        Dim filePath As String


        xlWorkbook = xlApp.Workbooks.Add(misValue)
        xlWorksheet1 = xlWorkbook.Sheets("sheet1")


        xlrange = xlWorksheet1.Range("A1", "E1")
        xlrange.Font.Bold = True
        xlrange.Font.Color = Color.White
        xlrange.Interior.ColorIndex = 49
        xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        xlrange.ColumnWidth = 20
        xlrange.Value = {"Store Name", "Destination", "CPC", "Sales Rep", "Comments"}
        xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

        xlWorkbook.Application.Visible = False

        ToolStripProgressBar1.Visible = True
        ToolStripStatusLabel5.Visible = True
        ToolStripStatusLabel5.Text = "Downloading..."

        ExecuteQuery("SELECT COUNT(*) as allcount FROM tbl_storemaster LEFT JOIN tbl_distributorbranch ON tbl_storemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_distributorcpc ON tbl_storemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeemaster employeemasterA ON tbl_storemaster.SalesRepCode=employeemasterA.EmpInternalID LEFT JOIN tbl_employeemaster employeemasterB ON tbl_storemaster.SalesRepID=employeemasterB.EmpInternalID WHERE employeemasterA.Comments='PRESELL' OR employeemasterA.Comments='MAM'")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                maximum_data = datareader("allcount")
            End While
        End If
        conn.Close()

        ExecuteQuery("SELECT CONCAT(StoreID,' ',StoreName) AS StoreName,Branch,CPC,CONCAT(employeemasterB.FName,' ',employeemasterB.LName) AS SaleRep,employeemasterA.Comments FROM tbl_storemaster LEFT JOIN tbl_distributorbranch ON tbl_storemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_distributorcpc ON tbl_storemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeemaster employeemasterA ON tbl_storemaster.SalesRepCode=employeemasterA.EmpInternalID LEFT JOIN tbl_employeemaster employeemasterB ON tbl_storemaster.SalesRepID=employeemasterB.EmpInternalID WHERE employeemasterA.Comments='PRESELL' OR employeemasterA.Comments='MAM' ORDER BY Branch ASC")
        datareader = cmd.ExecuteReader


        counter = 2
        i = 0
        If datareader.HasRows Then
            While (datareader.Read)
                xlWorksheet1.Cells(counter, 1) = datareader("StoreName")
                xlWorksheet1.Cells(counter, 2) = datareader("Branch")
                xlWorksheet1.Cells(counter, 3) = datareader("CPC")
                xlWorksheet1.Cells(counter, 4) = datareader("SaleRep")
                xlWorksheet1.Cells(counter, 5) = datareader("Comments")
                i += 1
                ToolStripProgressBar1.Value = (i / maximum_data) * 100
                ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"
                counter = counter + 1
            End While
        End If
        conn.Close()


        ToolStripProgressBar1.Visible = False
        ToolStripStatusLabel5.Visible = False

        file_exist_counter = 0

        Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop
        filePath = desktopPath & "/DiCom Store Master.xls"

        If System.IO.File.Exists(filePath) Then

            Do
                file_exist_counter += 1
                filePath = desktopPath & "/DCoM Store Master" & "(" & CStr(file_exist_counter) & ")" & ".xls"
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
    End Sub

    Private Sub FleetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FleetToolStripMenuItem1.Click
        Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

        Dim xlWorkbook As Excel.Workbook
        Dim xlWorksheet1 As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim xlrange As Excel.Range
        Dim counter, maximum_data, file_exist_counter, i As Integer
        Dim filePath As String


        xlWorkbook = xlApp.Workbooks.Add(misValue)
        xlWorksheet1 = xlWorkbook.Sheets("sheet1")


        xlrange = xlWorksheet1.Range("A1", "Q1")
        xlrange.Font.Bold = True
        xlrange.Font.Color = Color.White
        xlrange.Interior.ColorIndex = 49
        xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        xlrange.ColumnWidth = 20
        xlrange.Value = {"Plate No.", "Branch", "Branch Code", "Function", "Vehicle Type", "P&G Code Vehicle Type", "CPC", "CPCID", "Status", "Fuel Type", "Vehicle", "Date Acquisition", "Vehicle Class", "Remarks", "Brand", "Model", "Assigned To"}
        xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

        xlWorkbook.Application.Visible = False

        ToolStripProgressBar1.Visible = True
        ToolStripStatusLabel5.Visible = True
        ToolStripStatusLabel5.Text = "Downloading..."

        ExecuteQuery("SELECT COUNT(*) allcount FROM tbl_fleetmaster LEFT JOIN tbl_employeemaster ON tbl_fleetmaster.EmployeeID=tbl_employeemaster.EmployeeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                maximum_data = datareader("allcount")
            End While
        End If
        conn.Close()

        ExecuteQuery("SELECT tbl_fleetmaster.Vehicle,tbl_distributorbranch.Branch_NS,tbl_distributorbranch.BranchCode_NI,tbl_fleetmaster.`Function`,tbl_fleetvehicletype.VehicleType,tbl_fleetvehicletype.PG_Code_VehicleType,tbl_distributorcpc.CPC,tbl_distributorcpc.CPCID_NI,tbl_fleetmaster.`Status`,tbl_fleetmaster.FuelType,tbl_fleetmaster.PlateNo,tbl_fleetmaster.DateAcquisition,tbl_fleetmaster.VehicleClass,tbl_fleetmaster.remarks,tbl_fleetbrand.Brand,tbl_fleetmaster.Model,CONCAT(tbl_employeemaster.FName,'',tbl_employeemaster.LName) AS assignedto FROM tbl_fleetmaster LEFT JOIN tbl_employeemaster ON tbl_fleetmaster.EmployeeID=tbl_employeemaster.EmployeeID LEFT JOIN tbl_distributorbranch ON tbl_fleetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_fleetvehicletype ON tbl_fleetmaster.VehicleTypeID=tbl_fleetvehicletype.VehicleTypeID LEFT JOIN tbl_distributorcpc ON tbl_fleetmaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_fleetbrand ON tbl_fleetmaster.BrandID=tbl_fleetbrand.BrandID")
        datareader = cmd.ExecuteReader


        counter = 2
        i = 0
        If datareader.HasRows Then
            While (datareader.Read)
                xlWorksheet1.Cells(counter, 1) = datareader("PlateNo")
                xlWorksheet1.Cells(counter, 1).NumberFormat = "@"
                xlWorksheet1.Cells(counter, 2) = datareader("Branch_NS")
                xlWorksheet1.Cells(counter, 3) = datareader("BranchCode_NI")
                xlWorksheet1.Cells(counter, 4) = datareader("Function")

                If IsDBNull(datareader("VehicleType")) Then

                Else
                    xlWorksheet1.Cells(counter, 5) = datareader("VehicleType")
                End If


                If IsDBNull(datareader("PG_Code_VehicleType")) Then

                Else
                    xlWorksheet1.Cells(counter, 6) = datareader("PG_Code_VehicleType")
                End If


                xlWorksheet1.Cells(counter, 7) = datareader("CPC")
                xlWorksheet1.Cells(counter, 8) = datareader("CPCID_NI")
                xlWorksheet1.Cells(counter, 9) = datareader("Status")
                xlWorksheet1.Cells(counter, 10) = datareader("FuelType")
                xlWorksheet1.Cells(counter, 11) = datareader("Vehicle")
                xlWorksheet1.Cells(counter, 12) = datareader("DateAcquisition")
                xlWorksheet1.Cells(counter, 13) = datareader("VehicleClass")
                xlWorksheet1.Cells(counter, 14) = datareader("remarks")

                If IsDBNull(datareader("Brand")) Then

                Else
                    xlWorksheet1.Cells(counter, 15) = datareader("Brand")
                End If


                xlWorksheet1.Cells(counter, 16) = datareader("Model")


                If IsDBNull(datareader("assignedto")) Then

                Else
                    xlWorksheet1.Cells(counter, 17) = datareader("assignedto")
                End If


                i += 1
                ToolStripProgressBar1.Value = (i / maximum_data) * 100
                ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"
                counter = counter + 1
            End While
        End If
        conn.Close()


        ToolStripProgressBar1.Visible = False
        ToolStripStatusLabel5.Visible = False

        file_exist_counter = 0

        Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop
        filePath = desktopPath & "/DCoM Fleet Master.xls"

        If System.IO.File.Exists(filePath) Then

            Do
                file_exist_counter += 1
                filePath = desktopPath & "/DCoM Fleet Master" & "(" & CStr(file_exist_counter) & ")" & ".xls"
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
    End Sub

    Private Sub ITEquipmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ITEquipmentToolStripMenuItem.Click
        Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

        Dim xlWorkbook As Excel.Workbook
        Dim xlWorksheet1 As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim xlrange As Excel.Range
        Dim counter, maximum_data, file_exist_counter, i As Integer
        Dim filePath As String


        xlWorkbook = xlApp.Workbooks.Add(misValue)
        xlWorksheet1 = xlWorkbook.Sheets("sheet1")


        xlrange = xlWorksheet1.Range("A1", "L1")
        xlrange.Font.Bold = True
        xlrange.Font.Color = Color.White
        xlrange.Interior.ColorIndex = 49
        xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        xlrange.ColumnWidth = 20
        xlrange.Value = {"Description", "Type", "Brand", "Branch", "Department", "Model", "Model No.", "Serial No.", "Date Purchased", "Remarks", "Operational", "P&G Code"}
        xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

        xlWorkbook.Application.Visible = False

        ToolStripProgressBar1.Visible = True
        ToolStripStatusLabel5.Visible = True
        ToolStripStatusLabel5.Text = "Downloading..."

        ExecuteQuery("SELECT COUNT(*) as allcount FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_distributorbranch ON tbl_itassetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_itassetmaster.DeptID=tbl_employeedept.DeptID")
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While (datareader.Read)
                maximum_data = datareader("allcount")
            End While
        End If
        conn.Close()

        ExecuteQuery("SELECT AssetDesc,Type,Brand,Branch,Department,Model,ModelNo,SerialNo,DatePurchased,Remarks,IsOperational,PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_distributorbranch ON tbl_itassetmaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_itassetmaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID")
        datareader = cmd.ExecuteReader


        counter = 2
        i = 0
        If datareader.HasRows Then
            While (datareader.Read)
                xlWorksheet1.Cells(counter, 1) = datareader("AssetDesc")
                xlWorksheet1.Cells(counter, 2) = datareader("Type")
                xlWorksheet1.Cells(counter, 3) = datareader("Brand")
                xlWorksheet1.Cells(counter, 4) = datareader("Branch")
                xlWorksheet1.Cells(counter, 5) = datareader("Department")
                xlWorksheet1.Cells(counter, 6) = datareader("Model")
                xlWorksheet1.Cells(counter, 6).NumberFormat = "@"
                xlWorksheet1.Cells(counter, 7) = datareader("ModelNo")
                xlWorksheet1.Cells(counter, 7).NumberFormat = "@"
                xlWorksheet1.Cells(counter, 8) = datareader("SerialNo")
                xlWorksheet1.Cells(counter, 8).NumberFormat = "@"
                xlWorksheet1.Cells(counter, 9) = datareader("DatePurchased")
                xlWorksheet1.Cells(counter, 10) = datareader("Remarks")
                If datareader("IsOperational") = 1 Then
                    xlWorksheet1.Cells(counter, 11) = "YES"
                Else
                    xlWorksheet1.Cells(counter, 11) = "NO"
                End If

                xlWorksheet1.Cells(counter, 12) = datareader("PG_Code")

                i += 1
                ToolStripProgressBar1.Value = (i / maximum_data) * 100
                ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"
                counter = counter + 1
            End While
        End If
        conn.Close()


        ToolStripProgressBar1.Visible = False
        ToolStripStatusLabel5.Visible = False

        file_exist_counter = 0

        Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop
        filePath = desktopPath & "/DCoM IT Master.xls"

        If System.IO.File.Exists(filePath) Then

            Do
                file_exist_counter += 1
                filePath = desktopPath & "/DCoM IT Master" & "(" & CStr(file_exist_counter) & ")" & ".xls"
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
    End Sub

    Private Sub AddITEquipmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddITEquipmentToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_IT_Add.MdiParent = Me
        Frm_IT_Add.StartPosition = FormStartPosition.CenterScreen
        Frm_IT_Add.Show()
    End Sub

    Private Sub SearchITEquipmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchITEquipmentToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_IT_Search.MdiParent = Me
        Frm_IT_Search.StartPosition = FormStartPosition.CenterScreen
        Frm_IT_Search.Show()
    End Sub

    Private Sub AntivirusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AntivirusToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_Antivirus.MdiParent = Me
        Frm_Masterdata_IT_Antivirus.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_Antivirus.Show()
    End Sub

    Private Sub MSOfficeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MSOfficeToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_MS_Office.MdiParent = Me
        Frm_Masterdata_IT_MS_Office.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_MS_Office.Show()
    End Sub

    Private Sub OSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OSToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_OS.MdiParent = Me
        Frm_Masterdata_IT_OS.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_OS.Show()
    End Sub

    Private Sub PGCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PGCodeToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_PG_Code.MdiParent = Me
        Frm_Masterdata_IT_PG_Code.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_PG_Code.Show()
    End Sub

    Private Sub StatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_Status.MdiParent = Me
        Frm_Masterdata_IT_Status.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_Status.Show()
    End Sub

    Private Sub VendorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VendorToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_Vendor.MdiParent = Me
        Frm_Masterdata_IT_Vendor.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_Vendor.Show()
    End Sub

    Private Sub TypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypeToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_Type.MdiParent = Me
        Frm_Masterdata_IT_Type.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_Type.Show()
    End Sub

    Private Sub BrandToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrandToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_masterdata_IT_brand.MdiParent = Me
        Frm_masterdata_IT_brand.StartPosition = FormStartPosition.CenterScreen
        Frm_masterdata_IT_brand.Show()
    End Sub

    Private Sub BranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BranchToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Branch.MdiParent = Me
        Frm_Masterdata_Employee_Branch.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Branch.Show()
    End Sub

    Private Sub DepartmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Department.MdiParent = Me
        Frm_Masterdata_Employee_Department.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Department.Show()
    End Sub

    Private Sub CPCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CPCToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_CPC.MdiParent = Me
        Frm_Masterdata_Employee_CPC.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_CPC.Show()
    End Sub

    Private Sub JobClassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobClassToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Job_Class.MdiParent = Me
        Frm_Masterdata_Employee_Job_Class.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Job_Class.Show()
    End Sub

    Private Sub StatusToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles StatusToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Status.MdiParent = Me
        Frm_Masterdata_Employee_Status.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Status.Show()
    End Sub

    Private Sub TypeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TypeToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Type.MdiParent = Me
        Frm_Masterdata_Employee_Type.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Type.Show()
    End Sub

    Private Sub EducationalAttainmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EducationalAttainmentToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Employee_Educational_Attainment.MdiParent = Me
        Frm_Masterdata_Employee_Educational_Attainment.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Employee_Educational_Attainment.Show()
    End Sub

    Private Sub VendorToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles VendorToolStripMenuItem1.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Finance_BIX_Vendor.MdiParent = Me
        Frm_Masterdata_Finance_BIX_Vendor.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Finance_BIX_Vendor.Show()
    End Sub

    Private Sub PhoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhoneToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Finance_BIX_PHONE.MdiParent = Me
        Frm_Masterdata_Finance_BIX_PHONE.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Finance_BIX_PHONE.Show()
    End Sub

    Private Sub PINSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PINSToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Finance_BIX_PINS.MdiParent = Me
        Frm_Masterdata_Finance_BIX_PINS.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Finance_BIX_PINS.Show()
    End Sub

    Private Sub AreaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AreaToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Finance_BIX_Area_Rate.MdiParent = Me
        Frm_Masterdata_Finance_BIX_Area_Rate.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Finance_BIX_Area_Rate.Show()
    End Sub

    Private Sub SoftwareToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoftwareToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_Software.MdiParent = Me
        Frm_Masterdata_IT_Software.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_Software.Show()
    End Sub

    Private Sub SoftwareOptionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoftwareOptionalToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_IT_Software_Optional.MdiParent = Me
        Frm_Masterdata_IT_Software_Optional.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_IT_Software_Optional.Show()
    End Sub

    Private Sub BackupAreaRateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupAreaRateToolStripMenuItem.Click
        For Each aform As Form In Me.MdiChildren
            aform.Close()
        Next
        Frm_Masterdata_Finance_BIX_Area_Rate_Backup.MdiParent = Me
        Frm_Masterdata_Finance_BIX_Area_Rate_Backup.StartPosition = FormStartPosition.CenterScreen
        Frm_Masterdata_Finance_BIX_Area_Rate_Backup.Show()
    End Sub
End Class
