using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPortion : MonoBehaviour
{
  
    Rigidbody2D body;
    private void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        body.AddForce(Vector2.up * 1600);
        Destroy(this.gameObject, 5f);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.GetComponent<Player>().currHp += collision.GetComponent<Player>().maxHp / 10.0f;
            
        }
    }
    
}
