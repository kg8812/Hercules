using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingEffect : EffectDmgText
{
    float dmg = 7;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage idmg = collision.GetComponent<IOnDamage>();
        if(idmg != null)
        idmg.OnHit(dmg);
        Create(dmg, collision.ClosestPoint(transform.position), Color.white);
    }
}
