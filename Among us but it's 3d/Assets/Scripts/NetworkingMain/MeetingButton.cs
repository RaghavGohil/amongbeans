using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeetingButton : MonoBehaviour
{

    //~Animator:
    private Animator animator;
    
    //~Bools:
    public bool keyDown;
    private bool disableMeetingButton;
    private bool canCallMeeting;

    //~Consts:
    //Byte:
    private const byte E_MeetingPressedEvent = 0; //Dir init 0;
    private byte flag;

    //~Floats:
    public float timeLeft; //Acc in voting system.
    
    //~PhotonView:
    private PhotonView view;

    private void Start()
    {

        timeLeft = 50f;
        keyDown = false;
        disableMeetingButton = false;
        canCallMeeting = true;
        flag = 0;
        view = GetComponent<PhotonView>(); // Get the photon component.
        gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Use").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);

    }
    
    private void Update()
    {

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            canCallMeeting = false;

        }

        else if(!gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            canCallMeeting = true;

        }

        if((Input.GetKeyDown(KeyCode.Tab) && disableMeetingButton == false && canCallMeeting )&& (gameObject.GetComponent<Player_Data_Script>().playerStatusEnum != Player_Data_Script.Player_status.Dead))
        {

            keyDown = true;

        }

        if(flag>0)
        {

            disableMeetingButton = true;

        }

        /**if(Input.GetKeyDown(KeyCode.E) && disableMeetingButton == false) //Check key down for meeting button.
        {

            disableMeetingButton = true;

        }**/

        if(Input.GetKeyUp(KeyCode.Tab))
        {

            keyDown = false;

        }

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
        
        if(eventCode == E_MeetingPressedEvent)
        {

            //gameObject.GetComponent<Animator>().Play("Animation_UI_Panel_MeetingButtonPressed");
            gameObject.GetComponent<VotingSystem>().votingSystemIsShowing = (bool)content;
            gameObject.transform.Find("Canvas").transform.Find("Panels").gameObject.GetComponent<VoteOutSystem>().Reset(); //Reset voting system.

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("MeetingButton") && disableMeetingButton == false)
        {   

            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Use").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,1f);
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Use").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Sabotage").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject.SetActive(true);

        }


    }

    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.CompareTag("MeetingButton"))
        {

            if(keyDown == true)
            {
                flag = 1;
                animator = gameObject.GetComponent<Animator>();
                animator.Play("Animation_UI_Panel_MeetingButtonPressed");
                gameObject.GetComponent<SoundHandler>().PlaySFX("MeetingPressed");
                view.RPC("PressedButton" , PhotonTargets.Others, 1);
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject.SetActive(true);
                gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Use").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
                gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Use").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Sabotage").gameObject.SetActive(true);
                gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject.SetActive(false);
                GameObject.FindWithTag("Spawners").GetComponent<Change_Player_Position>().SpawnPlayers();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if(other.gameObject.CompareTag("MeetingButton"))
        {

            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Use").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Use").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Sabotage").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject.SetActive(false);
            
        }
        
    }

    [PunRPC]
    private void PressedButton(int takeMeetingInput)
    {

        if(takeMeetingInput == 1)
        {

            gameObject.GetComponent<SoundHandler>().PlaySFX("MeetingPressed");
            GameObject.FindWithTag("Spawners").GetComponent<Change_Player_Position>().SpawnPlayers();
            StartCoroutine(Wait());

        }

    }

    private IEnumerator Wait()
    {

        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<VotingSystem>().votingSystemIsShowing = true;

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.All,

        };

        PhotonNetwork.RaiseEvent(E_MeetingPressedEvent , gameObject.GetComponent<VotingSystem>().votingSystemIsShowing , true ,RaiseEventOptions.Default);

    }

}
