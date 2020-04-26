using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour
{
    public GameObject enemy;
    public bool isMid;
    public bool isAtack, isStoped;
    private ct ctScript;
    public float atachChance;

    private GameObject enemyClone;
    // Start is called before the first frame update
    void Start()
    {
        isStoped = false;
    }

    public void creatMob()
    {
        if (enemy == null)
            return;
        enemyClone = Instantiate(enemy, gameObject.transform.position, gameObject.transform.rotation);
        ctScript = enemyClone.GetComponent<ct>();
        ctScript.newZ(13,18);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStoped)
        {
            setPerspektif();
            MoveMob();
        }
    }

    void MoveMob()
    {
        if (enemyClone == null)
            return;
        Vector3 enemyPos = enemyClone.transform.position;
        if (enemyPos.x > 1.5f)
        {
            ctScript.swip(0);
            if (enemyPos.z >= 12.5f)
            {
                ctScript.newZ(13f,17.5f);
                ctScript.newSpeed(1.2f,2.2f);
                atachChance = (2.2f - ctScript.speed) / 20;
            }
            else
            {
                ctScript.newSpeed(2.2f,3.2f);
                atachChance = (3.2f - ctScript.speed) / 2;
            }
        }
        
        if (enemyPos.x < -1.5f)
        {
            ctScript.swip(180);
            ctScript.newZ(12.3f,17.5f);
            ctScript.newSpeed(1.2f,2.2f);
            atachChance = (2.2f - ctScript.speed) / 20;
        }
        Vector3 pos = new Vector3(-1, 0, 0);
        enemyClone.transform.Translate(pos * Time.deltaTime * ctScript.speed);
        ctScript.atack(atachChance);
    }

    void setPerspektif()
    {
        if (enemyClone == null || !isMid)
            return;
        
        float newScale = 7 / enemyClone.transform.position.z;
        enemyClone.transform.localScale = new Vector3(newScale, newScale, 1.0f);
        
        float newY = Convert.ToSingle((enemyClone.transform.position.z * 0.344) - 5.428);
        enemyClone.transform.position = new Vector3(enemyClone.transform.position.x, newY, enemyClone.transform.position.z);
    }

    public void stopMove()
    {
        isStoped = true;
    }
}
