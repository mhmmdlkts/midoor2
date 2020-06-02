using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public Image image, bg, frame;
    public Text weaponName, buyButtonText, priceText;
    public GameObject buyButton, myInventory;
    public int weaponCode, style, price, quality;
    public string name, playerPrefabKey, playerPrefabDef, inventoryCode;
    private Sprite[] awpImgs, knifeImgs;
    private ArraysData arraysData;

    private void init()
    {
        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        awpImgs = arraysData.awpImgs;
        knifeImgs = arraysData.knifeImgs;
        myInventory = GameObject.Find("ContainerInventoryStore");
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
        }

        return null;
    }
}
