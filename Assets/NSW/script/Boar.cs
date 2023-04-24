using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class Boar : Enemy, IOnDamage
{
    public Transform player;
    public Transform leftSide;
    public Transform rightSide;
    public bool isRight = false;
    public bool isMove = false;
    bool isRage = false; // 광폭화 판정
    public GameObject portal;
    Animator _ani;
    public Flowchart flowchart;
    public EnemyState state = EnemyState.idle;
    float maxHP;
    bool isDead = false;
    public AnimationClip deathClip;
    CapsuleCollider2D capsulecollider2D;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = hp;
        _ani = GetComponent<Animator>();
        portal.SetActive(false); // 포탈은 보스 클리어전까지 비활성화
        capsulecollider2D = GetComponentInChildren<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.dead) return; // 죽은상태면 업데이트 실행 X 
        float dirx = this.transform.position.x - player.position.x; // 플레어의 왼쪽인지 오른쪽인지 결정
        if(state == EnemyState.idle)
        {
            if(!isMove)
            {
                if (hp > maxHP / 2.0f) 
                   StartCoroutine(BoarMove());
                else if(isRage) // HP가 절반이하면 광폭화
                    StartCoroutine(BoarAngry());
              

            }
           
            if(hp<=maxHP/2.0f && !isRage)
            {
                isRage = true;
                speed = 15;
           
            }
        }
        else if (state == EnemyState.move )
        {
            if (!isRight) // 좌우 방향 전환
            {

                transform.Translate(Vector2.left * speed * Time.deltaTime, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
        else if (state == EnemyState.attack)
        {
           
                
            
           if (!isRight)
           {

              transform.Translate(Vector2.left * speed * Time.deltaTime, 0);
              this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
           }
           else
           {
              transform.Translate(Vector2.right * speed * Time.deltaTime, 0);
              this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
           }
           
           
           
        }
       
    }
    IEnumerator BoarMove()
    {
        //일반 패턴 
        isMove = true;
        state = EnemyState.move;
        _ani.SetBool("isMove", true);
        yield return new WaitForSeconds(4.5f);
        state = EnemyState.idle;
        _ani.SetBool("isMove", false);
        yield return new WaitForSeconds(1.0f);
        isMove = false;
    }
    IEnumerator BoarAngry()
    {
        //광폭화 패턴 
        isMove = true;
        state = EnemyState.attack;
        _ani.SetBool("isRun", true);
        yield return new WaitForSeconds(4.5f);
        state = EnemyState.idle;
        _ani.SetBool("isRun", false);
        yield return new WaitForSeconds(1.0f);
        isMove = false;

    }
    
    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {
            flowchart.ExecuteBlock("맷돼지클리어");
            state = EnemyState.dead;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (!isDead)
        {
            isDead = true;
            _ani.SetTrigger("Dead");          
            yield return new WaitForSeconds(deathClip.length);
            Destroy(bar.gameObject);
            Destroy(gameObject);
            portal.SetActive(true);
        }
    }
    public void DeadTranslate()
    {
        this.transform.position = new Vector2(this.transform.position.x, -8);
    }
}

