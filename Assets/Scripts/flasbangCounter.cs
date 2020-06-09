using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flasbangCounter : MonoBehaviour
{
    public GameObject panel, countText;
    private int count;

    public void setShow(bool show)
    {
        panel.SetActive(count > 0 && show);
        countText.GetComponent<Text>().text = "" + count;
    }

    public void setCount(int count)
    {
        this.count = count;
            if (panel.activeSelf)
                setShow(true);
    }
}
