using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoading : MonoBehaviour
{
    public int countOfMaxDots = 3;
    public float waitTime;
    private Text textLabel;

    private float timer;

    private int countOfCurrentDots;

    private void Start()
    {
        textLabel = gameObject.GetComponent<Text>();
    }

    public void setNewText(string text)
    {
        countOfCurrentDots = 0;
        textLabel.text = text;
        textLabel.text += getDots_and_Spaces(0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > waitTime)
        {
            nextDot();
            timer = 0;
        }
    }

    void nextDot()
    {
        string text = getText();
        countOfCurrentDots = (countOfCurrentDots+1) % (countOfMaxDots+1);
        textLabel.text= text + getDots_and_Spaces(countOfCurrentDots);
    }

    string getDots_and_Spaces(int countOfDots)
    {
        string dots_n_spaces = "";
        for (int i = 0; i < countOfMaxDots; i++)
            dots_n_spaces += i < countOfDots ? "." : " ";
        return dots_n_spaces;
    }

    string getText()
    {
        int countDots = 0;
        string text = textLabel.text;
        for (int i = text.Length - 1; text.Length - 1 - countOfMaxDots < i; i--)
            if (text[i] == '.' || text[i] == ' ')
                countDots++;
        return text.Substring(0, text.Length - countDots);
    }
}
