using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class GameScriptOnline : MonoBehaviourPunCallbacks
{
    public Queue<Online_EX> onlineExes = new Queue<Online_EX>();
    public GameScript game;
    // Start is called before the first frame update
    void Start()
    {
        game = gameObject.GetComponent<GameScript>();
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
    }
    
    public Online_EX getNewOnlineEx()
    {
        Online_EX onlineEx = new Online_EX();
        onlineExes.Enqueue(onlineEx);
        return onlineEx;
    }

    public void hited(int weaponCode, bool isWall, bool isHead, int enemyId, int damage)
    {
        if (!game.isOnline)
            return;
        photonView.RPC("DeserializeAndExecuteHit", RpcTarget.Others, SerializeHit(weaponCode,isWall,isHead,enemyId,damage));
        
    }

    public void shot_online_player()
    {
        if (!game.isOnline)
            return;
        photonView.RPC("getOnlineShot", RpcTarget.Others);
    }
    
    [PunRPC]
    public void getOnlineShot()
    {
        game.getOnlineShot();
    }
    
    private byte[] SerializeHit(int weaponCode, bool isWall, bool isHead, int enemyId, int damage)
    {
        byte max_byte = Byte.MaxValue;

        byte damage_b1, damage_b2, damage_b3;
        
        if (damage >= max_byte) {
            damage_b1 = max_byte;
            damage -= max_byte;
        } else {
            damage_b1 = (byte)damage;
            damage = 0;
        }
        
        if (damage >= max_byte) {
            damage_b2 = max_byte;
            damage -= max_byte;
        } else {
            damage_b2 = (byte)damage;
            damage = 0;
        }
        
        if (damage >= max_byte) {
            damage_b3 = max_byte;
        } else {
            damage_b3 = (byte)damage;
        }
        
        return new byte[] {(byte)weaponCode, (isWall ? (byte)1 : (byte)0), (isHead ? (byte)1 : (byte)0), (byte)enemyId, damage_b1, damage_b2, damage_b3};
    }

    [PunRPC]
    private void DeserializeAndExecuteHit(byte[] b)
    {
        int weaponCode = b[0];
        bool isWall = b[1] == 1;
        bool isHead = b[2] == 1; 
        int enemyId = b[3];
        int damage = b[4] + b[5] + b[6];
        game.friendGotShot(weaponCode, isWall, isHead, enemyId, damage);
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        
        game.otherLeavs();
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public bool isOnline()
    {
        return gameObject.GetComponent<GameScript>().isOnline;
    }

    public void bombPlanted(string enteredPin)
    {
        photonView.RPC("T_plants_bomb", RpcTarget.Others, Encoding.ASCII.GetBytes(enteredPin));
    }
    

    [PunRPC]
    private void T_plants_bomb(byte[] pin)
    {
        game.bombPlanted(Encoding.ASCII.GetString(pin));
    }
    

    [PunRPC]
    private void CT_defused_bomb()
    {
        game.bombDefused();
    }

    public void bombDefused()
    {
        photonView.RPC("CT_defused_bomb", RpcTarget.Others);
    }

    public void disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    [PunRPC]
    public void openedBomb()
    {
        game.bombOpened();
    }
    
    public void openBomb()
    {
        photonView.RPC("openedBomb", RpcTarget.Others);
    }

    public void sendFlash()
    {
        photonView.RPC("receiveFlash", RpcTarget.Others);
    }

    [PunRPC]
    public void receiveFlash()
    {
        game.explodeFlash();
    }
}
