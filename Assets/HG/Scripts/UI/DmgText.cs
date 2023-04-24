using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgText : MonoBehaviour
{   
    public Text dmgText;
    Vector2 pos;       
    void Start()
    {      
        Destroy(gameObject,0.5f);
    }

    private void Update()
    {
        if (pos != null&&pos!=Vector2.zero)
        {
            transform.position = Camera.main.WorldToScreenPoint(pos);
        }
    }
    public void Set(float dmg,Vector2 pos)
    {
        dmgText.text = Mathf.Round(dmg).ToString();
        this.pos = pos;
    }
}
