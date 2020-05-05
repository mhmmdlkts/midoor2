using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawn_Groups : MonoBehaviour
{
    public GameObject mobGen;
    public GameObject[] spawnPoints;

    private Random rnd;
    // Start is called before the first frame update
    void Start()
    {
        rnd = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void creatInARandomPointMob()
    {
        mobGen.GetComponent<CT_SPAWN>().newAction(spawnPoints[rnd.Next(0,spawnPoints.Length)]);
    }
}
