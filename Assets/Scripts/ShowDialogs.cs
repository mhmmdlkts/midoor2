using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogs : MonoBehaviour
{
    public GameObject gameEndPanel, roundEndPanel, deathWeaponPrefab;
    public float showAfter = 0;

    public void showRoundEndDialog(bool tWin)
    {
        if (tWin)
        {
            Invoke(nameof(showNowRoundWinDialog),showAfter);
        }
        else
        {
            Invoke(nameof(showNowRoundLoseDialog),showAfter);
        }
    }

    public void showGameEndDialog(int isWinn, int kills)
    {
        PlayerPrefs.SetInt("new_kills", kills);
        PlayerPrefs.SetInt("new_win", isWinn);
        if (isWinn == -2)
            return;
        Invoke(nameof(showNowGameEndDialog), 2f);
    }

    public void showNowGameEndDialog()
    {
        GameObject dialog = Instantiate(gameEndPanel, transform, false);
    }
    
    public void showNowRoundWinDialog()
    {
        GameObject dialog = Instantiate(roundEndPanel, transform, false);
        dialog.GetComponent<EndRoundShow>().show(true);
    }
    
    public void showNowRoundLoseDialog()
    {
        GameObject dialog = Instantiate(roundEndPanel, transform, false);
        dialog.GetComponent<EndRoundShow>().show(false);
    }

    public void showDeathWeapon(int weaponCode, int style, string killersName)
    {
        GameObject createdDeathWeapon = Instantiate(deathWeaponPrefab, transform, false);
        createdDeathWeapon.GetComponent<youKilledWithWeaponImg>().show(weaponCode, style, killersName);
    }
}
