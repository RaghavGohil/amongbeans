using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ready_Script : Photon.MonoBehaviour
{
    
    //~GameObjects:
    [SerializeField]
    private GameObject status;
    private GameObject[] playersInGame;

    //~PhotonView:
    private PhotonView view;

    private void Start()
    {
        
        status = GameObject.FindWithTag("ReadyStatus");
        PhotonNetwork.automaticallySyncScene = true;
        view = GetComponent<PhotonView>();

        if(!PhotonNetwork.isMasterClient)
        {

            status.SetActive(false);

        }

    }

    private void LateUpdate()
    {

        //Set funcs:
        changeStatus();
        UpdatePlayers();

    }

    private void Update()
    {

        RemoveComponent();

    }

    private void UpdatePlayers()
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

    }

    [PunRPC]
    private void ReadyUpInGame(int count)
    {

        if(count == 1 && playersInGame.Length >= 4) //Make Greater Than
        {

            PhotonNetwork.LoadLevel("MainGame");

        }

    }

    private void changeStatus()
    {

        if(Input.GetKeyDown(KeyCode.E)) //Press the e button to ready up.
        {

            status.GetComponent<Text>().color = new Color(0,255f,0);
            status.GetComponent<Text>().text = "READY";

            if(PhotonNetwork.isMasterClient)
            {

                view.RPC("ReadyUpInGame" , PhotonTargets.MasterClient, 1);

            }
        }

    }
    
    private void RemoveComponent()
    {
        if(SceneManager.GetActiveScene().name != "StarterScene")
        {
            Destroy(this);
        }
    }

}
