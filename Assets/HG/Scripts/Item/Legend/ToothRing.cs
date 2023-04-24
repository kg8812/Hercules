using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothRing : ItemInfo,IOnItemUse
{

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player.GetComponent<BonusAttack>() == null)
            player.gameObject.AddComponent<BonusAttack>();
    }

    
}
