using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LandEffect : EffectDmgText
{
    Player player;
    public static float dmgMulti = 1.5f;
   

    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        IOnDamage dmg = collision.gameObject.GetComponent<IOnDamage>();

        if (dmg != null)
        {
            dmg.OnHit(player.atk * dmgMulti);
            Create(player.atk * dmgMulti, collision.ClosestPoint(transform.position), Color.white);
        }
    }
}
