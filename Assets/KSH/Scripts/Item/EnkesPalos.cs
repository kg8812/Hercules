using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnkesPalos : ItemInfo,IOnItemUse
{
    //공격 속도 증가 
    public void Use()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

   
}
