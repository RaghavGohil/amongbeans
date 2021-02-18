using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoteOutSystem : MonoBehaviour
{
    //~Bools:
    public bool canVote;
    public bool startWork;

    //~Integers:
    public int black;
    public int darkblue;
    public int cyan;
    public int white;
    public int red;
    public int darkgreen; ///////Voting values:
    public int lime;
    public int purple;
    public int yellow;
    public int orange;
    public int skip;

    //~UI:
    [SerializeField]
    private Button[] buttons;

    //~Arrays:
    //GameObjects:
    public GameObject[] playersInGame;
    //Integer:
    private int[] voteCount;

    //PhotonView:
    private PhotonView view;

    //byte:
    private byte E_VoteEvent = 2;

    private void Start()
    {

        canVote = true;
        //doneVoting = false;
        view = GetComponent<PhotonView>();
        //StartRemove:
        StartCoroutine(StartWork());
        //init:
        startWork = false;
        black = 0;
        darkblue = 0;
        cyan = 0;
        white = 0;
        red = 0;
        darkgreen = 0;
        lime = 0;
        purple = 0;
        yellow = 0;
        orange = 0;
        skip = 0;

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

    }

    private void OnEnable()
    {

        PhotonNetwork.OnEventCall += NetworkingClient_EventRecieved;

    }

    private void OnDisable()
    {

        PhotonNetwork.OnEventCall -= NetworkingClient_EventRecieved;

    }

    private void NetworkingClient_EventRecieved(byte eventCode, object content, int senderId)
    {
        
        if(eventCode == E_VoteEvent)
        {

            if((string)content == "RGBA(0.000, 0.000, 0.000, 1.000)")
            {

                black = black+1;
                print(content);
            }

            else if((string)content == "RGBA(0.000, 0.043, 0.538, 1.000)")
            {

                darkblue = darkblue+1;
                print(content);

            }

            else if((string)content == "RGBA(0.000, 0.718, 0.792, 1.000)")
            {

                cyan = cyan+1;
                print(content);

            }

            else if((string)content == "RGBA(1.000, 1.000, 1.000, 1.000)")
            {

                white = white+1;
                print(content);

            }

            else if((string)content == "RGBA(1.000, 0.000, 0.000, 1.000)")
            {

                red = red+1;
                print(content);

            }

            else if((string)content == "RGBA(0.008, 0.189, 0.035, 1.000)")
            {

                darkgreen = darkgreen+1;
                print(content);

            }

            else if((string)content == "RGBA(0.000, 1.000, 0.165, 1.000)")
            {

                lime = lime+1;
                print(content);

            }

            else if((string)content == "RGBA(0.375, 0.000, 1.000, 1.000)")
            {

                purple = purple+1;
                print(content);

            }

            else if((string)content == "RGBA(1.000, 0.932, 0.000, 1.000)")
            {

                yellow = yellow+1;
                print(content);

            }

            else if((string)content == "RGBA(1.000, 0.401, 0.000, 1.000)")
            {

                orange = orange+1;
                print(content);

            }

            else if((string)content == "Skip")
            {

                skip = skip+1;
                print(content);

            }

        }

    }

    private void Update()
    {

        if(!canVote)
        {

            for(int i = 0; i<buttons.Length ; i++)
            {

                buttons[i].interactable = false;

            }

        }

        else if(canVote)
        {

            for(int i = 0; i<PhotonNetwork.playerList.Length ; i++)
            {
                buttons[i].interactable = true;

                if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(0.000, 0.000, 0.000, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    //print(content);
                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(0.000, 0.043, 0.538, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    //print(content);

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(0.000, 0.718, 0.792, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    //print(content);

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(1.000, 1.000, 1.000, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(1.000, 0.000, 0.000, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(0.008, 0.189, 0.035, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(0.000, 1.000, 0.165, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(0.375, 0.000, 1.000, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(1.000, 0.932, 0.000, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

                else if(buttons[i].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
                {

                    for(int j = 0; j < playersInGame.Length ; j++)
                    {

                        if(playersInGame[j].GetComponent<Player_Data_Script>().color == "RGBA(1.000, 0.401, 0.000, 1.000)" && playersInGame[j].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
                        {

                            buttons[i].interactable = false;
                            buttons[i].transform.parent.transform.Find("Panel").gameObject.SetActive(true);

                        }

                    }
                    

                }

            }

            gameObject.transform.Find("Voting").transform.Find("Skip").gameObject.GetComponent<Button>().interactable = true;

        }

        /**if(gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            int maxVote = 0;

            voteCountDisableSystem = new int[]{black, darkblue , cyan , darkgreen , lime , yellow , white , red , purple , orange , skip};

            for(int i = 0; i < voteCountDisableSystem.Length ; i++)
            {

                maxVote += voteCountDisableSystem[i];

            }

            if(maxVote >= PhotonNetwork.playerList.Length-1)
            {

                doneVoting = true;

            }

        }**/

        if(gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().timeLeftForVoting < 50f && startWork)
        {

            voteCount = new int[]{black, darkblue , cyan , darkgreen , lime , yellow , white , red , purple , orange , skip};

            int allCount = 0;

            for(int i = 0; i< voteCount.Length;i++)
            {

                allCount += voteCount[i];                

            }
            int alivePlayers = 0;

            for(int i = 0; i<playersInGame.Length;i++)
            {

                if(playersInGame[i].GetComponent<Player_Data_Script>().playerStatusEnum != Player_Data_Script.Player_status.Dead)
                {

                    alivePlayers += 1;

                }

            }
            if(allCount >= alivePlayers)
            {
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().timeLeftForVoting = 0f;
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().votingSystemIsShowing = false;
                ///Coroutines:

            }

            print(alivePlayers);
            print(allCount);
        }

        //print(gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().timeLeftForVoting);

        if(((gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().timeLeftForVoting < 1f) && startWork))
        {

            canVote = false;

            voteCount = new int[]{black, darkblue , cyan , darkgreen , lime , yellow , white , red , purple , orange , skip};

            int count = 0;
            bool tie = false;

            int max =  Mathf.Max(voteCount);

            for(int i = 0; i < voteCount.Length; i++) //Checking multiple votes:
            {

                if(voteCount[i] == max)
                {

                    count+=1;

                }

                if(count > 1)
                {

                    tie = true;

                }

            }
            

            if(tie)
            {

                for(int i = 0;i<PhotonNetwork.playerList.Length;i++)
                {
                    if(playersInGame[i].gameObject.GetComponent<NoEjection>() == null)
                        playersInGame[i].gameObject.AddComponent<NoEjection>();

                }


            }

            if(max<1)
            {

                for(int i = 0;i<PhotonNetwork.playerList.Length;i++)
                {
                    if(playersInGame[i].gameObject.GetComponent<NoEjection>() == null)
                        playersInGame[i].gameObject.AddComponent<NoEjection>();

                }

            }

            if(skip == max && !tie) //Skipping
            {

                print("Skipped lol");

                for(int i = 0;i<PhotonNetwork.playerList.Length;i++)
                {
                    if(playersInGame[i].gameObject.GetComponent<NoEjection>() == null)
                        playersInGame[i].gameObject.AddComponent<NoEjection>();

                }

            }


            if(skip!=max && !tie)
            {

                print("Yeeted lol");

                for(int i = 0;i<PhotonNetwork.playerList.Length;i++)
                {

                    if(playersInGame[i].gameObject.GetComponent<Ejection>() == null)
                        playersInGame[i].gameObject.AddComponent<Ejection>();

                }

            }

            if(black == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {

                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }



            }

            if(darkblue== max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {

                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(darkgreen == max && !tie)
            {
                
                for(int i = 0; i < playersInGame.Length;i++)
                {

                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(lime == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {

                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(yellow == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {


                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(white == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {


                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(red == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {
                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(purple == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {


                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(orange == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {


                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }

            if(cyan == max && !tie)
            {

                for(int i = 0; i < playersInGame.Length;i++)
                {


                    if(playersInGame[i].transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material.color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
                    {
                        view.RPC("SetPlayerDead" , PhotonTargets.AllBuffered , playersInGame[i].GetComponent<PhotonView>().viewID);
                        string name = "";
                        if(playersInGame[i].gameObject.GetComponent<Ejection>() != null)
                        {
                            name = playersInGame[i].gameObject.GetComponent<PhotonView>().owner.name;

                            for(int j = 0; j < playersInGame.Length ; j++)
                            {

                                playersInGame[j].gameObject.GetComponent<Ejection>().playerName = name;

                            }
                        }
                    }

                }

            }
            gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().votingSystemIsShowing = false;
            gameObject.transform.parent.transform.parent.gameObject.GetComponent<VotingSystem>().timeLeftForVoting = gameObject.transform.parent.transform.parent.gameObject.GetComponent<MeetingButton>().timeLeft;
            
            
        }

        if(gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
        {

            canVote = false;

        }

    }

    private IEnumerator StartWork()
    {

        yield return new WaitForSeconds(3f);
        startWork = true;

    }

    [PunRPC]
    private void SetPlayerDead(int viewID)
    {

        GameObject player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<Player_Data_Script>().playerStatusEnum = Player_Data_Script.Player_status.Dead; 
        player.transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        player.transform.Find("MainPlayer").transform.Find("Back Pack").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        //player.transform.Find("Ghost High Main").gameObject.SetActive(true);

    }

    public void Reset() //Meeting Button.
    {

        canVote = true; //Reset values for new vote.
        black = 0;
        darkblue = 0;
        cyan = 0;
        white = 0;
        red = 0;
        darkgreen = 0;
        lime = 0;
        purple = 0;
        yellow = 0;
        orange = 0;
        skip = 0;

    }

    /**[PunRPC]
    private void Vote(string data)
    {

        AssignVote(data);

    }

    private void AssignVote(string inp)
    {

        if(inp == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black +=1;
            print(inp);
        }

        else if(inp == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue +=1;
            print(inp);

        }

        else if(inp == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan += 1;
            print(inp);

        }

        else if(inp == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white += 1;
            print(inp);

        }

        else if(inp == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red += 1;
            print(inp);

        }

        else if(inp == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen += 1;
            print(inp);

        }

        else if(inp == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime += 1;
            print(inp);

        }

        else if(inp == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple += 1;
            print(inp);

        }

        else if(inp == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow += 1;
            print(inp);

        }

        else if(inp == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange += 1;
            print(inp);

        }

        else if(inp == "Skip")
        {

            skip += 1;
            print(inp);

        }

    }**/

    public void button1()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            //print(content);
        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            //print(content);

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            //print(content);

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[0].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }

    }

    public void button2()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[1].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button3()
    {
        
        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[2].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }
    public void button4()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[3].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button5()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[4].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button6()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[5].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button7()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[6].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button8()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[7].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button9()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[8].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void button10()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());
        print(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString());

        if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.000, 0.000, 1.000)")
        {

            black = black+1;
            
        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.043, 0.538, 1.000)")
        {

            darkblue = darkblue+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 0.718, 0.792, 1.000)")
        {

            cyan = cyan+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 1.000, 1.000, 1.000)")
        {

            white = white+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {

            red = red+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.008, 0.189, 0.035, 1.000)")
        {

            darkgreen = darkgreen+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.000, 1.000, 0.165, 1.000)")
        {

            lime = lime+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(0.375, 0.000, 1.000, 1.000)")
        {

            purple = purple+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.932, 0.000, 1.000)")
        {

            yellow = yellow+1;
            

        }

        else if(buttons[9].gameObject.transform.parent.gameObject.transform.Find("Body").gameObject.GetComponent<RawImage>().color.ToString() == "RGBA(1.000, 0.401, 0.000, 1.000)")
        {

            orange = orange+1;
            

        }
    }

    public void SkipButton()
    {

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        canVote = false;
        PhotonNetwork.RaiseEvent(E_VoteEvent , "Skip" , true ,options);
        //view.RPC("Vote" , PhotonTargets.AllBuffered , "Skip");
        skip = skip+1;
        print("Skipped");
    }

}
