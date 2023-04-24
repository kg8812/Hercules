using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    GameObject atkEffect;
    Vector2 size;
    Vector2 point;
    bool isCd = false;
    public static float cd = 7;

    private void Awake()
    {
        size = new Vector2(40, 25);
    }
    private void Start()
    {
        atkEffect = EffectManager.instance.autoAttackEffect;

    }
    private void Update()
    {
        point = transform.position + Vector3.up * 4.5f;
        Collider2D[] hit = Physics2D.OverlapBoxAll(point, size, 0, LayerMask.GetMask("Enemy"));
        StartCoroutine(Attack(hit));
    }
    IEnumerator Attack(Collider2D[] hit)
    {
        if (!isCd)
        {
            
            isCd = true;
            int rand = Random.Range(0, hit.Length);
            if (hit.Length > 0)
            {
                GameObject a = Instantiate(atkEffect, hit[rand].transform.position, Quaternion.identity);
                Destroy(a, 0.5f);
            }
            yield return new WaitForSeconds(cd);
            isCd = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(point, size);
    }

}

