using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionCape : ItemInfo, IOnItemUse
{
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player.GetComponent<Roar>() == null)
        {
            player.gameObject.AddComponent<Roar>();
        }

        PlayerPrefs.SetInt("Lion",1);
    }
}
