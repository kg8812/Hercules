using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderRushEffect : EffectDmgText
{
    float damage = 50f;
    GameObject eff;

    protected override void Start()
    {
        base.Start();
        eff = EffectManager.instance.thunderEffect1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage dmg = collision.GetComponent<IOnDamage>();

        if (dmg != null)
        {
            GameObject e = Instantiate(eff,collision.transform.position,Quaternion.identity);
            Destroy(e, 0.5f);
            dmg.OnHit(damage);
            Create(damage, collision.transform.position, Color.white);

        }
    }
}
