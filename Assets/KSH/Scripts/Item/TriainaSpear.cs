using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriainaSpear : ItemInfo, IOnItemUse

{
    //스킬 공격력% 증가 
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        player.skillDmg += 15;
        PlayerPrefs.SetFloat("PlayerSkillDmg", player.skillDmg);
    }
}



