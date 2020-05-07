using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int healthy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setHealty(int healthy)
    {
        this.healthy = healthy;
    }

    public int giveDamage(int damage)
    {
        setHealty(healthy-damage);
        return healthy;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
