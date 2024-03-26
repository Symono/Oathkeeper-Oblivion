using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    // Define camera boundary positions
    public Transform topLeftBoundary;
    public Transform bottomRightBoundary;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate target position
            Vector3 targetPosition = target.position + offset;

            // Clamp camera position within boundaries
            float clampedX = Mathf.Clamp(targetPosition.x, topLeftBoundary.position.x, bottomRightBoundary.position.x);
            float clampedY = Mathf.Clamp(targetPosition.y, bottomRightBoundary.position.y, topLeftBoundary.position.y);
            targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);

            // Smoothly move camera to target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
