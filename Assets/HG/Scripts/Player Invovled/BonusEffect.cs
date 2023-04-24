using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEffect : MonoBehaviour
{
    Player player;
    float dmg;
    public float dmgRate = 0.3f;
    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (BonusAttack.isEnhanced)
        {
            dmgRate = 0.45f;
        }

            dmg = player.atk * dmgRate;    

    }
    void Update()
    {
        transform.Translate(Vector3.right * 15 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BonusAttack.isEnhanced)
        {
            dmgRate = 0.45f;
        }

        dmg = player.atk * dmgRate;
        if (collision.tag == "Enemy")
        {
            IOnDamage onDmg = collision.GetComponent<IOnDamage>();
            if (onDmg != null)
            {                            
                onDmg.OnHit(dmg);
            }
        }
    }
}
