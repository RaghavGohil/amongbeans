using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class SpawnMainPlayer : MonoBehaviour
{

    //~Integers:
    private int RandomNumberForSpawnSet;

    //~PhotonView:
    private PhotonView view;

    //~GameObjects:
    public GameObject playerGameObject{ get; private set; }
    public GameObject[] playersInGame;

    //~Strings;
    [Header("Input Player Access:")]
    [SerializeField]
    private string playerGameObjectAccessString;

    //~Lists;
    [SerializeField]
    private List<GameObject> playerSpawnersInStarterScene = new List<GameObject>();

    private void Start()
    {
        
        RandomNumberForSpawnSet = Random.Range(0,playerSpawnersInStarterScene.Count);
        view = GetComponent<PhotonView>();
        //if(PhotonNetwork.isMasterClient)
        playerGameObject = PhotonNetwork.Instantiate(playerGameObjectAccessString, playerSpawnersInStarterScene[RandomNumberForSpawnSet].transform.position , Quaternion.identity , 0);

    }

    private void LateUpdate()
    {
        
        playersInGame = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0;i<playersInGame.Length;i++)
        {

            DontDestroyOnLoad(playersInGame[i]);

        }

    }
}
