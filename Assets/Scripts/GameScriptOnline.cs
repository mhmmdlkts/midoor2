using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    public void hited(int weaponCode, int weaponStyle, bool isWall, bool isHead, int enemyId, int damage)
    {
        if (!game.isOnline)
            return;
        photonView.RPC(nameof(DeserializeAndExecuteHit), RpcTarget.Others, SerializeHit(weaponCode, weaponStyle, isWall, isHead, enemyId, damage));
        
    }

    public void shot_online_player()
    {
        if (!game.isOnline)
            return;
        photonView.RPC(nameof(getOnlineShot), RpcTarget.Others);
    }
    
    [PunRPC]
    public void getOnlineShot()
    {
        game.getOnlineShot();
    }
    
    private byte[] SerializeHit(int weaponCode, int weaponStyle, bool isWall, bool isHead, int enemyId, int damage)
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
        
        return new byte[] {(byte)weaponCode, (byte)weaponStyle, (isWall ? (byte)1 : (byte)0), (isHead ? (byte)1 : (byte)0), (byte)enemyId, damage_b1, damage_b2, damage_b3};
    }

    [PunRPC]
    private void DeserializeAndExecuteHit(byte[] b)
    {
        int weaponCode = b[0];
        int weaponStyle = b[1];
        bool isWall = b[2] == 1;
        bool isHead = b[3] == 1; 
        int enemyId = b[4];
        int damage = b[5] + b[6] + b[7];
        game.friendGotShot(weaponCode, isWall, isHead, enemyId, damage, weaponStyle);
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

    public void bombPlanted(string enteredPin, int side)
    {
        byte[] pin = Encoding.ASCII.GetBytes(enteredPin);
        byte[] b = new byte[pin.Length+1];

        for (int i = 0; i < b.Length - 1; i++)
        {
            b[i] = pin[i];
        }

        b[b.Length - 1] = (byte)side;
        photonView.RPC(nameof(T_plants_bomb), RpcTarget.Others, b);
    }
    

    [PunRPC]
    private void T_plants_bomb(byte[] b)
    {
        byte[] pin = new byte[b.Length-1];
        for (int i = 0; i < pin.Length; i++)
        {
            pin[i] = b[i];
            Debug.Log(i + ": " + b[i]);
        }

        int side = b[b.Length - 1];
        
        game.bombPlanted(Encoding.ASCII.GetString(pin), side);
    }
    

    [PunRPC]
    private void CT_defused_bomb()
    {
        game.bombDefused();
    }

    public void bombDefused()
    {
        photonView.RPC(nameof(CT_defused_bomb), RpcTarget.Others);
    }

    public void disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    [PunRPC]
    public void openedBomb(byte[] b)
    {
        game.bombOpened(b[0], Convert.ToBoolean(b[1]));
    }
    
    public void openBomb(int side, bool openedT)
    {
        photonView.RPC(nameof(openedBomb), RpcTarget.Others, new byte[] {(byte)side, Convert.ToByte(openedT)});
    }

    public void sendFlash()
    {
        photonView.RPC(nameof(receiveFlash), RpcTarget.Others);
    }

    [PunRPC]
    public void receiveFlash()
    {
        game.explodeFlash();
    }

    public void knifeOther(int weaponId)
    {
        photonView.RPC(nameof(receiveKnife), RpcTarget.Others, new byte[] {(byte)weaponId});
    }

    [PunRPC]
    public void receiveKnife(byte[] b)
    {
        game.getKnifeTry(b[0]);
    }

    public void closeBomb()
    {
        photonView.RPC(nameof(receiveCloseBomb), RpcTarget.Others);
    }

    [PunRPC]
    public void receiveCloseBomb()
    {
        game.otherCloseBomb();
    }

    public void zeusOther()
    {
        photonView.RPC(nameof(receiveZeus), RpcTarget.Others);
    }

    [PunRPC]
    public void receiveZeus()
    {
        game.getZeusTry();
    }

    public void sendEndRoundData(int tScore, int ctScore, bool raundWin)
    {
        photonView.RPC(nameof(receiveEndRound), RpcTarget.Others, new byte[] {(byte)tScore, (byte)ctScore, (byte) (raundWin ? 1 : 0)});
        Debug.Log("Send New Round");
    }

    [PunRPC]
    public void receiveEndRound(byte[] b)
    {
        Debug.Log("Receive New Round");
        game.onlineReceiveEndRound(b[0], b[1], b[2] == 1);
    }
}
