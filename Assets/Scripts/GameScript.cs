using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class GameScript : MonoBehaviour
{
    public GameObject canvas, aim, timeLabel, ctScorLaber, tScorLabel, kill_info_dialog, sp0, t1, t2, t3, t4, t5, ct1, ct2, ct3, ct4, ct5, healthy_panel, healthy_panel_outside, healthy_text;
    public GameObject[] aimPoints; // B, Mid, Long
    public int isLokingIn, maxLooks, playersHealthy;

    private String yourName;
    private int time, ctCount, tScore = 0, ctScore = 0, kills = 0;
    public int roundTime;
    private Coroutine co;
    private static readonly int WIN_SCORE = 3;
    // Start is called before the first frame update
    void Start()
    {
        setHealthy(100);
        yourName = PlayerPrefs.GetString("name", "Mali");
        isLokingIn = 1; // Mid
        maxLooks = aimPoints.Length;
        newRound();
    }

    // Update is called once per frame
    void Update()
    {
       // setHealthy(playersHealthy); // TODO DELETE FROM HERE
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
        float maxHeight = healthy_panel_outside.GetComponent<RectTransform>().sizeDelta.y;
        RectTransform rct = healthy_panel.GetComponent<RectTransform>();
        rct.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxLength / 100 * playersHealthy);
        if (playersHealthy == 0)
            playerDeath();
    }

    public void givePlayerDamage(int damage)
    {
        setHealthy(playersHealthy - damage);
    }

    public void startCountdown()
    {
        InvokeRepeating("countdown", 0, 1);
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

    public void playerDeath()
    {
        roundLose();
    }

    void gameWin()
    {
        endGame();
        canvas.GetComponent<ShowDialogs>().showGameEndDialog(true, kills);
    }

    void gameLose()
    {
        endGame();
        canvas.GetComponent<ShowDialogs>().showGameEndDialog(false, kills);
    }

    void endGame()
    {
        sp0.GetComponent<mob>().stopMove();
    }

    public void quitGame()
    {
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    void newRound()
    {
        killAllMobs();
        setTime(roundTime+1);
        startCountdown();
        ctCount = 5;
        updateScore();
        sp0.GetComponent<CT_SPAWN>().creatFirstStrategy();
        ppSetActive(true);
        setLook(isLokingIn = 1);
    }

    void endRound()
    {
        CancelInvoke("countdown");
        updateScore();
        if (tScore >= WIN_SCORE)
        {
            gameWin();
        } 
        else if (ctScore >= WIN_SCORE)
        {
            gameLose();
        }
        else
        {
            newRound();
        }
    }

    void allKilled()
    {
        roundWin();
    }

    private void roundWin()
    {
        tScore++;
        canvas.GetComponent<ShowDialogs>().showRoundEndDialog(true);
        //gameObject.GetComponent<showWin>().show(true);
        endRound();
    }
    
    private void roundLose()
    {
        ctScore++;
        canvas.GetComponent<ShowDialogs>().showRoundEndDialog(false);
        //gameObject.GetComponent<showWin>().show(false);
        endRound();
    }

    void updateScore()
    {
        ctScorLaber.GetComponent<UnityEngine.UI.Text>().text = ctScore + "";
        tScorLabel.GetComponent<UnityEngine.UI.Text>().text = tScore + "";
    }

    public void hited(GameObject enemy, int damageGiven, bool isHead, bool isWall)
    {
        Debug.Log(enemy == null);
        if (enemy.GetComponent<enemy>().giveDamage(damageGiven) <= 0)
            killed(enemy, isHead, isWall);
    }

    public void killed(GameObject enemy, bool isHead, bool isWall)
    {
        Destroy(enemy);
        
        GameObject info = Instantiate(kill_info_dialog, kill_info_dialog.transform.position, kill_info_dialog.transform.rotation);
        info.GetComponent<deathInfo>().configure(true, 0, isHead, isWall, yourName, "Bot Test");
        ctCount--;
        kills++;
        switch (ctCount)
        {
            case 4: case 3: case 2: case 1:
                ppSetActive(false);
                //sp0.GetComponent<mob>().creatMob();
                break;
            case 0:
                ppSetActive(false);
                allKilled();
                break;
        }
    }

    private bool[] bArr = new bool[5];

    void ppSetActive(bool b)
    {
        if (b)
        {
            for (int i = 0; i < bArr.Length; i++)
            {
                bArr[i] = true;
            }
        }
        else
        {
            int j = 0;
            for (int i = 0; i < bArr.Length; i++)
            {
                if (bArr[i])
                {
                    j++;
                }
            }
            if (j == 0)
            {
                return;
            }
            int rnd;
            do
            {
                rnd = Random.Range(0, 5);
            } while (!bArr[rnd]);

            bArr[rnd] = false;
        }
        ct1.SetActive(bArr[0]);
        ct2.SetActive(bArr[1]);
        ct3.SetActive(bArr[2]);
        ct4.SetActive(bArr[3]);
        ct5.SetActive(bArr[4]);
        
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
        setLook(--isLokingIn);
    }

    public void lookRight()
    {
        if (isLokingIn >= maxLooks-1)
            return;
        setLook(++isLokingIn);
    }

    void setLook(int lookAt)
    {
        GameObject looks = aimPoints[lookAt];
        gameObject.GetComponent<Transform>().position = looks.GetComponent<Transform>().position;
    }
    
    public enum T_STRATEGY
    {
        B, MID, Long
    }
    
    public enum CT_STRATEGY
    {
        B1, B2, MID, LowerT, Short, Long
    }
    
}
