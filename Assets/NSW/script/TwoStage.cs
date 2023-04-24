using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class TwoStage : MonoBehaviour
{
    public int stage;
    public Transform returnPos21;
    public Flowchart flowchart;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrStage") == 2.0f)
        {
            flowchart.ExecuteBlock("2스테이지시작");
        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 2.2f)
        {
            player.position = returnPos21.position;
            flowchart.ExecuteBlock("2-1클리어");

        }
        else if(PlayerPrefs.GetFloat("CurrStage") == 2.4f)
        {
            flowchart.ExecuteBlock("2-2클리어");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
