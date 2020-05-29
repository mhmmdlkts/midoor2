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
    public Text zeusButtonText, flashButtonText, kitButtonText;
    public Text zeusPriceText, flashPriceText, kitPriceText;
    public int zeusPrice, flashPrice, kitPrice;
    private string dollarPrefix = "$";
    public AudioClip buy_cantAC;
    public AudioClip[] buy_equipAC;
    public Color32 cantBuyColor;
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

    void playEquip()
    {
        GetComponent<AudioSource>().PlayOneShot(buy_equipAC[Random.Range(0,buy_equipAC.Length)]);
    }

    private void checkcanBuyItem()
    {
        if (!checkEnoughMoney(0))
            setCantBuy(0);
        if (game.countOfZeus > 0 || !checkEnoughMoney(1))
            setCantBuy(1);
        if (game.hasCtKit || !checkEnoughMoney(2))
            setCantBuy(2);
    }

    private void setCantBuy(int id)
    {
        switch (id)
        {
            case 0:
                flashButtonText.color = cantBuyColor;
                flashPriceText.color = cantBuyColor;
                break;
            case 1:
                zeusButtonText.color = cantBuyColor;
                zeusPriceText.color = cantBuyColor;
                break;
            case 2:
                kitButtonText.color = cantBuyColor;
                kitPriceText.color = cantBuyColor;
                break;
        }
    }

    public void clickBuyItem(int item)
    {
        if (checkEnoughMoney(item))
        {
            buyItem(item);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(buy_cantAC);
        }
    }

    private void buyItem(int item)
    {
        GetComponent<AudioSource>().PlayOneShot(buy_equipAC[Random.Range(0,buy_equipAC.Length)]);
        money.getMoney(getPrice(item));
        switch (item)
        {
            case 0: 
                game.boughtFlash(); 
                break;
            case 1: 
                game.boughtZeus();
                setCantBuy(1);
                break;
            case 2: 
                game.boughtDefuseKit(); 
                setCantBuy(2);
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
