using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy , IOnDamage
{
    EnemyState state = EnemyState.idle;
    bool isRotate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.move;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.move)
        {


            if (!isRotate)
            {
                StartCoroutine(RotateBat());
                transform.Translate(Vector2.left * speed * Time.deltaTime, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime, 0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
        IEnumerator RotateBat()
        {
            yield return new WaitForSeconds(2.5f);
            isRotate = true;

            yield return new WaitForSeconds(2.5f);


            isRotate = false;
        }
    }
    public void OnHit(float damage)
    {
        if (hp > 0)
            hp -= damage;

        if (hp <= 0)
        {
            state = EnemyState.dead;
            Destroy(bar.gameObject);
            Destroy(this.gameObject);
        }
    }
}
