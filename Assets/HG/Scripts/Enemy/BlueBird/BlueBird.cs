using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BlueBird : Enemy, IOnDamage
{

    Animator ani;
    public GameObject blade;
    public GameObject player;
    public EnemyState state = EnemyState.idle;
    bool isAttack = false;
    Rigidbody2D body;
    bool isFlyingAttack = false;
    public Flowchart flowchart;
    public GameObject portal;
    bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void OnStart()
    {
        isStart = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.dead || !isStart) return;
        Vector3 dir;
        dir = player.transform.position - transform.position;   //차지후 방향 다시 설정
        dir.Normalize();
        if (dir.x > 0)  //플레이어가 왼쪽에 있을시
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
           
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        if (state == EnemyState.idle)
        {
            if ( transform.position.y < 6)    //6미터이하면 위로 비행
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
             
            }
            else if(transform.position.y > 6)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (!isAttack)
            StartCoroutine("RotationAttack");
           
        }
        else if (state == EnemyState.move)
        {
          if(!isAttack)
            {
                StartCoroutine(MoveAttack());
            }
          else
            {
                if(!isFlyingAttack)
                this.transform.Translate(dir * speed * Time.deltaTime);
                else
                {
                    body.AddForce(speed *Vector2.down, ForceMode2D.Impulse);
                }
               
            }
           
        }
      

    }

    IEnumerator RotationAttack()
    {
        isAttack = true;
       
        yield return new WaitForSeconds(0.1f);
        ani.SetTrigger("RotationAttack");
       GameObject bl = Instantiate(blade, transform.position + new Vector3(-2, 0, 0), transform.rotation);
        yield return new WaitForSeconds(1.0f);
        ani.SetTrigger("RotationAttack");
       GameObject b2 = Instantiate(blade, transform.position + new Vector3(-2, 0, 0), transform.rotation);
        yield return new WaitForSeconds(1.0f);
        ani.SetTrigger("RotationAttack");
       GameObject b3 = Instantiate(blade, transform.position + new Vector3(-2, 0, 0), transform.rotation);
        yield return new WaitForSeconds(3.0f);
        isAttack = false;
        state = EnemyState.move;
    }
    IEnumerator MoveAttack()
    {
        isAttack = true;
        
      

        isFlyingAttack = false;
        yield return new WaitForSeconds(3.0f);  //차지시간
        ani.SetBool("isAttack", true);
        Vector3 dir;
        dir = player.transform.position - transform.position;   //차지후 방향 다시 설정
        dir.Normalize();
       
        isFlyingAttack = true;
        

        yield return new WaitForSeconds(1f);   // 공격 종료
        ani.SetBool("isAttack", false);
        isAttack = false;
        state = EnemyState.idle;
    }
    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0 && state != EnemyState.dead )
        {
            state = EnemyState.dead;
            Destroy(bar.gameObject);
            Destroy(gameObject);
            if (BirdCnt.instance.cnt == 1) BirdCnt.instance.cnt--;
            else
            {
                flowchart.ExecuteBlock("2-3격파");
                portal.SetActive(true);
            }

              
        }
    }

   
}
