using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectlie : EnemyAttack
{
    float hitTime = 0;
    private void Update()
    {
        isAttacked = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IOnDamage iDmg = collision.GetComponent<IOnDamage>();
        hitTime += Time.deltaTime;
        if (iDmg != null && !isAttacked && hitTime>=1.5f)
        {
            hitTime = 0;
            iDmg.OnHit(dmg);

        }
    }

}
