using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aegis : ItemInfo, IOnItemUse , IOnItemEnhance,IonItemRename,IOnItemSell
{
    static int value = 5;

    public void Use()
    {
        
        PlayerPrefs.SetInt("Aegis", 1);
    }
    public void Enhance()
    {
        value = 10;
        isEnhanced = true;
        PlayerPrefs.SetInt("Aegis", 2);      
    }

    
    public void Rename()
    {
        description = $"근접공격을 맞았을 경우 공격한 적에게 {value}의 데미지를 준다";
    }

    public void ItemSell()
    {
        value = 5;
        PlayerPrefs.SetInt("Aegis", 0);
        Rename();
    }
}
