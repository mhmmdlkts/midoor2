using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCheckMenu : MonoBehaviour
{
    public Text button_closeLabel, titleLabel;
    
    void Start()
    {
        setLabels();
    }

    private void setLabels()
    {
        button_closeLabel.text = LanguageSystem.GET_UPDATE_GAME_MENU_BUTTON_LABEL_CLOSE();
        titleLabel.text = LanguageSystem.GET_UPDATE_GAME_MENU_TITLE();
    }

    public void onCloseButtonClick()
    {
        Destroy(gameObject);
    }
}
