using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbrosiaDef : ConsumableInfo, IOnItemUse
{
    
    Player player;
    public void Use()
    {
        Buff();
    }

    void Buff()
    {
        if (!isCd)
        {
            count--;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.def += 5;
            PlayerPrefs.SetFloat("PlayerDef", player.def);

            isCd = true;

            Invoke("EndBuff", 30);
        }
    }
    void EndBuff()
    {
        isCd = false;
        player.def -= 5;
        PlayerPrefs.SetFloat("PlayerDef", player.def);
    }
}
