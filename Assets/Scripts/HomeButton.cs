using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public void quitGame()
    {
        gameObject.GetComponent<GameScript>().quitGame();
    }
}