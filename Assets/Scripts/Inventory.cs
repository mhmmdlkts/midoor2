using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Sprite[] items; // 0 -> kit; 1 -> bomb
    
    void Start()
    {
        clear();
    }

    public void clear()
    {
        gameObject.SetActive(false);
    }

    public void set(int id)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Image>().sprite = items[id];
    }
}
