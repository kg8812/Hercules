using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ares : MonoBehaviour
{
    GameObject stingEffect;
    GameObject rushEffect;
    GameObject arrowEffect;
    GameObject bowEffect;
    GameObject tornadoEffect;
    GameObject tornado;
    Player player;
    GameObject rush;

    float cd1;
    float cd2;
    float cd3;

    float duration = 4;
    float atkTime = 0;
    public bool isCd = false;
    public static bool cdWork = false;

    public Sprite[] skillImages;


    // Start is called before the first frame update
    void Start()
    {
        cd1 = SkillManager.instance.aresSkill[0].cooldown;
        cd2 = SkillManager.instance.aresSkill[1].cooldown;
        cd3 = SkillManager.instance.aresSkill[2].cooldown;
        cdWork=true;
        stingEffect = EffectManager.instance.stingEffect;
        rushEffect = EffectManager.instance.stingRushEffect;
        arrowEffect = EffectManager.instance.FireArrowEffect;
        bowEffect = EffectManager.instance.FireBowEffect;
        tornadoEffect = EffectManager.instance.tornadoEffect;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PlayerWeapon") == 1)
        {
            
            if (Input.GetKeyDown(KeyCode.C)&&!player.isSkill)
            {
                StartCoroutine(Sting());
            }
            if (rush != null)
            {
                rush.transform.position = transform.position + Vector3.up * 2;
                if (player.isLeft)
                    rush.transform.rotation = Quaternion.identity;
                else
                    rush.transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }

        if (PlayerPrefs.GetInt("PlayerWeapon") == 2)
        {                    
            if (Input.GetKeyDown(KeyCode.C)&&!player.isSkill)
            {
                StartCoroutine(ArrowRain());
            }
        }

        if (PlayerPrefs.GetInt("PlayerWeapon") == 3)
        {               
            if (Input.GetKeyDown(KeyCode.C) && !player.isSkill)
            {
                StartCoroutine(Tornado());
            }
            if (tornado != null)
            {
                atkTime += Time.deltaTime;
                tornado.transform.position = transform.position + Vector3.down;
            }
            if (Input.GetKeyUp(KeyCode.C) || atkTime >= duration)
            {
                if (tornado != null)
                    Destroy(tornado.gameObject);
                atkTime = 0;
                player.isSkill = false;
            }
        }


    }

    IEnumerator Sting()
    {
        if (!isCd)
        {
            cdWork=false;
            isCd = true;
            rush = Instantiate(rushEffect, transform.position + Vector3.up * 2, Quaternion.identity);
            player.ani.SetBool("isRush", true);
            player.isSkill = true;           
            for (int i = 0; i < 50; i++)
            {
                GameObject sting;
                float rand = Random.Range(-2, 2);
                float vertical = Random.Range(-3, 2);

                if (player.isLeft)
                {
                    sting = Instantiate(stingEffect, transform.position + Vector3.left * 10 + Vector3.up * rand + Vector3.up * 2 + Vector3.left * vertical, Quaternion.identity);
                }
                else
                {
                    sting = Instantiate(stingEffect, transform.position + Vector3.right * 10 + Vector3.up * rand + Vector3.up * 2 + Vector3.right * vertical, Quaternion.Euler(0, 180, 0));
                }
                Destroy(sting, 0.2f);
                yield return new WaitForSeconds(0.07f);
            }
            Destroy(rush);
            cdWork = true;
            player.ani.SetBool("isRush", false);
            player.isSkill = false;
            yield return new WaitForSeconds(cd1);
            isCd = false;
        }
    }

    IEnumerator ArrowRain()
    {
        if (!isCd)
        {           
            isCd = true;
            cdWork=false;
            Vector3 dir;
            Vector3 rot;
            if (player.isLeft)
            {
                dir = Vector3.left;
                rot = new Vector3(0, 0, -42);
            }
            else
            {
                dir = Vector3.right;
                rot = new Vector3(0, 180, -42);
            }


            GameObject b = Instantiate(bowEffect, transform.position + Vector3.up * 4 + dir * 2, Quaternion.Euler(rot));

            Destroy(b, 0.8f);
            Vector3 pos = transform.position + Vector3.up * 25 + dir * 10;

            for (int i = 0; i < 60; i++)
            {
                float rand = Random.Range(-7f, 7f);

                Instantiate(arrowEffect, pos + Vector3.right * rand, Quaternion.Euler(0, 0, 90));
                yield return new WaitForSeconds(0.05f);
            }
            cdWork=true;
            yield return new WaitForSeconds(cd2);
            isCd = false;
        }
    }

    IEnumerator Tornado()
    {
        if (!isCd)
        {
            player.isSkill = true;
            isCd = true;
            cdWork=false;
            tornado = Instantiate(tornadoEffect, transform.position + Vector3.down * 3, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            cdWork=true;
            yield return new WaitForSeconds(cd3);
            isCd = false;
        }
    }

    private void OnDestroy() {
        cdWork=false;
    }

    private void OnDisable()
    {
        isCd= false;
    }
}
