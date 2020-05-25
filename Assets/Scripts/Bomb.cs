using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    private string enteredPin;
    public string correctPin;
    public GameObject Pin_text, Led, container, game;
    public float blinkTime;
    public Color32 white, red, green, transparan;

    public int maxPinLength;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("MOVABLE");
        container = GameObject.Find("SafeArea");
        gameObject.transform.SetParent (container.transform, false);
        deletePin();
        blinkOff();
    }

    void blink()
    {
        Led.GetComponent<Image>().color = transparan;
        Invoke("blinkOff", blinkTime);
    }

    void blinkOff()
    {
        Led.GetComponent<Image>().color = white;
    }

    void enter()
    {
        if (isItCorrect() || enteredPin.Equals("*")) // TODO delete '*'
        {
            Led.GetComponent<Image>().color = green;
            Invoke("plantBomb", 1f);
        }
        else
        {
            Led.GetComponent<Image>().color = red;
        }

        Invoke("blinkOff", blinkTime*3);
    }

    void deletePin()
    {
        setPin("");
    }

    public bool isItCorrect()
    {
        return enteredPin.Equals(correctPin);
    }

    void setPin(String pin)
    {
        enteredPin = pin;
        Pin_text.GetComponent<Text>().text = enteredPin;
    }

    void addPin(char nummer)
    {
        blink();
        if (maxPinLength <= enteredPin.Length)
            return;
        setPin(enteredPin + nummer);
    }

    public void btn_1()
    {
        addPin('1');
    }

    public void btn_2()
    {
        addPin('2');
    }

    public void btn_3()
    {
        addPin('3');
    }

    public void btn_4()
    {
        addPin('4');
    }

    public void btn_5()
    {
        addPin('5');
    }

    public void btn_6()
    {
        addPin('6');
    }

    public void btn_7()
    {
        addPin('7');
    }

    public void btn_8()
    {
        addPin('8');
    }

    public void btn_9()
    {
        addPin('9');
    }

    public void btn_0()
    {
        addPin('0');
    }

    public void btn_star()
    {
        addPin('*');
    }

    public void btn_hashtag()
    {
        enter();
        deletePin();
    }

    public void plantBomb()
    {
        game.GetComponent<GameScript>().bombIsOnScreen = false;
        game.GetComponent<GameScript>().bombPlanted();
        Destroy(gameObject);
    }
}
