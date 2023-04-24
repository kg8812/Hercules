using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Return : MonoBehaviour
{
    
    public Transform returnPos;
    public Transform returnPos2;
    // Start is called before the first frame update
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(PlayerPrefs.GetFloat("CurrStage") < 2.0f)
            {
                collision.transform.position = returnPos.position;
                collision.GetComponent<Player>().currHp -= 50;
            }
            else if (PlayerPrefs.GetFloat("CurrStage") == 2.0f)
            {            
                collision.transform.position = returnPos.position;
                collision.GetComponent<Player>().currHp -= 100;
            }
               
            else if (PlayerPrefs.GetFloat("CurrStage") == 2.2f)
            {
                
                collision.transform.position = returnPos2.position;
                collision.GetComponent<Player>().currHp -= 150;
            }
            else
            {
                collision.transform.position = returnPos.position;
                collision.GetComponent<Player>().currHp -= 200;
            }
           if(collision.GetComponent<Player>().currHp < 1)
                collision.GetComponent<Player>().currHp = 1;



        }
    }
}
