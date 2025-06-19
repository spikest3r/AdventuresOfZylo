using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public Sprite[] heartStates;
    public GameObject target;
    private List<SpriteRenderer> hearts;
    private int OldHealth = 0;
    private PlayerScript player;

    private void UpdateHealth()
    {
        for (float i = 1f; i < player.TotalHearts + 1; i += 1f)
        {
            if (i <= player.Health / player.HeartScore)
            {
                hearts[(int)i - 1].sprite = heartStates[0];
            }
            else
            {
                hearts[(int)i - 1].sprite = heartStates[1];
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = target.GetComponent<PlayerScript>();
        OldHealth = player.Health;
        hearts = GetComponentsInChildren<SpriteRenderer>().ToList<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Health != OldHealth)
        {
            OldHealth = player.Health;
            UpdateHealth();
        }
    }
}
