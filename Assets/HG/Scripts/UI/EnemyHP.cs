using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    GameObject canvas; // 생성할 캔버스
    public GameObject barPref; // 체력바 프리팹
    Image redBar; // 붉은색바 이미지 (채움값 조절용)
    Enemy enemy; // 적
    RectTransform bar; // 체력바 위치

    private void Awake()
    {
        canvas = GameObject.Find("Canvas"); // 캔버스 찾기
        enemy = GetComponent<Enemy>(); // 적 찾기
    }
    private void Start()
    {
        bar = Instantiate(barPref, canvas.transform).GetComponent<RectTransform>(); // 체력바 생성 및 위치 컴포넌트 할당
       
        redBar = bar.transform.Find("red").GetComponent<Image>(); // 붉은색바 이미지 가져오기
        enemy.bar = bar.gameObject; //적에게 체력바 할당

    }
    // Update is called once per frame
    void Update()
    {
        redBar.fillAmount = enemy.hp / enemy.maxHp; // 현재 체력에 비례해서 붉은바 채움값 조절
        if (bar != null)
        { 
            // 체력바 위치 조절
            bar.position = Camera.main.WorldToScreenPoint(enemy.transform.position + Vector3.up * enemy.height);                  
        }
    }
}
