using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexFruit : ItemInfo, IOnItemUse
{
    // Start is called before the first frame update
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        float speed = PlayerPrefs.GetFloat("PlayerSpeed");
        speed ++;
        PlayerPrefs.SetFloat("PlayerSpeed", speed);
        player.speed = PlayerPrefs.GetFloat("PlayerSpeed");
    }
}
