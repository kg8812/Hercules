using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderArrow : EffectDmgText
{
    public float dmg = 20;
    public bool isStart = false;
    public float speed = 25;
    public Animator ani;
    public GameObject effect;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isStart = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            ani.SetBool("isShot", true);
            transform.Translate(Vector2.up * speed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage idmg = collision.GetComponent<IOnDamage>();
        if (idmg != null)
            idmg.OnHit(dmg);
        Create(dmg, collision.ClosestPoint(transform.position), Color.white);
    }

}
