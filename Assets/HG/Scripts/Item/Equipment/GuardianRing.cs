using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianRing : ItemInfo, IOnItemUse,IOnItemEnhance,IonItemRename,IOnItemSell
{
    public void Enhance()
    {

        Shield.cd -= 5; // 쿨타임 5초 감소
        Rename();
    }

    public void ItemSell()
    {
        Shield.cd = 15;
        Rename();
    }

    public void Rename() // 설명 새로고침
    {
        description = $"{Shield.cd}초마다 한번 공격을 무효화시킨다.";
    }

    public void Use()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.gameObject.GetComponent<Shield>() == null)
            player.gameObject.AddComponent<Shield>();
    }
}
