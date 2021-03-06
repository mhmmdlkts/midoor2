﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class mobGoDesPoint : MonoBehaviour
{
    private Random rnd;
    public GameObject mob, createdMob, desPoint, firePoint;
    private GameObject mainObject;
    public bool isGoingPick = false, isGoingHide = false, isFiring = false;
    private Queue<Action> actionList;
    public int pendingDoActions, thisLokkingPos;
    public GameObject parrentGroup;
    public int minFireTime = 0;
    public int maxFireTime = 2;
    private AudioSource audioSource;
    public AudioClip[] hitted;
    public AudioClip[] misHitted;
    public float arriveDelay;
    
    public float localMoveSpeed;
    public static float globalMoveSpeed = 1.5f;
    
    public float localFireSpeed;
    public static float globalFireSpeed = 0.5f;
    
    public float localHitChance;
    public static float globalHitChance = 15.0f;
    
    void Start()
    {
        mainObject = GameObject.Find("MOVABLE");
        audioSource = gameObject.GetComponent<AudioSource>();
        actionList = new Queue<Action>();
        pendingDoActions = 0;
        rnd = new Random();
    }
    
    void Update()
    {
        if (GameScript.isStoped)
        {
            return;
        }

        if (createdMob == null)
        {
            stop();
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
            if(mainObject.GetComponent<GameScript>().isOnline)
                return;
            doFiring();
        }
    }

    private float getMoveSpeed()
    {
        return localMoveSpeed * globalMoveSpeed * rankMultiply(1);
    }

    private int getNextFireInMilliSec()
    {
        return (int)(localFireSpeed * globalFireSpeed * rankMultiply(1) * 1000);
    }

    private float getFireTime()
    {
        return Convert.ToSingle(rnd.Next(minFireTime * 1000, maxFireTime * 1000)) / 1000;
    }

    private int getHitChance()
    {
        return (int)(localHitChance * globalHitChance + rankMultiply(1.5f));
    }

    private int getHitHeadChance()
    {
        return getHitChance();
    }

    private float rankMultiply(float multply)
    {
        return (((float)GameScript.rank / (float)GameScript.tot_rank * 3) + 1) * multply;
    }

    public void doActionLater(float waitTime)
    {
        Invoke(nameof(doAction), waitTime);
    }

    public void newAction(Action action)
    {
        pendingDoActions++;
        action.addDelay(arriveDelay);
        actionList.Enqueue(action);
    }

    public void doAction()
    {
        pendingDoActions--;
        spawnMob(actionList.Dequeue());
        pick();
    }

    public void spawnMob(Action action)
    {
        createdMob = Instantiate(mob, gameObject.transform.position, gameObject.transform.rotation);
        createdMob.transform.localScale = gameObject.transform.localScale;
        createdMob.GetComponent<enemy>().setEnemy(action);
    }

    public void goPicking()
    {
        if(createdMob == null)
        {
            stop();
            return;
        }
        if (createdMob.GetComponent<Transform>().position == firePoint.GetComponent<Transform>().position)
        {
            startFiring();
            return;
        }
        
        float step =getMoveSpeed()*Time.deltaTime;
        createdMob.transform.position = Vector3.MoveTowards(createdMob.GetComponent<Transform>().position, firePoint.GetComponent<Transform>().position, step);
    }

    public void goHiding()
    {
        if(createdMob == null)
        {
            stop();
            return;
        }
        if (createdMob.GetComponent<Transform>().position == desPoint.GetComponent<Transform>().position)
        {
            changePoint();
        }
        float step =getMoveSpeed()*Time.deltaTime;
        createdMob.transform.position = Vector3.MoveTowards(createdMob.GetComponent<Transform>().position, desPoint.GetComponent<Transform>().position, step);
    }

    public void fire()
    {
        nextFire = CurrentTimeMillis() + getNextFireInMilliSec();
        bool hit = rnd.Next(0, 100) < getHitChance();
        if (createdMob != null)
            createdMob.GetComponent<enemy>().playFireParticle();
        if (hit && thisLokkingPos == GameScript.isLokingIn)
        {
            bool hitHead = rnd.Next(0, 100) < getHitHeadChance();
            if (hitHead)
            {
                audioSource.clip = hitted[rnd.Next(0, hitted.Length)]; // TODO different Head Voice
                giveDamage(true);
            }
            else
            {
                audioSource.clip = hitted[rnd.Next(0, hitted.Length)];
                giveDamage(false);
            }
        }
        else
        {
            audioSource.clip = misHitted[rnd.Next(0, misHitted.Length)];
        }
        audioSource.Play();
    }

    public void changePoint()
    {
        int healthy = createdMob.GetComponent<enemy>().healthy;
        int id = createdMob.GetComponent<enemy>().id;
        String name = createdMob.GetComponent<enemy>().name;
        Destroy(createdMob);
        parrentGroup.GetComponent<Spawn_Groups>().creatInARandomPointMob(id, healthy, name, (long)(arriveDelay*1000),mainObject.GetComponent<GameScript>().strategy);   
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
        float fireTime = getFireTime();
        Invoke(nameof(stopFiring), fireTime);
    }

    private long nextFire = 0;
    public void doFiring()
    {
        if(nextFire < CurrentTimeMillis())
            fire();

        if (createdMob != null && createdMob.GetComponent<enemy>().isFiring)
        {
            
        }
    }

    public void giveDamage(bool isHead)
    {
        GameObject.Find("Canvas").GetComponent<BloodieScreen>().showBlood();
        GameScript game = mainObject.GetComponent<GameScript>();
        int damage = 0;
        int weaponCode = 0;
        switch (thisLokkingPos)
        {
            case 0: 
                damage = isHead?88:27;
                weaponCode = game.isT ? deathInfo.M4A4_CODE : deathInfo.AK47_CODE;;
                break;
            case 1:
                damage = isHead?350:85;
                weaponCode = deathInfo.AWP_CODE;
                break;
            case 2:
                damage = isHead?84:26;
                weaponCode = game.isT ? deathInfo.M4A4_CODE : deathInfo.AK47_CODE;
                break;
            case 3:
                damage = 110;
                weaponCode = deathInfo.KNIFE_CODE;
                break;
        }
        mainObject.GetComponent<GameScript>().givePlayerDamage(damage, weaponCode, isHead, createdMob);
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
