using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Transform[] Blocks;
    public Vector2[] Oripos;
    int blockCnt=0;
    bool isBlink;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Oripos.Length; i++)
        {
            Oripos[i] = Blocks[i].transform.position;
        }
        for(int i=0;i<Blocks.Length;i++)
        {
            Blocks[i].transform.position = new Vector2(-900, -900);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlink)
        {
            float t = Random.Range(1, 3);
            StartCoroutine(BlinkWall(t));

        }


    }
    IEnumerator BlinkWall(float t)
    {
        isBlink = true;

        yield return new WaitForSeconds(4.5f - t);
        
        for(int i = 0; i < Blocks.Length;i++)
        {
            if (blockCnt >= 3) break;
            if(Random.Range(0,100)>=50 && blockCnt < 3)
            {
                Blocks[i].transform.position = Oripos[i];
                blockCnt++;
            }

        }
        yield return new WaitForSeconds(t+1.0f);
        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i].transform.position = new Vector2(-900, -900);
        }
        blockCnt = 0;
        isBlink = false;
       
    }
}
