using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
//using Assets.Scripts.Debug;
//using Newtonsoft.Json;
//using UnityEngine.Events;
//
//public class LoadAd : MonoBehaviour
//{
//    // These ad units are configured to always serve test ads.
//    // TEST: ca-app-pub-3940256099942544/5224354917
//    // PROD: ca-app-pub-4112900183758913/2996526638
//#if UNITY_ANDROID
//    private string adUnitId = "ca-app-pub-4112900183758913/2996526638";
//#elif UNITY_IPHONE
//  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
//#else
//  private string _adUnitId = "unused";
//#endif
//
//    private RewardedAd rewardedAd;
//
//    //[SerializeField] Revive revive;
//    [SerializeField] UnityEvent adStarted;
//    [SerializeField] UnityEvent adWatched;
//
//    //public void Start()
//    //{
//    //    // Initialize the Google Mobile Ads SDK.
//    //    MobileAds.Initialize((InitializationStatus initStatus) =>
//    //    {
//    //        // This callback is called once the MobileAds SDK is initialized.
//    //        LoadRewardedAd();
//    //    });
//    //}
//
//    public void Start()
//    {
//        if (adStarted == null)
//        {
//            adStarted = new UnityEvent();
//        }
//
//        if (adWatched == null)
//        {
//            adWatched = new UnityEvent();
//        }
//    }
//
//    public void LoadRewardedAd()
//    {
//        ProductionDebug.Log("Trying to load ad");
//        // Clean up the old ad before loading a new one.
//        if (rewardedAd != null)
//        {
//            ProductionDebug.Log("Removing previous Ad");
//            rewardedAd.Destroy();
//            rewardedAd = null;
//        }
//
//        ProductionDebug.Log("Create new ad request");
//        // create our request used to load the ad.
//        var adRequest = new AdRequest();
//        adRequest.Keywords.Add("unity-admob-sample");
//
//        // send the request to load the ad.
//        ProductionDebug.Log("Load the rewarded ad.");
//
//        try
//        {
//            RewardedAd.Load(adUnitId, adRequest,
//                (RewardedAd ad, LoadAdError error) =>
//                {
//                    ProductionDebug.Log("finished loading the rewarded ad.");
//
//                    // if error is not null, the load request failed.
//                    if (error != null || ad == null)
//                    {
//                        Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
//                        return;
//                    }
//
//                    ProductionDebug.Log("Rewarded ad loaded with response : "
//                                        + JsonConvert.SerializeObject(ad.GetResponseInfo()));
//
//                    rewardedAd = ad;
//                    RegisterEventHandlers(rewardedAd);
//                });
//        }
//        catch (Exception e)
//        {
//            ProductionDebug.Log("RewardedAd.Load threw an error " + e.Message);
//        }
//
//        ProductionDebug.Log("Sent load ad request");
//    }
//
//
//    public void ShowRewardedAd()
//    {
//        ProductionDebug.Log("Clicked TV button");
//        //LoadRewardedAd();
//        const string rewardMsg =
//            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
//
//        if (rewardedAd != null && rewardedAd.CanShowAd())
//        {
//            adStarted.Invoke();
//            rewardedAd.Show((Reward reward) =>
//            {
//                adWatched.Invoke();
//                //revive.WatchTV();
//                // TODO: Reward the user.
//                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
//                //LoadRewardedAd();
//            });
//        }
//    }
//
//    private void RegisterEventHandlers(RewardedAd ad)
//    {
//        // Raised when the ad is estimated to have earned money.
//        ad.OnAdPaid += (AdValue adValue) =>
//        {
//            ProductionDebug.Log(String.Format("Rewarded ad paid {0} {1}.",
//                adValue.Value,
//                adValue.CurrencyCode));
//            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
//                adValue.Value,
//                adValue.CurrencyCode));
//        };
//        // Raised when an impression is recorded for an ad.
//        ad.OnAdImpressionRecorded += () =>
//        {
//            ProductionDebug.Log("Rewarded ad recorded an impression.");
//            Debug.Log("Rewarded ad recorded an impression.");
//        };
//        // Raised when a click is recorded for an ad.
//        ad.OnAdClicked += () =>
//        {
//            ProductionDebug.Log("Rewarded ad was clicked.");
//            Debug.Log("Rewarded ad was clicked.");
//        };
//        // Raised when an ad opened full screen content.
//        ad.OnAdFullScreenContentOpened += () =>
//        {
//            ProductionDebug.Log("Rewarded ad full screen content opened.");
//            Debug.Log("Rewarded ad full screen content opened.");
//        };
//        // Raised when the ad closed full screen content.
//        ad.OnAdFullScreenContentClosed += () =>
//        {
//            ProductionDebug.Log("Rewarded ad full screen content closed.");
//            Debug.Log("Rewarded ad full screen content closed.");
//            //LoadRewardedAd();
//        };
//        // Raised when the ad failed to open full screen content.
//        ad.OnAdFullScreenContentFailed += (AdError error) =>
//        {
//            ProductionDebug.Log("Rewarded ad failed to open full screen content " +
//                                "with error : " + error);
//            Debug.LogError("Rewarded ad failed to open full screen content " +
//                           "with error : " + error);
//
//            //LoadRewardedAd();
//        };
//    }
//}