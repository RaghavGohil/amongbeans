using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VotingSystem : Photon.MonoBehaviour
{
    
    //~GameObjects:
    [SerializeField]
    private GameObject[] playersInGame;

    //~Bools:
    public bool votingSystemIsShowing;

    //~Floats:
    public float timeLeftForVoting;
    public float proceedTime;
    
    //UI:
    private Text timeLeftText;
    [SerializeField]
    private Text[] playerNameTextGameObjects;

    //~Int:
    private int setval; //player name set value.
    [SerializeField]
    private int flag; // SetFlag;

    //~Color32:
    private Color playerColor;

    //PhotonView;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if(!view.isMine)
        {

            this.enabled = false;

        }

        timeLeftText = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").transform.Find("TimeLeft").gameObject.GetComponent<Text>();
        playersInGame = GameObject.FindGameObjectsWithTag("Player");
        playerNameTextGameObjects = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").transform.Find("VotingPanel").gameObject.GetComponentsInChildren<Text>();

        flag = 0; //SetFlag 0;
        votingSystemIsShowing = false;
        timeLeftForVoting = gameObject.GetComponent<MeetingButton>().timeLeft;
        proceedTime = 5f;
        StartCoroutine(GetPlayers());

    }

    private void Update()
    {
        
        if(votingSystemIsShowing == true) //Showing true..
        {
            gameObject.GetComponent<Player_Movement>().playerCanMove = false;
            gameObject.GetComponent<Player_Movement>().playerCanJump = false;
            gameObject.GetComponent<Player_CameraLook>().canRotateCamera = false;
            Cursor.visible = true;
            gameObject.GetComponent<Player_CameraLook>().cursorLocked = false;         //PlayerLockState;
            timeLeftText.text = "Time Left : "+((int)timeLeftForVoting).ToString();
            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Minimap").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").gameObject.SetActive(false);
            timeLeftForVoting  -= Time.deltaTime;

        }

        else if(votingSystemIsShowing == false && gameObject.GetComponent<AssignRandomTasks>().taskShowing == false)
        {
            gameObject.GetComponent<Player_Movement>().playerCanMove = true;
            gameObject.GetComponent<Player_Movement>().playerCanJump = true;
            gameObject.GetComponent<Player_CameraLook>().canRotateCamera = true;
            Cursor.visible = false;
            gameObject.GetComponent<Player_CameraLook>().cursorLocked = true;          //PlayerFreeState;
            timeLeftForVoting = gameObject.GetComponent<MeetingButton>().timeLeft; //Reset.
            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("Minimap").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").gameObject.SetActive(true);

        }

        SetColors();

    }

    
    private void SetColors()
    {

        for(int i = 0;i < 10;i++)
        {
            
            if(i < PhotonNetwork.playerList.Length)
            {
                playerNameTextGameObjects[i].transform.Find("Body").gameObject.GetComponent<RawImage>().color = playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
                playerNameTextGameObjects[i].transform.Find("Back").gameObject.GetComponent<RawImage>().color = playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
            }
        }

    }

    private IEnumerator GetPlayers()
    {

        yield return new WaitForSeconds(1f);

        for(int i = 0;i < 10;i++)
        {
            
            if(i < PhotonNetwork.playerList.Length)
            {
                playerNameTextGameObjects[i].transform.Find("Body").gameObject.GetComponent<RawImage>().color = playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
                playerNameTextGameObjects[i].transform.Find("Back").gameObject.GetComponent<RawImage>().color = playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
                playerNameTextGameObjects[i].text = playersInGame[i].GetComponent<PhotonView>().owner.name.ToString();
                print(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum);
                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                {
                    
                    if(playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                    {

                        playerNameTextGameObjects[i].color = new Color(1,0,0,1);

                    }

                }
            }

            else
            {

                playerNameTextGameObjects[i].text = "None";
                playerNameTextGameObjects[i].transform.Find("Panel").gameObject.SetActive(true);
                playerNameTextGameObjects[i].transform.Find("VotingButton").gameObject.GetComponent<Button>().interactable = false;
            }
        }

    }

}
