using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMoveJoystick : MonoBehaviour
{ 
    public Joystick joystick;
    public float multiply, multiplyVer, multiplyHor, multiplyPos, multiplyRot, verticalBorder, horizontalBorder;
    private Vector3 firstPos;
    private Quaternion firstRot;
    private JoystickMove joystickMove;
    private GameScript gameScript;
    private int whereIsLooking;

    void Start()
    {
        joystickMove = joystick.GetComponent<JoystickMove>();
    }
    
    void Update()
    {
        if(GameScript.isStoped)
            return;
        gameScript = GameObject.Find("MOVABLE").GetComponent<GameScript>();
        whereIsLooking = GameScript.isLokingIn;
        if (JoystickMove.pressed || ControllerControls.isUsingJoystick)
            moveAim(joystickMove.getHorizontal(), joystickMove.getVertical(), gameScript.aimPoints[whereIsLooking].GetComponent<Transform>().position);
    }
    
    private void moveAim(float hor, float ver, Vector3 firstPos)
    {
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3((hor) * multiplyPos * multiply * multiplyHor,
            (ver) * multiplyPos * multiply * multiplyVer,0) + firstPos;
    }
}
