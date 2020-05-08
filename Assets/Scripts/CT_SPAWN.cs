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
    public Action(long executeTime, GameObject spawnPoint, int healthy)
    {
        this.executeTime = executeTime;
        this.spawnPoint = spawnPoint;
        this.healthy = healthy;
    }
}

public class CT_SPAWN : MonoBehaviour
{
    public Queue<Action> actions;
    public int actionsSize;
    public GameObject enemy;
    private GameObject[] enemyClone;
    private long lastExecutionTime;
    public GameObject[] spawnPointGroups;
    public int firstHealthy = 100;
    
    Random rnd;

    public int minWaitTime = 1, maxWaitTime = 3;
    private int PLAYERS_COUNT = 5;

    private int strategy; // for T  -> B1, B2, MID, LowerT, Short, Long;
    
    // Start is called before the first frame update
    void Start()
    {
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

    void doAction(Action action)
    {
        action.spawnPoint.GetComponent<mobGoDesPoint>().newAction(action.healthy);
    }

    public void creatFirstStrategy()
    {
        for (int i = 0; i < PLAYERS_COUNT; i++)
        {
            spawnPointGroups[0].GetComponent<Spawn_Groups>().creatInARandomPointMob(firstHealthy);
        }
    }

    public void newAction(GameObject spawnPoint, int healthy)
    {
        long exTime = generateRandomWaitTimeInMillis() + getLastTimeToExecute();
        lastExecutionTime = exTime;
        actions.Enqueue(new Action(exTime, spawnPoint, healthy));
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
    
    float generateRandomWaitTime()
    {
        return NextFloat(minWaitTime, maxWaitTime);
    }
    
    public static float NextFloat(int min, int max)
    {
        Random rnd = new Random();
        return Convert.ToSingle(rnd.Next(min*1000, max*1000)) / 1000;
    }

    public void creatMobs()
    {
        /*for (int i = 0; i < PLAYERS_COUNT; i++)
            enemyClone[i] = Instantiate(enemy, gameObject.transform.position, gameObject.transform.rotation);
        setDirections();*/
    }

    public void setDirections()
    {
        Random rnd = new Random();
        
        for (int i = 0; i < PLAYERS_COUNT; i++)
            if (rnd.Next(0, 1) == 0)
                enemyClone[i].GetComponent<ct>().swip(180f);
            
    }

    void creatRandomStrategy()
    {
        Random rnd = new Random();
        for (int i = 0; i < PLAYERS_COUNT; i++)
        {
            GameScript.CT_STRATEGY strategy;
            switch (rnd.Next(1,7))
            {
                case 1: strategy = GameScript.CT_STRATEGY.B1; break;
                case 2: strategy = GameScript.CT_STRATEGY.B2; break;
                case 3: strategy = GameScript.CT_STRATEGY.MID; break;
                case 4: strategy = GameScript.CT_STRATEGY.LowerT; break;
                case 5: strategy = GameScript.CT_STRATEGY.Short; break;
                case 6: strategy = GameScript.CT_STRATEGY.Long; break;
                default: strategy = GameScript.CT_STRATEGY.MID; break;
            }
            enemyClone[i].GetComponent<ct>().strategy = strategy;
        }
    }
}
