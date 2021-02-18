using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportBodies : Photon.MonoBehaviour
{


    //~Bools:
    private bool keyDown;
    private bool canReportBodies;

    //~Animator:
    private Animator animator;

    //~Byte:
    private const byte E_ReportPressedEvent = 1;

    //~PhotonView:
    private PhotonView view;

    //~Floats:
    public float timeLeft; //Acc in voting system.

    private void Start()
    {

        keyDown = false;
        animator = gameObject.GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        timeLeft = 50f;
        canReportBodies = true;
        gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
        gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);

    }

    private void Update()
    {

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            canReportBodies = false;

        }

        else if(!gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            canReportBodies = true;

        }

        if((Input.GetKeyDown(KeyCode.R) && canReportBodies )&&(gameObject.GetComponent<Player_Data_Script>().playerStatusEnum != Player_Data_Script.Player_status.Dead))
        {

            keyDown = true;

        }

        /**if(Input.GetKeyDown(KeyCode.E) && disableMeetingButton == false) //Check key down for meeting button.
        {

            disableMeetingButton = true;

        }**/

        if(Input.GetKeyUp(KeyCode.R))
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
        
        if(eventCode == E_ReportPressedEvent)
        {

            //gameObject.GetComponent<Animator>().Play("Animation_UI_Panel_ReportBody");
            gameObject.GetComponent<VotingSystem>().votingSystemIsShowing = (bool)content;
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
            gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
            gameObject.transform.Find("Canvas").transform.Find("Panels").gameObject.GetComponent<VoteOutSystem>().Reset(); //Reset voting system.

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Corpse"))
        {

            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,1f);
            gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,1f);

        }

    }
    
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.CompareTag("Corpse"))
        {

            if(keyDown == true)
            {
                animator.Play("Animation_UI_Panel_ReportBody");
                gameObject.GetComponent<SoundHandler>().PlaySFX("ReportBody");
                view.RPC("ReportPressedButton" , PhotonTargets.Others , 1 , other.gameObject.GetComponent<PhotonView>().viewID);
                other.gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Voting").gameObject.SetActive(false);
                GameObject.FindWithTag("Spawners").GetComponent<Change_Player_Position>().SpawnPlayers();
                //PhotonNetwork.Destroy(other.gameObject);
                gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject.SetActive(true);
                gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject.SetActive(false);
                gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
                gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if(other.gameObject.CompareTag("Corpse"))
        {

            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Crosshair").gameObject.SetActive(true);
            gameObject.transform.Find("Canvas").transform.Find("MainUIElements").transform.Find("CrosshairHolder").transform.Find("Hand").gameObject.SetActive(false);
            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);
            gameObject.transform.Find("Canvas").transform.Find("CrewmateUIElement").transform.Find("Report").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);

        }

    }

    [PunRPC]
    private void ReportPressedButton(int takeMeetingInput , int viewID)
    {

        if(takeMeetingInput == 1)
        {

            gameObject.GetComponent<SoundHandler>().PlaySFX("ReportBody");
            GameObject.FindWithTag("Spawners").GetComponent<Change_Player_Position>().SpawnPlayers();
            StartCoroutine(Wait());
            GameObject cor = PhotonView.Find(viewID).gameObject;
            cor.SetActive(false);

        }

    }

    private IEnumerator Wait()
    {

        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<VotingSystem>().votingSystemIsShowing = true;

        RaiseEventOptions options = new RaiseEventOptions()
        {

            CachingOption = EventCaching.DoNotCache,
            Receivers = ReceiverGroup.Others,

        };

        PhotonNetwork.RaiseEvent(E_ReportPressedEvent , gameObject.GetComponent<VotingSystem>().votingSystemIsShowing , true ,options);

    }

}
