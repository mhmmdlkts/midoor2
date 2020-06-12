using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class myInventroy : MonoBehaviour
{
    public GameObject inventoryItemPrefab, container;
    public InventoryMenu inventoryMenu;
    private string inventoryKnifeCode, inventoryAwpCode, inventoryZeusCode;
    private string[] inventoryKnifes, inventoryAwps, inventoryZeus;
    private int inventoryEquipedKnifeIdT, inventoryEquipedAwpIdT, inventoryEquipedZeusIdT;
    private int inventoryEquipedKnifeIdCT, inventoryEquipedAwpIdCT, inventoryEquipedZeusIdCT;
    public bool isChoseMenuOpen;
    private List<GameObject> itemsList;
    // {"0-0=0", "0,1-0=1"}
    void Start()
    {
        init();
    }

    private void desrtoyAll()
    {
        if (itemsList == null)
            return;
        foreach (var item in itemsList)
        {
            Destroy(item);
        }
    }

    public IEnumerator reload()
    {
        yield return new WaitForEndOfFrame();
        desrtoyAll();
        init();
    }

    private void init()
    {
        itemsList = new List<GameObject>();
        initPlayerPrefabs();
        initAwpItems();
        initKnifeItems();
        initZeusItems();
        registerAll();
        setContainerHeight();
    }

    private void setContainerHeight()
    {
        int tot_items = inventoryAwps.Length + inventoryZeus.Length + inventoryKnifes.Length;
        RectTransform rc = container.GetComponent<RectTransform>();
        float itemHeight = container.GetComponent<GridLayoutGroup>().cellSize.y;
        float itemWidth = inventoryItemPrefab.GetComponent<RectTransform>().rect.width;
        float firstContainerWidth = GameObject.Find("ScrollInventory").GetComponent<RectTransform>().rect.width;
        float firstContainerHeight = GameObject.Find("ScrollInventory").GetComponent<RectTransform>().rect.height;
        int mod1 = (int)Math.Ceiling(firstContainerWidth) / (int)Math.Ceiling(itemWidth);
        int mod2 = (int)Math.Ceiling(Math.Ceiling((float)tot_items) / mod1);
        container.GetComponent<GridLayoutGroup>().cellSize = new Vector2(firstContainerWidth / mod1, itemHeight);
        float containerHeight = (float)mod2 * itemHeight;
        rc.sizeDelta = new Vector2(0, containerHeight - firstContainerHeight);
    }

    private void initPlayerPrefabs()
    {
        inventoryAwpCode = PlayerPrefs.GetString(MainMenu.playerPrafsWeaponKey[0], MainMenu.playerPrafsWeaponDef[0]);
        inventoryKnifeCode = PlayerPrefs.GetString(MainMenu.playerPrafsWeaponKey[1], MainMenu.playerPrafsWeaponDef[1]);
        inventoryZeusCode = PlayerPrefs.GetString(MainMenu.playerPrafsWeaponKey[2], MainMenu.playerPrafsWeaponDef[2]);
        inventoryKnifes = inventoryKnifeCode.Split('-')[0].Split(',');
        inventoryAwps = inventoryAwpCode.Split('-')[0].Split(',');
        inventoryZeus = inventoryZeusCode.Split('-')[0].Split(',');
        inventoryEquipedKnifeIdT = Convert.ToInt32(inventoryKnifeCode.Split('-')[1].Split('=')[0]);
        inventoryEquipedAwpIdT = Convert.ToInt32(inventoryAwpCode.Split('-')[1].Split('=')[0]);
        inventoryEquipedZeusIdT = Convert.ToInt32(inventoryZeusCode.Split('-')[1].Split('=')[0]);
        inventoryEquipedKnifeIdCT = Convert.ToInt32(inventoryKnifeCode.Split('-')[1].Split('=')[1]);
        inventoryEquipedAwpIdCT = Convert.ToInt32(inventoryAwpCode.Split('-')[1].Split('=')[1]);
        inventoryEquipedZeusIdCT = Convert.ToInt32(inventoryZeusCode.Split('-')[1].Split('=')[1]);
    }
    private void initAwpItems()
    {
        for (int i = 0; i < inventoryAwps.Length; i++)
        {
            GameObject o = Instantiate(inventoryItemPrefab, container.transform, false);
            int style = Convert.ToInt32(inventoryAwps[i]);
            bool eqT = inventoryEquipedAwpIdT == style;
            bool eqCT = inventoryEquipedAwpIdCT == style;
            o.GetComponent<InventoryItem>().configure(InventoryMenu.getStruct(inventoryMenu.storeList,0, style),getTeam(eqT,eqCT));
            itemsList.Add(o);
        }
    }

    private void initKnifeItems()
    {
        for (int i = 0; i < inventoryKnifes.Length; i++)
        {
            GameObject o = Instantiate(inventoryItemPrefab, container.transform, false);
            int style = Convert.ToInt32(inventoryKnifes[i]);
            bool eqT = inventoryEquipedKnifeIdT == style;
            bool eqCT = inventoryEquipedKnifeIdCT == style;
            o.GetComponent<InventoryItem>().configure(InventoryMenu.getStruct(inventoryMenu.storeList,1, style),getTeam(eqT,eqCT));
            itemsList.Add(o);
        }
    }

    private void initZeusItems()
    {
        for (int i = 0; i < inventoryZeus.Length; i++)
        {
            GameObject o = Instantiate(inventoryItemPrefab, container.transform, false);
            int style = Convert.ToInt32(inventoryZeus[i]);
            bool eqT = inventoryEquipedZeusIdT == style;
            bool eqCT = inventoryEquipedZeusIdCT == style;
            o.GetComponent<InventoryItem>().configure(InventoryMenu.getStruct(inventoryMenu.storeList,2, style),getTeam(eqT,eqCT));
            itemsList.Add(o);
        }
    }

    private char getTeam(bool eqT, bool eqCT)
    {
        if (eqT && eqCT)
            return 'B';
        if (eqT)
            return 'T';
        if (eqCT)
            return 'C';
        return '-';
    }

    private void registerAll()
    {
        foreach (var o in itemsList)
        {
            o.GetComponent<InventoryItem>().registerObserver();
        }
    }
}
