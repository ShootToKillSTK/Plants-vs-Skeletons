using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;


[RequireComponent(typeof(InterstitialAd), typeof(RewardAd), typeof(BannerAd))]
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public string AndroidGameID = "5677169";
    public string iOSGameID = "5677168";

    private InterstitialAd _InterstitialAd;
    public InterstitialAd InterstitialAd => _InterstitialAd;

    private RewardAd _RewardAd;
    public RewardAd RewardAd => _RewardAd;

    private BannerAd _BannerAd;
    public BannerAd BannerAd => _BannerAd;

    void Awake()
    {
        _InterstitialAd = GetComponent<InterstitialAd>();
        _RewardAd = GetComponent<RewardAd>();
        _BannerAd = GetComponent<BannerAd>();

        string gameID = AndroidGameID;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameID = iOSGameID;

        if (string.IsNullOrEmpty(gameID))
            throw new InvalidDataException("no game ID");

        Advertisement.Initialize(gameID, true, this);
    }


    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Initalization Complete");
        BannerAd.LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Initalization Failed");
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
