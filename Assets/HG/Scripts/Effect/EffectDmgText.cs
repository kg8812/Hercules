using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDmgText : MonoBehaviour
{
    GameObject dmgText;
    GameObject canvas;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        dmgText = EffectManager.instance.dmgText;
        canvas = GameObject.Find("Canvas");
    }

    public void Create(float dmg,Vector2 point, Color color)
    {
        GameObject text = Instantiate(dmgText, canvas.transform);
        text.GetComponent<DmgText>().Set(dmg, point);
        text.GetComponent<DmgText>().dmgText.color = color;
    }
}
