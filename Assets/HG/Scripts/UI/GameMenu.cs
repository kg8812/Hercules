using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject title;    // 타이틀 메뉴
    public GameObject quit; // 종료 메뉴

    public void Close() // 메뉴 닫기
    {
        UIManager.instance.PausedBTN();
    }

    public void CloseTitle()    // 타이틀 메뉴 닫기
    {
        title.SetActive(false);
    }
    public void OpenTitle() // 타이틀 메뉴 열기
    {
        title.SetActive(true);
    }

    public void OpenQuit()  // 종료 메뉴 열기
    {
        quit.SetActive(true);
    }
    public void CloseQuit() // 종료 메뉴 닫기
    {
        quit.SetActive(false);
    }
    public void ToTitle()   // 타이틀로 돌아가기
    {
        SceneManager.LoadScene("Main Screen");
    }

    public void Quit()  // 게임 종료
    {
        Application.Quit();
    }
}
