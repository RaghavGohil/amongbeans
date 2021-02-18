using UnityEngine;
using UnityEngine.UI;

public class SheildsTask : MonoBehaviour
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

    //String:
    private string placeName;

    //Byte;
    private byte E_TaskEvent = 3;

    // Start is called before the first frame update
    private void Start()
    {
        passBool = true;
        placeName = "";
        options = new RaiseEventOptions() //Options for raiseevent.
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };

        areOn = false;
        buttonsOn = 0;
        toggles = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Shields").transform.Find("Panel").gameObject.GetComponentsInChildren<Toggle>();
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

        if(buttonsOn == 6)
        {

            areOn = true;

        }

        if(areOn)
        {
            
            Reset();
            placeName = "";
            passBool = true;
            PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);
            gameObject.GetComponent<SoundHandler>().PlaySFX("TaskComplete");
            gameObject.GetComponent<Player_Movement>().playerCanMove = true;
            gameObject.GetComponent<Player_Movement>().playerCanJump = true;
            gameObject.GetComponent<Player_CameraLook>().canRotateCamera = true;
            Cursor.visible = false;
            gameObject.GetComponent<Player_CameraLook>().cursorLocked = true;
            gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Shields").gameObject.SetActive(false);
            gameObject.GetComponent<AssignRandomTasks>().taskShowing = false;


        }

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing && passBool && placeName == "shields")
        {

            gameObject.GetComponent<AssignRandomTasks>().startShields = true;

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

    public void StartTask_Sheilds(string place) //Main wiring task handler.
    {
        placeName = place;
        gameObject.GetComponent<Player_CameraLook>().canRotateCamera = false;
        Cursor.visible = true;
        gameObject.GetComponent<Player_CameraLook>().cursorLocked = false; 
        gameObject.GetComponent<Player_Movement>().playerCanMove = false;
        gameObject.GetComponent<Player_Movement>().playerCanJump = false;

        GameObject Panel = gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Shields").gameObject;
        Panel.SetActive(true);

        if(areOn)
        {

            PhotonNetwork.RaiseEvent(E_TaskEvent , 1 , true ,options);


        }



    }
}
