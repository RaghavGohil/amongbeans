using UnityEngine;

public class RestrictPlayers : MonoBehaviour
{
    
    private void Start()
    {

        RestrictRooms();

    }

    private void RestrictRooms()
    {

        PhotonNetwork.room.open = false;
        PhotonNetwork.room.visible = false;

    }

}
