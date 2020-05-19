using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject pp, name, money, wins, rank, inputName;
    public Sprite[] rankList;
    // Start is called before the first frame update
    void Start()
    {
        setStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            finishTyping();
        }
    }

    private void setStatus()
    {
        wins.GetComponent<Text>().text = PlayerPrefs.GetInt("total_wins",0) + "";
        money.GetComponent<Text>().text = "$" + PlayerPrefs.GetInt("money",0);
        rank.GetComponent<Image>().sprite = rankList[PlayerPrefs.GetInt("rank", 4)];
        name.GetComponent<Text>().text = PlayerPrefs.GetString("name", "Mali");
    }
    
    public void button_ranked()
    {
        SceneManager.LoadScene("Assets/Scenes/Dust2_T_MID.unity", LoadSceneMode.Single);
    }
    
    public void button_ranked_online()
    {
        SceneManager.LoadScene("Assets/Scenes/Online_Ranked.unity", LoadSceneMode.Single);
    }
    
    public void button_set_name()
    {
        inputName.SetActive(true);
        name.SetActive(false);
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
