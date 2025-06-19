using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] MightyBearScript bear;
    [SerializeField] AudioSource audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            audio.Stop();
            bear.Activate();
            Destroy(this.gameObject);
        }
    }
}
