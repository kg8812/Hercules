using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jarngreipr : ItemInfo,IOnItemUse
{
    

    //치명타 확률 증가
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.critProb += 0.05f;
        PlayerPrefs.SetFloat("Player.critProb", player.critProb);

    }

    
    
}
