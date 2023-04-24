using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : EffectDmgText
{
    float dmg = 50;
    public float scaleFactor = 1;  

    void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage idmg = collision.gameObject.GetComponent<IOnDamage>();

        if (idmg != null)
        {
            idmg.OnHit(dmg * scaleFactor);
            Create(dmg* scaleFactor, collision.ClosestPoint(transform.position),Color.white);
        }
    }

}
