using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    Player player;
    float nSpeed;
    public GameObject wing;
    public static float fallSpeed = -4f;
    private void Awake()
    {
        wing = GameObject.FindWithTag("Player").transform.Find("날개").gameObject;

    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        nSpeed = PlayerPrefs.GetFloat("PlayerSpeed");

    }

    // Update is called once per frame
    void Update()
    {
        nSpeed = PlayerPrefs.GetFloat("PlayerSpeed");       
        if (!player.isGround && !player.isLand)
        {          
            if (Input.GetKey(KeyCode.Z))
            {
                if (player.body.velocity.y<fallSpeed)
                {
                    player.body.velocity = new Vector2(player.body.velocity.x, fallSpeed);                   
                }

                player.speed = nSpeed * 1.5f;

                if (player.curWeapon != null)
                {
                    player.curWeapon.SetActive(false);
                }
                wing.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                player.speed = nSpeed;
                if (player.curWeapon != null)
                {
                    player.curWeapon.SetActive(true);
                }
            }
        }
        else
            wing.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            player.speed = nSpeed;

        }




    }
}
