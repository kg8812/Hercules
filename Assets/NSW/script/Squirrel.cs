using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy, IOnDamage
{
    public Transform player;
    public GameObject banana;
    float distance;
    bool isAttack;
    float oriPosy;
    bool isUp=true;
    // Start is called before the first frame update
    void Start()  
    {
        if (player == null) // 플레이어 찾기
        {
            player = GameObject.Find("Player").transform.Find("Heracles").gameObject.transform;
        }
        oriPosy = this.transform.position.y; // 원래 위치 
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;

        float dirx = this.transform.position.x - player.position.x;  // 플레이어의 왼쪽에있나 오른쪽에있나 판단
        distance = Mathf.Abs(dirx);
        if(isUp)
        {
            this.transform.Translate(Vector2.up * speed * Time.deltaTime); // 위로이동
            if (this.transform.position.y - oriPosy > 5) // 일정 높이 이상이면 아래로 이동하도록 변경
            {
                isUp = false;
            }
        }
        else
        {
            this.transform.Translate(Vector2.down * speed * Time.deltaTime); // 아래로 이동
            if (this.transform.position.y - oriPosy < -5)// 일정 높이 이하면 위로 이동하도록 변경 
            {
                isUp = true;
            }
        }

       
       
        if (distance <= 15) // 플레이어와 거리가 15이하면 공격
        {
            if (!isAttack)
                StartCoroutine(SquirrelAttack());
            

        }
        if (dirx <= 0) // 좌우 방향 회전 
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        
    }
    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {

            Destroy(bar.gameObject);
            Destroy(this.gameObject);
        }
    }
    IEnumerator SquirrelAttack()
    {
        isAttack = true;
        Instantiate(banana, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }
}
