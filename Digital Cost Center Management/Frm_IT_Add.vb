Public Class Frm_IT_Add
    Dim seqno, userinternalID As Integer
    Dim assetid, value_assetid, seqno_result As String
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

    Private Sub Frm_add_IT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim installedapp, optinstalledapp, arrayinstalledapp(), arrayoptinstalledapp() As String

        userinternalID = Login.userinternalID
        value_assetid = Frm_IT_Search.value_assetid

        installedapp = ""
        optinstalledapp = ""


        If value_assetid = "" Then
            Call initialize()
        Else
            Dim TypeID_value, BrandID_value, VendorCode_value, AssetPG_ID_value, StatusID_value As Integer
            Dim OperatingSystem_value, MSOffice_value, AntiVirus As String

            OperatingSystem_value = ""
            MSOffice_value = ""
            AntiVirus = ""

            lblform_name.Text = "Update IT Equipment"
            bttnsave.Text = "UPDATE"

            lblassetid.Text = value_assetid
            assetid = value_assetid

            ExecuteQuery("SELECT * FROM tbl_itassetmaster WHERE AssetID=" & value_assetid & "")
            datareader = cmd.ExecuteReader

            If datareader.HasRows Then
                While (datareader.Read)
                    txtdescription.Text = datareader("AssetDesc")
                    txtmodel.Text = datareader("Model")
                    txtmodelno.Text = datareader("ModelNo")
                    txtserialno.Text = datareader("SerialNo")

                    seqno = datareader("SeqNo")

                    If IsDBNull(datareader("IMEINo")) Then
                        txtimeino.Text = ""
                    Else
                        txtimeino.Text = datareader("IMEINo")
                    End If

                    If IsDBNull(datareader("ChargerSerialNo")) Then
                        txtchargerserialno.Text = ""
                    Else
                        txtchargerserialno.Text = datareader("ChargerSerialNo")
                    End If


                    txtprice.Text = datareader("Price")
                    txtqtty.Text = datareader("Qty")
                    txtlifespan.Text = datareader("Lifespan")
                    txtcapacitydetails.Text = datareader("Capacity")

                    TypeID_value = datareader("TypeID")
                    BrandID_value = datareader("BrandID")
                    VendorCode_value = datareader("VendorCode")
                    AssetPG_ID_value = datareader("AssetPG_ID")

                    date_purchase.Value = datareader("DatePurchased")

                    txtlanmac.Text = datareader("LMACAdd")
                    txtwanmac.Text = datareader("WMACAdd")
                    txtlanip.Text = datareader("LANIPAdd")
                    txtwanip.Text = datareader("WANIPAdd")

                    OperatingSystem_value = datareader("OperatingSystem")
                    MSOffice_value = datareader("MSOffice")
                    AntiVirus = datareader("AntiVirus")

                    txtoslicensekey.Text = datareader("OSLicenseKey")
                    txtmsofficekey.Text = datareader("MSOfficeLicenseKey")
                    txtantiviruskey.Text = datareader("AntiVirusLicenseKey")

                    If IsDBNull(datareader("AppInstalled")) Then
                        installedapp = ""
                    Else
                        installedapp = datareader("AppInstalled")
                    End If

                    If IsDBNull(datareader("OptAppInstalled")) Then
                        optinstalledapp = ""
                    Else
                        optinstalledapp = datareader("OptAppInstalled")
                    End If

                    txtteamviewerid.Text = datareader("TeamViewerID")

                    If datareader("IsOperational") = 1 Then
                        cbooperational.SelectedItem = "YES"
                    Else
                        cbooperational.SelectedItem = "NO"
                    End If

                    If datareader("IsTag") = 1 Then
                        cbobarcodetag.SelectedItem = "YES"
                    Else
                        cbobarcodetag.SelectedItem = "NO"
                    End If

                    StatusID_value = datareader("StatusID")
                    txtremarks.Text = datareader("Remarks")


                    If IsDBNull(datareader("IsSFAOperational")) Then
                        cbosfaoperational.SelectedItem = -1
                    Else
                        If datareader("IsSFAOperational") = 1 Then
                            cbosfaoperational.SelectedItem = "YES"
                        ElseIf datareader("IsSFAOperational") = 0 Then
                            cbosfaoperational.SelectedItem = "NO"
                        ElseIf datareader("IsSFAOperational") = 2 Then
                            cbosfaoperational.SelectedItem = "N/A"
                        End If
                    End If


                    If IsDBNull(datareader("SimCardNo")) Then
                        txtsimcardno.Text = ""
                    Else
                        txtsimcardno.Text = datareader("SimCardNo")
                    End If

                    If IsDBNull(datareader("IvyActivationKey")) Then
                        txtivyactivationkey.Text = ""
                    Else
                        txtivyactivationkey.Text = datareader("IvyActivationKey")
                    End If

                    If IsDBNull(datareader("SFARemarks")) Then
                        txtsfaremarks.Text = ""
                    Else
                        txtsfaremarks.Text = datareader("SFARemarks")
                    End If

                End While
            End If
            conn.Close()

            arrayinstalledapp = Split(installedapp, ",")

            Dim table_software As New DataTable

            CheckedListBox1.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_itassetsoftware")
            datareader = cmd.ExecuteReader
            table_software.Load(datareader)

            If table_software.Rows.Count > 0 Then
                For i As Integer = 0 To table_software.Rows.Count - 1
                    CheckedListBox1.Items.Add(CStr(table_software.Rows(i).Item(1)), False)
                Next
            End If
            conn.Close()


            For Each word In arrayinstalledapp

                For x As Integer = 0 To CheckedListBox1.Items.Count - 1
                    If CheckedListBox1.Items(x).ToString = word Then
                        CheckedListBox1.SetItemChecked(x, True)
                    End If
                Next
            Next


            arrayoptinstalledapp = Split(optinstalledapp, ",")

            Dim table_softwareoptional As New DataTable

            CheckedListBox2.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_itassetsoftwareoptional")
            datareader = cmd.ExecuteReader
            table_softwareoptional.Load(datareader)

            If table_softwareoptional.Rows.Count > 0 Then
                For i As Integer = 0 To table_softwareoptional.Rows.Count - 1
                    CheckedListBox2.Items.Add(CStr(table_softwareoptional.Rows(i).Item(1)), False)
                Next
            End If
            conn.Close()


            For Each word In arrayoptinstalledapp

                For x As Integer = 0 To CheckedListBox2.Items.Count - 1
                    If CheckedListBox2.Items(x).ToString = word Then
                        CheckedListBox2.SetItemChecked(x, True)
                    End If
                Next
            Next

            Dim table_type As New DataTable
            ExecuteQuery("SELECT * FROM tbl_assettype")
            datareader = cmd.ExecuteReader
            table_type.Load(datareader)
            cbotype.DisplayMember = "Type"
            cbotype.ValueMember = "TypeID"
            cbotype.DataSource = table_type
            cbotype.SelectedValue = TypeID_value
            conn.Close()

            Dim table_brand As New DataTable

            ExecuteQuery("SELECT * FROM tbl_assetbrand")
            datareader = cmd.ExecuteReader
            table_brand.Load(datareader)
            cbobrand.DisplayMember = "Brand"
            cbobrand.ValueMember = "BrandID"
            cbobrand.DataSource = table_brand
            cbobrand.SelectedValue = BrandID_value
            conn.Close()


            Dim table_vendor As New DataTable

            ExecuteQuery("SELECT * FROM tbl_vendor")
            datareader = cmd.ExecuteReader
            table_vendor.Load(datareader)
            cbovendor.DisplayMember = "Vendor"
            cbovendor.ValueMember = "VendorCode"
            cbovendor.DataSource = table_vendor
            cbovendor.SelectedValue = VendorCode_value
            conn.Close()

            Dim table_pgcode As New DataTable

            ExecuteQuery("SELECT * FROM tbl_itassetpgcode")
            datareader = cmd.ExecuteReader
            table_pgcode.Load(datareader)
            cbopgcode.DisplayMember = "PG_Code"
            cbopgcode.ValueMember = "AssetPG_ID"
            cbopgcode.DataSource = table_pgcode
            cbopgcode.SelectedValue = AssetPG_ID_value
            conn.Close()

            Dim table_status As New DataTable

            ExecuteQuery("SELECT * FROM tbl_itassetstatus")
            datareader = cmd.ExecuteReader
            table_status.Load(datareader)
            cbostatus.DisplayMember = "Status"
            cbostatus.ValueMember = "StatusID"
            cbostatus.DataSource = table_status
            cbostatus.SelectedValue = StatusID_value
            conn.Close()

            cboos.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_itassetos")
            datareader = cmd.ExecuteReader
            cboos.Items.Add("")
            If datareader.HasRows Then
                While (datareader.Read)
                    cboos.Items.Add(datareader("OperatingSytem"))
                End While
            End If
            conn.Close()

            cboos.SelectedItem = OperatingSystem_value

            cbooffice.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_itassetoffice")
            datareader = cmd.ExecuteReader
            cbooffice.Items.Add("")
            If datareader.HasRows Then
                While (datareader.Read)
                    cbooffice.Items.Add(datareader("Office"))
                End While
            End If
            conn.Close()

            cbooffice.SelectedItem = MSOffice_value

            cboantivirus.Items.Clear()
            ExecuteQuery("SELECT * FROM tbl_itassetantivirus")
            datareader = cmd.ExecuteReader
            cboantivirus.Items.Add("")
            If datareader.HasRows Then
                While (datareader.Read)
                    cboantivirus.Items.Add(datareader("AntiVirus"))
                End While
            End If
            conn.Close()

            cboantivirus.SelectedItem = AntiVirus

        End If

    End Sub

    Private Sub initialize()
        txtdescription.Text = ""
        txtmodel.Text = ""
        txtmodelno.Text = ""
        txtserialno.Text = ""
        txtimeino.Text = ""
        txtchargerserialno.Text = ""
        txtprice.Text = ""
        txtqtty.Text = ""
        txtlifespan.Text = ""
        txtcapacitydetails.Text = ""
        txtlanmac.Text = ""
        txtwanmac.Text = ""
        txtlanip.Text = ""
        txtwanip.Text = ""
        txtoslicensekey.Text = ""
        txtmsofficekey.Text = ""
        txtantiviruskey.Text = ""
        txtteamviewerid.Text = ""
        txtremarks.Text = ""
        txtsimcardno.Text = ""
        txtivyactivationkey.Text = ""
        txtsfaremarks.Text = ""
        date_purchase.Value = Now
        cbooperational.SelectedItem = ""
        cbobarcodetag.SelectedItem = ""
        cbosfaoperational.SelectedItem = ""

        ExecuteQuery("SELECT SeqNo FROM tbl_itassetmaster ORDER BY SeqNo DESC")
        seqno = cmd.ExecuteScalar
        conn.Close()

        If CStr(seqno + 1).Length = 1 Then
            seqno_result = "0000" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 2 Then
            seqno_result = "000" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 3 Then
            seqno_result = "00" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 4 Then
            seqno_result = "0" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 5 Then
            seqno_result = CStr(seqno + 1)
        End If

        assetid = Format(date_purchase.Value, "yyyy") + Format(date_purchase.Value, "MM") + Format(date_purchase.Value, "dd") + seqno_result

        lblassetid.Text = assetid

        Dim table_type As New DataTable
        Dim row_type As DataRow = table_type.NewRow

        ExecuteQuery("SELECT * FROM tbl_assettype")
        datareader = cmd.ExecuteReader
        table_type.Load(datareader)
        cbotype.DisplayMember = "Type"
        cbotype.ValueMember = "TypeID"
        cbotype.DataSource = table_type
        conn.Close()

        row_type("Type") = ""
        row_type("TypeID") = -1
        table_type.Rows.Add(row_type)
        table_type.DefaultView.Sort = "Type ASC"
        table_type = table_type.DefaultView.ToTable



        Dim table_brand As New DataTable
        Dim row_brand As DataRow = table_brand.NewRow

        ExecuteQuery("SELECT * FROM tbl_assetbrand")
        datareader = cmd.ExecuteReader
        table_brand.Load(datareader)
        cbobrand.DisplayMember = "Brand"
        cbobrand.ValueMember = "BrandID"
        cbobrand.DataSource = table_brand
        conn.Close()

        row_brand("Brand") = ""
        row_brand("BrandID") = -1
        table_brand.Rows.Add(row_brand)
        table_brand.DefaultView.Sort = "Brand ASC"
        table_brand = table_brand.DefaultView.ToTable


        Dim table_vendor As New DataTable
        Dim row_vendor As DataRow = table_vendor.NewRow

        ExecuteQuery("SELECT * FROM tbl_vendor")
        datareader = cmd.ExecuteReader
        table_vendor.Load(datareader)
        cbovendor.DisplayMember = "Vendor"
        cbovendor.ValueMember = "VendorCode"
        cbovendor.DataSource = table_vendor
        conn.Close()

        row_vendor("Vendor") = ""
        row_vendor("VendorCode") = -1
        table_vendor.Rows.Add(row_vendor)
        table_vendor.DefaultView.Sort = "Vendor ASC"
        table_vendor = table_vendor.DefaultView.ToTable


        Dim table_pgcode As New DataTable
        Dim row_pgcode As DataRow = table_pgcode.NewRow

        ExecuteQuery("SELECT * FROM tbl_itassetpgcode")
        datareader = cmd.ExecuteReader
        table_pgcode.Load(datareader)
        cbopgcode.DisplayMember = "PG_Code"
        cbopgcode.ValueMember = "AssetPG_ID"
        cbopgcode.DataSource = table_pgcode
        conn.Close()

        row_pgcode("PG_Code") = ""
        row_pgcode("AssetPG_ID") = -1
        table_pgcode.Rows.Add(row_pgcode)
        table_pgcode.DefaultView.Sort = "PG_Code ASC"
        table_pgcode = table_pgcode.DefaultView.ToTable


        Dim table_software As New DataTable

        CheckedListBox1.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetsoftware")
        datareader = cmd.ExecuteReader
        table_software.Load(datareader)

        If table_software.Rows.Count > 0 Then
            For i As Integer = 0 To table_software.Rows.Count - 1
                CheckedListBox1.Items.Add(CStr(table_software.Rows(i).Item(1)), False)
            Next
        End If
        conn.Close()

        Dim table_softwareoptional As New DataTable

        CheckedListBox2.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetsoftwareoptional")
        datareader = cmd.ExecuteReader
        table_softwareoptional.Load(datareader)

        If table_softwareoptional.Rows.Count > 0 Then
            For i As Integer = 0 To table_softwareoptional.Rows.Count - 1
                CheckedListBox2.Items.Add(CStr(table_softwareoptional.Rows(i).Item(1)), False)
            Next
        End If
        conn.Close()



        Dim table_status As New DataTable
        Dim row_status As DataRow = table_status.NewRow

        ExecuteQuery("SELECT * FROM tbl_itassetstatus")
        datareader = cmd.ExecuteReader
        table_status.Load(datareader)
        cbostatus.DisplayMember = "Status"
        cbostatus.ValueMember = "StatusID"
        cbostatus.DataSource = table_status
        conn.Close()

        row_status("Status") = ""
        row_status("StatusID") = -1
        table_status.Rows.Add(row_status)
        table_status.DefaultView.Sort = "Status ASC"
        table_status = table_status.DefaultView.ToTable


        cboos.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetos")
        datareader = cmd.ExecuteReader
        cboos.Items.Add("")
        If datareader.HasRows Then
            While (datareader.Read)
                cboos.Items.Add(datareader("OperatingSytem"))
            End While
        End If
        conn.Close()

        cbooffice.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetoffice")
        datareader = cmd.ExecuteReader
        cbooffice.Items.Add("")
        If datareader.HasRows Then
            While (datareader.Read)
                cbooffice.Items.Add(datareader("Office"))
            End While
        End If
        conn.Close()

        cboantivirus.Items.Clear()
        ExecuteQuery("SELECT * FROM tbl_itassetantivirus")
        datareader = cmd.ExecuteReader
        cboantivirus.Items.Add("")
        If datareader.HasRows Then
            While (datareader.Read)
                cboantivirus.Items.Add(datareader("AntiVirus"))
            End While
        End If
        conn.Close()


    End Sub

    Private Sub trap()
        Dim num1 As Integer
        Dim num2 As Single

        If txtdescription.Text = "" Then
            MessageBox.Show("Please Enter Asset Description")
            txtdescription.Select()
        ElseIf txtmodel.Text = "" Then
            MessageBox.Show("Please Enter Model")
            txtmodel.Select()
        ElseIf txtmodelno.Text = "" Then
            MessageBox.Show("Please Enter Model No.")
            txtmodelno.Select()
        ElseIf txtserialno.Text = "" Then
            MessageBox.Show("Please Enter Serial No.")
            txtserialno.Select()
        ElseIf txtimeino.Text = "" Then
            MessageBox.Show("Please Enter IMEI No.")
            txtimeino.Select()
        ElseIf txtchargerserialno.Text = "" Then
            MessageBox.Show("Please Enter Charger Serial No.")
            txtchargerserialno.Select()
        ElseIf Not Single.TryParse(txtprice.Text, num2) Then
            MessageBox.Show("Price should be number")
            txtprice.Select()
        ElseIf Not Integer.TryParse(txtqtty.Text, num1) Then
            MessageBox.Show("Qtty should be number")
            txtqtty.Select()
        ElseIf txtlifespan.Text = "" Then
            MessageBox.Show("Please Enter Charger Lifespan")
            txtlifespan.Select()
        ElseIf txtcapacitydetails.Text = "" Then
            MessageBox.Show("Please Enter Capacity Details")
            txtcapacitydetails.Select()
        ElseIf cbotype.text = "" Then
            MessageBox.Show("Please Select Type")
            cbotype.Select()
        ElseIf cbobrand.text = "" Then
            MessageBox.Show("Please Select Brand")
            cbobrand.Select()
        ElseIf cbovendor.text = "" Then
            MessageBox.Show("Please Select Vendor")
            cbovendor.Select()
        ElseIf cbopgcode.text = "" Then
            MessageBox.Show("Please Select P&G Code")
            cbopgcode.Select()
        ElseIf cbostatus.text = "" Then
            MessageBox.Show("Please Select Status")
            cbostatus.Select()
        ElseIf cbosfaoperational.text = "" Then
            MessageBox.Show("Please Select Is SFA Operational")
            cbosfaoperational.Select()
        ElseIf cbooperational.text = "" Then
            MessageBox.Show("Please Select Is Operational")
            cbooperational.Select()
        ElseIf cbobarcodetag.text = "" Then
            MessageBox.Show("Please Select Is Barcode Tag")
            cbobarcodetag.Select()
        Else
            Call save()
        End If

    End Sub

    Private Sub save()
        Dim operational_id, sfaoperational_id, barcodetag_id As Integer
        Dim itemCheckedinstalled, itemCheckedoptinstalled As Object
        Dim installedapp, optinstalledapp As String

        If cbooperational.SelectedItem = "YES" Then
            operational_id = 1
        Else
            operational_id = 0
        End If

        If cbosfaoperational.SelectedItem = "YES" Then
            sfaoperational_id = 1
        ElseIf cbosfaoperational.SelectedItem = "NO" Then
            sfaoperational_id = 0
        Else
            sfaoperational_id = 2
        End If

        If cbobarcodetag.SelectedItem = "YES" Then
            barcodetag_id = 1
        Else
            barcodetag_id = 0
        End If

        installedapp = ""
        For Each itemCheckedinstalled In CheckedListBox1.CheckedItems
            If installedapp = "" Then
                installedapp += itemCheckedinstalled.ToString
            Else
                installedapp += "," + itemCheckedinstalled.ToString
            End If
        Next

        optinstalledapp = ""
        For Each itemCheckedoptinstalled In CheckedListBox2.CheckedItems
            If optinstalledapp = "" Then
                optinstalledapp += itemCheckedoptinstalled.ToString
            Else
                optinstalledapp += "," + itemCheckedoptinstalled.ToString
            End If
        Next

        If bttnsave.Text = "SAVE" Then
            Dim n As String = MsgBox("Add New Record?", MsgBoxStyle.YesNo, "")

            If n = vbYes Then

                Call Connection.checkconnection()
                internet_connection = Connection.internet_connection

                If internet_connection = False Then

                    MessageBox.Show("System Offline, Can't Connect to Server. Please Check your Internet Connection")
                    Exit Sub

                Else
                    ExecuteQuery("INSERT INTO tbl_itassetmaster (AssetID,AssetDesc,Model,ModelNo,SerialNo,IMEINo,ChargerSerialNo,Price,Qty,Lifespan,Capacity,TypeID,BrandID,VendorCode,AssetPG_ID,DatePurchased,LMACAdd,WMACAdd,LANIPAdd,WANIPAdd,TeamViewerID,IsOperational,IsTag,StatusID,Remarks,IsSFAOperational,SimCardNo,IvyActivationKey,SFARemarks,OperatingSystem,OSLicenseKey,MSOffice,MSOfficeLicenseKey,AntiVirus,AntiVirusLicenseKey,AppInstalled,OptAppInstalled,DateAdded,AddedBy,DateUpdated,UpdatedBy,IsPAR)" &
                             "VALUES(" & assetid & ",'" & txtdescription.Text & "','" & txtmodel.Text & "','" & txtmodelno.Text & "','" & txtserialno.Text & "','" & txtimeino.Text & "','" & txtchargerserialno.Text & "'," & txtprice.Text & "," & txtqtty.Text & ",'" & txtlifespan.Text & "','" & txtcapacitydetails.Text & "'," & cbotype.SelectedValue & "," & cbobrand.SelectedValue & "," & cbovendor.SelectedValue & ",'" & cbopgcode.SelectedValue & "','" & date_purchase.Text & "','" & txtlanmac.Text & "','" & txtwanmac.Text & "'" &
                             ",'" & txtlanip.Text & "','" & txtwanip.Text & "','" & txtteamviewerid.Text & "'," & operational_id & "," & barcodetag_id & "," & cbostatus.SelectedValue & ",'" & txtremarks.Text & "'," & sfaoperational_id & ",'" & txtsimcardno.Text & "','" & txtivyactivationkey.Text & "','" & txtsfaremarks.Text & "','" & cboos.Text & "','" & txtoslicensekey.Text & "','" & cbooffice.Text & "','" & txtmsofficekey.Text & "','" & cboantivirus.Text & "','" & txtantiviruskey.Text & "'" &
                             ",'" & installedapp & "','" & optinstalledapp & "','" & Format(Now, "yyyy-MM-dd HH:mm") & "'," & userinternalID & ",'" & Format(Now, "yyyy-MM-dd HH:mm") & "'," & userinternalID & ",0)")
                    conn.Close()

                    ExecuteQuery("INSERT INTO tbl_itassetmasterhistory (Assetmasterhistorytype,AssetID,AssetDesc,Model,ModelNo,SerialNo,IMEINo,ChargerSerialNo,Price,Qty,Lifespan,Capacity,TypeID,BrandID,VendorCode,AssetPG_ID,DatePurchased,LMACAdd,WMACAdd,LANIPAdd,WANIPAdd,TeamViewerID,IsOperational,IsTag,StatusID,Remarks,IsSFAOperational,SimCardNo,IvyActivationKey,SFARemarks,OperatingSystem,OSLicenseKey,MSOffice,MSOfficeLicenseKey,AntiVirus,AntiVirusLicenseKey,AppInstalled,OptAppInstalled,DateUpdated,UpdatedBy)" &
                             "VALUES('ADD'," & assetid & ",'" & txtdescription.Text & "','" & txtmodel.Text & "','" & txtmodelno.Text & "','" & txtserialno.Text & "','" & txtimeino.Text & "','" & txtchargerserialno.Text & "'," & txtprice.Text & "," & txtqtty.Text & ",'" & txtlifespan.Text & "','" & txtcapacitydetails.Text & "'," & cbotype.SelectedValue & "," & cbobrand.SelectedValue & "," & cbovendor.SelectedValue & ",'" & cbopgcode.SelectedValue & "','" & date_purchase.Text & "','" & txtlanmac.Text & "','" & txtwanmac.Text & "'" &
                             ",'" & txtlanip.Text & "','" & txtwanip.Text & "','" & txtteamviewerid.Text & "'," & operational_id & "," & barcodetag_id & "," & cbostatus.SelectedValue & ",'" & txtremarks.Text & "'," & sfaoperational_id & ",'" & txtsimcardno.Text & "','" & txtivyactivationkey.Text & "','" & txtsfaremarks.Text & "','" & cboos.Text & "','" & txtoslicensekey.Text & "','" & cbooffice.Text & "','" & txtmsofficekey.Text & "','" & cboantivirus.Text & "','" & txtantiviruskey.Text & "'" &
                             ",'" & installedapp & "','" & optinstalledapp & "','" & Format(Now, "yyyy-MM-dd HH:mm") & "'," & userinternalID & ")")
                    conn.Close()

                    MessageBox.Show("Successfully Saved!!")
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
                    ExecuteQuery("UPDATE tbl_itassetmaster SET AssetDesc='" & txtdescription.Text & "', Model='" & txtmodel.Text & "', ModelNo='" & txtmodelno.Text & "', SerialNo='" & txtserialno.Text & "', IMEINo='" & txtimeino.Text & "', ChargerSerialNo='" & txtchargerserialno.Text & "', Price=" & txtprice.Text & ", Qty=" & txtqtty.Text & ", Lifespan='" & txtlifespan.Text & "', Capacity='" & txtcapacitydetails.Text & "', TypeID=" & cbotype.SelectedValue & ", BrandID=" & cbobrand.SelectedValue & ", VendorCode=" & cbovendor.SelectedValue & ", AssetPG_ID='" & cbopgcode.SelectedValue & "', DatePurchased='" & date_purchase.Text & "', LMACAdd='" & txtlanmac.Text & "', WMACAdd='" & txtwanmac.Text & "'" &
                             ", LANIPAdd='" & txtlanip.Text & "', WANIPAdd='" & txtwanip.Text & "', TeamViewerID='" & txtteamviewerid.Text & "', IsOperational=" & operational_id & ", IsTag=" & barcodetag_id & ", StatusID=" & cbostatus.SelectedValue & ", Remarks='" & txtremarks.Text & "', IsSFAOperational=" & sfaoperational_id & ", SimCardNo='" & txtsimcardno.Text & "', IvyActivationKey='" & txtivyactivationkey.Text & "', SFARemarks='" & txtsfaremarks.Text & "', OperatingSystem='" & cboos.Text & "', OSLicenseKey='" & txtoslicensekey.Text & "', MSOffice='" & cbooffice.Text & "', MSOfficeLicenseKey='" & txtmsofficekey.Text & "', AntiVirus='" & cboantivirus.Text & "', AntiVirusLicenseKey='" & txtantiviruskey.Text & "'" &
                             ", AppInstalled='" & installedapp & "', OptAppInstalled='" & optinstalledapp & "', DateUpdated='" & Format(Now, "yyyy-MM-dd HH:mm") & "', UpdatedBy=" & userinternalID & " WHERE AssetID=" & value_assetid & "")
                    conn.Close()

                    ExecuteQuery("INSERT INTO tbl_itassetmasterhistory (Assetmasterhistorytype,AssetID,AssetDesc,Model,ModelNo,SerialNo,IMEINo,ChargerSerialNo,Price,Qty,Lifespan,Capacity,TypeID,BrandID,VendorCode,AssetPG_ID,DatePurchased,LMACAdd,WMACAdd,LANIPAdd,WANIPAdd,TeamViewerID,IsOperational,IsTag,StatusID,Remarks,IsSFAOperational,SimCardNo,IvyActivationKey,SFARemarks,OperatingSystem,OSLicenseKey,MSOffice,MSOfficeLicenseKey,AntiVirus,AntiVirusLicenseKey,AppInstalled,OptAppInstalled,DateUpdated,UpdatedBy)" &
                             "VALUES('UPDATE'," & value_assetid & ",'" & txtdescription.Text & "','" & txtmodel.Text & "','" & txtmodelno.Text & "','" & txtserialno.Text & "','" & txtimeino.Text & "','" & txtchargerserialno.Text & "'," & txtprice.Text & "," & txtqtty.Text & ",'" & txtlifespan.Text & "','" & txtcapacitydetails.Text & "'," & cbotype.SelectedValue & "," & cbobrand.SelectedValue & "," & cbovendor.SelectedValue & ",'" & cbopgcode.SelectedValue & "','" & date_purchase.Text & "','" & txtlanmac.Text & "','" & txtwanmac.Text & "'" &
                             ",'" & txtlanip.Text & "','" & txtwanip.Text & "','" & txtteamviewerid.Text & "'," & operational_id & "," & barcodetag_id & "," & cbostatus.SelectedValue & ",'" & txtremarks.Text & "'," & sfaoperational_id & ",'" & txtsimcardno.Text & "','" & txtivyactivationkey.Text & "','" & txtsfaremarks.Text & "','" & cboos.Text & "','" & txtoslicensekey.Text & "','" & cbooffice.Text & "','" & txtmsofficekey.Text & "','" & cboantivirus.Text & "','" & txtantiviruskey.Text & "'" &
                             ",'" & installedapp & "','" & optinstalledapp & "','" & Format(Now, "yyyy-MM-dd HH:mm") & "'," & userinternalID & ")")
                    conn.Close()

                    MessageBox.Show("Successfully Updated!!")
                    Call Frm_IT_Search.initialize()
                    Me.Close()
                End If
            End If

        End If
    End Sub

    Private Sub bttnsave_Click(sender As Object, e As EventArgs) Handles bttnsave.Click

        Call trap()

    End Sub

    Private Sub CheckedListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox2.SelectedIndexChanged
        CheckedListBox2.ClearSelected()
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        CheckedListBox1.ClearSelected()
    End Sub

    Private Sub date_purchase_ValueChanged(sender As Object, e As EventArgs) Handles date_purchase.ValueChanged
        If CStr(seqno + 1).Length = 1 Then
            seqno_result = "0000" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 2 Then
            seqno_result = "000" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 3 Then
            seqno_result = "00" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 4 Then
            seqno_result = "0" + CStr(seqno + 1)
        ElseIf CStr(seqno + 1).Length = 5 Then
            seqno_result = CStr(seqno + 1)
        End If

        assetid = Format(date_purchase.Value, "yyyy") + Format(date_purchase.Value, "MM") + Format(date_purchase.Value, "dd") + seqno_result
        lblassetid.Text = assetid
    End Sub
End Class