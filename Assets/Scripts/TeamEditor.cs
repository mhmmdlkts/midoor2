using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamEditor : MonoBehaviour, IDeselectHandler
{
    public GameObject[] names;
    public GameObject[] InputFields;

    void Start()
    {
        setStatus();
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
    }

    public void go_back_button()
    {
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    public static bool checkIsInputOk(string input)
    {
        if (input == null || input.Equals(""))
            return false;
        return true;
    }

    public void setName(int i)
    {
        InputFields[i].SetActive(true);
        InputFields[i].GetComponent<InputField>().text = names[i].GetComponent<Text>().text;
        names[i].SetActive(false);
        EventSystem.current.SetSelectedGameObject(InputFields[i].gameObject, null);
    }

    public void finishTyping(int i)
    {
        string input = InputFields[i].GetComponent<InputField>().text;
        if (!checkIsInputOk(input))
            return;
        PlayerPrefs.SetString("game_firend_"+i,input);
        setStatus();
    }


    private void setStatus()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].SetActive(true);
            InputFields[i].SetActive(false);
            names[i].GetComponent<Text>().text = PlayerPrefs.GetString("game_firend_" + i,"Set Your Friends Name");
        }
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<ArraysData>().menuSounds[soundId]);
    }
}
