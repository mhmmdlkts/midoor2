using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenu : MonoBehaviour
{
    public Button btnT, btnCT, btnBoth, btnCancel;
    private InventoryItem inventory;
    private int weaponCode, style;

    public void Start()
    {
        GameObject.Find("ScrollInventory").GetComponent<myInventroy>().isChoseMenuOpen = true;
    }

    public void btnClickT()
    {
        inventory.equipWeapon('T');
        close();
    }

    public void btnClickCT()
    {
        inventory.equipWeapon('C');
        close();
    }

    public void btnClickBoth()
    {
        inventory.equipWeapon('B');
        close();
    }

    public void btnClickCancel()
    {
        close();
    }

    private void close()
    {
        GameObject.Find("ScrollInventory").GetComponent<myInventroy>().isChoseMenuOpen = false;
        Destroy(gameObject);
    }
    
    public void open(InventoryItem inventory, int weaponCode, int style, char team)
    {
        this.inventory = inventory;
        this.weaponCode = weaponCode;
        this.style = style;

        switch (team)
        {
            case 'T':
                btnCT.interactable = false;
                btnBoth.interactable = false;
                break;
            case 'C':
                btnT.interactable = false;
                btnBoth.interactable = false;
                break;
        }
    }
}
