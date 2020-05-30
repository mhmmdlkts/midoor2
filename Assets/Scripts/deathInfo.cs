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
    
    public Sprite[] weapons; //awp: 0, ak47: 1, m4a4: 2, zeus: 3, knifeT: 4, knifeCT: 5,
    
    void Start()
    {
        CT_TEXT_COLOR = new Color32(167,180,207, 255);
        T_TEXT_COLOR = new Color32(177,169,122, 255); 
        container = GameObject.Find("kill_info_container");
        gameObject.transform.SetParent (container.transform, false);
        Invoke("destroyDialog",stayTime);
    }

    public void configure(bool ctIsDeath, int weaponCode, bool isHeadShot, bool isWall, String killerName, String killedName)
    {
        Debug.Log("wpCODE:" + weaponCode);
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
        
        killerNameLabel.GetComponent<Text>().color = (ctIsDeath ? T_TEXT_COLOR : CT_TEXT_COLOR);
        killedNameLabel.GetComponent<Text>().color = (ctIsDeath ? CT_TEXT_COLOR : T_TEXT_COLOR);
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
    }
}
