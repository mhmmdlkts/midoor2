using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : Subject, InventoryInterface
{
    public GameObject equipMenuPrefab, createdMenuPrefab, equipedCt, equipedT;
    public Image image, bg, qualityLine;
    public Text weaponName;
    public int weaponCode, style, quality;
    public char team;
    private ArraysData arraysData;
    public myInventroy myInventroy;

    void init()
    {
        myInventroy = GameObject.Find("ScrollInventory").GetComponent<myInventroy>();
        arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
    }

    public void registerObserver()
    {
        foreach (var subject in FindObjectsOfType<Subject>())
        {
            subject.RegisterObserver(this);
        }
    }

    public void equipWeapon(char team)
    {
        if (team == '-')
        {
            equipedT.SetActive(false);
            equipedCt.SetActive(false);
            return;
        }
        Notify(weaponCode, team);
        switch (team)
        {
            case 'T':
                equipedT.SetActive(true);
                setPrefabEquip(true);
                break;
            case 'C':
                equipedCt.SetActive(true);
                setPrefabEquip(false);
                break;
            case 'B':
                equipedCt.SetActive(true);
                equipedT.SetActive(true);
                setPrefabEquip(true);
                setPrefabEquip(false);
                break;
        }
    }

    private void setPrefabEquip(bool forT)
    {
        string code = PlayerPrefs.GetString(MainMenu.playerPrafsWeaponKey[weaponCode], MainMenu.playerPrafsWeaponDef[weaponCode]);
        string weapons = code.Split('-')[0];
        string eqT = code.Split('-')[1].Split('=')[0];
        string eqCT = code.Split('-')[1].Split('=')[1];
        Debug.Log("1?" + code);
        code = weapons + '-' + (forT ? Convert.ToString(style) : eqT) + '=' + (forT ? eqCT : Convert.ToString(style));
        Debug.Log("2?" + code);
        PlayerPrefs.SetString(MainMenu.playerPrafsWeaponKey[weaponCode], code);
    }

    public void buttonListener()
    {
        if (myInventroy.isChoseMenuOpen)
            return;
        Instantiate(equipMenuPrefab, GameObject.Find("Canvas").transform, false).GetComponent<EquipMenu>().open(this, weaponCode, style, team);
        //equipWeapon('B');
    }

    public void OnEquipOther(int weaponCode, char team)
    {
        if (this.weaponCode != weaponCode)
            return;
        switch (team)
        {
            case 'T':
                equipedT.SetActive(false);
                break;
            case 'C':
                equipedCt.SetActive(false);
                break;
            case 'B':
                equipedCt.SetActive(false);
                equipedT.SetActive(false);
                break;
        }
    }

    public void configure(StoreItemStruct storeItemStruct, char eqTeam)
    {
        init();
        
        team = storeItemStruct.team;
        weaponCode = storeItemStruct.weaponCode;
        style = storeItemStruct.style;
        quality = storeItemStruct.quality;
        equipWeapon('-');
        equipWeapon(eqTeam);
        image.sprite = getImg(weaponCode, style);
        qualityLine.color = arraysData.qualityColors[quality];
        weaponName.text = storeItemStruct.name;
    }

    private Sprite getImg(int weaponCode, int style)
    {
        switch (weaponCode)
        {
            case 0:
                return arraysData.awpImgs[style];
            case 1:
                return arraysData.knifeImgs[style];
        }

        return null;
    }
}
