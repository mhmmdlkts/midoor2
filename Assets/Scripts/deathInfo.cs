using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathInfo : MonoBehaviour
{
    private GameObject container;

    public GameObject killerNameLabel, killedNameLabel, weopenImg, headImg, wallImg;
    public float stayTime;

    private Color32 CT_TEXT_COLOR, T_TEXT_COLOR;

    public static int AWP_CODE   = 0;
    public static int AK47_CODE  = 1;
    public static int M4A4_CODE  = 2;
    public static int ZEUS_CODE  = 3;
    public static int KNIFE_CODE = 4;
    
    public Sprite[] weapons;

    public static int getKnifesGlobalCode(int localKnifeCode)
    {
        return localKnifeCode + KNIFE_CODE;
    }
    void Start()
    {
        CT_TEXT_COLOR = new Color32(167,180,207, 255);
        T_TEXT_COLOR = new Color32(177,169,122, 255); 
        container = GameObject.Find("kill_info_container");
        gameObject.transform.SetParent (container.transform, false);
        Invoke(nameof(destroyDialog),stayTime);
    }

    public void configure(bool ctIsDeath, int weaponCode, bool isHeadShot, bool isWall, String killerName, String killedName)
    {
        if (PlayerPrefs.GetString("name", LanguageSystem.GET_NAME()).Equals(killerName))
        {
            // you killed
            GetComponent<Image>().color = new Color32(142, 42, 52, 64);
        }
        if (PlayerPrefs.GetString("name", LanguageSystem.GET_NAME()).Equals(killedName))
        {
            // you death
            GetComponent<Image>().color = new Color32(200, 62, 75, 128);
        }
        killerNameLabel.GetComponent<Text>().text = killerName + "  ";
        killedNameLabel.GetComponent<Text>().text = "  " + killedName;
        headImg.SetActive(isHeadShot);
        wallImg.SetActive(isWall);
        weopenImg.GetComponent<Image>().sprite = weapons[weaponCode];
        StartCoroutine(AdjustTransInTheEndOfFrame(ctIsDeath));
    }

    // Update is called once per frame
    private void destroyDialog()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator AdjustTransInTheEndOfFrame(bool ctIsDeath) 
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        
        killerNameLabel.GetComponent<Text>().color = (ctIsDeath ? T_TEXT_COLOR : CT_TEXT_COLOR);
        killedNameLabel.GetComponent<Text>().color = (ctIsDeath ? CT_TEXT_COLOR : T_TEXT_COLOR);
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
    }
}
