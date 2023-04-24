using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadonFire : MonoBehaviour
{
    Animator ani;
    Vector3 dir;
    GameObject player;
    float speed = 10;
    private void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        ani = GetComponent<Animator>();
        dir = player.transform.position - transform.position;
        dir.Normalize();
        
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        transform.position+=dir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speed = 0;
            player.GetComponent<IOnDamage>().OnHit(20);
            ani.SetTrigger("Explode");
        }
    }
}
