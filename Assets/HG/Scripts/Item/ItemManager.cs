using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public ItemInfo[] equip;    // 장비아이템 목록
    public MaterialInfo[] mats; // 재료아이템 목록
    public ConsumableInfo[] consumes;   // 소비아이템 목록

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            for(int i = 0; i < consumes.Length; i++)
            {
                consumes[i].isCd = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }


        DontDestroyOnLoad(gameObject);
    }
    public void NewGame()
    {
        foreach (ItemInfo x in equip)
        {
            x.isOwn = false;
            x.isEnhanced = false;
            x.isCrafted = false;
            x.isSold = false;
            x.isResold = false;
            x.bluePrint = false;
        }
        Equipment.instance.itemList.Clear();

        foreach(MaterialInfo x in mats)
        {
            x.count = 0;          
        }
        PlayerPrefs.SetInt("AtkSpellBook", 0);
        PlayerPrefs.SetInt("CdmgSpellBook", 0);
        PlayerPrefs.SetInt("CrateSpellBook", 0);
        Material.instance.itemList.Clear();

        foreach(ConsumableInfo x in consumes)
        {
            x.count = 0;
        }
        Consumable.instance.itemList.Clear();
    }

    public void Load()
    {
        foreach (ItemInfo x in equip)
        {
            if (x.isOwn && !Equipment.instance.itemList.Contains(x))
            {
                Equipment.instance.Add(x);
            }
        }
        Equipment.instance.UseItems();
        
        foreach(MaterialInfo x in mats)
        {
            if (x.count > 0 && Material.instance.GetItemCount(x) == 0)
            {
                int num = x.count;
                for(int i = 0; i < num; i++)
                {
                    Material.instance.Add(x);
                }
            }
        }

        foreach(ConsumableInfo x in consumes)
        {
            if (x.count > 0 && Consumable.instance.GetCount(x) == 0)
            {
                int num = x.count;
                for(int i = 0; i < num; i++)
                {
                    Consumable.instance.Add(x);
                }
            }
        }

    }
}
