using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShotButtonHandler : MonoBehaviour
{
    private GameObject scope, aim;
    public bool isShotable;
    
    // Start is called before the first frame update
    void Start()
    {
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
        if (aim.GetComponent<aim>().onAim)
        {
            Destroy(aim.GetComponent<aim>().getEnemy());
            aim.GetComponent<aim>().hited();
        }
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
        Console.Write(shotable);
        isShotable = shotable;
        scope.SetActive(shotable);
    }
}
