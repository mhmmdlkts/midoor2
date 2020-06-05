using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class are_you_sure : MonoBehaviour
{
    public Text button_posLabel, button_negLabel, titleLabel;

    private GameObject parrent;
    // Start is called before the first frame update
    void Start()
    {
        parrent = GameObject.Find("Canvas");
        setLabels();
        gameObject.transform.SetParent (parrent.transform, false);
    }

    private void setLabels()
    {
        button_posLabel.text = LanguageSystem.GET_EXIT_GAME_MENU_BUTTON_LABEL_YES();
        button_negLabel.text = LanguageSystem.GET_EXIT_GAME_MENU_BUTTON_LABEL_NO();
        titleLabel.text = LanguageSystem.GET_EXIT_GAME_MENU_TITLE();
    }

    public void onPositiveButtonClick()
    {
        GameObject.Find("MOVABLE").GetComponent<GameScript>().gameQuit();
    }

    public void onNegativeButtonClick()
    {
        Destroy(gameObject);
    }
}
