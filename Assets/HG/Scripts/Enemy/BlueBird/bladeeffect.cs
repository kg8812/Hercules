using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeeffect : MonoBehaviour
{
    GameObject target;
    Vector3 dir;
    float awakeTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform.Find("Heracles").gameObject;
        if (target != null)
        {
           
            dir = target.transform.position - transform.position;
            dir = dir.normalized;
            float angle = Mathf.Atan2(dir.y + 0.15f, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
      
        dir = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        awakeTime += Time.deltaTime;
        if(awakeTime > 5.0f)
        
         Destroy(this.gameObject);
          
       
        else if(awakeTime > 1.0f)
            transform.Translate(dir * 20 * Time.deltaTime);   //이동

    }
}
