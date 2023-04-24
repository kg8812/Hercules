using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
//using UnityEngine.SocialPlatforms.GameCenter;

public class CameraMove : MonoBehaviour
{
    
    public GameObject player;
    public float CameraSpeed = 3f;
    public Vector2 center;
    public Vector2 size;
    float cameraHalfWidth, cameraHalfHeight;
    public bool isJump = false;
    Vector3 shakePos;


    void Start()
    {
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
        
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(Shake());
        }

        if (Vector2.Distance(transform.position, player.transform.position) > 40)
        {
            CameraSpeed = 4f;
        }
        else
            CameraSpeed = 1.5f;

        transform.position = Vector3.Lerp(transform.position, player.transform.position, CameraSpeed * Time.deltaTime)+shakePos;

        float lx = size.x * 0.5f - cameraHalfWidth;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - cameraHalfHeight;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }

    public IEnumerator Shake()
    {

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.05f);
            shakePos = Vector2.up / 10;
            yield return new WaitForSeconds(0.05f);
            shakePos = Vector2.down/10;
        }
        shakePos = Vector2.zero;
    }
}
