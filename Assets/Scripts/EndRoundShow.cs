using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoundShow : MonoBehaviour
{
    public static float stayTime = 2f;
    public GameObject text, panel;
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
        Invoke("destroyMe", stayTime);
    }

    public void show(bool tWin)
    {
        if (tWin)
            StartCoroutine("setT");
        else 
            StartCoroutine("setCT");
    }

    IEnumerator setCT()
    {
        yield return new WaitForEndOfFrame();
        text.GetComponent<Text>().color = CT_TEXT_COLOR;
        panel.GetComponent<Image>().color = CT_PANEL_COLOR;
        text.GetComponent<Text>().text = CT_WIN_MESSAGE;
    }

    IEnumerator setT()
    {
        yield return new WaitForEndOfFrame();
        text.GetComponent<Text>().color = T_TEXT_COLOR;
        panel.GetComponent<Image>().color = T_PANEL_COLOR;
        text.GetComponent<Text>().text = T_WIN_MESSAGE;
    }

    void destroyMe()
    {
        Destroy(gameObject);
    }
}
