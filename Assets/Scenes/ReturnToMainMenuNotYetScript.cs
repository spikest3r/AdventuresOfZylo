using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMainMenuNotYetScript : MonoBehaviour
{
    public Button btn;

    private void Click()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        btn.onClick.AddListener(Click);
    }
}
