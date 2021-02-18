using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Player_Position : MonoBehaviour
{

    //~List:
    [SerializeField]
    private List<GameObject> spawners = new List<GameObject>(); //Get the list
    //GameObjects:
    private GameObject[] playersInGame;

    private void Start()
    {

        SpawnPlayers();

    } 

    public void SpawnPlayers()
    {

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0;i<playersInGame.Length;i++)
        {

            playersInGame[i].transform.position = spawners[i].transform.position;

        }

    }

}
