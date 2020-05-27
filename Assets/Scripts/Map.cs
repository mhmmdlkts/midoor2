using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapChose
{
    plant,
    defus,
    knife
}
public class Map : MonoBehaviour
{
    private GameObject game, container;
    public MapChose mapChose;
    void Start()
    {
        game = GameObject.Find("MOVABLE");
        container = GameObject.Find("SafeArea");
        gameObject.transform.SetParent (container.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void side_picker_buttonListner(int side) // A -> 1 ; B -> 2
    {
        game.GetComponent<GameScript>().mapOK(side, mapChose);
        Destroy(gameObject);
    }

    public void close()
    {
        Destroy(gameObject);
    }
}
