using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Enemy
{
   public bool isRight = false;
    Animator _ani;

    // Start is called before the first frame update
    void Start()
    {
        _ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRight)
        transform.Translate(Vector3.left*speed*Time.deltaTime,0);
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, 0);
        }
       if (!isRight)
        {
            StartCoroutine(RotateRhino());
        }

    }
    
    IEnumerator RotateRhino()
    {
        yield return new WaitForSeconds(2);
        isRight = true;
        _ani.SetBool("isRight", true);
        yield return new WaitForSeconds(2);
        isRight = false;
        _ani.SetBool("isRight", false);
    }
}
