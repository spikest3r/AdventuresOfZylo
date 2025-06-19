using UnityEngine;

public class FinishMarkScript : MonoBehaviour
{
    public bool IsActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && IsActive)
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            player.IsMovementEnabled = false;
            player.IsWin = true;
            // todo: you win here!
            Debug.Log("Finish!");
        }
    }
}
