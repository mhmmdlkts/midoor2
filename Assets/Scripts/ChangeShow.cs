using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShow : MonoBehaviour
{
    GameScript gameScript;
    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.Find("MOVABLE").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lookRight()
    {
        gameScript.lookRight();
    }

    public void lookLeft()
    {
        gameScript.lookLeft();
    }
}
