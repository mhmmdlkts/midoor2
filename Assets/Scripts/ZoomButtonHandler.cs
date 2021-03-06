using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButtonHandler : MonoBehaviour
{
    public Camera cam1, cam2;
    public SpriteRenderer scope1;
    public AudioSource zoomSound;
    public Canvas canvas;
    private bool isZoom;
    public float zoomConstant;

    void Start()
    {
        zoomSound = gameObject.GetComponent<AudioSource>();
        isZoom = false;
        zoomConstant = 0.235355f;
    }
    
    public void zoom()
    {
        if (GameScript.isStoped)
            return;
        isZoom = !isZoom;
        if (isZoom)
        {
            scope1.transform.localScale = new Vector3(zoomConstant, zoomConstant, 1.0f);
            //cam1.orthographicSize = 2.554375f;
        } else
		{
            scope1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //cam1.orthographicSize = 10.6338f;
        }
        setCamZoom();
    }

    private void setCamZoom()
    {
        zoomSound.Play();
        canvas.worldCamera = isZoom?cam2:cam1;
        cam1.enabled = !isZoom;
        cam2.enabled = isZoom;
    }
}
