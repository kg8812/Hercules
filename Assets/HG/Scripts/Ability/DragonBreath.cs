using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    GameObject fireEffect;  // 화염 이펙트
    float duration = 5f;    // 지속시간
    public static float cd; // 쿨타임
    float inUse = 0;    // 사용중인 시간
    bool isCd;  // 코루틴 제어 변수
    GameObject fire;    // 스킬 전체 오브젝트
    Player player;  // 플레이어
   
    void Start()
    {
        SkillInven.instance.Add(SkillManager.instance.dragonBreath);
        cd = SkillManager.instance.dragonBreath.cooldown;
        fireEffect = EffectManager.instance.dragonBreathEffect;
        player = GetComponent<Player>();
        SkillManager.instance.dragonBreath.cdWork = true;
    }

    void Update()
    {
        
        if (Input.GetButton("스킬" + SkillInven.instance.skills.IndexOf(SkillManager.instance.dragonBreath)))
        {
            StartCoroutine(Breath());

            if (fire != null)
            {
                inUse += Time.deltaTime;
                Vector3 pos;
                if (player.isLeft)
                {
                    fire.transform.rotation = Quaternion.Euler(0, 180, 0);
                    pos = Vector2.left * 2;
                }
                else
                {
                    fire.transform.rotation = Quaternion.identity;
                    pos = Vector2.right * 2;

                }
                fire.transform.position = transform.position + Vector3.up * 3.5f + pos;

            }
        }
        if (Input.GetButtonUp("스킬" + SkillInven.instance.skills.IndexOf(SkillManager.instance.dragonBreath)) || inUse >= duration)
        {
            if(fire != null)
            Destroy(fire);

            player.isSkill = false;
        }
    }

    IEnumerator Breath()
    {
        if (!isCd && !player.isSkill)
        {
            isCd = true;
            player.isSkill = true;
            SkillManager.instance.dragonBreath.cdWork = false;
            inUse = 0;
            fire = Instantiate(fireEffect, transform.position + Vector3.up * 3.5f, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            SkillManager.instance.dragonBreath.cdWork = true;
            yield return new WaitForSeconds(cd);
            isCd = false;
           
        }
    }

    private void OnDisable()
    {
        isCd = false;
    }
}
