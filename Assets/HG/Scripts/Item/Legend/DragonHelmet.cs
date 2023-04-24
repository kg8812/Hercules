using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHelmet : ItemInfo, IOnItemUse
{  
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player.GetComponent<DragonBreath>() == null)
        {
            player.gameObject.AddComponent<DragonBreath>();
        }
       
    }
}
