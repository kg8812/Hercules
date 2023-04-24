using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Diomedes : Enemy,IOnDamage
{
    public EnemyState state = EnemyState.idle;
    Animator ani;
    Rigidbody2D rigid;

    float distance;
    bool isJump = false;
    bool isFlip = true;
    bool isAttack = false;

    Transform target;
    public AnimationClip jump;
    public AnimationClip[] attack;

    public GameObject bloodEffect;
    bool isStart = false;
    public Transform bloodPos;
    public GameObject say;

    public void ToStart()
    {
        isStart = true;
    }
    // Start is called before the first frame update
    void Start()
    {       
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform.Find("Heracles").transform;
    }

    private void OnEnable()
    {
        state = EnemyState.idle;
        isJump = false;
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(target.position, transform.position);
        switch (state)
        {
            case EnemyState.idle:
                ani.SetBool("IsIdle", true);
                if (isStart)
                StartCoroutine(Jump());
                break;
            case EnemyState.move:
                ani.SetBool("IsIdle", false);
                if (distance < 4) Attack();
                break;
            case EnemyState.attack:
                ani.SetBool("IsIdle", false);
                rigid.velocity = Vector3.zero;
                break;
        }
    }

    IEnumerator Jump()
    {
        if (!isJump)
        {
            state = EnemyState.move;
            isJump = true;
            ani.SetTrigger("Jump");
            yield return new WaitForSeconds(0.2f);
            if (isFlip) rigid.AddForce(Vector2.left * 25, ForceMode2D.Impulse);
            else rigid.AddForce(Vector2.right * 25, ForceMode2D.Impulse);            

            yield return new WaitForSeconds(jump.length-0.2f);
            Attack();

            yield return new WaitForSeconds(Random.Range(1.5f,2.5f));
            isJump = false;
        }
    }

    void Attack()
    {
        float random = Random.Range(0f, 1f);
        if (random < 0.4f) StartCoroutine(Attack1());
        else if (random < 0.7f) StartCoroutine(Attack2());
        else StartCoroutine(Attack3());
    }
    IEnumerator Attack1()
    {
        if (!isAttack)
        {
            isAttack = true;
            rigid.velocity= Vector2.zero;
            state = EnemyState.attack;
            ani.SetTrigger("Attack1");
            yield return new WaitForSeconds(attack[0].length);
            state = EnemyState.idle;
            isAttack = false;
        }
    }

    IEnumerator Attack2()
    {
        if (!isAttack)
        {
            isAttack = true;
            rigid.velocity = Vector2.zero;
            state = EnemyState.attack;
            int rand = Random.Range(1, 4);
            for(int i = 0; i < rand; i++)
            {
                ani.SetTrigger("Attack2");
                yield return new WaitForSeconds((attack[1].length));
            }
            state = EnemyState.idle;
            isAttack = false;
        }
    }

    IEnumerator Attack3()
    {
        if (!isAttack)
        {
            isAttack = true;
            rigid.velocity = Vector2.zero;
            state = EnemyState.attack;
            ani.SetTrigger("Attack3");
            yield return new WaitForSeconds(attack[2].length);
            state = EnemyState.idle;
            isAttack = false;
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 180, 0);
        if (isFlip) isFlip = false;
        else isFlip = true;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "MainCamera")
            Rotate();
    }

    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Die();

    }

    void Die()
    {
        GameObject b = Instantiate(bloodEffect, bloodPos.transform.position, Quaternion.identity);
        b.transform.localScale *= 1.5f;
        say.SetActive(true);
        Destroy(gameObject);
    }
}
