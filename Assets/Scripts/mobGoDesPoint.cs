using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class mobGoDesPoint : MonoBehaviour
{
    private Random rnd;
    public GameObject mob, createdMob, desPoint, mainObject;
    public bool isGoingPick = false, isGoingHide = false, isFiring = false;
    private Queue<int> healthyList;
    public int pendingDoActions, thisLokkingPos;
    public GameObject parrentGroup;
    public int minFireTime = 0;
    public int maxFireTime = 2;
    private AudioSource audioSource;
    public AudioClip[] hitted;
    public AudioClip[] misHitted;
    public int hitChance;
    
    private float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        mainObject = GameObject.Find("MOVABLE");
        audioSource = gameObject.GetComponent<AudioSource>();
        healthyList = new Queue<int>();
        pendingDoActions = 0;
        rnd = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameScript.isStoped)
        {
            return;
        }

        if (pendingDoActions > 0 && !isGoingHide && !isGoingPick && !isFiring)
        {
            doAction();
        }
        
        if (isGoingPick)
        {
            goPicking();
        }

        if (isGoingHide)
        {
            goHiding();
        }

        if (isFiring)
        {
            doFiring();
        }
    }

    public void doActionLater(float waitTime)
    {
        Invoke("doAction", waitTime);
    }

    public void newAction(int healthy)
    {
        pendingDoActions++;
        healthyList.Enqueue(healthy);
    }

    public void doAction()
    {
        pendingDoActions--;
        spawnMob(healthyList.Dequeue());
        pick();
    }

    public void spawnMob(int healthy)
    {
        createdMob = Instantiate(mob, gameObject.transform.position, gameObject.transform.rotation);
        createdMob.GetComponent<enemy>().setHealty(healthy);
    }

    public void goPicking()
    {
        if(createdMob == null)
        {
            stop();
            return;
        }
        if (createdMob.GetComponent<Transform>().position == desPoint.GetComponent<Transform>().position)
        {
            startFiring();
            return;
        }
        
        float step =speed*Time.deltaTime;
        createdMob.transform.position = Vector3.MoveTowards(createdMob.GetComponent<Transform>().position, desPoint.GetComponent<Transform>().position, step);
    }

    public void goHiding()
    {
        if(createdMob == null)
        {
            stop();
            return;
        }
        if (createdMob.GetComponent<Transform>().position == gameObject.GetComponent<Transform>().position)
        {
            changePoint();
        }
        float step =speed*Time.deltaTime;
        createdMob.transform.position = Vector3.MoveTowards(createdMob.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position, step);
    }

    public void changePoint()
    {
        int healthy = createdMob.GetComponent<enemy>().healthy;
        Destroy(createdMob);
        parrentGroup.GetComponent<Spawn_Groups>().creatInARandomPointMob(healthy);   
    }

    public void pick()
    {
        isGoingPick = true;
        isGoingHide = false;
        isFiring = false;
    }

    public void hide()
    {
        isGoingPick = false;
        isGoingHide = true;
        isFiring = false;
    }

    public void stop()
    {
        isGoingPick = false;
        isGoingHide = false;
        isFiring = false;
    }

    public void startFiring()
    {
        stop();
        isFiring = true;
        float fireTime = Convert.ToSingle(rnd.Next(minFireTime * 1000, maxFireTime * 1000)) / 1000;
        Invoke("stopFiring", fireTime);
    }

    private long nextFire = 0;
    public void doFiring()
    {
        if(nextFire < CurrentTimeMillis())
            fire();
    }

    public void fire()
    {
        nextFire = CurrentTimeMillis() + 1000;
        Debug.Log("FIRE!!!!");
        bool hit = rnd.Next(0, 100) < hitChance;
        if (createdMob != null)
            createdMob.GetComponent<enemy>().playFireParticle();
        if (hit && thisLokkingPos == GameScript.isLokingIn)
        {
            audioSource.clip = hitted[rnd.Next(0, hitted.Length)];
            giveDamage();
        }
        else
        {
            audioSource.clip = misHitted[rnd.Next(0, misHitted.Length)];
        }
        audioSource.Play();
    }

    public void giveDamage()
    {
        GameObject.Find("Canvas").GetComponent<BloodieScreen>().showBlood();
        int damage = 0;
        switch (thisLokkingPos)
        {
            case 0: damage = 27; break;
            case 1: damage = 350; break;
            case 2: damage = 26; break;
        }
        mainObject.GetComponent<GameScript>().givePlayerDamage(damage);
    }

    public void stopFiring()
    {
        hide();
    }

    public long CurrentTimeMillis()
    { 
        DateTime Jan1st1970 = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }
}
