using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Argryrotoxus : ItemInfo, IOnItemUse
{
    //치명타 피해 %증가
    
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        player.critDmg += 0.1f;

        PlayerPrefs.SetFloat("Player.critDmg",player.critDmg);



    }

    
}
