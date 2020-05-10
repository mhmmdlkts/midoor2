﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawn_Groups : MonoBehaviour
{
    public GameObject mobGen;
    public GameObject[] spawnPoints;

    private Random rnd;
    void Start()
    {
        rnd = new Random();
    }

    public void creatInARandomPointMob(int healthy, String name)
    {
        mobGen.GetComponent<CT_SPAWN>().newAction(spawnPoints[rnd.Next(0,spawnPoints.Length)], healthy, name);
    }
}
