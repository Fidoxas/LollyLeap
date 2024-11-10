using Assets.Scripts.Debug;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleMobileAdsInit : MonoBehaviour
{
    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            ProductionDebug.Log("Admob initialized");
            // This callback is called once the MobileAds SDK is initialized.
        });
    }
}