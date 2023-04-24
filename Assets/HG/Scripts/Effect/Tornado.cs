using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : EffectDmgText
{
    float attackPeriod = 0.2f;
    bool isAttacked = false;
    float atk = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            StartCoroutine(Attack(collision.gameObject));
        }
    }

    IEnumerator Attack(GameObject enemy)
    {
        if (!isAttacked)
        {
            isAttacked = true;
            IOnDamage idmg = enemy.GetComponent<IOnDamage>();
            if(idmg != null )
            {
                idmg.OnHit(atk);
                Create(atk, enemy.transform.position, Color.white);

            }
            yield return new WaitForSeconds(attackPeriod);
            isAttacked = false;
        }
    }
}
