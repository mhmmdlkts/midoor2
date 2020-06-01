using UnityEngine;

public class PP_Beavior : MonoBehaviour
{
    public GameObject ppPrefab, container;

    public void showPPs()
    {
        ArraysData arraysData = GameObject.Find(MainMenu.ArraysDataName).GetComponent<ArraysData>();
        float scrollContainerLegth = arraysData.ppList.Length * ppPrefab.GetComponent<RectTransform>().rect.width;
        Debug.Log(scrollContainerLegth);
        RectTransform rc = container.GetComponent<RectTransform>();
        rc.sizeDelta = new Vector2(scrollContainerLegth, rc.rect.y);
        for (int i = 0; i < arraysData.ppList.Length; i++)
        {
            Instantiate(ppPrefab, container.transform, false).GetComponent<Pp_choser_buttons>().setId(i);
        }
    }
}
