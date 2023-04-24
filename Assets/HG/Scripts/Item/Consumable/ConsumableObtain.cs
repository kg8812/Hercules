using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObtain : MonoBehaviour
{
    public ConsumableInfo item;
    Rigidbody2D body;

    private void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        body.AddForce(Vector2.up * 1600);
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Consumable.instance.Add(item);
            Destroy(gameObject);

        }
    }
}
