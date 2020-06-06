using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class EndGameShow : MonoBehaviour
{
    public GameObject ppImg, moneyLabel, nameLabel, killsLabel, winsLabel, rankImg;
    public int tot_kills, kills, money, winns, rank, newWin = 0, winSerie, loseSerie;
    public String name;
    private int winMoneyBonus = 200;
    private int loseMoneyBonus = 50;
    public Sprite[] rankList;
    public static int WinLoseSerieForNewRank = 3;
    public bool isRankUpgrade, isRankDowngrade;
    public AudioClip newItemSound, newRankSound;
    private InterstitialAd interstitial;

    private Text a, b, c, d;

    private Image e;
    
    void Start()
    {
        RequestInterstitial();
        ppImg.GetComponent<Image>().sprite = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>().ppList[PlayerPrefs.GetInt("pp", 2)];
        tot_kills = PlayerPrefs.GetInt("total_kill",0);
        winns = PlayerPrefs.GetInt("total_wins",0);
        money = PlayerPrefs.GetInt("money",0);
        rank = PlayerPrefs.GetInt("rank",4);
        newWin = PlayerPrefs.GetInt("new_win",0);
        kills = PlayerPrefs.GetInt("new_kills",0);
        name = PlayerPrefs.GetString("name", LanguageSystem.GET_NAME());
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

    public static void addPlays(int i)
    {
        PlayerPrefs.SetInt("plays", PlayerPrefs.GetInt("plays", 3)+i);
    }
    
    private void RequestInterstitial()
    {
        if (MainMenuAd.isRemovedAds())
            return;
        MobileAds.Initialize(initStatus => { });
        
        if (MainMenuAd.isRemovedAds())
            return;
        
        interstitial = new InterstitialAd(AdUnitIds.getAdUnitId(Ads.Menu_Popup));
        
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        if (MainMenuAd.isRemovedAds())
        {
            quitGame();
            return;
        }

        if (interstitial != null)
            interstitial.Destroy();
        quitGame();
    }

    private void saveStatus()
    {
        PlayerPrefs.SetInt("total_kill", tot_kills+kills);
        PlayerPrefs.SetInt("money", money+getNewMoney());
        PlayerPrefs.SetInt("total_wins", winns+newWin);
        if (newWin == 1)
        {
            addPlays(1);
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
        if (rank > 0)
            rank--;
        else
            isRankDowngrade = false;
    }

    private void rankUp()
    {
        if(rank < GameScript.tot_rank-1)
            rank++;
        else
            isRankUpgrade = false;
    }

    public void writeStatus()
    {
        a.text = name;
        b.text = LanguageSystem.GET_END_GAME_PANEL_LABEL_KILLS() + tot_kills;
        c.text = LanguageSystem.GET_END_GAME_PANEL_LABEL_WINS() + winns;
        d.text = LanguageSystem.GET_END_GAME_PANEL_LABEL_MONEY() + money;
        e.sprite = rankList[rank];
    }

    private int getNewMoney()
    {
        return (kills * 10) + (newWin == 1 ? winMoneyBonus : loseMoneyBonus);
    }

    public void show()
    {
        Invoke(nameof(setKills), 1);
    }

    private void setKills()
    {
        GetComponent<AudioSource>().PlayOneShot(newItemSound);
        b.text += " + " + kills;
        Invoke(nameof(setWins), 1);
    }

    private void setWins()
    {
        GetComponent<AudioSource>().PlayOneShot(newItemSound);
        c.text += " + " + (newWin == 1 ? 1 : 0);
        Invoke(nameof(setMoney), 1);
    }

    private void setMoney()
    {
        GetComponent<AudioSource>().PlayOneShot(newItemSound);
        d.text += " + " + getNewMoney();
        Invoke(nameof(setRank), 1);
    }

    private void setRank()
    {
        if (isRankUpgrade)
        {
            GetComponent<AudioSource>().PlayOneShot(newRankSound);
        }
        e.sprite = rankList[rank];
        
        if (MainMenuAd.isRemovedAds())
            Invoke(nameof(quitGame), 1);
        else
            Invoke(nameof(showPopup), 1);
    }

    private void showPopup()
    {
        if (MainMenuAd.isRemovedAds())
        {
            quitGame();
            return;
        }

        if (interstitial.IsLoaded())
            interstitial.Show();
        else
            quitGame();
    }

    private void quitGame()
    {
        GameObject.Find("MOVABLE").GetComponent<GameScript>().gameQuit();
    }
}
