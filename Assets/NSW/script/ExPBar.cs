using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExPBar : MonoBehaviour
{
    public Image expBar;
    public float curExp;
    public float MaxExp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expBar.fillAmount = curExp / MaxExp;
    }
}
