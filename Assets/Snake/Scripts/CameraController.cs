using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform snake;
    private float cameraOffset;

    void Start()
    {
        cameraOffset = -5.0f;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, snake.GetChild(0).position.z + cameraOffset);
    }
}

