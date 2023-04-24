using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public static float cd = 15;
    public static float afterHit;
    
    void Update()
    {
        afterHit += Time.deltaTime;
    }
}
