using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShotButtonHandler : MonoBehaviour
{
    private GameObject scope, aim;
    public AudioSource shotSound;
    public bool isShotable;
    
    // Start is called before the first frame update
    void Start()
    {
        shotSound = gameObject.GetComponent<AudioSource>();
        scope = GameObject.Find("Scope");
        aim = GameObject.Find("aim3d");
        isShotable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shot()
    {
        if (!isShotable)
            return;
        shotSound.Play();
        if (aim.GetComponent<aim>().onAim)
        {
            aim.GetComponent<aim>().hited();
          //  shot_kill();
        }
        setShotable(false);
        StartCoroutine("refreshAmmo");
    }

    public void shot_kill()
    {
        //Destroy(aim.GetComponent<aim>().getEnemy());
    }

    IEnumerator refreshAmmo()
    {
        yield return new WaitForSeconds(0.85f);
        setShotable(true);
    }

    void setShotable(bool shotable)
    {
        Console.Write(shotable);
        isShotable = shotable;
        scope.SetActive(shotable);
    }
}
