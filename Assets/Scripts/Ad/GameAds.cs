using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GameAds : MonoBehaviour
{
    private BannerView bannerView;
    // Start is called before the first frame update
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
    }

    private void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(AdUnitIds.getAdUnitId(Ads.Game_Banner), AdSize.Banner, AdPosition.BottomLeft);
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
