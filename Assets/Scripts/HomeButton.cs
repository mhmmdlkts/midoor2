using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public GameObject dialog;
    public void quitGame()
    {
        Instantiate(dialog, dialog.transform.position, dialog.transform.rotation);
    }
}