using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorTag : MonoBehaviour
{
    
    
    public byte alpha;

    public Color32 yellow, orange, green, purple, blue;

    public Sprite frame_img;

    public void configure(int colorId, GameObject container)
    {
        Debug.Log("color id: " + colorId);
        gameObject.transform.SetParent (container.transform, false);
        switch (colorId)
        {
            case 0:
                gameObject.GetComponent<Image>().color = new Color32(yellow.r, yellow.g, yellow.b, alpha);
                setFrame(container);
                break;
            case 1:
                gameObject.GetComponent<Image>().color = new Color32(orange.r, orange.g, orange.b, alpha);
                break;
            case 2:
                gameObject.GetComponent<Image>().color = new Color32(green.r, green.g, green.b, alpha);
                break;
            case 3:
                gameObject.GetComponent<Image>().color = new Color32(purple.r, purple.g, purple.b, alpha);
                break;
            case 4:
                gameObject.GetComponent<Image>().color = new Color32(blue.r, blue.g, blue.b, alpha);
                break;
        }
    }

    private void setFrame(GameObject container)
    {
        GameObject frame = new GameObject("Frame");
        frame.transform.SetParent(container.transform, false);
        frame.AddComponent<CanvasRenderer>();
        RectTransform rc = frame.AddComponent<RectTransform>();
        Image i = frame.AddComponent<Image>();
            
        rc.anchorMin = new Vector2(0, 0);
        rc.anchorMax = new Vector2(1, 1);
        rc.offsetMin = new Vector2(0, 0);
        rc.offsetMax = new Vector2(0, 0);
        
        rc.localPosition = new Vector3(0,0,0);
        i.sprite = frame_img;
    }
}