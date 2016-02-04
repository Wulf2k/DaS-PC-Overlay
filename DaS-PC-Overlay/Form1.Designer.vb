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
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.tabs = New System.Windows.Forms.TabControl()
        Me.tabMain = New System.Windows.Forms.TabPage()
        Me.btnSuicide = New System.Windows.Forms.Button()
        Me.lblBonfire = New System.Windows.Forms.Label()
        Me.cmbBonfire = New System.Windows.Forms.ComboBox()
        Me.lblStam = New System.Windows.Forms.Label()
        Me.lblHP = New System.Windows.Forms.Label()
        Me.lblFacing = New System.Windows.Forms.Label()
        Me.lblZpos = New System.Windows.Forms.Label()
        Me.lblYpos = New System.Windows.Forms.Label()
        Me.lblXpos = New System.Windows.Forms.Label()
        Me.chkLockPos = New System.Windows.Forms.CheckBox()
        Me.btnZMinus = New System.Windows.Forms.Button()
        Me.btnZMinusMinus = New System.Windows.Forms.Button()
        Me.btnYMinus = New System.Windows.Forms.Button()
        Me.btnYMinusMinus = New System.Windows.Forms.Button()
        Me.btnXMinus = New System.Windows.Forms.Button()
        Me.btnXMinusMinus = New System.Windows.Forms.Button()
        Me.btnZPlus = New System.Windows.Forms.Button()
        Me.btnZPlusPlus = New System.Windows.Forms.Button()
        Me.btnYPlus = New System.Windows.Forms.Button()
        Me.btnYPlusPlus = New System.Windows.Forms.Button()
        Me.btnXPlus = New System.Windows.Forms.Button()
        Me.btnXPlusPlus = New System.Windows.Forms.Button()
        Me.chkSetDeadMode = New System.Windows.Forms.CheckBox()
        Me.chkNoMapHit = New System.Windows.Forms.CheckBox()
        Me.chkNoGrav = New System.Windows.Forms.CheckBox()
        Me.lblRelease = New System.Windows.Forms.Label()
        Me.lblCharmapdata = New System.Windows.Forms.Label()
        Me.lblCharptr1 = New System.Windows.Forms.Label()
        Me.tabDebug = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nmbMPChannel = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.nmbContrast = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nmbBrighterCam = New System.Windows.Forms.NumericUpDown()
        Me.chkBrighterCam = New System.Windows.Forms.CheckBox()
        Me.chkHide = New System.Windows.Forms.CheckBox()
        Me.chkDebugDrawing = New System.Windows.Forms.CheckBox()
        Me.chkSelfVagrant = New System.Windows.Forms.CheckBox()
        Me.chkBoundingBoxes = New System.Windows.Forms.CheckBox()
        Me.tabStats = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.nmbTeamType = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.nmbPhantomType = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nmbHumanity = New System.Windows.Forms.NumericUpDown()
        Me.tabItems = New System.Windows.Forms.TabPage()
        Me.btnDropItem = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.tabs.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tabDebug.SuspendLayout()
        CType(Me.nmbMPChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmbContrast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmbBrighterCam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabStats.SuspendLayout()
        CType(Me.nmbTeamType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmbPhantomType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmbHumanity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabItems.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.Location = New System.Drawing.Point(1241, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'tabs
        '
        Me.tabs.Controls.Add(Me.tabMain)
        Me.tabs.Controls.Add(Me.tabItems)
        Me.tabs.Controls.Add(Me.tabDebug)
        Me.tabs.Controls.Add(Me.tabStats)
        Me.tabs.Location = New System.Drawing.Point(4, 12)
        Me.tabs.Name = "tabs"
        Me.tabs.SelectedIndex = 0
        Me.tabs.Size = New System.Drawing.Size(1312, 885)
        Me.tabs.TabIndex = 3
        '
        'tabMain
        '
        Me.tabMain.BackColor = System.Drawing.Color.Fuchsia
        Me.tabMain.Controls.Add(Me.btnSuicide)
        Me.tabMain.Controls.Add(Me.lblBonfire)
        Me.tabMain.Controls.Add(Me.cmbBonfire)
        Me.tabMain.Controls.Add(Me.lblStam)
        Me.tabMain.Controls.Add(Me.lblHP)
        Me.tabMain.Controls.Add(Me.lblFacing)
        Me.tabMain.Controls.Add(Me.lblZpos)
        Me.tabMain.Controls.Add(Me.lblYpos)
        Me.tabMain.Controls.Add(Me.lblXpos)
        Me.tabMain.Controls.Add(Me.chkLockPos)
        Me.tabMain.Controls.Add(Me.btnZMinus)
        Me.tabMain.Controls.Add(Me.btnZMinusMinus)
        Me.tabMain.Controls.Add(Me.btnYMinus)
        Me.tabMain.Controls.Add(Me.btnYMinusMinus)
        Me.tabMain.Controls.Add(Me.btnXMinus)
        Me.tabMain.Controls.Add(Me.btnXMinusMinus)
        Me.tabMain.Controls.Add(Me.btnZPlus)
        Me.tabMain.Controls.Add(Me.btnZPlusPlus)
        Me.tabMain.Controls.Add(Me.btnYPlus)
        Me.tabMain.Controls.Add(Me.btnYPlusPlus)
        Me.tabMain.Controls.Add(Me.btnXPlus)
        Me.tabMain.Controls.Add(Me.btnXPlusPlus)
        Me.tabMain.Controls.Add(Me.chkSetDeadMode)
        Me.tabMain.Controls.Add(Me.chkNoMapHit)
        Me.tabMain.Controls.Add(Me.chkNoGrav)
        Me.tabMain.Controls.Add(Me.lblRelease)
        Me.tabMain.Controls.Add(Me.lblCharmapdata)
        Me.tabMain.Controls.Add(Me.lblCharptr1)
        Me.tabMain.Location = New System.Drawing.Point(4, 22)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMain.Size = New System.Drawing.Size(1304, 859)
        Me.tabMain.TabIndex = 0
        Me.tabMain.Text = "Main"
        '
        'btnSuicide
        '
        Me.btnSuicide.BackColor = System.Drawing.Color.LightGray
        Me.btnSuicide.Location = New System.Drawing.Point(84, 194)
        Me.btnSuicide.Name = "btnSuicide"
        Me.btnSuicide.Size = New System.Drawing.Size(16, 24)
        Me.btnSuicide.TabIndex = 45
        Me.btnSuicide.Text = "X"
        Me.btnSuicide.UseVisualStyleBackColor = False
        '
        'lblBonfire
        '
        Me.lblBonfire.AutoSize = True
        Me.lblBonfire.BackColor = System.Drawing.Color.LightGray
        Me.lblBonfire.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBonfire.Location = New System.Drawing.Point(890, 822)
        Me.lblBonfire.Name = "lblBonfire"
        Me.lblBonfire.Size = New System.Drawing.Size(78, 16)
        Me.lblBonfire.TabIndex = 44
        Me.lblBonfire.Text = "Last Bonfire"
        '
        'cmbBonfire
        '
        Me.cmbBonfire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBonfire.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBonfire.FormattingEnabled = True
        Me.cmbBonfire.Location = New System.Drawing.Point(974, 818)
        Me.cmbBonfire.Name = "cmbBonfire"
        Me.cmbBonfire.Size = New System.Drawing.Size(296, 24)
        Me.cmbBonfire.TabIndex = 43
        '
        'lblStam
        '
        Me.lblStam.AutoSize = True
        Me.lblStam.BackColor = System.Drawing.Color.LightGray
        Me.lblStam.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStam.Location = New System.Drawing.Point(187, 219)
        Me.lblStam.Name = "lblStam"
        Me.lblStam.Size = New System.Drawing.Size(63, 16)
        Me.lblStam.TabIndex = 42
        Me.lblStam.Text = "Stamina: "
        '
        'lblHP
        '
        Me.lblHP.AutoSize = True
        Me.lblHP.BackColor = System.Drawing.Color.LightGray
        Me.lblHP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHP.Location = New System.Drawing.Point(187, 198)
        Me.lblHP.Name = "lblHP"
        Me.lblHP.Size = New System.Drawing.Size(36, 16)
        Me.lblHP.TabIndex = 41
        Me.lblHP.Text = "HP:  "
        '
        'lblFacing
        '
        Me.lblFacing.AutoSize = True
        Me.lblFacing.BackColor = System.Drawing.Color.LightGray
        Me.lblFacing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacing.Location = New System.Drawing.Point(74, 264)
        Me.lblFacing.Name = "lblFacing"
        Me.lblFacing.Size = New System.Drawing.Size(55, 16)
        Me.lblFacing.TabIndex = 40
        Me.lblFacing.Text = "Facing: "
        '
        'lblZpos
        '
        Me.lblZpos.AutoSize = True
        Me.lblZpos.BackColor = System.Drawing.Color.LightGray
        Me.lblZpos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZpos.Location = New System.Drawing.Point(74, 339)
        Me.lblZpos.Name = "lblZpos"
        Me.lblZpos.Size = New System.Drawing.Size(48, 16)
        Me.lblZpos.TabIndex = 39
        Me.lblZpos.Text = "Z pos: "
        '
        'lblYpos
        '
        Me.lblYpos.AutoSize = True
        Me.lblYpos.BackColor = System.Drawing.Color.LightGray
        Me.lblYpos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYpos.Location = New System.Drawing.Point(74, 314)
        Me.lblYpos.Name = "lblYpos"
        Me.lblYpos.Size = New System.Drawing.Size(49, 16)
        Me.lblYpos.TabIndex = 38
        Me.lblYpos.Text = "Y pos: "
        '
        'lblXpos
        '
        Me.lblXpos.AutoSize = True
        Me.lblXpos.BackColor = System.Drawing.Color.LightGray
        Me.lblXpos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXpos.Location = New System.Drawing.Point(74, 289)
        Me.lblXpos.Name = "lblXpos"
        Me.lblXpos.Size = New System.Drawing.Size(48, 16)
        Me.lblXpos.TabIndex = 37
        Me.lblXpos.Text = "X pos: "
        '
        'chkLockPos
        '
        Me.chkLockPos.AutoSize = True
        Me.chkLockPos.BackColor = System.Drawing.Color.LightGray
        Me.chkLockPos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockPos.Location = New System.Drawing.Point(20, 366)
        Me.chkLockPos.Name = "chkLockPos"
        Me.chkLockPos.Size = New System.Drawing.Size(80, 20)
        Me.chkLockPos.TabIndex = 36
        Me.chkLockPos.Text = "LockPos"
        Me.chkLockPos.UseVisualStyleBackColor = False
        '
        'btnZMinus
        '
        Me.btnZMinus.BackColor = System.Drawing.Color.LightGray
        Me.btnZMinus.Location = New System.Drawing.Point(46, 337)
        Me.btnZMinus.Name = "btnZMinus"
        Me.btnZMinus.Size = New System.Drawing.Size(18, 23)
        Me.btnZMinus.TabIndex = 35
        Me.btnZMinus.Text = "-"
        Me.btnZMinus.UseVisualStyleBackColor = False
        '
        'btnZMinusMinus
        '
        Me.btnZMinusMinus.BackColor = System.Drawing.Color.LightGray
        Me.btnZMinusMinus.Location = New System.Drawing.Point(20, 337)
        Me.btnZMinusMinus.Name = "btnZMinusMinus"
        Me.btnZMinusMinus.Size = New System.Drawing.Size(27, 23)
        Me.btnZMinusMinus.TabIndex = 34
        Me.btnZMinusMinus.Text = "--"
        Me.btnZMinusMinus.UseVisualStyleBackColor = False
        '
        'btnYMinus
        '
        Me.btnYMinus.BackColor = System.Drawing.Color.LightGray
        Me.btnYMinus.Location = New System.Drawing.Point(46, 312)
        Me.btnYMinus.Name = "btnYMinus"
        Me.btnYMinus.Size = New System.Drawing.Size(18, 23)
        Me.btnYMinus.TabIndex = 33
        Me.btnYMinus.Text = "-"
        Me.btnYMinus.UseVisualStyleBackColor = False
        '
        'btnYMinusMinus
        '
        Me.btnYMinusMinus.BackColor = System.Drawing.Color.LightGray
        Me.btnYMinusMinus.Location = New System.Drawing.Point(20, 312)
        Me.btnYMinusMinus.Name = "btnYMinusMinus"
        Me.btnYMinusMinus.Size = New System.Drawing.Size(27, 23)
        Me.btnYMinusMinus.TabIndex = 32
        Me.btnYMinusMinus.Text = "--"
        Me.btnYMinusMinus.UseVisualStyleBackColor = False
        '
        'btnXMinus
        '
        Me.btnXMinus.BackColor = System.Drawing.Color.LightGray
        Me.btnXMinus.Location = New System.Drawing.Point(46, 287)
        Me.btnXMinus.Name = "btnXMinus"
        Me.btnXMinus.Size = New System.Drawing.Size(18, 23)
        Me.btnXMinus.TabIndex = 31
        Me.btnXMinus.Text = "-"
        Me.btnXMinus.UseVisualStyleBackColor = False
        '
        'btnXMinusMinus
        '
        Me.btnXMinusMinus.BackColor = System.Drawing.Color.LightGray
        Me.btnXMinusMinus.Location = New System.Drawing.Point(20, 287)
        Me.btnXMinusMinus.Name = "btnXMinusMinus"
        Me.btnXMinusMinus.Size = New System.Drawing.Size(27, 23)
        Me.btnXMinusMinus.TabIndex = 30
        Me.btnXMinusMinus.Text = "--"
        Me.btnXMinusMinus.UseVisualStyleBackColor = False
        '
        'btnZPlus
        '
        Me.btnZPlus.BackColor = System.Drawing.Color.LightGray
        Me.btnZPlus.Location = New System.Drawing.Point(185, 337)
        Me.btnZPlus.Name = "btnZPlus"
        Me.btnZPlus.Size = New System.Drawing.Size(18, 23)
        Me.btnZPlus.TabIndex = 29
        Me.btnZPlus.Text = "+"
        Me.btnZPlus.UseVisualStyleBackColor = False
        '
        'btnZPlusPlus
        '
        Me.btnZPlusPlus.BackColor = System.Drawing.Color.LightGray
        Me.btnZPlusPlus.Location = New System.Drawing.Point(202, 337)
        Me.btnZPlusPlus.Name = "btnZPlusPlus"
        Me.btnZPlusPlus.Size = New System.Drawing.Size(27, 23)
        Me.btnZPlusPlus.TabIndex = 28
        Me.btnZPlusPlus.Text = "++"
        Me.btnZPlusPlus.UseVisualStyleBackColor = False
        '
        'btnYPlus
        '
        Me.btnYPlus.BackColor = System.Drawing.Color.LightGray
        Me.btnYPlus.Location = New System.Drawing.Point(185, 312)
        Me.btnYPlus.Name = "btnYPlus"
        Me.btnYPlus.Size = New System.Drawing.Size(18, 23)
        Me.btnYPlus.TabIndex = 27
        Me.btnYPlus.Text = "+"
        Me.btnYPlus.UseVisualStyleBackColor = False
        '
        'btnYPlusPlus
        '
        Me.btnYPlusPlus.BackColor = System.Drawing.Color.LightGray
        Me.btnYPlusPlus.Location = New System.Drawing.Point(202, 312)
        Me.btnYPlusPlus.Name = "btnYPlusPlus"
        Me.btnYPlusPlus.Size = New System.Drawing.Size(27, 23)
        Me.btnYPlusPlus.TabIndex = 26
        Me.btnYPlusPlus.Text = "++"
        Me.btnYPlusPlus.UseVisualStyleBackColor = False
        '
        'btnXPlus
        '
        Me.btnXPlus.BackColor = System.Drawing.Color.LightGray
        Me.btnXPlus.Location = New System.Drawing.Point(185, 287)
        Me.btnXPlus.Name = "btnXPlus"
        Me.btnXPlus.Size = New System.Drawing.Size(18, 23)
        Me.btnXPlus.TabIndex = 25
        Me.btnXPlus.Text = "+"
        Me.btnXPlus.UseVisualStyleBackColor = False
        '
        'btnXPlusPlus
        '
        Me.btnXPlusPlus.BackColor = System.Drawing.Color.LightGray
        Me.btnXPlusPlus.Location = New System.Drawing.Point(202, 287)
        Me.btnXPlusPlus.Name = "btnXPlusPlus"
        Me.btnXPlusPlus.Size = New System.Drawing.Size(27, 23)
        Me.btnXPlusPlus.TabIndex = 24
        Me.btnXPlusPlus.Text = "++"
        Me.btnXPlusPlus.UseVisualStyleBackColor = False
        '
        'chkSetDeadMode
        '
        Me.chkSetDeadMode.AutoSize = True
        Me.chkSetDeadMode.BackColor = System.Drawing.Color.LightGray
        Me.chkSetDeadMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSetDeadMode.Location = New System.Drawing.Point(20, 778)
        Me.chkSetDeadMode.Name = "chkSetDeadMode"
        Me.chkSetDeadMode.Size = New System.Drawing.Size(116, 20)
        Me.chkSetDeadMode.TabIndex = 23
        Me.chkSetDeadMode.Text = "SetDeadMode"
        Me.chkSetDeadMode.UseVisualStyleBackColor = False
        '
        'chkNoMapHit
        '
        Me.chkNoMapHit.AutoSize = True
        Me.chkNoMapHit.BackColor = System.Drawing.Color.LightGray
        Me.chkNoMapHit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNoMapHit.Location = New System.Drawing.Point(20, 798)
        Me.chkNoMapHit.Name = "chkNoMapHit"
        Me.chkNoMapHit.Size = New System.Drawing.Size(117, 20)
        Me.chkNoMapHit.TabIndex = 22
        Me.chkNoMapHit.Text = "DisableMapHit"
        Me.chkNoMapHit.UseVisualStyleBackColor = False
        '
        'chkNoGrav
        '
        Me.chkNoGrav.AutoSize = True
        Me.chkNoGrav.BackColor = System.Drawing.Color.LightGray
        Me.chkNoGrav.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNoGrav.Location = New System.Drawing.Point(20, 818)
        Me.chkNoGrav.Name = "chkNoGrav"
        Me.chkNoGrav.Size = New System.Drawing.Size(116, 20)
        Me.chkNoGrav.TabIndex = 21
        Me.chkNoGrav.Text = "DisableGravity"
        Me.chkNoGrav.UseVisualStyleBackColor = False
        '
        'lblRelease
        '
        Me.lblRelease.AutoSize = True
        Me.lblRelease.BackColor = System.Drawing.Color.LightGray
        Me.lblRelease.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelease.Location = New System.Drawing.Point(7, 74)
        Me.lblRelease.Name = "lblRelease"
        Me.lblRelease.Size = New System.Drawing.Size(166, 16)
        Me.lblRelease.TabIndex = 3
        Me.lblRelease.Text = "Release version detected."
        '
        'lblCharmapdata
        '
        Me.lblCharmapdata.AutoSize = True
        Me.lblCharmapdata.BackColor = System.Drawing.Color.LightGray
        Me.lblCharmapdata.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCharmapdata.Location = New System.Drawing.Point(7, 35)
        Me.lblCharmapdata.Name = "lblCharmapdata"
        Me.lblCharmapdata.Size = New System.Drawing.Size(90, 16)
        Me.lblCharmapdata.TabIndex = 2
        Me.lblCharmapdata.Text = "Charmapdata"
        '
        'lblCharptr1
        '
        Me.lblCharptr1.AutoSize = True
        Me.lblCharptr1.BackColor = System.Drawing.Color.LightGray
        Me.lblCharptr1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCharptr1.Location = New System.Drawing.Point(7, 7)
        Me.lblCharptr1.Name = "lblCharptr1"
        Me.lblCharptr1.Size = New System.Drawing.Size(58, 16)
        Me.lblCharptr1.TabIndex = 0
        Me.lblCharptr1.Text = "Charptr1"
        '
        'tabDebug
        '
        Me.tabDebug.BackColor = System.Drawing.Color.Fuchsia
        Me.tabDebug.Controls.Add(Me.Label6)
        Me.tabDebug.Controls.Add(Me.nmbMPChannel)
        Me.tabDebug.Controls.Add(Me.Label5)
        Me.tabDebug.Controls.Add(Me.nmbContrast)
        Me.tabDebug.Controls.Add(Me.Label4)
        Me.tabDebug.Controls.Add(Me.nmbBrighterCam)
        Me.tabDebug.Controls.Add(Me.chkBrighterCam)
        Me.tabDebug.Controls.Add(Me.chkHide)
        Me.tabDebug.Controls.Add(Me.chkDebugDrawing)
        Me.tabDebug.Controls.Add(Me.chkSelfVagrant)
        Me.tabDebug.Controls.Add(Me.chkBoundingBoxes)
        Me.tabDebug.Location = New System.Drawing.Point(4, 22)
        Me.tabDebug.Name = "tabDebug"
        Me.tabDebug.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDebug.Size = New System.Drawing.Size(1304, 859)
        Me.tabDebug.TabIndex = 1
        Me.tabDebug.Text = "Debug"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.LightGray
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 441)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 16)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "MP Channel"
        '
        'nmbMPChannel
        '
        Me.nmbMPChannel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmbMPChannel.Location = New System.Drawing.Point(106, 439)
        Me.nmbMPChannel.Name = "nmbMPChannel"
        Me.nmbMPChannel.Size = New System.Drawing.Size(53, 22)
        Me.nmbMPChannel.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.LightGray
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(172, 393)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 16)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Contrast"
        '
        'nmbContrast
        '
        Me.nmbContrast.DecimalPlaces = 1
        Me.nmbContrast.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmbContrast.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmbContrast.Location = New System.Drawing.Point(174, 412)
        Me.nmbContrast.Name = "nmbContrast"
        Me.nmbContrast.Size = New System.Drawing.Size(53, 22)
        Me.nmbContrast.TabIndex = 42
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.LightGray
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(99, 393)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 16)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Brightness"
        '
        'nmbBrighterCam
        '
        Me.nmbBrighterCam.DecimalPlaces = 1
        Me.nmbBrighterCam.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmbBrighterCam.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmbBrighterCam.Location = New System.Drawing.Point(106, 412)
        Me.nmbBrighterCam.Name = "nmbBrighterCam"
        Me.nmbBrighterCam.Size = New System.Drawing.Size(53, 22)
        Me.nmbBrighterCam.TabIndex = 23
        '
        'chkBrighterCam
        '
        Me.chkBrighterCam.AutoSize = True
        Me.chkBrighterCam.BackColor = System.Drawing.Color.LightGray
        Me.chkBrighterCam.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBrighterCam.Location = New System.Drawing.Point(16, 412)
        Me.chkBrighterCam.Name = "chkBrighterCam"
        Me.chkBrighterCam.Size = New System.Drawing.Size(70, 20)
        Me.chkBrighterCam.TabIndex = 22
        Me.chkBrighterCam.Text = "Enable"
        Me.chkBrighterCam.UseVisualStyleBackColor = False
        '
        'chkHide
        '
        Me.chkHide.AutoSize = True
        Me.chkHide.BackColor = System.Drawing.Color.LightGray
        Me.chkHide.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHide.Location = New System.Drawing.Point(16, 554)
        Me.chkHide.Name = "chkHide"
        Me.chkHide.Size = New System.Drawing.Size(98, 20)
        Me.chkHide.TabIndex = 21
        Me.chkHide.Text = "Player Hide"
        Me.chkHide.UseVisualStyleBackColor = False
        '
        'chkDebugDrawing
        '
        Me.chkDebugDrawing.AutoSize = True
        Me.chkDebugDrawing.BackColor = System.Drawing.Color.LightGray
        Me.chkDebugDrawing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDebugDrawing.Location = New System.Drawing.Point(16, 673)
        Me.chkDebugDrawing.Name = "chkDebugDrawing"
        Me.chkDebugDrawing.Size = New System.Drawing.Size(146, 20)
        Me.chkDebugDrawing.TabIndex = 20
        Me.chkDebugDrawing.Text = "Debugging Drawing"
        Me.chkDebugDrawing.UseVisualStyleBackColor = False
        '
        'chkSelfVagrant
        '
        Me.chkSelfVagrant.AutoSize = True
        Me.chkSelfVagrant.BackColor = System.Drawing.Color.LightGray
        Me.chkSelfVagrant.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelfVagrant.Location = New System.Drawing.Point(16, 633)
        Me.chkSelfVagrant.Name = "chkSelfVagrant"
        Me.chkSelfVagrant.Size = New System.Drawing.Size(100, 20)
        Me.chkSelfVagrant.TabIndex = 19
        Me.chkSelfVagrant.Text = "Self Vagrant"
        Me.chkSelfVagrant.UseVisualStyleBackColor = False
        '
        'chkBoundingBoxes
        '
        Me.chkBoundingBoxes.AutoSize = True
        Me.chkBoundingBoxes.BackColor = System.Drawing.Color.LightGray
        Me.chkBoundingBoxes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoundingBoxes.Location = New System.Drawing.Point(16, 653)
        Me.chkBoundingBoxes.Name = "chkBoundingBoxes"
        Me.chkBoundingBoxes.Size = New System.Drawing.Size(125, 20)
        Me.chkBoundingBoxes.TabIndex = 18
        Me.chkBoundingBoxes.Text = "Bounding Boxes"
        Me.chkBoundingBoxes.UseVisualStyleBackColor = False
        '
        'tabStats
        '
        Me.tabStats.BackColor = System.Drawing.Color.Fuchsia
        Me.tabStats.Controls.Add(Me.Label3)
        Me.tabStats.Controls.Add(Me.nmbTeamType)
        Me.tabStats.Controls.Add(Me.Label2)
        Me.tabStats.Controls.Add(Me.nmbPhantomType)
        Me.tabStats.Controls.Add(Me.Label1)
        Me.tabStats.Controls.Add(Me.nmbHumanity)
        Me.tabStats.Location = New System.Drawing.Point(4, 22)
        Me.tabStats.Name = "tabStats"
        Me.tabStats.Size = New System.Drawing.Size(1304, 859)
        Me.tabStats.TabIndex = 2
        Me.tabStats.Text = "Stats"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.LightGray
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(38, 291)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Team Type"
        '
        'nmbTeamType
        '
        Me.nmbTeamType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmbTeamType.Location = New System.Drawing.Point(123, 287)
        Me.nmbTeamType.Name = "nmbTeamType"
        Me.nmbTeamType.Size = New System.Drawing.Size(47, 22)
        Me.nmbTeamType.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.LightGray
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 268)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Phantom Type"
        '
        'nmbPhantomType
        '
        Me.nmbPhantomType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmbPhantomType.Location = New System.Drawing.Point(123, 264)
        Me.nmbPhantomType.Name = "nmbPhantomType"
        Me.nmbPhantomType.Size = New System.Drawing.Size(47, 22)
        Me.nmbPhantomType.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.LightGray
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(53, 335)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Humanity"
        '
        'nmbHumanity
        '
        Me.nmbHumanity.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmbHumanity.Location = New System.Drawing.Point(123, 331)
        Me.nmbHumanity.Name = "nmbHumanity"
        Me.nmbHumanity.Size = New System.Drawing.Size(47, 22)
        Me.nmbHumanity.TabIndex = 0
        '
        'tabItems
        '
        Me.tabItems.BackColor = System.Drawing.Color.Magenta
        Me.tabItems.Controls.Add(Me.ComboBox2)
        Me.tabItems.Controls.Add(Me.ComboBox1)
        Me.tabItems.Controls.Add(Me.btnDropItem)
        Me.tabItems.Location = New System.Drawing.Point(4, 22)
        Me.tabItems.Name = "tabItems"
        Me.tabItems.Size = New System.Drawing.Size(1304, 859)
        Me.tabItems.TabIndex = 3
        Me.tabItems.Text = "Items"
        '
        'btnDropItem
        '
        Me.btnDropItem.BackColor = System.Drawing.Color.LightGray
        Me.btnDropItem.Location = New System.Drawing.Point(38, 427)
        Me.btnDropItem.Name = "btnDropItem"
        Me.btnDropItem.Size = New System.Drawing.Size(75, 23)
        Me.btnDropItem.TabIndex = 47
        Me.btnDropItem.Text = "DropItem"
        Me.btnDropItem.UseVisualStyleBackColor = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(38, 367)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(126, 24)
        Me.ComboBox1.TabIndex = 48
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(38, 397)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(126, 24)
        Me.ComboBox2.TabIndex = 49
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Fuchsia
        Me.ClientSize = New System.Drawing.Size(1319, 898)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.tabs)
        Me.Name = "Form1"
        Me.Text = "Wulf's Dark Souls Overlay"
        Me.tabs.ResumeLayout(False)
        Me.tabMain.ResumeLayout(False)
        Me.tabMain.PerformLayout()
        Me.tabDebug.ResumeLayout(False)
        Me.tabDebug.PerformLayout()
        CType(Me.nmbMPChannel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmbContrast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmbBrighterCam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabStats.ResumeLayout(False)
        Me.tabStats.PerformLayout()
        CType(Me.nmbTeamType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmbPhantomType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmbHumanity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabItems.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents tabs As System.Windows.Forms.TabControl
    Friend WithEvents tabMain As System.Windows.Forms.TabPage
    Friend WithEvents tabDebug As System.Windows.Forms.TabPage
    Friend WithEvents lblCharmapdata As System.Windows.Forms.Label
    Friend WithEvents lblCharptr1 As System.Windows.Forms.Label
    Friend WithEvents lblRelease As System.Windows.Forms.Label
    Friend WithEvents lblZpos As System.Windows.Forms.Label
    Friend WithEvents lblYpos As System.Windows.Forms.Label
    Friend WithEvents lblXpos As System.Windows.Forms.Label
    Friend WithEvents chkLockPos As System.Windows.Forms.CheckBox
    Friend WithEvents btnZMinus As System.Windows.Forms.Button
    Friend WithEvents btnZMinusMinus As System.Windows.Forms.Button
    Friend WithEvents btnYMinus As System.Windows.Forms.Button
    Friend WithEvents btnYMinusMinus As System.Windows.Forms.Button
    Friend WithEvents btnXMinus As System.Windows.Forms.Button
    Friend WithEvents btnXMinusMinus As System.Windows.Forms.Button
    Friend WithEvents btnZPlus As System.Windows.Forms.Button
    Friend WithEvents btnZPlusPlus As System.Windows.Forms.Button
    Friend WithEvents btnYPlus As System.Windows.Forms.Button
    Friend WithEvents btnYPlusPlus As System.Windows.Forms.Button
    Friend WithEvents btnXPlus As System.Windows.Forms.Button
    Friend WithEvents btnXPlusPlus As System.Windows.Forms.Button
    Friend WithEvents chkSetDeadMode As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoMapHit As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoGrav As System.Windows.Forms.CheckBox
    Friend WithEvents lblFacing As System.Windows.Forms.Label
    Friend WithEvents lblStam As System.Windows.Forms.Label
    Friend WithEvents lblHP As System.Windows.Forms.Label
    Friend WithEvents chkBoundingBoxes As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelfVagrant As System.Windows.Forms.CheckBox
    Friend WithEvents chkDebugDrawing As System.Windows.Forms.CheckBox
    Friend WithEvents tabStats As System.Windows.Forms.TabPage
    Friend WithEvents lblBonfire As System.Windows.Forms.Label
    Friend WithEvents cmbBonfire As System.Windows.Forms.ComboBox
    Friend WithEvents btnSuicide As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents nmbHumanity As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkHide As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nmbPhantomType As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nmbTeamType As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkBrighterCam As System.Windows.Forms.CheckBox
    Friend WithEvents nmbBrighterCam As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nmbContrast As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As Label
    Friend WithEvents nmbMPChannel As NumericUpDown
    Friend WithEvents tabItems As System.Windows.Forms.TabPage
    Friend WithEvents btnDropItem As System.Windows.Forms.Button
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
