using UnityEngine;
using UnityEngine.UI;

public class WIringTask : MonoBehaviour
{
    
    //Bools:
    private bool areOn;
    private bool passBool;

    //~Integers;
    private int buttonsOn;

    //GameObjects:
    private Toggle[] toggles;

    //RaiseEvent;
    private RaiseEventOptions options;

    //Byte;
    private byte E_TaskEvent = 3;

    //String:
    private string placeName;

    // Start is called before the first frame update
    private void Start()
    {

        placeName = "";
        passBool = true;
        options = new RaiseEventOptions() //Options for raiseevent.
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };

        areOn = false;
        buttonsOn = 0;
        toggles = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").transform.Find("Panel").gameObject.GetComponentsInChildren<Toggle>();
    }

    // Update is called once per frame
    private void Update()
    {

        buttonsOn = 0;

        for(int i = 0 ; i < toggles.Length ; i++)
        {

            if(toggles[i].isOn)
            {

                buttonsOn += 1;

            }


        }

        if(buttonsOn == 16)
        {

            areOn = true;

        }

        if(areOn)
        {
            
            Reset();
            passBool = true;
            placeName = "";
            PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);
            gameObject.GetComponent<SoundHandler>().PlaySFX("TaskComplete");
            gameObject.GetComponent<Player_Movement>().playerCanMove = true;
            gameObject.GetComponent<Player_Movement>().playerCanJump = true;
            gameObject.GetComponent<Player_CameraLook>().canRotateCamera = true;
            Cursor.visible = false;
            gameObject.GetComponent<Player_CameraLook>().cursorLocked = true;
            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject.SetActive(false);
            gameObject.GetComponent<AssignRandomTasks>().taskShowing = false;



        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "cafeteria" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().cafeteriaWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "security" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().securityWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "upperengine" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().upperEngineWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "lowerengine" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().lowerEngineWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "reactor" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().reactorWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "storage" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().storageWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "admin" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().adminWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "shields" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().shieldsWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "navigation" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().navigationWiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "o2" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().o2Wiring = true;

            passBool = false;

        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && placeName == "weapons" && passBool)
        {

            gameObject.GetComponent<AssignRandomTasks>().weaponsWiring = true;

            passBool = false;

        }

    }

    public void Reset() //Resets all variables
    {

        for(int i = 0 ; i < toggles.Length ; i++)
        {

            toggles[i].isOn = false;

        }

        buttonsOn = 0;
        areOn = false;

    }

    public void StartTask_Wiring(string place) //Main wiring task handler.
    {

        gameObject.GetComponent<Player_CameraLook>().canRotateCamera = false;
        Cursor.visible = true;
        gameObject.GetComponent<Player_CameraLook>().cursorLocked = false; 
        gameObject.GetComponent<Player_Movement>().playerCanMove = false;
        gameObject.GetComponent<Player_Movement>().playerCanJump = false;

        GameObject Panel = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Wiring").gameObject;
        Panel.SetActive(true);

        if(areOn)
        {

            PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);

        }

        placeName = place;


    }

}
