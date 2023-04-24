using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class StageManager : MonoBehaviour
{
    public int stage;
    public Transform player;
    GameObject miniMap;
    public Texture[] miniTexture;
   
    // Start is called before the first frame update
   
    void Start()
    {
        miniMap = GameObject.FindGameObjectWithTag("MiniMap");
        if (miniMap != null)
        {
            miniMap.GetComponent<RawImage>().texture = miniTexture[stage - 1];         
        }
        else
        {
            Debug.Log(stage - 1);
        }
        
        
    }

}
