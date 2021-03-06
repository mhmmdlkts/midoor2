﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class aim : MonoBehaviour
{
    public int RESISTANCE_4, RESISTANCE_3, RESISTANCE_2, RESISTANCE_1;
    public float LAST_RES_Z_5, LAST_RES_Z_4, LAST_RES_Z_3, LAST_RES_Z_2, LAST_RES_Z_1;
    public int countOfHARD_0;
    public int countOfMobColliders;
    public GameObject game;
    public int whichBodyPart; // nothing: -1, head: 0, body: 1, legs: 2
    public int minHead, maxHead, minBody, maxBody, minLegs, maxLegs;
    private Random rnd;

    public GameObject enemy;
    
    void Start()
    {
        rnd = new Random();
        countOfMobColliders = 0;
        resetWallColliders();
    }

    private bool isOnAim()
    {
        if (enemy == null)
            countOfMobColliders = 0;
        return countOfMobColliders > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CT")
        {
            countOfMobColliders++;
            updateBodyPart(other);
            if (enemy == null)
                enemy = other.gameObject;
        }
        
        if (other.gameObject.tag == "HARD_0") countOfHARD_0++;

        if (other.gameObject.tag == "HARD_5")
            LAST_RES_Z_5 = Math.Min(LAST_RES_Z_5, other.gameObject.GetComponent<Transform>().position.z);
        if (other.gameObject.tag == "HARD_4") 
            LAST_RES_Z_4 = Math.Min(LAST_RES_Z_4, other.gameObject.GetComponent<Transform>().position.z);
        if (other.gameObject.tag == "HARD_3") 
            LAST_RES_Z_3 = Math.Min(LAST_RES_Z_3, other.gameObject.GetComponent<Transform>().position.z);
        if (other.gameObject.tag == "HARD_2") 
            LAST_RES_Z_2 = Math.Min(LAST_RES_Z_2, other.gameObject.GetComponent<Transform>().position.z);
        if (other.gameObject.tag == "HARD_1") 
            LAST_RES_Z_1 = Math.Min(LAST_RES_Z_1, other.gameObject.GetComponent<Transform>().position.z);
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CT")
        {
            countOfMobColliders--;
            updateBodyPart(other);
            if (!isOnAim())
                enemy = null;
        }
        
        if (other.gameObject.tag == "HARD_0") countOfHARD_0--;

        if (other.gameObject.tag == "HARD_5")
            LAST_RES_Z_5 = float.MaxValue;
        if (other.gameObject.tag == "HARD_4") 
            LAST_RES_Z_4 = float.MaxValue;
        if (other.gameObject.tag == "HARD_3") 
            LAST_RES_Z_3 = float.MaxValue;
        if (other.gameObject.tag == "HARD_2") 
            LAST_RES_Z_2 = float.MaxValue;
        if (other.gameObject.tag == "HARD_1") 
            LAST_RES_Z_1 = float.MaxValue;
    }

    public int aimResistance()
    {
        if (enemy == null)
            return 0;
        if (!isOnAim())
            return -1;
        if (LAST_RES_Z_5 < enemy.GetComponent<Transform>().position.z)
            return Int32.MaxValue;
        if (LAST_RES_Z_4 < enemy.GetComponent<Transform>().position.z)
            return RESISTANCE_4;
        if (LAST_RES_Z_3 < enemy.GetComponent<Transform>().position.z)
            return RESISTANCE_3;
        if (LAST_RES_Z_2 < enemy.GetComponent<Transform>().position.z)
            return RESISTANCE_2;
        if (LAST_RES_Z_1 < enemy.GetComponent<Transform>().position.z)
            return RESISTANCE_1;
        return 0;
    }
    

    public GameObject getEnemy()
    {
        return enemy;
    }

    private void resetWallColliders()
    {
        LAST_RES_Z_1 = float.MaxValue;
        LAST_RES_Z_2 = float.MaxValue;
        LAST_RES_Z_3 = float.MaxValue;
        LAST_RES_Z_4 = float.MaxValue;
        LAST_RES_Z_5 = float.MaxValue;
    }

    public void shoted()
    {
        game.GetComponent<GameScriptOnline>().shot_online_player();
        if (isOnAim() && isOnSafeZone())
        {
            hited();
        }
        resetWallColliders();
    }

    private bool isOnSafeZone()
    {
        return countOfHARD_0 > 0;
    }

    public void hited()
    {
        int ress = aimResistance();
        game.GetComponent<GameScript>().hited(getEnemy(), calculateDamage() - ress, whichBodyPart == 0, ress > 0);
        
        countOfMobColliders = 0;
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

        return damage;
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
