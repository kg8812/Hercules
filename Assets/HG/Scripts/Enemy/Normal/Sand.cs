using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : EnemyAI,IOnDamage
{  
    Animator ani; //애니메이터    
   
    bool AttackTime = false;    //공격 쿨타임

    bool isDead = false;
    public AnimationClip deathClip;

    
    public AnimationClip att;   //공격하는시간
    public EnemyAttack leftAtk;
    public EnemyAttack rightAtk;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        leftAtk.dmg = atk;
        rightAtk.dmg = atk;
        hp = maxHp;
        ani = GetComponent<Animator>(); //애니메이터 참조
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isStop) return;

        if (distance < 4) StartCoroutine(Attack());
    }
   
    void Rotate()   //회전
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }
    
    IEnumerator Attack()
    {
        
        if (!AttackTime)
        {
            AttackTime = true;           
            ani.SetTrigger("Attack");
            state = EnemyState.idle;
            yield return new WaitForSeconds(0.4f);
            GameObject trigger;
            if (isFlip)
            {
                trigger = leftAtk.gameObject;
            }
            else
            {
                trigger = rightAtk.gameObject;
            }
            trigger.SetActive(true);
            Rotate();
            yield return new WaitForSeconds(0.7f);
            trigger.SetActive(false);

            yield return new WaitForSeconds(att.length - 2.2f);
            Rotate();
            state = EnemyState.move;
            yield return new WaitForSeconds(0.7f);
            AttackTime = false;

        }
    }

    public void OnHit(float damage)
    {
        if (hp > 0)
        hp-= damage;

        if (hp <= 0)
        {
            state = EnemyState.dead;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (!isDead)
        {
            isDead = true;
            ani.SetTrigger("IsDead");
            yield return new WaitForSeconds(deathClip.length);
            Destroy(bar);

            Destroy(gameObject);
        }
    }
}