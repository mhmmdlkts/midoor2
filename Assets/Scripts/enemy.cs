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

    private ParticleSystem particleSystem;
    
    void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        
        gameObject.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setHealty(int healthy)
    {
        this.healthy = healthy;
    }

    public int giveDamage(int damage)
    {
        GetComponent<ParticleSystem>().Emit(damage*2);
        setHealty(healthy-damage);
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
