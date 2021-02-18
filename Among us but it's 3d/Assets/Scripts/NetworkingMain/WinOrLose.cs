using UnityEngine;
using Game_Settings_Net;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinOrLose : Photon.MonoBehaviour
{

    //~GameObjects;
    private GameObject[] playersInGame;
    //~Bools:
    public bool stopSound;
    private bool Win;
    private bool Lose;
    private bool check;
    //~Integers:
    private int numImposters;

    private void Start()
    {

        stopSound = false;
        playersInGame = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(WaitForData());
        Win = false;
        Lose = false;
        check = true;

    }


    private void Play()
    {

        if(Win)
        {

            gameObject.GetComponent<SoundHandler>().PlaySFX("Win");

        }

        if(Lose)
        {

            gameObject.GetComponent<SoundHandler>().PlaySFX("Lose");

        }

        Win = false;
        Lose = false;

    }

    private IEnumerator WaitForScene()
    {

        yield return new WaitForSeconds(7f);
        StartCoroutine(LeaveRoom());
        Cursor.visible = true;
        Cursor.lockState =  CursorLockMode.Confined;
        SceneManager.LoadScene("MainStartScreen");

    }

    private IEnumerator LeaveRoom()
    {

        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.inRoom)
            yield return null;
        
        SceneManager.LoadScene("MainStartScreen");

    }

    private void Update()
    {

        Play();
        if(!check)
        {

            StartCoroutine(WaitForScene());

        }

        int crewmateCount = 0;
        int imposterCount = 0;

        for(int i = 0; i<playersInGame.Length;i++)
        {
            if((playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate) && playersInGame[i].GetComponent<Player_Data_Script>().playerStatusEnum != Player_Data_Script.Player_status.Dead)
            {

                crewmateCount++;

            }
            if((playersInGame[i].GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter) && playersInGame[i].GetComponent<Player_Data_Script>().playerStatusEnum == Player_Data_Script.Player_status.Dead)
            {

                imposterCount++;

            }

        }

        if(check){
        //Imposters:
            if(numImposters == 1)
            {

                if(crewmateCount < 2)
                {
                    
                    if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
                    {
                        stopSound = true;
                        
                        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("You Lose").gameObject.SetActive(true);
                        Lose = true;
                        check = false;

                    }

                    if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                    {
                        stopSound = true;
                        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Victory").gameObject.SetActive(true);
                        Win = true;
                        check = false;

                    }

                }

            }

            if(numImposters == 2)
            {

                if(crewmateCount < 3)
                {
                    
                    if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
                    {
                        stopSound = true;
                        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("You Lose").gameObject.SetActive(true);
                        Lose = true;
                        check = false;

                    }

                    if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                    {
                        stopSound = true;
                        gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Victory").gameObject.SetActive(true);
                        Win = true;
                        check = false;

                    }

                }

            }

            //Crewmates:
            if(imposterCount == 1 && numImposters == 1)
            {
                
                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
                {
                    stopSound = true;
                    gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Victory").gameObject.SetActive(true);
                    Win = true;
                    check = false;


                }

                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                {
                    stopSound = true;
                    gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("You Lose").gameObject.SetActive(true);
                    Lose = true;
                    check = false;

                }

            }

            if(imposterCount == 2 && numImposters == 2)
            {
                
                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
                {
                    stopSound = true;
                    gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Victory").gameObject.SetActive(true);
                    Win = true;
                    check = false;


                }

                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                {
                    stopSound = true;
                    gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("You Lose").gameObject.SetActive(true);
                    Lose = true;
                    check = false;

                }

            }

            if(gameObject.GetComponent<TotalTasks>().taskSlider.GetComponent<Slider>().value == gameObject.GetComponent<TotalTasks>().taskSlider.GetComponent<Slider>().maxValue)
            {
                
                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Crewmate)
                {
                    stopSound = true;
                    gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("Victory").gameObject.SetActive(true);
                    Win = true;
                    check = false;


                }

                if(gameObject.GetComponent<Player_Data_Script>().playerTypeEnum == Player_Data_Script.Player_type.Imposter)
                {
                    stopSound = true;
                    gameObject.transform.Find("Canvas").transform.Find("Panels").transform.Find("You Lose").gameObject.SetActive(true);
                    Lose = true;
                    check = false;

                }

            }
        }

    }

    private IEnumerator WaitForData()
    {

        yield return new WaitForSeconds(1f);
        numImposters = (int)Game_Settings_Net.RoomSettings.maxImposters;

    }

}
