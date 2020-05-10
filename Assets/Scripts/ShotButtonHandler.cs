using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShotButtonHandler : MonoBehaviour
{
    private GameObject scope, aim, ammo;
    public AudioSource shotSound;
    public bool isShotable;
    
    // Start is called before the first frame update
    void Start()
    {
        shotSound = gameObject.GetComponent<AudioSource>();
        scope = GameObject.Find("Scope");
        aim = GameObject.Find("aim3d");
        ammo = GameObject.Find("ammo");
        isShotable = true;
    }

    public void shot()
    {
        if (GameScript.isStoped || !isShotable || !ammo.GetComponent<ammoPanel>().isShootable)
            return;
        ammo.GetComponent<ammoPanel>().oneShot();
        shotSound.Play();
        aim.GetComponent<aim>().shoted();
        setShotable(false);
        StartCoroutine("refreshAmmo");
    }

    IEnumerator refreshAmmo()
    {
        yield return new WaitForSeconds(0.85f);
        setShotable(true);
    }

    void setShotable(bool shotable)
    {
        isShotable = shotable;
        scope.SetActive(shotable);
    }
}
