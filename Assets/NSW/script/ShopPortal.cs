using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShopPortal : MonoBehaviour
{
    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOpen)
        {

            float stage = PlayerPrefs.GetFloat("CurrStage"); // 현재 스테이지 읽어오기 
            Debug.Log(stage); // 디버그용 확인 
            if (Mathf.Abs(stage - 1.1f) <= 0.01) SceneManager.LoadScene("Boss Stage1", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 1.3f) <= 0.01) SceneManager.LoadScene("Boss Stage2", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 1.5f) <= 0.01) SceneManager.LoadScene("Boss Stage3", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 2.1f) <= 0.01) SceneManager.LoadScene("2-1", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 2.3f) <= 0.01) SceneManager.LoadScene("2-2", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 2.5f) <= 0.01) SceneManager.LoadScene("2-3", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 3.1f) <= 0.1f) SceneManager.LoadScene("Crete", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 3.3f) <= 0.1f) SceneManager.LoadScene("Diomedes", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 3.5f) <= 0.1f) SceneManager.LoadScene("Hippolyta", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 4.1f) <= 0.1f) SceneManager.LoadScene("4-1boss", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 4.3f) <= 0.1f) SceneManager.LoadScene("4-2boss", LoadSceneMode.Single);

            else if (Mathf.Abs(stage - 4.5f) <= 0.1f) SceneManager.LoadScene("4-3boss", LoadSceneMode.Single);


            SceneManager.LoadScene("UI", LoadSceneMode.Additive); // UI씬 로드 


        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isOpen)
        {
            isOpen = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isOpen)
        {
            isOpen = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isOpen)
        {
            isOpen = false;

        }
    }
}
