using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : EffectDmgText
{
    Enemy enemy;    // 적
    float duration = 3f;    // 총 지속시간
    public float time = 0;  // 현재 지속시간
    public float dmg = 1f;  // 데미지 (최대체력 %)
    GameObject poisonEffect; // 독 이펙트 프리팹
    GameObject p;   // 독 이펙트 저장용 변수

    protected override void Start()
    {
        base.Start();
        enemy= GetComponent<Enemy>();
        InvokeRepeating("HpDown", 1,1);
        poisonEffect = EffectManager.instance.poisonEffect;
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if(time>duration)
        {
            HpDown();
            Destroy(this);
        }

        if (p != null)
        {
            p.transform.position = transform.position + Vector3.up * enemy.height;
        }
    }

    void HpDown()
    {       
        enemy.hp -= enemy.maxHp * dmg/100;
        Create(enemy.maxHp * dmg / 100, transform.position, Color.grey);
        p = Instantiate(poisonEffect);
        Destroy(p, 1);
    }
}
