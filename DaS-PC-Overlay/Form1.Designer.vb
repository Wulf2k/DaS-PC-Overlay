<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkNoMapHit = New System.Windows.Forms.CheckBox()
        Me.chkNoGrav = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkSetDeadMode = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.chkSetDeadMode)
        Me.GroupBox1.Controls.Add(Me.chkNoMapHit)
        Me.GroupBox1.Controls.Add(Me.chkNoGrav)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(833, 115)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'chkNoMapHit
        '
        Me.chkNoMapHit.AutoSize = True
        Me.chkNoMapHit.Location = New System.Drawing.Point(7, 76)
        Me.chkNoMapHit.Name = "chkNoMapHit"
        Me.chkNoMapHit.Size = New System.Drawing.Size(74, 17)
        Me.chkNoMapHit.TabIndex = 2
        Me.chkNoMapHit.Text = "NoMapHit"
        Me.chkNoMapHit.UseVisualStyleBackColor = True
        '
        'chkNoGrav
        '
        Me.chkNoGrav.AutoSize = True
        Me.chkNoGrav.Location = New System.Drawing.Point(7, 92)
        Me.chkNoGrav.Name = "chkNoGrav"
        Me.chkNoGrav.Size = New System.Drawing.Size(63, 17)
        Me.chkNoGrav.TabIndex = 1
        Me.chkNoGrav.Text = "NoGrav"
        Me.chkNoGrav.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkSetDeadMode
        '
        Me.chkSetDeadMode.AutoSize = True
        Me.chkSetDeadMode.Location = New System.Drawing.Point(7, 60)
        Me.chkSetDeadMode.Name = "chkSetDeadMode"
        Me.chkSetDeadMode.Size = New System.Drawing.Size(95, 17)
        Me.chkSetDeadMode.TabIndex = 3
        Me.chkSetDeadMode.Text = "SetDeadMode"
        Me.chkSetDeadMode.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Fuchsia
        Me.ClientSize = New System.Drawing.Size(1319, 898)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkNoMapHit As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoGrav As System.Windows.Forms.CheckBox
    Friend WithEvents chkSetDeadMode As System.Windows.Forms.CheckBox

End Class
