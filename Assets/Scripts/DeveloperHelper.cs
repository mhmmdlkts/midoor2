using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperHelper : MonoBehaviour
{
    public GameObject shot_button, zoom_button, shot_test;
    // Start is called before the first frame update
    private GameObject or;
    private GameObject dialog;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.GetComponent<ChangeShow>().lookRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<ChangeShow>().lookLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shot_button.GetComponent<ShotButtonHandler>().shot();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            zoom_button.GetComponent<ZoomButtonHandler>().zoom();
        }
        if (Input.GetKeyDown("space"))
        {
            shot_button.GetComponent<ShotButtonHandler>().shot_kill();
        }
        if (Input.GetKeyDown("t"))
        {
            gameObject.GetComponent<GameScript>().timeOut();
        }
        if (Input.GetKeyDown("5"))
        {
            GameObject.Find("longSpawn_dooble_door").GetComponent<ctLongDoorSpawn>().spawnMob();
        }
        if (Input.GetKeyDown("4"))
        {
            GameObject.Find("longSpawn_behind_wall").GetComponent<ctLongWallSpawn>().spawnMob();
        }
        if (Input.GetKeyDown("3"))
        {
            GameObject.Find("ctSpawn").GetComponent<ctMidSpawn>().spawnMob();
        }
        if (Input.GetKeyDown("2"))
        {
            GameObject.Find("bSpawn_behind_box").GetComponent<ctBBoxSpawn>().spawnMob();
        }
        if (Input.GetKeyDown("1"))
        {
            GameObject.Find("bSpawn_behind_wall").GetComponent<bWallSpawn>().spawnMob();
        }
        if (Input.GetKeyDown("p"))
        {
            GameObject.Find("longSpawn_dooble_door").GetComponent<ctLongDoorSpawn>().pick();
            GameObject.Find("longSpawn_behind_wall").GetComponent<ctLongWallSpawn>().pick();
            GameObject.Find("ctSpawn").GetComponent<ctMidSpawn>().pick();
            GameObject.Find("bSpawn_behind_box").GetComponent<ctBBoxSpawn>().pick();
            GameObject.Find("bSpawn_behind_wall").GetComponent<bWallSpawn>().pick();
        }
        if (Input.GetKeyDown("h"))
        {
            GameObject.Find("longSpawn_dooble_door").GetComponent<ctLongDoorSpawn>().hide();
            GameObject.Find("longSpawn_behind_wall").GetComponent<ctLongWallSpawn>().hide();
            GameObject.Find("ctSpawn").GetComponent<ctMidSpawn>().hide();
            GameObject.Find("bSpawn_behind_box").GetComponent<ctBBoxSpawn>().hide();
            GameObject.Find("bSpawn_behind_wall").GetComponent<bWallSpawn>().hide();
        }
        if (Input.GetKeyDown("m"))
        {
            Instantiate(shot_test, shot_test.transform.position, shot_test.transform.rotation);
        }
    }
}
