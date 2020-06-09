using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ads
{
    Menu_Banner,
    Game_Banner,
    Menu_Popup,
    Game_Popup,
    Search_Reward,
}
public class AdUnitIds : MonoBehaviour
{
    public static bool isTest = false;
    
    private static string adUnitId_UNEXPECTED = "unexpected_platform";
    
    private static string appID_Android                  = "ca-app-pub-2593865920560334~8261054930";
    private static string adUnitId_Test_Banner_Android   = "ca-app-pub-3940256099942544/6300978111";
    private static string adUnitId_Menu_Banner_Android   = "ca-app-pub-2593865920560334/4948495886";
    private static string adUnitId_Game_Banner_Android   = "ca-app-pub-2593865920560334/3632093523";
    
    private static string adUnitId_Test_Popup_Android    = "ca-app-pub-3940256099942544/1033173712";
    private static string adUnitId_Menu_Popup_Android    = "ca-app-pub-2593865920560334/5620248029";
    private static string adUnitId_Game_Popup_Android    = "ca-app-pub-2593865920560334/5783899551";
    
    private static string adUnitId_Test_Reward_Android   = "ca-app-pub-3940256099942544/5224354917";
    private static string adUnitId_Search_Reward_Android = "ca-app-pub-2593865920560334/9348142993";
    
    
    private static string appID__Iphone                  = "ca-app-pub-2593865920560334~8283098053";
    
    private static string adUnitId_Game_Popup__Iphone    = "ca-app-pub-2593865920560334/8437983054";
    private static string adUnitId_Menu_Popup__Iphone    = "ca-app-pub-2593865920560334/5464049948";
    private static string adUnitId_Test_Popup__Iphone    = "ca-app-pub-3940256099942544/4411468910";
    
    private static string adUnitId_Game_Banner__Iphone   = "ca-app-pub-2593865920560334/9403294957";
    private static string adUnitId_Menu_Banner__Iphone   = "ca-app-pub-2593865920560334/4723126990";
    private static string adUnitId_Test_Banner__Iphone   = "ca-app-pub-3940256099942544/2934735716";
    
    private static string adUnitId_Test_Reward__Iphone   = "ca-app-pub-3940256099942544/1712485313";
    private static string adUnitId_Search_Reward_Iphone  = "ca-app-pub-2593865920560334/6096467159";

    public static string getAdUnitId(Ads ad)
    {
        string adUnitId = "unexpected_platform";
        switch (ad)
        {
            case Ads.Menu_Banner:
                #if UNITY_ANDROID
                    adUnitId = isTest?adUnitId_Test_Banner_Android:adUnitId_Menu_Banner_Android;
                #elif UNITY_IPHONE
                    adUnitId = isTest?adUnitId_Test_Banner__Iphone:adUnitId_Menu_Banner__Iphone;
                #endif
                break;
            case Ads.Menu_Popup:
                #if UNITY_ANDROID
                    adUnitId = isTest?adUnitId_Test_Popup_Android:adUnitId_Menu_Popup_Android;
                #elif UNITY_IPHONE
                    adUnitId = isTest?adUnitId_Test_Popup__Iphone:adUnitId_Menu_Popup__Iphone;
                #endif
                break;
            case Ads.Game_Banner:
                #if UNITY_ANDROID
                    adUnitId = isTest?adUnitId_Test_Banner_Android:adUnitId_Game_Banner_Android;
                #elif UNITY_IPHONE
                    adUnitId = isTest?adUnitId_Test_Banner__Iphone:adUnitId_Game_Banner__Iphone;
                #endif
                break;
            case Ads.Game_Popup:
                #if UNITY_ANDROID
                    adUnitId = isTest?adUnitId_Test_Popup_Android:adUnitId_Game_Popup_Android;
                #elif UNITY_IPHONE
                    adUnitId = isTest?adUnitId_Test_Popup__Iphone:adUnitId_Game_Popup__Iphone;
                #endif
                break;
            case Ads.Search_Reward:
                #if UNITY_ANDROID
                    adUnitId = isTest?adUnitId_Test_Reward_Android:adUnitId_Search_Reward_Android;
                #elif UNITY_IPHONE
                    adUnitId = isTest?adUnitId_Test_Reward__Iphone:adUnitId_Search_Reward_Iphone;
                #endif
                break;
        }
        return adUnitId;
    }

    public static string getAdAppId()
    {
        #if UNITY_ANDROID
            return appID_Android;
        #elif UNITY_IPHONE
            return appID__Iphone;
        #endif
            return "No Supported Device for ads";
    }
}
