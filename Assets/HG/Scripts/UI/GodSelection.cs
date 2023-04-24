using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodSelection : MonoBehaviour
{
    public GameObject[] god;
    int num;
    public GameObject conv;
    GameObject player;
    int jumpMax;
    int dashMax;
    private void Start()
    {
        jumpMax = PlayerPrefs.GetInt("JumpMax");
        dashMax = PlayerPrefs.GetInt("DashMax");
    }
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < god.Length; i++)
        {
            god[i].SetActive(false);
        }
        num = 0;
        conv.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        god[num].SetActive(true);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ToLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            ToRight();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Select();
        }
    }

    public void ToRight()
    {
        god[num].SetActive(false);
        num++;
        if (num >= god.Length) num = 0;
    }
    public void ToLeft()
    {
        god[num].SetActive(false);
        num--;
        if (num < 0) num += god.Length;
    }

    public void Select()
    {
        switch (num)
        {
            case 0:                
                if (player.GetComponent<Zeus>() == null)
                {
                    player.AddComponent<Zeus>();
                    PlayerPrefs.SetInt("God", 0);                   
                }
                if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                    SkillInven.instance.Add(SkillManager.instance.zeusSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
                else
                    SkillInven.instance.godSkill = null;
                break;
            case 1:             
                if (player.GetComponent<Ares>() == null)
                {
                    player.AddComponent<Ares>();
                    PlayerPrefs.SetInt("God", 1);                 
                }
                if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                    SkillInven.instance.Add(SkillManager.instance.aresSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
                else
                    SkillInven.instance.godSkill = null;
                break;
            case 2:
                PlayerPrefs.SetInt("God", 2);
                PlayerPrefs.SetInt("Hades", 1);               

                if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                    SkillInven.instance.Add(SkillManager.instance.hadesSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
                else
                    SkillInven.instance.godSkill = null;
                break;
            case 3:
                PlayerPrefs.SetInt("God", 3);
                PlayerPrefs.SetInt("Hermes", 1);

                player.GetComponent<Player>().jumpMax = jumpMax + 1;
                player.GetComponent<Player>().dashMax = dashMax + 1;
                PlayerPrefs.SetInt("JumpMax", player.GetComponent<Player>().jumpMax);
                PlayerPrefs.SetInt("DashMax", player.GetComponent<Player>().dashMax);
                if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                    SkillInven.instance.Add(SkillManager.instance.hermesSkill[0]);
                else
                    SkillInven.instance.godSkill = null;
                

                break;
        }
        if (PlayerPrefs.GetInt("God") != 0)
        {
            Destroy(player.GetComponent<Zeus>());
        }
        if (PlayerPrefs.GetInt("God") != 1)
        {
            Destroy(player.GetComponent<Ares>());
        }
        if (PlayerPrefs.GetInt("God") != 2)
        {
            PlayerPrefs.SetInt("Hades", 0);
        }
        if (PlayerPrefs.GetInt("God") != 3)
        {
            PlayerPrefs.SetInt("Hermes", 0);
            player.GetComponent<Player>().jumpMax = jumpMax;
            player.GetComponent<Player>().dashMax = dashMax;
            PlayerPrefs.SetInt("JumpMax", player.GetComponent<Player>().jumpMax);
            PlayerPrefs.SetInt("DashMax", player.GetComponent<Player>().dashMax);
        }

        Time.timeScale = 1;
        gameObject.SetActive(false);

    }

    public static void Select(int num,GameObject player)
    {
        switch (num)
        {
            case 0:
                Destroy(player.GetComponent<Ares>());
                if (player.GetComponent<Zeus>() == null)
                {
                    player.AddComponent<Zeus>();
                    PlayerPrefs.SetInt("God", 0);
                }
                if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                    SkillInven.instance.Add(SkillManager.instance.zeusSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
                else
                    SkillInven.instance.godSkill = null;
                break;
            case 1:
                Destroy(player.GetComponent<Zeus>());
                if (player.GetComponent<Ares>() == null)
                {
                    player.AddComponent<Ares>();
                    PlayerPrefs.SetInt("God", 1);
                }
                if (PlayerPrefs.GetInt("PlayerWeapon") > 0)
                    SkillInven.instance.Add(SkillManager.instance.aresSkill[PlayerPrefs.GetInt("PlayerWeapon") - 1]);
                else
                    SkillInven.instance.godSkill = null;
                break;           
                
        }
        if (PlayerPrefs.GetInt("God") != 0)
        {
            Destroy(player.GetComponent<Zeus>());
        }
        if (PlayerPrefs.GetInt("God") != 1)
        {
            Destroy(player.GetComponent<Ares>());
        }
        

    }
}
