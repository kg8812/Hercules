using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atlas : ItemInfo, IOnItemUse, IOnItemEnhance,IonItemRename,IOnItemSell
{
    static int value = 30;
    static float increasement = 0;
    float sValue;

    public void Use()
    {            
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float maxHp = PlayerPrefs.GetFloat("PlayerMaxHp") + (PlayerPrefs.GetFloat("PlayerMaxHp")/10.0f*3);
        increasement += (PlayerPrefs.GetFloat("PlayerMaxHp") / 10.0f) * 3;
        float speed = PlayerPrefs.GetFloat("PlayerSpeed") - PlayerPrefs.GetFloat("PlayerSpeed")/10.0f;
        PlayerPrefs.SetFloat("PlayerMaxHp", maxHp);
        PlayerPrefs.SetFloat("PlayerSpeed", speed);
        sValue = player.speed - speed;
        player.maxHp = PlayerPrefs.GetFloat("PlayerMaxHp");
        player.speed = PlayerPrefs.GetFloat("PlayerSpeed");

    }
    public void Enhance()
    {
        isEnhanced = true;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float maxHp = PlayerPrefs.GetFloat("PlayerMaxHp") + (PlayerPrefs.GetFloat("PlayerMaxHp") / 10.0f);

        increasement += (PlayerPrefs.GetFloat("PlayerMaxHp") / 10.0f);
        PlayerPrefs.SetFloat("PlayerMaxHp", maxHp);
        value = 40;
        player.maxHp = PlayerPrefs.GetFloat("PlayerMaxHp");
       
    }

    public void Rename()
    {
        description = $"최대체력 {value}%증가 이동속도 10% 감소";

    }

    public void ItemSell()
    {
        value = 30;
        player.maxHp -= increasement;
        increasement = 0;
        player.speed += sValue;
        PlayerPrefs.SetFloat("PlayerSpeed", player.speed);
        PlayerPrefs.SetFloat("PlayerMaxHp", player.maxHp);
        Rename();

    }
}
