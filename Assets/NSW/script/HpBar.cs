using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    public Text hpText; //현재체력과 최대체력을 나타내는 Text
    public Image hpBar; //빨간체력바 이미지 (Fill Amount 조절용)
    float maxhp;   // 최대체력 저장변수
    public float hp;    // 현재체력 저장변수
    GameObject player;  // 플레이어 변수
    // Start is called before the first frame update
    void Start()
    {
        maxhp = PlayerPrefs.GetFloat("PlayerMaxHp");    
        hp = PlayerPrefs.GetFloat("PlayerCurrHp");      
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        player = GameObject.Find("Player").transform.Find("Heracles").gameObject;
        else
        {
            hp = player.GetComponent<Player>().currHp;
            maxhp = player.GetComponent<Player>().maxHp;
        }
       
        hpBar.fillAmount = hp / maxhp;
        hpText.text =$"{Mathf.Round(hp)}/{Mathf.Round(maxhp)}"; 

    }
}
