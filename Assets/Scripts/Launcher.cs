using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Launcher : MonoBehaviourPunCallbacks
{
        
    bool isConnecting;
    string gameVersion = "1";
    public GameObject accept_match_dialog;
    private OnlineMenu onlineMenu;
    public float waitTimeForSearchAgain = 3f;
    [SerializeField] byte maxPlayersPerRoom;
    [SerializeField] private GameObject progressLabel;
    [SerializeField] private GameObject him;
    private static byte END_OF_TEXT = 3;
    private bool isT;
    private bool accepted_you, accepted_him;
    private GameObject created_accept_dialog;
    public static String connectingLabel = "Connecting";
    public static String disconnectingLabel = "Disconnecting";
    public static String searchingLabel = "Waiting for player";
    private long room_time_stamp;
    private bool forceQuit = true;
    private bool isRejoining = false;
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    void Start()
    {
        onlineMenu = gameObject.GetComponent<OnlineMenu>();
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
            setIsConnecting(PhotonNetwork.ConnectUsingSettings());
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            setIsConnecting(false);
            setSearching(true);
        }
    }

    private void setIsConnecting(bool isCon)
    {
        setSearchinActive(isCon);
        isConnecting = isCon;
        if (isCon)
            progressLabel.GetComponent<TextLoading>().setNewText(connectingLabel);
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        leftScene();
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //room_time_stamp = DateTime.Now.ToFileTime();
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public void searchForOtherRooms()
    {
        Debug.Log("Searching");
        rejoinAfter();
        if (PhotonNetwork.CountOfRooms > 1 && PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            Invoke("rejoinAfter", Random.Range(0,10f));
        }
        else
            Invoke("searchForOtherRooms", waitTimeForSearchAgain);

    }

    void rejoinAfter()
    {
        isRejoining = true;
        PhotonNetwork.LeaveLobby();
        Debug.Log("rejoin");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            match_found();
        }
    }

    public void match_found()
    {
        stopInvokes();
        created_accept_dialog = Instantiate(accept_match_dialog);
        sendStatus();
        setSearching(false);
    }

    public void LeaveRoom()
    {
        setSearching(true);
        progressLabel.GetComponent<TextLoading>().setNewText(disconnectingLabel);
        forceQuit = false;
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        if (isRejoining)
        {
            PhotonNetwork.JoinRandomRoom();
            isRejoining = false;
        }
    }

    public override void OnLeftRoom()
    {
        if (!isRejoining)
            leftScene();
        else
        {
            PhotonNetwork.JoinRandomRoom();
            isRejoining = false;
        }
    }
    
    

    public void leftScene()
    {
        if (!forceQuit)
        {
            stopInvokes();
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
            GameObject createdData = GameObject.Find("Data");
            if (createdData != null)
                Destroy(createdData);
        }
    }

    private void stopInvokes()
    {
        CancelInvoke("searchForOtherRooms");
        CancelInvoke("rejoinAfter");
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        match_found();
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

    public void acceptGame()
    {
        accepted_you = true;
        checkIsAccepted();
        photonView.RPC("ReceiveAccepted", RpcTarget.Others, new byte[]{1});
    }

    private void checkIsAccepted()
    {
        if (accepted_you && accepted_him && !PhotonNetwork.IsMasterClient)
        {
            startGame();
        }
    }

    void sendStatus()
    {
        photonView.RPC("ReceiveName", RpcTarget.Others, SeializeString(onlineMenu.name));
        photonView.RPC("ReceiveRank", RpcTarget.Others, SeializeInt(onlineMenu.rank));
        photonView.RPC("ReceiveWins", RpcTarget.Others, SeializeInt(onlineMenu.wins));
        photonView.RPC("receivePlayerNames", RpcTarget.Others, SerializePlayersName(onlineMenu.myTeam));
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
    
    [PunRPC]
    void ReceiveAccepted(byte[] b)
    {
        accepted_him = b[0] == 1;
        checkIsAccepted();
    }

    private void loadGame()
    {
        stopInvokes();
        PhotonNetwork.LoadLevel("Assets/Scenes/Dust2_T_MID.unity");
    }
    
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
        
        setSearching(true);
        Destroy(created_accept_dialog);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public void setSearching(bool isSearching)
    {
        setSearchinActive(isSearching);
        if (isSearching)
        {
            progressLabel.GetComponent<TextLoading>().setNewText(searchingLabel);
            Invoke("searchForOtherRooms",waitTimeForSearchAgain);
        }
    }

    public void setSearchinActive(bool active)
    {
        progressLabel.SetActive(active);
        him.SetActive(!active);
    }

    [PunRPC]
    private void receivePlayerNames(byte[] b)
    {
        onlineMenu.setHisTeam(DeserializePlayersName(b).ToArray());
    }

    private byte[] SerializePlayersName(string[] names)
    { 
        List<Byte> byteList = new List<Byte>();
        
        for (int i = 0; i < names.Length; i++)
        {
            addAllToList(byteList, Encoding.ASCII.GetBytes(names[i]));
        }
        return byteList.ToArray();
    }

    private List <String> DeserializePlayersName(byte [] arr)
    {
        List<String> nameList = new List<String>();
        String name = "";
        for(int i = 0; i < arr.Length; i++)
        {
            byte b = arr[i];
            if (b == END_OF_TEXT)
            {
                nameList.Add(name);
                name = "";
                continue;
            }

            name = String.Concat(name, Convert.ToChar(b));
        }
        return nameList;
    }

    private void addAllToList(List<Byte> list, byte[] arr)
    {
        for(int i = 0; i < arr.Length; i++) {
            list.Add(arr[i]);
        }
        list.Add(END_OF_TEXT);
    }

}
