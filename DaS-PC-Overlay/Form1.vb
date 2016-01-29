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

    Dim debug As Boolean
    Dim beta As Boolean
    Dim dbgboost As Integer

    Dim clearctr As UInteger

    Dim clsBonfires As New Hashtable()
    Dim clsBonfireIDs As New Hashtable()



    Dim charptr1 As UInteger
    Dim charmapdataptr As UInteger
    Dim charposdataptr As UInteger

    Dim gamestatsptr As UInteger
    Dim bonfireptr As UInteger


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

    Public Sub initClls()
        Dim nameList As New List(Of String)

        clsBonfires.Clear()
        clsBonfires.Add(-1, "Nothing")
        clsBonfires.Add(1602950, "Abyss (Bonfire)")
        clsBonfires.Add(1512962, "Anor Londo (Gwyndolin Bonfire)")
        clsBonfires.Add(1512950, "Anor Londo (Gwynevere Bonfire)")
        clsBonfires.Add(1512961, "Anor Londo (Interior Bonfire)")
        clsBonfires.Add(1512960, "Anor Londo (Lady of the Darkling Bonfire)")
        clsBonfires.Add(1322961, "Ash Lake (Bonfire #1)")
        clsBonfires.Add(1322960, "Ash Lake (Bonfire #2)")
        clsBonfires.Add(1402962, "Blighttown (Bridge Bonfire)")
        clsBonfires.Add(1402963, "Blighttown (Entrance)")
        clsBonfires.Add(1402961, "Blighttown (Swamp Bonfire)")
        clsBonfires.Add(1212950, "Chasm of the Abyss (Manus)")
        clsBonfires.Add(1702950, "Crystal Cave (Seath)")
        clsBonfires.Add(1602961, "Darkroot Basin (Bonfire)")
        clsBonfires.Add(1202961, "Darkroot Garden (Bonfire)")
        clsBonfires.Add(1412961, "Demon Ruins (Bonfire #1)")
        clsBonfires.Add(1412962, "Demon Ruins (Bonfire #2)")
        clsBonfires.Add(1412963, "Demon Ruins (Bonfire #3)")
        clsBonfires.Add(1002960, "Depths (Bonfire)")
        clsBonfires.Add(1002900, "Depths (Entrance)")
        clsBonfires.Add(1002950, "Depths (Gaping Dragon's Room)")
        clsBonfires.Add(1802960, "Firelink Altar")
        clsBonfires.Add(1022960, "Firelink Shrine (Bonfire)")
        clsBonfires.Add(1322962, "Great Hollow (Bonfire)")
        clsBonfires.Add(1802961, "Kiln of the First Flame (Entrance)")
        clsBonfires.Add(1802130, "Kiln of the First Flame (Gwyn)")
        clsBonfires.Add(1412950, "Lost Izalith (Bed of Chaos)")
        clsBonfires.Add(1412964, "Lost Izalith (Bonfire #1)")
        clsBonfires.Add(1412960, "Lost Izalith (Bonfire #2)")
        clsBonfires.Add(1602951, "New Londo Ruins (Drainage Lever)")
        clsBonfires.Add(1602960, "New Londo Ruins (Pre-Ingward)")
        clsBonfires.Add(1812960, "Northern Undead Asylum (Bonfire #1)")
        clsBonfires.Add(1812961, "Northern Undead Asylum (Bonfire #2)")
        clsBonfires.Add(1812962, "Northern Undead Asylum (Cell)")
        clsBonfires.Add(1212961, "Oolacile Sanctuary (Bonfire)")
        clsBonfires.Add(1212962, "Oolacile Township (Bonfire)")
        clsBonfires.Add(1212964, "Oolacile Township Dungeon (Bonfire)")
        clsBonfires.Add(1102960, "Painted World of Ariamis (Bonfire)")
        clsBonfires.Add(1102961, "Painted World of Ariamis (Entrance)")
        clsBonfires.Add(1402960, "Quelaag's Domain (Bonfire)")
        clsBonfires.Add(1212963, "Sanctuary Garden (Bonfire)")
        clsBonfires.Add(1502961, "Sen's Fortress (Bonfire)")
        clsBonfires.Add(1502960, "Sen's Fortress (Bridge, #1)")
        clsBonfires.Add(1502962, "Sen's Fortress (Bridge, #2)")
        clsBonfires.Add(1302960, "The Catacombs (Bonfire #1)")
        clsBonfires.Add(1302961, "The Catacombs (Bonfire #2)")
        clsBonfires.Add(1302962, "The Catacombs (Entrance)")
        clsBonfires.Add(1312950, "The Catacombs (Nito)")
        clsBonfires.Add(1702960, "The Duke's Archives (Balcony Bonfire)")
        clsBonfires.Add(1702961, "The Duke's Archives (Cell Bonfire)")
        clsBonfires.Add(1702962, "The Duke's Archives (Elevator Bonfire)")
        clsBonfires.Add(1312960, "Tomb of the Giants (Bonfire #1)")
        clsBonfires.Add(1312961, "Tomb of the Giants (Bonfire #2)")
        clsBonfires.Add(1312962, "Tomb of the Giants (Entrance)")
        clsBonfires.Add(1012962, "Undead Burg (Bonfire)")
        clsBonfires.Add(1012960, "Undead Burg (Pre-Dragon Scare)")
        clsBonfires.Add(1012964, "Undead Parish (Bonfire)")
        clsBonfires.Add(1012965, "Undead Parish (Near Boar)")
        clsBonfires.Add(1012966, "Undead Parish (Near Cell)")
        clsBonfires.Add(1012961, "Undead Parish (Sunlight Altar Bonfire)")

        'clsBonfires.Add(1012967, "_Test")

        clsBonfireIDs.Clear()
        cmbBonfire.Items.Clear()
        For Each bonfire In clsBonfires.Keys
            clsBonfireIDs.Add(clsBonfires(bonfire), bonfire)
            nameList.Add(clsBonfires(bonfire))
        Next
        nameList.Sort()
        For Each bonfire In nameList
            cmbBonfire.Items.Add(bonfire)
        Next
        cmbBonfire.SelectedItem = "Nothing"
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

    Public Sub WriteInt32(ByVal addr As IntPtr, val As Int32)
        WriteProcessMemory(_targetProcessHandle, addr, BitConverter.GetBytes(val), 4, Nothing)
    End Sub
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

        initClls()

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



        debug = (ReadUInt32(&H400080) = &HCE9634B4&)
        beta = (ReadUInt32(&H400080) = &HE91B11E2&)


        If debug Then
            dbgboost = &H41C0
            lblRelease.Text = "Debug detected."
        End If

        If beta Then
            dbgboost = -&H3000
            lblRelease.Text = "Beta detected."
        End If

        bonfireptr = ReadUInt32(&H13784A0 + dbgboost)
        charptr1 = ReadInt32(&H137DC70 + dbgboost)
        charptr1 = ReadInt32(charptr1 + &H4)
        charptr1 = ReadInt32(charptr1)

        lblCharptr1.Text = "Charptr1: " & Hex(charptr1)

        charmapdataptr = ReadInt32(charptr1 + &H28)
        charposdataptr = ReadInt32(charmapdataptr + &H1C)

        lblCharmapdata.Text = "Charmapdata: " & Hex(charmapdataptr)


        Select Case tabs.SelectedIndex
            Case 0
                playerHP = ReadInt32(charptr1 + &H2D4)
                playerMaxHP = ReadInt32(charptr1 + &H2D8)

                lblHP.Text = playerHP & " / " & playerMaxHP
                lblStam.Text = playerStam & " / " & playerStam

                playerStam = ReadInt32(charptr1 + &H2E4)
                playerMaxStam = ReadInt32(charptr1 + &H2E8)

                playerFacing = (ReadFloat(charposdataptr + &H4) + Math.PI) / (Math.PI * 2) * 360
                playerXpos = ReadFloat(charposdataptr + &H10)
                playerYpos = ReadFloat(charposdataptr + &H14)
                playerZpos = ReadFloat(charposdataptr + &H18)

                lblFacing.Text = "Facing: " & playerFacing
                lblXpos.Text = "X pos: " & playerXpos
                lblYpos.Text = "Y pos: " & playerYpos
                lblZpos.Text = "Z pos: " & playerZpos

                chkNoMapHit.Checked = ((ReadUInt32(charmapdataptr + &HC4) And &H10) = &H10)
                chkNoGrav.Checked = ((ReadUInt32(charptr1 + &H1FC) And &H4000) = &H4000)
                chkSetDeadMode.Checked = ((ReadUInt32(charptr1 + &H1FC) And &H2000000) = &H2000000)
            Case 1
                If debug Then dbgboost = &H4000
                If beta Then dbgboost = -&H3000
                chkSelfVagrant.Checked = (ReadBytes(&H12DE238 + dbgboost, 1)(0) = 0)

                If debug Then dbgboost = &H3C20
                If beta Then dbgboost = -&H1370
                chkDebugDrawing.Checked = (ReadBytes(&HFA256C + dbgboost, 1)(0) = 1)
            Case 2
                Dim bonfireID As Integer
                bonfireID = ReadInt32(bonfireptr + &HB04)
                If Not cmbBonfire.DroppedDown Then
                    If clsBonfires(bonfireID) = "" Then
                        clsBonfires.Add(bonfireID, bonfireID.ToString)
                        clsBonfireIDs.Add(bonfireID.ToString, bonfireID)
                        cmbBonfire.Items.Add(bonfireID.ToString)
                    End If
                    cmbBonfire.SelectedItem = clsBonfires(bonfireID)
                End If
        End Select
        btnRefresh.PerformClick()
    End Sub

    Private Sub PosUpdate(ByVal bool As Boolean)
        If debug Then dbgboost = &H2EC0
        If beta Then dbgboost = -&H1390

        If bool Then
            WriteBytes(&HEBDBCF + dbgboost, {&H90, &H90, &H90, &H90, &H90})
            WriteBytes(&HEBDBE0 + dbgboost, {&H90, &H90, &H90, &H90, &H90})
        Else
            WriteBytes(&HEBDBCF + dbgboost, {&H66, &HF, &HD6, &H46, &H10})
            WriteBytes(&HEBDBE0 + dbgboost, {&H66, &HF, &HD6, &H46, &H18})
        End If
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

    Private Sub chkDebug_CheckedChanged(sender As Object, e As EventArgs) Handles chkDebugDrawing.MouseClick
        If debug Then dbgboost = &H3C20
        If beta Then dbgboost = -&H1370

        If chkDebugDrawing.Checked Then
            WriteBytes(&HFA256C + dbgboost, {&H1})
        Else
            WriteBytes(&HFA256C + dbgboost, {&H0})
        End If
    End Sub
    Private Sub chkBoundingBoxes_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoundingBoxes.MouseClick

        If debug Then dbgboost = &H28F0
        If beta Then dbgboost = -&H1390

        If chkBoundingBoxes.Checked Then
            WriteBytes(&HEAF5AD + dbgboost, {&H1})
        Else
            WriteBytes(&HEAF5AD + dbgboost, {&H0})
        End If
    End Sub
    Private Sub chkSelfVagrant_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelfVagrant.MouseClick
        If chkSelfVagrant.Checked Then
            If debug Then dbgboost = -&H840
            If beta Then dbgboost = -&HF30
            WriteBytes(&HBD4444 + dbgboost, {1})

            If debug Then dbgboost = -&H850
            If beta Then dbgboost = -&HF40
            WriteBytes(&HBD26A6 + dbgboost, {1})

            If debug Then dbgboost = &H4000
            If beta Then dbgboost = -&H3000
            WriteUInt32(&H12DE238 + dbgboost, 0)
            WriteFloat(&H12DE248 + dbgboost, 1)
            WriteFloat(&H12DE24C + dbgboost, 1)
        Else
            If debug Then dbgboost = -&H840
            If beta Then dbgboost = -&HF30
            WriteBytes(&HBD4444 + dbgboost, {0})

            If debug Then dbgboost = -&H850
            If beta Then dbgboost = -&HF40
            WriteBytes(&HBD26A6 + dbgboost, {0})

            If debug Then dbgboost = &H4000
            If beta Then dbgboost = -&H3000
            WriteUInt32(&H12DE238 + dbgboost, 1)
            WriteFloat(&H12DE248 + dbgboost, 1800)
            WriteFloat(&H12DE24C + dbgboost, 3000)
        End If

    End Sub

    Private Sub cmbBonfire_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBonfire.DropDownClosed
        If Not IsNothing(cmbBonfire.SelectedItem) Then
            WriteInt32(bonfireptr + &HB04, clsBonfireIDs(cmbBonfire.SelectedItem))
        End If
    End Sub
End Class
