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
    
    
    void Start()
    {
        CT_TEXT_COLOR = new Color32(167,180,207, 255);
        T_TEXT_COLOR = new Color32(177,169,122, 255);
        CT_PANEL_COLOR = new Color32(124,146,172,159);
        T_PANEL_COLOR = new Color32(147,122,83,159);
        Invoke(nameof(destroyMe), stayTime);
    }

    public void show(bool tWin)
    {
        if (tWin)
            StartCoroutine(nameof(setT));
        else 
            StartCoroutine(nameof(setCT));
    }

    IEnumerator setCT()
    {
        yield return new WaitForEndOfFrame();
        text.GetComponent<Text>().color = CT_TEXT_COLOR;
        panel.GetComponent<Image>().color = CT_PANEL_COLOR;
        text.GetComponent<Text>().text = LanguageSystem.GET_END_ROUND_PANEL_LABEL_CT_WIN();
    }

    IEnumerator setT()
    {
        yield return new WaitForEndOfFrame();
        text.GetComponent<Text>().color = T_TEXT_COLOR;
        panel.GetComponent<Image>().color = T_PANEL_COLOR;
        text.GetComponent<Text>().text = LanguageSystem.GET_END_ROUND_PANEL_LABEL_T_WIN();
    }

    void destroyMe()
    {
        Destroy(gameObject);
    }
}
