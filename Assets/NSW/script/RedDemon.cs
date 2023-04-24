using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDemon : Enemy
{
    public bool isLeftMove = false;
    Transform player;
    float attackRange = 1.2f;
    Animator _ani;
    public float distance;
    EnemyState state = EnemyState.move;
    private void Awake()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").gameObject.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        _ani =this.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop) return;

        distance = Vector3.Distance(player.position, this.transform.position);
        if(state == EnemyState.move)
        {
            if (!isLeftMove)
                StartCoroutine(RedDemonMove());
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (distance <= attackRange)
            {
                state = EnemyState.attack;
                _ani.SetBool("IsMove", false);
            }
        }
        else if(state == EnemyState.attack)
        {
            if(Random.Range(0,100) <= 50)
            {
                _ani.SetTrigger("Attack1");
            }
            else
            {
                _ani.SetTrigger("Attack2");
            }
            if (distance >= attackRange)
            {
                state = EnemyState.move;
                _ani.SetBool("IsMove", true);
            }
        }
        
       

    }

    IEnumerator RedDemonMove()
    {
        yield return new WaitForSeconds(2);
        isLeftMove = true;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        yield return new WaitForSeconds(2);
        isLeftMove = false;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
      

        
    }
}
