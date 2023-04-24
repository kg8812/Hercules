using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public GameObject status;
    public GameObject skillMenu;
    public GameObject godStatus;
    int num = 0;
    private void OnEnable()
    {
        Time.timeScale = 0;
        num = 0;
        ChangeTab();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            num++;
            if (num > 2)
            {
                num = 0;
            }
            ChangeTab();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.SetActive(false);
        }
    }

    void ChangeTab()
    {
        switch (num)
        {
            case 0:
                status.SetActive(true);
                skillMenu.SetActive(false);
                godStatus.SetActive(false);
                break;
            case 1:
                status.SetActive(false);
                skillMenu.SetActive(true);
                godStatus.SetActive(false);
                break;
            case 2:
                status.SetActive(false);
                skillMenu.SetActive(false);
                godStatus.SetActive(true);
                break;
        }
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
