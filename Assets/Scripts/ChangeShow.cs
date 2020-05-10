using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShow : MonoBehaviour
{
    GameScript gameScript;
    
    void Start()
    {
        gameScript = GameObject.Find("MOVABLE").GetComponent<GameScript>();
    }
    
    public void lookRight()
    {
        if (GameScript.isStoped)
            return;
        gameScript.lookRight();
    }

    public void lookLeft()
    {
        if (GameScript.isStoped)
            return;
        gameScript.lookLeft();
    }
}
