using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperHelper : MonoBehaviour
{
    public GameObject shot_button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            shot_button.GetComponent<ShotButtonHandler>().shot_kill();
        }
        if (Input.GetKeyDown("t"))
        {
            gameObject.GetComponent<GameScript>().timeOut();
        }
    }
}
