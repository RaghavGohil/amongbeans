using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GetPlayerList : Photon.MonoBehaviour
{

    //UI_TEXT:
    public Text status;
    
    private void Start()
    {

        status.text = "";
        for(int i = 0; i<PhotonNetwork.playerList.Length; i++) 
		{
            status.text += PhotonNetwork.playerList[i].name +"\n";
		}

    }

    private void OnPhotonPlayerConnected(PhotonPlayer other)
    {

        status.text += other.name +"\n";

        //for(int i = 0; i<PhotonNetwork.playerList.Length; i++) 
		//{
        //    status.text += PhotonNetwork.playerList[i].name +"\n";
		//}

    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {

        status.text = "";

        for(int i = 0; i<PhotonNetwork.playerList.Length; i++) 
		{
            status.text += PhotonNetwork.playerList[i].name +"\n";
		}

    }

}
