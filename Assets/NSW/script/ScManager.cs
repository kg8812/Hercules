using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScManager : MonoBehaviour
{

    static public ScManager instace;
    private void Awake()
    {
        instace = this;
    }
    public void Start2staage()
    {
        SceneManager.LoadScene("2stage", LoadSceneMode.Single);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        PlayerPrefs.SetFloat("CurrStage", 2.0f);

    }
    public void Start3staage()
    {
        SceneManager.LoadScene("Stage 3", LoadSceneMode.Single);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        PlayerPrefs.SetFloat("CurrStage", 3.0f);


    }
}
