using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCrit : MonoBehaviour
{
    Player player;
   
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        player.critProb = 0;
    }

   
}
