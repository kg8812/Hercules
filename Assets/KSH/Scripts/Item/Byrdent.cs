using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Byrdent : ItemInfo, IOnItemUse, IOnItemEnhance, IonItemRename, IOnItemSell
{
    static float critProb = 0.2f;
    static float critDmg = 0.12f;

    public void Enhance()
    {
        isEnhanced = true;
        critProb += 0.15f;
        critDmg += 0.09f;

        player.critDmg -= 0.09f;
        player.critProb += 0.15f;
        PlayerPrefs.SetFloat("PlayerCritProb", player.critProb);
        PlayerPrefs.SetFloat("PlayerCritDmg", player.critDmg);
    }

    public void ItemSell()
    {
        player.critDmg += critDmg;
        player.critProb -= critProb;

        critProb = 0.2f;
        critDmg = 0.12f;
        PlayerPrefs.SetFloat("PlayerCritProb", player.critProb);
        PlayerPrefs.SetFloat("PlayerCritDmg", player.critDmg);
        Rename();

    }

    public void Rename()
    {
        description = $"치명타 피해가 {critDmg * 100}%만큼 감소하고 치명타확률이 {critProb*100}%만큼 증가한다.";
    }


    //치명타 피해가 30%만큼 감소하고  치명타확률이 15%만큼 증가한다.
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.critDmg -= critDmg;
        player.critProb += critProb;
        PlayerPrefs.SetFloat("PlayerCritProb", player.critProb);
        PlayerPrefs.SetFloat("PlayerCritDmg", player.critDmg);

    }


}
