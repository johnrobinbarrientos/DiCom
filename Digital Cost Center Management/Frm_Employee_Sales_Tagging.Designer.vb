<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Employee_Sales_Tagging
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblclose2 = New System.Windows.Forms.Label()
        Me.lblclose = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.EmployeeID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.cbosubsegment = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbobtdt = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbosfa = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbosupervisor = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbosales_rep = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtcpc = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtsubsegment = New System.Windows.Forms.TextBox()
        Me.txtbtdt = New System.Windows.Forms.TextBox()
        Me.txtsfa = New System.Windows.Forms.TextBox()
        Me.txtsales_rep = New System.Windows.Forms.TextBox()
        Me.txtsupervisor = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtjob_title = New System.Windows.Forms.TextBox()
        Me.txtposition = New System.Windows.Forms.TextBox()
        Me.txtdepartment = New System.Windows.Forms.TextBox()
        Me.txtbranch = New System.Windows.Forms.TextBox()
        Me.txtlast_name = New System.Windows.Forms.TextBox()
        Me.txtmiddle_name = New System.Windows.Forms.TextBox()
        Me.txtfirst_name = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(93, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblclose2)
        Me.Panel1.Controls.Add(Me.lblclose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(627, 34)
        Me.Panel1.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Sales Tagging"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(592, 0)
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
        Me.lblclose.Location = New System.Drawing.Point(592, 0)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.EmployeeID, Me.lvname})
        Me.ListView1.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.Location = New System.Drawing.Point(52, 74)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(211, 83)
        Me.ListView1.TabIndex = 26
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
        Me.lvname.Width = 200
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_search_50
        Me.PictureBox1.Location = New System.Drawing.Point(17, 44)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = False
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(52, 49)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(203, 25)
        Me.txtsearch.TabIndex = 24
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Linen
        Me.GroupBox2.Controls.Add(Me.bttnsave)
        Me.GroupBox2.Controls.Add(Me.cbosubsegment)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cbobtdt)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cbosfa)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cbosupervisor)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cbosales_rep)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(19, 359)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(596, 182)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sales Tag"
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_restart_48
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(370, 100)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(171, 56)
        Me.bttnsave.TabIndex = 31
        Me.bttnsave.Text = "    UPDATE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'cbosubsegment
        '
        Me.cbosubsegment.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbosubsegment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbosubsegment.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbosubsegment.FormattingEnabled = True
        Me.cbosubsegment.Location = New System.Drawing.Point(370, 57)
        Me.cbosubsegment.Name = "cbosubsegment"
        Me.cbosubsegment.Size = New System.Drawing.Size(220, 23)
        Me.cbosubsegment.TabIndex = 37
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(277, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Subsegment:"
        '
        'cbobtdt
        '
        Me.cbobtdt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbobtdt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbobtdt.FormattingEnabled = True
        Me.cbobtdt.Items.AddRange(New Object() {"NO", "YES"})
        Me.cbobtdt.Location = New System.Drawing.Point(370, 24)
        Me.cbobtdt.Name = "cbobtdt"
        Me.cbobtdt.Size = New System.Drawing.Size(220, 25)
        Me.cbobtdt.TabIndex = 35
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(277, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 17)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Is BTDT:"
        '
        'cbosfa
        '
        Me.cbosfa.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbosfa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbosfa.FormattingEnabled = True
        Me.cbosfa.Items.AddRange(New Object() {"NO", "YES"})
        Me.cbosfa.Location = New System.Drawing.Point(101, 92)
        Me.cbosfa.Name = "cbosfa"
        Me.cbosfa.Size = New System.Drawing.Size(162, 25)
        Me.cbosfa.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 17)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Is SFA:"
        '
        'cbosupervisor
        '
        Me.cbosupervisor.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbosupervisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbosupervisor.FormattingEnabled = True
        Me.cbosupervisor.Items.AddRange(New Object() {"NO", "YES"})
        Me.cbosupervisor.Location = New System.Drawing.Point(101, 24)
        Me.cbosupervisor.Name = "cbosupervisor"
        Me.cbosupervisor.Size = New System.Drawing.Size(162, 25)
        Me.cbosupervisor.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 17)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Is Supervisor:"
        '
        'cbosales_rep
        '
        Me.cbosales_rep.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbosales_rep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbosales_rep.FormattingEnabled = True
        Me.cbosales_rep.Items.AddRange(New Object() {"NO", "YES"})
        Me.cbosales_rep.Location = New System.Drawing.Point(101, 57)
        Me.cbosales_rep.Name = "cbosales_rep"
        Me.cbosales_rep.Size = New System.Drawing.Size(162, 25)
        Me.cbosales_rep.TabIndex = 29
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 17)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Is Sales Rep.:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Linen
        Me.GroupBox1.Controls.Add(Me.txtcpc)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtsubsegment)
        Me.GroupBox1.Controls.Add(Me.txtbtdt)
        Me.GroupBox1.Controls.Add(Me.txtsfa)
        Me.GroupBox1.Controls.Add(Me.txtsales_rep)
        Me.GroupBox1.Controls.Add(Me.txtsupervisor)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtjob_title)
        Me.GroupBox1.Controls.Add(Me.txtposition)
        Me.GroupBox1.Controls.Add(Me.txtdepartment)
        Me.GroupBox1.Controls.Add(Me.txtbranch)
        Me.GroupBox1.Controls.Add(Me.txtlast_name)
        Me.GroupBox1.Controls.Add(Me.txtmiddle_name)
        Me.GroupBox1.Controls.Add(Me.txtfirst_name)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label29)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(19, 93)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(596, 245)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Employee Info."
        '
        'txtcpc
        '
        Me.txtcpc.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtcpc.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcpc.Location = New System.Drawing.Point(396, 197)
        Me.txtcpc.Name = "txtcpc"
        Me.txtcpc.ReadOnly = True
        Me.txtcpc.Size = New System.Drawing.Size(194, 22)
        Me.txtcpc.TabIndex = 93
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(306, 197)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 17)
        Me.Label15.TabIndex = 92
        Me.Label15.Text = "CPC:"
        '
        'txtsubsegment
        '
        Me.txtsubsegment.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtsubsegment.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsubsegment.Location = New System.Drawing.Point(396, 165)
        Me.txtsubsegment.Name = "txtsubsegment"
        Me.txtsubsegment.ReadOnly = True
        Me.txtsubsegment.Size = New System.Drawing.Size(194, 23)
        Me.txtsubsegment.TabIndex = 91
        '
        'txtbtdt
        '
        Me.txtbtdt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtbtdt.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbtdt.Location = New System.Drawing.Point(396, 131)
        Me.txtbtdt.Name = "txtbtdt"
        Me.txtbtdt.ReadOnly = True
        Me.txtbtdt.Size = New System.Drawing.Size(194, 23)
        Me.txtbtdt.TabIndex = 90
        '
        'txtsfa
        '
        Me.txtsfa.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtsfa.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsfa.Location = New System.Drawing.Point(396, 94)
        Me.txtsfa.Name = "txtsfa"
        Me.txtsfa.ReadOnly = True
        Me.txtsfa.Size = New System.Drawing.Size(194, 23)
        Me.txtsfa.TabIndex = 89
        '
        'txtsales_rep
        '
        Me.txtsales_rep.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtsales_rep.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsales_rep.Location = New System.Drawing.Point(396, 59)
        Me.txtsales_rep.Name = "txtsales_rep"
        Me.txtsales_rep.ReadOnly = True
        Me.txtsales_rep.Size = New System.Drawing.Size(194, 23)
        Me.txtsales_rep.TabIndex = 88
        '
        'txtsupervisor
        '
        Me.txtsupervisor.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtsupervisor.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupervisor.Location = New System.Drawing.Point(396, 26)
        Me.txtsupervisor.Name = "txtsupervisor"
        Me.txtsupervisor.ReadOnly = True
        Me.txtsupervisor.Size = New System.Drawing.Size(194, 23)
        Me.txtsupervisor.TabIndex = 87
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(307, 165)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 17)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "Subsegment:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(307, 131)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 17)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "Is BTDT:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(307, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 17)
        Me.Label9.TabIndex = 84
        Me.Label9.Text = "Is SFA:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(307, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 17)
        Me.Label10.TabIndex = 83
        Me.Label10.Text = "Is Supervisor:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(306, 65)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 17)
        Me.Label11.TabIndex = 82
        Me.Label11.Text = "Is Sales Rep.:"
        '
        'txtjob_title
        '
        Me.txtjob_title.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtjob_title.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjob_title.Location = New System.Drawing.Point(98, 210)
        Me.txtjob_title.Name = "txtjob_title"
        Me.txtjob_title.ReadOnly = True
        Me.txtjob_title.Size = New System.Drawing.Size(206, 22)
        Me.txtjob_title.TabIndex = 78
        '
        'txtposition
        '
        Me.txtposition.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtposition.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtposition.Location = New System.Drawing.Point(98, 181)
        Me.txtposition.Name = "txtposition"
        Me.txtposition.ReadOnly = True
        Me.txtposition.Size = New System.Drawing.Size(206, 23)
        Me.txtposition.TabIndex = 77
        '
        'txtdepartment
        '
        Me.txtdepartment.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtdepartment.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdepartment.Location = New System.Drawing.Point(98, 148)
        Me.txtdepartment.Name = "txtdepartment"
        Me.txtdepartment.ReadOnly = True
        Me.txtdepartment.Size = New System.Drawing.Size(206, 23)
        Me.txtdepartment.TabIndex = 76
        '
        'txtbranch
        '
        Me.txtbranch.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtbranch.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch.Location = New System.Drawing.Point(98, 119)
        Me.txtbranch.Name = "txtbranch"
        Me.txtbranch.ReadOnly = True
        Me.txtbranch.Size = New System.Drawing.Size(206, 23)
        Me.txtbranch.TabIndex = 75
        '
        'txtlast_name
        '
        Me.txtlast_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtlast_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlast_name.Location = New System.Drawing.Point(98, 87)
        Me.txtlast_name.Name = "txtlast_name"
        Me.txtlast_name.ReadOnly = True
        Me.txtlast_name.Size = New System.Drawing.Size(206, 23)
        Me.txtlast_name.TabIndex = 74
        '
        'txtmiddle_name
        '
        Me.txtmiddle_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtmiddle_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmiddle_name.Location = New System.Drawing.Point(98, 58)
        Me.txtmiddle_name.Name = "txtmiddle_name"
        Me.txtmiddle_name.ReadOnly = True
        Me.txtmiddle_name.Size = New System.Drawing.Size(206, 23)
        Me.txtmiddle_name.TabIndex = 73
        '
        'txtfirst_name
        '
        Me.txtfirst_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtfirst_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfirst_name.Location = New System.Drawing.Point(98, 29)
        Me.txtfirst_name.Name = "txtfirst_name"
        Me.txtfirst_name.ReadOnly = True
        Me.txtfirst_name.Size = New System.Drawing.Size(206, 23)
        Me.txtfirst_name.TabIndex = 72
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(7, 182)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 17)
        Me.Label27.TabIndex = 54
        Me.Label27.Text = "Position:"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(7, 211)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(65, 17)
        Me.Label29.TabIndex = 53
        Me.Label29.Text = "Job Title:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(7, 148)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(87, 17)
        Me.Label25.TabIndex = 52
        Me.Label25.Text = "Department:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 119)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(57, 17)
        Me.Label24.TabIndex = 51
        Me.Label24.Text = "Branch:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 30)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 17)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "First Name:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 57)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 17)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Middle Name:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(7, 87)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 17)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "Last Name:"
        '
        'Frm_sales_tagging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(627, 547)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtsearch)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_sales_tagging"
        Me.Text = "Frm_sales_tagging"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents EmployeeID As ColumnHeader
    Friend WithEvents lvname As ColumnHeader
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtsearch As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cbosubsegment As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbobtdt As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbosfa As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cbosupervisor As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbosales_rep As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label27 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txtjob_title As TextBox
    Friend WithEvents txtposition As TextBox
    Friend WithEvents txtdepartment As TextBox
    Friend WithEvents txtbranch As TextBox
    Friend WithEvents txtlast_name As TextBox
    Friend WithEvents txtmiddle_name As TextBox
    Friend WithEvents txtfirst_name As TextBox
    Friend WithEvents bttnsave As Button
    Friend WithEvents txtsubsegment As TextBox
    Friend WithEvents txtbtdt As TextBox
    Friend WithEvents txtsfa As TextBox
    Friend WithEvents txtsales_rep As TextBox
    Friend WithEvents txtsupervisor As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtcpc As TextBox
    Friend WithEvents Label15 As Label
End Class
