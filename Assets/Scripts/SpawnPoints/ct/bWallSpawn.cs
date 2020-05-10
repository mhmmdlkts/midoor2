using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bWallSpawn : MonoBehaviour
{
    public GameObject mob, createdMob, desPoint;
    bool isGoingPick = false, isGoingHide = false;

    private float speed = 1.0f;
    
    void Update()
    {
        if (isGoingPick)
        {
            goPicking();
        }

        if (isGoingHide)
        {
            goHiding();
        }
    }

    public void spawnMob()
    {
        createdMob = Instantiate(mob, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void goPicking()
    {
        if(createdMob == null)
            return;
        float step =speed*Time.deltaTime;
        createdMob.transform.position = Vector3.MoveTowards(createdMob.transform.position, desPoint.GetComponent<Transform>().position, step);
    }

    public void goHiding()
    {
        if(createdMob == null)
            return;
        float step =speed*Time.deltaTime;
        createdMob.transform.position = Vector3.MoveTowards(createdMob.transform.position, transform.position, step);
    }

    public void pick()
    {
        isGoingPick = true;
        isGoingHide = false;
    }

    public void hide()
    {
        isGoingPick = false;
        isGoingHide = true;
    }

    public void stop()
    {
        isGoingPick = false;
        isGoingHide = false;
    }
}
