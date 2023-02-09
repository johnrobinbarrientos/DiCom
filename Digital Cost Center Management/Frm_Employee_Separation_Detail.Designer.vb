<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Employee_Separation_Detail
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
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.date_separation = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblclose2 = New System.Windows.Forms.Label()
        Me.lblclose = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbotype_sep = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.date_hired = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cboreason_sep = New System.Windows.Forms.ComboBox()
        Me.cbojobtitle = New System.Windows.Forms.ComboBox()
        Me.bttndelete = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(334, 390)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(139, 48)
        Me.bttncancel.TabIndex = 7
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(189, 390)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(139, 48)
        Me.bttnsave.TabIndex = 6
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 19)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Job Title:"
        '
        'date_separation
        '
        Me.date_separation.CustomFormat = "yyyy-MM-dd"
        Me.date_separation.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.date_separation.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.date_separation.Location = New System.Drawing.Point(189, 155)
        Me.date_separation.Name = "date_separation"
        Me.date_separation.Size = New System.Drawing.Size(284, 26)
        Me.date_separation.TabIndex = 2
        Me.date_separation.Value = New Date(2018, 8, 19, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(171, 23)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Reason for Separation:"
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
        Me.Panel1.Size = New System.Drawing.Size(512, 34)
        Me.Panel1.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(207, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Separation Details"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(474, 3)
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
        Me.lblclose.Location = New System.Drawing.Point(474, 3)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 27)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Type for Separation:"
        '
        'cbotype_sep
        '
        Me.cbotype_sep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotype_sep.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbotype_sep.FormattingEnabled = True
        Me.cbotype_sep.Items.AddRange(New Object() {"Resignation", "Termination"})
        Me.cbotype_sep.Location = New System.Drawing.Point(189, 108)
        Me.cbotype_sep.Name = "cbotype_sep"
        Me.cbotype_sep.Size = New System.Drawing.Size(284, 27)
        Me.cbotype_sep.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 23)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Date of Separation:"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 249)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 20)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Date Hired:"
        '
        'date_hired
        '
        Me.date_hired.CustomFormat = "yyyy-MM-dd"
        Me.date_hired.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.date_hired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.date_hired.Location = New System.Drawing.Point(189, 243)
        Me.date_hired.Name = "date_hired"
        Me.date_hired.Size = New System.Drawing.Size(284, 26)
        Me.date_hired.TabIndex = 4
        Me.date_hired.Value = New Date(2018, 8, 19, 0, 0, 0, 0)
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 295)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 20)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Remarks:"
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(189, 291)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(284, 81)
        Me.txtremarks.TabIndex = 5
        '
        'cboreason_sep
        '
        Me.cboreason_sep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboreason_sep.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboreason_sep.FormattingEnabled = True
        Me.cboreason_sep.Items.AddRange(New Object() {"Career Opportunity", "Integrity – Monetary", "Integrity – Disciplinary", "Health Problem", "Family Problem", "Work Location"})
        Me.cboreason_sep.Location = New System.Drawing.Point(189, 60)
        Me.cboreason_sep.Name = "cboreason_sep"
        Me.cboreason_sep.Size = New System.Drawing.Size(284, 27)
        Me.cboreason_sep.TabIndex = 0
        '
        'cbojobtitle
        '
        Me.cbojobtitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbojobtitle.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbojobtitle.FormattingEnabled = True
        Me.cbojobtitle.Location = New System.Drawing.Point(189, 207)
        Me.cbojobtitle.Name = "cbojobtitle"
        Me.cbojobtitle.Size = New System.Drawing.Size(284, 23)
        Me.cbojobtitle.TabIndex = 52
        '
        'bttndelete
        '
        Me.bttndelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttndelete.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttndelete.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_trash_can_48
        Me.bttndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttndelete.Location = New System.Drawing.Point(256, 447)
        Me.bttndelete.Name = "bttndelete"
        Me.bttndelete.Size = New System.Drawing.Size(139, 48)
        Me.bttndelete.TabIndex = 62
        Me.bttndelete.Text = "     DELETE"
        Me.bttndelete.UseVisualStyleBackColor = True
        '
        'Frm_separation_detail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(512, 507)
        Me.Controls.Add(Me.bttndelete)
        Me.Controls.Add(Me.cbojobtitle)
        Me.Controls.Add(Me.cboreason_sep)
        Me.Controls.Add(Me.txtremarks)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.date_hired)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cbotype_sep)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.date_separation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_separation_detail"
        Me.Text = "Frm_separation_detail"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents bttncancel As Button
    Friend WithEvents bttnsave As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents date_separation As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cbotype_sep As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents date_hired As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents cboreason_sep As ComboBox
    Friend WithEvents cbojobtitle As ComboBox
    Friend WithEvents bttndelete As Button
End Class
