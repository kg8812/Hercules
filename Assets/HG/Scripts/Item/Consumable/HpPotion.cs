using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : ConsumableInfo,IOnItemUse
{
    public int recovery;
    Player player;

    public void Use()
    {
        count--;
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();

        player.currHp += player.maxHp / 10 * recovery;
        PlayerPrefs.SetFloat("PlayerCurrHp", player.currHp);
    }
}
