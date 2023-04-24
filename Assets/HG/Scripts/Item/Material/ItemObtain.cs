using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObtain : MonoBehaviour
{
    public MaterialInfo item;

    void OnTriggerEnter2D(Collider2D other)
    {
        Material.instance.Add(item);
        Destroy(gameObject);
    }
}
