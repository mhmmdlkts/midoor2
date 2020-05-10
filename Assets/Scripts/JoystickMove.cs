using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMove : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
  public bool pressed;
  public Joystick joystick;
  public float joyVertical, joyHorizontal, tmpVer, tmpHor, limVer, limHor;
  
    // Start is called before the first frame update
    void Start()
    {
      joyVertical = 0.0f;
      joyHorizontal = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
      tmpVer = joystick.Vertical;
      tmpHor = joystick.Horizontal;
    }

    public float getVertical()
    {
      return joystick.Vertical;
    }

    public float getHorizontal()
    {
      return joystick.Horizontal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      pressed = false;
      joyVertical = tmpVer + getVertical();
      joyHorizontal = tmpHor + getHorizontal();
    }
}
