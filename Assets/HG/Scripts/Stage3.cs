using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Stage3 : MonoBehaviour
{
    public Flowchart flowchart;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrStage") == 3.0f)
        {
            flowchart.ExecuteBlock("3스테이지시작");
        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 3.2f)
        {           
            flowchart.ExecuteBlock("3-1클리어");

        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 3.4f)
        {
            flowchart.ExecuteBlock("3-2클리어");
        }
    }
   

    public void ToNothing()
    {
        Camera.main.cullingMask = 0;
    }

    public void ToEverything()
    {
        Camera.main.cullingMask = -1;
    }
}
