using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class match_ready_dialog : MonoBehaviour
{
    public Text time_text, accept_text;
    public Button accept_button;
    public int wait_sec;
    private GameObject parent;
    private GameObject canvas;
    public AudioClip acceptedBeepAC, notAcceptedBeepAC, matchFoundAC;

    public bool accepted;
    // Start is called before the first frame update
    void Start()
    {
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
            reject();
        else
            Invoke("decSec",1f);
        wait_sec--;
    }

    public void reject()
    {
        parent.GetComponent<Launcher>().LeaveRoom();
        Destroy(gameObject);
    }

    public void accept()
    {
        parent.GetComponent<Launcher>().acceptGame();
        accept_button.GetComponent<Button>().interactable = false;
        accept_text.GetComponent<Text>().text = "ACCEPTED";
        accept_text.GetComponent<Text>().color = new Color32(37,227,0,255);
        accepted = true;
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find("Sound");
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<MenuSound>().menuSounds[soundId]);
    }
}
