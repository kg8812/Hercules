using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenStep : MonoBehaviour
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
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(Appear());
            }
        }
    }

    IEnumerator Appear()
    {
        ani.SetTrigger("disappear");
        yield return new WaitForSeconds(dis.length);
        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(5);
        GetComponent<BoxCollider2D>().isTrigger = false;
        ani.SetTrigger("appear");
    }
}
