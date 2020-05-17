using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public struct Action
{
    public long executeTime;
    public GameObject spawnPoint;
    public int healthy;
    public String name;
    public int id;
    public Action(long executeTime, GameObject spawnPoint, int healthy, String name, int id)
    {
        this.executeTime = executeTime;
        this.spawnPoint = spawnPoint;
        this.healthy = healthy;
        this.name = name;
        this.id = id;
    }

    public void addDelay(float delay)
    {
        executeTime += (long)(delay*1000);
    }
}

public class ENEMY_SPAWN : MonoBehaviour
{
    public Queue<Action> actions;
    public int actionsSize;
    public GameObject enemy, main;
    private GameObject[] enemyClone;
    private long lastExecutionTime;
    public GameObject[] spawnPointGroups;
    public int firstHealthy = 100;
    private List<String> enemysNameList;
    public float firstMobWaitTime;

    Random rnd;

    public int minWaitTime = 1, maxWaitTime = 3;
    private int PLAYERS_COUNT = 5;

    void Start()
    {
        rnd = new Random();
        actions = new Queue<Action>();
        enemyClone = new GameObject[PLAYERS_COUNT];
        enemysNameList = new List<String>();
        initEnemysFirstNameList(PLAYERS_COUNT);
    }

    void Update()
    {
        if(actions == null)
            return;
        if (actions.Count > 0)
            if (actions.Peek().executeTime < CurrentTimeMillis())
                doAction(actions.Dequeue());
    }

    void initEnemysFirstNameList(int count)
    {
        main.GetComponent<GetRandomEnemyName>().initNames();
        for (int i = 0; i < count; i++)
            enemysNameList.Add(main.GetComponent<GetRandomEnemyName>().getRandomName());
    }

    void doAction(Action action)
    {
        action.spawnPoint.GetComponent<mobGoDesPoint>().newAction(action);
    }

    public void creatFirstStrategy()
    {
        for (int i = 0; i < PLAYERS_COUNT; i++)
        {
            spawnPointGroups[0].GetComponent<Spawn_Groups>().creatInARandomPointMob(i, firstHealthy, enemysNameList[i], (long)(firstMobWaitTime*1000));
        }
    }

    public void newAction(GameObject spawnPoint, int healthy, String name, long delay, int id)
    {
        long exTime = generateRandomWaitTimeInMillis() + getLastTimeToExecute() + delay;
        lastExecutionTime = exTime;
        actions.Enqueue(new Action(exTime, spawnPoint, healthy, name, id));
    }

    public void resetActions()
    {
        actions.Clear();
        lastExecutionTime = CurrentTimeMillis();
    }

    public long getLastTimeToExecute()
    {
        if (actions.Count == 0)
        {
            return CurrentTimeMillis();
        }
        else
        {
            return lastExecutionTime;
        }
}

    public long CurrentTimeMillis()
    { 
        DateTime Jan1st1970 = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }
    
    long generateRandomWaitTimeInMillis()
    {
        return rnd.Next(minWaitTime * 1000, maxWaitTime * 1000);
    }
}
