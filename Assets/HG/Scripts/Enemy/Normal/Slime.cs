using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Slime : EnemyAI,IOnDamage
{
    Animator ani; //애니메이터
    public AnimationClip att;   //공격 애니메이션
    bool isAttack = false; //공격 코루틴 제어 변수
    bool isIdle = false;
    GameObject blood;
    EnemyAttack attack;

    protected override void Start()
    {
        base.Start();
        attack = transform.GetComponentInChildren<EnemyAttack>();

        blood = EffectManager.instance.bloodEffect;
        ani = GetComponent<Animator>();
    }
    protected override void Update()
    {
       
        base.Update();
        if (isStop) return;

        switch (state)
        {
            case EnemyState.idle:
                ani.SetBool("IsWalk", false);
                StartCoroutine(ToMove());
                break;
            case EnemyState.move:
                ani.SetBool("IsWalk", true);
                if (distance < 4) StartCoroutine(Attack());
                break;
        }
    }

    IEnumerator ToMove()
    {
        if (!isIdle)
        {
            isIdle = true;
            yield return new WaitForSeconds(2f); //대기상태에서 2초후에 다시 움직임
            state = EnemyState.move;
            isIdle = false;
        }
    }

    IEnumerator Attack()
    {
        if (!isAttack)
        {
            attack.dmg = atk;
            isAttack = true;
            ani.SetTrigger("Attack");
            state = EnemyState.idle;
            yield return new WaitForSeconds(2);
            isAttack = false;
        }
    }

    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Instantiate(blood,transform.position,Quaternion.identity);
            Destroy(bar);
            Destroy(gameObject);
        }
    }
}
