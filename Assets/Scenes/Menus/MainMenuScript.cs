using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button startGameButton;
    public GameObject player;
    public float speed = 0.2f;
    private bool run = false;
    private int animStage = -1;
    private float timerTarget = 0;
    private float startTime = 0;
    private Animator animator;
    public AudioSource backgroundMusic;
    public GameObject fadeObject;
    private SpriteRenderer fade;
    public float fadeSpeed = 0.05f;
    public bool Transition = false;
    public Text PlayButtonText;
    private bool deleting = false;
    private int levelToLoad = 1;

    private void startGame()
    {
        if (!deleting)
        {
            Debug.Log("starting game");
            startGameButton.gameObject.transform.localScale = Vector3.zero;
            run = true;
            animStage = 0;
            timerTarget = 1f;
            startTime = Time.time;
        } else
        {
            PlayerPrefs.DeleteAll();
            deleting = false;
            PlayButtonText.text = PlayerSaveLoad.HasPlayerPlayed() == 1 ? "Continue game" : "Start game";
            levelToLoad = PlayerSaveLoad.HasPlayerPlayed() == 1 ? PlayerSaveLoad.GetLastLevel() : 1;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerScript script = player.GetComponent<PlayerScript>();
        script.IsMovementEnabled = false;
        startGameButton.onClick.AddListener(startGame);
        animator = player.GetComponent<Animator>();
        Debug.Log(animator);
        fade = fadeObject.GetComponent<SpriteRenderer>();
        PlayButtonText.text = PlayerSaveLoad.HasPlayerPlayed() == 1 ? "Continue game" : "Start game";
        levelToLoad = PlayerSaveLoad.HasPlayerPlayed() == 1 ? PlayerSaveLoad.GetLastLevel() : 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animStage == 0 && Time.time - startTime >= timerTarget)
        {
            animStage = 1;
            startTime = Time.time;
            animator.SetBool("run", true);
        } else if (player.transform.position.x < 10f && animStage == 1)
        {
            player.transform.position += new Vector3(speed, 0f, 0f);
        } else if(player.transform.position.x >= 10f && animStage == 1)
        {
            animStage = 2;
            animator.SetBool("run", false);
            startTime = Time.time;
            timerTarget = 1;
        } else if(animStage == 2 && Time.time - startTime >= timerTarget)
        {
            animStage = 3;
        } else if(animStage == 3 && fade.color.a < 1)
        {
            Color c = fade.color;
            c.a += fadeSpeed;
            fade.color = c;
            backgroundMusic.volume -= fadeSpeed;
        } else if(animStage == 3 && fade.color.a >= 1)
        {
            animStage = 4;
        } else if(animStage == 4 && !Transition)
        {
            SceneManager.LoadScene(levelToLoad);
            Transition = true;
        }
    }

    IEnumerator DeleteCancelCountdown()
    {
        yield return new WaitForSeconds(3);
        deleting = false;
        PlayButtonText.text = PlayerSaveLoad.HasPlayerPlayed() == 1 ? "Continue game" : "Start game";
    }

    private void Update()
    {
        Keyboard state = Keyboard.current;
        if (state.sKey.isPressed && state.deleteKey.isPressed && state.enterKey.isPressed && state.qKey.isPressed)
        {
            PlayButtonText.text = "Confirm delete save data";
            deleting = true;
            StartCoroutine(DeleteCancelCountdown());
        }
    }
}
