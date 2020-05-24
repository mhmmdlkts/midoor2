using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineRoom : MonoBehaviour
{
    public GameObject  pp_me,  name_me,  money_me,  wins_me,  rank_me;
    public GameObject pp_him, name_him, money_him, wins_him, rank_him;
    public Sprite[] rankList;
    
    void Start()
    {
        setStatus();
    }

    private void setStatus()
    {
        wins_me.GetComponent<Text>().text = PlayerPrefs.GetInt("total_wins",0) + "";
        money_me.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("money",0);
        rank_me.GetComponent<Image>().sprite = rankList[PlayerPrefs.GetInt("rank", 4)];
        name_me.GetComponent<Text>().text = PlayerPrefs.GetString("name", "Mali");
    }
    
    public void button_signed_ranked()
    {
        
    }
    
    public void button_create_room()
    {
        
    }
    
    public void button_join_room()
    {
        
    }
}