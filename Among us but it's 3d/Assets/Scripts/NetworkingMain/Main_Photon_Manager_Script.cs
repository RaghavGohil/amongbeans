using UnityEngine; // Main Unity namespace.
using Game_Settings_Net; //Main Game Settings
using System; //Exception Handling.
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// [REM]:Photon.Pun namespace is not available.

namespace Game_Settings_Net //[Main Game Settings]
{

    public class UserGameSettings // User Settings
    {

        //-

    }

    public class RoomSettings
    {

        public static byte maxPlayers = 0; //Set Max Players.
        public static byte maxImposters = 0; //Set Max Imposters. (init for no garbage value) (value changed via panel)

    }

    public class GameSettings // Main Game Settings.
    {

        //-

    }

}

public class Main_Photon_Manager_Script : Photon.MonoBehaviour //[Main photon networking script]
{

    //~GameObjects:
    public GameObject retryConnection; //Retry Connection Button
    public GameObject panelMainComponent; //The Main panel container

    //~UI_Text:
    public Text statusText;
    public Text numMaxImpostersText;
    public Text numMaxPlayersText;

    //~UI_Input_Field;
    public InputField playerNameInput;
    public InputField createRoomNameInput; //Get room name

    //~UI_Button:
    public Button setupButton;
    public Button joinGameButton;

    //~UI_Sliders:
    public Slider numMaxImposters;
    public Slider numMaxPlayers;

    //~Strings:
    [SerializeField]
    private string version_number_; //Main Version Of Game.

    //~Bools:
    private bool isConnectedToServer; //Check if the player is connected to the server.
    private bool isJoined; // Joined to main server lobby.

    //~Animators;
    private Animator setupButtonAnim;

    private void Start()
    {

        //DontDestroyOnLoad(this);

        setupButtonAnim = panelMainComponent.GetComponent<Animator>();

        isConnectedToServer = false; //Set false on start.

        version_number_ = "0.5"; //Version [String]

        statusText.text = "Connecting to master..."; //Set connection status.
        retryConnection.SetActive(false);

        PhotonNetwork.ConnectUsingSettings(version_number_); //Connect to version number.
        
        print("[:Sys:] Connected using version:- "+version_number_);

    }

    private void Update()
    {
               
        //RUN STARTER METHODS:
        SetPlayerName();
        SetMaxPlayersAndImposters();

    }
    
    public void OnClickRetryConnection()
    {
        retryConnection.SetActive(false);
        PhotonNetwork.ConnectUsingSettings(version_number_);
        print("[:Sys:] Connected using version:- " + version_number_);
    }

    private void OnConnectedToMaster() 
    {

        isConnectedToServer = true;

        statusText.color = new Color(0,255f,0);
        statusText.text = "Client Connected: v"+(string)version_number_ +" ["+PhotonNetwork.CloudRegion+"]";
        print("[:Sys:] Connected to Master.");
        
        //JoinGameLobby:
        JoinGameLobby();
    }

    private void OnPhotonJoinRoomFailed()
    {
        statusText.color = new Color(255f,0,0);
        statusText.text = "Error: Failed to join room.";
        joinGameButton.interactable = true;
    }

    private void OnJoinedLobby() 
    {
        statusText.color = new Color(0,255f,0);
        statusText.text = "Joined Lobby: [TypedLobby.Default]";
    }

    private void OnJoinedRoom() 
    {
        statusText.color = new Color(0,255f,0);
        statusText.text = "Joining room \""+PhotonNetwork.room.name+"\"...";
        isJoined = true;

        OnClickMainGame();
    }

    private void OnPhotonRandomJoinFailed()
    {
        statusText.color = new Color(255f,0f,0f);
        statusText.text = "Error: Random Room Join Failed.(Create a room)";
    }

    private void OnPhotonCreateRoomFailed()
    {

        statusText.color = new Color(255f,0,0);
        statusText.text = "Error: Failed To Create Room.[A Room with that name already exists.]";

    }

    private void OnDisconnectedFromPhoton() 
    {
        isConnectedToServer = false; //Set false if not connected.

        retryConnection.SetActive(true);
        statusText.color = new Color(255f,0,0);
        statusText.text = "Client Disconnected";
        print("[:Sys:] Error: Disconnected.");
    }

    private void OnDisconnectedFromRoom()
    {

        isJoined = false;

    }

    private void OnDisconnectedFromLobby()
    {

        isJoined = false;

    }

    ////////////////////////////////////////////STARTER METHODS/////////////////////////////////////////////////////////

    void OnReceivedRoomListUpdate()
    {
        GetRooms();
    }

    private void GetRooms() //Get available rooms[Test]
    {

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        foreach (RoomInfo room in rooms)
        {
            print(room.name);
        }

    }

    private void SetPlayerName()
    {

        //Set the player name in game:
        PhotonNetwork.playerName = playerNameInput.text; //SET
        PhotonNetwork.player.NickName = playerNameInput.text; //SET
        //If playername == null -> do stuff.

    }

    private void SetMaxPlayersAndImposters() // This function sets max numbers of players and imposters and shows it to the user.
    {

        numMaxPlayersText.text = numMaxPlayers.value.ToString();
        numMaxImpostersText.text = numMaxImposters.value.ToString(); //State values to the player.

        Game_Settings_Net.RoomSettings.maxPlayers = (byte)numMaxPlayers.value;
        Game_Settings_Net.RoomSettings.maxImposters = (byte)numMaxImposters.value; //Set to namespace.

        //print(Game_Settings_Net.RoomSettings.maxPlayers);
        //print(Game_Settings_Net.RoomSettings.maxImposters);

    }

    public void JoinGameLobby() //////////////Implementation left.
    {

        PhotonNetwork.JoinLobby(TypedLobby.Default);

    }

    public void SetRoom() //Sets room at start
    {

        //Throw error if name is not there;
        if(playerNameInput.text == "")
        {

            statusText.color = new Color(255f,0,0);
            statusText.text = "Error: Invalid Name.";

        }

        //Throw error if room name is "":
        if(createRoomNameInput.text == "")
        {

            statusText.color = new Color(255f,0,0);
            statusText.text = "Error: Room name can't be empty.";

        }

        if(isConnectedToServer && playerNameInput.text != "" && createRoomNameInput.text != "")
        { // if connected and a room is created with the same name then join room.

            ////Set Room Pressed://///////
            int enterFlag = 0; //Play the anim if this is 1 (Animation_UI_MainPanel_To_SetupPanel)
            RoomInfo[] rooms = PhotonNetwork.GetRoomList(); //Get Rooms<-
            foreach (RoomInfo room in rooms)
            {
                if(room.name == createRoomNameInput.text) //already available rooms
                {
                    //Join or create:
                    PhotonNetwork.JoinRoom(createRoomNameInput.text);   
                    statusText.color = new Color(0,255f,0);
                    statusText.text = "Joining room...";
                    enterFlag = 1;
                }

            }

            if(enterFlag == 0)
            {

                //Play anim here;
                setupButtonAnim.Play("Animation_UI_MainPanel_To_SetupPanel");

            }

        }

    }

    public void SetRandomRoom() //Sets room at start
    {

        //Throw error if name is not there;
        if(playerNameInput.text == "")
        {

            statusText.color = new Color(255f,0,0);
            statusText.text = "Error: Invalid Name.";

        }


        if(isConnectedToServer && playerNameInput.text != "")
        { // if connected and a room is created with the same name then join room.

            ////Set Room Pressed://///////

            PhotonNetwork.JoinRandomRoom();   
            statusText.color = new Color(0,255f,0);
            statusText.text = "Joining room...";

        }

    }

    public void CreateRoom() //use as button[Main button for creating room.]
    {

        if(isConnectedToServer && playerNameInput.text != "" && createRoomNameInput.text != "")
        { // if connected , create room.

            ////Create Room Pressed://///////

            print("Room created!");

            if(createRoomNameInput.text != null)
            {
                //Join or create:
                PhotonNetwork.CreateRoom(createRoomNameInput.text , new RoomOptions(){isVisible = true , MaxPlayers = Game_Settings_Net.RoomSettings.maxPlayers}, TypedLobby.Default);   
                statusText.color = new Color(0,255f,0);
                statusText.text = "Setting room...";
            }
            else
            {

                //Teleport to room list:
                //JoinLobby.
                
            }

            //Disable UI Button:
            //joinGameButton.interactable = false;
            //Now player can create and join rooms.

        }

    }

    private void OnClickMainGame()
    {

        //Load the main level:
        if(isConnectedToServer && playerNameInput.text != "" && isJoined == true)
        {
            PhotonNetwork.LoadLevel("StarterScene");
        }
        //Here we are loaded to main game.

    }

    /////////////END OF MAIN SCRIPT/////////////////////

}
