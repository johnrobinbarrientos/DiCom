<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Masterdata_Finance_BIX_Area_Rate
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
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.bttnnew = New System.Windows.Forms.Button()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtarea = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtrate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtsss = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtpagibig = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtphilhealth = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtincentive = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(757, 34)
        Me.Panel1.TabIndex = 118
        '
        'lblform_name
        '
        Me.lblform_name.AutoSize = True
        Me.lblform_name.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblform_name.ForeColor = System.Drawing.Color.White
        Me.lblform_name.Location = New System.Drawing.Point(3, 6)
        Me.lblform_name.Name = "lblform_name"
        Me.lblform_name.Size = New System.Drawing.Size(115, 24)
        Me.lblform_name.TabIndex = 11
        Me.lblform_name.Text = "Area Rate"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(722, 3)
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
        Me.lblclose.Location = New System.Drawing.Point(722, 3)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.ListView2.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.Location = New System.Drawing.Point(17, 94)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(732, 193)
        Me.ListView2.TabIndex = 126
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "StatusID"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Area"
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Rate"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "SSS Premium"
        Me.ColumnHeader4.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Pag-IBIG Premium"
        Me.ColumnHeader5.Width = 120
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "PhilHealth Premium"
        Me.ColumnHeader6.Width = 130
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Incentive Leave"
        Me.ColumnHeader7.Width = 120
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(497, 446)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(142, 39)
        Me.bttnsave.TabIndex = 121
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_search_50
        Me.PictureBox1.Location = New System.Drawing.Point(12, 58)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 125
        Me.PictureBox1.TabStop = False
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(47, 58)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(265, 25)
        Me.txtsearch.TabIndex = 124
        '
        'bttnnew
        '
        Me.bttnnew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnnew.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnnew.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_add_new_30
        Me.bttnnew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnnew.Location = New System.Drawing.Point(184, 446)
        Me.bttnnew.Name = "bttnnew"
        Me.bttnnew.Size = New System.Drawing.Size(140, 39)
        Me.bttnnew.TabIndex = 123
        Me.bttnnew.Text = "NEW"
        Me.bttnnew.UseVisualStyleBackColor = True
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(339, 446)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(142, 39)
        Me.bttncancel.TabIndex = 122
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(13, 313)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 19)
        Me.Label13.TabIndex = 120
        Me.Label13.Text = "Area:"
        '
        'txtarea
        '
        Me.txtarea.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtarea.Location = New System.Drawing.Point(65, 313)
        Me.txtarea.Name = "txtarea"
        Me.txtarea.Size = New System.Drawing.Size(271, 26)
        Me.txtarea.TabIndex = 119
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 356)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 19)
        Me.Label1.TabIndex = 128
        Me.Label1.Text = "Rate:"
        '
        'txtrate
        '
        Me.txtrate.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrate.Location = New System.Drawing.Point(65, 353)
        Me.txtrate.Name = "txtrate"
        Me.txtrate.Size = New System.Drawing.Size(271, 26)
        Me.txtrate.TabIndex = 127
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 394)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 19)
        Me.Label2.TabIndex = 130
        Me.Label2.Text = "SSS:"
        '
        'txtsss
        '
        Me.txtsss.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsss.Location = New System.Drawing.Point(65, 391)
        Me.txtsss.Name = "txtsss"
        Me.txtsss.Size = New System.Drawing.Size(271, 26)
        Me.txtsss.TabIndex = 129
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(377, 313)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 19)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Pag-IBIG:"
        '
        'txtpagibig
        '
        Me.txtpagibig.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpagibig.Location = New System.Drawing.Point(467, 313)
        Me.txtpagibig.Name = "txtpagibig"
        Me.txtpagibig.Size = New System.Drawing.Size(271, 26)
        Me.txtpagibig.TabIndex = 131
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(377, 356)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 19)
        Me.Label4.TabIndex = 134
        Me.Label4.Text = "PhilHealth:"
        '
        'txtphilhealth
        '
        Me.txtphilhealth.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphilhealth.Location = New System.Drawing.Point(467, 353)
        Me.txtphilhealth.Name = "txtphilhealth"
        Me.txtphilhealth.Size = New System.Drawing.Size(271, 26)
        Me.txtphilhealth.TabIndex = 133
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(377, 394)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 19)
        Me.Label5.TabIndex = 136
        Me.Label5.Text = "Incentive:"
        '
        'txtincentive
        '
        Me.txtincentive.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtincentive.Location = New System.Drawing.Point(467, 391)
        Me.txtincentive.Name = "txtincentive"
        Me.txtincentive.Size = New System.Drawing.Size(271, 26)
        Me.txtincentive.TabIndex = 135
        '
        'Frm_Masterdata_Finance_BIX_Area_Rate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(757, 513)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtincentive)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtphilhealth)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtpagibig)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtsss)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtrate)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtsearch)
        Me.Controls.Add(Me.bttnnew)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtarea)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_Masterdata_Finance_BIX_Area_Rate"
        Me.Text = "Frm_Masterdata_Finance_BIX_Area_Rate"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblform_name As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents bttnsave As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtsearch As TextBox
    Friend WithEvents bttnnew As Button
    Friend WithEvents bttncancel As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txtarea As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtrate As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtsss As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtpagibig As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtphilhealth As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtincentive As TextBox
End Class
