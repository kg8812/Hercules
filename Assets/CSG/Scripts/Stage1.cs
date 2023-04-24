using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public Flowchart flowchart;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrStage") == 1.2f)
        {
            flowchart.ExecuteBlock("Boss1 End");
        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 1.4f)
        {
            flowchart.ExecuteBlock("Boss1 End");

        }
        else if (PlayerPrefs.GetFloat("CurrStage") == 1.6f)
        {
            flowchart.ExecuteBlock("Boss2 End");
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
