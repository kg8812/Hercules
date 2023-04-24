using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Equipment.instance.ResetItem();
            Consumable.instance.ResetItem();
            GameManager.Reset();
            SceneManager.LoadScene("StartArea", LoadSceneMode.Single);
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
