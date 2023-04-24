using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artemis : Enemy,IOnDamage
{
    Rigidbody2D body;
    Animator ani;
    bool isLeft = true;
    public GameObject arrow;
    public Transform firePos;
    bool isDead = false;
    Vector2 dir;
    GameObject player;
    bool isRush = false;
    bool inRush = false;
    bool isStart = false;
    public GameObject portal;
    public GameObject artemis;
    public GameObject artemis2;
    public void ToStart()
    {
        isStart = true;
    }
    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp < 0) Die();
    }
    void FireArrow()
    {
        if (isLeft)
        {
            Instantiate(arrow, firePos.transform.position, Quaternion.Euler(0, 180, 0));
        }
        else
        {
            Instantiate(arrow, firePos.transform.position, Quaternion.Euler(0, 0, 0));
        }

        if(Random.Range(0,1f)>0.5f)
        ani.SetBool("isAttack", false);
    }

    IEnumerator Rush()
    {
        if (!isRush)
        {
            inRush = true;
            isRush = true;
            if (isLeft)
            {
                dir = Vector2.right;
            }
            else
            {
                dir = Vector2.left;
            }
            ani.SetTrigger("Rush");
            body.AddForce(dir * Random.Range(20, 30), ForceMode2D.Impulse);
            ani.SetBool("isAttack", true);
            yield return new WaitForSeconds(0.5f);
            inRush = false;
            body.velocity = Vector2.zero;

            yield return new WaitForSeconds(Random.Range(2, 5));
            isRush = false;
        }
    }
    void Die()
    {
        isDead = true;
        portal.SetActive(true);
        ani.SetTrigger("Hit");

        Destroy(gameObject, 4f);
        artemis2.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ani = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || !isStart) return;

        StartCoroutine(Rush());
        Vector2 tDir = player.transform.position- transform.position;
        tDir.Normalize();
        float distance = Vector2.Distance(tDir, transform.position);

        if (distance > 3&&!inRush)
        {
            if (tDir.x < 0)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
        }
        
        if (transform.rotation.y == 0)
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            Rotate();
    }
    void Rotate()
    {
        transform.Rotate(0, 180, 0);
        
    }

    private void OnBecameVisible()
    {
        artemis.SetActive(true);
    }
}
