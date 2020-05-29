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
        knife_button, rightButton, leftButton, shotButton, zoomButton, map_prefab, created_map, flashCountContainer, flashCountText;
    private GameObject created_bomb_icon, created_bomb;
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

    public static String yourName;
    private int time, tScore, ctScore, kills, round, buyTime;
    public int roundTime, enemyCount, teamCount, BUY_TIME;
    public int round_per_half, defLookPintCT = 1, defLookPintT = 1;
    private static readonly int WIN_SCORE = 16;
    public static bool isStoped = true;
    public static bool am_i_Death;
    private bool roundEnd;

    public static int rank, START_ENEMY_COUNT = 5;
    public static int tot_rank = 18;

    public OnlineData online_data;
    public bool isOnline, isOtherPlayerSpawned;
    public string[] myTeam;
    public string[] otherTeam;
    private static int otherRank;
    private Sprite otherPP;
    public Sprite a_side_sprite, b_side_sprite;
    public GameObject bgSideImg;
    public Sprite bombPlantButtonSprite, bombDefuseButtonSprite;
    public Sprite[] knifeButtonSprite;
    public int chosedKnifeId;

    public TeamFriend[] friends;
    public Online strategy;
    public List<String> enemysNameList;
    public int bombIsPlanted, whereIsOther;

    public string correctBombPin;
    public bool hasCtKit;

    public int countOfFlashs, countOfZeus;

    public Color32 lastSecColor, normalSecColor;

    public AudioSource timeAS, bombAS, flashAS, knifeAS, stepAS;
    public AudioClip bombHasBeenDefusedAC, bombHasBeenPlantedAC, bombPlantStartAC, bombDefusStarAC, bombExplodeAC, lastSecondsAC, goChangeSideStepAC, goBombStepAC;
    public AudioClip throughFlashAC, farFlashAC, explodeFlashAC, leftGameAC;
    public AudioClip T_Win_AC, CT_Win_AC;
    public AudioClip[] getKnifedAC, knifeAirAC, otherShotsAWP;

    void takeFlash()
    {
        if (countOfFlashs <= 0)
            return;
        countOfFlashs--;
        refreshFlashCount();
    }

    void refreshFlashCount()
    {
        if (countOfFlashs > 0)
        {
            flashCountContainer.SetActive(true);
            flashCountText.GetComponent<Text>().text = countOfFlashs.ToString();
        }
        else
        {
            flashCountContainer.SetActive(false);
        }
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
        chosedKnifeId = id;
        knife_button.GetComponent<buttonDelay>().changeSprite(knifeButtonSprite[id]);
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
        plant_bomb_button.GetComponent<buttonDelay>().changeSprite(isT? bombPlantButtonSprite : bombDefuseButtonSprite);
        gameMoney.setMoney(gameMoney.FIRST_MONEY);
        friends = new TeamFriend[START_ENEMY_COUNT];
        myTeam[0] = yourName;
        for (int i = 0; i < friends.Length; i++)
            friends[i] = new TeamFriend(i, myTeam[i], isT? tPPholder[i]: ctPPholder[i]);
        //knife_button.SetActive(!isT);
        refreshColorTags();
    }

    public void getOnlineShot()
    {
        GetComponent<AudioSource>().PlayOneShot(otherShotsAWP[Random.Range(0,otherShotsAWP.Length)]);
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
        
        timeLabel.GetComponent<Text>().text = min + ":" + ((sec < 10)?"0":"") + sec;
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
        ButtonClickSound(4);
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
        if(isStoped)
            return;
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
        if (created_map != null)
            return;
        if (why == MapChose.plant && bombHasBeenPlant())
            return;
        if (why == MapChose.defus && !bombHasBeenPlant())
            return;
        created_map = Instantiate(map_prefab);
        created_map.GetComponent<Map>().mapChose = why;
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
        
        StartCoroutine(flashFade(panel, 0.0f, i.color, 2.0f, 1.5f, null));
    }

    public void walk(bool longWalk, string methodeName)
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
        
        StartCoroutine(flashFade(walk, 0.0f, i.color, alphaTime, walkSound.length-alphaTime, longWalk? methodeName:null));
    }
    
    IEnumerator flashFade(GameObject panel, float aValue, Color32 c, float aTime, float delayTime, string methodName)
    {
        yield return new WaitForSeconds(delayTime);
        float alpha = panel.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(c.r, c.g, c.b, Mathf.Lerp(alpha,aValue,t));
            panel.GetComponent<Image>().color = newColor;
            yield return null;
        }
        Destroy(panel);
        if (methodName != null)
            Invoke(methodName,0);
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
        setLook(1);
        isOtherPlayerSpawned = false;
        enemyCount = START_ENEMY_COUNT;
        teamCount = START_ENEMY_COUNT;
    }

    private void beforeNewRound()
    {
        setButtonsActive(false);
        setBGside(0);
        cleanUpForRound();
        isStoped = true;
        round++;
        updateScore();
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
        if (isT) GetComponent<AudioSource>().PlayOneShot(T_Win_AC);
        else GetComponent<AudioSource>().PlayOneShot(CT_Win_AC);
        endRound();
    }
    
    private void roundLose()
    {
        resetItems();
        gameMoney.addMoney(gameMoney.ROUNDLOSE_MONEY);
        giveScore(false);
        StartCoroutine(showDialgNextFrame(!isT));
        if (isT) GetComponent<AudioSource>().PlayOneShot(CT_Win_AC);
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
        if (!isStoped)
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
        bombAS.PlayOneShot(bombHasBeenDefusedAC);
        CT_win();
    }

    public void bombExplode()
    {
        bombAS.PlayOneShot(bombExplodeAC);
        T_win();
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
        flash_button.SetActive(active);
        knife_button.SetActive(active);
        plant_bomb_button.SetActive(active);
        rightButton.SetActive(active);
        leftButton.SetActive(active);
        zoomButton.SetActive(active);
        shotButton.SetActive(active);
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
                if (whereIsOther == side)
                {
                    methodName = nameof(knifeOther);
                }
                else
                {
                    methodName = nameof(playKnifeAir);
                }
                break;
        }
        
        if (chosedKnifeId == 0)
        {  // zeus
            countOfZeus = 0;
            refreshKnife();
        }
        
        walk(true, methodName);
    }

    void playKnifeAir()
    {
        knifeAS.PlayOneShot(knifeAirAC[chosedKnifeId]);
    }

    private void knifeOther()
    {
        knifeAS.PlayOneShot(getKnifedAC[chosedKnifeId]);
        gameMoney.addMoney(gameMoney.KNIFE_MONEY);
        showKillInfo(!isT, chosedKnifeId+3, false, false, yourName, enemysNameList[0]);

        roundWin();
        online.knifeOther(chosedKnifeId);
    }

    private void refreshKnife()
    {
        changeKnife(countOfZeus > 0 ? 0:PlayerPrefs.GetInt("knife", isT?1:2));
    }

    public void getKnifeTry(int knifeId)
    {
        if (created_bomb != null)
            knifed(knifeId);
        else
            knifeAS.PlayOneShot(knifeAirAC[knifeId]);
    }

    private void knifed(int knifeId)
    {
        knifeAS.PlayOneShot(getKnifedAC[knifeId]);
        roundLose();
        isOtherPlayerSpawned = true;
        showKillInfo(!isT, knifeId + 3, false, false, enemysNameList[0], yourName);
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
        GameObject soundManeger = GameObject.Find("Sound");
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<MenuSound>().menuSounds[soundId]);
    }
}
