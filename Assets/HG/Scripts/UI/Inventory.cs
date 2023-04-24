using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] itemTab;    // 아이템 탭 배열 (장비,소비,기타)
    int num =0; // 아이템 탭 배열 인덱스
    public static Inventory instance;   //싱글톤
    public Text tokenText;  // 토큰(캐시) 텍스트

    void OnEnable()
    {
        Time.timeScale = 0;
        for(int i=0;i<itemTab.Length;i++)
        {
            itemTab[i].SetActive(false);
        }
        num=0;
        itemTab[num].SetActive(true);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        if (instance == this)
            UIManager.inventory = this.gameObject;
        for (int i = 0; i < itemTab.Length; i++)
        {
            itemTab[i].SetActive(true);
        }
        gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    
    void Update()
    {
        tokenText.text = PlayerPrefs.GetInt("Token").ToString();
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Tab();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.SetActive(false);
        }
    }

    void Tab()
    {
        itemTab[num].SetActive(false);
        num++;
        if(num>=itemTab.Length)
        num=0;

        itemTab[num].SetActive(true);
    }

    public void Close()
    {
        
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

}
