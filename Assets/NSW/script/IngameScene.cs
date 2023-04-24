using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IngameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("IngameUI", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
