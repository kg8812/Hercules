using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy,IOnDamage
{
    public void OnHit(float damage)
    {
        hp-= damage;
    }

    private void Start()
    {
        maxHp = 100000;
        hp = maxHp;
        InvokeRepeating("ToMax", 1, 1);
    }
   
    void ToMax()
    {
        hp = maxHp;
    }
}
