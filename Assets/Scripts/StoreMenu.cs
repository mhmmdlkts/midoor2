using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    public GameObject storeItemPrefab, container;
    public InventoryMenu inventoryMenu;
    
    void Start()
    {
        
        initStoreItems();
        float containerHeight = storeItemPrefab.GetComponent<RectTransform>().rect.height * inventoryMenu.storeItemStruct.Count-1;
        RectTransform rc = container.GetComponent<RectTransform>();
        rc.sizeDelta = new Vector2(0, containerHeight + 10);
    }

    private void initStoreItems()
    {
        for (int i = 0; i < inventoryMenu.storeItemStruct.Count; i++)
            Instantiate(storeItemPrefab, container.transform, false).GetComponent<StoreItem>().configure(inventoryMenu.storeItemStruct[i]);
    }
}
