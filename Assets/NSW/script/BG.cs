using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public Transform[] BGimage;
    public static BG instance;
    public bool isBG1 = true;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveBG(int num)
    {
        if (!isBG1)
            return;
        float KeyH = Input.GetAxis("Horizontal");
        if(num == 1)
        {
            if(KeyH>=0)
            BGimage[1].transform.position = new Vector2(BGimage[0].transform.position.x+32,0);
            else
            BGimage[1].transform.position = new Vector2(BGimage[0].transform.position.x - 32, 0);
         
        }
        else if(num == 2)
        {
            if (KeyH >= 0)
                BGimage[0].transform.position = new Vector2(BGimage[1].transform.position.x+32, 0);
            else
                BGimage[0].transform.position = new Vector2(BGimage[1].transform.position.x - 32, 0);
        }
    }
}
