using Game_Settings_Net;
using UnityEngine;
using System.Collections;

public class AssignImposterData : Photon.MonoBehaviour
{
    
    //~PhotonView:
    private PhotonView view;


    [PunRPC]
    private void SetNumImposters(byte count)//Sets the number of imposters in the game in the other devices.
    {

        Game_Settings_Net.RoomSettings.maxImposters = count;

    }

    private void Start()
    {
    
        view = GetComponent<PhotonView>();
        StartCoroutine(StartAssign());

    }

    private IEnumerator StartAssign()
    {

        yield return new WaitForSeconds(1f);

        if(PhotonNetwork.isMasterClient) // if master client, send rpc
        {

            view.RPC("SetNumImposters" , PhotonTargets.AllBuffered , Game_Settings_Net.RoomSettings.maxImposters);

        }

    }
    
}
