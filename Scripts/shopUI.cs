using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopUI : MonoBehaviour
{
    public static shopUI instance;
    public GameObject ShopUIPanel;
    public GameObject backButton;
    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;
    public TextMeshProUGUI CurrencyText;
 


    public Material PlayerMaterial;

    private int[] costs = { 0, 150, 150, 150,
                           300, 300, 300, 300,
                           500, 500, 500, 500,
                           1000, 1250, 1500, 2000};
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
      
        int textureIndex = 0;
       
      
        ChangePlayerSkin(ShopManager.Instance.currentSkinIndex);
        CurrencyText.text = "Currency : " + ShopManager.Instance.currency.ToString();

        Sprite[] textures = Resources.LoadAll<Sprite>("Player");
        foreach(Sprite texture in textures)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopButtonContainer.transform, false);

            int index = textureIndex;
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
            container.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = costs[index].ToString();
            //container.transform.GetComponentInChildren<Text>().text = costs[index].ToString();
            if ((ShopManager.Instance.skinAvailability & 1 << index) == 1 << index)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
            }
           textureIndex++;
        }
    }

   
    void Update()
    {
       
    }

 public void ShopUIMethod()
    {
      
        AppInitialisation.instance.InMenuUI.gameObject.SetActive(false);
        AppInitialisation.instance.PauseGameUI.gameObject.SetActive(false);
        AppInitialisation.instance.InGameUI.gameObject.SetActive(false);
        AppInitialisation.instance.GameOverUI.gameObject.SetActive(false);
        ShopUIPanel.gameObject.SetActive(true);


    }

    public void BackShopUI()
    {
  
        AppInitialisation.instance.InMenuUI.gameObject.SetActive(true);
        AppInitialisation.instance.PauseGameUI.gameObject.SetActive(false);
        AppInitialisation.instance.InGameUI.gameObject.SetActive(false);
        AppInitialisation.instance.GameOverUI.gameObject.SetActive(false);
        ShopUIPanel.gameObject.SetActive(false);
    }

    private void ChangePlayerSkin(int index)
    {
        if ((ShopManager.Instance.skinAvailability & 1 << index) == 1 << index)
        {
            float x = (index % 4) * 0.25f;
            float y = ((int)index / 4) * 0.25f;

            if (y == 0.0f)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.5f;
            else if (y == 0.50f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0f;

            PlayerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
            ShopManager.Instance.currentSkinIndex = index;
            ShopManager.Instance.save();
        }
        else
        {
            // You do not have the skin, do you want to buy it
            int  cost = costs[index];
            Debug.Log(cost);

            if(ShopManager.Instance.currency >= cost)
            {
                ShopManager.Instance.currency -= cost;
                ShopManager.Instance.skinAvailability += 1 << index;
                ShopManager.Instance.save();
                CurrencyText.text = "Currency : " + ShopManager.Instance.currency.ToString();
                shopButtonContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                ChangePlayerSkin(index);
            }
        }
    }


}
