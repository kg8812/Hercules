using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Enemy,IOnDamage
{
    Animator ani;
    Vector2 dir;
    public AnimationClip rot;
    public AnimationClip att;
    public AnimationClip fire;
    public GameObject last;

    public Transform target;
    float moveSpeed;    
    Rigidbody2D rigid;

    bool isIdle = false;
    bool isButt = false;
    bool isRotate = false;
    bool isFlip = true;
    bool isFire = false;
    SpriteRenderer sprite;
    EnemyState state = EnemyState.idle;
    float distance;

    public EnemyAttack eAtk;
    bool isStart = false;
    bool isDead = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveSpeed = speed;
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform.Find("Heracles").transform;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
            return;

        distance = Vector2.Distance(target.position, transform.position);
        dir = target.position - transform.position;       
        if (isFlip) moveSpeed = -speed;
        else moveSpeed = speed;

        switch (state)
        {
            case EnemyState.idle:
                ani.SetBool("IsIdle", true);
                ani.SetBool("IsRun", false);
                rigid.velocity = new Vector2(0, rigid.velocity.y);

                if (isStart)
                StartCoroutine(ToMove());
                break;
            case EnemyState.move:
                ani.SetBool("IsRun", true);
                ani.SetBool("IsIdle", false);
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);               
                if (distance < 6)
                {
                    if ((isFlip && dir.x < 0) || (!isFlip && dir.x > 0))
                    StartCoroutine(Butt());
                }
                if (distance > 15 && distance < 20)
                {
                    if ((isFlip && dir.x < 0) || (!isFlip && dir.x > 0))
                        StartCoroutine(FireAttack());
                }

                break;
            case EnemyState.rotate:
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                break;
            case EnemyState.attack:

                if (!isFire || isButt)
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                break;
        }
    }
    void DmgReset()
    {
        eAtk.dmg = atk;
    }
    void ToStart()
    {
        isStart = true;
    }
    IEnumerator Rotate()
    {
        if (!isRotate)
        {
            state = EnemyState.rotate;
            isRotate = true;
            ani.SetTrigger("Rotate");
            yield return new WaitForSeconds(rot.length);
            if (isFlip)
            {               
                isFlip = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {               
                isFlip = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }


            isRotate = false;
            state = EnemyState.move;
        }
    }   
    IEnumerator Butt()
    {
        if (!isButt)
        {
            eAtk.dmg = atk;
            isButt = true;
            state = EnemyState.attack;
            ani.SetTrigger("Butt");
            yield return new WaitForSeconds(att.length);
            state = EnemyState.idle;
            yield return new WaitForSeconds(2);
            isButt = false;
        }
    }
    IEnumerator FireAttack()
    {
        if (!isFire)
        {
            eAtk.dmg = atk * 1.5f;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            isFire = true;
            state = EnemyState.attack;
            ani.SetTrigger("FireAttack");
            yield return new WaitForSeconds(0.5f);
            if (isFlip) rigid.AddForce(Vector2.left * 30, ForceMode2D.Impulse);
            else rigid.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            yield return new WaitForSeconds(fire.length-0.5f);
            state = EnemyState.move;

            yield return new WaitForSeconds(7f);
            isFire = false;
        }
    }
    IEnumerator ToMove()
    {
        if (!isIdle)
        {
            isIdle = true;
            yield return new WaitForSeconds(1.5f);
            state = EnemyState.move;
            isIdle = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "MainCamera")
            StartCoroutine(Rotate());
    }

    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ani.SetTrigger("Die");
        isDead = true;
        last.SetActive(true);
        Destroy(gameObject, 3f);
    }
}
