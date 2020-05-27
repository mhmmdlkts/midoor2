using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonDelay : MonoBehaviour
{
    public float delay;
    public static float alpha = 0.4f; // 0 -> transparent after click; 1 -> opposite
    private GameObject createdMask;
    public GameObject siblingButton;
    // Start is called before the first frame update
    void Start()
    {
        createdMask = new GameObject("Mask_" + gameObject.name);
        
        
        createdMask = Instantiate(gameObject);
        createdMask.transform.SetParent(transform.parent.gameObject.transform, false);
        Destroy(createdMask.GetComponent<Button>());
        Destroy(createdMask.GetComponent<AudioSource>());
        Destroy(createdMask.GetComponent<buttonDelay>());
        Destroy(createdMask.GetComponent<ZoomButtonHandler>());
        foreach (Transform child in createdMask.transform) {
            Destroy(child.gameObject);
        }

        createdMask.transform.parent = transform;
        
        Color color = gameObject.GetComponent<Image>().color;
        createdMask.GetComponent<Image>().color = new Color(color.r, color.g, color.b, alpha);
        gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a - alpha/2);
    }

    public void wait()
    {
        gameObject.GetComponent<Button>().interactable = false;
        StartCoroutine(fillButton());
    }
    
    IEnumerator fillButton()
    {
        for (float t = 0.0f; t <= 1.1f; t += Time.deltaTime / delay)
        {
            gameObject.GetComponent<Image>().fillAmount = t;
            if (t >= 1.0f)
                break;
            yield return null;
        }
        gameObject.GetComponent<Button>().interactable = true;
    }
}
