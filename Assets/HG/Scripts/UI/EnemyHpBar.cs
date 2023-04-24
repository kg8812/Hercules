using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            Debug.Log(enemy.hp);

            if (enemy.hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }

    public void EnemySet(Enemy enemy)
    {
        this.enemy = enemy;
    }

}
