using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStone : MonoBehaviour
{
    bool isBlink = false;
    Vector2 oripos;
    
    // Start is called before the first frame update
    void Start()
    {
        oripos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlink)
        {
            float t = Random.Range(1,3);
                StartCoroutine(BlinkWall(t));
           
        }
    }
    IEnumerator BlinkWall(float t)
    {
        isBlink = true;

        yield return new WaitForSeconds(4.5f-t);
        this.transform.position = new Vector2(-900, -900);
        yield return new WaitForSeconds(t);
        isBlink = false;
        this.transform.position = oripos;

    }
}
