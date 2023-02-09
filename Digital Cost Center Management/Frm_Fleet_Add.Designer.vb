<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Fleet_Add
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblform_name = New System.Windows.Forms.Label()
        Me.lblclose2 = New System.Windows.Forms.Label()
        Me.lblclose = New System.Windows.Forms.Label()
        Me.cbovehicle = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtplate_no = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbobranch = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbotype = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbofunction = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbofuel_type = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.cbocpc = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboclass = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.date_acquisition = New System.Windows.Forms.DateTimePicker()
        Me.cbobrand = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtmodel = New System.Windows.Forms.TextBox()
        Me.cbostatus = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(93, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblform_name)
        Me.Panel1.Controls.Add(Me.lblclose2)
        Me.Panel1.Controls.Add(Me.lblclose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(761, 34)
        Me.Panel1.TabIndex = 11
        '
        'lblform_name
        '
        Me.lblform_name.AutoSize = True
        Me.lblform_name.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblform_name.ForeColor = System.Drawing.Color.White
        Me.lblform_name.Location = New System.Drawing.Point(3, 6)
        Me.lblform_name.Name = "lblform_name"
        Me.lblform_name.Size = New System.Drawing.Size(138, 24)
        Me.lblform_name.TabIndex = 11
        Me.lblform_name.Text = "Add Vehicle"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(723, 0)
        Me.lblclose2.Name = "lblclose2"
        Me.lblclose2.Size = New System.Drawing.Size(35, 31)
        Me.lblclose2.TabIndex = 10
        Me.lblclose2.Text = "X"
        Me.lblclose2.Visible = False
        '
        'lblclose
        '
        Me.lblclose.AutoSize = True
        Me.lblclose.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose.ForeColor = System.Drawing.Color.White
        Me.lblclose.Location = New System.Drawing.Point(723, 0)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'cbovehicle
        '
        Me.cbovehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbovehicle.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbovehicle.FormattingEnabled = True
        Me.cbovehicle.Items.AddRange(New Object() {"Service", "Delivery Truck", "Motorcycle"})
        Me.cbovehicle.Location = New System.Drawing.Point(100, 62)
        Me.cbovehicle.Name = "cbovehicle"
        Me.cbovehicle.Size = New System.Drawing.Size(239, 27)
        Me.cbovehicle.TabIndex = 63
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 19)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Vehicle:"
        '
        'txtplate_no
        '
        Me.txtplate_no.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtplate_no.Location = New System.Drawing.Point(479, 248)
        Me.txtplate_no.Name = "txtplate_no"
        Me.txtplate_no.Size = New System.Drawing.Size(249, 26)
        Me.txtplate_no.TabIndex = 65
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 117)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 19)
        Me.Label6.TabIndex = 66
        Me.Label6.Text = "Branch:"
        '
        'cbobranch
        '
        Me.cbobranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbobranch.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbobranch.FormattingEnabled = True
        Me.cbobranch.Items.AddRange(New Object() {"CDO", "BKD", "BXU", "ILI", "MNL", "SUR"})
        Me.cbobranch.Location = New System.Drawing.Point(100, 109)
        Me.cbobranch.Name = "cbobranch"
        Me.cbobranch.Size = New System.Drawing.Size(239, 27)
        Me.cbobranch.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(374, 251)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 19)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "Plate No.:"
        '
        'cbotype
        '
        Me.cbotype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotype.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbotype.FormattingEnabled = True
        Me.cbotype.Location = New System.Drawing.Point(100, 200)
        Me.cbotype.Name = "cbotype"
        Me.cbotype.Size = New System.Drawing.Size(239, 27)
        Me.cbotype.TabIndex = 70
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 208)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 19)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "Type:"
        '
        'cbofunction
        '
        Me.cbofunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbofunction.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbofunction.FormattingEnabled = True
        Me.cbofunction.Items.AddRange(New Object() {"COLLECTOR", "DELIVERY", "EX TRUCK", "PATROL", "PERSONAL", "PHARMA", "SERVICE"})
        Me.cbofunction.Location = New System.Drawing.Point(100, 154)
        Me.cbofunction.Name = "cbofunction"
        Me.cbofunction.Size = New System.Drawing.Size(239, 27)
        Me.cbofunction.TabIndex = 72
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 162)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 19)
        Me.Label5.TabIndex = 71
        Me.Label5.Text = "Function:"
        '
        'cbofuel_type
        '
        Me.cbofuel_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbofuel_type.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbofuel_type.FormattingEnabled = True
        Me.cbofuel_type.Items.AddRange(New Object() {"DIESEL", "GASOLINE"})
        Me.cbofuel_type.Location = New System.Drawing.Point(100, 250)
        Me.cbofuel_type.Name = "cbofuel_type"
        Me.cbofuel_type.Size = New System.Drawing.Size(239, 27)
        Me.cbofuel_type.TabIndex = 74
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 254)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 19)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "Fuel Type:"
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(479, 289)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(249, 81)
        Me.txtremarks.TabIndex = 77
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(374, 292)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 19)
        Me.Label9.TabIndex = 78
        Me.Label9.Text = "Remarks:"
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(588, 403)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(140, 55)
        Me.bttncancel.TabIndex = 80
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'cbocpc
        '
        Me.cbocpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbocpc.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbocpc.FormattingEnabled = True
        Me.cbocpc.Location = New System.Drawing.Point(100, 302)
        Me.cbocpc.Name = "cbocpc"
        Me.cbocpc.Size = New System.Drawing.Size(239, 27)
        Me.cbocpc.TabIndex = 84
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 306)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 23)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "CPC:"
        '
        'cboclass
        '
        Me.cboclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboclass.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboclass.FormattingEnabled = True
        Me.cboclass.Items.AddRange(New Object() {"Brand New", "Second Hand", "Surplus"})
        Me.cboclass.Location = New System.Drawing.Point(479, 67)
        Me.cboclass.Name = "cboclass"
        Me.cboclass.Size = New System.Drawing.Size(249, 27)
        Me.cboclass.TabIndex = 86
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(374, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(111, 23)
        Me.Label8.TabIndex = 87
        Me.Label8.Text = "Vehicle Class:"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(375, 200)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(87, 41)
        Me.Label12.TabIndex = 89
        Me.Label12.Text = "Date Acquisition:"
        '
        'date_acquisition
        '
        Me.date_acquisition.CustomFormat = "yyyy-MM-dd"
        Me.date_acquisition.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.date_acquisition.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.date_acquisition.Location = New System.Drawing.Point(479, 209)
        Me.date_acquisition.Name = "date_acquisition"
        Me.date_acquisition.Size = New System.Drawing.Size(249, 25)
        Me.date_acquisition.TabIndex = 88
        Me.date_acquisition.Value = New Date(2018, 8, 19, 0, 0, 0, 0)
        '
        'cbobrand
        '
        Me.cbobrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbobrand.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbobrand.FormattingEnabled = True
        Me.cbobrand.Items.AddRange(New Object() {"Brand New", "Second Hand"})
        Me.cbobrand.Location = New System.Drawing.Point(479, 109)
        Me.cbobrand.Name = "cbobrand"
        Me.cbobrand.Size = New System.Drawing.Size(249, 27)
        Me.cbobrand.TabIndex = 90
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(374, 112)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 23)
        Me.Label11.TabIndex = 91
        Me.Label11.Text = "Brand:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(374, 157)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 19)
        Me.Label13.TabIndex = 93
        Me.Label13.Text = "Model:"
        '
        'txtmodel
        '
        Me.txtmodel.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmodel.Location = New System.Drawing.Point(479, 154)
        Me.txtmodel.Name = "txtmodel"
        Me.txtmodel.Size = New System.Drawing.Size(249, 26)
        Me.txtmodel.TabIndex = 92
        '
        'cbostatus
        '
        Me.cbostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbostatus.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbostatus.FormattingEnabled = True
        Me.cbostatus.Items.AddRange(New Object() {"ACTIVE", "INACTIVE"})
        Me.cbostatus.Location = New System.Drawing.Point(100, 352)
        Me.cbostatus.Name = "cbostatus"
        Me.cbostatus.Size = New System.Drawing.Size(239, 27)
        Me.cbostatus.TabIndex = 95
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 355)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 19)
        Me.Label10.TabIndex = 94
        Me.Label10.Text = "Status:"
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(438, 403)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(135, 55)
        Me.bttnsave.TabIndex = 79
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'Frm_add_vehicle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(761, 477)
        Me.Controls.Add(Me.cbostatus)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtmodel)
        Me.Controls.Add(Me.cbobrand)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.date_acquisition)
        Me.Controls.Add(Me.cboclass)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cbocpc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.txtremarks)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cbofuel_type)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cbofunction)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cbotype)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbobranch)
        Me.Controls.Add(Me.txtplate_no)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbovehicle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_add_vehicle"
        Me.Text = "Frm_fleet"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblform_name As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
    Friend WithEvents cbovehicle As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtplate_no As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cbobranch As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbotype As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbofunction As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbofuel_type As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents bttncancel As Button
    Friend WithEvents bttnsave As Button
    Friend WithEvents cbocpc As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboclass As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents date_acquisition As DateTimePicker
    Friend WithEvents cbobrand As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtmodel As TextBox
    Friend WithEvents cbostatus As ComboBox
    Friend WithEvents Label10 As Label
End Class
