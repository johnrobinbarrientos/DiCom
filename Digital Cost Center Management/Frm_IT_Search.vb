Imports Excel = Microsoft.Office.Interop.Excel
Public Class Frm_IT_Search
    Public value_assetid As String
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

    Private Sub Frm_search_IT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call initialize()
    End Sub

    Public Sub initialize()
        Dim isoperational As String

        txtsearch.Text = ""
        cbofilter.SelectedItem = "Description"

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If internet_connection = False Then
            MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
            Exit Sub
        Else
            ListView2.Items.Clear()
            ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID ORDER BY DateAdded DESC")
            datareader = cmd.ExecuteReader
        End If

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("AssetID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Type"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AssetDesc"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("DatePurchased"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Model"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ModelNo"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("SerialNo"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Price"))

                If datareader("IsOperational") = 1 Then
                    isoperational = "YES"
                Else
                    isoperational = "NO"
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(isoperational)

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Status"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Vendor"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("PG_Code"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Dim isoperational As String

        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If txtsearch.Text = "" Then
            ListView2.Items.Clear()
            ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID ORDER BY DateAdded DESC")
            datareader = cmd.ExecuteReader

        Else
            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ListView1.Visible = True

                If cbofilter.Text = "Description" Then

                    ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID WHERE AssetDesc LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
                    datareader = cmd.ExecuteReader

                ElseIf cbofilter.Text = "Asset ID" Then

                    ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID WHERE AssetID LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
                    datareader = cmd.ExecuteReader

                ElseIf cbofilter.Text = "Type" Then

                    ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID WHERE Type LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
                    datareader = cmd.ExecuteReader

                ElseIf cbofilter.Text = "Brand" Then

                    ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID WHERE Brand LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
                    datareader = cmd.ExecuteReader

                ElseIf cbofilter.Text = "Model No." Then

                    ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID WHERE ModelNo LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
                    datareader = cmd.ExecuteReader

                ElseIf cbofilter.Text = "Last Name" Then

                    ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID LEFT JOIN tbl_employeemaster ON tbl_itassetmaster.PAREmpInternalID=tbl_employeemaster.EmpInternalID WHERE LName LIKE '%" & txtsearch.Text.Replace("'", "''") & "%'")
                    datareader = cmd.ExecuteReader

                End If

                'ExecuteQuery("SELECT AssetID,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_itassetmaster.AssetDesc,tbl_itassetmaster.DatePurchased,tbl_itassetmaster.Model,tbl_itassetmaster.ModelNo,tbl_itassetmaster.SerialNo,tbl_itassetmaster.Price,tbl_itassetmaster.IsOperational,tbl_itassetstatus.`Status`,tbl_vendor.Vendor,tbl_itassetpgcode.PG_Code FROM tbl_itassetmaster LEFT JOIN tbl_assettype ON tbl_itassetmaster.TypeID=tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmaster.BrandID=tbl_assetbrand.BrandID LEFT JOIN tbl_itassetstatus ON tbl_itassetmaster.StatusID=tbl_itassetstatus.StatusID LEFT JOIN tbl_vendor ON tbl_itassetmaster.VendorCode=tbl_vendor.VendorCode LEFT JOIN tbl_itassetpgcode ON tbl_itassetmaster.AssetPG_ID=tbl_itassetpgcode.AssetPG_ID LEFT JOIN tbl_employeemaster ON tbl_itassetmaster.PAREmpInternalID=tbl_employeemaster.EmpInternalID WHERE (AssetID LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' OR AssetDesc LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' OR LName LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' OR Type LIKE '%" & txtsearch.Text.Replace("'", "''") & "%' OR ModelNo LIKE '%" & txtsearch.Text.Replace("'", "''") & "%')")
                'datareader = cmd.ExecuteReader
            End If

        End If

        ListView2.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView2.Items.Add(datareader("AssetID"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Type"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("AssetDesc"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("DatePurchased"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Model"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("ModelNo"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("SerialNo"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Price"))

                If datareader("IsOperational") = 1 Then
                    isoperational = "YES"
                Else
                    isoperational = "NO"
                End If

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(isoperational)

                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Status"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("Vendor"))
                ListView2.Items(ListView2.Items.Count - 1).SubItems.Add(datareader("PG_Code"))
            End While
        End If
        conn.Close()
    End Sub

    Private Sub ListView2_Click(sender As Object, e As EventArgs) Handles ListView2.Click
        Call Connection.checkconnection()
        internet_connection = Connection.internet_connection

        If ListView2.Items.Count <> 0 Then
            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT CONCAT(employeemasterA.LName,', ',employeemasterA.FName,' ',employeemasterA.MName) as empname,PARDate,PARType,tbl_itassetpar.Remarks,CONCAT(employeemasterB.LName,', ',employeemasterB.FName,' ',employeemasterB.MName) as processed_by,DateProcessed,CONCAT(employeemasterC.LName,', ',employeemasterC.FName,' ',employeemasterC.MName) as HRadmin,tbl_itassetmaster.AssetDesc FROM tbl_itassetpar LEFT JOIN tbl_itassetmaster ON tbl_itassetpar.AssetID = tbl_itassetmaster.AssetID LEFT JOIN tbl_employeemaster employeemasterA ON tbl_itassetpar.PAREmpInternalID =  employeemasterA.EmpInternalID LEFT JOIN tbl_employeemaster employeemasterB ON tbl_itassetpar.ProcessedBy = employeemasterB.EmpInternalID LEFT JOIN tbl_employeemaster employeemasterC ON tbl_itassetpar.ReceivedByHRAdmin = employeemasterC.EmpInternalID WHERE tbl_itassetpar.AssetID=" & ListView2.SelectedItems(0).Text & " ORDER BY DateProcessed DESC")
                datareader = cmd.ExecuteReader
            End If

        End If

        ListView1.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView1.Items.Add(datareader("empname"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("PARDate"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("PARType"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("Remarks"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("processed_by"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("DateProcessed"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("HRadmin"))
                ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(datareader("AssetDesc"))
            End While
        End If
        conn.Close()



        If ListView2.Items.Count <> 0 Then
            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else
                ExecuteQuery("SELECT Assetmasterhistorytype,AssetDesc,Model,ModelNo,SerialNo,IMEINo,ChargerSerialNo,IsSFAOperational,SimCardNo,IvyActivationKey,SFARemarks,AppInstalled,OptAppInstalled,DatePurchased,Qty,Price,Lifespan,Remarks,Capacity,LMACAdd,WMACAdd,LANIPAdd,WANIPAdd,OperatingSystem,OSLicenseKey,MSOffice,MSOfficeLicenseKey,AntiVirus,AntiVirusLicenseKey,TeamViewerID,IsOperational,IsTag,tbl_itassetpgcode.PG_Code,tbl_assettype.Type,tbl_assetbrand.Brand,tbl_vendor.Vendor,tbl_itassetstatus.`Status`,CONCAT(tbl_employeemaster.LName,', ',tbl_employeemaster.FName,' ',tbl_employeemaster.MName) as updatedbyemp,DateUpdated FROM tbl_itassetmasterhistory LEFT JOIN tbl_itassetpgcode ON tbl_itassetmasterhistory.AssetPG_ID = tbl_itassetpgcode.AssetPG_ID LEFT JOIN tbl_assettype ON tbl_itassetmasterhistory.TypeID = tbl_assettype.TypeID LEFT JOIN tbl_assetbrand ON tbl_itassetmasterhistory.BrandID = tbl_assetbrand.BrandID  LEFT JOIN tbl_vendor ON tbl_itassetmasterhistory.VendorCode = tbl_vendor.VendorCode LEFT JOIN tbl_itassetstatus ON tbl_itassetmasterhistory.StatusID = tbl_itassetstatus.StatusID LEFT JOIN tbl_employeemaster ON tbl_itassetmasterhistory.UpdatedBy = tbl_employeemaster.EmpInternalID WHERE tbl_itassetmasterhistory.AssetID=" & ListView2.SelectedItems(0).Text & "")
                datareader = cmd.ExecuteReader
            End If

        End If

        ListView3.Items.Clear()

        If datareader.HasRows Then
            While (datareader.Read)
                ListView3.Items.Add(datareader("Assetmasterhistorytype"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("AssetDesc"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Model"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("ModelNo"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("SerialNo"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("IMEINo"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("ChargerSerialNo"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Qty"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Price"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Lifespan"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Capacity"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Type"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Brand"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Vendor"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("PG_Code"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("DatePurchased"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("LMACAdd"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("WMACAdd"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("LANIPAdd"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("WANIPAdd"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("OperatingSystem"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("OSLicenseKey"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("MSOffice"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("MSOfficeLicenseKey"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("AntiVirus"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("AntiVirusLicenseKey"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("AppInstalled"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("OptAppInstalled"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("TeamViewerID"))

                If datareader("IsOperational") = 1 Then
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("YES")
                Else
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("NO")
                End If

                If datareader("IsTag") = 1 Then
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("YES")
                Else
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("NO")
                End If

                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Status"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("Remarks"))

                If datareader("IsSFAOperational") = 1 Then
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("YES")
                ElseIf datareader("IsSFAOperational") = 0 Then
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("NO")
                ElseIf datareader("IsSFAOperational") = 2 Then
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("N/A")
                Else
                    ListView3.Items(ListView3.Items.Count - 1).SubItems.Add("")
                End If

                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("SimCardNo"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("SFARemarks"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("IvyActivationKey"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("updatedbyemp"))
                ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(datareader("DateUpdated"))

            End While
        End If
        conn.Close()


    End Sub


    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.SelectedItems.Count = 0 Then

            MessageBox.Show("Please Select Asset")
        Else


            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else

                value_assetid = ListView2.SelectedItems(0).Text
                Frm_IT_PAR.MdiParent = Me.MdiParent
                Frm_IT_PAR.StartPosition = FormStartPosition.CenterScreen
                Frm_IT_PAR.Show()
            End If
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("No History Found")
        Else
            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

            Dim xlWorkbook As Excel.Workbook
            Dim xlWorksheet1 As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlrange As Excel.Range
            Dim counter, maximum_data, file_exist_counter, i As Integer
            Dim filePath As String


            xlWorkbook = xlApp.Workbooks.Add(misValue)
            xlWorksheet1 = xlWorkbook.Sheets("sheet1")


            xlrange = xlWorksheet1.Range("A1", "H1")
            xlrange.Font.Bold = True
            xlrange.Font.Color = Color.White
            xlrange.Interior.ColorIndex = 49
            xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            xlrange.ColumnWidth = 20
            xlrange.Value = {"Employee Name", "PAR Date", "PAR Type", "Remarks", "Processed By", "Date Processed", "Recieved By HR", "Asset Description"}
            xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

            xlWorkbook.Application.Visible = False

            MDIParent1.ToolStripProgressBar1.Visible = True
            MDIParent1.ToolStripStatusLabel5.Visible = True
            MDIParent1.ToolStripStatusLabel5.Text = "Downloading..."
            maximum_data = ListView1.Items.Count

            counter = 2
            For i = 0 To ListView1.Items.Count - 1
                xlWorksheet1.Cells(counter, 1) = ListView1.Items.Item(i).Text.ToString
                xlWorksheet1.Cells(counter, 2) = ListView1.Items.Item(i).SubItems(1).Text
                xlWorksheet1.Cells(counter, 3) = ListView1.Items.Item(i).SubItems(2).Text
                xlWorksheet1.Cells(counter, 4) = ListView1.Items.Item(i).SubItems(3).Text
                xlWorksheet1.Cells(counter, 5) = ListView1.Items.Item(i).SubItems(4).Text
                xlWorksheet1.Cells(counter, 6) = ListView1.Items.Item(i).SubItems(5).Text
                xlWorksheet1.Cells(counter, 7) = ListView1.Items.Item(i).SubItems(6).Text
                xlWorksheet1.Cells(counter, 8) = ListView1.Items.Item(i).SubItems(7).Text

                MDIParent1.ToolStripProgressBar1.Value = (i / maximum_data) * 100
                MDIParent1.ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"

                counter = counter + 1
            Next

            MDIParent1.ToolStripProgressBar1.Visible = False
            MDIParent1.ToolStripStatusLabel5.Visible = False

            file_exist_counter = 0

            'Dim AppPath = Application.StartupPath
            Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop

            filePath = desktopPath & "/IT Asset Reports/" & "PAR UNPAR History_" & ListView1.Items(0).SubItems(7).Text & ".xls"

            If System.IO.File.Exists(filePath) Then

                Do
                    file_exist_counter += 1
                    filePath = desktopPath & "/IT Asset Reports/" & "PAR UNPAR History_" & ListView1.Items(0).SubItems(7).Text & "(" & CStr(file_exist_counter) & ")" & ".xls"
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

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If ListView3.Items.Count = 0 Then
            MessageBox.Show("No History Found")
        Else
            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

            Dim xlWorkbook As Excel.Workbook
            Dim xlWorksheet1 As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlrange As Excel.Range
            Dim counter, file_exist_counter, maximum_data, i As Integer
            Dim filePath As String


            xlWorkbook = xlApp.Workbooks.Add(misValue)
            xlWorksheet1 = xlWorkbook.Sheets("sheet1")


            xlrange = xlWorksheet1.Range("A1", "AM1")
            xlrange.Font.Bold = True
            xlrange.Font.Color = Color.White
            xlrange.Interior.ColorIndex = 49
            xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            xlrange.ColumnWidth = 20
            xlrange.Value = {"History Type", "Asset Description", "Model", "Model No.", "Serial No.", "IMEI No.", "Charger Serial No.", "Qtty", "Price", "Lifespan", "Capacity Details", "Type", "Brand", "Vendor", "P&G Code", "Date Purchase", "LAN MAC Address", "WAN MAC Address", "LAN IP Address", "WAN IP Address", "Operating System", "OS License Key", "MS Office", "MS Office Key", "AntiVirus", "AntiVirus Key", "Application Installed", "Optional Application Installed", "Team Viewer ID", "Is Operational", "Is Barcode Tag", "Status", "Remarks", "Is Operational", "Sim Card No.", "SFA Remarks", "Ivy Activation Key", "Updated By", "Date Added/Updated"}
            xlrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

            xlWorkbook.Application.Visible = False
            MDIParent1.ToolStripProgressBar1.Visible = True
            MDIParent1.ToolStripStatusLabel5.Visible = True
            MDIParent1.ToolStripStatusLabel5.Text = "Downloading..."
            maximum_data = ListView3.Items.Count

            counter = 2
            For i = 0 To ListView3.Items.Count - 1
                xlWorksheet1.Cells(counter, 1) = ListView3.Items.Item(i).Text.ToString
                xlWorksheet1.Cells(counter, 2) = ListView3.Items.Item(i).SubItems(1).Text
                xlWorksheet1.Cells(counter, 3) = ListView3.Items.Item(i).SubItems(2).Text
                xlWorksheet1.Cells(counter, 4) = ListView3.Items.Item(i).SubItems(3).Text
                xlWorksheet1.Cells(counter, 5) = ListView3.Items.Item(i).SubItems(4).Text
                xlWorksheet1.Cells(counter, 6) = ListView3.Items.Item(i).SubItems(5).Text
                xlWorksheet1.Cells(counter, 7) = ListView3.Items.Item(i).SubItems(6).Text
                xlWorksheet1.Cells(counter, 8) = ListView3.Items.Item(i).SubItems(7).Text
                xlWorksheet1.Cells(counter, 9) = ListView3.Items.Item(i).SubItems(8).Text
                xlWorksheet1.Cells(counter, 10) = ListView3.Items.Item(i).SubItems(9).Text
                xlWorksheet1.Cells(counter, 11) = ListView3.Items.Item(i).SubItems(10).Text
                xlWorksheet1.Cells(counter, 12) = ListView3.Items.Item(i).SubItems(11).Text
                xlWorksheet1.Cells(counter, 13) = ListView3.Items.Item(i).SubItems(12).Text
                xlWorksheet1.Cells(counter, 14) = ListView3.Items.Item(i).SubItems(13).Text
                xlWorksheet1.Cells(counter, 15) = ListView3.Items.Item(i).SubItems(14).Text
                xlWorksheet1.Cells(counter, 16) = ListView3.Items.Item(i).SubItems(15).Text
                xlWorksheet1.Cells(counter, 17) = ListView3.Items.Item(i).SubItems(16).Text
                xlWorksheet1.Cells(counter, 18) = ListView3.Items.Item(i).SubItems(17).Text
                xlWorksheet1.Cells(counter, 19) = ListView3.Items.Item(i).SubItems(18).Text
                xlWorksheet1.Cells(counter, 20) = ListView3.Items.Item(i).SubItems(19).Text
                xlWorksheet1.Cells(counter, 21) = ListView3.Items.Item(i).SubItems(20).Text
                xlWorksheet1.Cells(counter, 22) = ListView3.Items.Item(i).SubItems(21).Text
                xlWorksheet1.Cells(counter, 23) = ListView3.Items.Item(i).SubItems(22).Text
                xlWorksheet1.Cells(counter, 24) = ListView3.Items.Item(i).SubItems(23).Text
                xlWorksheet1.Cells(counter, 25) = ListView3.Items.Item(i).SubItems(24).Text
                xlWorksheet1.Cells(counter, 26) = ListView3.Items.Item(i).SubItems(25).Text
                xlWorksheet1.Cells(counter, 27) = ListView3.Items.Item(i).SubItems(26).Text
                xlWorksheet1.Cells(counter, 28) = ListView3.Items.Item(i).SubItems(27).Text
                xlWorksheet1.Cells(counter, 29) = ListView3.Items.Item(i).SubItems(28).Text
                xlWorksheet1.Cells(counter, 30) = ListView3.Items.Item(i).SubItems(29).Text
                xlWorksheet1.Cells(counter, 31) = ListView3.Items.Item(i).SubItems(30).Text
                xlWorksheet1.Cells(counter, 32) = ListView3.Items.Item(i).SubItems(31).Text
                xlWorksheet1.Cells(counter, 33) = ListView3.Items.Item(i).SubItems(32).Text
                xlWorksheet1.Cells(counter, 34) = ListView3.Items.Item(i).SubItems(33).Text
                xlWorksheet1.Cells(counter, 35) = ListView3.Items.Item(i).SubItems(34).Text
                xlWorksheet1.Cells(counter, 36) = ListView3.Items.Item(i).SubItems(35).Text
                xlWorksheet1.Cells(counter, 37) = ListView3.Items.Item(i).SubItems(36).Text
                xlWorksheet1.Cells(counter, 38) = ListView3.Items.Item(i).SubItems(37).Text
                xlWorksheet1.Cells(counter, 39) = ListView3.Items.Item(i).SubItems(38).Text

                MDIParent1.ToolStripProgressBar1.Value = (i / maximum_data) * 100
                MDIParent1.ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"


                counter = counter + 1
            Next

            MDIParent1.ToolStripProgressBar1.Visible = False
            MDIParent1.ToolStripStatusLabel5.Visible = False


            file_exist_counter = 0

            'Dim AppPath = Application.StartupPath

            Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop
            filePath = desktopPath & "/IT Asset Reports/" & "IT UPDATE History_" & ListView3.Items(0).SubItems(1).Text & ".xls"

            If System.IO.File.Exists(filePath) Then

                Do
                    file_exist_counter += 1
                    filePath = desktopPath & "/IT Asset Reports/" & "IT UPDATE History_" & ListView3.Items(0).SubItems(1).Text & "(" & CStr(file_exist_counter) & ")" & ".xls"
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

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If ListView2.SelectedItems.Count = 0 Then

            MessageBox.Show("Please Select Asset")
        Else


            Call Connection.checkconnection()
            internet_connection = Connection.internet_connection

            If internet_connection = False Then
                MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                Exit Sub
            Else

                value_assetid = ListView2.SelectedItems(0).Text
                Frm_IT_Add.MdiParent = Me.MdiParent
                Frm_IT_Add.StartPosition = FormStartPosition.CenterScreen
                Frm_IT_Add.Show()
            End If
        End If
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles PictureBox4.MouseHover
        Label2.Visible = True
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        Label2.Visible = False
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        If ListView2.Items.Count = 0 Then
            MessageBox.Show("No History Found")
        Else
            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application

            Dim xlWorkbook As Excel.Workbook
            Dim xlWorksheet1 As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim xlrange As Excel.Range
            Dim counter, maximum_data, file_exist_counter, i As Integer
            Dim filePath As String


            xlWorkbook = xlApp.Workbooks.Add(misValue)
            xlWorksheet1 = xlWorkbook.Sheets("sheet1")


            xlrange = xlWorksheet1.Range("A1", "M1")
            xlrange.Font.Bold = True
            xlrange.Font.Color = Color.White
            xlrange.Interior.ColorIndex = 49
            xlrange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            xlrange.ColumnWidth = 20
            xlrange.Value = {"Asset ID", "Type", "Brand", "Asset Description", "Date Purchased", "Model", "Model No.", "Serial No.", "Price", "Is Operational", "Status", "Vendor", "P&G Code"}
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

                MDIParent1.ToolStripProgressBar1.Value = (i / maximum_data) * 100
                MDIParent1.ToolStripStatusLabel5.Text = "Downloading..." & CStr(Math.Round((i / maximum_data) * 100, 0)) & "%"

                counter = counter + 1
            Next

            MDIParent1.ToolStripProgressBar1.Visible = False
            MDIParent1.ToolStripStatusLabel5.Visible = False

            file_exist_counter = 0

            'Dim AppPath = Application.StartupPath
            Dim desktopPath = My.Computer.FileSystem.SpecialDirectories.Desktop

            filePath = desktopPath & "/IT Asset Reports/" & "IT Asset" & ".xls"

            If System.IO.File.Exists(filePath) Then

                Do
                    file_exist_counter += 1
                    filePath = desktopPath & "/IT Asset Reports/" & "IT Asset" & "(" & CStr(file_exist_counter) & ")" & ".xls"
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
End Class