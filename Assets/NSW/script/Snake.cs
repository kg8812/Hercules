using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy, IOnDamage
{
    public Transform player;
    float distance;
    bool isRotate = false;
    Animator _ani;
    EnemyState state = EnemyState.idle;
    
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.move;
        _ani = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop) return;


        distance = Vector2.Distance(player.position ,this.transform.position);
        float dirx = this.transform.position.x - player.position.x; // 플레이어의 왼쪽에있나 오른쪽에있나 판단
        if (state == EnemyState.move)
        {
           
             
            if (!isRotate)
            {
                StartCoroutine(RotateSnake()); //이동중엔 일정시간마다 방향 전환
                transform.Translate(Vector2.left * speed * Time.deltaTime, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
                
            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            if (distance< 2) // 플레이어와의 거리가 가까우면 공격 
            {
                if (dirx <= 0) //좌우 방향전환 
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
               
                state = EnemyState.attack;
                _ani.SetBool("isAttack", true);
            }
        }
        else if(state == EnemyState.attack)
        {
          
              
            
        }
        
        
       
    }
    public void OnHit(float damage) // 피격판정
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {
            state = EnemyState.dead;
            Destroy(bar.gameObject);
            Destroy(this.gameObject);
        }
    }
    void MoveState()
    {
        state = EnemyState.move;
        _ani.SetBool("isAttack", false);
    }
    IEnumerator RotateSnake()
    {
        yield return new WaitForSeconds(2.5f);
        isRotate = true;
        
        yield return new WaitForSeconds(2.5f);
      
       
        isRotate = false;
    }
}
