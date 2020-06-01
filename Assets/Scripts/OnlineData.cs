using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineData : MonoBehaviour
{

    public int pp_him;
    public int wins_him, rank_him;
    private bool isT_me;
    public string[] otherTeam;
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void setTeam(bool isT)
    {
        isT_me = isT;
    }

    public bool getTeam()
    {
        return isT_me;
    }
    
    
}
