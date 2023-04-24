using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public GameObject conversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        conversation.SetActive(true);
        Destroy(gameObject);
    }
}
