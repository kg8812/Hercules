using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterDash : ItemInfo, IOnItemUse,IOnItemEnhance,IonItemRename,IOnItemSell
{
    static int cd = 30;
    public void Enhance()
    {
        cd = 50;
        player.dashCd = 0.25f;
        PlayerPrefs.SetFloat("PlayerDashCd", 0.25f);

    }

    public void ItemSell()
    {
        cd = 30;
        player.dashCd = 0.5f;
        PlayerPrefs.SetFloat("PlayerDashCd", 0.5f);
        Rename();
    }

    public void Rename()
    {
        description = $"대시의 쿨타임이 {cd}% 감소한다";

    }

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        cd = 30;
        player.dashCd = 0.35f;
        PlayerPrefs.SetFloat("PlayerDashCd", 0.35f);
    }
}
