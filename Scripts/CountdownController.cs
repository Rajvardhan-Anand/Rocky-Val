using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public static CountdownController instance;
    //public int seconds = 3;
    public TextMeshProUGUI countdownUI;
    //public GameObject AdButton;
    //public GameObject RestartButton;
    public int i = 3;


    private void Awake()
    {
        instance = this;
     }

    void Start()
    {

        if (countdownUI == null)
        {
            countdownUI = GetComponent<TextMeshProUGUI>();
        }
        //StartCountdown();
        StartCoroutine(DoCountdown());
    }

    public void StopCountdown()
    {
        StopAllCoroutines();
    }

    //public void StartCountdown()
    //{
    //    StartCoroutine(DoCountdown());
    //}

    IEnumerator DoCountdown()
    {
        for (; i >= 1;)
        {
            yield return new WaitForSeconds(1);
            countdownUI.text = i.ToString();
            if (i == 1)                                  //changes made
            {
                yield return new WaitForSeconds(1);
              
                //AppInitialisation.instance.RestartButton.gameObject.SetActive(true);
                //AppInitialisation.instance.AdButton.gameObject.SetActive(false);

                //GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().RestartButton.gameObject.SetActive(true);
                //GameObject.FindGameObjectWithTag("sceneManager").GetComponent<AppInitialisation>().AdButton.gameObject.SetActive(false);
            }
            i--;
        }

    }

}
