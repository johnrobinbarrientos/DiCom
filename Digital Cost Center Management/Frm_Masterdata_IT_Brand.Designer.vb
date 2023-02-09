<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_masterdata_IT_brand
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblform_name = New System.Windows.Forms.Label()
        Me.lblclose2 = New System.Windows.Forms.Label()
        Me.lblclose = New System.Windows.Forms.Label()
        Me.bttnsave = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.bttnnew = New System.Windows.Forms.Button()
        Me.bttncancel = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtbrand = New System.Windows.Forms.TextBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cbotype_tag = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
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
        Me.Panel1.Size = New System.Drawing.Size(401, 34)
        Me.Panel1.TabIndex = 14
        '
        'lblform_name
        '
        Me.lblform_name.AutoSize = True
        Me.lblform_name.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblform_name.ForeColor = System.Drawing.Color.White
        Me.lblform_name.Location = New System.Drawing.Point(3, 6)
        Me.lblform_name.Name = "lblform_name"
        Me.lblform_name.Size = New System.Drawing.Size(105, 24)
        Me.lblform_name.TabIndex = 11
        Me.lblform_name.Text = "IT Brand"
        '
        'lblclose2
        '
        Me.lblclose2.AutoSize = True
        Me.lblclose2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblclose2.Font = New System.Drawing.Font("Cooper Black", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclose2.ForeColor = System.Drawing.Color.Red
        Me.lblclose2.Location = New System.Drawing.Point(365, 3)
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
        Me.lblclose.Location = New System.Drawing.Point(365, 3)
        Me.lblclose.Name = "lblclose"
        Me.lblclose.Size = New System.Drawing.Size(35, 31)
        Me.lblclose.TabIndex = 9
        Me.lblclose.Text = "X"
        '
        'bttnsave
        '
        Me.bttnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnsave.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnsave.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_save_30
        Me.bttnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnsave.Location = New System.Drawing.Point(47, 398)
        Me.bttnsave.Name = "bttnsave"
        Me.bttnsave.Size = New System.Drawing.Size(142, 39)
        Me.bttnsave.TabIndex = 112
        Me.bttnsave.Text = "SAVE"
        Me.bttnsave.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_search_50
        Me.PictureBox1.Location = New System.Drawing.Point(12, 55)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 116
        Me.PictureBox1.TabStop = False
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(47, 55)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(203, 25)
        Me.txtsearch.TabIndex = 115
        '
        'bttnnew
        '
        Me.bttnnew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttnnew.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttnnew.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_add_new_30
        Me.bttnnew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttnnew.Location = New System.Drawing.Point(139, 448)
        Me.bttnnew.Name = "bttnnew"
        Me.bttnnew.Size = New System.Drawing.Size(140, 39)
        Me.bttnnew.TabIndex = 114
        Me.bttnnew.Text = "NEW"
        Me.bttnnew.UseVisualStyleBackColor = True
        '
        'bttncancel
        '
        Me.bttncancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bttncancel.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttncancel.Image = Global.Digital_Cost_Center_Management.My.Resources.Resources.icons8_cancel_30
        Me.bttncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bttncancel.Location = New System.Drawing.Point(204, 398)
        Me.bttncancel.Name = "bttncancel"
        Me.bttncancel.Size = New System.Drawing.Size(142, 39)
        Me.bttncancel.TabIndex = 113
        Me.bttncancel.Text = "CANCEL"
        Me.bttncancel.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(13, 310)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 19)
        Me.Label13.TabIndex = 111
        Me.Label13.Text = "Brand:"
        '
        'txtbrand
        '
        Me.txtbrand.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbrand.Location = New System.Drawing.Point(90, 303)
        Me.txtbrand.Name = "txtbrand"
        Me.txtbrand.Size = New System.Drawing.Size(271, 26)
        Me.txtbrand.TabIndex = 110
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView2.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.Location = New System.Drawing.Point(12, 102)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(365, 170)
        Me.ListView2.TabIndex = 121
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Brand"
        Me.ColumnHeader2.Width = 300
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Type"
        Me.ColumnHeader3.Width = 150
        '
        'cbotype_tag
        '
        Me.cbotype_tag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotype_tag.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbotype_tag.FormattingEnabled = True
        Me.cbotype_tag.Items.AddRange(New Object() {"IT", "Fleet"})
        Me.cbotype_tag.Location = New System.Drawing.Point(90, 347)
        Me.cbotype_tag.Name = "cbotype_tag"
        Me.cbotype_tag.Size = New System.Drawing.Size(271, 28)
        Me.cbotype_tag.TabIndex = 123
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Cambria", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(14, 352)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(69, 17)
        Me.Label24.TabIndex = 124
        Me.Label24.Text = "Type Tag:"
        '
        'Frm_masterdata_IT_brand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(401, 517)
        Me.Controls.Add(Me.cbotype_tag)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.bttnsave)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtsearch)
        Me.Controls.Add(Me.txtbrand)
        Me.Controls.Add(Me.bttnnew)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.bttncancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_masterdata_IT_brand"
        Me.Text = "Frm_masterdata_IT_brand"
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
    Friend WithEvents bttnsave As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtsearch As TextBox
    Friend WithEvents bttnnew As Button
    Friend WithEvents bttncancel As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txtbrand As TextBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents cbotype_tag As ComboBox
    Friend WithEvents Label24 As Label
End Class
