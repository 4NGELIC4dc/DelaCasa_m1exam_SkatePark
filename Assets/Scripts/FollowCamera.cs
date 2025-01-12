using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 10, -100); // Adjusted for third-person view
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + player.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(player.position + Vector3.up * 1.5f); // Slightly above player for better view
        }
    }
}
