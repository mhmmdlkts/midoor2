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

public struct Online_EX
{
    public long actionExTime;
    public Action action;
    public Online strategy;
}

public enum Online {
    READ,
    WRITE,
    OFFLINE
}

public class ENEMY_SPAWN : MonoBehaviour
{
    public Queue<Action> actions;
    public int actionsSize;
    public GameObject enemy, main;
    private GameScript game;
    private GameObject[] enemyClone;
    private long lastExecutionTime;
    public GameObject[] spawnPointGroups;
    public static int firstHealthy = 100;
    public float firstMobWaitTime;

    Random rnd;

    public int minWaitTime = 1, maxWaitTime = 3;
    private int PLAYERS_COUNT = 5;

    void Start()
    {
        game = main.GetComponent<GameScript>();
        rnd = new Random();
        actions = new Queue<Action>();
        enemyClone = new GameObject[PLAYERS_COUNT];
        
    }

    void Update()
    {
        if(actions == null)
            return;
        if (actions.Count > 0)
            if (actions.Peek().executeTime < CurrentTimeMillis())
                doAction(actions.Dequeue());
    }

    public void initEnemysFirstNameList(int count, bool isOnline)
    {
        main.GetComponent<GetRandomEnemyName>().initNames();
        if (!isOnline)
        {
            for (int i = 0; i < count; i++)
                game.enemysNameList.Add(main.GetComponent<GetRandomEnemyName>().getRandomName());
        }
        else
        {
            OnlineData data = main.GetComponent<GameScript>().online_data;
            if (data.otherTeam == null)
                return;
            for (int i = 0; i < count; i++) {
                game.enemysNameList.Add(i < data.otherTeam.Length ? data.otherTeam[i] : main.GetComponent<GetRandomEnemyName>().getRandomName());
            }
        }
    }

    void doAction(Action action)
    {
        action.spawnPoint.GetComponent<mobGoDesPoint>().newAction(action);
    }

    public void creatFirstStrategy(Online strategy, bool isOnline)
    {
        for (int i = isOnline? 1: 0; i < PLAYERS_COUNT; i++)
        {
            createNew(strategy, isOnline, i);
        }
    }

    public void createNew(Online strategy, bool isOnline, int id)
    {
        spawnPointGroups[0].GetComponent<Spawn_Groups>().creatInARandomPointMob(id, firstHealthy, game.enemysNameList[id], (long)(firstMobWaitTime*1000),strategy);
    }

    public void newAction(GameObject spawnPoint, int healthy, String name, long delay, int id, Online strategy, Online_EX onlineEx)
    {
        if (strategy == Online.READ)
        {
            Action action = onlineEx.action;
            addActionQueue(action);
        }
        else
        {
            Action action = getNewAction(spawnPoint, healthy, name, delay, id);
            onlineEx.action = action;
            addActionQueue(action);
        }
    }

    public void newAction(GameObject spawnPoint, int healthy, String name, long delay, int id)
    {
        addActionQueue(getNewAction(spawnPoint, healthy, name, delay, id));
    }

    private Action getNewAction(GameObject spawnPoint, int healthy, String name, long delay, int id)
    {
        long exTime =  getLastTimeToExecute() + delay + generateRandomWaitTimeInMillis();
        Action action =  new Action(exTime, spawnPoint, healthy, name, id);

        return action;
    }

    private void addActionQueue(Action action)
    {
        lastExecutionTime = action.executeTime;
        actions.Enqueue(action);
    }

    public void resetActions()
    {
        actions.Clear();  // TODO NULL Pointer Exception aldim incelee
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
