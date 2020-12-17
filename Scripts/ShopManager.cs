using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    public static ShopManager Instance{get {return instance;} }

    public int currentSkinIndex =0;
    public int currency= 0;
    public int skinAvailability= 1;
  

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

       if(PlayerPrefs.HasKey("CurrentSkin"))
        {
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            currency = PlayerPrefs.GetInt("Currency");
            skinAvailability = PlayerPrefs.GetInt("skinAvailability");
        }
        else
        {
            save();
        }
    }

    public void save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("skinAvailability", skinAvailability);
    }
}
