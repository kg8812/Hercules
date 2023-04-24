using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerberus : EnemyAI, IOnDamage
{
    public AnimationClip att;
    public Animator ani;
    bool isAttack = false; //공격 코루틴 제어 변수
    bool isIdle = false;
    public GameObject bossPortal;
    public Transform LeftAttack;
    public Transform RightAttack;
    GameObject target2;

    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        bossPortal.SetActive(false);
        target2 = GameObject.Find("Player").transform.Find("Heracles").gameObject;
    }

    protected override void Update()
    {
       
        
        base.Update();
        switch (state)
        {
            case EnemyState.idle:
                 ani.SetBool("IsWalk", false);
                StartCoroutine(ToMove());
                break;
            case EnemyState.move:
                ani.SetBool("IsWalk", true);
                if (distance < 3) StartCoroutine(Attack());
                break;
        }
        float dirx = this.transform.position.x - target2.transform.position.x;
        if(dirx>0)
        {
            LeftAttack.gameObject.SetActive(true);
            RightAttack.gameObject.SetActive(false);
        }
        else
        {
            LeftAttack.gameObject.SetActive(false);
            RightAttack.gameObject.SetActive(true);
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
            yield return new WaitForSeconds(1.5f);
            isAttack = false;
        }
    }

    public void OnHit(float damage)
    {



        if (hp > 0) hp -= damage;



        if (hp <= 0)
        {
            state = EnemyState.dead;
            Destroy(bar.gameObject);
            Destroy(this.gameObject);
            bossPortal.SetActive(true);
        }



    }
}
