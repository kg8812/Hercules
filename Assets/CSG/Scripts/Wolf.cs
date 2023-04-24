using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Wolf : EnemyAI, IOnDamage
{
   

    public bool isAttack = false;
    public bool isChargeAttack = false;

    public GameObject trigger;

    public GameObject leftAtk;
    public GameObject rightAtk;

    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {

            Destroy(bar.gameObject);
            Destroy(this.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isFlip)
        {         
            leftAtk.SetActive(true);
            rightAtk.SetActive(false);
        }
        else
        {         
            leftAtk.SetActive(false);
            rightAtk.SetActive(true);
        }
    }
    void TriggerOn()
    {
        trigger.SetActive(true);
        Invoke("TriggerOff", 0.6f);
    }
    void TriggerOff()
    {
        trigger.SetActive(false);
        TriggerOn();
    }
}
