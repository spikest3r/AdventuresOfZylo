using UnityEngine;

public class GemScript : MonoBehaviour
{
    public int Score;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().ModifyScore(Score);
            Destroy(gameObject);
        }
    }
}
