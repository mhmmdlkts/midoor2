using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineMenu : MonoBehaviour
{
    public GameObject pp, name, money, wins, rank;
    public Sprite[] rankList;
    void Start()
    {
        setStatus();
    }

    void Update()
    {
        
    }

    private void setStatus()
    {
        wins.GetComponent<Text>().text = PlayerPrefs.GetInt("total_wins",0) + "";
        money.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("money",0);
        rank.GetComponent<Image>().sprite = rankList[PlayerPrefs.GetInt("rank", 4)];
        name.GetComponent<Text>().text = PlayerPrefs.GetString("name", "Mali");
    }
    
    public void button_ranked_room()
    {
        
    }
    
    public void button_join_room()
    {
        
    }
    
    public void button_create_room()
    {
        
    }
}
