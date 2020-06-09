using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class match_ready_dialog : MonoBehaviour
{
    public Text time_text, accept_text, title_text;
    public Button accept_button;
    public int wait_sec;
    private GameObject parent;
    private GameObject canvas;
    public AudioClip acceptedBeepAC, notAcceptedBeepAC, matchFoundAC;

    public bool accepted;
    // Start is called before the first frame update
    void Start()
    {
        accept_text.GetComponent<Text>().text = LanguageSystem.GET_SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPT();
        title_text.GetComponent<Text>().text = LanguageSystem.GET_SEARCH_GAME_MENU_TITLE();
        GetComponent<AudioSource>().PlayOneShot(matchFoundAC);
        parent = GameObject.Find("Main Camera");
        canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent (canvas.transform, false);
        accepted = false;
        decSec();
    }

    private void setTime()
    {
        time_text.GetComponent<Text>().text = "0:" + (wait_sec < 10 ? "0" : "" )+ wait_sec;
    }

    private void decSec()
    {
        if (accepted)
            GetComponent<AudioSource>().PlayOneShot(acceptedBeepAC);
        else
            GetComponent<AudioSource>().PlayOneShot(notAcceptedBeepAC);
        setTime();
        if (wait_sec <= 0)
            notAccepted();
        else
            Invoke(nameof(decSec),1f);
        wait_sec--;
    }

    public void reject()
    {
        parent.GetComponent<Launcher>().notAccepted(false);
        close();
    }

    public void notAccepted()
    {
        parent.GetComponent<Launcher>().notAccepted(accepted);
        close();
    }

    private void close()
    {
        parent.GetComponent<Launcher>().resetAccepts();
        Destroy(gameObject);
    }

    public void accept()
    {
        parent.GetComponent<Launcher>().acceptGame();
        accept_button.GetComponent<Button>().interactable = false;
        accept_text.GetComponent<Text>().text = LanguageSystem.GET_SEARCH_GAME_MENU_BUTTON_LABEL_ACCEPTED();
        accept_text.GetComponent<Text>().color = new Color32(37,227,0,255);
        accepted = true;
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<ArraysData>().menuSounds[soundId]);
    }
}
