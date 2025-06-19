using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float PlayerSpeed = 2f;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer renderer;
    public float JumpForce = 5f;
    public GameObject groundCheck;
    public LayerMask groundLayer;
    public int Score = 0;
    public Text scoreText;
    public int Health = 30;
    public int HeartScore = 10;
    public int TotalHearts = 3;
    public bool IsMovementEnabled = true;
    public bool IsGameOver = false;
    public bool IsWin = false;
    public bool IsDebug = false;
    public Text SignMessageText;
    private bool IsSignMessageShowing = false;
    private bool DoubleJump = false;
    private float OldTime = 0f; // used only for signs

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1; // resume if paused
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerSaveLoad.DataEntry entry = PlayerSaveLoad.Load();
            Health = entry.health;
            Score = entry.score;
            ModifyScore(0); // update text
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer);
    }

    public void ModifyScore(int score)
    {
        Score += score;
        scoreText.text = Score.ToString();
    }

    public void ShowMessage(string Message)
    {
        try
        {
            if (!IsSignMessageShowing || SignMessageText.text != Message)
            {
                OldTime = Time.time; // assing time then show or else bug of instant disappear occurs
                SignMessageText.text = Message;
                IsSignMessageShowing = true;
            }
        } catch(Exception)
        {
            Debug.Log("No sign message text assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (IsMovementEnabled)
            {
                float xAxis = GetXAxis();
                HandleAnim(xAxis);
                Vector2 velocity = rigidbody.linearVelocity;
                velocity.x = xAxis * PlayerSpeed;
                rigidbody.linearVelocity = velocity;
                if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && (IsGrounded() || !DoubleJump))
                {
                    if (!IsGrounded() && !DoubleJump)
                    {
                        DoubleJump = true;
                        Debug.Log("Double jump");
                    }
                    rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, JumpForce);
                }
            }
            if(IsGrounded() && DoubleJump)
            {
                DoubleJump = false;
            }
            if (Health <= 0 && !IsGameOver && !IsDebug)
            {
                IsGameOver = true;
                IsMovementEnabled = false;
                if(IsSignMessageShowing)
                {
                    SignMessageText.text = "";
                }
            }
        }
        if(Time.time - OldTime >= 3f && IsSignMessageShowing)
        {
            IsSignMessageShowing = false;
            SignMessageText.text = "";
        }
    }

    void HandleAnim(float x)
    {
        if(x != 0)
        {
            if (!animator.GetBool("run")) animator.SetBool("run",true);
            if (x > 0) { 
                renderer.flipX = false;
            }
            else if (x < 0) renderer.flipX = true;
        } else
        {
            if (animator.GetBool("run") && IsMovementEnabled) animator.SetBool("run", false);
        }
    }

    float GetXAxis()
    {
        float axis = 0f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            axis = 1f;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            axis = -1f;
        }
        return axis;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyKillZone"))
        {
            collision.gameObject.GetComponentInParent<KillEnemy>().Kill(this);
        }
    }
}
