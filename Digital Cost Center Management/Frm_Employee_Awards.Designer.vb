﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Employee_Awards
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
        Me.txtaward = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.date_given = New System.Windows.Forms.DateTimePicker()
        Me.txtgiven_by = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.bttndelete = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(431, 34)
        Me.Panel1.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Awards"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(396, 6)
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
        Me.lblclose.Location = New System.Drawing.Point(396, 6)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'txtaward
        '
        Me.txtaward.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaward.Location = New System.Drawing.Point(115, 54)
        Me.txtaward.Name = "txtaward"
        Me.txtaward.Size = New System.Drawing.Size(288, 26)
        Me.txtaward.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 19)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Awards:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(23, 106)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 19)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Date Given:"
        '
        'date_given
        '
        Me.date_given.CustomFormat = "yyyy-MM-dd"
        Me.date_given.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.date_given.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.date_given.Location = New System.Drawing.Point(117, 99)
        Me.date_given.Name = "date_given"
        Me.date_given.Size = New System.Drawing.Size(286, 26)
        Me.date_given.TabIndex = 24
        Me.date_given.Value = New Date(2018, 8, 19, 0, 0, 0, 0)
        '
        'txtgiven_by
        '
        Me.txtgiven_by.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgiven_by.Location = New System.Drawing.Point(115, 143)
        Me.txtgiven_by.Name = "txtgiven_by"
        Me.txtgiven_by.Size = New System.Drawing.Size(288, 26)
        Me.txtgiven_by.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 19)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Given By:"
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(115, 286)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(141, 48)
        Me.bttnsave.TabIndex = 28
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(262, 286)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(141, 48)
        Me.bttncancel.TabIndex = 29
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(115, 188)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(288, 81)
        Me.txtremarks.TabIndex = 55
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 20)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "Remarks:"
        '
        'bttndelete
        '
        Me.bttndelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttndelete.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttndelete.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_trash_can_48
        Me.bttndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttndelete.Location = New System.Drawing.Point(188, 343)
        Me.bttndelete.Name = "bttndelete"
        Me.bttndelete.Size = New System.Drawing.Size(139, 48)
        Me.bttndelete.TabIndex = 61
        Me.bttndelete.Text = "     DELETE"
        Me.bttndelete.UseVisualStyleBackColor = True
        '
        'Frm_awards
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(431, 403)
        Me.Controls.Add(Me.bttndelete)
        Me.Controls.Add(Me.txtremarks)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.txtgiven_by)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.date_given)
        Me.Controls.Add(Me.txtaward)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_awards"
        Me.Text = "Frm_awards"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents lblclose2 As Label
    Friend WithEvents lblclose As Label
    Friend WithEvents txtaward As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents date_given As DateTimePicker
    Friend WithEvents txtgiven_by As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents bttnsave As Button
    Friend WithEvents bttncancel As Button
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents bttndelete As Button
End Class
