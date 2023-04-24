using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ajax : ItemInfo, IOnItemUse, IOnItemEnhance,IonItemRename,IOnItemSell
{
    static int value = 30;

    public void Use()
    {
        PlayerPrefs.SetInt("Ajax", 1);
    }
    public void Enhance()
    {
        isEnhanced = true;
        value = 50;
        PlayerPrefs.SetInt("Ajax", 2);
    }

    public void Rename()
    {
        description = $"받는 피해를 {value}% 감소시킨다.";

    }

    public void ItemSell()
    {
        value = 30;
        PlayerPrefs.SetInt("Ajax", 0);

        Rename();
    }
}
