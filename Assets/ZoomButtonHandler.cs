using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButtonHandler : MonoBehaviour
{
    public Camera cam1;
    public SpriteRenderer scope1;
    private bool isZoom;
    public float zoomConstant = 0.235355f;

    public void zoom()
    {
        if (isZoom)
        {
            scope1.transform.localScale = new Vector3(zoomConstant, zoomConstant, 1.0f);
            cam1.orthographicSize = 2.554375f;
        } else
		{
            scope1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            cam1.orthographicSize = 10.75888f;
        }
        isZoom = !isZoom;
    }
}
