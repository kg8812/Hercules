using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int num;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           
            if(num==1)
            {
              
              BG.instance.MoveBG(1);
            }
            else if(num==2)
            {
                BG.instance.MoveBG(2);
            }
        }
    }
}
