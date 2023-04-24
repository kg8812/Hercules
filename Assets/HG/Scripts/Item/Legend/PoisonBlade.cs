using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlade : ItemInfo, IOnItemUse
{
    public void Use()
    {
        PlayerPrefs.SetInt("HydraPoison", 1);
    }
}
