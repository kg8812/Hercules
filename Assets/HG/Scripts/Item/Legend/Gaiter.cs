using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaiter : ItemInfo, IOnItemUse
{  

    public void Use()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.GetComponent<PowerLand>() == null)
        {
            player.gameObject.AddComponent<PowerLand>();
        }
       
    }
}
