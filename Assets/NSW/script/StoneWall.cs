using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWall : MonoBehaviour
{
    
    bool isBlink = false; //코루틴 제어용 함수 
    Vector2 oripos;
    public bool isReverse; // 체크되어있다면 등장하고 사라지는 타이밍이 반대로 된다.
    // Start is called before the first frame update
    void Start()
    {
        oripos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBlink) 
        {
            // 사라졌다 나왔다를 반복 
            if (!isReverse) 
            StartCoroutine(BlinkWall()); 
            else
            StartCoroutine(ReverseBlinkWall());
        }
    }

    IEnumerator BlinkWall()
    {
        isBlink = true;
        
        yield return new WaitForSeconds(2.3f);
        this.transform.position = new Vector2(-900, -900);
        yield return new WaitForSeconds(2.0f);
        isBlink = false;
        this.transform.position = oripos;

    }
    IEnumerator ReverseBlinkWall()
    {
        isBlink = true;
       
        yield return new WaitForSeconds(2.0f);
        this.transform.position = oripos;
      
        yield return new WaitForSeconds(2.3f);
        this.transform.position = new Vector2(-900, -900);
        isBlink = false;




    }
}
