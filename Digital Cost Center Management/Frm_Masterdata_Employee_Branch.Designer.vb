<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Masterdata_Employee_Branch
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtbranch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtbranch_code_ni = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.bttnnew = New System.Windows.Forms.Button()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtbranch_code = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtbranch_ns = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtregion_code = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtbranch_prefix = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtbranch_address = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtmw_id = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Size = New System.Drawing.Size(735, 34)
        Me.Panel1.TabIndex = 14
        '
        'lblform_name
        '
        Me.lblform_name.AutoSize = True
        Me.lblform_name.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblform_name.ForeColor = System.Drawing.Color.White
        Me.lblform_name.Location = New System.Drawing.Point(3, 6)
        Me.lblform_name.Name = "lblform_name"
        Me.lblform_name.Size = New System.Drawing.Size(76, 24)
        Me.lblform_name.TabIndex = 11
        Me.lblform_name.Text = "Brand"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(700, 3)
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
        Me.lblclose.Location = New System.Drawing.Point(700, 3)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 396)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 19)
        Me.Label2.TabIndex = 173
        Me.Label2.Text = "Branch:"
        '
        'txtbranch
        '
        Me.txtbranch.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch.Location = New System.Drawing.Point(77, 396)
        Me.txtbranch.Name = "txtbranch"
        Me.txtbranch.Size = New System.Drawing.Size(275, 26)
        Me.txtbranch.TabIndex = 172
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 348)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 38)
        Me.Label1.TabIndex = 171
        Me.Label1.Text = "Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Code NI:"
        '
        'txtbranch_code_ni
        '
        Me.txtbranch_code_ni.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch_code_ni.Location = New System.Drawing.Point(77, 354)
        Me.txtbranch_code_ni.Name = "txtbranch_code_ni"
        Me.txtbranch_code_ni.Size = New System.Drawing.Size(275, 26)
        Me.txtbranch_code_ni.TabIndex = 170
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_search_50
        Me.PictureBox1.Location = New System.Drawing.Point(6, 47)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 169
        Me.PictureBox1.TabStop = False
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(41, 47)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(295, 25)
        Me.txtsearch.TabIndex = 168
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8})
        Me.ListView2.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.Location = New System.Drawing.Point(7, 92)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(714, 193)
        Me.ListView2.TabIndex = 167
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Branch Code"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Branch Code NI"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Branch"
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Branch Netsuite"
        Me.ColumnHeader4.Width = 250
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Region Code"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Branch Prefix"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Branch Address"
        Me.ColumnHeader7.Width = 300
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Main Warehouse ID"
        Me.ColumnHeader8.Width = 130
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(417, 529)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(142, 39)
        Me.bttnsave.TabIndex = 164
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'bttnnew
        '
        Me.bttnnew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnnew.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnnew.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_add_new_30
        Me.bttnnew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnnew.Location = New System.Drawing.Point(82, 529)
        Me.bttnnew.Name = "bttnnew"
        Me.bttnnew.Size = New System.Drawing.Size(140, 39)
        Me.bttnnew.TabIndex = 166
        Me.bttnnew.Text = "NEW"
        Me.bttnnew.UseVisualStyleBackColor = True
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(251, 529)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(142, 39)
        Me.bttncancel.TabIndex = 165
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 301)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(59, 38)
        Me.Label13.TabIndex = 163
        Me.Label13.Text = "Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Code:"
        '
        'txtbranch_code
        '
        Me.txtbranch_code.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch_code.Location = New System.Drawing.Point(77, 313)
        Me.txtbranch_code.Name = "txtbranch_code"
        Me.txtbranch_code.Size = New System.Drawing.Size(275, 26)
        Me.txtbranch_code.TabIndex = 162
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 426)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 38)
        Me.Label3.TabIndex = 175
        Me.Label3.Text = "Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "NS:"
        '
        'txtbranch_ns
        '
        Me.txtbranch_ns.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch_ns.Location = New System.Drawing.Point(77, 438)
        Me.txtbranch_ns.Name = "txtbranch_ns"
        Me.txtbranch_ns.Size = New System.Drawing.Size(275, 26)
        Me.txtbranch_ns.TabIndex = 174
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(374, 301)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 38)
        Me.Label4.TabIndex = 177
        Me.Label4.Text = "Region" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Code:"
        '
        'txtregion_code
        '
        Me.txtregion_code.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtregion_code.Location = New System.Drawing.Point(448, 313)
        Me.txtregion_code.Name = "txtregion_code"
        Me.txtregion_code.Size = New System.Drawing.Size(275, 26)
        Me.txtregion_code.TabIndex = 176
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(374, 348)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 38)
        Me.Label5.TabIndex = 179
        Me.Label5.Text = "Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Prefix:"
        '
        'txtbranch_prefix
        '
        Me.txtbranch_prefix.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch_prefix.Location = New System.Drawing.Point(448, 354)
        Me.txtbranch_prefix.Name = "txtbranch_prefix"
        Me.txtbranch_prefix.Size = New System.Drawing.Size(275, 26)
        Me.txtbranch_prefix.TabIndex = 178
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(372, 438)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 38)
        Me.Label6.TabIndex = 180
        Me.Label6.Text = "Branch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Address:"
        '
        'txtbranch_address
        '
        Me.txtbranch_address.Location = New System.Drawing.Point(448, 442)
        Me.txtbranch_address.Multiline = True
        Me.txtbranch_address.Name = "txtbranch_address"
        Me.txtbranch_address.Size = New System.Drawing.Size(273, 59)
        Me.txtbranch_address.TabIndex = 181
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(374, 396)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 19)
        Me.Label7.TabIndex = 183
        Me.Label7.Text = "MW ID:"
        '
        'txtmw_id
        '
        Me.txtmw_id.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmw_id.Location = New System.Drawing.Point(448, 396)
        Me.txtmw_id.Name = "txtmw_id"
        Me.txtmw_id.Size = New System.Drawing.Size(275, 26)
        Me.txtmw_id.TabIndex = 182
        '
        'Frm_Masterdata_Employee_Branch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(735, 594)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtmw_id)
        Me.Controls.Add(Me.txtbranch_address)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtbranch_prefix)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtregion_code)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtbranch_ns)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtbranch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtbranch_code_ni)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtsearch)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.bttnnew)
        Me.Controls.Add(Me.bttncancel)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtbranch_code)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_Masterdata_Employee_Branch"
        Me.Text = "Frm_Masterdata_Employee_Branch"
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
    Friend WithEvents Label2 As Label
    Friend WithEvents txtbranch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtbranch_code_ni As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtsearch As TextBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents bttnsave As Button
    Friend WithEvents bttnnew As Button
    Friend WithEvents bttncancel As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txtbranch_code As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtbranch_ns As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtregion_code As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtbranch_prefix As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtbranch_address As TextBox
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents Label7 As Label
    Friend WithEvents txtmw_id As TextBox
End Class
