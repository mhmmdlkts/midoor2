﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bomb_icon : MonoBehaviour
{
    public Color32 red_bright, red_dark, green_bright, green_dark, white;

    private GameObject Container;

    public AudioClip bombBeep;

        public int explodeTime;
    // Start is called before the first frame update
    void Start()
    {
        Container = GameObject.Find("Time_Bomb");
        gameObject.transform.SetParent (Container.transform, false);
        Invoke(nameof(counter), 1f);
    }

    void counter()
    {
        GetComponent<AudioSource>().PlayOneShot(bombBeep);
        if (explodeTime > 0)
        {
            explodeTime--;
            Invoke(nameof(counter), 1f);
            return;
        }
        Debug.Log("BOOM");
        gameObject.GetComponent<Image>().color = green_bright;
        GameObject.Find("MOVABLE").GetComponent<GameScript>().bombExplode();
    }
    
    void Update()
    {
    }
    
}
