using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : MonoBehaviour
{
    public Flowchart flowchart;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrStage") == 4.0f)
        {
            flowchart.ExecuteBlock("4스테이지시작");
        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 4.2f)
        {
            flowchart.ExecuteBlock("4-1클리어");

        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 4.4f)
        {
            flowchart.ExecuteBlock("4-2클리어");
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
