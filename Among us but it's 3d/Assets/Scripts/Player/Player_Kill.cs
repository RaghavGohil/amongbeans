using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player_Kill : Photon.MonoBehaviour
{

    //~Bools:
    private bool isImposter;
    private bool canKill = false;
    private bool killStop;
    private bool hasKilled;
    //String:
    public string corpsePath = "Prefabs/Corpse";

    //~Camera;
    private Camera camera;
    
    //~Raycast:
    private RaycastHit hit;
    private Ray ray;

    //~float:
    public float killDistance;

    //~PhotonView:
    private PhotonView view;

    //~Lists:
    [SerializeField]
    private GameObject[] corpseList;

    private void Start()
    {

        isImposter = false;
        hasKilled = false;
        killStop = false;
        killDistance = 2f; //Kill distance init(no change).
        camera = gameObject.transform.Find("Main Camera").GetComponent<Camera>();
        view = GetComponent<PhotonView>();
        StartCoroutine(Wait());
        StartCoroutine(StartCanKill());

        if(!canKill) //Start Disable.
        {

            gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Kill").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);

        }


    }


    private void Update()      
    {

        if(gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            killStop = true;

        }

        else if(!gameObject.GetComponent<VotingSystem>().votingSystemIsShowing)
        {

            killStop = false;

        }

        if(isImposter && canKill && view.isMine && !killStop && gameObject.GetComponent<Player_Data_Script>().playerStatusEnum != Player_Data_Script.Player_status.Dead){
            //ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camera.transform.position ,camera.transform.forward, out hit , killDistance))
            {
                if(hit.transform.gameObject.CompareTag("Player"))
                {

                    //print("HIIII");
                    //hit.transform.gameObject.GetComponent<Player_Data_Script>().playerStatusEnum = Player_Data_Script.Player_status.Dead; //Change Dead
                    //hit.transform.Find("ParticleSystem").gameObject.SetActive(true); //Blood particles.
                    //PhotonNetwork.Instantiate(corpsePath, hit.transform.position , Quaternion.identity , 0);
                    //PhotonNetwork.Destroy(hit.transform.gameObject);

                    if(hit.transform.gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate && Input.GetKeyDown(KeyCode.Q) && hit.transform.gameObject.GetComponent<Player_Data_Script>().playerStatusEnum != Player_Data_Script.Player_status.Dead)
                    {
                        hasKilled = true;
                        //hit.transform.gameObject.SetActive(false);
                        hit.transform.gameObject.GetComponent<Player_Data_Script>().playerStatusEnum = Player_Data_Script.Player_status.Dead; //Change Dead
                        GameObject temp = PhotonNetwork.Instantiate(corpsePath, hit.transform.position ,Quaternion.Euler(-90, 0, hit.transform.eulerAngles.y) , 0);
                        temp.GetComponent<SkinnedMeshRenderer>().material= hit.transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material;
                        temp.transform.Find("Corpse Backpack").gameObject.GetComponent<SkinnedMeshRenderer>().material = hit.transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().material;
                        gameObject.GetComponent<SoundHandler>().PlaySFX("Die");
                        canKill = false;
                        StartCoroutine(CanKill());
                        view.RPC("SetDead" , PhotonTargets.All , hit.transform.gameObject.GetComponent<PhotonView>().viewID);            ////////SENDING DATA.
                        float r = temp.GetComponent<SkinnedMeshRenderer>().material.color.r;
                        float g = temp.GetComponent<SkinnedMeshRenderer>().material.color.g;
                        float b = temp.GetComponent<SkinnedMeshRenderer>().material.color.b;
                        view.RPC("SyncColor" , PhotonTargets.Others , r , g , b);             /////////SENDING DATA.

                    }
                }
            }
        }

        if(isImposter)
        {

            if(!canKill)
            {

                gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Kill").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,0.4f);

            }

            if(canKill)
            {

                gameObject.transform.Find("Canvas").transform.Find("ImposterUIElement").transform.Find("Kill").gameObject.GetComponent<RawImage>().color = new Color(1,1,1,1f);

            }

        }

    }

    private void LateUpdate()
    {
        
        if(hasKilled)
            Array.Clear(corpseList,0,corpseList.Length);
            corpseList = GameObject.FindGameObjectsWithTag("Corpse");

    }

    private IEnumerator Wait()
    {

        yield return new WaitForSeconds(5f);
        //if(!view.isMine)
        //{

           // gameObject.GetComponent<Player_Kill>().enabled = false;

        //}
        if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
        {

            isImposter = true;

        }

    }

    private IEnumerator StartCanKill()
    {

        yield return new WaitForSeconds(10f);
        canKill = true;

    }

    private IEnumerator CanKill()
    {

        yield return new WaitForSeconds(20f);
        canKill = true;

    }

    [PunRPC]
    private void SyncColor(float red , float green , float blue)
    {

        StartCoroutine(WaitForKill(red , green , blue));

    }

    [PunRPC]
    private void SetDead(int viewID)
    {

        GameObject player = PhotonView.Find(viewID).gameObject;
        player.GetComponent<Player_Data_Script>().playerStatusEnum = Player_Data_Script.Player_status.Dead; 
        player.transform.Find("MainPlayer").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        player.transform.Find("MainPlayer").transform.Find("Back Pack").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        //player.transform.Find("Ghost High Main").gameObject.SetActive(true);

    }

    private IEnumerator WaitForKill(float a , float b , float c)
    {

        yield return new WaitForSeconds(1f);

        corpseList[corpseList.Length-1].GetComponent<SkinnedMeshRenderer>().material.color = new Color(a, b, c , 1);
        corpseList[corpseList.Length-1].transform.Find("Corpse Backpack").gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(a, b, c , 1);

    }
    
    /**private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate && hasPressedKill)
            {

                other.gameObject.GetComponent<Player_Data_Script>().playerStatusEnum = Player_Data_Script.Player_status.Dead; //Change Dead
                other.gameObject.transform.Find("ParticleSystem").gameObject.SetActive(true); //Blood particles.
                PhotonNetwork.Instantiate(corpsePath, other.gameObject.transform.position , Quaternion.identity , 0);
                PhotonNetwork.Destroy(other.gameObject);

            }
        }

    }**/

}
