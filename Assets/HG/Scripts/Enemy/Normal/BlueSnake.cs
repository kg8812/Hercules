using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSnake : Enemy,IOnDamage
{
    public GameObject poison;   //독 프리팹
    public EnemyState state;
    float distance;
    Animator ani;
    GameObject target;
    public Transform mouth;
    public AnimationClip att;
    bool isAttack = false;
    Vector3 dir;
    EnemyAttack attack;
    GameObject blood;
    bool inAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        attack = transform.Find("공격").GetComponent<EnemyAttack>();
        blood = EffectManager.instance.bloodEffect;

        target = GameObject.Find("Player").transform.Find("Heracles").gameObject;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop) return;

        if (target != null) //플레이어를 찾았으면
        {
            distance = Vector2.Distance(transform.position, target.transform.position); //플레이어와의 거리 측정
            dir = target.transform.position - transform.position;
            dir.Normalize();

            if (distance < 30)
            {
                if (distance < 5)  //가까이오면 근접공격
                {
                    StartCoroutine("MeleeAttack");
                }
                else if (distance < 20) //멀어지면 독 발사
                {
                    StartCoroutine("PoisonAttack");
                }
                if(!inAttack)
                {
                if (dir.x < 0)  //플레이어가 왼쪽에 있을시
                {
                    transform.rotation = Quaternion.Euler(new Vector2(0, 180)); //왼쪽으로 회전
                    dir *= -1;
                }
                else transform.rotation = Quaternion.Euler(Vector2.zero);   //아니면 오른쪽으로 회전
                }
            }
        }
    }
    
    IEnumerator PoisonAttack()
    {
        if (!isAttack)
        {
            isAttack= true;
            inAttack=true;
            ani.SetTrigger("RangeAttack");
            yield return new WaitForSeconds(att.length-0.3f);
            inAttack=false;
            GameObject p = Instantiate(poison,mouth.position,transform.rotation);
            p.GetComponent<Poison>().dmg = atk * 1.2f;

            yield return new WaitForSeconds(2);
            isAttack = false;
        }
    }

    IEnumerator MeleeAttack()
    {
        if (!isAttack)
        {           
            isAttack = true;
            attack.dmg = atk;
            ani.SetTrigger("MeleeAttack");
            yield return new WaitForSeconds(1.5f);
            isAttack = false;
        }
    }

    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);

            Destroy(bar);
            Destroy(gameObject);
        }
    }
}
