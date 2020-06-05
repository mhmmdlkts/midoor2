using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMoney : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    public int money;
    public int FIRST_MONEY, ROUNDWIN_MONEY, ROUNDLOSE_MONEY, BOMBPLANT_MONEY, BOMBDEFUSE_MONEY, KNIFE_MONEY;
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void setMoney(int money)
    {
        this.money = money;
        refreshUI();
    }

    private void refreshUI()
    {
        text.text = LanguageSystem.GET_CURRENCY() + money;
    }

    public void addMoney(int money)
    {
        this.money += money;
        refreshUI();
    }

    public void getMoney(int money)
    {
        this.money -= money;
        refreshUI();
    }

    public bool hasMoney(int money)
    {
        return money <= this.money;
    }
}
