using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;
    public float minX = -8.5f;     // Gi?i h?n t?a ?? X
    public float maxX = 8.5f;      // Gi?i h?n t?a ?? X
    public float minY = -6.4f;     // Gi?i h?n t?a ?? Y
    public float maxY = 6.4f;

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = targetPosition;
    }
}
