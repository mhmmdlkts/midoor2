using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class MainMenuAd : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private static string removedAdsPlayerprefsString = "isAd";
    // Start is called before the first frame update
    public void Start()
    {
        if (isRemovedAds())
            return;
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestInterstitial();
    }

    private void RequestBanner()
    {
        if (isRemovedAds())
            return;
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(AdUnitIds.getAdUnitId(Ads.Menu_Banner), AdSize.Banner, AdPosition.Bottom);
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
    
    private void RequestInterstitial()
    {
        if (isRemovedAds())
            return;
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(AdUnitIds.getAdUnitId(Ads.Menu_Popup));
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void showPopup()
    {
        if (isRemovedAds())
            return;
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        }
    }

    public static bool isRemovedAds()
    {
        return PlayerPrefs.HasKey(removedAdsPlayerprefsString);
    }

    public static void removeAdsPermananty()
    {
        PlayerPrefs.SetInt(removedAdsPlayerprefsString, 1); // The value is doesn't important
    }

    public void destroyAdds()
    {
        if (bannerView != null)
            bannerView.Destroy();
        if (interstitial != null)
            interstitial.Destroy();
        Destroy(gameObject);
    }
}
