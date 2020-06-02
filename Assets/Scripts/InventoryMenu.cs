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
            string[] splitedVirgül = storeItemsStringArr[i].Split(',');
            
            int weaponCode = Convert.ToInt32(splitedVirgül[0]);
            int style = Convert.ToInt32(splitedVirgül[1]);
            int quality = Convert.ToInt32(splitedVirgül[2]);
            int price = Convert.ToInt32(splitedVirgül[3]);
            char team = Convert.ToChar(splitedVirgül[4]);
            int iconId = Convert.ToInt32(splitedVirgül[5]);
            string name = splitedVirgül[6];
            storeItemStruct.Add(new StoreItemStruct(weaponCode, quality, style, price, team, iconId, name));
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

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<ArraysData>().menuSounds[soundId]);
    }

    public StoreItemStruct getStruct(int weaponCode, int style)
    {
        foreach (var item in storeItemStruct)
        {
            if (item.style == style && item.weaponCode == weaponCode)
                return item;
        }

        return new StoreItemStruct();
    }
}
