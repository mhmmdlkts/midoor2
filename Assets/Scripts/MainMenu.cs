using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject set_team_dialog;
    public string[] myTeam;
    void Start()
    {
    }

    public void button_ranked()
    {
        SceneManager.LoadScene("Assets/Scenes/Dust2_T_MID.unity", LoadSceneMode.Single);
    }

    public void button_ranked_online()
    {
        if (not_inited_team())
        {
            Instantiate(set_team_dialog, set_team_dialog.transform.position, set_team_dialog.transform.rotation);
            return;
        }
        SceneManager.LoadScene("Assets/Scenes/Online_Ranked.unity", LoadSceneMode.Single);
    }
    
    public void button_edit_team()
    {
        SceneManager.LoadScene("Assets/Scenes/Team Editor.unity", LoadSceneMode.Single);
    }

    private bool not_inited_team()
    {
     
        String name = PlayerPrefs.GetString("name", null);
        if (name == null || name.Equals(""))
            return true;
        for (int i = 1; i < 5; i++)
        {
            name = PlayerPrefs.GetString("game_firend_" + i, null);
            if (name == null || name.Equals(""))
                return true;
        }

        return false;
    }
}
