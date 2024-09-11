using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string AndroidAdUnit = "Rewarded_Android";
    string iOSAdUnit = "Rewarded_iOS";

    string AdUnitID;

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(AdUnitID, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();

        //reward goes here...
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        AdUnitID = AndroidAdUnit;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
            AdUnitID = iOSAdUnit;
    }

    public void LoadAd()
    {
        Advertisement.Load(AdUnitID, this);
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
