using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LeaveGame : Photon.MonoBehaviour
{

    //Bools:
    public bool checker;

    private void Start()
    {

        checker = false;

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            checker = true;

            if(checker)
            {

                Leave();
                checker = false;
                

            }


        }

    }

    public void Leave()
    {

        SceneManager.LoadScene("MainStartScreen");
        PhotonNetwork.LeaveRoom();

    }

}
