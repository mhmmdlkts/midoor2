﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ct : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool isAtack;
    public float d, a;
    void Start()
    {
        speed = 1;
        isAtack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAtack)
        {
            if (transform.position.x < 0.1f && transform.position.x > 0)
            {
                killT();
            }
        }
    }

    public void swip(float degre)
    {
        float speed = 10F;
        transform.rotation = Quaternion.Euler(0, degre, 0);
    }

    public void newZ(float rangeDown, float rangeUp)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(rangeDown, rangeUp));
    }

    public void newSpeed(float rangeDown, float rangeUp)
    {
        speed = Random.Range(rangeDown, rangeUp);
    }

    public bool atack(float chance)
    {
        d = Random.Range(0.0f, 1.0f);
        a = chance;
        isAtack = d < chance;
        return isAtack;
    }

    public void killT()
    {
        isAtack = false;
    }
    
}
