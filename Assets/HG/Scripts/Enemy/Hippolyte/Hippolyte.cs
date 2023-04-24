
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hippolyte : Enemy,IOnDamage
{
   enum State   //비행상태 체크
    {
        Flying=0,
        Ground,
        Die
    }

    Rigidbody2D body;
    State state;   
    public bool isGround;   // 땅 체크
    bool fly = true;   //비행 코루틴 제어 함수
    bool isFlyAttack = false;  //공중공격체크
    bool isGroundAttack = false;    //지상공격체크
    bool isImmune = false;  //무적체크
    bool isRage = false;    //광폭화
    bool isRageAttack = false;  //광폭화돌진공격
    bool isRageSting = false; //찌르는중
    bool isDead = false;

    Animator ani;   // 애니메이터
    public AnimationClip att;   //공중공격 애니메이션클립
    public AnimationClip att2; //지상공격 애니메이션클립
    public AnimationClip dieClip;
    public EnemyAttack dmg;
    Vector2 dir;    // 이동방향
    float moveSpeed;    //이동속도
    
    Transform target;   // 타겟(플레이어)

    float FlyAttackCd = 0; //공중공격 쿨타임 3~5초
    float fCd1 = 3, fCd2 = 5;
    float afterFlyAttack = 0;  //공중공격시간체크(공격후)
    float immuneCd = 0; //무적 쿨타임
    float iCd1 = 7, iCd2 = 10;
    float afterImmune;  //무적후 시간;
    float gCd = 2;  //지상공격 쿨타임
    float rCd = 15; //광폭화공격 쿨타임
    float afterRageAttack = 0;

    int hipLayer;
    int atkLayer;
    
    public float hpPer; // 체력 퍼센트

    public GameObject conv;

    // Start is called before the first frame update
    void Start()
    {
        hipLayer = LayerMask.NameToLayer("Hippolyta");
        atkLayer = LayerMask.NameToLayer("Effect");
        ani = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        FlyAttackCd = Random.Range(3f, 5f);
        moveSpeed = speed;
        target = GameObject.Find("Player").transform.Find("Heracles").transform;

        immuneCd = Random.Range(10f, 20f);
    }

    private void OnEnable()
    {
        fly = true;
        state = State.Flying;
    }

    // Update is called once per frame
    void Update()
    {
        hpPer = hp / maxHp;
        
        switch (state)
        {
            case State.Flying:
                if (!isFlyAttack && transform.position.y >= 6) //공격중이 아니면 쿨타임 진행
                    afterFlyAttack += Time.deltaTime;  

                if (afterFlyAttack >= FlyAttackCd) //쿨타임 준비시 공격
                    StartCoroutine(FlyAttack());

                if (!isFlyAttack&&transform.position.y < 6)    //6미터이하면 위로 비행
                {
                    transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                    ani.SetBool("IsFly", true);
                    ani.SetBool("IsWalk", false);
                }
                         
                
                break;
            case State.Ground:
                float distance = Vector2.Distance(target.transform.position, transform.position);

                if (!isImmune)
                {
                    afterImmune += Time.deltaTime;
                }
                if (afterImmune > immuneCd && !isGroundAttack && distance < 12&&!isRageAttack)
                {
                    StartCoroutine(Immune());
                }

                if (distance < 10 && !isImmune&&!isRageAttack)
                {
                    StartCoroutine(GroundAttack1());
                }

                if (!isGround&&!isImmune)  
                {
                    transform.Translate(Vector2.down * 9.8f * Time.deltaTime);  //중력적용 *rigidbody로 구현할 시 바닥에서 통통튀는현상 생김
                }
                else   //땅에 닿으면 걷는 모션 적용
                {
                    ani.SetBool("IsFly", false); 
                    ani.SetBool("IsWalk", true);
                }

                if (isRage)
                {
                    if (!isRageAttack)
                        afterRageAttack += Time.deltaTime;

                    if (afterRageAttack > rCd && !isGroundAttack && !isImmune)
                    {
                        StartCoroutine(RageAttack());
                    }
                }

                break;
            case State.Die:
                isDead = true;
                return;
        }
        isDead = false;
        if (!isFlyAttack && !isImmune)
            dir = Vector2.left; //공격중이 아니면 기본 이동설정
        else if (isImmune)
        {
            dir = target.transform.position - transform.position;
            dir.Normalize();
            if (dir.x < 0)
                transform.rotation = Quaternion.Euler(new Vector2(0, 0));
            else
            {
                transform.rotation = Quaternion.Euler(new Vector2(0, 180));
                dir.x *= -1;
            }
        }

        if (hpPer < 0.3f&&!isRage)
        {
            Rage();
        }
        
        StartCoroutine(Fly());  //비행함수호출
    }

    private void FixedUpdate()
    {      
        if ((!isFlyAttack||!isGround)&&!isDead)
        transform.Translate(dir * moveSpeed * Time.deltaTime);  //이동        
    }

    IEnumerator Die()
    {
        ani.SetTrigger("Die");
        state = State.Die;
        yield return new WaitForSeconds(dieClip.length);
        conv.SetActive(true);
        Destroy(gameObject);
    }
    void Rotate()
    {      
            transform.Rotate(new Vector2(0, 180));  //회전
    }
    IEnumerator Fly()
    {
        if (fly&&!isFlyAttack)
        {
            fly = false;
            yield return new WaitForSeconds(30);    //비행 지속시간

            state = State.Ground;
            yield return new WaitForSeconds(30);    //비행 쿨타임

            if(!isRage)
            state = State.Flying;
            fly = true;
        }
    }

    IEnumerator FlyAttack()
    {
        FlyAttackCd = Random.Range(fCd1, fCd2); //공중공격 쿨타임 3~5초
        dmg.dmg = atk * 1.5f;
        afterFlyAttack = 0;    //공격후 시간 초기화
        dir = target.transform.position - transform.position;   // 플레이어쪽으로 방향 설정
        dir.Normalize();
        if (dir.x < 0)  //회전 조건문
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        else
        {
            transform.rotation = Quaternion.Euler(new Vector2(0, 180));            
        }
        isFlyAttack = true;
        ani.SetTrigger("Attack1");  //공격실행
        moveSpeed = 0;  //차지중에 움직임 X
        yield return new WaitForSeconds(0.8f);  //차지시간
        dir = target.transform.position - transform.position;   //차지후 방향 다시 설정
        dir = new Vector2(dir.x, -10);
        dir.Normalize();
        if (dir.x < 0)  //차지 후 회전 다시 설정
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
        else
        {
            transform.rotation = Quaternion.Euler(new Vector2(0, 180));
            dir.x *= -1;
        }
        
        moveSpeed = speed*2.5f;   //거리에 비례해 돌격속도 증가
        yield return new WaitForSeconds(att.length-0.8f);   // 공격 종료
        Rotate();   //종료후 회전 (벽에끼임 방지)
        moveSpeed = speed;  //이동속도 원상복구
        isFlyAttack = false;  
    }

    IEnumerator GroundAttack1()
    {
        if (!isGroundAttack)
        {
            Vector2 pDir = target.transform.position - transform.position;
            dmg.dmg = atk;
            isGroundAttack = true;
            ani.SetTrigger("Attack2");
            moveSpeed = 0;
            if (pDir.x < 0) 
                transform.rotation = Quaternion.Euler(new Vector2(0, 0));
            else
            {
                transform.rotation = Quaternion.Euler(new Vector2(0, 180));
                dir.x *= -1;
            }
            yield return new WaitForSeconds(0.65f);  //차지시간

            if(transform.rotation == Quaternion.Euler(new Vector2(0, 0)))           
                transform.position += Vector3.left * 5; //짧게 돌격
            else
                transform.position += Vector3.right * 5; //짧게 돌격

            yield return new WaitForSeconds(att2.length-1f);   
            moveSpeed = speed;
            yield return new WaitForSeconds(gCd);
            isGroundAttack = false;
        }

    }

    IEnumerator RageAttack()
    {
        if (!isRageAttack)
        {
            dmg.dmg = atk * 2;
            afterRageAttack = 0;
            isRageAttack = true;
            moveSpeed = 50;          
            yield return new WaitForSeconds(1f);
            ani.SetTrigger("RageAttack");
            yield return new WaitForSeconds(1f);
            moveSpeed = 80;
            isRageSting = true;
            yield return new WaitForSeconds(1.5f);
            moveSpeed = speed;
            Rotate();
            isRageAttack = false;
            isRageSting = false;
        }
    }
    IEnumerator Immune()    //무적
    {
        if (!isImmune)
        {
            Physics2D.IgnoreLayerCollision(atkLayer, hipLayer, true);
            immuneCd = Random.Range(iCd1, iCd2);
            afterImmune = 0;
            isImmune = true;
            ani.SetTrigger("Immune");
            ani.SetBool("IsImmune", true);
            
            yield return new WaitForSeconds(2f);           
            ani.SetBool("IsImmune",false);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreLayerCollision(atkLayer, hipLayer, false);
            isImmune = false;
        }
    }
    void Rage()
    {
        isRage = true;
        speed *= 1.3f;
        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f);
        fCd1 *= 0.7f;
        fCd2 *= 0.7f;
        iCd1 *= 0.7f;
        iCd2 *= 0.7f;
        gCd *= 0.7f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "MainCamera") //벽에 부딪힐시 회전
        {
            if (isRageAttack) moveSpeed = 0;

            if(!isRageSting)
            Rotate();
        }
        if (collision.gameObject.tag=="Ground")
        {
            isGround = true;
        }

    }
  
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

    public void OnHit(float damage)
    {
       
        hp -= damage;
       
        if(hp<=0)
        {
            StartCoroutine(Die());
        }

    }
}
