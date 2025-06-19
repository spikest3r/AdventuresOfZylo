using UnityEngine;

public class BunnyCollectGoalScript : MonoBehaviour
{
    private int Current = 0;
    public TeleportSignScript door;
    public PlayerScript player;

    [SerializeField] Vector3 PlayerSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door.IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Current == 3 && !door.IsActive)
        {
            door.IsActive = true;
            player.ShowMessage("Yay! You did this! Now you got the key and you can continue!");
            player.ModifyScore(250); // gratitude for work lol
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Bunny"))
        {
            Current += 1;
            Debug.Log("one bunny in!");
        } else if(obj.CompareTag("Player"))
        {
            obj.transform.position = PlayerSpawn;
            Debug.Log("Player fell in");
        }
    }
}
