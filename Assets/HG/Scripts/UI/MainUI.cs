using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameObject exitWindow;
    public GameObject credit;

    public void OpenCredit()
    {
        credit.SetActive(true);
    }
    public void CloseCredit()
    {
        credit.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Close()
    {
        exitWindow.SetActive(false);
    }

    public void Open()
    {
        exitWindow.SetActive(true);
    }
}
