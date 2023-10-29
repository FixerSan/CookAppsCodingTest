using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UISlot_UseBattleEntity;

public class UISlot_UseBattleEntity : UIBase
{
    public BattleEntityData data;
    private SlotType slotType;

    public void Init(BattleEntityData _data, SlotType _type)
    {
        data = _data;
        slotType = _type;
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));


        GetText((int)Texts.Text_NameAndLevel).text = $"{_data.name} Lv.{_data.level}";
        if (_type == SlotType.Enemy)
        {
            GetButton((int)Buttons.Slot_UseBattleEntity).interactable = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
            GetText((int)Texts.Text_NameAndLevel).transform.eulerAngles = new Vector3(0, 0, 0);
            return;
        }
        BindEvent(GetButton((int)Buttons.Slot_UseBattleEntity).gameObject, RemoveSlot);
    }

    public void RemoveSlot()
    {
        Managers.Game.battleInfo.UnUseBattleEntity(data);
    }

    private enum Buttons
    {
        Slot_UseBattleEntity
    }

    private enum Images
    {

    }

    private enum Texts
    {
        Text_NameAndLevel
    }

    public enum SlotType
    {
        Army, Enemy
    }
}
