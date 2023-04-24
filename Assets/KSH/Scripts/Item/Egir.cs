using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egir : ItemInfo,IOnItemUse,IOnItemSell
{
    public void ItemSell()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.atkSpeed -= 0.1f;
        player.critDmg -= 0.2f;
    }

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.atkSpeed += 0.1f;
        player.critDmg += 0.2f;
    }

   
}
