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
      float ver = joyVertical + joystick.Vertical;
      /*if (ver > 0)
        ver = Math.Min(ver, limVer);
      else
        ver = Math.Max(ver, -limVer);*/
      if(-limVer <= ver && ver <= limVer)
        return ver;
      else if (-limVer > ver)
      {
        ver = -limVer;
      }
      else if (limVer < ver)
      {
        ver = limVer;
      }
      joyVertical = ver;
      return ver;
    }

    public float getHorizontal()
    {
      float hor = joyHorizontal + joystick.Horizontal;
      /*if (hor > 0)
        hor = Math.Min(hor, limHor);
      else
        hor = Math.Max(hor, -limHor);
      return hor;*/
      if(-limHor <= hor && hor <= limHor)
        return hor;
      else if (-limHor > hor)
      {
        hor = -limHor;
      }
      else if (limHor < hor)
      {
        hor = limHor;
      }
      joyHorizontal = hor;
      return hor;
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
