using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AppInitialisation : MonoBehaviour
{
    public static AppInitialisation instance;
    public GameObject InGameUI;
    public GameObject InMenuUI;
    public GameObject PauseGameUI;
    public GameObject GameOverUI;
  
    public GameObject player;
    public GameObject AdButton;     //changes made
    public GameObject RestartButton;
    private Rigidbody rb;
    public int gamecount;
    public bool hasGameStarted = false;
    public bool hasSeenRewardedAd = false;
    public bool hasGameOver=false;
  

    private void Awake()
    {
        instance = this;
        Shader.SetGlobalFloat("_Curvature", 2.1f);
        Shader.SetGlobalFloat("_Trimming", 0.1f);
        Application.targetFrameRate = 60;
     
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        hasGameOver = false;
       /* PlayerPrefs.DeleteAll();  */      // use to reset gamecount
        rb.constraints = RigidbodyConstraints.FreezePosition;
        InMenuUI.gameObject.SetActive(true);
        PauseGameUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);
        shopUI.instance.ShopUIPanel.gameObject.SetActive(false);
      
        gamecount = PlayerPrefs.GetInt("count");

    
        Admanager.instance.RequestBanner();         // changes made BannerAd
        Admanager.instance.RequestFullScreenAd();  // changes made for full screen ad

    }

    public void PlayButton()
    {   
        if(hasGameStarted == true)
        {
            //Time.timeScale = 1;
            StartCoroutine(StartGame(1.0f));
            Admanager.instance.HideBanner();    // changes made hide BannerAd
        }
        else
        {   
            StartCoroutine(StartGame(0.0f));
        }
           
    }

    public void PauseGame()
    {
        hasGameOver = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = true;
        InMenuUI.gameObject.SetActive(false);       //changes made to false
        PauseGameUI.gameObject.SetActive(true);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);
        shopUI.instance.ShopUIPanel.gameObject.SetActive(false);
        Admanager.instance.RequestBanner();     //changes made bannerAd
    }

    public void GameOver()
    {
        hasGameOver = true;
        /* Time.timeScale = 0; */        //to freeze time and game
        GameObject.FindGameObjectWithTag("Player").GetComponent<player>().directionalSpeed = 0;
      

        rb.constraints = RigidbodyConstraints.FreezeAll;


        RestartButton.gameObject.SetActive(false);       // serious shit remove if doesnt work


        //========= check f internet is on or off and accordingly turn restart button on or off=========//
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //RestartButton.gameObject.SetActive(false);          //Do something about this
            //AdButton.gameObject.SetActive(true);

            if (hasSeenRewardedAd == true)                   // if rewarded ad is seen then turn off the reward ad button
            {
                CountdownController.instance.StopCountdown();
                AdButton.gameObject.SetActive(false);
                RestartButton.gameObject.SetActive(true);
            }
            else
            {   

                AdButton.gameObject.SetActive(true);        // changes made to countdown
                StartCoroutine(DooCountdown(3.3f));
               
            }
            Debug.Log("Network available");
        }
        else
        {
            RestartButton.gameObject.SetActive(true);
            AdButton.gameObject.SetActive(false);
         
            Debug.Log("Network not  available");
        }
//================= check f internet is on or off and accordingly turn restart button on or off=========//


        //===========gamecount================//
        hasGameStarted = true;
        gamecount++;
        PlayerPrefs.SetInt("count", gamecount);     //changes made
        if (gamecount == 4)
        {
            Admanager.instance.ShowFullScreenAd();  // changes made fullScreenAd
            gamecount = 0;
            PlayerPrefs.SetInt("count", gamecount);
        }

        //=========gamecount=============//

        InMenuUI.gameObject.SetActive(false);
        PauseGameUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
       GameOverUI.gameObject.SetActive(true);
        shopUI.instance.ShopUIPanel.gameObject.SetActive(false);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
      
    }

    //public void ShowAd()
    //{
    //    hasSeenRewardedAd = true;   // temparary testing
    //    StartCoroutine(StartGame(1.0f));   //Fix later  StartCoroutine(StartGame(1.0f)); changes made
    //}
public IEnumerator StartGame(float waitTime)
    {
        //Time.timeScale = 1;
        InMenuUI.gameObject.SetActive(false);
        PauseGameUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(true);
        GameOverUI.gameObject.SetActive(false);
        shopUI.instance.ShopUIPanel.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitTime);
        rb.constraints = RigidbodyConstraints.None;        
        rb.constraints = RigidbodyConstraints.FreezePositionY;

    }

 public IEnumerator DooCountdown(float waitTime)
    {
         yield return new WaitForSeconds(waitTime);
        AdButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
    }
}
