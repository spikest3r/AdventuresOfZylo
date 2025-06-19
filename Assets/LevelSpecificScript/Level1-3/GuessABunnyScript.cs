using System.Collections;
using UnityEngine;

public class GuessABunnyScript : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject[] Bunnies;
    public InfoSignScript TriggerSign;
    public GameObject CurtainBlock;
    public float[] BlockStateY;
    public float BlockSpeed = 1;
    public BoxCollider2D BlockBarrier;
    public TeleportSignScript door;
    private float DefaultX = 0f;
    private int selectedBunny;
    private System.Random rnd;

    private int BlockState = 0;
    private bool Move = false;
    private PlayerScript player;
    private bool IsMinigameActive = false;
    private bool Win = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 0;
        DefaultX = CurtainBlock.transform.position.x;
        CurtainBlock.transform.position = new Vector2(DefaultX, BlockStateY[0]); // 0 -close; 1 -open;
        foreach(GameObject bunny in Bunnies)
        {
            BunnyColliderRelay relay = bunny.GetComponent<BunnyColliderRelay>();
            if(relay == null)
            {
                relay = bunny.AddComponent<BunnyColliderRelay>();
            }
            relay.me = i;
            relay.onTriggerEnter2D += HandleBunnyTrigger;
            i += 1;
        }
        rnd = new System.Random();
        player = playerObject.GetComponent<PlayerScript>();
        door.IsActive = false;
    }

    void HandleBunnyTrigger(int self, Collider2D collider)
    {
        if (!Win)
        {
            if (self == selectedBunny)
            {
                player.ShowMessage("That's it! You can defeat the boss now! Good luck!");
                door.IsActive = true;
                Win = true;
                StartCoroutine(Destruct());
            }
            else
            {
                player.ShowMessage("Try again...");
                playerObject.transform.position = new Vector2(65, playerObject.transform.position.y);
                BlockState = 0;
                Move = true;
                BlockBarrier.isTrigger = false;
                StartCoroutine(BlockMovementCountDown());
            }
        }
    }

    void GenerateBunny()
    {
        selectedBunny = rnd.Next(0, Bunnies.Length);
        Debug.Log("Bunny: " + selectedBunny.ToString());
    }

    IEnumerator BlockMovementCountDown()
    {
        GenerateBunny();
        yield return new WaitForSeconds(3f);
        BlockState = 1;
        Move = true;
        BlockBarrier.isTrigger = true;
    }

    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(1f);
        foreach(GameObject bunny in Bunnies)
        {
            bunny.GetComponent<Animator>().SetBool("Kill", true); // beautiful disappear and destroy
        }
    }

    private void Update()
    {
        if(Move)
        {
            CurtainBlock.transform.position += new Vector3(0f, BlockSpeed * Time.deltaTime * (BlockState == 1 ? 1 : -1));
            if(BlockState == 1 ? CurtainBlock.transform.position.y >= BlockStateY[BlockState] : CurtainBlock.transform.position.y <= BlockStateY[BlockState])
            {
                Move = false;
            }
        }
        if(TriggerSign.IsReadAtLeastOnce && !IsMinigameActive)
        {
            IsMinigameActive = true;
            TriggerSign.Activated = false;
            StartCoroutine(BlockMovementCountDown()); // activate minigame
        }
    }
}
