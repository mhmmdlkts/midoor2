using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineMenu : MonoBehaviour
{
    public int money, rank, wins, ppId, plays;
    public string name;
    public GameObject pp_me, name_me, money_me, wins_me, rank_me, plays_me;
    public GameObject pp_him, name_him, wins_him, rank_him;
    public Start_Cancel_Search_Button scs;
    public GameObject watchAddMenuPrefab;
    public OnlineData data;
    public ArraysData arraysData;
    public Sprite[] rankList;
    public string[] myTeam;
    
    void Start()
    {
        
        if (GameObject.Find(MainMenu.ArraysDataName) == null)
        {
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
            return;
        }

        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        
        setStatus();
        initializeMyTeam();
    }
    
    private void initializeMyTeam()
    {
        myTeam = new string[GameScript.START_ENEMY_COUNT];
        myTeam[0] = name;
        for (int i = 0; i < myTeam.Length-1; i++)
        {
            myTeam[i+1] = PlayerPrefs.GetString("game_firend_" + i, "Team_" + i + "_name");
        }
    }

    private void setStatus()
    {
        wins = PlayerPrefs.GetInt("total_wins",0);
        money = PlayerPrefs.GetInt("money",0);
        rank = PlayerPrefs.GetInt("rank", 4);
        name = PlayerPrefs.GetString("name", "Name");
        ppId = PlayerPrefs.GetInt("pp", 2);
        
        wins_me.GetComponent<Text>().text = "" + wins;
        money_me.GetComponent<Text>().text = "$" + money;
        rank_me.GetComponent<Image>().sprite = rankList[rank];
        name_me.GetComponent<Text>().text = name;
        pp_me.GetComponent<Image>().sprite = arraysData.ppList[ppId];
        refreshPlays();
    }

    private void refreshPlays()
    {
        if (PlayerStatus.isPlaysRemoved())
        {
            plays_me.GetComponent<Text>().text = "∞";
            plays = Int32.MaxValue;
            return;
        }

        plays = PlayerPrefs.GetInt("plays", 3);
        plays_me.GetComponent<Text>().text = "" + plays;
    }

    public void rewardAdListener()
    {
        if (PlayerStatus.isPlaysRemoved()) 
            return;
        Instantiate(watchAddMenuPrefab, GameObject.Find("Canvas").transform, false);
    }

    public void setEnemyStatus(String name, int rank)
    {
        name_him.GetComponent<Text>().text = name;
        rank_him.GetComponent<Image>().sprite = rankList[rank];
    }
    
    public void button_leave_room()
    {
        gameObject.GetComponent<Launcher>().LeaveRoom();
    }
    
    public void button_start_cancel_listener()
    {
        if (scs.isGreen)
        {
            if (plays <= 0 && false)
            {
                rewardAdListener();
                return;
            }
            scs.setToRed();
            gameObject.GetComponent<Launcher>().Connect();
        }
        else
        {
            scs.setToGreen();
            gameObject.GetComponent<Launcher>().Disconnect();
        }
            
    }

    public void setHisRank(int rank)
    {
        data.rank_him = rank;
        rank_him.GetComponent<Image>().sprite = rankList[rank];
    }

    public void setHisWins(int wins)
    {
        data.wins_him = wins;
        wins_him.GetComponent<Text>().text = wins + "";
    }

    public void setHisPP(int ppId)
    {
        data.pp_him = ppId;
        pp_him.GetComponent<Image>().sprite = arraysData.ppList[ppId];
    }

    public void setMyTeam(bool isT)
    {
        data.setTeam(isT);
    }

    public void setHisTeam(string[] names)
    {
        data.otherTeam = names;
        name_him.GetComponent<Text>().text = names[0];
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<ArraysData>().menuSounds[soundId]);
    }

    public void watchRewardAd()
    {
        GetComponent<SearchRewardAd>().showRewardAd();
    }

    public void addPlays(int i)
    {
        EndGameShow.addPlays(i);
        refreshPlays();
    }
}
