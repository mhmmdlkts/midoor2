using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Cancel_Search_Button : MonoBehaviour
{
    public static string cancelText = "CANCEL SEARCH";
    public static string startText = "START SEARCH";

    public Color32 CancelButtonColor;
    public Color32 StartButtonColor;

    public Color32 CancelLabelColor;
    public Color32 StartLabelColor;

    public GameObject textObj;

    [SerializeField]
    public bool isGreen;
    // Start is called before the first frame update
    void Start()
    {
        setToGreen();
    }

    public void setToGreen()
    {
        GetComponent<Image>().color = StartButtonColor;
        textObj.GetComponent<Text>().color = StartLabelColor;
        textObj.GetComponent<Text>().text = startText;
        isGreen = true;
    }

    public void setToRed()
    {
        isGreen = false;
        GetComponent<Image>().color = CancelButtonColor;
        textObj.GetComponent<Text>().color = CancelLabelColor;
        textObj.GetComponent<Text>().text = cancelText;
    }
}
