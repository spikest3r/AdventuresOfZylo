using System;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public int Score = 20;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;

    private void Start()
    {
        enemyCollider = GetComponentInParent<Collider2D>();
        try
        {
            rb = GetComponentInParent<Rigidbody2D>();
        } catch(Exception _)
        {
            rb = null; // enemy doesnt have rb
        }
    }

    public void Kill(PlayerScript player)
    {
        GetComponentInParent<Animator>().SetBool("Kill", true);
        try
        {
            rb.simulated = false; // disable
        }
        catch (Exception _) { } // no rb so no action
        enemyCollider.isTrigger = true;
        player.ModifyScore(Score);
    }
}
