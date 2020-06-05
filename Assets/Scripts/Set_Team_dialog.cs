using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Set_Team_dialog : MonoBehaviour
{
    public Text button_posLabel, button_negLabel, titleLabel;

    private GameObject parrent;
    // Start is called before the first frame update
    void Start()
    {
        parrent = GameObject.Find("Canvas");
        gameObject.transform.SetParent (parrent.transform, false);

        button_negLabel.text = LanguageSystem.GET_SET_TEAM_NAMES_MENU_BUTTON_LABEL_CLOSE();
        button_posLabel.text = LanguageSystem.GET_SET_TEAM_NAMES_MENU_BUTTON_LABEL_SET_TEAM();
        titleLabel.text = LanguageSystem.GET_SET_TEAM_NAMES_MENU_TITLE();
    }

    public void onPositiveButtonClick()
    {
        SceneManager.LoadScene("Assets/Scenes/Team Editor.unity", LoadSceneMode.Single);
    }

    public void onNegativeButtonClick()
    {
        Destroy(gameObject);
    }
}