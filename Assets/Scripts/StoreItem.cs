using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public Image image, bg, frame;
    public Text weaponName, buyButtonText, priceText;
    public GameObject buyButton;
    private GameObject container;
    public int weaponCode, style, price, quality;
    public string name, playerPrefabKey, playerPrefabDef, inventoryCode;
    private Sprite[] awpImgs, knifeImgs, zeusImgs;
    private ArraysData arraysData;
    private RectTransform _rectTransform, _containerRectTransform;

    private void init()
    {
        initWidth();
        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        awpImgs = arraysData.awpImgs;
        knifeImgs = arraysData.knifeImgs;
        zeusImgs = arraysData.zeusImgs;
    }

    private void initWidth()
    {
        container = GameObject.Find("StorePanel");
        _rectTransform = GetComponent<RectTransform>();
        _containerRectTransform = container.GetComponent<RectTransform>();
        UpdateWidth();
    }
    
    
    private void UpdateWidth()
    {
        _rectTransform.sizeDelta = new Vector2(_containerRectTransform.rect.size.x, _rectTransform.sizeDelta.y);
    }

    private void disableButton()
    {
        buyButton.GetComponent<Button>().interactable = false;
        buyButtonText.text = "Bought";
    }

    public void buyButtonListener()
    {
        if(PlayerPrefs.GetInt("money",0) >= price)
            buyIt();
    }

    public void configure(StoreItemStruct storeItemStruct)
    {
        init();
        weaponCode = storeItemStruct.weaponCode;
        quality = storeItemStruct.quality;
        style = storeItemStruct.style;
        price = storeItemStruct.price;
        name = storeItemStruct.name;
        
        priceText.text = "$" + price;
        weaponName.text = name;
        frame.color = arraysData.qualityColors[quality];
        image.sprite = getImg(weaponCode, style);
        
        playerPrefabKey = MainMenu.playerPrafsWeaponKey[weaponCode];
        playerPrefabDef = MainMenu.playerPrafsWeaponDef[weaponCode];
        inventoryCode = PlayerPrefs.GetString(playerPrefabKey, playerPrefabDef);
        
        if (haveBought())
            disableButton();
    }

    private void buyIt()
    {
        PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")-price);
        GameObject.Find("Panel").GetComponent<PlayerStatus>().setStatus();
        PlayerPrefs.SetString(playerPrefabKey, style + "," + inventoryCode);
        disableButton();
    }

    private bool haveBought()
    {
        string s = inventoryCode.Split('-')[0];
        return s.Contains(style.ToString());
    }

    private Sprite getImg(int weaponCode, int style)
    {
        switch (weaponCode)
        {
            case 0:
                return awpImgs[style];
            case 1:
                return knifeImgs[style];
            case 2:
                return zeusImgs[style];
        }

        return null;
    }
}
