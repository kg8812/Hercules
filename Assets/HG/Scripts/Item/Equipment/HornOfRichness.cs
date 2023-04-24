using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornOfRichness : ItemInfo, IOnItemUse
{
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.goldGain += 0.5f;
        PlayerPrefs.SetFloat("PlayerGoldGain", player.goldGain);
    }
}
