using UnityEngine; // Unity main.
using Game_Settings_Net; //Get main game settings.
using System.Collections.Generic; // For Lists.
using System.Collections;

public class Assigner : MonoBehaviour
{
    
    //~Reminder-[Read Only Script please do not keep it open.]

    //~GameObjects:
    public GameObject[] playersInGame { get; private set;}
    
    //~Integers:
    public int numberOfImposters; // Number of imposters.

    //~PhotonView:
    private PhotonView view;


    //[PunRPC]
    //private void SetNumImposters(byte count)//Sets the number of imposters in the game in the other devices.
    //{

        //numberOfImposters = (int)count;

    //}

    //private void Start()
    //

        //StartCoroutine(StartAssign());

    //}

    private void Start()
    {

        if(!PhotonNetwork.isMasterClient) // if master client, send rpc
        {

            this.enabled = false;

        }

        //print(Game_Settings_Net.RoomSettings.maxPlayers);

        //Init numberOfImposters:
        numberOfImposters = Game_Settings_Net.RoomSettings.maxImposters;
        print("Num imposters:"+ numberOfImposters);
        //RandomAssigner will assign random imposters.
        //Init maxPlayers in Editor.

        view = GetComponent<PhotonView>();    

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////LOGIC
        int[] RandomAssignInteger = new int[numberOfImposters];
        int[] integerArray = new int[numberOfImposters];

        //Get the players in game.
        playersInGame = GameObject.FindGameObjectsWithTag("Player");
        
        int[] masterHeirarchyIn = new int[playersInGame.Length];

        //Generate random imposters:
        for(int i = 0; i < numberOfImposters ;i ++) // Random Assigner.
        {

            if(i == 0)
            {

                RandomAssignInteger[i] = Random.RandomRange(1,playersInGame.Length); //Get Random
            
            }

            else 
            {

                RandomAssignInteger[i] = Random.RandomRange(1,playersInGame.Length);
                
                while(RandomAssignInteger[i] == RandomAssignInteger[i-1])
                {

                    RandomAssignInteger[i] = Random.RandomRange(1,playersInGame.Length); //Get Random

                }
            }

            //Debug.Log(RandomAssignInteger[i]);

        }

        /////////////////////////////
        //Now we got the imposters.//
        //Let's assign some data.//
        ////////////////////////////

        //for(int i = 0; i < playersInGame.Length;i++) // Array of players 
        //{

        //    for(int j = 0; j < RandomAssignInteger.Length;j++) // Set in editor int
        //    {

        //        if(playersInGame.Length != RandomAssignInteger[j])
        //        {

        //            playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum = Player_Data_Script.Player_type.Crewmate; //Set Crewmate.

        //        }

        //        else {break;} //Break loop.

        //    }

        //}

        //for(int i = 0; i < numberOfImposters;i++)
        //{
        
        //    playersInGame[RandomAssignInteger[i]].GetComponent<Player_Data_Script>().playerTypeEnum = Player_Data_Script.Player_type.Imposter; // Random imposter assigned
        
        //}
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////LOGIC

        for(int i = 0; i < RandomAssignInteger.Length;i++)
        {

            integerArray[i] = RandomAssignInteger[i];
            //print(integerArray[i]);

        }

        for(int i = 0; i<playersInGame.Length ; i++)
        {

            masterHeirarchyIn[i] = playersInGame[i].GetComponent<PhotonView>().viewID;

        }

        if(PhotonNetwork.isMasterClient)
        {

            view.RPC("RPC_SENDASSIGNDATA" , PhotonTargets.All , integerArray , masterHeirarchyIn);

        }

    }

    [PunRPC]
    private void RPC_SENDASSIGNDATA(int[] arr , int[] masterHierarchy)//, GameObject[] masterHierarchy)
    {
        

        for(int i = 0; i < playersInGame.Length; i++)
        {

            playersInGame[i] = PhotonView.Find(masterHierarchy[i]).gameObject;

        }

        for(int i = 0; i < playersInGame.Length;i++) // Array of players 
        {

            for(int j = 0; j < arr.Length;j++) // Set in editor int
            {

                if(i != arr[j])
                {

                    playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum = Player_Data_Script.Player_type.Crewmate; //Set Crewmate.

                }
                else{break;}//Break loop.
                //else {playersInGame[arr[j]].GetComponent<Player_Data_Script>().playerTypeEnum = Player_Data_Script.Player_type.Imposter;}

            }

        }

        for(int i = 0; i < numberOfImposters;i++)
        {
        
            playersInGame[arr[i]].GetComponent<Player_Data_Script>().playerTypeEnum = Player_Data_Script.Player_type.Imposter; // Random imposter assigned
        
        }
        //print(arr.Length);
        for(int i = 0; i < arr.Length ; i++){print(arr[i]);}

    }

    private void Update()
    {

        //-

    }

}
