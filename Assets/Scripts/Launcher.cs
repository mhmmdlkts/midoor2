﻿using System;
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
    private long room_time_stamp;
    private bool forceQuit = true;
    private bool isRejoining = false;
    private bool isLeftingScene = false;
    
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        resetAccepts();
    }

    void OnDisable()
    {
        base.OnDisable();
        if (!isLeftingScene)
            Disconnect();
    }

    public void resetAccepts()
    {
        accepted_you = false;
        accepted_him = false;
    }
    
    void Start()
    {
        onlineMenu = gameObject.GetComponent<OnlineMenu>();
        Disconnect();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        isT = generateTeamSide();
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
            progressLabel.GetComponent<TextLoading>().setNewText(LanguageSystem.GET_IS_CONNECTING_LABEL());
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        if (!forceQuit)
        {
            GetComponent<OnlineMenu>().setButtonSearch(false);
            if (isLeftingScene)
                leftScene();
            return;
        }

        GetComponent<OnlineMenu>().scs.setToGreen();
        progressLabel.SetActive(false);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //room_time_stamp = DateTime.Now.ToFileTime();
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public void searchForOtherRooms()
    {
        if (PhotonNetwork.CountOfRooms > 1 && PhotonNetwork.CurrentRoom.PlayerCount < 2) // TODO hic calismiyor
        {
            Invoke(nameof(rejoinAfter), Random.Range(0,10f));
        }
        else
            Invoke(nameof(searchForOtherRooms), waitTimeForSearchAgain);

    }

    void rejoinAfter()
    {
        isRejoining = true;
        PhotonNetwork.LeaveLobby();
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
        forceQuit = false;
        isLeftingScene = true;
        if (PhotonNetwork.IsConnected)
            Disconnect();
        else
        {
            DestroyData();
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
        }

    }

    public void Disconnect()
    {
        him.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            progressLabel.GetComponent<TextLoading>().setNewText(LanguageSystem.GET_IS_DISCONNECTING_LABEL());
            PhotonNetwork.Disconnect();
        }
        else
        {
            progressLabel.SetActive(false);
            if (isLeftingScene)
                leftScene();
        }
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
        resetAccepts();
        if (!isRejoining)
        {
            if (!forceQuit)
            {
                Disconnect();
            }
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
            isRejoining = false;
        }
    }
    
    

    public void leftScene()
    {
        DestroyData();
        if (!forceQuit)
        {
            stopInvokes();
            SceneManager.LoadScene("Assets/Scenes/Main Menu.unity", LoadSceneMode.Single);
        }
    }

    private void DestroyData()
    {
        GameObject createdData = GameObject.Find("Data");
        if (createdData != null)
            Destroy(createdData);
    }

    private void stopInvokes()
    {
        CancelInvoke(nameof(searchForOtherRooms));
        CancelInvoke(nameof(rejoinAfter));
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

    public void acceptGame()
    {
        accepted_you = true;
        if (checkIsAccepted())
            return;
        photonView.RPC(nameof(ReceiveAccepted), RpcTarget.Others);
    }

    private bool checkIsAccepted()
    {
        if (accepted_you && accepted_him)
        {
            if (PhotonNetwork.IsMasterClient)
                startGame();
            else
            {
                photonView.RPC(nameof(startGame), RpcTarget.Others);
            }

            return true;
        }
        return false;
    }

    [PunRPC]
    public void startGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            loadGame();
        }
    }

    void sendStatus()
    {
        isT = generateTeamSide();
        onlineMenu.setMyTeam(isT);
        photonView.RPC(nameof(receiveStatus), RpcTarget.Others, SerializeStatus(onlineMenu.myTeam,onlineMenu.rank,onlineMenu.wins,onlineMenu.ppId,isT));
    }

    private bool generateTeamSide()
    {
        int range = 100_000_000;
        bool generetedTeamSide = Random.Range(-range, range) <= 0;
        if (PhotonNetwork.IsMasterClient)
            onlineMenu.setMyTeam(generetedTeamSide);
        return generetedTeamSide;
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
    
    [PunRPC]
    void ReceiveAccepted()
    {
        accepted_him = true;
        checkIsAccepted();
    }

    private void destroyAd()
    {
        MainMenuAd ad = MainMenu.getAdManager();
        if (ad == null)
            return;
        ad.destroyAdds();
    }

    private void loadGame()
    {
        isLeftingScene = true;
        destroyAd();
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
            progressLabel.GetComponent<TextLoading>().setNewText(LanguageSystem.GET_IS_SEARCHING_PLAYER_LABEL());
            Invoke(nameof(searchForOtherRooms),waitTimeForSearchAgain);
        }
    }

    public void setSearchinActive(bool active)
    {
        progressLabel.SetActive(active);
        him.SetActive(!active);
    }

    [PunRPC]
    private void receiveStatus(byte[] b)
    {
        onlineMenu.setHisTeam(DeserializePlayersName(b).ToArray());
        onlineMenu.setHisRank(b[b.Length-4]);
        onlineMenu.setHisWins(b[b.Length-3]);
        onlineMenu.setHisPP  (b[b.Length-2]);
        bool receivedTeam = b[b.Length - 1] == 1;
        
        if (!PhotonNetwork.IsMasterClient)
        {
            onlineMenu.setMyTeam(!receivedTeam);
        }
    }

    private byte[] SerializeStatus(string[] names, int rank, int wins, int pp, bool isT)
    { 
        List<Byte> byteList = new List<Byte>();
        byte byteRank = (byte)rank;
        byte byteWins = (byte)wins; // TODO for more wins < 255
        byte bytePP = (byte)pp;
        byte byteisT =(byte) (isT?1:0);

        for (int i = 0; i < names.Length; i++)
        {
            addAllToList(byteList, stringToArr(names[i]));
        }
        
        byteList.Add(byteRank);
        byteList.Add(byteWins);
        byteList.Add(bytePP);
        byteList.Add(byteisT);
        return byteList.ToArray();
    }
    
    public static byte[] stringToArr(string str)
    {
        return Convert.FromBase64String(Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(str)));
    }
    
    public static string arrToString(byte[] arr)
    {
        return UTF8Encoding.UTF8.GetString(arr);
    }
    
    private List <String> DeserializePlayersName(byte [] arr)
    {
        List<String> nameList = new List<String>();
        String name = "";
        
        List<byte> byteArr = new List<byte>();
        for(int i = 0; i < arr.Length-3; i++)
        {
            byte b = arr[i];
            if (b == END_OF_TEXT)
            {
                nameList.Add(arrToString(byteArr.ToArray()));
                byteArr.Clear();
                continue;
            }
            byteArr.Add(b);
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

    public void notAccepted(bool accepted)
    {
        if (!accepted)
            LeaveRoom();
    }
}
