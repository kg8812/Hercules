using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject cheat;
   public void OpenCheat()
    {
        cheat.SetActive(true);
    }

    private void Awake()
    {
        GameManager.Reset();
    }

    private void Start()
    {
        ItemManager.instance.NewGame();

    }
}
