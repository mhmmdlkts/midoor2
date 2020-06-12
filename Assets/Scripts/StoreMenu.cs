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
        int count = initStoreItems();
        float containerHeight = storeItemPrefab.GetComponent<RectTransform>().rect.height * count;
        RectTransform rc = container.GetComponent<RectTransform>();
        rc.sizeDelta = new Vector2(0, containerHeight + 10);
    }

    private int initStoreItems()
    {
        int count = 0;
        for (int i = 0; i < inventoryMenu.storeItemStruct.Count; i++)
        {
            if (Instantiate(storeItemPrefab, container.transform, false).GetComponent<StoreItem>()
                .configure(inventoryMenu.storeItemStruct[i]))
                count++;
        }

        return count;
    }
}
