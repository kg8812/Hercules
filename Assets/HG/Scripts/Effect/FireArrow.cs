using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : EffectDmgText
{
    public GameObject explosionEffect;
    Player player;
    float dmg;
    
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Invoke("Explode", 1.2f);
        dmg = player.dmg * 0.4f;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<IOnDamage>() != null)
        {
            collision.GetComponent<IOnDamage>().OnHit(dmg);
            Create(dmg, collision.ClosestPoint(transform.position), Color.white);
        }

        Explode();

    }

    void Explode()
    {
        GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(exp, 0.4f);
        Destroy(gameObject);
    }
}
