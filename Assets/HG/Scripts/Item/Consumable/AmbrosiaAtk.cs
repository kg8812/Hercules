using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbrosiaAtk : ConsumableInfo,IOnItemUse
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
            player.atk += 5;
            PlayerPrefs.SetFloat("PlayerAtk", player.atk);

            isCd = true;

            Invoke("EndBuff", 30);
        }
    }
    void EndBuff()
    {
        isCd = false;
        player.atk -= 5;
        PlayerPrefs.SetFloat("PlayerAtk", player.atk);
    }
}
