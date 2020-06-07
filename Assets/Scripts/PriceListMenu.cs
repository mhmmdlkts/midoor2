using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceListMenu : MonoBehaviour
{
    private static string _2k = "2.000";
    private static string _5k = "5.000";
    private static string _10k = "10.000";
    private static string _25k = "25.000";
    private static string _50k = "50.000";
    private static string _100k = "100.000";
    public Text _2kProductLabel,
        _5kProductLabel,
        _10kProductLabel,
        _25kProductLabel,
        _50kProductLabel,
        _100kProductLabel,
        _bestSellerLabel,
        _closeLabel; 
    void Start()
    {
        setProductsLabel();
    }

    private void setProductsLabel()
    {
        _2kProductLabel.text = _2k + " " + LanguageSystem.GET_PRICE_LIST_MENU_COIN();
        _5kProductLabel.text = _5k + " " + LanguageSystem.GET_PRICE_LIST_MENU_COIN();
        _10kProductLabel.text = _10k + " " + LanguageSystem.GET_PRICE_LIST_MENU_COIN();
        _25kProductLabel.text = _25k + " " + LanguageSystem.GET_PRICE_LIST_MENU_COIN();
        _50kProductLabel.text = _50k + " " + LanguageSystem.GET_PRICE_LIST_MENU_COIN();
        _100kProductLabel.text = _100k + " " + LanguageSystem.GET_PRICE_LIST_MENU_COIN();
        _bestSellerLabel.text = LanguageSystem.GET_PRICE_LIST_MENU_BEST_SELLER();
        _closeLabel.text = LanguageSystem.GET_PRICE_LIST_MENU_CLOSE();
    }

    public void close()
    {
        Destroy(gameObject);
    }
}
