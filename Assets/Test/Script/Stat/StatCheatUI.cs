using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class StatCheatUI : MonoBehaviour
{   
    public Button[] upBtn;
    public Button[] downBtn;
    public InputField[] statValue;
    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        for(int i = 0; i < upBtn.Length; i++)
        {
            int temp = i;
            upBtn[temp].onClick.AddListener(() => StatUp(temp));
        }

        for(int i = 0; i < downBtn.Length; i++)
        {
            int temp = i;
            downBtn[temp].onClick.AddListener(() => StatDown(temp));
        }
    }

    public void StatReset()
    {
        player.maxHp = PlayerPrefs.GetFloat("PlayerMaxHp");
        player.currHp = PlayerPrefs.GetFloat("PlayerCurrHp");
        player.atk = PlayerPrefs.GetFloat("PlayerAtk");
        player.atkSpeed = PlayerPrefs.GetFloat("PlayerAtkSpeed");      
        player.def = PlayerPrefs.GetFloat("PlayerDef");
        player.speed = PlayerPrefs.GetFloat("PlayerSpeed");
        player.critProb = PlayerPrefs.GetFloat("PlayerCritProb");
        player.critDmg = PlayerPrefs.GetFloat("PlayerCritDmg");
        player.goldGain = PlayerPrefs.GetFloat("PlayerGoldGain");
        player.skillDmg = PlayerPrefs.GetFloat("PlayerSkillDmg");
    }    

    public void StatUp(int x)
    {
        ref float y = ref player.atk;

        switch (x)
        {
            case 0: 
                y = ref player.atk;
                break;
            case 1:
                y = ref player.def;
                break;
            case 2:
                y = ref player.maxHp;
                break;
            case 3:
                y = ref player.critProb;
                break;
            case 4:
                y = ref player.critDmg;
                break;
            case 5:
                y = ref player.skillDmg;
                break;
            case 6:
                y = ref player.atkSpeed;
                break;
            case 7:
                y = ref player.speed;
                break;
            case 8:
                y = ref player.goldGain;
                break;
            case 9:
                y = ref player.gold;
                break;
        }
        
        if (float.TryParse(statValue[x].text, out float z))
        {
            if (3 <= x && x <= 8 && x != 5 && x != 7)
                z /= 100;
            else if (x == 7)
                z = z * 15 / 100;

            y += z;
        }

    }

    public void StatDown(int x)
    {
        ref float y = ref player.atk;

        switch (x)
        {
            case 0:
                y = ref player.atk;
                break;
            case 1:
                y = ref player.def;
                break;
            case 2:
                y = ref player.maxHp;
                break;
            case 3:
                y = ref player.critProb;
                break;
            case 4:
                y = ref player.critDmg;
                break;
            case 5:
                y = ref player.skillDmg;
                break;
            case 6:
                y = ref player.atkSpeed;
                break;
            case 7:
                y = ref player.speed;
                break;
            case 8:
                y = ref player.goldGain;
                break;
            case 9:
                y = ref player.gold;
                break;
        }
        if (float.TryParse(statValue[x].text, out float z))
        {

            if (3 <= x && x <= 8 && x != 5 && x != 7)
                z /= 100;
            else if (x == 7)
                z = z * 15 / 100;

            y -= z;
        }

        if (y <= 0) y = 1;
    }

}
