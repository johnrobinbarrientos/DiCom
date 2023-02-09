Imports Excel = Microsoft.Office.Interop.Excel
Public Class Frm_IT_PAR
    Public value_assetid As String
    Dim userinternalID, seqno, qtty As Integer
    Dim value_person, supervisor, value_hr, parunparID, name_generate, department, branch As String
    Dim itprice As Single
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

    Private Sub Frm_tag_IT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub

    Public Sub initialize()
        Dim PARinternal_id As String

        userinternalID = Login.userinternalID
        value_assetid = Frm_IT_Search.value_assetid
        lblassetid.Text = value_assetid
        PARinternal_id = ""


        ExecuteQuery("SELECT * FROM tbl_itassetmaster LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID  WHERE AssetID=" & value_assetid & "")
        datareader = cmd.ExecuteReader

        If datareader.HasRows Then
            While (datareader.Read)
                txtdescription.Text = datareader("AssetDesc")
                txtmodel.Text = datareader("Model")
                txtmodel_no.Text = datareader("ModelNo")
                txtserialno.Text = datareader("SerialNo")
                txtbrand.Text = datareader("Brand")
                txttype.Text = datareader("Type")
                txtdatepurchased.Text = datareader("DatePurchased")

                If IsDBNull(datareader("PAREmpInternalID")) Then
                    PARinternal_id = ""
                Else
                    PARinternal_id = datareader("PAREmpInternalID")
                End If

                itprice = datareader("Price")
                qtty = datareader("Qty")
            End While
        End If
        conn.Close()

        If PARinternal_id <> "" Then
            ListView4.Items.Clear()
            ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.SupID,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmpInternalID ='" & PARinternal_id & "'")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    ListView4.Items.Add(datareader("FName"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("MName"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("LName"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Department"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("CPC"))
                    ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("Branch"))

                    name_generate = datareader("FName") & " " & datareader("MName") & " " & datareader("LName")
                    department = datareader("Department")
                    branch = datareader("Branch")

                    If IsDBNull(datareader("JobTitle")) Then
                        ListView4.Items(ListView4.Items.Count - 1).SubItems.Add("")
                    Else
                        ListView4.Items(ListView4.Items.Count - 1).SubItems.Add(datareader("JobTitle"))
                    End If
                End While
            End If
            conn.Close()
        Else
            ListView4.Items.Clear()
        End If

        ExecuteQuery("SELECT SeqNo FROM tbl_itassetpar ORDER BY SeqNo DESC")
        seqno = cmd.ExecuteScalar
        conn.Close()

        parunparID = Format(Now, "MM") + Format(Now, "dd") + Format(Now, "yy") + Format(Now, "HH") + Format(Now, "mm") + CStr(seqno + 1)

        lblsupervisor.Visible = False
        lblhrname.Visible = False
        txtfirst_name.Text = ""
        txtmiddle_name.Text = ""
        txtlast_name.Text = ""
        txtdepartment.Text = ""
        txtcpc.Text = ""
        txtbranch.Text = ""
        txtjob_title.Text = ""
        txtremarks.Text = ""
        value_hr = ""
        value_person = ""

    End Sub

    Private Sub bttnPAR_Click(sender As Object, e As EventArgs) Handles bttnPAR.Click
        If ListView4.Items.Count <> 0 Then
            MessageBox.Show("Please UnPAR Employee First")
        Else

            If value_person = "" Then
                MessageBox.Show("Please Select Employee")
                txtsearch.Select()
            ElseIf value_hr = "" Then
                MessageBox.Show("Please Select HR Admin")
                txtsearch_hr.Select()
            Else

                Dim n As String = MsgBox("PAR Employee?", MsgBoxStyle.YesNo, "")

                If n = vbYes Then

                    Call Connection.checkconnection()
                    internet_connection = Connection.internet_connection

                    If internet_connection = False Then

                        MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                        Exit Sub
                    Else

                        ExecuteQuery("INSERT INTO tbl_itassetpar (PARNo,PARDate,PARType,PAREmpInternalID,AssetID,Qty,Amount,ApprovedBy,ReceivedByHRAdmin,DateProcessed,ProcessedBy,DateUpdated,UpdatedBy,Remarks) VALUES('" & "PAR_" + parunparID & "','" & Format(Now, "yyyy-MM-dd") & "','PAR'," & value_person & "," & value_assetid & "," & qtty & "," & itprice & "," & supervisor & "," & value_hr & ",'" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "'," & userinternalID & ",'" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "'," & userinternalID & ",'" & txtremarks.Text.Replace("'", "''") & "')")
                        conn.Close()

                        ExecuteQuery("UPDATE tbl_itassetmaster SET PAREmpInternalID=" & value_person & " WHERE AssetID=" & value_assetid & "")
                        conn.Close()

                        MessageBox.Show("Successfuly PAR")
                        Call initialize()

                    End If

                End If


            End If

        End If
    End Sub


    Private Sub txtsearch_hr_TextChanged(sender As Object, e As EventArgs) Handles txtsearch_hr.TextChanged
        If txtsearch_hr.Text = "" Then
            ListView2.Visible = False

        Else

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ListView2.Visible = True
                ExecuteQuery("SELECT EmpInternalID, CONCAT(LName,', ',FName,' ',MName) as fullname from tbl_employeemaster WHERE (LName LIKE '" & txtsearch_hr.Text.Replace("'", "''") & "%' OR FName LIKE '%" & txtsearch_hr.Text.Replace("'", "''") & "%') AND (JobTitle<>'SFA PURPOSES ONLY' OR JobTitle IS NUll) AND InActiveID=0 AND DeptID=5")
                datareader = cmd.ExecuteReader
            End If

        End If

        ListView2.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("EmpInternalID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("fullname"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub txtsearch_hr_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch_hr.KeyDown
        If ListView2.Items.Count = 0 Then
        Else
            If e.KeyCode = Keys.Down Then
                ListView2.Items(0).Selected = True
                ListView2.Select()
            End If

        End If
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        value_hr = ListView2.SelectedItems.Item(0).Text
        lblhrname.Text = ListView2.SelectedItems(0).SubItems(1).Text
        ListView2.Visible = False
        lblhrname.Visible = True
        txtsearch_hr.Text = ""
    End Sub

    Private Sub ListView2_KeyUp(sender As Object, e As KeyEventArgs) Handles ListView2.KeyUp
        If ListView2.Items(0).Selected = True Then
            If e.KeyCode = Keys.Up Then
                txtsearch_hr.Select()
            End If
        End If

    End Sub

    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            value_hr = ListView2.SelectedItems.Item(0).Text
            lblhrname.Text = ListView2.SelectedItems(0).SubItems(1).Text
            ListView2.Visible = False
            lblhrname.Visible = True
            txtsearch_hr.Text = ""
        End If
    End Sub



    Private Sub bttncancel_Click(sender As Object, e As EventArgs) Handles bttncancel.Click
        Me.Close()
    End Sub


    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text = "" Then
            ListView1.Visible = False

        Else

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ListView1.Visible = True
                ExecuteQuery("SELECT SupID,EmpInternalID, CONCAT(LName,', ',FName,' ',MName) as fullname from tbl_employeemaster WHERE (LName LIKE '" & txtsearch.Text.Replace("'", "''") & "%' OR FName LIKE '%" & txtsearch.Text.Replace("'", "''") & "%') AND (JobTitle<>'SFA PURPOSES ONLY' OR JobTitle IS NUll) AND InActiveID=0")
                datareader = cmd.ExecuteReader
            End If

        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("EmpInternalID"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("fullname"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("SupID").ToString)

            End While
        End If
        conn.Close()
    End Sub

    Private Sub bttngenerate_Click(sender As Object, e As EventArgs) Handles bttngenerate.Click
        If ListView4.Items.Count = 0 Then
            MessageBox.Show("No PAR Employee")
        Else

            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

            Dim xlWorkbook As Excel.Workbook
            Dim xlWorksheet1 As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlrange As Excel.Range
            Dim file_exist_counter As Integer
            Dim filePath, PARno, remarks, hrname, immediate_head As String


            xlWorkbook = xlApp.Workbooks.Add(misValue)
            xlWorksheet1 = xlWorkbook.Sheets("sheet1")


            PARno = ""
            remarks = ""
            hrname = ""
            immediate_head = ""

            ExecuteQuery("SELECT PARNo,Remarks,CONCAT(employeemasterA.FName,' ',employeemasterA.MName,' ',employeemasterA.LName) as hrname,CONCAT(employeemasterB.FName,' ',employeemasterB.MName,' ',employeemasterB.LName) as immediate_head FROM tbl_itassetpar LEFT JOIN tbl_employeemaster employeemasterA ON tbl_itassetpar.ReceivedByHRAdmin=employeemasterA.EmpInternalID LEFT JOIN tbl_employeemaster employeemasterB ON tbl_itassetpar.ApprovedBy=employeemasterB.EmpInternalID WHERE AssetID=" & value_assetid & " ORDER BY DateProcessed ASC")
            datareader = cmd.ExecuteReader
            If datareader.HasRows Then
                While (datareader.Read)
                    PARno = datareader("PARNo")
                    remarks = datareader("Remarks")
                    hrname = datareader("hrname")
                    immediate_head = datareader("immediate_head")
                End While
            End If
            conn.Close()



            With xlWorksheet1
                .Columns("A:A").ColumnWidth = 1
                .Columns("B:B").ColumnWidth = 10
                .Columns("C:C").ColumnWidth = 23.43
                .Columns("D:D").ColumnWidth = 15.29
                .Columns("E:E").ColumnWidth = 17
                .Columns("F:F").ColumnWidth = 22.43
                .Columns("G:G").ColumnWidth = 16.29
                .Columns("H:H").ColumnWidth = 1
                .Rows("1:1").RowHeight = 12.75
                .Rows("2:2").RowHeight = 30
                .Rows("3:3").RowHeight = 25
                .Rows("4:4").RowHeight = 18
                .Rows("5:5").RowHeight = 18
                .Rows("6:6").RowHeight = 12.75
                .Rows("7:7").RowHeight = 14.25
                .Rows("8:8").RowHeight = 12.75
                .Rows("9:9").RowHeight = 20
                .Rows("10:19").RowHeight = 15
                .Rows("20:20").RowHeight = 12.75
                .Rows("21:24").RowHeight = 12.75
                .Rows("25:25").RowHeight = 12.75
            End With


            xlrange = xlWorksheet1.Range("B2", "G2")
            xlrange.Merge()
            xlrange.Value = "Oro Grande Distributors, Inc."
            With xlrange
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .ReadingOrder = Excel.Constants.xlContext
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Font.Name = "Arial"
                .Font.Size = 16
                .Font.Bold = True
                .Font.Italic = True
            End With

            xlrange = xlWorksheet1.Range("B3", "G3")
            xlrange.Merge()
            xlrange.Value = "PROPERTY ACCOUNTABILITY RECEIPT"
            With xlrange
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .ReadingOrder = Excel.Constants.xlContext
                .MergeCells = True
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Font.Bold = True
                .Font.Name = "Arial"
                .Font.Size = 16
            End With

            xlrange = xlWorksheet1.Range("B4", "E4")
            xlrange.Merge()
            xlrange.Value = "  EMPLOYEE NAME : " & name_generate
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B5", "E5")
            xlrange.Merge()
            xlrange.Value = "  DEPARTMENT       : " & department
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("F4", "G4")
            xlrange.Merge()
            xlrange.Value = "  Reference No. : " & PARno
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("F5", "G5")
            xlrange.Merge()
            xlrange.Value = "  Date                 : " & Format(Now, "MM/dd/yyyy")
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B6", "G8")
            xlrange.Merge()
            xlrange.Value = "  I acknowledge receipt of the following OGDI assets:"
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B9")
            xlrange.Value = "Quantity"
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Font.Bold = True
            End With

            xlrange = xlWorksheet1.Range("E9")
            xlrange.Value = "Model #"
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Font.Bold = True
            End With

            xlrange = xlWorksheet1.Range("F9")
            xlrange.Value = "SN #"
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Font.Bold = True
            End With

            xlrange = xlWorksheet1.Range("G9")
            xlrange.Value = "Amount"
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Font.Bold = True
            End With

            xlrange = xlWorksheet1.Range("C9")
            xlrange.Value = "Description"

            xlrange = xlWorksheet1.Range("C9", "D9")
            With xlrange
                .Font.Bold = True
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With


            xlrange = xlWorksheet1.Range("B10")
            xlrange.Value = qtty
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            xlrange = xlWorksheet1.Range("C10")
            xlrange.Value = txtdescription.Text
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("D10")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("E10", "F10")
            xlrange.Value = {txtmodel.Text, txtserialno.Text}
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            xlrange = xlWorksheet1.Range("G10")
            xlrange.Value = itprice
            xlrange.NumberFormat = "#,##0.00"

            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            xlrange = xlWorksheet1.Range("B11", "B15")
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            xlrange = xlWorksheet1.Range("E11", "G15")
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            xlrange = xlWorksheet1.Range("C11", "D11")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("C12", "D12")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("C13", "D13")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("C14", "D14")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("C15", "D15")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("C12")
            xlrange.Value = "Asset ID: " & value_assetid

            xlrange = xlWorksheet1.Range("C14")
            xlrange.Value = "Remarks : " & remarks

            xlrange = xlWorksheet1.Range("B16", "G16")
            xlrange.Merge()
            xlrange.Value = "Note: Once asset is received, the employee is expected to handle the item with utmost care and held"
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .Font.Italic = True
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B17", "G17")
            xlrange.Merge()
            xlrange.Value = "responsible and accountable on the item according to the affidavit signed."
            With xlrange
                .Font.Name = "Arial"
                .Font.Size = 11
                .Font.Italic = True
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B18", "G19")
            With xlrange
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            End With

            xlrange = xlWorksheet1.Range("B20", "B25")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous
                '.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.Constants.xlNone
                '.Borders(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.Constants.xlNone
            End With

            xlrange = xlWorksheet1.Range("D20", "D25")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("F20", "F25")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("G20", "G25")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B24", "G24")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B25", "G25")
            With xlrange
                .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
            End With

            xlrange = xlWorksheet1.Range("B20")
            xlrange.Value = "  Received By:                          Date"

            xlrange = xlWorksheet1.Range("B24")
            xlrange.Value = name_generate
            With xlrange
                .Font.Bold = True
            End With

            xlrange = xlWorksheet1.Range("D20")
            xlrange.Value = "  Approved By:"

            xlrange = xlWorksheet1.Range("D24")
            xlrange.Value = immediate_head
            With xlrange
                .Font.Bold = True
            End With


            xlrange = xlWorksheet1.Range("F20")
            xlrange.Value = "  Received By:"

            xlrange = xlWorksheet1.Range("F24")
            xlrange.Value = hrname
            With xlrange
                .Font.Bold = True
            End With


            xlrange = xlWorksheet1.Range("B25")
            xlrange.Value = "  Signature over Printed Name"

            xlrange = xlWorksheet1.Range("D25")
            xlrange.Value = "  Department Head"

            xlrange = xlWorksheet1.Range("F25")
            xlrange.Value = "  HR Admin"

            With xlWorksheet1.PageSetup
                .PrintHeadings = False
                .PrintGridlines = False
            End With

            file_exist_counter = 0

            'Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop


            Dim AppPath = Application.StartupPath

            filePath = AppPath & "/IT Asset Reports/" & PARno & "_AssetID_" & value_assetid & "_" & name_generate & " " & Format(Now, "MMddyyyy") & "_" & Format(Now, "HHmm") & ".xls"

            xlWorksheet1.PageSetup.PrintGridlines = False

            If System.IO.File.Exists(filePath) Then

                Do
                    file_exist_counter += 1
                    filePath = AppPath & "/IT Asset Reports/" & PARno & "_AssetID_" & value_assetid & "_" & name_generate & " " & Format(Now, "MMddyyyy") & "_" & Format(Now, "HHmm") & "(" & CStr(file_exist_counter) & ")" & ".xls"
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

    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
        If ListView1.Items.Count = 0 Then
        Else
            If e.KeyCode = Keys.Down Then
                ListView1.Items(0).Selected = True
                ListView1.Select()
            End If

        End If
    End Sub


    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        value_person = ListView1.SelectedItems.Item(0).Text
        supervisor = ListView1.SelectedItems(0).SubItems(2).Text

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmpInternalID ='" & ListView1.SelectedItems.Item(0).Text & "'")
            datareader = cmd.ExecuteReader
        End If

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

            End While
        End If
        conn.Close()
        ListView1.Visible = False

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & ListView1.SelectedItems(0).SubItems(2).Text & "'")
            datareader = cmd.ExecuteReader
        End If


        If datareader.HasRows Then
            While (datareader.Read)
                lblsupervisor.Text = datareader("fullname")
                lblsupervisor.Visible = True
            End While
        End If
        conn.Close()

        txtsearch.Text = ""
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            value_person = ListView1.SelectedItems.Item(0).Text
            supervisor = ListView1.SelectedItems(0).SubItems(2).Text

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT tbl_employeejobtitle.JobTitle,tbl_employeemaster.EmpExternalID,tbl_employeemaster.EmpInternalID,tbl_employeemaster.FName,tbl_employeemaster.MName,tbl_employeemaster.LName,tbl_distributorbranch.Branch, tbl_employeedept.Department, tbl_distributorcpc.CPC FROM tbl_employeemaster LEFT JOIN tbl_distributorbranch ON tbl_employeemaster.BranchCode=tbl_distributorbranch.BranchCode LEFT JOIN tbl_employeedept ON tbl_employeemaster.DeptID=tbl_employeedept.DeptID LEFT JOIN tbl_distributorcpc ON tbl_employeemaster.CPCID=tbl_distributorcpc.CPCID LEFT JOIN tbl_employeejobtitle ON tbl_employeemaster.JobTitleID=tbl_employeejobtitle.JobTitleID WHERE tbl_employeemaster.EmpInternalID ='" & ListView1.SelectedItems.Item(0).Text & "'")
                datareader = cmd.ExecuteReader
            End If

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

                End While
            End If
            conn.Close()
            ListView1.Visible = False

            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT CONCAT(FName,' ',MName,' ',LName) as fullname from tbl_employeemaster WHERE EmpInternalID='" & ListView1.SelectedItems(0).SubItems(2).Text & "'")
                datareader = cmd.ExecuteReader
            End If


            If datareader.HasRows Then
                While (datareader.Read)
                    lblsupervisor.Text = datareader("fullname")
                    lblsupervisor.Visible = True
                End While
            End If
            conn.Close()
            txtsearch.Text = ""
        End If


    End Sub

    Private Sub ListView1_KeyUp(sender As Object, e As KeyEventArgs) Handles ListView1.KeyUp
        If ListView1.Items(0).Selected = True Then
            If e.KeyCode = Keys.Up Then
                txtsearch.Select()
            End If
        End If

    End Sub



    Private Sub ListView4_DoubleClick(sender As Object, e As EventArgs) Handles ListView4.DoubleClick
        If ListView4.Items.Count <> 0 Then
            Frm_IT_UNPAR.MdiParent = Me.MdiParent
            Frm_IT_UNPAR.StartPosition = FormStartPosition.CenterScreen
            Frm_IT_UNPAR.Show()
        End If
    End Sub
End Class