using System;
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

public class TeamFriend
{
    public int id;
    public int healty;
    public GameObject pp;
    public String name;

    public TeamFriend(int id, String name, GameObject pp)
    {
        this.id = id;
        this.name = name;
        this.pp = pp;
    }

    public void setReset()
    {
        pp.SetActive(true);
        setHealthy(ENEMY_SPAWN.firstHealthy);
    }

    public void setHealthy(int healthy)
    {
        this.healty = healthy;
        // TODO helthybar
        if (healthy <= 0)
            kill();
    }

    public void kill()
    {
        this.healty = 0;
        pp.SetActive(false);
    }


    public bool giveDamage(int damage)
    {
        setHealthy(healty - damage);
        return healty <= 0;
    }
}

public class GameScript : MonoBehaviour
{
    public GameObject canvas, aim, timeLabel, ctScorLaber, tScorLabel, kill_info_dialog, mobGenT, mobGenCT, healthy_panel,
        healthy_panel_outside, healthy_text, ammo, bomb_prefab, bomb_icon_hud, plant_bomb_button, flash_button, colorTag_prefab, buy_panel_prefab, created_buy_panel,
        knife_button, map_prefab, created_map;
    private GameObject created_bomb_icon, created_bomb;
    public GameObject[] createdColorTags;
    public GameObject[] T_aimPoints; // B, Mid, Long
    public GameObject[] CT_aimPoints; // Long, Mid, B
    public GameObject[] ctPPholder;
    public GameObject[] tPPholder;
    public GameScriptOnline online;
    public GameMoney gameMoney;
    public Sprite ownPP, tPP, ctPP, ctKnifeSprite, tKnifeSprite;
    public int maxLooks, playersHealthy, PLAYERS_START_HEALTHY;
    public static int isLokingIn;
    public bool isT;
    public static int gameMode; // 0: Ranked
    public bool isGameFinished; // 0: Ranked

    public static String yourName;
    private int time, tScore, ctScore, kills, round, buyTime;
    public int roundTime, enemyCount, teamCount, BUY_TIME;
    public int round_per_half, defLookPintCT = 1, defLookPintT = 1;
    private static readonly int WIN_SCORE = 16;
    public static bool isStoped = true;
    public static bool am_i_Death;

    public static int rank, START_ENEMY_COUNT = 5;
    public static int tot_rank = 18;

    public OnlineData online_data;
    public bool isOnline, isOtherPlayerSpawned;
    public string[] myTeam;
    public string[] otherTeam;
    private static int otherRank;
    private Sprite otherPP;

    public TeamFriend[] friends;
    public Online strategy;
    public List<String> enemysNameList;
    public int bombIsPlanted, whereIsOther;

    public string correctBombPin;
    public bool hasCtKit;

    private int countOfFlashs, countOfZeus, countDefuseKit;

    public void boughtFlash()
    {
        countOfFlashs++;
    }
    public void boughtZeus()
    {
        countOfZeus++;
    }
    public void boughtDefuseKit()
    {
        hasCtKit = true;
    }

    public void resetItems()
    {
        hasCtKit = false;
        countOfFlashs = 0;
        countOfZeus = 0;
    }
    void Start()
    {
        Application.targetFrameRate = 300;

        online = gameObject.GetComponent<GameScriptOnline>();
        isOnline = GameObject.Find("Data") != null;
        if (isOnline) {
            online_data = GameObject.Find("Data").GetComponent<OnlineData>();
            initializeOtherTeam();
            strategy = Online.WRITE;
        }
        else
        {
            strategy = Online.OFFLINE;
            //hideOnlineObjects();
        }
        isT = isOnline? online_data.isT_me : Random.Range(0,2) == 1;
        initializeMyTeam();
        gameMode = 0;
        
        enemysNameList = new List<String>();
        kills = 0;
        round = (WIN_SCORE - round_per_half);
        ctScore = round / 2;
        tScore = round - ctScore;
        rank = PlayerPrefs.GetInt("rank",4);
        yourName = PlayerPrefs.GetString("name", "Mali");
        maxLooks = isT ? T_aimPoints.Length : CT_aimPoints.Length;
        getEnemySpawn().initEnemysFirstNameList(START_ENEMY_COUNT, isOnline);
        newTeam();
        resetLook();

        beforeNewRound();
    }

    private void refreshKnifeButtonImg()
    {
        knife_button.GetComponent<Image>().sprite = isT ? tKnifeSprite : ctKnifeSprite;
    }

    private void hideOnlineObjects()
    {
        plant_bomb_button.SetActive(false);
        flash_button.SetActive(false);
    }

    private void refreshColorTags()
    {
        for (int i = 0; i < createdColorTags.Length; i++)
            Destroy(createdColorTags[i]);
        createdColorTags = new GameObject[START_ENEMY_COUNT];
        for (int i = 0; i < createdColorTags.Length; i++)
        {
            GameObject container_colorTag = getPPHolder(i);
            createdColorTags[i] = Instantiate(colorTag_prefab);
            createdColorTags[i].GetComponent<ColorTag>().configure(i, container_colorTag);
        }
    }

    GameObject getPPHolder(int i)
    {
        return isT ? tPPholder[i] : ctPPholder[i];
    }

    private void initializeMyTeam()
    {
        myTeam = new string[START_ENEMY_COUNT];
        myTeam[0] = yourName;
        for (int i = 1; i < START_ENEMY_COUNT; i++)
        {
            myTeam[i] = PlayerPrefs.GetString("game_firend_" + i, "US_" + i + "_name"); // TODO
        }
    }

    private void initializeOtherTeam()
    {
        otherTeam = new string[START_ENEMY_COUNT];
        otherTeam[0] = online_data.name_him;
    }

    private void newTeam()
    {
        resetItems();
        gameMoney.setMoney(gameMoney.FIRST_MONEY);
        friends = new TeamFriend[START_ENEMY_COUNT];
        myTeam[0] = yourName;
        for (int i = 0; i < friends.Length; i++)
            friends[i] = new TeamFriend(i, myTeam[i], isT? tPPholder[i]: ctPPholder[i]);
        knife_button.SetActive(!isT);
        refreshColorTags();
        refreshKnifeButtonImg();
    }

    public void getOnlineShot()
    {
        
    }

    public void friendGotShot(int weaponCode, bool isWall, bool isHead, int enemyId, int damage)
    {
        if (friends[enemyId].giveDamage(damage))
        {
            showKillInfo(!isT, weaponCode, isHead, isWall, online_data.name_him, myTeam[enemyId]);
            teamCount--;
            switch (teamCount)
            {
                case 4: case 3: case 2: case 1:
                    break;
                case 0:
                    setHealthy(playersHealthy - damage);
                    teamDeath();
                    break;
            }
        }
    }

    public void resetLook()
    {
        setLook(isT ? defLookPintT : defLookPintCT);
    }

    public void switchTeam()
    {
        //changeStrategy(); TODO 
        isT = !isT;
        int scoreTmp = tScore;
        tScore = ctScore + (WIN_SCORE - round_per_half) / 2;
        ctScore = scoreTmp + (WIN_SCORE - round_per_half) / 2;
        updateScore();
        
        newTeam();
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
        //rct.localScale = new Vector3(playersHealthy / 100f, rct.localScale.y,rct.localScale.z);
    }

    public void startCountdown(string methodName)
    {
        InvokeRepeating(methodName, 1, 1);
    }

    public void countdown()
    {
        setTime(time-1);
        if(time <= 0)
        {
            CancelInvoke(nameof(timeOut));
            timeOut();
        }
    }

    public void countdownForBuyTime()
    {
        setTimeLabel(--buyTime);
        if(buyTime <= 0)
        {
            CancelInvoke(nameof(countdownForBuyTime));
            newRound();
        }
    }
    
    void setTime(int sec)
    {
        time = sec;
        setTimeLabel(sec);
    }

    void setTimeLabel(int sec)
    {
        int min = sec / 60;
        sec %= 60;
        
        timeLabel.GetComponent<UnityEngine.UI.Text>().text = min + ":" + ((sec < 10)?"0":"") + sec;
    }

    public void timeOut()
    {
        CT_win();
    }

    public void T_win()
    {
        if (isT)
            roundWin();
        else
            roundLose();
    }

    public void CT_win()
    {
        if (isT)
            roundLose();
        else
            roundWin();
    }

    public void playerDeath(int weaponCode, bool isHead, GameObject enemy)
    {
        if (!isOnline)
            roundLose();
        showKillInfo(!isT, weaponCode, isHead, false, enemy.GetComponent<enemy>().name, yourName);
        am_i_Death = true;
    }
    

    public void showKillInfo(bool ctIsDeath, int weaponCode, bool isHead, bool isWall, String killerName,String killedName)
    {
        GameObject info = Instantiate(kill_info_dialog, kill_info_dialog.transform.position, kill_info_dialog.transform.rotation);
        info.GetComponent<deathInfo>().configure(ctIsDeath, weaponCode, isHead, isWall, killerName, killedName);
    }

    public void gameWin()
    {
        endGame(1);
    }

    public void gameTied()
    {
        endGame(0);
    }

    public void gameLose()
    {
        endGame(-1);
    }

    public void gameQuit()
    {
        banControl();
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    void endGame(int isWinn)
    {
        if(isOnline)
            online.disconnect();
        if (isWinn == -2)
            return;
        isGameFinished = true;
        canvas.GetComponent<ShowDialogs>().showGameEndDialog(isWinn, kills);
    }

    public bool bombHasBeenPlant()
    {
        return bombIsPlanted != 0;
    }

    public void knifeButtonListener()
    {
        openMap(MapChose.knife);
    }

    public void bombButtonListener()
    {
        openMap(isT? MapChose.plant : MapChose.defus);
    }

    public void openMap(MapChose why)
    {
        if (created_map != null)
            return;
        created_map = Instantiate(map_prefab);
        created_map.GetComponent<Map>().mapChose = why;
    }

    public void openBomb(int side)
    {
        if (created_bomb != null)
            return;
        if (isT && !bombHasBeenPlant())
        {
            created_bomb = Instantiate(bomb_prefab);
            created_bomb.GetComponent<Bomb>().plantingSide = side;
            created_bomb.GetComponent<Bomb>().forPlanting();
        }
        else if (!isT && bombHasBeenPlant())
        {
            created_bomb = Instantiate(bomb_prefab);
            created_bomb.GetComponent<Bomb>().plantingSide = side;
            created_bomb.GetComponent<Bomb>().forDefusing(correctBombPin);
        }
        online.openBomb(side);
    }

    public void throughFlash()
    {
        if (countOfFlashs <= 0)
            return;
        countOfFlashs--;
        online.sendFlash();
    }

    public void explodeFlash()
    {
        GameObject panel = new GameObject("Flash");
        panel.AddComponent<CanvasRenderer>();
        RectTransform rc = panel.AddComponent<RectTransform>();
        Image i = panel.AddComponent<Image>();
            
        rc.anchorMin = new Vector2(0, 0);
        rc.anchorMax = new Vector2(1, 1);
        rc.offsetMin = new Vector2(0, 0);
        rc.offsetMax = new Vector2(0, 0);

        i.color = Color.white;
        panel.transform.SetParent(canvas.transform, false);
        
        StartCoroutine(flashFade(panel, 0.0f, 2.0f, 1.5f));
    }
    
    IEnumerator flashFade(GameObject panel, float aValue, float aTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        float alpha = panel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            panel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        Destroy(panel);
    }

    public void banControl()
    {
        if (!isGameFinished && isOnline)
        {
            PlayerPrefs.SetInt("loseSerie",-1);
            endGame(-2);
        }
    }

    private void OnDisable()
    {
        banControl();
    }
    void newRound()
    {
        cleanUpForRound();
        //countOfFlashs = startFlashCounts;
        isStoped = false;
        setTime(roundTime);
        startCountdown(nameof(countdown));
        getEnemySpawn().creatFirstStrategy(strategy, isOnline);
    }

    private void cleanUpForRound()
    {
        if (created_buy_panel != null)
            created_buy_panel.GetComponent<BuyPanel>().close();
        Destroy(created_bomb_icon);
        Destroy(created_bomb);
        timeLabel.SetActive(true);
        am_i_Death = false;
        ppReset();
        ammo.GetComponent<ammoPanel>().resetAmmo();
        setHealthy(PLAYERS_START_HEALTHY);
        killAllMobs();
        bombIsPlanted = 0;
        whereIsOther = 0;
        setLook(1);
        isOtherPlayerSpawned = false;
        enemyCount = START_ENEMY_COUNT;
        teamCount = START_ENEMY_COUNT;
    }

    private void beforeNewRound()
    {
        cleanUpForRound();
        isStoped = true;
        round++;
        updateScore();
        buyTime = BUY_TIME;
        setTimeLabel(buyTime);
        created_buy_panel = Instantiate(buy_panel_prefab);
        startCountdown(nameof(countdownForBuyTime));
    }

    private ENEMY_SPAWN getEnemySpawn()
    {
        if (isT)
            return mobGenT.GetComponent<ENEMY_SPAWN>();
        else
            return mobGenCT.GetComponent<ENEMY_SPAWN>();
    }

    void endRound()
    {
        isStoped = true;
        getEnemySpawn().resetActions();
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
            Invoke("beforeNewRound",EndRoundShow.stayTime);
        }
    }

    void allKilled()
    {
        if (bombHasBeenPlant() && !isT)
            return;
        roundWin();
    }

    void teamDeath()
    {
        if(bombHasBeenPlant() && isT)
            return;
        roundLose();
    }

    private void roundWin()
    {
        gameMoney.addMoney(gameMoney.ROUNDWIN_MONEY);
        giveScore(true);
        StartCoroutine(showDialgNextFrame(isT));
        endRound();
    }
    
    private void roundLose()
    {
        resetItems();
        gameMoney.addMoney(gameMoney.ROUNDLOSE_MONEY);
        giveScore(false);
        StartCoroutine(showDialgNextFrame(!isT));
        endRound();
    }

    private void giveScore(bool forYou)
    {
        if (forYou)
            if (isT)
                tScore++;
            else
                ctScore++;
        else
            if (!isT)
                tScore++;
            else
                ctScore++;
    }

    private IEnumerator showDialgNextFrame(bool forT)
    {
        yield return new WaitForEndOfFrame();
        canvas.GetComponent<ShowDialogs>().showRoundEndDialog(forT);
    }

    void updateScore()
    {
        ctScorLaber.GetComponent<Text>().text = ctScore + "";
        tScorLabel.GetComponent<Text>().text = tScore + "";
    }

    public bool hited(GameObject enemy, int damageGiven, bool isHead, bool isWall)
    {
        int weaponCode = 0; //TODO find code
        online.hited(enemy.GetComponent<enemy>().weaponCode, isWall, isHead, enemy.GetComponent<enemy>().id, damageGiven);
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
        
        showKillInfo(isT, 0, isHead, isWall, yourName, enemy.GetComponent<enemy>().name);
        deactivePP(isT, enemy.GetComponent<enemy>().id);
        enemyCount--;
        kills++;
        switch (enemyCount)
        {
            case 4: case 3: case 2:
                break;
            case 1:
                if (isOnline && !isOtherPlayerSpawned)
                {
                    getEnemySpawn().createNew(strategy, isOnline, 0);
                    isOtherPlayerSpawned = true;
                }

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
            ctPPholder[id].SetActive(false);
        }
        else
        {
            tPPholder[id].SetActive(false);
        }
    }

    private void ppReset()
    {
        for (int i = 0; i < START_ENEMY_COUNT; i++)
        {
            ctPPholder[i].SetActive(true);
            tPPholder[i].SetActive(true);
            tPPholder[i].GetComponent<Image>().sprite = tPP;
            ctPPholder[i].GetComponent<Image>().sprite = ctPP;
            friends[i].setReset();
        }

        if (isT)
            tPPholder[0].GetComponent<Image>().sprite = ownPP;
        else
            ctPPholder[0].GetComponent<Image>().sprite = ownPP;
            
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

    public void bombPlanted(string pin, int plantSide)
    {
        gameMoney.addMoney(gameMoney.BOMBPLANT_MONEY);
        if (isT)
            online.bombPlanted(pin, plantSide);
        correctBombPin = pin;
        bombIsPlanted = plantSide;
        whereIsOther = 0;
        created_bomb_icon = Instantiate(bomb_icon_hud);
        timeLabel.SetActive(false);
        CancelInvoke("countdown");
        CancelInvoke();
    }

    public void bombDefused()
    {
        gameMoney.addMoney(gameMoney.BOMBDEFUSE_MONEY);
        if (!isT)
            online.bombDefused();
        CT_win();
    }

    public void bombExplode()
    {
        T_win();
    }

    public void otherLeavs()
    {
        if (!isGameFinished)
            gameWin();
    }

    public void bombOpened(int side)
    {
        whereIsOther = side;
        //TODO difus starting sound or planting
    }

    public void mapOK(int side, MapChose mapChose)
    {
        switch (mapChose)
        {
            case MapChose.plant:
                openBomb(side);
                break;
            case MapChose.defus:
                if (bombIsPlanted == side)
                    openBomb(side);
                else 
                    Debug.Log("Wrong Side Chosed for defuse");
                break;
            case MapChose.knife:
                Debug.Log("side--:" + side);
                Debug.Log("bombIsPlanted--:" + bombIsPlanted);
                if (whereIsOther == side)
                    knifeOther();
                else 
                    Debug.Log("Wrong Side Chosed for Knife");
                break;
        }
    }

    private void knifeOther()
    {
        gameMoney.addMoney(gameMoney.KNIFE_MONEY);
        showKillInfo(!isT, 3, false, false, yourName, enemysNameList[0]);
        //TODO kil other
        roundWin();
        online.knifeOther();
    }

    public void getKnifeTry()
    {
        if (created_bomb != null)
            knifed();
        else
            Debug.Log("No one is there");
    }

    private void knifed()
    {
        //TODO me
        roundLose();
        isOtherPlayerSpawned = true;
        showKillInfo(!isT, 3, false, false, enemysNameList[0], yourName);
    }

    public void closeBomb()
    {
        online.closeBomb();
    }

    public void otherCloseBomb()
    {
        whereIsOther = 0;
    }
}
