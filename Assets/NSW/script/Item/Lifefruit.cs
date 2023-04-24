using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifefruit : ItemInfo, IOnItemUse
{
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float maxhp = PlayerPrefs.GetFloat("PlayerMaxHp");
        maxhp += 100;
        PlayerPrefs.SetFloat("PlayerMaxHp", maxhp);
        player.maxHp = maxhp;
    }
}
