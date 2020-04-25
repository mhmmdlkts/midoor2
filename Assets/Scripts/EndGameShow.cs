using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameShow : MonoBehaviour
{
    public GameObject ppImg, nameLabel, killsLabel, winsLabel, rankImg;
    public int tot_kills, kills, winns, rank, newWin=0;
    public String name;
    public String KILLS_TEXT = "Kills : ";
    public String WINS_TEXT  = "Wins  : ";
    public Sprite[] rankList;

    private Text a, b, c;

    private Image d;
    // Start is called before the first frame update
    void Start()
    {
        tot_kills = PlayerPrefs.GetInt("total_kill",0);
        winns = PlayerPrefs.GetInt("total_wins",0);
        rank = PlayerPrefs.GetInt("rank",0);
        newWin = PlayerPrefs.GetInt("new_win",0);
        kills = PlayerPrefs.GetInt("new_kills",0);
        name = PlayerPrefs.GetString("name", "Mali");
        a = nameLabel.GetComponent<UnityEngine.UI.Text>();
        b = killsLabel.GetComponent<UnityEngine.UI.Text>();
        c = winsLabel.GetComponent<UnityEngine.UI.Text>();
        d = rankImg.GetComponent<Image>();
        writeStatus();
        Invoke("show", 1);
    }

    public void writeStatus()
    {
        a.text = name;
        b.text = KILLS_TEXT + tot_kills;
        c.text = WINS_TEXT + winns;
        d.sprite = rankList[rank];
    }

    public void show()
    {
        Invoke("setKills", 1);
    }

    private void setKills()
    {
        b.text += " + " + kills;
        PlayerPrefs.SetInt("total_kill", tot_kills+kills);
        Invoke("setWins", 1);
    }

    private void setWins()
    {
        c.text += " + " + newWin;
        PlayerPrefs.SetInt("total_wins", winns+newWin);
        Invoke("setRank", 1);
    }

    private void setRank()
    {
        PlayerPrefs.SetInt("rank", rank+newWin); 
        d.sprite = rankList[rank+newWin];
        Invoke("quitGame", 1);
    }

    private void quitGame()
    {
        GameObject.Find("MOVABLE").GetComponent<GameScript>().quitGame();
    }
}
