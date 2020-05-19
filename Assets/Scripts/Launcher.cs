using System;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
        
    bool isConnecting;
    string gameVersion = "1";
    private OnlineMenu onlineMenu;
    [SerializeField] byte maxPlayersPerRoom;
    [SerializeField] private GameObject progressLabel;
    [SerializeField] private GameObject him;
    
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    void Start()
    {
        onlineMenu = gameObject.GetComponent<OnlineMenu>();
        setSearching(true);
        Connect();
    }
    
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        setSearching(true);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {

            setSearching(false);
            //PhotonNetwork.LoadLevel("Room for 1");
        }
        sendStatus();
    }

    public void LeaveRoom()
    {
        if(PhotonNetwork.IsConnected)
            PhotonNetwork.LeaveRoom();
    }
    
    public override void OnPlayerEnteredRoom(Player other)
    {
        setSearching(false);
        sendStatus();
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            //PhotonNetwork.LoadLevel("Room for 1");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    void sendStatus()
    {
        photonView.RPC("ReceiveName", RpcTarget.Others, SeializeString(onlineMenu.name));
        photonView.RPC("ReceiveRank", RpcTarget.Others, SeializeInt(onlineMenu.rank));
        photonView.RPC("ReceiveWins", RpcTarget.Others, SeializeInt(onlineMenu.wins));
        //photonView.RPC("ReceivePP", RpcTarget.Others, SeializeString(onlineMenu.name_me.GetComponent<Text>().text));
    }

    private byte[] SeializeString(string s)
    {
        return Encoding.ASCII.GetBytes(s);
    }

    private string DeseializeString(byte[] b)
    {
        return Encoding.ASCII.GetString(b);
    }

    private byte[] SeializeInt(int i)
    {
        return BitConverter.GetBytes(i);
    }

    private int DeseializeInt(byte[] b)
    {
        return BitConverter.ToInt32(b,0);
    }
    
    [PunRPC]
    void ReceiveName(byte[] b)
    {
        onlineMenu.setHisName(DeseializeString(b));
    }
    
    [PunRPC]
    void ReceiveRank(byte[] b)
    {
        onlineMenu.setHisRank(DeseializeInt(b));
    }
    
    [PunRPC]
    void ReceiveWins(byte[] b)
    {
        onlineMenu.setHisWins(DeseializeInt(b));
    }
    
    [PunRPC]
    void ReceivePP(byte[] b)
    {
        
    }
    
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
        
        setSearching(true);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public void setSearching(bool isSearching)
    {
        progressLabel.SetActive(isSearching);
        him.SetActive(!isSearching);
    }

}
