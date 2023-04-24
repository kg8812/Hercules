using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHpBar : MonoBehaviour
{
    
    public Image hpBar;
    float maxhp = 100;
    public float hp = 100;
    public GameObject Boss;
    GameObject miniMap;
    // Start is called before the first frame update
    void Start()
    {
        maxhp = Boss.GetComponent<Enemy>().maxHp;
        miniMap = GameObject.FindGameObjectWithTag("MiniMap");
        if(miniMap!=null)
        miniMap.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss != null)
        {
            hp = Boss.GetComponent<Enemy>().hp;
            hpBar.fillAmount = hp / maxhp;
        }
        else
        {
            hpBar.fillAmount = 0 / 100;
        }
     
       
    }
}
