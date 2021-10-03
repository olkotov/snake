using UnityEngine;

public class SnakePart : MonoBehaviour
{
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    public void UpdateTransform()
    {
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    public Vector3 GetPreviousPosition()
    {
        return previousPosition;
    }

    public Quaternion GetPreviousRotation()
    {
        return previousRotation;
    }
}

