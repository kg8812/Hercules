using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFall : MonoBehaviour
{
    public GameObject blade;
    bool isFire = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFire)
        {
            StartCoroutine(FallBlade());
        }
    }
    IEnumerator FallBlade()
    {
        isFire = true;
        Instantiate(blade, this.transform);
        yield return new WaitForSeconds(2.5f);
        isFire = false;
    }
}
