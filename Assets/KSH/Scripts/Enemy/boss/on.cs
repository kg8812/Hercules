using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on : EnemyAI,IOnDamage
{
    public AnimationClip att;
    public Animator ani;
    bool isAttack = false; //공격 코루틴 제어 변수
    bool isIdle = false;
    public GameObject bossPortal;
    public GameObject leftAtk;
    public GameObject rightAtk;
    public GameObject flowchart;
    protected override void Start()
    {
        bossPortal.SetActive(false);
        base.Start();
        ani = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        switch (state)
        {
            case EnemyState.idle:
                // ani.SetBool("IsWalk", false);
                StartCoroutine(ToMove());
                break;
            case EnemyState.move:
                //ani.SetBool("IsWalk", true);
                if (distance < 8) StartCoroutine(Attack());
                break;
        }
    }

    IEnumerator ToMove()
    {
        if (!isIdle)
        {
            isIdle = true;
            yield return new WaitForSeconds(1f); //대기상태에서 2초후에 다시 움직임
            state = EnemyState.move;
            isIdle = false;
        }
    }

    IEnumerator Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            ani.SetTrigger("Attack");
            state = EnemyState.idle;
            yield return new WaitForSeconds(2.5f);
            isAttack = false;
        }
    }

    public void TriggerOn()
    {
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

    public void TriggerOff()
    {
        rightAtk.SetActive(false);
        leftAtk.SetActive(false);

    }
    public void OnHit(float damage)
    {



        if (hp > 0) hp -= damage;



        if (hp <= 0)
        {
            flowchart.SetActive(true);
            state = EnemyState.dead;
            Destroy(bar.gameObject);
            Destroy(this.gameObject);
            bossPortal.SetActive(true);
        }



    }
}

