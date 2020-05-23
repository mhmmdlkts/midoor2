using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Set_Team_dialog : MonoBehaviour
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
        SceneManager.LoadScene("Assets/Scenes/Team Editor.unity", LoadSceneMode.Single);
    }

    public void onNegativeButtonClick()
    {
        Destroy(gameObject);
    }
}