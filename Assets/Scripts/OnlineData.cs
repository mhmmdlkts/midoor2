using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineData : MonoBehaviour
{

    public Sprite pp_him;
    public string name_him;
    public int wins_him, rank_him;
    public bool isT_me;
    public string[] otherTeam;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
