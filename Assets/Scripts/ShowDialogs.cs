using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogs : MonoBehaviour
{
    public GameObject gameEndPanel, roundEndPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showRoundEndDialog(bool tWin)
    {
        String methodName;
        if (tWin)
        {
            methodName = "showNowRoundWinDialog";
        }
        else
        {
            methodName = "showNowRoundLoseDialog";
        }
        Invoke(methodName, 0);
    }

    public void showGameEndDialog(bool tWin, int kills)
    {
        PlayerPrefs.SetInt("new_kills", kills);
        PlayerPrefs.SetInt("new_win", tWin?1:0);
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
