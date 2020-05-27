using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    private GameObject container;
    private GameScript game;
    private GameMoney money;
    public GameObject zeusButton, flashButton, kitButton;
    public Text zeusPriceText, flashPriceText, kitPriceText;
    public int zeusPrice, flashPrice, kitPrice;
    private string dollarPrefix = "$";
    void Start()
    {
        game = GameObject.Find("MOVABLE").GetComponent<GameScript>();
        money = GameObject.Find("Money").GetComponent<GameMoney>();
        container = GameObject.Find("SafeArea");
        gameObject.transform.SetParent (container.transform, false);
        zeusPriceText.text = dollarPrefix + zeusPrice;
        flashPriceText.text = dollarPrefix + flashPrice;
        kitPriceText.text = dollarPrefix + kitPrice;
        if (game.isT)
            kitButton.SetActive(false);
        checkcanBuyItem();
    }

    private void checkcanBuyItem()
    {
        if (!checkEnoughMoney(0))
            flashButton.GetComponent<Button>().interactable = false;
        if (game.countOfZeus > 0 || !checkEnoughMoney(1))
            zeusButton.GetComponent<Button>().interactable = false;
        if (game.hasCtKit || !checkEnoughMoney(2))
            kitButton.GetComponent<Button>().interactable = false;
    }

    public void clickBuyItem(int item)
    {
        if (checkEnoughMoney(item))
        {
            buyItem(item);
        }
        else
        {
            // TODO not enough money
        }
    }

    private void buyItem(int item)
    {
        money.getMoney(getPrice(item));
        switch (item)
        {
            case 0: 
                game.boughtFlash(); 
                break;
            case 1: 
                game.boughtZeus();
                zeusButton.GetComponent<Button>().interactable = false;
                break;
            case 2: 
                game.boughtDefuseKit(); 
                kitButton.GetComponent<Button>().interactable = false;
                break;
        }

        checkcanBuyItem();
    }

    private int getPrice(int item)
    {
        switch (item)
        {
            case 0: return flashPrice;
            case 1: return zeusPrice;
            case 2: return kitPrice;
        }
        return 0;
    }

    private bool checkEnoughMoney(int item)
    {
        return (money.money >= getPrice(item));
    }

    public void close()
    {
        Destroy(gameObject);
    }
}
