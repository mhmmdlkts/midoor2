using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watchAddMenu : MonoBehaviour
{
    
    public void onPositiveButtonClick()
    {
        GameObject.Find("Main Camera").GetComponent<OnlineMenu>().watchRewardAd();
        Destroy(gameObject);
    }

    public void onNegativeButtonClick()
    {
        Destroy(gameObject);
    }
}
