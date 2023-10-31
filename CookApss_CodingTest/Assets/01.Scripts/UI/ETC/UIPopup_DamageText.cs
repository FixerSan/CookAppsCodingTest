using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPopup_DamageText : UIBase
{
    public float fadeTime;
    public void Init(int _damage , Vector2 _position)
    {
        BindText(typeof(Texts));
        GetText((int)Texts.Text_Damage).text = $"{_damage}";
        GetText((int)Texts.Text_Damage).transform.position = _position;
        StartCoroutine(FadeOut());
    }

    public void FixedUpdate()
    {
        GetText((int)Texts.Text_Damage).transform.position += Vector3.up * Time.deltaTime;
    }

    IEnumerator FadeOut()
    {
        while (GetText((int)Texts.Text_Damage).color.a > 0)
        {
            transform.position += Vector3.up * Time.deltaTime;
            GetText((int)Texts.Text_Damage).color = new Color(GetText((int)Texts.Text_Damage).color.r, GetText((int)Texts.Text_Damage).color.g, GetText((int)Texts.Text_Damage).color.b, GetText((int)Texts.Text_Damage).color.a - Time.deltaTime / fadeTime);
            yield return null;
        }

        Managers.Resource.Destroy(this.gameObject);
    }

    public enum Texts
    {
        Text_Damage
    }
}
