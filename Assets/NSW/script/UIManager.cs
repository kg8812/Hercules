using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Camera miniMap;  // 미니맵용 카메라
    public RawImage rawImage;   // 미니맵 UI
    public Texture[] miniMapTexture;    // 미니맵 카메라 텍스쳐 저장 배열 (스테이지마다 텍스쳐가 다름)
    public static UIManager instance;   // 싱글톤용 변수
    public Canvas mainMenu; // 게임 메뉴 캔버스
    public Canvas status;   // 상태창 캔버스
    public static GameObject inventory; // 인벤토리
    public Canvas shop; // 캐시 상점 캔버스
   
    private void Awake()
    {       
        miniMap = GameObject.FindGameObjectWithTag("MiniMap").GetComponent<Camera>();   //미니맵 카메라 찾기
        instance = this;    
        mainMenu.gameObject.SetActive(false);
        status.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && Time.timeScale != 0) // 인벤토리 열기
        {
            inventory.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale != 0) // 상태창 열기
        {
            status.gameObject.SetActive(true);
        }
    }
    public bool isPaused = false;   // 정지여부 확인 변수
    public void PausedBTN() // 정지버튼 함수
    {
        if (!isPaused)  //멈춰있지 않을경우 정지
        {
            isPaused = true;
            Time.timeScale = 0;
            mainMenu.gameObject.SetActive(true);
        }
        else // 멈춰있을경우 정지 해제
        {
            isPaused = false;
            Time.timeScale = 1;
            mainMenu.gameObject.SetActive(false);
        }
    }
    
    public void ShopBTN()   // 캐시 상점 열기
    {
        shop.gameObject.SetActive(true);
    }
    
    

}
