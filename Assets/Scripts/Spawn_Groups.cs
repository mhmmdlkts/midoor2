using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawn_Groups : MonoBehaviour
{
    public GameObject mobGen;
    public GameObject mainSpawnPoint;
    public GameObject[] spawnPoints;

    private Random rnd;
    void Start()
    {
        rnd = new Random();
    }

    public void creatInARandomPointMob(int id, int healthy, String name, long delay)
    {
        if (rnd.Next() % 2 == 0) 
            mobGen.GetComponent<ENEMY_SPAWN>().newAction(mainSpawnPoint, healthy, name, delay, id);
        else
            mobGen.GetComponent<ENEMY_SPAWN>().newAction(spawnPoints[rnd.Next(0,spawnPoints.Length)], healthy, name, delay, id);
    }
}
