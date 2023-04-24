using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class Babarian : Enemy, IOnDamage
{
    Animator ani;
    Transform player;
    EnemyState state = EnemyState.idle;
    bool isAttack = false;
    public GameObject Monkey;
    public GameObject Flying;
    float roat = 0;
    bool isRight = true;
    public GameObject portal;
    bool isDead = false;
    public AnimationClip deathClip;
    public Flowchart flowchart;
    bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").gameObject.transform;
        ani = GetComponent<Animator>();
        portal.SetActive(false);
    }
    void OnStart()
    {
        isStart = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.dead || !isStart) return; // 죽은 상태거나 스테이지 시작전이면 업데이트 실행을 막음
        Vector3 dir;
        dir = player.transform.position - transform.position;   //차지후 방향 다시 설정
        dir.Normalize();
        if (roat > 1.5f) // 1.5초가 지났다면 방향 회전 
        {
            if (dir.x > 0)  //플레이어가 왼쪽에 있을시
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                isRight = true;

            }
            else
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                isRight = false;

            }
            roat = 0;
        }
        else
        {
            roat += Time.deltaTime; //1.5초가 지나지 않았다면 시간이 증가
        }

        if (isRight)
        {
            this.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (state == EnemyState.idle)
        {
            if (!isAttack)
                StartCoroutine(Spawn()); // 적 몬스터 증원 패턴
        }
        else if (state == EnemyState.move)
        {

            if (!isAttack)
                StartCoroutine(Run()); // 달리기 공격 패턴
        }
    }
    IEnumerator Spawn()
    {
        isAttack = true;
        yield return new WaitForSeconds(1.0f);
        GameObject bl = Instantiate(Monkey, transform.position + new Vector3(-2, 15, 0), transform.rotation);
        yield return new WaitForSeconds(1.0f);
        GameObject b2 = Instantiate(Monkey, transform.position + new Vector3(-2, 15, 0), transform.rotation);

        yield return new WaitForSeconds(3.0f);
        GameObject b3;
        if (hp <= maxHp / 2) // 체력이 절반이하면 날다람쥐도 소환 
            b3 = Instantiate(Flying, transform.position + new Vector3(-2, 0, 0), transform.rotation);
        isAttack = false;
        state = EnemyState.move;
    }
    IEnumerator Run()
    {
        isAttack = true;
        speed = 8;
        ani.SetBool("isRun", true);

        yield return new WaitForSeconds(5);
        state = EnemyState.idle;
        speed = 3;
        ani.SetBool("isRun", false);
        yield return new WaitForSeconds(3);
        isAttack = false;
    }
    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {
            flowchart.ExecuteBlock("2-2클리어");
            state = EnemyState.dead;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (!isDead)
        {
            isDead = true;
            ani.SetTrigger("Dead");
            yield return new WaitForSeconds(deathClip.length);
            Destroy(bar.gameObject);
            Destroy(gameObject);
            portal.SetActive(true);
        }
    }
    public void DeadTranslate()
    {
        this.transform.position = new Vector2(this.transform.position.x, 5.08f); // 사망 애니메이션 위치 마추기
    }
}
