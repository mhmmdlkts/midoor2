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

    void Start()
    {
        firstPos = gameObject.transform.position;
        firstRot = gameObject.transform.rotation;
        joystickMove = joystick.GetComponent<JoystickMove>();
    }
    
    void Update()
    {
        if (joystickMove.pressed)
            moveAim(joystickMove.getHorizontal(), joystickMove.getVertical());
    }

    private void resetAim()
    {
        gameObject.transform.position = firstPos;
        gameObject.transform.rotation = firstRot;
    }
    
    private void moveAim(float hor, float ver)
    {
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3((hor + firstPos.x) * multiplyPos * multiply * multiplyHor,
                (ver + firstPos.y) * multiplyPos * multiply * multiplyVer, pos.z);
        
        gameObject.transform.rotation = Quaternion.Euler(new Vector3((ver + firstRot.x) * multiplyRot * multiply * multiplyVer,
            (hor + firstRot.y) * multiplyRot * multiply * multiplyHor, pos.z));
    }
    
    private bool isInVerticalBorder()
    {
        return Math.Abs(joystick.Vertical) < verticalBorder;
    }

    private bool isInHorizontalBorder()
    {
        return Math.Abs(joystick.Horizontal) < horizontalBorder;   
    }
}
