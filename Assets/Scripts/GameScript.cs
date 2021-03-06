﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using CompleteProject;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public enum RoundEnd
{
    TimeEnd,
    Knifed,
    AllDeath,
    Defused,
    Explode
}

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
        knife_button, rightButton, leftButton, shotButton, zoomButton, map_prefab, created_map, quitGameMenuPrefab, createdQuitGameMenuPrefab;
    private GameObject created_bomb_icon, created_bomb;
    private ArraysData arraysData;
    public GameObject[] createdColorTags;
    public GameObject[] T_aimPoints; // B, Mid, Long
    public GameObject[] CT_aimPoints; // Long, Mid, B
    public GameObject[] ctPPholder;
    public GameObject[] tPPholder;
    public GameScriptOnline online;
    public GameMoney gameMoney;
    public Sprite ownPP, tPP, ctPP;
    public int maxLooks, playersHealthy, PLAYERS_START_HEALTHY;
    public static int isLokingIn;
    public bool isT;
    public static int gameMode; // 0: Ranked
    public bool isGameFinished; // 0: Ranked
    public Inventory inventory;
    public bool gameQuitNow;

    public static String yourName;
    private int time, tScore, ctScore, kills, round, buyTime;
    public int roundTime, enemyCount, teamCount, BUY_TIME;
    public int round_per_half, defLookPointCT = 1, defLookPointT = 1;
    private static readonly int WIN_SCORE = 16;
    public static bool isStoped = true;
    public static bool am_i_Death;
    private bool roundEnd;

    public static int rank, START_ENEMY_COUNT = 5;
    public static int tot_rank = 18;

    public OnlineData online_data;
    public bool isOnline, isOtherPlayerSpawned;
    public string[] myTeam;
    private static int otherRank;
    private Sprite otherPP;
    public Sprite a_side_sprite, b_side_sprite;
    public GameObject bgSideImg;
    public Sprite bombPlantButtonSprite, bombDefuseButtonSprite;
    public Sprite[] knifeButtonSprite;
    public TextAsset weaponList;

    private Queue<Coroutine> startedCoroutineBombKnife;
    public TeamFriend[] friends;
    public Online strategy;
    public List<String> enemysNameList;
    public int bombIsPlanted, whereIsOther;
    public int myWeapon = deathInfo.AWP_CODE;

    public string correctBombPin;
    public bool hasCtKit;

    public int countOfFlashs, countOfZeus;

    public Color32 lastSecColor, normalSecColor;

    public AudioSource timeAS, bombAS, flashAS, knifeAS;
    public AudioClip bombHasBeenDefusedAC, bombHasBeenPlantedAC, bombPlantStartAC, bombDefusStarAC, bombExplodeAC, lastSecondsAC, goChangeSideStepAC, goBombStepAC;
    public AudioClip throughFlashAC, farFlashAC, explodeFlashAC;
    public AudioClip T_Win_AC, CT_Win_AC;
    public AudioClip getKnifedAC, knifeAirAC, getZeusedAC, zeusAirAC;
    public AudioClip awpShotDeath;
    public AudioClip[] otherShotsAWP;

    void takeFlash()
    {
        if (countOfFlashs <= 0)
            return;
        countOfFlashs--;
        refreshFlashCount();
    }

    void refreshFlashCount()
    {
        flash_button.GetComponent<flasbangCounter>().setCount(countOfFlashs);
    }

    public void boughtFlash()
    {
        countOfFlashs++;
        refreshFlashCount();
    }
    public void boughtZeus()
    {
        countOfZeus++;
        refreshKnife();
    }
    public void boughtDefuseKit()
    {
        inventory.set(0);
        hasCtKit = true;
    }

    public void resetItems()
    {
        inventory.clear();
        hasCtKit = false;
        countOfFlashs = 0;
        countOfZeus = 0;
        refreshKnife();
        refreshFlashCount();
    }

    public void changeKnife(int id)
    {
        knife_button.GetComponent<buttonDelay>().changeSprite(knifeButtonSprite[id]);
    }

    private void Update()
    {
        if (gameQuitNow)
            gameQuit();
    }

    void Start()
    {
        if (GameObject.Find(MainMenu.ArraysDataName) == null)
        {
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
            return;
        }

        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        startedCoroutineBombKnife = new Queue<Coroutine>();
        Application.targetFrameRate = 300;

        online = gameObject.GetComponent<GameScriptOnline>();
        GameObject data = GameObject.Find("Data");
        isOnline = data != null;
        if (isOnline) {
            EndGameShow.addPlays(-1);
            online_data = data.GetComponent<OnlineData>();
            strategy = Online.WRITE;
            rank = (PlayerPrefs.GetInt("rank",4) + online_data.rank_him)/2;
        }
        else
        {
            rank = PlayerPrefs.GetInt("rank",4);
            strategy = Online.OFFLINE;
            hideOnlineObjects();
        }
        isT = isOnline? online_data.getTeam() : Random.Range(0, 2) == 1;
        initializeMyTeam();
        gameMode = 0;
        enemysNameList = new List<String>();
        kills = 0;
        round = (WIN_SCORE - round_per_half);
        ctScore = round / 2;
        tScore = round - ctScore;
        yourName = PlayerPrefs.GetString("name", LanguageSystem.GET_NAME());
        maxLooks = isT ? T_aimPoints.Length : CT_aimPoints.Length;
        getEnemySpawn().initEnemysFirstNameList(START_ENEMY_COUNT, isOnline);
        newTeam();
        resetLook();

        beforeNewRound();
    }

    private void Awake()
    {
        Application.runInBackground = true;
    }

    private void hideOnlineObjects()
    {
        plant_bomb_button.SetActive(false);
        flash_button.SetActive(false);
        knife_button.SetActive(false);
    }

    private void refreshColorTags()
    {
        for (int i = 0; i < createdColorTags.Length; i++)
            Destroy(createdColorTags[i]);
        createdColorTags = new GameObject[START_ENEMY_COUNT];
        for (int i = 0; i < createdColorTags.Length; i++)
        {
            GameObject container_colorTag = getPPHolder(isT, i);
            createdColorTags[i] = Instantiate(colorTag_prefab);
            createdColorTags[i].GetComponent<ColorTag>().configure(i, container_colorTag);
        }
        
        for (int i = 0; i < createdColorTags.Length; i++)
        {
            GameObject container_colorTag = getPPHolder(!isT, i);
            createdColorTags[i] = Instantiate(colorTag_prefab);
            createdColorTags[i].GetComponent<ColorTag>().configure(i, container_colorTag);
        }
    }

    GameObject getPPHolder(bool forT, int i)
    {
        return forT ? tPPholder[i] : ctPPholder[i];
    }

    private void initializeMyTeam()
    {
        myTeam = new string[START_ENEMY_COUNT];
        myTeam[0] = yourName;
        for (int i = 0; i < START_ENEMY_COUNT-1; i++)
        {
            myTeam[i+1] = PlayerPrefs.GetString("game_firend_" + i, "US_" + i + "_name");
        }
    }

    private void newTeam()
    {
        resetItems();
        plant_bomb_button.GetComponent<buttonDelay>().changeSprite(isT? bombPlantButtonSprite : bombDefuseButtonSprite);
        gameMoney.setMoney(gameMoney.FIRST_MONEY);
        friends = new TeamFriend[START_ENEMY_COUNT];
        myTeam[0] = yourName;
        for (int i = 0; i < friends.Length; i++)
            friends[i] = new TeamFriend(i, myTeam[i], isT? tPPholder[i]: ctPPholder[i]);
        refreshColorTags();
    }

    public void getOnlineShot()
    {
        GetComponent<AudioSource>().PlayOneShot(otherShotsAWP[Random.Range(0,otherShotsAWP.Length)]);
    }

    public void friendGotShot(int weaponCode, bool isWall, bool isHead, int enemyId, int damage, int style)
    {
        if (friends[enemyId].giveDamage(damage))
        {
            showKillInfo(!isT, weaponCode, isHead, isWall, online_data.otherTeam[0], myTeam[enemyId]);
            teamCount--;
            switch (teamCount)
            {
                case 4: case 3: case 2: case 1:
                    break;
                case 0:
                    setHealthy(playersHealthy - damage);
                    iAmDeath();
                    teamDeath(style);
                    break;
            }
        }
    }

    public void resetLook()
    {
        setLook(isT ? defLookPointT : defLookPointCT, false);
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
        if (buyTime <= 3)
        {
            if (buyTime == 3)
                timeLabel.GetComponent<Text>().color = lastSecColor;
            timeAS.PlayOneShot(lastSecondsAC);
        }

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
        if (sec < 0) 
            return;
        timeLabel.GetComponent<Text>().text = min + ":" + ((sec < 10)?"0":"") + sec;
    }

    public void timeOut()
    {
        CT_win(RoundEnd.TimeEnd);
    }

    public void T_win(RoundEnd why)
    {
        if (isT)
        {
            if (isOnline)
            {
                if (!PhotonNetwork.IsMasterClient)
                    return;
                sendRoundLose(why);
            }
            roundWin(why);
            Debug.Log("rw0");
        }
        else
        {
            if (isOnline)
            {
                if (!PhotonNetwork.IsMasterClient)
                    return;
                sendRoundWin(why);
            }
            roundLose(why);
            Debug.Log("rl0");
        }
    }

    public void CT_win(RoundEnd why)
    {
        
        if (isT)
        {
            if (isOnline)
            {
                if (!PhotonNetwork.IsMasterClient)
                    return;
                sendRoundWin(why);
            }
            roundLose(why);
            Debug.Log("rl1");
        }
        else
        {
            if (isOnline)
            {
                if (!PhotonNetwork.IsMasterClient)
                    return;
                sendRoundLose(why);
            }
            roundWin(why);
            Debug.Log("rw1");
        }
    }

    public void playerDeath(int weaponCode, bool isHead, GameObject enemy)
    {
        if (!isOnline)
        {
            roundLose(RoundEnd.AllDeath);
            Debug.Log("rl2");
        }
        else
            ;// TODOBURAYA
        showKillInfo(!isT, weaponCode, isHead, false, enemy.GetComponent<enemy>().name, yourName);
        am_i_Death = true;
    }
    

    public void showKillInfo(bool ctIsDeath, int style, bool isHead, bool isWall, String killerName,String killedName)
    {
        GameObject info = Instantiate(kill_info_dialog, kill_info_dialog.transform.position, kill_info_dialog.transform.rotation);
        info.GetComponent<deathInfo>().configure(ctIsDeath, style, isHead, isWall, killerName, killedName);
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

    public void homeButtonListener()
    {
        if (createdQuitGameMenuPrefab != null)
            return;
        
        if (isGameFinished)
            gameQuit();
        else
            createdQuitGameMenuPrefab = Instantiate(quitGameMenuPrefab, canvas.transform, false);
    }

    public void gameQuit()
    {
        if(isOnline)
            online.disconnect();
        banControl();
        ButtonClickSound(4);
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    void endGame(int isWinn)
    {
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
        if(isStoped)
            return;
        if (countOfZeus > 0)
            openMap(MapChose.zeus);
        else
            openMap(MapChose.knife);
    }

    public void bombButtonListener()
    {
        if(isStoped)
            return;
        openMap(isT? MapChose.plant : MapChose.defus);
    }

    public void openMap(MapChose why)
    {
        if (checkBomb(why))
            return;
        created_map = Instantiate(map_prefab);
        created_map.GetComponent<Map>().mapChose = why;
    }

    private bool checkBomb(MapChose why)
    {
        if (created_map != null)
            return true;
        if (why == MapChose.plant && bombHasBeenPlant())
            return true;
        if (why == MapChose.defus && !bombHasBeenPlant())
            return true;
        return false;
    }

    public void openBomb()
    {
        if (created_bomb != null)
            return;
        if (isT && !bombHasBeenPlant())
        {
            created_bomb = Instantiate(bomb_prefab);
            bombAS.PlayOneShot(bombPlantStartAC);
            created_bomb.GetComponent<Bomb>().plantingSide = localInvokeChosedSide;
            created_bomb.GetComponent<Bomb>().forPlanting();
        }
        else if (!isT && bombHasBeenPlant())
        {
            created_bomb = Instantiate(bomb_prefab);
            bombAS.PlayOneShot(bombDefusStarAC);
            created_bomb.GetComponent<Bomb>().plantingSide = localInvokeChosedSide;
            created_bomb.GetComponent<Bomb>().forDefusing(correctBombPin);
        }
        online.openBomb(localInvokeChosedSide, isT);
    }

    public void throughFlash()
    {
        if (countOfFlashs <= 0)
            return;
        takeFlash();
        flashAS.PlayOneShot(throughFlashAC);
        Invoke(nameof(explodeFarFlash),2f);
    }

    public void explodeFarFlash()
    {
        flashAS.PlayOneShot(farFlashAC);
        online.sendFlash();
    }

    public void explodeFlash()
    {
        flashAS.PlayOneShot(explodeFlashAC);
        GameObject panel = createFullScreenCanvas("Flash", Color.white);
        
        StartCoroutine(flashFade(panel, 0.0f, panel.GetComponent<Image>().color, 2.0f, 1.5f, null));
    }

    private GameObject createFullScreenCanvas(string objectName, Color color)
    {
        GameObject go = new GameObject(objectName);
        go.AddComponent<CanvasRenderer>();
        RectTransform rc = go.AddComponent<RectTransform>();
        Image i = go.AddComponent<Image>();
            
        rc.anchorMin = new Vector2(0, 0);
        rc.anchorMax = new Vector2(1, 1);
        rc.offsetMin = new Vector2(0, 0);
        rc.offsetMax = new Vector2(0, 0);

        i.color = color;
        go.transform.SetParent(canvas.transform, false);
        return go;
    }

    public void walk(bool longWalk, string methodName)
    {
        AudioClip walkSound = longWalk ? goBombStepAC : goChangeSideStepAC;
        float alphaTime = 0.3f;
        GetComponent<AudioSource>().PlayOneShot(walkSound);
        GameObject walk = new GameObject("walk");
        walk.AddComponent<CanvasRenderer>();
        RectTransform rc = walk.AddComponent<RectTransform>();
        Image i = walk.AddComponent<Image>();
            
        rc.anchorMin = new Vector2(0, 0);
        rc.anchorMax = new Vector2(1, 1);
        rc.offsetMin = new Vector2(0, 0);
        rc.offsetMax = new Vector2(0, 0);

        i.color = Color.black;
        walk.transform.SetParent(canvas.transform, false);
        
        StartCoroutine(flashFade(walk, 0.0f, i.color, alphaTime, walkSound.length-alphaTime, methodName));
    }
    
    IEnumerator flashFade(GameObject panel, float aValue, Color32 c, float aTime, float delayTime, string methodName)
    {
        yield return new WaitForSeconds(delayTime);
        float alpha = panel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            if (am_i_Death)
            {
                Destroy(panel);
                yield break;
            }

            Color newColor = new Color(c.r, c.g, c.b, Mathf.Lerp(alpha,aValue,t));
            panel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        Destroy(panel);
        if (methodName != null && !isStoped && !am_i_Death)
            StartCoroutine(methodName);
    }
    
    IEnumerator killedRedScreen(GameObject panel, float aValue, float aTime)
    {
        Color c = panel.GetComponent<Image>().color;
        float alpha = panel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(c.r, c.g, c.b, Mathf.Lerp(alpha,aValue,t));
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
        isStoped = false;
        setTime(roundTime);
        timeLabel.GetComponent<Text>().color = normalSecColor;
        startCountdown(nameof(countdown));
        getEnemySpawn().creatFirstStrategy(strategy, isOnline);
        setButtonsActive(true);
    }

    void setBGside(int i)
    {
        if (i == 0)
        {
            bgSideImg.GetComponent<Image>().sprite = null;
            bgSideImg.SetActive(false);
            return;
        }
        
        bgSideImg.SetActive(true);
        bgSideImg.GetComponent<Image>().sprite = i == 1 ? a_side_sprite : b_side_sprite;
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
        if (isT)
            inventory.set(1);
        bombIsPlanted = 0;
        whereIsOther = 0;
        resetLook();
        isOtherPlayerSpawned = false;
        enemyCount = START_ENEMY_COUNT;
        teamCount = START_ENEMY_COUNT;
    }

    public void onlineReceiveEndRound(int tScore, int ctScore, bool isRoundWin, RoundEnd why)
    {
        this.tScore = tScore;
        this.ctScore = ctScore;
        Debug.Log("I receive now round win: " + isRoundWin + " ct:"+ctScore+" t:" + tScore);
        if (isRoundWin)
        {
            roundWin(why);
            Debug.Log("rw2");
        }
        else
        {
            roundLose(why);
            Debug.Log("rl3");
        }
    }

    private void beforeNewRound()
    {
        setButtonsActive(false);
        setBGside(0);
        cleanUpForRound();
        isStoped = true;
        round++;
        updateScore();
        if (isOnline)
            setForBuy();
        else
            newRound();
    }

    private void setForBuy()
    {
        buyTime = BUY_TIME;
        setTimeLabel(buyTime);
        created_buy_panel = Instantiate(buy_panel_prefab);
        timeLabel.GetComponent<Text>().color = normalSecColor;
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
        setButtonsActive(false);
        cancelKnifeAndBomb();
        destroyPrefabs();
        //if (!bombHasBeenPlant() && time > 0)
        isStoped = true;
        getEnemySpawn().resetActions();
        CancelInvoke(nameof(countdown));
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
            Invoke(nameof(beforeNewRound),EndRoundShow.stayTime);
        }
    }

    private void destroyPrefabs()
    {
        closePrefab(created_bomb);
        closePrefab(created_map);
        closePrefab(created_buy_panel);
        closePrefab(created_bomb_icon);
    }

    private void closePrefab(GameObject obj)
    {
        if (obj != null)
            Destroy(obj);
    }
    
    void allKilled()
    {
        if (bombHasBeenPlant() && !isT)
            return;
        
        if (isOnline)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            sendRoundLose(RoundEnd.AllDeath);
            Debug.Log("rl4");
        }
        roundWin(RoundEnd.AllDeath);
        Debug.Log("rw3");
    }

    void teamDeath(int style)
    {
        if(bombHasBeenPlant() && isT)
            return;
        StartCoroutine(showDeathWeapon(0.2f,0,style));
        if (isOnline)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            sendRoundWin(RoundEnd.AllDeath);
        }
        roundLose(RoundEnd.AllDeath);
        Debug.Log("rl5");
    }

    public void iAmDeath()
    {
        GetComponent<AudioSource>().PlayOneShot(awpShotDeath);
        am_i_Death = true;
        isStoped = true;
        GameObject blood = createFullScreenCanvas("Blood", new Color32(189, 19, 19, 255));
        StartCoroutine(killedRedScreen(blood, 0, 1.6f));
        cancelKnifeAndBomb();
        resetLook();
    }

    private void cancelKnifeAndBomb()
    {
        while (startedCoroutineBombKnife.Count != 0)
        {
            StopCoroutine(startedCoroutineBombKnife.Dequeue());
            
        }
    }

    private void cancelBomb()
    {
        StopCoroutine(nameof(openBomb));
    }

    private void sendRoundWin(RoundEnd why)
    {
        Debug.Log("PhotonNetwork.IsMasterClient:"+PhotonNetwork.IsMasterClient);
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        Debug.Log("I send now round win ct:"+ctScore+" t:" + tScore);
        online.sendEndRoundData(tScore, ctScore, true, why);
    }

    private void roundWin(RoundEnd why)
    {
        Debug.Log("Round Win");
        gameMoney.addMoney(gameMoney.ROUNDWIN_MONEY);
        giveScore(true);
        StartCoroutine(showDialogNextFrame(isT));
        if (isT) GetComponent<AudioSource>().PlayOneShot(T_Win_AC);
        else GetComponent<AudioSource>().PlayOneShot(why == RoundEnd.Defused ? bombHasBeenDefusedAC : CT_Win_AC);
        endRound();
    }

    private void sendRoundLose(RoundEnd why)
    {
        Debug.Log("PhotonNetwork.IsMasterClient:"+PhotonNetwork.IsMasterClient);
        if (!PhotonNetwork.IsMasterClient)
            return;
        Debug.Log("I send now roundlose ct:"+ctScore+" t:" + tScore);
        online.sendEndRoundData(tScore, ctScore, false, why);
    }
    
    private void roundLose(RoundEnd why)
    {
        Debug.Log("Round Lose");
        resetItems();
        gameMoney.addMoney(gameMoney.ROUNDLOSE_MONEY);
        giveScore(false);
        StartCoroutine(showDialogNextFrame(!isT));
        if (isT) GetComponent<AudioSource>().PlayOneShot(why == RoundEnd.Defused ? bombHasBeenDefusedAC : CT_Win_AC);
        else GetComponent<AudioSource>().PlayOneShot(T_Win_AC);
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

    private IEnumerator showDialogNextFrame(bool forT)
    {
        yield return new WaitForEndOfFrame();
        canvas.GetComponent<ShowDialogs>().showRoundEndDialog(forT);
    }

    private IEnumerator showDeathWeapon(float wait, int weaponCode, int style)
    {
        if (!isOnline)
            yield return null;
        yield return new WaitForSecondsRealtime(wait);
        yield return new WaitForEndOfFrame();
        
        canvas.GetComponent<ShowDialogs>().showDeathWeapon(weaponCode, style, online_data.otherTeam[0]);
        
        if (isOnline)
        {
            if (!PhotonNetwork.IsMasterClient)
                yield break;
            sendRoundWin(RoundEnd.AllDeath);
        }
        roundLose(RoundEnd.AllDeath);
        Debug.Log("rl6");
    }

    void updateScore()
    {
        ctScorLaber.GetComponent<Text>().text = ctScore + "";
        tScorLabel.GetComponent<Text>().text = tScore + "";
    }

    public bool hited(GameObject enemy, int damageGiven, bool isHead, bool isWall)
    {
        online.hited(myWeapon, getMyWeaponStyle(myWeapon), isWall, isHead, enemy.GetComponent<enemy>().id, damageGiven);
        if (enemy.GetComponent<enemy>().giveDamage(damageGiven) <= 0)
        {
            killed(enemy, isHead, isWall);
            return true;
        }

        return false;
    }

    private int getMyWeaponStyle(int weaponCode)
    {
        string allInfo = PlayerPrefs.GetString(MainMenu.playerPrafsWeaponKey[weaponCode], MainMenu.playerPrafsWeaponDef[weaponCode]);
        string[] equStyle = allInfo.Split('-')[1].Split('=');
        return Convert.ToInt32(equStyle[isT ? 0 : 1]);
    }

    public void killed(GameObject enemy, bool isHead, bool isWall)
    {
        Destroy(enemy);
        
        showKillInfo(isT,0, isHead, isWall, yourName, enemy.GetComponent<enemy>().name);
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
        {
            tPPholder[0].GetComponent<Image>().sprite = arraysData.ppList[PlayerPrefs.GetInt("pp", 2)];
            if (isOnline)
                ctPPholder[0].GetComponent<Image>().sprite = arraysData.ppList[online_data.pp_him];
        }
        else
        {
            ctPPholder[0].GetComponent<Image>().sprite = arraysData.ppList[PlayerPrefs.GetInt("pp", 2)];
            if (isOnline)
                tPPholder[0].GetComponent<Image>().sprite = arraysData.ppList[online_data.pp_him];
        }

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
        setLook(isLokingIn-1, true);
    }

    public void lookRight()
    {
        if (isLokingIn >= maxLooks-1)
            return;
        setLook(isLokingIn+1, true);
    }

    void setLook(int lookAt, bool withWalking)
    {
        if (withWalking)
            walk(false, null);
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
        setBGside(0);
        bombAS.PlayOneShot(bombHasBeenPlantedAC);
        gameMoney.addMoney(gameMoney.BOMBPLANT_MONEY);
        if (isT)
        {
            online.bombPlanted(pin, plantSide);
            inventory.clear();
        }

        correctBombPin = pin;
        bombIsPlanted = plantSide;
        whereIsOther = 0;
        created_bomb_icon = Instantiate(bomb_icon_hud);
        timeLabel.SetActive(false);
        CancelInvoke(nameof(countdown));
        CancelInvoke();
    }

    public void bombDefused()
    {
        gameMoney.addMoney(gameMoney.BOMBDEFUSE_MONEY);
        if (!isT)
            online.bombDefused();
        CT_win(RoundEnd.Defused);
    }

    public void bombExplode()
    {
        bombAS.PlayOneShot(bombExplodeAC);
        T_win(RoundEnd.Explode);
    }

    public void otherLeavs()
    {
        if (!isGameFinished)
            gameWin();
    }

    public void bombOpened(int side, bool openedT)
    {
        whereIsOther = side;
        if (openedT)
            bombAS.PlayOneShot(bombPlantStartAC);
        else
            bombAS.PlayOneShot(bombDefusStarAC);
    }

    public void setButtonsActive(bool active)
    {
        flash_button.GetComponent<buttonDelay>().showButton(active);
        knife_button.GetComponent<buttonDelay>().showButton(active);
        plant_bomb_button.GetComponent<buttonDelay>().showButton(active);
        rightButton.GetComponent<buttonDelay>().showButton(active);
        leftButton.GetComponent<buttonDelay>().showButton(active);
        zoomButton.GetComponent<buttonDelay>().showButton(active);
        shotButton.GetComponent<buttonDelay>().showButton(active);
    }

    private int localInvokeChosedSide;
    
    public void mapOK(int side, MapChose mapChose)
    {
        localInvokeChosedSide = side;
        string methodName = null;
        switch (mapChose)
        {
            case MapChose.plant:
                methodName = nameof(openBomb);
                setBGside(localInvokeChosedSide);
                //openBomb(side);
                break;
            case MapChose.defus:
                if (bombIsPlanted == side)
                {
                    methodName = nameof(openBomb);
                    setBGside(localInvokeChosedSide);
                    //openBomb(side);
                }
                else 
                    Debug.Log("Wrong Side Chosed for defuse");
                break;
            case MapChose.knife:
                methodName = nameof(tryToKnife);
                break;
            case MapChose.zeus:
                methodName = nameof(tryToZeus);
                countOfZeus = 0;
                refreshKnife();
                break;
        }
        walk(true, methodName);
    }

    private void tryToKnife()
    {
        if (whereIsOther == localInvokeChosedSide)
            knifeOther();
        else
            playKnifeAir();
    }

    private void tryToZeus()
    {
        if (whereIsOther == localInvokeChosedSide)
            zeusOther();
        else
            playZeusAir();
    }

    void playKnifeAir()
    {
        knifeAS.PlayOneShot(knifeAirAC);
    }

    void playZeusAir()
    {
        knifeAS.PlayOneShot(zeusAirAC);
    }

    public void zeusOther()
    {
        gameMoney.addMoney(gameMoney.KNIFE_MONEY);
        knifeAS.PlayOneShot(getZeusedAC);
        online.zeusOther();
        showKillInfo(isT, deathInfo.ZEUS_CODE, false, false, yourName, enemysNameList[0]);
        if (isOnline)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            sendRoundLose(RoundEnd.Knifed);
        }
        roundWin(RoundEnd.Knifed);
        Debug.Log("rw4");
    }

    public void knifeOther()
    {
        gameMoney.addMoney(gameMoney.KNIFE_MONEY);
        knifeAS.PlayOneShot(getKnifedAC);
        int knifeId = getKnifePlayerPrefs();
        online.knifeOther(knifeId);
        showKillInfo(isT, deathInfo.getKnifesGlobalCode(knifeId), false, false, yourName, enemysNameList[0]);
        if (isOnline)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            sendRoundLose(RoundEnd.Knifed);
        }
        roundWin(RoundEnd.Knifed);
        Debug.Log("rw5");
    }
    
    private void knifed(int knifeId)
    {
        setHealthy(0);
        knifeAS.PlayOneShot(getKnifedAC);
        StartCoroutine(showDeathWeapon(getKnifedAC.length*2/3, 1, knifeId));
        isOtherPlayerSpawned = true;
        showKillInfo(!isT, deathInfo.getKnifesGlobalCode(knifeId), false, false, enemysNameList[0], yourName);
    }

    private void refreshKnife()
    {
        if (countOfZeus > 0)
        {
            changeKnife(0);
            return;
        }

        StoreItemStruct knifeStruct = InventoryMenu.getStruct(weaponList, 1, getKnifePlayerPrefs());
        changeKnife(knifeStruct.iconId);
    }

    private int getKnifePlayerPrefs()
    {
        string knifeCode = PlayerPrefs.GetString(MainMenu.playerPrafsWeaponKey[1], MainMenu.playerPrafsWeaponDef[1]);
        knifeCode = knifeCode.Split('-')[1].Split('=')[isT ? 0 : 1];
        return Convert.ToInt32(knifeCode);
    }
    
    public void getKnifeTry(int knifeId)
    {
        if (created_bomb != null)
            knifed(knifeId);
        else
            knifeAS.PlayOneShot(knifeAirAC);
    }

    public void closeBomb()
    {
        setBGside(0);
        online.closeBomb();
    }

    public void otherCloseBomb()
    {
        whereIsOther = 0;
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(arraysData.menuSounds[soundId]);
    }

    public void getZeusTry()
    {
        if (created_bomb != null)
            zeused();
        else
            knifeAS.PlayOneShot(zeusAirAC);
    }

    public void zeused()
    {
        setHealthy(0);
        knifeAS.PlayOneShot(getZeusedAC);
        StartCoroutine(showDeathWeapon(getZeusedAC.length*2/3, 2, 0));
        isOtherPlayerSpawned = true;
        showKillInfo(!isT, deathInfo.ZEUS_CODE, false, false, enemysNameList[0], yourName);
    }

    public bool checkButtonDelays(int checkId)
    {
        switch (checkId)
        {
            case 0: // flashbang button
                return countOfFlashs > 0;
            case 1: // knife button
                return true;
            case 2: // bombbutton
                return checkBomb(isT? MapChose.plant : MapChose.defus);
        }

        return false;
    }
}
