using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Minicam : MonoBehaviour
{
    public GameObject player;
    public float CameraSpeed = 3.0f;
    public Vector2 center;
    public Vector2 size;
    float cameraHalfWidth, cameraHalfHeight;
    public RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.aspect * 25;
        cameraHalfHeight = 25;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 0, -10), CameraSpeed * Time.deltaTime);
        float lx = size.x * 0.5f - cameraHalfWidth;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - cameraHalfHeight;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, lx + center.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }
}
