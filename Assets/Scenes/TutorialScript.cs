using Unity.VisualScripting;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public string MessageIntro, MessageMove, MessageEnemy;
    public PlayerScript player;
    public OpossumScript opossumEnemy;
    public FinishMarkScript finish;
    public InfoSignScript info;
    public TeleportSignScript teleport;

    private int TutorialStage = 0;
    private bool IncrementOnSignEnd = false;
    private float OldTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.IsMovementEnabled = false;
        player.IsDebug = true; // prevent game over
        if (info != null) info.Activated = false; else Debug.Log("No info sign attached!");
        if (finish != null) finish.IsActive = false; else Debug.Log("No finish sign attached!");
        if (teleport != null) teleport.IsActive = false; else Debug.Log("No teleport sign attached!");
        player.ShowMessage(MessageIntro);
        IncrementOnSignEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.SignMessageText.text == "" && IncrementOnSignEnd)
        {
            TutorialStage++;
            Debug.Log("Increment tutorial stage " + TutorialStage.ToString());
            IncrementOnSignEnd = false;
            OldTime = Time.time;
        }
        if(Time.time - OldTime >= 1f && TutorialStage == 1)
        {
            Debug.Log("Movement stage. Enabled!");
            player.ShowMessage(MessageMove);
            player.IsMovementEnabled = true;
            IncrementOnSignEnd = true;
            TutorialStage += 1;
            info.Activated = true;
        }
        if(opossumEnemy.IsDestroyed() && TutorialStage == 3)
        {
            player.ShowMessage(MessageEnemy);
            IncrementOnSignEnd = true;
            TutorialStage += 1;
        }
    }
}
