using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AresShield: ItemInfo, IOnItemUse
{
    public void Use()
    {
        float def = PlayerPrefs.GetFloat("PlayerDef") + 5.0f;
        PlayerPrefs.SetFloat("PlayerDef",def);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
      
        player.def = PlayerPrefs.GetFloat("PlayerDef");
       

    }
}
