using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseEffect : MonoBehaviour
{
    public AnimationClip poisonAni;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, poisonAni.length);   
    }

   
}
