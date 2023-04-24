using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class Hydra : EnemyAI, IOnDamage
{
    public Animator ani;
    bool isEnabled = false;
    bool isAttackCd = false;
    public Flowchart middle;
    public GameObject hydra;
    public GameObject hydra2;
    public GameObject portal;
    bool isStart = false;
    public void ToStart()
    {
        isStart = true;
    }
    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
            hydra2.SetActive(true);
        }
        else
        {
            if (damage >= 50)
            {
                ani.SetTrigger("Hit");
            }
        }
    }

    public void Die()
    {
        ani.SetBool("isDeath", true);
        state = EnemyState.dead;
        Destroy(gameObject,4);
        portal.SetActive(true);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        portal.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(!isEnabled || !isStart)
        {
            return;
        }
        base.Update();
        switch (state)
        {
            case EnemyState.idle:
                ani.SetBool("isIdle", true);
                ani.SetBool("isWalking", false);
                break;
            case EnemyState.move:
                ani.SetBool("isIdle", false);
                ani.SetBool("isWalking", true);
                if (distance < 7)
                {
                    StartCoroutine(Attack());
                }
                break;
        }

        
    }
    IEnumerator Attack()
    {
        if (!isAttackCd)
        {
            isAttackCd = true;
            ani.SetTrigger("Attack");
            yield return new WaitForSeconds(1.2f);
            isAttackCd = false;
        }
    }
   void ToMove()
    {
        state = EnemyState.move;
        Invoke("ToIdle", 7);
    }

    void ToIdle()
    {
        state = EnemyState.idle;
        Invoke("ToMove", 3);
    }

    private void OnBecameVisible()
    {
        isEnabled = true;
        ToMove();

        hydra.SetActive(true);

    }
   

}
