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

    public AudioClip[] menuSounds;
    public Sprite[] ppList;
    public Sprite[] knifesSprite;
    public Sprite[] awpsSprite;
    public Sprite[] zeusSprite;
    public Color32[] qualityColors;
    public static string ArraysDataName = "Arrays";
    public static string[] playerPrafsWeaponKey = {"AWP_inventory5", "Knive_inventory5", "Zeus_inventory5"}; // TODO
    public static string[] playerPrafsWeaponDef = {"0-0=0", "0,1-0=1", "0-0=0"};
    public static string AdManagerGOName = "AdManager";
    void Start()
    {
        createArraysData();
        createAdObject();
    }

    private void createArraysData()
    {
        if (GameObject.Find(ArraysDataName) != null)
            return;
        GameObject sound = new GameObject(ArraysDataName);
        sound.AddComponent<AudioSource>().playOnAwake = false;
        ArraysData arraysData = sound.AddComponent<ArraysData>();
        arraysData.menuSounds = menuSounds;
        arraysData.ppList = ppList;
        arraysData.awpImgs = awpsSprite;
        arraysData.knifeImgs = knifesSprite;
        arraysData.zeusImgs = zeusSprite;
        arraysData.qualityColors = qualityColors;
        Instantiate(sound);
        DontDestroyOnLoad(sound);
    }

    private void createAdObject()
    {
        if (getAdManager() != null)
            return;
        GameObject adManager = new GameObject(AdManagerGOName);
        adManager.AddComponent<MainMenuAd>();
        DontDestroyOnLoad(adManager);
    }

    public static MainMenuAd getAdManager()
    {
        GameObject ad = GameObject.Find(AdManagerGOName);
        if (ad == null)
            return null;
        return ad.GetComponent<MainMenuAd>();
    }

    private void destroyAd()
    {
        MainMenuAd ad = getAdManager();
        if (ad == null)
            return;
        ad.destroyAdds();
    }

    public void button_ranked() // ofline
    {
        destroyAd();
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
    
    public void button_restore_purchase()
    {
        
    }
    
    public void button_more_coins()
    {
        
    }
    
    public void button_remove_ads()
    {
        
    }
    
    public void button_inventory()
    {
        SceneManager.LoadScene("Assets/Scenes/Inventory.unity", LoadSceneMode.Single);
    }

    private bool not_inited_team()
    {
     
        String name = PlayerPrefs.GetString("name", null);
        if (name == null || name.Equals(""))
            return true;
        for (int i = 0; i < 4; i++)
        {
            name = PlayerPrefs.GetString("game_firend_" + i, null);
            if (name == null || name.Equals(""))
                return true;
        }

        return false;
    }

    public void ButtonClickSound(int soundId)
    {
        GameObject soundManeger = GameObject.Find(MainMenu.ArraysDataName);
        soundManeger.GetComponent<AudioSource>().PlayOneShot(soundManeger.GetComponent<ArraysData>().menuSounds[soundId]);
    }
    
}
