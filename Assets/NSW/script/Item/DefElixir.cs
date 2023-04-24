using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefElixir : ItemInfo, IOnItemUse
{
    // Update is called once per frame
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        int num = PlayerPrefs.GetInt("DefElxir");
        num++;
        PlayerPrefs.SetInt("DefElxir", num);
        player.def = PlayerPrefs.GetFloat("PlayerDef") + PlayerPrefs.GetInt("DefElxir") * 10;
    }
}
