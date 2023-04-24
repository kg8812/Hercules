using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingBoots : ItemInfo, IOnItemUse
{
    public void Use()
    {
        PlayerPrefs.SetInt("WingBoots", 1);
    }
}
