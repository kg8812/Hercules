using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkElixir : ItemInfo, IOnItemUse
{
    // Update is called once per frame
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        int num = PlayerPrefs.GetInt("AttackElxir");
        num++;
        PlayerPrefs.SetInt("AttackElxir", num);
        player.atk = PlayerPrefs.GetFloat("PlayerAtk") + PlayerPrefs.GetInt("AttackElxir") * 10;
    }
}
