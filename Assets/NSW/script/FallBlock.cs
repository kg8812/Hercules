using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    public Vector2 oripos;
    public Transform downPos;
    float speed = 15.5f;
    bool isFall = false; // 코루틴 제어용 함수 
    bool isRealFall = false; // 실제 발판이 떨어지기 시작하는걸 표시함 
    // Start is called before the first frame update
    void Start()
    {
        oripos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRealFall)
        {
            this.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (!isFall)
            {
                StartCoroutine(Fall());
            }
        }
    }
   
    IEnumerator Fall()
    {
        isFall = true;
        this.transform.Translate(Vector2.left * 15.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.3f);
        this.transform.Translate(Vector2.right * 15.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.3f);
        this.transform.Translate(Vector2.left * 30.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.3f);
        this.transform.Translate(Vector2.right * 30.0f * Time.deltaTime);
        yield return new WaitForSeconds(1.2f);
        isRealFall = true;
        yield return new WaitForSeconds(3.5f);
        this.transform.position = oripos;
        isFall = false;
        isRealFall = false;
    }
}
