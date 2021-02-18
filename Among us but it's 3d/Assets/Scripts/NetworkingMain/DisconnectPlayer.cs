using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;   
public class DisconnectPlayer : Photon.MonoBehaviour
{
    
    private void OnDisconnectedFromPhoton()
    {

        SceneManager.LoadScene("MainStartScreen");

    }

    private void OnPhotonPlayerDisconnected()
    {

        StartCoroutine(LeaveRoom());

        if(PhotonNetwork.isMasterClient)
        {

            SceneManager.LoadScene("MainStartScreen");
            PhotonNetwork.LeaveRoom();

        }


    }

    private IEnumerator LeaveRoom()
    {

        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.inRoom)
            yield return null;
        
        SceneManager.LoadScene("MainStartScreen");

    }

}
