using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MightyBearScript : MonoBehaviour
{
    public PlayerScript player;
    public int MaxOpossum = 15;
    public Vector3 OpossumSpawnLocation, MissileSpawnLocation;
    public float MissileMinY, MissileMaxY;
    public float OpossumSpeed = 3f;
    public GameObject Opossum, Missile;
    bool Win = false;

    Animator anim;

    public FinishMarkScript finish;
    private bool Running = false;
    
    IEnumerator WaitBeforeRun()
    {
        yield return new WaitForSeconds(3f);
        Running = true;
        StartCoroutine(OpossumGen());
        // StartCoroutine(MissileGen()); missiles are shitty ngl
    }

    IEnumerator OpossumGen()
    {
        yield return new WaitForSeconds(Random.Range(2f,4f));
        GameObject instance = Opossum;
        instance.GetComponent<OpossumScript>().Speed = OpossumSpeed;
        instance.transform.position = OpossumSpawnLocation;
        Instantiate(instance);
        if(!Win)
        {
            StartCoroutine(OpossumGen());
        }
    }

    IEnumerator MissileGen()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.6f));
        GameObject instance = Missile;
        instance.GetComponent<Missile>().Speed = 5f;
        Vector3 spawn = MissileSpawnLocation;
        spawn.y = Random.Range(MissileMinY, MissileMaxY);
        instance.transform.position = spawn;
        Instantiate(instance);
        if (!Win)
        {
            StartCoroutine(MissileGen());
        }
    }

    public void Activate()
    {
        player.ShowMessage("Hello, Zylo! If you think you're that cool and strong, defeat me! And then we will talk!");
        StartCoroutine(WaitBeforeRun());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        finish.IsActive = false;
    }

    private void Update()
    {
        if(anim.GetBool("Kill"))
        {
            player.ShowMessage("You maybe won this battle, but definitely not the war!");
            Win = true;
            Running = false;
            finish.IsActive = true;
        }
    }
}
