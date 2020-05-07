using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showWin : MonoBehaviour
{
    public float stayTime;
    private GameObject text, panel;
    private Color32 CT_PANEL_COLOR, T_PANEL_COLOR, CT_TEXT_COLOR, T_TEXT_COLOR;
    private String T_WIN_MESSAGE, CT_WIN_MESSAGE;
    
    // Start is called before the first frame update
    
    void Start()
    {
        CT_TEXT_COLOR = new Color32(167,180,207, 255);
        T_TEXT_COLOR = new Color32(177,169,122, 255);
        CT_PANEL_COLOR = new Color32(124,146,172,159);
        T_PANEL_COLOR = new Color32(147,122,83,159);
        T_WIN_MESSAGE = "Terrorists Win";
        CT_WIN_MESSAGE = "Anti-Terrorists Win";
        panel = GameObject.Find("win_lose_panel");
        text = GameObject.Find("win_lose_text");
        setClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float show(bool tWin)
    {
        if(tWin)
            setT();
        else 
            setCT();
        StartCoroutine("clear");
        return stayTime;
    }

    public void setCT()
    {
        panel.SetActive(true);
        text.GetComponent<Text>().color = CT_TEXT_COLOR;
        panel.GetComponent<Image>().color = CT_PANEL_COLOR;
        text.GetComponent<Text>().text = CT_WIN_MESSAGE;
        panel.SetActive(true);
    }

    public void setT()
    {
        panel.SetActive(true);
        text.GetComponent<Text>().color = T_TEXT_COLOR;
        panel.GetComponent<Image>().color = T_PANEL_COLOR;
        text.GetComponent<Text>().text = T_WIN_MESSAGE;
        panel.SetActive(true);
    }

    IEnumerator clear()
    {
        yield return new WaitForSecondsRealtime(stayTime);
        setClear();
    }

    void setClear()
    {
        if(text == null || panel == null)
            return;
        text.GetComponent<Text>().color = new Color(0,0,0,0);
        panel.GetComponent<Image>().color = new Color(0,0,0,0);
        text.GetComponent<Text>().text = "";
        panel.SetActive(false);
    }
}
