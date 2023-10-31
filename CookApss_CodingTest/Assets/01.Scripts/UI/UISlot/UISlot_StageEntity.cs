using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot_StageEntity : UIBase
{
    private BattleEntityController controller;
    public void Init(BattleEntityController _controller)
    {
        controller = _controller;
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        BindEvent(GetImage((int)Images.Image_Illust).gameObject, UseSkill);
    }
    public void FixedUpdate()
    {
        if (controller == null) return;
        CheckCooltime();
    }

    public void UseSkill()
    {
        if (Managers.Screen.isSkillCasting) return;
        controller.entity.Skill(); 
        Debug.Log("Ω∫≈≥ ΩΩ∑‘ ¿€µøµ ");
    }

    public void CheckCooltime()
    {
        GetImage((int)Images.Image_Cooltime).fillAmount = controller.status.currentSkillCooltime / controller.entity.data.skillCooltime;
        GetText((int)Texts.Text_Cooltime).text = $"{(int)controller.status.currentSkillCooltime}";
        if (controller.status.currentSkillCooltime == 0)
        {
            GetText((int)Texts.Text_Cooltime).gameObject.SetActive(false);
            GetImage((int)Images.Image_Cooltime).gameObject.SetActive(false);
        }
        else 
        {
            if (!GetImage((int)Images.Image_Cooltime).gameObject.activeSelf)
                GetImage((int)Images.Image_Cooltime).gameObject.SetActive(true);
            if(!GetText((int)Texts.Text_Cooltime).gameObject.activeSelf)
                GetText((int)Texts.Text_Cooltime).gameObject.SetActive(true);
        }
    }

    private enum Images
    {
        Image_Cooltime, Image_Illust
    }
    private enum Texts
    {
        Text_Cooltime
    }
}
