using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : ItemInfo, IOnItemUse
{ 

    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player.GetComponent<Gliding>() == null)
        player.gameObject.AddComponent<Gliding>();
    }
}
