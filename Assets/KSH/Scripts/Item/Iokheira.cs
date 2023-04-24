using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iokheira : ItemInfo, IOnItemUse,IOnItemSell
{
    public void ItemSell()
    {
        PlayerPrefs.SetFloat("Iokheira", 0); 
    }

    //이오케이라 적에게 입힌 피해중 ?% 피해무시 데미지입힘
    public void Use()
    {
        PlayerPrefs.SetFloat("Iokheira", 1);
    }
}

