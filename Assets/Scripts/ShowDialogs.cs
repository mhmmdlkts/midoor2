using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogs : MonoBehaviour
{
    public GameObject gameEndPanel, roundEndPanel;
    public float showAfter = 0;

    public void showRoundEndDialog(bool tWin)
    {
        if (tWin)
        {
            Invoke("showNowRoundWinDialog",showAfter);
        }
        else
        {
            Invoke("showNowRoundLoseDialog",showAfter);
        }
    }

    public void showGameEndDialog(int isWinnn, int kills)
    {
        PlayerPrefs.SetInt("new_kills", kills);
        PlayerPrefs.SetInt("new_win", isWinnn);
        Invoke("showNowGameEndDialog", 1);
    }

    public void showNowGameEndDialog()
    {
        GameObject dialog = Instantiate(gameEndPanel, gameEndPanel.transform.position, gameEndPanel.transform.rotation);
        dialog.transform.SetParent (gameObject.transform, false);
    }
    
    public void showNowRoundWinDialog()
    {
        GameObject dialog = Instantiate(roundEndPanel, roundEndPanel.transform.position, roundEndPanel.transform.rotation);
        dialog.GetComponent<EndRoundShow>().show(true);
        dialog.transform.SetParent (gameObject.transform, false);
    }
    
    public void showNowRoundLoseDialog()
    {
        GameObject dialog = Instantiate(roundEndPanel, roundEndPanel.transform.position, roundEndPanel.transform.rotation);
        dialog.GetComponent<EndRoundShow>().show(false);
        dialog.transform.SetParent (gameObject.transform, false);
    }
}
