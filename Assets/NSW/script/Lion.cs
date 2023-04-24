using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class Lion : Enemy, IOnDamage
{
    bool isSleep = true;
    public EnemyState state = EnemyState.idle;
    public Transform player;
    bool isRight=true;
    Animator animator;
    public AnimationClip clip;
    bool isMove = false;
    bool isDead = false;
    public GameObject portal;
    public Flowchart liondead;
    public GameObject lion;
    public GameObject lion2;

    void Start()
    {
        animator = GetComponent<Animator>();
        portal.SetActive(false);
    }


    void Update()
    {
        if (isSleep || state == EnemyState.dead) return;
        float dirX = transform.position.x - player.position.x;
        float distance = Vector2.Distance(this.transform.position, player.position);
        if(dirX > 0)
        {
            isRight = false;
            this.transform.localScale = new Vector3(-30, 30, 1);
        }
        else if(dirX <0)
        {
            isRight = true;
            this.transform.localScale = new Vector3(30, 30, 1);
        }
        if(state == EnemyState.idle)
        {
            animator.SetBool("isMove", false);
            animator.SetBool("isAttack", false);
            if (!isMove)
            {
                StartCoroutine(Run());
            }
        }
        else if(state == EnemyState.move)
        {
            if (isRight)
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            if(distance < 10)
            {
                state = EnemyState.attack;
                animator.SetBool("isMove", false);
                animator.SetBool("isAttack", true);
            }
        }
        else if(state == EnemyState.attack)
        {
            if (distance >= 10)
            {
                state = EnemyState.move;
                animator.SetBool("isMove", true);
                animator.SetBool("isAttack", false);
            }
        }
    }

   
    private void OnBecameVisible()
    {
        isSleep = false;
        lion.SetActive(true);
    }
    IEnumerator Run()
    {
        isMove = true;
        state = EnemyState.move;
        animator.SetBool("isMove", true);
        yield return new WaitForSeconds(5.0f);
        state = EnemyState.idle;
        yield return new WaitForSeconds(2.5f);
        isMove = false;
    }
    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {
            
            state = EnemyState.dead;
            StartCoroutine(Die());
            lion2.SetActive(true);
            
        }
    }
    IEnumerator Die()
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            yield return new WaitForSeconds(clip.length);
            Destroy(bar.gameObject);
            Destroy(gameObject);
            portal.SetActive(true);
            liondead.ExecuteBlock("후반부");
        }
    }
}
