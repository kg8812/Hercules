using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Enemy, IOnDamage
{
    public Transform player;
    bool isGround;
    Animator _ani;
    Rigidbody2D body;
    public GameObject banana;
    float distance;
    bool isAttack;
   

    // Start is called before the first frame update
    void Start()
    {
       
        if (player == null) // 플레이어 찾기 
        {
            player = GameObject.Find("Player").transform.Find("Heracles").gameObject.transform;
        }
        _ani = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;

        float dirx = this.transform.position.x - player.position.x; // 플레이어의 왼쪽에있나 오른쪽에있나 판단
        distance = Mathf.Abs(dirx);
       

        if(distance <= 15) // 플레이어와의 거리가 15 이하일때 공격
        {
            if(!isAttack)
            StartCoroutine(MonkeyAttack());
            
            
        }
        if (dirx <= 0) // 좌우 방향전환
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }


        float move = 15;
        if(Input.GetKeyUp(KeyCode.Z) && isGround == true) // 플레이어가 점프키를 누르면 같이 점프 
        {
            isGround = false;
            _ani.SetBool("isJump", true);
            body.AddForce(Vector2.up * move, ForceMode2D.Impulse);
        }
                                       

    }
    private void LateUpdate()
    {
        if (body.velocity.y > 20) // 점프 velocity제한
        {
            body.velocity = new Vector2(body.velocity.x, 20);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGround = true;
            _ani.SetBool("isJump", false);
        }

    }
    public void OnHit(float damage) // 피격 판정
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {
          
            Destroy(bar.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
    IEnumerator MonkeyAttack() // 공격 코루틴
    {
        isAttack = true;
        Instantiate(banana, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }
}
