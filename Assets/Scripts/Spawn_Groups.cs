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
    public GameObject game;

    private Random rnd;
    void Start()
    {
        game = GameObject.Find("MOVABLE");
        rnd = new Random();
    }

    public void creatInARandomPointMob(int id, int healthy, String name, long delay, Online strategy)
    {
        Online_EX onlineEx;
        int randomPoint = rnd.Next(0, spawnPoints.Length);
        if (strategy == Online.OFFLINE)
        {
            if (randomPoint % 2 == 0)
                mobGen.GetComponent<ENEMY_SPAWN>().newAction(mainSpawnPoint, healthy, name, delay, id);
            else
                mobGen.GetComponent<ENEMY_SPAWN>().newAction(spawnPoints[randomPoint], healthy, name, delay, id);
        }
        else
        {
            onlineEx = game.GetComponent<GameScriptOnline>().getNewOnlineEx();
            if (randomPoint % 2 == 0)
                mobGen.GetComponent<ENEMY_SPAWN>().newAction(mainSpawnPoint, healthy, name, delay, id, strategy, onlineEx);
            else
                mobGen.GetComponent<ENEMY_SPAWN>().newAction(spawnPoints[randomPoint], healthy, name, delay, id, strategy, onlineEx);
        }
    }
}
