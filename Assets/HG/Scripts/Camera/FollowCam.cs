using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject player;
    public float CameraSpeed = 5.0f;
    public Vector2 center;
    public Vector2 size;
    float cameraHalfWidth, cameraHalfHeight;

    public Transform[] cameraPos;
    Vector2 target;
    int num = 0;

    Vector2 dir;
    void Start()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").gameObject;
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
        target = cameraPos[num].position;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {
        

        Vector2 playerPos = player.transform.position;
        Vector2 curPos = transform.position;       
        float ms = CameraSpeed * Time.deltaTime;
        Vector2 p = Vector2.MoveTowards(curPos, target, ms);

        if (playerPos.x + 15> curPos.x)
        {
            transform.position = new Vector3(p.x, p.y, -10);
        }
        if (curPos == target && num < cameraPos.Length - 1)
        {
            num++;
            target = cameraPos[num].position;
        }

        float lx = size.x * 0.5f - cameraHalfWidth;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - cameraHalfHeight;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }
}
