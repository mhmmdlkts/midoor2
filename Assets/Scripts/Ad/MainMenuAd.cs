using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class MainMenuAd : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    // Start is called before the first frame update
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestInterstitial();
    }

    private void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(AdUnitIds.getAdUnitId(Ads.Menu_Banner), AdSize.Banner, AdPosition.Bottom);
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
    
    private void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(AdUnitIds.getAdUnitId(Ads.Menu_Popup));
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
        interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        showPopup();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
    }

    public void showPopup()
    {
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        }
    }

}
