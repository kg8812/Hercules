using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfUnluck :ItemInfo,IOnItemUse,IOnItemEnhance,IonItemRename,IOnItemSell
{
    static int increase = 50;
    float num;
    public void Enhance()
    {
        increase = 80;
        player.atkRatio += 0.3f;
        PlayerPrefs.SetFloat("PlayerAtkRatio", player.atkRatio);
    }

    public void ItemSell()
    {
        player.atkRatio -= (increase / 100);
        PlayerPrefs.SetFloat("PlayerAtkRatio", player.atkRatio);
        player.critProb += num;
        if (player.critProb >= 1)
            player.critProb = 1;
        PlayerPrefs.SetFloat("PlayerCritProb", player.critProb);

        increase = 50;
        Rename();
    }

    public void Rename()
    {
        description = $"공격력이 {increase}% 증가하지만 치명타 확률이 0%로 감소한다.";
    }

   

    public void Use()
    {
        increase = 50;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        player.atkRatio += 0.5f;
        PlayerPrefs.SetFloat("PlayerAtkRatio",player.atkRatio);
        num = player.critProb;
        player.critProb = 0;
        PlayerPrefs.SetFloat("PlayerCritProb", 0);


    }
}
