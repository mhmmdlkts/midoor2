using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameScriptOnline : MonoBehaviourPunCallbacks
{
    public Queue<Online_EX> onlineExes = new Queue<Online_EX>();
    public GameScript game;
    // Start is called before the first frame update
    void Start()
    {
        game = gameObject.GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Online_EX getNewOnlineEx()
    {
        Online_EX onlineEx = new Online_EX();
        onlineExes.Enqueue(onlineEx);
        return onlineEx;
    }

    public void teamFriendDamage(int weaponCode, bool isWall, bool isHead, int enemyId, int damage)
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

    public bool isOnline()
    {
        return gameObject.GetComponent<GameScript>().isOnline;
    }
}
