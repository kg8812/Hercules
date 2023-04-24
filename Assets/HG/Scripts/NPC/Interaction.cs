using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    float distance; // 플레이어와의 거리
    Transform player;   // 플레이어의 위치
    public GameObject text; // 상호작용 텍스트
    public GameObject UI;   // 상호작용시 띄울 UI

    void Start()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").transform;
    }

    void Update()
    {
        distance = Vector2.Distance(player.position, transform.position);
        if (distance < 3)
        {
            text.gameObject.SetActive(true);
            if (UI != null&&Input.GetKeyDown(KeyCode.F))
            {
                UI.SetActive(true);
            }
        }
        else text.gameObject.SetActive(false);

        if (UI != null && distance > 6)
        {
            UI.SetActive(false);
        }
        
    }
}
