using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerControls : MonoBehaviour
{
    public GameObject shot_button, zoom_button;

    public static bool isUsingJoystick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            shot_button.GetComponent<ShotButtonHandler>().shot();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button10))
        {
            zoom_button.GetComponent<ZoomButtonHandler>().zoom();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            gameObject.GetComponent<ChangeShow>().lookLeft();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            gameObject.GetComponent<ChangeShow>().lookRight();
        }
        if (isUsingJoystick = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
        } 
    }
}
