﻿using System.Collections;
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
        money.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("money",0);
        rank.GetComponent<Image>().sprite = rankList[PlayerPrefs.GetInt("rank", 4)];
        name.GetComponent<Text>().text = PlayerPrefs.GetString("name", "Set Your Name");
        pp.GetComponent<Image>().sprite = arraysData.ppList[PlayerPrefs.GetInt("pp",2)];
    }
    
    public void button_set_name()
    {
        inputName.SetActive(true);
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
}
