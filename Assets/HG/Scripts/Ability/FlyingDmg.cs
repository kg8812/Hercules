using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDmg : MonoBehaviour
{ 
    float nAtk;
    Player player;
    public static float ratio = 0.5f;
    public static bool isEnhanced = false;
    private void Start()
    {

        player = GetComponent<Player>();
        nAtk = PlayerPrefs.GetFloat("PlayerAtkRatio");
    }
    void Update()
    {
        nAtk = PlayerPrefs.GetFloat("PlayerAtkRatio");
        if (isEnhanced)
        {
            ratio = 1f;
        }
        else
        {
            ratio = 0.5f;
        }
        if (!player.isGround)
        {
            
            player.atkRatio = nAtk + ratio;
        }
        else
        {
            player.atkRatio = nAtk;
        }

    }
}
