using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public String name;
    public int healthy;
    public GameObject fireParticle;
    public bool isFiring;
    public int id;

    private ParticleSystem particleSystem;
    
    void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        
        gameObject.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    public void setEnemy(Action action)
    {
        this.id = action.id;
        this.name = action.name;
        this.healthy = action.healthy;
    }

    public int giveDamage(int damage)
    {
        GetComponent<ParticleSystem>().Emit(damage*2);
        healthy -= damage;
        return healthy;
    }

    public void playFireParticle()
    {
        isFiring = true;
        fireParticle.GetComponent<ParticleSystem>().Play();
        Invoke("stopFireParticle",0.15f);
    }

    public void stopFireParticle()
    {
        isFiring = false;
        fireParticle.GetComponent<ParticleSystem>().Stop();
    }
}
