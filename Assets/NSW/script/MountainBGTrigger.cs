using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainBGTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float KeyH = Input.GetAxis("Horizontal");
            if (KeyH > 0)
                BG.instance.isBG1 = false;
            else if (KeyH < 0)
                BG.instance.isBG1 = true;
        }
    }
}
