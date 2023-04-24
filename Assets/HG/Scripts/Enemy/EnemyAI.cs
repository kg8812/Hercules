using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Enemy
{
    #region 설명
    /* 적 이동,회전 ai입니다. 적용하려는 유닛에 상속 시켜주세요.
     * 기본적으로 이동하다 벽에 부딪히면 회전하고 플레이어가 추적범위내로가면 추적하도록 해뒀습니다.
     * State변수로 상태체크하게 해뒀습니다. idle(대기),move(이동),dead(죽음) 세개만 넣어뒀으니
     * 공격이나 점프등 다른 상태는 따로 구현하셔야합니다.
     * 상태 바꾸는것도 따로 넣으셔야합니다. 기본적으로 move로 설정되어있습니다.
     * 스프라이트 왼쪽 보고있는 유닛들은 인스펙터창에서 isLeft 체크해주세요.
     * rayLeft와 rayRight는 충돌감지박스 위치입니다. 
     * 왼쪽 오른쪽에 각각 하나씩 스프라이트 크기에 맞춰 gameobject 만들어서 인스펙터창에서 넣어주세요.
     * 그리고 inspector 창에서 박스크기 조정할 수 있으니까 이것도 스프라이트 크기 맞춰서 조정해주세요
     * (위치 지정하고 저장하면 크기바꿀때마다 그거에 맞춰서 박스 그려지는거 볼 수 있습니다.)
     * 여기 구현되어있는 함수 덮어 씌우시고 싶으시면 그냥 똑같은 이름 함수 스크립트에서 구현하시면됩니다.
     * 덮어 씌우는게 아니라 추가로 뭔가 넣고 싶으시면 같은 이름 함수 만드시고
     * 반환형 앞에 protected override 쓰신다음 함수내에 base.함수 넣으시면 둘다 사용됩니다.
     */
    #endregion

    float moveSpeed; //이동속도   

    GameObject target; // 타겟변수 (플레이어)
    Rigidbody2D rigid; // rigidbody 변수

    protected float distance;     //플레이어와의 거리
    Vector2 dir;    // 플레이어쪽 방향
    bool isChase;   //추적확인변수
    protected bool isFlip = false;    //스프라이트 회전 확인 변수
    SpriteRenderer sprite;  //스프라이트 렌더러 변수

    public bool isLeft; //스프라이트 왼쪽인지 확인
    public float detect = 20; // 플레이어 감지(추적) 거리
    protected EnemyState state = EnemyState.move;

    public Transform rayLeft; // 왼쪽 박스캐스트 위치
    public Transform rayRight; // 오른쪽 박스캐스트 위치
    public Vector2 boxArea; // 박스캐스트 크기

    public float flipDis = 1; // 회전제한 거리

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // 리지드바디 참조
        sprite = GetComponent<SpriteRenderer>(); // 스프라이트 참조
        target = GameObject.Find("Player").transform.Find("Heracles").gameObject; // 플레이어 찾기

        // 스프라이트의 좌우를 확인해 속도 설정
        if (isLeft)
            moveSpeed = -speed;
        else
            moveSpeed = speed;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isStop) // 정지
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        switch (state) // 적 상태 확인
        {
            case EnemyState.idle:   //대기
                moveSpeed = 0; // 속도 0
                break;
            case EnemyState.move:   //이동
                if (target != null) //플레이어를 찾았을 시
                {
                    dir = target.transform.position - transform.position;   //플레이어쪽으로 방향 설정
                    dir.Normalize();    //방향벡터 정규화
                    distance = Vector2.Distance(transform.position, target.transform.position); //플레이어와의 거리 측정                

                    if (distance < detect)  //감지범위내에 들어왔을시
                    {
                        isChase = true; //추적 true
                        if (distance > flipDis)
                        {
                            if (dir.x < 0)  // 플레이어가 왼쪽에 있을시 왼쪽으로 회전
                            {
                                if (!isLeft) // 오른쪽을 보는 스프라이트일시
                                {
                                    sprite.flipX = true; // 반대로 회전
                                    isFlip = true; // 회전됨 true
                                }
                                else // 왼쪽을 보는 스프라이트일시
                                {
                                    sprite.flipX = false; // 회전 안함
                                    isFlip = false; // 회전됨 false
                                }

                                moveSpeed = -speed;        //이동방향 왼쪽

                            }
                            else        //아니면 오른쪽으로 회전
                            {
                                if (!isLeft) // 오른쪽을 보는 스프라이트일시
                                {
                                    sprite.flipX = false; // 회전 안함
                                    isFlip = false; // 회전됨 false
                                }
                                else
                                {
                                    sprite.flipX = true; // 반대로 회전
                                    isFlip = true; // 회전됨 true
                                }
                                moveSpeed = speed; //이동방향 오른쪽
                            }
                        }

                    }
                    else isChase = false;   //사정거리밖에선 추적해제
                }
                else //플레이어가 없을시
                    isChase = false; // 추적해제;


                //이동속도 원상복구

                if (moveSpeed == 0)   // 정지 상태일시
                {
                    // 왼쪽 오른쪽을 확인하여 속도(이동방향) 설정
                    if (isLeft)
                    {
                        if (isFlip)
                            moveSpeed = speed;
                        else
                            moveSpeed = -speed;
                    }
                    else
                    {
                        if (isFlip)
                            moveSpeed = -speed;
                        else
                            moveSpeed = speed;
                    }
                }

                break;
            case EnemyState.dead: // 죽었을시
                rigid.velocity = Vector2.zero; // 이동 정지
                return; // 함수종료
        }
    }

    protected virtual void FixedUpdate()
    {
        if (isStop) return; // 정지

        if (state == EnemyState.dead) return; // 죽었을시 종료

        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y); // 이동
        WallCheck(); // 벽 체크

    }

    void Rotate() //회전
    {
        moveSpeed *= -1; //이동방향 반대로 설정

        //스프라이트 회전
        if (isFlip)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isFlip = false;
        }
        else
        {
            sprite.flipX = true;
            isFlip = true;
        }
    }
    void WallCheck() // 벽 체크
    {
        RaycastHit2D rayHit; // 레이캐스트 변수
        if (isLeft && isFlip || !isLeft && !isFlip) // 오른쪽을 보고있을시
        {
            // 오른쪽에 박스캐스트 생성
            rayHit = Physics2D.BoxCast(rayRight.position, boxArea, 0, Vector2.right, 1f, LayerMask.GetMask("Wall"));
        }
        else // 왼쪽을 보고있을시
        {
            // 왼쪽에 박스캐스트 생성
            rayHit = Physics2D.BoxCast(rayLeft.position, boxArea, 0, Vector2.left, 1f, LayerMask.GetMask("Wall"));
        }

        if (rayHit.collider != null) // 박스캐스트에 벽이 인식될시
        {
            if (distance < 2) Rotate(); // 플레이어와의 거리가 가까우면 회전
            else if (isChase) rigid.velocity = new Vector2(0, rigid.velocity.y); // 추적중엔 회전하지않고 멈춤
            else Rotate(); // 추적중이 아닐땐 회전
        }

    }

    private void OnDrawGizmos()
    {
        if (isLeft && isFlip || !isLeft && !isFlip)
        {
            Gizmos.DrawWireCube(rayRight.position, boxArea);
        }
        else
        {
            Gizmos.DrawWireCube(rayLeft.position, boxArea);
        }

    }

}
