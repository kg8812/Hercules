using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambrosia : ItemInfo, IOnItemUse
{
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        int r = Random.Range(0, 6);
        if(r == 1)
        {
            PlayerPrefs.SetInt("Buff", 5);
            player.def = PlayerPrefs.GetFloat("PlayerDef") + PlayerPrefs.GetInt("Buff");
        }
        else if(r == 2)
        {
            PlayerPrefs.SetInt("Buff", 5);
            player.atk = PlayerPrefs.GetFloat("PlayerAtk") + PlayerPrefs.GetInt("Buff");
        }
        else if(r == 0)
        {
            PlayerPrefs.SetInt("Buff", 100);
            player.maxHp = PlayerPrefs.GetFloat("PlayerMaxHp") + PlayerPrefs.GetInt("Buff");
            player.currHp = PlayerPrefs.GetFloat("PlayerCurrHp") + PlayerPrefs.GetInt("Buff");
        }
        else if(r == 4)
        {
            PlayerPrefs.SetInt("Buff", 5);
            player.def = PlayerPrefs.GetFloat("PlayerDef") - PlayerPrefs.GetInt("Buff");
        }
        else if (r == 5)
        {
            PlayerPrefs.SetInt("Buff", 5);
            player.atk = PlayerPrefs.GetFloat("PlayerAtk") - PlayerPrefs.GetInt("Buff");
        }
        else
        {
            PlayerPrefs.SetInt("Buff", 100);
            player.maxHp = PlayerPrefs.GetFloat("PlayerMaxHp") - PlayerPrefs.GetInt("Buff");
            player.currHp = PlayerPrefs.GetFloat("PlayerCurrHp") - PlayerPrefs.GetInt("Buff");
        }


    }
}
