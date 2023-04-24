using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoarBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetFloat("PlayerCurrHp", collision.GetComponent<Player>().currHp);
            SceneManager.LoadScene("2-1", LoadSceneMode.Single);
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}
