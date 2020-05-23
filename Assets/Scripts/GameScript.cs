﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class GameScript : MonoBehaviour
{
    public GameObject canvas, aim, timeLabel, ctScorLaber, tScorLabel, kill_info_dialog, mobGenT, mobGenCT, 
        t1, t2, t3, t4, t5, ct1, ct2, ct3, ct4, ct5, healthy_panel, healthy_panel_outside, healthy_text, ammo;
    public GameObject[] T_aimPoints; // B, Mid, Long
    public GameObject[] CT_aimPoints; // Long, Mid, B
    public GameScriptOnline online;
    public Sprite ownPP, tPP, ctPP;
    public int maxLooks, playersHealthy, PLAYERS_START_HEALTHY;
    public static int isLokingIn;
    public static bool isT;
    public static int gameMode; // 0: Ranked

    public static String yourName;
    private int time, enemyCount, tScore, ctScore, kills, round;
    public int roundTime;
    public int round_per_half, defLookPintCT = 1, defLookPintT = 1;
    private static readonly int WIN_SCORE = 16;
    public static bool isStoped = true;

    public static int rank;
    public static int tot_rank = 18;

    private OnlineData online_data;
    public bool isOnline;
    private static string[] myTeam;
    private static string[] otherTeam;
    private static int otherRank;
    private Sprite otherPP;

    public Online strategy;
    
    
    void Start()
    {
        online = gameObject.GetComponent<GameScriptOnline>();
        isOnline = GameObject.Find("Data") != null;
        if (isOnline) {
            online_data = GameObject.Find("Data").GetComponent<OnlineData>();
            strategy = Online.WRITE;
        } else strategy = Online.OFFLINE;
        myTeam = new string[enemyCount];
        otherTeam = new string[enemyCount];
        Application.targetFrameRate = 300;
        isT = isOnline? online_data.isT_me : Random.Range(0,2) == 1;
        gameMode = 0;
        
        kills = 0;
        round = (WIN_SCORE - round_per_half);
        ctScore = round / 2;
        tScore = round - ctScore;
        rank = PlayerPrefs.GetInt("rank",4);
        yourName = PlayerPrefs.GetString("name", "Mali");
        maxLooks = isT ? T_aimPoints.Length : CT_aimPoints.Length;
        resetLook();
        newRound();
    }

    public void getOnlineShot()
    {
        Debug.Log(isT? "CT":"T" + " fires");
    }

    public void friendGotShot(int weaponCode, bool isWall, bool isHead, int enemyId, int damage)
    {
        Debug.Log(isT? "CT: ":"T: " + (weaponCode + " " + isWall + " " + isHead + " " + enemyId + " " +damage));
    }

    public void resetLook()
    {
        setLook(isT ? defLookPintT : defLookPintCT);
    }

    public void switchTeam()
    {
        changeStrategy();
        isT = !isT;
        int scoreTmp = tScore;
        tScore = ctScore + (WIN_SCORE - round_per_half) / 2;
        ctScore = scoreTmp + (WIN_SCORE - round_per_half) / 2;
        updateScore();
        
        resetLook();
        ppReset();
    }

    private void changeStrategy()
    {
        switch (strategy)
        {
            case Online.READ:
                strategy = Online.WRITE;
                break;
            case Online.WRITE:
                strategy = Online.READ;
                break;
        }
    }

    public void givePlayerDamage(int damage, int weaponCode, bool isHead, GameObject enemy)
    {
        setHealthy(playersHealthy - damage);
        if (playersHealthy == 0)
            playerDeath(weaponCode, isHead, enemy);
    }

    public void setHealthy(int healthy)
    {
        playersHealthy = healthy;
        if (playersHealthy < 0)
            playersHealthy = 0;
        healthy_text.GetComponent<Text>().text = playersHealthy+"";
        Color32 h_bar_color = healthy_panel.GetComponent<Image>().color;
        byte newGreen = Decimal.ToByte(255 / 100 * playersHealthy);
        healthy_panel.GetComponent<Image>().color = new Color32(h_bar_color.r,newGreen,h_bar_color.b,h_bar_color.a);
        float maxLength = healthy_panel_outside.GetComponent<RectTransform>().sizeDelta.x;
        RectTransform rct = healthy_panel.GetComponent<RectTransform>();
        rct.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxLength / 100 * playersHealthy);
    }

    public void startCountdown()
    {
        InvokeRepeating("countdown", 1, 1);
    }

    public void countdown()
    {
        setTime(time-1);
        if(time == 0)
        {
            timeOut();
        }
    }
    
    void setTime(int sec)
    {
        time = sec;
        int min = sec / 60;
        sec %= 60;
        
        timeLabel.GetComponent<UnityEngine.UI.Text>().text = min + ":" + ((sec < 10)?"0":"") + sec;
    }

    public void timeOut()
    {
        roundLose();
    }

    public void playerDeath(int weaponCode, bool isHead, GameObject enemy)
    {
        roundLose();
        GameObject info = Instantiate(kill_info_dialog, kill_info_dialog.transform.position, kill_info_dialog.transform.rotation);
        info.GetComponent<deathInfo>().configure(false, weaponCode, isHead, false, enemy.GetComponent<enemy>().name, yourName);
    }

    void gameWin()
    {
        endGame(1);
    }

    void gameTied()
    {
        endGame(0);
    }

    void gameLose()
    {
        endGame(-1);
    }

    void endGame(int isWinn)
    {
        canvas.GetComponent<ShowDialogs>().showGameEndDialog(isWinn, kills);
        
    }

    public void quitGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    void newRound()
    {
        round++;
        ppReset();
        ammo.GetComponent<ammoPanel>().resetAmmo();
        isStoped = false;
        setHealthy(PLAYERS_START_HEALTHY);
        killAllMobs();
        setTime(roundTime);
        startCountdown();
        enemyCount = 5;
        updateScore();
        setLook(1);
        if (isT)
            mobGenT.GetComponent<ENEMY_SPAWN>().creatFirstStrategy(strategy);
        else
            mobGenCT.GetComponent<ENEMY_SPAWN>().creatFirstStrategy(strategy);
    }

    void endRound()
    {
        isStoped = true;
        mobGenT.GetComponent<ENEMY_SPAWN>().resetActions();
        CancelInvoke("countdown");
        updateScore();
        if (tScore == 15 && ctScore == 15)
        {
            gameTied();
        }
        else if (tScore >= WIN_SCORE)
        {
            if (isT)
                gameWin();
            else
                gameLose();
        } 
        else if (ctScore >= WIN_SCORE)
        {
            if (isT)
                gameLose();
            else
                gameWin();
        }
        else
        {
            if (round == 15)
            {
                switchTeam();
            }
            Invoke("newRound",EndRoundShow.stayTime);
        }
    }

    void allKilled()
    {
        roundWin();
    }

    private void roundWin()
    {
        if (isT)
            tScore++;
        else
            ctScore++;
        StartCoroutine(showDialgNextFrame(true));
        endRound();
    }
    
    private void roundLose()
    {
        if (isT)
            ctScore++;
        else
            tScore++;
        StartCoroutine(showDialgNextFrame(false));
        endRound();
    }

    private IEnumerator showDialgNextFrame(bool roundWinn)
    {
        yield return new WaitForEndOfFrame();
        canvas.GetComponent<ShowDialogs>().showRoundEndDialog(isT?roundWinn:!roundWinn);
    }

    void updateScore()
    {
        ctScorLaber.GetComponent<Text>().text = ctScore + "";
        tScorLabel.GetComponent<Text>().text = tScore + "";
    }

    public bool hited(GameObject enemy, int damageGiven, bool isHead, bool isWall)
    {
        int weaponCode = 0; //TODO find code
        online.teamFriendDamage(enemy.GetComponent<enemy>().weaponCode, isWall, isHead, enemy.GetComponent<enemy>().id, damageGiven);
        if (enemy.GetComponent<enemy>().giveDamage(damageGiven) <= 0)
        {
            killed(enemy, isHead, isWall);
            return true;
        }

        return false;
    }

    public void killed(GameObject enemy, bool isHead, bool isWall)
    {
        Destroy(enemy);
        
        GameObject info = Instantiate(kill_info_dialog, kill_info_dialog.transform.position, kill_info_dialog.transform.rotation);
        info.GetComponent<deathInfo>().configure(isT, 0, isHead, isWall, yourName, enemy.GetComponent<enemy>().name);
        deactivePP(isT, enemy.GetComponent<enemy>().id);
        enemyCount--;
        kills++;
        switch (enemyCount)
        {
            case 4: case 3: case 2: case 1:
                break;
            case 0:
                allKilled();
                break;
        }
    }

    private void deactivePP(bool forCT, int id)
    {
        if (forCT)
        {
            switch (id)
            {
                case 0: ct1.SetActive(false);
                    break;
                case 1: ct2.SetActive(false);
                    break;
                case 2: ct3.SetActive(false);
                    break;
                case 3: ct4.SetActive(false);
                    break;
                case 4: ct5.SetActive(false);
                    break;
            }
        }
        else
        {
            switch (id)
            {
                case 0: t1.SetActive(false);
                    break;
                case 1: t2.SetActive(false);
                    break;
                case 2: t3.SetActive(false);
                    break;
                case 3: t4.SetActive(false);
                    break;
                case 4: t5.SetActive(false);
                    break;
            }
        }
    }

    private void ppReset()
    {
        
        t1.SetActive(true);
        t2.SetActive(true);
        t3.SetActive(true);
        t4.SetActive(true);
        t5.SetActive(true);

        //t1.GetComponent<Image>().sprite = isT? ownPP : (isOnline? otherPP : ctPP);
        t1.GetComponent<Image>().sprite = isT? ownPP : tPP;
        t2.GetComponent<Image>().sprite = tPP;
        t3.GetComponent<Image>().sprite = tPP;
        t4.GetComponent<Image>().sprite = tPP;
        t5.GetComponent<Image>().sprite = tPP;
        
        ct1.SetActive(true);
        ct2.SetActive(true);
        ct3.SetActive(true);
        ct4.SetActive(true);
        ct5.SetActive(true);

        //ct1.GetComponent<Image>().sprite = isT? (isOnline? otherPP : ctPP) : ownPP;
        ct1.GetComponent<Image>().sprite = isT? ctPP : ownPP;
        ct2.GetComponent<Image>().sprite = ctPP;
        ct3.GetComponent<Image>().sprite = ctPP;
        ct4.GetComponent<Image>().sprite = ctPP;
        ct5.GetComponent<Image>().sprite = ctPP;
    }

    void killAllMobs()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("CT");
        foreach(GameObject enemy in enemies)
            Destroy(enemy);
    }

    public void lookLeft()
    {
        if (isLokingIn <= 0)
            return;
        setLook(isLokingIn-1);
    }

    public void lookRight()
    {
        if (isLokingIn >= maxLooks-1)
            return;
        setLook(isLokingIn+1);
    }

    void setLook(int lookAt)
    {
        isLokingIn = lookAt;
        GameObject looks = getAimPoints(lookAt);
        gameObject.GetComponent<Transform>().position = looks.GetComponent<Transform>().position;
    }

    public GameObject getAimPoints(int lookAt)
    {
        return isT ? T_aimPoints[lookAt] : CT_aimPoints[lookAt];
    }
    
}
