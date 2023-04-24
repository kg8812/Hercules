using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatUI : MonoBehaviour
{
    public GameObject[] canvs;
    int num;
    public Text pageText;

    private void OnEnable()
    {
        num = 0;
        Time.timeScale = 0;

        canvs[0].SetActive(true);

        for (int i = 1; i < canvs.Length; i++)
        {
            canvs[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (canvs.Length > 0)
        {
            pageText.text = $"{num + 1} / {canvs.Length}";
        }
        else
        {
            pageText.text = $"{num + 1} / 1";
        }
    }
    public void PageUp()
    {
        canvs[num].SetActive(false);
        num++;
        if (num >= canvs.Length) num = 0;

        canvs[num].SetActive(true);
    }

    public void PageDown()
    {
        canvs[num].SetActive(false);
        num--;
        if (num < 0)
        {
            if (canvs.Length > 0)
                num = canvs.Length - 1;
            else
                num = 0;
        }

        canvs[num].SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
