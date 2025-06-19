using UnityEngine;

public class InfoSignScript : MonoBehaviour
{
    // editor vars
    [SerializeField] string Message;
    [SerializeField] int ReadLimit = -1; // -1 means unlim, 0 means never

    public bool IsReadAtLeastOnce { get; private set; }
    public bool Activated = true;
    int TimesRead = 0;

    bool LimitNotReached()
    {
        return ReadLimit == -1 || TimesRead < ReadLimit;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Activated && LimitNotReached())
        {
            collision.gameObject.GetComponent<PlayerScript>().ShowMessage(Message);
            IsReadAtLeastOnce = true;
            TimesRead++;
        }
    }
}
