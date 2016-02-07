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


    Dim debug As Boolean
    Dim beta As Boolean
    Dim dbgboost As Integer

    Dim clearctr As UInteger

    Dim clsFuncNames As New Hashtable
    Dim clsFuncLocs As New Hashtable

    Dim clsBonfires As New Hashtable()
    Dim clsBonfireIDs As New Hashtable()

    Dim clsGoods As New Hashtable()
    Dim clsGoodsIDs As New Hashtable()



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

        nameList.Clear()
        clsFuncNames.Clear()
        clsFuncNames.Add(14030544, "ActionEnd")
        clsFuncNames.Add(14023712, "AddActionCount")
        clsFuncNames.Add(14017664, "AddBlockClearBonus")
        clsFuncNames.Add(14019616, "AddClearCount")
        clsFuncNames.Add(14027056, "AddCorpseEvent")
        clsFuncNames.Add(14024912, "AddCurrentVowRankPoint")
        clsFuncNames.Add(14042560, "AddCustomRoutePoint")
        clsFuncNames.Add(14016032, "AddDeathCount")
        clsFuncNames.Add(14028016, "AddEventParts_Ignore")
        clsFuncNames.Add(14035040, "AddEventSimpleTalk")
        clsFuncNames.Add(14034976, "AddEventSimpleTalkTimer")
        clsFuncNames.Add(14034832, "AddFieldInsFilter")
        clsFuncNames.Add(14033584, "AddGeneEvent")
        clsFuncNames.Add(14015312, "AddHelpWhiteGhost")
        clsFuncNames.Add(14044496, "AddHitMaskByBit")
        clsFuncNames.Add(14042912, "AddInfomationBuffer")
        clsFuncNames.Add(14042928, "AddInfomationBufferTag")
        clsFuncNames.Add(14033776, "AddInfomationList")
        clsFuncNames.Add(14042864, "AddInfomationTimeMsgTag")
        clsFuncNames.Add(14042832, "AddInfomationTosBuffer")
        clsFuncNames.Add(14042800, "AddInfomationTosBufferPlus")
        clsFuncNames.Add(14050496, "AddInventoryItem")
        clsFuncNames.Add(14015280, "AddKillBlackGhost")
        clsFuncNames.Add(14022672, "AddQWC")
        clsFuncNames.Add(14021552, "AddTreasureEvent")
        clsFuncNames.Add(14015984, "AddTrueDeathCount")
        clsFuncNames.Add(14023920, "BeginAction")
        clsFuncNames.Add(14023568, "BeginLoopCheck")
        clsFuncNames.Add(14023424, "CamReset")
        clsFuncNames.Add(14049200, "CastPointSpell")
        clsFuncNames.Add(14048912, "CastTargetSpell")
        clsFuncNames.Add(14048848, "CastTargetSpellPlus")
        clsFuncNames.Add(14028144, "ChangeInitPosAng")
        clsFuncNames.Add(14050256, "ChangeGreyGhost")
        clsFuncNames.Add(14044272, "ChangeModel")
        clsFuncNames.Add(14032288, "ChangeTarget")
        clsFuncNames.Add(14047392, "ChangeThink")
        clsFuncNames.Add(14028800, "ChangeWander")
        clsFuncNames.Add(14026848, "CharacterAllAttachSys")
        clsFuncNames.Add(14041488, "CharactorCopyPosAng")
        clsFuncNames.Add(14019680, "CheckEventBody")
        clsFuncNames.Add(14020176, "CheckPenalty")
        clsFuncNames.Add(14029920, "ChrDisableUpdate")
        clsFuncNames.Add(14026720, "ChrFadeIn")
        clsFuncNames.Add(14026608, "ChrFadeOut")
        clsFuncNames.Add(14026352, "ChrResetAnimation")
        clsFuncNames.Add(14026400, "ChrResetRequest")
        clsFuncNames.Add(14015872, "ClearBossGauge")
        clsFuncNames.Add(14020048, "ClearMyWorldState")
        clsFuncNames.Add(14020304, "ClearSosSign")
        clsFuncNames.Add(14029760, "ClearTarget")
        clsFuncNames.Add(14017792, "CloseGenDialog")
        clsFuncNames.Add(14019840, "CloseMenu")
        clsFuncNames.Add(14014800, "CloseRankingDialog")
        clsFuncNames.Add(14018064, "CloseTalk")
        clsFuncNames.Add(14049440, "CompleteEvent")
        clsFuncNames.Add(14033072, "CreateCamSfx")
        clsFuncNames.Add(14034496, "CreateHeroBloodStain")
        clsFuncNames.Add(14020608, "CreateSfx")
        clsFuncNames.Add(14041408, "CreateSfx_DummyPoly")
        clsFuncNames.Add(14017536, "CroseBriefingMsg")
        clsFuncNames.Add(14036016, "CustomLuaCall")
        clsFuncNames.Add(14049856, "CustomLuaCallStart")
        clsFuncNames.Add(14049808, "CustomLuaCallStartPlus")
        clsFuncNames.Add(14018720, "DeleteCamSfx")
        clsFuncNames.Add(14019232, "DeleteEvent")
        clsFuncNames.Add(14018480, "DeleteObjSfxAll")
        clsFuncNames.Add(14018544, "DeleteObjSfxDmyPlyID")
        clsFuncNames.Add(14018512, "DeleteObjSfxEventID")
        clsFuncNames.Add(14027328, "DisableCollection")
        clsFuncNames.Add(14028240, "DisableDamage")
        clsFuncNames.Add(14027264, "DisableHpGauge")
        clsFuncNames.Add(14029488, "DisableInterupt")
        clsFuncNames.Add(14030736, "DisableMapHit")
        clsFuncNames.Add(14030800, "DisableMove")
        clsFuncNames.Add(14020384, "EnableGeneratorSystem")
        clsFuncNames.Add(14028288, "EnableHide")
        clsFuncNames.Add(14028176, "EnableInvincible")
        clsFuncNames.Add(14024080, "EnableLogic")
        clsFuncNames.Add(14049744, "EnableObjTreasure")
        clsFuncNames.Add(14031856, "EndAnimation")
        clsFuncNames.Add(14029328, "EraseEventSpecialEffect")
        clsFuncNames.Add(14029168, "EraseEventSpecialEffect_2")
        clsFuncNames.Add(14018592, "EventTagInsertString_forPlayerNo")
        clsFuncNames.Add(14032384, "ForceChangeTarget")
        clsFuncNames.Add(14050960, "ForceDead")
        clsFuncNames.Add(14032112, "ForcePlayAnimation")
        clsFuncNames.Add(14032032, "ForcePlayAnimationStayCancel")
        clsFuncNames.Add(14031952, "ForcePlayLoopAnimation")
        clsFuncNames.Add(14027920, "ForceUpdateNextFrame")
        clsFuncNames.Add(14025008, "GetBountyRankPoint")
        clsFuncNames.Add(14016176, "GetClearBonus")
        clsFuncNames.Add(14019856, "GetEnemyPlayerId_Random")
        clsFuncNames.Add(14029552, "GetEventGoalState")
        clsFuncNames.Add(14030480, "GetEventMode")
        clsFuncNames.Add(14025856, "GetFloorMaterial")
        clsFuncNames.Add(14022736, "GetGlobalQWC")
        clsFuncNames.Add(14023296, "GetHp")
        clsFuncNames.Add(14017088, "GetItem")
        clsFuncNames.Add(14023536, "GetLocalPlayerChrType")
        clsFuncNames.Add(14023504, "GetLocalPlayerInvadeType")
        clsFuncNames.Add(14023456, "GetLocalPlayerVowType")
        clsFuncNames.Add(14022768, "GetLocalQWC")
        clsFuncNames.Add(14019888, "GetPlayerId_Random")
        clsFuncNames.Add(14041344, "GetPlayerNo_LotNitoMultiItem")
        clsFuncNames.Add(14022848, "GetQWC")
        clsFuncNames.Add(14016752, "GetRandom")
        clsFuncNames.Add(14050736, "GetRateItem")
        clsFuncNames.Add(14020480, "GetRightCurrentWeaponId")
        clsFuncNames.Add(14016096, "GetSoloClearBonus")
        clsFuncNames.Add(14026896, "GetSummonAnimId")
        clsFuncNames.Add(14041440, "GetTargetChrID")
        clsFuncNames.Add(14025792, "GetTravelItemParamId")
        clsFuncNames.Add(14025776, "HasSuppleItem")
        clsFuncNames.Add(14035968, "HoverMoveVal")
        clsFuncNames.Add(14043360, "HoverMoveValDmy")
        clsFuncNames.Add(14021072, "IncrementCoopPlaySuccessCount")
        clsFuncNames.Add(14021024, "IncrementThiefInvadePlaySuccessCount")
        clsFuncNames.Add(14041360, "InfomationMenu")
        clsFuncNames.Add(14014432, "InitDeathState")
        clsFuncNames.Add(14028368, "InvalidMyBloodMarkInfo")
        clsFuncNames.Add(14028336, "InvalidMyBloodMarkInfo_Tutorial")
        clsFuncNames.Add(14020704, "InvalidPointLight")
        clsFuncNames.Add(14022528, "InvalidSfx")
        clsFuncNames.Add(14023344, "IsAction")
        clsFuncNames.Add(14030304, "IsAlive")
        clsFuncNames.Add(14031664, "IsAngle")
        clsFuncNames.Add(14020976, "IsAppearancePlayer")
        clsFuncNames.Add(14028528, "IsBlackGhost")
        clsFuncNames.Add(14028464, "IsColiseumGhost")
        clsFuncNames.Add(14028656, "IsDisable")
        clsFuncNames.Add(14031776, "IsDistance")
        clsFuncNames.Add(14027776, "IsDropCheck_Only")
        clsFuncNames.Add(14021872, "IsEquip")
        clsFuncNames.Add(14031920, "IsEventAnim")
        clsFuncNames.Add(14025728, "IsFireDead")
        clsFuncNames.Add(14028592, "IsGreyGhost")
        clsFuncNames.Add(14028496, "IsIntruder")
        clsFuncNames.Add(14020416, "IsInventoryEquip")
        clsFuncNames.Add(14025808, "IsLand")
        clsFuncNames.Add(14028624, "IsLivePlayer")
        clsFuncNames.Add(14019968, "IsMatchingMultiPlay")
        clsFuncNames.Add(14030352, "IsPlayerStay")
        clsFuncNames.Add(14027504, "IsPlayMovie")
        clsFuncNames.Add(14029632, "IsProcessEventGoal")
        clsFuncNames.Add(14027824, "IsRegionDrop")
        clsFuncNames.Add(14028832, "IsRegionIn")
        clsFuncNames.Add(14022608, "IsSuccessQWC")
        clsFuncNames.Add(14021904, "IsValidInstance")
        clsFuncNames.Add(14028560, "IsWhiteGhost")
        clsFuncNames.Add(14035200, "LeaveSession")
        clsFuncNames.Add(14020192, "LockSession")
        clsFuncNames.Add(14036064, "LuaCall")
        clsFuncNames.Add(14049936, "LuaCallStart")
        clsFuncNames.Add(14049888, "LuaCallStartPlus")
        clsFuncNames.Add(14032224, "MultiDoping_AllEventBody")
        clsFuncNames.Add(14029952, "NoAnimeTurnCharactor")
        clsFuncNames.Add(14015456, "NotNetMessage_begin")
        clsFuncNames.Add(14015440, "NotNetMessage_end")
        clsFuncNames.Add(14043040, "ObjRootMtxMove")
        clsFuncNames.Add(14042976, "ObjRootMtxMoveByChrDmyPoly")
        clsFuncNames.Add(14043008, "ObjRootMtxMoveDmyPoly")
        clsFuncNames.Add(14040176, "OnActionEventRegion")
        clsFuncNames.Add(14040032, "OnActionEventRegionAttribute")
        clsFuncNames.Add(14039136, "OnCharacterAnimEnd")
        clsFuncNames.Add(14039728, "OnCharacterDead")
        clsFuncNames.Add(14039632, "OnCharacterHP")
        clsFuncNames.Add(14039520, "OnCharacterHP_CheckAttacker")
        clsFuncNames.Add(14042480, "OnChrAnimEnd")
        clsFuncNames.Add(14038992, "OnChrAnimEndPlus")
        clsFuncNames.Add(14044752, "OnDistanceJustIn")
        clsFuncNames.Add(14038192, "OnFireDamage")
        clsFuncNames.Add(14034384, "OnNetRegion")
        clsFuncNames.Add(14036480, "OnNetRegionPlus")
        clsFuncNames.Add(14042464, "OnObjAnimEnd")
        clsFuncNames.Add(14042448, "OnObjAnimEndPlus")
        clsFuncNames.Add(14039296, "OnObjDestroy")
        clsFuncNames.Add(14040464, "OnPlayerActionInRegionAngle")
        clsFuncNames.Add(14040320, "OnPlayerActionInRegionAngleAttribute")
        clsFuncNames.Add(14046080, "OnPlayerDistanceInTarget")
        clsFuncNames.Add(14044896, "OnPlayerDistanceOut")
        clsFuncNames.Add(14038064, "OnPlayerKill")
        clsFuncNames.Add(14041168, "OnRegionIn")
        clsFuncNames.Add(14041024, "OnRegionJustIn")
        clsFuncNames.Add(14040896, "OnRegionJustOut")
        clsFuncNames.Add(14037936, "OnSelfBloodMark")
        clsFuncNames.Add(14037808, "OnSelfHeroBloodMark")
        clsFuncNames.Add(14037552, "OnSessionIn")
        clsFuncNames.Add(14037680, "OnSessionJustIn")
        clsFuncNames.Add(14037424, "OnSessionJustOut")
        clsFuncNames.Add(14037296, "OnSessionOut")
        clsFuncNames.Add(14038336, "OnSimpleDamage")
        clsFuncNames.Add(14048048, "OnTalkEventDistIn")
        clsFuncNames.Add(14047920, "OnTalkEventDistOut")
        clsFuncNames.Add(14034720, "OnTextEffectEnd")
        clsFuncNames.Add(14035504, "OnTurnCharactorEnd")
        clsFuncNames.Add(14033472, "OnWanderingDemon")
        clsFuncNames.Add(14015104, "OpenCampMenu")
        clsFuncNames.Add(14018384, "OpenSOSMsg_Tutorial")
        clsFuncNames.Add(14050816, "OpeningDead")
        clsFuncNames.Add(14050144, "ParamInitialize")
        clsFuncNames.Add(14016688, "PauseTutorial")
        clsFuncNames.Add(14032144, "PlayAnimation")
        clsFuncNames.Add(14032064, "PlayAnimationStayCancel")
        clsFuncNames.Add(14026288, "PlayerChrResetAnimation_RemoOnly")
        clsFuncNames.Add(14031984, "PlayLoopAnimation")
        clsFuncNames.Add(14030432, "PlayObjectSE")
        clsFuncNames.Add(14032640, "PlayPointSE")
        clsFuncNames.Add(14030384, "PlayTypeSE")
        clsFuncNames.Add(14034816, "RecallMenuEvent")
        clsFuncNames.Add(14049392, "ReconstructBreak")
        clsFuncNames.Add(14026560, "RecoveryHeroin")
        clsFuncNames.Add(14027888, "RemoveInventoryEquip")
        clsFuncNames.Add(14015424, "RepeatMessage_begin")
        clsFuncNames.Add(14015408, "RepeatMessage_end")
        clsFuncNames.Add(14015952, "RequestEnding")
        clsFuncNames.Add(14027424, "RequestForceUpdateNetwork")
        clsFuncNames.Add(14015808, "RequestFullRecover")
        clsFuncNames.Add(14021264, "RequestGenerate")
        clsFuncNames.Add(14027376, "RequestNormalUpdateNetwork")
        clsFuncNames.Add(14019408, "RequestOpenBriefingMsg")
        clsFuncNames.Add(14033504, "RequestOpenBriefingMsgPlus")
        clsFuncNames.Add(14047056, "RequestPlayMovie")
        clsFuncNames.Add(14046928, "RequestPlayMoviePlus")
        clsFuncNames.Add(14050368, "RequestRemo")
        clsFuncNames.Add(14050272, "RequestRemoPlus")
        clsFuncNames.Add(14019504, "RequestUnlockTrophy")
        clsFuncNames.Add(14017504, "ReqularLeavePlayer")
        clsFuncNames.Add(14019360, "ResetCamAngle")
        clsFuncNames.Add(14032592, "ResetEventQwcSpEffect")
        clsFuncNames.Add(14019456, "ResetSummonParam")
        clsFuncNames.Add(14025984, "ResetSyncRideObjInfo")
        clsFuncNames.Add(14028912, "ResetThink")
        clsFuncNames.Add(14044224, "RestorePiece")
        clsFuncNames.Add(14016960, "ReturnMapSelect")
        clsFuncNames.Add(14042592, "RevivePlayer")
        clsFuncNames.Add(14016672, "RevivePlayerNext")
        clsFuncNames.Add(14020240, "SaveRequest")
        clsFuncNames.Add(14020208, "SaveRequest_Profile")
        clsFuncNames.Add(14029104, "SendEventRequest")
        clsFuncNames.Add(14050464, "SetAlive")
        clsFuncNames.Add(14018240, "SetAliveMotion")
        clsFuncNames.Add(14026096, "SetAlwaysDrawForEvent")
        clsFuncNames.Add(14026576, "SetAlwaysEnableBackread_forEvent")
        clsFuncNames.Add(14031488, "SetAngleFoward")
        clsFuncNames.Add(14022432, "SetAreaStartMapUid")
        clsFuncNames.Add(14026496, "SetBossGauge")
        clsFuncNames.Add(14030608, "SetBossUnitJrHit")
        clsFuncNames.Add(14044672, "SetBountyRankPoint")
        clsFuncNames.Add(14022560, "SetBrokenPiece")
        clsFuncNames.Add(14016256, "SetCamModeParamTargetId")
        clsFuncNames.Add(14016240, "SetCamModeParamTargetIdForBossLock")
        clsFuncNames.Add(14028400, "SetChrType")
        clsFuncNames.Add(14016432, "SetChrTypeDataGrey")
        clsFuncNames.Add(14016336, "SetChrTypeDataGreyNext")
        clsFuncNames.Add(14021584, "SetClearBonus")
        clsFuncNames.Add(14015696, "SetClearItem")
        clsFuncNames.Add(14017728, "SetClearSesiionCount")
        clsFuncNames.Add(14017888, "SetClearState")
        clsFuncNames.Add(14024848, "SetColiEnable")
        clsFuncNames.Add(14043808, "SetColiEnableArray")
        clsFuncNames.Add(14025952, "SetCompletelyNoMove")
        clsFuncNames.Add(14029872, "SetDeadMode")
        clsFuncNames.Add(14029808, "SetDeadMode2")
        clsFuncNames.Add(14042528, "SetDefaultRoutePoint")
        clsFuncNames.Add(14022272, "SetDefaultMapUid")
        clsFuncNames.Add(14028736, "SetDisable")
        clsFuncNames.Add(14027472, "SetDisableBackread_ForEvent")
        clsFuncNames.Add(14019632, "SetDisableDamage")
        clsFuncNames.Add(14028960, "SetDisableGravity")
        clsFuncNames.Add(14025680, "SetDisableWeakDamageAnim")
        clsFuncNames.Add(14025616, "SetDisableWeakDamageAnim_light")
        clsFuncNames.Add(14030848, "SetDispMask")
        clsFuncNames.Add(14024880, "SetDrawEnable")
        clsFuncNames.Add(14042416, "SetDrawEnableArray")
        clsFuncNames.Add(14031360, "SetDrawGroup")
        clsFuncNames.Add(14017568, "SetEnableEventPad")
        clsFuncNames.Add(14018320, "SetEventBodyMaterialSeAndSfx")
        clsFuncNames.Add(14032240, "SetEventBodyMaxHp")
        clsFuncNames.Add(14023856, "SetEventCommand")
        clsFuncNames.Add(14023792, "SetEventCommandIndex")
        clsFuncNames.Add(14025104, "SetEventFlag")
        clsFuncNames.Add(14025904, "SetEventGenerate")
        clsFuncNames.Add(14027696, "SetEventMovePointType")
        clsFuncNames.Add(14029440, "SetEventSpecialEffect")
        clsFuncNames.Add(14029248, "SetEventSpecialEffect_2")
        clsFuncNames.Add(14029376, "SetEventSpecialEffectOwner")
        clsFuncNames.Add(14029200, "SetEventSpecialEffectOwner_2")
        clsFuncNames.Add(14031520, "SetEventTarget")
        clsFuncNames.Add(14032528, "SetFirstSpeed")
        clsFuncNames.Add(14015472, "SetFlagInitState")
        clsFuncNames.Add(14025488, "SetFootIKInterpolateType")
        clsFuncNames.Add(14048752, "SetForceJoinBlackRequest")
        clsFuncNames.Add(14031248, "SetHitInfo")
        clsFuncNames.Add(14043424, "SetHitMask")
        clsFuncNames.Add(14023072, "SetHp")
        clsFuncNames.Add(14030672, "SetIgnoreHit")
        clsFuncNames.Add(14015248, "SetInfomationPriority")
        clsFuncNames.Add(14028880, "SetInsideBattleArea")
        clsFuncNames.Add(14021376, "SetIsAnimPauseOnRemoPlayForEvent")
        clsFuncNames.Add(14034160, "SetKeepCommandIndex")
        clsFuncNames.Add(14014784, "SetLoadWait")
        clsFuncNames.Add(14026992, "SetLockActPntInvalidateMask")
        clsFuncNames.Add(14042640, "SetMapUid")
        clsFuncNames.Add(14022928, "SetMaxHp")
        clsFuncNames.Add(14015344, "SetMenuBrake")
        clsFuncNames.Add(14022128, "SetMiniBlockIndex")
        clsFuncNames.Add(14029712, "SetMovePoint")
        clsFuncNames.Add(14021120, "SetMultiWallMapUid")
        clsFuncNames.Add(14026448, "SetNoNetSync")
        clsFuncNames.Add(14020896, "SetObjDeactivate")
        clsFuncNames.Add(14021440, "SetObjDisableBreak")
        clsFuncNames.Add(14024576, "SetObjEventCollisionFill")
        clsFuncNames.Add(14015600, "SetReturnPointEntityId")
        clsFuncNames.Add(14018176, "SetReviveWait")
        clsFuncNames.Add(14024688, "SetSelfBloodMapUid")
        clsFuncNames.Add(14022000, "SetSosSignPos")
        clsFuncNames.Add(14016624, "SetSosSignWarp")
        clsFuncNames.Add(14027200, "SetSpStayAndDamageAnimId")
        clsFuncNames.Add(14014592, "SetSubMenuBrake")
        clsFuncNames.Add(14016656, "SetSummonedPos")
        clsFuncNames.Add(14026016, "SetSyncRideObjInfo")
        clsFuncNames.Add(14026144, "SetSystemIgnore")
        clsFuncNames.Add(14016912, "SetTalkMsg")
        clsFuncNames.Add(14027600, "SetTeamType")
        clsFuncNames.Add(14027520, "SetTeamTypeDefault")
        clsFuncNames.Add(14027568, "SetTeamTypePlus")
        clsFuncNames.Add(14017584, "SetTextEffect")
        clsFuncNames.Add(14016640, "SetTutorialSummonedPos")
        clsFuncNames.Add(14017984, "SetValidTalk")
        clsFuncNames.Add(14014816, "ShowRankingDialog")
        clsFuncNames.Add(14024640, "StopLoopAnimation")
        clsFuncNames.Add(14027088, "StopPlayer")
        clsFuncNames.Add(14019168, "StopPointSE")
        clsFuncNames.Add(14023616, "SubActionCount")
        clsFuncNames.Add(14024352, "SubDispMaskByBit")
        clsFuncNames.Add(14044320, "SubHitMask")
        clsFuncNames.Add(14044400, "SubHitMaskByBit")
        clsFuncNames.Add(14034896, "SummonBlackRequest")
        clsFuncNames.Add(14026832, "SummonedMapReload")
        clsFuncNames.Add(14044080, "SummonSuccess")
        clsFuncNames.Add(14024480, "SwitchDispMask")
        clsFuncNames.Add(14044592, "SwitchHitMask")
        clsFuncNames.Add(14018112, "TalkNextPage")
        clsFuncNames.Add(14043232, "TreasureDispModeChange")
        clsFuncNames.Add(14033824, "TurnCharactor")
        clsFuncNames.Add(14016560, "Tutorial_begin")
        clsFuncNames.Add(14016544, "Tutorial_end")
        clsFuncNames.Add(14017520, "UnLockSession")
        clsFuncNames.Add(14049776, "UpDateBloodMark")
        clsFuncNames.Add(14014512, "Util_RequestLevelUp")
        clsFuncNames.Add(14014544, "Util_RequestLevelUpFirst")
        clsFuncNames.Add(14014496, "Util_RequestRegene")
        clsFuncNames.Add(14014480, "Util_RequestRespawn")
        clsFuncNames.Add(14020640, "ValidPointLight")
        clsFuncNames.Add(14020768, "ValidSfx")
        clsFuncNames.Add(14032192, "Warp")
        clsFuncNames.Add(14043760, "WarpDmy")
        clsFuncNames.Add(14036224, "WarpNextStage")
        clsFuncNames.Add(14035248, "WarpNextStageKick")
        clsFuncNames.Add(14036128, "WarpNextStage_Bonfire")
        clsFuncNames.Add(14034304, "WarpRestart")
        clsFuncNames.Add(14034256, "WarpRestartNoGrey")

        clsFuncLocs.Clear()
        cmbFuncName.Items.Clear()
        For Each func In clsFuncNames.Keys
            clsFuncLocs.Add(clsFuncNames(func), func)
            nameList.Add(clsFuncNames(func))
        Next
        nameList.Sort()
        For Each func In nameList
            cmbFuncName.Items.Add(func)
        Next
        cmbFuncName.SelectedItem = "PlayAnimation"


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
        clsBonfires.Add(1812100, "Northern Undead Asylum (Cell)")
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

        nameList.Clear()
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



        clsGoods.Add("100", "White Sign Soapstone")
        clsGoods.Add("101", "Red Sign Soapstone")
        clsGoods.Add("102", "Red Eye Orb")
        clsGoods.Add("103", "Black Separation Crystal")
        clsGoods.Add("106", "Orange Guidance Soapstone")
        clsGoods.Add("108", "Book of the Guilty")
        clsGoods.Add("109", "Eye of Death")
        clsGoods.Add("111", "Cracked Red Eye Orb")
        clsGoods.Add("112", "Servant Roster")
        clsGoods.Add("113", "Blue Eye Orb")
        clsGoods.Add("114", "Dragon Eye")
        clsGoods.Add("115", "Black Eye Orb (115)")
        clsGoods.Add("116", "Black Eye Orb (116)")
        clsGoods.Add("117", "Darksign")
        clsGoods.Add("118", "Purple Coward's Crystal")
        clsGoods.Add("200", "Estus Flask (200)")
        clsGoods.Add("201", "Estus Flask (201)")
        clsGoods.Add("202", "Estus Flask+1 (202)")
        clsGoods.Add("203", "Estus Flask+1 (203)")
        clsGoods.Add("204", "Estus Flask+2 (204)")
        clsGoods.Add("205", "Estus Flask+2 (205)")
        clsGoods.Add("206", "Estus Flask+3 (206)")
        clsGoods.Add("207", "Estus Flask+3 (207)")
        clsGoods.Add("208", "Estus Flask+4 (208)")
        clsGoods.Add("209", "Estus Flask+4 (209)")
        clsGoods.Add("210", "Estus Flask+5 (210)")
        clsGoods.Add("211", "Estus Flask+5 (211)")
        clsGoods.Add("212", "Estus Flask+6 (212)")
        clsGoods.Add("213", "Estus Flask+6 (213)")
        clsGoods.Add("214", "Estus Flask+7 (214)")
        clsGoods.Add("215", "Estus Flask+7 (215)")
        clsGoods.Add("220", "Silver Pendant")
        clsGoods.Add("230", "Elizabeth's Mushroom")
        clsGoods.Add("240", "Divine Blessing")
        clsGoods.Add("260", "Green Blossom")
        clsGoods.Add("270", "Bloodred Moss Clump")
        clsGoods.Add("271", "Purple Moss Clump")
        clsGoods.Add("272", "Blooming Purple Moss Clump")
        clsGoods.Add("274", "Purging Stone")
        clsGoods.Add("275", "Egg Vermifuge")
        clsGoods.Add("280", "Repair Powder")
        clsGoods.Add("290", "Throwing Knife")
        clsGoods.Add("291", "Poison Throwing Knife")
        clsGoods.Add("292", "Firebomb")
        clsGoods.Add("293", "Dung Pie")
        clsGoods.Add("294", "Alluring Skull")
        clsGoods.Add("296", "Lloyd's Talisman")
        clsGoods.Add("297", "Black Firebomb")
        clsGoods.Add("310", "Charcoal Pine Resin")
        clsGoods.Add("311", "Gold Pine Resin")
        clsGoods.Add("312", "Transient Curse")
        clsGoods.Add("313", "Rotten Pine Resin")
        clsGoods.Add("330", "Homeward Bone")
        clsGoods.Add("350", "Humanity (350)")
        clsGoods.Add("370", "Prism Stone")
        clsGoods.Add("371", "Binoculars")
        clsGoods.Add("373", "Indictment")
        clsGoods.Add("374", "Souvenir of Reprisal")
        clsGoods.Add("375", "Sunlight Medal")
        clsGoods.Add("376", "Pendant")
        clsGoods.Add("377", "Dragon Head Stone")
        clsGoods.Add("378", "Dragon Torso Stone")
        clsGoods.Add("380", "Rubbish")
        clsGoods.Add("381", "Copper Coin")
        clsGoods.Add("382", "Silver Coin")
        clsGoods.Add("383", "Gold Coin")
        clsGoods.Add("384", "Peculiar Doll")
        clsGoods.Add("385", "Dried Finger")
        clsGoods.Add("390", "Fire Keeper Soul (390)")
        clsGoods.Add("391", "Fire Keeper Soul (391)")
        clsGoods.Add("392", "Fire Keeper Soul (392)")
        clsGoods.Add("393", "Fire Keeper Soul (393)")
        clsGoods.Add("394", "Fire Keeper Soul (394)")
        clsGoods.Add("395", "Fire Keeper Soul (395)")
        clsGoods.Add("396", "Fire Keeper Soul (396)")
        clsGoods.Add("400", "Soul of a Lost Undead")
        clsGoods.Add("401", "Large Soul of a Lost Undead")
        clsGoods.Add("402", "Soul of a Nameless Soldier")
        clsGoods.Add("403", "Large Soul of a Nameless Soldier")
        clsGoods.Add("404", "Soul of a Proud Knight")
        clsGoods.Add("405", "Large Soul of a Proud Knight")
        clsGoods.Add("406", "Soul of a Brave Warrior")
        clsGoods.Add("407", "Large Soul of a Brave Warrior")
        clsGoods.Add("408", "Soul of a Hero")
        clsGoods.Add("409", "Soul of a Great Hero")
        clsGoods.Add("500", "Humanity (500)")
        clsGoods.Add("501", "Twin Humanities")
        clsGoods.Add("510", "Hello Carving")
        clsGoods.Add("511", "Thank you Carving")
        clsGoods.Add("512", "Very good! Carving")
        clsGoods.Add("513", "I'm sorry Carving")
        clsGoods.Add("514", "Help me! Carving")
        clsGoods.Add("700", "Soul of Quelaag")
        clsGoods.Add("701", "Soul of Sif")
        clsGoods.Add("702", "Soul of Gwyn, Lord of Cinder")
        clsGoods.Add("703", "Core of an Iron Golem")
        clsGoods.Add("704", "Soul of Ornstein")
        clsGoods.Add("705", "Soul of the Moonlight Butterfly")
        clsGoods.Add("706", "Soul of Smough")
        clsGoods.Add("707", "Soul of Priscilla")
        clsGoods.Add("708", "Soul of Gwyndolin")
        clsGoods.Add("709", "Guardian Soul")
        clsGoods.Add("710", "Soul of Artorias")
        clsGoods.Add("711", "Soul of Manus")
        clsGoods.Add("800", "Large Ember")
        clsGoods.Add("801", "Very Large Ember")
        clsGoods.Add("802", "Crystal Ember")
        clsGoods.Add("806", "Large Magic Ember")
        clsGoods.Add("807", "Enchanted Ember")
        clsGoods.Add("808", "Divine Ember")
        clsGoods.Add("809", "Large Divine Ember")
        clsGoods.Add("810", "Dark Ember")
        clsGoods.Add("812", "Large Flame Ember")
        clsGoods.Add("813", "Chaos Flame Ember")
        clsGoods.Add("1000", "Titanite Shard")
        clsGoods.Add("1010", "Large Titanite Shard")
        clsGoods.Add("1020", "Green Titanite Shard")
        clsGoods.Add("1030", "Titanite Chunk")
        clsGoods.Add("1040", "Blue Titanite Chunk")
        clsGoods.Add("1050", "White Titanite Chunk")
        clsGoods.Add("1060", "Red Titanite Chunk")
        clsGoods.Add("1070", "Titanite Slab")
        clsGoods.Add("1080", "Blue Titanite Slab")
        clsGoods.Add("1090", "White Titanite Slab")
        clsGoods.Add("1100", "Red Titanite Slab")
        clsGoods.Add("1110", "Dragon Scale")
        clsGoods.Add("1120", "Demon Titanite")
        clsGoods.Add("1130", "Twinkling Titanite")
        clsGoods.Add("2001", "Basement Key")
        clsGoods.Add("2002", "Crest of Artorias")
        clsGoods.Add("2003", "Cage Key")
        clsGoods.Add("2004", "Archive Tower Cell Key")
        clsGoods.Add("2005", "Archive Tower Giant Door Key")
        clsGoods.Add("2006", "Archive Tower Giant Cell Key")
        clsGoods.Add("2007", "Blighttown Key")
        clsGoods.Add("2008", "Key to New Londo Ruins")
        clsGoods.Add("2009", "Annex Key")
        clsGoods.Add("2010", "Dungeon Cell Key")
        clsGoods.Add("2011", "Big Pilgrim's Key")
        clsGoods.Add("2012", "Undead Asylum F2 East Key")
        clsGoods.Add("2013", "Key to the Seal")
        clsGoods.Add("2014", "Key to Depths")
        clsGoods.Add("2015", "Lift Chamber Key")
        clsGoods.Add("2016", "Undead Asylum F2 West Key")
        clsGoods.Add("2017", "Mystery Key")
        clsGoods.Add("2018", "Sewer Chamber Key")
        clsGoods.Add("2019", "Watchtower Basement Key")
        clsGoods.Add("2020", "Archive Prison Extra Key")
        clsGoods.Add("2021", "Residence Key")
        clsGoods.Add("2022", "Crest Key")
        clsGoods.Add("2100", "Master Key")
        clsGoods.Add("2200", "2200")
        clsGoods.Add("2500", "Lord Soul (2500)")
        clsGoods.Add("2501", "Lord Soul (2501)")
        clsGoods.Add("2502", "Bequeathed Lord Soul Shard (2502)")
        clsGoods.Add("2503", "Bequeathed Lord Soul Shard (2503)")
        clsGoods.Add("2504", "2504")
        clsGoods.Add("2510", "Lordvessel")
        clsGoods.Add("2520", "Broken Pendant")
        clsGoods.Add("2600", "Weapon Smithbox")
        clsGoods.Add("2601", "Armor Smithbox")
        clsGoods.Add("2602", "Repairbox")
        clsGoods.Add("2603", "2603")
        clsGoods.Add("2604", "2604")
        clsGoods.Add("2605", "2605")
        clsGoods.Add("2606", "2606")
        clsGoods.Add("2607", "Rite of Kindling")
        clsGoods.Add("2608", "Bottomless Box")
        clsGoods.Add("2609", "2609")
        clsGoods.Add("3000", "Sorcery: Soul Arrow")
        clsGoods.Add("3010", "Sorcery: Great Soul Arrow")
        clsGoods.Add("3020", "Sorcery: Heavy Soul Arrow")
        clsGoods.Add("3030", "Sorcery: Great Heavy Soul Arrow")
        clsGoods.Add("3040", "Sorcery: Homing Soulmass")
        clsGoods.Add("3050", "Sorcery: Homing Crystal Soulmass")
        clsGoods.Add("3060", "Soul Spear")
        clsGoods.Add("3070", "Sorcery: Crystal Soul Spear")
        clsGoods.Add("3100", "Magic Weapon")
        clsGoods.Add("3110", "Sorcery: Great Magic Weapon")
        clsGoods.Add("3120", "Sorcery: Crystal Magic Weapon")
        clsGoods.Add("3300", "Sorcery: Magic Shield")
        clsGoods.Add("3310", "Sorcery: Strong Magic Shield")
        clsGoods.Add("3400", "Sorcery: Hidden Weapon")
        clsGoods.Add("3410", "Sorcery: Hidden Body")
        clsGoods.Add("3500", "Cast Light")
        clsGoods.Add("3510", "Sorcery: Hush")
        clsGoods.Add("3520", "Sorcery: Aural Decoy")
        clsGoods.Add("3530", "Sorcery: Repair")
        clsGoods.Add("3540", "Sorcery: Fall Control")
        clsGoods.Add("3550", "Sorcery: Chameleon")
        clsGoods.Add("3600", "Sorcery: Resist Curse")
        clsGoods.Add("3610", "Sorcery: Remedy")
        clsGoods.Add("3700", "Sorcery: White Dragon Breath")
        clsGoods.Add("3710", "Dark Orb")
        clsGoods.Add("3720", "Dark Bead")
        clsGoods.Add("3730", "Dark Fog")
        clsGoods.Add("3740", "Pursuers")
        clsGoods.Add("4000", "Pyromancy: Fireball")
        clsGoods.Add("4010", "Fire Orb")
        clsGoods.Add("4020", "Pyromancy: Great Fireball")
        clsGoods.Add("4030", "Pyromancy: Firestorm")
        clsGoods.Add("4040", "Pyromancy: Fire Tempest")
        clsGoods.Add("4050", "Pyromancy: Fire Surge")
        clsGoods.Add("4060", "Pyromancy: Fire Whip")
        clsGoods.Add("4100", "Pyromancy: Combustion")
        clsGoods.Add("4110", "Great Combustion")
        clsGoods.Add("4200", "Pyromancy: Poison Mist")
        clsGoods.Add("4210", "Pyromancy: Toxic Mist")
        clsGoods.Add("4220", "Pyromancy: Acid Surge")
        clsGoods.Add("4300", "Iron Flesh")
        clsGoods.Add("4310", "Pyromancy: Flash Sweat")
        clsGoods.Add("4360", "Pyromancy: Undead Rapport")
        clsGoods.Add("4400", "Pyromancy: Power Within")
        clsGoods.Add("4500", "Pyromancy: Great Chaos Fireball")
        clsGoods.Add("4510", "Pyromancy: Chaos Storm")
        clsGoods.Add("4520", "Pyromancy: Chaos Fire Whip")
        clsGoods.Add("4530", "Black Flame")
        clsGoods.Add("5000", "Miracle: Heal")
        clsGoods.Add("5010", "Miracle: Great Heal")
        clsGoods.Add("5020", "Great Heal Excerpt")
        clsGoods.Add("5030", "Miracle: Soothing Sunlight")
        clsGoods.Add("5040", "Replenishment")
        clsGoods.Add("5050", "Miracle: Bountiful Sunlight")
        clsGoods.Add("5100", "Miracle: Gravelord Sword Dance")
        clsGoods.Add("5110", "Miracle: Gravelord Greatsword Dance")
        clsGoods.Add("5200", "Miracle: Escape Death")
        clsGoods.Add("5210", "Homeward")
        clsGoods.Add("5300", "Miracle: Force")
        clsGoods.Add("5310", "Miracle: Wrath of the Gods")
        clsGoods.Add("5320", "Miracle: Emit Force")
        clsGoods.Add("5400", "Seek Guidance")
        clsGoods.Add("5500", "Miracle: Lightning Spear")
        clsGoods.Add("5510", "Miracle: Great Lightning Spear")
        clsGoods.Add("5520", "Miracle: Sunlight Spear")
        clsGoods.Add("5600", "Miracle: Magic Barrier")
        clsGoods.Add("5610", "Miracle: Great Magic Barrier")
        clsGoods.Add("5700", "Miracle: Karmic Justice")
        clsGoods.Add("5800", "Miracle: Tranquil Walk of Peace")
        clsGoods.Add("5810", "Miracle: Vow of Silence")
        clsGoods.Add("5900", "Miracle: Sunlight Blade")
        clsGoods.Add("5910", "Miracle: Darkmoon Blade")
        clsGoods.Add("9000", "Beckon")
        clsGoods.Add("9001", "Point forward")
        clsGoods.Add("9002", "Hurrah!")
        clsGoods.Add("9003", "Bow")
        clsGoods.Add("9004", "Joy")
        clsGoods.Add("9005", "Shrug")
        clsGoods.Add("9006", "Wave")
        clsGoods.Add("9007", "Praise the Sun")
        clsGoods.Add("9008", "Point up")
        clsGoods.Add("9009", "Point down")
        clsGoods.Add("9010", "Look skyward")
        clsGoods.Add("9011", "Well! What is it!")
        clsGoods.Add("9012", "Prostration")
        clsGoods.Add("9013", "Proper bow")
        clsGoods.Add("9014", "Prayer")


        nameList.Clear()
        clsGoodsIDs.Clear()
        cmbItemName.Items.Clear()
        For Each good In clsGoods.Keys
            clsGoodsIDs.Add(clsGoods(good), good)
            nameList.Add(clsGoods(good))
        Next
        nameList.Sort()
        For Each good In nameList
            cmbItemName.Items.Add(good)
        Next
        cmbItemName.SelectedItem = "Alluring Skull"








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

        Me.Location = New Point(rect.X - 10, rect.Y - 90)

        refTimer = New System.Windows.Forms.Timer
        refTimer.Interval = delay
        refTimer.Enabled = True
        refTimer.Start()

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
                        clsBonfireIDs.Add(bonfireID.ToString, bonfireID)
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



            Case 3
                nmbHumanity.Value = ReadInt32(charptr2 + &H7C)
                nmbPhantomType.Value = ReadInt32(charptr1 + &H70)
                nmbTeamType.Value = ReadInt32(charptr1 + &H74)


            Case 5
                'Mapped for Debug only
                Dim crtdata As Integer = ReadInt32(&H1381E30)
                Dim crtstart As Integer = ReadInt32(crtdata + 4)
                Dim crtend As Integer = ReadInt32(crtdata + 8)

                txtNumCreatures.Text = ((crtend - crtstart) / 4)

                nmbCrtNum.Maximum = txtNumCreatures.Text







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
            WriteInt32(bonfireptr + &HB04, clsBonfireIDs(cmbBonfire.SelectedItem))
        End If
    End Sub

    Private Sub btnSuicide_Click(sender As Object, e As EventArgs) Handles btnSuicide.Click
        WriteInt32(charptr1 + &H2D4, 0)
    End Sub

    Private Sub nmbHumanity_ValueChanged(sender As Object, e As EventArgs) Handles nmbHumanity.ValueChanged
        WriteInt32(charptr2 + &H7C, nmbHumanity.Value)
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


        bytes = {&HBD, &H0, &H0, &H0, &H40, &HBB, &HF0, &H0, &H0, &H0, &HB9, &HFF, &HFF, &HFF, &HFF, &HBA, 0, 0, 0, 0, &HA1, &HD0, &H86, &H37, &H1, &H89, &HA8, &H28, &H8, &H0, &H0, &H89, &H98, &H2C, &H8, &H0, &H0, &H89, &H88, &H30, &H8, &H0, &H0, &H89, &H90, &H34, &H8, &H0, &H0, &HA1, &HBC, &HD6, &H37, &H1, &H50, &HE8, 0, 0, 0, 0, &HC3}


        insertPtr = VirtualAllocEx(_targetProcessHandle, 0, TargetBufferSize, MEM_COMMIT, PAGE_READWRITE)


        bytes2 = BitConverter.GetBytes(Convert.ToInt32(clsGoodsIDs(cmbItemName.SelectedItem)))
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
        MsgBox(Hex(insertPtr))
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

        Rtn = WriteProcessMemory(_targetProcessHandle, insertPtr, bytes, TargetBufferSize, 0)
        CreateRemoteThread(_targetProcessHandle, 0, 0, insertPtr, 0, 0, 0)

    End Sub

    Private Sub btnCrtControl_Click(sender As Object, e As EventArgs) Handles btnCrtControl.Click
        Dim crtdata As Integer = ReadInt32(&H1381E30)
        Dim crtstart As Integer = ReadInt32(crtdata + 4)
        Dim crtend As Integer = ReadInt32(crtdata + 8)

        Dim crtdata1ptr As Integer = ReadInt32(crtstart + 4 * nmbCrtNum.Value - 4)
        Dim crtdata3ptr As Integer = ReadInt32(crtdata1ptr + &H28)

        Dim camPtr As Integer = ReadInt32(&H1381808) + &HEC

        WriteInt32(camPtr, crtdata1ptr)

        Dim ctrlptr As Integer = ReadInt32(&H1381808)
        ctrlptr = ReadInt32(ctrlptr + &HE8)

        WriteInt32(crtdata3ptr + &H244, ctrlptr)

    End Sub
End Class
