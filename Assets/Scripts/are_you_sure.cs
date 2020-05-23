using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class are_you_sure : MonoBehaviour
{
    public GameObject button_pos, button_neg, text;

    private GameObject parrent;
    // Start is called before the first frame update
    void Start()
    {
        parrent = GameObject.Find("Canvas");
        gameObject.transform.SetParent (parrent.transform, false);
    }

    public void onPositiveButtonClick()
    {
        GameObject.Find("MOVABLE").GetComponent<GameScript>().quitGame();
    }

    public void onNegativeButtonClick()
    {
        Destroy(gameObject);
    }
}
