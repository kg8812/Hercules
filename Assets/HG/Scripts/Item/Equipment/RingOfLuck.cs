using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfLuck : ItemInfo, IOnItemUse,IOnItemEnhance,IonItemRename,IOnItemSell
{
    static float debuff = 0.5f;
    float num = 30;

    public void Enhance()
    {
        debuff = 0.3f;
        player.atkDebuff = debuff;
        PlayerPrefs.SetFloat("PlayerAtkDebuff", debuff);
    }

    public void ItemSell()
    {
        player.atkDebuff = 0;
        PlayerPrefs.SetFloat("PlayerAtkDebuff", debuff);
        
        debuff = 0.5f;
        player.critProb -= num;
        if (player.critProb <= 0)
        {
            player.critProb = 0;
        }
        PlayerPrefs.SetFloat("PlayerCritProb", player.critProb);

        Rename();
    }

    public void Rename()
    {
        description = $"치명타 확률이 100%가 되는 대신 공격력이 {debuff*100}% 감소한다.";

    }

    public void Use()
    {
        debuff = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        num = 1 - player.critProb;
        player.critProb = 1;
        PlayerPrefs.SetFloat("PlayerCritProb", 1);
        player.atkDebuff = debuff;
        PlayerPrefs.SetFloat("PlayerAtkDebuff", debuff);
    }  
}
