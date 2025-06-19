using UnityEngine;

public class CherryScript : MonoBehaviour
{
    public int Score;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerScript p = collision.gameObject.GetComponent<PlayerScript>();
            p.ModifyScore(Score);
            if(p.Health/p.HeartScore<p.TotalHearts)
            {
                p.Health += p.HeartScore;
            }
            Destroy(gameObject);
        }
    }
}
