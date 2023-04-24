using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaterialColor : MonoBehaviour
{
    Image image;
    public ItemInfo item;
    Color color;
    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Equipment.instance.itemList.Contains(item))
            image.color = color;
        else
            image.color = Color.red;
    }
}
