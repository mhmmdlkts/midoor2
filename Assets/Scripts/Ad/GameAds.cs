using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class GameAds : MonoBehaviour
{
    private BannerView bannerView;
    public void Start()
    {
        if (MainMenuAd.isRemovedAds())
            return;
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
    }

    private void RequestBanner()
    {
        if (MainMenuAd.isRemovedAds())
            return;
        bannerView = new BannerView(AdUnitIds.getAdUnitId(Ads.Game_Banner), AdSize.Banner, AdPosition.BottomLeft);
        
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    public void OnDestroy()
    {
        if (bannerView != null)
            bannerView.Destroy();
    }

    private void destroyMenuAdIfExist()
    {
        GameObject ad = GameObject.Find("AdManager");
        if (ad == null)
            return;
        ad.GetComponent<MainMenuAd>().destroyAdds();
    }
}
