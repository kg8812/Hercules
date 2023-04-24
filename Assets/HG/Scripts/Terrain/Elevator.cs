using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float velocity = 10.0f; // 엘리베이터 이동속도
    List<GameObject> collide = new List<GameObject>(); // 접촉중인 오브젝트 목록
    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * velocity * Time.deltaTime); // 이동
        for (int i = 0; i < collide.Count; i++) // 접촉중인 오브젝트 속도 변경
        {
            // 접촉 오브젝트 속도 가져오기
            float velY = collide[i].GetComponent<Rigidbody2D>().velocity.y;

            if (velY > -5f && velY < 5f) // 접촉 오브젝트 속도 엘리베이터와 같게 만들기
                collide[i].transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "object") // 설정된 오브젝트에 부딪힐시
        {         
            StartCoroutine(MoveStop()); // 멈추기
        }
    }
       
    IEnumerator MoveStop() // 멈추기
    {
        float speed = velocity; // 속도 저장
        velocity = 0; // 엘리베이터 정지
        yield return new WaitForSeconds(3); // 3초동안 대기
        velocity = speed * -1; // 이동방향 반대로 적용
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Projectile") // 충돌체가 투사체가 아닐시
        collide.Add(collision.gameObject); // 접촉중인 오브젝트 목록에 추가

    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        // 떨어질때 접촉목록에서 제거
        collide.Remove(collision.gameObject);       
    }
}
