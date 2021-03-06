﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class youKilledWithWeaponImg : MonoBehaviour
{
    public TextAsset listWeapon;
    public Image image, bg, qualityLine;
    public Text weaponName, title;
    private ArraysData arraysData;
    void Start()
    {
        Invoke(nameof(destroyMe), EndRoundShow.stayTime);
    }

    void destroyMe()
    {
        Destroy(gameObject);
    }

    public void show(int weaponCode, int style, string playersName)
    {
        Debug.Log("weaponCode: " + weaponCode + " style: " + style);
        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        StoreItemStruct itemStruct = InventoryMenu.getStruct(listWeapon, weaponCode, style);
        image.sprite = getSprite(weaponCode, style);
        qualityLine.color = arraysData.qualityColors[itemStruct.quality];
        weaponName.text = itemStruct.name;
        title.text = LanguageSystem.GET_SHOW_WEAPON_PANEL_TITLE_START() + " " + playersName + LanguageSystem.GET_SHOW_WEAPON_PANEL_TITLE_MIDDLE() + " " + LanguageSystem.GET_SHOW_WEAPON_PANEL_TITLE_END();
    }

    private Sprite getSprite(int weaponCode, int style)
    {
        switch (weaponCode)
        {
            case 0:
                return arraysData.awpImgs[style];
            case 1:
                return arraysData.knifeImgs[style];
            case 2:
                return arraysData.zeusImgs[style];
        }

        return null;
    }
}
