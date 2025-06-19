using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinHandler : MonoBehaviour
{
    public PlayerScript player;
    private bool IsShowing = false;
    private Vector3 visibleScale;
    public SpriteRenderer fadeObject;
    private bool fade = false;
    public AudioSource audio;
    public Button button;
    public bool SavePlayerData = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visibleScale = transform.localScale;
        transform.localScale = Vector3.zero;
        button.onClick.AddListener(Click);
    }

    void Click()
    {
        if (SavePlayerData)
        {
            PlayerSaveLoad.DataEntry entry;
            entry.score = player.Score;
            entry.health = player.Health;
            PlayerSaveLoad.Save(entry);
        } else
        {
            PlayerSaveLoad.DataEntry entry;
            entry.score = 0;
            entry.health = 0;
            PlayerSaveLoad.Save(entry);
        }
        fade = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsWin && !IsShowing)
        {
            transform.localScale = visibleScale;
            IsShowing = true;
        }
        if (IsShowing)
        {
            if (Keyboard.current.enterKey.IsPressed() && !fade)
            {
                Click();
            }
        }
        if(fade)
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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
