using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class aim : MonoBehaviour
{
    public bool onAim;
    public int countOfColliders;
    public GameObject game;
    public int whichBodyPart; // nothing: -1, head: 0, body: 1, legs: 2
    public int minHead, maxHead, minBody, maxBody, minLegs, maxLegs;
    private Random rnd;

    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        rnd = new Random();
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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "CT")
        {
            countOfColliders++;
            updateBodyPart(other);
            enemy = other.gameObject;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CT")
        {
            countOfColliders--;
            updateBodyPart(other);
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
        game.GetComponent<GameScript>().hited(getEnemy(), calculateDamage(), whichBodyPart == 0);
        Debug.Log("Hitted");
        countOfColliders = 0;
        onAim = false;
        enemy = null;
    }

    private int calculateDamage()
    {
        int damage = 0;
        switch (whichBodyPart)
        {
            case 0:
                damage = rnd.Next(minHead, maxHead);
                break;
            case 1:
                damage = rnd.Next(minBody, maxBody);
                break;
            case 2:
                damage = rnd.Next(minLegs, maxLegs);
                break;
        }

        return damage - resistanceBlock();
    }

    private int resistanceBlock()
    {
        return 0;
    }

    private void updateBodyPart(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
        {
            whichBodyPart = 0;
        } 
        else if (other.GetType() == typeof(BoxCollider))
        {
            whichBodyPart = 1;
        } 
        else if (other.GetType() == typeof(CapsuleCollider))
        {
            whichBodyPart = 2;
        }
    }
}
