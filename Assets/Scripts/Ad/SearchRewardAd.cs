using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class SearchRewardAd : MonoBehaviour
{
    private RewardedAd rewardedAd;
    OnlineMenu onlineMenu;
    
    public void showRewardAd(OnlineMenu online)
    {
        onlineMenu = online;
        if (MainMenuAd.isRemovedAds())
            return;
        rewardedAd = new RewardedAd(AdUnitIds.getAdUnitId(Ads.Search_Reward));
        
        AdRequest request = new AdRequest.Builder().Build();
        
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        
        rewardedAd.LoadAd(request);
    }
    
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("ADTEST + YÜKLENDI");
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        rewardedAd.Show();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        int amount = Convert.ToInt32(args.Amount);
        Debug.Log("ADTEST + GOLDD S: + " + amount);
        onlineMenu.rewardPlays = amount;
        Debug.Log("ADTEST + GOLDD F: + " + amount);
    }
}
