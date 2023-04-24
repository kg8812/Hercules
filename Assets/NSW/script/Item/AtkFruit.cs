using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkFruit : ItemInfo, IOnItemUse
{
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float atk = PlayerPrefs.GetFloat("PlayerAtk");
        atk += 5;
        PlayerPrefs.SetFloat("PlayerAtk", atk);
        player.atk = PlayerPrefs.GetFloat("PlayerAtk");

    }
}
