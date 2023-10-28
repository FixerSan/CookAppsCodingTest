using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup_WorldMap_ChapterOne : UIPopup
{
    public override bool Init()
    {
        if(!base.Init()) return false;

        BindButton(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Button_StageOne_One).gameObject, () => { Debug.Log("1 �ٽ� 1 �������� ����");   Managers.UI.ShowPopupUI<UIPopup_BattleBefore>(); });
        BindEvent(GetButton((int)Buttons.Button_ClosePopup).gameObject, () => { Managers.UI.ClosePopupUI(this); });

        return true;
    }

    private enum Buttons
    {
        Button_StageOne_One, Button_ClosePopup
    }
}