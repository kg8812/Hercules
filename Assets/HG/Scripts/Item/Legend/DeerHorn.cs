using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerHorn : ItemInfo, IOnItemUse
{
    public void Use()
    {
        PlayerPrefs.SetInt("DearHorn", 1);      
    }
}
