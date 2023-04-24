using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Skelet : Enemy,IOnDamage
{
    Animator ani; //애니메이터
    float moveSpeed; //이동속도
    float sp;
    GameObject target; //플레이어
    Rigidbody2D rigid;
    
    public bool isAttack = false; //공격 코루틴 제어 변수
    public bool isChargeAttack = false; //차지 공격 코루틴 제어
    float distance;     //플레이어와의 거리
    Vector2 dir;    // 이동방향
    bool isChase;
    bool rot;
    public bool isWalk = true;
    bool isTaunt = false;
    bool isFlip = false;
    bool isJump = false;
    

    public AnimationClip att;   //공격하는시간
    public AnimationClip chargeAtt; //차지
    public AnimationClip taunt; //도발시간
    public AnimationClip Rot; //회전 애니메이션

    public EnemyAttack leftAtk;
    public EnemyAttack rightAtk;
    GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        blood = EffectManager.instance.bloodEffect;
        leftAtk.dmg = atk;
        rightAtk.dmg = atk;

        sp = speed;
        moveSpeed = -sp;
        rigid =GetComponent<Rigidbody2D>(); 
        rot = false;
        ani = GetComponent<Animator>(); //애니메이터 참조
        target = GameObject.Find("Player").transform.Find("Heracles").gameObject;  //타겟 플레이어 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            rigid.velocity= Vector2.zero;
            return;
        }

        if (target != null) //플레이어를 찾았으면
        {
            distance = Vector2.Distance(transform.position, target.transform.position); //플레이어와의 거리 측정           

            if (distance < 10)  //거리 20미터이하면 플레이어 추적
            {
                isChase = true;

                if (isWalk)
                {
                    dir = target.transform.position - transform.position;   //이동방향 플레이어쪽으로 설정
                    
                    dir.Normalize();

                    if (distance > 6)
                        StartCoroutine(Taunt());
                    else if (distance < 4)
                        StartCoroutine(Attack());
                    

                    if (dir.y > -0.1f && dir.y < 0.1f)
                    {
                        if (dir.x < 0)  //플레이어가 왼쪽에 있을시
                        {
                            GetComponent<SpriteRenderer>().flipX = false;
                            isFlip = false;
                            moveSpeed = -sp;
                        }
                        else
                        {
                            GetComponent<SpriteRenderer>().flipX = true;
                            isFlip = true;
                            moveSpeed = sp;
                        }   //아니면 오른쪽으로 회전
                    }
                }                                 
                
            }
       
            else isChase = false;
            
        }
        else isChase = false;

        if (distance < 3) ani.SetBool("IsIdle", true);
        else ani.SetBool("IsIdle", false);

        RotateOnce(isChase);
       
    }

    void FixedUpdate()
    {
        if (isStop) return;

        if (isWalk && distance > 3)
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
        else if(!isJump)
            rigid.velocity = new Vector2(0, rigid.velocity.y);
    }
    IEnumerator Taunt()
    {
        if (!isTaunt)
        {          
            isTaunt = true;
            isWalk = false;
            
            ani.SetTrigger("Taunt");            
            yield return new WaitForSeconds(taunt.length+0.2f);
            isWalk = true;           
            yield return new WaitForSeconds(20);
            isTaunt = false;
        }

    }
    
    IEnumerator Rotate() //회전
    {
        if (isWalk)
        {
            moveSpeed *= -1;
            ani.SetTrigger("Rotate");
            isWalk = false;
            yield return new WaitForSeconds(Rot.length);
            isWalk = true;
            if (isFlip)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                isFlip = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                isFlip = true;
            }
        }
    }
    void RotateOnce(bool chase)
    {
        if (chase != rot&&!chase) StartCoroutine(Rotate());

        rot = chase;

    }
    IEnumerator Attack()
    {
        if (!isAttack&&isWalk)
        {
            isWalk = false;
            isAttack = true;
            GameObject trigger;

            if (!isFlip)
            {
                trigger = leftAtk.gameObject;
            }
            else
            {
                trigger = rightAtk.gameObject;
            }
            trigger.SetActive(true);
            ani.SetTrigger("Attack");
            yield return new WaitForSeconds(att.length);    //공격 애니메이션 종료시간에 맞춰서 다시 공격
            trigger.SetActive(false);

            isWalk = true;
            yield return new WaitForSeconds(2);
            isAttack = false;
        }
    }
    IEnumerator ChargeAttack()
    {
        if (!isChargeAttack && isWalk)
        {
            isWalk = false;
            isChargeAttack = true;          
            ani.SetTrigger("Charge");
            yield return new WaitForSeconds(chargeAtt.length-0.2f);
            isJump = true;
            if (isFlip)
            {
                rigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
                rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            }
            else
            {
                rigid.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
                rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(0.9f);
            isJump = false;
            ani.SetTrigger("ChargeAttack");
            yield return new WaitForSeconds(0.5f);
            isWalk = true;
            if(!isChase)
            StartCoroutine(Rotate());

            yield return new WaitForSeconds(10f);
            isChargeAttack = false;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall") //벽 충돌시 회전
        {
            StartCoroutine(Rotate());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))   //절벽트리거 충돌시 회전
            StartCoroutine(Rotate());
    }

    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);

            Destroy(bar);
            Destroy(gameObject);
        }
    }
}
