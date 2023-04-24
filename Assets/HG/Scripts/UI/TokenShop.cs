using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenShop : MonoBehaviour
{
    public GameObject complete; // 구매완료창 할당 변수
    public Text completeText; // 구매완료창 내 텍스트

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    bool isBuy = false;

    IEnumerator Buy(int n)
    {
        completeText.text = $"토큰을 {n}개 구매하셨습니다!";
        isBuy = true;
        complete.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        complete.SetActive(false);
        isBuy = false;

    }
    public void Button1()
    {
        if (!isBuy)
        {
            PlayerPrefs.SetInt("Token", GameManager.Instance.token + 1000);
            StartCoroutine(Buy(1000));
        }
    }

    public void Button2()
    {
        if (!isBuy)
        {
            PlayerPrefs.SetInt("Token", GameManager.Instance.token + 5500);
            StartCoroutine(Buy(5500));
        }
    }

    public void Button3()
    {
        if (!isBuy)
        {
            PlayerPrefs.SetInt("Token", GameManager.Instance.token + 12000);
            StartCoroutine(Buy(12000));
        }

    }
    public void Button4()
    {
        if (!isBuy)
        {
            PlayerPrefs.SetInt("Token", GameManager.Instance.token + 42000);
            StartCoroutine(Buy(42000));
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
