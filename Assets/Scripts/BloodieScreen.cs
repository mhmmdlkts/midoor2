using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodieScreen : MonoBehaviour
{
    public GameObject bloodie;

    public void showBlood()
    {
        bloodie.SetActive(true);
        Invoke("hideBlood",1f);
    }

    public void hideBlood()
    {
        bloodie.SetActive(false);
    }
}
