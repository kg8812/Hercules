using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float dmg;
    public bool isAttacked = false;

    private void OnEnable()
    {
        isAttacked = false;
    }
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage iDmg = collision.GetComponent<IOnDamage>();
        
        if(iDmg != null&&!isAttacked)
        {
            isAttacked = true;
            iDmg.OnHit(dmg);
            
        }
    }


}
