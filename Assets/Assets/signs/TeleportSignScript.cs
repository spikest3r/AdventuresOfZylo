using UnityEngine;

public class TeleportSignScript : MonoBehaviour
{
    public Vector3 Destination;
    public bool IsActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && IsActive)
        {
            collision.gameObject.transform.position = Destination;
        }
    }
}
