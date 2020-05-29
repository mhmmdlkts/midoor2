using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamEditor : MonoBehaviour
{
    public GameObject name1, name2, name3, name4;
    public GameObject InputField1, InputField2, InputField3, InputField4;
    TouchScreenKeyboard keyboard;

    private int finishTypingWith;
    // Start is called before the first frame update
    void Start()
    {
        setStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            switch (finishTypingWith)
            {
                case 1: finishTyping1();
                    break;
                case 2: finishTyping2();
                    break;
                case 3: finishTyping3();
                    break;
                case 4: finishTyping4();
                    break;
            }
        }
    }

    public void go_back_button()
    {
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    public void setName1()
    {
        Debug.Log("1");
        finishTypingWith = 1;
        InputField1.SetActive(true);
        name1.SetActive(false);
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        EventSystem.current.SetSelectedGameObject(InputField1.gameObject, null);
    }

    private void finishTyping1()
    {
        PlayerPrefs.SetString("game_firend_1",InputField1.GetComponent<InputField>().text);
        setStatus();
    }

    public void setName2()
    {
        finishTypingWith = 2;
        InputField2.SetActive(true);
        name2.SetActive(false);
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        EventSystem.current.SetSelectedGameObject(InputField2.gameObject, null);
    }

    private void finishTyping2()
    {
        PlayerPrefs.SetString("game_firend_2",InputField2.GetComponent<InputField>().text);
        setStatus();
    }

    public void setName3()
    {
        finishTypingWith = 3;
        InputField3.SetActive(true);
        name3.SetActive(false);
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        EventSystem.current.SetSelectedGameObject(InputField3.gameObject, null);
    }

    private void finishTyping3()
    {
        PlayerPrefs.SetString("game_firend_3",InputField3.GetComponent<InputField>().text);
        setStatus();
    }

    public void setName4()
    {
        finishTypingWith = 4;
        InputField4.SetActive(true);
        name4.SetActive(false);
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        EventSystem.current.SetSelectedGameObject(InputField4.gameObject, null);
    }

    private void finishTyping4()
    {
        PlayerPrefs.SetString("game_firend_4",InputField4.GetComponent<InputField>().text);
        setStatus();
    }  
        

    private void setStatus()
    {
        name1.SetActive(true); name2.SetActive(true); name3.SetActive(true); name4.SetActive(true); 
        InputField1.SetActive(false); InputField2.SetActive(false); InputField3.SetActive(false); InputField4.SetActive(false); 
        name1.GetComponent<Text>().text = PlayerPrefs.GetString("game_firend_1","Set Your Friends Name");
        name2.GetComponent<Text>().text = PlayerPrefs.GetString("game_firend_2","Set Your Friends Name");
        name3.GetComponent<Text>().text = PlayerPrefs.GetString("game_firend_3","Set Your Friends Name");
        name4.GetComponent<Text>().text = PlayerPrefs.GetString("game_firend_4","Set Your Friends Name");
        finishTypingWith = 0;
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find("Sound");
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<MenuSound>().menuSounds[soundId]);
    }
}
