using System.Collections;
using System.Collections.Generic;
using CompleteProject;
using UnityEngine;
using UnityEngine.UI;

public class watchAddMenu : MonoBehaviour
{
    public Text titleLabel, buyUnlimitedPlaysLabel, buy20PlaysLabel, watchAdLabel, closeLabel;
    public void onWatchAdClick()
    {
        GameObject.Find("Main Camera").GetComponent<OnlineMenu>().watchRewardAd();
        setLabels();
        Destroy(gameObject);
    }

    private void setLabels()
    {
        titleLabel.text = LanguageSystem.GET_MORE_PLAYS_MENU_TITLE();
        buyUnlimitedPlaysLabel.text = LanguageSystem.GET_MORE_PLAYS_MENU_BUTTON_LABEL_BUY_NO_PLAYS();
        buy20PlaysLabel.text = LanguageSystem.GET_MORE_PLAYS_MENU_BUTTON_LABEL_BUY_20_PLAYS();
        watchAdLabel.text = LanguageSystem.GET_MORE_PLAYS_MENU_BUTTON_LABEL_WATCH_AD();
        closeLabel.text = LanguageSystem.GET_MORE_PLAYS_MENU_BUTTON_LABEL_CLOSE();
    }

    public void onCloseClick()
    {
        Destroy(gameObject);
    }

    public void buy20Click()
    {
        GetComponent<Purchaser>().Buy20Plays();
    }

    public void buynoPlaysClick()
    {
        GetComponent<Purchaser>().BuyNoPlays();
    }
}
