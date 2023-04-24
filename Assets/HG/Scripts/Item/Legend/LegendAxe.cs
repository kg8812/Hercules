using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendAxe : ItemInfo, IOnItemUse
{

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player.GetComponent<AutoAttack>() == null)
        {
            player.gameObject.AddComponent<AutoAttack>();
        }
    }
}
