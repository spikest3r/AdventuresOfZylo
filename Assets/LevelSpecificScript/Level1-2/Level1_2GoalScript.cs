using Unity.VisualScripting;
using UnityEngine;

public class Level1_2GoalScript : MonoBehaviour
{
    public TeleportSignScript TeleportDoor, TeleportDoor2;
    public PlayerScript player;
    public BunnyScript bunny;
    public FinishMarkScript FinishMark;
    private int ThisLevelScore = 0;
    private int OldScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TeleportDoor.IsActive = false;
        TeleportDoor2.IsActive = false;
        FinishMark.IsActive = false;
        OldScore = PlayerSaveLoad.Load().score; // load score directly because delay between player load and this script init may lead to bug where ThisLevelScore = player.Score
    }

    // Update is called once per frame
    void Update()
    {
        if(ThisLevelScore >= 130 && !TeleportDoor.IsActive)
        {
            TeleportDoor.IsActive = true;
            player.ShowMessage("The door has been unlocked! Get the key!");
        }
        if(bunny.IsDestroyed() && !FinishMark.IsActive)
        {
            FinishMark.IsActive = true;
            TeleportDoor2.IsActive = true;
            player.ShowMessage("Now you have the key and you can end this level! Congrats!");
        }
        if(OldScore != player.Score)
        {
            ThisLevelScore += player.Score - OldScore;
            Debug.Log(ThisLevelScore);
            OldScore = player.Score;
        }
    }
}
