using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hector : ItemInfo, IOnItemUse, IOnItemEnhance,IOnItemSell,IonItemRename
{
    static float value = 30;
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float num = PlayerPrefs.GetFloat("PlayerAtkRatio") +0.3f;
        PlayerPrefs.SetFloat("PlayerAtkRatio", num);
        player.atkRatio = PlayerPrefs.GetFloat("PlayerAtkRatio");
    }
    public void Enhance()
    {
        isEnhanced = true;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float num = PlayerPrefs.GetFloat("PlayerAtkRatio") + 0.2f;
        PlayerPrefs.SetFloat("PlayerAtkRatio", num);
        player.atkRatio = num;

        value = 50;
        Rename();

    }

    public void ItemSell()
    {

        player.atkRatio -= value / 100f;
        PlayerPrefs.SetFloat("PlayerAtkRatio", player.atkRatio);

        value = 30;
        Rename();
    }

    public void Rename()
    {
        description = $"플레이어가 적에게 가하는 피해가 {value}% 증가한다.";
    }
}
