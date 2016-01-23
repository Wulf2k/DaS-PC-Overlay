Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Threading


Public Class Form1

    Private _Font As New Font("Courier New", 16)

    Private WithEvents refTimer As New System.Windows.Forms.Timer()

    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAcess As UInt32, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Int32) As IntPtr
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer() As Byte, ByVal iSize As Integer, ByRef lpNumberOfBytesRead As Integer) As Boolean
    Private Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer() As Byte, ByVal iSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Boolean
    Private Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As Boolean

    Dim clearctr As UInteger


    Dim charptr1 As UInteger
    Dim charmapdataptr As UInteger
    Dim charposdataptr As UInteger

    Dim gamestatsptr As UInteger


    Dim charptr2 As UInteger
    Dim charptr3 As UInteger
    Dim enemyptr As UInteger
    Dim enemyptr2 As UInteger
    Dim enemyptr3 As UInteger
    Dim enemyptr4 As UInteger
    Dim tendptr As UInteger

    Dim delay As Integer = 500

    Dim crtCount As Integer

    Dim playerHP As Integer
    Dim playerStam As Integer

    Dim playerMaxHP As Integer
    Dim playerMaxStam As Integer

    Dim playerFacing As Integer
    Dim playerXpos As Integer
    Dim playerYpos As Integer
    Dim playerZpos As Integer

    Private _targetProcess As Process = Nothing 'to keep track of it. not used yet.
    Private _targetProcessHandle As IntPtr = IntPtr.Zero 'Used for ReadProcessMemory
    Private PROCESS_ALL_ACCESS As UInt32 = &H1F0FFF
    Private PROCESS_VM_READ As UInt32 = &H10
    Public Function TryAttachToProcess(ByVal windowCaption As String) As Boolean
        Dim _allProcesses() As Process = Process.GetProcesses
        For Each pp As Process In _allProcesses
            If pp.MainWindowTitle.ToLower.Equals(windowCaption.ToLower) Then
                'found it! proceed.
                Return TryAttachToProcess(pp)
            End If
        Next
        MessageBox.Show("Unable to find process '" & windowCaption & ".' Is running?")
        Return False
    End Function
    Public Function TryAttachToProcess(ByVal proc As Process) As Boolean
        If _targetProcessHandle = IntPtr.Zero Then 'not already attached
            _targetProcess = proc
            _targetProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, False, _targetProcess.Id)
            If _targetProcessHandle = 0 Then
                TryAttachToProcess = False
                MessageBox.Show("OpenProcess() FAIL! Are you Administrator??")
            Else
                'if we get here, all connected and ready to use ReadProcessMemory()
                TryAttachToProcess = True
                'MessageBox.Show("OpenProcess() OK")
            End If
        Else
            MessageBox.Show("Already attached! (Please Detach first?)")
            TryAttachToProcess = False
        End If
    End Function
    Public Sub DetachFromProcess()
        If Not (_targetProcessHandle = IntPtr.Zero) Then
            _targetProcess = Nothing
            Try
                CloseHandle(_targetProcessHandle)
                _targetProcessHandle = IntPtr.Zero
                MessageBox.Show("MemReader::Detach() OK")
            Catch ex As Exception
                MessageBox.Show("MemoryManager::DetachFromProcess::CloseHandle error " & Environment.NewLine & ex.Message)
            End Try
        End If
    End Sub

    Public Function ReadInt16(ByVal addr As IntPtr) As Int16
        Dim _rtnBytes(1) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 2, vbNull)
        Return BitConverter.ToInt16(_rtnBytes, 0)
    End Function
    Public Function ReadInt32(ByVal addr As IntPtr) As Int32
        Dim _rtnBytes(3) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 4, vbNull)

        Return BitConverter.ToInt32(_rtnBytes, 0)
    End Function
    Public Function ReadInt64(ByVal addr As IntPtr) As Int64
        Dim _rtnBytes(7) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 8, vbNull)
        Return BitConverter.ToInt64(_rtnBytes, 0)
    End Function
    Public Function ReadUInt16(ByVal addr As IntPtr) As UInt16
        Dim _rtnBytes(1) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 2, vbNull)
        Return BitConverter.ToUInt16(_rtnBytes, 0)
    End Function
    Public Function ReadUInt32(ByVal addr As IntPtr) As UInt32
        Dim _rtnBytes(3) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 4, vbNull)
        Return BitConverter.ToUInt32(_rtnBytes, 0)
    End Function
    Public Function ReadUInt64(ByVal addr As IntPtr) As UInt64
        Dim _rtnBytes(7) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 8, vbNull)
        Return BitConverter.ToUInt64(_rtnBytes, 0)
    End Function
    Public Function ReadFloat(ByVal addr As IntPtr) As Single
        Dim _rtnBytes(3) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 4, vbNull)
        Return BitConverter.ToSingle(_rtnBytes, 0)
    End Function
    Public Function ReadDouble(ByVal addr As IntPtr) As Double
        Dim _rtnBytes(7) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 8, vbNull)
        Return BitConverter.ToDouble(_rtnBytes, 0)
    End Function
    Public Function ReadIntPtr(ByVal addr As IntPtr) As IntPtr
        Dim _rtnBytes(IntPtr.Size - 1) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, IntPtr.Size, Nothing)
        If IntPtr.Size = 4 Then
            Return New IntPtr(BitConverter.ToUInt32(_rtnBytes, 0))
        Else
            Return New IntPtr(BitConverter.ToInt64(_rtnBytes, 0))
        End If
    End Function
    Public Function ReadBytes(ByVal addr As IntPtr, ByVal size As Int32) As Byte()
        Dim _rtnBytes(size - 1) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, size, vbNull)
        Return _rtnBytes
    End Function

    Public Sub WriteUInt32(ByVal addr As IntPtr, val As UInt32)
        WriteProcessMemory(_targetProcessHandle, addr, BitConverter.GetBytes(val), 4, Nothing)
    End Sub
    Public Sub WriteFloat(ByVal addr As IntPtr, val As Single)
        WriteProcessMemory(_targetProcessHandle, addr, BitConverter.GetBytes(val), 4, Nothing)
    End Sub
    Public Sub WriteBytes(ByVal addr As IntPtr, val As Byte())
        WriteProcessMemory(_targetProcessHandle, addr, val, val.Length, Nothing)
    End Sub

    Public Sub DrawString(ByVal text As String, ByVal pt As Point, ByVal col As Brush)

        Using g As Graphics = Me.CreateGraphics
            g.DrawString(text, _Font, Brushes.Black, pt.X - 1, pt.Y) 'left
            g.DrawString(text, _Font, Brushes.Black, pt.X, pt.Y + 1) 'top
            g.DrawString(text, _Font, Brushes.Black, pt.X + 1, pt.Y) 'right
            g.DrawString(text, _Font, Brushes.Black, pt.X, pt.Y + 1) 'bottom
            g.DrawString(text, _Font, Brushes.Black, pt.X - 1, pt.Y - 1) 'top left
            g.DrawString(text, _Font, Brushes.Black, pt.X - 1, pt.Y + 1) 'bottom left
            g.DrawString(text, _Font, Brushes.Black, pt.X + 1, pt.Y - 1) 'top right
            g.DrawString(text, _Font, Brushes.Black, pt.X + 1, pt.Y + 1) 'bottom right
            g.DrawString(text, _Font, col, pt)
        End Using

    End Sub

    Private Sub Refresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Me.CreateGraphics.Clear(BackColor)


        Me.TopMost = True

        If tabs.SelectedIndex() = 0 Then

            btnXMinus.Visible = True
            btnXMinusMinus.Visible = True
            btnYMinus.Visible = True
            btnYMinusMinus.Visible = True
            btnZMinus.Visible = True
            btnZMinusMinus.Visible = True

            btnXPlus.Visible = True
            btnXPlusPlus.Visible = True
            btnYPlus.Visible = True
            btnYPlusPlus.Visible = True
            btnZPlus.Visible = True
            btnZPlusPlus.Visible = True

            chkLockPos.Visible = True
            chkNoGrav.Visible = True
            chkNoMapHit.Visible = True
            chkSetDeadMode.Visible = True



            DrawString(playerHP & " / " & playerMaxHP, New Point(200, 230), Brushes.Yellow)
            DrawString(playerStam & " / " & playerMaxStam, New Point(200, 250), Brushes.Yellow)


            DrawString("Facing: " & playerFacing, New Point(70, 300), Brushes.White)
            DrawString("X: " & playerXpos, New Point(70, 325), Brushes.White)
            DrawString("Y: " & playerYpos, New Point(70, 350), Brushes.White)
            DrawString("Z: " & playerZpos, New Point(70, 375), Brushes.White)

            DrawString("LockPos", New Point(70, 775), Brushes.White)
            DrawString("SetDeadMode", New Point(70, 800), Brushes.White)
            DrawString("NoMapHit", New Point(70, 825), Brushes.White)
            DrawString("NoGrav", New Point(70, 850), Brushes.White)


        Else
            btnXMinus.Visible = False
            btnXMinusMinus.Visible = False
            btnYMinus.Visible = False
            btnYMinusMinus.Visible = False
            btnZMinus.Visible = False
            btnZMinusMinus.Visible = False

            btnXPlus.Visible = False
            btnXPlusPlus.Visible = False
            btnYPlus.Visible = False
            btnYPlusPlus.Visible = False
            btnZPlus.Visible = False
            btnZPlusPlus.Visible = False

            chkLockPos.Visible = False
            chkNoGrav.Visible = False
            chkNoMapHit.Visible = False
            chkSetDeadMode.Visible = False
        End If



        'DrawString("Charptr1: " & Hex(charptr1), New Point(50, 380), Brushes.White)
    End Sub
    <System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="GetWindowRect")>
    Shared Function GetWindowRectangle(
           ByVal [Handle] As IntPtr,
           ByRef [Rectangle] As Rectangle
    ) As Boolean
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TransparencyKey = Me.BackColor
        Me.TopMost = True

        Dim rect As New Rectangle
        Dim hwnd As IntPtr = Process.GetProcessesByName("DARKSOULS").First.MainWindowHandle

        GetWindowRectangle(hwnd, rect)

        Me.Location = New Point(rect.X - 10, rect.Y - 160)

        refTimer = New System.Windows.Forms.Timer
        refTimer.Interval = delay
        refTimer.Enabled = True
        refTimer.Start()

        TryAttachToProcess("DARK SOULS")

    End Sub

    Private Sub refTimer_Tick() Handles refTimer.Tick
        'crtCount = Four2UInteger(Four2UInteger(&H1B3D6E0) + &H14)

        'charptr1 = Four2UInteger(Four2UInteger(Four2UInteger(&H1B3D6E0) + &HC) + crtCount * 4)
        ' charptr2 = Four2UInteger(charptr1 + &H49C)
        'charptr3 = Four2UInteger(charptr1 + &H1C)

        'tendptr = Four2UInteger(Four2UInteger(&H1B4EF9C) + &H24)

        charptr1 = ReadInt32(&H137AC70)
        charptr1 = ReadInt32(charptr1 + &H4)
        charptr1 = ReadInt32(charptr1)

        charmapdataptr = ReadInt32(charptr1 + &H28)
        charposdataptr = ReadInt32(charmapdataptr + &H1C)


        playerHP = ReadInt32(charptr1 + &H2D4)
        playerMaxHP = ReadInt32(charptr1 + &H2D8)

        playerStam = ReadInt32(charptr1 + &H2E4)
        playerMaxStam = ReadInt32(charptr1 + &H2E8)

        playerFacing = (ReadFloat(charposdataptr + &H4) + Math.PI) / (Math.PI * 2) * 360
        playerXpos = ReadFloat(charposdataptr + &H10)
        playerYpos = ReadFloat(charposdataptr + &H14)
        playerZpos = ReadFloat(charposdataptr + &H18)

        chkNoMapHit.Checked = ((ReadUInt32(charmapdataptr + &HC4) And &H10) = &H10)
        chkNoGrav.Checked = ((ReadUInt32(charptr1 + &H1FC) And &H4000) = &H4000)
        chkSetDeadMode.Checked = ((ReadUInt32(charptr1 + &H1FC) And &H2000000) = &H2000000)

        btnRefresh.PerformClick()



    End Sub

    Private Sub PosUpdate(ByVal bool As Boolean)

        If bool Then
            WriteBytes(&HEBC83F, {&H90, &H90, &H90, &H90, &H90})
            WriteBytes(&HEBC850, {&H90, &H90, &H90, &H90, &H90})
        Else
            WriteBytes(&HEBC83F, {&H66, &HF, &HD6, &H46, &H10})
            WriteBytes(&HEBC850, {&H66, &HF, &HD6, &H46, &H18})
        End If

        Thread.Sleep(500)
    End Sub

    Private Sub chkNoMapHit_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoMapHit.MouseClick
        Dim curval = ReadUInt32(charmapdataptr + &HC4)
        If (curval And &H10) = &H10 Then
            curval -= &H10
        Else
            curval += &H10
        End If
        WriteUInt32(charmapdataptr + &HC4, curval)
    End Sub
    Private Sub chkNoGrav_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoGrav.MouseClick
        Dim curval = ReadUInt32(charptr1 + &H1FC)
        If (curval And &H4000) = &H4000 Then
            curval -= &H4000
        Else
            curval += &H4000
        End If
        WriteUInt32(charptr1 + &H1FC, curval)
    End Sub
    Private Sub chkSetDeadMode_CheckedChanged(sender As Object, e As EventArgs) Handles chkSetDeadMode.MouseClick
        Dim curval = ReadUInt32(charptr1 + &H1FC)
        If (curval And &H2000000) = &H2000000 Then
            curval -= &H2000000
        Else
            curval += &H2000000
        End If
        WriteUInt32(charptr1 + &H1FC, curval)
    End Sub

    Private Sub btnXPlus_Click(sender As Object, e As EventArgs) Handles btnXPlus.Click
        WriteFloat(charposdataptr + &H10, playerXpos + 1)
    End Sub
    Private Sub btnXPlusPlus_Click(sender As Object, e As EventArgs) Handles btnXPlusPlus.Click
        WriteFloat(charposdataptr + &H10, playerXpos + 10)
    End Sub
    Private Sub btnYPlus_Click(sender As Object, e As EventArgs) Handles btnYPlus.Click
        WriteFloat(charposdataptr + &H14, playerYpos + 1)
    End Sub
    Private Sub btnYPlusPlus_Click(sender As Object, e As EventArgs) Handles btnYPlusPlus.Click
        WriteFloat(charposdataptr + &H14, playerYpos + 10)
    End Sub
    Private Sub btnZPlus_Click(sender As Object, e As EventArgs) Handles btnZPlus.Click
        WriteFloat(charposdataptr + &H18, playerZpos + 1)
    End Sub
    Private Sub btnZPlusPlus_Click(sender As Object, e As EventArgs) Handles btnZPlusPlus.Click
        WriteFloat(charposdataptr + &H18, playerZpos + 10)
    End Sub

    Private Sub btnXMinus_Click(sender As Object, e As EventArgs) Handles btnXMinus.Click
        WriteFloat(charposdataptr + &H10, playerXpos - 1)
    End Sub
    Private Sub btnXMinusMinus_Click(sender As Object, e As EventArgs) Handles btnXMinusMinus.Click
        WriteFloat(charposdataptr + &H10, playerXpos - 10)
    End Sub
    Private Sub btnYMinus_Click(sender As Object, e As EventArgs) Handles btnYMinus.Click
        WriteFloat(charposdataptr + &H14, playerYpos - 1)
    End Sub
    Private Sub btnYMinusMinus_Click(sender As Object, e As EventArgs) Handles btnYMinusMinus.Click
        WriteFloat(charposdataptr + &H14, playerYpos - 10)
    End Sub
    Private Sub btnZMinus_Click(sender As Object, e As EventArgs) Handles btnZMinus.Click
        WriteFloat(charposdataptr + &H18, playerZpos - 1)
    End Sub
    Private Sub btnZMinusMinus_Click(sender As Object, e As EventArgs) Handles btnZMinusMinus.Click
        WriteFloat(charposdataptr + &H18, playerZpos - 10)
    End Sub

    Private Sub chkLockPos_CheckedChanged(sender As Object, e As EventArgs) Handles chkLockPos.MouseClick
        PosUpdate(chkLockPos.Checked)
    End Sub

    Private Sub chkSetDeadMode_CheckedChanged(sender As Object, e As MouseEventArgs) Handles chkSetDeadMode.MouseClick

    End Sub
End Class
