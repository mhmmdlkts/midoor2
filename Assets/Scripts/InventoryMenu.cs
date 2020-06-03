using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct StoreItemStruct
{
    public char team;
    public int price, weaponCode, style, quality, iconId;
    public string name;
    public StoreItemStruct(int weaponCode, int quality, int style, int price, char team, int iconId, string name)
    {
        this.weaponCode = weaponCode;
        this.quality = quality;
        this.price = price;
        this.style = style;
        this.team = team;
        this.iconId = iconId;
        this.name = name;
    }

    public StoreItemStruct(string allInfo)
    {
        string[] splitedVirgül = allInfo.Split(',');
            
        weaponCode = Convert.ToInt32(splitedVirgül[0]);
        style = Convert.ToInt32(splitedVirgül[1]);
        quality = Convert.ToInt32(splitedVirgül[2]);
        price = Convert.ToInt32(splitedVirgül[3]);
        team = Convert.ToChar(splitedVirgül[4]);
        iconId = Convert.ToInt32(splitedVirgül[5]);
        name = splitedVirgül[6];
    }
}
public class InventoryMenu : MonoBehaviour
{
    private static string storeButtonText = "Store";
    private static string inventoryButtonText = "Inventory";
    
    public TextAsset storeList;
    private string[] storeItemsStringArr;
    public List<StoreItemStruct> storeItemStruct;

    public Text storeInventoryButtonText;
    
    public GameObject storeLayout, inventoryLayout;
    
    void Start()
    {
        if (GameObject.Find(MainMenu.ArraysDataName) == null)
        {
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
            return;
        }
        openPanel(true);
        storeItemsStringArr = storeList.text.Split('\n');
        storeItemStruct = new List<StoreItemStruct>();
        initStructs();
    }

    public void storeInventoryButton()
    {
        openPanel(!inventoryLayout.activeSelf);
    }
    
    private void initStructs()
    {
        for (int i = 1; i < storeItemsStringArr.Length; i++)
        {
            storeItemStruct.Add(new StoreItemStruct(storeItemsStringArr[i]));
        }
    }

    public void openPanel(bool isInventory)
    {
        inventoryLayout.SetActive(isInventory);
        storeLayout.SetActive(!isInventory);
        storeInventoryButtonText.text = isInventory ? storeButtonText : inventoryButtonText;
        if (isInventory)
            StartCoroutine(inventoryLayout.GetComponent<myInventroy>().reload());
    }
    
    public void go_back_button()
    {
        SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<ArraysData>().menuSounds[soundId]);
    }

    public static StoreItemStruct getStruct(TextAsset weaponList, int weaponCode, int style)
    {
        string[] arr = weaponList.text.Split('\n');
        StoreItemStruct itemStruct;
        for (int i = 1; i < arr.Length; i++)
        {
            itemStruct = new StoreItemStruct(arr[i]);
            if (itemStruct.style == style && itemStruct.weaponCode == weaponCode)
                return itemStruct;
        }
        return new StoreItemStruct();
    }
}
