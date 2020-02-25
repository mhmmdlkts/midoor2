using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class MoveAim : MonoBehaviour
{
    private Touch touch;

    public float deltaX, deltaY, sensivity;
    public float x, y;
    private float x0, y0;
    public float frameX, frameY;
    
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
         
    }

    // Start is called before the first frame update
    void Start()
    {
        sensivity = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width/2)
            {
                return;
            }
            
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 pos = gameObject.transform.position;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x;
                    deltaY = touchPos.y;
                    x0 = pos.x;
                    y0 = pos.y;
                    break;
                case TouchPhase.Moved:
                    x = ((touchPos.x - deltaX)*sensivity + x0);
                    y = ((touchPos.y - deltaY)*sensivity + y0);
                    gameObject.transform.position = new Vector3((Math.Abs(x) <= frameX)?x:pos.x, (Math.Abs(y) <= frameY)?y:pos.y, pos.z);
                    break;
                case TouchPhase.Ended:
                    //gameObject.transform.position = Vector3.zero;
                    break;
            }
        }*/
    }
}
