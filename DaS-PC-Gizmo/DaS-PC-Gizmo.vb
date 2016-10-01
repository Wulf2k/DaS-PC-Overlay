Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Threading


Public Class DaS_PC_Gizmo

    Private _Font As New Font("Courier New", 16)

    Private WithEvents refMpData As New System.Windows.Forms.Timer()
    Private WithEvents refTimer As New System.Windows.Forms.Timer()
    Private WithEvents mouseMoveTimer As New System.Windows.Forms.Timer()
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short


    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAcess As UInt32, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Int32) As IntPtr
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer() As Byte, ByVal iSize As Integer, ByRef lpNumberOfBytesRead As Integer) As Boolean
    Private Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer() As Byte, ByVal iSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Boolean
    Private Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As Boolean
    Private Declare Function VirtualAllocEx Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As IntPtr, ByVal flAllocationType As Integer, ByVal flProtect As Integer) As IntPtr
    Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer

    Public Const PROCESS_VM_READ = &H10
    Public Const TH32CS_SNAPPROCESS = &H2
    Public Const MEM_COMMIT = 4096
    Public Const PAGE_READWRITE = 4
    Public Const PROCESS_CREATE_THREAD = (&H2)
    Public Const PROCESS_VM_OPERATION = (&H8)
    Public Const PROCESS_VM_WRITE = (&H20)
    Public Const PROCESS_ALL_ACCESS = &H1F0FFF

    Dim ForceIdPtr As Integer


    Dim debug As Boolean
    Dim beta As Boolean
    Dim dbgboost As Integer

    Dim clearctr As UInteger

    Dim clsFuncNames As New Hashtable
    Dim clsFuncLocs As New Hashtable

    Dim clsBonfires As New Hashtable
    Dim clsBonfiresIDs As New Hashtable

    Dim clsItemCats As New Hashtable
    Dim clsItemCatsIDs As New Hashtable


    Dim cllItemCats As Hashtable()
    Dim cllItemCatsIDs As Hashtable()

    Dim clsWeapons As New Hashtable
    Dim clsWeaponsIDs As New Hashtable

    Dim clsArmor As New Hashtable
    Dim clsArmorIDs As New Hashtable

    Dim clsRings As New Hashtable
    Dim clsRingsIDs As New Hashtable

    Dim clsGoods As New Hashtable
    Dim clsGoodsIDs As New Hashtable

    Dim nodeDumpPtr As Integer = 0


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

    Dim ctrlHeld As Boolean
    Dim mouseStartXPos As Integer
    Dim mouseStartYPos As Integer
    Dim charStartXPos As Single
    Dim charstartYPos As Single
    Dim charstartZpos As Single



    Private _targetProcess As Process = Nothing 'to keep track of it. not used yet.
    Private _targetProcessHandle As IntPtr = IntPtr.Zero 'Used for ReadProcessMemory


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

        cllItemCats = {clsWeapons, clsArmor, clsRings, clsGoods}
        cllItemCatsIDs = {clsWeaponsIDs, clsArmorIDs, clsRingsIDs, clsGoodsIDs}


        '-----------------------Function names-----------------------
        nameList = ParseItems(clsFuncNames, clsFuncLocs, My.Resources.FuncLocs)
        For Each func In nameList
            cmbFuncName.Items.Add(func)
        Next
        cmbFuncName.SelectedItem = "PlayAnimation"


        '-----------------------Bonfires-----------------------
        nameList = ParseItems(clsBonfires, clsBonfiresIDs, My.Resources.Bonfires)
        For Each bonfire In nameList
            cmbBonfire.Items.Add(bonfire)
        Next
        cmbBonfire.SelectedItem = "Nothing"


        '-----------------------Item Categories-----------------------
        clsItemCats.Clear()
        clsItemCats.Add(0, "Weapons")
        clsItemCats.Add(268435456, "Armor")
        clsItemCats.Add(536870912, "Rings")
        clsItemCats.Add(1073741824, "Goods")

        clsItemCatsIDs.Clear()
        For Each itemCat In clsItemCats.Keys
            clsItemCatsIDs.Add(clsItemCats(itemCat), itemCat)
        Next


        '-----------------------Items-----------------------
        ParseItems(clsWeapons, clsWeaponsIDs, My.Resources.Weapons)
        ParseItems(clsArmor, clsArmorIDs, My.Resources.Armor)
        ParseItems(clsRings, clsRingsIDs, My.Resources.Rings)
        ParseItems(clsGoods, clsGoodsIDs, My.Resources.Goods)


        cmbItemCat.SelectedIndex = 0
    End Sub

    Public Function ParseItems(ByRef cls As Hashtable, ByRef clsIDs As Hashtable, ByRef txt As String) As List(Of String)
        Dim nameList As New List(Of String)
        Dim tmpList = txt.Replace(Chr(&HD), "").Split(Chr(&HA))
        Dim tmp1 As Integer
        Dim tmp2 As String

        cls.Clear()
        For i = 0 To tmpList.Length - 1
            tmp1 = tmpList(i).Split("|")(0)
            tmp2 = tmpList(i).Split("|")(1)
            cls.Add(tmp1, tmp2)
        Next

        nameList.Clear()
        clsIDs.Clear()
        For Each item In cls.Keys
            clsIDs.Add(cls(item), item)
            nameList.Add(cls(item))
        Next

        nameList.Sort()
        Return nameList
    End Function

    Public Function ReadInt8(ByVal addr As IntPtr) As SByte
        Dim _rtnBytes(0) As Byte
        ReadProcessMemory(_targetProcessHandle, addr, _rtnBytes, 1, vbNull)
        Return _rtnBytes(0)
    End Function
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
    Private Function ReadAsciiStr(ByVal addr As UInteger) As String
        Dim Str As String = ""
        Dim cont As Boolean = True
        Dim loc As Integer = 0

        Dim bytes(&H10) As Byte

        ReadProcessMemory(_targetProcessHandle, addr, bytes, &H10, vbNull)

        While (cont And loc < &H10)
            If bytes(loc) > 0 Then

                Str = Str + Convert.ToChar(bytes(loc))

                loc += 1
            Else
                cont = False
            End If
        End While

        Return Str
    End Function
    Private Function ReadUnicodeStr(ByVal addr As UInteger) As String
        Dim Str As String = ""
        Dim cont As Boolean = True
        Dim loc As Integer = 0

        Dim bytes(&H20) As Byte


        ReadProcessMemory(_targetProcessHandle, addr, bytes, &H20, vbNull)

        While (cont And loc < &H20)
            If bytes(loc) > 0 Then

                Str = Str + Convert.ToChar(bytes(loc))

                loc += 2
            Else
                cont = False
            End If
        End While

        Return Str
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
    Public Sub WriteAsciiStr(addr As UInteger, str As String)
        WriteProcessMemory(_targetProcessHandle, addr, System.Text.Encoding.ASCII.GetBytes(str), str.Length, Nothing)
    End Sub

    <System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="GetWindowRect")>
    Shared Function GetWindowRectangle(
           ByVal [Handle] As IntPtr,
           ByRef [Rectangle] As Rectangle
    ) As Boolean
    End Function

    Private Sub DaS_PC_Gizmo_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        initClls()


        refTimer = New System.Windows.Forms.Timer
        refTimer.Interval = delay
        refTimer.Enabled = True
        refTimer.Start()

        refMpData = New System.Windows.Forms.Timer
        refMpData.Interval = 10000
        refTimer.Enabled = True


        TryAttachToProcess("DARK SOULS")



    End Sub


    Private Sub refTimer_Tick() Handles refTimer.Tick

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

        gamestatsptr = ReadUInt32(&H1378700 + dbgboost)
        charptr2 = ReadUInt32(gamestatsptr + &H8)

        charmapdataptr = ReadInt32(charptr1 + &H28)
        charposdataptr = ReadInt32(charmapdataptr + &H1C)

        Select Case tabs.SelectedIndex
            Case 0
                playerHP = ReadInt32(charptr1 + &H2D4)
                playerMaxHP = ReadInt32(charptr1 + &H2D8)

                lblHP.Text = playerHP & " / " & playerMaxHP
                lblStam.Text = playerStam & " / " & playerMaxStam

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

                Dim bonfireID As Integer
                bonfireID = ReadInt32(bonfireptr + &HB04)
                If Not cmbBonfire.DroppedDown Then
                    If clsBonfires(bonfireID) = "" Then
                        clsBonfires.Add(bonfireID, bonfireID.ToString)
                        clsBonfiresIDs.Add(bonfireID.ToString, bonfireID)
                        cmbBonfire.Items.Add(bonfireID.ToString)
                    End If
                    cmbBonfire.SelectedItem = clsBonfires(bonfireID)
                End If

            Case 1


            Case 2
                Dim tmpptr As UInteger

                If debug Then dbgboost = &H4000
                If beta Then dbgboost = -&H3000
                chkSelfVagrant.Checked = (ReadBytes(&H12DE238 + dbgboost, 1)(0) = 0)

                If debug Then dbgboost = &H3C20
                If beta Then dbgboost = -&H1370
                chkDebugDrawing.Checked = (ReadBytes(&HFA256C + dbgboost, 1)(0) = 1)

                If Not beta Then


                    If debug Then dbgboost = &H41C0
                    tmpptr = ReadUInt32(&H1378520 + dbgboost)
                    tmpptr = ReadUInt32(tmpptr + &H10)
                    chkBrighterCam.Checked = (ReadBytes(tmpptr + &H26D, 1)(0) = 1)
                    nmbBrighterCam.Value = ReadFloat(tmpptr + &H270)
                    nmbContrast.Value = ReadFloat(tmpptr + &H280)

                    tmpptr = ReadUInt32(&H137E204 + dbgboost)
                    nmbMPChannel.Value = ReadBytes(tmpptr + &HB69, 1)(0)


                    'Only mapped for Debug
                    chkHide.Checked = (ReadBytes(&H137C6A8, 1)(0) = 1)
                End If



            Case 3
                If Not nmbPhantomType.Focused Then nmbPhantomType.Value = ReadInt32(charptr1 + &H70)
                If Not nmbTeamType.Focused Then nmbTeamType.Value = ReadInt32(charptr1 + &H74)

                If Not nmbVitality.Focused Then nmbVitality.Value = ReadInt32(charptr2 + &H38)
                If Not nmbAttunement.Focused Then nmbAttunement.Value = ReadInt32(charptr2 + &H40)
                If Not nmbEnd.Focused Then nmbEnd.Value = ReadInt32(charptr2 + &H48)
                If Not nmbStr.Focused Then nmbStr.Value = ReadInt32(charptr2 + &H50)
                If Not nmbDex.Focused Then nmbDex.Value = ReadInt32(charptr2 + &H58)
                If Not nmbIntelligence.Focused Then nmbIntelligence.Value = ReadInt32(charptr2 + &H60)
                If Not nmbFaith.Focused Then nmbFaith.Value = ReadInt32(charptr2 + &H68)

                If Not nmbHumanity.Focused Then nmbHumanity.Value = ReadInt32(charptr2 + &H7C)
                If Not nmbResistance.Focused Then nmbResistance.Value = ReadInt32(charptr2 + &H80)
                If Not nmbSoulLevel.Focused Then nmbSoulLevel.Value = ReadInt32(charptr2 + &H88)

                If Not txtSouls.Focused Then txtSouls.Text = ReadInt32(charptr2 + &H8C)

                If Not nmbIndictments.Focused Then nmbIndictments.value = ReadInt32(charptr2 + &HEC)



            Case 5
                If debug Then
                    dbgboost = &H41C0
                Else
                    dbgboost = 0
                End If

                Dim crtdata As Integer = ReadInt32(&H137DC70 + dbgboost)
                Dim crtstart As Integer = ReadInt32(crtdata + 4)
                Dim crtend As Integer = ReadInt32(crtdata + 8)

                txtNumCreatures.Text = ((crtend - crtstart) / 4)

                nmbCrtNum.Maximum = txtNumCreatures.Text
            Case 6
                If ForceIdPtr > 0 Then
                    lblAttemptCount.Text = "Attempts: " & ReadInt32(ForceIdPtr + &H120)
                End If



        End Select
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
            WriteInt32(bonfireptr + &HB04, clsBonfiresIDs(cmbBonfire.SelectedItem))
        End If
    End Sub

    Private Sub btnSuicide_Click(sender As Object, e As EventArgs) Handles btnSuicide.Click
        WriteInt32(charptr1 + &H2D4, 0)
    End Sub


    Private Sub chkHide_CheckedChanged(sender As Object, e As EventArgs) Handles chkHide.MouseClick
        If chkHide.Checked Then
            WriteBytes(&H137C6A8, {1})
        Else
            WriteBytes(&H137C6A8, {0})
        End If
    End Sub

    Private Sub nmbPhantomType_ValueChanged(sender As Object, e As EventArgs) Handles nmbPhantomType.ValueChanged
        WriteInt32(charptr1 + &H70, nmbPhantomType.Value)
    End Sub
    Private Sub nmbTeamType_ValueChanged(sender As Object, e As EventArgs) Handles nmbTeamType.ValueChanged
        WriteInt32(charptr1 + &H74, nmbTeamType.Value)
    End Sub


    Private Sub chkBrighterCam_CheckedChanged(sender As Object, e As EventArgs) Handles chkBrighterCam.CheckedChanged
        Dim tmpptr As UInteger
        If debug Then dbgboost = &H41C0
        tmpptr = ReadUInt32(&H1378520 + dbgboost)
        tmpptr = ReadUInt32(tmpptr + &H10)

        If chkBrighterCam.Checked Then
            WriteBytes(tmpptr + &H26D, {1})
        Else
            WriteBytes(tmpptr + &H26D, {0})
        End If
        chkBrighterCam.Checked = (ReadBytes(tmpptr + &H26D, 1)(0) = 1)
    End Sub
    Private Sub nmbBrighterCam_ValueChanged(sender As Object, e As EventArgs) Handles nmbBrighterCam.ValueChanged
        Dim tmpptr As UInteger
        If debug Then dbgboost = &H41C0
        tmpptr = ReadUInt32(&H1378520 + dbgboost)
        tmpptr = ReadUInt32(tmpptr + &H10)

        WriteFloat(tmpptr + &H270, nmbBrighterCam.Value)
        WriteFloat(tmpptr + &H274, nmbBrighterCam.Value)
        WriteFloat(tmpptr + &H278, nmbBrighterCam.Value)
    End Sub
    Private Sub nmbContrast_ValueChanged(sender As Object, e As EventArgs) Handles nmbContrast.ValueChanged
        Dim tmpptr As UInteger
        If debug Then dbgboost = &H41C0
        tmpptr = ReadUInt32(&H1378520 + dbgboost)
        tmpptr = ReadUInt32(tmpptr + &H10)

        WriteFloat(tmpptr + &H280, nmbContrast.Value)
        WriteFloat(tmpptr + &H284, nmbContrast.Value)
        WriteFloat(tmpptr + &H288, nmbContrast.Value)
    End Sub

    Private Sub nmbMPChannel_ValueChanged(sender As Object, e As EventArgs) Handles nmbMPChannel.ValueChanged
        Dim tmpptr As UInteger
        If debug Then dbgboost = &H41C0
        tmpptr = ReadUInt32(&H137E204 + dbgboost)
        WriteBytes(tmpptr + &HB69, {nmbMPChannel.Value})
    End Sub

    Private Sub btnDropItem_Click(sender As Object, e As EventArgs) Handles btnDropItem.Click

        Dim TargetBufferSize = 1024
        Dim Rtn As Integer
        Dim insertPtr As Integer

        Dim bytes() As Byte
        Dim bytes2() As Byte

        Dim bytcat As Integer = &H1
        Dim bytitem As Integer = &H6
        Dim bytcount As Integer = &H10
        Dim bytptr1 As Integer = &H15
        Dim bytptr2 As Integer = &H32
        Dim bytjmp As Integer = &H38

        Dim dbgboost As Integer

        If debug Then
            dbgboost = &H41C0
        Else
            dbgboost = 0
        End If


        bytes = {&HBD, 0, 0, 0, 0, &HBB, &HF0, &H0, &H0, &H0, &HB9, &HFF, &HFF, &HFF, &HFF, &HBA, 0, 0, 0, 0, &HA1, &HD0, &H86, &H37, &H1, &H89, &HA8, &H28, &H8, &H0, &H0, &H89, &H98, &H2C, &H8, &H0, &H0, &H89, &H88, &H30, &H8, &H0, &H0, &H89, &H90, &H34, &H8, &H0, &H0, &HA1, &HBC, &HD6, &H37, &H1, &H50, &HE8, 0, 0, 0, 0, &HC3}


        insertPtr = VirtualAllocEx(_targetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(clsItemCatsIDs(cmbItemCat.SelectedItem)))
        Array.Copy(bytes2, 0, bytes, bytcat, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(cllItemCatsIDs(cmbItemCat.SelectedIndex)(cmbItemName.SelectedItem)))
        Array.Copy(bytes2, 0, bytes, bytitem, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(nmbItemCount.Value))
        Array.Copy(bytes2, 0, bytes, bytcount, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(&H13786D0 + dbgboost))
        Array.Copy(bytes2, 0, bytes, bytptr1, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(&H137D6BC + dbgboost))
        Array.Copy(bytes2, 0, bytes, bytptr2, bytes2.Length)

        If debug Then
            dbgboost = &H28F0
        Else
            dbgboost = 0
        End If

        bytes2 = BitConverter.GetBytes(0 - ((insertPtr + &H3C) - (&HDC8C60 + dbgboost)))
        Array.Copy(bytes2, 0, bytes, bytjmp, bytes2.Length)

        Rtn = WriteProcessMemory(_targetProcessHandle, insertPtr, bytes, TargetBufferSize, 0)
        'MsgBox(Hex(insertPtr))
        CreateRemoteThread(_targetProcessHandle, 0, 0, insertPtr, 0, 0, 0)

    End Sub

    Private Sub btnFuncExecute_Click(sender As Object, e As EventArgs) Handles btnFuncExecute.Click
        Dim TargetBufferSize = 1024
        Dim Rtn As Integer
        Dim insertPtr As Integer

        Dim bytes() As Byte
        Dim bytes2() As Byte

        Dim bytParam5 As Integer = &H5
        Dim bytParam4 As Integer = &HB
        Dim bytParam3 As Integer = &H11
        Dim bytParam2 As Integer = &H17
        Dim bytParam1 As Integer = &H1D
        Dim bytJmp As Integer = &H23

        Dim dbgboost As Integer

        If debug Then
            dbgboost = &H1590
        Else
            dbgboost = 0
        End If


        bytes = {&H55, &H8B, &HEC, &H50, &HB8, 0, 0, 0, 0, &H50, &HB8, 0, 0, 0, 0, &H50, &HB8, 0, 0, 0, 0, &H50, &HB8, 0, 0, 0, 0, &H50, &HB8, 0, 0, 0, 0, &H50, &HE8, 0, 0, 0, 0, &H58, &H58, &H58, &H58, &H58, &H58, &H8B, &HE5, &H5D, &HC3}
        insertPtr = VirtualAllocEx(_targetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(txtFuncParam5.Text))
        Array.Copy(bytes2, 0, bytes, bytParam5, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(txtFuncParam4.Text))
        Array.Copy(bytes2, 0, bytes, bytParam4, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(txtFuncParam3.Text))
        Array.Copy(bytes2, 0, bytes, bytParam3, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(txtFuncParam2.Text))
        Array.Copy(bytes2, 0, bytes, bytParam2, bytes2.Length)

        bytes2 = BitConverter.GetBytes(Convert.ToInt32(txtFuncParam1.Text))
        Array.Copy(bytes2, 0, bytes, bytParam1, bytes2.Length)

        bytes2 = BitConverter.GetBytes(0 - ((insertPtr + bytJmp + 4) - (clsFuncLocs(cmbFuncName.SelectedItem) + dbgboost)))
        Array.Copy(bytes2, 0, bytes, bytJmp, bytes2.Length)

        MsgBox(Hex(insertPtr))

        Rtn = WriteProcessMemory(_targetProcessHandle, insertPtr, bytes, TargetBufferSize, 0)
        CreateRemoteThread(_targetProcessHandle, 0, 0, insertPtr, 0, 0, 0)

    End Sub

    Private Sub btnCrtControl_Click(sender As Object, e As EventArgs) Handles btnCrtControl.Click
        'Only fully works in debug.
        If debug Then
            dbgboost = &H41C0
        Else
            dbgboost = 0
        End If

        Dim crtdata As Integer = ReadInt32(&H137DC70 + dbgboost)
        Dim crtstart As Integer = ReadInt32(crtdata + 4)
        Dim crtend As Integer = ReadInt32(crtdata + 8)

        Dim crtdata1ptr As Integer = ReadInt32(crtstart + 4 * nmbCrtNum.Value - 4)
        Dim crtdata3ptr As Integer = ReadInt32(crtdata1ptr + &H28)

        Dim camPtr As Integer = ReadInt32(&H137D648 + dbgboost) + &HEC

        WriteInt32(camPtr, crtdata1ptr)

        Dim ctrlptr As Integer = ReadInt32(&H137D648 + dbgboost)
        ctrlptr = ReadInt32(ctrlptr + &HE8)

        WriteInt32(crtdata3ptr + &H244, ctrlptr)

    End Sub

    Private Sub chkMouseMove_CheckedChanged(sender As Object, e As EventArgs) Handles chkMouseMove.CheckedChanged

        If chkMouseMove.Checked Then
            mouseMoveTimer.Enabled = True
            mouseMoveTimer.Interval = 10
            mouseMoveTimer.Start()
        Else
            mouseMoveTimer.Stop()
        End If


    End Sub

    Private Shared Sub MouseMoveTimer_Tick() Handles mouseMoveTimer.Tick

        Dim ctrlkey As Boolean
        Dim shiftkey As Boolean
        ctrlkey = GetAsyncKeyState(Keys.ControlKey)
        shiftkey = GetAsyncKeyState(Keys.ShiftKey)

        If ctrlkey And Not DaS_PC_Gizmo.ctrlHeld Then
            DaS_PC_Gizmo.PosUpdate(True)
            DaS_PC_Gizmo.ctrlHeld = True
            DaS_PC_Gizmo.mouseStartXPos = MousePosition.X
            DaS_PC_Gizmo.mouseStartYPos = MousePosition.Y
            DaS_PC_Gizmo.charStartXPos = DaS_PC_Gizmo.playerXpos
            DaS_PC_Gizmo.charstartZpos = DaS_PC_Gizmo.playerZpos
        End If

        If shiftkey And Not DaS_PC_Gizmo.ctrlHeld Then
            DaS_PC_Gizmo.PosUpdate(True)
            DaS_PC_Gizmo.ctrlHeld = True
            DaS_PC_Gizmo.mouseStartYPos = MousePosition.Y
            DaS_PC_Gizmo.charstartYPos = DaS_PC_Gizmo.playerYpos
        End If

        If ctrlkey Then
            DaS_PC_Gizmo.WriteFloat(DaS_PC_Gizmo.charposdataptr + &H10, DaS_PC_Gizmo.charStartXPos + (MousePosition.X - DaS_PC_Gizmo.mouseStartXPos) * 0.1)
            DaS_PC_Gizmo.WriteFloat(DaS_PC_Gizmo.charposdataptr + &H18, DaS_PC_Gizmo.charstartZpos + (MousePosition.Y - DaS_PC_Gizmo.mouseStartYPos) * 0.1)
        End If

        If shiftkey Then
            DaS_PC_Gizmo.WriteFloat(DaS_PC_Gizmo.charposdataptr + &H14, DaS_PC_Gizmo.charstartYPos + (DaS_PC_Gizmo.mouseStartYPos - MousePosition.Y) * 0.1)
        End If

        If Not ctrlkey And Not shiftkey Then
            DaS_PC_Gizmo.ctrlHeld = False
            DaS_PC_Gizmo.PosUpdate(False)
        End If
    End Sub

    Private Sub chkTopMost_CheckedChanged(sender As Object, e As EventArgs) Handles chkOverlay.CheckedChanged
        Me.TopMost = chkOverlay.Checked
    End Sub


    Private Sub nmbVitality_ValueChanged(sender As Object, e As EventArgs) Handles nmbVitality.ValueChanged
        WriteInt32(charptr2 + &H38, nmbVitality.Value)
    End Sub
    Private Sub nmbAttunement_ValueChanged(sender As Object, e As EventArgs) Handles nmbAttunement.ValueChanged
        WriteInt32(charptr2 + &H40, nmbAttunement.Value)
    End Sub
    Private Sub nmbEnd_ValueChanged(sender As Object, e As EventArgs) Handles nmbEnd.ValueChanged
        WriteInt32(charptr2 + &H48, nmbEnd.Value)
    End Sub
    Private Sub nmbStr_ValueChanged(sender As Object, e As EventArgs) Handles nmbStr.ValueChanged
        WriteInt32(charptr2 + &H50, nmbStr.Value)
    End Sub
    Private Sub nmbDex_ValueChanged(sender As Object, e As EventArgs) Handles nmbDex.ValueChanged
        WriteInt32(charptr2 + &H58, nmbDex.Value)
    End Sub
    Private Sub nmbHumanity_ValueChanged(sender As Object, e As EventArgs) Handles nmbHumanity.ValueChanged
        WriteInt32(charptr2 + &H7C, nmbHumanity.Value)
    End Sub
    Private Sub nmbResistance_ValueChanged(sender As Object, e As EventArgs) Handles nmbResistance.ValueChanged
        WriteInt32(charptr2 + &H80, nmbResistance.Value)
    End Sub
    Private Sub nmbIntelligence_ValueChanged(sender As Object, e As EventArgs) Handles nmbIntelligence.ValueChanged
        WriteInt32(charptr2 + &H60, nmbIntelligence.Value)
    End Sub
    Private Sub nmbFaith_ValueChanged(sender As Object, e As EventArgs) Handles nmbFaith.ValueChanged
        WriteInt32(charptr2 + &H68, nmbFaith.Value)
    End Sub
    Private Sub nmbSoulLevel_ValueChanged(sender As Object, e As EventArgs) Handles nmbSoulLevel.ValueChanged
        WriteInt32(charptr2 + &H88, nmbSoulLevel.Value)
    End Sub
    Private Sub txtSouls_TextChanged(sender As Object, e As EventArgs) Handles txtSouls.TextChanged
        WriteInt32(charptr2 + &H8C, Val(txtSouls.Text))
    End Sub

    Private Sub cmbItemCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItemCat.SelectedIndexChanged
        Dim nameList As New List(Of String)

        cmbItemName.Items.Clear()
        For Each item In cllItemCats(cmbItemCat.SelectedIndex).Values
            nameList.Add(item)
        Next

        nameList.Sort()
        For Each item In nameList
            cmbItemName.Items.Add(item)
        Next
        cmbItemName.SelectedIndex = 0
    End Sub

    Private Sub chkForce_CheckedChanged(sender As Object, e As EventArgs) Handles chkForce.CheckedChanged
        Dim TargetBufferSize = 1024
        Dim bytes() As Byte
        Dim bytes2() As Byte

        'mov ESI,val = BE xx xx xx xx
        If chkForce.Checked Then
            If ForceIdPtr = 0 Then
                ForceIdPtr = VirtualAllocEx(_targetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)
            End If


            bytes = {&H50, &H53, &H51, &H52, &HB8, 0, 0, 0, 0,
                &H8B, &HD0, &H84, &HC0, &H0F, &H84, 0, 0, 0, 0,
                &H8B, &H08, &H89, &H0B,
                &H83, &HC0, &H04, &H83, &HC3, &H4, &H8B, &H08, &H89, &H0B,
                &H83, &HC0, &H04, &H83, &HC3, &H4, &H8B, &H08, &H89, &H0B,
                &H83, &HC0, &H04, &H83, &HC3, &H4, &H8B, &H08, &H89, &H0B,
                &H83, &HC2, &H20, &H8A, &H02, &HFE, &HC0, &H88, &H02, &H90, &H90, &H90,
                &H5A, &H59, &H5B, &H58,
                &HE8, 0, 0, 0, 0,
                &HE9, 0, 0, 0, 0}
            bytes2 = BitConverter.GetBytes(ForceIdPtr + &H100)
            Array.Copy(bytes2, 0, bytes, &H5, bytes2.Length)

            'Handle original call
            bytes2 = BitConverter.GetBytes((&HBE3C70 - &H4A + dbgboost) - ForceIdPtr)
            Array.Copy(bytes2, 0, bytes, &H46, bytes2.Length)

            'Handle return jump
            bytes2 = BitConverter.GetBytes((&HFA1839 - &H4A + dbgboost) - ForceIdPtr)
            Array.Copy(bytes2, 0, bytes, &H4B, bytes2.Length)

            WriteAsciiStr(ForceIdPtr + &H100, txtSteamID.Text)
            WriteProcessMemory(_targetProcessHandle, (ForceIdPtr + dbgboost), bytes, bytes.Length, 0)

            bytes = {0, 0, 0, 0}
            WriteProcessMemory(_targetProcessHandle, (ForceIdPtr + dbgboost + &H120), bytes, bytes.Length, 0)

            'Handle jump to new code
            bytes = {&HE9, 0, 0, 0, 0}
            bytes2 = BitConverter.GetBytes((ForceIdPtr - (&HFA1839 + dbgboost) - 5))
            Array.Copy(bytes2, 0, bytes, 1, bytes2.Length)
            WriteProcessMemory(_targetProcessHandle, (&HFA1839 + dbgboost), bytes, bytes.Length, 0)

        Else
            bytes = {&HE8, &H32, &H24, &HC4, &HFF}
            WriteProcessMemory(_targetProcessHandle, (&HFA1839 + dbgboost), bytes, bytes.Length, 0)
        End If
    End Sub
End Class
