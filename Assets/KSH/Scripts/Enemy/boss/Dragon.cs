using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon :Enemy,IOnDamage
{
    public AnimationClip att;
    public Animator ani;
    bool isAttack = false; //공격 코루틴 제어 변수  
    public GameObject bossPortal;
    public GameObject fire;
    public Transform firePos;
    
    void Start()
    {

        Invoke("ToAttack", 1.5f);
        bossPortal.SetActive(false);
    }
    
   
    void ToAttack()
    {
        StartCoroutine(Attack());
        Invoke("ToAttack", Random.Range(3, 5));
    }

    

   
    IEnumerator Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            ani.SetTrigger("Attack");
            yield return new WaitForSeconds(0.9f);
            Instantiate(fire, firePos.transform.position, Quaternion.identity);

            
            isAttack = false;
        }
    }

   
    public void OnHit(float damage)
    {

         hp -= damage;

        if (hp <= 0)
        {            
            Destroy(bar.gameObject);
            Destroy(this.gameObject);
            bossPortal.SetActive(true);
        }



    }
}
