using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattleBefore : UIPopup
{
    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        return true;
    }

    private enum Buttons
    {

    }
    
    private enum Images
    {

    }

    private enum Texts
    {

    }
}
