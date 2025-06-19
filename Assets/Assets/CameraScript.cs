using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; // the player
    public Vector2 deadZoneSize = new Vector2(3f, 2f); // how far player can move before camera follows

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 cameraPos = transform.position;
        Vector3 playerPos = target.position;

        // Calculate camera boundaries
        float leftBound = cameraPos.x - deadZoneSize.x / 2f;
        float rightBound = cameraPos.x + deadZoneSize.x / 2f;
        float bottomBound = cameraPos.y - deadZoneSize.y / 2f;
        float topBound = cameraPos.y + deadZoneSize.y / 2f;

        Vector3 newCameraPos = cameraPos;

        // Check X bounds
        if (playerPos.x < leftBound)
            newCameraPos.x = playerPos.x + deadZoneSize.x / 2f;
        else if (playerPos.x > rightBound)
            newCameraPos.x = playerPos.x - deadZoneSize.x / 2f;

        // (Optional) Check Y bounds if you want vertical scrolling
        if (playerPos.y < bottomBound)
            newCameraPos.y = playerPos.y + deadZoneSize.y / 2f;
        else if (playerPos.y > topBound)
            newCameraPos.y = playerPos.y - deadZoneSize.y / 2f;

        // You can lerp it for soft movement, or just snap for that classic NES stiffness
        transform.position = Vector3.Lerp(transform.position, newCameraPos, 0.25f);
    }
}
