﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GetRandomEnemyName : MonoBehaviour
{
    public TextAsset botListDat;

    private List<int> usedIndexes;
    private int tot_names;

    private Random rnd;

    public void initNames()
    {
        rnd = new Random();
        usedIndexes = new List<int>();
        tot_names = botListDat.text.Split('\n').Length;
    }

    public String getRandomName()
    {
        int index;
        do { index = rnd.Next(0,tot_names); }
        while (usedIndexes.Contains(index));
        usedIndexes.Add(index);
        return getAt(index);
    }

    private String getAt(int index)
    {
        if (tot_names <= index)
        {
            Debug.LogError("Out of Bound -GetRandomEnemyName.cs-");
            return "NULL";
        }
        return botListDat.text.Split('\n')[index];
    }
}
