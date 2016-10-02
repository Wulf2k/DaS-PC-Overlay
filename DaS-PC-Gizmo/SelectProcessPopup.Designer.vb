<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectProcessPopup
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
        Me.buttonAccept = New System.Windows.Forms.Button()
        Me.buttonCancel = New System.Windows.Forms.Button()
        Me.comboBoxProcess = New System.Windows.Forms.ComboBox()
        Me.buttonAutoScan = New System.Windows.Forms.Button()
        Me.buttonRefresh = New System.Windows.Forms.Button()
        Me.lblScanStatus = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'buttonAccept
        '
        Me.buttonAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonAccept.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.buttonAccept.Enabled = False
        Me.buttonAccept.Location = New System.Drawing.Point(324, 49)
        Me.buttonAccept.Name = "buttonAccept"
        Me.buttonAccept.Size = New System.Drawing.Size(75, 23)
        Me.buttonAccept.TabIndex = 0
        Me.buttonAccept.Text = "Accept"
        Me.buttonAccept.UseVisualStyleBackColor = True
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.buttonCancel.Location = New System.Drawing.Point(405, 49)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
        Me.buttonCancel.TabIndex = 1
        Me.buttonCancel.Text = "Cancel"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'comboBoxProcess
        '
        Me.comboBoxProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboBoxProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBoxProcess.FormattingEnabled = True
        Me.comboBoxProcess.Location = New System.Drawing.Point(13, 13)
        Me.comboBoxProcess.Name = "comboBoxProcess"
        Me.comboBoxProcess.Size = New System.Drawing.Size(441, 21)
        Me.comboBoxProcess.TabIndex = 2
        '
        'buttonAutoScan
        '
        Me.buttonAutoScan.Location = New System.Drawing.Point(12, 49)
        Me.buttonAutoScan.Name = "buttonAutoScan"
        Me.buttonAutoScan.Size = New System.Drawing.Size(130, 23)
        Me.buttonAutoScan.TabIndex = 3
        Me.buttonAutoScan.Text = "Try Automatic Scan"
        Me.buttonAutoScan.UseVisualStyleBackColor = True
        '
        'buttonRefresh
        '
        Me.buttonRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonRefresh.BackgroundImage = Global.DaS_PC_Gizmo.My.Resources.Resources.RefreshSymbol
        Me.buttonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonRefresh.Location = New System.Drawing.Point(457, 12)
        Me.buttonRefresh.Margin = New System.Windows.Forms.Padding(0)
        Me.buttonRefresh.Name = "buttonRefresh"
        Me.buttonRefresh.Size = New System.Drawing.Size(23, 23)
        Me.buttonRefresh.TabIndex = 4
        Me.buttonRefresh.UseVisualStyleBackColor = False
        '
        'lblScanStatus
        '
        Me.lblScanStatus.AutoSize = True
        Me.lblScanStatus.Location = New System.Drawing.Point(149, 55)
        Me.lblScanStatus.Name = "lblScanStatus"
        Me.lblScanStatus.Size = New System.Drawing.Size(115, 13)
        Me.lblScanStatus.TabIndex = 5
        Me.lblScanStatus.Text = "Could not find process."
        '
        'SelectProcessPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 81)
        Me.Controls.Add(Me.lblScanStatus)
        Me.Controls.Add(Me.buttonRefresh)
        Me.Controls.Add(Me.buttonAutoScan)
        Me.Controls.Add(Me.comboBoxProcess)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonAccept)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectProcessPopup"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Dark Souls Process"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents buttonAccept As Button
    Friend WithEvents buttonCancel As Button
    Friend WithEvents comboBoxProcess As ComboBox
    Friend WithEvents buttonAutoScan As Button
    Friend WithEvents buttonRefresh As Button
    Friend WithEvents lblScanStatus As Label
End Class
