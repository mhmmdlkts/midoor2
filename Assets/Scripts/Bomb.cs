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
    public bool isPlanting, buttonsInteractible;
    public int tryingAt = 0, plantingSide;
    public float waitTimeForDefWithoutKit;
    public float waitTimeForDefWithKit;

    public int maxPinLength;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("MOVABLE");
        container = GameObject.Find("SafeArea");
        gameObject.transform.SetParent (container.transform, false);
        blinkOff();
        deletePin();
        if (!isPlanting)
            refreshDefPass();
    }

    float getDefBlinkWaitTime()
    {
        return game.GetComponent<GameScript>().hasCtKit ? waitTimeForDefWithKit : waitTimeForDefWithoutKit;
    }

    void blink(Color32 color, float blinkTime)
    {
        Led.GetComponent<Image>().color = color;
        Invoke("blinkOff", blinkTime);
        buttonsInteractible = false;
    }

    void blinkOff()
    {
        Led.GetComponent<Image>().color = white;
        buttonsInteractible = true;
    }

    void enter()
    {
        if (isItCorrect())
        {
            blink(green, blinkTime*3);
            Invoke("plantOk", 1f);
        }
        else
        {
            Invoke("deletePin", blinkTime*3);
            Led.GetComponent<Image>().color = red;
        }

        Invoke("blinkOff", blinkTime*3);
    }

    public void setCorrectPin(string pin)
    {
        correctPin = pin;
    }

    public void close()
    {
        game.GetComponent<GameScript>().closeBomb();
        Destroy(gameObject);
    }

    void deletePin()
    {
        setPin("");
    }

    public bool isItCorrect()
    {
        if (isPlanting)
            return true;
        return enteredPin.Equals(correctPin);
    }

    public void addPin(int nummer)
    {
        if (!buttonsInteractible)
            return;
        if (maxPinLength <= enteredPin.Length)
            return;
        if (isPlanting)
        {
            blink(green, blinkTime);
            setPin(enteredPin + nummer);
        }
        else
            tryPin((char)(nummer+'0'));
    }

    public void setPin(String pin)
    {
        enteredPin = pin;
        Pin_text.GetComponent<Text>().text = enteredPin;
    }

    void tryPin(char pin)
    {
        string stars = getStars(1);
        Pin_text.GetComponent<Text>().text = stars + enteredPin + pin;
        buttonsInteractible = false;
        if (correctPin[tryingAt] == pin)
            tryCorrect(pin);
        else
        {
            blink(transparan, getDefBlinkWaitTime());
            Invoke("refreshDefPass", getDefBlinkWaitTime());
        }
    }

    void refreshDefPass()
    {
        buttonsInteractible = true;
        string stars = getStars(0);
        Pin_text.GetComponent<Text>().text = stars + enteredPin;
    }

    void tryCorrect(char pin)
    {
        enteredPin += pin;
        blink(green, getDefBlinkWaitTime());
        tryingAt++;
        if (tryingAt >= correctPin.Length)
            plantOk();
    }

    public string getStars(int minusStars)
    {
        string stars = "";
        for (int i = minusStars; i < correctPin.Length - enteredPin.Length; i++)
            stars += '*';
        return stars;
    }

    public void btn_star()
    {
        deletePin();
    }

    public void btn_hashtag()
    {
        enter();
    }

    public void plantOk()
    {
        if (isPlanting)
            game.GetComponent<GameScript>().bombPlanted(enteredPin, plantingSide);
        else
            game.GetComponent<GameScript>().bombDefused();
        Destroy(gameObject);
    }

    public void forPlanting()
    {
        isPlanting = true;
    }

    public void forDefusing(String settedPin)
    {
        setCorrectPin(settedPin);
        isPlanting = false;
    }
}
