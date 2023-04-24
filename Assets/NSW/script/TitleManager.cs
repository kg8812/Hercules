using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject newGameWindow;    //새로 시작창
    public void OpenNewGame()   //새로시작창 열기
    {
        newGameWindow.SetActive(true);
    }

    public void CloseNewGame()  //새로시작창 닫기
    {
        newGameWindow.SetActive(false);
    }
    public void NewGame()   //새로 시작
    {
        SceneManager.LoadScene("StartArea", LoadSceneMode.Single);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        GameManager.Reset();
        ItemManager.instance.NewGame();
        SkillInven.instance.NewGame();
    }
    public void ToIntro() // Intro로 이동
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
    public void LoadGame() //게임 로드 (이어서하기)
    {
        float stage = PlayerPrefs.GetFloat("CurrStage");
        ItemManager.instance.Load();

        if (stage >= 4.0f)
        {
            SceneManager.LoadScene("4stage", LoadSceneMode.Single);
            if (Mathf.Abs(stage - 4.1f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 4.0f);
            else if (Mathf.Abs(stage - 4.3f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 4.2f);
            else if (Mathf.Abs(stage - 4.5f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 4.4f);
        }
        else if (stage >= 3.0f)
        {
            SceneManager.LoadScene("Stage 3", LoadSceneMode.Single);
            if (Mathf.Abs(stage - 3.1f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 3.0f);
            else if (Mathf.Abs(stage - 3.3f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 3.2f);
            else if (Mathf.Abs(stage - 3.5f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 3.4f);
        }
        else if (stage >= 2.0f)
        {
            SceneManager.LoadScene("2stage", LoadSceneMode.Single);
            if (Mathf.Abs(stage - 2.1f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 2.0f);
            else if (Mathf.Abs(stage - 2.3f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 2.2f);
            else if (Mathf.Abs(stage - 2.5f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 2.4f);

        }
        else if (stage >= 1.0f)
        {
            SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
            if (Mathf.Abs(stage - 1.1f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 1.0f);
            else if (Mathf.Abs(stage - 1.3f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 1.2f);
            else if (Mathf.Abs(stage - 1.5f) <= 0.1f)
                PlayerPrefs.SetFloat("CurrStage", 1.4f);
        }
        else
        {
            Debug.Log("저장된 데이터 없음");
            return;
        }

        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

}
