﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class mobGoDesPoint : MonoBehaviour
{
    private Random rnd;
    public GameObject mob, createdMob, desPoint;
    public bool isGoingPick = false, isGoingHide = false, isFiring = false;
    private Queue<int> healthyList;
    public int pendingDoActions;
    public GameObject parrentGroup;
    public int minFireTime = 0;
    public int maxFireTime = 2;

    private float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        healthyList = new Queue<int>();
        pendingDoActions = 0;
        rnd = new Random();
    }

    // Update is called once per frame
    void Update()
    {
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

    public void doFiring()
    {
        
    }

    public void stopFiring()
    {
        hide();
    }
}
