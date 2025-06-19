using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public PlayerScript player;
    private bool IsShowing = false;
    private Vector3 visibleScale;
    public Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visibleScale = transform.localScale;
        transform.localScale = Vector3.zero;
        button.onClick.AddListener(Click);
    }

    void Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.IsGameOver && !IsShowing)
        {
            transform.localScale = visibleScale;
            IsShowing = true;
        }
        if(IsShowing)
        {
            if(Keyboard.current.enterKey.IsPressed())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
