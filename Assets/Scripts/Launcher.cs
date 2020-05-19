using System;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class Launcher : MonoBehaviourPunCallbacks
{
        
    bool isConnecting;
    string gameVersion = "1";
    private OnlineMenu onlineMenu;
    [SerializeField] byte maxPlayersPerRoom;
    [SerializeField] private GameObject progressLabel;
    [SerializeField] private GameObject him;
    private bool isT;
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
        

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public void startGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            isT = Random.Range(0,2) == 1;
            photonView.RPC("ReceiveTeam", RpcTarget.All, SeializeBool(!isT));
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
    
    [Serializable]
    public class SerializeTexture
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;
        [SerializeField]
        public byte[] bytes;
    }

    private byte[] SeializeSprite(Sprite sprite)
    {
        return sprite.texture.GetRawTextureData();
    }

    private Sprite DeseializeSprite(byte[] b)
    {
        Texture2D a = null;//Texture2D.LoadImage();
        Sprite sp = Sprite.Create(a, new Rect(0.0f, 0.0f, 100f, 100f), new Vector2(0.5f, 0.5f), 100.0f);
        return sp;
    }

    private byte[] SeializeBool(bool logic)
    {
        return BitConverter.GetBytes(logic);
    }

    private bool DeseializeBool(byte[] b)
    {
        return BitConverter.ToBoolean(b,0);
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
    void ReceiveTeam(byte[] b)
    {
        isT = DeseializeBool(b);
        onlineMenu.setMyTeam(PhotonNetwork.IsMasterClient? !isT : isT);
        loadGame();
    }
    
    [PunRPC]
    void ReceivePP(byte[] b)
    {
        onlineMenu.setHisPP(DeseializeSprite(b));
    }

    private void loadGame()
    {
        PhotonNetwork.LoadLevel("Assets/Scenes/Dust2_T_MID.unity");
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
