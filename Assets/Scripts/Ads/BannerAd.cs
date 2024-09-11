
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    public Button hideBannerButton;

    BannerPosition bannerPos = BannerPosition.BOTTOM_CENTER;

    string AndroidAdUnit = "Banner_Android";
    string iOSAdUnit = "Banner_iOS";

    string AdUnitID;

    void Awake()
    {
        AdUnitID = AndroidAdUnit;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
            AdUnitID = iOSAdUnit;

        Advertisement.Banner.SetPosition(bannerPos);
    }

    public void LoadBanner()
    {
        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(loadOptions);
    }    

    void OnBannerLoaded()
    {
        if (hideBannerButton != null)
        {
            hideBannerButton.onClick.AddListener(HideBannerAd);
            hideBannerButton.interactable = true;
        }

        ShowBannerAd();
        
    }
    
    void ShowBannerAd()
    {
        BannerOptions bannerOptions = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
        Advertisement.Banner.Show(AdUnitID, bannerOptions);
    }

    void OnBannerError(string msg)
    {
        Debug.Log(msg);
    }

    void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }    

    void OnBannerClicked()
    {
        //when somebody clicks the banner
    }

    void OnBannerHidden()
    {
        if (hideBannerButton != null)
            hideBannerButton.interactable = false;
    }    

    void OnBannerShown()
    {
        if (hideBannerButton != null)
            hideBannerButton.interactable = true;
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
