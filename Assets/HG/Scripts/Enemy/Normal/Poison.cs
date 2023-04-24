using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public GameObject explosion;    //폭발 프리팹
    public AnimationClip expAni;    //폭발 애니메이션
    GameObject target;
    Vector3 dir;
    public float dmg;

    void Start()
    {
        target = GameObject.Find("Player").transform.Find("Heracles").gameObject;
        if (target != null)
        {
            dir = target.transform.position - transform.position;
            dir = dir.normalized;
            float angle = Mathf.Atan2(dir.y+0.15f, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);          
        }
        dir = Vector2.right;

        Invoke("Explode", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir* 10 * Time.deltaTime);   //이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy")    //지형이나 플레이어 충돌시
        {
            Explode();
            IOnDamage iDmg = collision.gameObject.GetComponent<IOnDamage>();

            if(iDmg!= null)
            {
                iDmg.OnHit(dmg);
            }
        }
    }

    void Explode()
    {
        GameObject exp = Instantiate(explosion, transform.position, transform.rotation);    //폭발
        Destroy(exp, expAni.length);  //폭발 애니메이션 한번 실행 후 파괴
        Destroy(gameObject); //파괴
    }
}
