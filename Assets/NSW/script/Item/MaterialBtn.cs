using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaterialBtn : MonoBehaviour
{
    Image image;
    public ItemInfo currentItem;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        if(currentItem != null)
        currentItem.isMaterial = false;
    }
}
