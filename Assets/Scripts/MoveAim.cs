using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAim : MonoBehaviour
{
    private Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.getTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Ended:
                    break;
            }
        }
    }
}
