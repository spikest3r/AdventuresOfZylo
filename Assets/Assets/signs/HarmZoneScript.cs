using UnityEngine;

public class HarmZoneScript : MonoBehaviour
{
    private bool Harmed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !Harmed)
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            player.Health -= player.HeartScore;
            Harmed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Harmed)
        {
            Harmed = false;
        }
    }
}
