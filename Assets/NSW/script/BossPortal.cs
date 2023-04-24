using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour
{
    public string sceneName; // 다음 씬 이름 
    public float portalStage; // 다음 스테이지 
    float currStage; // 현재 스테이지 
    public bool isOpen = false;
    Transform player;
    private void Awake()
    {
        isOpen = false;
        currStage = PlayerPrefs.GetFloat("CurrStage");
        player = GameObject.Find("Player").transform.Find("Heracles").gameObject.transform;
    }
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform.Find("Heracles").gameObject.transform;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && Mathf.Abs(currStage + 0.1f - portalStage) < 0.01f)  //0번을 누르면 다음 포탈을 플레이어가 있는곳으로 옮기는 치트키 
        {
            this.transform.position = player.position;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOpen)
        {

            if (currStage == 0) //현재 스테이지가 0이면 1스테이지로 이동 
            {
                SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
                SceneManager.LoadScene("UI", LoadSceneMode.Additive);
                PlayerPrefs.SetFloat("CurrStage", 1.0f);
            }
            else if(Mathf.Abs(currStage + 0.1f - portalStage) < 0.01f)//아닌경우 포탈에 쓰여있는 스테이지로 이동 
            {

                PlayerPrefs.SetFloat("PlayerCurrHp", player.GetComponent<Player>().currHp);
                PlayerPrefs.SetFloat("CurrStage", portalStage);
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                SceneManager.LoadScene("UI", LoadSceneMode.Additive);
                PlayerPrefs.SetInt("Buff", 0);

                if (PlayerPrefs.GetFloat("CurrStage") == 1.6f) PlayerPrefs.SetFloat("CurrStage", 2.0f); 
                else if (PlayerPrefs.GetFloat("CurrStage") == 2.6f) PlayerPrefs.SetFloat("CurrStage", 3.0f);
                else if (PlayerPrefs.GetFloat("CurrStage") == 3.6f) PlayerPrefs.SetFloat("CurrStage", 4.0f);
                else if (PlayerPrefs.GetFloat("CurrStage") == 4.6f) PlayerPrefs.SetFloat("CurrStage", 0.0f);
            }




        }
    }
    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.tag == "Player") // 플레이어가 포탈에 접촉하면 isOpen을 트루로 
        {
            if (!isOpen)
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
        if (collision.tag == "Player" && isOpen) // 플레이어가 포탈에서 멀어지면 isOpen을 false로 
        {
            isOpen = false;

        }
    }
}
