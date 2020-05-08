using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int healthy;

    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        
        gameObject.GetComponent<Renderer>().sortingLayerName = "Foreground";
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
