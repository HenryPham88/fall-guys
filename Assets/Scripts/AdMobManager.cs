using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class AdMobManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdMobManager _AdMobInstance;

    public enum BannerAd { Admob, Unity }
    public enum InterstitialAds {Admob, Unity }

    [Header("SELECT AD NETWORK")]
    public BannerAd bannerAd;
    public InterstitialAds FullScreenAd;
    [Space]
    [Header("[*]  ADMOB AD IDs")]
    public string bannerAdId;
    public string interstitialAdId;
    public  bool isOnTop;
    [Space]

    [Header("[*]  UNITY AD IDs")]
    public string BannerID;
    public string InterstitialID;
    private static BannerView bannerView;
	private static InterstitialAd interstitial ;

	// Use this for initialization

	void Awake ()
	{
		if (_AdMobInstance) {
		DestroyImmediate (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			_AdMobInstance = this;
		}
	}


	void Start ()
	{
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);


        loadInterstitial();

        switch (FullScreenAd)
        {
            case InterstitialAds.Admob:
                showBannerAd();
                 break;
            case InterstitialAds.Unity:
                LoadBanner();

                break;
        }
	}
	
   

	public  void showBannerAd ()
	{
		
			bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
			AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);


    }
	

	public  void loadInterstitial ()
	{
        switch (FullScreenAd)
        {
            case InterstitialAds.Admob:
                interstitial = new InterstitialAd(interstitialAdId);
                AdRequest request = new AdRequest.Builder().Build();
                interstitial.LoadAd(request);
                break;

            case InterstitialAds.Unity:
                Advertisement.Load(InterstitialID, this);
                break;

        

        }
       
	}

	public  void showInterstitial ()
	{
        switch (FullScreenAd)
        {
            case InterstitialAds.Admob:
                if (interstitial.IsLoaded())
                {

                    interstitial.Show();

                }
                else
                {
                    loadInterstitial();
                    interstitial.Show();

                }
                break;

            case InterstitialAds.Unity:
                Advertisement.Show(InterstitialID, this);

                break;



        }

       
    }

	

	public  void hideBannerAd ()
	{
		bannerView.Hide();
	}


    //----------------------
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
       
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
      
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }

    //-------------------------------------

    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(BannerID, options);
    }

    void OnBannerLoaded()
    {

        showBannerAd();
    }

   
    void OnBannerError(string message)
    {
    
    }

    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(BannerID, options);
    }
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }



}
