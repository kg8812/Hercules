using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRange : ItemInfo, IOnItemUse,IOnItemEnhance,IonItemRename,IOnItemSell
{
    static float num = 0.5f;
    public void Enhance()
    {
        num = 1;
        player.dashPower = 300 * (1 + num);
        PlayerPrefs.SetFloat("PlayerDashPower", player.dashPower);
    }

    public void ItemSell()
    {
        player.dashPower = 300;
        PlayerPrefs.SetFloat("PlayerDashPower", player.dashPower);
        
        num = 0.5f;
        Rename();
    }

    public void Rename()
    {
        description = $"대시의 거리가 {num * 100}% 증가한다.";
    }

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        num = 0.5f;
        player.dashPower = 300 * (1 + num);
        PlayerPrefs.SetFloat("PlayerDashPower", player.dashPower);       
    }
}
