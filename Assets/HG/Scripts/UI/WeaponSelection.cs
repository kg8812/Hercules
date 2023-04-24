using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public GameObject[] selections;
    int snum;
    Player player;

    private void OnEnable()
    {
        for(int i = 0;i< selections.Length;i++)
        {
            selections[i].SetActive(false);
        }
        snum = 0;
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();
        Time.timeScale = 0;
    }
    private void Update()
    {
        selections[snum].SetActive(true);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selections[snum].SetActive(false);
            snum++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selections[snum].SetActive(false);
            snum--;
        }
        if (snum < 0) snum += selections.Length;
        if (snum >= selections.Length) snum = 0;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (snum)
            {
                case 0:
                    PlayerPrefs.SetInt("PlayerWeapon", 1);
                    player.weapon = PlayerPrefs.GetInt("PlayerWeapon");
                    break;
                case 1:
                    PlayerPrefs.SetInt("PlayerWeapon", 2);
                    player.weapon = PlayerPrefs.GetInt("PlayerWeapon");
                    break;
                case 2:
                    PlayerPrefs.SetInt("PlayerWeapon", 3);
                    player.weapon = PlayerPrefs.GetInt("PlayerWeapon");
                    break;
            }
            player.WeaponSelect();
            SkillReset();
            Time.timeScale = 1;
            gameObject.SetActive(false);

        }
    }

    void SkillReset()
    {
        if (PlayerPrefs.GetInt("God") == 0)
        {
            if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                SkillInven.instance.Add(SkillManager.instance.zeusSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
            else
                SkillInven.instance.godSkill = null;
        }
        else if (PlayerPrefs.GetInt("God") == 1)
        {
            if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                SkillInven.instance.Add(SkillManager.instance.aresSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
            else
                SkillInven.instance.godSkill = null;
        }
        else if (PlayerPrefs.GetInt("God") == 2)
        {
            if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                SkillInven.instance.Add(SkillManager.instance.hadesSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
            else
                SkillInven.instance.godSkill = null;
        }
        else if (PlayerPrefs.GetInt("God") == 3)
        {
            if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                SkillInven.instance.Add(SkillManager.instance.hermesSkill[0]);
            else
                SkillInven.instance.godSkill = null;
        }

    }
}
