using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject pp, name, money, wins, rank, inputName;
    public Sprite[] rankList;
    TouchScreenKeyboard keyboard;
    void Start()
    {
        setStatus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            finishTyping();
        }
    }

    private void setStatus()
    {
        wins.GetComponent<Text>().text = PlayerPrefs.GetInt("total_wins",0) + "";
        money.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("money",0);
        rank.GetComponent<Image>().sprite = rankList[PlayerPrefs.GetInt("rank", 4)];
        name.GetComponent<Text>().text = PlayerPrefs.GetString("name", "Set Your Name");
    }
    
    public void button_set_name()
    {
        inputName.SetActive(true);
        name.SetActive(false);
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
        EventSystem.current.SetSelectedGameObject(inputName.gameObject, null);
    }

    private void finishTyping()
    {
        PlayerPrefs.SetString("name",inputName.GetComponent<InputField>().text);
        inputName.SetActive(false);
        name.SetActive(true);
        setStatus();
    }
}
