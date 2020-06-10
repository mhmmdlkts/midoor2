using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject pp, name, money, wins, rank, inputName;
    public Sprite[] rankList;
    public ArraysData arraysData;
    private bool isPPsLoaded;
    public GameObject Pp_choser;
    private static string removedPlaysPlayerprefsString = "isPlays";
    void Start()
    {
        
        if (GameObject.Find(MainMenu.ArraysDataName) == null)
        {
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
            return;
        }

        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        setStatus();
    }

    public void setStatus()
    {
        wins.GetComponent<Text>().text = PlayerPrefs.GetInt("total_wins",0) + "";
        money.GetComponent<Text>().text = LanguageSystem.GET_CURRENCY() + PlayerPrefs.GetInt("money",0);
        rank.GetComponent<Image>().sprite = rankList[PlayerPrefs.GetInt("rank", 4)];
        name.GetComponent<Text>().text = PlayerPrefs.GetString("name", LanguageSystem.GET_PLAYER_STATUS_NO_NAME_ME());
        pp.GetComponent<Image>().sprite = arraysData.ppList[PlayerPrefs.GetInt("pp",2)];
    }
    
    public void button_set_name()
    {
        inputName.SetActive(true);
        if (PlayerPrefs.HasKey("name"))
            inputName.GetComponent<InputField>().text = name.GetComponent<Text>().text;
        name.SetActive(false);
        EventSystem.current.SetSelectedGameObject(inputName.gameObject, null);
    }
    
    public void button_change_pp()
    {
        if (!isPPsLoaded)
        {
            isPPsLoaded = true;
            Pp_choser.SetActive(true);
            Pp_choser.GetComponent<PP_Beavior>().showPPs();
            return;
        }
        Pp_choser.SetActive(!Pp_choser.activeSelf);
        if (Pp_choser.activeSelf)
            Pp_choser.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }

    public void finishTyping()
    {
        string input = inputName.GetComponent<InputField>().text;
        if (!TeamEditor.checkIsInputOk(input))
            return;
        PlayerPrefs.SetString("name", input);
        inputName.SetActive(false);
        name.SetActive(true);
        setStatus();
    }

    public void setPP(int ppId)
    {
        PlayerPrefs.SetInt("pp",ppId);
        setStatus();
    }

    public static void addMoney(int extra)
    {
        int money = PlayerPrefs.GetInt("money", 0) + extra;
        PlayerPrefs.SetInt("money",money);
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("UI_coin");
        foreach (var coinObject in coinObjects)
        {
            Text txt = coinObject.GetComponent<Text>();
            if (txt != null)
                txt.text = LanguageSystem.GET_CURRENCY() + money;
        }
    }

    public static void addPlays(int extra)
    {
        int plays = PlayerPrefs.GetInt("plays", 3) + extra;
        PlayerPrefs.SetInt("plays",plays);
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("UI_plays");
        foreach (var coinObject in coinObjects)
        {
            Text txt = coinObject.GetComponent<Text>();
            if (txt != null)
                txt.text = "" + plays;
        }
    }

    public static bool isPlaysRemoved()
    {
        return PlayerPrefs.HasKey(removedPlaysPlayerprefsString);
    }

    public static void removePlaysPermanently()
    {
        PlayerPrefs.SetInt("plays", Int32.MaxValue);
        PlayerPrefs.SetInt(removedPlaysPlayerprefsString, 1); // The value is doesn't important
    }
}
