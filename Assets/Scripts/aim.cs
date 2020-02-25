using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    public bool onAim;
    public int countOfColliders;

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        countOfColliders = 0;
        onAim = false;
    }

    // Update is called once per frame
    void Update()
    {
        onAim = countOfColliders > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CT")
        {
            countOfColliders++;
            enemy = other.gameObject;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CT")
        {
            countOfColliders--;
            if (!onAim)
                enemy = null;
        }
    }

    public GameObject getEnemy()
    {
        return enemy;
    }

    public void hited()
    {
        countOfColliders = 0;
        onAim = false;
        enemy = null;
    }
}
