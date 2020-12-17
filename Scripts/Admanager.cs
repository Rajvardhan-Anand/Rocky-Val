using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class Admanager : MonoBehaviour
{

   
    public static Admanager instance;

    private string appID = "ca-app-pub-3940256099942544~3347511713";

    private BannerView bannerView;
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    private InterstitialAd fullScreenAd;
    private string fullScreenAdID = "ca-app-pub-3940256099942544/1033173712";

    private RewardBasedVideoAd rewardedAd;                                                          
    private string rewardedAdID = "ca-app-pub-3940256099942544/5224354917";

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        RequestFullScreenAd();

        rewardedAd = RewardBasedVideoAd.Instance;

        RequestRewardedAd();


        rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;

        rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

        rewardedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

        rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;
    }


    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
        print("Banner ad is shown");    //changes
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void RequestFullScreenAd()
    {
        fullScreenAd = new InterstitialAd(fullScreenAdID);

        AdRequest request = new AdRequest.Builder().Build();

        fullScreenAd.LoadAd(request);
        print("full screen ad loaded");     //changes

    }

    public void ShowFullScreenAd()
    {
        if (fullScreenAd.IsLoaded())
        {
          /*  GameObject.FindGameObjectWithTag("countdownController").GetComponent<CountdownController>().StopCountdown();  */      //stop countdownCounter 
            GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().AdButton.gameObject.SetActive(false);  // disable adbutton after interstetial ad
            fullScreenAd.Show();
            print("full screen ad is loaded");    // changes made
          
        }
        else
        {
            Debug.Log("Full screen ad not loaded");
        }
    }

    //=============================Rewarded Ad ==============================================// 
public void RequestRewardedAd()
{
    AdRequest request = new AdRequest.Builder().Build();

    rewardedAd.LoadAd(request, rewardedAdID);   
}

public void ShowRewardedAd()
{
    if (rewardedAd.IsLoaded())
    {
           rewardedAd.Show();
            print("Rewarde ad is shown");
            AppInitialisation.instance.hasSeenRewardedAd = true;
            AppInitialisation.instance.RestartButton.SetActive(true);
            AppInitialisation.instance.hasGameOver = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.y, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.z-20);
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayDelayed(1.0f);
            GameObject.FindGameObjectWithTag("Player").GetComponent<player>().directionalSpeed = 20;
            StartCoroutine(AppInitialisation.instance.StartGame(1.5f));


        }
    else
    {
        Debug.Log("Rewarded ad not loaded");
    }
}



public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
{
    Debug.Log("Rewarded Video ad loaded successfully");

}

public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
{
    Debug.Log("Failed to load rewarded video ad : " + args.Message);


}



public void HandleRewardBasedVideoRewarded(object sender, Reward args)
{
    string type = args.Type;
    double amount = args.Amount;
    Debug.Log("You have been rewarded with  " + amount.ToString() + " " + type);
     

        //GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().hasSeenRewardedAd = true;
        //GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().RestartButton.gameObject.SetActive(true);
        //GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().hasGameOver = false;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayDelayed(1.0f);
        //StartCoroutine(GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().StartGame(1.5f));      // changes made for rewarded ad


        //UIManager.instance.RewardPanel.SetActive(true);
        //UIManager.instance.GameOverUI.SetActive(false);


    }


public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
{
    Debug.Log("Rewarded video has closed");
        RequestRewardedAd(); // change it later

}
}

