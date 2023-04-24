using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Zeus : MonoBehaviour
{
    Player player;
    RaycastHit2D rayHit;
    bool isCd = false;
    bool isRushCharge = false;
    float rushRange = 20;
    GameObject rushEffect;
    GameObject shockWaveEffect;
    GameObject arrowEffect;
    GameObject bowEffect;
    bool isArrowCharge = false;
    float arrowScaleFactor = 2;
    
    GameObject arrow;
    GameObject bow;

    public static bool cdWork = false;
    float cd1;
    float cd2;
    float cd3;

    private void OnEnable()
    {
        isCd = false;
        isRushCharge = false;
        cdWork = true;
    }
    void Start()
    {
        cd1 = SkillManager.instance.zeusSkill[0].cooldown;
        cd2 = SkillManager.instance.zeusSkill[1].cooldown;
        cd3 = SkillManager.instance.zeusSkill[2].cooldown;
        cdWork=true;
        player = GetComponent<Player>();
        rushEffect = EffectManager.instance.thunderRushEffect;
        shockWaveEffect = EffectManager.instance.shockWaveEffect;
        arrowEffect = EffectManager.instance.thunderArrowEffect;
        bowEffect = EffectManager.instance.ThunderBow;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("PlayerWeapon") == 1)
        {
            
            if (player.isGround && Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ThunderRush());
            }

            if (isRushCharge)
            {
                player.ani.SetBool("isDash", true);
                if (player.isLeft)
                {                  
                    player.isLeft = true;
                    rayHit = Physics2D.BoxCast(transform.position + Vector3.up * 2, player.sideBoxSize, 0, Vector3.left, rushRange, LayerMask.GetMask("Wall"));

                }
                else
                {                   
                    player.isLeft = false;
                    rayHit = Physics2D.BoxCast(transform.position + Vector3.up * 2, player.sideBoxSize, 0, Vector3.right, rushRange, LayerMask.GetMask("Wall"));
                }
            }

        }
        else if (PlayerPrefs.GetInt("PlayerWeapon") == 3)
        {
            
            if (player.isGround && Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ShockWave());
            }
        }
        else if (PlayerPrefs.GetInt("PlayerWeapon") == 2)
        {
           
            if (!isCd && Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ThunderArrow());
            }
            if (isArrowCharge && Input.GetKey(KeyCode.C))
            {
                arrowScaleFactor += Time.deltaTime * 1.5f;
                arrow.GetComponent<ThunderArrow>().effect.transform.localScale = new Vector3(arrowScaleFactor, arrowScaleFactor, arrowScaleFactor);
                bow.transform.localScale = new Vector3(1 + arrowScaleFactor / 6, 1 + arrowScaleFactor / 6, 1 + arrowScaleFactor / 6);
                arrow.transform.position = transform.position + Vector3.up * 2;
                bow.transform.position = transform.position + Vector3.up * 2;
                if (player.isLeft)
                {
                    arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                    bow.transform.rotation = Quaternion.Euler(0, 180, 90);
                }
                else
                {
                    arrow.transform.rotation = Quaternion.Euler(0, 180, 90);
                    bow.transform.rotation = Quaternion.Euler(0, 0, 90);
                }

            }
            if (isArrowCharge && (Input.GetKeyUp(KeyCode.C) || arrowScaleFactor > 6))
            {
                cdWork=true;
                Destroy(bow);
                arrow.GetComponent<ThunderArrow>().dmg *= arrowScaleFactor;
                arrow.GetComponent<ThunderArrow>().speed *= (1 + arrowScaleFactor / 6);
                arrow.GetComponent<ThunderArrow>().isStart = true;
                Destroy(arrow, 1f);
                isArrowCharge = false;
                arrowScaleFactor = 1;
            }
        }

    }
    private void OnDrawGizmos()
    {
        if (isRushCharge)
        {
            RaycastHit2D ray;
            Vector3 dir;
            if (player.isLeft)
            {
                ray = Physics2D.BoxCast(transform.position + Vector3.up * 2, player.sideBoxSize, 0, Vector3.left, rushRange, LayerMask.GetMask("Wall"));
                dir = Vector3.left;

            }
            else
            {
                ray = Physics2D.BoxCast(transform.position + Vector3.up * 2, player.sideBoxSize, 0, Vector3.right, rushRange, LayerMask.GetMask("Wall"));
                dir = Vector3.right;
            }

            Gizmos.color = Color.yellow;
            if (ray.collider != null)
            {
                Gizmos.DrawRay(transform.position + Vector3.up * 2, dir * ray.distance);
                Gizmos.DrawWireCube(transform.position + Vector3.up * 2 + dir * ray.distance, player.sideBoxSize);
            }
            else
            {
                Gizmos.DrawRay(transform.position + Vector3.up * 2, dir * rushRange);
                Gizmos.DrawWireCube(transform.position + Vector3.up * 2 + dir * rushRange, player.sideBoxSize);
            }
        }
    }
    IEnumerator ThunderRush()
    {
        if (!isCd)
        {
            cdWork=false;
            isCd = true;
            isRushCharge = true;
            player.isDash = true;
            yield return new WaitForSeconds(0.5f);
            GameObject r;
            if (rayHit.collider == null)
            {
                if (player.isLeft)
                {
                    player.body.MovePosition(transform.position + Vector3.left * rushRange);
                    r = Instantiate(rushEffect, transform.position + Vector3.up * 2, Quaternion.Euler(0, 180, 0));
                    Destroy(r, 1f);
                }
                else
                {
                    player.body.MovePosition(transform.position + Vector3.right * rushRange);
                    r = Instantiate(rushEffect, transform.position + Vector3.up * 2, Quaternion.identity);
                    Destroy(r, 1f);
                }

            }
            else
            {
                if (player.isLeft)
                {
                    r = Instantiate(rushEffect, transform.position + Vector3.up * 2, Quaternion.Euler(0, 180, 0));
                    player.body.MovePosition(rayHit.point + Vector2.down * 2);

                }
                else
                {
                    r = Instantiate(rushEffect, transform.position + Vector3.up * 2, Quaternion.identity);
                    player.body.MovePosition(rayHit.point + Vector2.down * 2);

                }
                r.transform.localScale = new Vector3(r.transform.localScale.x * (rayHit.distance / rushRange), 0.7f, 1);
                Destroy(r, 1f);
            }
            yield return new WaitForSeconds(0.3f);
            cdWork=true;
            player.isDash = false;

            isRushCharge = false;
            player.ani.SetBool("isDash", false);
            yield return new WaitForSeconds(cd1);
            isCd = false;
        }
    }

    IEnumerator ShockWave()
    {
        if (!isCd)
        {
            cdWork=false;
            isCd = true;
            float length = 1;
            float scaleFactor = 1;
            Vector3 pos = transform.position;
            bool isLeft = player.isLeft;
            for (int i = 0; i < 5; i++)
            {
                length += 3 * scaleFactor;
                GameObject s;

                if (!isLeft)
                {
                    s = Instantiate(shockWaveEffect, pos + Vector3.right * length, Quaternion.identity);
                }
                else
                {
                    s = Instantiate(shockWaveEffect, pos + Vector3.left * length, Quaternion.Euler(0, 180, 0));
                }
                s.transform.localScale *= scaleFactor;
                s.GetComponent<ShockWave>().scaleFactor = scaleFactor;
                scaleFactor += 0.2f;
                yield return new WaitForSeconds(0.3f);
            }
            cdWork=true;
            yield return new WaitForSeconds(cd3);
            isCd = false;
        }
    }

    IEnumerator ThunderArrow()
    {
        if (!isCd)
        {
            cdWork=false;
            isCd = true;
            isArrowCharge = true;

            if (player.isLeft)
            {
                arrow = Instantiate(arrowEffect, transform.position + Vector3.up * 2, Quaternion.Euler(0, 0, 90));
                bow = Instantiate(bowEffect, transform.position + Vector3.up * 2, Quaternion.Euler(0, 180, 90));
            }
            else
            {
                arrow = Instantiate(arrowEffect, transform.position + Vector3.up * 2, Quaternion.Euler(0, 180, 90));
                bow = Instantiate(bowEffect, transform.position + Vector3.up * 2, Quaternion.Euler(0, 0, 90));

            }
            yield return new WaitForSeconds(cd2);
            isCd = false;
        }
    }

    private void OnDestroy() 
    {
        cdWork=false;
    }
}
