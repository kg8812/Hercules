using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    Animator ani;
    public AnimationClip dis;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {           
            if (Input.GetKeyDown(KeyCode.X))
                StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        ani.SetTrigger("disappear");
        yield return new WaitForSeconds(dis.length);
        gameObject.SetActive(false);
    }
}
