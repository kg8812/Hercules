using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAttack : MonoBehaviour
{
    GameObject effect;
    Player player;
    public static float range = 0.5f;
    public static float scale = 1;
    public static bool isEnhanced = false;
    bool isAttack = false;

    void Start()
    {
        player = GetComponent<Player>();
        effect = EffectManager.instance.bonusAttackEffect;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
            StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            Vector3 dir;
            Quaternion rot;

            if (player.isLeft)
            {
                dir = Vector3.left;
                rot = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                dir = Vector3.right;
                rot = Quaternion.identity;
            }
            GameObject a = Instantiate(effect, transform.position + dir * 3 + Vector3.up * 3, rot);
            a.transform.localScale *= scale;
            Destroy(a, range);
            yield return new WaitForSeconds(0.4f);
            isAttack = false;
        }
    }
   
}
