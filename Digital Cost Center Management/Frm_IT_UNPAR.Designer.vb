<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IT_UNPAR
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtjob_title = New System.Windows.Forms.TextBox()
        Me.txtcpc = New System.Windows.Forms.TextBox()
        Me.txtdepartment = New System.Windows.Forms.TextBox()
        Me.txtbranch = New System.Windows.Forms.TextBox()
        Me.txtlast_name = New System.Windows.Forms.TextBox()
        Me.txtmiddle_name = New System.Windows.Forms.TextBox()
        Me.txtfirst_name = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.bttnUnPAR = New System.Windows.Forms.Button()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblassetid = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(967, 34)
        Me.Panel1.TabIndex = 14
        '
        'lblform_name
        '
        Me.lblform_name.AutoSize = True
        Me.lblform_name.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblform_name.ForeColor = System.Drawing.Color.White
        Me.lblform_name.Location = New System.Drawing.Point(3, 6)
        Me.lblform_name.Name = "lblform_name"
        Me.lblform_name.Size = New System.Drawing.Size(120, 24)
        Me.lblform_name.TabIndex = 11
        Me.lblform_name.Text = "UNPAR IT"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(932, 3)
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
        Me.lblclose.Location = New System.Drawing.Point(932, 3)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Linen
        Me.Panel3.Controls.Add(Me.txtjob_title)
        Me.Panel3.Controls.Add(Me.txtcpc)
        Me.Panel3.Controls.Add(Me.txtdepartment)
        Me.Panel3.Controls.Add(Me.txtbranch)
        Me.Panel3.Controls.Add(Me.txtlast_name)
        Me.Panel3.Controls.Add(Me.txtmiddle_name)
        Me.Panel3.Controls.Add(Me.txtfirst_name)
        Me.Panel3.Controls.Add(Me.Label29)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Location = New System.Drawing.Point(12, 71)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(941, 140)
        Me.Panel3.TabIndex = 89
        '
        'txtjob_title
        '
        Me.txtjob_title.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtjob_title.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjob_title.Location = New System.Drawing.Point(708, 28)
        Me.txtjob_title.Multiline = True
        Me.txtjob_title.Name = "txtjob_title"
        Me.txtjob_title.ReadOnly = True
        Me.txtjob_title.Size = New System.Drawing.Size(220, 80)
        Me.txtjob_title.TabIndex = 68
        '
        'txtcpc
        '
        Me.txtcpc.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtcpc.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcpc.Location = New System.Drawing.Point(410, 62)
        Me.txtcpc.Name = "txtcpc"
        Me.txtcpc.ReadOnly = True
        Me.txtcpc.Size = New System.Drawing.Size(214, 22)
        Me.txtcpc.TabIndex = 65
        '
        'txtdepartment
        '
        Me.txtdepartment.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtdepartment.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdepartment.Location = New System.Drawing.Point(410, 21)
        Me.txtdepartment.Name = "txtdepartment"
        Me.txtdepartment.ReadOnly = True
        Me.txtdepartment.Size = New System.Drawing.Size(214, 23)
        Me.txtdepartment.TabIndex = 64
        '
        'txtbranch
        '
        Me.txtbranch.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtbranch.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch.Location = New System.Drawing.Point(410, 98)
        Me.txtbranch.Name = "txtbranch"
        Me.txtbranch.ReadOnly = True
        Me.txtbranch.Size = New System.Drawing.Size(214, 23)
        Me.txtbranch.TabIndex = 63
        '
        'txtlast_name
        '
        Me.txtlast_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtlast_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlast_name.Location = New System.Drawing.Point(95, 92)
        Me.txtlast_name.Name = "txtlast_name"
        Me.txtlast_name.ReadOnly = True
        Me.txtlast_name.Size = New System.Drawing.Size(209, 23)
        Me.txtlast_name.TabIndex = 62
        '
        'txtmiddle_name
        '
        Me.txtmiddle_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtmiddle_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmiddle_name.Location = New System.Drawing.Point(95, 56)
        Me.txtmiddle_name.Name = "txtmiddle_name"
        Me.txtmiddle_name.ReadOnly = True
        Me.txtmiddle_name.Size = New System.Drawing.Size(211, 23)
        Me.txtmiddle_name.TabIndex = 61
        '
        'txtfirst_name
        '
        Me.txtfirst_name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtfirst_name.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfirst_name.Location = New System.Drawing.Point(95, 23)
        Me.txtfirst_name.Name = "txtfirst_name"
        Me.txtfirst_name.ReadOnly = True
        Me.txtfirst_name.Size = New System.Drawing.Size(209, 23)
        Me.txtfirst_name.TabIndex = 60
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(636, 24)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(65, 17)
        Me.Label29.TabIndex = 46
        Me.Label29.Text = "Job Title:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(314, 62)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(37, 17)
        Me.Label26.TabIndex = 44
        Me.Label26.Text = "CPC:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(312, 22)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(87, 17)
        Me.Label25.TabIndex = 43
        Me.Label25.Text = "Department:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(314, 98)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(57, 17)
        Me.Label24.TabIndex = 42
        Me.Label24.Text = "Branch:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 17)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "First Name:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 56)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 17)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "Middle Name:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 91)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 17)
        Me.Label14.TabIndex = 41
        Me.Label14.Text = "Last Name:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(15, 121)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(0, 17)
        Me.Label15.TabIndex = 17
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(651, 240)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(127, 44)
        Me.bttncancel.TabIndex = 95
        Me.bttncancel.Text = "Cancel"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'bttnUnPAR
        '
        Me.bttnUnPAR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnUnPAR.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnUnPAR.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_delete_ticket_30
        Me.bttnUnPAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnUnPAR.Location = New System.Drawing.Point(507, 240)
        Me.bttnUnPAR.Name = "bttnUnPAR"
        Me.bttnUnPAR.Size = New System.Drawing.Size(129, 44)
        Me.bttnUnPAR.TabIndex = 94
        Me.bttnUnPAR.Text = "   UnPAR"
        Me.bttnUnPAR.UseVisualStyleBackColor = True
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(103, 227)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(352, 75)
        Me.txtremarks.TabIndex = 105
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 226)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 19)
        Me.Label11.TabIndex = 104
        Me.Label11.Text = "Remarks:"
        '
        'lblassetid
        '
        Me.lblassetid.AutoSize = True
        Me.lblassetid.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblassetid.ForeColor = System.Drawing.Color.PaleGoldenrod
        Me.lblassetid.Location = New System.Drawing.Point(98, 41)
        Me.lblassetid.Name = "lblassetid"
        Me.lblassetid.Size = New System.Drawing.Size(64, 17)
        Me.lblassetid.TabIndex = 107
        Me.lblassetid.Text = "Asset ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 17)
        Me.Label2.TabIndex = 106
        Me.Label2.Text = "Asset ID:"
        '
        'Frm_unpar_IT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(967, 318)
        Me.Controls.Add(Me.lblassetid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtremarks)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.bttnUnPAR)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_unpar_IT"
        Me.Text = "Frm_unpar_IT"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblform_name As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txtjob_title As TextBox
    Friend WithEvents txtcpc As TextBox
    Friend WithEvents txtdepartment As TextBox
    Friend WithEvents txtbranch As TextBox
    Friend WithEvents txtlast_name As TextBox
    Friend WithEvents txtmiddle_name As TextBox
    Friend WithEvents txtfirst_name As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents bttncancel As Button
    Friend WithEvents bttnUnPAR As Button
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents lblassetid As Label
    Friend WithEvents Label2 As Label
End Class
