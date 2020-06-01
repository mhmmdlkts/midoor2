using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pp_choser_buttons : MonoBehaviour
{
    private int ppId;
    public Image image;

    public void setId(int id)
    {
        ppId = id;
        image.sprite = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>().ppList[id];
    }

    public void clickListener()
    {
        GameObject.Find("Panel").GetComponent<PlayerStatus>().setPP(ppId);
    }
}
