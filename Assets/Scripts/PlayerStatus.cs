using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject pp, name, money, wins, rank, inputName;
    public Sprite[] rankList;
    void Start()
    {
        setStatus();
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
        inputName.GetComponent<InputField>().text = name.GetComponent<Text>().text;
        name.SetActive(false);
        EventSystem.current.SetSelectedGameObject(inputName.gameObject, null);
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
}
