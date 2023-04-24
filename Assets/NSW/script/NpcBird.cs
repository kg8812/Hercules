using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBird : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y > 10)
        {
            this.transform.Translate(Vector2.down * 5 * Time.deltaTime);
        }
    }
}
