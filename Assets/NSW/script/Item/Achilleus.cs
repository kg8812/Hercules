using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achilleus : ItemInfo, IOnItemUse, IOnItemEnhance
{
    public void Use()
    {
        
        PlayerPrefs.SetInt("Achilleus", 1);

    }
    public void Enhance()
    {
        description = "7이하의 피해를 받았을 경우 피해가 1이 된다.";
        isEnhanced = true;
        PlayerPrefs.SetInt("Achilleus", 2);
    }
}
