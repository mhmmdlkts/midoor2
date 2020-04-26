using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogs : MonoBehaviour
{
    public GameObject gameEndPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showGameEndDialog(bool tWin, int kills)
    {
        PlayerPrefs.SetInt("new_kills", kills);
        PlayerPrefs.SetInt("new_win", tWin?1:0);
        Invoke("showNowEndDialog", 1);
    }

    public void showNowEndDialog()
    {
        GameObject dialog = Instantiate(gameEndPanel, gameEndPanel.transform.position, gameEndPanel.transform.rotation);
        dialog.transform.SetParent (gameObject.transform, false);
    }
}
