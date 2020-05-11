using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameShow : MonoBehaviour
{
    public GameObject ppImg, moneyLabel, nameLabel, killsLabel, winsLabel, rankImg;
    public int tot_kills, kills, money, winns, rank, newWin = 0, winSerie, loseSerie;
    public String name;
    public String KILLS_TEXT = "Kills : ";
    public String WINS_TEXT  = "Wins : ";
    public String MONEY_TEXT = "Money : $";
    private int winMoneyBonus = 200;
    private int loseMoneyBonus = 50;
    public Sprite[] rankList;
    public static int WinLoseSerieForNewRank = 3;
    public bool isRankUpgrade, isRankDowngrade;

    private Text a, b, c, d;

    private Image e;
    
    void Start()
    {
        tot_kills = PlayerPrefs.GetInt("total_kill",0);
        winns = PlayerPrefs.GetInt("total_wins",0);
        money = PlayerPrefs.GetInt("money",0);
        rank = PlayerPrefs.GetInt("rank",4);
        newWin = PlayerPrefs.GetInt("new_win",0);
        kills = PlayerPrefs.GetInt("new_kills",0);
        name = PlayerPrefs.GetString("name", "Mali");
        winSerie = PlayerPrefs.GetInt("winSerie",0);
        loseSerie = PlayerPrefs.GetInt("loseSerie",0);
        a = nameLabel.GetComponent<UnityEngine.UI.Text>();
        b = killsLabel.GetComponent<UnityEngine.UI.Text>();
        c = winsLabel.GetComponent<UnityEngine.UI.Text>();
        d = moneyLabel.GetComponent<UnityEngine.UI.Text>();
        e = rankImg.GetComponent<Image>();
        saveStatus();
        writeStatus();
        show();
    }

    private void saveStatus()
    {
        PlayerPrefs.SetInt("total_kill", tot_kills+kills);
        PlayerPrefs.SetInt("money", money+getNewMoney());
        PlayerPrefs.SetInt("total_wins", winns+newWin);
        if (newWin == 1)
        {
            winSerie++;
            loseSerie = 0;
        } else if (newWin == -1){
            loseSerie++;
            winSerie = 0;
        } else if (newWin == 0){ /* tie */}

        PlayerPrefs.SetInt("loseSerie",loseSerie);
        PlayerPrefs.SetInt("winSerie",winSerie);
        isRankUpgrade = (winSerie != 0 && winSerie % WinLoseSerieForNewRank == 0);
        isRankDowngrade = (loseSerie != 0 && loseSerie % WinLoseSerieForNewRank == 0);
        if (isRankUpgrade)
            rankUp();
        if (isRankDowngrade)
            rankDown();
        PlayerPrefs.SetInt("rank", rank); 
    }

    private void rankDown()
    {
        if(rank > 0)
            rank--;
    }

    private void rankUp()
    {
        if(rank < GameScript.tot_rank)
            rank++;
    }

    public void writeStatus()
    {
        a.text = name;
        b.text = KILLS_TEXT + tot_kills;
        c.text = WINS_TEXT + winns;
        d.text = MONEY_TEXT + money;
        e.sprite = rankList[rank];
    }

    private int getNewMoney()
    {
        return (kills * 10) + (newWin == 1 ? winMoneyBonus : loseMoneyBonus);
    }

    public void show()
    {
        Invoke("setKills", 1);
    }

    private void setKills()
    {
        b.text += " + " + kills;
        Invoke("setWins", 1);
    }

    private void setWins()
    {
        c.text += " + " + (newWin == 1 ? 1 : 0);
        Invoke("setMoney", 1);
    }

    private void setMoney()
    {
        d.text += " + " + getNewMoney();
        Invoke("setRank", 1);
    }

    private void setRank()
    {
        if (isRankUpgrade)
        {
            // TODO play stars sound
        }
        e.sprite = rankList[rank];
        Invoke("quitGame", 1);
    }

    private void quitGame()
    {
        GameObject.Find("MOVABLE").GetComponent<GameScript>().quitGame();
    }
}
