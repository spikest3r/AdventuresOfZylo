using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
    private bool IsShowing = false;
    private Vector3 visibleScale;
    public SpriteRenderer fadeObject;
    private bool fade = false;
    public AudioSource audio;
    public Button resume, restart, exit;
    private int index;

    void ResumeBtn()
    {
        IsShowing = false;
        transform.localScale = Vector3.zero;
        Time.timeScale = 1;
    }

    void RestartBtn()
    {
        fade = true;
        transform.localScale = Vector3.zero;
        index = SceneManager.GetActiveScene().buildIndex;
    }

    void ExitBtn()
    {
        fade = true;
        transform.localScale = Vector3.zero;
        index = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visibleScale = transform.localScale;
        transform.localScale = Vector3.zero;
        resume.onClick.AddListener(ResumeBtn);
        restart.onClick.AddListener(RestartBtn);
        exit.onClick.AddListener(ExitBtn);
    }

    void Switch()
    {
        if (IsShowing)
        {
            IsShowing = false;
            transform.localScale = Vector3.zero;
            Time.timeScale = 1;
        }
        else
        {
            IsShowing = true;
            transform.localScale = visibleScale;
            Time.timeScale = 0;
        }
    }

    // Update is callved once per frame

    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Switch();
        }
        if (fade)
        {
            if (fadeObject.color.a < 1f)
            {
                Color c = fadeObject.color;
                c.a += 0.05f;
                fadeObject.color = c;
                audio.volume = 1f - c.a;
            }
            else
            {
                if (fadeObject.color.a >= 1f)
                {
                    SceneManager.LoadScene(index);
                }
            }
        }
    }
}
