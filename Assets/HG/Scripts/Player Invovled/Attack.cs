using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EffectDmgText
{
    Player player;
    protected override void Start()
    {
       base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.weapon == 2)
        {
            transform.Rotate(0, 0, -90);
            Destroy(gameObject, 1.5f);
        }
        else
        {
            Destroy(gameObject, 0.2f);
        }
        
    }
    private void Update()
    {
        if (player.weapon == 2)
        {
            transform.Translate(Vector2.up * 15*Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            IOnDamage onDmg = collision.GetComponent<IOnDamage>();
            if (onDmg != null)
            {
                float dmg = player.dmg;
                if (player.weapon == 2) dmg /= 2;
                float crit = Random.Range(0f, 1f);
                float def = collision.GetComponent<Enemy>().def;

                if (PlayerPrefs.GetInt("God") == 2 && collision.GetComponent<Enemy>().isDebuff == false)
                {
                    collision.GetComponent<Enemy>().isDebuff = true;
                    if (PlayerPrefs.GetInt("PlayerWeapon") == 1)
                    {
                        collision.GetComponent<Enemy>().speed /= 2;

                    }
                    else if (PlayerPrefs.GetInt("PlayerWeapon") == 2)
                    {
                        collision.gameObject.AddComponent<Curse>();
                    }

                }
                dmg = dmg / (1 + def / 100.0f);

                if (PlayerPrefs.GetFloat("Iokheira") != 0)
                {
                    dmg = (dmg * 0.7f) + ((dmg * (1 + def / 100.0f)) * 0.3f);
                }

                if (crit < player.critProb)
                {
                    dmg *= player.critDmg;
                }
              
                float dura = PlayerPrefs.GetFloat("Weapon_Durability"); //현재 무기 내구도 가져오기
                if (dura <= 10) //무기내구도가 10이하면 플레이어가 주는 데미지가 절반이됨 
                {
                    dmg *= 5 / 10.0f;
                }
                else if (dura <= 30) //무기내구도가 30이하면 플레이어가 주는 데미지가 70%가됨
                {
                    dmg *= 7 / 10.0f;
                }
                else if (dura <= 50) //무기내구도가 50이하면 플레이어가 주는 데미지가 90%가됨
                {
                    dmg *= 9 / 10.0f;
                }
                if (Random.Range(1, 100) >= 20) //플레이어 공격적중시 일정확률로 내구도 감소  
                {
                    dura--;
                    player.weapon_Durability--;
                    PlayerPrefs.SetFloat("Weapon_Durability", dura);
                }

                if (PlayerPrefs.GetInt("HydraPoison") != 0)
                {
                    if (collision.GetComponent<PoisonDebuff>() == null)
                    {
                        collision.gameObject.AddComponent<PoisonDebuff>();
                    }
                    else
                    {
                        collision.GetComponent<PoisonDebuff>().time = 0;
                    }
                }
               
                onDmg.OnHit(dmg);
                
                Create(dmg, collision.ClosestPoint(transform.position), Color.white);

                if (player.weapon == 2)
                {
                    Destroy(gameObject);
                }

            }



        }
    }
}
