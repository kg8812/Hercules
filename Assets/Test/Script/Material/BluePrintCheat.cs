using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrintCheat : MonoBehaviour
{
    public Button[] buttons;
    public ItemInfo[] items;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[temp].onClick.AddListener(() => Obtain(temp));
        }
    }
  
    void Obtain(int x)
    {
        items[x].bluePrint = true;
    }
}
