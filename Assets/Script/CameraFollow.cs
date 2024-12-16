using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;
    public float lerpSpeed = 0.5f;

    private float minX = -100f;     // Gi?i h?n t?a ?? X
    private float maxX = 100f;      // Gi?i h?n t?a ?? X
    private float minY = -100f;     // Gi?i h?n t?a ?? Y
    private float maxY = 100f;

    //private void Start()
    //{
    //    if (player == null) return;

    //    offset = transform.position - player.position;
    //}

    //void LateUpdate()
    //{
    //    Vector3 targetPosition = player.position + offset;

    //    targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
    //    targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

    //    transform.position = targetPosition;
    //}

    private void Update()
    {
        if (player == null) return;
        Vector3 targetPosition = player.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
