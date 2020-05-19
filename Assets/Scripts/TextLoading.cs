using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoading : MonoBehaviour
{
    public int countOfMaxDots = 3;
    public float waitTime = 3f;

    private float timer;

    private int countOfCurrentDots;

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
        string text = gameObject.GetComponent<Text>().text;
        text = text.Substring(0, text.Length - countOfCurrentDots );
        countOfCurrentDots = (countOfCurrentDots+1) % (countOfMaxDots+1);
        for (int i = 0; i < countOfCurrentDots; i++)
            text += ".";
        gameObject.GetComponent<Text>().text = text;
    }
}
