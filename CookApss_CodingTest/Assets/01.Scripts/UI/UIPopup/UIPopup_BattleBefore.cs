using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup_BattleBefore : UIPopup
{
    private List<UISlot_CanUseBattleEntity> canUseSlots = new List<UISlot_CanUseBattleEntity>();
    private List<UISlot_UseBattleEntity> useSlots = new List<UISlot_UseBattleEntity>();
    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));

        Managers.Event.OnVoidEvent -= OnChangeBattleInfo;
        Managers.Event.OnVoidEvent += OnChangeBattleInfo;
        BindEvent(GetButton((int)Buttons.Button_Clear).gameObject, ClearUseSlots);
        BindEvent(GetButton((int)Buttons.Button_ClosePopup).gameObject, () => { Managers.UI.ClosePopupUI(this); });

        for (int i = 0; i < Managers.Data.playerData.hasEntites.Count; i++)
            CreateCanUseSlot(Managers.Data.playerData.hasEntites[i].UID, Managers.Data.playerData.hasEntites[i].level);


        return true;
    }

    public void CreateCanUseSlot(int _UID, int _level)
    {
        UISlot_CanUseBattleEntity slot = Managers.Resource.Instantiate("Slot_CanUseBattleEntity", _parent: GetObject((int)Objects.Panel_Slot).transform).GetOrAddComponent<UISlot_CanUseBattleEntity>();
        BattleEntityData data = Managers.Data.GetBattleEntityData(_UID, _level);
        slot.Init(data, UISlot_CanUseBattleEntity.SlotState.UnUsed);
        canUseSlots.Add(slot);
    }

    public void UpdateCanUseSlots()
    {
        for (int i = 0; i < canUseSlots.Count; i++)
        {
            canUseSlots[i].UpdateState(UISlot_CanUseBattleEntity.SlotState.UnUsed);
        }

        for (int i = 0; i < canUseSlots.Count; i++)
        {
            for (int j = 0; j < Managers.Game.battleInfo.armyFront.Length; j++)
            {
                if (canUseSlots[i].data == Managers.Game.battleInfo.armyFront[j])
                    canUseSlots[i].UpdateState(UISlot_CanUseBattleEntity.SlotState.Used);
            }

            for (int j = 0; j < Managers.Game.battleInfo.armyCenter.Length; j++)
            {
                if (canUseSlots[i].data == Managers.Game.battleInfo.armyCenter[j])
                    canUseSlots[i].UpdateState(UISlot_CanUseBattleEntity.SlotState.Used);
            }

            for (int j = 0; j < Managers.Game.battleInfo.armyRear.Length; j++)
            {
                if (canUseSlots[i].data == Managers.Game.battleInfo.armyRear[j])
                    canUseSlots[i].UpdateState(UISlot_CanUseBattleEntity.SlotState.Used);
            }
        }
    }

    public void OnChangeBattleInfo(Define.VoidEventType _type)
    {
        if (_type != Define.VoidEventType.OnChangeBattleInfo) return;

        GetText((int)Texts.Text_CanSpawnEntityCount).text = $"{Managers.Game.battleInfo.nowUseBattleEntityCount} / {Managers.Game.battleInfo.isCanUseBattleEntityCount}";
        GetText((int)Texts.Text_BattleForce).text = $"{Managers.Game.battleInfo.battleForce}";
        ClearUseSlots();
        CreateUseSlots();
        UpdateCanUseSlots();
    }



    public void ClearUseSlots()
    {
        for (int i = 0; i < useSlots.Count; i++)
        {
            Managers.Resource.Destroy(useSlots[i].gameObject);
        }
        useSlots.Clear();
    }

    public void CreateUseSlots()
    {
        for (int i = 0; i < Managers.Game.battleInfo.armyFront.Length; i++)
        {
            if (Managers.Game.battleInfo.armyFront[i] != null)
            {
                UISlot_UseBattleEntity slot = Managers.Resource.Instantiate("Slot_UseBattleEntity", GetImage((int)Images.Image_BattleEntitySpace_front).transform).GetOrAddComponent<UISlot_UseBattleEntity>();
                slot.Init(Managers.Game.battleInfo.armyFront[i]);
                useSlots.Add(slot);
            }
        }

        for (int i = 0; i < Managers.Game.battleInfo.armyCenter.Length; i++)
        {
            if (Managers.Game.battleInfo.armyCenter[i] != null)
            {
                UISlot_UseBattleEntity slot = Managers.Resource.Instantiate("Slot_UseBattleEntity", GetImage((int)Images.Image_BattleEntitySpace_Center).transform).GetOrAddComponent<UISlot_UseBattleEntity>();
                slot.Init(Managers.Game.battleInfo.armyCenter[i]);
                useSlots.Add(slot);
            }
        }

        for (int i = 0; i < Managers.Game.battleInfo.armyRear.Length; i++)
        {
            if (Managers.Game.battleInfo.armyRear[i] != null)
            {
                UISlot_UseBattleEntity slot = Managers.Resource.Instantiate("Slot_UseBattleEntity", GetImage((int)Images.Image_BattleEntitySpace_Rear).transform).GetOrAddComponent<UISlot_UseBattleEntity>();
                slot.Init(Managers.Game.battleInfo.armyRear[i]);
                useSlots.Add(slot);
            }
        }
    }

    private enum Buttons
    {
        Button_Clear, Button_ClosePopup
    }
    
    private enum Images
    {
        Image_BattleEntitySpace_front, Image_BattleEntitySpace_Center, Image_BattleEntitySpace_Rear
    }

    private enum Texts
    {
        Text_CanSpawnEntityCount, Text_BattleForce
    }

    public enum Objects
    {
        Panel_Slot
    }
}
