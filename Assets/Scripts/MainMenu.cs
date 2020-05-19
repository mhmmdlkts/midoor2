using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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
        SceneManager.LoadScene("Assets/Scenes/Online_Ranked.unity", LoadSceneMode.Single);
    }
    
    public void button_edit_team()
    {
        SceneManager.LoadScene("Assets/Scenes/Team Editor.unity", LoadSceneMode.Single);
    }
}
