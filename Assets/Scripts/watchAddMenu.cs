using System.Collections;
using System.Collections.Generic;
using CompleteProject;
using UnityEngine;

public class watchAddMenu : MonoBehaviour
{
    
    public void onWatchAdClick()
    {
        GameObject.Find("Main Camera").GetComponent<OnlineMenu>().watchRewardAd();
        Destroy(gameObject);
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
