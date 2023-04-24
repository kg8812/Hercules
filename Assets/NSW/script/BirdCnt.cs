using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCnt : MonoBehaviour
{
    public int cnt = 1;
    public static BirdCnt instance;
    private void Awake()
    {
        instance = this;
    }
}
