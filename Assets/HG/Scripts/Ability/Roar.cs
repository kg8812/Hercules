using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : MonoBehaviour
{
    Vector3 centre;
    public Vector2 size;
    Collider2D[] collisions;
    public static float cd;
    bool isCd = false;
    public static bool cdWork=true;
    GameObject lion;
    GameObject stun;
    public CameraMove main;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        SkillInven.instance.Add(SkillManager.instance.lionRoar);
        cd = SkillManager.instance.lionRoar.cooldown;
        main = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        lion = EffectManager.instance.LionEffect;
        stun = EffectManager.instance.stunEffect;
        size = new Vector2(25,25);
        SkillManager.instance.lionRoar.cdWork = true;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        centre = transform.position + Vector3.up * 2.5f;

        if (Input.GetButtonDown("스킬" + SkillInven.instance.skills.IndexOf(SkillManager.instance.lionRoar)))
        {
            StartCoroutine(Use());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centre, size);
    }

   
    IEnumerator Use()
    {
        if(!isCd&&!player.isSkill)
        {
            player.isSkill = true;
            StartCoroutine(main.Shake());
            SkillManager.instance.lionRoar.cdWork = false; 
            isCd= true;            
            collisions = Physics2D.OverlapBoxAll(centre, size, 0, LayerMask.GetMask("Enemy"));
            Instantiate(lion, transform.position + Vector3.up * 7, Quaternion.identity);
            if (collisions.Length > 0)
            {
                for (int i = 0; i < collisions.Length; i++)
                {
                    Enemy enemy = collisions[i].GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        StartCoroutine(enemy.Stop(3));
                        enemy.hp -= enemy.maxHp * 0.05f;
                        
                        GameObject s = Instantiate(stun, enemy.transform.position + Vector3.up * enemy.height,Quaternion.identity);
                        Destroy(s, 3);
                    }
                }
            }       
            yield return new WaitForSeconds(0.5f);
            SkillManager.instance.lionRoar.cdWork = true;
            player.isSkill = false;

            yield return new WaitForSeconds(cd);
            isCd = false;
        }
    }

    private void OnDisable()
    {
        isCd = false;
    }

}
