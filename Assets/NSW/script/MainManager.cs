using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("PlayerMaxHp", 1000);
        PlayerPrefs.SetFloat("PlayerCurrHp", PlayerPrefs.GetFloat("PlayerMaxHp"));
        PlayerPrefs.SetFloat("PlayerAtk", 5);
        PlayerPrefs.SetFloat("CurrStage", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
