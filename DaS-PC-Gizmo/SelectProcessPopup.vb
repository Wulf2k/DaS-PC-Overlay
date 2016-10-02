Public Class SelectProcessPopup
    Friend SelectedProcess = ""
    Dim gizmo As DaS_PC_Gizmo
    Dim WithEvents scanTimer As New Timer()
    Dim WithEvents refreshTimer As New Timer()

    Public Sub New(mainWindow As DaS_PC_Gizmo)
        gizmo = mainWindow
        InitializeComponent()
    End Sub

    Private Sub SelectProcessPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblScanStatus.Text = ""
        scanTimer.Interval = 20
        lblScanStatus.Enabled = False
        refreshList()
        buttonAccept.Enabled = False
    End Sub

    Private Sub comboBoxProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxProcess.SelectedIndexChanged
        SelectedProcess = comboBoxProcess.SelectedItem
        buttonAccept.Enabled = True
    End Sub

    Private Sub refreshList()
        comboBoxProcess.Items.Clear()
        Dim _allProcesses() As Process = Process.GetProcesses
        For Each pp As Process In _allProcesses
            If (Not pp.MainWindowHandle = 0) And (Not pp.MainWindowTitle.Length = 0) Then
                comboBoxProcess.Items.Add(pp.MainWindowHandle.ToString("X8") & " | " & pp.MainWindowTitle)
            End If
        Next
    End Sub

    Private Sub buttonAccept_Click(sender As Object, e As EventArgs) Handles buttonAccept.Click
        Close()
    End Sub

    Private Sub buttonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click
        Close()
    End Sub

    Private Sub buttonRefresh_Click(sender As Object, e As EventArgs) Handles buttonRefresh.Click
        UseWaitCursor = True
        gizmo.UseWaitCursor = True
        refreshTimer.Enabled = True
    End Sub

    Private Sub buttonAutoScan_Click(sender As Object, e As EventArgs) Handles buttonAutoScan.Click
        lblScanStatus.Text = "Scanning..."
        UseWaitCursor = True
        gizmo.UseWaitCursor = True
        scanTimer.Enabled = True
    End Sub

    Private Sub refreshTimer_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick
        refreshTimer.Enabled = False
        refreshList()
        UseWaitCursor = False
        gizmo.UseWaitCursor = False
    End Sub

    Private Sub scanTimer_Tick(sender As Object, e As EventArgs) Handles scanTimer.Tick
        scanTimer.Enabled = False
        If Not gizmo.ScanForProcess("DARK SOULS", True) Then
            lblScanStatus.Text = "Process not found."
            UseWaitCursor = False
            gizmo.UseWaitCursor = False
        Else
            DialogResult = DialogResult.Cancel 'The process was found in ScanForProcess anyways
            Close()
        End If
    End Sub
End Class