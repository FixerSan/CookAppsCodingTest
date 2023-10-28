using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot_UseBattleEntity : UIBase
{
    public BattleEntityData data;
    public void Init(BattleEntityData _data)
    {
        data = _data;
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        GetText((int)Texts.Text_NameAndLevel).text = $"{_data.name} Lv.{_data.level}";
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
}
