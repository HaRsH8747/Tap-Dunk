using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Admob : Singleton<Admob>
{
    [Header("Admob Ad Units :")]
    // Test App Id = "ca-app-pub-3940256099942544~3347511713";
    [SerializeField] [TextArea(1, 2)] string idBanner;
    [SerializeField] [TextArea(1, 2)] string idInterstitial;
    [SerializeField] [TextArea(1, 2)] string idReward;

    [Header("Toggle Admob Ads :")]
    [SerializeField] private bool bannerAdEnabled = true;
    [SerializeField] private bool interstitialAdEnabled = true;
    [SerializeField] private bool rewardedAdEnabled = true;

    [HideInInspector] public BannerView AdBanner;
    [HideInInspector] public InterstitialAd interstitialAd;

    [HideInInspector] public RewardedAd rewardedAd;

    bool _firstInit = false;

    [System.Serializable]
    public class AdConfig
    {
        public bool adEnabled;
        public bool bannerEnabled;
        public bool interstitialEnabled;
        public bool rewardedEnabled;
        public string appId;
        public string appOpenId;
        public string bannerId;
        public string interstitialId;
        public string rewardedId;
    }

    IEnumerator FetchAdConfig(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                AdConfig adConfig = ParseAdConfig(jsonResponse);

                if (adConfig.adEnabled)
                {
                    // Initialize MobileAds with the fetched app ID
                    MobileAds.Initialize(initstatus =>
                    {
                        MobileAdsEventExecutor.ExecuteInUpdate(() =>
                        {
                            // Use the fetched ad unit IDs
                            idBanner = adConfig.bannerId;
                            idInterstitial = adConfig.interstitialId;
                            idReward = adConfig.rewardedId;

                            // Set ad enabled flags
                            bannerAdEnabled = adConfig.bannerEnabled;
                            interstitialAdEnabled = adConfig.interstitialEnabled;
                            rewardedAdEnabled = adConfig.rewardedEnabled;

                            ShowBanner();
                            RequestRewardAd();
                            RequestInterstitialAd();

                            _firstInit = true;
                        });
                    });
                }
                else
                {
                    Debug.Log("Ads are disabled.");
                }
            }
        }
    }

    AdConfig ParseAdConfig(string jsonResponse)
    {
        return JsonUtility.FromJson<AdConfig>(jsonResponse);
    }

    protected override void Awake()
    {
        base.Awake();

        // show banner every scene loaded
        SceneManager.sceneLoaded += (Scene s, LoadSceneMode lsm) => { if (_firstInit) ShowBanner(); };
    }

    private bool IsRunningInFirebaseTestLab()
    {
        // Call the Java method to check if running in Firebase Test Lab
        AndroidJavaClass firebaseTestLabCheckerClass = new AndroidJavaClass("com.playmate.flappydunkmaster.FirebaseTestLabChecker");
        if (firebaseTestLabCheckerClass != null)
        {
            return firebaseTestLabCheckerClass.CallStatic<bool>("isRunningInTestLab");
        }
        else
        {
            Debug.LogError("Failed to find FirebaseTestLabChecker class");
            return false;
        }
    }

    private void InitializeAdMob()
    {
        MobileAds.Initialize(initstatus => {
            MobileAdsEventExecutor.ExecuteInUpdate(() => {
                ShowBanner();
                RequestRewardAd();
                RequestInterstitialAd();

                _firstInit = true;
            });
        });
    }

    

    private void Start()
    {
        // RequestConfiguration requestConfiguration =
        //     new RequestConfiguration.Builder()
        //         .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
        //         .build();
   
        // if(!IsRunningInFirebaseTestLab()){
        //     MobileAds.Initialize(initstatus => {
        //         MobileAdsEventExecutor.ExecuteInUpdate(() => {
        //             ShowBanner();
        //             RequestRewardAd();
        //             RequestInterstitialAd();

        //             _firstInit = true;
        //         });
        //     });
        // }

        StartCoroutine(FetchAdConfig("https://play-mate.web.app/flappy-dunk-master/custom-ads.json"));

    }

    private void OnDestroy()
    {
        DestroyBannerAd();
        DestroyInterstitialAd();
    }

    public void Destroy() => Destroy(gameObject);

    public bool IsRewardAdLoaded()
    {
        if (rewardedAdEnabled && rewardedAd.CanShowAd())
            return true;
        else
            return false;
    }

    // AdRequest CreateAdRequest()
    // {

    //     // var adRequest = new AdRequest();
    //     // adRequest.Keywords.Add("unity-admob-sample");
    //     // return new AdRequest.Builder()
    //     //    .TagForChildDirectedTreatment(false)
    //     //    .AddExtra("npa", PlayerPrefs.GetInt("npa", 1).ToString())
    //     //    .Build();
    // }

    #region Banner Ad ------------------------------------------------------------------------------
    public void ShowBanner()
    {
        if (!bannerAdEnabled) return;

        DestroyBannerAd();

        AdSize adSize = AdSize.GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        AdBanner = new BannerView(idBanner, adSize, AdPosition.Bottom);
        
        var adRequest = new AdRequest();

        AdBanner.LoadAd(adRequest);
    }

    public void DestroyBannerAd()
    {
        if (AdBanner != null)
            AdBanner.Destroy();
    }
    #endregion

    #region Interstitial Ad ------------------------------------------------------------------------
    public void RequestInterstitialAd()
    {
        // AdInterstitial = new InterstitialAd(idInterstitial);

        // AdInterstitial.OnAdClosed += HandleInterstitialAdClosed;

        // AdInterstitial.LoadAd(CreateAdRequest());
        
        if (interstitialAd!=null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(idInterstitial, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
              if (error!=null||ad==null)
              {
                print("Interstitial ad failed to load"+error);
                return;
              }

            print("Interstitial ad loaded !!"+ad.GetResponseInfo());

            interstitialAd = ad;
            InterstitialEvent(interstitialAd);
        });

    }

    public void ShowInterstitialAd()
    {
        if (!interstitialAdEnabled) return;

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else {
            print("Intersititial ad not ready!!");
        }
    }

    public void InterstitialEvent(InterstitialAd ad) {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) => 
        {
            Debug.Log("Interstitial ad paid {0} {1}."+
                adValue.Value+
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => {
            DestroyInterstitialAd();
            RequestInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
            interstitialAd.Destroy();
    }
    #endregion

    #region Rewarded Ad ----------------------------------------------------------------------------
    public void RequestRewardAd()
    {
        // AdReward = new RewardedAd(idReward);

        // AdReward.OnAdFullScreenContentClosed += HandleOnRewardedAdClosed;
        // AdReward.OnAdPaid += HandleOnRewardedAdWatched;

        // AdReward.LoadAd(CreateAdRequest());

        if (rewardedAd!=null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        var adRequest = new AdRequest();
        // adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(idReward, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Rewarded failed to load"+error);
                return;
            }

            print("Rewarded ad loaded !!");
            rewardedAd = ad;
            RewardedAdEvents(rewardedAd);
        });
    }

    public void ShowRewardAd()
    {
        if (!rewardedAdEnabled) return;

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                print("rewarded watched");
                FindObjectOfType<GameManager>().RevivePlayer();
            });
        }
        else {
            print("Rewarded ad not ready");
        }
    }


    public void RewardedAdEvents(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            print("rewarded watched");
            FindObjectOfType<GameManager>().RevivePlayer();
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            RequestRewardAd();
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
    #endregion

    #region Event Handler
    private void HandleInterstitialAdClosed(object sender, EventArgs e)
    {
        DestroyInterstitialAd();
        RequestInterstitialAd();
    }

    private void HandleOnRewardedAdClosed(object sender, EventArgs e)
    {
        RequestRewardAd();
    }

    private void HandleOnRewardedAdWatched(object sender, Reward e)
    {
        print("rewarded watched");
        FindObjectOfType<GameManager>().RevivePlayer();
    }
    #endregion
}
