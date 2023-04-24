using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesHat : ItemInfo, IOnItemUse,IOnItemEnhance,IonItemRename,IOnItemSell
{  
    public void Enhance()
    {
        FlyingDmg.isEnhanced = true;       
    }

    public void ItemSell()
    {
        FlyingDmg.isEnhanced = false;
        Rename();
    }

    public void Rename()
    {
        description = $"헤르메스의 모자. 공중에서 데미지가 {FlyingDmg.ratio * 100}% 증가한다.";
    }

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player.GetComponent<FlyingDmg>() == null)
        {
            player.gameObject.AddComponent<FlyingDmg>();
        }
    }
}
