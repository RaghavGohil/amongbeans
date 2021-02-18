using UnityEngine;
using UnityEngine.SceneManagement;

public class AssignScripts : Photon.MonoBehaviour
{

    //GameObjects:
    [SerializeField]
    private GameObject[] playersInGame;
    //[SerializeField]
    //private GameObject[] minimapCameras; //Get main minimapcam

    private void OnPhotonPlayerConnected(PhotonPlayer other)
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < playersInGame.Length;i++){

            if(SceneManager.GetActiveScene().name == "MainGame")
            {

                if(playersInGame[i].GetComponent<Minimap_Controller>() == null)
                {

                    playersInGame[i].AddComponent(typeof(Minimap_Controller));

                    playersInGame[i].AddComponent(typeof(Assigner));

                    playersInGame[i].AddComponent(typeof(RoleRevealAssign));

                    playersInGame[i].AddComponent(typeof(EnableGUIOnPlay));

                    playersInGame[i].AddComponent(typeof(Enable_GUI));

                    playersInGame[i].AddComponent(typeof(VotingSystem));

                    playersInGame[i].AddComponent(typeof(Player_Kill));

                    playersInGame[i].AddComponent(typeof(ReportBodies));

                    playersInGame[i].AddComponent(typeof(MeetingButton));

                    playersInGame[i].AddComponent(typeof(TotalTasks));

                    playersInGame[i].AddComponent(typeof(AssignRandomTasks));

                    playersInGame[i].AddComponent(typeof(WIringTask));

                    playersInGame[i].AddComponent(typeof(DownloadTask));

                    playersInGame[i].AddComponent(typeof(ChangeTemperature));

                    playersInGame[i].AddComponent(typeof(SheildsTask));

                    playersInGame[i].AddComponent(typeof(StartReactor));

                    playersInGame[i].AddComponent(typeof(WinOrLose));

                    playersInGame[i].AddComponent(typeof(RestrictPlayers));

                    playersInGame[i].GetComponent<PhotonView>().ObservedComponents.Add(playersInGame[i].GetComponent<VotingSystem>());

                }

            }

        }

    }

    private void Start() //If the name of the scene is "MainGame" , it will assign the MiniMap Controller.
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < playersInGame.Length;i++){

            if(SceneManager.GetActiveScene().name == "MainGame" && playersInGame[i].GetComponent<Minimap_Controller>() == null)
            {
                
                playersInGame[i].AddComponent(typeof(Minimap_Controller));

                playersInGame[i].AddComponent(typeof(Assigner));

                playersInGame[i].AddComponent(typeof(RoleRevealAssign));

                playersInGame[i].AddComponent(typeof(EnableGUIOnPlay));

                playersInGame[i].AddComponent(typeof(Enable_GUI));

                playersInGame[i].AddComponent(typeof(VotingSystem));

                playersInGame[i].AddComponent(typeof(Player_Kill));

                playersInGame[i].AddComponent(typeof(ReportBodies));

                playersInGame[i].AddComponent(typeof(MeetingButton));

                playersInGame[i].AddComponent(typeof(TotalTasks));

                playersInGame[i].AddComponent(typeof(AssignRandomTasks));

                playersInGame[i].AddComponent(typeof(WIringTask));

                playersInGame[i].AddComponent(typeof(DownloadTask));

                playersInGame[i].AddComponent(typeof(ChangeTemperature));

                playersInGame[i].AddComponent(typeof(SheildsTask));

                playersInGame[i].AddComponent(typeof(StartReactor));

                playersInGame[i].AddComponent(typeof(WinOrLose));

                playersInGame[i].AddComponent(typeof(RestrictPlayers));

                playersInGame[i].GetComponent<PhotonView>().ObservedComponents.Add(playersInGame[i].GetComponent<VotingSystem>());

            }

        }

    }

}
