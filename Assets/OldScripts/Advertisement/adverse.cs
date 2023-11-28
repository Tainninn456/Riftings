using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class adverse : MonoBehaviour
{
    private BannerView bannerView;
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        
        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6657879172919264/8910215133";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
        this.bannerView.Show();
    }
    
}
