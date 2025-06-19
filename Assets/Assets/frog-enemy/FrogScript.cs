using System.Collections;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    public int Harm = 10;
    public int HarmCoolDown = 5;
    private bool IsHarmed = false;
    private GameObject player;
    private IEnumerator WaitForNextHarm()
    {
        Debug.Log("Waiting for next harm");
        yield return new WaitForSeconds((float)HarmCoolDown);
        IsHarmed = false;
        Debug.Log("Ready to harm again");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void Update()
    {
        if (!IsHarmed && player != null && Time.timeScale == 1)
        {
            Debug.Log("Harming!");
            PlayerScript script = player.GetComponent<PlayerScript>();
            script.Health -= Harm;
            Debug.Log(script.Health);
            IsHarmed = true;
            StartCoroutine(WaitForNextHarm());
        }
    }
}
