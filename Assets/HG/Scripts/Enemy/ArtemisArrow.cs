using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtemisArrow : MonoBehaviour
{
    public float dmg;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, -90);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage iDmg = collision.GetComponent<IOnDamage>();

        if (iDmg != null)
        {           
            iDmg.OnHit(dmg);
        }
        Destroy(gameObject);
    }
}
