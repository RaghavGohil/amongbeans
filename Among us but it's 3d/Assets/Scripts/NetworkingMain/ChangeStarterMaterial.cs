using UnityEngine;
using Photon.Realtime;
using System.Collections;

public class ChangeStarterMaterial : Photon.MonoBehaviour
{
    
    //~Arrays:
    [SerializeField]
    private GameObject[] playersInGame;
    [SerializeField]
    private Material[] materials;

    //~PhotonView:
    private PhotonView view;

    //~GameObjects:

    private void Start()
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");
        view = gameObject.GetComponent<PhotonView>();//Get view

        StartCoroutine(Wait());
        

    }

    private void Update()
    {
        
        //-

    }

    private void OnPhotonPlayerConnected(PhotonPlayer other)
    {

        for(int i = 0;i<playersInGame.Length;i++)
        {
            if(PhotonNetwork.isMasterClient)
                view.RPC("AssignColors" , PhotonTargets.AllBuffered, i);
        }

    }

    private IEnumerator Wait()
    {

        yield return new WaitForSeconds(5f);

        for(int i = 0;i<playersInGame.Length;i++)
        {
            if(PhotonNetwork.isMasterClient)
                view.RPC("AssignColors" , PhotonTargets.AllBuffered, i);
        }

        //if(PhotonNetwork.isMasterClient)
        //        view.RPC("AssignColors" , PhotonTargets.All, playersInGame.Length);

        StartCoroutine(Wait());
    }

    [PunRPC]
    private void AssignColors(int num)
    {
        //Assigns colors for the main player:
        gameObject.transform.Find("MainPlayer").GetComponent<SkinnedMeshRenderer>().material = materials[num];
        //Get BackPack Mat:
        gameObject.transform.Find("MainPlayer").transform.Find("Back Pack").GetComponent<SkinnedMeshRenderer>().material= materials[num];
    }

}
