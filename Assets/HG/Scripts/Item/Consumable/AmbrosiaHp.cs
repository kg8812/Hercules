using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbrosiaHp : ConsumableInfo,IOnItemUse
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
            player.maxHp += 100;
            player.currHp += 100;
            PlayerPrefs.SetFloat("PlayerMaxHp", player.maxHp);
            PlayerPrefs.SetFloat("PlayerCurrHp", player.currHp);
            isCd = true;
            Invoke("EndBuff", 30);

        }
    }

    void EndBuff()
    {
        
        isCd = false;
        player.maxHp -= 100;
        PlayerPrefs.SetFloat("PlayerMaxHp", player.maxHp);
    }
}
