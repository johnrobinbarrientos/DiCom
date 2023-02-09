<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Masterdata_Finance_BIX_PINS
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
        Me.cbophone_status = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbophone_type = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtmonthly_allowance = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtservice_no = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtacct_no = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.EmployeeID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtfirst_name = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtmid_name = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtlast_name = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txtsearch_employee = New System.Windows.Forms.TextBox()
        Me.bttnnew = New System.Windows.Forms.Button()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblform_name = New System.Windows.Forms.Label()
        Me.lblclose2 = New System.Windows.Forms.Label()
        Me.lblclose = New System.Windows.Forms.Label()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbophone_status
        '
        Me.cbophone_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbophone_status.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbophone_status.FormattingEnabled = True
        Me.cbophone_status.Items.AddRange(New Object() {"ACTIVE", "INACTIVE"})
        Me.cbophone_status.Location = New System.Drawing.Point(862, 478)
        Me.cbophone_status.Name = "cbophone_status"
        Me.cbophone_status.Size = New System.Drawing.Size(188, 24)
        Me.cbophone_status.TabIndex = 168
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(773, 487)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 15)
        Me.Label8.TabIndex = 167
        Me.Label8.Text = "Phone Status:"
        '
        'cbophone_type
        '
        Me.cbophone_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbophone_type.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbophone_type.FormattingEnabled = True
        Me.cbophone_type.Items.AddRange(New Object() {"OGDI", "PERSONAL"})
        Me.cbophone_type.Location = New System.Drawing.Point(862, 440)
        Me.cbophone_type.Name = "cbophone_type"
        Me.cbophone_type.Size = New System.Drawing.Size(188, 24)
        Me.cbophone_type.TabIndex = 166
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(773, 449)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 15)
        Me.Label7.TabIndex = 165
        Me.Label7.Text = "Phone Type:"
        '
        'txtmonthly_allowance
        '
        Me.txtmonthly_allowance.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtmonthly_allowance.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmonthly_allowance.Location = New System.Drawing.Point(508, 522)
        Me.txtmonthly_allowance.Name = "txtmonthly_allowance"
        Me.txtmonthly_allowance.Size = New System.Drawing.Size(190, 23)
        Me.txtmonthly_allowance.TabIndex = 163
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(421, 522)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 30)
        Me.Label6.TabIndex = 164
        Me.Label6.Text = "Monthly" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Allowance:"
        '
        'txtservice_no
        '
        Me.txtservice_no.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtservice_no.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtservice_no.Location = New System.Drawing.Point(508, 484)
        Me.txtservice_no.Name = "txtservice_no"
        Me.txtservice_no.Size = New System.Drawing.Size(190, 23)
        Me.txtservice_no.TabIndex = 161
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(421, 487)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 15)
        Me.Label4.TabIndex = 162
        Me.Label4.Text = "Service No.:"
        '
        'txtacct_no
        '
        Me.txtacct_no.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtacct_no.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtacct_no.Location = New System.Drawing.Point(508, 447)
        Me.txtacct_no.Name = "txtacct_no"
        Me.txtacct_no.Size = New System.Drawing.Size(190, 23)
        Me.txtacct_no.TabIndex = 159
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(421, 449)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 15)
        Me.Label5.TabIndex = 160
        Me.Label5.Text = "Account No.:"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.EmployeeID, Me.lvname})
        Me.ListView1.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.Location = New System.Drawing.Point(55, 462)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(212, 83)
        Me.ListView1.TabIndex = 152
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.Visible = False
        '
        'EmployeeID
        '
        Me.EmployeeID.Text = "Employee ID"
        Me.EmployeeID.Width = 0
        '
        'lvname
        '
        Me.lvname.Text = "Name"
        Me.lvname.Width = 250
        '
        'txtfirst_name
        '
        Me.txtfirst_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtfirst_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfirst_name.Location = New System.Drawing.Point(108, 479)
        Me.txtfirst_name.Name = "txtfirst_name"
        Me.txtfirst_name.ReadOnly = True
        Me.txtfirst_name.Size = New System.Drawing.Size(190, 23)
        Me.txtfirst_name.TabIndex = 153
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 481)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 154
        Me.Label1.Text = "First Name:"
        '
        'txtmid_name
        '
        Me.txtmid_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtmid_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmid_name.Location = New System.Drawing.Point(107, 507)
        Me.txtmid_name.Name = "txtmid_name"
        Me.txtmid_name.ReadOnly = True
        Me.txtmid_name.Size = New System.Drawing.Size(191, 23)
        Me.txtmid_name.TabIndex = 155
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 513)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 15)
        Me.Label3.TabIndex = 157
        Me.Label3.Text = "Middle Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 538)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 15)
        Me.Label2.TabIndex = 158
        Me.Label2.Text = "Last Name:"
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Status"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "ID"
        Me.ColumnHeader6.Width = 0
        '
        'txtlast_name
        '
        Me.txtlast_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtlast_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlast_name.Location = New System.Drawing.Point(107, 536)
        Me.txtlast_name.Name = "txtlast_name"
        Me.txtlast_name.ReadOnly = True
        Me.txtlast_name.Size = New System.Drawing.Size(191, 23)
        Me.txtlast_name.TabIndex = 156
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_search_50
        Me.PictureBox2.Location = New System.Drawing.Point(20, 437)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(29, 30)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 151
        Me.PictureBox2.TabStop = False
        '
        'txtsearch_employee
        '
        Me.txtsearch_employee.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch_employee.Location = New System.Drawing.Point(55, 437)
        Me.txtsearch_employee.Name = "txtsearch_employee"
        Me.txtsearch_employee.Size = New System.Drawing.Size(203, 25)
        Me.txtsearch_employee.TabIndex = 150
        '
        'bttnnew
        '
        Me.bttnnew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnnew.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnnew.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_add_new_30
        Me.bttnnew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnnew.Location = New System.Drawing.Point(868, 575)
        Me.bttnnew.Name = "bttnnew"
        Me.bttnnew.Size = New System.Drawing.Size(142, 39)
        Me.bttnnew.TabIndex = 149
        Me.bttnnew.Text = "NEW"
        Me.bttnnew.UseVisualStyleBackColor = True
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(790, 522)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(142, 39)
        Me.bttncancel.TabIndex = 148
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(954, 522)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(142, 39)
        Me.bttnsave.TabIndex = 147
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(46, 48)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(203, 25)
        Me.txtsearch.TabIndex = 143
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader4, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.ListView2.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.Location = New System.Drawing.Point(11, 84)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(1100, 283)
        Me.ListView2.TabIndex = 142
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Last Name"
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "First Name"
        Me.ColumnHeader7.Width = 150
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Middle Name"
        Me.ColumnHeader4.Width = 150
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Account No."
        Me.ColumnHeader1.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Service No."
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Monthly Allowance"
        Me.ColumnHeader8.Width = 120
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Phone Type"
        Me.ColumnHeader9.Width = 130
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_search_50
        Me.PictureBox1.Location = New System.Drawing.Point(11, 48)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 144
        Me.PictureBox1.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(16, 408)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(138, 19)
        Me.Label13.TabIndex = 146
        Me.Label13.Text = "Add PINS Account:"
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
        Me.Panel1.Size = New System.Drawing.Size(1130, 34)
        Me.Panel1.TabIndex = 145
        '
        'lblform_name
        '
        Me.lblform_name.AutoSize = True
        Me.lblform_name.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblform_name.ForeColor = System.Drawing.Color.White
        Me.lblform_name.Location = New System.Drawing.Point(3, 6)
        Me.lblform_name.Name = "lblform_name"
        Me.lblform_name.Size = New System.Drawing.Size(64, 24)
        Me.lblform_name.TabIndex = 11
        Me.lblform_name.Text = "PINS"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(1088, 3)
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
        Me.lblclose.Location = New System.Drawing.Point(1088, 3)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'Frm_Masterdata_Finance_BIX_PINS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1130, 621)
        Me.Controls.Add(Me.cbophone_status)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cbophone_type)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtmonthly_allowance)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtservice_no)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtacct_no)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.txtfirst_name)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtmid_name)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtlast_name)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.txtsearch_employee)
        Me.Controls.Add(Me.bttnnew)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.txtsearch)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_Masterdata_Finance_BIX_PINS"
        Me.Text = "Frm_Masterdata_BIX_Finance_PINS"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbophone_status As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cbophone_type As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtmonthly_allowance As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtservice_no As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtacct_no As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents EmployeeID As ColumnHeader
    Friend WithEvents lvname As ColumnHeader
    Friend WithEvents txtfirst_name As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtmid_name As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents txtlast_name As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents txtsearch_employee As TextBox
    Friend WithEvents bttnnew As Button
    Friend WithEvents bttncancel As Button
    Friend WithEvents bttnsave As Button
    Friend WithEvents txtsearch As TextBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblform_name As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
End Class
