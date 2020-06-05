using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenu : MonoBehaviour
{
    public Button btnT, btnCT, btnBoth, btnCancel;
    public Text btnT_text, btnCT_text, btnBoth_text, btnCancel_text;
    private InventoryItem inventory;
    private int weaponCode, style;

    public void Start()
    {
        btnBoth_text.text = LanguageSystem.GET_EQUIP_MENU_BUTTON_LABEL_BOTH();
        btnT_text.text = LanguageSystem.GET_EQUIP_MENU_BUTTON_LABEL_T();
        btnCT_text.text = LanguageSystem.GET_EQUIP_MENU_BUTTON_LABEL_CT();
        btnCancel_text.text = LanguageSystem.GET_EQUIP_MENU_BUTTON_LABEL_CANCEL();
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
